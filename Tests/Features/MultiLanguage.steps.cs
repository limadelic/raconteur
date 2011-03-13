using MbUnit.Framework;
using Raconteur;

namespace Features 
{
    public partial class Multilingual 
    {
        dynamic backup;

        [SetUp]
        public void SetUp()
        {
            backup = Settings.Language;    
        }

        void Select_language(string Language)
        {
            Settings.Language = Languages.In[Language];  
        }

        [TearDown]
        public void TearDown()
        {
            Settings.Language = backup;    
        }
    }
}
