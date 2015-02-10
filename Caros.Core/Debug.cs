using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public static class Debug
    {
        public static bool IsDebugging
        {
            get { return System.Diagnostics.Debugger.IsAttached; }
        }
    }
}
