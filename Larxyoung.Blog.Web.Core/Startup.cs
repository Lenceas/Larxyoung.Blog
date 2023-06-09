﻿using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text.Json;

namespace Larxyoung.Blog.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // IP限流（放最前面）
        services.AddIpPolicyRateLimitSetup(App.Configuration);
        // 控制台默认格式化器
        services.AddConsoleFormatter();
        // JWT 拦截器
        services.AddJwt<JwtHandler>();
        // SqlSugar
        services.AddSqlSugarSetup(App.Configuration);
        // 跨域
        services.AddCorsAccessor();
        // Redis缓存
        services.AddStackExchangeRedisCache(options =>
        {
            // 连接字符串
            options.Configuration = App.GetConfig<string>("Redis:ConnectionStrings");
            // 键名前缀
            options.InstanceName = App.GetConfig<string>("Redis:InstanceName");
        });
        // Mapster
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        // 控制器
        services.AddControllers()
                // Json 序列化配置
                .AddNewtonsoftJson(options =>
                {
                    // long 类型序列化时转 string
                    options.SerializerSettings.Converters.AddLongTypeConverters();
                    // 时间格式化
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    // 序列化属性名全部小写
                    options.SerializerSettings.ContractResolver = new LowerCasePropertyNameContractResolver();
                })
                // 注入基础配置和规范化结果
                .AddInjectWithUnifyResult();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 是否开发环境
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        // IP限流
        app.UseIpLimitMiddle();
        // 规范化结果状态码
        app.UseUnifyResultStatusCodes();
        // Https
        //app.UseHttpsRedirection();
        // 路由
        app.UseRouting();
        // 跨域
        app.UseCorsAccessor();
        // 认证
        app.UseAuthentication();
        // 授权
        app.UseAuthorization();
        // 注入基础中间件(带Swagger)
        app.UseInject(string.Empty);
        // 端点
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        // 生成数据种子
        app.UseSeedDataMildd();
    }
}
