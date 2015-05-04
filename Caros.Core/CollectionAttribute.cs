using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class CollectionAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionAttribute(string name)
        {
            Name = name;
        }
    }
}
