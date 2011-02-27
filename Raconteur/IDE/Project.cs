using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            LoadSettings();
        }

        // breaking SRP shall eventually refactor
        #region Settings

        string[] settings;

        void LoadSettings()
        {
            if (!HasSettingsFile)
            {
                Raconteur.Settings.RestoreDefaults();
                return;
            }

            Raconteur.Settings.Project = DTEProject;

            this.settings = Regex.Split(SettingsFileContent, Environment.NewLine);

            var setting = Setting("xunit:");
            if (!setting.IsEmpty() && XUnits.Framework.ContainsKey(setting)) 
                Raconteur.Settings.XUnit = XUnits.Framework[setting];
                    
            setting = Setting("language:");
            if (!setting.IsEmpty() && Languages.In.ContainsKey(setting))
                Raconteur.Settings.Language = Languages.In[setting];

            var settings = Settings("using:");
            if (settings.HasItems()) 
                Raconteur.Settings.StepLibraries = 
                    settings.Select(s => s.CamelCase()).ToList();
        }
        
        string Setting(string SettingName)
        {
            if (!settings.Any(x => x.StartsWith(SettingName))) return null;

            return settings
                .First(x => x.StartsWith(SettingName))
                .Split(A.Colon, 2)[1].Trim();
        }

        List<string> Settings(string SettingName)
        {
            if (!settings.Any(x => x.StartsWith(SettingName))) return new List<string>();

            return settings
                .Where(s => s.StartsWith(SettingName))
                .Select(s => s.Split(A.Colon, 2)[1].Trim())
                .ToList();
        }

        public virtual bool HasSettingsFile { get { return DTEProject.Items().Any(IsSettingsFile); } }

        string SettingsFileName { get { return DTEProject.Items().First(IsSettingsFile).FileNames[1]; } }

        bool IsSettingsFile(ProjectItem Item) { return Item.Name.EqualsEx("raconteur.settings.txt"); }

        public virtual string SettingsFileContent
        {
            get { return File.ReadAllText(SettingsFileName).ToLower(); }
        }

        #endregion
    }
}