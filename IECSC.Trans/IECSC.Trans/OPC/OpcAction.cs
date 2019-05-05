using IECSC.Trans.Common;
using MSTL.LogAgent;
using MSTL.OpcClient;
using System;
using System.Collections.Generic;

namespace IECSC.Trans.OPC
{
    public class OpcAction
    {

        //系统日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }

        public List<IOpcNetObserver> observersList = new List<IOpcNetObserver>();

        public Opc.Da.Server opcServer;

        private OpcClient opcClient;

        #region 单例模式

        private static OpcAction _instance = null;
        public static OpcAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(OpcAction))
                    {
                        if (_instance == null)
                        {
                            _instance = new OpcAction();
                        }
                    }
                }
                return _instance;
            }
        }

        public OpcAction()
        {
            opcClient = new OpcClient();
        }

        #endregion

        #region OpcNetAction初始化操作

        /// <summary>
        /// OPC初始化
        /// </summary>
        /// <param name="itemsArr"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool OpcNetIntial(string SERVERNAME, string[] itemsArr, string opcGroupName, ref string errMsg,string GANTRYSERVERIP="127.0.0.1")
        {
            try
            {
                if (opcClient.ConnectOpcServer(GANTRYSERVERIP, SERVERNAME, ref errMsg))
                {
                    Log.Debug("opcServer：" + opcServer);
                    if (opcClient.AddOpcGroup(opcGroupName, 200, ref errMsg))
                    {
                        Log.Debug("添加OPC组成功");
                        opcClient.DataChanged += OpcClient_DataChanged;
                        opcClient.Disconnected += OpcClient_Disconnected;
                        //添加ITEM
                        if (opcClient.AddOpcItems(opcGroupName, itemsArr, ref errMsg))
                        {
                            Log.Debug("添加OPC项成功");
                            return true;
                        }
                        else
                        {
                            Log.Error("添加OPC项失败:" + errMsg);
                        }
                        return false;
                    }
                    Log.Error("添加OPC组失败:" + errMsg);
                    return false;
                }
                Log.Error("连接OPC服务器失败:" + errMsg);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("OpcNetIntial", ex);
                return false;
            }
        }
        private void OpcClient_Disconnected(object obj)
        {
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"OPC连接断开，请重启！！！"));
        }

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity"></param>
        public void AttachEntity(IOpcNetObserver entity)
        {
            observersList.Add(entity);
        }

        /// <summary>
        /// 消息通知
        /// </summary>
        /// <param name="item"></param>
        public void NotifyEntity(string TagLongName, Object TagValue, bool Quality)
        {
            if (observersList != null && observersList.Count > 0)
            {
                foreach (var m in observersList)
                {
                    m.UpdateEntity(TagLongName, TagValue, Quality);
                }
            }
        }

        /// <summary>
        /// 数据改变事件处理
        /// </summary>
        /// <param name="subscriptionHandle"></param>
        /// <param name="requestHandle"></param>
        /// <param name="opcItemValueResults"></param>
        private void OpcClient_DataChanged(MSTL.OpcClient.Model.DataChangedEventArgs e)
        {
            try
            {
                foreach (var item in e.Data)
                {
                    if (!string.IsNullOrWhiteSpace(item.TagLongName))
                    {
                        NotifyEntity(item.TagLongName, item.TagValue, item.Quality.Equals(Opc.Da.Quality.Good));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("OPC监控DataChanged：",ex);
            }
        }

        #endregion

        /// <summary>
        /// 同步写操作
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="itemValuesDic"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool WriteItems(string groupName, KeyValuePair<string, object>[] itemValues, ref string errMsg)
        {
            return opcClient.WriteValues(groupName, itemValues, ref errMsg);
        }

        /// <summary>
        /// 同步写单个项操作
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="itemValues"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool WriteItemSync(string groupName, KeyValuePair<string, object> itemValue, ref string errMsg)
        {
            return opcClient.WriteValue(groupName, itemValue, ref errMsg);
        }
    }
} 

