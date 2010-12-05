using System.Collections.Generic;

namespace Raconteur
{
    public class XUnit
    {
        public string Name, Namespace, ClassAttr, MethodAttr;
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
                MethodAttr = "[TestMethod]"
            }},

            { "nunit", new XUnit
            {
                Name = "NUnit",
                Namespace = "NUnit.Framework",
                ClassAttr = "[TestFixture]",
                MethodAttr = "[Test]"
            }},

            { "mbunit", new XUnit
            {
                Name = "MbUnit",
                Namespace = "MbUnit.Framework",
                ClassAttr = "[TestFixture]",
                MethodAttr = "[Test]"
            }},

            { "xunit", new XUnit
            {
                Name = "xUnit",
                Namespace = "Xunit",
                ClassAttr = string.Empty,
                MethodAttr = "[Fact]"
            }},
        };
    }
}