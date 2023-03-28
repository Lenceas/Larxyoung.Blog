namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("User", TableDescription = "用户表", IsCreateTableFiledSort = true)]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(ColumnDescription = "账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnDescription = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(ColumnDescription = "用户名称", IsNullable = true)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(ColumnDescription = "角色ID")]
        public long? RoleID { get; set; } = 0;

        /// <summary>
        /// 登录令牌
        /// </summary>
        [SugarColumn(ColumnDescription = "登录令牌", IsNullable = true)]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 最后登录时间(UTC)
        /// </summary>
        [SugarColumn(ColumnDescription = "最后登录时间(UTC)")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP地址
        /// </summary>
        [SugarColumn(ColumnDescription = "最后登录IP地址", IsNullable = true)]
        public string LastLoginIP { get; set; } = string.Empty;
    }
}
