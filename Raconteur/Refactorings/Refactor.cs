using Raconteur.Helpers;

namespace Raconteur.Refactorings
{
    public interface Refactor<out T>
    {
        T Execute();
    }

    public class Refactor
    {
        public static void Rename(Step Step, string NewName)
        {
            ObjectFactory.NewRenameStep(Step, NewName).Execute();
        }
    }
}