using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Integration
{
    public static class DatabaseServer
    {
        private const string MongodPath = "c:/dev/mongodb/bin/mongod.exe";
        private const string MongodArguments = "--dbpath {0}";

        public static void Start()
        {
            var processInfo = new ProcessStartInfo();
            processInfo.Arguments = String.Format(MongodArguments, Storage.DataDirectory);
            processInfo.FileName = MongodPath;

            if (!Debug.IsDebugging)
            {
                processInfo.CreateNoWindow = true;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            
            Process.Start(processInfo);
        }
    }
}
