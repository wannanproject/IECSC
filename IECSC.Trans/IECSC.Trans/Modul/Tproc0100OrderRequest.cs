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
    ///表TPROC_0100_ORDER_REQUEST的实体类
    ///</summary>
    public class Tproc0100OrderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Objid {get;set;}
        /// <summary>
        /// 【返回值】：异常编码
        /// </summary>
        public decimal ErrCode {get;set;}
        /// <summary>
        /// 【返回值】：异常描述  
        /// </summary>
        public string ErrDesc {get;set;}
        /// <summary>
        /// 【返回值】：执行结果  0:未执行 1：执行中  2：执行结束    
        /// </summary>
        public long ProcStatus {get;set;}
        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime ProcCreateTime {get;set;}
        /// <summary>
        /// 执行时间
        /// </summary>
        public System.DateTime ProcExcuteTime {get;set;}
        /// <summary>
        /// 结束时间
        /// </summary>
        public System.DateTime ProcFinishTime {get;set;}
        /// <summary>
        /// 【必填项】：订单类型                  
        /// </summary>
        public string OrderTypeNo {get;set;}
        /// <summary>
        /// 【必填项】：起始站台(库位)    
        /// </summary>
        public string SlocNo {get;set;}
        /// <summary>
        /// 【必填项】：目标站台(库位)             
        /// </summary>
        public string ElocNo {get;set;}
        /// <summary>
        /// 【必填项】：来源类型  1：手持   2: WMS  3：WCS  
        /// </summary>
        public long SourceType {get;set;}
        /// <summary>
        /// 【必填项】：工装编码     
        /// </summary>
        public string PalletNo {get;set;}
        /// <summary>
        /// 【可选项】：订单数量 
        /// </summary>
        public long RequireQty {get;set;}
        /// <summary>
        /// 【可选项】：锁定目标      1；锁定  0:不锁定  
        /// </summary>
        public byte LockEndLoc {get;set;}
        /// <summary>
        /// 【返回值】：订单号码         
        /// </summary>
        public string OrderNo {get;set;}
        /// <summary>
        /// 【返回值】：订单行GUID 
        /// </summary>
        public string OrderLineGuid {get;set;}
        /// <summary>
        /// 【返回值】：唯一GUID
        /// </summary>
        public string GlobalGuid {get;set;}
        /// <summary>
        /// 【返回值】：目标位置类型
        /// </summary>
        public string ElocType {get;set;}
        /// <summary>
        /// 【返回值】：目标位置区域
        /// </summary>
        public string ElocArea {get;set;}
        /// <summary>
        /// 【返回值】：路径
        /// </summary>
        public string RouteNo {get;set;}
        /// <summary>
        /// 【返回值】：发起站台类型
        /// </summary>
        public string SlocType {get;set;}
        /// <summary>
        /// 【返回值】：发起站台区域
        /// </summary>
        public string SlocArea {get;set;}
        /// <summary>
        /// 【必填项】：需求物料编码
        /// </summary>
        public string ProductNo {get;set;}
        /// <summary>
        ///  【返回值】：订单优先级
        /// </summary>
        public long OrderPriority {get;set;}
        /// <summary>
        /// 【返回值】：销售类型
        /// </summary>
        public string SalesType {get;set;}
        /// <summary>
        /// 【返回值】：工装类型
        /// </summary>
        public string PalletType {get;set;}
        /// <summary>
        /// 【返回值】：订单类型
        /// </summary>
        public string OrderTypeModule {get;set;}
        /// <summary>
        /// 【必填项】：是否需要贴标
        /// </summary>
        public long StickerFlag {get;set;}
    }
    
    ///<summary>
    ///表TPROC_0100_ORDER_REQUEST的实体ID映射类
    ///</summary>
    public class  Tproc0100OrderRequestOraMap : ClassMapper<Tproc0100OrderRequest>
    {
        public Tproc0100OrderRequestOraMap()
        {
            Table("TPROC_0100_ORDER_REQUEST");
            Map(x => x.Objid).Key(KeyType.TriggerIdentity);
           
            Map(x => x.ErrCode).Column("ERR_CODE");
            Map(x => x.ErrDesc).Column("ERR_DESC");
            Map(x => x.ProcStatus).Column("PROC_STATUS");
            Map(x => x.ProcCreateTime).Column("PROC_CREATE_TIME");
            Map(x => x.ProcExcuteTime).Column("PROC_EXCUTE_TIME");
            Map(x => x.ProcFinishTime).Column("PROC_FINISH_TIME");
            Map(x => x.OrderTypeNo).Column("ORDER_TYPE_NO");
            Map(x => x.SlocNo).Column("SLOC_NO");
            Map(x => x.ElocNo).Column("ELOC_NO");
            Map(x => x.SourceType).Column("SOURCE_TYPE");
            Map(x => x.PalletNo).Column("PALLET_NO");
            Map(x => x.RequireQty).Column("REQUIRE_QTY");
            Map(x => x.LockEndLoc).Column("LOCK_END_LOC");
            Map(x => x.OrderNo).Column("ORDER_NO");
            Map(x => x.OrderLineGuid).Column("ORDER_LINE_GUID");
            Map(x => x.GlobalGuid).Column("GLOBAL_GUID");
            Map(x => x.ElocType).Column("ELOC_TYPE");
            Map(x => x.ElocArea).Column("ELOC_AREA");
            Map(x => x.RouteNo).Column("ROUTE_NO");
            Map(x => x.SlocType).Column("SLOC_TYPE");
            Map(x => x.SlocArea).Column("SLOC_AREA");
            Map(x => x.ProductNo).Column("PRODUCT_NO");
            Map(x => x.OrderPriority).Column("ORDER_PRIORITY");
            Map(x => x.SalesType).Column("SALES_TYPE");
            Map(x => x.PalletType).Column("PALLET_TYPE");
            Map(x => x.OrderTypeModule).Column("ORDER_TYPE_MODULE");
            Map(x => x.StickerFlag).Column("STICKER_FLAG");
            AutoMap();
        }
    }
}