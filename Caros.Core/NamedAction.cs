using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class NamedAction
    {
        public string Name { get; set; }
        public Action Action { get; set; }

        public NamedAction(string name, Action action)
        {
            Name = name;
            Action = action;
        }
    }

    public class NamedActionBuilder
    {
        public List<NamedAction> Items { get; set; }

        public NamedActionBuilder()
        {
            Items = new List<NamedAction>();
        }

        public void Add(string name, Action action)
        {
            Items.Add(new NamedAction(name, action));
        }
    }
}
