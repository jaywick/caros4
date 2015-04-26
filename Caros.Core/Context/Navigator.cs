using Caros.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public interface INavigator : IContextComponent
    {
        event Action<PageViewModel> OnNavigate;

        Reference<PageViewModel> ErrorPage { get; set; }
        Reference<PageViewModel> HomePage { get; set; }
        Reference<PageViewModel> MenuPage { get; set; }
        PageViewModel CurrentPage { get; }
        bool IsMenuOpen { get; }

        void GoHome();
        void Return();
        void OpenMenu();
        void Visit<T>(bool bypassHistory = false) where T : Caros.Core.UI.PageViewModel;
    }

    public class Navigator : INavigator
    {
        public event Action<PageViewModel> OnNavigate;

        public virtual IContext Context { get; set; }
        public Reference<PageViewModel> ErrorPage { get; set; }
        public Reference<PageViewModel> HomePage { get; set; }
        public Reference<PageViewModel> MenuPage { get; set; }
        public bool IsMenuOpen { get; private set; }

        private Stack<PageViewModel> _history = new Stack<PageViewModel>();
        private Dictionary<Type, PageViewModel> _instances = new Dictionary<Type, PageViewModel>();

        public Navigator(IContext context)
        {
            Context = context;
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

        private void Visit(Type type, bool bypassHistory)
        {
            var isNew = false;

            if (!_instances.ContainsKey(type))
            {
                CreateInstance(type);
                isNew = true;
            }

            var page = _instances[type];

            if (!bypassHistory)
                _history.Push(page);

            CallNavigate(page, isNew);
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

            if (!_history.Any())
                return;

            CallNavigate(_history.Peek(), false);
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
