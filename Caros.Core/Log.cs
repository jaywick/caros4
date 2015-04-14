using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Caros.Core.Extensions;

namespace Caros.Core
{
    public static class Log
    {
        private const string Quote = "\"";

        private static string _logFile;
        private static JsonWriterSettings _jsonWriterSettings;

        static Log()
        {
            var fileName = DateTime.Now.ToString("yyyy-MM") + ".log";
            _logFile = Path.Combine(Context.Storage.LogsDirectory, fileName);

            _jsonWriterSettings = new JsonWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                MaxSerializationDepth = 2,
                NewLineChars = Environment.NewLine,
                OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict,
            };

            File.AppendAllText(_logFile, "");
            WriteLine("Logging Started");
        }

        public static void WriteLine(string message, params string[] args)
        {
            var dateStamp = DateTime.Now.ToString();
            File.AppendAllText(_logFile, dateStamp + "\t" + String.Format(message, args) + Environment.NewLine);
        }

        public static void WriteFault(Exception exception, string summary = null)
        {
            WriteLine("FAULT: {0}", summary ?? "Unexpected failure");
            Dump("exception", exception);
        }

        public static void Dump(string name, object @object)
        {
            var dumpId = Guid.NewGuid().ToString();
            var dumpPath = Path.Combine(Context.Storage.LogDumpDirectory, dumpId + ".dump");

            WriteLine("Dumping object graph of '{0}' to {1}", name, dumpId);

            File.WriteAllText(dumpPath, @object.ToJson(_jsonWriterSettings));
        }

        public static void HandleUnexpectedException(Exception exception, bool startRecovery = true)
        {
            Caros.Core.Log.WriteFault(exception, "Unhandled exception");

            if (!startRecovery)
                return;

            var source = exception.StackTrace.SplitByLines().FirstOrDefault() ?? "UNKNOWN";
            var argument = String.Format("\"{0} @ {1}\"", exception.Message.Replace(Quote, Quote + Quote), source);
            var recoveryExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recovery.exe");

            System.Diagnostics.Process.Start(recoveryExePath, argument);
        }
    }
}
