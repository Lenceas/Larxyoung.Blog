namespace Larxyoung.Blog.Web.Core
{
    /// <summary>
    /// 数据种子中间件服务
    /// </summary>
    public static class SeedDataMilddleware
    {
        public static void UseSeedDataMildd(SqlSugarScope db)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("************ 开始自动初始化数据 *****************");
                Console.WriteLine();

                Console.WriteLine("开始重置数据库...");
                if (db.DbMaintenance.CreateDatabase())
                {
                    Console.WriteLine("数据库重置成功!");
                    Console.WriteLine();
                }

                Console.WriteLine("开始初始化数据...");
                Console.WriteLine();

                #region 测试表 Test
                db.CodeFirst.SetStringDefaultLength(255).InitTables(typeof(TestEntity));
                Console.WriteLine("Test 测试表 数据初始化成功!");
                #endregion

                Console.WriteLine();
                Console.WriteLine("数据初始化完成!");
                Console.WriteLine();

                Console.WriteLine("************ 自动初始化数据完成 *****************");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("【数据种子】错误信息：" + ex.Message);
            }
        }
    }
}
