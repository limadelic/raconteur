using System.Collections.Generic;
using MbUnit.Framework;
using Raconteur.Helpers;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local

namespace Features 
{
    public class BaseSteps
    {
        public void Inherited_Step() { }

        protected void Inherited_Step(string Arg) { }
    }

    public partial class ReusingSteps : BaseSteps
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

        public void Step() { }

        void Step(string Arg) { }
    }
}
// ReSharper restore UnusedParameter.Local
// ReSharper restore UnusedMember.Local

