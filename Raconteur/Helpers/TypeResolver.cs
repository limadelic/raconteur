using System;

namespace Raconteur
{
    public interface TypeResolver
    {
        Type TypeOf(string Name);
    }

    public class TypeResolverClass : TypeResolver
    {
        public string Assembly { get; set; }

        public Type TypeOf(string Name)
        {
            return Type.GetType(Assembly + "." + Name + ", " + Assembly);
        }
    }    
}