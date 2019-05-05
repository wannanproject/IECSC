
namespace IECSC.Trans.Entity
{
    public class TransferTask
    {
        /// <summary>
        /// TASK_GUID
        /// </summary>
        public string TASK_GUID { get; set; }

        /// <summary>
        /// 指令OBJID
        /// </summary>
        public string OBJID { get; set; }

        /// <summary>
        /// 起始站台编号
        /// </summary>
        public string SLOC_NO { get; set; }

        /// <summary>
        /// 起始站台PLC编号
        /// </summary>
        public string SLOC_PLC_NO { get; set; }

        /// <summary>
        /// 终点站台编号
        /// </summary>
        public string ELOC_NO { get; set; }

        /// <summary>
        /// 终点站台PLC编号
        /// </summary>
        public string ELOC_PLC_NO { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string TASK_NO { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int EMERGENCY { get; set; }

        /// <summary>
        /// 托盘编号
        /// </summary>
        public string PALLET_NO { get; set; }

        /// <summary>
        /// 任务步骤
        /// </summary>
        public string TASK_STEP { get; set; }

        /// <summary>
        /// 指令步骤
        /// </summary>
        public string CMD_STEP { get; set; }

        /// <summary>
        /// 展示指令步骤
        /// </summary>
        public string CmdStepForShow
        {
            get
            {
                switch (this.CMD_STEP)
                {
                    case "00":
                        return "生成指令";
                    case "01":
                        return "等待下传";
                    case "02":
                        return "下传成功";
                    case "03":
                        return "正在执行";
                    case "04":
                        return "指令结束";
                    case "":
                        return "";
                    default:
                        return "错误";
                }
            }
        }

        /// <summary>
        /// 指令类型I/O
        /// </summary>
        public string CMD_TYPE { get; set; }

        /// <summary>
        /// 展示指令类型
        /// </summary>
        public string CmdTypeForShow
        {
            get
            {
                switch (this.CMD_TYPE)
                {
                    case "I":
                        return "入库";
                    case "O":
                        return "出库";
                    case "":
                        return "";
                    default:
                        return "错误";
                }
            }
        }

        /// <summary>
        /// 任务时间
        /// </summary>
        public string CREATION_DATE { get; set; }

        /// <summary>
        /// 任务时间
        /// </summary>
        public string MATERIAL_NO { get; set; }
    }
}
