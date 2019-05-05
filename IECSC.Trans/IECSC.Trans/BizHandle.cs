using MSTL.LogAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using IECSC.Trans.DB;
using IECSC.Trans.Entity;
using IECSC.Trans.Common;
using IECSC.Trans.OPC;

namespace IECSC.Trans
{
    class BizHandle
    {
        #region 系统日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }
        #endregion

        #region 初始化参数
        public Dictionary<string, TransLocStatusInfo> locStatuesDic = new Dictionary<string, TransLocStatusInfo>();
        public Dictionary<string, CommonOpcItems> opcItemsDic = new Dictionary<string, CommonOpcItems>();
        
        //OPC相关
        private OPCWrite opcWrite;
        private string opcServerName;
        private string opcServerIp;
        private const string OPCGROUPNAME = "Trans";
        #endregion

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

        private BizHandle()
        {
            opcServerIp = McConfig.Instance.OpcServerIp;
            opcServerName = McConfig.Instance.OpcServerName;
            opcWrite = new OPCWrite(OPCGROUPNAME);
        }
        #endregion

        #region 初始化
        public bool Init(ref string errMsg)
        {
            try
            {
                //测试数据库连接
                if (!DbAction.Instance.GetDbTime())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已断开", ShowInfoTypeEnum.DbConn));
                    errMsg = "数据库连接失败";
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已连接", ShowInfoTypeEnum.DbConn));
                //站台初始化
                if (!LocInit(ref errMsg))
                {
                    errMsg = "站台初始化信息失败:" + errMsg;
                    return false;
                }
                //OPC初始化
                if (!OPCInit(ref errMsg))
                {
                    errMsg = "OPC初始化错误:" + errMsg;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errMsg += "初始化异常,请检查配置";
                Log.Error("初始化异常：", ex);
                return false;
            }
        }
        /// <summary>
        /// 站台初始化
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool LocInit(ref string errMsg)
        {
            try
            {
                var locFromDbs = DbAction.Instance.GetLocStatus(McConfig.Instance.AppCode);
                if (locFromDbs == null || locFromDbs.Rows.Count <= 0)
                {
                    errMsg += "数据库未获取到站台信息";
                    Log.Error("初始化站台失败：数据库未获取到数据信息");
                    return false;
                }
                foreach (DataRow dr in locFromDbs.Rows)
                {
                    locStatuesDic.Add(dr["LOC_NO"].ToString(), new TransLocStatusInfo()
                    {
                        LocNo = dr["LOC_NO"].ToString(),
                        LocPlcNo = dr["LOC_PLC_NO"].ToString(),
                        LocTypeNo = Convert.ToInt32(dr["LOC_TYPE_NO"]),
                        LocTypeDesc = dr["LOC_TYPE_NAME"].ToString(),
                        TaskType = dr["TYPEDESC"].ToString()
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                errMsg += "站台基本信息初始化失败";
                Log.Error("站台基本信息初始化失败(LocInit)：", ex);
                return false;
            }
        }
        /// <summary>
        /// OPC初始化
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool OPCInit(ref string errMsg)
        {
            if (!OpcItemInit(ref errMsg))
            {
                return false;
            }
            //opc初始化
            OpcAction.Instance.AttachEntity(new OPCObserver());
            bool isOpcOpen = OpcAction.Instance.OpcNetIntial(opcServerName, opcItemsDic.Keys.ToArray(), OPCGROUPNAME, ref errMsg, opcServerIp);
            if (!isOpcOpen)
            {
                errMsg += "初始化OPC失败";
                Log.Error("初始化OPC失败:"+ errMsg);
            }
            return isOpcOpen;
        }
        /// <summary>
        /// 加载OPC项目
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool OpcItemInit(ref string errMsg)
        {
            try
            {
                var dt = DbAction.Instance.GetLocOPC(McConfig.Instance.AppCode);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    errMsg += "数据库未获取到OPC数据信息";
                    Log.Error("数据库未获取到OPC数据信息");
                    return false;
                }
                opcItemsDic.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    opcItemsDic.Add(row["TAGLONGNAME"].ToString(), new CommonOpcItems
                    {
                        LocNo = row["LOC_NO"].ToString(),
                        BusinessIdentity = row["BUSINESSIDENTITY"].ToString(),
                        TagLongName = row["TAGLONGNAME"].ToString()
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                errMsg += "初始化站台OPC基本信息失败";
                Log.Error("初始化站台OPC基本信息失败(LocInit)：", ex);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 业务处理主入口 时钟循环处理
        /// </summary>
        public void HandleBussiness()
        {
            DateTime beforDT = DateTime.Now;
            foreach (var locStates in locStatuesDic.Values)
            {
                switch (locStates.TaskType)
                {
                    case "RequestSrmTask":
                        RequestSrmTask.Instance.HandleLoc(locStates.LocNo, opcWrite);
                        break;
                    case "UpdateFreeFlag":
                        UpdateFreeFlag.Instance.HandleLoc(locStates.LocNo, opcWrite);
                        break;
                    case "RequestAgvTask":
                        RequestAgvTask.Instance.HandleLoc(locStates.LocNo, opcWrite);
                        break;
                }
            }
            DateTime AfterDT = DateTime.Now;
            TimeSpan ts = AfterDT.Subtract(beforDT);
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData(ts.TotalMilliseconds.ToString().Split('.')[0], ShowInfoTypeEnum.CirTime));
        }
    }
}