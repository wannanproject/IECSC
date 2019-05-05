using IECSC.Trans.Common;
using IECSC.Trans.Entity;
using MSTL.LogAgent;
using System;
using System.Collections.Generic;

namespace IECSC.Trans.OPC
{
    public class OPCObserver : IOpcNetObserver
    {
        #region 记录日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }

        #endregion

        private Dictionary<string,CommonOpcItems> OpcItemsDic;

        public OPCObserver()
        {
            OpcItemsDic = BizHandle.Instance.opcItemsDic;
        }

        public bool UpdateEntity(string TagLongName, object TagValue, bool Quality)
        {
            try
            {
                if (OpcItemsDic.ContainsKey(TagLongName))
                {
                    SetManipRealTimeValue(OpcItemsDic[TagLongName].LocNo, OpcItemsDic[TagLongName].BusinessIdentity, TagValue, Quality, TagLongName);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace + "\r\n" + ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// 设置实时值
        /// </summary>
        /// <param name="cncgNo"></param>
        /// <param name="businessIdentity"></param>
        /// <param name="itemValue"></param>
        private void SetManipRealTimeValue(string locNo, string businessIdentity, object itemValue, bool QualitySpecified, string TagLongName)
        {
            try
            {
                if (BizHandle.Instance.locStatuesDic[locNo] == null)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC赋值错误，未找到赋值点,请检查数据库点，是否配置正确！！！"));
                    return;
                }
                if (!QualitySpecified)
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC已断开连接"));
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已断开", ShowInfoTypeEnum.plcConn));
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已连接", ShowInfoTypeEnum.plcConn));
                switch (businessIdentity)
                {
                    case "Read.TaskNo":
                        BizHandle.Instance.locStatuesDic[locNo].TaskNo = Convert.ToInt32(itemValue);
                        break;
                    case "Read.PalletNo":
                        BizHandle.Instance.locStatuesDic[locNo].PalletNo = itemValue.ToString().Trim();
                        break;
                    case "Read.ProductWeight":
                        BizHandle.Instance.locStatuesDic[locNo].ProductWeight = Convert.ToDouble(itemValue ?? 0);
                        break;
                    case "Read.SlocArea":
                        BizHandle.Instance.locStatuesDic[locNo].SourceAddressAreaNo = itemValue.ToString().Trim();
                        break;
                    case "Read.SlocNo":
                        BizHandle.Instance.locStatuesDic[locNo].SourceAddressDeviceId = itemValue.ToString().Trim();
                        break;
                    case "Read.ElocArea":
                        BizHandle.Instance.locStatuesDic[locNo].TargetAddressAreaNo = itemValue.ToString().Trim();
                        break;
                    case "Read.ElocNo":
                        BizHandle.Instance.locStatuesDic[locNo].TargetAddressDeviceId = itemValue.ToString().Trim();
                        break;
                    case "Read.StatusAuto":
                        BizHandle.Instance.locStatuesDic[locNo].StatusAutomatic = Convert.ToInt32(itemValue);
                        break;
                    case "Read.StatusFault":
                        BizHandle.Instance.locStatuesDic[locNo].StatusFault = Convert.ToInt32(itemValue);
                        break;
                    case "Read.StatusLoading":
                        BizHandle.Instance.locStatuesDic[locNo].StatusLoad = Convert.ToInt32(itemValue);
                        break;
                    case "Read.StatusRequest":
                        BizHandle.Instance.locStatuesDic[locNo].StatusRequestTask = Convert.ToInt32(itemValue);
                        break;
                    case "Read.StatusToUnload":
                        BizHandle.Instance.locStatuesDic[locNo].StatusFreeAndPut = Convert.ToInt32(itemValue);
                        break;
                    case "Read.StatusToLoad":
                        BizHandle.Instance.locStatuesDic[locNo].StatusBusyAndTake = Convert.ToInt32(itemValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("OPC读取出错"+ ex.ToString()));
                Log.Error(ex.StackTrace + "\r\n" + ex.Message);
            }

        }
    }
}
