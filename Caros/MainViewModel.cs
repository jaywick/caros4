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
using Caros.Core.Services;

namespace Caros
{
    class MainViewModel : RootViewModel
    {
        #region Properties

        private PageViewModel _activePage;

        public virtual IContext Context { get; set; }

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
            StartApplication();
        }

        void Navigator_OnNavigate(PageViewModel page)
        {
            this.ActivePage = page;
        }

        private async void StartApplication()
        {
            Caros.Core.IntegrationServices.Start();
            
            Context = ApplicationContext.Create();
            Context.Navigator.ErrorPage = new ErrorPageViewModel(Context);
            Context.Navigator.OnNavigate += Navigator_OnNavigate;

            Context.Services.StartSystemServices();

            Context.Navigator.Visit<SplashPageViewModel>();
            await Task.Delay(2000);

            Context.Navigator.Visit<HomePageViewModel>();
        }
    }
}
