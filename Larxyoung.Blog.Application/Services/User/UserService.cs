using Microsoft.AspNetCore.Http;

namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 用户表接口服务类
    /// </summary>
    public class UserService : SqlSugarRepository<UserEntity>, IUserService, ITransient
    {
        /// <summary>
        /// 是否启用登录验证码
        /// </summary>
        /// <returns></returns>
        public bool GetLoginShowCaptcha()
        {
            return App.GetConfig<bool>("LoginShowCaptcha");
        }

        /// <summary>
        /// 获取登录图片验证码
        /// </summary>
        /// <param name="codeLength">验证码长度</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>返回验证码和base64格式验证码图片字符串</returns>
        public (string code, string base64Code) GetLoginVerifyCode(int codeLength, int width, int height, int fontSize)
        {
            var (verificationCode, bytes) = VerifyCodeGenerator.CreateValidateGraphic(codeLength, width, height, fontSize);
            return (verificationCode, string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes)));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">登录入参WebModel</param>
        /// <returns></returns>
        public async Task<(TokenWebModel, TokenInfo)> Login(LoginWebModel model)
        {
            // 1.根据用户名和密码（MD5加密）去查数据库
            var pwd = MD5Encryption.Encrypt(model.PassWord);
            var user = await base.GetSingleAsync(_ => _.Account == model.UserName && _.Password == pwd);
            if (user != null)
            {
                // 2.根据用户信息JWT加密生成Token
                var jwtSettings = JWTEncryption.GetJWTSettings();
                if (jwtSettings != null)
                {
                    var tokenWebModel = new TokenWebModel()
                    {
                        Token = JWTEncryption.Encrypt(new Dictionary<string, object> {
                            { "uID", user.ID },
                            { "userName", user.UserName },
                            { "roleID", user.RoleID },
                            { "isSuperAdmin", user.ID == 99999 }
                        }, jwtSettings.ExpiredTime),
                        ExpiredTime = jwtSettings.ExpiredTime
                    };
                    var tokenInfo = new TokenInfo()
                    {
                        Token = tokenWebModel.Token,
                        ExpiredTime = (long)tokenWebModel.ExpiredTime,
                        SpecificExpiredTime = DateTime.Now.AddMinutes((long)tokenWebModel.ExpiredTime),
                        UID = user.ID,
                        UserName = user.UserName,
                        RoleID = (long)user.RoleID,
                        IsSuperAdmin = user.ID == 99999,
                    };

                    // 获取刷新 token
                    var refreshToken = JWTEncryption.GenerateRefreshToken(tokenWebModel.Token, 43200);

                    // 设置响应报文头 第二个参数是刷新 token 的有效期（分钟），默认三十天
                    App.HttpContext.Response.Headers["access-token"] = tokenWebModel.Token;
                    App.HttpContext.Response.Headers["x-access-token"] = refreshToken;

                    // 实现 Swagger 刷新也能记住登录
                    App.HttpContext.SigninToSwagger(tokenWebModel.Token);

                    return (tokenWebModel, tokenInfo);
                }
                else throw Oops.Bah("系统未获取到JWT配置！");
            }
            else throw Oops.Bah("账号或密码错误！");
        }
    }
}
