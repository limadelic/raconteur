using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class StepTable 
    {
        
        [TestMethod]
        public void UsingTables()
        {         
            When_a_Table_is_declared();        
            Each_row_should_become_a_Step_with_cols_as_Args();
        }
        
        [TestMethod]
        public void TablesWithArgs()
        {         
            When_a_Table_declaration_has_Args();        
            Each_Step_will_start_with_the_Args();
        }
        
        [TestMethod]
        public void TablesWithHeader()
        {         
            When_a_Table_declaration_has_a_Header_row();        
            The_Header_should_be_skipped();        
            And_each_row_should_become_a_Step_with_cols_as_Args();
        }

    }
}
