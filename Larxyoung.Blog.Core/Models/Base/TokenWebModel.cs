namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// Token令牌信息
    /// </summary>
    public class TokenWebModel
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间（单位：分钟）
        /// </summary>
        public long? ExpiredTime { get; set; }
    }
}
