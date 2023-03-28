namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 用户表接口类
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 是否启用登录验证码
        /// </summary>
        /// <returns></returns>
        bool GetLoginShowCaptcha();

        /// <summary>
        /// 获取登录图片验证码
        /// </summary>
        /// <param name="codeLength">验证码长度</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>返回验证码和base64格式验证码图片字符串</returns>
        (string code, string base64Code) GetLoginVerifyCode(int codeLength, int width, int height, int fontSize);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">登录入参WebModel</param>
        /// <returns></returns>
        Task<(TokenWebModel, TokenInfo)> Login(LoginWebModel model);
    }
}
