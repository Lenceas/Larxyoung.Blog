﻿namespace Larxyoung.Blog.Web.Core
{
    /// <summary>
    /// 生成数据种子中间件服务
    /// </summary>
    public static class SeedDataMilddleware
    {
        public static void UseSeedDataMildd(this IApplicationBuilder app, ISqlSugarClient db = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (db == null) db = App.GetService<ISqlSugarClient>();

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

                #region 用户表 User
                db.CodeFirst.SetStringDefaultLength(255).InitTables(typeof(UserEntity));
                // 初始化超级管理员
                if (!db.Queryable<UserEntity>().Any(_ => _.ID == 99999 && _.RoleID == 99999))
                {
                    db.Insertable(new UserEntity()
                    {
                        ID = 99999,
                        Account = "admin",
                        Password = MD5Encryption.Encrypt("admin888"),
                        UserName = "卢杰晟",
                        RoleID = 99999,
                        CID = 99999,
                        MID = 99999,
                    }).ExecuteCommand();
                }
                Console.WriteLine("User 用户表 数据初始化成功！");
                #endregion

                Console.WriteLine();
                Console.WriteLine("数据初始化完成!");
                Console.WriteLine();

                Console.WriteLine("************ 自动初始化数据完成 *****************");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("【生成数据种子】错误信息：" + ex.Message);
            }
        }
    }
}
