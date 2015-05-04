using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class TypeExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Type target) where TAttribute : Attribute
        {
            var attributes = Attribute.GetCustomAttributes(target);

            if (!attributes.Any())
                return null;

            return attributes.Cast<TAttribute>().Single();
        }
    }
}
