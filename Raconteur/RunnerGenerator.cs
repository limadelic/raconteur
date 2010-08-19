namespace Raconteur
{
    public class RunnerGenerator
    {
        public string RunnerFor(FeatureFile FeatureFile)
        {
            return @"
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public class " + FeatureFile.Name + @"Runner {}
}
            ";
        }
    }
}