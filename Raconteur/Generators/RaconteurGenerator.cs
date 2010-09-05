using System.CodeDom.Compiler;
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

        public void GenerateFeature(FeatureFile FeatureFile, 
            CodeDomProvider CodeProvider, TextReader InputReader,
            TextWriter OutputWriter)
        {
            FeatureFile.Load(Project);
            var Feature = FeatureParser.FeatureFrom(FeatureFile);
            
            GenerateTestFile(Feature, OutputWriter);
        }

        private void GenerateTestFile(Feature Feature, TextWriter OutputWriter)
        {
            OutputWriter.Write(ObjectFactory.NewRunnerGenerator
                                   .RunnerFor(Feature));
        }
    }
}