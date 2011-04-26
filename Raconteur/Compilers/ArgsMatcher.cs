using System.Reflection;

namespace Raconteur.Compilers
{
    public interface ArgsMatcher
    {
        bool Matches(MethodInfo Method);
    }
}