using Furion;

namespace Larxyoung.Blog.Core
{
    public class SqlSugarRepository<T> : SimpleClient<T> where T : class, new()
    {
        public SqlSugarRepository(ISqlSugarClient context = null) : base(context)// 默认值等于null不能少
        {
            // 用手动获取方式支持切换仓储
            base.Context = App.GetService<ISqlSugarClient>();
        }
    }
}
