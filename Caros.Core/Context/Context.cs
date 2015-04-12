using Caros.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public interface IContext
    {
        RootViewModel RootViewModel { get; set; }
        Theme Theme { get; set; }
        Navigator Navigator { get; set; }
        Storage Storage { get; set; }
        IDatabase Database { get; set; }
        ServicesManager Services { get; set; }
        IProfiles Profiles { get; set; }
        IEnvironment Environment { get; set; }
    }

    public class ApplicationContext : IContext
    {
        public RootViewModel RootViewModel { get; set; }
        public Theme Theme { get; set; }
        public Navigator Navigator { get; set; }
        public Storage Storage { get; set; }
        public IDatabase Database { get; set; }
        public ServicesManager Services { get; set; }
        public IProfiles Profiles { get; set; }
        public IEnvironment Environment { get; set; }

        public static IContext Create(RootViewModel rootViewModel)
        {
            var context = new ApplicationContext();
            context.RootViewModel = rootViewModel;
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
