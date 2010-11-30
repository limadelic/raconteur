using System.IO;
using System.Linq;
using EnvDTE;

namespace Raconteur.IDE
{
    public class Project
    {
        public EnvDTE.Project DTEProject;

        public static void LoadFrom(FeatureItem FeatureItem)
        {
            if (FeatureItem.Project == null) return;

            new Project { DTEProject = FeatureItem.Project }
                .Load();
        }

        public void Load()
        {
            LoadAppConfig();
        }

        #region AppConfig
        
        public virtual bool HasAppConfig { get { return DTEProject.Items().Any(IsAppConfig); } }

        string AppConfigFile { get { return DTEProject.Items().First(IsAppConfig).FileNames[1]; } }

        bool IsAppConfig(ProjectItem Item) { return Item.Name.EqualsEx("app.config"); }

        public virtual System.Xml.XmlDocument AppConfig
        {
            get { return XmlDocument.Load(File.ReadAllText(AppConfigFile).ToLower()); }
        }

        void LoadAppConfig() 
        {
            if (!HasAppConfig) return;

            var ConfigSection = AppConfig.SelectSingleNode("/configuration/raconteur");
            
            if (ConfigSection == null) return;

            Settings.XUnit = ConfigSection.SelectSingleNode("xunit").Attributes["name"].Value;

            var Language = ConfigSection.SelectSingleNode("language").Attributes["code"].Value;
            Languages.Current = Languages.All[Language];
        }

        #endregion
    }
}