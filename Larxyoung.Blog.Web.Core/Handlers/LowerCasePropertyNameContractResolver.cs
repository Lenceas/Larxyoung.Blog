using Newtonsoft.Json.Serialization;

namespace Larxyoung.Blog.Web.Core
{
    public class LowerCasePropertyNameContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            // 全部转小写
            return propertyName.ToLower();
        }
    }
}
