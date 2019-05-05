/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2017/12/12 15:51:16
*生成者：wlitsoft
*/
using System;
using DapperExtensions.Mapper;
namespace IECSC.Trans.Modul
{
    ///<summary>
    ///表TPROC_0303_CMD_FINISH的实体类
    ///</summary>
    public class Tproc0303CmdFinish
    {
        /// <summary>
        /// 主键  编号
        /// </summary>
        public long Objid {get;set;}
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrCode {get;set;}
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrDesc {get;set;}
        /// <summary>
        /// 0 未执行  1执行中 2：执行完毕
        /// </summary>
        public int ProcStatus {get;set;}
        /// <summary>
        /// 启动时间
        /// </summary>
        public System.DateTime ProcCreateTime {get;set;}
        /// <summary>
        /// 执行时间
        /// </summary>
        public System.DateTime ProcExcuteTime {get;set;}
        /// <summary>
        /// 完成时间
        /// </summary>
        public System.DateTime ProcFinishTime {get;set;}
        /// <summary>
        /// 【必填项】    ：任务指令号                                                                                    
        /// </summary>
        public long TaskNo {get;set;}
        /// <summary>
        /// 【必填项】    ：当前站台
        /// </summary>
        public string CurrLocNo {get;set;}
        /// <summary>
        /// 【选填项】    ：结束状态    1 ：正常结束   201：空出库 
        /// </summary>
        public long FinishStatus {get;set;}
        /// <summary>
        /// 【必填项】    ：工装编码
        /// </summary>
        public string PalletNo {get;set;}
        /// <summary>
        /// 【系统占用】：起始库位类型
        /// </summary>
        public string SlocType {get;set;}
        /// <summary>
        /// 【系统占用】：起始库位
        /// </summary>
        public string SlocNo {get;set;}
        /// <summary>
        /// 【必填项】：结束库位
        /// </summary>
        public string ElocNo {get;set;}
        /// <summary>
        /// 【系统占用】：结束指令GUID
        /// </summary>
        public string GlobalGuid {get;set;}
        /// <summary>
        /// 【系统占用】：指令OBJID
        /// </summary>
        public long CmdObjid {get;set;}
        /// <summary>
        /// 【系统占用】：任务号GUID
        /// </summary>
        public string TaskGuid {get;set;}
        /// <summary>
        /// 【选填项】    ：任务结束标志 1：强制结束任务 0：不结束
        /// </summary>
        public long TaskFinishFlag {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string ElocType {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public long Ftype {get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string Ftype01 {get;set;}
        /// <summary>
        /// 输送设备类型
        /// </summary>
        public string TransferType {get;set;}
        /// <summary>
        /// 错误标记
        /// </summary>
        public long ErrSign {get;set;}
    }
    
    ///<summary>
    ///表TPROC_0303_CMD_FINISH的实体ID映射类
    ///</summary>
    public class  Tproc0303CmdFinishOraMap : ClassMapper<Tproc0303CmdFinish>
    {
        public Tproc0303CmdFinishOraMap()
        {
            Table("TPROC_0303_CMD_FINISH");
            Map(x => x.Objid).Key(KeyType.TriggerIdentity);
           
            Map(x => x.ErrCode).Column("ERR_CODE");
            Map(x => x.ErrDesc).Column("ERR_DESC");
            Map(x => x.ProcStatus).Column("PROC_STATUS");
            Map(x => x.ProcCreateTime).Column("PROC_CREATE_TIME");
            Map(x => x.ProcExcuteTime).Column("PROC_EXCUTE_TIME");
            Map(x => x.ProcFinishTime).Column("PROC_FINISH_TIME");
            Map(x => x.TaskNo).Column("TASK_NO");
            Map(x => x.CurrLocNo).Column("CURR_LOC_NO");
            Map(x => x.FinishStatus).Column("FINISH_STATUS");
            Map(x => x.PalletNo).Column("PALLET_NO");
            Map(x => x.SlocType).Column("SLOC_TYPE");
            Map(x => x.SlocNo).Column("SLOC_NO");
            Map(x => x.ElocNo).Column("ELOC_NO");
            Map(x => x.GlobalGuid).Column("GLOBAL_GUID");
            Map(x => x.CmdObjid).Column("CMD_OBJID");
            Map(x => x.TaskGuid).Column("TASK_GUID");
            Map(x => x.TaskFinishFlag).Column("TASK_FINISH_FLAG");
            Map(x => x.ElocType).Column("ELOC_TYPE");
            Map(x => x.Ftype).Column("FTYPE");
            Map(x => x.Ftype01).Column("FTYPE01");
            Map(x => x.TransferType).Column("TRANSFER_TYPE");
            Map(x => x.ErrSign).Column("ERR_SIGN");
            AutoMap();
        }
    }
}