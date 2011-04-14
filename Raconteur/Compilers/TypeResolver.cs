using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Raconteur.Helpers;

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
//            System.Diagnostics.Debugger.Launch();

            if (Name.IsEmpty() || AssemblyName.IsEmpty()) return null;

            return
                (from Type in Load(AssemblyName).GetTypes()
                where Type.Name == Name
                select Type).FirstOrDefault();
        }

        string AssemblyPath;
        
        Assembly Load(string AssemblyName)
        {
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromFile;

            AssemblyPath = Path.GetDirectoryName(AssemblyName);
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