using System;
using System.Linq;
using System.Reflection;

namespace Raconteur
{
    public interface TypeResolver
    {
        Type TypeOf(string Name, string AssemblyName);
    }

    public class TypeResolverClass : TypeResolver
    {
        public Type TypeOf(string Name, string AssemblyName)
        {
            return
                (from Type in Assembly.Load(AssemblyName).GetTypes()
                where Type.Name == Name
                select Type).SingleOrDefault();
        }
    }    
}