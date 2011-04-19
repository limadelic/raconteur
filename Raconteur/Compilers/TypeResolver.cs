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

            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += LoadFromFile;

                return 
                    Load(AssemblyName)
                    .GetTypes()
                    .Where(x => x.Name == Name)
                    .FirstOrDefault();
            } 
            catch { return null; }
            finally { AppDomain.CurrentDomain.AssemblyResolve -= LoadFromFile; }
        }

        string DefaultPath;
        string AssemblyPath;
        void InitAssemblyPath(string Assembly)
        {
            try
            {
                DefaultPath = DefaultPath ?? Path.GetDirectoryName(Assembly);
                AssemblyPath = Path.GetDirectoryName(Assembly);
            } 
            catch { AssemblyPath = DefaultPath; }
        }

        Assembly Load(string AssemblyName)
        {
            var Name = Path.GetFileNameWithoutExtension(AssemblyName);
            var FileName = Path.Combine(AssemblyPath, Name.Trim() + ".dll");

            return Assembly.Load(File.ReadAllBytes(FileName));
        }

        Assembly LoadFromFile(object Sender, ResolveEventArgs Args)
        {
            var Parts = Args.Name.Split(',');
            var FileName = Path.Combine(AssemblyPath, Parts[0].Trim() + ".dll");

            try
            {
                return Assembly.Load(File.ReadAllBytes(FileName));
            }
            catch { return null; }
        }
    }    
}