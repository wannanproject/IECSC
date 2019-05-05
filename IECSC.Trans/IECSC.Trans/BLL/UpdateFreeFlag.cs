using System;
using IECSC.Trans.DB;
using IECSC.Trans.Entity;
using IECSC.Trans.Common;
using IECSC.Trans.OPC;

namespace IECSC.Trans
{
    public class UpdateFreeFlag
    {
        #region 单例模式 
        private static UpdateFreeFlag _instance = null;
        public static UpdateFreeFlag Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(UpdateFreeFlag))
                    {
                        if (_instance == null)
                        {
                            _instance = new UpdateFreeFlag();
                        }
                    }
                }
                return _instance;
            }
        }
        private UpdateFreeFlag()
        {
        }
        #endregion

        /// <summary>
        /// 胶片库出库站台
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
            }
            catch (Exception ex)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"站台{locNo}更新空闲信号失败：{ex.ToString()}", locNo: locNo));
                SetLocStatus(locNo, $"站台{locNo}更新空闲信号失败：{ex.ToString()}");
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