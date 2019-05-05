using IECSC.Trans.Entity;
using System;

namespace IECSC.Trans.Common
{
    /// <summary>
    /// 此类用于将相关信息显示到界面上
    /// </summary>
    public class ShowFormData
    {

        #region 单例实现
        private static ShowFormData _instance;
        public static ShowFormData Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(ShowFormData))
                    {
                        if (_instance == null)
                        {
                            _instance = new ShowFormData();
                        }
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 清理数据
        /// </summary>
        public void Reset()
        {
            _instance = null;
        }
        private ShowFormData()
        {
        }
        #endregion

        #region  定义事件
        public delegate void AppDataEventHandler(object sender, AppDataEventArgs e);
        public event AppDataEventHandler OnAppDtoData;
        #endregion

        public void ShowFormInfo(ShowInfoData data)
        {
            if (OnAppDtoData == null)
            {
                return;
            }
            var eventArgs = new AppDataEventArgs
            {
                AppData = data
            };
            OnAppDtoData(this, eventArgs);
        }
    }
    public class AppDataEventArgs : EventArgs
    {
        public ShowInfoData AppData { get; set; }
    }
    /// <summary>
    /// 要显示在界面上的信息
    /// </summary>
    public class ShowInfoData
    {
        public ShowInfoData(string stringInfo, ShowInfoTypeEnum infoType = ShowInfoTypeEnum.logInfo,string locNo = "")
        {
            StringInfo = stringInfo;
            InfoType = infoType;
            LocNo = locNo;
        }
        
        /// 显示在界面的文字信息
        public string StringInfo { get; set; }
        // 显示信息类型
        public ShowInfoTypeEnum InfoType { get; set; }
        //站台号
        public string LocNo { get; set; }
    }
}
