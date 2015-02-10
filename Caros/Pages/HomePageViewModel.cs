using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Music.Pages;

namespace Caros.Pages
{
    class HomePageViewModel : PageViewModel
    {
        public HomePageViewModel(IContext context)
            : base(context)
        {
        }

        public void LaunchMusic()
        {
            Context.Navigator.Visit<MusicPageViewModel>();
        }

        public void SwitchUser()
        {
            Context.Navigator.Visit<ProfilesPageViewModel>();
        }
    }
}
