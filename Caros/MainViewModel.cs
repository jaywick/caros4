using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using System.Windows;
using Caros.Core.Context;
using Caros.Core.Contracts;
using Caros.Pages;

namespace Caros
{
    class MainViewModel : RootViewModel
    {
        #region Properties

        private PageViewModel _activePage;

        public IContext Context { get; set; }

        public PageViewModel ActivePage
        {
            get { return _activePage; }
            set
            {
                _activePage = value;
                NotifyOfPropertyChange(() => ActivePage);
            }
        }

        #endregion

        public MainViewModel()
        {
            Context = Caros.Core.Context.Context.Create(this);
            Context.Navigator.OnNavigate += Navigator_OnNavigate;
            StartApplication();
        }

        void Navigator_OnNavigate(PageViewModel page)
        {
            this.ActivePage = page;
        }

        private async void StartApplication()
        {
            //Context.Navigator.Visit<SplashPageViewModel>();
            //await Task.Delay(2000);

            Context.Navigator.Visit<HomePageViewModel>();
        }
    }
}
