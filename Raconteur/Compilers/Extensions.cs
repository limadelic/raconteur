using System;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class Extensions
    {
        static Type ArgType;

        public static bool HasTableArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Last()
                .ParameterType.IsArray;
        }

        public static Type TableItemType(this Step Step)
        {
            ArgType = Step.Implementation.GetParameters().Last()
                .ParameterType.GetElementType();

            return ArgType.IsArray ? ArgType.GetElementType() : ArgType;
        }
    }
}