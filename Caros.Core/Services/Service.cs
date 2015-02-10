using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Services
{
    public abstract class Service
    {
        public IContext Context { get; set; }

        public Service(IContext context)
        {
            this.Context = context;
        }

        public abstract void Start();
    }

    public abstract class SystemService : Service
    {
        public SystemService(IContext context)
            : base(context)
        {
        }
    }
}
