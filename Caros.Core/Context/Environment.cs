using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Caros.Core.Context
{
    public interface IEnvironment : IContextComponent
    {
        bool IsNight { get; }
    }

    public class Environment : IEnvironment
    {
        public virtual IContext Context { get; set; }

        public Environment(IContext context)
        {
            Context = context;
        }

        public bool IsNight
        {
            get { return Context.Clock.CurrentTime.Hour < 6 || Context.Clock.CurrentTime.Hour > 17; }
        }
    }
}
