using Caros.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public static class IntegrationServices
    {
        public static void Start()
        {
            DatabaseServer.Start();
            HostMachine.HideInterfaceArtifacts();
        }
    }
}
