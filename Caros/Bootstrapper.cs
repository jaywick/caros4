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
            
            AppDomain.CurrentDomain.UnhandledException += (s, e) => DisplayError((Exception)e.ExceptionObject);
            TaskScheduler.UnobservedTaskException += (s, e) => DisplayError(e.Exception);
        }

        private void DisplayError(Exception exception)
        {
            MessageBox.Show(exception.Message + "\n\n" + exception.StackTrace);
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
