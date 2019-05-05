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
    ///表WBS_PALLET_LIST的实体类
    ///</summary>
    public class WbsPalletList
    {
        /// <summary>
        /// 叠盘机编码
        /// </summary>
        public string LocNo {get;set;}
        /// <summary>
        /// RFID号码
        /// </summary>
        public string RfidNo {get;set;}
        /// <summary>
        /// 序号
        /// </summary>
        public int OrderId {get;set;}
    }
    
    ///<summary>
    ///表WBS_PALLET_LIST的实体ID映射类
    ///</summary>
    public class  WbsPalletListOraMap : ClassMapper<WbsPalletList>
    {
        public WbsPalletListOraMap()
        {
            Table("WBS_PALLET_LIST");
            Map(x => x.RfidNo).Key(KeyType.TriggerIdentity);
           
            Map(x => x.LocNo).Column("LOC_NO");
            Map(x => x.RfidNo).Column("RFID_NO");
            Map(x => x.OrderId).Column("ORDER_ID");
            AutoMap();
        }
    }
}