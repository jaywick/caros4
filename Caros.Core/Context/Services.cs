using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Services : ContextComponent
    {
        private Dictionary<Type, Service> _instances = new Dictionary<Type, Service>();

        public Services(IContext context)
            : base(context)
        {
        }

        public void StartSystemServices()
        {
            var systemServices = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(filepath => new FileInfo(filepath).Name.ToLower().StartsWith("caros") && filepath.EndsWith(".dll"))
                .Select(filepath => Assembly.Load(AssemblyName.GetAssemblyName(filepath)))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType == typeof(SystemService));

            foreach (var service in systemServices)
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
