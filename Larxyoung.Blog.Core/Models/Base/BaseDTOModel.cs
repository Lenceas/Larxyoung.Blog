namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// DTOModel基类
    /// </summary>
    public class BaseDTOModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime MTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public long? MID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CID { get; set; }
    }
}
