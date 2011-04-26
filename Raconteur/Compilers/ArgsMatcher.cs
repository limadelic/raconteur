using System.Reflection;

namespace Raconteur.Compilers
{
    public interface ArgsMatcher
    {
        bool IsMatch { get; }
        bool Matches(MethodInfo Method);
    }
}