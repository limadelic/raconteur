using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Demo 
{
    [TestClass]
    public partial class EmployeeSearch 
    {

        
        [TestMethod]
        public void FindExistingEmployees()
        {         
            When_I_search_for_existing_Employees();        
            I_should_be_able_to_find_them();
        }
        
        [TestMethod]
        public void FindByFirstName()
        {         
            Given_an_Employee_named("Marco", "Polo");        
            When_I_search_for_Employees_whose("first name", "is", "Marco");        
            I_should_find_one_whose__is("last name", "Polo");
        }
        
        [TestMethod]
        public void FindByLastName()
        {         
            Given_an_Employee_named("Marco", "Polo");        
            When_I_search_for_Employees_whose("last name", "is", "Polo");        
            I_should_find_one_whose__is("first name", "Marco");
        }
        
        [TestMethod]
        public void FindWaldo()
        {         
            Given_the_Employees_("Robinson", "Crusoe");        
            Given_the_Employees_("Long John", "Silver");        
            Given_the_Employees_("Waldo", "");        
            Given_the_Employees_("Ali", "Baba");        
            When_I_search_for_Employees_whose("first name", "is", "Waldo");        
            I_should_find_one_whose__is("first name", "Waldo");
        }
        
        [TestMethod]
        public void FindWaldo_Crusoe()
        {         
            Given_the_Employees_("Robinson", "Crusoe");        
            Given_the_Employees_("Long John", "Silver");        
            Given_the_Employees_("Waldo", "");        
            Given_the_Employees_("Ali", "Baba");        
            When_I_search_for_Employees_whose_
            (        
                new[] {"first name", "contains", "o"},        
                new[] {"last name", "is not", "Silver"}
            );        
            I_should_find("Robinson", "Crusoe");        
            I_should_find("Waldo", "");
        }
        
        [TestMethod]
        public void FindWaldo_Crusoe_AliBaba_1()
        {         
            When_I_search_for_Employees_whose("first name", "is", "Waldo");        
            I_should_find_one_whose__is("first name", "Waldo");
        }
        
        [TestMethod]
        public void FindWaldo_Crusoe_AliBaba_2()
        {         
            When_I_search_for_Employees_whose("first name", "starts with", "Robin");        
            I_should_find_one_whose__is("last name", "Crusoe");
        }
        
        [TestMethod]
        public void FindWaldo_Crusoe_AliBaba_3()
        {         
            When_I_search_for_Employees_whose("last name", "contains", "Baba");        
            I_should_find_one_whose__is("first name", "Ali");
        }

    }
}
