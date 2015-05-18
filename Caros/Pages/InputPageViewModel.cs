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
    class InputPageViewModel : PageViewModel, IPromptDisplayer
    {
        public event Action<string> RequestAccept;
        public event System.Action RequestCancel;

        public InputPageViewModel(IContext context)
            : base(context)
        {
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        private string _okButtonText;
        public string OkButtonText
        {
            get { return _okButtonText; }
            set
            {
                _okButtonText = value;
                NotifyOfPropertyChange(() => OkButtonText);
            }
        }

        private string _cancelButtonText;
        public string CancelButtonText
        {
            get { return _cancelButtonText; }
            set
            {
                _cancelButtonText = value;
                NotifyOfPropertyChange(() => CancelButtonText);
            }
        }

        private bool _isPrompt;
        public bool IsPrompt
        {
            get { return _isPrompt; }
            set
            {
                _isPrompt = value;
                NotifyOfPropertyChange(() => IsPrompt);
            }
        }

        public void Accept()
        {
            RequestAccept.Invoke(Value);
        }

        public void Cancel()
        {
            RequestCancel.Invoke();
        }

        public void ShowPrompt(string message, string defautValue = "")
        {
            Message = message;
            Value = defautValue;

            OkButtonText = "Accept";
            CancelButtonText = "Cancel";

            IsPrompt = true;
        }

        public void ShowAlert(string message)
        {
            Message = message;
            IsPrompt = false;
        }
    }
}
