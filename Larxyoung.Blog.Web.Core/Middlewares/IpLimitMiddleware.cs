using AspNetCoreRateLimit;
using Furion.FriendlyException;

namespace Larxyoung.Blog.Web.Core
{
    /// <summary>
    /// IPLimit限流 中间件
    /// </summary>
    public static class IpLimitMiddleware
    {
        public static void UseIpLimitMiddle(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (App.GetConfig<bool>("Middleware:IpRateLimit:Enabled"))
                {
                    app.UseIpRateLimiting();
                }
            }
            catch (Exception ex)
            {
                throw Oops.Bah($"IP限流配置错误：{ex.Message}");
            }
        }
    }
}
