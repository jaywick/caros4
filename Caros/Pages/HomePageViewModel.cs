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
using Caros.Core.Extensions;

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
                                    "Do you wish to deploy the latest update {0}".ApplyArguments(updateService.UpdateVersion),
                                    new NamedAction("Deploy", async () => await updateService.Deploy()));

                Context.Events.Post("Caros update installed",
                                    "Do you wish to relaunch Caros with the latest version ({0})".ApplyArguments(updateService.UpdateVersion),
                                    new NamedAction("Relaunch", () => updateService.Relaunch()));
            }
        }

        public async void ShowPrompt()
        {
            var result = await Context.Navigator.Prompt("hello", "default");
            Context.Events.Post(result, "got this value back");
        }

        public async void AddUser()
        {
            bool waitingForUser = true;
            string userName = null;

            while (waitingForUser)
            {
                userName = await Context.Navigator.Prompt("Please enter username");

                if (userName == null)
                    return;

                if (!Context.Profiles.NameExists(userName))
                    break;

                Context.Events.Tip("Username is already taken.");
            }
            
            Context.Profiles.Add(userName);
            Context.Profiles.Switch(userName);
        }
    }
}
