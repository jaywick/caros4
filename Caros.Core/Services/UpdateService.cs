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

        public Update CheckForUpdates()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return Update.None;

            var remoteVersion = GetRemoteVersion().ReleaseNumber;
            var currentVersion = Integration.VersionMeta.CurrentVersion.ReleaseNumber;

            if (remoteVersion <= currentVersion)
                return Update.None;

            return new Update(remoteVersion);
        }

        public VersionMeta.ReleaseVersion GetRemoteVersion()
        {
            var web = new System.Net.WebClient();
            var contents = web.DownloadString(UpdatesPathUrl + VersionPointerName);

            return new VersionMeta.ReleaseVersion(int.Parse(contents));
        }

        private void DownloadUpdate(string filename, string destination)
        {
            var web = new System.Net.WebClient();
            web.DownloadFile(UpdatesPathUrl + filename, destination);
        }

        public class Update
        {
            public bool Exists { get; private set; }
            public string DownloadAddress { get; private set; }
            public VersionMeta.ReleaseVersion Version { get; private set; }

            private static Update _none = new Update();
            public static Update None { get { return _none; } }

            private const string DownloadAddressFormat = "http://internal.jay-wick.com/caros/updates/{0}.caros-update";

            public Update(int releaseNumber)
            {
                Exists = true;
                Version = new VersionMeta.ReleaseVersion(releaseNumber);
                DownloadAddress = String.Format(DownloadAddressFormat, Version.ReleaseName);
            }

            private Update()
            {
                Exists = false;
            }
        }

        public static class Updater
        {
            public static void Start(UpdateService.Update update)
            {
                
            }
        }
    }
}
