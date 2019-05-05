using System;
using System.Collections.Generic;
using MSTL.LogAgent;

namespace IECSC.CRN.SINGLE
{
    public class BizHandle
    {
        #region 变量
        /// <summary>
        /// 指令结束请求ID
        /// </summary>
        public int FINISHID = 0;
        /// <summary>
        /// 报警记录ID
        /// </summary>
        public int FAULTID = 0;
        /// <summary>
        /// 设备报警码
        /// </summary>
        public int FAULTNO = 0;
        /// <summary>
        /// 异常反馈
        /// </summary>
        private string errMsg = string.Empty;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string errLog = string.Empty;
        /// <summary>
        /// 任务信息
        /// </summary>
        public TaskCmd taskCmd = null;
        /// <summary>
        /// PLC读取状态
        /// </summary>
        public PlcStatus plcStatus = null;
        /// <summary>
        /// 报警信息索引
        /// </summary>
        public IndexerCrnFault crnErr = null;
        /// <summary>
        /// OPC读取项
        /// </summary>
        public List<OpcItem> readItems = null;
        /// <summary>
        /// OPC写入项
        /// </summary>
        public List<OpcItem> writeItems = null;
        /// <summary>
        /// 设备执行状态 
        /// </summary>
        public Enums.EquipStatus equipStatus = Enums.EquipStatus.None;
        #endregion

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
            taskCmd = new TaskCmd();
            plcStatus = new PlcStatus();
            crnErr = new IndexerCrnFault();
            readItems = new List<OpcItem>();
            writeItems = new List<OpcItem>();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public bool BizInit()
        {
            try
            {
                if(!DbAction.Instance.GetDbTime())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.dbConn));
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.dbConn));
                if (!DbAction.Instance.LoadCrnFault())
                {
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]报警编码绑定成功"));
                if (!DbAction.Instance.LoadOpcItems())
                {
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[基础配置]数据库配置表读取成功"));
                if (!OpcAction.Instance.OpcInit())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.plcConn));
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.plcConn));
                return true;
            }
            catch(Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"系统初始化异常:{ex.ToString()}"));
                log.Error($"系统初始化异常:{ex.ToString()}");
                return false;
            }
        }

        public bool BizListen()
        {
            try
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC数据监控", InfoType.opcDgv));
                if (plcStatus.OperateMode == 0)
                {
                    return false;
                }
                if (plcStatus.HeartBeat <= 0)
                {
                    return false;
                }
                if (!OpcAction.Instance.WriteHeartBeat(ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"写入心跳信号失败:{errMsg}", InfoType.errLog));
                    errLog = $"写入心跳信号失败:{errMsg}";
                    return false;
                }
                //业务处理
                ExecuteReceiveData();
                return true;
            }
            catch(Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"业务处理异常:{ex.ToString()}", InfoType.errLog));
                errLog = $"业务处理异常:{ex.ToString()}";
                log.Error($"业务处理异常:{ex.ToString()}");
                return false;
            }
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        private void ExecuteReceiveData()
        {
            if(equipStatus == Enums.EquipStatus.None)
            {
                EquipStatusNone();
            }
            if (equipStatus == Enums.EquipStatus.Down)
            {
                EquipStatusDown();
            }
            if (equipStatus == Enums.EquipStatus.Ready)
            {
                EquipStatusReady();
            }
            if (equipStatus == Enums.EquipStatus.Exec)
            {
                EquipStatusExec();
            }
            if (equipStatus == Enums.EquipStatus.Error)
            {
                EquipStatusError();
            }
            if (equipStatus == Enums.EquipStatus.ErrorDeal)
            {
                EquipStatusErrorDeal();
            }
            if (equipStatus == Enums.EquipStatus.End)
            {
                EquipStatusEnd();
            }
            if (equipStatus == Enums.EquipStatus.Reset)
            {
                EquipStatusReset();
            }
        }

        /// <summary>
        /// 指令查询
        /// </summary>
        private void EquipStatusNone()
        {
            try
            {
                if (plcStatus.MissionState != 0)
                {
                    ResetEquipStatus();
                    return;
                }
                if(plcStatus.FaultNo > 0 && plcStatus.FaultNo != 68)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[NONE]设备故障:{crnErr[plcStatus.FaultNo.ToString()]}", InfoType.errLog));
                    errLog = $"[NONE]设备故障:{crnErr[plcStatus.FaultNo.ToString()]}";
                    return;
                }
                if(!DbAction.Instance.LoadCmdToCrnFork())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[NONE]获取指令出错", InfoType.errLog));
                    errLog = $"[NONE]获取指令出错";
                    return;
                }
                equipStatus = Enums.EquipStatus.Down;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[NONE]获取到指令{taskCmd.ObjId}", InfoType.cmdDgv));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[NONE]查找指令信息异常", InfoType.errLog));
                errLog = "[NONE]查找指令信息异常";
                log.Error("[NONE]查找指令信息异常:" + ex.ToString());
            }
        }

        /// <summary>
        /// 指令下传
        /// </summary>
        private void EquipStatusDown()
        {
            try
            {
                if(!OpcAction.Instance.DownCmdToOpc(ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[DOWN]指令[{taskCmd.ObjId}]写入PLC失败", InfoType.errLog));
                    errLog = $"[DOWN]指令[{taskCmd.ObjId}]写入PLC失败";
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[DOWN]指令{taskCmd.ObjId}写入PLC成功"));
                if (!DbAction.Instance.UpdateCmdStep("02"))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[DOWN]跟新指令[{taskCmd.ObjId}]步骤[02]失败", InfoType.errLog));
                    errLog = $"[DOWN]跟新指令[{taskCmd.ObjId}]步骤[02]失败";
                    return;
                }
                equipStatus = Enums.EquipStatus.Ready;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[DOWN]修改指令步骤为02成功,等待PLC反馈执行...", InfoType.cmdDgv));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[DOWN]写入指令至PLC异常", InfoType.errLog));
                errLog = "[DOWN]写入指令至PLC异常";
                log.Error($"[DOWN]写入指令至PLC异常:{ex.ToString()}");
            }
        }

        /// <summary>
        /// 等待下位机执行任务
        /// </summary>
        private void EquipStatusReady()
        {
            try
            {
                if (plcStatus.MissionState != 1)
                {
                    return;
                }
                equipStatus = Enums.EquipStatus.Exec;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[READY]堆垛机开始执行指令,等待PLC反馈完成..."));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[READY]监控任务状态[1]异常", InfoType.errLog));
                errLog = "[READY]监控任务状态[1]异常";
                log.Error($"[READY]监控任务状态[1]异常:{ex.ToString()}");
            }
        }

        /// <summary>
        /// 指令执行 
        /// </summary>
        private void EquipStatusExec()
        {
            try
            {
                if (plcStatus.FaultNo > 0)
                {
                    equipStatus = Enums.EquipStatus.Error;
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[EXEC]设备故障:{crnErr[plcStatus.FaultNo.ToString()]}"));
                    return;
                }
                if (plcStatus.MissionState != 2)
                {
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[EXEC]PLC反馈任务完成,等待WCS结束指令..."));
                if (FINISHID == 0)
                {
                    FINISHID = DbAction.Instance.GetObjidForCmdFinish();
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[EXEC]WCS指令结束参数表ID:{FINISHID}"));
                if (!DbAction.Instance.FinishCrnCmd(FINISHID, taskCmd.ObjId, taskCmd.ElocNo, 1, ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[EXEC]结束指令[{taskCmd.ObjId}]异常:{errMsg}", InfoType.errLog));
                    errLog = $"[EXEC]结束指令[{taskCmd.ObjId}]异常:{errMsg}";
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[EXEC]结束指令{taskCmd.ObjId}成功", InfoType.cmdDgv));
                if (!OpcAction.Instance.WriteSeqNo(2, ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[END]任务处理完成信号[2]写入PLC失败", InfoType.errLog));
                    errLog = "[END]任务处理完成信号[2]写入PLC失败";
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[EXEC]任务处理完成信号[2]写入PLC成功,等待WCS复位..."));
                equipStatus = Enums.EquipStatus.End;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[EXEC]等待PLC反馈完成信号[2]异常", InfoType.errLog));
                errLog = "[EXEC]等待PLC反馈完成信号[2]异常";
                log.Error($"[EXEC]等待PLC反馈完成信号[2]异常:{ex.ToString()}");
            }
        }
        /// <summary>
        /// 指令结束
        /// </summary>
        public void EquipStatusEnd()
        {
            try
            {
                if (plcStatus.MissionState != 2)
                {
                    return;
                }
                if (!OpcAction.Instance.WriteSeqNo(0, ref errMsg))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[END]写入PLC任务状态[0]失败...", InfoType.errLog));
                    errLog = "[END]写入PLC任务状态[0]失败...";
                    return;
                }
                equipStatus = Enums.EquipStatus.Reset;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[END]写入PLC任务状态[0]成功,等待WCS复位..."));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[END]写入PLC任务状态[0]异常", InfoType.errLog));
                errLog = "[END]写入PLC任务状态[0]异常";
                log.Error($"[END]写入PLC任务状态[0]异常:{ex.ToString()}");
            }
        }
        /// <summary>
        /// 信号复位
        /// </summary>
        public void EquipStatusReset()
        {
            try
            {
                if (plcStatus.MissionState != 0)
                {
                    return;
                }
                FINISHID = 0;
                errMsg = string.Empty;
                errLog = string.Empty;
                taskCmd = new TaskCmd();
                equipStatus = Enums.EquipStatus.None;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[RESET]结束", InfoType.cmdDgv));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[RESET]复位WCS状态异常", InfoType.errLog));
                errLog = $"[RESET]复位WCS状态异常";
                log.Error($"[RESET]复位WCS状态异常:{ex.ToString()}");
            }
        }
        /// <summary>
        /// 堆垛机叉异常指令处理
        /// </summary>
        public void EquipStatusError()
        {
            try
            {
                if (plcStatus.MissionState == 0 && plcStatus.FaultNo == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERROR]收到PLC复位信号,WCS状态复位,请人工处理指令[{taskCmd.ObjId}]"));
                    equipStatus = Enums.EquipStatus.Reset;
                    return;
                }
                if (plcStatus.MissionState == 2 && plcStatus.FaultNo == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERROR]收到PLC完成信号[2],进入[EXEC]准备完成指令[{taskCmd.ObjId}]"));
                    equipStatus = Enums.EquipStatus.Exec;
                    return;
                }
                // 空出库处理
                if (plcStatus.FaultNo == 53)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[ERROR]检测到PLC空出库报警,等待WCS处理..."));
                    if (DbAction.Instance.UpdateEquipErrStatus(taskCmd.TaskNo, plcStatus.FaultNo, 1))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[ERROR]更新空出库状态成功,等待人工确认..."));
                        equipStatus = Enums.EquipStatus.ErrorDeal;
                    }
                }
                //先入品处理
                else if (plcStatus.FaultNo == 68)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERROR]检测到PLC先入品报警[{ plcStatus.FaultNo}],等待WCS处理..."));
                    if (DbAction.Instance.UpdateEquipErrStatus(taskCmd.TaskNo, plcStatus.FaultNo, 1))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[ERROR]更新先入品状态成功,等待人工确认..."));
                        equipStatus = Enums.EquipStatus.ErrorDeal;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERROR]WCS处理设备报警出错", InfoType.errLog));
                errLog = $"[ERROR]WCS处理设备报警出错";
                log.Error($"[ERROR]WCS处理设备报警出错:{ex.ToString()}");
            }
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        public void EquipStatusErrorDeal()
        {
            try
            {
                if (plcStatus.MissionState == 0 && plcStatus.FaultNo == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERRORDEAL]收到PLC复位信号,WCS状态复位,请人工处理[{taskCmd.ObjId}]"));
                    equipStatus = Enums.EquipStatus.Reset;
                    return;
                }
                if (plcStatus.MissionState == 2 && plcStatus.FaultNo == 0)
                {
                    if (DbAction.Instance.UpdateEquipErrStatus(taskCmd.TaskNo))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[ERRORDEAL]收到PLC完成信号[2],进入[EXEC]准备完成[{taskCmd.ObjId}]"));
                        equipStatus = Enums.EquipStatus.Exec;
                    }
                    return;
                }
                // 空出库处理
                if (plcStatus.FaultNo == 53)
                {
                    if (DbAction.Instance.GetFipFlag() != 2)
                    {
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[空出库]获取到人工确认信号,等待WCS处理..."));
                    if (FINISHID == 0)
                    {
                        FINISHID = DbAction.Instance.GetObjidForCmdFinish();
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[空出库]WCS指令结束参数表ID[{FINISHID}]"));
                    if (!DbAction.Instance.FinishCrnCmd(FINISHID, taskCmd.ObjId, taskCmd.ElocNo, 201, ref errMsg))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[空出库]结束指令[{taskCmd.ObjId}]异常", InfoType.errLog));
                        errLog = $"[空出库]结束指令[{taskCmd.ObjId}]异常";
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[空出库]结束指令[{taskCmd.ObjId}]成功,准备复位PLC状态...", InfoType.cmdDgv));
                    if (!OpcAction.Instance.WriteSeqNo(2, ref errMsg))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[空出库]任务处理完成信号[2]写入PLC失败", InfoType.errLog));
                        errLog = $"[空出库]任务处理完成信号[2]写入PLC失败";
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[空出库]任务处理完成信号[2]写入PLC成功,等待WCS复位..."));
                    equipStatus = Enums.EquipStatus.Reset;
                }
                // 先入品处理
                else if(plcStatus.FaultNo == 68)
                {
                    if (DbAction.Instance.GetFipFlag() != 2)
                    {
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"先入品任务{taskCmd.TaskNo}人工确认完成,等待WCS处理..."));
                    if (DbAction.Instance.ExecProcFirstInProduct(ref errMsg) != 3)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[先入品]先入品指令处理失败", InfoType.errLog));
                        errLog = "[先入品]先入品指令处理失败";
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[先入品]先入品指令处理成功"));
                    if (!OpcAction.Instance.WriteSeqNo(2, ref errMsg))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[先入品]任务处理完成信号[2]写入PLC失败", InfoType.errLog));
                        errLog = "[先入品]任务处理完成信号[2]写入PLC失败";
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[先入品]任务处理完成信号[2]写入PLC成功"));
                    if (DbAction.Instance.UpdateEquipErrStatus(taskCmd.TaskNo))
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[先入品]先入品处理完成,转入[RESERT]复位,重新获取指令..."));
                        equipStatus = Enums.EquipStatus.Reset;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[ErrorDeal]WCS处理设备报警出错", InfoType.errLog));
                errLog = "[ErrorDeal]WCS处理设备报警出错";
                log.Error($"[ErrorDeal]WCS处理设备报警出错:{ex.ToString()}");
            }
        }
        /// <summary>
        /// 修改项：记录步骤，记录到配置文档
        /// 初次启动堆垛机,任务状态不为0
        /// 检测当前叉是否存在任务,若存在任务,先结束再复位
        /// </summary>
        public bool ResetEquipStatus()
        {
            try
            {
                //需记录堆垛机程序状态
                if (plcStatus.MissionState == 0 || plcStatus.MissionState == 3)
                {
                    return false;
                }
                if (!DbAction.Instance.GetCrnForksCmd(plcStatus.CrnNo))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("复位WCS状态异常,请确认设备状态是否正常!", InfoType.errLog));
                    errLog = "复位WCS状态异常,请确认设备状态是否正常!";
                    return false;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("[重启复位]None→Exec"));
                equipStatus = Enums.EquipStatus.Exec;
                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("复位堆垛机状态异常", InfoType.errLog));
                errLog = "复位堆垛机状态异常";
                log.Error("复位堆垛机状态异常:" + ex.ToString());
                return false;
            }
        }
    }
}
