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
        Theme Theme { get; set; }
        Navigator Navigator { get; set; }
        RootViewModel RootViewModel { get; set; }
    }

    public class Context : IContext
    {
        public Theme Theme { get; set; }
        public Navigator Navigator { get; set; }
        public RootViewModel RootViewModel { get; set; }

        public static IContext Create(RootViewModel rootViewModel)
        {
            var context = new Context();
            context.Theme = new Theme(Theme.Style.Light);
            context.RootViewModel = rootViewModel;
            context.Navigator = new Navigator(context);
            return context;
        }
    }
}
