using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.ALARM
{
    public class AlarmLoc
    {
        /// <summary>
        /// 站台PLC编号
        /// </summary>
        public string LocPlcNo{get; set;}
        /// <summary>
        /// 站台分区
        /// </summary>
        public int LocArea { get; set; } = -1;
        /// <summary>
        /// 站台编号
        /// </summary>
        public int LocCode { get; set; } = -1;
        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get; set; } = -1;
        /// <summary>
        /// 报警描述
        /// </summary>
        public Dictionary<int, AlarmInfo> alarmInfo = new Dictionary<int, AlarmInfo>();
    }

    public class AlarmInfo
    {
        /// <summary>
        /// 故障描述
        /// </summary>
        public string AlarmDesc { get; set; }
        /// <summary>
        /// 处理ID
        /// </summary>
        public int Objid { get; set; }
    }
}
