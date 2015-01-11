using Caros.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Context
{
    public interface IContext
    {
        Theme Theme { get; set; }
        ViewModel RootViewModel { get; set; }
    }

    public class Context : IContext
    {
        public Theme Theme { get; set; }
        public ViewModel RootViewModel { get; set; }

        internal static IContext Create(ViewModel rootViewModel)
        {
            return new Context
            {
                Theme = new Theme(),
                RootViewModel = rootViewModel,
            };
        }
    }
}
