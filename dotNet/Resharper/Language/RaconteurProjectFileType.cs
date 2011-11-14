using JetBrains.ProjectModel;

namespace Raconteur.Resharper.Language
{
    [ProjectFileTypeDefinition(Name, Edition = Name)]
    public class RaconteurProjectFileType : KnownProjectFileType
    {
        public new const string Name = RaconteurLanguage.Name;

        public const string Extension = ".feature";
        
        public new static readonly RaconteurProjectFileType Instance = new RaconteurProjectFileType();

        RaconteurProjectFileType()
            : base(Name, Name, new[] { Extension })
        {
        }

        protected RaconteurProjectFileType(string Name)
            : base(Name)
        {
        }

        protected RaconteurProjectFileType(string Name, string PresentableName)
            : base(Name, PresentableName)
        {
        }
    }
}