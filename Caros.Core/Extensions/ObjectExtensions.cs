using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static TReturn IfNotNull<TReturn, T>(this T target, Func<T, TReturn> selector)
            where T : class
            where TReturn: class
        {
            if (target != null)
                return selector(target);

            return null;
        }
    }
}
