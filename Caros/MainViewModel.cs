using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using System.Windows;
using Caros.Core.Context;
using Caros.Core.UI;
using Caros.Pages;
using Caros.Core.Services;
using Caros.Core;

namespace Caros
{
    class MainViewModel : RootViewModel
    {
        #region Properties

        public Components.NavigationBarViewModel NavigationBarControl { get; set; }

        public virtual IContext Context { get; set; }

        private PageViewModel _activePage;
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
            RenderLayout();
        }

        private void RenderLayout()
        {
            NavigationBarControl = new Components.NavigationBarViewModel(Context);
        }

        void Navigator_OnNavigate(PageViewModel page)
        {
            this.ActivePage = page;
        }

        private async void StartApplication()
        {
            Caros.Core.IntegrationServices.Start();
            
            Context = ApplicationContext.Create();
            Context.Navigator.ErrorPage = new TypeOf<ErrorPageViewModel>();
            Context.Navigator.HomePage = new TypeOf<HomePageViewModel>();
            Context.Navigator.MenuPage = new TypeOf<MenuPageViewModel>();
            Context.Navigator.OnNavigate += Navigator_OnNavigate;

            Context.Services.StartSystemServices();

            Context.Navigator.Visit<SplashPageViewModel>(bypassHistory: true);
            await Task.Delay(2000);

            Context.Navigator.Visit<HomePageViewModel>();
        }
    }
}
