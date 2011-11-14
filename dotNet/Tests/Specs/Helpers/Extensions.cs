using Raconteur.Helpers;

namespace Specs
{
    public static class Extensions
    {
        public static bool Has(this object o, string property, object value)
        {
            return o.GetType().GetField(property.CamelCase()).GetValue(o).Equals(value);
        }
    }
}