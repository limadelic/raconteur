using System.Configuration;

namespace Raconteur
{
    public class ConfigurationSection : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("xUnit", IsRequired = false)]
        public XUnitConfigElement XUnit
        {
            get { return (XUnitConfigElement)this["xUnit"]; }
            set { this["xUnit"] = value; }
        }
    }

    public class XUnitConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, DefaultValue = "MbUnit")]
        [StringValidator(MinLength = 1)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }
    }
}