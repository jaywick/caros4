using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Diagnostics;

namespace Caros.Core.Integration
{
    // Thanks to http://www.dotnetcurry.com/showarticle.aspx?ID=431
    public class Volume
    {
        private const int AppCommand_Volume_Mute = 0x80000;
        private const int AppCommand_Volume_Up = 0xA0000;
        private const int AppCommand_Volume_Down = 0x90000;
        private const int AppCommand = 0x319;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private IntPtr handle;
        private const short SCALE_FACTOR = 2;

        private static Volume instance = new Volume();
        public static Volume Instance { get { return instance; } }

        private Volume()
        {
            this.handle = Process.GetCurrentProcess().MainWindowHandle;
        }

        public void Increase()
        {
            for (int i = 0; i < SCALE_FACTOR; i++)
                SendMessage(AppCommand_Volume_Up);
        }

        public void Decrease()
        {
            for (int i = 0; i < SCALE_FACTOR; i++)
                SendMessage(AppCommand_Volume_Down);
        }

        public void Mute()
        {
            SendMessage(AppCommand_Volume_Mute);
        }

        private void SendMessage(int message)
        {
            SendMessageW(handle, AppCommand, handle, (IntPtr)message);
        }
    }
}