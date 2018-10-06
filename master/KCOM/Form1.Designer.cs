
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
			if(disposing && (components != null))
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
            this.checkBox_tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox_NetCom = new System.Windows.Forms.GroupBox();
            this.label_ShowIP = new System.Windows.Forms.Label();
            this.button_NetRun = new System.Windows.Forms.Button();
            this.button_NetPoint = new System.Windows.Forms.Button();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP4 = new System.Windows.Forms.TextBox();
            this.textBox_IP3 = new System.Windows.Forms.TextBox();
            this.textBox_IP2 = new System.Windows.Forms.TextBox();
            this.textBox_IP1 = new System.Windows.Forms.TextBox();
            this.groupBox_BitCal = new System.Windows.Forms.GroupBox();
            this.textBox_bit = new System.Windows.Forms.TextBox();
            this.button_Cal = new System.Windows.Forms.Button();
            this.textBox_Console = new System.Windows.Forms.TextBox();
            this.groupBox_Uart = new System.Windows.Forms.GroupBox();
            this.label_Baudrate1 = new System.Windows.Forms.Label();
            this.textBox_baudrate1 = new System.Windows.Forms.TextBox();
            this.button_COMOpen = new System.Windows.Forms.Button();
            this.button_CreateLog = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox_COMStopBit = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_COMDataBit = new System.Windows.Forms.ComboBox();
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
            this.button_FastSave = new System.Windows.Forms.Button();
            this.checkBox_CursorMove = new System.Windows.Forms.CheckBox();
            this.checkBox_LockRecLen = new System.Windows.Forms.CheckBox();
            this.checkBox_Color = new System.Windows.Forms.CheckBox();
            this.label_ClearRec = new System.Windows.Forms.Label();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox_WindowsHeight = new System.Windows.Forms.TextBox();
            this.testBox_WindowsWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_chkWindowsSize = new System.Windows.Forms.CheckBox();
            this.checkBox_ClearRecvWhenFastSave = new System.Windows.Forms.CheckBox();
            this.checkBox_Backgroup = new System.Windows.Forms.CheckBox();
            this.label_FastSaveLocation = new System.Windows.Forms.Label();
            this.textBox_FastSaveLocation = new System.Windows.Forms.TextBox();
            this.timer_renew_com = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBox_tabPage1.SuspendLayout();
            this.groupBox_NetCom.SuspendLayout();
            this.groupBox_BitCal.SuspendLayout();
            this.groupBox_Uart.SuspendLayout();
            this.groupBox_COMSnd.SuspendLayout();
            this.groupBox_COMRec.SuspendLayout();
            this.PageTag.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            // checkBox_tabPage1
            // 
            this.checkBox_tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBox_tabPage1.Controls.Add(this.groupBox_NetCom);
            this.checkBox_tabPage1.Controls.Add(this.groupBox_BitCal);
            this.checkBox_tabPage1.Controls.Add(this.groupBox_Uart);
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
            // groupBox_NetCom
            // 
            this.groupBox_NetCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_NetCom.Controls.Add(this.label_ShowIP);
            this.groupBox_NetCom.Controls.Add(this.button_NetRun);
            this.groupBox_NetCom.Controls.Add(this.button_NetPoint);
            this.groupBox_NetCom.Controls.Add(this.label_IP);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP4);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP3);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP2);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP1);
            this.groupBox_NetCom.Location = new System.Drawing.Point(882, 212);
            this.groupBox_NetCom.Name = "groupBox_NetCom";
            this.groupBox_NetCom.Size = new System.Drawing.Size(152, 156);
            this.groupBox_NetCom.TabIndex = 43;
            this.groupBox_NetCom.TabStop = false;
            this.groupBox_NetCom.Text = "NetCom";
            // 
            // label_ShowIP
            // 
            this.label_ShowIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ShowIP.AutoSize = true;
            this.label_ShowIP.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ShowIP.Location = new System.Drawing.Point(7, 100);
            this.label_ShowIP.Name = "label_ShowIP";
            this.label_ShowIP.Size = new System.Drawing.Size(59, 11);
            this.label_ShowIP.TabIndex = 63;
            this.label_ShowIP.Text = "Local IP:";
            // 
            // button_NetRun
            // 
            this.button_NetRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetRun.Location = new System.Drawing.Point(6, 76);
            this.button_NetRun.Name = "button_NetRun";
            this.button_NetRun.Size = new System.Drawing.Size(134, 23);
            this.button_NetRun.TabIndex = 20;
            this.button_NetRun.Text = "Run";
            this.button_NetRun.UseVisualStyleBackColor = true;
            this.button_NetRun.Click += new System.EventHandler(this.button_NetRun_Click);
            // 
            // button_NetPoint
            // 
            this.button_NetPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetPoint.ForeColor = System.Drawing.Color.Red;
            this.button_NetPoint.Location = new System.Drawing.Point(6, 51);
            this.button_NetPoint.Name = "button_NetPoint";
            this.button_NetPoint.Size = new System.Drawing.Size(134, 23);
            this.button_NetPoint.TabIndex = 19;
            this.button_NetPoint.Text = "I am Server";
            this.button_NetPoint.UseVisualStyleBackColor = true;
            this.button_NetPoint.Click += new System.EventHandler(this.button_NetPoint_Click);
            // 
            // label_IP
            // 
            this.label_IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_IP.AutoSize = true;
            this.label_IP.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_IP.Location = new System.Drawing.Point(6, 14);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(83, 11);
            this.label_IP.TabIndex = 18;
            this.label_IP.Text = "Set local IP:";
            // 
            // textBox_IP4
            // 
            this.textBox_IP4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP4.Location = new System.Drawing.Point(107, 28);
            this.textBox_IP4.Name = "textBox_IP4";
            this.textBox_IP4.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP4.TabIndex = 17;
            this.textBox_IP4.Text = "100";
            // 
            // textBox_IP3
            // 
            this.textBox_IP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP3.Location = new System.Drawing.Point(74, 28);
            this.textBox_IP3.Name = "textBox_IP3";
            this.textBox_IP3.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP3.TabIndex = 16;
            this.textBox_IP3.Text = "0";
            // 
            // textBox_IP2
            // 
            this.textBox_IP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP2.Location = new System.Drawing.Point(40, 28);
            this.textBox_IP2.Name = "textBox_IP2";
            this.textBox_IP2.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP2.TabIndex = 15;
            this.textBox_IP2.Text = "168";
            // 
            // textBox_IP1
            // 
            this.textBox_IP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP1.Location = new System.Drawing.Point(6, 28);
            this.textBox_IP1.Name = "textBox_IP1";
            this.textBox_IP1.Size = new System.Drawing.Size(28, 21);
            this.textBox_IP1.TabIndex = 14;
            this.textBox_IP1.Text = "192";
            // 
            // groupBox_BitCal
            // 
            this.groupBox_BitCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_BitCal.Controls.Add(this.textBox_bit);
            this.groupBox_BitCal.Controls.Add(this.button_Cal);
            this.groupBox_BitCal.Controls.Add(this.textBox_Console);
            this.groupBox_BitCal.Location = new System.Drawing.Point(885, 374);
            this.groupBox_BitCal.Name = "groupBox_BitCal";
            this.groupBox_BitCal.Size = new System.Drawing.Size(152, 157);
            this.groupBox_BitCal.TabIndex = 42;
            this.groupBox_BitCal.TabStop = false;
            this.groupBox_BitCal.Text = "BitCal";
            // 
            // textBox_bit
            // 
            this.textBox_bit.Location = new System.Drawing.Point(4, 132);
            this.textBox_bit.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_bit.Name = "textBox_bit";
            this.textBox_bit.Size = new System.Drawing.Size(90, 21);
            this.textBox_bit.TabIndex = 59;
            // 
            // button_Cal
            // 
            this.button_Cal.Location = new System.Drawing.Point(98, 130);
            this.button_Cal.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cal.Name = "button_Cal";
            this.button_Cal.Size = new System.Drawing.Size(49, 23);
            this.button_Cal.TabIndex = 58;
            this.button_Cal.Text = "CAL";
            this.button_Cal.UseVisualStyleBackColor = true;
            this.button_Cal.Click += new System.EventHandler(this.button_Cal_Click);
            // 
            // textBox_Console
            // 
            this.textBox_Console.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Console.Location = new System.Drawing.Point(5, 19);
            this.textBox_Console.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Console.Multiline = true;
            this.textBox_Console.Name = "textBox_Console";
            this.textBox_Console.ReadOnly = true;
            this.textBox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Console.Size = new System.Drawing.Size(143, 110);
            this.textBox_Console.TabIndex = 57;
            // 
            // groupBox_Uart
            // 
            this.groupBox_Uart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Uart.Controls.Add(this.label_Baudrate1);
            this.groupBox_Uart.Controls.Add(this.textBox_baudrate1);
            this.groupBox_Uart.Controls.Add(this.button_COMOpen);
            this.groupBox_Uart.Controls.Add(this.button_CreateLog);
            this.groupBox_Uart.Controls.Add(this.label13);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMStopBit);
            this.groupBox_Uart.Controls.Add(this.label12);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMDataBit);
            this.groupBox_Uart.Controls.Add(this.label11);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMCheckBit);
            this.groupBox_Uart.Controls.Add(this.label10);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMBaudrate);
            this.groupBox_Uart.Controls.Add(this.label9);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMNumber);
            this.groupBox_Uart.Location = new System.Drawing.Point(882, 4);
            this.groupBox_Uart.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Name = "groupBox_Uart";
            this.groupBox_Uart.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Size = new System.Drawing.Size(153, 203);
            this.groupBox_Uart.TabIndex = 10;
            this.groupBox_Uart.TabStop = false;
            this.groupBox_Uart.Text = "Uart Config";
            // 
            // label_Baudrate1
            // 
            this.label_Baudrate1.AutoSize = true;
            this.label_Baudrate1.Location = new System.Drawing.Point(5, 180);
            this.label_Baudrate1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Baudrate1.Name = "label_Baudrate1";
            this.label_Baudrate1.Size = new System.Drawing.Size(77, 12);
            this.label_Baudrate1.TabIndex = 58;
            this.label_Baudrate1.Text = "Custom Baud:";
            // 
            // textBox_baudrate1
            // 
            this.textBox_baudrate1.Location = new System.Drawing.Point(86, 177);
            this.textBox_baudrate1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_baudrate1.Name = "textBox_baudrate1";
            this.textBox_baudrate1.Size = new System.Drawing.Size(49, 21);
            this.textBox_baudrate1.TabIndex = 57;
            this.textBox_baudrate1.Text = "1222400";
            // 
            // button_COMOpen
            // 
            this.button_COMOpen.ForeColor = System.Drawing.Color.Red;
            this.button_COMOpen.Location = new System.Drawing.Point(81, 142);
            this.button_COMOpen.Margin = new System.Windows.Forms.Padding(2);
            this.button_COMOpen.Name = "button_COMOpen";
            this.button_COMOpen.Size = new System.Drawing.Size(67, 32);
            this.button_COMOpen.TabIndex = 11;
            this.button_COMOpen.Text = "COM is closed";
            this.button_COMOpen.UseVisualStyleBackColor = true;
            this.button_COMOpen.Click += new System.EventHandler(this.button_ComOpen_Click);
            // 
            // button_CreateLog
            // 
            this.button_CreateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CreateLog.Location = new System.Drawing.Point(10, 142);
            this.button_CreateLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_CreateLog.Name = "button_CreateLog";
            this.button_CreateLog.Size = new System.Drawing.Size(67, 32);
            this.button_CreateLog.TabIndex = 15;
            this.button_CreateLog.Text = "Create a log";
            this.button_CreateLog.UseVisualStyleBackColor = true;
            this.button_CreateLog.Click += new System.EventHandler(this.button_CreateLog_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1, 120);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 19;
            this.label13.Text = "StopBit:";
            // 
            // comboBox_COMStopBit
            // 
            this.comboBox_COMStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMStopBit.FormattingEnabled = true;
            this.comboBox_COMStopBit.Location = new System.Drawing.Point(58, 118);
            this.comboBox_COMStopBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMStopBit.Name = "comboBox_COMStopBit";
            this.comboBox_COMStopBit.Size = new System.Drawing.Size(90, 20);
            this.comboBox_COMStopBit.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 94);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "DataBit:";
            // 
            // comboBox_COMDataBit
            // 
            this.comboBox_COMDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMDataBit.FormattingEnabled = true;
            this.comboBox_COMDataBit.Location = new System.Drawing.Point(58, 91);
            this.comboBox_COMDataBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMDataBit.Name = "comboBox_COMDataBit";
            this.comboBox_COMDataBit.Size = new System.Drawing.Size(90, 20);
            this.comboBox_COMDataBit.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 68);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "ChkBit:";
            // 
            // comboBox_COMCheckBit
            // 
            this.comboBox_COMCheckBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMCheckBit.FormattingEnabled = true;
            this.comboBox_COMCheckBit.Location = new System.Drawing.Point(58, 65);
            this.comboBox_COMCheckBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMCheckBit.Name = "comboBox_COMCheckBit";
            this.comboBox_COMCheckBit.Size = new System.Drawing.Size(90, 20);
            this.comboBox_COMCheckBit.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 43);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "Baud";
            // 
            // comboBox_COMBaudrate
            // 
            this.comboBox_COMBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMBaudrate.FormattingEnabled = true;
            this.comboBox_COMBaudrate.Location = new System.Drawing.Point(58, 38);
            this.comboBox_COMBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMBaudrate.Name = "comboBox_COMBaudrate";
            this.comboBox_COMBaudrate.Size = new System.Drawing.Size(90, 20);
            this.comboBox_COMBaudrate.TabIndex = 12;
            this.comboBox_COMBaudrate.DropDown += new System.EventHandler(this.comboBox_COMBaudrate_DropDown);
            this.comboBox_COMBaudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMBaudrate_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "COM:";
            // 
            // comboBox_COMNumber
            // 
            this.comboBox_COMNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMNumber.FormattingEnabled = true;
            this.comboBox_COMNumber.Location = new System.Drawing.Point(58, 13);
            this.comboBox_COMNumber.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMNumber.Name = "comboBox_COMNumber";
            this.comboBox_COMNumber.Size = new System.Drawing.Size(90, 20);
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
            this.groupBox_COMSnd.Text = "Data Send";
            // 
            // checkBox_Cmdline
            // 
            this.checkBox_Cmdline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Cmdline.AutoSize = true;
            this.checkBox_Cmdline.Location = new System.Drawing.Point(286, 123);
            this.checkBox_Cmdline.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Cmdline.Name = "checkBox_Cmdline";
            this.checkBox_Cmdline.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Cmdline.TabIndex = 42;
            this.checkBox_Cmdline.Text = "Cmdline";
            this.checkBox_Cmdline.UseVisualStyleBackColor = true;
            this.checkBox_Cmdline.CheckedChanged += new System.EventHandler(this.checkBox_Cmdline_CheckedChanged);
            // 
            // label_com_running
            // 
            this.label_com_running.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_com_running.AutoSize = true;
            this.label_com_running.Location = new System.Drawing.Point(4, 130);
            this.label_com_running.Name = "label_com_running";
            this.label_com_running.Size = new System.Drawing.Size(77, 12);
            this.label_com_running.TabIndex = 41;
            this.label_com_running.Text = "TIMETIMETIME";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sended:";
            // 
            // button_AddTime
            // 
            this.button_AddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AddTime.ForeColor = System.Drawing.Color.Red;
            this.button_AddTime.Location = new System.Drawing.Point(611, 108);
            this.button_AddTime.Margin = new System.Windows.Forms.Padding(2);
            this.button_AddTime.Name = "button_AddTime";
            this.button_AddTime.Size = new System.Drawing.Size(80, 32);
            this.button_AddTime.TabIndex = 40;
            this.button_AddTime.Text = "Timestamp";
            this.button_AddTime.UseVisualStyleBackColor = true;
            this.button_AddTime.Click += new System.EventHandler(this.button_AddTime_Click);
            // 
            // checkBox_EnAutoSndTimer
            // 
            this.checkBox_EnAutoSndTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_EnAutoSndTimer.AutoSize = true;
            this.checkBox_EnAutoSndTimer.Location = new System.Drawing.Point(286, 106);
            this.checkBox_EnAutoSndTimer.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnAutoSndTimer.Name = "checkBox_EnAutoSndTimer";
            this.checkBox_EnAutoSndTimer.Size = new System.Drawing.Size(78, 16);
            this.checkBox_EnAutoSndTimer.TabIndex = 12;
            this.checkBox_EnAutoSndTimer.Text = "Auto Send";
            this.checkBox_EnAutoSndTimer.UseVisualStyleBackColor = true;
            this.checkBox_EnAutoSndTimer.CheckedChanged += new System.EventHandler(this.checkBox_EnAutoSndTimer_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 106);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "X 100ms";
            // 
            // textBox_N100ms
            // 
            this.textBox_N100ms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_N100ms.Location = new System.Drawing.Point(186, 101);
            this.textBox_N100ms.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_N100ms.Name = "textBox_N100ms";
            this.textBox_N100ms.Size = new System.Drawing.Size(44, 21);
            this.textBox_N100ms.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 106);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "Timing:";
            // 
            // button_ASCIISend
            // 
            this.button_ASCIISend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ASCIISend.ForeColor = System.Drawing.Color.Blue;
            this.button_ASCIISend.Location = new System.Drawing.Point(551, 108);
            this.button_ASCIISend.Margin = new System.Windows.Forms.Padding(2);
            this.button_ASCIISend.Name = "button_ASCIISend";
            this.button_ASCIISend.Size = new System.Drawing.Size(56, 34);
            this.button_ASCIISend.TabIndex = 8;
            this.button_ASCIISend.Text = "ASCII Send";
            this.button_ASCIISend.UseVisualStyleBackColor = true;
            this.button_ASCIISend.Click += new System.EventHandler(this.button_ASCIISend_Click);
            // 
            // button_CleanSND
            // 
            this.button_CleanSND.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CleanSND.Location = new System.Drawing.Point(695, 108);
            this.button_CleanSND.Margin = new System.Windows.Forms.Padding(2);
            this.button_CleanSND.Name = "button_CleanSND";
            this.button_CleanSND.Size = new System.Drawing.Size(80, 34);
            this.button_CleanSND.TabIndex = 6;
            this.button_CleanSND.Text = "Send Clear";
            this.button_CleanSND.UseVisualStyleBackColor = true;
            this.button_CleanSND.Click += new System.EventHandler(this.button_CleanSnd_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(102, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Bytes";
            // 
            // label_Send_Bytes
            // 
            this.label_Send_Bytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Send_Bytes.AutoSize = true;
            this.label_Send_Bytes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_Send_Bytes.Location = new System.Drawing.Point(76, 106);
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
            this.button_SendData.Location = new System.Drawing.Point(776, 108);
            this.button_SendData.Margin = new System.Windows.Forms.Padding(2);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(80, 34);
            this.button_SendData.TabIndex = 5;
            this.button_SendData.Text = "Data Send";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendDataClick);
            // 
            // groupBox_COMRec
            // 
            this.groupBox_COMRec.Controls.Add(this.button_FastSave);
            this.groupBox_COMRec.Controls.Add(this.checkBox_CursorMove);
            this.groupBox_COMRec.Controls.Add(this.checkBox_LockRecLen);
            this.groupBox_COMRec.Controls.Add(this.checkBox_Color);
            this.groupBox_COMRec.Controls.Add(this.label_ClearRec);
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
            this.groupBox_COMRec.Text = "Data Recv";
            // 
            // button_FastSave
            // 
            this.button_FastSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FastSave.Location = new System.Drawing.Point(776, 346);
            this.button_FastSave.Margin = new System.Windows.Forms.Padding(2);
            this.button_FastSave.Name = "button_FastSave";
            this.button_FastSave.Size = new System.Drawing.Size(80, 32);
            this.button_FastSave.TabIndex = 43;
            this.button_FastSave.Text = "Fast Save";
            this.button_FastSave.UseVisualStyleBackColor = true;
            this.button_FastSave.Click += new System.EventHandler(this.button_FastSave_Click);
            // 
            // checkBox_CursorMove
            // 
            this.checkBox_CursorMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_CursorMove.AutoSize = true;
            this.checkBox_CursorMove.Location = new System.Drawing.Point(259, 344);
            this.checkBox_CursorMove.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_CursorMove.Name = "checkBox_CursorMove";
            this.checkBox_CursorMove.Size = new System.Drawing.Size(96, 16);
            this.checkBox_CursorMove.TabIndex = 42;
            this.checkBox_CursorMove.Text = "Cursor fixed";
            this.checkBox_CursorMove.UseVisualStyleBackColor = true;
            this.checkBox_CursorMove.CheckedChanged += new System.EventHandler(this.checkBox_CursorMove_CheckedChanged);
            // 
            // checkBox_LockRecLen
            // 
            this.checkBox_LockRecLen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_LockRecLen.AutoSize = true;
            this.checkBox_LockRecLen.Location = new System.Drawing.Point(259, 364);
            this.checkBox_LockRecLen.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_LockRecLen.Name = "checkBox_LockRecLen";
            this.checkBox_LockRecLen.Size = new System.Drawing.Size(114, 16);
            this.checkBox_LockRecLen.TabIndex = 41;
            this.checkBox_LockRecLen.Text = "Max recv length";
            this.checkBox_LockRecLen.UseVisualStyleBackColor = true;
            this.checkBox_LockRecLen.CheckedChanged += new System.EventHandler(this.checkBox_LockRecLength_CheckedChanged);
            // 
            // checkBox_Color
            // 
            this.checkBox_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Color.AutoSize = true;
            this.checkBox_Color.Location = new System.Drawing.Point(171, 344);
            this.checkBox_Color.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Color.Name = "checkBox_Color";
            this.checkBox_Color.Size = new System.Drawing.Size(84, 16);
            this.checkBox_Color.TabIndex = 14;
            this.checkBox_Color.Text = "Anti color";
            this.checkBox_Color.UseVisualStyleBackColor = true;
            this.checkBox_Color.CheckedChanged += new System.EventHandler(this.checkBox_Color_CheckedChanged);
            // 
            // label_ClearRec
            // 
            this.label_ClearRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
            this.label_ClearRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ClearRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ClearRec.ForeColor = System.Drawing.Color.Magenta;
            this.label_ClearRec.Location = new System.Drawing.Point(695, 346);
            this.label_ClearRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ClearRec.Name = "label_ClearRec";
            this.label_ClearRec.Size = new System.Drawing.Size(80, 32);
            this.label_ClearRec.TabIndex = 40;
            this.label_ClearRec.Text = "Recv Clear";
            this.label_ClearRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_ClearRec.DoubleClick += new System.EventHandler(this.label_ClearRec_DoubleClick);
            // 
            // button_FontSize
            // 
            this.button_FontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontSize.Location = new System.Drawing.Point(397, 346);
            this.button_FontSize.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSize.Name = "button_FontSize";
            this.button_FontSize.Size = new System.Drawing.Size(56, 32);
            this.button_FontSize.TabIndex = 13;
            this.button_FontSize.Text = "Courier New";
            this.button_FontSize.UseVisualStyleBackColor = true;
            this.button_FontSize.Click += new System.EventHandler(this.button_FontSize_Click);
            // 
            // button_FontBigger
            // 
            this.button_FontBigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontBigger.Location = new System.Drawing.Point(457, 344);
            this.button_FontBigger.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontBigger.Name = "button_FontBigger";
            this.button_FontBigger.Size = new System.Drawing.Size(44, 34);
            this.button_FontBigger.TabIndex = 12;
            this.button_FontBigger.Text = "Font+";
            this.button_FontBigger.UseVisualStyleBackColor = true;
            this.button_FontBigger.Click += new System.EventHandler(this.button_FontBigger_Click);
            // 
            // button_FontSmaller
            // 
            this.button_FontSmaller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_FontSmaller.Location = new System.Drawing.Point(505, 344);
            this.button_FontSmaller.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSmaller.Name = "button_FontSmaller";
            this.button_FontSmaller.Size = new System.Drawing.Size(43, 34);
            this.button_FontSmaller.TabIndex = 11;
            this.button_FontSmaller.Text = "Font-";
            this.button_FontSmaller.UseVisualStyleBackColor = true;
            this.button_FontSmaller.Click += new System.EventHandler(this.button_FontSmaller_Click);
            // 
            // button_SaveLog
            // 
            this.button_SaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveLog.Location = new System.Drawing.Point(611, 346);
            this.button_SaveLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_SaveLog.Name = "button_SaveLog";
            this.button_SaveLog.Size = new System.Drawing.Size(80, 32);
            this.button_SaveLog.TabIndex = 10;
            this.button_SaveLog.Text = "Save as file";
            this.button_SaveLog.UseVisualStyleBackColor = true;
            this.button_SaveLog.Click += new System.EventHandler(this.button_SaveLog_Click);
            // 
            // button_ASCIIShow
            // 
            this.button_ASCIIShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ASCIIShow.ForeColor = System.Drawing.Color.Blue;
            this.button_ASCIIShow.Location = new System.Drawing.Point(551, 346);
            this.button_ASCIIShow.Margin = new System.Windows.Forms.Padding(2);
            this.button_ASCIIShow.Name = "button_ASCIIShow";
            this.button_ASCIIShow.Size = new System.Drawing.Size(56, 32);
            this.button_ASCIIShow.TabIndex = 9;
            this.button_ASCIIShow.Text = "ASCII Show";
            this.button_ASCIIShow.UseVisualStyleBackColor = true;
            this.button_ASCIIShow.Click += new System.EventHandler(this.button_ASCIIShow_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 346);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bytes";
            // 
            // label_Rec_Bytes
            // 
            this.label_Rec_Bytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Rec_Bytes.AutoSize = true;
            this.label_Rec_Bytes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label_Rec_Bytes.Location = new System.Drawing.Point(76, 346);
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
            this.label3.Location = new System.Drawing.Point(6, 346);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Recieved:";
            // 
            // PageTag
            // 
            this.PageTag.Controls.Add(this.checkBox_tabPage1);
            this.PageTag.Controls.Add(this.tabPage1);
            this.PageTag.Location = new System.Drawing.Point(0, 0);
            this.PageTag.Margin = new System.Windows.Forms.Padding(0);
            this.PageTag.Name = "PageTag";
            this.PageTag.SelectedIndex = 0;
            this.PageTag.Size = new System.Drawing.Size(1050, 572);
            this.PageTag.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox_WindowsHeight);
            this.tabPage1.Controls.Add(this.testBox_WindowsWidth);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkBox_chkWindowsSize);
            this.tabPage1.Controls.Add(this.checkBox_ClearRecvWhenFastSave);
            this.tabPage1.Controls.Add(this.checkBox_Backgroup);
            this.tabPage1.Controls.Add(this.label_FastSaveLocation);
            this.tabPage1.Controls.Add(this.textBox_FastSaveLocation);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1042, 546);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_WindowsHeight
            // 
            this.textBox_WindowsHeight.Location = new System.Drawing.Point(324, 128);
            this.textBox_WindowsHeight.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_WindowsHeight.Name = "textBox_WindowsHeight";
            this.textBox_WindowsHeight.Size = new System.Drawing.Size(36, 21);
            this.textBox_WindowsHeight.TabIndex = 59;
            // 
            // testBox_WindowsWidth
            // 
            this.testBox_WindowsWidth.Location = new System.Drawing.Point(237, 128);
            this.testBox_WindowsWidth.Margin = new System.Windows.Forms.Padding(2);
            this.testBox_WindowsWidth.Name = "testBox_WindowsWidth";
            this.testBox_WindowsWidth.Size = new System.Drawing.Size(34, 21);
            this.testBox_WindowsWidth.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "Height:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "Width:";
            // 
            // checkBox_chkWindowsSize
            // 
            this.checkBox_chkWindowsSize.AutoSize = true;
            this.checkBox_chkWindowsSize.Location = new System.Drawing.Point(37, 130);
            this.checkBox_chkWindowsSize.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_chkWindowsSize.Name = "checkBox_chkWindowsSize";
            this.checkBox_chkWindowsSize.Size = new System.Drawing.Size(144, 16);
            this.checkBox_chkWindowsSize.TabIndex = 61;
            this.checkBox_chkWindowsSize.Text = "Defined Windows size";
            this.checkBox_chkWindowsSize.UseVisualStyleBackColor = true;
            // 
            // checkBox_ClearRecvWhenFastSave
            // 
            this.checkBox_ClearRecvWhenFastSave.AutoSize = true;
            this.checkBox_ClearRecvWhenFastSave.Location = new System.Drawing.Point(37, 109);
            this.checkBox_ClearRecvWhenFastSave.Name = "checkBox_ClearRecvWhenFastSave";
            this.checkBox_ClearRecvWhenFastSave.Size = new System.Drawing.Size(228, 16);
            this.checkBox_ClearRecvWhenFastSave.TabIndex = 3;
            this.checkBox_ClearRecvWhenFastSave.Text = "Clear received data when fast save";
            this.checkBox_ClearRecvWhenFastSave.UseVisualStyleBackColor = true;
            // 
            // checkBox_Backgroup
            // 
            this.checkBox_Backgroup.AutoSize = true;
            this.checkBox_Backgroup.Location = new System.Drawing.Point(37, 87);
            this.checkBox_Backgroup.Name = "checkBox_Backgroup";
            this.checkBox_Backgroup.Size = new System.Drawing.Size(120, 16);
            this.checkBox_Backgroup.TabIndex = 2;
            this.checkBox_Backgroup.Text = "Run in Backgroup";
            this.checkBox_Backgroup.UseVisualStyleBackColor = true;
            // 
            // label_FastSaveLocation
            // 
            this.label_FastSaveLocation.AutoSize = true;
            this.label_FastSaveLocation.Location = new System.Drawing.Point(35, 44);
            this.label_FastSaveLocation.Name = "label_FastSaveLocation";
            this.label_FastSaveLocation.Size = new System.Drawing.Size(107, 12);
            this.label_FastSaveLocation.TabIndex = 1;
            this.label_FastSaveLocation.Text = "FastSaveLocation:";
            // 
            // textBox_FastSaveLocation
            // 
            this.textBox_FastSaveLocation.Location = new System.Drawing.Point(142, 41);
            this.textBox_FastSaveLocation.Name = "textBox_FastSaveLocation";
            this.textBox_FastSaveLocation.Size = new System.Drawing.Size(291, 21);
            this.textBox_FastSaveLocation.TabIndex = 0;
            // 
            // timer_renew_com
            // 
            this.timer_renew_com.Interval = 10;
            this.timer_renew_com.Tick += new System.EventHandler(this.timer_renew_com_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
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
            this.checkBox_tabPage1.ResumeLayout(false);
            this.groupBox_NetCom.ResumeLayout(false);
            this.groupBox_NetCom.PerformLayout();
            this.groupBox_BitCal.ResumeLayout(false);
            this.groupBox_BitCal.PerformLayout();
            this.groupBox_Uart.ResumeLayout(false);
            this.groupBox_Uart.PerformLayout();
            this.groupBox_COMSnd.ResumeLayout(false);
            this.groupBox_COMSnd.PerformLayout();
            this.groupBox_COMRec.ResumeLayout(false);
            this.groupBox_COMRec.PerformLayout();
            this.PageTag.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        //private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Timer timer_AutoSnd;
        private System.Windows.Forms.Timer timer_ColorShow;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage checkBox_tabPage1;
        private System.Windows.Forms.GroupBox groupBox_Uart;
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
        private System.Windows.Forms.Timer timer_renew_com;
		private System.Windows.Forms.Label label_com_running;
        private System.Windows.Forms.CheckBox checkBox_CursorMove;
		private System.Windows.Forms.CheckBox checkBox_Cmdline;
		private System.Windows.Forms.TextBox textBox_baudrate1;
		private System.Windows.Forms.Label label_Baudrate1;
        private System.Windows.Forms.Button button_FastSave;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label_FastSaveLocation;
        private System.Windows.Forms.TextBox textBox_FastSaveLocation;
        private System.Windows.Forms.CheckBox checkBox_Backgroup;
        private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.CheckBox checkBox_ClearRecvWhenFastSave;
		private System.Windows.Forms.TextBox textBox_WindowsHeight;
		private System.Windows.Forms.TextBox testBox_WindowsWidth;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBox_chkWindowsSize;
		private System.Windows.Forms.GroupBox groupBox_NetCom;
		private System.Windows.Forms.Label label_IP;
		private System.Windows.Forms.TextBox textBox_IP4;
		private System.Windows.Forms.TextBox textBox_IP3;
		private System.Windows.Forms.TextBox textBox_IP2;
		private System.Windows.Forms.TextBox textBox_IP1;
		private System.Windows.Forms.Button button_NetPoint;
		private System.Windows.Forms.Button button_NetRun;
		private System.Windows.Forms.Label label_ShowIP;
	}
}

