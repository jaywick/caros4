using Caliburn.Micro;
using Caros.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Contracts
{
    public class ViewModel : PropertyChangedBase, IPage
    {
        public IContext Context { get; set; }

        public ViewModel(IContext context)
        {
            Context = context;
        }
    }
}
