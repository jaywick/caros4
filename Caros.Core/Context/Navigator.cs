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

        void Return();
        void Visit<T>() where T : Caros.Core.Contracts.PageViewModel;
    }

    public class Navigator : INavigator
    {
        public event Action<PageViewModel> OnNavigate;

        public virtual IContext Context { get; set; }

        private Stack<PageViewModel> _history = new Stack<PageViewModel>();
        
        public Navigator(IContext context)
        {
            Context = context;
        }

        public void Visit<T>() where T : PageViewModel
        {
            var page = CreateInstance(typeof(T));
            _history.Push(page);
            CallNavigate(page);
        }

        private PageViewModel CreateInstance(Type pageViewModelType)
        {
            return (PageViewModel)Activator.CreateInstance(pageViewModelType, Context);
        }

        public void Return()
        {
            var page = _history.Pop();
            _history.Push(page);
            CallNavigate(page);
        }

        private void CallNavigate(PageViewModel page)
        {
            if (OnNavigate != null)
                OnNavigate.Invoke(page);
        }
    }
}
