using System;
using System.Data;

namespace IECSC.CRN.SINGLE
{
    public partial class DbAction
    {
        /// <summary>
        /// 初始化报警信息
        /// </summary>
        /// <returns></returns>
        public bool LoadCrnFault()
        {
            try
            {
                var dt = GetCrnFault();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return false;
                }
                foreach(DataRow row in dt.Rows)
                {
                    BizHandle.Instance.crnErr[row["ERR_NO"].ToString()] = row["ERR_NAME"].ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("初始化设备报警信息异常:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 初始化堆垛机信息
        /// </summary>
        public bool LoadOpcItems()
        {
            try
            {
                var dt = GetCrnData();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return false;
                }
                BizHandle.Instance.plcStatus.CrnNo = dt.Rows[0]["CRN_NO"].ToString();
                BizHandle.Instance.plcStatus.CrnName = dt.Rows[0]["CRN_NAME"].ToString();
                
                //初始化读取项信息
                var dtRead = GetReadItemsData();
                if (dtRead == null || dtRead.Rows.Count <= 0)
                {
                    return false;
                }
                foreach (DataRow Row in dtRead.Rows)
                {
                    var opcItem = new OpcItem(); 
                    opcItem.TagName = Row["TAGLONGNAME"].ToString();
                    opcItem.TagLongName = Row["TAGLONGNAME"].ToString();
                    opcItem.BusIdentity = Row["BUSIDENTITY"].ToString();
                    opcItem.TagIndex = Convert.ToInt32(Row["TAGINDEX"].ToString());
                    BizHandle.Instance.readItems.Add(opcItem);
                }

                //初始化写入项信息
                var dtWrite = GetWriteItemsData();
                if (dtWrite == null || dtWrite.Rows.Count <= 0)
                {
                    return false;
                }
                foreach (DataRow Row in dtWrite.Rows)
                {
                    var opcItem = new OpcItem();
                    opcItem.TagName = Row["TAGLONGNAME"].ToString();
                    opcItem.TagLongName = Row["TAGLONGNAME"].ToString();
                    opcItem.BusIdentity = Row["BUSIDENTITY"].ToString();
                    opcItem.TagIndex = Convert.ToInt32(Row["TAGINDEX"].ToString());
                    BizHandle.Instance.writeItems.Add(opcItem);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化堆垛机信息失败"));
                log.Error("初始化堆垛机信息失败:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取指令
        /// </summary>
        /// <returns></returns>
        public bool LoadCmdToCrnFork()
        {
            try
            {
                var dt = GetCmdNoExec();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return false;
                }
                BizHandle.Instance.taskCmd = new TaskCmd();
                BizHandle.Instance.taskCmd.ObjId = Convert.ToInt32(dt.Rows[0]["OBJID"].ToString());
                BizHandle.Instance.taskCmd.TaskNo = Convert.ToInt32(dt.Rows[0]["TASK_NO"]);
                BizHandle.Instance.taskCmd.SlocType = dt.Rows[0]["SLOC_TYPE"].ToString();
                BizHandle.Instance.taskCmd.SlocNo = dt.Rows[0]["SLOC_NO"].ToString();
                BizHandle.Instance.taskCmd.SlocPlcNo = dt.Rows[0]["SLOC_PLC_NO"].ToString();
                BizHandle.Instance.taskCmd.ElocType = dt.Rows[0]["ELOC_TYPE"].ToString();
                BizHandle.Instance.taskCmd.ElocNo = dt.Rows[0]["ELOC_NO"].ToString();
                BizHandle.Instance.taskCmd.ElocPlcNo = dt.Rows[0]["ELOC_PLC_NO"].ToString();
                BizHandle.Instance.taskCmd.PalletNo = dt.Rows[0]["PALLET_NO"].ToString();
                BizHandle.Instance.taskCmd.CmdType = dt.Rows[0]["CMD_TYPE"].ToString();
                BizHandle.Instance.taskCmd.ProductNo = dt.Rows[0]["PRODUCT_NO"].ToString();
                BizHandle.Instance.taskCmd.CmdStep = dt.Rows[0]["CMD_STEP"].ToString();
                BizHandle.Instance.plcStatus.TaskNo = Convert.ToInt32(dt.Rows[0]["TASK_NO"]);
                BizHandle.Instance.plcStatus.SLocNo = dt.Rows[0]["SLOC_PLC_NO"].ToString();
                BizHandle.Instance.plcStatus.ELocNo = dt.Rows[0]["ELOC_PLC_NO"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"获取指令信息异常:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 获取指令
        /// </summary>
        /// <returns></returns>
        public bool GetCrnForksCmd(string CrnNo)
        {
            try
            {
                var dt = GetCmdByCrnNo(CrnNo);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return false;
                }
                if(dt.Rows[0]["CMD_STEP"].Equals("00"))
                {
                    return false;
                }
                BizHandle.Instance.taskCmd = new TaskCmd();
                BizHandle.Instance.taskCmd.ObjId = Convert.ToInt32(dt.Rows[0]["OBJID"].ToString());
                BizHandle.Instance.taskCmd.TaskNo = Convert.ToInt32(dt.Rows[0]["TASK_NO"]);
                BizHandle.Instance.taskCmd.SlocType = dt.Rows[0]["SLOC_TYPE"].ToString();
                BizHandle.Instance.taskCmd.SlocNo = dt.Rows[0]["SLOC_NO"].ToString();
                BizHandle.Instance.taskCmd.SlocPlcNo = dt.Rows[0]["SLOC_PLC_NO"].ToString();
                BizHandle.Instance.taskCmd.ElocType = dt.Rows[0]["ELOC_TYPE"].ToString();
                BizHandle.Instance.taskCmd.ElocNo = dt.Rows[0]["ELOC_NO"].ToString();
                BizHandle.Instance.taskCmd.ElocPlcNo = dt.Rows[0]["ELOC_PLC_NO"].ToString();
                BizHandle.Instance.taskCmd.PalletNo = dt.Rows[0]["PALLET_NO"].ToString();
                BizHandle.Instance.taskCmd.CmdType = dt.Rows[0]["CMD_TYPE"].ToString();
                BizHandle.Instance.taskCmd.ProductNo = dt.Rows[0]["PRODUCT_NO"].ToString();
                BizHandle.Instance.taskCmd.CmdStep = dt.Rows[0]["CMD_STEP"].ToString();
                BizHandle.Instance.plcStatus.TaskNo = Convert.ToInt32(dt.Rows[0]["TASK_NO"]);
                BizHandle.Instance.plcStatus.SLocNo = dt.Rows[0]["SLOC_PLC_NO"].ToString();
                BizHandle.Instance.plcStatus.ELocNo = dt.Rows[0]["ELOC_PLC_NO"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"复位堆垛机状态异常:{ex.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// 记录设备报警信息
        /// </summary>
        /// <returns></returns>
        public bool RecordSrmFaultInfo()
        {
            try
            {
                if (BizHandle.Instance.plcStatus.OperateMode == 0)
                {
                    return false;
                }
                if (BizHandle.Instance.plcStatus.FaultNo != BizHandle.Instance.FAULTNO)
                {
                    if (BizHandle.Instance.FAULTID <= 0)
                    {
                        BizHandle.Instance.FAULTID = DbAction.Instance.GetObjidForFault();
                        if (!DbAction.Instance.RecordWarnLog(BizHandle.Instance.FAULTID))
                        {
                            return false;
                        }
                        BizHandle.Instance.FAULTNO = BizHandle.Instance.plcStatus.FaultNo;
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[异常日志]WCS记录设备故障[{BizHandle.Instance.crnErr[BizHandle.Instance.plcStatus.FaultNo.ToString()]}]"));
                    }
                    else
                    {
                        if (!DbAction.Instance.UpdateWarnLog(BizHandle.Instance.FAULTID))
                        {
                            return false;
                        }
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"[异常日志]WCS更新设备故障[{BizHandle.Instance.crnErr[BizHandle.Instance.FAULTNO.ToString()]}]已处理标记"));
                        BizHandle.Instance.FAULTID = 0;
                    } 
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error($"记录设备报警信息异常:{ex.ToString()}");
                return false;
            }
        }
    }
}
