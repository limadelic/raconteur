using FluentSpec;

namespace Features.Refactoring 
{
    public partial class RenameStep
    {
        string NewFeature;

        void When__is_renamed_to(string OldName, string NewName)
        {
            var Step = FeatureRunner.Feature.Steps.Find(s => s.Name == OldName);
/*
            Refactor.Rename(Step, NewName);
*/
        }

        void The_Feature_should_contain(string Content)
        {
            NewFeature.ShouldContain(Content);
        }
    }
}
