using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;
using Caros.Core.Data;

namespace Caros.Core.Context
{
    public interface IProfiles
    {
        Caros.Core.User CurrentUser { get; }

        void SwitchProfile(Caros.Core.User user);

        System.Collections.Generic.IEnumerable<Caros.Core.User> Users { get; }
    }

    public class Profiles : ContextComponent, IProfiles
    {
        public Profiles(IContext context)
            : base(context)
        {
            CurrentUser = Users.First();
        }

        public User CurrentUser { get; private set; }

        public IEnumerable<User> Users
        {
            get
            {
                return Context.Database
                    .GetCollection<UserModel>(UserModel.CollectionName)
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
