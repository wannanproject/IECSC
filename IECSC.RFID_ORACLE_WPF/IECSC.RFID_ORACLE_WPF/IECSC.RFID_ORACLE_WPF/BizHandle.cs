using GRreader;
using MSTL.LogAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.RFID_ORACLE_WPF
{
    public class BizHandle
    {
      

        public List<RfidRead> mRfIdReadList = null;
        public Dictionary<string, UHFreader> myUHFreader = null;

        string LastReadInfo = null;
        /// <summary>
        /// 日志
        /// </summary>
        private ILog log
        {
            get
            {
                return Log.Store[this.GetType().FullName];
            }
        }

        #region 单例模式
        private static BizHandle _instance = null;
        public static BizHandle Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(BizHandle))
                    {
                        if (_instance == null)
                        {
                            _instance = new BizHandle();
                        }
                    }
                }
                return _instance;
            }
        }
        private BizHandle()
        {
           
            mRfIdReadList = new List<RfidRead>();
            myUHFreader = new Dictionary<string, UHFreader>();
        }
        #endregion

        /// <summary>
        /// 业务处理入口
        /// </summary>
        public void BizListen()
        {
            //IBiz biz = null;
            foreach (var loc in mRfIdReadList)
            {
                if (!myUHFreader.ContainsKey(loc.LocNo))
                {
                    UHFreader MyReader = new UHFreader();

                    bool result = MyReader.NetConnect(loc.IPAddress, int.Parse(loc.PortNo));
                    if (result)
                    {
                        myUHFreader.Add(loc.LocNo, MyReader);
                        TagInfo tagInfo = MyReader.TagTimeSpanInventory(300);//时间段盘点标签的时间建议在200ms以上，否则比较容易读取失败，读取时间大约在 time + 50 ms
                        //判断是否为空
                        string readInfo = null;
                        if (tagInfo.EPC != null)
                        {
                            readInfo = System.Text.Encoding.ASCII.GetString(tagInfo.EPC);
                        }
                        if (LastReadInfo != readInfo)
                        {
                            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{readInfo}", loc.LocNo, InfoType.locStatus));
                            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"当前读取RFID:{readInfo}", loc.LocNo, InfoType.logInfo));

                            UpdatePemLocStatus(loc.LocNo, readInfo);
                            LastReadInfo = readInfo;
                        }


                    }
                }
                else
                {
                    UHFreader myReader = myUHFreader.SingleOrDefault(k => k.Key == loc.LocNo).Value;
                    TagInfo tagInfo = myReader.TagTimeSpanInventory(300);
                    string readInfo = null;
                    if (tagInfo.EPC != null)
                    {
                        readInfo = System.Text.Encoding.ASCII.GetString(tagInfo.EPC);
                    }
                    if (LastReadInfo != readInfo)
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{readInfo}", loc.LocNo, InfoType.locStatus));
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"当前读取RFID:{readInfo}", loc.LocNo, InfoType.logInfo));

                        UpdatePemLocStatus(loc.LocNo, readInfo);
                        LastReadInfo = readInfo;
                    }
                }
            }

        }

        internal void UpdatePemLocStatus(string locNo, string msg)
        {
            DbAction.Instance.UpdatePemLocStatus(locNo, msg);
        }
    }
}
