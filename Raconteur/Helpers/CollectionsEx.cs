using System.Collections.Generic;

namespace Raconteur.Helpers
{
    public static class CollectionsEx
    {
        public static bool IsEmpty<T>(this ICollection<T> Items)
        {
            return Items == null || Items.Count == 0;
        }

        public static bool HasItems<T>(this ICollection<T> Items)
        {
            return !IsEmpty(Items);
        }
    }
}