using System.Drawing;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;

namespace Raconteur.Resharper.Language
{
    [ProjectFileType(typeof(RaconteurProjectFileType))]
    public class RaconteurProjectFileLanguageService : IProjectFileLanguageService
    {
        readonly RaconteurProjectFileType RaconteurProjectFileType;

        public RaconteurProjectFileLanguageService(RaconteurProjectFileType RaconteurProjectFileType) 
        {
            this.RaconteurProjectFileType = RaconteurProjectFileType;    
        }

        public PsiLanguageType GetPsiLanguageType(IProjectFile ProjectFile) 
        {
            return ProjectFile.LanguageType.Is<RaconteurProjectFileType>() ? 
                (PsiLanguageType) RaconteurLanguage.Instance : 
                UnknownLanguage.Instance;
        }

        public IPsiSourceFileProperties GetPsiProperties(IProjectFile projectFile, IPsiSourceFile sourceFile)
        {
            return null;
        }

        public PsiLanguageType GetPsiLanguageType(ProjectFileType LanguageType) 
        {
            return LanguageType.Is<RaconteurProjectFileType>() ? 
                (PsiLanguageType) RaconteurLanguage.Instance : 
                UnknownLanguage.Instance;
        }

        public ILexerFactory GetMixedLexerFactory(IBuffer buffer, IPsiSourceFile sourceFile, PsiManager manager)
        {
            var LanguageService = RaconteurLanguage.Instance.LanguageService();

            return LanguageService == null ? null 
                : LanguageService.GetPrimaryLexerFactory();
        }

        public PreProcessingDirective[] GetPreprocessorDefines(IProject project)
        {
            return EmptyArray<PreProcessingDirective>.Instance;
        }

        public ProjectFileType LanguageType { get { return RaconteurProjectFileType; } }

        public Image Icon { get { return null; } }
    }
}