using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Raconteur.Compilers
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
                (from Type in Load(AssemblyName).GetTypes()
                where Type.Name == Name
                select Type).FirstOrDefault();
        }

        public string AssemblyPath;
        
        Assembly Load(string AssemblyName)
        {
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromFile;

            AssemblyPath = AssemblyPath ?? Path.GetDirectoryName(AssemblyName);
            var Name = Path.GetFileNameWithoutExtension(AssemblyName);

            return Assembly.Load(Name);
        }

        Assembly LoadFromFile(object Sender, ResolveEventArgs Args)
        {
            var Parts = Args.Name.Split(',');
            var FileName = Path.Combine(AssemblyPath, Parts[0].Trim() + ".dll");

            return Assembly.Load(File.ReadAllBytes(FileName));
        }
    }    
}