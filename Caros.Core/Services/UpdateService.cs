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
        private const string UpdatesPathUrl = "http://inhouse.jaywick.io/caros/updates/";
        private const string PackageNameFormat = "r{0}.caros-update";
        private const string VersionPointerName = "version.pointer";

        private UpdateInfo _lastUpdate;

        public UpdateService(IContext context)
            : base(context)
        {
        }

        public override void Start()
        {
        }

        public async Task CheckForUpdates()
        {
            _lastUpdate = await GetLatestUpdateInfo();
        }

        private async Task<UpdateInfo> GetLatestUpdateInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                _lastUpdate = UpdateInfo.None;

            var remoteVersion = (await GetRemoteVersion()).ReleaseNumber;
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

        private async Task<ReleaseVersion> GetRemoteVersion()
        {
            var web = new System.Net.WebClient();
            var contents = await Task.Run(() => web.DownloadString(UpdatesPathUrl + VersionPointerName));

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

        public async Task DownloadUpdate()
        {
            var _lastPackage = await _lastUpdate.Download();
        }

        public async Task Deploy()
        {
            await Deployment.Deploy(_lastUpdate, Storage.BinariesDirectory);
        }

        public void Relaunch()
        {
            Deployment.Launch(Storage.BinariesDirectory);
            Application.Current.Shutdown();
        }
    }
}
