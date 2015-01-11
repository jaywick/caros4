using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Context;

namespace Caros.Views.Pages
{
    public class SplashPageViewModel : ViewModel
    {
        public SplashPageViewModel(IContext context)
            : base(context)
        {
        }
    }
}
