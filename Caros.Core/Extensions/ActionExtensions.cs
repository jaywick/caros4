using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class ActionExtensions
    {
        public static void InvokeIfExists(this Action target)
        {
            if (target == null)
                return;

            target.Invoke();
        }

        public static void InvokeIfExists<T1>(this Action<T1> target, T1 arg1)
        {
            if (target == null)
                return;

            target.Invoke(arg1);
        }

        public static void InvokeIfExists<T1, T2>(this Action<T1, T2> target, T1 arg1, T2 arg2)
        {
            if (target == null)
                return;

            target.Invoke(arg1, arg2);
        }

        public static void InvokeIfExists<T1, T2, T3>(this Action<T1, T2, T3> target, T1 arg1, T2 arg2, T3 arg3)
        {
            if (target == null)
                return;

            target.Invoke(arg1, arg2, arg3);
        }

        public static void InvokeIfExists<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> target, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (target == null)
                return;

            target.Invoke(arg1, arg2, arg3, arg4);
        }
    }
}
