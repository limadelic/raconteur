using System.Reflection;
using FluentSpec;
using MbUnit.Framework;
using NSubstitute;
using Raconteur;
using Raconteur.Resharper.Refactorings;
using ObjectFactory = Raconteur.Helpers.ObjectFactory;

namespace Specs
{
    [TestFixture]
    public class When_renaming_a_Step
    {
        const string NewName = "renamed step";
        const string NewMethodName = "renamed_step";

        static Step NewStep { get { return new Step {Name = "step"}; } }

        Refactor<MethodInfo> RenameMethod;

        [FixtureSetUp]
        public void FixtureSetUp()
        {
            RenameMethod = Substitute.For<Refactor<MethodInfo>>();
            ObjectFactory.Return<RenameMethod>(RenameMethod);
        }

        [FixtureTearDown]
        public void FixtureTearDown()
        {
            ObjectFactory.ReturnNew<RenameMethod>();
        }

        [Test]
        public void should_change_to_new_name()
        {
            var Step = NewStep;

            Refactor.Rename(Step, NewName);

            Step.Name.ShouldBe(NewName);
        }

        [Test]
        public void should_rename_all_the_Steps_sharing_same_implementation()
        {
            var StepBeingRefactored = NewStep;
            var StepSharingImplementation = NewStep;

            StepBeingRefactored.Implementation = new StepImplementation
            {
                Steps = { StepBeingRefactored, StepSharingImplementation }
            };

            Refactor.Rename(StepBeingRefactored, NewName);
             
            StepSharingImplementation.Name.ShouldBe(NewName);
        }

        public void step() { }
        public void renamed_step() { }

        [Test]
        public void should_rename_the_implementation_Method()
        {
            var Step = NewStep;

            Step.Implementation = new StepImplementation
            {
                Method = GetType().GetMethod("step"),
            };

            RenameMethod.Execute().Returns(GetType().GetMethod("renamed_step"));

            Refactor.Rename(Step, NewName);

            Step.Method.Name.ShouldBe(NewMethodName);
        }
    }
}