using System.Collections.Generic;
using System.Linq;
using Raconteur.Helpers;
using Raconteur.Parsers;

namespace Common 
{
    public static class Extensions 
    {
        public static void Set(this object O, string Property, object Value)
        {
            try { O.GetType().GetProperty(Property).SetValue(O, Value, null); }
            catch { O.GetType().GetField(Property).SetValue(O, Value); }
        }

        public static dynamic Get(this object O, string Property)
        {
            try { return O.GetType().GetProperty(Property).GetValue(O, null); }
            catch { return O.GetType().GetField(Property).GetValue(O); }
        }

        public static I Mock<I, T>(this object o) where I : class where T : class
        {
            var NewMock = NSubstitute.Substitute.For<I>();
            ObjectFactory.Return<T>(NewMock);
            return NewMock;
        }

        public static Location AsLocation(this string Content)
        {
            return new Location(0, Content);
        }

        public static List<Location> AsLocations(this List<string> Contents)
        {
            return Contents.Select(x => x.AsLocation()).ToList();
        }
    }
}
