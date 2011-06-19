using System.Reflection;
using Raconteur.Resharper.Refactorings;

namespace Raconteur.Resharper
{
    public class ObjectFactory : Helpers.ObjectFactory
    {
        public static Refactor<Step> NewRenameStep(Step Step, string NewName) 
        {
            return Object<RenameStep, Refactor<Step>>() ??
                new Refactorings.RenameStep(Step, NewName);
        }

        public static Refactor<MethodInfo> NewRenameMethod(MethodInfo Method, string NewName) 
        {
            return Object<RenameMethod, Refactor<MethodInfo>>() ??
                new RenameMethod(Method, NewName);
        }

    }
}