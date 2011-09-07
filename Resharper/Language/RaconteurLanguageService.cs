using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;

namespace Raconteur.Resharper.Language
{
    [Language(typeof(RaconteurLanguage))]
    public class RaconteurLanguageService : LanguageService
    {
        public RaconteurLanguageService(RaconteurLanguage RaconteurLanguage) 
            : base(RaconteurLanguage)
        {
        }

        public override ILexerFactory GetPrimaryLexerFactory()
        {
            return null;
        }

        public override ILexer CreateFilteringLexer(ILexer lexer)
        {
            return null;
        }

        public override string GetTokenReprByTokenType(TokenNodeType token)
        {
            return null;
        }

        public override string GetTokenContentsText(string tokenText, TokenNodeType tokenType)
        {
            return null;
        }

        public override IParser CreateParser(ILexer lexer, ISolution solution, IPsiModule module, IPsiSourceFile sourceFile)
        {
            return null;
        }

        public override bool IsFilteredNode(ITreeNode node)
        {
            return false;
        }

        public override IWordIndexLanguageProvider WordIndexLanguageProvider
        {
            get
            {
                return null;
            }
        }

        public override ILanguageCacheProvider CacheProvider
        {
            get
            {
                return null;
            }
        }

        public override ITypePresenter TypePresenter
        {
            get
            {
                return null;
            }
        }
    }
}