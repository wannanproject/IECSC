using MSTL.LogAgent;
using MSTL.McSocket;
using RFID.Entity;
using RFID.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID
{
    public partial class MainForm : Form
    {

        private Thread thread;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //注册事件
            ShowFormData.Instance.OnAppDtoData += ShowInfo;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string ErrMsg = string.Empty;

            if (BizRFID.Instance.BizInit(ref ErrMsg))
            {
                this.dgv_info.DataSource = BizRFID.Instance.dataTable;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"程序初始化异常！{ErrMsg}", 0));
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Entity.ShowInfoType.logintime));
            //另起一个线程刷新界面与判断数据库连接状态
            SecondThreadStart();
        }
        /// <summary>
        /// 创建线程
        /// </summary>
        private void SecondThreadStart()
        {
            thread = new Thread(RefreshStart);
            thread.IsBackground = true;
            thread.Start();
        }

        private void RefreshStart()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(800);
                    if (DBHelper.Instance.GetDbTime())
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Lime", Entity.ShowInfoType.backcolor));
                    }
                    else
                    {
                        ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Red", Entity.ShowInfoType.backcolor));
                    }
                    //刷新列表数据
                    BizRFID.Instance.InitDgvDisplay();
                    SetUIStatus();
                }

            }
            catch (Exception ex)
            {
                log.Error("时钟执行异常" + ex.ToString());
            }

        }

        private void SetUIStatus()
        {
            if (this.InvokeRequired)
            {
                Action action = SetUIStatus;
                this.Invoke(action);
            }
            else
            {
                this.dgv_info.DataSource = BizRFID.Instance.dataTable;
            }
        }

        /// <summary>
        /// 定义委托进行跨线程操作控件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tyreInfo"></param>
        private delegate void FlushForm(string msg, ShowInfoType infoType);

        /// <summary>
        /// 显示界面信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowInfo(object sender, AppDataEventArgs e)
        {
            var appData = e.AppData;
            string msg = appData.StringInfo;
            ShowInfoType infoType = appData.InfoType;
            FormShow(msg, infoType);
        }
        /// <summary>
        /// 根据类型分类显示界面信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="infoType"></param>
        private void FormShow(string msg, ShowInfoType infoType)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    FlushForm ff = new FlushForm(FormShow);
                    this.Invoke(ff, new object[] { msg, infoType });
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        switch (infoType)
                        {
                            case ShowInfoType.logInfo:
                                this.richTextBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":  " + msg + "\r\n");
                                this.richTextBox1.ScrollToCaret();
                                if (richTextBox1.Lines.Length >= 1000)
                                {
                                    richTextBox1.Clear();
                                }
                                break;
                            case ShowInfoType.logintime:
                                this.connTime.Text = msg;
                                break;
                            case ShowInfoType.backcolor:
                                if (msg.ToString().Equals("Lime"))
                                {
                                    this.label1.BackColor = System.Drawing.Color.Lime;
                                }
                                else
                                {
                                    this.label1.BackColor = System.Drawing.Color.Red;
                                }
                                break;
                            case ShowInfoType.socketconn:
                                if (msg.ToString().Equals("Lime"))
                                {
                                    this.lblSocketConnectStatus.BackColor = System.Drawing.Color.Lime;
                                }
                                else
                                {
                                    this.lblSocketConnectStatus.BackColor = System.Drawing.Color.Red;
                                }
                                break;
                            case ShowInfoType.status:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("显示桌面信息时出现错误" + ex.ToString());
            }
        }



        /// <summary>
        /// 系统日志
        /// </summary>
        private ILog log { get { return Log.Store[this.GetType().FullName]; } }


    }
}
