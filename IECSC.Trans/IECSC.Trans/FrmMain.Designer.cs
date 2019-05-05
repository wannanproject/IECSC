namespace IECSC.Trans
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.sts_Status = new System.Windows.Forms.StatusStrip();
            this.tssl_PLCConnT = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_PLCConnectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_DBConnT = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDbConnectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCirTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_AppStartT = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslAppStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DgvLoc = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxAll = new System.Windows.Forms.CheckBox();
            this.cbxPdOutLoc = new System.Windows.Forms.CheckBox();
            this.cbxPdInLoc = new System.Windows.Forms.CheckBox();
            this.cbxFurLoc = new System.Windows.Forms.CheckBox();
            this.cbxExpLoc = new System.Windows.Forms.CheckBox();
            this.cbxTransIn = new System.Windows.Forms.CheckBox();
            this.cbxTransOut = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DgvCmdInfo = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefSend = new System.Windows.Forms.Button();
            this.ButtonCmdFinish = new System.Windows.Forms.Button();
            this.ButtonDeleteCmd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.timerBusiness = new System.Windows.Forms.Timer(this.components);
            this.sts_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLoc)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCmdInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sts_Status
            // 
            this.sts_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_PLCConnT,
            this.tssl_PLCConnectStatus,
            this.tssl_DBConnT,
            this.tsslDbConnectStatus,
            this.toolStripStatusLabel5,
            this.TsslTime,
            this.tsslCirTime,
            this.tssl_AppStartT,
            this.tsslAppStartTime,
            this.toolStripStatusLabel1});
            this.sts_Status.Location = new System.Drawing.Point(0, 463);
            this.sts_Status.Name = "sts_Status";
            this.sts_Status.Size = new System.Drawing.Size(999, 22);
            this.sts_Status.TabIndex = 1;
            this.sts_Status.Text = "状态栏";
            // 
            // tssl_PLCConnT
            // 
            this.tssl_PLCConnT.Name = "tssl_PLCConnT";
            this.tssl_PLCConnT.Size = new System.Drawing.Size(89, 17);
            this.tssl_PLCConnT.Text = "PLC连接状态：";
            // 
            // tssl_PLCConnectStatus
            // 
            this.tssl_PLCConnectStatus.BackColor = System.Drawing.Color.Red;
            this.tssl_PLCConnectStatus.Name = "tssl_PLCConnectStatus";
            this.tssl_PLCConnectStatus.Size = new System.Drawing.Size(32, 17);
            this.tssl_PLCConnectStatus.Text = "      ";
            this.tssl_PLCConnectStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tssl_DBConnT
            // 
            this.tssl_DBConnT.Margin = new System.Windows.Forms.Padding(30, 3, 0, 2);
            this.tssl_DBConnT.Name = "tssl_DBConnT";
            this.tssl_DBConnT.Size = new System.Drawing.Size(104, 17);
            this.tssl_DBConnT.Text = "数据库连接状态：";
            // 
            // tsslDbConnectStatus
            // 
            this.tsslDbConnectStatus.BackColor = System.Drawing.Color.Red;
            this.tsslDbConnectStatus.Name = "tsslDbConnectStatus";
            this.tsslDbConnectStatus.Size = new System.Drawing.Size(32, 17);
            this.tsslDbConnectStatus.Text = "      ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(415, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // TsslTime
            // 
            this.TsslTime.Name = "TsslTime";
            this.TsslTime.Size = new System.Drawing.Size(76, 17);
            this.TsslTime.Text = "  执行用时：";
            // 
            // tsslCirTime
            // 
            this.tsslCirTime.Name = "tsslCirTime";
            this.tsslCirTime.Size = new System.Drawing.Size(15, 17);
            this.tsslCirTime.Text = "0";
            // 
            // tssl_AppStartT
            // 
            this.tssl_AppStartT.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.tssl_AppStartT.Name = "tssl_AppStartT";
            this.tssl_AppStartT.Size = new System.Drawing.Size(92, 17);
            this.tssl_AppStartT.Text = "系统启动时间：";
            // 
            // tsslAppStartTime
            // 
            this.tsslAppStartTime.Name = "tsslAppStartTime";
            this.tsslAppStartTime.Size = new System.Drawing.Size(49, 17);
            this.tsslAppStartTime.Text = " NONE";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(999, 463);
            this.splitContainer1.SplitterDistance = 732;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(732, 463);
            this.splitContainer2.SplitterDistance = 241;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DgvLoc);
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 241);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "站点状态监控";
            // 
            // DgvLoc
            // 
            this.DgvLoc.AllowUserToAddRows = false;
            this.DgvLoc.AllowUserToDeleteRows = false;
            this.DgvLoc.AllowUserToResizeRows = false;
            this.DgvLoc.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvLoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvLoc.Location = new System.Drawing.Point(3, 47);
            this.DgvLoc.MultiSelect = false;
            this.DgvLoc.Name = "DgvLoc";
            this.DgvLoc.ReadOnly = true;
            this.DgvLoc.RowHeadersVisible = false;
            this.DgvLoc.RowTemplate.Height = 23;
            this.DgvLoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvLoc.Size = new System.Drawing.Size(726, 191);
            this.DgvLoc.TabIndex = 1;
            //this.DgvLoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLoc_CellClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxAll, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxPdOutLoc, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxPdInLoc, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxFurLoc, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxExpLoc, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxTransIn, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxTransOut, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(726, 28);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "站点类型:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxAll
            // 
            this.cbxAll.AutoSize = true;
            this.cbxAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxAll.Location = new System.Drawing.Point(73, 3);
            this.cbxAll.Name = "cbxAll";
            this.cbxAll.Size = new System.Drawing.Size(54, 22);
            this.cbxAll.TabIndex = 18;
            this.cbxAll.Text = "全部";
            this.cbxAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxAll.UseVisualStyleBackColor = true;
            this.cbxAll.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxPdOutLoc
            // 
            this.cbxPdOutLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPdOutLoc.AutoSize = true;
            this.cbxPdOutLoc.Location = new System.Drawing.Point(367, 3);
            this.cbxPdOutLoc.Name = "cbxPdOutLoc";
            this.cbxPdOutLoc.Size = new System.Drawing.Size(93, 21);
            this.cbxPdOutLoc.TabIndex = 21;
            this.cbxPdOutLoc.Text = "PDOUT站台";
            this.cbxPdOutLoc.UseVisualStyleBackColor = true;
            this.cbxPdOutLoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxPdInLoc
            // 
            this.cbxPdInLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPdInLoc.AutoSize = true;
            this.cbxPdInLoc.Location = new System.Drawing.Point(277, 3);
            this.cbxPdInLoc.Name = "cbxPdInLoc";
            this.cbxPdInLoc.Size = new System.Drawing.Size(84, 21);
            this.cbxPdInLoc.TabIndex = 20;
            this.cbxPdInLoc.Text = "PDIN站台";
            this.cbxPdInLoc.UseVisualStyleBackColor = true;
            this.cbxPdInLoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxFurLoc
            // 
            this.cbxFurLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFurLoc.AutoSize = true;
            this.cbxFurLoc.Location = new System.Drawing.Point(133, 3);
            this.cbxFurLoc.Name = "cbxFurLoc";
            this.cbxFurLoc.Size = new System.Drawing.Size(63, 21);
            this.cbxFurLoc.TabIndex = 19;
            this.cbxFurLoc.Text = "叠盘机";
            this.cbxFurLoc.UseVisualStyleBackColor = true;
            this.cbxFurLoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxExpLoc
            // 
            this.cbxExpLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxExpLoc.AutoSize = true;
            this.cbxExpLoc.Location = new System.Drawing.Point(202, 3);
            this.cbxExpLoc.Name = "cbxExpLoc";
            this.cbxExpLoc.Size = new System.Drawing.Size(69, 21);
            this.cbxExpLoc.TabIndex = 22;
            this.cbxExpLoc.Text = "异常口";
            this.cbxExpLoc.UseVisualStyleBackColor = true;
            this.cbxExpLoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxTransIn
            // 
            this.cbxTransIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTransIn.AutoSize = true;
            this.cbxTransIn.Location = new System.Drawing.Point(466, 3);
            this.cbxTransIn.Name = "cbxTransIn";
            this.cbxTransIn.Size = new System.Drawing.Size(95, 21);
            this.cbxTransIn.TabIndex = 23;
            this.cbxTransIn.Text = "输送线入口";
            this.cbxTransIn.UseVisualStyleBackColor = true;
            this.cbxTransIn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // cbxTransOut
            // 
            this.cbxTransOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTransOut.AutoSize = true;
            this.cbxTransOut.Location = new System.Drawing.Point(567, 3);
            this.cbxTransOut.Name = "cbxTransOut";
            this.cbxTransOut.Size = new System.Drawing.Size(156, 21);
            this.cbxTransOut.TabIndex = 24;
            this.cbxTransOut.Text = "输送线出口";
            this.cbxTransOut.UseVisualStyleBackColor = true;
            this.cbxTransOut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbxLoc_MouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DgvCmdInfo);
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(732, 218);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "指令信息";
            // 
            // DgvCmdInfo
            // 
            this.DgvCmdInfo.AllowUserToAddRows = false;
            this.DgvCmdInfo.AllowUserToDeleteRows = false;
            this.DgvCmdInfo.AllowUserToResizeRows = false;
            this.DgvCmdInfo.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCmdInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvCmdInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvCmdInfo.Location = new System.Drawing.Point(3, 19);
            this.DgvCmdInfo.MultiSelect = false;
            this.DgvCmdInfo.Name = "DgvCmdInfo";
            this.DgvCmdInfo.ReadOnly = true;
            this.DgvCmdInfo.RowTemplate.Height = 23;
            this.DgvCmdInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCmdInfo.Size = new System.Drawing.Size(726, 154);
            this.DgvCmdInfo.TabIndex = 1;
            this.DgvCmdInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvCmdInfo_RowPostPaint);
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
            this.tableLayoutPanel1.Controls.Add(this.ButtonCmdFinish, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonDeleteCmd, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 173);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(726, 42);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnRefSend
            // 
            this.btnRefSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefSend.Image = global::IECSC.Trans.Properties.Resources.takeback24;
            this.btnRefSend.Location = new System.Drawing.Point(429, 3);
            this.btnRefSend.Name = "btnRefSend";
            this.btnRefSend.Size = new System.Drawing.Size(94, 36);
            this.btnRefSend.TabIndex = 0;
            this.btnRefSend.Text = "任务重发";
            this.btnRefSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefSend.UseVisualStyleBackColor = true;
            this.btnRefSend.Click += new System.EventHandler(this.BtnRefSend_Click);
            // 
            // ButtonCmdFinish
            // 
            this.ButtonCmdFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonCmdFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonCmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonCmdFinish.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonCmdFinish.Image = global::IECSC.Trans.Properties.Resources.check;
            this.ButtonCmdFinish.Location = new System.Drawing.Point(529, 3);
            this.ButtonCmdFinish.Name = "ButtonCmdFinish";
            this.ButtonCmdFinish.Size = new System.Drawing.Size(94, 36);
            this.ButtonCmdFinish.TabIndex = 1;
            this.ButtonCmdFinish.Text = "任务结束";
            this.ButtonCmdFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonCmdFinish.UseVisualStyleBackColor = true;
            this.ButtonCmdFinish.Click += new System.EventHandler(this.ButtonCmdFinish_Click);
            // 
            // ButtonDeleteCmd
            // 
            this.ButtonDeleteCmd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDeleteCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonDeleteCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDeleteCmd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonDeleteCmd.Image = global::IECSC.Trans.Properties.Resources.clear_icon_24;
            this.ButtonDeleteCmd.Location = new System.Drawing.Point(629, 3);
            this.ButtonDeleteCmd.Name = "ButtonDeleteCmd";
            this.ButtonDeleteCmd.Size = new System.Drawing.Size(94, 36);
            this.ButtonDeleteCmd.TabIndex = 2;
            this.ButtonDeleteCmd.Text = "任务删除";
            this.ButtonDeleteCmd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonDeleteCmd.UseVisualStyleBackColor = true;
            this.ButtonDeleteCmd.Click += new System.EventHandler(this.ButtonDeleteCmd_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Image = global::IECSC.Trans.Properties.Resources.refresh_24;
            this.btnSearch.Location = new System.Drawing.Point(329, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 36);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "指令查询";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtb_log);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 463);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "监控日志";
            // 
            // rtb_log
            // 
            this.rtb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_log.Location = new System.Drawing.Point(3, 19);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(257, 441);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.Text = "";
            // 
            // timerBusiness
            // 
            this.timerBusiness.Interval = 1000;
            this.timerBusiness.Tick += new System.EventHandler(this.timerBusiness_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.sts_Status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.sts_Status.ResumeLayout(false);
            this.sts_Status.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvLoc)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvCmdInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sts_Status;
        private System.Windows.Forms.ToolStripStatusLabel tssl_PLCConnT;
        private System.Windows.Forms.ToolStripStatusLabel tssl_PLCConnectStatus;
        private System.Windows.Forms.ToolStripStatusLabel tssl_DBConnT;
        public System.Windows.Forms.ToolStripStatusLabel tsslDbConnectStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel TsslTime;
        private System.Windows.Forms.ToolStripStatusLabel tsslCirTime;
        private System.Windows.Forms.ToolStripStatusLabel tssl_AppStartT;
        private System.Windows.Forms.ToolStripStatusLabel tsslAppStartTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DataGridView DgvCmdInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRefSend;
        private System.Windows.Forms.Button ButtonCmdFinish;
        private System.Windows.Forms.Button ButtonDeleteCmd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView DgvLoc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxAll;
        private System.Windows.Forms.CheckBox cbxFurLoc;
        private System.Windows.Forms.CheckBox cbxPdInLoc;
        private System.Windows.Forms.CheckBox cbxPdOutLoc;
        private System.Windows.Forms.CheckBox cbxExpLoc;
        private System.Windows.Forms.CheckBox cbxTransIn;
        private System.Windows.Forms.CheckBox cbxTransOut;
        private System.Windows.Forms.Timer timerBusiness;
    }
}