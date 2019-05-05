using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MSTL.LogAgent;
using MSTL.DbClient;
using Dapper;
using DapperExtensions;
using IECSC.Trans.Common;
using IECSC.Trans.Modul;
using IECSC.Trans.Entity;
using System.Text;

namespace IECSC.Trans.DB
{
    public class DbAction
    {
        #region 系统日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }

        #endregion

        private string connectionString = McConfig.Instance.connectionString;
        private IDatabase Db = null;

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
            string errMsg = string.Empty;
            this.Db = DbHelper.GetDb(connectionString, DbHelper.DataBaseType.Oracle, ref errMsg);
        }
        #endregion


        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public bool GetDbTime()
        {
            try
            {
                var dTable = Db.Connection.QueryTable("select sysdate from dual");
                if (dTable.Rows.Count == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("获取系统时间出错:", ex);
                return false;
            }
        }

        /// <summary>
        /// 加载OPC信息
        /// </summary>
        /// <param name="areaNo"></param>
        /// <param name="locType"></param>
        /// <returns></returns>
        public DataTable GetLocOPC(string appCode)
        {
            try
            {
                var para = new DynamicParameters();
                para.Add("APPCODE", appCode);
                var sql = new StringBuilder();
                sql.Append(" SELECT");
                sql.Append(" POL.TAGCHANNELPREFIX||POL.TAGGROUP||POL.TAGGROUPPREFIX||POLI.TAGPREFIX||POLI.TAGNAME TAGLONGNAME,");
                sql.Append(" PLB.LOC_NO, POL.LOCPLCNO, POLI.BUSINESSIDENTITY");
                sql.Append(" FROM PSB_LOC_BLL PLB");
                sql.Append(" JOIN PSB_OPC_LOC POL ON PLB.LOC_NO = POL.LOCNO");
                sql.Append(" JOIN PSB_LOC T ON T.LOC_NO = POL.LOCNO");
                sql.Append(" LEFT JOIN PSB_OPC_LOC_ITEM POLI ON POLI.ISENABLE = 1");
                sql.Append(" WHERE PLB.ISUSED = 1");
                sql.Append(" AND POL.ISENABLE = 1");
                sql.Append(" AND PLB.PROGRAM_NAME = :APPCODE");
                sql.Append(" ORDER BY PLB.LOC_NO");
                var dtable = Db.Connection.QueryTable(sql.ToString(), para);
                return dtable;
            }
            catch (Exception ex)
            {
                Log.Error("获取OPC项出错（GetLocOPC）:", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取站台基础信息
        /// </summary>
        public DataTable GetLocStatus(string appCode)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("APPCODE", appCode);
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT");
                sql.Append(" L.LOC_NO,L.LOC_PLC_NO,L.LOC_TYPE_NO,LT.LOC_TYPE_NAME,LA.AREA_NO,LA.PAREA_NO,PLB.TYPEDESC");
                sql.Append(" FROM PSB_LOC L JOIN PSB_LOC_BLL PLB ON L.LOC_NO = PLB.LOC_NO");
                sql.Append(" LEFT JOIN PSB_LOC_TYPE LT  ON L.LOC_TYPE_NO = LT.LOC_TYPE_NO");
                sql.Append(" LEFT JOIN PSB_AREA LA ON L.AREA_NO = LA.AREA_NO");
                sql.Append(" WHERE L.LOC_ENABLE = 1 AND PLB.PROGRAM_NAME = :APPCODE AND PLB.ISUSED = 1");
                sql.Append(" ORDER BY L.LOC_TYPE_NO, L.LOC_NO");
                var dtable = Db.Connection.QueryTable(sql.ToString(), para);
                return dtable;
            }
            catch (Exception ex)
            {
                Log.Error("获取站台信息失败（GetLocStatus）:", ex);
                return null;
            }
        }

        /// <summary>
        /// 将叠盘机托盘列表插入到数据库中
        /// </summary>
        public string InserDisPalletList(string locNo, Dictionary<int, string> dicPallet)
        {
            foreach (var pallet in dicPallet)
            {
                if (string.IsNullOrEmpty(pallet.Value))
                {
                    return "托盘号不完整，请确认请求";
                }
            }
            var transaction = Db.Connection.BeginTransaction();
            try
            {
                Db.Connection.Execute("DELETE FROM WBS_PALLET_LIST T WHERE T.LOC_NO = :LOCNO", new { locNo = locNo }, transaction);
                int row = 0;
                foreach (var item in dicPallet)
                {
                    string sql = "INSERT INTO WBS_PALLET_LIST(LOC_NO, RFID_NO, ORDER_ID) VALUES(:LOCNO, :PALLETNO,:ORDERID)";
                    row += Db.Connection.Execute(sql, new { locNo = locNo, palletNo = item.Value, orderId = item.Key }, transaction);

                }
                if (row == 0)
                {
                    transaction.Rollback();
                    return "插入托盘数据出错";
                }
                transaction.Commit();
                return null;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error("更新指令步骤状态出错(UpdateCmdStatus):", ex);
                return "插入托盘数据出错";
            }
        }

        /// <summary>
        /// 获取指令
        /// </summary>
        public WbsTaskCmd GetLocTaskCmd(string taskNo)
        {
            try
            {
                var wbsTaskCmd = Db.GetList<WbsTaskCmd>(new { TaskNo=taskNo,CmdStep="00"}).FirstOrDefault();
                return wbsTaskCmd;
            }
            catch (Exception ex)
            {
                Log.Error(taskNo + ":获取指令出错(GetLocTaskCmd)", ex);
                return null;
            }
        }

        /// <summary>
        /// 更新站台空闲状态
        /// </summary>
        public void UpdateLocFreeFlag(string locNo, int freeFlag)
        {
            try
            {
                var entity = Db.Get<PemLocStatus>(locNo);
                if (entity == null)
                {
                    Log.Error(locNo + "未找到对应的站点");
                    return;
                }
                entity.FreeFlag = freeFlag;
                Db.Update(entity);
            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":更新站台是否空闲(UpdatePdOutLocFreeFlag)", ex);
            }
        }

        /// <summary>
        /// 更新指令步骤状态
        /// </summary>
        public bool UpdateCmdStatus(string taskNo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE WBS_TASK_CMD T");
                sql.Append(" SET T.CMD_STEP = '02',");
                sql.Append(" T.EXCUTE_DATE = SYSDATE");
                sql.Append(" WHERE T.TASK_NO = :TASK_NO");
                var para = new DynamicParameters();
                para.Add("TASK_NO", taskNo);
                Db.Connection.Execute(sql.ToString(), para);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("更新指令(" + taskNo + "）步骤状态出错(UpdateCmdStatus):", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取当前站台未下发的任务
        /// </summary>
        public WbsTask GetLocTaskNotDownload(string locNo, string palletNo)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("SLOCNO", locNo);
                para.Add("PALLETNO", palletNo);

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM WBS_TASK T");
                sql.Append(" WHERE T.SLOC_NO =:SLOCNO AND T.PALLET_NO =:PALLETNO ");
                sql.Append(" AND(T.TASK_STEP = '0000' OR T.TASK_STEP = '2000')");
                var wbstTaskTable = Db.Connection.QueryTable(sql.ToString(), para);
                if (wbstTaskTable == null || wbstTaskTable.Rows.Count != 1)
                {
                    return null;
                }
                var wbsTask = new WbsTask();
                wbsTask.TaskNo =Convert.ToInt32(wbstTaskTable.Rows[0]["TASK_NO"].ToString()??"0");
                wbsTask.SlocNo = locNo;
                wbsTask.ElocNo = wbstTaskTable.Rows[0]["ELOC_NO"].ToString();
                wbsTask.PalletNo = palletNo;
                wbsTask.TaskStep = wbstTaskTable.Rows[0]["TASK_STEP"].ToString();
                return wbsTask;
            }
            catch (Exception ex)
            {
                Log.Error("获取当前站台未下发的任务出错(GetLocTaskNotDownload):", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取任务展示
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransferTask> GetTransforTask()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT WT.TASK_GUID, WT.TASK_NO, WT.EMERGENCY, WC.PALLET_NO, WC.CREATION_DATE,");
                sql.Append(" WT.TASK_STEP, WC.CMD_STEP, WC.CMD_TYPE, WC.SLOC_NO, WC.SLOC_PLC_NO, WC.ELOC_NO, WC.ELOC_PLC_NO,WC.OBJID");
                sql.Append(" FROM WBS_TASK WT LEFT JOIN WBS_TASK_CMD WC ON WT.TASK_GUID = WC.TASK_GUID");
                IEnumerable<TransferTask> tasklist = Db.Connection.Query<TransferTask>(sql.ToString());
                return tasklist;
            }
            catch (Exception ex)
            {
                Log.Error("获取任务失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 手动更新指令步骤
        /// </summary>
        public bool UpdateCmdStatusByManual(string cmdObjid,string step)
        {
            try
            {
                var entity = Db.Get<WbsTaskCmd>(cmdObjid);
                if (entity == null)
                {
                    return false;
                }
                entity.CmdStep = step;
                Db.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("更新任务步骤出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 删除备份任务
        /// </summary>
        internal bool DelTask(int taskNo)
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
                Log.Error("删除任务失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取未下发指令
        /// </summary>
        public TransferCommand GetCmd(string locNo)
        {
            try
            {
                string sql = $"SELECT * FROM WBS_TASK_CMD T WHERE T.SLOC_NO = '{locNo}' AND T.CMD_STEP = '00'";
                var dtCmd = Db.Connection.QueryTable(sql);
                if (dtCmd == null || dtCmd.Rows.Count != 1)
                {
                    return null;
                }
                TransferCommand taskCmd = new TransferCommand
                {
                    LocNo = locNo,
                    TaskNo = Convert.ToInt64(dtCmd.Rows[0]["TASK_NO"].ToString() ?? "0"),
                    SourceAddress = dtCmd.Rows[0]["SLOC_PLC_NO"].ToString(),
                    TargetAddress = dtCmd.Rows[0]["ELOC_PLC_NO"].ToString(),
                    PalletNo = dtCmd.Rows[0]["PALLET_NO"].ToString()
                };
                return taskCmd;
            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":获取指令出错(GetCmd)", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取未下发指令,无工装号
        /// </summary>
        public TransferCommand GetCmdNoPallet(string locNo)
        {
            try
            {
                string sql = "SELECT * FROM WBS_TASK_CMD T WHERE T.SLOC_NO = LOCNO AND T.CMD_STEP = '00' ORDER BY CREATION_DATE DESC";
                var dtCmd = Db.Connection.QueryTable(sql, new { LocNo = locNo});
                if (dtCmd == null || dtCmd.Rows.Count != 1)
                {
                    return null;
                }
                TransferCommand taskCmd = new TransferCommand
                {
                    LocNo = locNo,
                    TaskNo = Convert.ToInt64(dtCmd.Rows[0]["TASK_NO"].ToString() ?? "0"),
                    PalletNo = dtCmd.Rows[0]["PALLET_NO"].ToString(),
                    SourceAddress = dtCmd.Rows[0]["SLOC_PLC_NO"].ToString(),
                    TargetAddress = dtCmd.Rows[0]["ELOC_PLC_NO"].ToString()
                };
                return taskCmd;
            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":获取指令出错(GetCmd)", ex);
                return null;
            }
        }

        /// <summary>
        /// 更新站台空闲状态
        /// </summary>
        public void UpdateLocFreeAndLoad(string locNo, int freeFlag, int loadFlag)
        {
            try
            {
                var entity = Db.Get<PemLocStatus>(locNo);
                if (entity == null)
                {
                    Log.Error(locNo + "未找到对应的站点");
                    return;
                }
                entity.LoadedStatus = loadFlag;
                entity.FreeFlag = freeFlag;
                Db.Update(entity);
            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":更新站台是否空闲(UpdatePdOutLocFreeFlag)", ex);
            }
        }

        /// <summary>
        /// 获取任务objid
        /// </summary>
        /// <returns></returns>
        public long GetObjidForTask()
        {
            try
            {
                long objid = Db.Connection.ExecuteScalar<long>("SELECT SEQ_TPROC_0100_TASK_REQUEST.NEXTVAL FROM DUAL");
                return objid;
            }
            catch (Exception ex)
            {
                Log.Error("获取指令objid出错（GetObjidForTask）", ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取指令objid
        /// </summary>
        /// <returns></returns>
        public long GetObjidForCmd()
        {
            try
            {
                long objid = Db.Connection.ExecuteScalar<long>("SELECT SEQ_TPROC_0200_CMD_REQUEST.NEXTVAL FROM DUAL");
                return objid;
            }
            catch (Exception ex)
            {
                Log.Error("获取指令objid出错（GetObjidForCmd）", ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取指令结束objid
        /// </summary>
        /// <returns></returns>
        public long GetObjidForFinishCmd()
        {
            try
            {
                long objid = Db.Connection.ExecuteScalar<long>("SELECT SEQ_TPROC_0300_CMD_FINISH.NEXTVAL FROM DUAL");
                return objid;
            }
            catch (Exception ex)
            {
                Log.Error("获取指令结束objid出错（GetObjidForCmd）", ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        public int RequestTaskCmdNoPallet(long objid, string locNo, string orderTypeNo,string palletNo, ref string errMsg)
        {
            try
            {
                //获取是否已经插入数据
                var sql = new StringBuilder();
                sql.Append($"SELECT * FROM TPROC_0100_TASK_REQUEST T WHERE T.OBJID = {objid}");
                var dtList = Db.Connection.QueryTable(sql.ToString());
                if (dtList.Rows.Count == 0 || dtList == null)
                {
                    //插入参数表
                    var sqlForInsert = new StringBuilder();
                    sqlForInsert.Append(" INSERT INTO TPROC_0100_TASK_REQUEST");
                    sqlForInsert.Append(" (OBJID, ORDER_TYPE_NO, SLOC_NO, PALLET_NO)");
                    sqlForInsert.Append(" VALUES (:OBJID,:ORDERTYPENO,:SLOCNO,:PALLETNO)");
                    var para = new DynamicParameters();
                    para.Add("OBJID", objid);
                    para.Add("SLOCNO", locNo);
                    para.Add("PALLETNO", palletNo);
                    para.Add("PRODUCT_WEIGHT", palletNo);
                    Db.Connection.Execute(sqlForInsert.ToString(), para);
                }
                else
                {
                    if(dtList.Rows[0]["PROC_STATUS"].ToString().Equals("2") && dtList.Rows[0]["ERR_CODE"].ToString().Equals("0"))
                    {
                        return 0;
                    }
                }
                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", objid);
                dp.Add("O_TASK_NO", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size:80);
                Db.Connection.Execute("PROC_0100_TASK_REQUEST", param: dp, commandType: CommandType.StoredProcedure);
                var taskNo = dp.Get<int>("O_TASK_NO");
                errMsg = dp.Get<string>("O_ERR_DESC");
                return taskNo;
            }
            catch (Exception ex)
            {
                errMsg = "站台请求任务出错(RequestTask)" + ex.ToString();
                Log.Error("站台请求任务出错(RequestTask):", ex);
                return -1;
            }
        }

        /// <summary>
        /// 只获取任务(无托盘号)
        /// </summary>
        public int RequestTaskCmdNoPallet(long objid, string locNo, string orderTypeNo, ref string errMsg)
        {
            try
            {
                //获取是否已经插入数据
                var sql = new StringBuilder();
                sql.Append($"SELECT * FROM TPROC_0100_TASK_REQUEST T WHERE T.OBJID = {objid}");
                var dtList = Db.Connection.QueryTable(sql.ToString());
                if (dtList.Rows.Count == 0 || dtList == null)
                {
                    //插入参数表
                    var sqlForInsert = new StringBuilder();
                    sqlForInsert.Append(" INSERT INTO TPROC_0100_TASK_REQUEST");
                    sqlForInsert.Append(" (OBJID, ORDER_TYPE_NO, SLOC_NO, PALLET_NO)");
                    sqlForInsert.Append(" VALUES (:OBJID,:ORDERTYPENO,:SLOCNO, NULL)");
                    var para = new DynamicParameters();
                    para.Add("ORDERTYPENO", orderTypeNo);
                    para.Add("SLOCNO", locNo);
                    para.Add("OBJID", objid);
                    Db.Connection.Execute(sqlForInsert.ToString(), para);
                }
                else
                {
                    if (dtList.Rows[0]["PROC_STATUS"].ToString().Equals("2") && dtList.Rows[0]["ERR_CODE"].ToString().Equals("0"))
                    {
                        return 0;
                    }
                }
                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", objid);
                dp.Add("O_TASK_NO", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0100_TASK_REQUEST", param: dp, commandType: CommandType.StoredProcedure);
                var taskNo = dp.Get<int>("O_TASK_NO");
                errMsg = dp.Get<string>("O_ERR_DESC");
                return taskNo;
            }
            catch (Exception ex)
            {
                errMsg = "站台请求任务出错(RequestTask)" + ex.ToString();
                Log.Error("站台请求任务出错(RequestTask):", ex);
                return 0;
            }
        }

        /// <summary>
        /// 请求生成指令
        /// </summary>
        public int RequestCmdNoPallet(long objid, string locNo, int taskNo, ref string errMsg)
        {
            try
            {
                //获取是否已经插入数据
                var sql = new StringBuilder();
                sql.Append($"SELECT * FROM TPROC_0200_CMD_REQUEST T WHERE T.OBJID = {objid}");
                var dtList = Db.Connection.QueryTable(sql.ToString());
                if (dtList.Rows.Count == 0 || dtList == null)
                {
                    //插入参数表
                    DynamicParameters para = new DynamicParameters();
                    para.Add("OBJID", objid);
                    para.Add("TASKNO", taskNo);
                    para.Add("LOCNO", locNo);
                    StringBuilder sqlForInsert = new StringBuilder();
                    sqlForInsert.Append(" INSERT INTO TPROC_0200_CMD_REQUEST");
                    sqlForInsert.Append(" (OBJID,TASK_NO,CURR_LOC_NO) VALUES");
                    sqlForInsert.Append(" (:OBJID, :TASKNO, :LOCNO)");
                    Db.Connection.Execute(sqlForInsert.ToString(), para);
                }
                else
                {
                    if (dtList.Rows[0]["PROC_STATUS"].ToString().Equals("2") && dtList.Rows[0]["ERR_CODE"].ToString().Equals("0"))
                    {
                        return 0;
                    }
                }
                //执行存储过程
                var dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", objid);
                dp.Add("O_CMD_OBJID", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0200_CMD_REQUEST", param: dp, commandType: CommandType.StoredProcedure);
                var cmdObjid = dp.Get<int>("O_CMD_OBJID");
                errMsg = dp.Get<string>("O_ERR_DESC");
                return cmdObjid;
            }
            catch (Exception ex)
            {
                errMsg = "站台请求指令出错(RequestCmd)";
                Log.Error("站台请求指令出错(RequestCmd):", ex);
                return 0;
            }
        }

        /// <summary>
        /// 结束输送线指令
        /// </summary>
        public string FinishCmd(long objid, string locNo, long taskNo)
        {
            try
            {
                //获取指令OBJID
                var cmdId = Db.Connection.ExecuteScalar<long>($"SELECT NVL(MIN(T.OBJID),0) FROM WBS_TASK_CMD T WHERE T.TASK_NO = {taskNo}");
                if (cmdId <= 0)
                {
                    return null;
                }
                var sql = new StringBuilder();
                sql.Append($"SELECT * FROM TPROC_0300_CMD_FINISH T WHERE T.OBJID = {objid}");
                var dt = Db.Connection.QueryTable(sql.ToString());
                if (dt == null || dt.Rows.Count ==0)
                {
                    //获取是否已经请求结束
                    var sb = new StringBuilder();
                    sb.Append(" INSERT INTO TPROC_0300_CMD_FINISH");
                    sb.Append(" (OBJID,CMD_OBJID,CURR_LOC_NO,FINISH_STATUS)");
                    sb.Append(" VALUES");
                    sb.Append(" (:OBJID,:CMD_OBJID,:CURR_LOC_NO,:FINISH_STATUS)");
                    var param = new DynamicParameters();
                    param.Add("OBJID", objid);
                    param.Add("CMD_OBJID", cmdId);
                    param.Add("CURR_LOC_NO", locNo);
                    param.Add("FINISH_STATUS", 101);
                    Db.Connection.Execute(sb.ToString(), param);
                }
                //执行存储过程
                DynamicParameters dp = new DynamicParameters();
                dp.Add("I_PARAM_OBJID", objid);
                dp.Add("O_ERR_CODE", 0, DbType.Int32, ParameterDirection.Output);
                dp.Add("O_ERR_DESC", 0, DbType.String, ParameterDirection.Output, size: 80);
                Db.Connection.Execute("PROC_0300_CMD_FINISH", param: dp, commandType: CommandType.StoredProcedure);
                return dp.Get<string>("O_ERR_DESC") ?? string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":结束输送线指令[" + taskNo + "]出错(FinishTransTask):", ex);
                return locNo + ":结束输送线指令[" + taskNo + "]出错(FinishTransTask);";
            }
        }

        /// <summary>
        /// 浸漆机获取未下发的Agv指令
        /// </summary>
        internal bool GetAgvCmd(string locNo)
        {
            try
            {
                string sql = $"SELECT * FROM WBS_TASK_CMD_AGV T WHERE T.SLOC_NO = '{locNo}' AND T.CMD_STEP = '00'";
                var dtCmd = Db.Connection.QueryTable(sql);
                if (dtCmd == null || dtCmd.Rows.Count != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(locNo + ":获取指令出错(GetCmd)", ex);
                return false;
            }
        }

        /// <summary>
        /// 生成浸漆机至换盘区AGV指令
        /// </summary>
        internal bool ExecuteProcedure(string locNo, string palletNo)
        {
            try
            {
                long objid = Db.Connection.ExecuteScalar<long>("select seq_tproc_0020_group_request.nextval from dual");
                DynamicParameters para = new DynamicParameters();
                para.Add("objid", objid);
                para.Add("palletNo", palletNo);
                para.Add("palletNo", locNo);
                para.Add("slocNo", locNo);
                string strSql = "insert into tproc_0020_group_request(objid,pallet_no,sloc_no)values(:objid,:palletNo,:slocNo)";
                if (Db.Connection.Execute(strSql, para) > 0)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("I_PARAM_OBJID", objid);
                    parameters.Add("O_ERR_DESC", null, DbType.String, ParameterDirection.Output, size: 50);
                    parameters.Add("O_EQUIP_NO", null, DbType.String, ParameterDirection.Output, size: 50);
                    Db.Connection.Execute("PROC_0020_GROUP_REQUEST", para, commandType: CommandType.StoredProcedure);
                    var result = parameters.Get<string>("O_ERR_DESC");
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
