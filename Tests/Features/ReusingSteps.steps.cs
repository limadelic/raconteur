using System.Collections.Generic;
using MbUnit.Framework;
using Raconteur.Helpers;

namespace Features 
{
    public partial class ReusingSteps
    {
        readonly List<dynamic> backup = new List<dynamic>();

        [SetUp]
        public void SetUp()
        {
            backup.Add(Settings.StepDefinitions);    
            backup.Add(Settings.Libraries);
            
            Settings.StepDefinitions = new List<string>();    
            Settings.Libraries = new List<string>();    
        }

        [TearDown]
        public void TearDown()
        {
            Settings.StepDefinitions = backup[0];
            Settings.Libraries = backup[1];
        }
    }
}
