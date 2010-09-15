using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class RefactorFeature 
    {
        
        [TestMethod]
        public void RenameFeature()
        {         
            Given_I_have_already_defined_a_feature();        
            If_I_rename_it();        
            Then_the_steps_and_the_runner_should_reflect_the_change();
        }

    }
}
