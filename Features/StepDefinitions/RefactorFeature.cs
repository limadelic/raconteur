using System;
using System.CodeDom.Compiler;
using System.IO;
using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using Specs;

namespace Features.StepDefinitions
{
    public class RefactorFeature
    {
        private readonly string DefinitionPath = Environment.CurrentDirectory + "TestDefinition.cs";

        public void Given_an_existing_Runner_and_Definition_file()
        {
//            var Definition = new StreamWriter(File.Create(DefinitionPath));
//            
//            Definition.Write(Actors.DefinitionCode);
//            Definition.Flush();
//            Definition.Close();

        }

        //going to refactor this... see if we can simplyfy the call.
        public void If_the_Feature_Name_has_changed()
        {
//            var Generator = ObjectFactory.NewRaconteurGenerator(new Project());
//
//            Generator.GenerateFeature(new FeatureFile(), 
//                CodeDomProvider.CreateProvider("C#"),
//                TextReader.Null, TextWriter.Null);
        }

        public void The_Definition_class_should_be_refactored()
        {
//            var Reader = new StreamReader(File.OpenRead(DefinitionPath));
//            var Definition = Reader.ReadToEnd();
//
//            Definition.ShouldContain("public class RenamedDefinition");
//
//            Reader.Close();
//            File.Delete(Definition);
        }
    }
}