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
        FeatureItem featureItem;
        public FeatureItem FeatureItem
        {
            get { return featureItem ?? LoadFeatureItem; } 
            set { featureItem = value; } 
        }

        FeatureItem LoadFeatureItem
        {
            get
            {
                var NewFeatureItem = ObjectFactory.FeatureItemFrom(FeatureFile);
                NewFeatureItem.DefaultNamespace = CodeFileNameSpace ?? NewFeatureItem.DefaultNamespace;
                return NewFeatureItem;
            }
        }

        protected override string GetDefaultExtension()
        {
            return ".runner." + GetCodeProvider().FileExtension;
        }

        public override string GenerateCode(string InputFileContent)
        {
            return ObjectFactory.NewRaconteurGenerator(FeatureItem)
                .Generate(CodeFilePath, InputFileContent);
        }
    }
}
