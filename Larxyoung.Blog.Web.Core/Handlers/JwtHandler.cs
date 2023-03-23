using Furion.Authorization;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Larxyoung.Blog.Web.Core;

/// <summary>
/// JWT 授权自定义处理程序
/// </summary>
public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    /// 重写 Handler 添加自动刷新收取逻辑
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
        var jwtSettings = JWTEncryption.GetJWTSettings();
        if (jwtSettings != null)
        {
            // 自动刷新 token
            if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext(), jwtSettings.ExpiredTime, (int)jwtSettings.ExpiredTime, clockSkew: (long)jwtSettings.ClockSkew))
            {
                await AuthorizeHandleAsync(context);
            }
            else context.Fail();// 授权失败
        }
        else context.Fail();// 授权失败
    }

    /// <summary>
    /// 请求管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 此处已经自动验证 Jwt token的有效性了，无需手动验证
        // 检查权限，如果方法是异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
        return Task.FromResult(CheckAuthorzie(httpContext));
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static bool CheckAuthorzie(DefaultHttpContext httpContext)
    {
        // 获取权限特性
        return httpContext.GetMetadata<SecurityDefineAttribute>() == null;
    }
}
