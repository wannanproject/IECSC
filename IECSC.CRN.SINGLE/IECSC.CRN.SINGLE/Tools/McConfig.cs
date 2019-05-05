using System;
using System.Configuration;
using MSTL.LogAgent;

namespace IECSC.CRN.SINGLE
{
    public class McConfig
    {
        private ILog log
        {
            get
            {
                return Log.Store[this.GetType().FullName];
            }
        }

        private static McConfig _instance = null;
        public static McConfig Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                lock (typeof(McConfig))
                {
                    if (_instance == null)
                    {
                        _instance = new McConfig();
                    }
                    return _instance;
                }
            }
        }
        private McConfig()
        {
            try
            {
                OpcSeverName = getConfig("OpcSeverName");
                OpcGroupName = getConfig("OpcGroupName");
                OpcServerIp = getConfig("OpcServerIP");
                CrnName = getConfig("CrnName");
                OrclConnect = getConfig("OrlConnect");

            }
            catch (Exception ex)
            {
                log.Error($"获取配置文件时出错{ex.ToString()}");
            }
        }

        /// <summary>
        /// OPC名称
        /// </summary>
        public string OpcSeverName { get; set; }
        /// <summary>
        /// OPC组名称
        /// </summary>
        public string OpcGroupName { get; set; }
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        public string CrnName { get; set; }
        /// <summary>
        /// OPC服务器地址
        /// </summary>
        public string OpcServerIp { get; set; }
        /// <summary>
        /// Oracle连接
        /// </summary>
        public string OrclConnect { get; set; }

        /// <summary>
        /// 读配置文档
        /// </summary>
        public string getConfig(string key)
        {
            var values = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(values))
            {
                return values;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 写配置文档
        /// </summary>
        public void setConfig(string key, int value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value.ToString();
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value.ToString());
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                log.Error($"写配置文件时出错{ex.ToString()}");
            }
        }
    }
}
