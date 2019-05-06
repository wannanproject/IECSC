using GRreader;
using MSTL.LogAgent;
using MSTL.McSocket;
using RFID.Entity;
using RFID.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RFID
{
    public class BizRFID
    {
        /// <summary>
        /// 设置允许处理数据的时间
        /// </summary>
        private const int ALLOWTIME = 400;
        public DataTable dataTable;
        public UHFreader MyReader = new UHFreader();

        private List<RfidRead> mRfIdReadList = new List<RfidRead>();
        private static BizRFID _instance = null;
        public static BizRFID Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(BizRFID))
                    {
                        if (_instance == null)
                        {
                            _instance = new BizRFID();
                        }
                    }
                }
                return _instance;
            }
        }
        public BizRFID() { }
        public bool BizInit(ref string ErrMsg)
        {
            //连接数据库
            DbConnect(ref ErrMsg);
            //初始化socket
            InitSocket(ref ErrMsg);

            //初始化DataGridView
            if (!InitDgvDisplay())
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化列表异常", 0));
            }
            else
            {
            }

            return true;
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InitDgvDisplay()
        {
            try
            {
                var dt = DBHelper.Instance.GetDgvInfoFromDataBase();
                dataTable = dt;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                log.Error("显示列表信息时出现错误" + ex.ToString());
                return false;
            }

        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        ITCPAsyncClient Socketclient_Scanner = null;
        public void InitSocket(ref string ErrMsg)
        {
            try
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化socket...", 0));

                mRfIdReadList = DBHelper.Instance.GetIPAndPortNoFromDB();
                for (int i = 0; i < mRfIdReadList.Count; i++)
                {
                    Socketclient_Scanner = new TCPAsyncClient(new IPEndPoint(IPAddress.Parse(mRfIdReadList[i].IPAddress), Convert.ToInt32(mRfIdReadList[i].PortNo)));
                    Socketclient_Scanner.Connected += Socketclient_Scanner_Connected;
                    Socketclient_Scanner.Disconnected += Socketclient_Scanner_Disconnected;
                    Socketclient_Scanner.Received += Socketclient_Scanner_Received;
                    Socketclient_Scanner.SocketErred += Socketclient_Scanner_SocketErred;
                    Socketclient_Scanner.Connect();
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已成功初始化socket！", 0));

            }
            catch (Exception ex)
            {
                ErrMsg += ex.Message;
                log.Error("初始化socket异常：" + ex.ToString());
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"初始化socket异常！{ErrMsg}", 0));

            }
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        private void DbConnect(ref string ErrMsg)
        {
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData("初始化数据库连接...", 0));
            try
            {
                if (DBHelper.Instance.GetDbTime())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Lime", Entity.ShowInfoType.backcolor));
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已成功连接数据库！", 0));
                }
                else
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Red", Entity.ShowInfoType.backcolor));
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("连接数据库失败！", 0));
                }
            }
            catch (Exception ex)
            {
                ErrMsg += ex.Message;
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"连接数据库失败！{ErrMsg}", 0));
            }
        }

        /// <summary>
        /// 与服务器连接出现错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Socketclient_Scanner_SocketErred(object sender, ExceptionEventArgs e)
        {
            string ipAndPort = string.Empty;
            if (sender is TCPAsyncClient tcpClient)
            {
                ipAndPort = tcpClient.IpEndPoint.Address.ToString() + " :" + tcpClient.IpEndPoint.Port;
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Red", ShowInfoType.socketconn));

            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"连接RFID异常！    --{ipAndPort}", 0));
        }

        //定义一个委托
        delegate bool UpdateDBInfoDelegate(string trimReceived, string ip, string portNo);

        /// <summary>
        /// 回调函数，当异步调用函数执行成功时执行此函数
        /// </summary>
        /// <param name="result"></param>
        void UpdateDBInfoFinishedCallnack(IAsyncResult result)
        {
        }

        /// <summary>
        /// 接收到信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Socketclient_Scanner_Received(object sender, ReceivedEventArgs e)
        {
            try
            {
                var ipEndPoint = e.EndPoint as IPEndPoint;
                byte[] buffer = e.Buff;

                TagInfo tagInfo = MyReader.TagTimeSpanInventory(300);//时间段盘点标签的时间建议在200ms以上，否则比较容易读取失败，读取时间大约在 time + 50 ms

                //判断是否为空

                var sas = System.Text.Encoding.ASCII.GetString(tagInfo.EPC);



                string strReceived = Encoding.UTF8.GetString(buffer);
                if (string.IsNullOrWhiteSpace(strReceived))
                {
                    return;
                }
                //截取字符串
                string[] splitReceived = strReceived.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splitReceived.Length; i++)
                {
                    //删掉读取信息前的'&'符号
                    string trimReceived = splitReceived[i].TrimStart('&');
                    if (string.IsNullOrWhiteSpace(trimReceived))
                    {
                        continue;
                    }
                    else
                    {
                        //异步写入数据库
                        UpdateDBInfoDelegate updateDBInfoDelegate = new UpdateDBInfoDelegate(UpdateInfoToDataBase);
                        IAsyncResult result = updateDBInfoDelegate.BeginInvoke(trimReceived, ipEndPoint.Address.ToString(), ipEndPoint.Port.ToString(), UpdateDBInfoFinishedCallnack, null);
                        result.AsyncWaitHandle.WaitOne(ALLOWTIME);
                        if (result.IsCompleted)
                        {
                            AsyncResult asyncResult = (AsyncResult)result;
                            UpdateDBInfoDelegate del = (UpdateDBInfoDelegate)asyncResult.AsyncDelegate;
                            bool bResult = del.EndInvoke(result);
                            if (bResult == true)
                            {
                                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"数据库处理成功！  --来自{ipEndPoint.Address.ToString()}:{ipEndPoint.Port.ToString()}", 0));
                            }
                            else
                            {
                                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"数据库处理失败..--来自{ipEndPoint.Address.ToString()}:{ipEndPoint.Port.ToString()}", 0));
                            }
                        }


                    }
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData(strReceived, 0));
                }
            }
            catch (Exception ex)
            {
                log.Error("初始化socket,接收信息异常：" + ex.ToString());
            }
        }


        public bool UpdateInfoToDataBase(string trimReceived, string ip, string portNo)
        {

            var intType = DBHelper.Instance.SetRFIDInfoToDb(trimReceived, ip, portNo);
            return intType > 0;
        }

        /// <summary>
        /// 与客户端断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Socketclient_Scanner_Disconnected(object sender, EventArgs e)
        {
            string ip = string.Empty;
            var tcpClient = sender as TCPAsyncClient;
            if (tcpClient != null)
            {
                ip = tcpClient.IpEndPoint.Address.ToString();
                SetRfidConnectState(ip, false);
                SetRfidConnectUiStatus();
            }
            var ipAndPort = ip + " :" + tcpClient.IpEndPoint.Port;
            ShowInfoData showInfoData = new ShowInfoData($"RFID已断开！      --{ipAndPort}", 0);
            ShowFormData.Instance.ShowFormInfo(showInfoData);
        }

        /// <summary>
        /// 设置RFID连接状态
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="state"></param>
        private void SetRfidConnectState(string ip, bool state)
        {
            foreach (var item in this.mRfIdReadList)
            {
                if (item.IPAddress == ip)
                {
                    item.IsConnected = state;
                }
            }
        }
        /// <summary>
        /// 设置状态栏Socket连接状态
        /// </summary>
        private void SetRfidConnectUiStatus()
        {
            var result = this.mRfIdReadList.Any(p => p.IsConnected == false);
            if (result)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Red", ShowInfoType.socketconn));

            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Lime", ShowInfoType.socketconn));
            }
        }

        /// <summary>
        /// 成功连接上客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Socketclient_Scanner_Connected(object sender, ConnectionEventArgs e)
        {
            var point = e.EndPoint as System.Net.IPEndPoint;
            string ip = point.Address.ToString();
            SetRfidConnectState(ip, true);
            SetRfidConnectUiStatus();
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"已成功连接RFID！  --{ip}:{ point.Port }", ShowInfoType.logInfo));
        }


        /// <summary>
        /// 系统日志
        /// </summary>
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }
    }
}
