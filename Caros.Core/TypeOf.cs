using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core
{
    public interface Reference<out T>
    {
        Type Type { get; set; }
        T Instantiate(params object[] args);
    }

    public class TypeOf<T> : Reference<T>
    {
        public Type Type { get; set; }

        public TypeOf()
        {
            Type = typeof(T);
        }

        public T Instantiate(params object[] args)
        {
            return (T)Activator.CreateInstance(Type, args);
        }
    }
}
