using System.Collections.Generic;

namespace Examples.Demo
{
    public interface EmployeeSearchView
    {
        string Criteria { get; set; }
        string Operator { get; set; }
        string Value { get; set; }

        List<Employee> Employees { get; set; }
        dynamic Filters { get; set; }        
    }
}