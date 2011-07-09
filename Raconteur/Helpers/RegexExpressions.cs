using System;

namespace Raconteur.Helpers
{
    public class RegexExpressions
    {
        public static string FeatureDefinition 
        {
            get
            {
                return Settings.Language.Feature + @": (.+)(" +
                    Environment.NewLine + "|$)";
            }
        }
        
        public static string UsingStatement 
        {
            get
            {
                return Settings.Language.Using + @" (\w.+)(\r\n|$)";
            }
        }
        
        public static readonly string Punctuation = @"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]";
        public static readonly string NamespaceDeclaration = @"namespace (.+)";
        public static readonly string PartialClassDeclaration = @"public partial class (\w+)";
    }
}