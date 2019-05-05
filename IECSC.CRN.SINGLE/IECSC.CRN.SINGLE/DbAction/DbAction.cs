using System;
using System.Text;
using MSTL.LogAgent;
using System.Data;
using Dapper;
using DapperExtensions;
using MSTL.DbClient;

namespace IECSC.CRN.SINGLE
{
    public partial class DbAction
    {
        private IDatabase Db = null;
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
        private static DbAction _instance = null;
        public static DbAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(DbAction))
                    {
                        if (_instance == null)
                        {
                            _instance = new DbAction();
                        }
                    }
                }
                return _instance;
            }
        }
        public DbAction()
        {
            try
            {
                var errMsg = string.Empty;
                this.Db = DbHelper.GetDb(McConfig.Instance.OrclConnect, DbHelper.DataBaseType.Oracle, ref errMsg);
            }
            catch (Exception ex)
            {
                log.Error($"数据库连接异常：{ex.ToString()}");
            }
        }
        #endregion

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        public bool GetDbTime()
        {
            try
            {
                var dt = Db.Connection.QueryTable("SELECT SYSDATE FROM DUAL");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("连接数据库异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        public DataTable GetCrnFault()
        {
            try
            {
                return Db.Connection.QueryTable("SELECT * FROM PSB_CRN_ERR");
            }
            catch (Exception ex)
            {
                log.Error("获取堆垛机错误描述异常:" +ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取堆垛机信息
        /// </summary>
        private DataTable GetCrnData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" SELECT T.CRN_NO, T.CRN_NAME FROM PSB_CRN T");
                sb.Append($" WHERE T.CRN_NO = '{McConfig.Instance.CrnName}'");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error("读取堆垛机基础信息表异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取堆垛机读取项信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetReadItemsData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" SELECT T.TAGCHANNELPREFIX||T.TAGGROUP||T1.BUSIDENTITY TAGLONGNAME,");
                sb.Append(" T.TAGCHANNELPREFIX, T.TAGGROUP, T1.TAGNAME, T1.TAGINDEX,");
                sb.Append(" T1.BUSIDENTITY, T1.KIND FROM PSB_OPC_CRN_GROUP T");
                sb.Append(" LEFT JOIN PSB_OPC_CRN_ITEMS T1 ON T1.TAGPREFIX = 'SINGLE' AND T1.KIND = 'R'");
                sb.Append($" WHERE T.EQUIPNO = '{McConfig.Instance.CrnName}'");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error("获取堆垛机读取项异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取堆垛机写入项信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetWriteItemsData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" SELECT T.TAGCHANNELPREFIX||T.TAGGROUP||T1.BUSIDENTITY TAGLONGNAME,");
                sb.Append(" T.TAGCHANNELPREFIX, T.TAGGROUP, T1.TAGNAME, T1.TAGINDEX,");
                sb.Append(" T1.BUSIDENTITY, T1.KIND FROM PSB_OPC_CRN_GROUP T");
                sb.Append(" LEFT JOIN PSB_OPC_CRN_ITEMS T1 ON T1.TAGPREFIX = 'SINGLE' AND T1.KIND = 'W'");
                sb.Append($" WHERE T.EQUIPNO = '{McConfig.Instance.CrnName}'");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error("获取堆垛机写入项异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取堆垛机指令
        /// </summary>
        /// <returns></returns>
        private DataTable GetCmdNoExec()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" SELECT * FROM WBS_TASK_CMD T");
                sb.Append(" WHERE T.TRANSFER_TYPE = 10 AND T.CMD_STEP = '00'");
                sb.Append($" AND T.TRANSFER_NO LIKE '{McConfig.Instance.CrnName}%'");
                sb.Append(" ORDER BY T.OBJID");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error("获取未执行指令信息异常:" + ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取堆垛机指令
        /// </summary>
        /// <returns></returns>
        public DataTable GetCmdByCrnNo(string CrnNo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT T.OBJID,T.TASK_NO,T.SLOC_TYPE,T.SLOC_NO,T.SLOC_PLC_NO,T.ELOC_TYPE,T.ELOC_NO,");
                sb.Append(" T.ELOC_PLC_NO,T.PRODUCT_NO,T.PALLET_NO,T.CMD_TYPE,T.CMD_STEP FROM WBS_TASK_CMD T");
                sb.Append(" WHERE T.TRANSFER_TYPE = 10");
                sb.Append($" AND T.TRANSFER_NO LIKE '{McConfig.Instance.CrnName}%'");
                sb.Append(" ORDER BY T.OBJID DESC");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error("获取堆垛机指令信息异常:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 修改指令步骤
        /// </summary>
        public bool UpdateCmdStep(string cmdStep)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE WBS_TASK_CMD T SET ");
                sb.Append(" T.CMD_STEP = :CMD_STEP,");
                sb.Append(" T.EXCUTE_DATE = SYSDATE");
                sb.Append(" WHERE T.OBJID = :OBJID");
                var param = new DynamicParameters();
                param.Add("OBJID", BizHandle.Instance.taskCmd.ObjId);
                param.Add("CMD_STEP", cmdStep);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("修改指令步骤异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取报警记录ID
        /// </summary>
        /// <returns></returns>
        public int GetObjidForFault()
        {
            try
            {
                var objid = Db.Connection.ExecuteScalar<int>("SELECT SEQ_Z40_WH_CRN_FORK_ERR_LOG.NEXTVAL FROM DUAL");
                return objid;
            }
            catch (Exception ex)
            {
                log.Error("获取报警记录ID异常:" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 记录报警信息
        /// </summary>
        /// <returns></returns>
        public bool RecordWarnLog(int objid)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" INSERT INTO Z40_CRN_FORK_ERR_LOG");
                sb.Append(" (OBJID, CRN_FORK_NO, ERR_NO, ERR_DESC, ERR_BEGIN_TIME, TASK_NO, CMD_OBJID)");
                sb.Append(" VALUES");
                sb.Append(" (:OBJID, :CRN_FORK_NO, :ERR_NO, :ERR_DESC, SYSDATE, :TASK_NO, :CMD_OBJID)");
                var param = new DynamicParameters();
                param.Add("OBJID", objid);
                param.Add("CRN_FORK_NO", BizHandle.Instance.plcStatus.CrnNo);
                param.Add("ERR_NO", BizHandle.Instance.plcStatus.FaultNo.ToString());
                param.Add("ERR_DESC", BizHandle.Instance.crnErr[BizHandle.Instance.plcStatus.FaultNo.ToString()]);
                param.Add("TASK_NO", BizHandle.Instance.taskCmd.TaskNo.ToString()??"0");
                param.Add("CMD_OBJID", BizHandle.Instance.taskCmd.ObjId.ToString() ?? "0");
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("记录设备报警异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 更新报警已处理信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateWarnLog(int objid)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE Z40_CRN_FORK_ERR_LOG T");
                sb.Append(" SET T.ERR_END_TIME = SYSDATE,");
                sb.Append(" T.ERR_SECONDS = (SYSDATE - T.ERR_BEGIN_TIME) * 24 * 60 * 60");
                sb.Append(" WHERE T.OBJID = :OBJID");
                var param = new DynamicParameters();
                param.Add("OBJID", objid);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("更新报警已处理信息异常:" + ex.ToString());
                return false;
            }
        }

        #region 指令结束
        /// <summary>
        /// 指令结束
        /// </summary>
        public bool FinishCrnCmd(int objid, int cmdId, string curLoc, int finishState, ref string errMsg)
        {
            try
            {
                var dtCmd = Db.Connection.QueryTable($"SELECT * FROM WBS_TASK_CMD WHERE OBJID = {cmdId}");
                if (dtCmd == null || dtCmd.Rows.Count == 0)
                {
                    return true;
                }
                if (!InsertFinishData(objid, cmdId, curLoc, finishState))
                {
                    return false;
                }
                if (!ExecProcCmdFinish(objid, ref errMsg))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("结束指令异常:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 保存数据至参数表
        /// </summary>
        public bool InsertFinishData(int ObjId, int cmdId, string curLoc, int finishState)
        {
            try
            {
                var dt = Db.Connection.QueryTable($"SELECT * FROM TPROC_0300_CMD_FINISH WHERE OBJID = {ObjId}");
                if (dt == null || dt.Rows.Count <= 0)
                {
                    var sb = new StringBuilder();
                    sb.Append(" INSERT INTO TPROC_0300_CMD_FINISH");
                    sb.Append(" (OBJID,CMD_OBJID,CURR_LOC_NO,FINISH_STATUS)");
                    sb.Append(" VALUES");
                    sb.Append(" (:OBJID,:CMD_OBJID,:CURR_LOC_NO,:FINISH_STATUS)");
                    var param = new DynamicParameters();
                    param.Add("OBJID", ObjId);
                    param.Add("CMD_OBJID", cmdId);
                    param.Add("CURR_LOC_NO", curLoc);
                    param.Add("FINISH_STATUS", finishState);
                    Db.Connection.Execute(sb.ToString(), param); 
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"保存数据至指令结束参数表异常{ex.ToString()}");
                return false;
            }
        }

        public bool ExecProcCmdFinish(int objId, ref string errMsg)
        {
            try
            {
                var procName = "PROC_0300_CMD_FINISH";
                var param = new DynamicParameters();
                param.Add("I_PARAM_OBJID", objId);
                param.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                param.Add("O_ERR_DESC", null, DbType.String, ParameterDirection.Output, size:80);
                Db.Connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
                var result = param.Get<string>("O_ERR_DESC");
                if (!string.IsNullOrEmpty(result))
                {
                    errMsg = result;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"执行指令结束存储过程异常:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取指令结束参数表主键ID
        /// </summary>
        /// <returns></returns>
        public int GetObjidForCmdFinish()
        {
            try
            {
                var objid = Db.Connection.ExecuteScalar<int>("SELECT SEQ_TPROC_0300_CMD_FINISH.NEXTVAL FROM DUAL");
                return objid;
            }
            catch (Exception ex)
            {
                log.Error($"获取指令结束参数表主键ID异常:{ex.Message}");
                return 0;
            }
        }
        #endregion

        #region 先入品、空出库
        /// <summary>
        /// 获取空出库人工确认标记
        /// </summary>
        public int GetFipFlag()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" SELECT * FROM PEM_CRN_FORK_STATUS t");
                sb.Append($" WHERE T.CRN_FORK_NO LIKE '{McConfig.Instance.CrnName}%'");
                var dt = Db.Connection.QueryTable(sb.ToString());
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return 0;
                }
                return Convert.ToInt32(dt.Rows[0]["FIP_FLAG"]);
            }
            catch (Exception ex)
            {
                log.Error("获取空出库人工确认标记异常:" + ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 更新设备故障异常
        /// </summary>
        public bool UpdateEquipErrStatus(int taskNo, int faultNo, int fipFlag)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE PEM_CRN_FORK_STATUS T");
                sb.Append(" SET T.TASK_NO = :TASKNO,");
                sb.Append(" T.FIP_FAULT_NO = :FAULTNO,");
                sb.Append(" T.FIP_FLAG = :FIP_FLAG,");
                sb.Append(" T.FIP_DATE = SYSDATE");
                sb.Append(" WHERE T.CRN_NO = :CRN_NO");
                var param = new DynamicParameters();
                param.Add("I_TASKNO", taskNo);
                param.Add("I_FAULTNO", faultNo);
                param.Add("I_FIP_FLAG", fipFlag);
                param.Add("CRN_NO", McConfig.Instance.CrnName);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"更新设备故障异常{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 更新设备故障异常
        /// </summary>
        public bool UpdateEquipErrStatus(int taskNo)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE PEM_CRN_FORK_STATUS T");
                sb.Append(" SET T.TASK_NO = :TASKNO,");
                sb.Append(" T.FIP_FAULT_NO = 0,");
                sb.Append(" T.FIP_FLAG = 0,");
                sb.Append(" T.FIP_DATE = NULL,");
                sb.Append(" T.FIP_HANDLE_RESULT = NULL");
                sb.Append(" WHERE T.CRN_NO = :CRN_NO");
                var param = new DynamicParameters();
                param.Add("I_TASKNO", taskNo);
                param.Add("CRN_NO", McConfig.Instance.CrnName);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"更新设备故障异常{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 先入品处理
        /// </summary>
        public int ExecProcFirstInProduct(ref string errMsg)
        {
            try
            {
                var procName = "PROC_0400_FIRST_IN_PRODUCT";
                var param = new DynamicParameters();
                param.Add("I_EQUIP_NO", McConfig.Instance.CrnName);
                param.Add("O_FIP_FLAG", 0, DbType.Int32, ParameterDirection.Output);
                param.Add("O_FIP_HANDLE_RESULT", null, DbType.String, ParameterDirection.Output, size:80);
                Db.Connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
                errMsg = param.Get<string>("O_FIP_HANDLE_RESULT");
                return param.Get<int>("O_FIP_FLAG");
            }
            catch (Exception ex)
            {
                log.Error($"处理先入品故障异常:{ex.Message}");
                return -1;
            }
        }
        #endregion
        
        /// <summary>
        /// 任务删除
        /// </summary>
        public bool DeleteTaskCmd(int taskNo)
        {
            try
            {
                var procName = "PROC_WCS_CRNCMD_DEL";
                var param = new DynamicParameters();
                param.Add("I_TASK_NO", taskNo);
                Db.Connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"PROC_WCS_CRNCMD_DEL异常:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 更新堆垛机状态
        /// </summary>
        public bool UpdateCrnStatus()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE PEM_CRN_FORK_STATUS T");
                sb.Append(" SET T.STEP =:STEP,");
                sb.Append(" T.STATUS =:STATUS,");
                sb.Append(" T.TO_BIN =:TO_BIN,");
                sb.Append(" T.LOADING =:LOADING,");
                sb.Append(" T.TASK_NO =:TASK_NO,");
                sb.Append(" T.FROM_BIN =:FROM_BIN,");
                sb.Append(" T.CRN_MODE =:CRN_MODE,");
                sb.Append(" T.FAULT_NO =:FAULT_NO,");
                sb.Append(" T.RECORD_TIME =SYSDATE,");
                sb.Append(" T.ACT_POS_X =:ACT_POS_X,");
                sb.Append(" T.ACT_POS_Y =:ACT_POS_Y,");
                sb.Append(" T.ACT_POS_Z =:ACT_POS_Z,");
                sb.Append(" T.ACT_SPEED_X =:ACT_SPEED_X,");
                sb.Append(" T.ACT_SPEED_Y =:ACT_SPEED_Y,");
                sb.Append(" T.ACT_SPEED_Z =:ACT_SPEED_Z");
                sb.Append(" WHERE T.CRN_NO =:CRN_NO");
                var param = new DynamicParameters();
                param.Add("STEP", BizHandle.Instance.plcStatus.MissionType);
                param.Add("STATUS", BizHandle.Instance.plcStatus.MissionState);
                param.Add("TO_BIN", BizHandle.Instance.plcStatus.ELocNo);
                param.Add("LOADING", BizHandle.Instance.plcStatus.LoadStatus);
                param.Add("TASK_NO", BizHandle.Instance.plcStatus.MissionId);
                param.Add("FROM_BIN", BizHandle.Instance.plcStatus.SLocNo);
                param.Add("CRN_MODE", BizHandle.Instance.plcStatus.OperateMode);               
                param.Add("FAULT_NO", BizHandle.Instance.plcStatus.FaultNo);
                param.Add("ACT_POS_X", BizHandle.Instance.plcStatus.ActPosX);
                param.Add("ACT_POS_Y", BizHandle.Instance.plcStatus.ActPosY);
                param.Add("ACT_POS_Z", BizHandle.Instance.plcStatus.ActPosZ);
                param.Add("ACT_SPEED_X", BizHandle.Instance.plcStatus.ActSpeedX);
                param.Add("ACT_SPEED_Y", BizHandle.Instance.plcStatus.ActSpeedY);
                param.Add("ACT_SPEED_Z", BizHandle.Instance.plcStatus.ActSpeedZ);      
                param.Add("CRN_NO", BizHandle.Instance.plcStatus.CrnNo);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"更新参数表设备状态异常:{ex.ToString()}");
                return false;
            }
        }
    }
}
