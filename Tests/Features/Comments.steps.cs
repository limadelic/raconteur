
namespace Features 
{
	public partial class Comments : FeatureRunner
	{
		void When_lines_a_line_in_a_Multiline_Args_starts_with_a_comment()
		{
			Given_the_Feature_contains
			(@"
				Scenario: The Doors
			
					When 
					"" 
					// the doors of perception are cleansed, 
					// man will see things as they truly are 
					""
					Infinite
			");
		}

		void It_will_still_be_passed_in_as_part_of_the_Arg()
		{
			The_Runner_should_contain
			(@"
				public void TheDoors()
				{
					When(
					@""// the doors of perception are cleansed,
					// man will see things as they truly are
					"");
					Infinite();
				}
			");    
		}
	}
}
