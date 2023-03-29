namespace Larxyoung.Blog.Application
{
    /// <summary>
    /// 基础权限接口
    /// </summary>
    [Produces("application/json")]
    [ApiDescriptionSettings(nameof(ApiVersions.V1))]
    public class BaseAuthApiController : IDynamicApiController
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;

        public BaseAuthApiController(IUserService userService, IMemoryCache memoryCache, IDistributedCache cache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
            _cache = cache;
        }

        /// <summary>
        /// 是否启用登录验证码
        /// </summary>
        /// <returns></returns>
        [ApiDescriptionSettings(Tag = "后台登录页", Version = "1.0")]
        [AllowAnonymous]
        public bool GetLoginShowCaptcha()
        {
            try
            {
                return _userService.GetLoginShowCaptcha();
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }

        /// <summary>
        /// 获取登录图片验证码
        /// </summary>
        /// <param name="id">该次登录唯一标识</param>
        /// <returns>base64Code</returns>
        [ApiDescriptionSettings(Tag = "后台登录页", Version = "1.0")]
        [AllowAnonymous]
        public async Task<string> GetLoginVerifyCode([Required] string id)
        {
            try
            {
                if (_userService.GetLoginShowCaptcha())
                {
                    var (code, base64Code) = _userService.GetLoginVerifyCode(4, 116, 46, 22);
                    // 把唯一标识和验证码 缓存到内存并设置过期时间
                    await _cache.SetStringAsync($"{App.GetConfig<string>("Redis:CaptchaName")}{id}", code, new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(60) });
                    return base64Code;
                }
                else throw Oops.Bah("尚未启用登录验证码功能！");
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">登录入参WebModel</param>
        /// <returns></returns>
        [ApiDescriptionSettings(Tag = "后台登录页", Version = "1.0")]
        [AllowAnonymous]
        public async Task<TokenWebModel> Login(LoginWebModel model)
        {
            try
            {
                // 判断是否启用了登录验证码
                if (_userService.GetLoginShowCaptcha())
                {
                    // 根据该次登录唯一标识和验证码验证Redis缓存里的值是否一致
                    var captcha = await _cache.GetStringAsync($"{App.GetConfig<string>("Redis:CaptchaName")}{model.ID}");
                    if (!string.IsNullOrEmpty(captcha) && captcha.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                    {
                        var (tokenWebModel, tokenInfo) = await _userService.Login(model);
                        // 把登录用户信息缓存到Redis
                        await _cache.SetStringAsync($"{App.GetConfig<string>("Redis:TokenName")}{tokenInfo.UID}", JSON.Serialize(tokenInfo), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes((double)tokenInfo.ExpiredTime) });
                        return tokenWebModel;
                    }
                    else throw Oops.Bah("验证码错误或已过期,请点击验证码刷新并重新登录！");
                }
                else
                {
                    var (tokenWebModel, tokenInfo) = await _userService.Login(model);
                    // 把登录用户信息缓存到Redis
                    await _cache.SetStringAsync($"{App.GetConfig<string>("Redis:TokenName")}{tokenInfo.UID}", JSON.Serialize(tokenInfo), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes((double)tokenInfo.ExpiredTime) });
                    return tokenWebModel;
                }
            }
            catch (AppFriendlyException ex)
            {
                throw Oops.Bah(ex.Message);
            }
        }
    }
}
