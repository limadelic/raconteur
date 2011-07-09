using System;
using System.IO;
using System.Threading;
using FluentSpec;
using MbUnit.Framework;
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

        void Given_the_Feature__contains(string Name, string Content)
        {
            File.WriteAllText
            (
                Name + ".feature",
                "Feature: " + Name + Environment.NewLine + Content
            );
        }

        void And_the_Feature__contains(string Name, string Content)
        {
            Given_the_Feature__contains(Name, Content);
        }

        void The_Feature__should_contain(string Name, string Content)
        {
            File.ReadAllText(Name + ".feature").TrimLines()
                .ShouldContain(Content.TrimLines());
        }

        void and_the_Feature__should_contain(string Name, string Content)
        {
            The_Feature__should_contain(Name, Content);
        }

        void When__used_in_multiple_features_is_renamed_to(string OldName, string NewName)
        {
            // The file needs to exist already
            Directory.EnumerateFiles(".", "*.feature").ForEach( f => 
                File.WriteAllText(Path.GetFileNameWithoutExtension(f) + ".runner.cs",""));

            RunnerFileWatcher.Timeout = 50;

            RunnerFileWatcher.OnFileChange(f =>
                ObjectFactory.NewRenameStep(f.FeatureFileFromRunner(), OldName, NewName).Execute());
            
            // and now it is modified
            Directory.EnumerateFiles(".", "*.feature").ForEach( f => 
                File.WriteAllText(Path.GetFileNameWithoutExtension(f) + ".runner.cs",""));

            Thread.Sleep(100);

            RunnerFileWatcher.IsRunning.ShouldBeFalse("Watcher did not stop");
        }

        [TearDown]
        public void TearDown()
        {
            RunnerFileWatcher.Timeout = RunnerFileWatcher.DefaultTimeout;

            Directory.EnumerateFiles(".", "*.feature")
                .ForEach(File.Delete);

            Directory.EnumerateFiles(".", "*.runner.cs")
                .ForEach(File.Delete);
        }
    }
}
