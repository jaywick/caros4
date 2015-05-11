using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class Device
    {
        public string ID;
        public DriveInfo Drive;

        public Device()
        {
        }

        public Device(string id, DriveInfo drive)
        {
            ID = id;
            Drive = drive;
        }

        public Device(DriveInfo drive)
        {
            Drive = drive;
            ID = new Caros.Core.Integration.UsbSerialNumber().GetSerialNumber(drive);
        }
    }
}
