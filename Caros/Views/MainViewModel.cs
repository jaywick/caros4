using Caros.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using System.Windows;
using Caros.Views;
using Caros.Context;

namespace Caros
{
    class MainViewModel : ViewModel
    {
        private IPage _activePage;

        public IPage ActivePage
        {
            get { return _activePage; }
            set
            {
                _activePage = value;
                NotifyOfPropertyChange(() => ActivePage);
            }
        }

        public MainViewModel()
            : base(null)
        {
            Context = Caros.Context.Context.Create(this);
            StartApplication();
        }

        private async void StartApplication()
        {
            this.ActivePage = new SplashPageViewModel(Context);
            await Task.Delay(2000);

            this.ActivePage = new HomePageViewModel(Context);
        }
    }
}
