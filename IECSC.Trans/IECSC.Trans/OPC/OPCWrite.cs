using System;
using System.Collections.Generic;
using System.Linq;
using IECSC.Trans.Common;
using IECSC.Trans.Entity;
using MSTL.LogAgent;

namespace IECSC.Trans.OPC
{
    public class OPCWrite
    {

        #region Method

        private string GROUPNAME;
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }

        #endregion

        public OPCWrite(string groupName)
        {
            GROUPNAME = groupName;
        }

        /// <summary>
        /// 写入opc项
        /// </summary>
        /// <param name="cmdInfo"></param>
        /// <returns></returns>
        public bool WriteCmdOPC(string locNo, TransferCommand cmdInfo)
        {
            try
            {
                if (cmdInfo.TaskNo <= 0)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("未找到下传指令"));
                    return false;
                }
                string ErrMsg = string.Empty;
                KeyValuePair<string, object>[] itemPairs = new KeyValuePair<string, object>[] { };
                itemPairs = GetCmdDownItemDic(cmdInfo);

                if (itemPairs.Length > 0)
                {
                    //写入任务信息
                    if (OpcAction.Instance.WriteItems(GROUPNAME, itemPairs, ref ErrMsg))
                    {
                        WriteTaskFinsh(locNo);
                    }
                }
                if (string.IsNullOrWhiteSpace(ErrMsg))
                {
                    return true;
                }
                Log.Error("OPC下发任务错误" + ErrMsg);
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC下发任务错误"+ErrMsg));
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("指令下发异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 指令生成下发字典
        /// </summary>
        /// <param name="gantryOpcGroupNo"></param>
        /// <param name="gantryCmdInfo"></param>
        /// <returns></returns>
        private KeyValuePair<string, object>[] GetCmdDownItemDic(TransferCommand CmdInfo)
        {
            try
            {
                KeyValuePair<string, object>[] cmdPairs = new KeyValuePair<string, object>[6];
                foreach (var opcItem in BizHandle.Instance.opcItemsDic.Values)
                {
                    if (opcItem.LocNo == CmdInfo.LocNo)
                    {
                        switch (opcItem.BusinessIdentity)
                        {
                            case "Write.TaskNo":
                                cmdPairs[0] = new KeyValuePair<string, object>(opcItem.TagLongName, CmdInfo.TaskNo);
                                break;
                            case "Write.PalletNo":
                                cmdPairs[1] = new KeyValuePair<string, object>(opcItem.TagLongName, CmdInfo.PalletNo??string.Empty);
                                break;
                            case "Write.SlocArea":
                                cmdPairs[2] = new KeyValuePair<string, object>(opcItem.TagLongName, (int)Convert.ToChar(CmdInfo.SourceAddressAreaNo));
                                break;
                            case "Write.SlocNo":
                                cmdPairs[3] = new KeyValuePair<string, object>(opcItem.TagLongName, CmdInfo.SourceAddressDeviceId);
                                break;
                            case "Write.ElocArea":
                                cmdPairs[4] = new KeyValuePair<string, object>(opcItem.TagLongName, (int)Convert.ToChar(CmdInfo.TargetAddressAreaNo));
                                break;
                            case "Write.ElocNo":
                                cmdPairs[5] = new KeyValuePair<string, object>(opcItem.TagLongName, CmdInfo.TargetAddressDeviceId);
                                break;
                        }
                    }
                }
                return cmdPairs;
            }
            catch (Exception ex)
            {
                Log.Error("生成任务出错",ex);
                return null;
            }
        }

        /// <summary>
        /// 下传任务处理标记
        /// </summary>
        /// <param name="locNo"></param>
        /// <returns></returns>
        public bool WriteTaskFinsh(string locNo)
        {
            try
            {
                string ErrMsg = string.Empty;
                var opcConfigItem = BizHandle.Instance.opcItemsDic.FirstOrDefault(p => p.Value.LocNo == locNo && p.Value.BusinessIdentity == "Write.TaskDeal");
                if (opcConfigItem.Key == null)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC赋值错误，未找到赋值点,请检查数据库点，是否配置正确！！！"));
                    return false;
                }
                OpcAction.Instance.WriteItemSync(GROUPNAME, new KeyValuePair<string, object>(opcConfigItem.Key, 1), ref ErrMsg);
                if (ErrMsg == string.Empty)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("写入任务已处理标记失败（" + locNo + "）", ex);
                return false;
            }
        }
    }
}
