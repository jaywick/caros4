using Caros.CI.API;
using Caros.Core.Context;
using Caros.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros.Core.Services
{
    public class UpdateService : SystemService
    {
        private const string UpdatesPathUrl = "http://internal.jay-wick.com/caros/updates/";
        private const string PackageNameFormat = "r{0}.caros-update";
        private const string VersionPointerName = "version.pointer";

        private UpdateInfo _lastUpdate;
        private string _lastPackage;

        public UpdateService(IContext context)
            : base(context)
        {
        }

        public override void Start()
        {
        }

        public void CheckForUpdates()
        {
            _lastUpdate = GetLatestUpdateInfo();
        }

        private UpdateInfo GetLatestUpdateInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                _lastUpdate = UpdateInfo.None;

            var remoteVersion = GetRemoteVersion().ReleaseNumber;
            var currentVersion = ClientVersion.CurrentVersion.ReleaseNumber;

            if (remoteVersion <= currentVersion)
                return UpdateInfo.None;

            return new UpdateInfo(remoteVersion);
        }

        public string CurrentReleaseName
        {
            get { return ClientVersion.CurrentVersion.ReleaseName; }
        }

        public int CurrentReleaseNumber
        {
            get { return ClientVersion.CurrentVersion.ReleaseNumber; }
        }

        private ReleaseVersion GetRemoteVersion()
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

        public bool IsUpdateAvailable
        {
            get { return _lastUpdate.Exists; }
        }

        public void DownloadUpdate()
        {
            var _lastPackage = _lastUpdate.Download();
        }

        public void Deploy()
        {
            Deployment.Deploy(_lastUpdate, Storage.BinariesDirectory);
        }

        public void Relaunch()
        {
            Deployment.Launch(Storage.BinariesDirectory);
            Application.Current.Shutdown();
        }
    }
}
