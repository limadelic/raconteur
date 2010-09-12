using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Raconteur.IDE;

namespace Raconteur.IDEIntegration
{
    [ComVisible(true)]
    [Guid("747D47AC-4681-4B88-8218-623AC7C70145")]
    [ProvideObject(typeof(RaconteurSingleFileGenerator))]
    public class RaconteurSingleFileGenerator : BaseCodeGeneratorWithSite
    {
        Project project;
        public Project Project
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
            return ".runner." + GetCodeProvider().FileExtension;
        }

        public override string GenerateCode(string InputFileContent)
        {
            return ObjectFactory.NewRaconteurGenerator(Project)
                .Generate(CodeFilePath, InputFileContent);
        }
    }
}
