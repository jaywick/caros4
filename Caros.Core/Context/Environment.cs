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
    public interface IEnvironment
    {
        bool IsNight { get; }
    }

    public class Environment : ContextComponent, IEnvironment
    {
        public Environment(IContext context)
            : base(context)
        {
        }

        public bool IsNight
        {
            get { return DateTime.Now.Hour > 6 && DateTime.Now.Hour < 17; }
        }
    }
}
