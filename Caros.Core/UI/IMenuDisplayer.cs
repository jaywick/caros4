using Caliburn.Micro;
using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.UI
{
    public interface IMenuDisplayer
    {
        void LoadTasks(IEnumerable<NamedAction> tasks);
    }
}
