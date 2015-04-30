using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Core.Services;
using Caros.Core;

namespace Caros.Components
{
    public class KeyboardViewModel : PropertyChangedBase
    {
        public event System.Action RequestClose;
    }
}
