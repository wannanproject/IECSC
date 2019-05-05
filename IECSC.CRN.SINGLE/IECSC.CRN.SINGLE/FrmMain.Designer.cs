namespace IECSC.CRN.SINGLE
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timer_Monitor = new System.Windows.Forms.Timer(this.components);
            this.sts_Status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.plcConnStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_DBConnT = new System.Windows.Forms.ToolStripStatusLabel();
            this.dbConnStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.connTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbCrnItems = new System.Windows.Forms.GroupBox();
            this.dgvCrn = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCmd = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefSend = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbExecLog = new System.Windows.Forms.RichTextBox();
            this.rtxErrLog = new System.Windows.Forms.RichTextBox();
            this.sts_Status.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbCrnItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrn)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_Monitor
            // 
            this.timer_Monitor.Interval = 1000;
            this.timer_Monitor.Tick += new System.EventHandler(this.timer_Monitor_Tick);
            // 
            // sts_Status
            // 
            this.sts_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.plcConnStatus,
            this.tssl_DBConnT,
            this.dbConnStatus,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel3,
            this.connTime});
            this.sts_Status.Location = new System.Drawing.Point(0, 518);
            this.sts_Status.Name = "sts_Status";
            this.sts_Status.Size = new System.Drawing.Size(1043, 22);
            this.sts_Status.TabIndex = 14;
            this.sts_Status.Text = "状态栏";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(89, 17);
            this.toolStripStatusLabel1.Text = "PLC连接状态：";
            // 
            // plcConnStatus
            // 
            this.plcConnStatus.Name = "plcConnStatus";
            this.plcConnStatus.Size = new System.Drawing.Size(28, 17);
            this.plcConnStatus.Text = "     ";
            // 
            // tssl_DBConnT
            // 
            this.tssl_DBConnT.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.tssl_DBConnT.Name = "tssl_DBConnT";
            this.tssl_DBConnT.Size = new System.Drawing.Size(104, 17);
            this.tssl_DBConnT.Text = "数据库连接状态：";
            // 
            // dbConnStatus
            // 
            this.dbConnStatus.Name = "dbConnStatus";
            this.dbConnStatus.Size = new System.Drawing.Size(28, 17);
            this.dbConnStatus.Text = "     ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(562, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel3.Text = "登录时间：";
            // 
            // connTime
            // 
            this.connTime.Name = "connTime";
            this.connTime.Size = new System.Drawing.Size(140, 17);
            this.connTime.Text = "yyyy-MM-dd hh:mm:ss";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.gbCrnItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 518);
            this.panel1.TabIndex = 15;
            // 
            // gbCrnItems
            // 
            this.gbCrnItems.Controls.Add(this.dgvCrn);
            this.gbCrnItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCrnItems.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbCrnItems.Location = new System.Drawing.Point(0, 0);
            this.gbCrnItems.Name = "gbCrnItems";
            this.gbCrnItems.Size = new System.Drawing.Size(282, 518);
            this.gbCrnItems.TabIndex = 1;
            this.gbCrnItems.TabStop = false;
            this.gbCrnItems.Text = "设备状态监控";
            // 
            // dgvCrn
            // 
            this.dgvCrn.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvCrn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCrn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCrn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCrn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCrn.Location = new System.Drawing.Point(3, 19);
            this.dgvCrn.Name = "dgvCrn";
            this.dgvCrn.RowHeadersVisible = false;
            this.dgvCrn.RowTemplate.Height = 23;
            this.dgvCrn.Size = new System.Drawing.Size(276, 496);
            this.dgvCrn.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(282, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 518);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Controls.Add(this.splitter2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(285, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(758, 518);
            this.panel2.TabIndex = 3;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 213);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(758, 3);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCmd);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 213);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指令信息";
            // 
            // dgvCmd
            // 
            this.dgvCmd.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvCmd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCmd.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCmd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCmd.Location = new System.Drawing.Point(3, 19);
            this.dgvCmd.MultiSelect = false;
            this.dgvCmd.Name = "dgvCmd";
            this.dgvCmd.ReadOnly = true;
            this.dgvCmd.RowHeadersVisible = false;
            this.dgvCmd.RowTemplate.Height = 23;
            this.dgvCmd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCmd.Size = new System.Drawing.Size(752, 149);
            this.dgvCmd.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnRefSend, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFinish, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 168);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(752, 42);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnRefSend
            // 
            this.btnRefSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefSend.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefSend.Image = global::IECSC.CRN.SINGLE.Properties.Resources.takeback24;
            this.btnRefSend.Location = new System.Drawing.Point(455, 3);
            this.btnRefSend.Name = "btnRefSend";
            this.btnRefSend.Size = new System.Drawing.Size(94, 36);
            this.btnRefSend.TabIndex = 0;
            this.btnRefSend.Text = "任务重发";
            this.btnRefSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefSend.UseVisualStyleBackColor = true;
            this.btnRefSend.Click += new System.EventHandler(this.btnRefSend_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinish.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFinish.Image = global::IECSC.CRN.SINGLE.Properties.Resources.check;
            this.btnFinish.Location = new System.Drawing.Point(555, 3);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(94, 36);
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Text = "任务结束";
            this.btnFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnDel
            // 
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.Image = global::IECSC.CRN.SINGLE.Properties.Resources.clear_icon_24;
            this.btnDel.Location = new System.Drawing.Point(655, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(94, 36);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "任务删除";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Image = global::IECSC.CRN.SINGLE.Properties.Resources.refresh_24;
            this.btnSearch.Location = new System.Drawing.Point(355, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 36);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "指令查询";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 216);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(758, 302);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbExecLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 296);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "执行日志";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtxErrLog);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(382, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 296);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "异常日志";
            // 
            // rtbExecLog
            // 
            this.rtbExecLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbExecLog.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbExecLog.Location = new System.Drawing.Point(3, 19);
            this.rtbExecLog.Name = "rtbExecLog";
            this.rtbExecLog.Size = new System.Drawing.Size(367, 274);
            this.rtbExecLog.TabIndex = 4;
            this.rtbExecLog.Text = "";
            // 
            // rtxErrLog
            // 
            this.rtxErrLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxErrLog.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxErrLog.Location = new System.Drawing.Point(3, 19);
            this.rtxErrLog.Name = "rtxErrLog";
            this.rtxErrLog.Size = new System.Drawing.Size(367, 274);
            this.rtxErrLog.TabIndex = 4;
            this.rtxErrLog.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 540);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sts_Status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "堆垛机监控程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.sts_Status.ResumeLayout(false);
            this.sts_Status.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbCrnItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrn)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer_Monitor;
        public System.Windows.Forms.StatusStrip sts_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel plcConnStatus;
        private System.Windows.Forms.ToolStripStatusLabel tssl_DBConnT;
        public System.Windows.Forms.ToolStripStatusLabel dbConnStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        public System.Windows.Forms.ToolStripStatusLabel connTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCmd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRefSend;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox gbCrnItems;
        private System.Windows.Forms.DataGridView dgvCrn;
        private System.Windows.Forms.RichTextBox rtxErrLog;
        private System.Windows.Forms.RichTextBox rtbExecLog;
    }
}

