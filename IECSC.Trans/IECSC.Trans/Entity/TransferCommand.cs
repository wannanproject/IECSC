
namespace IECSC.Trans.Entity
{
    public class TransferCommand
    {
        /// <summary>
        /// 站台编号
        /// </summary>
        public string LocNo { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public long TaskNo { get; set; }
        /// <summary>
        /// 托盘编号
        /// </summary>
        public string PalletNo { get; set; }
        /// <summary>
        /// 目的地址区域
        /// </summary>
        public string TargetAddress { get; set; }

        /// <summary>
        /// 目的地址区域符号
        /// </summary>
        public string TargetAddressAreaNo
        {
            get
            {
                if (this.TargetAddress.Length > 0)
                {
                    return this.TargetAddress.Substring(0, 1);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 目的地址设备ID
        /// </summary>
        public string TargetAddressDeviceId
        {
            get
            {
                if (this.TargetAddress.Length > 0)
                {
                    return this.TargetAddress.Substring(1, 4);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 源地址区域
        /// </summary>
        public string SourceAddress { get; set; }
        /// <summary>
        /// 源地址区域符号
        /// </summary>
        public string SourceAddressAreaNo {
            get
            {
                if (this.SourceAddress.Length > 0)
                {
                    return this.SourceAddress.Substring(0, 1);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 源地址设备Id
        /// </summary>
        public string SourceAddressDeviceId
        {
            get
            {
                if (this.SourceAddress.Length > 0)
                {
                    return this.SourceAddress.Substring(1, 4);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
