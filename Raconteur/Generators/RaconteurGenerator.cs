using System;
using System.IO;
using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public interface RaconteurGenerator
    {
        string Generate(string FeatureFilePath, string Content);
    }

    public class RaconteurGeneratorClass : RaconteurGenerator
    {
        public FeatureItem FeatureItem { get; set; }
        public FeatureParser FeatureParser { get; set; }
        public FeatureCompiler FeatureCompiler { get; set; }
        public RunnerGenerator RunnerGenerator { get; set; }
        public StepDefinitionsGenerator StepDefinitionsGenerator { get; set; }

        public string Generate(string FeatureFilePath, string Content)
        {
            try
            {
                var Feature = Parse(FeatureFilePath, Content);

                GenerateStepDefinitions(Feature);

                FeatureCompiler.Compile(Feature, FeatureItem);

                return ObjectFactory.NewRunnerGenerator(Feature).Code;
            } 
            catch (Exception e)
            {
                return AppendErrorToOriginalRunner(FeatureFilePath, e);
            }
        }

        Feature Parse(string FeatureFilePath, string Content) 
        {
            Project.LoadFrom(FeatureItem);

            var FeatureFile = new FeatureFile(FeatureFilePath){Content = Content};

            return FeatureParser.FeatureFrom(FeatureFile, FeatureItem);
        }

        string AppendErrorToOriginalRunner(string FeatureFilePath, Exception e) 
        {
            var RunnerFile = FeatureFilePath.Replace(".feature", ".runner.cs");
            var OriginalRunner = File.Exists(RunnerFile) ? File.ReadAllText(RunnerFile) : string.Empty;
            var eol = Environment.NewLine;

            return "/*" + eol + e + eol + "*/" + eol + OriginalRunner;
        }

        // Tech Debt: this is side effect to generate
        void GenerateStepDefinitions(Feature Feature) 
        {
            var StepDefinitions = ObjectFactory.NewStepDefinitionsGenerator
            (
                Feature, FeatureItem.ExistingStepDefinitions
            ).Code;

            FeatureItem.AddStepDefinitions(StepDefinitions);
        }
    }
}