using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caros.Core.Context
{
    public class ContextComponent
    {
        public IContext Context { get; set; }

        public ContextComponent(IContext context)
        {
            this.Context = context;
        }
    }
}
