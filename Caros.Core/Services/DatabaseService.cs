using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Services
{
    public class DatabaseService : SystemService
    {
        private const string MongodPath = "c:/dev/mongodb/bin/mongod.exe";
        private const string MongodArguments = "--dbpath {0}";

        public DatabaseService(IContext context)
            : base(context)
        {
        }

        public override void Start()
        {
            var processInfo = new ProcessStartInfo
            {
                Arguments = String.Format(MongodArguments, Context.Storage.DataFolder),
                CreateNoWindow = true,
                FileName = MongodPath,
                WindowStyle = ProcessWindowStyle.Hidden,
            };
            
            Process.Start(processInfo);
        }
    }
}
