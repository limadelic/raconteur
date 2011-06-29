using Raconteur.Helpers;

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
    }
}
