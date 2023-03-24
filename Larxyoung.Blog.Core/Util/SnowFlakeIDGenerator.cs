using Furion;
using SharpAbp.Abp.Snowflakes;

namespace Larxyoung.Blog.Core
{
    /// <summary>
    /// 雪花算法ID生成器（获取唯一值的场景可以使用）
    /// </summary>
    public static class SnowFlakeIDGenerator
    {
        private static readonly Snowflake snowflake = new(App.GetConfig<long>("SnowFlakeTwepoch"), App.GetConfig<int>("SnowFlakeWorkerIdBits"));

        /// <summary>
        /// 获取下一个ID
        /// </summary>
        /// <returns></returns>
        public static long NextLongID
        {
            get
            {
                return snowflake.NextId();
            }
        }
    }
}
