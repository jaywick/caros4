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
    public interface IClock : IContextComponent
    {
        DateTime CurrentTime { get; }
    }

    public class Clock : IClock
    {
        public virtual IContext Context { get; set; }

        public Clock(IContext context)
        {
            Context = context;
        }

        public virtual DateTime CurrentTime
        {
            get { return DateTime.Now; }
        }
    }
}
