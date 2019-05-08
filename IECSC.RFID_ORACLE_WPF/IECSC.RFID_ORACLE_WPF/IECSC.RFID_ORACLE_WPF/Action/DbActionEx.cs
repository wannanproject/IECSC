using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace IECSC.RFID_ORACLE_WPF
{
    public partial class DbAction
    {
       
        /// <summary>
        /// 在数据库中获得IP和端口号
        /// </summary>
        /// <returns></returns>
        public List<RfidRead> GetIPAndPortNoFromDB()
        {
            try
            {
                var list = new List<RfidRead>();
                string strSql = "select * from rfid_read t where t.is_enable = 1";
                var dt = Db.Connection.QueryTable(strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new RfidRead
                        {
                            IPAddress = (dr["IP_ADDRESS"] ?? "").ToString(),
                            PortNo = (dr["PORT_NO"] ?? "").ToString(),
                            LocNo = (dr["LOC_NO"] ?? "").ToString()
                        });
                    }
                    return list;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                log.Error("执行GetIPAndPortNoFromDB时出错" + ex.ToString());
                return null;
            }
        }

      
       

        internal void UpdatePemLocStatus(string locNo, string msg)
        {
            try
            {
                string strSql = $"update pem_loc_status set  scan_rfid_no = '{msg}',  update_date = sysdate  where loc_plc_no = '{locNo}'";
                Db.Connection.Execute(strSql);
            }
            catch
            {
            }
        }
    }
}
