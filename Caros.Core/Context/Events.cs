using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using Caros.Core.UI;

namespace Caros.Core.Context
{
    public interface IEvents : IContextComponent
    {
        event Action<EventPost> OnToast;

        List<EventPost> Items { get; set; }

        void Post(string title, string summary, IEnumerable<NamedAction> tasks);
    }

    public class Events : IEvents
    {
        public event Action<EventPost> OnToast;

        public List<EventPost> Items { get; set; }
        public virtual IContext Context { get; set; }

        public Events(IContext context)
        {
            Context = context;
            Items = new List<EventPost>();
        }

        public void Post(string title, string summary, IEnumerable<NamedAction> tasks)
        {
            var post = new EventPost(title, summary, tasks);
            Items.Add(post);
            OnToast.Invoke(post);
        }
    }
}
