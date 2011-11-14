using System.IO;
using FluentSpec;
using MbUnit.Framework;
using Raconteur.Compilers;

namespace Features.Integration
{
    [TestFixture]
    public class When_loading_assemblies
    {
        private TypeResolverClass Sut;

        private readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        [SetUp]
        public void SetUp()
        {
            Sut = new TypeResolverClass();
        }

        [Test]
        public void should_find_types_in_different_locations()
        {
            var Assembly = Path.Combine(CurrentDirectory, "Features.dll");

            Sut.TypeOf("FeatureName", Assembly).Name.ShouldBe("FeatureName");

            Sut = new TypeResolverClass();

            Assembly = Path.Combine(CurrentDirectory, //"Examples.dll");
                @"..\..\..\Examples\bin\Debug\Examples.dll");

            Sut.TypeOf("BowlingGame", Assembly).Name.ShouldBe("BowlingGame");
        }

        [Ignore]
        [Test]
        public void should_find_types_on_assemblies_names_without_file_extension()
        {
            var Assembly = Path.Combine(CurrentDirectory, "Raconteur.Web.dll");

            Sut.TypeOf("Browser", Assembly).Name.ShouldBe("Browser");

            Assembly = Path.Combine(CurrentDirectory, "Raconteur.Web");

            Sut.TypeOf("Browser", Assembly).Name.ShouldBe("Browser");
        }
    }
}