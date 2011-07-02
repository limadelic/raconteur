using System;
using System.Collections.Generic;
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

        void AssertRenamed(string OldStep, string OldContent, string NewStep, string NewContent, Location Location = null)
        {
            RenameStep = Substitute.For<RenameStep>
            (
                null, OldStep, NewStep
            );
            
            Feature.Content += OldContent;

            Step.Name = RenameStep.OriginalName;
            Step.Location = Location ?? 
                new Location(Feature.Content.IndexOf(RenameStep.OriginalName), RenameStep.OriginalName);

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
        public void should_rename_Step_with_Args()
        {
            Step.Args = new List<string> { "X" };

            var Location = new Location(129, @"old step ""X"" with Args");

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
                ",
                Location
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

        [Test]
        
        [Row("2 Args starting with Name", 
            2, "new Name with an  and another  at the end",
            @"Name with an ""Arg"" and another ""Arg""",
            @"new Name with an ""Arg"" and another ""Arg"" at the end")]
        
        [Row("Arg at the end", 
            1, "new Name with an", @"Name with an ""Arg""", 
            @"new Name with an ""Arg""")]
        
        [Row("Arg at the beginning",
            1, "still at beginning", @"""Arg"" at beginning", 
            @"""Arg"" still at beginning")]

        [Row("Starting and ending with Args",
            2, "still a sandwich", @"""Arg"" sandwich ""Arg""",
            @"""Arg"" still a sandwich ""Arg""")]

        [Row("Arg in the middle",
            1, "the  is here", @"where the ""Arg"" is",
            @"the ""Arg"" is here")]

        public void should_generate_new_Sentence_for_Step_with_Args(
            string Example, int ArgsCount, string NewName, string Content, string ExpectedSentence)
        {
            var Args = new List<string>();
            for (var i = 0; i < ArgsCount; i++) Args.Add("Arg");

            Step = new Step
            {
                Name = NewName,
                Args = Args,
                Location = new Location { Content = Content },
                IsDirty = true
            };

            Step.Sentence.ShouldBe(ExpectedSentence, Example);
        }
    }
}