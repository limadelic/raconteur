using System.Collections.Generic;

namespace Raconteur.Helpers
{
    public class XUnit
    {
        public string 
            Name, 
            Namespace, 
            ClassAttr = "[TestFixture]", 
            MethodAttr = "[Test]", 
            Category = @"[Category(""{0}"")]", 
            Ignore = "[Ignore]", 
            IgnoreWithReason = @"[Ignore(""{0}"")]";
    }

    public class XUnits
    {
        public static readonly Dictionary<string, XUnit> Framework = new Dictionary<string, XUnit>
        {
            { "mstest", new XUnit
            {
                Name = "MsTest",
                Namespace = "Microsoft.VisualStudio.TestTools.UnitTesting",
                ClassAttr = "[TestClass]",
                MethodAttr = "[TestMethod]",
                Category = @"[TestCategory(""{0}"")]",
                IgnoreWithReason = @"[Ignore] // {0}",
            }},

            { "nunit", new XUnit
            {
                Name = "NUnit",
                Namespace = "NUnit.Framework",
            }},

            { "mbunit", new XUnit
            {
                Name = "MbUnit",
                Namespace = "MbUnit.Framework",
            }},

            { "xunit", new XUnit
            {
                Name = "xUnit",
                Namespace = "Xunit",
                ClassAttr = string.Empty,
                MethodAttr = "[Fact]",
                Category = @"[Trait(""Category"", ""{0}"")]",
                Ignore = string.Empty,
                IgnoreWithReason = string.Empty
/*
                TODO [Fact] can be used only once                
                Ignore = @"[Fact(Skip="""")]",
                IgnoreWithReason = @"[Fact(Skip=""{0}"")]",
*/
            }},
        };
    }
}