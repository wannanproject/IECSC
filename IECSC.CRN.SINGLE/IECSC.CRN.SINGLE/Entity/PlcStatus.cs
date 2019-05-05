using System.ComponentModel;

namespace IECSC.CRN.SINGLE
{
    public class PlcStatus
    {
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        [DisplayName("堆垛机编号")]
        public string CrnNo { get; set; }
        /// <summary>
        /// 堆垛机名称
        /// </summary>
        [DisplayName("堆垛机名称")]
        public string CrnName { get; set; }
        /// <summary>
        /// PLC传递名称
        /// </summary>
        [DisplayName("读:设备名称")]
        public string DeviceId { get; set; }
        /// <summary>
        /// 心跳信息
        /// </summary>
        [DisplayName("读:心跳信号")]
        public int HeartBeat { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        [DisplayName("读:设备状态")]
        public int OperateMode { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        [DisplayName("读:任务状态")]
        public int MissionState { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [DisplayName("读:任务类型")]
        public int MissionType { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [DisplayName("读:任务编号")]
        public int MissionId { get; set; }
        /// <summary>
        /// RFID代码
        /// </summary>
        [DisplayName("读:RFID代码")]
        public string PalletNo { get; set; }
        /// <summary>
        /// 前叉当前列
        /// </summary>
        [DisplayName("读:叉当前列")]
        public int ActPosBay { get; set; }
        /// <summary>
        /// 当前层
        /// </summary>
        [DisplayName("读:叉当前层")]
        public int ActPosLevel { get; set; }
        /// <summary>
        /// 当前水平位置
        /// </summary>
        [DisplayName("读:叉水平位置")]
        public int ActPosX { get; set; }
        /// <summary>
        /// 当前垂直位置
        /// </summary>
        [DisplayName("读:叉垂直位置")]
        public int ActPosY { get; set; }
        /// <summary>
        /// 叉当前位置，编码器数值
        /// </summary>
        [DisplayName("读:浅库位货叉位置")]
        public int ActPosZ { get; set; }
        /// <summary>
        /// 叉深库位货叉当前位置，编码器数值
        /// </summary>
        [DisplayName("读:深库位货叉位置")]
        public int ActPosZDeep { get; set; }
        /// <summary>
        /// 当前行走速度
        /// </summary>
        [DisplayName("读:行走速度")]
        public int ActSpeedX { get; set; }
        /// <summary>
        /// 当前升降速度
        /// </summary>
        [DisplayName("读:升降速度")]
        public int ActSpeedY { get; set; }
        /// <summary>
        /// 叉当前速度
        /// </summary>
        [DisplayName("读:叉当前速度")]
        public int ActSpeedZ { get; set; }
        /// <summary>
        /// 叉深库位货叉当前速度
        /// </summary>
        [DisplayName("读:深库位叉速度")]
        public int ActSpeedZDeep { get; set; }
        /// <summary>
        /// 负载状态
        /// </summary>
        [DisplayName("读:负载状态")]
        public int LoadStatus { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        [DisplayName("读:故障代码")]
        public int FaultNo { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        [DisplayName("读:备用字段")]
        public int NoFunction { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        [DisplayName("写:任务号")]
        public int TaskNo { get; set; }
        /// <summary>
        /// 起始位置编号
        /// </summary>
        [DisplayName("写:起始位置")]
        public string SLocNo { get; set; }
        /// <summary>
        /// 结束位置编号
        /// </summary>
        [DisplayName("写:结束位置")]
        public string ELocNo { get; set; }
    }
}
