using Furion;
using System.Reflection;

namespace Larxyoung.Blog.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "Larxyoung.Blog.Application",
            "Larxyoung.Blog.Core",
            "Larxyoung.Blog.Web.Core"
        };
    }
}