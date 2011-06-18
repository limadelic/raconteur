
using FluentSpec;
using Raconteur.Refactorings;

namespace Features.Refactoring 
{
    public partial class RenameStep
    {
        string NewFeature, NewRunner;

        void When__is_renamed_to(string OldName, string NewName)
        {
            var Step = FeatureRunner.Feature.Steps.Find(s => s.Name == OldName);
            Refactor.Rename(Step, NewName);
        }

        void The_Feature_should_contain(string Content)
        {
            NewFeature.ShouldContain(Content);
        }

        void And_the_Runner_should_contain(string Content)
        {
            NewRunner.ShouldContain(Content);
        }
    }
}
