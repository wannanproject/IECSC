using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MSTL.LogAgent;

namespace IECSC.CRN.SINGLE
{  
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 连接正常
        /// </summary>
        private Color LIME = Color.Lime;
        /// <summary>
        /// 连接异常
        /// </summary>
        private Color RED = Color.Red;
        /// <summary>
        /// 初始状态
        /// </summary>
        private Color WHITE = Color.White;
        /// <summary>
        /// 等待状态
        /// </summary>
        private Color YELLOW = Color.Yellow;

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
        public FrmMain()
        {
            InitializeComponent();
            ShowFormData.Instance.OnAppDtoData += ShowInfo;
            this.dgvCrn.SetDoubleBuffered(true);
            this.timer_Monitor.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
            this.connTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            if(BizHandle.Instance.BizInit())
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("系统初始化完成"));
                //记录设备故障日志
                var thread = new Thread(RecordEquipStatus);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        #region 功能按键
        /// <summary>
        /// 查询按键
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetTaskCmd();
        }
        /// <summary>
        /// 重发按键
        /// </summary>
        private void btnRefSend_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.dgvCmd.SelectedRows)
                {
                    var cmdObjid = row.Cells["OBJID"].Value.ToString();
                    if (MessageBox.Show($"确定要重发当前指令[{cmdObjid}]吗？", "指令重发", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(cmdObjid))
                    {
                        MessageBox.Show("未找到对应的指令编号");
                        return;
                    }
                    if (DbAction.Instance.UpdateCmdStep("00"))
                    {
                        MessageBox.Show($"[{cmdObjid}]-指令重发成功");
                    }
                }
                BizHandle.Instance.equipStatus = Enums.EquipStatus.Reset;
            }
            catch (Exception ex)
            {
                MessageBox.Show("任务重发异常：" + ex.ToString());
            }
        }
        /// <summary>
        /// 完成按键
        /// </summary>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.dgvCmd.SelectedRows)
                {
                    var objid = row.Cells["OBJID"].Value.ToString();
                    var elocNo = row.Cells["ELOC_NO"].Value.ToString();
                    if (MessageBox.Show($"确定要完成指令[{objid}]吗？", "指令完成", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(objid))
                    {
                        MessageBox.Show("[指令完成]未找到对应的任务号");
                        return;
                    }
                    var errMsg = string.Empty;
                    if (!DbAction.Instance.FinishCrnCmd(DbAction.Instance.GetObjidForCmdFinish(), int.Parse(objid), elocNo, 1, ref errMsg))
                    {
                        MessageBox.Show($"指令[{objid}]结束异常:{errMsg}");
                    }
                    MessageBox.Show($"指令[{objid}]结束成功");
                    BizHandle.Instance.equipStatus = Enums.EquipStatus.Reset;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"任务完成错误：{ ex.ToString()}");
            }
        }
        /// <summary>
        /// 删除按键
        /// </summary>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.dgvCmd.SelectedRows)
                {
                    var taskNo = row.Cells["TASK_NO"].Value.ToString();
                    if (MessageBox.Show($"确定要删除当前任务[{taskNo}]吗？", "任务删除", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(taskNo))
                    {
                        MessageBox.Show("[任务删除]未找到对应的任务号");
                        return;
                    }
                    if (DbAction.Instance.DeleteTaskCmd(int.Parse(taskNo)))
                    {
                        MessageBox.Show($"任务[{taskNo}]已手动删除");
                    }
                }
                BizHandle.Instance.equipStatus = Enums.EquipStatus.Reset;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"任务删除异常：{ex.ToString()}");
            }
        }
        #endregion

        #region 界面展示
        /// <summary>
        /// 定义委托进行跨线程操作控件
        /// </summary>
        private delegate void FlushForm(string msg, InfoType infoType);

        /// <summary>
        /// 显示界面信息
        /// </summary>
        public void ShowInfo(object sender, AppDataEventArgs e)
        {
            var appData = e.AppData;
            string msg = appData.StringInfo;
            InfoType infoType = appData.InfoType;
            FormShow(msg, infoType);
        }

        private void FormShow(string msg, InfoType infoType)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    FlushForm ff = new FlushForm(FormShow);
                    this.BeginInvoke(ff, new object[] { msg, infoType });
                    return;
                }
                if (string.IsNullOrWhiteSpace(msg) && infoType == InfoType.rtb)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(msg) && infoType == InfoType.errLog)
                {
                    return;
                }
                if (infoType == InfoType.errLog && msg.Trim().Equals(BizHandle.Instance.errLog.Trim()))
                {
                    return;
                }
                switch (infoType)
                {
                    case InfoType.cmdDgv:
                        GetTaskCmd();
                        ShowExecStatus(msg);
                        break;
                    case InfoType.opcDgv:
                        ShowCrnPlcStatus();
                        break;
                    case InfoType.rtb:
                        ShowExecStatus(msg);
                        break;
                    case InfoType.errLog:
                        ShowErrStatus(msg);
                        break;
                    case InfoType.dbConn:
                        if (msg.Equals("Y"))
                        {
                            this.dbConnStatus.BackColor = LIME;
                        }
                        else
                        {
                            this.dbConnStatus.BackColor = RED;
                        }
                        break;
                    case InfoType.plcConn:
                        if (msg.Equals("Y"))
                        {
                            this.plcConnStatus.BackColor = LIME;
                        }
                        else
                        {
                            this.plcConnStatus.BackColor = RED;
                        }
                        break;
                    default:
                        ShowExecStatus(msg);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error($"显示桌面信息时出现错误{ex.ToString()}");
            }
        }

        /// <summary>
        /// GridView数据显示(堆垛机叉)
        /// </summary>
        public void ShowCrnPlcStatus()
        {
            try
            {
                DataTable dtCrn = new DataTable();
                if (dtCrn.Columns.Count == 0)
                {
                    DataColumn columns = new DataColumn();
                    columns.ColumnName = "属性名称";
                    dtCrn.Columns.Add(columns);
                    columns = new DataColumn();
                    columns.ColumnName = BizHandle.Instance.plcStatus.CrnNo;
                    dtCrn.Columns.Add(columns);
                }
                var type = typeof(PlcStatus);
                var types = type.GetProperties();
                var x = 0;
                foreach (var i in types)
                {
                    object[] attrs = i.GetCustomAttributes(true);
                    var attr = attrs[0] as DisplayNameAttribute;
                    var displayName = attr.DisplayName;
                    i.GetValue(BizHandle.Instance.plcStatus, null);
                    displayName += $",{i.GetValue(BizHandle.Instance.plcStatus, null)}";
                    dtCrn.Rows.Add(displayName.Split(','));
                    x++;
                }
                this.dgvCrn.DataSource = dtCrn;
                this.gbCrnItems.Width = 120 * dtCrn.Columns.Count;
                this.dgvCrn.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;

                //颜色标注
                for (int y = 0; y < dgvCrn.Rows.Count - 1; y++)
                {
                    var row = dgvCrn.Rows[y];
                    if (row.Cells[0].Value.Equals("读:设备状态"))
                    {
                        if (row.Cells[1].Value.Equals("1"))
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = LIME;
                        }
                        else
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = RED;
                        }
                    }
                    if (row.Cells[0].Value.Equals("读:任务状态"))
                    {
                        if (row.Cells[1].Value.Equals("0"))
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = WHITE;
                        }
                        else if (row.Cells[1].Value.Equals("1") || row.Cells[1].Value.Equals("2"))
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = LIME;
                        }
                        else
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = YELLOW;
                        }
                    }
                    if (row.Cells[0].Value.Equals("读:故障代码"))
                    {
                        if (row.Cells[1].Value.Equals("0"))
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = LIME;
                        }
                        else
                        {
                            dgvCrn.Rows[y].Cells[1].Style.BackColor = RED;
                        }
                    }
                    if (row.Cells[0].Value.Equals("写:任务号") || row.Cells[0].Value.Equals("写:起始位置") || row.Cells[0].Value.Equals("写:结束位置"))
                    {
                        dgvCrn.Rows[y].Cells[1].Style.BackColor = Color.LightSteelBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"显示桌面信息时ShowCrnPlcStatus()出现错误{ex.ToString()}");
            }
        }

        /// <summary>
        /// 显示运行日志
        /// </summary>
        /// <param name="msg"></param>
        public void ShowExecStatus(string msg)
        {
            try
            {
                this.rtbExecLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}-{msg}\r\n");
                this.rtbExecLog.ScrollToCaret();
                if (rtbExecLog.Lines.Length >= 20000)
                {
                    rtbExecLog.Clear();
                }
            }
            catch (Exception ex)
            {
                log.Error($"ShowExecStatus()出现错误{ex.ToString()}");
            }
        }

        /// <summary>
        /// 显示异常日志
        /// </summary>
        /// <param name="msg"></param>
        public void ShowErrStatus(string msg)
        {
            try
            {
                this.rtxErrLog.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}-{msg}\r\n");
                this.rtxErrLog.ScrollToCaret();
                if (rtxErrLog.Lines.Length >= 20000)
                {
                    rtxErrLog.Clear();
                }
            }
            catch (Exception ex)
            {
                log.Error($"ShowExecStatus()出现错误{ex.ToString()}");
            }
        }

        /// <summary>
        /// 指令查找
        /// </summary>
        private void GetTaskCmd()
        {
            try
            {
                var dt = DbAction.Instance.GetCmdByCrnNo(McConfig.Instance.CrnName);
                this.dgvCmd.DataSource = dt;
            }
            catch (Exception ex)
            {
                log.Error($"GetTaskCmd()出现错误{ex.ToString()}");
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void Init()
        {
            #region 添加数据列
            DataGridViewTextBoxColumn columns = new DataGridViewTextBoxColumn();
            columns.Name = "TRANSFER_NO";
            columns.HeaderText = "输送设备号";
            columns.DataPropertyName = "TRANSFER_NO";
            columns.Width = 100;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "OBJID";
            columns.HeaderText = "指令编号";
            columns.DataPropertyName = "OBJID";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "TASK_NO";
            columns.HeaderText = "任务编号";
            columns.DataPropertyName = "TASK_NO";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "CMD_TYPE";
            columns.HeaderText = "指令类型";
            columns.DataPropertyName = "CMD_TYPE";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "CMD_STEP";
            columns.HeaderText = "指令步骤";
            columns.DataPropertyName = "CMD_STEP";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "SLOC_TYPE";
            columns.HeaderText = "源位置类型";
            columns.DataPropertyName = "SLOC_TYPE";
            columns.Width = 100;
            columns.ReadOnly = false;
            columns.Visible = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "SLOC_NO";
            columns.HeaderText = "源位置";
            columns.DataPropertyName = "SLOC_NO";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "SLOC_PLC_NO";
            columns.HeaderText = "源位置PLC";
            columns.DataPropertyName = "SLOC_PLC_NO";
            columns.Width = 100;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "ELOC_TYPE";
            columns.HeaderText = "目标位置类型";
            columns.DataPropertyName = "ELOC_TYPE";
            columns.Width = 100;
            columns.ReadOnly = false;
            columns.Visible = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "ELOC_NO";
            columns.HeaderText = "目标位置";
            columns.DataPropertyName = "ELOC_NO";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "ELOC_PLC_NO";
            columns.HeaderText = "目标位置PLC";
            columns.DataPropertyName = "ELOC_PLC_NO";
            columns.Width = 100;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "PRODUCT_NO";
            columns.HeaderText = "物料编号";
            columns.DataPropertyName = "PRODUCT_NO";
            columns.Width = 110;
            columns.ReadOnly = false;
            columns.Visible = false;
            this.dgvCmd.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "PALLET_NO";
            columns.HeaderText = "托盘编号";
            columns.DataPropertyName = "PALLET_NO";
            columns.Width = 80;
            columns.ReadOnly = false;
            this.dgvCmd.Columns.Add(columns);
            #endregion
        }
        #endregion

        private void timer_Monitor_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!DbAction.Instance.GetDbTime())
                {
                    ShowFormData.Instance.ShowFormInfo(new ShowInfoData("N", InfoType.dbConn));
                    return;
                }
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("Y", InfoType.dbConn));
                //业务处理
                BizHandle.Instance.BizListen();
            }
            catch (Exception ex)
            {
                log.Error($"[TIMER]执行异常：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 更新记录设备状态
        /// </summary>
        private void RecordEquipStatus()
        {
            try
            {
                while(true)
                {
                    //更新堆垛机监控状态
                    DbAction.Instance.UpdateCrnStatus();
                    //记录设备报警
                    DbAction.Instance.RecordSrmFaultInfo();

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                log.Error($"记录设备状态异常：{ex.ToString()}");
            }
        }
    }
}
