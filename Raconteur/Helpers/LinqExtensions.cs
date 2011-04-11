using System;
using System.Collections.Generic;
using System.Linq;

namespace Raconteur.Helpers
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T> Items) where T : class 
        {
            return Items.Where(Item => Item != null);
        }

        public static IEnumerable<T> Evens<T>(this IEnumerable<T> Items)
        {
            return Items.Where((Item, Index) => Index % 2 == 0);
        }

        public static IEnumerable<T> Odds<T>(this IEnumerable<T> Items)
        {
            return Items.Where((Item, Index) => Index % 2 != 0);
        }

        public static IEnumerable<T> Chop<T>(this IEnumerable<T> Items, int Length)
        {
            return Items.Skip(Length).Take(Items.Count() - 2 * Length);
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> Items, T Item, T Value)
        {
            return Items.Select(x => x.Equals(Item) ? Value : x);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> Items, Action<T> Action)
        {
            foreach (var Item in Items) Action(Item);
            return Items;
        }
    }
}