using Caros.Core.Data;
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
        private UserModel _model;

        public string Name { get; set; }
		
		public User(string name)
		{
			Name = name;
		}

        public User(UserModel model)
        {
            _model = model;

            Name = model.Name;
        }
    }
}
