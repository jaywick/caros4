using Caros.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Caros
{
    class MainViewModel : PropertyChangedBase
    {
        private SplashPageViewModel _activePage;

        public SplashPageViewModel ActivePage
        {
            get { return _activePage; }
            set
            {
                _activePage = value;
                NotifyOfPropertyChange(() => ActivePage);
            }
        }

        public MainViewModel()
        {
            this.ActivePage = new SplashPageViewModel();
        }
    }
}
