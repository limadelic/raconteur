using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Refactorings;

namespace Specs
{
    [TestFixture]
    public class When_renaming_a_Step
    {
        const string NewName = "renamed step";

        [Test]
        public void should_change_to_new_name()
        {
            var Step = new Step { Name = "step" };

            Refactor.Rename(Step, NewName);

            Step.Name.ShouldBe(NewName);
        }

        [Test]
        public void should_rename_all_the_Steps_sharing_same_implementation()
        {
            var StepBeingRefactored = new Step {Name = "step"};
            var StepSharingImplementation = new Step {Name = "step"};

            StepBeingRefactored.Implementation = new StepImplementation
            {
                Steps = { StepBeingRefactored, StepSharingImplementation }
            };

            Refactor.Rename(StepBeingRefactored, NewName);

            StepSharingImplementation.Name.ShouldBe(NewName);
        }
    }
}