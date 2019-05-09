using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.TRANS
{
    public class CommonBiz
    {
        /// <summary>
        /// 请求生成任务
        /// </summary>
        public Loc RequstTask(Loc loc)
        {
            var result = loc;
            if (loc.RequestTaskObjid <= 0)
            {
                loc.RequestTaskObjid = DbAction.Instance.GetObjidForRequestTask();
            }
            if (loc.RequestTaskObjid <= 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]生成请求任务参数表OBJID失败", loc.LocNo));
                return result;
            }
            //传入参数，请求任务
            result = DbAction.Instance.RequestTask(loc);
            if (result.TaskNo > 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功请求生成任务[{result.TaskNo}]", loc.LocNo));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]请求生成任务失败,原因{result.FaultDesc}", loc.LocNo));
                //写入故障码至下位机
                WriteFaultNo(loc);
            }
            return result;
        }

        /// <summary>
        /// 请求生成指令
        /// </summary>
        public Loc RequstCmd(Loc loc)
        {
            var result = loc;
            //获取指令生成请求OBJID
            if (loc.RequestCmdObjid <= 0)
            {
                loc.RequestCmdObjid = DbAction.Instance.GetObjidForRequestCmd();
            }
            if (loc.RequestCmdObjid <= 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]生成请求参数表OBJID失败", loc.LocNo));
                return result;
            }
            //传入参数，请求指令
            result = DbAction.Instance.RequestCmd(loc);
            if (result.CmdId > 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功请求生成指令[{result.CmdId}]", loc.LocNo, InfoType.taskCmd));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]请求生成指令失败,原因{result.FaultDesc}", loc.LocNo));
                //写入故障码至下位机
                WriteFaultNo(loc);
            }
            return result;
        }

        /// <summary>
        /// 获取指令信息
        /// </summary>
        public bool SelectTaskCmd(Loc loc)
        {
            var errMsg = string.Empty;
            loc.taskCmd = DbAction.Instance.LoadTaskCmd(loc.LocNo, "00", ref errMsg);
            if (loc.taskCmd != null)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]站台接收到请求任务信号,获取到指令[{loc.taskCmd.ObjId}]", loc.LocNo, InfoType.taskCmd));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]站台接收到请求任务信号,但未找到指令 {errMsg}", loc.LocNo));
                return false;
            }
        }

        /// <summary>
        /// 修改指令信息步骤
        /// </summary>
        public bool UpdateTaskCmd(Loc loc)
        {
            var errMsg = string.Empty;
            //更新指令步骤
            var result = DbAction.Instance.UpdateCmdStep(loc.taskCmd.ObjId, "02");
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]更新指令步骤为执行[02]成功", loc.LocNo, InfoType.taskCmd));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]更新指令步骤为执行[02]失败,原因{errMsg}", loc.LocNo));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 结束指令
        /// </summary>
        public Loc FinishCmd(Loc loc)
        {
            var result = loc;
            //获取指令结束请求OBJID
            if (loc.RequestFinishObjid <= 0)
            {
                loc.RequestFinishObjid = DbAction.Instance.GetObjidForCmdFinish();
            }
            if (loc.RequestFinishObjid <= 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]生成指令结束参数表OBJID失败", loc.LocNo));
                return result;
            }
            //传入参数，结束指令
            result = DbAction.Instance.RequestFinishTaskCmd(loc, 1);
            if (result.FaultNo == 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功结束任务[{loc.plcStatus.TaskNo}]", loc.LocNo, InfoType.taskCmd));
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]结束任务[{loc.plcStatus.TaskNo}]失败,原因{loc.FaultDesc}", loc.LocNo));
                //写入故障码至下位机
                WriteFaultNo(loc);
            }
            return result;
        }

        /// <summary>
        /// 下发指令信息
        /// </summary>
        public bool WriteTaskCmd(Loc loc)
        {
            var errMsg = string.Empty;
            //写入指令信息
            var result = OpcAction.Instance.WriteTaskCmd(loc, ref errMsg);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功写入指令[{loc.taskCmd.ObjId}]", loc.LocNo));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]写入指令失败,原因：{errMsg}", loc.LocNo));
                return false;
            }
        }

        /// <summary>
        /// 下发验证信息，用于输送线与堆垛机验证任务流水号是否一致
        /// </summary>
        public bool WriteCheckInfo(Loc loc)
        {
            var errMsg = string.Empty;
            //写入指令信息
            var result = OpcAction.Instance.WriteCheckInfo(loc, ref errMsg);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功写入验证信息[任务流水号-{loc.taskCmd.TaskNo}|工装号-{loc.taskCmd.PalletNo}]", loc.LocNo));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]写入验证信息[任务流水号-{loc.taskCmd.TaskNo}|工装号-{loc.taskCmd.PalletNo}]失败,原因：{errMsg}", loc.LocNo));
                return false;
            }
        }

        /// <summary>
        /// 下发“请求上位机下发任务”处理信号
        /// </summary>
        public bool WriteFaultNo(Loc loc)
        {
            var errMsg = string.Empty;
            //写入任务已处理信号
            var result = OpcAction.Instance.WriteFaultNo(loc, ref errMsg);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功写入故障码[{loc.FaultNo + loc.FaultDesc}]", loc.LocNo));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]写入故障码[{loc.FaultNo + loc.FaultDesc}]失败,原因：{errMsg}", loc.LocNo));
                return false;
            }
        }

        /// <summary>
        /// 下发“请求上位机下发任务”处理信号
        /// </summary>
        public bool WriteRequestDeal(Loc loc)
        {
            var errMsg = string.Empty;
            //写入任务已处理信号
            var result = OpcAction.Instance.WriteRequestDeal(loc, ref errMsg);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功写入请求上位机下发任务处理标记[1]", loc.LocNo));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]写入请求上位机下发任务处理标记失败,原因：{errMsg}", loc.LocNo));
                return false;
            }
        }

        /// <summary>
        /// 下发“站点有货需取货”处理信号
        /// </summary>
        public bool WriteToLoadDeal(Loc loc)
        {
            var errMsg = string.Empty;
            //写入任务已处理信号
            var result = OpcAction.Instance.WriteToLoadDeal(loc, ref errMsg);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]成功写入站点有货需取货处理标记[1]", loc.LocNo));
                return true;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[{loc.LocPlcNo}]写入站点有货需取货处理标记失败,原因：{errMsg}", loc.LocNo));
                return false;
            }
        }
    }
}
