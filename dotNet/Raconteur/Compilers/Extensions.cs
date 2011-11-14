using System;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
{
    public static class Extensions
    {
        public static bool HasArgs(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Length > 0;
        }

        public static Type LastArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.GetParameters().Last().ParameterType;
        }

        public static Type ElementType(this Type Type)
        {
            while (Type.IsArray) Type = Type.GetElementType();
            return Type;
        }

        public static bool HasTableArg(this MethodInfo StepDefinition)
        {
            return StepDefinition.LastArg().IsArray;
        }

        public static bool HasObjectArgFor(this MethodInfo StepDefinition, Step Step)
        {
            var Type = StepDefinition.LastArg().ElementType();

            return Type.IsClass 
                && Step.Table.Header.All(x => Type.FieldType(x) != null);
        }

        public static Type TableItemType(this Step Step)
        {
            return Step.Method.LastArg().ElementType();
        }

        public static Type FieldType(this Type Type, string FieldName)
        {
            FieldName = FieldName.Replace(" ", "").ToLower();

            return 
                
                Type.GetProperties()
                .Where(x => x.Name.ToLower() == FieldName)
                .Select(x => x.PropertyType)
                .FirstOrDefault() 
                
                ??

                Type.GetFields()
                .Where(x => x.Name.ToLower() == FieldName)
                .Select(x => x.FieldType)
                .FirstOrDefault();
        }

        public static string FieldName(this Type Type, string FieldName)
        {
            FieldName = FieldName.Replace(" ", "").ToLower();

            return 
                
                Type.GetProperties()
                .Where(x => x.Name.ToLower() == FieldName)
                .Select(x => x.Name)
                .FirstOrDefault() 
                
                ??

                Type.GetFields()
                .Where(x => x.Name.ToLower() == FieldName)
                .Select(x => x.Name)
                .FirstOrDefault();
        }

    }
}