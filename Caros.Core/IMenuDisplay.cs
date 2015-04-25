using Caliburn.Micro;
using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Contracts
{
    public interface IMenuDisplay
    {
        void LoadTasks(IEnumerable<NamedAction> tasks);
    }
}
