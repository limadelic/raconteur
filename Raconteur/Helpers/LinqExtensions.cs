using System.Collections.Generic;
using System.Linq;

namespace Raconteur
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Evens<T>(this IEnumerable<T> Items)
        {
            return Items.ToList().Where((Item, Index) =>
                Index % 2 == 0);
        }

        public static IEnumerable<T> Odds<T>(this IEnumerable<T> Items)
        {
            return Items.ToList().Where((Item, Index) =>
                Index % 2 != 0);
        }
    }
}