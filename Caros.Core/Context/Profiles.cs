using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;
using Caros.Core.Data;

namespace Caros.Core.Context
{
    public interface IProfiles : IContextComponent
    {
        Caros.Core.User CurrentUser { get; }

        void SwitchProfile(Caros.Core.User user);

        System.Collections.Generic.IEnumerable<Caros.Core.User> Users { get; }
    }

    public class Profiles : IProfiles
    {
        public virtual IContext Context { get; set; }

        public Profiles(IContext context)
        {
            Context = context;
            CurrentUser = Users.FirstOrDefault() ?? new User("Default");
        }

        public User CurrentUser { get; private set; }

        public IEnumerable<User> Users
        {
            get
            {
                return Context.Database
                    .Load<UserModel>()
                    .Select(x => new User(x));
            }
        }

        public void SwitchProfile(User user)
        {
            CurrentUser = user;
        }
    }
}
