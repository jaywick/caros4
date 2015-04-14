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
    public class ErrorPageViewModel : PageViewModel
    {
        public ErrorPageViewModel(IContext context)
            : base(context)
        {
        }
    }
}
