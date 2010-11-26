using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace Raconteur
{
    public static class ProjectEx
    {
        public static IEnumerable<ProjectItem> Items(this Project Project) 
        {
            return Project.ProjectItems
                .Cast<ProjectItem>()
                .SelectMany(Items);
        }

        static IEnumerable<ProjectItem> Items(ProjectItem Item)
        {
            yield return Item;

            if (Item.ProjectItems == null) yield break;
            
            foreach 
            (
                var ItemsItem 
                in Item.ProjectItems.Cast<ProjectItem>().SelectMany(Items)
            ) 
            yield return ItemsItem;
        }
    }
}