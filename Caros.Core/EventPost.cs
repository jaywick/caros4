using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public class EventPost
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public IEnumerable<NamedAction> Tasks { get; set; }

        public EventPost(string title, string summary, IEnumerable<NamedAction> tasks)
        {
            Title = title;
            Summary = summary;
            Tasks = tasks.ToList();
        }
    }
}
