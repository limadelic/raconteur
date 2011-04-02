using System;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class Extensions
    {
        public static bool HasTableArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Last()
                .ParameterType.IsArray;
        }

        public static Type TableItemType(this Step Step)
        {
            return Step.Implementation.GetParameters().Last()
                .ParameterType.GetElementType().GetElementType();
        }
    }
}