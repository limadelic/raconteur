using System.CodeDom.Compiler;
using System.Globalization;

namespace Raconteur
{
    public enum GenerationTargetLanguage
    {
        CSharp,
        VB,
        Other
    }

    public interface ICodeDomHelperRequired
    {
        CodeDomHelper CodeDomHelper { get; set; }
    }

    public class CodeDomHelper
    {
        public GenerationTargetLanguage TargetLanguage { get; private set; }

        public CodeDomHelper(CodeDomProvider codeComProvider)
        {
            switch (codeComProvider.FileExtension.ToLower(CultureInfo.InvariantCulture))
            {
            case "cs":
                TargetLanguage = GenerationTargetLanguage.CSharp;
                break;
            case "vb":
                TargetLanguage = GenerationTargetLanguage.VB;
                break;
            default:
                TargetLanguage = GenerationTargetLanguage.Other;
                break;
            }
        }

        public CodeDomHelper(GenerationTargetLanguage targetLanguage) { TargetLanguage = targetLanguage; }
    }
}