using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static CodeFunction CodeFunction(this Project Project, Step Step)
        {
            return Step.IsImplemented ? 
                Project.CodeFunction(Step.Implementation.Method) :
                Project.CodeFunctionForUnimplemented(Step);
        }

        static CodeFunction CodeFunction(this Project Project, MethodInfo Method)
        {
            return Project.Classes()
                .Where(x => x.FullName == Method.DeclaringType.FullName)
                .SelectMany(x => x.Functions())
                .FirstOrDefault(x => x.EqualsTo(Method));
        }

        static CodeFunction CodeFunctionForUnimplemented(this Project project, Step Step)
        {
            var Class = Step.Feature.Namespace + "." + Step.Feature.Name;

            return project.Classes()
                .Where(x => x.FullName == Class)
                .SelectMany(x => x.Functions())
                .FirstOrDefault(x => x.EqualsTo(Step));
        }

        static bool EqualsTo(this CodeFunction CodeFunction, MethodInfo Method)
        {
            return CodeFunction.Name == Method.Name 
                && CodeFunction.Parameters.Count == Method.GetParameters().Length;        
        }

        static bool EqualsTo(this CodeFunction CodeFunction, Step Step)
        {
            return CodeFunction.Name == Step.Name 
                && CodeFunction.Parameters.Count == Step.Args.Count;        
        }

        static IEnumerable<CodeFunction> Functions(this CodeClass codeClass)
        {
            return codeClass.Children.OfType<CodeFunction>();
        }
        
        static IEnumerable<CodeClass> Classes(this Project Project)
        {
            return Project.Items()
                .Where(x => x.FileCodeModel != null)
                .SelectMany(projectItem => projectItem.FileCodeModel.CodeElements.Classes());
        }        
        
        static IEnumerable<CodeClass> Classes(this CodeElements codeElements)
        {
            foreach (CodeElement CodeElement in codeElements)
            {
                switch (CodeElement.Kind)
                {
                    case vsCMElement.vsCMElementClass: 
                        yield return (CodeClass)CodeElement; 
                        break;
                    case vsCMElement.vsCMElementNamespace:
                        foreach (var Child in Classes(CodeElement.Children))
                            yield return Child;
                        break;
                }
            }
        }    
    }
}