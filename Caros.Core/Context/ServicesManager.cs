using Caros.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class ServicesManager : IContextComponent
    {
        private Dictionary<Type, Service> _instances = new Dictionary<Type, Service>();

        private IEnumerable<Type> _systemServices = Enumerable.Empty<Type>();

        public virtual IContext Context { get; set; }

        public ServicesManager(IContext context)
        {
            Context = context;
            CollectSystemServices();
        }

        public void CollectSystemServices()
        {
            _systemServices = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(filepath => new FileInfo(filepath).Name.ToLower().StartsWith("caros") && (filepath.EndsWith(".dll") || filepath.EndsWith(".exe")))
                .Select(filepath => Assembly.Load(AssemblyName.GetAssemblyName(filepath)))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(SystemService).IsAssignableFrom(type) && type != typeof(SystemService));
        }

        public void StartSystemServices()
        {
            foreach (var service in _systemServices)
            {
                Utilise(service).Start();
            }
        }

        private Service Utilise(Type serviceType)
        {
            if (!_instances.ContainsKey(serviceType))
            {
                var instance = (Service)Activator.CreateInstance(serviceType, Context);
                _instances.Add(serviceType, instance);
            }

            return _instances[serviceType];
        }

        public TService Utilise<TService>() where TService : Service
        {
            return (TService)Utilise(typeof(TService));
        }
    }
}
