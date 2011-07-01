using System;
using System.Diagnostics;
using System.Linq;
using Common;
using MbUnit.Framework;
using NSubstitute;
using FluentSpec;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.Parsers;
using Raconteur.Refactoring;

namespace Specs
{
    [TestFixture]
    public class When_renaming_a_Step
    {
        RenameStep RenameStep;

        FeatureParser FeatureParser;

        Feature Feature;
        Step Step;

        [SetUp]
        public void SetUp()
        {
            Feature = Actors.FeatureWithStepDefinitions;
            Step = Feature.Steps.First();

            Feature.Content = 
            @"
                Feature: Name
                Scenario: Name
            ";

            FeatureParser = Create.TestObjectFor<FeatureParser>();
            
            ObjectFactory.Return<FeatureParserClass>(FeatureParser);

            Given.That(FeatureParser)
                .IgnoringArgs()
                .FeatureFrom("", null)
                .WillReturn(Feature);
        }

        [TearDown]
        public void TearDown()
        {
            ObjectFactory.ReturnNew<FeatureParserClass>();
        }

        void AssertRenamed(string OldStep, string OldContent, string NewStep, string NewContent)
        {
            RenameStep = Substitute.For<RenameStep>
            (
                null, OldStep, NewStep
            );
            
            Feature.Content += OldContent;

            Step.Name = RenameStep.OriginalName;
            var Start = Feature.Content.IndexOf(RenameStep.OriginalName);
            Step.Location = new Location(Start, RenameStep.OriginalName);

            RenameStep.FeatureContent.Returns(Feature.Content);

            RenameStep.Execute();

            RenameStep.Received().Write(Feature.Content);

            Feature.Content.ShouldContain(NewContent);
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

        [Test]
        public void should_refresh_the_Feature()
        {
            Feature.Content = 
            @"
                Feature: Name
                ...
                Step
                ...
            ";

            Step.Location = new Location
            (
                Feature.Content.IndexOf(Step.Name),
                Step.Name
            );

            Step.Rename("new step");

            Feature.Refresh();

            Feature.Content.ShouldContain("new step");
        }
    }
}