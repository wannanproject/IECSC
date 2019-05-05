
using System.Collections.Generic;

namespace IECSC.CRN.SINGLE
{
    public class OpcGroup
    {
        /// <summary>
        /// PLC读取状态
        /// </summary>
        public PlcStatus crnPlcStatus = new PlcStatus();
        /// <summary>
        /// 读取项
        /// </summary>
        public List<OpcItem> OpcItemRead = new List<OpcItem>();
        /// <summary>
        /// 写入项
        /// </summary>
        public List<OpcItem> OpcItemWrite = new List<OpcItem>();
        /// <summary>
        /// 执行状态 
        /// </summary>
        public Enums.EquipStatus EquipStatus = Enums.EquipStatus.None;
        /// <summary>
        /// 任务信息
        /// </summary>
        public TaskCmd taskCmd = new TaskCmd();
        /// <summary>
        /// 指令结束请求主键
        /// </summary>
        public int objId = 0;
        /// <summary>
        /// 设备报警码
        /// </summary>
        public int FaultNo = 0;
    }
}
