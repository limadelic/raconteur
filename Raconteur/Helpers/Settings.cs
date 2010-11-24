using System;
using System.Collections.Generic;

namespace Raconteur
{
    enum XUnits
    {
        MSTEST, MBUNIT
    }
    public class Settings
    {
        static XUnits xUnit;
        public static string XUnit
        {
            get { return xUnit.ToString(); } 
            set { xUnit = (XUnits) Enum.Parse(typeof(XUnits), value.ToUpper()); }
        }

        public static string Namespace { get { return Namespaces[xUnit]; } }
        static readonly Dictionary<XUnits, string> Namespaces = new Dictionary<XUnits, string>
        {
            { XUnits.MSTEST, "Microsoft.VisualStudio.TestTools.UnitTesting"},
            { XUnits.MBUNIT, "MbUnit.Framework"},
        };

        public static string ClassAttr { get { return ClassAttrs[xUnit]; } }
        static readonly Dictionary<XUnits, string> ClassAttrs = new Dictionary<XUnits, string>
        {
            { XUnits.MSTEST, "TestClass"},
            { XUnits.MBUNIT, "TestFixture"},
        };

        public static string MethodAttr { get { return MethodAttrs[xUnit]; } }
        static readonly Dictionary<XUnits, string> MethodAttrs = new Dictionary<XUnits, string>
        {
            { XUnits.MSTEST, "TestMethod"},
            { XUnits.MBUNIT, "Test"},
        };
    }
}