using System;
using System.Linq;
using MSTL.LogAgent;
using MSTL.OpcClient;
using System.Collections.Generic;

namespace IECSC.CRN.SINGLE
{
    public class OpcAction
    {
        public OpcClient opcClient;
        /// <summary>
        /// 日志
        /// </summary>
        private ILog log
        {
            get
            {
                return Log.Store[this.GetType().FullName];
            }
        }

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
        private OpcAction()
        {
            opcClient = new OpcClient();
        }
        #endregion

        /// <summary>
        /// OPC初始化
        /// </summary>
        public bool OpcInit()
        {
            var errMsg = string.Empty;
            try
            {
                //连接OPC
                if (opcClient.ConnectOpcServer(McConfig.Instance.OpcServerIp, McConfig.Instance.OpcSeverName, ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]连接OPCSERVER成功!"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]连接OPCSERVER失败:{errMsg}!"));
                    return false;
                }

                opcClient.DataChanged += OpcClient_DataChanged;

                //添加OPC组
                if (opcClient.AddOpcGroup(McConfig.Instance.OpcGroupName, ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]成功添加OPC分组!"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]添加OPC分组失败:{errMsg}!"));
                    return false;
                }

                //添加OPC项
                if (opcClient.AddOpcItems(McConfig.Instance.OpcGroupName, BizHandle.Instance.readItems.Select(p => p.TagLongName).ToArray(), ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]添加读取项成功!"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]添加读取项失败:{ errMsg}!"));
                    return false;
                }
                if (opcClient.AddOpcItems(McConfig.Instance.OpcGroupName, BizHandle.Instance.writeItems.Select(p => p.TagLongName).ToArray(), ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]添加写入项成功!"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]添加写入项失败:{ errMsg}!"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]OPC初始化失败:{ex.ToString()}"));
                return false;
            }
        }

        /// <summary>
        /// OPCCLIENT数据改变事件处理方法
        /// </summary>
        /// <param name="e"></param>
        private void OpcClient_DataChanged(MSTL.OpcClient.Model.DataChangedEventArgs e)
        {
            try
            {
                foreach (var item in e.Data)
                {
                    if (item.Quality.Equals(Opc.Da.Quality.Bad))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.plcConn));
                        continue;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.plcConn));
                    var items = BizHandle.Instance.readItems.FirstOrDefault(p => p.TagLongName == item.TagLongName);
                    if (items == null)
                    {
                        continue;
                    }
                    #region 绑定读取值
                    switch (items.BusIdentity)
                    {
                        case "Read.DeviceId":
                            BizHandle.Instance.plcStatus.DeviceId = (item.TagValue ?? 0).ToString();
                            break;
                        case "Read.HeartBeat":
                            BizHandle.Instance.plcStatus.HeartBeat = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.OperateMode":
                            BizHandle.Instance.plcStatus.OperateMode = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.MissionState":
                            BizHandle.Instance.plcStatus.MissionState = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.MissionType":
                            BizHandle.Instance.plcStatus.MissionType = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.MissionId":
                            BizHandle.Instance.plcStatus.MissionId = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.PalletId":
                            BizHandle.Instance.plcStatus.PalletNo = (item.TagValue ?? 0).ToString();
                            break;
                        case "Read.ActPosBay":
                            BizHandle.Instance.plcStatus.ActPosBay = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActPosLevel":
                            BizHandle.Instance.plcStatus.ActPosLevel = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActPosX":
                            BizHandle.Instance.plcStatus.ActPosX = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActPosY":
                            BizHandle.Instance.plcStatus.ActPosY = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActPosZ":
                            BizHandle.Instance.plcStatus.ActPosZ = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActPosZDeep":
                            BizHandle.Instance.plcStatus.ActPosZDeep = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActSpeedX":
                            BizHandle.Instance.plcStatus.ActSpeedX = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActSpeedY":
                            BizHandle.Instance.plcStatus.ActSpeedY = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActSpeedZ":
                            BizHandle.Instance.plcStatus.ActSpeedZ = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.ActSpeedZDeep":
                            BizHandle.Instance.plcStatus.ActSpeedZDeep = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.LoadStatus":
                            BizHandle.Instance.plcStatus.LoadStatus = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.FaultNo":
                            BizHandle.Instance.plcStatus.FaultNo = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                        case "Read.NoFunction":
                            BizHandle.Instance.plcStatus.NoFunction = Convert.ToInt32(item.TagValue ?? 0);
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[基础配置]OPC读取信息失败:{ex.ToString()}"));
            }
        }

        /// <summary>
        /// 写入心跳信号
        /// </summary>
        public bool WriteHeartBeat(ref string errMsg)
        {
            try
            {
                var item = BizHandle.Instance.writeItems.FirstOrDefault(p => p.BusIdentity.Equals("Write.HeartBeat"));
                opcClient.WriteValueAsync(McConfig.Instance.OpcGroupName, new KeyValuePair<string, object>(item.TagLongName, 1), ref errMsg);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"WriteHeartBeat()异常:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        ///写入任务标记
        /// </summary>
        public bool WriteSeqNo(int value, ref string errMsg)
        {
            try
            {
                var seqNo = BizHandle.Instance.writeItems.FirstOrDefault(p => p.BusIdentity.Equals("Write.SequenceNo"));
                opcClient.WriteValue(McConfig.Instance.OpcGroupName, new KeyValuePair<string, object>(seqNo.TagLongName, value), ref errMsg);
                if(value == 0)
                {
                    var missionType = BizHandle.Instance.writeItems.FirstOrDefault(p => p.BusIdentity.Equals("Write.MissionType"));
                    opcClient.WriteValue(McConfig.Instance.OpcGroupName, new KeyValuePair<string, object>(missionType.TagLongName, 0), ref errMsg);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"WriteSeqNo()异常:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 写入指令
        /// </summary>
        public bool DownCmdToOpc(ref string errMsg)
        {
            try
            {
                var taskCmd = BizHandle.Instance.taskCmd;

                var keyValues = new KeyValuePair<string, object>[17];
                foreach (var item in BizHandle.Instance.writeItems)
                {
                    #region 写入指令信息
                    if(item.BusIdentity.Equals("Write.HeartBeat"))
                    {
                        keyValues[0] = new KeyValuePair<string, object>(item.TagLongName, 1);
                    }
                    else if (item.BusIdentity.Equals("Write.MissionCount"))
                    {
                        keyValues[1] = new KeyValuePair<string, object>(item.TagLongName, 1);
                    }
                    else if (item.BusIdentity.Equals("Write.DeviceId"))
                    {
                        keyValues[2] = new KeyValuePair<string, object>(item.TagLongName, BizHandle.Instance.plcStatus.CrnName);
                    }
                    else if (item.BusIdentity.Equals("Write.MissionType"))
                    {
                        keyValues[3] = new KeyValuePair<string, object>(item.TagLongName, BizHandle.Instance.plcStatus.LoadStatus == 1 ? 5 : 1);
                    }
                    else if (item.BusIdentity.Equals("Write.MissionId"))
                    {
                        keyValues[4] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.TaskNo);
                    }
                    else if (item.BusIdentity.Equals("Write.PalletId"))
                    {
                        keyValues[5] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.PalletNo);
                    }
                    else if (item.BusIdentity.Equals("Write.NoFunction"))
                    {
                        keyValues[6] = new KeyValuePair<string, object>(item.TagLongName, 0);
                    }
                    else if (item.BusIdentity.Equals("Write.EpArea"))
                    {
                        if (taskCmd.CmdType.Equals("I"))
                        {
                            keyValues[7] = new KeyValuePair<string, object>(item.TagLongName, GetASCII(taskCmd.SlocPlcNo.Substring(0, 1)));
                        }
                        else
                        {
                            keyValues[7] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.EpNo"))
                    {
                        if (taskCmd.CmdType.Equals("I"))
                        {
                            keyValues[8] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.SlocPlcNo.Substring(1, 4));
                        }
                        else
                        {
                            keyValues[8] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.FromRow"))
                    {
                        if (taskCmd.CmdType.Equals("O") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[9] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.SlocPlcNo.Substring(0, 2));
                        }
                        else
                        {
                            keyValues[9] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.FromBay"))
                    {
                        if (taskCmd.CmdType.Equals("O") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[10] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.SlocPlcNo.Substring(2, 2));
                        }
                        else
                        {
                            keyValues[10] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.FromLevel"))
                    {
                        if (taskCmd.CmdType.Equals("O") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[11] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.SlocPlcNo.Substring(4, 2));
                        }
                        else
                        {
                            keyValues[11] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.ApArea"))
                    {
                        if (taskCmd.CmdType.Equals("O"))
                        {
                            keyValues[12] = new KeyValuePair<string, object>(item.TagLongName, GetASCII(taskCmd.ElocPlcNo.Substring(0, 1)));
                        }
                        else
                        {
                            keyValues[12] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.ApNo"))
                    {
                        if (taskCmd.CmdType.Equals("O"))
                        {
                            keyValues[13] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.ElocPlcNo.Substring(1, 4));
                        }
                        else
                        {
                            keyValues[13] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.ToRow"))
                    {
                        if (taskCmd.CmdType.Equals("I") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[14] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.ElocPlcNo.Substring(0, 2));
                        }
                        else
                        {
                            keyValues[14] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.ToBay"))
                    {
                        if (taskCmd.CmdType.Equals("I") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[15] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.ElocPlcNo.Substring(2, 2));
                        }
                        else
                        {
                            keyValues[15] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    else if (item.BusIdentity.Equals("Write.ToLevel"))
                    {
                        if (taskCmd.CmdType.Equals("I") || taskCmd.CmdType.Equals("M"))
                        {
                            keyValues[16] = new KeyValuePair<string, object>(item.TagLongName, taskCmd.ElocPlcNo.Substring(4, 2));
                        }
                        else
                        {
                            keyValues[16] = new KeyValuePair<string, object>(item.TagLongName, 0);
                        }
                    }
                    #endregion 
                }
                if (keyValues.Length > 0)
                {
                    opcClient.WriteValues(McConfig.Instance.OpcGroupName, keyValues, ref errMsg);
                    var tagLongName = BizHandle.Instance.writeItems.FirstOrDefault(p => p.BusIdentity.Equals("Write.SequenceNo")).TagLongName;
                    opcClient.WriteValue(McConfig.Instance.OpcGroupName, new KeyValuePair<string, object>(tagLongName, 1), ref errMsg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error($"写入指令信息至PLC异常：{ex.ToString()}");
                return false;
            }
        }
        /// <summary>
        /// ASCII码转化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetASCII(string value)
        {
            byte[] array = System.Text.Encoding.ASCII.GetBytes(value);
            int I = (int)(array[0]);
            return I;
        }
    }
}
