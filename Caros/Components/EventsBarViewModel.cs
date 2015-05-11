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
using System.Windows.Threading;

namespace Caros.Components
{
    public class EventsBarViewModel : PropertyChangedBase
    {
        public event System.Action RequestClose;

        public BindableCollection<NamedAction> Tasks { get; set; }
        public EventPost Post { get; private set; }

        private DispatcherTimer _timer;

        public EventsBarViewModel(EventPost post)
        {
            Post = post;

            CreateTasks(post);
            StartTimer();
        }

        private void CreateTasks(EventPost post)
        {
            Tasks = new BindableCollection<NamedAction>();
            Tasks.AddRange(post.Tasks);
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += (s, e) => Close();
            _timer.Start();
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
