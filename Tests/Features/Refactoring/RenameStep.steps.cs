using NSubstitute;
using Raconteur.Helpers;
using RenameStepRefactoring=Raconteur.Refactoring.RenameStep;

namespace Features.Refactoring 
{
    public partial class RenameStep
    {
        RenameStepRefactoring RenameStepRefactoring;

        void When__is_renamed_to(string OldName, string NewName)
        {
            RenameStepRefactoring = Substitute.For<RenameStepRefactoring>(null, OldName, NewName);
            
            RenameStepRefactoring.FeatureContent
                .Returns(FeatureRunner.FeatureContent);

            RenameStepRefactoring.Execute();
        }

        void The_Feature_should_contain(string Content)
        {
            RenameStepRefactoring.Received()
                .Write(Arg.Is<string>(s => s.Contains(Content.TrimLines())));
        }
    }
}
