
namespace KCOM
{
	partial class Form1
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
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer_AutoSnd = new System.Windows.Forms.Timer(this.components);
            this.timer_ColorShow = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_ShowIP = new System.Windows.Forms.Label();
            this.button_CleanNetSnd = new System.Windows.Forms.Button();
            this.button_CleanNetRec = new System.Windows.Forms.Button();
            this.textBox_NetSend = new System.Windows.Forms.TextBox();
            this.button_NetSend = new System.Windows.Forms.Button();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP4 = new System.Windows.Forms.TextBox();
            this.textBox_IP3 = new System.Windows.Forms.TextBox();
            this.textBox_IP2 = new System.Windows.Forms.TextBox();
            this.textBox_IP1 = new System.Windows.Forms.TextBox();
            this.textBox_NetRecv = new System.Windows.Forms.TextBox();
            this.button_NetRun = new System.Windows.Forms.Button();
            this.button_NetPoint = new System.Windows.Forms.Button();
            this.checkBox_tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox_BitCal = new System.Windows.Forms.GroupBox();
            this.textBox_bit = new System.Windows.Forms.TextBox();
            this.button_Cal = new System.Windows.Forms.Button();
            this.textBox_Console = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_COMOpen = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label_ClearRec = new System.Windows.Forms.Label();
            this.comboBox_COMStopBit = new System.Windows.Forms.ComboBox();
            this.windows_Height = new System.Windows.Forms.TextBox();
            this.windows_Width = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_COMDataBit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_chkWindowsSize = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_COMCheckBit = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_COMBaudrate = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_COMNumber = new System.Windows.Forms.ComboBox();
            this.groupBox_COMSnd = new System.Windows.Forms.GroupBox();
            this.checkBox_Cmdline = new System.Windows.Forms.CheckBox();
            this.label_com_running = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_AddTime = new System.Windows.Forms.Button();
            this.checkBox_EnAutoSndTimer = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_N100ms = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_ASCIISend = new System.Windows.Forms.Button();
            this.button_CleanSND = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label_Send_Bytes = new System.Windows.Forms.Label();
            this.textBox_ComSnd = new System.Windows.Forms.TextBox();
            this.button_SendData = new System.Windows.Forms.Button();
            this.groupBox_COMRec = new System.Windows.Forms.GroupBox();
            this.checkBox_CursorMove = new System.Windows.Forms.CheckBox();
            this.checkBox_LockRecLen = new System.Windows.Forms.CheckBox();
            this.button_CreateLog = new System.Windows.Forms.Button();
            this.checkBox_Color = new System.Windows.Forms.CheckBox();
            this.button_FontSize = new System.Windows.Forms.Button();
            this.button_FontBigger = new System.Windows.Forms.Button();
            this.button_FontSmaller = new System.Windows.Forms.Button();
            this.button_SaveLog = new System.Windows.Forms.Button();
            this.button_ASCIIShow = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label_Rec_Bytes = new System.Windows.Forms.Label();
            this.textBox_ComRec = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PageTag = new System.Windows.Forms.TabControl();
            this.timer_renew_com = new System.Windows.Forms.Timer(this.components);
            this.tabPage2.SuspendLayout();
            this.checkBox_tabPage1.SuspendLayout();
            this.groupBox_BitCal.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_COMSnd.SuspendLayout();
            this.groupBox_COMRec.SuspendLayout();
            this.PageTag.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_AutoSnd
            // 
            this.timer_AutoSnd.Tick += new System.EventHandler(this.timer_AutoSnd_Tick);
            // 
            // timer_ColorShow
            // 
            this.timer_ColorShow.Tick += new System.EventHandler(this.timer_ColorShow_Tick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.label_ShowIP);
            this.tabPage2.Controls.Add(this.button_CleanNetSnd);
            this.tabPage2.Controls.Add(this.button_CleanNetRec);
            this.tabPage2.Controls.Add(this.textBox_NetSend);
            this.tabPage2.Controls.Add(this.button_NetSend);
            this.tabPage2.Controls.Add(this.label_IP);
            this.tabPage2.Controls.Add(this.textBox_IP4);
            this.tabPage2.Controls.Add(this.textBox_IP3);
            this.tabPage2.Controls.Add(this.textBox_IP2);
            this.tabPage2.Controls.Add(this.textBox_IP1);
            this.tabPage2.Controls.Add(this.textBox_NetRecv);
            this.tabPage2.Controls.Add(this.button_NetRun);
            this.tabPage2.Controls.Add(this.button_NetPoint);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1042, 546);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NetCom";
            // 
            // label_ShowIP
            // 
            this.label_ShowIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ShowIP.AutoSize = true;
            this.label_ShowIP.Location = new System.Drawing.Point(903, 187);
            this.label_ShowIP.Name = "label_ShowIP";
            this.label_ShowIP.Size = new System.Drawing.Size(23, 12);
            this.label_ShowIP.TabIndex = 12;
            this.label_ShowIP.Text = "IP:";
            // 
            // button_CleanNetSnd
            // 
            this.button_CleanNetSnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CleanNetSnd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_CleanNetSnd.ForeColor = System.Drawing.Color.Red;
            this.button_CleanNetSnd.Location = new System.Drawing.Point(898, 416);
            this.button_CleanNetSnd.Name = "button_CleanNetSnd";
            this.button_CleanNetSnd.Size = new System.Drawing.Size(134, 23);
            this.button_CleanNetSnd.TabIndex = 11;
            this.button_CleanNetSnd.Text = "发送清空";
            this.button_CleanNetSnd.UseVisualStyleBackColor = true;
            this.button_CleanNetSnd.Click += new System.EventHandler(this.button_CleanNetSnd_Click);
            // 
            // button_CleanNetRec
            // 
            this.button_CleanNetRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CleanNetRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_CleanNetRec.ForeColor = System.Drawing.Color.Red;
            this.button_CleanNetRec.Location = new System.Drawing.Point(898, 136);
            this.button_CleanNetRec.Name = "button_CleanNetRec";
            this.button_CleanNetRec.Size = new System.Drawing.Size(134, 23);
            this.button_CleanNetRec.TabIndex = 10;
            this.button_CleanNetRec.Text = "接收清空";
            this.button_CleanNetRec.UseVisualStyleBackColor = true;
            this.button_CleanNetRec.Click += new System.EventHandler(this.button_CleanNetRec_Click);
            // 
            // textBox_NetSend
            // 
            this.textBox_NetSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_NetSend.Location = new System.Drawing.Point(8, 416);
            this.textBox_NetSend.MaxLength = 0;
            this.textBox_NetSend.Multiline = true;
            this.textBox_NetSend.Name = "textBox_NetSend";
            this.textBox_NetSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_NetSend.Size = new System.Drawing.Size(884, 123);
            this.textBox_NetSend.TabIndex = 9;
            // 
            // button_NetSend
            // 
            this.button_NetSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetSend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_NetSend.ForeColor = System.Drawing.Color.Blue;
            this.button_NetSend.Location = new System.Drawing.Point(898, 445);
            this.button_NetSend.Name = "button_NetSend";
            this.button_NetSend.Size = new System.Drawing.Size(134, 23);
            this.button_NetSend.TabIndex = 8;
            this.button_NetSend.Text = "数据发送";
            this.button_NetSend.UseVisualStyleBackColor = true;
            this.button_NetSend.Click += new System.EventHandler(this.button_NetSend_Click);
            // 
            // label_IP
            // 
            this.label_IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(898, 82);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(59, 12);
            this.label_IP.TabIndex = 7;
            this.label_IP.Text = "Local IP:";
            // 
            // textBox_IP4
            // 
            this.textBox_IP4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP4.Location = new System.Drawing.Point(999, 100);
            this.textBox_IP4.Name = "textBox_IP4";
            this.textBox_IP4.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP4.TabIndex = 6;
            this.textBox_IP4.Text = "100";
            // 
            // textBox_IP3
            // 
            this.textBox_IP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP3.Location = new System.Drawing.Point(966, 100);
            this.textBox_IP3.Name = "textBox_IP3";
            this.textBox_IP3.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP3.TabIndex = 5;
            this.textBox_IP3.Text = "0";
            // 
            // textBox_IP2
            // 
            this.textBox_IP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP2.Location = new System.Drawing.Point(932, 100);
            this.textBox_IP2.Name = "textBox_IP2";
            this.textBox_IP2.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP2.TabIndex = 4;
            this.textBox_IP2.Text = "168";
            // 
            // textBox_IP1
            // 
            this.textBox_IP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP1.Location = new System.Drawing.Point(898, 100);
            this.textBox_IP1.Name = "textBox_IP1";
            this.textBox_IP1.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP1.TabIndex = 3;
            this.textBox_IP1.Text = "192";
            // 
            // textBox_NetRecv
            // 
            this.textBox_NetRecv.BackColor = System.Drawing.Color.White;
            this.textBox_NetRecv.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_NetRecv.Location = new System.Drawing.Point(8, 5);
            this.textBox_NetRecv.MaxLength = 0;
            this.textBox_NetRecv.Multiline = true;
            this.textBox_NetRecv.Name = "textBox_NetRecv";
            this.textBox_NetRecv.ReadOnly = true;
            this.textBox_NetRecv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_NetRecv.Size = new System.Drawing.Size(884, 405);
            this.textBox_NetRecv.TabIndex = 2;
            this.textBox_NetRecv.WordWrap = false;
            this.textBox_NetRecv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_NetRecv_KeyDown);
            // 
            // button_NetRun
            // 
            this.button_NetRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetRun.Location = new System.Drawing.Point(898, 46);
            this.button_NetRun.Name = "button_NetRun";
            this.button_NetRun.Size = new System.Drawing.Size(134, 23);
            this.button_NetRun.TabIndex = 1;
            this.button_NetRun.Text = "Run";
            this.button_NetRun.UseVisualStyleBackColor = true;
            this.button_NetRun.Click += new System.EventHandler(this.button_NetRun_Click);
            // 
            // button_NetPoint
            // 
            this.button_NetPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetPoint.ForeColor = System.Drawing.Color.Red;
            this.button_NetPoint.Location = new System.Drawing.Point(898, 17);
            this.button_NetPoint.Name = "button_NetPoint";
            this.button_NetPoint.Size = new System.Drawing.Size(134, 23);
            this.button_NetPoint.TabIndex = 0;
            this.button_NetPoint.Text = "I am Server";
            this.button_NetPoint.UseVisualStyleBackColor = true;
            this.button_NetPoint.Click += new System.EventHandler(this.button_NetPoint_Click);
            // 
            // checkBox_tabPage1
            // 
            this.checkBox_tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBox_tabPage1.Controls.Add(this.groupBox_BitCal);
            this.checkBox_tabPage1.Controls.Add(this.groupBox3);
            this.checkBox_tabPage1.Controls.Add(this.groupBox_COMSnd);
            this.checkBox_tabPage1.Controls.Add(this.groupBox_COMRec);
            this.checkBox_tabPage1.Location = new System.Drawing.Point(4, 22);
            this.checkBox_tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_tabPage1.Name = "checkBox_tabPage1";
            this.checkBox_tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_tabPage1.Size = new System.Drawing.Size(1042, 546);
            this.checkBox_tabPage1.TabIndex = 0;
            this.checkBox_tabPage1.Text = "KCOM";
            // 
            // groupBox_BitCal
            // 
            this.groupBox_BitCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_BitCal.Controls.Add(this.textBox_bit);
            this.groupBox_BitCal.Controls.Add(this.button_Cal);
            this.groupBox_BitCal.Controls.Add(this.textBox_Console);
            this.groupBox_BitCal.Location = new System.Drawing.Point(882, 208);
            this.groupBox_BitCal.Name = "groupBox_BitCal";
            this.groupBox_BitCal.Size = new System.Drawing.Size(152, 320);
            this.groupBox_BitCal.TabIndex = 42;
            this.groupBox_BitCal.TabStop = false;
            this.groupBox_BitCal.Text = "BitCal";
            // 
            // textBox_bit
            // 
            this.textBox_bit.Location = new System.Drawing.Point(5, 292);
            this.textBox_bit.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_bit.Name = "textBox_bit";
            this.textBox_bit.Size = new System.Drawing.Size(90, 21);
            this.textBox_bit.TabIndex = 59;
            // 
            // button_Cal
            // 
            this.button_Cal.Location = new System.Drawing.Point(99, 290);
            this.button_Cal.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cal.Name = "button_Cal";
            this.button_Cal.Size = new System.Drawing.Size(49, 23);
            this.button_Cal.TabIndex = 58;
            this.button_Cal.Text = "cal";
            this.button_Cal.UseVisualStyleBackColor = true;
            this.button_Cal.Click += new System.EventHandler(this.button_Cal_Click);
            // 
            // textBox_Console
            // 
            this.textBox_Console.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Console.Location = new System.Drawing.Point(5, 14);
            this.textBox_Console.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Console.Multiline = true;
            this.textBox_Console.Name = "textBox_Console";
            this.textBox_Console.ReadOnly = true;
            this.textBox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Console.Size = new System.Drawing.Size(143, 272);
            this.textBox_Console.TabIndex = 57;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button_COMOpen);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label_ClearRec);
            this.groupBox3.Controls.Add(this.comboBox_COMStopBit);
            this.groupBox3.Controls.Add(this.windows_Height);
            this.groupBox3.Controls.Add(this.windows_Width);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.comboBox_COMDataBit);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.checkBox_chkWindowsSize);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.comboBox_COMCheckBit);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.comboBox_COMBaudrate);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.comboBox_COMNumber);
            this.groupBox3.Location = new System.Drawing.Point(882, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(153, 201);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "串口设置";
            // 
            // button_COMOpen
            // 
            this.button_COMOpen.ForeColor = System.Drawing.Color.Red;
            this.button_COMOpen.Location = new System.Drawing.Point(81, 142);
            this.button_COMOpen.Margin = new System.Windows.Forms.Padding(2);
            this.button_COMOpen.Name = "button_COMOpen";
            this.button_COMOpen.Size = new System.Drawing.Size(67, 32);
            this.button_COMOpen.TabIndex = 11;
            this.button_COMOpen.Text = "串口已关";
            this.button_COMOpen.UseVisualStyleBackColor = true;
            this.button_COMOpen.Click += new System.EventHandler(this.button_ComOpen_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1, 120);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 19;
            this.label13.Text = "停止位:";
            // 
            // label_ClearRec
            // 
            this.label_ClearRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
            this.label_ClearRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ClearRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ClearRec.ForeColor = System.Drawing.Color.Magenta;
            this.label_ClearRec.Location = new System.Drawing.Point(7, 142);
            this.label_ClearRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ClearRec.Name = "label_ClearRec";
            this.label_ClearRec.Size = new System.Drawing.Size(70, 32);
            this.label_ClearRec.TabIndex = 40;
            this.label_ClearRec.Text = "清空接收";
            this.label_ClearRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_ClearRec.DoubleClick += new System.EventHandler(this.label_ClearRec_DoubleClick);
            // 
            // comboBox_COMStopBit
            // 
            this.comboBox_COMStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMStopBit.FormattingEnabled = true;
            this.comboBox_COMStopBit.Location = new System.Drawing.Point(49, 118);
            this.comboBox_COMStopBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMStopBit.Name = "comboBox_COMStopBit";
            this.comboBox_COMStopBit.Size = new System.Drawing.Size(99, 20);
            this.comboBox_COMStopBit.TabIndex = 18;
            // 
            // windows_Height
            // 
            this.windows_Height.Location = new System.Drawing.Point(112, 177);
            this.windows_Height.Margin = new System.Windows.Forms.Padding(2);
            this.windows_Height.Name = "windows_Height";
            this.windows_Height.Size = new System.Drawing.Size(36, 21);
            this.windows_Height.TabIndex = 54;
            // 
            // windows_Width
            // 
            this.windows_Width.Location = new System.Drawing.Point(61, 177);
            this.windows_Width.Margin = new System.Windows.Forms.Padding(2);
            this.windows_Width.Name = "windows_Width";
            this.windows_Width.Size = new System.Drawing.Size(34, 21);
            this.windows_Width.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 182);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "H: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 94);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "数据位:";
            // 
            // comboBox_COMDataBit
            // 
            this.comboBox_COMDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMDataBit.FormattingEnabled = true;
            this.comboBox_COMDataBit.Location = new System.Drawing.Point(49, 91);
            this.comboBox_COMDataBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMDataBit.Name = "comboBox_COMDataBit";
            this.comboBox_COMDataBit.Size = new System.Drawing.Size(99, 20);
            this.comboBox_COMDataBit.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 181);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "W: ";
            // 
            // checkBox_chkWindowsSize
            // 
            this.checkBox_chkWindowsSize.AutoSize = true;
            this.checkBox_chkWindowsSize.Location = new System.Drawing.Point(4, 179);
            this.checkBox_chkWindowsSize.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_chkWindowsSize.Name = "checkBox_chkWindowsSize";
            this.checkBox_chkWindowsSize.Size = new System.Drawing.Size(42, 16);
            this.checkBox_chkWindowsSize.TabIndex = 56;
            this.checkBox_chkWindowsSize.Text = "Def";
            this.checkBox_chkWindowsSize.UseVisualStyleBackColor = true;
            this.checkBox_chkWindowsSize.CheckedChanged += new System.EventHandler(this.checkBox_chkWindowsSize_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 68);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "校验位:";
            // 
            // comboBox_COMCheckBit
            // 
            this.comboBox_COMCheckBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMCheckBit.FormattingEnabled = true;
            this.comboBox_COMCheckBit.Location = new System.Drawing.Point(49, 65);
            this.comboBox_COMCheckBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMCheckBit.Name = "comboBox_COMCheckBit";
            this.comboBox_COMCheckBit.Size = new System.Drawing.Size(99, 20);
            this.comboBox_COMCheckBit.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 43);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "波特率:";
            // 
            // comboBox_COMBaudrate
            // 
            this.comboBox_COMBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMBaudrate.FormattingEnabled = true;
            this.comboBox_COMBaudrate.Location = new System.Drawing.Point(49, 38);
            this.comboBox_COMBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMBaudrate.Name = "comboBox_COMBaudrate";
            this.comboBox_COMBaudrate.Size = new System.Drawing.Size(99, 20);
            this.comboBox_COMBaudrate.TabIndex = 12;
            this.comboBox_COMBaudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMBaudrate_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "串口号:";
            // 
            // comboBox_COMNumber
            // 
            this.comboBox_COMNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMNumber.FormattingEnabled = true;
            this.comboBox_COMNumber.Location = new System.Drawing.Point(49, 13);
            this.comboBox_COMNumber.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMNumber.Name = "comboBox_COMNumber";
            this.comboBox_COMNumber.Size = new System.Drawing.Size(99, 20);
            this.comboBox_COMNumber.TabIndex = 0;
            this.comboBox_COMNumber.DropDown += new System.EventHandler(this.comboBox_COMNumber_DropDown);
            this.comboBox_COMNumber.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMNumber_SelectedIndexChanged);
            // 
            // groupBox_COMSnd
            // 
            this.groupBox_COMSnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox_COMSnd.Controls.Add(this.checkBox_Cmdline);
            this.groupBox_COMSnd.Controls.Add(this.label_com_running);
            this.groupBox_COMSnd.Controls.Add(this.label1);
            this.groupBox_COMSnd.Controls.Add(this.button_AddTime);
            this.groupBox_COMSnd.Controls.Add(this.checkBox_EnAutoSndTimer);
            this.groupBox_COMSnd.Controls.Add(this.label8);
            this.groupBox_COMSnd.Controls.Add(this.textBox_N100ms);
            this.groupBox_COMSnd.Controls.Add(this.label7);
            this.groupBox_COMSnd.Controls.Add(this.button_ASCIISend);
            this.groupBox_COMSnd.Controls.Add(this.button_CleanSND);
            this.groupBox_COMSnd.Controls.Add(this.label6);
            this.groupBox_COMSnd.Controls.Add(this.label_Send_Bytes);
            this.groupBox_COMSnd.Controls.Add(this.textBox_ComSnd);
            this.groupBox_COMSnd.Controls.Add(this.button_SendData);
            this.groupBox_COMSnd.Location = new System.Drawing.Point(7, 389);
            this.groupBox_COMSnd.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_COMSnd.Name = "groupBox_COMSnd";
            this.groupBox_COMSnd.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_COMSnd.Size = new System.Drawing.Size(872, 151);
            this.groupBox_COMSnd.TabIndex = 9;
            this.groupBox_COMSnd.TabStop = false;
            this.groupBox_COMSnd.Text = "数据发送";
            // 
            // checkBox_Cmdline
            // 
            this.checkBox_Cmdline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Cmdline.AutoSize = true;
            this.checkBox_Cmdline.Location = new System.Drawing.Point(356, 123);
            this.checkBox_Cmdline.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Cmdline.Name = "checkBox_Cmdline";
            this.checkBox_Cmdline.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Cmdline.TabIndex = 42;
            this.checkBox_Cmdline.Text = "命令行";
            this.checkBox_Cmdline.UseVisualStyleBackColor = true;
            // 
            // label_com_running
            // 
            this.label_com_running.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_com_running.AutoSize = true;
            this.label_com_running.Location = new System.Drawing.Point(362, 122);
            this.label_com_running.Name = "label_com_running";
            this.label_com_running.Size = new System.Drawing.Size(0, 12);
            this.label_com_running.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "累计发送：";
            // 
            // button_AddTime
            // 
            this.button_AddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AddTime.ForeColor = System.Drawing.Color.Red;
            this.button_AddTime.Location = new System.Drawing.Point(516, 112);
            this.button_AddTime.Margin = new System.Windows.Forms.Padding(2);
            this.button_AddTime.Name = "button_AddTime";
            this.button_AddTime.Size = new System.Drawing.Size(80, 32);
            this.button_AddTime.TabIndex = 40;
            this.button_AddTime.Text = "显示时间";
            this.button_AddTime.UseVisualStyleBackColor = true;
            this.button_AddTime.Click += new System.EventHandler(this.button_AddTime_Click);
            // 
            // checkBox_EnAutoSndTimer
            // 
            this.checkBox_EnAutoSndTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_EnAutoSndTimer.AutoSize = true;
            this.checkBox_EnAutoSndTimer.Location = new System.Drawing.Point(277, 122);
            this.checkBox_EnAutoSndTimer.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnAutoSndTimer.Name = "checkBox_EnAutoSndTimer";
            this.checkBox_EnAutoSndTimer.Size = new System.Drawing.Size(72, 16);
            this.checkBox_EnAutoSndTimer.TabIndex = 12;
            this.checkBox_EnAutoSndTimer.Text = "定时发送";
            this.checkBox_EnAutoSndTimer.UseVisualStyleBackColor = true;
            this.checkBox_EnAutoSndTimer.CheckedChanged += new System.EventHandler(this.checkBox_EnAutoSndTimer_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 122);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "X 100ms";
            // 
            // textBox_N100ms
            // 
            this.textBox_N100ms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_N100ms.Location = new System.Drawing.Point(175, 118);
            this.textBox_N100ms.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_N100ms.Name = "textBox_N100ms";
            this.textBox_N100ms.Size = new System.Drawing.Size(44, 21);
            this.textBox_N100ms.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 120);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "定时：";
            // 
            // button_ASCIISend
            // 
            this.button_ASCIISend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ASCIISend.ForeColor = System.Drawing.Color.Blue;
            this.button_ASCIISend.Location = new System.Drawing.Point(602, 110);
            this.button_ASCIISend.Margin = new System.Windows.Forms.Padding(2);
            this.button_ASCIISend.Name = "button_ASCIISend";
            this.button_ASCIISend.Size = new System.Drawing.Size(80, 34);
            this.button_ASCIISend.TabIndex = 8;
            this.button_ASCIISend.Text = "ASCII发送";
            this.button_ASCIISend.UseVisualStyleBackColor = true;
            this.button_ASCIISend.Click += new System.EventHandler(this.button_ASCIISend_Click);
            // 
            // button_CleanSND
            // 
            this.button_CleanSND.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CleanSND.Location = new System.Drawing.Point(690, 110);
            this.button_CleanSND.Margin = new System.Windows.Forms.Padding(2);
            this.button_CleanSND.Name = "button_CleanSND";
            this.button_CleanSND.Size = new System.Drawing.Size(80, 34);
            this.button_CleanSND.TabIndex = 6;
            this.button_CleanSND.Text = "清空发送";
            this.button_CleanSND.UseVisualStyleBackColor = true;
            this.button_CleanSND.Click += new System.EventHandler(this.button_CleanSnd_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(107, 120);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "字节";
            // 
            // label_Send_Bytes
            // 
            this.label_Send_Bytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Send_Bytes.AutoSize = true;
            this.label_Send_Bytes.Location = new System.Drawing.Point(76, 119);
            this.label_Send_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Send_Bytes.Name = "label_Send_Bytes";
            this.label_Send_Bytes.Size = new System.Drawing.Size(11, 12);
            this.label_Send_Bytes.TabIndex = 7;
            this.label_Send_Bytes.Text = "0";
            // 
            // textBox_ComSnd
            // 
            this.textBox_ComSnd.Location = new System.Drawing.Point(7, 20);
            this.textBox_ComSnd.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ComSnd.Multiline = true;
            this.textBox_ComSnd.Name = "textBox_ComSnd";
            this.textBox_ComSnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ComSnd.Size = new System.Drawing.Size(860, 80);
            this.textBox_ComSnd.TabIndex = 0;
            this.textBox_ComSnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ComSnd_KeyDown);
            // 
            // button_SendData
            // 
            this.button_SendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SendData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_SendData.ForeColor = System.Drawing.Color.Red;
            this.button_SendData.Location = new System.Drawing.Point(776, 110);
            this.button_SendData.Margin = new System.Windows.Forms.Padding(2);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(80, 34);
            this.button_SendData.TabIndex = 5;
            this.button_SendData.Text = "发送数据";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendDataClick);
            // 
            // groupBox_COMRec
            // 
            this.groupBox_COMRec.Controls.Add(this.checkBox_CursorMove);
            this.groupBox_COMRec.Controls.Add(this.checkBox_LockRecLen);
            this.groupBox_COMRec.Controls.Add(this.button_CreateLog);
            this.groupBox_COMRec.Controls.Add(this.checkBox_Color);
            this.groupBox_COMRec.Controls.Add(this.button_FontSize);
            this.groupBox_COMRec.Controls.Add(this.button_FontBigger);
            this.groupBox_COMRec.Controls.Add(this.button_FontSmaller);
            this.groupBox_COMRec.Controls.Add(this.button_SaveLog);
            this.groupBox_COMRec.Controls.Add(this.button_ASCIIShow);
            this.groupBox_COMRec.Controls.Add(this.label5);
            this.groupBox_COMRec.Controls.Add(this.label_Rec_Bytes);
            this.groupBox_COMRec.Controls.Add(this.textBox_ComRec);
            this.groupBox_COMRec.Controls.Add(this.label3);
            this.groupBox_COMRec.Location = new System.Drawing.Point(7, 4);
            this.groupBox_COMRec.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_COMRec.Name = "groupBox_COMRec";
            this.groupBox_COMRec.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_COMRec.Size = new System.Drawing.Size(872, 384);
            this.groupBox_COMRec.TabIndex = 8;
            this.groupBox_COMRec.TabStop = false;
            this.groupBox_COMRec.Text = "数据接收";
            // 
            // checkBox_CursorMove
            // 
            this.checkBox_CursorMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_CursorMove.AutoSize = true;
            this.checkBox_CursorMove.Location = new System.Drawing.Point(275, 360);
            this.checkBox_CursorMove.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_CursorMove.Name = "checkBox_CursorMove";
            this.checkBox_CursorMove.Size = new System.Drawing.Size(72, 16);
            this.checkBox_CursorMove.TabIndex = 42;
            this.checkBox_CursorMove.Text = "固定光标";
            this.checkBox_CursorMove.UseVisualStyleBackColor = true;
            this.checkBox_CursorMove.CheckedChanged += new System.EventHandler(this.checkBox_CursorMove_CheckedChanged);
            // 
            // checkBox_LockRecLen
            // 
            this.checkBox_LockRecLen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_LockRecLen.AutoSize = true;
            this.checkBox_LockRecLen.Location = new System.Drawing.Point(182, 360);
            this.checkBox_LockRecLen.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_LockRecLen.Name = "checkBox_LockRecLen";
            this.checkBox_LockRecLen.Size = new System.Drawing.Size(96, 16);
            this.checkBox_LockRecLen.TabIndex = 41;
            this.checkBox_LockRecLen.Text = "限定接收长度";
            this.checkBox_LockRecLen.UseVisualStyleBackColor = true;
            this.checkBox_LockRecLen.CheckedChanged += new System.EventHandler(this.checkBox_LockRecLength_CheckedChanged);
            // 
            // button_CreateLog
            // 
            this.button_CreateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CreateLog.Location = new System.Drawing.Point(690, 347);
            this.button_CreateLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_CreateLog.Name = "button_CreateLog";
            this.button_CreateLog.Size = new System.Drawing.Size(80, 32);
            this.button_CreateLog.TabIndex = 15;
            this.button_CreateLog.Text = "生成日志";
            this.button_CreateLog.UseVisualStyleBackColor = true;
            this.button_CreateLog.Click += new System.EventHandler(this.button_CreateLog_Click);
            // 
            // checkBox_Color
            // 
            this.checkBox_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Color.AutoSize = true;
            this.checkBox_Color.Location = new System.Drawing.Point(137, 360);
            this.checkBox_Color.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Color.Name = "checkBox_Color";
            this.checkBox_Color.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Color.TabIndex = 14;
            this.checkBox_Color.Text = "反色";
            this.checkBox_Color.UseVisualStyleBackColor = true;
            this.checkBox_Color.CheckedChanged += new System.EventHandler(this.checkBox_Color_CheckedChanged);
            // 
            // button_FontSize
            // 
            this.button_FontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontSize.Location = new System.Drawing.Point(348, 347);
            this.button_FontSize.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSize.Name = "button_FontSize";
            this.button_FontSize.Size = new System.Drawing.Size(80, 32);
            this.button_FontSize.TabIndex = 13;
            this.button_FontSize.Text = "Courier New";
            this.button_FontSize.UseVisualStyleBackColor = true;
            this.button_FontSize.Click += new System.EventHandler(this.button_FontSize_Click);
            // 
            // button_FontBigger
            // 
            this.button_FontBigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontBigger.Location = new System.Drawing.Point(432, 346);
            this.button_FontBigger.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontBigger.Name = "button_FontBigger";
            this.button_FontBigger.Size = new System.Drawing.Size(80, 34);
            this.button_FontBigger.TabIndex = 12;
            this.button_FontBigger.Text = "字体加大";
            this.button_FontBigger.UseVisualStyleBackColor = true;
            this.button_FontBigger.Click += new System.EventHandler(this.button_FontBigger_Click);
            // 
            // button_FontSmaller
            // 
            this.button_FontSmaller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontSmaller.Location = new System.Drawing.Point(516, 346);
            this.button_FontSmaller.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSmaller.Name = "button_FontSmaller";
            this.button_FontSmaller.Size = new System.Drawing.Size(80, 34);
            this.button_FontSmaller.TabIndex = 11;
            this.button_FontSmaller.Text = "字体减少";
            this.button_FontSmaller.UseVisualStyleBackColor = true;
            this.button_FontSmaller.Click += new System.EventHandler(this.button_FontSmaller_Click);
            // 
            // button_SaveLog
            // 
            this.button_SaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveLog.Location = new System.Drawing.Point(776, 347);
            this.button_SaveLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_SaveLog.Name = "button_SaveLog";
            this.button_SaveLog.Size = new System.Drawing.Size(80, 32);
            this.button_SaveLog.TabIndex = 10;
            this.button_SaveLog.Text = "保存数据";
            this.button_SaveLog.UseVisualStyleBackColor = true;
            this.button_SaveLog.Click += new System.EventHandler(this.button_SaveLog_Click);
            // 
            // button_ASCIIShow
            // 
            this.button_ASCIIShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ASCIIShow.ForeColor = System.Drawing.Color.Blue;
            this.button_ASCIIShow.Location = new System.Drawing.Point(602, 348);
            this.button_ASCIIShow.Margin = new System.Windows.Forms.Padding(2);
            this.button_ASCIIShow.Name = "button_ASCIIShow";
            this.button_ASCIIShow.Size = new System.Drawing.Size(80, 32);
            this.button_ASCIIShow.TabIndex = 9;
            this.button_ASCIIShow.Text = "ASCII显示";
            this.button_ASCIIShow.UseVisualStyleBackColor = true;
            this.button_ASCIIShow.Click += new System.EventHandler(this.button_ASCIIShow_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 362);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "字节";
            // 
            // label_Rec_Bytes
            // 
            this.label_Rec_Bytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Rec_Bytes.AutoSize = true;
            this.label_Rec_Bytes.Location = new System.Drawing.Point(76, 362);
            this.label_Rec_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Rec_Bytes.Name = "label_Rec_Bytes";
            this.label_Rec_Bytes.Size = new System.Drawing.Size(11, 12);
            this.label_Rec_Bytes.TabIndex = 5;
            this.label_Rec_Bytes.Text = "0";
            // 
            // textBox_ComRec
            // 
            this.textBox_ComRec.BackColor = System.Drawing.Color.White;
            this.textBox_ComRec.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_ComRec.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ComRec.Location = new System.Drawing.Point(6, 20);
            this.textBox_ComRec.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ComRec.MaxLength = 0;
            this.textBox_ComRec.Multiline = true;
            this.textBox_ComRec.Name = "textBox_ComRec";
            this.textBox_ComRec.ReadOnly = true;
            this.textBox_ComRec.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ComRec.Size = new System.Drawing.Size(860, 320);
            this.textBox_ComRec.TabIndex = 0;
            this.textBox_ComRec.WordWrap = false;
            this.textBox_ComRec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ComRec_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 362);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "累计接收：";
            // 
            // PageTag
            // 
            this.PageTag.Controls.Add(this.checkBox_tabPage1);
            this.PageTag.Controls.Add(this.tabPage2);
            this.PageTag.Location = new System.Drawing.Point(0, 0);
            this.PageTag.Margin = new System.Windows.Forms.Padding(0);
            this.PageTag.Name = "PageTag";
            this.PageTag.SelectedIndex = 0;
            this.PageTag.Size = new System.Drawing.Size(1050, 572);
            this.PageTag.TabIndex = 0;
            // 
            // timer_renew_com
            // 
            this.timer_renew_com.Interval = 10;
            this.timer_renew_com.Tick += new System.EventHandler(this.timer_renew_com_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 573);
            this.Controls.Add(this.PageTag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "KTOOL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.checkBox_tabPage1.ResumeLayout(false);
            this.groupBox_BitCal.ResumeLayout(false);
            this.groupBox_BitCal.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_COMSnd.ResumeLayout(false);
            this.groupBox_COMSnd.PerformLayout();
            this.groupBox_COMRec.ResumeLayout(false);
            this.groupBox_COMRec.PerformLayout();
            this.PageTag.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        //private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Timer timer_AutoSnd;
        private System.Windows.Forms.Timer timer_ColorShow;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage checkBox_tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_COMOpen;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox_COMStopBit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_COMDataBit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_COMCheckBit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_COMBaudrate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_COMNumber;
        private System.Windows.Forms.GroupBox groupBox_COMSnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_AddTime;
        private System.Windows.Forms.CheckBox checkBox_EnAutoSndTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_N100ms;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_ASCIISend;
        private System.Windows.Forms.Button button_CleanSND;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_Send_Bytes;
        private System.Windows.Forms.TextBox textBox_ComSnd;
        private System.Windows.Forms.Button button_SendData;
        private System.Windows.Forms.GroupBox groupBox_COMRec;
        private System.Windows.Forms.Button button_CreateLog;
        private System.Windows.Forms.Label label_ClearRec;
        private System.Windows.Forms.CheckBox checkBox_Color;
        private System.Windows.Forms.Button button_FontSize;
        private System.Windows.Forms.Button button_FontBigger;
        private System.Windows.Forms.Button button_FontSmaller;
        private System.Windows.Forms.Button button_SaveLog;
        private System.Windows.Forms.Button button_ASCIIShow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_Rec_Bytes;
        private System.Windows.Forms.TextBox textBox_ComRec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl PageTag;
		private System.Windows.Forms.CheckBox checkBox_LockRecLen;
		private System.Windows.Forms.GroupBox groupBox_BitCal;
		private System.Windows.Forms.TextBox textBox_bit;
		private System.Windows.Forms.Button button_Cal;
		private System.Windows.Forms.TextBox textBox_Console;
		private System.Windows.Forms.TextBox windows_Height;
		private System.Windows.Forms.TextBox windows_Width;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_chkWindowsSize;
        private System.Windows.Forms.Timer timer_renew_com;
        private System.Windows.Forms.Label label_com_running;
        private System.Windows.Forms.Button button_NetPoint;
        private System.Windows.Forms.Button button_NetRun;
        private System.Windows.Forms.TextBox textBox_NetRecv;
        private System.Windows.Forms.TextBox textBox_IP4;
        private System.Windows.Forms.TextBox textBox_IP3;
        private System.Windows.Forms.TextBox textBox_IP2;
        private System.Windows.Forms.TextBox textBox_IP1;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Button button_NetSend;
        private System.Windows.Forms.TextBox textBox_NetSend;
        private System.Windows.Forms.Button button_CleanNetRec;
        private System.Windows.Forms.Button button_CleanNetSnd;
        private System.Windows.Forms.CheckBox checkBox_CursorMove;
		private System.Windows.Forms.CheckBox checkBox_Cmdline;
        private System.Windows.Forms.Label label_ShowIP;
	}
}

