namespace Larxyoung.Blog.Web.Core
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugarSetup(this IServiceCollection services, IConfiguration configuration, string dbName = "db_mysql")
        {
            // 如果多个数据库传 List<ConnectionConfig>
            var configConnection = new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = configuration.GetConnectionString(dbName),
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityNameService = (type, entity) =>
                    {
                        // 全局AOP全部禁止删除列(CodeFirst)
                        //entity.IsDisabledDelete = true;
                        // 全局AOP全部禁止更新+删除(CodeFirst) - 比上面优先级更高
                        entity.IsDisabledUpdateAll = true;
                    },
                    EntityService = (c, p) =>
                    {
                        // 建表技巧：自动Nullable 
                        if (p.IsPrimarykey == false && new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                        {
                            p.IsNullable = true;
                        }
                    }
                }
            };

            SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
                db =>
                {
                    // 单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        // 输出sql
                        //Console.WriteLine(sql);
                    };
                });

            // 这边是SqlSugarScope用AddSingleton
            services.AddSingleton<ISqlSugarClient>(sqlSugar);
        }
    }
}
