using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using Caros.Core.UI;
using Caros.Core.Extensions;

namespace Caros.Core.Context
{
    public interface IEvents : IContextComponent
    {
        event Action<EventPost> OnPost;
        event Action<string> OnTip;

        List<EventPost> Items { get; set; }

        void Post(string title, string summary, IEnumerable<NamedAction> tasks);
        void Post(string title, string summary, params NamedAction[] tasks);
        void Tip(string message);
    }

    public class Events : IEvents
    {
        public event Action<EventPost> OnPost;
        public event Action<string> OnTip;

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
            OnPost.InvokeIfExists(post);
        }

        public void Post(string title, string summary, params NamedAction[] tasks)
        {
            Post(title, summary, tasks.AsEnumerable());
        }

        public void Tip(string message)
        {
            OnTip.InvokeIfExists(message);
        }
    }
}
