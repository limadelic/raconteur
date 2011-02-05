using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayDohs;

namespace Examples.Demo 
{
    public partial class EmployeeSearch 
    {
        List<Employee> Employees;
        readonly dynamic EmployeeRepo = new PlayDoh();

        void When_I_search_for_existing_Employees() 
        {
            EmployeeRepo
                .Returns(new List<Employee> { new Employee() })
                .When.FindByLastName("smith");

            Employees = EmployeeRepo.FindByLastName("smith");
        }
        
        void I_should_be_able_to_find_them() 
        {
            Assert.IsTrue(Employees.Count > 0);
        }

        readonly dynamic EmployeeDAL = new PlayDoh();
        readonly dynamic Browser = new PlayDoh();

        void Given_an_Employee_named(string firstName, string lastName) 
        {
            EmployeeDAL.Insert(firstName, lastName);
        }

        void When_I_search_for_Employees_with_first_name(string firstName) 
        {
            Browser
                .Login()
                .NavigateToEmployeeDirectory()
                .Enter("first_name", firstName)
                .ClickFind();
        }

        void I_should_find_one_with_last_name(string lastName) 
        {
            Browser.AssertExist("last_name", lastName);
        }
    }
}
