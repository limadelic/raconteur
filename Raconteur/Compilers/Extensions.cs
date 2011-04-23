using System;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class Extensions
    {
        static Type ArgType;

        public static Type LastArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Last().ParameterType;
        }

        public static bool HasTableArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.LastArg().IsArray;
        }

        public static bool HasObjectArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.LastArg().IsClass;
        }

        public static Type TableItemType(this Step Step)
        {
            ArgType = Step.Implementation.GetParameters().Last()
                .ParameterType.GetElementType();

            return ArgType.IsArray ? ArgType.GetElementType() : ArgType;
        }

        public static Type FieldType(this Type Type, string FieldName)
        {
            return 
                
                Type.GetProperties()
                .Where(x => x.Name == FieldName)
                .Select(x => x.PropertyType)
                .FirstOrDefault() 
                
                ??

                Type.GetFields()
                .Where(x => x.Name == FieldName)
                .Select(x => x.FieldType)
                .FirstOrDefault();
        }


    }
}