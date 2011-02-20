using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentSpec;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur;

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

        public static Step ShouldBe(this Step Step, string Name, params object[] Args)
        {
            Step.Name.ShouldBe(Name);
            Step.Args.ShouldBe(Args);
            return Step;
        }

        public static void ShouldContain(this IEnumerable<Completion> completions, string text)
        {
            completions.Any(completion => completion.DisplayText == text).ShouldBeTrue();
        }

        public static void ShouldNotContain(this IEnumerable<Completion> completions, string text)
        {
            completions.Any(completion => completion.DisplayText == text).ShouldBeFalse();
        }
    }
}