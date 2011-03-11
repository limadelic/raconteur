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
            SettingsLoader.LoadFrom(this);
        }
    }
}