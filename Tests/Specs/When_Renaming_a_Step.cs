using MbUnit.Framework;
using NSubstitute;
using Raconteur.Refactoring;

namespace Specs
{
    [TestFixture]
    public class When_renaming_a_Step
    {
        RenameStep RenameStep;

        void AssertRenamed(string OldStep, string OldContent, string NewStep, string NewContent)
        {
            RenameStep = Substitute.For<RenameStep>
            (
                null, OldStep, NewStep
            );
            
            RenameStep.FeatureContent.Returns(OldContent);

            RenameStep.Execute();

            RenameStep.Received().Write(NewContent);
        }

        [Test]
        public void should_replace_new_name_in_Feature()
        {
            AssertRenamed("OldName", "OldName", "NewName", "NewName");
        }

        [Test]
        public void should_match_the_whole_sentence_to_the_Step()
        {
            AssertRenamed
            (
                "old_step",
                @"
                    old step
                    old step do not rename
                ",
                "renamed_step", 
                @"
                    renamed step
                    old step do not rename
                "
            );
        }

        [Test]
        [Category("wip")]
        public void should_rename_Step_with_Args()
        {
            AssertRenamed
            (
                "old_step__with_Args",
                @"
                    old step
                    old step ""X"" with Args
                ",
                "renamed_step__with_Arg", 
                @"
                    old step
                    renamed step ""X"" with Arg
                "
            );
        }
    }
}