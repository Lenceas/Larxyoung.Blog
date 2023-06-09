﻿namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 测试表接口服务类
    /// </summary>
    public class TestService : SqlSugarRepository<TestEntity>, ITestService, ITransient
    {
        /// <summary>
        /// 获取一条测试信息
        /// </summary>
        /// <returns></returns>
        public async Task<TestWebModel> GetTestMsg()
        {
            var list = await base.GetListAsync();
            if (!list.Any())
            {
                var en = await base.InsertReturnEntityAsync(new TestEntity() { ID = SnowFlakeIDGenerator.NextLongID, CID = 99999, MID = 99999 });
                return en.Adapt<TestWebModel>();
            }
            else
            {
                return list.OrderByDescending(_ => _.MTime).Adapt<List<TestWebModel>>().ToList().FirstOrDefault();
            }
        }
    }
}
