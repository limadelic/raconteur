using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RunnerGenerator
    {
        FeatureFile FeatureFile;

        public string RunnerFor(FeatureFile FeatureFile)
        {
            this.FeatureFile = FeatureFile;

            return @"
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace " + FeatureFile.Namespace + @" 
{
    [TestClass]
    public class " + FeatureFile.Name + @"Runner 
    {
        " + StepDefinitionFullClassName + " Steps = new " + StepDefinitionFullClassName  + @"();
    }
}
            ";
        }

        protected string StepDefinitionFullClassName
        {
            get { return "StepDefinitions." + Parser.FeatureFrom(FeatureFile.Content).Name; } 
        }

        public FeatureParser Parser { get; set; }
    }
}