using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caros.Core.Context
{
    public interface IContextComponent
    {
        IContext Context { get; set; }
    }
}
