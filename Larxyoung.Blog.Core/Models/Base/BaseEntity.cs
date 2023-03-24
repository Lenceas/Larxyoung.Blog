namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键")]
        public long ID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDescription = "备注", IsNullable = true, CreateTableFieldSort = 95)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间(UTC)
        /// </summary>
        [SugarColumn(InsertServerTime = true, UpdateServerTime = true, ColumnDescription = "修改时间(UTC)", CreateTableFieldSort = 96)]
        public DateTime MTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 修改人ID
        /// </summary>
        [SugarColumn(ColumnDescription = "修改人ID", CreateTableFieldSort = 97)]
        public long? MID { get; set; } = 0;

        /// <summary>
        /// 创建时间(UTC)
        /// </summary>
        [SugarColumn(InsertServerTime = true, ColumnDescription = "创建时间(UTC)", CreateTableFieldSort = 98)]
        public DateTime CTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 创建人ID
        /// </summary>
        [SugarColumn(ColumnDescription = "创建人ID", CreateTableFieldSort = 99)]
        public long? CID { get; set; } = 0;
    }
}
