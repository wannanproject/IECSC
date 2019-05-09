using System;

namespace IECSC.TRANS
{
    public class RequestSrmTask : IBiz
    {
        private CommonBiz commonBiz = null;
        
        public RequestSrmTask()
        {
            commonBiz = new CommonBiz();
        }

        public void HandleLoc (string locNo)
        {
            try
            {
                var loc = BizHandle.Instance.locDic[locNo];
                //更新站台状态
                DbAction.Instance.RecordPlcInfo(loc);
                if (loc.plcStatus.StatusAuto <= 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]非自动状态", locNo));
                    return;
                }
                if (loc.plcStatus.StatusFault > 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]站台故障", locNo));
                    return;
                }
                if (loc.plcStatus.StatusLoading != 1)
                {
                    return;
                }
                //判断是否给出“站点有货需取货”信号
                if (loc.plcStatus.StatusToLoad != 1)
                {
                    loc.RequestTaskObjid = 0;
                    loc.RequestCmdObjid = 0;
                    loc.TaskNo = 0;
                    loc.CmdId = 0;
                    loc.bizStatus = BizStatus.None;
                    return;
                }
                if (string.IsNullOrEmpty(loc.plcStatus.PalletNo))
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]未获取到PLC传递工装编号", locNo));
                    return;
                }
                //获取指令
                if (loc.bizStatus == BizStatus.None)
                {
                    //检查是否已生成任务
                    var result = commonBiz.SelectTaskCmd(loc);
                    if (result)
                    {
                        loc.bizStatus = BizStatus.ResetFault;
                    }
                    else
                    {
                        loc.bizStatus = BizStatus.ReqTask;
                    }
                }
                //获取任务
                if(loc.bizStatus == BizStatus.ReqTask)
                {
                    loc = commonBiz.RequstTask(loc);
                    if (loc.TaskNo > 0)
                    {
                        loc.bizStatus = BizStatus.ReqCmd;
                    }
                    else
                    {
                        return;
                    }
                }
                //请求指令
                if (loc.bizStatus == BizStatus.ReqCmd)
                {
                    loc = commonBiz.RequstCmd(loc);
                    if (loc.CmdId > 0)
                    {
                        loc.bizStatus = BizStatus.ResetFault;
                    }
                    else
                    {
                        return;
                    }
                }
                //复位下位机故障码
                if (loc.bizStatus == BizStatus.ResetFault)
                {
                    var result = commonBiz.WriteFaultNo(loc);
                    if(result)
                    {
                        loc.bizStatus = BizStatus.WriteDeal;
                    }
                    else
                    {
                        return;
                    }
                }
                //写入已处理信号
                if (loc.bizStatus == BizStatus.WriteDeal)
                {
                    var result = commonBiz.WriteToLoadDeal(loc);
                    if (result)
                    {
                        loc.plcStatus.StatusRequest = 0;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"站台{locNo}请求处理失败：{ex.Message}", locNo));
            }
        }
    }
}