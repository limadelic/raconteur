namespace Raconteur
{
    public class RunnerGenerator
    {
        public string RunnerFor(FeatureFile FeatureFile)
        {
            return @"
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace " + FeatureFile.Namespace + @" 
{
    [TestClass]
    public class " + FeatureFile.Name + @"Runner {}
}
            ";
        }
    }
}