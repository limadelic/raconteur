using System.Linq;
using FluentSpec;

namespace Features
{
    static class ShouldAssertions
    {
        public static void ShouldContainInOrder(this string Whole, params string[] Parts)
        {
            Parts.ToList().ForEach(Part =>
            {
                Whole.ShouldContain(Part);
                Whole = Whole.Substring(Whole.IndexOf(Part) + Part.Length);
            });
        }
    }
}