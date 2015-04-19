using Caros.Core.Contracts;
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
        PageViewModel ErrorPage { get; set; }

        void Return();
        void Visit<T>() where T : Caros.Core.Contracts.PageViewModel;
    }

    public class Navigator : INavigator
    {
        public event Action<PageViewModel> OnNavigate;

        public virtual IContext Context { get; set; }
        public PageViewModel ErrorPage { get; set; }

        private Stack<PageViewModel> _history = new Stack<PageViewModel>();
        private Dictionary<Type, PageViewModel> _instances = new Dictionary<Type, PageViewModel>();

        public Navigator(IContext context)
        {
            Context = context;
        }

        public void Visit<T>() where T : PageViewModel
        {
            var type = typeof(T);
            var isNew = false;

            if (!_instances.ContainsKey(type))
            {
                CreateInstance(typeof(T));
                isNew = true;
            }

            var page = _instances[type];
            _history.Push(page);

            CallNavigate(page, isNew);
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
                instance = ErrorPage;
            }

            _instances.Add(pageViewModelType, instance);

            return instance;
        }

        public void Return()
        {
            // pop current
            _history.Pop();

            CallNavigate(_history.Peek(), false);
        }

        private void CallNavigate(PageViewModel page, bool isNew)
        {
            if (OnNavigate != null)
                OnNavigate.Invoke(page);

            page.OnVisit(isNew);
        }
    }
}
