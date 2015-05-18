using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caros.Core.Extensions;
using Caros.Core.Data;
using Caros.Core.UI;

namespace Caros.Core.Context
{
    public interface IProfiles : IContextComponent
    {
        User CurrentUser { get; }
        IEnumerable<Caros.Core.User> Users { get; }

        Reference<PageViewModel> HomePage { get; set; }
        Reference<PageViewModel> SplashPage { get; set; }

        void Switch(string name);
        void Switch(User user);
        bool NameExists(string name);
        void Add(string name);
    }

    public class Profiles : IProfiles
    {
        public virtual IContext Context { get; set; }

        public Reference<PageViewModel> HomePage { get; set; }
        public Reference<PageViewModel> SplashPage { get; set; }

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

        public void Switch(string name)
        {
            var user = Users.SingleOrDefault(x => x.Name.Equals(name,StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
                throw new Exception("User not found by name");

            Switch(user);
        }

        public async void Switch(User user)
        {
            CurrentUser = user;
            Context.Navigator.Visit(SplashPage);
            await Task.Delay(2000);
            Context.Navigator.Visit(HomePage);
        }

        public bool NameExists(string name)
        {
            return Users.Any(x => x.Name == name);
        }

        public void Add(string name)
        {
            if (NameExists(name))
                throw new Exception("Username exists!");

            Context.Database.Insert(new UserModel
            {
                Name = name,
                Added = DateTime.Now,
            });
        }
    }
}
