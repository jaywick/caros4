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
        User CurrentUser { get; }
        IEnumerable<Caros.Core.User> Users { get; }

        void SwitchProfile(User user);
        void Add(string name);
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

        public void Add(string name)
        {
            Context.Database.Insert(new UserModel
            {
                Name = name,
                UserCode = String.Format("{0}_{1}", name, Guid.NewGuid()),
                Added = DateTime.Now,
            });
        }
    }
}
