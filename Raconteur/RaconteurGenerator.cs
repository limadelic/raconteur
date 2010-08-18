using System.CodeDom.Compiler;
using System.IO;

namespace Raconteur
{
    public class RaconteurGenerator
    {
        readonly Project project;

        public RaconteurGenerator(Project project) { this.project = project; }

        public void GenerateCSharpTestFile(FeatureFile featureFile, TextWriter outputWriter)
        {
        }

        public void GenerateTestFile(FeatureFile featureFile, CodeDomProvider codeProvider, TextReader inputReader,
            TextWriter outputWriter)
        {
        }
    }
}