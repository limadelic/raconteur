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

        [SetUp]
        public void SetUp()
        {
            RunnerFileWatcher.Timeout = 100;
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

        void When__is_renamed_to(string OldName, string NewName)
        {
            RenameStepRefactoring = Substitute.For<RenameStepRefactoring>
            (
                null, 
                OldName.Replace(" ", "_"), 
                NewName.Replace(" ", "_")
            );
            
            RenameStepRefactoring.FeatureContent
                .Returns(FeatureRunner.FeatureContent);

            RenameStepRefactoring.Execute();
        }

        void Rename__to(string OldName, string NewName)
        {
            EnsureRunnerFileExists();

            RunnerFileWatcher.OnFileChange(f =>
                ObjectFactory.NewRenameStep
                (
                    f.FeatureFileFromRunner(), 
                    OldName.Replace(" ", "_"), 
                    NewName.Replace(" ", "_")
                )
                .Execute());
            
            ChangeRunnerFile();

            Thread.Sleep(200);

            RunnerFileWatcher.IsRunning.ShouldBeFalse("Watcher did not stop");
        }

        static void EnsureRunnerFileExists()
        {
            // The file needs to exist already
            Directory.EnumerateFiles(".", "*.feature").ForEach(f =>
            {
                var RunnerFile = Path.GetFileNameWithoutExtension(f) + ".runner.cs";

                if (!File.Exists(RunnerFile)) File.WriteAllText(RunnerFile, "");
            });
        }

        static void ChangeRunnerFile()
        {
            // and now it is modified
            Directory.EnumerateFiles(".", "*.feature").ForEach(
                f => File.WriteAllText(Path.GetFileNameWithoutExtension(f) + ".runner.cs", ""));
        }

        void The_Feature_should_contain(string Content)
        {
            RenameStepRefactoring.Received()
                .Write(Arg.Is<string>(s => s.Contains(Content.TrimLines())));
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

        [TearDown]
        public void TearDown()
        {
            RunnerFileWatcher.Timeout = RunnerFileWatcher.DefaultTimeout;

            Directory.EnumerateFiles(".", "*.feature")
                .ForEach(File.Delete);

            Directory.EnumerateFiles(".", "*.runner.cs")
                .ForEach(File.Delete);

            Thread.Sleep(50);

            RunnerFileWatcher.IsRunning.ShouldBeFalse("Watcher did not stop");
        }
    }
}
