using System.Collections.Generic;

namespace Examples.Demo
{
    class EmployeeRepo 
    {
        public List<Employee> FindByLastName(string lastName)
        {
            return new List<Employee> { new Employee() };
        }
    }
}