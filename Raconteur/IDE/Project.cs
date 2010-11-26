using System.IO;
using System.Linq;
using EnvDTE;

namespace Raconteur.IDE
{
    public class Project
    {
        EnvDTE.Project DTEProject;

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

        string AppConfigFile { get { return DTEProject.Items().First(IsAppConfig).Name; } }

        bool IsAppConfig(ProjectItem Item) { return Item.Name.EqualsInv("app.config"); }

        public virtual System.Xml.XmlDocument AppConfig
        {
            get { return XmlDocument.Load(File.ReadAllText(AppConfigFile).ToLower()); }
        }

        void LoadAppConfig() 
        {
            if (!HasAppConfig) return;

            Settings.XUnit = AppConfig.SelectSingleNode("/configuration/raconteur/xunit").InnerText;
        }

        #endregion
    }
}