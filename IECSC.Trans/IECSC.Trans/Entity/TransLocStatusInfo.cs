using System.Collections.Generic;

namespace IECSC.Trans.Entity
{
    /// <summary>
    /// 站台状态信息
    /// </summary>
    public class TransLocStatusInfo
    {
        public TransLocStatusInfo()
        {
            //初始化入库托盘列表
            this.DicPallet = new Dictionary<int, string>();
            for (int i = 1; i <= 8; i++)
            {
                this.DicPallet.Add(i, string.Empty);
            }
        }

        /// <summary>
        /// 任务OBJID
        /// </summary>
        public long TaskObjid { get; set; }
        /// <summary>
        /// 任务OBJID
        /// </summary>
        public long CmdObjid { get; set; }
        /// <summary>
        /// 任务结束OBJID
        /// </summary>
        public long FinishObjid { get; set; }
        /// <summary>
        /// 站台编号
        /// </summary>
        public string LocNo { get; set; }
        /// <summary>
        /// 站台PLC编号
        /// </summary>
        public string LocPlcNo { get; set; }
        /// <summary>
        /// 业务描述
        /// </summary>
        public string BusinessDesc { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 业务处理时间
        /// </summary>
        public string BusinessHandleTime { get; set; }
        /// <summary>
        /// 站台类型编号
        /// </summary>
        public int LocTypeNo { get; set; }
        /// <summary>
        /// 站台类型描述
        /// </summary>
        public string LocTypeDesc { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        public long TaskNo { get; set; }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string PalletNo { get; set; }
        
        /// <summary>
        /// 托盘号
        /// </summary>
        public string LastPalletNo { get; set; }
        /// <summary>
        /// 源地址
        /// </summary>
        public string SourceAddress => this.SourceAddressAreaNo + this.SourceAddressDeviceId;
        /// <summary>
        /// 源地址区域符号
        /// </summary>
        public string SourceAddressAreaNo { get; set; }
        /// <summary>
        /// 源地址设备Id
        /// </summary>
        public string SourceAddressDeviceId { get; set; }
        /// <summary>
        /// 源地址
        /// </summary>
        public string TargetAddress => this.TargetAddressAreaNo + this.TargetAddressDeviceId;
        /// <summary>
        /// 目的地址区域符号
        /// </summary>
        public string TargetAddressAreaNo { get; set; }
        /// <summary>
        /// 目的地址设备ID
        /// </summary>
        public string TargetAddressDeviceId { get; set; }
        /// <summary>
        /// 自动 标识
        /// </summary>
        public int StatusAutomatic { get; set; }
        /// <summary>
        /// 故障 标识
        /// </summary>
        public int StatusFault { get; set; }
        /// <summary>
        /// 空闲可放货 标识
        /// </summary>
        public int StatusFreeAndPut { get; set; }
        /// <summary>
        /// 有载 标识
        /// </summary>
        public int StatusLoad { get; set; }
        /// <summary>
        /// 请求任务 标识
        /// </summary>
        public int StatusRequestTask { get; set; }
        /// <summary>
        /// 等待装卸货完成 标识
        /// </summary>
        public int StatusWaitPutFinish { get; set; }
        /// <summary>
        /// 有货需取货 标识
        /// </summary>
        public int StatusBusyAndTake { get; set; }
        /// <summary>
        /// 允许装卸货 标识
        /// </summary>
        public int StatusAllowPutOrTake { get; set; }
        /// <summary>
        /// 叠盘机入库托盘列表
        /// </summary>
        public Dictionary<int, string> DicPallet;
        /// <summary>
        /// 称重重量
        /// </summary>
        public double ProductWeight { get; set; }
    }
}
