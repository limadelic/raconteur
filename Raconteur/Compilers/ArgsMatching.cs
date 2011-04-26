using System;
using System.Collections.Generic;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class ArgsMatching
    {
        static readonly Dictionary<StepType, Func<Step, MethodInfo, bool >> Matchers = new Dictionary<StepType, Func<Step, MethodInfo, bool>>
        {
            {
                StepType.Simple, (s, m) => m.ArgsCount() == s.Args.Count
            }, 
            {
                StepType.Table, (s, m) => 
                    (m.ArgsCount() == s.Args.Count + 1) && m.HasTableArg()
            },    
            {
                StepType.HeaderTable, (s, m) => 
                    m.ArgsCount() == s.Args.Count + s.Table.Header.Count
            },    
            {
                StepType.ObjectTable, (s, m) => m.ArgsCount() == s.Args.Count + 1
            },    
        };

        public static bool Matches(this Step Step, MethodInfo Method)
        {
            return Matchers[Step.Type](Step, Method);
        }

        public static int ArgsCount(this MethodInfo Method)
        {
            return Method.GetParameters().Length;
        }
    }
}