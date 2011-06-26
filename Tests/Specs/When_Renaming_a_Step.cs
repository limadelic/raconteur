using MbUnit.Framework;
using NSubstitute;
using Raconteur.Refactoring;

namespace Specs
{
    [TestFixture]
    public class When_renaming_a_Step
    {
        RenameStep RenameStepRefactoring;

        [Test]
        public void should_replace_new_name_in_Feature()
        {
            RenameStepRefactoring = Substitute.For<RenameStep>(null, "OldName", "NewName");
            
            RenameStepRefactoring.FeatureContent
                .Returns("OldName");

            RenameStepRefactoring.Execute();

            RenameStepRefactoring.Received()
                .Write("NewName");
        }

        [Test]
        public void should_not_replace_common_prefixs()
        {
            RenameStepRefactoring = Substitute.For<RenameStep>
            (
                null, "old_step", "renamed_step"
            );
            
            RenameStepRefactoring.FeatureContent.Returns(@"
                old step
                old step do not rename
            ");

            RenameStepRefactoring.Execute();

            RenameStepRefactoring.Received().Write(@"
                renamed step
                old step do not rename
            ");
        }

    }
}