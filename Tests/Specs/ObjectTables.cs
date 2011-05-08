using Common;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Specs
{
    [TestFixture]
    public class ObjectTables
    {
        Feature Feature;
        readonly FeatureItem FeatureItem = Actors.FeatureItem("Common");
        
        Step Step { get { return Feature.Steps[0]; } }

        [SetUp]
        public void SetUp()
        {
            Feature = Actors.FeatureWithStepDefinitions;
            Step.Table = Actors.ObjectTable;
        }

        void Compile() { ObjectFactory.NewFeatureCompiler.Compile(Feature, FeatureItem); }

        void ShouldGenerate(string Code)
        {
            ObjectFactory.NewRunnerGenerator(Feature)
                .Code.TrimLines().ShouldContain(Code.TrimLines());
        }

        [Test]
        public void should_resolve_ObjectTable_to_object_Arg()
        {
            Step.Name = "Step_with_object";

            Compile();

            Step.Implementation.ShouldBe(StepDefinitions.StepWithObject);
        }

        [Test]
        [Row("UserName")]
        [Row("User Name")]
        [Row("user name")]
        public void should_resolve_Header_to_Fields(string UserName)
        {
            Step.Name = "Step_with_object";
            Step.Table.Header[0] = UserName;

            Compile();

            Step.Implementation.ShouldBe(StepDefinitions.StepWithObject);
        }

        [Test]
        public void should_pass_the_object_in_the_call()
        {
            Step.Name = "Step_with_object";
            Step.Type = StepType.ObjectTable;
            Step.Implementation = StepDefinitions.StepWithObject;

            ShouldGenerate(
            @"
			    Step_with_object( 
				    new Common.User
				    {
					    UserName = ""lola"",
					    Password = ""run""
				    });
            ");
        }

        [Test]
        public void should_call_an_object_per_Row()
        {
            Step.Name = "Step_with_object";
            Step.Type = StepType.ObjectTable;
            Step.Implementation = StepDefinitions.StepWithObject;

            ShouldGenerate(
            @"
			    Step_with_object( 
				    new Common.User
				    {
					    UserName = ""mani"",
					    Password = ""dumb""
				    });
            ");
        }

        [Test]
        public void should_resolve_ObjectTable_to_params_object_array_Arg()
        {
            Step.Name = "Step_with_object_array";

            Compile();

            Step.Implementation.ShouldBe(StepDefinitions.StepWithObjectArray);        
        }

        [Test]
        public void should_generate_params_object_array_Arg()
        {
            Step.Name = "Step_with_object_array";
            Step.Type = StepType.ObjectTable;
            Step.Implementation = StepDefinitions.StepWithObjectArray;

            ShouldGenerate(
            @"
			    Step_with_object_array
                ( 
					new Common.User
					{
						UserName = ""lola"",
						Password = ""run""
					},				
					new Common.User
					{
						UserName = ""mani"",
						Password = ""dumb""
					}
                );
            ");
        }
    }
}