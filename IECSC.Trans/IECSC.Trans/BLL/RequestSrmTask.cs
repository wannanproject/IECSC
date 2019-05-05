using System;
using IECSC.Trans.DB;
using IECSC.Trans.Entity;
using IECSC.Trans.Common;
using IECSC.Trans.OPC;

namespace IECSC.Trans
{
    public class RequestSrmTask
    {
        #region 单例模式 
        private static RequestSrmTask _instance = null;
        public static RequestSrmTask Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(RequestSrmTask))
                    {
                        if (_instance == null)
                        {
                            _instance = new RequestSrmTask();
                        }
                    }
                }
                return _instance;
            }
        }
        private RequestSrmTask()
        {
        }
        #endregion

        /// <summary>
        /// 原材料胶料库一楼出入库站台
        /// </summary>
        public void HandleLoc (string locNo, OPCWrite opcWrite)
        {
            try
            {
                var locStatus = BizHandle.Instance.locStatuesDic[locNo];
                var msgPre = $"【{locStatus.LocPlcNo}-{locNo}】:";
                //判断是否自动
                if (locStatus.StatusAutomatic == 0)
                {
                    return;
                }
                //更新站台有载、空闲信号
                DbAction.Instance.UpdateLocFreeAndLoad(locNo, locStatus.StatusFreeAndPut, locStatus.StatusLoad);
                //判断是否请求取货
                if (locStatus.StatusBusyAndTake == 0)
                {
                    return;
                }
                SetLocStatus(locNo, msgPre + "收到请求任务信号");
                //检测当前站台是否已存在指令
                var Cmd = DbAction.Instance.GetCmd(locNo);
                if (Cmd == null)
                {
                    if (string.IsNullOrWhiteSpace(locStatus.PalletNo.ToString()))
                    {
                        SetLocStatus(locNo, $"{msgPre}未获取RFID编号!");
                    }
                    //判断是否已请求任务
                    if (locStatus.TaskObjid == 0)
                    {
                        //获取请求任务ID
                        locStatus.TaskObjid = DbAction.Instance.GetObjidForTask();
                    }
                    if (locStatus.TaskObjid <= 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}获取任务请求主键[Objid]出错!", locNo: locNo));
                        SetLocStatus(locNo, $"{msgPre}获取任务请求主键[Objid]出错!");
                        return;
                    }
                    //任务请求
                    var errTaskMsg = string.Empty;
                    var taskNo = DbAction.Instance.RequestTaskCmdNoPallet(locStatus.TaskObjid, locNo, "100064", locStatus.PalletNo.ToString().Trim(), ref errTaskMsg);
                    if (taskNo <= 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}请求任务失败：{errTaskMsg}", locNo: locNo));
                        SetLocStatus(locNo, $"请求任务失败：{errTaskMsg}");
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}任务请求成功，任务号：{taskNo}", locNo: locNo));
                    SetLocStatus(locNo, $"{msgPre}任务请求成功，任务号：{taskNo}");
                    //判断是否已请求指令
                    if (locStatus.CmdObjid == 0)
                    {
                        //获取请求指令ID
                        locStatus.CmdObjid = DbAction.Instance.GetObjidForCmd();
                    }
                    if (locStatus.CmdObjid <= 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}获取指令请求主键[Objid]出错!", locNo: locNo));
                        SetLocStatus(locNo, $"{msgPre}获取指令请求主键[Objid]出错!");
                        return;
                    }
                    //指令请求
                    var errCmdMsg = string.Empty;
                    var cmdObjid = DbAction.Instance.RequestCmdNoPallet(locStatus.CmdObjid, locNo, taskNo, ref errCmdMsg);
                    if (cmdObjid <= 0)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}指令请求报错{errCmdMsg}!", locNo: locNo));
                        SetLocStatus(locNo, $"{msgPre}指令请求报错{errCmdMsg}");
                        return;
                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}指令请求完成，已生成指令!", locNo: locNo));
                    SetLocStatus(locNo, $"{msgPre}指令请求完成，已生成指令!");
                }
                if (!opcWrite.WriteTaskFinsh(locNo))
                {
                    SetLocStatus(locNo, "传递[任务已处理]信号失败！");
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}传递[任务已处理]信号失败!", ShowInfoTypeEnum.logInfo, locNo));
                    return;
                }
                //清空任务记录
                BizHandle.Instance.locStatuesDic[locNo].CmdObjid = 0;
                BizHandle.Instance.locStatuesDic[locNo].TaskObjid = 0;
                BizHandle.Instance.locStatuesDic[locNo].StatusBusyAndTake = 0;
                SetLocStatus(locNo, "传递[任务已处理]信号成功！");
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{msgPre}传递[任务已处理]信号成功!", ShowInfoTypeEnum.logInfo, locNo));
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"站台{locNo}请求取货失败：{ex.ToString()}", locNo: locNo));
                SetLocStatus(locNo, $"站台{locNo}请求取货失败：{ex.ToString()}");
            }
        }

        private static void SetLocStatus(string locNo, string desc, string palletNo = null)
        {
            BizHandle.Instance.locStatuesDic[locNo].BusinessDesc = desc;
            BizHandle.Instance.locStatuesDic[locNo].BusinessHandleTime = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            if (!string.IsNullOrEmpty(palletNo))
            {
                BizHandle.Instance.locStatuesDic[locNo].LastPalletNo = palletNo;
            }
        }
    }
}