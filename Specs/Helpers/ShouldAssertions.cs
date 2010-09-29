using System.Collections.Generic;
using System.Linq;
using FluentSpec;

namespace Specs
{
    public static class ShouldAssertions
    {
        public static void ShouldContainInOrder(this string Whole, params string[] Parts)
        {
            Parts.ToList().ForEach(Part =>
            {
                Whole.ShouldContain(Part);
                Whole = Whole.Substring(Whole.IndexOf(Part) + Part.Length);
            });
        }

        public static void ShouldBe<T>(this List<T> Ones, params object[] Others)
        {
            Ones.Count.ShouldBe(Others.Length);
            
            for (var i = 0; i < Ones.Count; i++)
                Ones[i].ShouldBe(Others[i]);
        }
    }
}