using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Integration
{
    public static class HostMachine
    {
        [Conditional("RELEASE")]
        public static void Shutdown()
        {
            Process.Start("shutdown", "/s /t 0");
        }

        public static void HideInterfaceArtifacts()
        {
            Taskbar.Hide();
        }

        private static class Taskbar
        {
            [DllImport("user32.dll")]
            private static extern int FindWindow(string className, string windowText);

            [DllImport("user32.dll")]
            private static extern int FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

            [DllImport("user32.dll")]
            private static extern int ShowWindow(int hwnd, int command);

            private static readonly string SystemTrayClassName = "Shell_TrayWnd";
            private static readonly IntPtr StartOrbClassName = (IntPtr)0xC017; // thanks to http://www.codeproject.com/Articles/38372/A-Simplified-Solution-for-Hiding-the-Taskbar-and-S
            private static readonly int HideCommand = 0;
            private static readonly int ShowCommand = 1;

            static Taskbar()
            {
                IsShown = true;
            }

            private static IEnumerable<int> Handles
            {
                get
                {
                    yield return FindWindow(SystemTrayClassName, null);
                    yield return FindWindowEx(IntPtr.Zero, IntPtr.Zero, StartOrbClassName, null);
                }
            }

            public static bool IsShown { get; private set; }

            /// <summary>
            /// Release Only: Shows Windows taskbar if hidden
            /// </summary>
            [Conditional("RELEASE")]
            public static void Show()
            {
                if (IsShown)
                    return;

                foreach (var item in Handles)
                {
                    ShowWindow(item, ShowCommand);
                }

                IsShown = true;
            }

            /// <summary>
            /// Release Only: Hides the windows taskbar if shown
            /// </summary>
            [Conditional("RELEASE")]
            public static void Hide()
            {
                if (!IsShown)
                    return;

                foreach (var item in Handles)
                {
                    ShowWindow(item, HideCommand);
                }

                IsShown = false;
            }
        }
    }
}
