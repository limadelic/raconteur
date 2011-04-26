using System.Reflection;

namespace Raconteur.Compilers
{
    public abstract class ArgsMatcherBase : ArgsMatcher
    {
        protected abstract bool Matches { get; }

        bool ArgsMatcher.Matches(MethodInfo Method)
        {
            this.Method = Method;

            return Matches;
        }

        protected Step Step { get; set; }
        protected MethodInfo Method { get; set;  }

        protected int ArgsCount { get { return Method.GetParameters().Length; } }
    }
}