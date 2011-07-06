using System.IO;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Compilers;

namespace Features.Integration
{
    [TestFixture]
    public class When_loading_assemblies
    {
        TypeResolverClass Sut;

        readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        [Test]
        public void should_find_types_in_different_locations()
        {
            Sut = new TypeResolverClass();
            
            var Assembly = Path.Combine(CurrentDirectory, "Features.dll");

            Sut.TypeOf("FeatureName", Assembly).Name.ShouldBe("FeatureName");

            Sut = new TypeResolverClass();

            Assembly = Path.Combine(CurrentDirectory, //"Examples.dll");
                @"..\..\..\Examples\bin\Debug\Examples.dll");

            Sut.TypeOf("BowlingGame", Assembly).Name.ShouldBe("BowlingGame");
        }
    }
}