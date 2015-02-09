using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Profiles : ContextComponent
    {
        public Profiles(IContext context)
            : base(context)
        {
        }

        public User CurrentUser { get; private set; }

        public IEnumerable<User> Users
        {
            get
            {
                return Context.Database
                    .GetCollection<UserModel>(DatabaseReferences.Users)
                    .FindAllAs<UserModel>()
                    .Select(x => new User(x));
            }
        }

        public void SwitchProfile(User user)
        {
            CurrentUser = user;
        }
    }
}
