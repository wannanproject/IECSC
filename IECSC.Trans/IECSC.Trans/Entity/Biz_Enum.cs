namespace IECSC.Trans.Entity
{
    /// <summary>
    /// 显示信息枚举类
    /// </summary>
    public enum ShowInfoTypeEnum
    {
        /// <summary>
        /// 状态显示
        /// </summary>
        logInfo = 0,

        /// <summary>
        /// 站台信息显示
        /// </summary>
        LocInfo = 2,

        /// <summary>
        /// plc连接状态
        /// </summary>
        plcConn = 4,

        /// <summary>
        /// 指令信息
        /// </summary>
        cmdInfo = 6,

        /// <summary>
        /// 数据库连接
        /// </summary>
        DbConn = 8,

        /// <summary>
        /// 业务循环时间
        /// </summary>
        CirTime = 10
    }

    /// <summary>
    /// 站台类型
    /// </summary>
    public enum LocationTypeEnum
    {
        /// <summary>
        /// 叠盘机
        /// </summary>
        FurLoc = 4000,
        /// <summary>
        /// 异常口
        /// </summary>
        ExpLoc = 5000,
        /// <summary>
        /// 发货出口站台
        /// </summary>
        SendingLoc = 6022,
        /// <summary>
        /// 备货出口站台
        /// </summary>
        StockLoc = 6032,
        /// <summary>
        /// PDIN站台
        /// </summary>
        PdInLoc = 7001,
        /// <summary>
        /// PDOUT站台
        /// </summary>
        PdOutLoc = 7002,
        /// <summary>
        /// 分拣装笼站台
        /// </summary>
        GangtryLoc = 6001
    }

    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BllTypeEnum
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        Err = 000000,
        #region PDout
        /// <summary>
        /// PDOut连接输送线
        /// </summary>
        PDOutConnTrans = 1007002,

        /// <summary>
        /// PDOut连接输送线，生成输送线指令
        /// </summary>
        PDOutConnTransCreateTask = 1017002,

        /// <summary>
        /// PD连接输送线
        /// </summary>
        PDOutConnAgv = 1027002,
        #endregion

        #region PDin
        /// <summary>
        /// PNIN输送线连接输送线
        /// </summary>
        PDInConnTrans = 1007001,

        /// <summary>
        /// 输送线连接输送线（生成堆垛机任务）
        /// </summary>
        PDInConnTransCreateTask = 1017001,

        /// <summary>
        /// PDIN输送线连接输送线
        /// </summary>
        PDInConnAgv = 1027001,
        #endregion

        #region 输送线
        /// <summary>
        /// 分拣线入口
        /// </summary>
        GanteyLoc = 1006011,

        /// <summary>
        /// 输送线入口
        /// </summary>
        TransIn = 1006001,

        /// <summary>
        /// 输送线出口
        /// </summary>
        TransOut = 1006002,
        
        /// <summary>
        /// 叠盘机
        /// </summary>
        DiscPallet = 1004000,
        #endregion


        /// <summary>
        ///  异常口
        /// </summary>
        ExpLoc = 100500,
       
    }
}
