using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.ALARM
{
    public class AlarmItem
    {
        /// <summary>
         /// 分区主键
         /// </summary>
        public string AlarmAreaKey { get; set; }
        /// <summary>
        /// 站台编号主键
        /// </summary>
        public string AlarmCodeKey { get; set; }
        /// <summary>
        /// 报警类型主键
        /// </summary>
        public string AlarmTypeKey { get; set; }
        /// <summary>
        /// 报警序号
        /// </summary>
        public int AlarmIndex { get; set; }
        /// <summary>
        /// 配置类型 0：非报警项 1：报警项
        /// </summary>
        public int Kind { get; set; }
        /// <summary>
        /// 项长名
        /// </summary>
        public string TagLongName { get; set; }
        /// <summary>
        /// PLC读取值
        /// </summary>
        public int TagValue { get; set; } = 0;
        /// <summary>
        /// 报警处理标记
        /// </summary>
        public int AlarmMark { get; set; } = 0;
        /// <summary>
        /// 报警站台
        /// </summary>
        public string AlarmLocNo { get; set;}
    }
}
