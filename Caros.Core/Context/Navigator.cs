using Caros.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public interface INavigator : IContextComponent
    {
        event Action<PageViewModel> OnNavigate;
        event Action<bool> OnShowKeyboard;

        Reference<PageViewModel> ErrorPage { get; set; }
        Reference<PageViewModel> HomePage { get; set; }
        Reference<PageViewModel> MenuPage { get; set; }
        Reference<PageViewModel> PromptPage { get; set; }
        PageViewModel CurrentPage { get; }
        bool IsMenuOpen { get; }

        void GoHome();
        void Return();
        void OpenMenu();
        void ShowKeyboard(bool value = true);
        void Visit<T>(bool bypassHistory = false) where T : PageViewModel;
        Task<string> Prompt(string message, string defaultValue = "");
        Task Alert(string message);
        Task WaitForUserResponse();

        void UserRequestsPromptAccept(string value);
        void UserRequestsPromptCancel();
    }

    public class Navigator : INavigator
    {
        public event Action<PageViewModel> OnNavigate;
        public event Action<bool> OnShowKeyboard;

        public virtual IContext Context { get; set; }
        public Reference<PageViewModel> ErrorPage { get; set; }
        public Reference<PageViewModel> HomePage { get; set; }
        public Reference<PageViewModel> MenuPage { get; set; }
        public Reference<PageViewModel> PromptPage { get; set; }
        public bool IsMenuOpen { get; private set; }

        private Stack<PageViewModel> _history = new Stack<PageViewModel>();
        private Dictionary<Type, PageViewModel> _instances = new Dictionary<Type, PageViewModel>();

        private string _promptResult;
        private bool _responseRecieved = false;

        public Navigator(IContext context)
        {
            Context = context;
        }

        private Stack<PageViewModel> VisibleHistory
        {
            get { return new Stack<PageViewModel>(_history.Where(x => !x.ShowInHistory)); }
        }

        public PageViewModel CurrentPage
        {
            get
            {
                if (!_history.Any())
                    return null;

                return _history.Peek();
            }
        }

        private PageViewModel Visit(Type type, bool bypassHistory = false)
        {
            var isNew = false;

            if (!_instances.ContainsKey(type))
            {
                CreateInstance(type);
                isNew = true;
            }

            var page = _instances[type];
            page.ShowInHistory = !bypassHistory;

            _history.Push(page);

            CallNavigate(page, isNew);

            return page;
        }

        public void Visit<T>(bool bypassHistory = false) where T : PageViewModel
        {
            Visit(typeof(T), bypassHistory);
        }

        private PageViewModel CreateInstance(Type pageViewModelType)
        {
            PageViewModel instance;

            try
            {
                instance = (PageViewModel)Activator.CreateInstance(pageViewModelType, Context);
            }
            catch (Exception ex)
            {
                Core.Log.HandleUnexpectedException(ex, false);
                instance = ErrorPage.Instantiate(Context);
            }

            _instances.Add(pageViewModelType, instance);

            return instance;
        }

        public void GoHome()
        {
            Visit(HomePage.Type, false);
        }

        public void Return()
        {
            // pop current
            _history.Pop();

            if (!VisibleHistory.Any())
                return;

            CallNavigate(VisibleHistory.Peek(), false);
        }

        public void OpenMenu()
        {
            if (IsMenuOpen)
            {
                Return();
                return;
            }

            if (CurrentPage == null)
                return;

            var menuBuilder = new NamedActionBuilder();
            CurrentPage.OnExtra(menuBuilder);
            Visit(MenuPage.Type, false);
            (_instances[MenuPage.Type] as IMenuDisplayer).LoadTasks(menuBuilder.Items);
            IsMenuOpen = true;
        }

        public void ShowKeyboard(bool value = true)
        {
            if (OnShowKeyboard != null)
                OnShowKeyboard(value);
        }

        public async Task<string> Prompt(string message, string defaultValue = "")
        {
            var page = Visit(PromptPage.Type, false);

            var promptDisplayer = (IAlertDisplayer)page;
            promptDisplayer.ShowPrompt(message, defaultValue);
            promptDisplayer.RequestAccept += UserRequestsPromptAccept;
            promptDisplayer.RequestCancel += UserRequestsPromptCancel;

            await WaitForUserResponse();
            Return();

            return _promptResult;
        }

        public async Task Alert(string message)
        {
            var page = Visit(PromptPage.Type, false);

            var promptDisplayer = (IAlertDisplayer)page;
            promptDisplayer.ShowAlert(message);
            promptDisplayer.RequestCancel += UserRequestsPromptCancel;

            await WaitForUserResponse();
            Return();
        }

        public virtual async Task WaitForUserResponse()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    // wait for response
                    while (!_responseRecieved) { }

                    // reset flag
                    _responseRecieved = false;
                });
            }
            catch(ThreadAbortException)
            {
                return;
            }
        }

        public virtual void UserRequestsPromptCancel()
        {
            _promptResult = null;
            _responseRecieved = true;
        }

        public virtual void UserRequestsPromptAccept(string value)
        {
            _promptResult = value ?? "";
            _responseRecieved = true;
        }

        private void CallNavigate(PageViewModel page, bool isNew)
        {
            // close menu if navigating elsewhere
            if (page.GetType() != MenuPage.Type)
                IsMenuOpen = false;

            if (OnNavigate != null)
                OnNavigate.Invoke(page);

            page.OnVisit(isNew);
        }
    }
}
