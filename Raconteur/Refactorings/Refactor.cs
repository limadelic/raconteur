namespace Raconteur.Refactorings
{
    public class Refactor
    {
        public static void Rename(Step Step, string NewName)
        {
            if (Step.IsImplemented)

                Step.Implementation.Steps.ForEach(
                    s => s.Name = NewName);

            else Step.Name = NewName;
        }
    }
}