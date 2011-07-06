using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Project = Raconteur.IDE.Project;

namespace Raconteur.Helpers
{
    public static class ProjectEx
    {
        public static IEnumerable<ProjectItem> Items(this Project Project) 
        {
            return Items(Project.DTEProject.ProjectItems);
        }

        static IEnumerable<ProjectItem> Items(ProjectItems ProjectItems)
        {
            return ProjectItems.Cast<ProjectItem>().SelectMany(Items);
        }

        public static IEnumerable<ProjectItem> Items(this ProjectItem Item)
        {
            yield return Item;

            if (Item.ProjectItems == null) yield break;

            foreach (var ChildItem in Items(Item.ProjectItems)) 
                yield return ChildItem;
        }

        public static string FeatureFileFromRunner(this string File)
        {
            return File.Replace(".runner.cs", ".feature");
        }
    }
}