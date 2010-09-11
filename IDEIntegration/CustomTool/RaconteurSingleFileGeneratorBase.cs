using System.IO;
using System.Runtime.InteropServices;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("0509186D-E29A-49C2-A71F-0E530E52D199")]
    public abstract class RaconteurSingleFileGeneratorBase : BaseCodeGeneratorWithSite
    {
        Project project;
        protected Project Project
        {
            get { return project ?? LoadProject; } 
            set { project = value; } 
        }
        Project LoadProject
        {
            get
            {
                var NewProject = DteProjectReader.LoadProjectFrom(CurrentProject);
                NewProject.DefaultNamespace = NewProject.DefaultNamespace ?? CodeFileNameSpace;
                return NewProject;
            }
        }

        protected override string GetDefaultExtension()
        {
            return "." + GetCodeProvider().FileExtension;
        }

        public override string GenerateCode(string InputFileContent)
        {
            var Generator = ObjectFactory.NewRaconteurGenerator(Project);
            var FeatureFile = new FeatureFile(CodeFilePath){Content = InputFileContent};

            using (var Writer = new StringWriter())
            {
                Generator.GenerateFeature(FeatureFile, Writer);

                return Writer.ToString();
            }
        }
    }
}