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
    public class Services : ContextComponent
    {
        private Dictionary<Type, Service> _instances = new Dictionary<Type, Service>();

        private List<Type> _systemServices = new List<Type>();

        public Services(IContext context)
            : base(context)
        {
            var systemServices = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(filepath => new FileInfo(filepath).Name.ToLower().StartsWith("caros") && filepath.EndsWith(".dll"))
                .Select(filepath => Assembly.Load(AssemblyName.GetAssemblyName(filepath)))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.CustomAttributes
                    .OfType<AutoStartAttribute>()
                    .Any());
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
