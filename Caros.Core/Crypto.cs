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
            return MD5.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(message))
                .Select(x => x.ToString("x2"))
                .Aggregate((a, b) => a + b);
        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
