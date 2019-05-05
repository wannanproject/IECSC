using System;
using IECSC.Trans.Common;
using IECSC.Trans.DB;
using IECSC.Trans.OPC;

namespace IECSC.Trans
{
    class RequestAgvTask
    {
        #region 单例模式 
        private static RequestAgvTask _instance = null;
        public static RequestAgvTask Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(RequestAgvTask))
                    {
                        if (_instance == null)
                        {
                            _instance = new RequestAgvTask();
                        }
                    }
                }
                return _instance;
            }
        }
        private RequestAgvTask()
        {
        }
        #endregion

        public void HandleLoc(string locNo, OPCWrite opcWrite)
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
            if (locStatus.StatusRequestTask == 0)
            {
                return;
            }
            //验证工装号是否存在
            if (string.IsNullOrWhiteSpace(locStatus.PalletNo.ToString()))
            {
                SetLocStatus(locNo, $"{msgPre}未获取到工装信息!");
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"工装信息不能为空!", locNo: locNo));
                return;
            }
            //检测当前站台是否已存在指令
            if (DbAction.Instance.GetAgvCmd(locNo))
            {
                SetLocStatus(locNo, $"{msgPre}已存在Agv指令!");
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"已存在Agv指令!", locNo: locNo));
                return;
            }
            else
            {
                //浸漆机出口站台不存在Agv指令
                if (DbAction.Instance.ExecuteProcedure(locNo, locStatus.PalletNo.ToString()))
                {
                    SetLocStatus(locNo, $"{msgPre}生成Agv指令成功!");
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"成功生成Agv指令!", locNo: locNo));
                }
                else
                {
                    SetLocStatus(locNo, $"{msgPre}生成Agv指令失败!");
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"生成Agv指令失败!", locNo: locNo));
                }
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

