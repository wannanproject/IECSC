using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.ALARM
{
    public class BizHandle
    {
        /// <summary>
        /// 站台信息
        /// </summary>
        public List<AlarmLoc> locList = null;
        /// <summary>
        /// 报警信息
        /// </summary>
        public Dictionary<string, AlarmItem> alarmItems = null;


        #region 单例模式
        private static BizHandle _instance = null;
        public static BizHandle Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(BizHandle))
                    {
                        if (_instance == null)
                        {
                            _instance = new BizHandle();
                        }
                    }
                }
                return _instance;
            }
        }
        public BizHandle()
        {
            locList = new List<AlarmLoc>();
            alarmItems = new Dictionary<string, AlarmItem>();
        }
        #endregion

        /// <summary>
        /// 初始化数据库配置
        /// </summary>
        public bool InitDb()
        {
            var errMsg = string.Empty;
            if (DbAction.Instance.GetDbTime())
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.dbConn));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.dbConn));
                return false;
            }
            if (!InitLoc())
            {
                return false;
            }
            if(!InitItems())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 初始化站台信息
        /// </summary>
        public bool InitLoc()
        {
            try
            {
                //初始化站台信息
                var dtLoc = DbAction.Instance.GetAlarmLoc();
                if(dtLoc == null || dtLoc.Rows.Count == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("未获取到站台信息"));
                    return false;
                }
                foreach(DataRow row in dtLoc.Rows)
                {
                    var loc = new AlarmLoc();
                    loc.LocPlcNo = row["loc_plc_no"].ToString();
                    loc.LocArea = Convert.ToInt32(row["loc_area"]);
                    loc.LocCode = Convert.ToInt32(row["loc_code"]);
                    loc.AlarmType = Convert.ToInt32(row["alarm_type"]);
                    var dtAlarm = DbAction.Instance.GetAlarmInfo(loc.AlarmType);
                    if (dtAlarm == null || dtAlarm.Rows.Count == 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"站台{loc.LocPlcNo}未获取到报警描述"));
                        locList.Add(loc);
                        continue;
                    }
                    foreach (DataRow descRow in dtAlarm.Rows)
                    {
                        var alarmIndex = Convert.ToInt32(descRow["alarm_index"]);
                        var alarmInfo = new AlarmInfo();
                        alarmInfo.Objid = 0;
                        alarmInfo.AlarmDesc = descRow["alarm_desc"].ToString();
                        loc.alarmInfo.Add(alarmIndex, alarmInfo);
                    }
                    locList.Add(loc);
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化站台故障描述成功"));
                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[异常]初始化站台故障描述,[原因]{ex.Message}"));
                return false;
            }
        }
        /// <summary>
        /// 初始化项信息
        /// </summary>
        public bool InitItems()
        {
            try
            {
                var dtGroup = DbAction.Instance.GetAlarmGroup();
                if (dtGroup == null || dtGroup.Rows.Count == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("未获取到配置OPC组信息"));
                    return false;
                }
                foreach (DataRow row in dtGroup.Rows)
                {
                    var groupName = row["group_name"].ToString();
                    var dtLoc = DbAction.Instance.GetOpcLocItems(groupName);
                    if (dtLoc == null || dtLoc.Rows.Count == 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"组{groupName}未获取到OPC分区、编号、类型配置项信息"));
                        continue;
                    }
                    //获取OPC分区、编号、类型长名
                    var alarmAreaKey = string.Empty;
                    var alarmCodeKey = string.Empty;
                    var alarmTypeKey = string.Empty;
                    foreach (DataRow locRow in dtLoc.Rows)
                    {
                        switch(locRow["tagname"].ToString())
                        {
                            case "Alarm_Area":
                                alarmAreaKey = locRow["tagLongName"].ToString();
                                break;
                            case "Alarm_Code":
                                alarmCodeKey = locRow["tagLongName"].ToString();
                                break;
                            case "Alarm_Type":
                                alarmTypeKey = locRow["tagLongName"].ToString();
                                break;
                        }
                    }
                    //获取OPC项
                    var dtItems = DbAction.Instance.GetOpcAlarmItems(groupName);
                    if (dtItems == null || dtItems.Rows.Count == 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"组{groupName}未获取到OPC配置项信息"));
                        continue;
                    }
                    foreach (DataRow itemRow in dtItems.Rows)
                    {
                        var alarmItem = new AlarmItem();
                        alarmItem.AlarmAreaKey = alarmAreaKey;
                        alarmItem.AlarmCodeKey = alarmCodeKey;
                        alarmItem.AlarmTypeKey = alarmTypeKey;
                        alarmItem.Kind = Convert.ToInt32(itemRow["kind"]);
                        alarmItem.AlarmIndex = Convert.ToInt32(itemRow["tagindex"]);
                        alarmItem.TagLongName = itemRow["tagLongName"].ToString();
                        alarmItems.Add(alarmItem.TagLongName, alarmItem);
                    }
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化OPC配置项成功"));
                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[异常]初始化OPC配置项,[原因]{ex.Message}"));
                return false;
            }
        }

        public bool InitOpc()
        {
            try
            {
                var errMsg = string.Empty;
                if (Tools.Instance.PingNetAddress(McConfig.Instance.LocIp))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.plcConn));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.plcConn));
                    return false;
                }
                if (OpcAction.Instance.ConnectOpc(ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化OPC连接成功"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化OPC连接失败,原因{errMsg}"));
                    return false;
                }
                if (OpcAction.Instance.AddOpcGroup(ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化OPC组成功"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化OPC组失败,原因{errMsg}"));
                    return false;
                }
                if (OpcAction.Instance.AddOpcItem(ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化OPC项成功"));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化OPC项失败,原因{errMsg}"));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[异常]初始化OPC,[原因]{ex.Message}"));
                return false;
            }
        }

        public void BizListen()
        {
            foreach(AlarmItem alarmItem in alarmItems.Values)
            {
                //验证 是否为异常项
                if (alarmItem.Kind == 0)
                {
                    continue;
                }
                //获取报警站台分区、编码、类型
                var alarmArea = alarmItems[alarmItem.AlarmAreaKey].TagValue;
                if(alarmArea == 0)
                {
                    continue;
                }
                var alarmCode = alarmItems[alarmItem.AlarmCodeKey].TagValue;
                if (alarmCode == 0)
                {
                    continue;
                }
                var alarmType = alarmItems[alarmItem.AlarmTypeKey].TagValue;
                if (alarmType == 0)
                {
                    continue;
                }
                var alarmIndex = alarmItem.AlarmIndex;
                if (alarmIndex == 0)
                {
                    continue;
                }
                var alarmLocNo = alarmArea.ToString() + alarmCode.ToString();
                //验证 异常是否被记录
                if (alarmItem.AlarmMark == alarmItem.TagValue && alarmItem.AlarmLocNo == alarmLocNo)
                {
                    continue;
                }
                //获取站台信息
                var alarmLoc = locList.FirstOrDefault(p => p.LocArea.Equals(alarmArea) && p.LocCode.Equals(alarmCode));
                var errMsg = string.Empty;
                //如果报警位没变，但是组对应的报警站台改变，则复位原站台报警
                if (CheckAlarmLoc(alarmItem, alarmLoc, alarmLocNo, alarmIndex))
                {
                    alarmItem.AlarmLocNo = string.Empty;
                }
                if (alarmLoc == null)
                {
                    continue;
                }
                //标记报警站台
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData(alarmLoc.LocPlcNo, InfoType.locStatus));
                //工位报警
                if (alarmItem.TagValue == 1)
                {
                    //获取报警记录OBJID
                    alarmLoc.alarmInfo[alarmIndex].Objid = DbAction.Instance.GetObjid(ref errMsg);
                    if (alarmLoc.alarmInfo[alarmIndex].Objid <= 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[运行]获取OBJID出错：{errMsg}"));
                        continue;
                    }
                    //记录报警
                    var result = DbAction.Instance.SaveAlarmData(alarmLoc, alarmIndex, ref errMsg);
                    if (result)
                    {
                        alarmItem.AlarmMark = alarmItem.TagValue;
                        alarmItem.AlarmLocNo = alarmLocNo;
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[报警]{alarmLoc.LocPlcNo}-{alarmLoc.alarmInfo[alarmIndex].AlarmDesc}"));
                    }
                    else
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[运行]记录报警{alarmLoc.LocPlcNo}-{alarmLoc.alarmInfo[alarmIndex].AlarmDesc}失败：{errMsg}"));
                        continue;
                    }
                }
                else
                {
                    //如果OBJID信息丢失，不再处理
                    if (alarmLoc.alarmInfo[alarmIndex].Objid <= 0)
                    {
                        alarmItem.AlarmMark = alarmItem.TagValue;
                        alarmItem.AlarmLocNo = alarmLocNo;
                        continue;
                    }
                    //更新报警已处理信息
                    var result = DbAction.Instance.UpdateAlarmData(alarmLoc, alarmIndex, ref errMsg);
                    if (result)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[报警]{alarmLoc.LocPlcNo}-{alarmLoc.alarmInfo[alarmIndex].AlarmDesc} 已处理"));
                        alarmItem.AlarmMark = alarmItem.TagValue;
                        alarmItem.AlarmLocNo = string.Empty;
                        alarmLoc.alarmInfo[alarmIndex].Objid = 0;
                    }
                    else
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[运行]更新报警记录报警{alarmLoc.LocPlcNo}-{alarmLoc.alarmInfo[alarmIndex].AlarmDesc}已处理失败：{errMsg}"));
                        continue;
                    }
                }
            }
        }

        private bool CheckAlarmLoc(AlarmItem alarmItem, AlarmLoc alarmLoc, string currLocNo, int alarmIndex)
        {
            if (string.IsNullOrEmpty(alarmItem.AlarmLocNo))
            {
                return false;
            }
            if ( alarmItem.AlarmLocNo == currLocNo)
            {
                return false;
            }
            var oldAlarmLoc = locList.FirstOrDefault(p => p.LocArea.ToString() + p.LocCode.ToString() == alarmItem.AlarmLocNo);
            if(oldAlarmLoc == null)
            {
                return false;
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData(oldAlarmLoc.LocPlcNo, InfoType.locStatus));
            if (oldAlarmLoc.alarmInfo[alarmIndex].Objid <= 0)
            {
                return false;
            }
            var errMsg = string.Empty;
            //更新报警已处理信息
            if (DbAction.Instance.UpdateAlarmData(oldAlarmLoc, alarmIndex, ref errMsg))
            {
                oldAlarmLoc.alarmInfo[alarmIndex].Objid = 0;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[报警]下位机[{oldAlarmLoc.LocPlcNo}]变更[{alarmLoc?.LocPlcNo}]报警[{alarmLoc.alarmInfo[alarmIndex].AlarmDesc}]复位"));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[报警]下位机[{oldAlarmLoc.LocPlcNo}]变更[{alarmLoc?.LocPlcNo}]报警[{alarmLoc.alarmInfo[alarmIndex].AlarmDesc}]复位失败：{errMsg}"));
                return false;
            }
            return true;
        }
    }
}
