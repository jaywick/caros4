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
        public virtual IContext Context { get; set; }

        public Service(IContext context)
        {
            this.Context = context;
        }

        public abstract void Start();
    }

    /// <summary>
    /// System services start automatically with the application
    /// </summary>
    public abstract class SystemService : Service
    {
        public SystemService(IContext context)
            : base(context)
        {
        }
    }
}
