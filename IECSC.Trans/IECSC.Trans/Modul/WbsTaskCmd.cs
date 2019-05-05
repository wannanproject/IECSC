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
    ///表WBS_TASK_CMD的实体类
    ///</summary>
    public class WbsTaskCmd
    {
        /// <summary>
        /// 指令ID 序列
        /// </summary>
        public decimal Objid {get;set;}
        /// <summary>
        /// 任务GUID
        /// </summary>
        public string TaskGuid { get;set;}
        /// <summary>
        /// 任务编号
        /// </summary>
        public long TaskNo {get;set;}
        /// <summary>
        /// 指令类型
        /// </summary>
        public string CmdType {get;set;}
        /// <summary>
        /// 源
        /// </summary>
        public string SlocNo {get;set;}
        /// <summary>
        /// 目标
        /// </summary>
        public string ElocNo {get;set;}
        /// <summary>
        /// 源
        /// </summary>
        public string SlocPlcNo {get;set;}
        /// <summary>
        /// 目标
        /// </summary>
        public string ElocPlcNo {get;set;}
        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreationDate {get;set;}
        /// <summary>
        /// 执行时间
        /// </summary>
        public System.DateTime ExcuteDate {get;set;}
        /// <summary>
        /// 执行时间
        /// </summary>
        public System.DateTime FinishDate {get;set;}
        /// <summary>
        /// 错误编码
        /// </summary>
        public decimal ErrNo {get;set;}
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrName {get;set;}
        /// <summary>
        /// 物料编号
        /// </summary>
        public string ProductNo {get;set;}
        /// <summary>
        /// 输送设备类型
        /// </summary>
        public string TransferType {get;set;}
        /// <summary>
        /// 是否锁定物料输送顺序  0:不锁定  1:锁定
        /// </summary>
        public byte LockProduct {get;set;}
        /// <summary>
        /// 路径编号
        /// </summary>
        public string RouteNo {get;set;}
        /// <summary>
        /// 输送设备号码
        /// </summary>
        public string TransferNo {get;set;}
        /// <summary>
        /// 结束状态  201 空出库
        /// </summary>
        public string FinishStatus {get;set;}
        /// <summary>
        /// 工装编号
        /// </summary>
        public string PalletNo {get;set;}
        /// <summary>
        /// 订单行项目GUID
        /// </summary>
        public string OrderLineGuid {get;set;}
        /// <summary>
        /// 开始站台类型
        /// </summary>
        public string SlocType {get;set;}
        /// <summary>
        /// 结束站台类型
        /// </summary>
        public string ElocType {get;set;}
        /// <summary>
        /// 指令步骤
        /// </summary>
        public string CmdStep {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public byte FipFlag {get;set;}
        /// <summary>
        /// 下传时间
        /// </summary>
        public System.DateTime DownloadDate {get;set;}
        /// <summary>
        /// B道标志
        /// </summary>
        public byte BTransFlag {get;set;}
    }
    
    ///<summary>
    ///表WBS_TASK_CMD的实体ID映射类
    ///</summary>
    public class  WbsTaskCmdOraMap : ClassMapper<WbsTaskCmd>
    {
        public WbsTaskCmdOraMap()
        {
            Table("WBS_TASK_CMD");
            Map(x => x.Objid).Key(KeyType.TriggerIdentity);
           
            Map(x => x.TaskGuid).Column("TASK_GUID");
            Map(x => x.TaskNo).Column("TASK_NO");
            Map(x => x.CmdType).Column("CMD_TYPE");
            Map(x => x.SlocNo).Column("SLOC_NO");
            Map(x => x.ElocNo).Column("ELOC_NO");
            Map(x => x.SlocPlcNo).Column("SLOC_PLC_NO");
            Map(x => x.ElocPlcNo).Column("ELOC_PLC_NO");
            Map(x => x.CreationDate).Column("CREATION_DATE");
            Map(x => x.ExcuteDate).Column("EXCUTE_DATE");
            Map(x => x.FinishDate).Column("FINISH_DATE");
            Map(x => x.ErrNo).Column("ERR_NO");
            Map(x => x.ErrName).Column("ERR_NAME");
            Map(x => x.ProductNo).Column("PRODUCT_NO");
            Map(x => x.TransferType).Column("TRANSFER_TYPE");
            Map(x => x.LockProduct).Column("LOCK_PRODUCT");
            Map(x => x.RouteNo).Column("ROUTE_NO");
            Map(x => x.TransferNo).Column("TRANSFER_NO");
            Map(x => x.FinishStatus).Column("FINISH_STATUS");
            Map(x => x.PalletNo).Column("PALLET_NO");
            Map(x => x.OrderLineGuid).Column("ORDER_LINE_GUID");
            Map(x => x.SlocType).Column("SLOC_TYPE");
            Map(x => x.ElocType).Column("ELOC_TYPE");
            Map(x => x.CmdStep).Column("CMD_STEP");
            Map(x => x.FipFlag).Column("FIP_FLAG");
            Map(x => x.DownloadDate).Column("DOWNLOAD_DATE");
            Map(x => x.BTransFlag).Column("B_TRANS_FLAG");
            AutoMap();
        }
    }
}