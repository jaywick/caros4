using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
	public class User
	{
        private UserModel x;

		public string Name { get; set; }
		
		public User(string name)
		{
			Name = name;
		}

        public User(UserModel x)
        {
            Name = x.Name;
        }
	}
}
