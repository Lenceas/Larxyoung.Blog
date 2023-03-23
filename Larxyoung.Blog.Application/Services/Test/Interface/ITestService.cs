namespace Larxyoung.Blog.Application
{
    public interface ITestService
    {
        /// <summary>
        /// 获取一条测试信息
        /// </summary>
        /// <returns></returns>
        Task<TestWebModel> GetTestMsg();
    }
}