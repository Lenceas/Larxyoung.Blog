namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// Token令牌详细信息
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间（单位：分钟）
        /// </summary>
        public long ExpiredTime { get; set; }

        /// <summary>
        /// 具体过期时间
        /// </summary>
        public DateTime SpecificExpiredTime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }
    }
}
