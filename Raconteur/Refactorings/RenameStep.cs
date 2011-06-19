using Raconteur.Helpers;

namespace Raconteur.Refactorings
{
    public class RenameStep : Refactor<Step>
    {
        public Step Step { get; set; }
        public string NewName { get; set; }

        public RenameStep(Step Step, string NewName)
        {
            this.Step = Step;
            this.NewName = NewName;
        }

        public Step Execute() 
        {
            RenameSteps();
            RenameMethod();

            return Step;
        }

        void RenameSteps()
        {
            if (Step.IsImplemented) 
                
                Step.Implementation.Steps.ForEach(s => s.Name = NewName);

            else Step.Name = NewName;
        }

        void RenameMethod()
        {
            Step.Method = ObjectFactory.NewRenameMethod(Step.Method, NewName).Execute();
        }
    }
}