using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTL.LogAgent;

namespace IECSC.Trans.Common
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class McConfig
    {
        //系统日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }

        #region 单例模式
        private static McConfig _instance = null;
        public static McConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(McConfig))
                    {
                        if (_instance == null)
                        {
                            _instance = new McConfig();
                        }
                    }
                }
                return _instance;
            }
        }
        public McConfig()
        {
            try
            {
                // 获取配置项
                AppName = GetConfig("AppName");
                AppCode = GetConfig("AppCode");
                OpcServerIp = GetConfig("OPCServerIP");
                OpcServerName = GetConfig("OpcServerName");
                connectionString = GetConfig("ConnectionString");
            }
            catch (Exception ex)
            {
                Log.Error("获取配置文件时出错" + ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region 配置项
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 应用程序编号
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// OPC服务器名称
        /// </summary>
        public string OpcServerName { get; set; }

        /// <summary>
        /// OPC服务器名称
        /// </summary>
        public string OpcServerIp { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string connectionString { get; set; }
        #endregion

        #region 函数
        /// <summary>
        /// 设置配置变量值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetConfig(string key, object value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                    config.AppSettings.Settings[key].Value = value.ToString();
                else
                    config.AppSettings.Settings.Add(key, value.ToString());

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                Log.Error("写配置文件时出错" + ex.ToString());
            }
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfig(string key)
        {
            var values = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(values))
            {
                return values;
            }
            return string.Empty;
        }
        #endregion
    }
}
