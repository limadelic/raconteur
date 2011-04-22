using System;

namespace Raconteur.Helpers
{
    public class RegexExpressions
    {
        public static readonly string FeatureDefinition = 
            Settings.Language.Feature + @": (\w.+)(" + 
            Environment.NewLine + "|$)";

        public static readonly string UsingStatement =
            Settings.Language.Using + @" (\w.+)(\r\n|$)";

        public static readonly string Punctuation = @"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]";
        public static readonly string NamespaceDeclaration = @"namespace (.+)";
        public static readonly string PartialClassDeclaration = @"public partial class (\w+)";
    }
}