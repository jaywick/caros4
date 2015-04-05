using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Music;

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

        public void UpdateSystem()
        {
            var updateService = Context.Services.Utilise<Caros.Core.Services.UpdateService>();

            updateService.CheckForUpdates();
            if (updateService.IsUpdateAvailable)
            {
                //todo: toast + yes/no to update
                updateService.DownloadUpdate();
                updateService.Deploy();

                //todo: toast + yes/no to relaunch
                updateService.Relaunch();
            }
        }
    }
}
