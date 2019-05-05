
namespace IECSC.CRN.SINGLE
{
    public class TaskCmd
    {
        /// <summary>
        /// ID序号
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 当前任务号
        /// </summary>
        public int TaskNo { get; set; }
        /// <summary>
        /// 起始位置类型
        /// </summary>
        public string SlocType { get; set; }
        /// <summary>
        /// 起始地址
        /// </summary>
        public string SlocNo { get; set; }
        /// <summary>
        /// 起始PLC地址
        /// </summary>
        public string SlocPlcNo { get; set; }
        /// <summary>
        /// 结束地址类型
        /// </summary>
        public string ElocType { get; set; }
        /// <summary>
        /// 结束地址
        /// </summary>
        public string ElocNo { get; set; }
        /// <summary>
        /// 结束PLC地址
        /// </summary>
        public string ElocPlcNo { get; set; }
        /// <summary>
        /// RFID号码
        /// </summary>
        public string PalletNo { get; set; }
        /// <summary>
        /// 指令类型
        /// </summary>
        public string CmdType { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string ProductNo { get; set; }
        /// <summary>
        /// 指令步骤
        /// </summary>
        public string CmdStep { get; set; }
    }
}
