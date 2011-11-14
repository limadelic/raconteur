namespace Raconteur.IDE
{
    public class Project
    {
        public EnvDTE.Project DTEProject;

        public static Project LoadFrom(FeatureItem FeatureItem)
        {
            if (FeatureItem.Project == null) return null;

            var Result = new Project { DTEProject = FeatureItem.Project };
            
            Result.Load();

            return Result;
        }

        public void Load()
        {
            SettingsLoader.LoadFrom(this);
        }
    }
}