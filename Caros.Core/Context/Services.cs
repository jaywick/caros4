using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Context
{
    public class Services
    {
        public void StartSystemServices(IContext context)
        {
            Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(filepath => new FileInfo(filepath).Name.ToLower().StartsWith("caros") && filepath.EndsWith(".dll"))
                .Select(filepath => Assembly.Load(AssemblyName.GetAssemblyName(filepath)))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType == typeof(SystemService))
                .Select(type => (SystemService)Activator.CreateInstance(type, context))
                .ToList()
                .ForEach(service => service.Start());
        }
    }
}
