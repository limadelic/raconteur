using Features.StepDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;

namespace Features 
{
    public partial class UsingMultipleRunners : FeatureRunner
    {
        dynamic backup;

        [TestInitialize]
        public void SetUp()
        {
            backup = Settings.XUnit;    
        }

        void Using(string xUnit)
        {
            Settings.XUnit = XUnits.Framework[xUnit.ToLower()];
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.XUnit = backup;    
        }
    }
}
