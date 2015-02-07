using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public abstract class Service
    {
        public IContext Context { get; set; }

        public Service(IContext context)
        {
            this.Context = context;
        }
    }
}
