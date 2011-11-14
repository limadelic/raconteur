using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace Raconteur.Resharper.Language
{
    [LanguageDefinition(Name, Edition = Name)]
    public class RaconteurLanguage : KnownLanguage
    {
        public new const string Name = "Raconteur";

        public static readonly RaconteurLanguage Instance = new RaconteurLanguage();

        private RaconteurLanguage() : this(Name, Name) {}

        protected RaconteurLanguage([NotNull] string Name) : base(Name) {}

        protected RaconteurLanguage([NotNull] string Name, [NotNull] string PresentableName)
          : base(Name, PresentableName) {}
    }
}
