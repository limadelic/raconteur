using System.CodeDom.Compiler;
using System.IO;

namespace Raconteur
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
            FeatureFile.Namespace = Project.DefaultNamespace;
            OutputWriter.Write(new RunnerGenerator().RunnerFor(FeatureFile));
        }
    }
}