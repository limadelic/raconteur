namespace Raconteur.Generators
{
    public abstract class StepCodeGenerator : CodeGenerator
    {
        public StepGenerator StepGenerator { get; set; }

        protected Step Step { get { return StepGenerator.Step; } }

        protected StepCodeGenerator(StepGenerator StepGenerator) 
        {
            this.StepGenerator = StepGenerator;
        }

        public abstract string Code { get; }
    }
}