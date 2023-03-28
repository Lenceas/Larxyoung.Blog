namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 测试表接口类
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// 获取一条测试信息
        /// </summary>
        /// <returns></returns>
        Task<TestWebModel> GetTestMsg();
    }
}