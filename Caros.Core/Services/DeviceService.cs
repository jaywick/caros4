using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Caros.Core.Context;
using Caros.Core.Extensions;

namespace Caros.Core.Services
{
    public class DeviceService : SystemService
    {
        public event Action<Device> DeviceAdded;
        public event Action<Device> DeviceRemoved;

        private ManagementEventWatcher _watcher;
        private List<DriveInfo> _drives;

        public DeviceService(IContext context)
            : base(context)
        { }

        public override void Start()
        {
            _drives = GetRemovableDrives();
            StartListener();
        }

        private void StartListener()
        {
            _watcher = new ManagementEventWatcher();
            _watcher.EventArrived += new EventArrivedEventHandler(Watcher_EventArrived);
            _watcher.Query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            _watcher.Start();
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            CompareDrives();
        }

        private void CompareDrives()
        {
            var drivesNow = GetRemovableDrives();

            var addedDrives = drivesNow.Except(_drives);
            var removedDrives = _drives.Except(drivesNow);

            if (addedDrives.Any())
            {
                foreach (var addedDrive in addedDrives)
                {
                    if (DeviceAdded != null)
                        DeviceAdded.Invoke(new Device(addedDrive));

                    DisplayNewDeviceToast(addedDrive);
                }
            }

            if (removedDrives.Any())
            {
                foreach (var removedDrive in removedDrives)
                {
                    if (DeviceRemoved != null)
                        DeviceRemoved.Invoke(new Device(removedDrive));
                }
            }
        }

        private void DisplayNewDeviceToast(DriveInfo addedDrive)
        {
            Context.Events.Post("New device", "A new device has been connected",
                                new NamedAction("Create Profile", null),
                                new NamedAction("Import Music", null));
        }

        private List<DriveInfo> GetRemovableDrives()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.IsReady && x.DriveType == DriveType.Removable)
                .ToList();
        }

    }
}
