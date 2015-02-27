using Caros.CI.API;
using Caros.Core.Context;
using Caros.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Services
{
    [AutoStart]
    public class UpdateService : Service
    {
        private const string UpdatesPathUrl = "http://internal.jay-wick.com/caros/updates/";
        private const string PackageNameFormat = "r{0}.caros-update";
        private const string VersionPointerName = "version.pointer";

        public UpdateService(IContext context)
            : base(context)
        {
        }

        public override void Start()
        {
        }

        public UpdateInfo CheckForUpdates()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return UpdateInfo.None;

            var remoteVersion = GetRemoteVersion().ReleaseNumber;
            var currentVersion = Versioning.CurrentVersion.ReleaseNumber;

            if (remoteVersion <= currentVersion)
                return UpdateInfo.None;

            return new UpdateInfo(remoteVersion);
        }

        public ReleaseVersion GetRemoteVersion()
        {
            var web = new System.Net.WebClient();
            var contents = web.DownloadString(UpdatesPathUrl + VersionPointerName);

            return new ReleaseVersion(int.Parse(contents));
        }

        private void DownloadUpdate(string filename, string destination)
        {
            var web = new System.Net.WebClient();
            web.DownloadFile(UpdatesPathUrl + filename, destination);
        }
    }
}
