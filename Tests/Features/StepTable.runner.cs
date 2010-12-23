using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class StepTable 
    {
        
        [TestMethod]
        public void UsingTables()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
Given some values:
|0|0|
|0|1|
");        
            The_Runner_should_contain(
@"
[TestMethod]
public void ScenarioName()
{
Given_some_values_
(
new[] {0, 0},
new[] {0, 1}
);
}
");
        }
        
        [TestMethod]
        public void TablesWithHeader()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
Verify some values:
[X|Y]
|0|0|
|0|1|
");        
            The_Runner_should_contain(
@"
[TestMethod]
public void ScenarioName()
{
Verify_some_values_(0, 0);
Verify_some_values_(0, 1);
}
");
        }
        
        [TestMethod]
        public void TablesWithArgs()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
Given stuff in ""X"" place
[ stuff ]
|one    |
|another|
""Y"" stuff in
|somewhere|
|else	  |
");        
            The_Runner_should_contain(
@"
[TestMethod]
public void ScenarioName()
{
Given_stuff_in__place(""X"", ""one"");
Given_stuff_in__place(""X"", ""another"");
stuff_in
(
new[] {""Y"", ""somewhere""},
new[] {""Y"", ""else""}
);
}
");
        }

    }
}
