using Caros.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Navigator : ContextComponent
    {
        public event Action<PageViewModel> OnNavigate;

        private Stack<PageViewModel> _history = new Stack<PageViewModel>();
        
        public Navigator(IContext context)
            : base(context)
        {
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

        public void CallNavigate(PageViewModel page)
        {
            if (OnNavigate != null)
                OnNavigate.Invoke(page);
        }
    }
}
