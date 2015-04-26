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
    public class NavigationBarViewModel : PropertyChangedBase
    {
        private IContext Context { get; set; }

        public NavigationBarViewModel(IContext context)
        {
            Context = context;
        }

        public void GoHome()
        {
            Context.Navigator.GoHome();
        }

        public void OpenMenu()
        {
            Context.Navigator.OpenMenu();
        }

        public void OpenSearch()
        {
            Context.Navigator.CurrentPage.OnSearch();
        }

        public void GoBack()
        {
            Context.Navigator.Return();
        }
    }
}
