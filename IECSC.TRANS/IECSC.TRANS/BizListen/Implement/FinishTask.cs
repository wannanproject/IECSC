using System;

namespace IECSC.TRANS
{
    public class FinishTask : IBiz
    {
        private CommonBiz commonBiz = null;
        
        public FinishTask()
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
                if (loc.plcStatus.StatusToLoad != 1)
                {
                    loc.RequestFinishObjid = 0;
                    loc.bizStatus = BizStatus.None;
                    return;
                }
                //检查下位机传递任务号
                if (loc.plcStatus.TaskNo == 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]PLC传递任务编号错误", locNo));
                    return;
                }
                //结束指令
                if (loc.bizStatus == BizStatus.None)
                {
                    var result = commonBiz.FinishCmd(loc);
                    if (result.FaultNo == 0)
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
                    if (result)
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
                        loc.plcStatus.StatusToLoad = 0;
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