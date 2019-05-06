using MSTL.LogAgent;
using System;
using System.Collections.Generic;
using System.Data;
using RFID.Entity;
using DapperExtensions;
using Dapper;
using MSTL.DbClient;


namespace RFID.Tools
{
    public class DBHelper
    {
        private IDatabase db;
        private string constr = McConfig.Instance.getConfig("OrclConnect");

        private static DBHelper _instance = null;
        public static DBHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(DbHelper))
                    {
                        if (_instance == null)
                        {
                            _instance = new DBHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        public DBHelper()
        {
            string errMsg = string.Empty;
            db = DbHelper.GetDb(constr, DbHelper.DataBaseType.Oracle, ref errMsg);

            log.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：数据库实例化异常：" + errMsg);
        }

        /// <summary>
        /// 获取数据库服务器时间（检测数据库连接状态）
        /// </summary>
        /// <returns></returns>
        public bool GetDbTime()
        {
            try
            {
                string sql = "select sysdate from dual ";

                var o = db.Connection.ExecuteScalar(sql);
                return o != null;
            }
            catch (Exception ex)
            {
                log.Error("建立数据库连接，获取数据库服务器时间异常"+ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// 更新数据库中rfid_no
        /// </summary>
        /// <param name="rfidNo"></param>
        /// <param name="ip"></param>
        /// <param name="portNo"></param>
        /// <returns></returns>
        public int SetRFIDInfoToDb(string rfidNo, string ip, string portNo)
        {
            try
            {
                string strSql = "update rfid_read t set t.read_date = sysdate, t.rfid_no =:rfid_no where t.ip_address =:ip_address and t.port_no =:port_no and t.is_enable = 1";
                DynamicParameters param = new DynamicParameters();
                param.Add("rfid_no", rfidNo);
                param.Add("ip_address", ip);
                param.Add("port_no", portNo);
                return db.Connection.Execute(strSql, param);
            }
            catch (Exception ex)
            {
                log.Error("执行SetRFIDInfoToDb时出错" + ex.ToString());
                return 0;
            }
        }

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
                var dt = db.Connection.QueryTable(strSql);
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


        /// <summary>
        /// 系统日志
        /// </summary>
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }

        public DataTable GetDgvInfoFromDataBase()
        {
            try
            {
                string strSql = "select t.ip_address,t.rfid_no,t.loc_no,t.read_date,t.port_no,t.is_enable from rfid_read t";
                return db.Connection.QueryTable(strSql);
            }
            catch (Exception ex)
            {
                log.Error("执行GetDgvInfoFromDataBase出错" + ex.ToString());
                return null;
            }

        }
    }
}
