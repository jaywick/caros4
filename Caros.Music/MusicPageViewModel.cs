using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.Contracts;

namespace Caros.Music.Pages
{
    public class MusicPageViewModel : PageViewModel
    {
        public MusicPageViewModel(IContext context)
            : base(context)
        {
        }
    }
}
