using Dapper;
using DapperExtensions;
using MSTL.DbClient;
using MSTL.LogAgent;
using System;
using System.Data;
using System.Text;

namespace IECSC.TRANS
{
    public partial class DbAction
    {
        /// <summary>
        /// 数据库操作类
        /// </summary>
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
            var errMsg = string.Empty;
            ConnDb(ref errMsg);
        }
        #endregion

        public bool ConnDb(ref string errMsg)
        {
            try
            {
                this.Db = DbHelper.GetDb(McConfig.Instance.DbConnect, DbHelper.DataBaseType.Oracle, ref errMsg);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error($"[异常]执行DbAction()建立数据库连接失败:{ex.ToString()}");
                return false;
            }
        }

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
                var Ip = McConfig.Instance.DbIp;
                if (Tools.Instance.PingNetAddress(Ip))
                {
                    var errMsg = string.Empty;
                    ConnDb(ref errMsg);
                }
                else
                {
                    log.Error($"[异常]执行GetDbTime()获取服务器时间失败:{ex.ToString()}");
                }
                return false;
            }
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        public DataTable GetLocData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT T.LOC_NO,T.LOC_PLC_NO,T.LOC_TYPE,T1.LOC_TYPE_NAME,T.BIZ_TYPE");
                sb.Append(" FROM PSB_OPC_LOC_GROUP T");
                sb.Append(" LEFT JOIN PSB_LOC_TYPE T1 ON T1.LOC_TYPE_NO = T.LOC_TYPE");
                sb.Append(" WHERE T.ISENABLE = 1");
                sb.Append($" AND T.BIZ_AREA = '{McConfig.Instance.LocArea}'");
                sb.Append(" ORDER BY T.LOC_NO");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetLocData()获取站台信息失败:{ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// 获取读取项
        /// </summary>
        public DataTable GetReadItemsData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT T.TAGGROUP||T1.BUSIDENTITY TAGLONGNAME,");
                sb.Append(" T.LOC_NO, T.LOC_PLC_NO, T1.BUSIDENTITY");
                sb.Append(" FROM PSB_OPC_LOC_GROUP T ");
                sb.Append(" LEFT JOIN PSB_OPC_LOC_ITEMS T1 ON T1.KIND = T.KIND AND T1.ISENABLE = 1");
                sb.Append(" WHERE T.ISENABLE = 1");
                sb.Append(" AND T1.BUSIDENTITY LIKE 'Read.%'");
                sb.Append($" AND T.BIZ_AREA = '{McConfig.Instance.LocArea}'");
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetReadItemsData()获取站台读取项信息失败:{ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// 获取写入项
        /// </summary>
        public DataTable GetWriteItemsData()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT T.TAGGROUP||T1.BUSIDENTITY TAGLONGNAME,");
                sb.Append(" T.LOC_NO, T.LOC_PLC_NO, T1.BUSIDENTITY");
                sb.Append(" FROM PSB_OPC_LOC_GROUP T ");
                sb.Append(" LEFT JOIN PSB_OPC_LOC_ITEMS T1 ON T1.KIND = T.KIND AND T1.ISENABLE = 1");
                sb.Append(" WHERE T.ISENABLE = 1");
                sb.Append(" AND T1.BUSIDENTITY LIKE 'Write.%'");
                sb.Append($" AND T.BIZ_AREA = '{McConfig.Instance.LocArea}'");
                var param = new DynamicParameters();
                return Db.Connection.QueryTable(sb.ToString());
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetWriteItemsData()获取站台写入项信息失败:{ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// 获取指令信息
        /// </summary>
        public DataTable GetTaskCmd(string locNo, string cmdStep)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT * FROM WBS_TASK_CMD T");
                sb.Append(" WHERE T.SLOC_NO = :SLOCNO");
                sb.Append(" AND T.CMD_STEP = :CMDSTEP");
                var param = new DynamicParameters();
                param.Add("SLOCNO", locNo);
                param.Add("CMDSTEP", cmdStep);
                return Db.Connection.QueryTable(sb.ToString(), param);
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetTaskCmd({locNo}, {cmdStep})获取指令信息失败:{ex.ToString()}");
                return null;
            }
        }
        /// <summary>
        /// 获取指令信息
        /// </summary>
        public DataTable GetTaskCmd(string locNo, string palletNo, string cmdStep)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT * FROM WBS_TASK_CMD T");
                sb.Append(" WHERE T.SLOC_NO = :SLOCNO");
                sb.Append(" AND T.PALLET_NO = :PALLETNO");
                sb.Append(" AND T.CMD_STEP = :CMDSTEP");
                var param = new DynamicParameters();
                param.Add("SLOCNO", locNo);
                param.Add("PALLETNO", palletNo);
                param.Add("CMDSTEP", cmdStep);
                return Db.Connection.QueryTable(sb.ToString(), param);
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetTaskCmd({locNo},{palletNo},{cmdStep})获取指令信息失败:{ex.ToString()}");
                return null;
            }
        }
        /// <summary>
        /// 获取指令信息
        /// </summary>
        public DataTable GetTaskCmd(string locNo)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT T.OBJID,T.TASK_NO,T.CMD_TYPE,T.CMD_STEP,T.SLOC_NO,T.SLOC_PLC_NO,T1.LOC_TYPE_NAME SLOC_TYPE,");
                sb.Append(" T.ELOC_NO,T.ELOC_PLC_NO,T2.LOC_TYPE_NAME ELOC_TYPE,T.PALLET_NO FROM WBS_TASK_CMD T");
                sb.Append(" LEFT JOIN PSB_LOC_TYPE T1 ON T1.LOC_TYPE_NO = T.SLOC_TYPE");
                sb.Append(" LEFT JOIN PSB_LOC_TYPE T2 ON T2.LOC_TYPE_NO = T.ELOC_TYPE");
                sb.Append(" WHERE T.TRANSFER_TYPE = '20'");
                sb.Append(" AND T.SLOC_NO = :SLOCNO");
                sb.Append(" ORDER BY T.OBJID DESC");
                var param = new DynamicParameters();
                param.Add("SLOCNO", locNo);
                return Db.Connection.QueryTable(sb.ToString(), param);
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetTaskCmd({locNo})获取指令信息失败:{ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// 获取任务objid
        /// </summary>
        public int GetObjidForRequestTask()
        {
            try
            {
                return Db.Connection.ExecuteScalar<int>("SELECT SEQ_TPROC_0100_TASK_REQUEST.NEXTVAL FROM DUAL");
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetObjidForRequestTask()获取请求生成任务参数表主键ID失败:{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 请求生成任务
        /// </summary>
        public Loc RequestTask(Loc loc)
        {
            var result = loc;
            try
            {
                var dt = Db.Connection.QueryTable($"SELECT * FROM TPROC_0100_TASK_REQUEST T WHERE T.OBJID = {loc.RequestTaskObjid}");
                var sb = new StringBuilder();
                if (dt == null || dt.Rows.Count == 0)
                {
                    sb.Append(" INSERT INTO TPROC_0100_TASK_REQUEST");
                    sb.Append(" (OBJID, ORDER_TYPE_NO, SLOC_NO, PALLET_NO)");
                    sb.Append(" VALUES ");
                    sb.Append(" (:OBJID, :ORDERTYPENO ,:SLOCNO, :PALLETNO)");
                }
                else
                {
                    sb.Append(" UPDATE TPROC_0100_TASK_REQUEST");
                    sb.Append(" SET ORDER_TYPE_NO = :ORDERTYPENO, SLOC_NO, PALLET_NO)");
                    sb.Append(" , SLOC_NO = :SLOCNO");
                    sb.Append(" , PALLET_NO = :PALLETNO)");
                    sb.Append(" WHERE OBJID = :OBJID");
                }
                var param = new DynamicParameters();
                param.Add("OBJID", loc.RequestTaskObjid);
                param.Add("SLOCNO", loc.LocNo);
                param.Add("PALLETNO", loc.plcStatus.PalletNo);
                param.Add("ORDERTYPENO", "100064");
                Db.Connection.Execute(sb.ToString(), param);

                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", loc.RequestTaskObjid);
                dp.Add("O_TASK_NO", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0100_TASK_REQUEST", param: dp, commandType: CommandType.StoredProcedure);

                result.TaskNo = dp.Get<int>("O_TASK_NO");
                result.FaultNo = dp.Get<int>("O_ERR_CODE");
                result.FaultDesc = dp.Get<string>("O_ERR_DESC");
            }
            catch (Exception ex)
            {
                log.Error($"[异常]站台[{loc.LocPlcNo}]执行RequestTask(Loc loc)请求生成任务失败:{ex.ToString()}");
                result.TaskNo = -1;
                result.FaultNo = 1000;
                result.FaultDesc = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取指令objid
        /// </summary>
        /// <returns></returns>
        public int GetObjidForRequestCmd()
        {
            try
            {
                return Db.Connection.ExecuteScalar<int>("SELECT SEQ_TPROC_0200_CMD_REQUEST.NEXTVAL FROM DUAL"); ;
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetObjidForRequestCmd()获取请求生成任务参数表主键ID失败:{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 请求生成指令
        /// </summary>
        public Loc RequestCmd(Loc loc)
        {
            var result = loc;
            try
            {
                //获取是否已经插入数据
                var dt = Db.Connection.QueryTable($"SELECT * FROM TPROC_0200_CMD_REQUEST T WHERE T.OBJID = {loc.RequestCmdObjid}");
                var sb = new StringBuilder();
                if (dt == null || dt.Rows.Count == 0)
                {
                    sb.Append(" INSERT INTO TPROC_0200_CMD_REQUEST");
                    sb.Append(" (OBJID, TASK_NO, CURR_LOC_NO)");
                    sb.Append(" VALUES");
                    sb.Append(" (:OBJID, :TASKNO, :LOCNO)");
                }
                else
                {
                    sb.Append(" UPDATE TPROC_0200_CMD_REQUEST");
                    sb.Append(" SET TASK_NO = :TASKNO,");
                    sb.Append(" , CURR_LOC_NO = :LOCNO");
                    sb.Append(" WHERE OBJID = :OBJID");
                }
                var param = new DynamicParameters();
                param.Add("OBJID", loc.RequestCmdObjid);
                param.Add("TASKNO", loc.TaskNo);
                param.Add("LOCNO", loc.LocNo);
                Db.Connection.Execute(sb.ToString(), param);
                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", loc.RequestCmdObjid);
                dp.Add("O_CMD_OBJID", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0200_CMD_REQUEST", param: dp, commandType: CommandType.StoredProcedure);

                result.CmdId = dp.Get<int>("O_CMD_OBJID");
                result.FaultNo = dp.Get<int>("O_ERR_CODE");
                result.FaultDesc = dp.Get<string>("O_ERR_DESC");
            }
            catch (Exception ex)
            {
                log.Error($"[异常]站台[{loc.LocPlcNo}]执行RequestCmd(Loc loc)请求生成指令失败:{ex.ToString()}");
                result.CmdId = -1;
                result.FaultNo = 1000;
                result.FaultDesc = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改指令步骤
        /// </summary>
        public bool UpdateCmdStep(int cmdId, string cmdStep)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" UPDATE WBS_TASK_CMD SET ");
                sb.Append(" CMD_STEP = :CMD_STEP,");
                sb.Append(" EXCUTE_DATE = SYSDATE");
                sb.Append(" WHERE OBJID = :OBJID");
                var param = new DynamicParameters();
                param.Add("OBJID", cmdId);
                param.Add("CMD_STEP", cmdStep);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行UpdateCmdStep({cmdId},{cmdStep})修改指令步骤失败:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 获取指令结束参数表主键ID
        /// </summary>
        public int GetObjidForCmdFinish()
        {
            try
            {
                return Db.Connection.ExecuteScalar<int>("SELECT SEQ_TPROC_0300_CMD_FINISH.NEXTVAL FROM DUAL");
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行GetObjidForCmdFinish()获取指令结束参数表主键ID异常:{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 请求结束指令指令结束
        /// </summary>
        public Loc RequestFinishTaskCmd(Loc loc, int finishState)
        {
            var result = loc;
            try
            {
                var dt = Db.Connection.QueryTable($"SELECT * FROM TPROC_0300_CMD_FINISH T WHERE T.OBJID = {loc.RequestFinishObjid}");
                var sb = new StringBuilder();
                if (dt == null || dt.Rows.Count == 0)
                {
                    //获取指令号
                    var cmdId = Db.Connection.ExecuteScalar<int>($"SELECT NVL(MIN(T.OBJID),0) FROM WBS_TASK_CMD T WHERE T.TASK_NO = {loc.plcStatus.TaskNo}");
                    if (cmdId <= 0)
                    {
                        return result;
                    }
                    //插入参数表请求
                    sb.Append(" INSERT INTO TPROC_0300_CMD_FINISH");
                    sb.Append(" (OBJID,CMD_OBJID,CURR_LOC_NO,FINISH_STATUS)");
                    sb.Append(" VALUES");
                    sb.Append(" (:OBJID,:CMD_OBJID,:CURR_LOC_NO,:FINISH_STATUS)");
                    var param = new DynamicParameters();
                    param.Add("OBJID", loc.RequestFinishObjid);
                    param.Add("CMD_OBJID", cmdId);
                    param.Add("CURR_LOC_NO", loc.LocNo);
                    param.Add("FINISH_STATUS", finishState);
                    Db.Connection.Execute(sb.ToString(), param);
                }
                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", loc.RequestFinishObjid);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0300_CMD_FINISH", param: dp, commandType: CommandType.StoredProcedure);

                result.FaultNo = dp.Get<int>("O_ERR_CODE");
                result.FaultDesc = dp.Get<string>("O_ERR_DESC");
            }
            catch (Exception ex)
            {
                log.Error($"[异常]站台[{loc.LocPlcNo}]执行RequestFinishTaskCmd(Loc loc)请求结束指令失败:{ex.ToString()}");
                result.FaultNo = 1000;
                result.FaultDesc = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 保存数据至参数表
        /// </summary>
        public bool InsertFinishData(int ObjId, int cmdId, string curLoc, int finishStatus)
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
                    param.Add("FINISH_STATUS", finishStatus);
                    Db.Connection.Execute(sb.ToString(), param);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"执行InsertFinishData({ObjId},{cmdId},{curLoc},{finishStatus})保存指令结束请求参数表{ObjId}失败:{ex.ToString()}");
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
                param.Add("O_ERR_DESC", null, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
                errMsg = param.Get<string>("O_ERR_DESC");
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行ExecProcCmdFinish({objId},ref string errMsg)请求结束指令失败:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 任务删除
        /// </summary>
        public bool DeleteTaskCmd(int taskNo, ref string errMsg)
        {
            try
            {
                var procName = "PROC_WCS_DELETE_CMD";
                var param = new DynamicParameters();
                param.Add("I_TASK_NO", taskNo);
                Db.Connection.Execute(procName, param, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 更新站台状态
        /// </summary>
        public bool RecordPlcInfo(Loc loc)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("UPDATE dbo.PEM_LOC_STATUS");
                sb.Append(" SET TASK_NO = :TASKNO,PALLET_NO = :PALLETNO,SLOC_PLC_NO = :SLOCPLCNO");
                sb.Append(" ,ELOC_PLC_NO = :ELOCPLCNO,AUTO_STATUS = :STATUSAUTO,FAULT_STATUS = :STATUSFAULT");
                sb.Append(" ,LOADED_STATUS = :STATUSLOADING,REQUEST_STATUS = :STATUSREQUEST,FREE_FLAG = :STATUSFREE");
                sb.Append(" ,TOLOAD_STATUS = :STATUSTOLOAD,SCAN_RFID_NO = :SCANRFIDNO,UPDATE_DATE = SYSDATE");
                sb.Append(" WHERE LOC_NO = :LOCNO");
                var param = new DynamicParameters();
                param.Add("TASKNO", loc.plcStatus.TaskNo);
                param.Add("PALLETNO", loc.plcStatus.PalletNo);
                param.Add("SLOCPLCNO", loc.plcStatus.Sloc);
                param.Add("ELOCPLCNO", loc.plcStatus.Eloc);
                param.Add("STATUSAUTO", loc.plcStatus.StatusAuto);
                param.Add("STATUSFAULT", loc.plcStatus.StatusFault);
                param.Add("STATUSLOADING", loc.plcStatus.StatusLoading);
                param.Add("STATUSREQUEST", loc.plcStatus.StatusRequest);
                param.Add("STATUSFREE", loc.plcStatus.StatusFree);
                param.Add("STATUSTOLOAD", loc.plcStatus.StatusToLoad);
                param.Add("SCANRFIDNO", loc.plcStatus.PalletNo);
                param.Add("LOCNO", loc.LocNo);
                Db.Connection.Execute(sb.ToString(), param);
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"[异常]执行RecordPlcInfo()更新站台状态失败:{ex.ToString()}");
                return false;
            }
        }
    }
}
