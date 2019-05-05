
namespace IECSC.Trans.Entity
{
    public class CommonOpcItems
    {
        /// <summary>
        /// 分组前缀
        /// </summary>
        public string TagGroupPrefix { get; set; }

        /// <summary>
        /// 分组标记 
        /// </summary>
        public string TagGroup { get; set; }

        /// <summary>
        /// 测点前缀
        /// </summary>
        public string TagPrefix { get; set; }

        /// <summary>
        /// 测点名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 测点索引
        /// </summary>
        public int TagIndex { get; set; }

        /// <summary>
        /// 机械手编号
        /// </summary>
        public string LocNo { get; set; }

        /// <summary>
        /// 业务唯一标示
        /// </summary>
        public string BusinessIdentity { get; set; }

        /// <summary>
        /// 测点长名
        /// </summary>
        public string TagLongName { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public int GroupIndex { get; set; }
    }
}
