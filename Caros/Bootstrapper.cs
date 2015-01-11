using Caliburn.Micro;
using Caros.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros
{
    // see also: https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro.Platform/Bootstrapper.cs
    public class Bootstrapper : BootstrapperBase
    {
        public IContext ApplicateContext { get; set; }

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();

            //var mainViewModelInstance = (MainViewModel)GetInstance(typeof(MainViewModel), null);
            //mainViewModelInstance.StartApplication(Caros.Context.Context.Create(mainViewModelInstance));
        }
    }
}
