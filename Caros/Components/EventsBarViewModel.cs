using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Core.Services;
using Caros.Core;

namespace Caros.Components
{
    public class EventsBarViewModel : PropertyChangedBase
    {
        public event System.Action RequestClose;

        public BindableCollection<NamedAction> Tasks { get; set; }
        public EventPost Post { get; private set; }

        public EventsBarViewModel(EventPost post)
        {
            Post = post;
            Tasks = new BindableCollection<NamedAction>();
            Tasks.AddRange(post.Tasks);
        }

        public void Close()
        {
            RequestClose.Invoke();
        }

        public void LaunchTask(NamedAction task)
        {
            task.Action.Invoke();
        }
    }
}
