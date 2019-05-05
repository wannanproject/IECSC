/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2017/12/12 15:51:17
*生成者：wlitsoft
*/
using System;
using DapperExtensions.Mapper;
namespace IECSC.Trans.Modul
{
    ///<summary>
    ///表WBS_TASK的实体类
    ///</summary>
    public class WbsTask
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string TaskGuid {get;set;}
        /// <summary>
        /// 任务编号
        /// </summary>
        public long TaskNo {get;set;}
        /// <summary>
        /// 优先级(1: 最优先,  100: 最后)  默认20
        /// </summary>
        public int Emergency {get;set;}
        /// <summary>
        /// 入/出库(I:入库, O:出库 ,T 移栽 S:巷道转移)
        /// </summary>
        public string IoType {get;set;}
        /// <summary>
        /// 输送设备号
        /// </summary>
        public string TransferNo {get;set;}
        /// <summary>
        /// 当前正在执行的指令 起始站台
        /// </summary>
        public string SlocNo {get;set;}
        /// <summary>
        /// 当前正在执行的指令 结束站台
        /// </summary>
        public string ElocNo {get;set;}
        /// <summary>
        /// 货笼ID
        /// </summary>
        public string PalletNo {get;set;}
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNo {get;set;}
        /// <summary>
        /// 订单行项目GUID
        /// </summary>
        public string OrderLineGuid {get;set;}
        /// <summary>
        /// 执行顺序
        /// </summary>
        public int SortId {get;set;}
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public System.DateTime CreationDate {get;set;}
        /// <summary>
        /// 任务开始执行时间
        /// </summary>
        public System.DateTime TaskExecStartDt {get;set;}
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public System.DateTime TaskExecEndDt {get;set;}
        /// <summary>
        /// 最后修改日期
        /// </summary>
        public System.DateTime LastUpdateDate {get;set;}
        /// <summary>
        /// (0:表示无错误 >0: 对应 ERR_CODE，堆垛机、地面线异常码需要唯一)
        /// </summary>
        public int FinishFlag {get;set;}
        /// <summary>
        /// 错误代码  1： 空出库    2:路径查找失败
        /// </summary>
        public string ErrNo {get;set;}
        /// <summary>
        /// 立库库位信息，如果非立库，可以为空
        /// </summary>
        public string BinNo {get;set;}
        /// <summary>
        /// 本次任务数量
        /// </summary>
        public int UseQty {get;set;}
        /// <summary>
        /// 工装类型
        /// </summary>
        public string PalletType {get;set;}
        /// <summary>
        /// '0000'
        /// </summary>
        public string TaskStep {get;set;}
        /// <summary>
        /// 结束描述
        /// </summary>
        public string FinishDesc {get;set;}
        /// <summary>
        /// 描述
        /// </summary>
        public string Fdesc {get;set;}
        /// <summary>
        /// 父区域编码
        /// </summary>
        public string PareaNo {get;set;}
        /// <summary>
        /// 当前所处区域类型 0：未进  1：进入缓存  2：A道   3: B道
        /// </summary>
        public int TaskPos {get;set;}
        /// <summary>
        /// 是否允许准入 1:准入 
        /// </summary>
        public int AdmitFlag {get;set;}
        /// <summary>
        /// 0：默认A道 ; 1：默认B道
        /// </summary>
        public int BTransFlag {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string OrderTypeNo {get;set;}
    }
    
    ///<summary>
    ///表WBS_TASK的实体ID映射类
    ///</summary>
    public class  WbsTaskOraMap : ClassMapper<WbsTask>
    {
        public WbsTaskOraMap()
        {
            Table("WBS_TASK");
            Map(x => x.TaskGuid).Key(KeyType.TriggerIdentity);
           
            Map(x => x.TaskNo).Column("TASK_NO");
            Map(x => x.Emergency).Column("EMERGENCY");
            Map(x => x.IoType).Column("IO_TYPE");
            Map(x => x.TransferNo).Column("TRANSFER_NO");
            Map(x => x.SlocNo).Column("SLOC_NO");
            Map(x => x.ElocNo).Column("ELOC_NO");
            Map(x => x.PalletNo).Column("PALLET_NO");
            Map(x => x.MaterialNo).Column("MATERIAL_NO");
            Map(x => x.OrderLineGuid).Column("ORDER_LINE_GUID");
            Map(x => x.SortId).Column("SORT_ID");
            Map(x => x.CreationDate).Column("CREATION_DATE");
            Map(x => x.TaskExecStartDt).Column("TASK_EXEC_START_DT");
            Map(x => x.TaskExecEndDt).Column("TASK_EXEC_END_DT");
            Map(x => x.LastUpdateDate).Column("LAST_UPDATE_DATE");
            Map(x => x.FinishFlag).Column("FINISH_FLAG");
            Map(x => x.ErrNo).Column("ERR_NO");
            Map(x => x.BinNo).Column("BIN_NO");
            Map(x => x.UseQty).Column("USE_QTY");
            Map(x => x.PalletType).Column("PALLET_TYPE");
            Map(x => x.TaskStep).Column("TASK_STEP");
            Map(x => x.FinishDesc).Column("FINISH_DESC");
            Map(x => x.Fdesc).Column("FDESC");
            Map(x => x.PareaNo).Column("PAREA_NO");
            Map(x => x.TaskPos).Column("TASK_POS");
            Map(x => x.AdmitFlag).Column("ADMIT_FLAG");
            Map(x => x.BTransFlag).Column("B_TRANS_FLAG");
            Map(x => x.OrderTypeNo).Column("ORDER_TYPE_NO");
            AutoMap();
        }
    }
}