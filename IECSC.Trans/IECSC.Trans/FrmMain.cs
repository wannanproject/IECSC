using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MSTL.LogAgent;
using IECSC.Trans.Common;
using IECSC.Trans.Entity;
using IECSC.Trans.DB;
using System.Linq;
using System.Drawing;

namespace IECSC.Trans
{
    public partial class FrmMain : Form
    {
        #region 系统日志
        private ILog Log { get { return MSTL.LogAgent.Log.Store[GetType().FullName]; } }
        #endregion

        //Color of flag
        private Color VALIDATECOLOR = Color.LightGreen;
        private Color UNVALIDATECOLOR = Color.White;
        private Color DEVICEFALTCOLOR = Color.Red;
        private Color DEVICEAUTOCOLOR = Color.LightGreen;
        private Color DEVICEMANUALCOLOR = Color.LightSteelBlue;
        private Color DEVICENORMALCOLOR = Color.LightGreen;

        /// <summary>
        /// 站台监控列表
        /// </summary>
        public IEnumerable<TransLocStatusInfo> MonitorStatusInfo;
        public IEnumerable<TransferTask> MonitorCmdInfo;
        public FrmMain()
        {
            InitializeComponent();
            this.Text = McConfig.Instance.AppName;
            this.tsslAppStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //禁用自动生成列
            this.DgvLoc.AutoGenerateColumns = false;
            this.DgvCmdInfo.AutoGenerateColumns = false;
            //打开双缓冲，减少闪烁
            this.DgvLoc.SetDoubleBuffered(true);
            this.DgvCmdInfo.SetDoubleBuffered(true);
            //渲染DataGridView
            this.DgvLoc.Invalidated += DgvLoc_Invalidated;
            InitGrid();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //绑定日志显示类
            ShowFormData.Instance.OnAppDtoData += Instance_OnAppDtoData;

            //初始化
            string errMsg = string.Empty;

            if (!BizHandle.Instance.Init(ref errMsg))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData(errMsg, ShowInfoTypeEnum.logInfo));
                return;
            }
            //选中所有站台类型
            SetAllLocTypeChecked(true);

            this.MonitorStatusInfo = BizHandle.Instance.locStatuesDic.Values.ToList();
            string locNo = this.MonitorStatusInfo.First().LocNo;
            if (!string.IsNullOrEmpty(locNo))
            {
                RefreshCmdOnShow(locNo);
            }
            //站台状态监控
            this.DgvLoc.DataSource = MonitorStatusInfo;
            this.DgvCmdInfo.DataSource = MonitorCmdInfo;
           
            //业务循环
            this.timerBusiness.Start();
        }

        #region  显示界面信息
        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_OnAppDtoData(object sender, AppDataEventArgs e)
        {
            switch (e.AppData.InfoType)
            {
                case ShowInfoTypeEnum.logInfo:
                    if (this.rtb_log.TextLength > 5000)
                    {
                        this.rtb_log.Clear();
                    }
                    rtb_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff：") + e.AppData.StringInfo + "\r\n");
                    rtb_log.ScrollToCaret();
                    break;
                case ShowInfoTypeEnum.plcConn:
                    if (e.AppData.StringInfo == "已连接")
                    {
                        this.tssl_PLCConnectStatus.BackColor = Color.Green;
                    }
                    else
                    {
                        this.tssl_PLCConnectStatus.BackColor = Color.Red;
                    }
                    break;
                case ShowInfoTypeEnum.DbConn:
                    if (e.AppData.StringInfo == "已连接")
                    {
                        this.tsslDbConnectStatus.BackColor = Color.Green;
                    }
                    else
                    {
                        this.tsslDbConnectStatus.BackColor = Color.Red;
                    }
                    break;
                case ShowInfoTypeEnum.CirTime:
                    this.tsslCirTime.Text = e.AppData.StringInfo;
                    break;
                default:
                    if (this.rtb_log.TextLength > 5000)
                    {
                        this.rtb_log.Clear();
                    }
                    rtb_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff：") + e.AppData.StringInfo + "【未知处理类型】" + "\r\n");
                    rtb_log.ScrollToCaret();
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 窗口关闭时给出提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(@"关闭后输送线请求将无法处理，确认关闭吗？", @"警告", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 业务时钟TICK事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBusiness_Tick(object sender, EventArgs e)
        {
            //判断数据库是否连接
            if (!DbAction.Instance.GetDbTime())
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("未连接", ShowInfoTypeEnum.DbConn));
                return;
            }
            else
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("已连接", ShowInfoTypeEnum.DbConn));
            }
            BizHandle.Instance.HandleBussiness();
            this.DgvLoc.Refresh();
            this.DgvCmdInfo.Refresh();
        }

        #region 界面绘制
        /// <summary>
        /// 初始化DataGridview
        /// </summary>
        private void InitGrid()
        {
            #region 添加DgvLoc数据列
            DataGridViewTextBoxColumn columns = new DataGridViewTextBoxColumn();
            columns.Name = "LocNo";
            columns.HeaderText = "站台编号";
            columns.DataPropertyName = "LocNo";
            columns.Width = 90;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "LocPlcNo";
            columns.HeaderText = "PLC编号";
            columns.DataPropertyName = "LocPlcNo";
            columns.Width = 80;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusAutomatic";
            columns.HeaderText = "自动";
            columns.DataPropertyName = "StatusAutomatic";
            columns.Width = 40;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusLoad";
            columns.HeaderText = "有载";
            columns.DataPropertyName = "StatusLoad";
            columns.Width = 40;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusFault";
            columns.HeaderText = "故障";
            columns.DataPropertyName = "StatusFault";
            columns.Width = 40;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusRequestTask";
            columns.HeaderText = "请求任务";
            columns.DataPropertyName = "StatusRequestTask";
            columns.Width = 90;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusFreeAndPut";
            columns.HeaderText = "站点空闲";
            columns.DataPropertyName = "StatusFreeAndPut";
            columns.Width = 90;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "StatusBusyAndTake";
            columns.HeaderText = "请求取货";
            columns.DataPropertyName = "StatusBusyAndTake";
            columns.Width = 100;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "PalletNo";
            columns.HeaderText = "托盘编号";
            columns.DataPropertyName = "PalletNo";
            columns.Width = 120;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "TaskNo";
            columns.HeaderText = "任务号";
            columns.DataPropertyName = "TaskNo";
            columns.Width = 70;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "LocTypeDesc";
            columns.HeaderText = "站台类型";
            columns.DataPropertyName = "LocTypeDesc";
            columns.Width = 105;
            columns.ReadOnly = false;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "BusinessDesc";
            columns.HeaderText = "业务描述";
            columns.DataPropertyName = "BusinessDesc";
            columns.Width = 100;
            columns.ReadOnly = false;
            this.DgvLoc.Columns.Add(columns);

            columns = new DataGridViewTextBoxColumn();
            columns.Name = "BusinessHandleTime";
            columns.HeaderText = "处理时间";
            columns.DataPropertyName = "BusinessHandleTime";
            columns.Width = 100;
            columns.ReadOnly = false;
            columns.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgvLoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvLoc.Columns.Add(columns);
            
            #endregion

            #region 添加DgvCmdInfo数据列
            DataGridViewTextBoxColumn columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "LocNoForTask";
            columnsCmd.HeaderText = "站台编号";
            columnsCmd.DataPropertyName = "SLOC_NO";
            columnsCmd.Width = 100;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "TaskNoForTask";
            columnsCmd.HeaderText = "任务号";
            columnsCmd.DataPropertyName = "TASK_NO";
            columnsCmd.Width = 70;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "MaterialNoForTask";
            columnsCmd.HeaderText = "产品号";
            columnsCmd.DataPropertyName = "MATERIAL_NO";
            columnsCmd.Width = 130;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "PalletNoForTask";
            columnsCmd.HeaderText = "托盘编号";
            columnsCmd.DataPropertyName = "PALLET_NO";
            columnsCmd.Width = 120;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "SourceAddress";
            columnsCmd.HeaderText = "起始地址";
            columnsCmd.DataPropertyName = "SLOC_PLC_NO";
            columnsCmd.Width = 70;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "TargetAddress";
            columnsCmd.HeaderText = "目的地址";
            columnsCmd.DataPropertyName = "ELOC_PLC_NO";
            columnsCmd.Width = 90;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "CmdStepForTask";
            columnsCmd.HeaderText = "指令步骤";
            columnsCmd.DataPropertyName = "CmdStepForShow";
            columnsCmd.Width = 90;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "CmdTypeForTask";
            columnsCmd.HeaderText = "指令类型";
            columnsCmd.DataPropertyName = "CmdTypeForShow";
            columnsCmd.Width = 90;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "CreatDateForTask";
            columnsCmd.HeaderText = "生成时间";
            columnsCmd.DataPropertyName = "CREATION_DATE";
            columnsCmd.Width = 120;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "CmdObjidForTask";
            columnsCmd.HeaderText = "指令Objid";
            columnsCmd.DataPropertyName = "OBJID";
            columnsCmd.Width = 120;
            columnsCmd.Visible = false;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);

            columnsCmd = new DataGridViewTextBoxColumn();
            columnsCmd.Name = "TaskGuifForTask";
            columnsCmd.HeaderText = "任务GUID";
            columnsCmd.DataPropertyName = "TASK_GUID";
            columnsCmd.Width = 120;
            columnsCmd.Visible = false;
            columnsCmd.ReadOnly = false;
            columnsCmd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DgvCmdInfo.Columns.Add(columnsCmd);
            #endregion
        }

        /// <summary>
        /// 任务添加序号
        /// </summary>
        private void DgvCmdInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, DgvCmdInfo.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), DgvCmdInfo.RowHeadersDefaultCellStyle.Font, rectangle, DgvCmdInfo.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        #region 站台类型选择时间处理
        /// <summary>
        /// 勾选站台事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLoc_MouseClick(object sender, MouseEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.Name == "cbxAll")
            {
                SetAllLocTypeChecked(checkBox.Checked);
            }

            string locTypeStr = string.Empty;
            if (cbxFurLoc.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.FurLoc + ",";
            }
            if (cbxTransOut.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.SendingLoc + ",";
            }
            if (cbxTransIn.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.StockLoc + ",";
                locTypeStr += (int)LocationTypeEnum.GangtryLoc + ",";
            }
            if (cbxPdInLoc.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.PdInLoc + ",";
            }
            if (cbxPdOutLoc.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.PdOutLoc + ",";
            }
            if (cbxExpLoc.Checked)
            {
                locTypeStr += (int)LocationTypeEnum.ExpLoc + ",";
            }
            //根据类型过滤站台

            this.MonitorStatusInfo = BizHandle.Instance.locStatuesDic.Values.Where(p => locTypeStr.Contains(p.LocTypeNo.ToString()));
            this.DgvLoc.DataSource = this.MonitorStatusInfo.ToList();
        }

        /// <summary>
        /// 设置全选
        /// </summary>
        /// <param name="checkStatus"></param>
        private void SetAllLocTypeChecked(bool checkStatus)
        {
            cbxFurLoc.Checked = checkStatus;
            cbxAll.Checked = checkStatus;
            cbxTransIn.Checked = checkStatus;
            cbxPdInLoc.Checked = checkStatus;
            cbxPdOutLoc.Checked = checkStatus;
            cbxExpLoc.Checked = checkStatus;
            cbxTransOut.Checked = checkStatus;
        }
        #endregion

        #region DataGridView颜色渲染

        /// <summary>
        /// 站台状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLoc_Invalidated(object sender, InvalidateEventArgs e)
        {
            for (int i = 0; i < DgvLoc.Rows.Count; i++)
            {
                //请求任务
                if (DgvLoc.Rows[i].Cells["StatusRequestTask"].Value.ToString() == "1")
                {
                    DgvLoc.Rows[i].Cells["StatusRequestTask"].Style.BackColor = VALIDATECOLOR;
                }
                else
                {
                    DgvLoc.Rows[i].Cells["StatusRequestTask"].Style.BackColor = UNVALIDATECOLOR;
                }
                //有货需取货
                if (DgvLoc.Rows[i].Cells["StatusBusyAndTake"].Value.ToString() == "1")
                {
                    DgvLoc.Rows[i].Cells["StatusBusyAndTake"].Style.BackColor = VALIDATECOLOR;
                }
                else
                {
                    DgvLoc.Rows[i].Cells["StatusBusyAndTake"].Style.BackColor = UNVALIDATECOLOR;
                }
                //故障
                if (DgvLoc.Rows[i].Cells["StatusFault"].Value.ToString() == "1")
                {
                    DgvLoc.Rows[i].Cells["StatusFault"].Style.BackColor = DEVICEFALTCOLOR;
                }
                else if (DgvLoc.Rows[i].Cells["StatusFault"].Value.ToString() == "0")
                {
                    DgvLoc.Rows[i].Cells["StatusFault"].Style.BackColor = DEVICENORMALCOLOR;
                }
                //自动
                if (DgvLoc.Rows[i].Cells["StatusAutomatic"].Value.ToString() == "1")
                {
                    DgvLoc.Rows[i].Cells["StatusAutomatic"].Style.BackColor = VALIDATECOLOR;
                }
                else
                {
                    DgvLoc.Rows[i].Cells["StatusAutomatic"].Style.BackColor = UNVALIDATECOLOR;
                }
                //有载
                if (DgvLoc.Rows[i].Cells["StatusLoad"].Value.ToString() == "1")
                {
                    DgvLoc.Rows[i].Cells["StatusLoad"].Style.BackColor = VALIDATECOLOR;
                }
                else
                {
                    DgvLoc.Rows[i].Cells["StatusLoad"].Style.BackColor = UNVALIDATECOLOR;
                }
            }
        }
        #endregion

        /// <summary>
        /// 任务列表状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = DgvLoc.CurrentRow;
            if (row == null)
            {
                return;
            }
            int index = row.Index;
            string locNo = DgvLoc.Rows[index].Cells["LocNo"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值  
            if (!string.IsNullOrEmpty(locNo))
            {
                IEnumerable<TransferTask> transferTask = DbAction.Instance.GetTransforTask();
                this.MonitorCmdInfo = transferTask.Where(p => p.SLOC_NO == locNo);
                this.DgvCmdInfo.DataSource = this.MonitorCmdInfo.ToList();
            }
        }

        /// <summary>
        /// 刷新任务界面
        /// </summary>
        /// <param name="locNo"></param>
        private void RefreshCmdOnShow(string locNo)
        {
            IEnumerable<TransferTask> transferTask = DbAction.Instance.GetTransforTask();
            if (!string.IsNullOrEmpty(locNo))
            {
                //获取单元格列名为‘Id’的值
                this.MonitorCmdInfo = transferTask.Where(p => p.SLOC_NO == locNo);
                this.DgvCmdInfo.DataSource = this.MonitorCmdInfo.ToList();
            }
            else
            {
                this.DgvCmdInfo.DataSource = transferTask;
            }
        }
        #endregion

        /// <summary>
        /// 重新查询任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var row = DgvLoc.CurrentRow;
            if (row == null)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("请选中站点!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            int index = row.Index;
            string locNo = DgvLoc.Rows[index].Cells["LocNo"].Value.ToString().Trim();
            RefreshCmdOnShow(locNo);
        }

        /// <summary>
        /// 重发任务
        /// 任务重发通过修改指令步骤为00进行
        /// 因此需要下发任务的业务实现中，需要先首先检测任务是否有待下发任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefSend_Click(object sender, EventArgs e)
        {
            var row = DgvCmdInfo.CurrentRow;
            
            if (row == null)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("请选中任务!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            int index = row.Index;
            string locNo = DgvCmdInfo.Rows[index].Cells["LocNoForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            string taskNo = DgvCmdInfo.Rows[index].Cells["TaskNoForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            string palletNo = DgvCmdInfo.Rows[index].Cells["PalletNoForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            string cmdObjid = DgvCmdInfo.Rows[index].Cells["CmdObjidForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            if (string.IsNullOrEmpty(locNo) || string.IsNullOrEmpty(palletNo) || string.IsNullOrEmpty(cmdObjid) || string.IsNullOrEmpty(taskNo))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】任务信息不全，不能重发!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            //判断当前站台是否在可处理范围内
            if (!BizHandle.Instance.locStatuesDic.ContainsKey(locNo))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{locNo}】无法处理当前站台信息", ShowInfoTypeEnum.logInfo));
                return;
            }
            //判断当前站台是否存在工装，工装 是否与任务一致
            var locInfo = BizHandle.Instance.locStatuesDic[locNo];
            if (locInfo.PalletNo != palletNo || locInfo.StatusLoad == 0)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{locNo}】站台当前工装与任务不符，不能重发", ShowInfoTypeEnum.logInfo));
                return;
            }
            if (MessageBox.Show("是否重发?", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】取消重发!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            if (!DbAction.Instance.UpdateCmdStatusByManual(cmdObjid, "00"))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】任务信重发失败!!!", ShowInfoTypeEnum.logInfo));
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】任务信重发成功!!!", ShowInfoTypeEnum.logInfo));
            RefreshCmdOnShow(locNo);
        }

        /// <summary>
        /// 任务结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCmdFinish_Click(object sender, EventArgs e)
        {
            var row = DgvCmdInfo.CurrentRow;
            if (row == null)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("请选中任务!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            int index = row.Index;
            string locNo = DgvCmdInfo.Rows[index].Cells["LocNoForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            string taskNo = DgvCmdInfo.Rows[index].Cells["TaskNoForTask"].Value.ToString().Trim();   //获取单元格列名为‘Id’的值
            if (string.IsNullOrEmpty(locNo) ||string.IsNullOrEmpty(taskNo))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】任务信息不全，不能结束!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            if (MessageBox.Show("是否结束?", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{taskNo}】取消结束任务!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            var locStatus = BizHandle.Instance.locStatuesDic[locNo];
            //判断是否已请求指令结束
            if (locStatus.FinishObjid == 0)
            {
                //获取请求指令结束ID
                locStatus.FinishObjid = DbAction.Instance.GetObjidForFinishCmd();
            }
            //结束任务
            string result = DbAction.Instance.FinishCmd(locStatus.FinishObjid, locNo, Convert.ToInt64(taskNo));
            if (!string.IsNullOrEmpty(result))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{locNo}指令【{taskNo}】结束失败！原因：" + result, ShowInfoTypeEnum.logInfo, locNo));
                return;
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{locNo}指令【{taskNo}】结束成功！", ShowInfoTypeEnum.logInfo, locNo));
            //重新刷新任务
            RefreshCmdOnShow(locNo);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteCmd_Click(object sender, EventArgs e)
        {
            var row = DgvCmdInfo.CurrentRow;
            if (row == null)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData("请选中任务!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            int index = row.Index;
            //删除任务
            string locNo = DgvCmdInfo.Rows[index].Cells["LocNoForTask"].Value.ToString().Trim();
            string taskNo = DgvCmdInfo.Rows[index].Cells["TaskNoForTask"].Value.ToString().Trim();
            if (string.IsNullOrEmpty(taskNo))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{locNo}无删除标志，无法删除!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            if (MessageBox.Show("是否删除?", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"【{locNo}】取消删除任务!!!", ShowInfoTypeEnum.logInfo));
                return;
            }
            //结束任务
            if (!DbAction.Instance.DelTask(int.Parse(taskNo)))
            {
                ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{locNo}指令【{taskNo}】删除失败！", ShowInfoTypeEnum.logInfo, locNo));
                return;
            }
            ShowFormData.Instance.ShowFormInfo(new ShowInfoData($"{locNo}指令【{taskNo}】删除成功！", ShowInfoTypeEnum.logInfo, locNo));
            //刷新界面
            RefreshCmdOnShow(locNo);
        }
    }
}
