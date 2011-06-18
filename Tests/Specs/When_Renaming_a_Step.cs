using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Refactorings;

namespace Specs
{
    [TestFixture]
    public class When_Renaming_a_Step
    {
        [Test]
        [Category("wip")]
        public void should_rename_all_the_Steps_sharing_same_implementation()
        {
            const string NewName = "renamed step";
            var StepBeingRefactored = new Step {Name = "step",};
            var StepSharingImplementation = new Step();

            Refactor.Rename(StepBeingRefactored, NewName);

            StepSharingImplementation.Name.ShouldBe(NewName);
        }
    }
}