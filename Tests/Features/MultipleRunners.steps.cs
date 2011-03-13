using MbUnit.Framework;
using Raconteur;

namespace Features 
{
    public partial class UsingMultipleRunners 
    {
        dynamic backup;

        [SetUp]
        public void SetUp()
        {
            backup = Settings.XUnit;    
        }

        void Using(string xUnit)
        {
            Settings.XUnit = XUnits.Framework[xUnit.ToLower()];
        }

        [TearDown]
        public void TearDown()
        {
            Settings.XUnit = backup;    
        }
    }
}
