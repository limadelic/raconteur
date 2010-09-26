using FluentSpec;
using MbUnit.Framework;
using Raconteur.Parsers;

namespace Specs 
{
    [TestFixture]
    public class When_testing_with_arguments : BehaviorOf<StepParserClass>
    {
        [Test]
        public void Step_should_contain_Args()
        {
            const string Sentence = @"If ""X"" happens";

            The.StepFrom(Sentence)
                .Name.ShouldBe("If__happens");

            The.StepFrom(Sentence)
                .Args.Count.ShouldBe(1);
                
            The.StepFrom(Sentence)
                .Args[0].ShouldBe("\"X\"");
        }

        [Test]
        public void Step_should_contain_multiple_Args()
        {
            const string Sentence = @"If ""X"" and ""Y"" happens";

            The.StepFrom(Sentence)
                .Name.ShouldBe("If__and__happens");

            The.StepFrom(Sentence).Args.Count.ShouldBe(2);
                
            The.StepFrom(Sentence)
                .Args[1].ShouldBe("\"Y\"");
        }
    }
}
