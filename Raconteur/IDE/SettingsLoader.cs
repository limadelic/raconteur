using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EnvDTE;

namespace Raconteur.IDE
{
    public class SettingsLoader
    {
        public static void LoadFrom(Project Project)
        {
            new SettingsLoader(Project).Load();
        }

        string[] settings;

        Project Project { get; set; }

        EnvDTE.Project DTEProject { get { return Project.DTEProject; } }

        public SettingsLoader(Project Project) 
        {
            this.Project = Project;
        }

        public void Load()
        {
            if (!HasSettingsFile)
            {
                Raconteur.Settings.RestoreDefaults();
                return;
            }

            Raconteur.Settings.Project = Project.DTEProject;

            settings = Regex.Split(SettingsFileContent, Environment.NewLine);

            var setting = Setting("xunit:").ToLower();
            if (!setting.IsEmpty() && XUnits.Framework.ContainsKey(setting)) 
                Raconteur.Settings.XUnit = XUnits.Framework[setting];
                    
            setting = Setting("language:").ToLower();
            if (!setting.IsEmpty() && Languages.In.ContainsKey(setting))
                Raconteur.Settings.Language = Languages.In[setting];

            var usings = Settings("using:");
            if (usings.HasItems()) 
                Raconteur.Settings.StepDefinitions = 
                    usings.Select(s => s.CamelCase()).ToList();

            Raconteur.Settings.Libraries = Settings("lib:");
        }
        
        string Setting(string SettingName)
        {
            var Setting = settings.FirstOrDefault(x => x.StartsWithEx(SettingName));
            return Setting == null ? null : Setting.YamlValue();
        }

        List<string> Settings(string SettingName)
        {
            if (!settings.Any(x => x.StartsWithEx(SettingName))) return new List<string>();

            return settings
                .Where(s => s.StartsWithEx(SettingName))
                .Select(s => s.YamlValue())
                .ToList();
        }

        public virtual bool HasSettingsFile
        {
            get { return DTEProject.Items().Any(IsSettingsFile); }
        }

        string SettingsFileName
        {
            get { return DTEProject.Items().First(IsSettingsFile).FileNames[1]; }
        }

        bool IsSettingsFile(ProjectItem Item) { return Item.Name.EqualsEx("raconteur.settings"); }

        public virtual string SettingsFileContent
        {
            get { return File.ReadAllText(SettingsFileName); }
        }
    }
}