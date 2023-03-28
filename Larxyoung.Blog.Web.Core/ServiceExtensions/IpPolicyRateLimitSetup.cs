using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;

namespace Larxyoung.Blog.Web.Core
{
    /// <summary>
    /// IPLimit限流 启动服务
    /// </summary>
    public static class IpPolicyRateLimitSetup
    {
        public static void AddIpPolicyRateLimitSetup(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 需要存储速率限制计数器和ip规则
            services.AddMemoryCache();
            // 从配置文件加载常规配置
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            // 从配置加载IP规则
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            // 注入计数器和规则内存存储
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // 注入计数器和规则分布式缓存存储
            services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();

            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            // clientId/clientIp解析器使用
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // 配置（解析器、计数器密钥生成器）
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
