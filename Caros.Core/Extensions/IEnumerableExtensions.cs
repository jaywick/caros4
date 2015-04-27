using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caros.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void Shuffle<T>(this IList<T> target)
        {
            var rng = new Random();
            var n = target.Count;

            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = target[k];
                target[k] = target[n];
                target[n] = value;
            }
        }

        public static IEnumerable<T> Find<T>(this IEnumerable<T> target, T candidate)
        {
            return target.Where(x => x.Equals(candidate));
        }

        public static IEnumerable<T> SymmetricDifference<T>(this IEnumerable<T> target, IEnumerable<T> other)
        {
            return target
                .Except(other)
                .Union(other.Except(target));
        }

        public static IEnumerable<Tuple<T, T>> JoinWhere<T>(this IEnumerable<T> target, IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            foreach (var t in target)
                foreach (var s in second)
                    if (predicate(t, s))
                        yield return Tuple.Create(t, s);
        }
    }
}
