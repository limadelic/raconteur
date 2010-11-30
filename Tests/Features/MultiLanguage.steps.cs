using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;

namespace Features 
{
    public partial class Multilingual : FeatureRunner
    {
        dynamic backup;

        [TestInitialize]
        public void SetUp()
        {
            backup = Languages.Current;    
        }

        void Select_language(string Language)
        {
            Languages.Current = Languages.All[Language];  
        }

        [TestCleanup]
        public void TearDown()
        {
            Languages.Current = backup;    
        }
    }
}
