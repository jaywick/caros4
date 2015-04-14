using Caliburn.Micro;
using Caros.Core.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caros
{
    // see also: https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro.Platform/Bootstrapper.cs
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Core.Log.HandleUnexpectedException(e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Core.Log.HandleUnexpectedException((Exception)e.ExceptionObject);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(base.SelectAssemblies());
            assemblies.AddRange(Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(file => new FileInfo(file).Name.ToLower().StartsWith("caros") && file.EndsWith(".dll"))
                .Select(file => Assembly.Load(AssemblyName.GetAssemblyName(file))));
            return assemblies;
        }
    }
}
