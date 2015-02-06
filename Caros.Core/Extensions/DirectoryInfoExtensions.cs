using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static string Combine(this DirectoryInfo target, string fileName)
        {
            return Path.Combine(target.FullName, fileName);
        }
    }
}
