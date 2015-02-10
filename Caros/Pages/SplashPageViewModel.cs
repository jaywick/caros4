using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;

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
            get { return String.Format("Welcome back {0}!", Context.Profiles.CurrentUser.Name); }
        }
    }
}
