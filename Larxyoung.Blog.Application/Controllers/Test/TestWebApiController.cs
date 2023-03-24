using Furion.JsonSerialization;
using Microsoft.Extensions.Caching.Distributed;

namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [Produces("application/json")]
    [ApiDescriptionSettings(nameof(ApiVersions.V1))]
    public class TestWebApiController : IDynamicApiController
    {
        private readonly ITestService _testService;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;

        public TestWebApiController(ITestService testService, IMemoryCache memoryCache, IDistributedCache cache)
        {
            _testService = testService;
            _memoryCache = memoryCache;
            _cache = cache;
        }

        /// <summary>
        /// 获取一条测试信息（内存缓存）
        /// </summary>
        /// <returns></returns>
        [ApiDescriptionSettings(Version = "1.0")]
        [AllowAnonymous]
        public async Task<TestWebModel> GetTestMsgByMemoryCache()
        {
            try
            {
                return await _memoryCache.GetOrCreateAsync("Test", async entry =>
                {
                    entry.SetSlidingExpiration(TimeSpan.FromSeconds(5));
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
                    return await _testService.GetTestMsg();
                });
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }

        /// <summary>
        /// 获取一条测试信息（Redis缓存）
        /// </summary>
        /// <returns></returns>
        [ApiDescriptionSettings(Version = "1.0")]
        [AllowAnonymous]
        public async Task<TestWebModel> GetTestMsgByRedisCache()
        {
            try
            {
                var en = await _cache.GetStringAsync("Test:Test");
                if (!string.IsNullOrEmpty(en))
                {
                    return JSON.Deserialize<TestWebModel>(en);
                }
                else
                {
                    var r = await _testService.GetTestMsg();
                    await _cache.SetStringAsync("Test:Test", JSON.Serialize(r), new DistributedCacheEntryOptions()
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(20)
                    });
                    return r;
                }
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }
    }
}
