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
            backup = Settings.Language;    
        }

        void Select_language(string Language)
        {
            Settings.Language = Languages.In[Language];  
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.Language = backup;    
        }
    }
}
