using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Core.Services;

namespace Caros.Pages
{
    public class SplashPageViewModel : PageViewModel
    {
        public SplashPageViewModel(IContext context)
            : base(context)
        {
        }

        public string WelcomeMessage
        {
            get
            {
                var updateService = Context.Services.Utilise<UpdateService>();
                return String.Format("Welcome back {0}! You're running {1}", Context.Profiles.CurrentUser.Name, updateService.CurrentReleaseName);
            }
        }
    }
}
