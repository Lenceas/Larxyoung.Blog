namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// 登录入参WebModel
    /// </summary>
    public class LoginWebModel
    {
        /// <summary>
        /// 该次登录唯一标识
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")
            , MinLength(5, ErrorMessage = "请输入长度不少于5位的用户名!")
            , MaxLength(15, ErrorMessage = "请输入长度不多于15位的用户名!")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填")
            , MinLength(5, ErrorMessage = "请输入长度不少于5位的密码!")
            , MaxLength(32, ErrorMessage = "请输入长度不多于32位的密码!")]
        public string PassWord { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [MinLength(4, ErrorMessage = "请输入4位验证码!"), MaxLength(4, ErrorMessage = "请输入4位验证码!")]
        public string Captcha { get; set; }
    }
}
