Feature: Table Types
	In order top make the Step implementation easier
	There are multiple types of Tables

Scenario: Single column Table becomes an array Arg

	Given the Feature contains
	"
		Scenario: Scenario Name
			Given some values:
			|0|
			|1|
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName()
		{
			Given_some_values_
			(
				new[] {0, 1}
			);
		}
	"

Scenario: Table passed as an array

	Given the Feature is
	"
		Feature: Table Types

		Scenario: Scenario Name
			Given some values:
			|0|1|
			|1|1|
	"

	The Runner should contain
	"
		[TestMethod]
		public void ScenarioName()
		{
			Given_some_values_
			(
				new[] {""0"", ""1""},
				new[] {""1"", ""1""}
			);
		}
	"

Scenario: Object Table implemented with ObjectArg generates multiple calls

	Given the Feature is
	"
		Feature: Table Types

		Scenario: Login User
			Given the User:
			[UserName|Password]
			|neo	 |53cr3t  |
			|lola	 |run	  |
	"

	The Runner should contain
	"
		[TestMethod]
		public void LoginUser()
		{
			Given_the_User_( 
				new Common.User
				{
					UserName = ""neo"",
					Password = ""53cr3t""
				});
			Given_the_User_( 
				new Common.User
				{
					UserName = ""lola"",
					Password = ""run""
				});
		}
	"

Scenario: Object Table with multiple rows becomes an [] Arg

	Given the Feature is
	"
		Feature: Table Types

		Scenario: Login User
			Given the Users:
			[user name|password]
			|neo	  |53cr3t  |
			|lola	  |run	   |
	"

	The Runner should contain
	"
		[TestMethod]
		public void LoginUser()
		{
			Given_the_Users_
			(
				new Common.User
				{
					UserName = ""neo"",
					Password = ""53cr3t""
				},				
				new Common.User
				{
					UserName = ""lola"",
					Password = ""run""
				}
			);
		}
	"
