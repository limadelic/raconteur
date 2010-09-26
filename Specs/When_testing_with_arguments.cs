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
//                The.StepFrom("Do what you like")
//                    .ShouldBe("Do_what_you_like");
        }
    }
}
