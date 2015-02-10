using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class Crypto
    {
        public static string GenerateMD5(string message)
        {
            var hash = MD5.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(message))
                .Select(x => x.ToString("x2"))
                .Aggregate((a, b) => a + b);

            return String.Format("{0}-{1}-{2}-{3}-{4}", new[] { hash.Substring(0, 8), hash.Substring(8, 4), hash.Substring(12, 4), hash.Substring(16, 4), hash.Substring(20, 12) });
        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
