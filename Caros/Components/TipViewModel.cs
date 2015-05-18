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
using Caros.Core.Extensions;
using System.Windows.Threading;

namespace Caros.Components
{
    public class TipViewModel : PropertyChangedBase
    {
        public event System.Action RequestClose;

        private DispatcherTimer _timer;

        public TipViewModel(string message)
        {
            Message = message;
            StartTimer();
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

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += (s, e) => Close();
            _timer.Start();
        }

        public void Close()
        {
            RequestClose.InvokeIfExists();
        }
    }
}
