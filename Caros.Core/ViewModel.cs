using Caliburn.Micro;
using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Contracts
{
    public class PageViewModel : PropertyChangedBase
    {
        public IContext Context { get; set; }

        public PageViewModel(IContext context)
        {
            Context = context;
        }
    }
}
