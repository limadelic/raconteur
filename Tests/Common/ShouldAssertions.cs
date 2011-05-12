using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentSpec;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur;

namespace Common
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

        public static Step ShouldBe(this Step Step, string Name, params object[] Args)
        {
            Step.Name.ShouldBe(Name);
            Step.Args.ShouldBe(Args);
            return Step;
        }

        public static void ShouldBe(this List<Type> Ones, List<Type> Others)
        {
            Ones.Count.ShouldBe(Others.Count);
            
            for (var i = 0; i < Ones.Count; i++)
                Ones[i].ShouldBe(Others[i]);
        }

        public static void ShouldBe(this Type One, Type Another)
        {
            One.Namespace.ShouldBe(Another.Namespace);
            One.Name.ShouldBe(Another.Name);
        }

        public static void ShouldBe(this MethodInfo Method, MethodInfo AnotherMethod)
        {
            Method.Name.ShouldBe(AnotherMethod.Name);
            Method.ReturnType.ShouldBe(AnotherMethod.ReturnType);
            Method.GetParameters().ShouldBe(AnotherMethod.GetParameters());
        }

        public static void ShouldBe(this ParameterInfo[] Ones, params ParameterInfo[] Others)
        {
            Ones.Length.ShouldBe(Others.Length);
            
            for (var i = 0; i < Ones.Length; i++)
                Ones[i].ShouldBe(Others[i]);
        }

        public static void ShouldBe(this ParameterInfo Arg, ParameterInfo AnotherArg)
        {
            Arg.Name.ShouldBe(AnotherArg.Name);
            Arg.ParameterType.ShouldBe(AnotherArg.ParameterType);
        }

        public static void ShouldBe<T>(this List<T> Ones, params T[] Others)
        {
            Ones.Count.ShouldBe(Others.Length);
            
            for (var i = 0; i < Ones.Count; i++)
                Ones[i].ShouldBe(Others[i]);
        }

        public static void ShouldContain(this IEnumerable<Completion> completions, string text)
        {
            completions.Any(completion => completion.DisplayText == text)
                .ShouldBeTrue(
                    string.Format("Expected \"{0}\" to be found in \"{1}\"", 
                    text, 
                    completions.Aggregate(string.Empty, (seed, source) => seed + "\n" + source.DisplayText))
                );
        }

        public static void ShouldNotContain(this IEnumerable<Completion> completions, string text)
        {
            completions.Any(completion => completion.DisplayText == text)
               .ShouldBeFalse(
                   string.Format("Found \"{0}\" in \"{1}\"",
                   text,
                   completions.Aggregate(string.Empty, (seed, source) => seed + "\n" + source.DisplayText))
            );
        }
    }
}