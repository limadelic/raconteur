using System;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class Extensions
    {
        static Type ArgType;

        public static bool HasArgs(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Length > 0;
        }

        public static Type LastArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Last().ParameterType;
        }

        public static bool HasTableArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.LastArg().IsArray;
        }

        public static bool HasObjectArgFor(this MethodInfo StepDefinition, Step Step)
        {
            var Type = StepDefinition.LastArg();

            return Type.IsClass 
                && Step.Table.Header.All(x => Type.FieldType(x) != null);
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