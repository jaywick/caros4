using Caliburn.Micro;
using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.UI
{
    public class PageViewModel : ViewModel
    {
        public virtual IContext Context { get; set; }

        public bool ShowInHistory { get; set; }

        public virtual void OnVisit(bool isFirst) { }

        public virtual void OnExtra(NamedActionBuilder builder) { }

        public virtual void OnSearch() { }

        public PageViewModel(IContext context)
        {
            Context = context;
        }
    }
}
