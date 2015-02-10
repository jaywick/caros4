using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
	public class User
	{
        private UserModel x;

        public string Name { get; set; }
        public string UserCode { get; set; }
		
		public User(string name)
		{
			Name = name;
            UserCode = Crypto.GenerateMD5(name);
		}

        public User(UserModel x)
        {
            Name = x.Name;
            UserCode = x.UserCode;
        }
    }
}
