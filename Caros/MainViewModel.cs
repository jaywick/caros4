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
using Caros.Components;

namespace Caros
{
    class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            StartApplication();
            RenderLayout();
        }

        private void RenderLayout()
        {
            NavigationBarControl = new Components.NavigationBarViewModel(Context);
        }

        private async void StartApplication()
        {
            Caros.Core.IntegrationServices.Start();

            SetupContext();

            Context.Services.StartSystemServices();

            Context.Profiles.Switch(Context.Profiles.CurrentUser);
        }

        private void SetupContext()
        {
            Context = ApplicationContext.Create();

            Context.Profiles.HomePage = new TypeOf<HomePageViewModel>();
            Context.Profiles.SplashPage = new TypeOf<SplashPageViewModel>();

            Context.Navigator.ErrorPage = new TypeOf<ErrorPageViewModel>();
            Context.Navigator.HomePage = new TypeOf<HomePageViewModel>();
            Context.Navigator.MenuPage = new TypeOf<MenuPageViewModel>();
            Context.Navigator.PromptPage = new TypeOf<InputPageViewModel>();
            Context.Navigator.OnNavigate += Navigator_OnNavigate;
            Context.Navigator.OnShowKeyboard += Navigator_OnShowKeyboard;

            Context.Events.OnPost += Events_OnToast;
            Context.Events.OnTip += Events_OnTip;
        }

        private void Navigator_OnNavigate(PageViewModel page)
        {
            this.ActivePage = page;
        }

        private void Events_OnToast(EventPost post)
        {
            EventsControl = new EventsBarViewModel(post);
            EventsControl.RequestClose += () =>
            {
                EventsControl = null;
                NotifyOfPropertyChange(() => EventsControl);
            };

            NotifyOfPropertyChange(() => EventsControl);
        }

        private void Events_OnTip(string message)
        {
            TipControl = new TipViewModel(message);
            TipControl.RequestClose += () =>
            {
                TipControl = null;
                NotifyOfPropertyChange(() => TipControl);
            };

            NotifyOfPropertyChange(() => TipControl);
        }


        private void Navigator_OnShowKeyboard(bool value)
        {
            KeyboardControl = new KeyboardViewModel();
            KeyboardControl.RequestClose += KeyboardControl_RequestClose;
            NotifyOfPropertyChange(() => KeyboardControl);
        }

        private void KeyboardControl_RequestClose()
        {
            KeyboardControl = null;
            NotifyOfPropertyChange(() => KeyboardControl);
        }

        #region Properties

        public Components.NavigationBarViewModel NavigationBarControl { get; set; }
        public Components.EventsBarViewModel EventsControl { get; set; }
        public Components.TipViewModel TipControl { get; set; }
        public Components.KeyboardViewModel KeyboardControl { get; set; }

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

    }
}
