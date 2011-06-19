using System.Reflection;

namespace Raconteur.Refactorings
{
    public class RenameMethod : Refactor<MethodInfo>
    {
        public MethodInfo Method { get; set; }
        public string NewName { get; set; }

        public RenameMethod(MethodInfo Method, string NewName)
        {
            this.Method = Method;
            this.NewName = NewName;
        }

        public MethodInfo Execute() { return null; }
    }
}