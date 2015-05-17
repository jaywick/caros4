using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.UI
{
    public interface IAlertDisplayer
    {
        event Action<string> RequestAccept;
        event Action RequestCancel;

        void ShowPrompt(string message, string defaultValue);
        void ShowAlert(string message);
    }
}
