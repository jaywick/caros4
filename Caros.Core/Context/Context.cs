using Caros.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public interface IContext
    {
        ITheme Theme { get; set; }
        INavigator Navigator { get; set; }
        IStorage Storage { get; set; }
        IDatabase Database { get; set; }
        ServicesManager Services { get; set; }
        IProfiles Profiles { get; set; }
        IEnvironment Environment { get; set; }
        IClock Clock { get; set; }
    }

    public class ApplicationContext : IContext
    {
        public ITheme Theme { get; set; }
        public INavigator Navigator { get; set; }
        public IStorage Storage { get; set; }
        public IDatabase Database { get; set; }
        public ServicesManager Services { get; set; }
        public IProfiles Profiles { get; set; }
        public IEnvironment Environment { get; set; }
        public IClock Clock { get; set; }

        public static IContext Create()
        {
            var context = new ApplicationContext();
            context.Clock = new Clock(context);
            context.Environment = new Environment(context);
            context.Theme = new Theme(context);
            context.Navigator = new Navigator(context);
            context.Storage = new Storage(context);
            context.Database = new Database(context);
            context.Services = new ServicesManager(context);
            context.Profiles = new Profiles(context);
            return context;
        }
    }
}
