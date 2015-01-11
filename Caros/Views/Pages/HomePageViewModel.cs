using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Context;

namespace Caros.Views.Pages
{
    class HomePageViewModel : ViewModel
    {
        public HomePageViewModel(IContext context)
            : base(context)
        {
        }

        public void SwitchTheme()
        {
            Context.Theme.Switch(Theme.Style.Dark);
        }
    }
}
