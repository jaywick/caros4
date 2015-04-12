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
    public interface IClock
    {
        DateTime CurrentTime { get; }
    }

    public class Clock : ContextComponent, IClock
    {
        public Clock(IContext context)
            : base(context)
        {
        }

        public DateTime CurrentTime
        {
            get { return DateTime.Now; }
        }
    }
}
