using System.CodeDom.Compiler;
using System.IO;

namespace Raconteur.Generators
{
    public class RaconteurGenerator
    {
        readonly Project Project;

        public RaconteurGenerator(Project Project)
        {
            this.Project = Project;
        }

        public void GenerateTestFile(FeatureFile FeatureFile, 
            CodeDomProvider CodeProvider, TextReader InputReader,
            TextWriter OutputWriter)
        {
            FeatureFile.Load(Project);

            OutputWriter.Write(ObjectFactory.NewRunnerGenerator
                .RunnerFor(FeatureFile));
        }
    }
}