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

        public TestWebApiController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// 获取一条测试信息
        /// </summary>
        /// <returns></returns>
        [ApiDescriptionSettings(Version = "1.0")]
        [AllowAnonymous]
        public async Task<TestWebModel> GetTestMsg()
        {
            try
            {
                return await _testService.GetTestMsg();
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }
    }
}
