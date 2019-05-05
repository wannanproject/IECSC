/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2017/12/12 15:04:31
*生成者：RAN
*/
using System;
using DapperExtensions.Mapper;
namespace IECSC.Trans.Modul
{
    ///<summary>
    ///表PEM_LOC_STATUS的实体类
    ///</summary>
    public class PemLocStatus
    {
        /// <summary>
        /// 站台号码
        /// </summary>
        public string Loc_No { get; set; }
        /// <summary>
        /// 任务号码
        /// </summary>
        public int TaskNo { get; set; }
        /// <summary>
        /// 任务日期
        /// </summary>
        public System.DateTime TaskDate { get; set; }
        /// <summary>
        /// 工装编码
        /// </summary>
        public string PalletNo { get; set; }
        /// <summary>
        /// 有载标志 1:有载  0:空载
        /// </summary>
        public int LoadedStatus { get; set; }
        /// <summary>
        /// 站台优先级  默认20  越小优先级越高
        /// </summary>
        public int LocPriority { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单行唯一编码
        /// </summary>
        public string OrderLineGuid { get; set; }
        /// <summary>
        /// 空闲标志 1：空闲  0:不空闲 --作业使用
        /// </summary>
        public int FreeFlag { get; set; }
        /// <summary>
        /// 当前任务数量
        /// </summary>
        public int TaskQty { get; set; }
        /// <summary>
        /// 不满足量
        /// </summary>
        public int EmptyQty { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime UpdateDate { get; set; }
        /// <summary>
        /// 扫描到的条码号
        /// </summary>
        public string ScanRfidNo { get; set; }
        /// <summary>
        /// AGV到站请求标志 1：发起请求  0：无请求
        /// </summary>
        public int RequestFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrDesc { get; set; }
        /// <summary>
        /// 当前站台是否有货，PLC信号为准
        /// </summary>
        public int PlcIsLoaded { get; set; }
        /// <summary>
        /// 当前站台空闲数量
        /// </summary>
        public int PlcFreeLocCount { get; set; }
    }

    ///<summary>
    ///表PEM_LOC_STATUS的实体ID映射类
    ///</summary>
    public class PemLocStatusOraMap : ClassMapper<PemLocStatus>
    {
        public PemLocStatusOraMap()
        {
            Table("PEM_LOC_STATUS");
            Map(x => x.Loc_No).Key(KeyType.TriggerIdentity);

            Map(x => x.TaskNo).Column("TASK_NO");
            Map(x => x.TaskDate).Column("TASK_DATE");
            Map(x => x.PalletNo).Column("PALLET_NO");
            Map(x => x.LoadedStatus).Column("LOADED_STATUS");
            Map(x => x.LocPriority).Column("LOC_PRIORITY");
            Map(x => x.OrderNo).Column("ORDER_NO");
            Map(x => x.OrderLineGuid).Column("ORDER_LINE_GUID");
            Map(x => x.FreeFlag).Column("FREE_FLAG");
            Map(x => x.TaskQty).Column("TASK_QTY");
            Map(x => x.EmptyQty).Column("EMPTY_QTY");
            Map(x => x.UpdateDate).Column("UPDATE_DATE");
            Map(x => x.ScanRfidNo).Column("SCAN_RFID_NO");
            Map(x => x.RequestFlag).Column("REQUEST_FLAG");
            Map(x => x.ErrDesc).Column("ERR_DESC");
            Map(x => x.PlcIsLoaded).Column("PLC_IS_LOADED");
            Map(x => x.PlcFreeLocCount).Column("PLC_FREE_LOC_COUNT");
            AutoMap();
        }
    }
}