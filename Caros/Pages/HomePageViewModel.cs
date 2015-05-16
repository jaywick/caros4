using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Music;
using Caros.Core;

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

        public void ShowKeyboard()
        {
            Context.Navigator.ShowKeyboard();
        }

        public void ShowToast()
        {
            var tasks = new List<NamedAction>();
            tasks.Add(new NamedAction("Task 1", () => { }));
            tasks.Add(new NamedAction("Task 2", () => { }));
            tasks.Add(new NamedAction("Task 3", () => { }));
            Context.Events.Post("Test poast", "hey there!", tasks);
        }

        public async void UpdateSystem()
        {
            var updateService = Context.Services.Utilise<Caros.Core.Services.UpdateService>();

            await updateService.CheckForUpdates();
            if (updateService.IsUpdateAvailable)
            {
                Context.Events.Post("An update is available",
                                    String.Format("Do you wish to deploy the latest update {0}", updateService.UpdateVersion),
                                    new[]
                                    {
                                        new NamedAction("Deploy", async () => await updateService.Deploy())
                                    });

                Context.Events.Post("Caros update installed",
                                    String.Format("Do you wish to relaunch Caros with the latest version ({0})", updateService.UpdateVersion),
                                    new[]
                                    {
                                        new NamedAction("Relaunch", () => updateService.Relaunch())
                                    });
            }
        }

        public async void ShowPrompt()
        {
            var x = await Context.Navigator.Prompt("hello", "default");
            Context.Events.Post(x, "got this value back");
        }
    }
}
