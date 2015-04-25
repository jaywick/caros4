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

        public override void OnExtra(Core.NamedActionBuilder builder)
        {
            builder.Add("shortcut to music", LaunchMusic);
            builder.Add("switch user", SwitchUser);
        }

        public void LaunchMusic()
        {
            Context.Navigator.Visit<MusicPageViewModel>();
        }

        public void SwitchUser()
        {
            Context.Navigator.Visit<ProfilesPageViewModel>();
        }

        public async void UpdateSystem()
        {
            var updateService = Context.Services.Utilise<Caros.Core.Services.UpdateService>();

            await updateService.CheckForUpdates();
            if (updateService.IsUpdateAvailable)
            {
                //todo: toast + yes/no to update
                await updateService.Deploy();

                //todo: toast + yes/no to relaunch
                updateService.Relaunch();
            }
        }
    }
}
