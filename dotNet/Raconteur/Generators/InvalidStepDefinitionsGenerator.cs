namespace Raconteur.Generators
{
    public class InvalidStepDefinitionsGenerator : CodeGenerator
    {
        readonly string ExistingStepDefinitions;

        internal InvalidStepDefinitionsGenerator(string ExistingStepDefinitions)
        {
            this.ExistingStepDefinitions = ExistingStepDefinitions;
        }

        public string Code
        {
            get { return ExistingStepDefinitions ?? string.Empty; }
        }
    }
}