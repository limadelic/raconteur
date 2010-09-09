using System.IO;
using Raconteur.Parsers;

namespace Raconteur.Generators
{
    public class RaconteurGenerator
    {
        readonly Project Project;
        private readonly FeatureParser FeatureParser;

        public RaconteurGenerator(Project Project, FeatureParser FeatureParser)
        {
            this.Project = Project;
            this.FeatureParser = FeatureParser;
        }

        public void GenerateFeature(FeatureFile FeatureFile, TextWriter OutputWriter)
        {
            var Feature = FeatureParser.FeatureFrom(FeatureFile, Project);
            
            GenerateTestFile(Feature, OutputWriter);
        }

        void GenerateTestFile(Feature Feature, TextWriter OutputWriter)
        {
            OutputWriter.Write(new RunnerGenerator().RunnerFor(Feature));
        }
    }
}