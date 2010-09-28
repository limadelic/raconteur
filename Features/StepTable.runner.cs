using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class StepTable 
    {
        
        [TestMethod]
        public void UsingTables()
        {         
            When_a_table_is_declared();        
            Each_row_should_become_a_Step_with_cols_as_Args();
        }

    }
}
