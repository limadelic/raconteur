using System.Collections.Generic;

namespace Raconteur
{
    public static class CollectionsEx
    {
        public static bool HasItems<T>(this List<T> List)
        {
            return List != null && List.Count > 0;
        }
    }
}