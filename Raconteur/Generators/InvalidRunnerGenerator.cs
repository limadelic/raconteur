namespace Raconteur.Generators
{
    public class InvalidRunnerGenerator : CodeGenerator
    {
        readonly InvalidFeature InvalidFeature;

        public InvalidRunnerGenerator(InvalidFeature InvalidFeature)
        {
            this.InvalidFeature = InvalidFeature;
        }

        public string Code { get { return InvalidFeature.Reason; } }
    }
}