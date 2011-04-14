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
            if (Name.IsEmpty() || AssemblyName.IsEmpty()) return null;

            InitAssemblyPath(AssemblyName);

            return
                (from Type in Load(AssemblyName).GetTypes()
                where Type.Name == Name
                select Type).FirstOrDefault();
        }

        string AssemblyPath;
        void InitAssemblyPath(string FirstAssembly)
        {
            AssemblyPath = AssemblyPath ?? Path.GetDirectoryName(FirstAssembly);
        }

        Assembly Load(string AssemblyName)
        {
            AppDomain.CurrentDomain.AssemblyResolve += LoadFromFile;

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