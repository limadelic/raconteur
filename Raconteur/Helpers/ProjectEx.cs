using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace Raconteur
{
    public static class ProjectEx
    {
        public static IEnumerable<ProjectItem> Items(this Project Project) 
        {
            return Items(Project.ProjectItems);
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
    }
}