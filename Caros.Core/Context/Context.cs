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
        Database Database { get; set; }
        Services Services { get; set; }
        Profiles Profiles { get; set; }
    }

    public class Context : IContext
    {
        public RootViewModel RootViewModel { get; set; }
        public Theme Theme { get; set; }
        public Navigator Navigator { get; set; }
        public Storage Storage { get; set; }
        public Database Database { get; set; }
        public Services Services { get; set; }
        public Profiles Profiles { get; set; }

        public static IContext Create(RootViewModel rootViewModel)
        {
            var context = new Context();
            context.RootViewModel = rootViewModel;
            context.Theme = new Theme(context);
            context.Navigator = new Navigator(context);
            context.Storage = new Storage(context);
            context.Database = new Database(context);
            context.Services = new Services(context);
            context.Profiles = new Profiles(context);
            return context;
        }
    }
}
