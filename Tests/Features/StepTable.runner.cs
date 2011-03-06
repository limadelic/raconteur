using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features.StepDefinitions;

namespace Features 
{
    [TestClass]
    public partial class StepTable 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
        public void UsingTables()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
Given some values:
|0|0|
|0|1|
");        
            FeatureRunner.The_Runner_should_contain(
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
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
Verify some values:
[X|Y]
|0|0|
|0|1|
");        
            FeatureRunner.The_Runner_should_contain(
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
            FeatureRunner.Given_the_Feature_contains(
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
            FeatureRunner.The_Runner_should_contain(
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
