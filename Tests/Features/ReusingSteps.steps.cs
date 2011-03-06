using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;

namespace Features 
{
    public partial class ReusingSteps
    {
        readonly List<dynamic> backup = new List<dynamic>();

        [TestInitialize]
        public void SetUp()
        {
            backup.Add(Settings.StepDefinitions);    
            backup.Add(Settings.Libraries);
            
            Settings.StepDefinitions = new List<string>();    
            Settings.Libraries = new List<string>();    
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.StepDefinitions = backup[0];
            Settings.Libraries = backup[1];
        }
    }
}
