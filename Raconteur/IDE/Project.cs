namespace Raconteur.IDE
{
    public class Project
    {
        EnvDTE.Project DTEProject;

        public virtual System.Xml.XmlDocument AppConfig { get { return null; } }

        public void Load()
        {
            LoadAppConfig();
        }

        public static void LoadFrom(FeatureItem FeatureItem)
        {
            if (FeatureItem.Project == null) return;

            new Project { DTEProject = FeatureItem.Project }
                .Load();
        }

        void LoadAppConfig()
        {
            Settings.XUnit = AppConfig.SelectSingleNode("/configuration/raconteur/xunit").InnerText;
        }
    }
}