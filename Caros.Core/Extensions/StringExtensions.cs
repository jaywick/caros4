using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitByLines(this string target)
        {
            return target.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string IfEmpty(this string target, string defaultValue)
        {
            if (String.IsNullOrWhiteSpace(target))
                return defaultValue;

            return target;
        }
    }
}
