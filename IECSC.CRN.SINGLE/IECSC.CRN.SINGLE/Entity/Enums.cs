
namespace IECSC.CRN.SINGLE
{
    public class Enums
    {
        public enum EquipStatus
        {
            /// <summary>
            /// 初始状态
            /// </summary>
            None = 0,
            /// <summary>
            /// 已从数据库获取,等待下传
            /// </summary>
            Down = 1,
            /// <summary>
            /// 等待PLC任务状态变更为1
            /// </summary>
            Ready = 2,
            /// <summary>
            /// 已下传PLC,正在执行
            /// </summary>
            Exec = 3,
            /// <summary>
            /// 已接受任务完成信号,并已回传任务信号2
            /// </summary>
            End = 4,
            /// <summary>
            /// 已接受复位信号0,并已回传复位信号0
            /// </summary>
            Reset = 5,
            /// <summary>
            /// 异常
            /// </summary>
            Error = 6,
            /// <summary>
            /// 异常处理
            /// </summary>
            ErrorDeal = 7
        }
    }
}
