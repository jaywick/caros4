using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Music;
using Caros.Core;

namespace Caros.Pages
{
    public class MenuPageViewModel : PageViewModel, IMenuDisplay
    {
        public BindableCollection<NamedAction> Tasks { get; set; }

        public MenuPageViewModel(IContext context)
            : base(context)
        {
            Tasks = new BindableCollection<NamedAction>();
        }

        public void LoadTasks(IEnumerable<NamedAction> tasks)
        {
            Tasks.Clear();
            Tasks.AddRange(tasks);
        }

        public void LaunchTask(NamedAction dataContext)
        {
            dataContext.Action.Invoke();
        }
    }
}
