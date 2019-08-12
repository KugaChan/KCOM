
namespace KCOM
{
	partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.timer_ColorShow = new System.Windows.Forms.Timer(this.components);
            this.tabPage_COM = new System.Windows.Forms.TabPage();
            this.textBox_ComRec = new System.Windows.Forms.TextBox();
            this.textBox_ComSnd = new System.Windows.Forms.TextBox();
            this.groupBox_Uart = new System.Windows.Forms.GroupBox();
            this.button_RunEXE = new System.Windows.Forms.Button();
            this.groupBox_BitCal = new System.Windows.Forms.GroupBox();
            this.textBox_bit = new System.Windows.Forms.TextBox();
            this.textBox_Console = new System.Windows.Forms.TextBox();
            this.button_Cal = new System.Windows.Forms.Button();
            this.button_Test = new System.Windows.Forms.Button();
            this.label_Speed = new System.Windows.Forms.Label();
            this.label_DataRemain = new System.Windows.Forms.Label();
            this.label_MissData = new System.Windows.Forms.Label();
            this.label_RealTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_AutoSndInterval_100ms = new System.Windows.Forms.TextBox();
            this.label_Rec_Bytes = new System.Windows.Forms.Label();
            this.checkBox_EnAutoSnd = new System.Windows.Forms.CheckBox();
            this.button_TimeStamp = new System.Windows.Forms.Button();
            this.checkBox_CursorFixed = new System.Windows.Forms.CheckBox();
            this.button_FastSave = new System.Windows.Forms.Button();
            this.label_Baudrate1 = new System.Windows.Forms.Label();
            this.button_CleanSND = new System.Windows.Forms.Button();
            this.button_CreateLog = new System.Windows.Forms.Button();
            this.button_SendData = new System.Windows.Forms.Button();
            this.label_Send_Bytes = new System.Windows.Forms.Label();
            this.button_SaveFile = new System.Windows.Forms.Button();
            this.textBox_custom_baudrate = new System.Windows.Forms.TextBox();
            this.button_COMOpen = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox_COMStopBit = new System.Windows.Forms.ComboBox();
            this.label_ClearRec = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_COMDataBit = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_COMCheckBit = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_COMBaudrate = new System.Windows.Forms.ComboBox();
            this.label_COM = new System.Windows.Forms.Label();
            this.comboBox_COMNumber = new System.Windows.Forms.ComboBox();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.checkBox_FastPrintf = new System.Windows.Forms.CheckBox();
            this.button_COMSyncOpen = new System.Windows.Forms.Button();
            this.label_COM_Sync = new System.Windows.Forms.Label();
            this.comboBox_SyncComNum = new System.Windows.Forms.ComboBox();
            this.checkBox_Cmdline = new System.Windows.Forms.CheckBox();
            this.checkBox_LimitRecLen = new System.Windows.Forms.CheckBox();
            this.checkBox_Color = new System.Windows.Forms.CheckBox();
            this.button_FontSize = new System.Windows.Forms.Button();
            this.button_FontBigger = new System.Windows.Forms.Button();
            this.button_FontSmaller = new System.Windows.Forms.Button();
            this.PageTag = new System.Windows.Forms.TabControl();
            this.tabPage_Config = new System.Windows.Forms.TabPage();
            this.groupBox_FastPrint = new System.Windows.Forms.GroupBox();
            this.button_FPSelect_HEX = new System.Windows.Forms.Button();
            this.groupBox_eCMD = new System.Windows.Forms.GroupBox();
            this.textBox_RunExeCode = new System.Windows.Forms.TextBox();
            this.label_RunExeCode = new System.Windows.Forms.Label();
            this.checkBox_eCMD = new System.Windows.Forms.CheckBox();
            this.button_SelectEXE = new System.Windows.Forms.Button();
            this.button_SysFont = new System.Windows.Forms.Button();
            this.groupBox_COMSync = new System.Windows.Forms.GroupBox();
            this.comboBox_SyncBaud = new System.Windows.Forms.ComboBox();
            this.label_SyncBaudRate = new System.Windows.Forms.Label();
            this.groupBox_CurrentSetting = new System.Windows.Forms.GroupBox();
            this.checkBox_DbgLog = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableBakup = new System.Windows.Forms.CheckBox();
            this.checkBox_WordWrap = new System.Windows.Forms.CheckBox();
            this.groupBox_SavedSetting = new System.Windows.Forms.GroupBox();
            this.checkBox_ASCII_Snd = new System.Windows.Forms.CheckBox();
            this.checkBox_ASCII_Rcv = new System.Windows.Forms.CheckBox();
            this.checkBox_esc_clear_data = new System.Windows.Forms.CheckBox();
            this.checkBox_MidMouseClear = new System.Windows.Forms.CheckBox();
            this.checkBox_Fliter = new System.Windows.Forms.CheckBox();
            this.checkBox_Backgroup = new System.Windows.Forms.CheckBox();
            this.checkBox_ClearRecvWhenFastSave = new System.Windows.Forms.CheckBox();
            this.button_FastSavePath = new System.Windows.Forms.Button();
            this.groupBox_NetCom = new System.Windows.Forms.GroupBox();
            this.label_ShowIP = new System.Windows.Forms.Label();
            this.button_NetRun = new System.Windows.Forms.Button();
            this.button_NetRole = new System.Windows.Forms.Button();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP4 = new System.Windows.Forms.TextBox();
            this.textBox_IP3 = new System.Windows.Forms.TextBox();
            this.textBox_IP2 = new System.Windows.Forms.TextBox();
            this.textBox_IP1 = new System.Windows.Forms.TextBox();
            this.button_ParmSave = new System.Windows.Forms.Button();
            this.tabPage_BAK = new System.Windows.Forms.TabPage();
            this.button_Test1 = new System.Windows.Forms.Button();
            this.textBox_Bakup = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer_backgroud = new System.Windows.Forms.Timer(this.components);
            this.tabPage_COM.SuspendLayout();
            this.groupBox_Uart.SuspendLayout();
            this.groupBox_BitCal.SuspendLayout();
            this.PageTag.SuspendLayout();
            this.tabPage_Config.SuspendLayout();
            this.groupBox_FastPrint.SuspendLayout();
            this.groupBox_eCMD.SuspendLayout();
            this.groupBox_COMSync.SuspendLayout();
            this.groupBox_CurrentSetting.SuspendLayout();
            this.groupBox_SavedSetting.SuspendLayout();
            this.groupBox_NetCom.SuspendLayout();
            this.tabPage_BAK.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_ColorShow
            // 
            this.timer_ColorShow.Tick += new System.EventHandler(this.timer_ColorShow_Tick);
            // 
            // tabPage_COM
            // 
            this.tabPage_COM.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage_COM.Controls.Add(this.textBox_ComRec);
            this.tabPage_COM.Controls.Add(this.textBox_ComSnd);
            this.tabPage_COM.Controls.Add(this.groupBox_Uart);
            this.tabPage_COM.Location = new System.Drawing.Point(4, 24);
            this.tabPage_COM.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_COM.Name = "tabPage_COM";
            this.tabPage_COM.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_COM.Size = new System.Drawing.Size(906, 553);
            this.tabPage_COM.TabIndex = 0;
            this.tabPage_COM.Text = "KCOM";
            // 
            // textBox_ComRec
            // 
            this.textBox_ComRec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ComRec.BackColor = System.Drawing.Color.White;
            this.textBox_ComRec.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_ComRec.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ComRec.Location = new System.Drawing.Point(2, 4);
            this.textBox_ComRec.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ComRec.MaxLength = 0;
            this.textBox_ComRec.Multiline = true;
            this.textBox_ComRec.Name = "textBox_ComRec";
            this.textBox_ComRec.ReadOnly = true;
            this.textBox_ComRec.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ComRec.Size = new System.Drawing.Size(760, 487);
            this.textBox_ComRec.TabIndex = 0;
            this.textBox_ComRec.WordWrap = false;
            this.textBox_ComRec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ComRec_KeyDown);
            this.textBox_ComRec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_ComRec_MouseDown);
            // 
            // textBox_ComSnd
            // 
            this.textBox_ComSnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ComSnd.Location = new System.Drawing.Point(2, 495);
            this.textBox_ComSnd.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ComSnd.Multiline = true;
            this.textBox_ComSnd.Name = "textBox_ComSnd";
            this.textBox_ComSnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ComSnd.Size = new System.Drawing.Size(760, 48);
            this.textBox_ComSnd.TabIndex = 0;
            this.textBox_ComSnd.TextChanged += new System.EventHandler(this.textBox_ComSnd_TextChanged);
            this.textBox_ComSnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ComSnd_KeyDown);
            this.textBox_ComSnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_ComSnd_MouseDown);
            // 
            // groupBox_Uart
            // 
            this.groupBox_Uart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Uart.Controls.Add(this.button_RunEXE);
            this.groupBox_Uart.Controls.Add(this.groupBox_BitCal);
            this.groupBox_Uart.Controls.Add(this.button_Test);
            this.groupBox_Uart.Controls.Add(this.label_Speed);
            this.groupBox_Uart.Controls.Add(this.label_DataRemain);
            this.groupBox_Uart.Controls.Add(this.label_MissData);
            this.groupBox_Uart.Controls.Add(this.label_RealTime);
            this.groupBox_Uart.Controls.Add(this.label7);
            this.groupBox_Uart.Controls.Add(this.textBox_AutoSndInterval_100ms);
            this.groupBox_Uart.Controls.Add(this.label_Rec_Bytes);
            this.groupBox_Uart.Controls.Add(this.checkBox_EnAutoSnd);
            this.groupBox_Uart.Controls.Add(this.button_TimeStamp);
            this.groupBox_Uart.Controls.Add(this.checkBox_CursorFixed);
            this.groupBox_Uart.Controls.Add(this.button_FastSave);
            this.groupBox_Uart.Controls.Add(this.label_Baudrate1);
            this.groupBox_Uart.Controls.Add(this.button_CleanSND);
            this.groupBox_Uart.Controls.Add(this.button_CreateLog);
            this.groupBox_Uart.Controls.Add(this.button_SendData);
            this.groupBox_Uart.Controls.Add(this.label_Send_Bytes);
            this.groupBox_Uart.Controls.Add(this.button_SaveFile);
            this.groupBox_Uart.Controls.Add(this.textBox_custom_baudrate);
            this.groupBox_Uart.Controls.Add(this.button_COMOpen);
            this.groupBox_Uart.Controls.Add(this.label13);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMStopBit);
            this.groupBox_Uart.Controls.Add(this.label_ClearRec);
            this.groupBox_Uart.Controls.Add(this.label12);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMDataBit);
            this.groupBox_Uart.Controls.Add(this.label11);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMCheckBit);
            this.groupBox_Uart.Controls.Add(this.label10);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMBaudrate);
            this.groupBox_Uart.Controls.Add(this.label_COM);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMNumber);
            this.groupBox_Uart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Uart.Location = new System.Drawing.Point(766, 4);
            this.groupBox_Uart.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Name = "groupBox_Uart";
            this.groupBox_Uart.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Size = new System.Drawing.Size(136, 744);
            this.groupBox_Uart.TabIndex = 10;
            this.groupBox_Uart.TabStop = false;
            this.groupBox_Uart.Text = "Uart Config";
            // 
            // button_RunEXE
            // 
            this.button_RunEXE.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_RunEXE.Location = new System.Drawing.Point(5, 481);
            this.button_RunEXE.Name = "button_RunEXE";
            this.button_RunEXE.Size = new System.Drawing.Size(120, 22);
            this.button_RunEXE.TabIndex = 64;
            this.button_RunEXE.Text = "Run EXE";
            this.button_RunEXE.UseVisualStyleBackColor = true;
            this.button_RunEXE.Click += new System.EventHandler(this.button_RunEXE_Click);
            // 
            // groupBox_BitCal
            // 
            this.groupBox_BitCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_BitCal.Controls.Add(this.textBox_bit);
            this.groupBox_BitCal.Controls.Add(this.textBox_Console);
            this.groupBox_BitCal.Controls.Add(this.button_Cal);
            this.groupBox_BitCal.Location = new System.Drawing.Point(5, 534);
            this.groupBox_BitCal.Name = "groupBox_BitCal";
            this.groupBox_BitCal.Size = new System.Drawing.Size(127, 196);
            this.groupBox_BitCal.TabIndex = 42;
            this.groupBox_BitCal.TabStop = false;
            this.groupBox_BitCal.Text = "BitCal";
            // 
            // textBox_bit
            // 
            this.textBox_bit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_bit.Location = new System.Drawing.Point(5, 167);
            this.textBox_bit.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_bit.Name = "textBox_bit";
            this.textBox_bit.Size = new System.Drawing.Size(84, 22);
            this.textBox_bit.TabIndex = 59;
            // 
            // textBox_Console
            // 
            this.textBox_Console.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Console.Location = new System.Drawing.Point(5, 20);
            this.textBox_Console.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Console.Multiline = true;
            this.textBox_Console.Name = "textBox_Console";
            this.textBox_Console.ReadOnly = true;
            this.textBox_Console.Size = new System.Drawing.Size(117, 143);
            this.textBox_Console.TabIndex = 57;
            // 
            // button_Cal
            // 
            this.button_Cal.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Cal.Location = new System.Drawing.Point(91, 166);
            this.button_Cal.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cal.Name = "button_Cal";
            this.button_Cal.Size = new System.Drawing.Size(33, 23);
            this.button_Cal.TabIndex = 58;
            this.button_Cal.Text = "CAL";
            this.button_Cal.UseVisualStyleBackColor = true;
            this.button_Cal.Click += new System.EventHandler(this.button_Cal_Click);
            // 
            // button_Test
            // 
            this.button_Test.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Test.Location = new System.Drawing.Point(5, 506);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(120, 22);
            this.button_Test.TabIndex = 60;
            this.button_Test.Text = "T";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // label_Speed
            // 
            this.label_Speed.AutoSize = true;
            this.label_Speed.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Speed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_Speed.Location = new System.Drawing.Point(4, 351);
            this.label_Speed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Speed.Name = "label_Speed";
            this.label_Speed.Size = new System.Drawing.Size(53, 14);
            this.label_Speed.TabIndex = 63;
            this.label_Speed.Text = "Speed: 0";
            // 
            // label_DataRemain
            // 
            this.label_DataRemain.AutoSize = true;
            this.label_DataRemain.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_DataRemain.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_DataRemain.Location = new System.Drawing.Point(4, 323);
            this.label_DataRemain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_DataRemain.Name = "label_DataRemain";
            this.label_DataRemain.Size = new System.Drawing.Size(61, 14);
            this.label_DataRemain.TabIndex = 62;
            this.label_DataRemain.Text = "Remain: 0";
            // 
            // label_MissData
            // 
            this.label_MissData.AutoSize = true;
            this.label_MissData.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_MissData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_MissData.Location = new System.Drawing.Point(4, 337);
            this.label_MissData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MissData.Name = "label_MissData";
            this.label_MissData.Size = new System.Drawing.Size(45, 14);
            this.label_MissData.TabIndex = 61;
            this.label_MissData.Text = "Miss: 0";
            // 
            // label_RealTime
            // 
            this.label_RealTime.AutoSize = true;
            this.label_RealTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RealTime.Location = new System.Drawing.Point(5, 464);
            this.label_RealTime.Name = "label_RealTime";
            this.label_RealTime.Size = new System.Drawing.Size(85, 14);
            this.label_RealTime.TabIndex = 41;
            this.label_RealTime.Text = "_____________";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 395);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "Interval(100ms):";
            // 
            // textBox_AutoSndInterval_100ms
            // 
            this.textBox_AutoSndInterval_100ms.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_AutoSndInterval_100ms.Location = new System.Drawing.Point(97, 391);
            this.textBox_AutoSndInterval_100ms.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_AutoSndInterval_100ms.Name = "textBox_AutoSndInterval_100ms";
            this.textBox_AutoSndInterval_100ms.Size = new System.Drawing.Size(33, 22);
            this.textBox_AutoSndInterval_100ms.TabIndex = 11;
            this.textBox_AutoSndInterval_100ms.TextChanged += new System.EventHandler(this.textBox_AutoSndInterval_100ms_TextChanged);
            // 
            // label_Rec_Bytes
            // 
            this.label_Rec_Bytes.AutoSize = true;
            this.label_Rec_Bytes.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Rec_Bytes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_Rec_Bytes.Location = new System.Drawing.Point(4, 309);
            this.label_Rec_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Rec_Bytes.Name = "label_Rec_Bytes";
            this.label_Rec_Bytes.Size = new System.Drawing.Size(68, 14);
            this.label_Rec_Bytes.TabIndex = 5;
            this.label_Rec_Bytes.Text = "Received: 0";
            // 
            // checkBox_EnAutoSnd
            // 
            this.checkBox_EnAutoSnd.AutoSize = true;
            this.checkBox_EnAutoSnd.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_EnAutoSnd.Location = new System.Drawing.Point(7, 379);
            this.checkBox_EnAutoSnd.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnAutoSnd.Name = "checkBox_EnAutoSnd";
            this.checkBox_EnAutoSnd.Size = new System.Drawing.Size(81, 18);
            this.checkBox_EnAutoSnd.TabIndex = 12;
            this.checkBox_EnAutoSnd.Text = "Auto Send";
            this.checkBox_EnAutoSnd.UseVisualStyleBackColor = true;
            this.checkBox_EnAutoSnd.CheckedChanged += new System.EventHandler(this.checkBox_EnAutoSnd_CheckedChanged);
            // 
            // button_TimeStamp
            // 
            this.button_TimeStamp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TimeStamp.ForeColor = System.Drawing.Color.Gray;
            this.button_TimeStamp.Location = new System.Drawing.Point(5, 206);
            this.button_TimeStamp.Margin = new System.Windows.Forms.Padding(2);
            this.button_TimeStamp.Name = "button_TimeStamp";
            this.button_TimeStamp.Size = new System.Drawing.Size(120, 22);
            this.button_TimeStamp.TabIndex = 40;
            this.button_TimeStamp.Text = "Time stamp";
            this.button_TimeStamp.UseVisualStyleBackColor = true;
            this.button_TimeStamp.Click += new System.EventHandler(this.button_TimeStamp_Click);
            // 
            // checkBox_CursorFixed
            // 
            this.checkBox_CursorFixed.AutoSize = true;
            this.checkBox_CursorFixed.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_CursorFixed.Location = new System.Drawing.Point(7, 294);
            this.checkBox_CursorFixed.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_CursorFixed.Name = "checkBox_CursorFixed";
            this.checkBox_CursorFixed.Size = new System.Drawing.Size(88, 18);
            this.checkBox_CursorFixed.TabIndex = 42;
            this.checkBox_CursorFixed.Text = "Cursor fixed";
            this.checkBox_CursorFixed.UseVisualStyleBackColor = true;
            this.checkBox_CursorFixed.CheckedChanged += new System.EventHandler(this.checkBox_CursorFixed_CheckedChanged);
            // 
            // button_FastSave
            // 
            this.button_FastSave.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FastSave.Location = new System.Drawing.Point(5, 272);
            this.button_FastSave.Margin = new System.Windows.Forms.Padding(2);
            this.button_FastSave.Name = "button_FastSave";
            this.button_FastSave.Size = new System.Drawing.Size(120, 22);
            this.button_FastSave.TabIndex = 43;
            this.button_FastSave.Text = "Fast Save";
            this.button_FastSave.UseVisualStyleBackColor = true;
            this.button_FastSave.Click += new System.EventHandler(this.button_FastSave_Click);
            // 
            // label_Baudrate1
            // 
            this.label_Baudrate1.AutoSize = true;
            this.label_Baudrate1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Baudrate1.Location = new System.Drawing.Point(1, 137);
            this.label_Baudrate1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Baudrate1.Name = "label_Baudrate1";
            this.label_Baudrate1.Size = new System.Drawing.Size(67, 14);
            this.label_Baudrate1.TabIndex = 58;
            this.label_Baudrate1.Text = "Cstm Baud:";
            // 
            // button_CleanSND
            // 
            this.button_CleanSND.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CleanSND.Location = new System.Drawing.Point(5, 414);
            this.button_CleanSND.Margin = new System.Windows.Forms.Padding(2);
            this.button_CleanSND.Name = "button_CleanSND";
            this.button_CleanSND.Size = new System.Drawing.Size(120, 22);
            this.button_CleanSND.TabIndex = 6;
            this.button_CleanSND.Text = "Send Clear";
            this.button_CleanSND.UseVisualStyleBackColor = true;
            this.button_CleanSND.Click += new System.EventHandler(this.button_CleanSND_Click);
            // 
            // button_CreateLog
            // 
            this.button_CreateLog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_CreateLog.Location = new System.Drawing.Point(5, 228);
            this.button_CreateLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_CreateLog.Name = "button_CreateLog";
            this.button_CreateLog.Size = new System.Drawing.Size(120, 22);
            this.button_CreateLog.TabIndex = 15;
            this.button_CreateLog.Text = "Create log";
            this.button_CreateLog.UseVisualStyleBackColor = true;
            this.button_CreateLog.Click += new System.EventHandler(this.button_CreateLog_Click);
            // 
            // button_SendData
            // 
            this.button_SendData.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SendData.ForeColor = System.Drawing.Color.Blue;
            this.button_SendData.Location = new System.Drawing.Point(5, 437);
            this.button_SendData.Margin = new System.Windows.Forms.Padding(2);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(120, 22);
            this.button_SendData.TabIndex = 5;
            this.button_SendData.Text = "Data Send";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendData_Click);
            // 
            // label_Send_Bytes
            // 
            this.label_Send_Bytes.AutoSize = true;
            this.label_Send_Bytes.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Send_Bytes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_Send_Bytes.Location = new System.Drawing.Point(4, 365);
            this.label_Send_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Send_Bytes.Name = "label_Send_Bytes";
            this.label_Send_Bytes.Size = new System.Drawing.Size(43, 14);
            this.label_Send_Bytes.TabIndex = 7;
            this.label_Send_Bytes.Text = "Sent: 0";
            // 
            // button_SaveFile
            // 
            this.button_SaveFile.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SaveFile.Location = new System.Drawing.Point(5, 250);
            this.button_SaveFile.Margin = new System.Windows.Forms.Padding(2);
            this.button_SaveFile.Name = "button_SaveFile";
            this.button_SaveFile.Size = new System.Drawing.Size(120, 22);
            this.button_SaveFile.TabIndex = 10;
            this.button_SaveFile.Text = "Save file";
            this.button_SaveFile.UseVisualStyleBackColor = true;
            this.button_SaveFile.Click += new System.EventHandler(this.button_SaveFile_Click);
            // 
            // textBox_custom_baudrate
            // 
            this.textBox_custom_baudrate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_custom_baudrate.Location = new System.Drawing.Point(70, 133);
            this.textBox_custom_baudrate.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_custom_baudrate.Name = "textBox_custom_baudrate";
            this.textBox_custom_baudrate.Size = new System.Drawing.Size(59, 22);
            this.textBox_custom_baudrate.TabIndex = 57;
            this.textBox_custom_baudrate.Text = "1222400";
            this.textBox_custom_baudrate.TextChanged += new System.EventHandler(this.textBox_custom_baudrate_TextChanged);
            // 
            // button_COMOpen
            // 
            this.button_COMOpen.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_COMOpen.ForeColor = System.Drawing.Color.Red;
            this.button_COMOpen.Location = new System.Drawing.Point(5, 158);
            this.button_COMOpen.Margin = new System.Windows.Forms.Padding(2);
            this.button_COMOpen.Name = "button_COMOpen";
            this.button_COMOpen.Size = new System.Drawing.Size(120, 22);
            this.button_COMOpen.TabIndex = 11;
            this.button_COMOpen.Text = "COM is closed";
            this.button_COMOpen.UseVisualStyleBackColor = true;
            this.button_COMOpen.Click += new System.EventHandler(this.button_COMOpen_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 113);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 14);
            this.label13.TabIndex = 19;
            this.label13.Text = "Stop:";
            // 
            // comboBox_COMStopBit
            // 
            this.comboBox_COMStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMStopBit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_COMStopBit.FormattingEnabled = true;
            this.comboBox_COMStopBit.Location = new System.Drawing.Point(39, 109);
            this.comboBox_COMStopBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMStopBit.Name = "comboBox_COMStopBit";
            this.comboBox_COMStopBit.Size = new System.Drawing.Size(90, 22);
            this.comboBox_COMStopBit.TabIndex = 18;
            this.comboBox_COMStopBit.DropDown += new System.EventHandler(this.comboBox_COMStopBit_DropDown);
            this.comboBox_COMStopBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMStopBit_SelectedIndexChanged);
            // 
            // label_ClearRec
            // 
            this.label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
            this.label_ClearRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ClearRec.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ClearRec.ForeColor = System.Drawing.Color.Magenta;
            this.label_ClearRec.Location = new System.Drawing.Point(5, 182);
            this.label_ClearRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ClearRec.Name = "label_ClearRec";
            this.label_ClearRec.Size = new System.Drawing.Size(120, 22);
            this.label_ClearRec.TabIndex = 40;
            this.label_ClearRec.Text = "Recv Clear";
            this.label_ClearRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_ClearRec.DoubleClick += new System.EventHandler(this.label_ClearRec_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 90);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 14);
            this.label12.TabIndex = 17;
            this.label12.Text = "Data:";
            // 
            // comboBox_COMDataBit
            // 
            this.comboBox_COMDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMDataBit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_COMDataBit.FormattingEnabled = true;
            this.comboBox_COMDataBit.Location = new System.Drawing.Point(39, 86);
            this.comboBox_COMDataBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMDataBit.Name = "comboBox_COMDataBit";
            this.comboBox_COMDataBit.Size = new System.Drawing.Size(90, 22);
            this.comboBox_COMDataBit.TabIndex = 16;
            this.comboBox_COMDataBit.DropDown += new System.EventHandler(this.comboBox_COMDataBit_DropDown);
            this.comboBox_COMDataBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMDataBit_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1, 67);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 14);
            this.label11.TabIndex = 15;
            this.label11.Text = "Chk:";
            // 
            // comboBox_COMCheckBit
            // 
            this.comboBox_COMCheckBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMCheckBit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_COMCheckBit.FormattingEnabled = true;
            this.comboBox_COMCheckBit.Location = new System.Drawing.Point(39, 63);
            this.comboBox_COMCheckBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMCheckBit.Name = "comboBox_COMCheckBit";
            this.comboBox_COMCheckBit.Size = new System.Drawing.Size(90, 22);
            this.comboBox_COMCheckBit.TabIndex = 14;
            this.comboBox_COMCheckBit.DropDown += new System.EventHandler(this.comboBox_COMCheckBit_DropDown);
            this.comboBox_COMCheckBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMCheckBit_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1, 44);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "Baud:";
            // 
            // comboBox_COMBaudrate
            // 
            this.comboBox_COMBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMBaudrate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_COMBaudrate.FormattingEnabled = true;
            this.comboBox_COMBaudrate.Location = new System.Drawing.Point(39, 40);
            this.comboBox_COMBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMBaudrate.Name = "comboBox_COMBaudrate";
            this.comboBox_COMBaudrate.Size = new System.Drawing.Size(90, 22);
            this.comboBox_COMBaudrate.TabIndex = 12;
            this.comboBox_COMBaudrate.DropDown += new System.EventHandler(this.comboBox_COMBaudrate_DropDown);
            this.comboBox_COMBaudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMBaudrate_SelectedIndexChanged);
            // 
            // label_COM
            // 
            this.label_COM.AutoSize = true;
            this.label_COM.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_COM.Location = new System.Drawing.Point(1, 20);
            this.label_COM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_COM.Name = "label_COM";
            this.label_COM.Size = new System.Drawing.Size(34, 14);
            this.label_COM.TabIndex = 11;
            this.label_COM.Text = "COM:";
            // 
            // comboBox_COMNumber
            // 
            this.comboBox_COMNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMNumber.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_COMNumber.FormattingEnabled = true;
            this.comboBox_COMNumber.Location = new System.Drawing.Point(39, 17);
            this.comboBox_COMNumber.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMNumber.Name = "comboBox_COMNumber";
            this.comboBox_COMNumber.Size = new System.Drawing.Size(90, 22);
            this.comboBox_COMNumber.TabIndex = 0;
            this.comboBox_COMNumber.DropDown += new System.EventHandler(this.comboBox_COMNumber_DropDown);
            this.comboBox_COMNumber.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMNumber_SelectedIndexChanged);
            this.comboBox_COMNumber.DropDownClosed += new System.EventHandler(this.comboBox_COMNumber_DropDownClosed);
            // 
            // textBox_Message
            // 
            this.textBox_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Message.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_Message.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Message.ForeColor = System.Drawing.Color.Black;
            this.textBox_Message.Location = new System.Drawing.Point(8, 458);
            this.textBox_Message.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Message.Size = new System.Drawing.Size(893, 85);
            this.textBox_Message.TabIndex = 11;
            // 
            // checkBox_FastPrintf
            // 
            this.checkBox_FastPrintf.AutoSize = true;
            this.checkBox_FastPrintf.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_FastPrintf.Location = new System.Drawing.Point(5, 20);
            this.checkBox_FastPrintf.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_FastPrintf.Name = "checkBox_FastPrintf";
            this.checkBox_FastPrintf.Size = new System.Drawing.Size(80, 18);
            this.checkBox_FastPrintf.TabIndex = 59;
            this.checkBox_FastPrintf.Text = "Fast Printf";
            this.checkBox_FastPrintf.UseVisualStyleBackColor = true;
            this.checkBox_FastPrintf.CheckedChanged += new System.EventHandler(this.checkBox_FastPrintf_CheckedChanged);
            // 
            // button_COMSyncOpen
            // 
            this.button_COMSyncOpen.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_COMSyncOpen.ForeColor = System.Drawing.Color.Red;
            this.button_COMSyncOpen.Location = new System.Drawing.Point(11, 74);
            this.button_COMSyncOpen.Margin = new System.Windows.Forms.Padding(2);
            this.button_COMSyncOpen.Name = "button_COMSyncOpen";
            this.button_COMSyncOpen.Size = new System.Drawing.Size(183, 21);
            this.button_COMSyncOpen.TabIndex = 66;
            this.button_COMSyncOpen.Text = "COM is closed";
            this.button_COMSyncOpen.UseVisualStyleBackColor = true;
            this.button_COMSyncOpen.Click += new System.EventHandler(this.button_COMSyncOpen_Click);
            // 
            // label_COM_Sync
            // 
            this.label_COM_Sync.AutoSize = true;
            this.label_COM_Sync.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_COM_Sync.Location = new System.Drawing.Point(9, 27);
            this.label_COM_Sync.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_COM_Sync.Name = "label_COM_Sync";
            this.label_COM_Sync.Size = new System.Drawing.Size(60, 14);
            this.label_COM_Sync.TabIndex = 65;
            this.label_COM_Sync.Text = "Sync COM:";
            // 
            // comboBox_SyncComNum
            // 
            this.comboBox_SyncComNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SyncComNum.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SyncComNum.FormattingEnabled = true;
            this.comboBox_SyncComNum.Location = new System.Drawing.Point(73, 24);
            this.comboBox_SyncComNum.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_SyncComNum.Name = "comboBox_SyncComNum";
            this.comboBox_SyncComNum.Size = new System.Drawing.Size(121, 22);
            this.comboBox_SyncComNum.TabIndex = 64;
            this.comboBox_SyncComNum.DropDown += new System.EventHandler(this.comboBox_SyncComNum_DropDown);
            this.comboBox_SyncComNum.SelectedIndexChanged += new System.EventHandler(this.comboBox_SyncComNum_SelectedIndexChanged);
            this.comboBox_SyncComNum.DropDownClosed += new System.EventHandler(this.comboBox_SyncComNum_DropDownClosed);
            // 
            // checkBox_Cmdline
            // 
            this.checkBox_Cmdline.AutoSize = true;
            this.checkBox_Cmdline.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Cmdline.Location = new System.Drawing.Point(7, 57);
            this.checkBox_Cmdline.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Cmdline.Name = "checkBox_Cmdline";
            this.checkBox_Cmdline.Size = new System.Drawing.Size(71, 18);
            this.checkBox_Cmdline.TabIndex = 42;
            this.checkBox_Cmdline.Text = "Cmdline";
            this.checkBox_Cmdline.UseVisualStyleBackColor = true;
            this.checkBox_Cmdline.CheckedChanged += new System.EventHandler(this.checkBox_Cmdline_CheckedChanged);
            // 
            // checkBox_LimitRecLen
            // 
            this.checkBox_LimitRecLen.AutoSize = true;
            this.checkBox_LimitRecLen.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_LimitRecLen.Location = new System.Drawing.Point(7, 38);
            this.checkBox_LimitRecLen.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_LimitRecLen.Name = "checkBox_LimitRecLen";
            this.checkBox_LimitRecLen.Size = new System.Drawing.Size(110, 18);
            this.checkBox_LimitRecLen.TabIndex = 41;
            this.checkBox_LimitRecLen.Text = "Max recv length";
            this.checkBox_LimitRecLen.UseVisualStyleBackColor = true;
            this.checkBox_LimitRecLen.CheckedChanged += new System.EventHandler(this.checkBox_LimitRecLen_CheckedChanged);
            // 
            // checkBox_Color
            // 
            this.checkBox_Color.AutoSize = true;
            this.checkBox_Color.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Color.Location = new System.Drawing.Point(7, 19);
            this.checkBox_Color.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Color.Name = "checkBox_Color";
            this.checkBox_Color.Size = new System.Drawing.Size(77, 18);
            this.checkBox_Color.TabIndex = 14;
            this.checkBox_Color.Text = "Anti color";
            this.checkBox_Color.UseVisualStyleBackColor = true;
            this.checkBox_Color.CheckedChanged += new System.EventHandler(this.checkBox_Color_CheckedChanged);
            // 
            // button_FontSize
            // 
            this.button_FontSize.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FontSize.Location = new System.Drawing.Point(264, 13);
            this.button_FontSize.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSize.Name = "button_FontSize";
            this.button_FontSize.Size = new System.Drawing.Size(80, 22);
            this.button_FontSize.TabIndex = 13;
            this.button_FontSize.Text = "Courier New";
            this.button_FontSize.UseVisualStyleBackColor = true;
            this.button_FontSize.Click += new System.EventHandler(this.button_FontSize_Click);
            // 
            // button_FontBigger
            // 
            this.button_FontBigger.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FontBigger.Location = new System.Drawing.Point(92, 13);
            this.button_FontBigger.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontBigger.Name = "button_FontBigger";
            this.button_FontBigger.Size = new System.Drawing.Size(80, 22);
            this.button_FontBigger.TabIndex = 12;
            this.button_FontBigger.Text = "Font+";
            this.button_FontBigger.UseVisualStyleBackColor = true;
            this.button_FontBigger.Click += new System.EventHandler(this.button_FontBigger_Click);
            // 
            // button_FontSmaller
            // 
            this.button_FontSmaller.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FontSmaller.Location = new System.Drawing.Point(178, 13);
            this.button_FontSmaller.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSmaller.Name = "button_FontSmaller";
            this.button_FontSmaller.Size = new System.Drawing.Size(80, 22);
            this.button_FontSmaller.TabIndex = 11;
            this.button_FontSmaller.Text = "Font-";
            this.button_FontSmaller.UseVisualStyleBackColor = true;
            this.button_FontSmaller.Click += new System.EventHandler(this.button_FontSmaller_Click);
            // 
            // PageTag
            // 
            this.PageTag.Controls.Add(this.tabPage_COM);
            this.PageTag.Controls.Add(this.tabPage_Config);
            this.PageTag.Controls.Add(this.tabPage_BAK);
            this.PageTag.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PageTag.Location = new System.Drawing.Point(0, 0);
            this.PageTag.Margin = new System.Windows.Forms.Padding(0);
            this.PageTag.Name = "PageTag";
            this.PageTag.SelectedIndex = 0;
            this.PageTag.Size = new System.Drawing.Size(914, 581);
            this.PageTag.TabIndex = 0;
            // 
            // tabPage_Config
            // 
            this.tabPage_Config.Controls.Add(this.textBox_Message);
            this.tabPage_Config.Controls.Add(this.groupBox_FastPrint);
            this.tabPage_Config.Controls.Add(this.groupBox_eCMD);
            this.tabPage_Config.Controls.Add(this.button_SysFont);
            this.tabPage_Config.Controls.Add(this.groupBox_COMSync);
            this.tabPage_Config.Controls.Add(this.groupBox_CurrentSetting);
            this.tabPage_Config.Controls.Add(this.groupBox_SavedSetting);
            this.tabPage_Config.Controls.Add(this.button_FastSavePath);
            this.tabPage_Config.Controls.Add(this.groupBox_NetCom);
            this.tabPage_Config.Controls.Add(this.button_FontSmaller);
            this.tabPage_Config.Controls.Add(this.button_FontBigger);
            this.tabPage_Config.Controls.Add(this.button_ParmSave);
            this.tabPage_Config.Controls.Add(this.button_FontSize);
            this.tabPage_Config.Location = new System.Drawing.Point(4, 24);
            this.tabPage_Config.Name = "tabPage_Config";
            this.tabPage_Config.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Config.Size = new System.Drawing.Size(906, 553);
            this.tabPage_Config.TabIndex = 2;
            this.tabPage_Config.Text = "Config";
            this.tabPage_Config.UseVisualStyleBackColor = true;
            // 
            // groupBox_FastPrint
            // 
            this.groupBox_FastPrint.Controls.Add(this.checkBox_FastPrintf);
            this.groupBox_FastPrint.Controls.Add(this.button_FPSelect_HEX);
            this.groupBox_FastPrint.Location = new System.Drawing.Point(8, 292);
            this.groupBox_FastPrint.Name = "groupBox_FastPrint";
            this.groupBox_FastPrint.Size = new System.Drawing.Size(653, 98);
            this.groupBox_FastPrint.TabIndex = 74;
            this.groupBox_FastPrint.TabStop = false;
            this.groupBox_FastPrint.Text = "FastPrintf";
            // 
            // button_FPSelect_HEX
            // 
            this.button_FPSelect_HEX.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FPSelect_HEX.Location = new System.Drawing.Point(5, 43);
            this.button_FPSelect_HEX.Name = "button_FPSelect_HEX";
            this.button_FPSelect_HEX.Size = new System.Drawing.Size(644, 47);
            this.button_FPSelect_HEX.TabIndex = 67;
            this.button_FPSelect_HEX.Text = "FP HEX path:(Select)";
            this.button_FPSelect_HEX.UseVisualStyleBackColor = true;
            this.button_FPSelect_HEX.Click += new System.EventHandler(this.button_FPSelect_HEX_Click);
            // 
            // groupBox_eCMD
            // 
            this.groupBox_eCMD.Controls.Add(this.textBox_RunExeCode);
            this.groupBox_eCMD.Controls.Add(this.label_RunExeCode);
            this.groupBox_eCMD.Controls.Add(this.checkBox_eCMD);
            this.groupBox_eCMD.Controls.Add(this.button_SelectEXE);
            this.groupBox_eCMD.Location = new System.Drawing.Point(8, 390);
            this.groupBox_eCMD.Name = "groupBox_eCMD";
            this.groupBox_eCMD.Size = new System.Drawing.Size(653, 63);
            this.groupBox_eCMD.TabIndex = 72;
            this.groupBox_eCMD.TabStop = false;
            this.groupBox_eCMD.Text = "eCMD";
            // 
            // textBox_RunExeCode
            // 
            this.textBox_RunExeCode.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_RunExeCode.Location = new System.Drawing.Point(189, 12);
            this.textBox_RunExeCode.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_RunExeCode.Name = "textBox_RunExeCode";
            this.textBox_RunExeCode.Size = new System.Drawing.Size(460, 22);
            this.textBox_RunExeCode.TabIndex = 60;
            this.textBox_RunExeCode.TextChanged += new System.EventHandler(this.textBox_RunExeCode_TextChanged);
            // 
            // label_RunExeCode
            // 
            this.label_RunExeCode.AutoSize = true;
            this.label_RunExeCode.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RunExeCode.Location = new System.Drawing.Point(103, 15);
            this.label_RunExeCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_RunExeCode.Name = "label_RunExeCode";
            this.label_RunExeCode.Size = new System.Drawing.Size(82, 14);
            this.label_RunExeCode.TabIndex = 69;
            this.label_RunExeCode.Text = "Run exe code:";
            // 
            // checkBox_eCMD
            // 
            this.checkBox_eCMD.AutoSize = true;
            this.checkBox_eCMD.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_eCMD.Location = new System.Drawing.Point(4, 14);
            this.checkBox_eCMD.Name = "checkBox_eCMD";
            this.checkBox_eCMD.Size = new System.Drawing.Size(81, 18);
            this.checkBox_eCMD.TabIndex = 64;
            this.checkBox_eCMD.Text = "Run eCMD";
            this.checkBox_eCMD.UseVisualStyleBackColor = true;
            this.checkBox_eCMD.CheckedChanged += new System.EventHandler(this.checkBox_eCMD_CheckedChanged);
            // 
            // button_SelectEXE
            // 
            this.button_SelectEXE.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SelectEXE.Location = new System.Drawing.Point(4, 35);
            this.button_SelectEXE.Name = "button_SelectEXE";
            this.button_SelectEXE.Size = new System.Drawing.Size(645, 22);
            this.button_SelectEXE.TabIndex = 73;
            this.button_SelectEXE.Text = "Default EXE:(select)";
            this.button_SelectEXE.UseVisualStyleBackColor = true;
            this.button_SelectEXE.Click += new System.EventHandler(this.button_SelectEXE_Click);
            // 
            // button_SysFont
            // 
            this.button_SysFont.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SysFont.Location = new System.Drawing.Point(348, 13);
            this.button_SysFont.Margin = new System.Windows.Forms.Padding(2);
            this.button_SysFont.Name = "button_SysFont";
            this.button_SysFont.Size = new System.Drawing.Size(80, 22);
            this.button_SysFont.TabIndex = 72;
            this.button_SysFont.Text = "Sys Font";
            this.button_SysFont.UseVisualStyleBackColor = true;
            this.button_SysFont.Click += new System.EventHandler(this.button_SysFont_Click);
            // 
            // groupBox_COMSync
            // 
            this.groupBox_COMSync.Controls.Add(this.comboBox_SyncBaud);
            this.groupBox_COMSync.Controls.Add(this.label_SyncBaudRate);
            this.groupBox_COMSync.Controls.Add(this.button_COMSyncOpen);
            this.groupBox_COMSync.Controls.Add(this.comboBox_SyncComNum);
            this.groupBox_COMSync.Controls.Add(this.label_COM_Sync);
            this.groupBox_COMSync.Location = new System.Drawing.Point(264, 189);
            this.groupBox_COMSync.Name = "groupBox_COMSync";
            this.groupBox_COMSync.Size = new System.Drawing.Size(214, 101);
            this.groupBox_COMSync.TabIndex = 71;
            this.groupBox_COMSync.TabStop = false;
            this.groupBox_COMSync.Text = "COM Sync";
            // 
            // comboBox_SyncBaud
            // 
            this.comboBox_SyncBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SyncBaud.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SyncBaud.FormattingEnabled = true;
            this.comboBox_SyncBaud.Location = new System.Drawing.Point(73, 49);
            this.comboBox_SyncBaud.Name = "comboBox_SyncBaud";
            this.comboBox_SyncBaud.Size = new System.Drawing.Size(121, 22);
            this.comboBox_SyncBaud.TabIndex = 68;
            this.comboBox_SyncBaud.DropDown += new System.EventHandler(this.comboBox_SyncBaud_DropDown);
            this.comboBox_SyncBaud.SelectedIndexChanged += new System.EventHandler(this.comboBox_SyncBaud_SelectedIndexChanged);
            // 
            // label_SyncBaudRate
            // 
            this.label_SyncBaudRate.AutoSize = true;
            this.label_SyncBaudRate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SyncBaudRate.Location = new System.Drawing.Point(9, 52);
            this.label_SyncBaudRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SyncBaudRate.Name = "label_SyncBaudRate";
            this.label_SyncBaudRate.Size = new System.Drawing.Size(64, 14);
            this.label_SyncBaudRate.TabIndex = 67;
            this.label_SyncBaudRate.Text = "Sync Baud:";
            // 
            // groupBox_CurrentSetting
            // 
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_DbgLog);
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_EnableBakup);
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_WordWrap);
            this.groupBox_CurrentSetting.Location = new System.Drawing.Point(264, 64);
            this.groupBox_CurrentSetting.Name = "groupBox_CurrentSetting";
            this.groupBox_CurrentSetting.Size = new System.Drawing.Size(214, 119);
            this.groupBox_CurrentSetting.TabIndex = 70;
            this.groupBox_CurrentSetting.TabStop = false;
            this.groupBox_CurrentSetting.Text = "Current Setting";
            // 
            // checkBox_DbgLog
            // 
            this.checkBox_DbgLog.AutoSize = true;
            this.checkBox_DbgLog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_DbgLog.Location = new System.Drawing.Point(6, 56);
            this.checkBox_DbgLog.Name = "checkBox_DbgLog";
            this.checkBox_DbgLog.Size = new System.Drawing.Size(118, 18);
            this.checkBox_DbgLog.TabIndex = 63;
            this.checkBox_DbgLog.Text = "Create debug log";
            this.checkBox_DbgLog.UseVisualStyleBackColor = true;
            this.checkBox_DbgLog.CheckedChanged += new System.EventHandler(this.checkBox_DbgLog_CheckedChanged);
            // 
            // checkBox_EnableBakup
            // 
            this.checkBox_EnableBakup.AutoSize = true;
            this.checkBox_EnableBakup.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_EnableBakup.Location = new System.Drawing.Point(6, 37);
            this.checkBox_EnableBakup.Name = "checkBox_EnableBakup";
            this.checkBox_EnableBakup.Size = new System.Drawing.Size(101, 18);
            this.checkBox_EnableBakup.TabIndex = 62;
            this.checkBox_EnableBakup.Text = "Enable Bakup";
            this.checkBox_EnableBakup.UseVisualStyleBackColor = true;
            this.checkBox_EnableBakup.CheckedChanged += new System.EventHandler(this.checkBox_EnableBakup_CheckedChanged);
            // 
            // checkBox_WordWrap
            // 
            this.checkBox_WordWrap.AutoSize = true;
            this.checkBox_WordWrap.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_WordWrap.Location = new System.Drawing.Point(6, 20);
            this.checkBox_WordWrap.Name = "checkBox_WordWrap";
            this.checkBox_WordWrap.Size = new System.Drawing.Size(86, 18);
            this.checkBox_WordWrap.TabIndex = 61;
            this.checkBox_WordWrap.Text = "Word Wrap";
            this.checkBox_WordWrap.UseVisualStyleBackColor = true;
            this.checkBox_WordWrap.CheckedChanged += new System.EventHandler(this.checkBox_WordWrap_CheckedChanged);
            // 
            // groupBox_SavedSetting
            // 
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_ASCII_Snd);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_ASCII_Rcv);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_esc_clear_data);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_MidMouseClear);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_Fliter);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_Color);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_Backgroup);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_ClearRecvWhenFastSave);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_Cmdline);
            this.groupBox_SavedSetting.Controls.Add(this.checkBox_LimitRecLen);
            this.groupBox_SavedSetting.Location = new System.Drawing.Point(8, 63);
            this.groupBox_SavedSetting.Name = "groupBox_SavedSetting";
            this.groupBox_SavedSetting.Size = new System.Drawing.Size(242, 227);
            this.groupBox_SavedSetting.TabIndex = 69;
            this.groupBox_SavedSetting.TabStop = false;
            this.groupBox_SavedSetting.Text = "Saved Setting";
            // 
            // checkBox_ASCII_Snd
            // 
            this.checkBox_ASCII_Snd.AutoSize = true;
            this.checkBox_ASCII_Snd.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ASCII_Snd.Location = new System.Drawing.Point(7, 190);
            this.checkBox_ASCII_Snd.Name = "checkBox_ASCII_Snd";
            this.checkBox_ASCII_Snd.Size = new System.Drawing.Size(111, 18);
            this.checkBox_ASCII_Snd.TabIndex = 47;
            this.checkBox_ASCII_Snd.Text = "ASCII send data";
            this.checkBox_ASCII_Snd.UseVisualStyleBackColor = true;
            this.checkBox_ASCII_Snd.CheckedChanged += new System.EventHandler(this.checkBox_ASCII_Snd_CheckedChanged);
            // 
            // checkBox_ASCII_Rcv
            // 
            this.checkBox_ASCII_Rcv.AutoSize = true;
            this.checkBox_ASCII_Rcv.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ASCII_Rcv.Location = new System.Drawing.Point(7, 171);
            this.checkBox_ASCII_Rcv.Name = "checkBox_ASCII_Rcv";
            this.checkBox_ASCII_Rcv.Size = new System.Drawing.Size(123, 18);
            this.checkBox_ASCII_Rcv.TabIndex = 46;
            this.checkBox_ASCII_Rcv.Text = "ASCII receive data";
            this.checkBox_ASCII_Rcv.UseVisualStyleBackColor = true;
            this.checkBox_ASCII_Rcv.CheckedChanged += new System.EventHandler(this.checkBox_ASCII_Rcv_CheckedChanged);
            // 
            // checkBox_esc_clear_data
            // 
            this.checkBox_esc_clear_data.AutoSize = true;
            this.checkBox_esc_clear_data.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_esc_clear_data.Location = new System.Drawing.Point(7, 152);
            this.checkBox_esc_clear_data.Name = "checkBox_esc_clear_data";
            this.checkBox_esc_clear_data.Size = new System.Drawing.Size(102, 18);
            this.checkBox_esc_clear_data.TabIndex = 45;
            this.checkBox_esc_clear_data.Text = "ESC clear data";
            this.checkBox_esc_clear_data.UseVisualStyleBackColor = true;
            this.checkBox_esc_clear_data.CheckedChanged += new System.EventHandler(this.checkBox_esc_clear_data_CheckedChanged);
            // 
            // checkBox_MidMouseClear
            // 
            this.checkBox_MidMouseClear.AutoSize = true;
            this.checkBox_MidMouseClear.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_MidMouseClear.Location = new System.Drawing.Point(7, 133);
            this.checkBox_MidMouseClear.Name = "checkBox_MidMouseClear";
            this.checkBox_MidMouseClear.Size = new System.Drawing.Size(163, 18);
            this.checkBox_MidMouseClear.TabIndex = 44;
            this.checkBox_MidMouseClear.Text = "middle mouse clear data";
            this.checkBox_MidMouseClear.UseVisualStyleBackColor = true;
            this.checkBox_MidMouseClear.CheckedChanged += new System.EventHandler(this.checkBox_MidMouseClear_CheckedChanged);
            // 
            // checkBox_Fliter
            // 
            this.checkBox_Fliter.AutoSize = true;
            this.checkBox_Fliter.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Fliter.Location = new System.Drawing.Point(7, 114);
            this.checkBox_Fliter.Name = "checkBox_Fliter";
            this.checkBox_Fliter.Size = new System.Drawing.Size(119, 18);
            this.checkBox_Fliter.TabIndex = 43;
            this.checkBox_Fliter.Text = "fliter ileagal char";
            this.checkBox_Fliter.UseVisualStyleBackColor = true;
            this.checkBox_Fliter.CheckedChanged += new System.EventHandler(this.checkBox_Fliter_CheckedChanged);
            // 
            // checkBox_Backgroup
            // 
            this.checkBox_Backgroup.AutoSize = true;
            this.checkBox_Backgroup.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Backgroup.Location = new System.Drawing.Point(7, 76);
            this.checkBox_Backgroup.Name = "checkBox_Backgroup";
            this.checkBox_Backgroup.Size = new System.Drawing.Size(120, 18);
            this.checkBox_Backgroup.TabIndex = 2;
            this.checkBox_Backgroup.Text = "Run in Backgroup";
            this.checkBox_Backgroup.UseVisualStyleBackColor = true;
            // 
            // checkBox_ClearRecvWhenFastSave
            // 
            this.checkBox_ClearRecvWhenFastSave.AutoSize = true;
            this.checkBox_ClearRecvWhenFastSave.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ClearRecvWhenFastSave.Location = new System.Drawing.Point(7, 95);
            this.checkBox_ClearRecvWhenFastSave.Name = "checkBox_ClearRecvWhenFastSave";
            this.checkBox_ClearRecvWhenFastSave.Size = new System.Drawing.Size(216, 18);
            this.checkBox_ClearRecvWhenFastSave.TabIndex = 3;
            this.checkBox_ClearRecvWhenFastSave.Text = "Clear received data when fast save";
            this.checkBox_ClearRecvWhenFastSave.UseVisualStyleBackColor = true;
            // 
            // button_FastSavePath
            // 
            this.button_FastSavePath.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FastSavePath.Location = new System.Drawing.Point(8, 39);
            this.button_FastSavePath.Name = "button_FastSavePath";
            this.button_FastSavePath.Size = new System.Drawing.Size(644, 22);
            this.button_FastSavePath.TabIndex = 68;
            this.button_FastSavePath.Text = "Fast Save Path:(select)";
            this.button_FastSavePath.UseVisualStyleBackColor = true;
            this.button_FastSavePath.Click += new System.EventHandler(this.button_FastSavePath_Click);
            // 
            // groupBox_NetCom
            // 
            this.groupBox_NetCom.Controls.Add(this.label_ShowIP);
            this.groupBox_NetCom.Controls.Add(this.button_NetRun);
            this.groupBox_NetCom.Controls.Add(this.button_NetRole);
            this.groupBox_NetCom.Controls.Add(this.label_IP);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP4);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP3);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP2);
            this.groupBox_NetCom.Controls.Add(this.textBox_IP1);
            this.groupBox_NetCom.Location = new System.Drawing.Point(481, 65);
            this.groupBox_NetCom.Name = "groupBox_NetCom";
            this.groupBox_NetCom.Size = new System.Drawing.Size(141, 157);
            this.groupBox_NetCom.TabIndex = 63;
            this.groupBox_NetCom.TabStop = false;
            this.groupBox_NetCom.Text = "NetCom";
            // 
            // label_ShowIP
            // 
            this.label_ShowIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ShowIP.AutoSize = true;
            this.label_ShowIP.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ShowIP.Location = new System.Drawing.Point(4, 100);
            this.label_ShowIP.Name = "label_ShowIP";
            this.label_ShowIP.Size = new System.Drawing.Size(51, 14);
            this.label_ShowIP.TabIndex = 63;
            this.label_ShowIP.Text = "Local IP:";
            // 
            // button_NetRun
            // 
            this.button_NetRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetRun.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_NetRun.Location = new System.Drawing.Point(3, 76);
            this.button_NetRun.Name = "button_NetRun";
            this.button_NetRun.Size = new System.Drawing.Size(134, 23);
            this.button_NetRun.TabIndex = 20;
            this.button_NetRun.Text = "Run";
            this.button_NetRun.UseVisualStyleBackColor = true;
            this.button_NetRun.Click += new System.EventHandler(this.button_NetRun_Click);
            // 
            // button_NetRole
            // 
            this.button_NetRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetRole.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_NetRole.ForeColor = System.Drawing.Color.Red;
            this.button_NetRole.Location = new System.Drawing.Point(3, 51);
            this.button_NetRole.Name = "button_NetRole";
            this.button_NetRole.Size = new System.Drawing.Size(134, 23);
            this.button_NetRole.TabIndex = 19;
            this.button_NetRole.Text = "I am Server";
            this.button_NetRole.UseVisualStyleBackColor = true;
            this.button_NetRole.Click += new System.EventHandler(this.button_NetRole_Click);
            // 
            // label_IP
            // 
            this.label_IP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_IP.AutoSize = true;
            this.label_IP.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_IP.Location = new System.Drawing.Point(3, 14);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(70, 14);
            this.label_IP.TabIndex = 18;
            this.label_IP.Text = "Set local IP:";
            // 
            // textBox_IP4
            // 
            this.textBox_IP4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_IP4.Location = new System.Drawing.Point(104, 28);
            this.textBox_IP4.Name = "textBox_IP4";
            this.textBox_IP4.Size = new System.Drawing.Size(28, 22);
            this.textBox_IP4.TabIndex = 17;
            this.textBox_IP4.Text = "100";
            // 
            // textBox_IP3
            // 
            this.textBox_IP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_IP3.Location = new System.Drawing.Point(71, 28);
            this.textBox_IP3.Name = "textBox_IP3";
            this.textBox_IP3.Size = new System.Drawing.Size(28, 22);
            this.textBox_IP3.TabIndex = 16;
            this.textBox_IP3.Text = "0";
            // 
            // textBox_IP2
            // 
            this.textBox_IP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_IP2.Location = new System.Drawing.Point(37, 28);
            this.textBox_IP2.Name = "textBox_IP2";
            this.textBox_IP2.Size = new System.Drawing.Size(28, 22);
            this.textBox_IP2.TabIndex = 15;
            this.textBox_IP2.Text = "168";
            // 
            // textBox_IP1
            // 
            this.textBox_IP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IP1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_IP1.Location = new System.Drawing.Point(3, 28);
            this.textBox_IP1.Name = "textBox_IP1";
            this.textBox_IP1.Size = new System.Drawing.Size(28, 22);
            this.textBox_IP1.TabIndex = 14;
            this.textBox_IP1.Text = "192";
            // 
            // button_ParmSave
            // 
            this.button_ParmSave.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ParmSave.Location = new System.Drawing.Point(8, 13);
            this.button_ParmSave.Name = "button_ParmSave";
            this.button_ParmSave.Size = new System.Drawing.Size(80, 22);
            this.button_ParmSave.TabIndex = 62;
            this.button_ParmSave.Text = "Parm Save";
            this.button_ParmSave.UseVisualStyleBackColor = true;
            this.button_ParmSave.Click += new System.EventHandler(this.button_ParmSave_Click);
            // 
            // tabPage_BAK
            // 
            this.tabPage_BAK.Controls.Add(this.button_Test1);
            this.tabPage_BAK.Controls.Add(this.textBox_Bakup);
            this.tabPage_BAK.Location = new System.Drawing.Point(4, 24);
            this.tabPage_BAK.Name = "tabPage_BAK";
            this.tabPage_BAK.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_BAK.Size = new System.Drawing.Size(906, 553);
            this.tabPage_BAK.TabIndex = 3;
            this.tabPage_BAK.Text = "BAK";
            this.tabPage_BAK.UseVisualStyleBackColor = true;
            // 
            // button_Test1
            // 
            this.button_Test1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Test1.Location = new System.Drawing.Point(8, 487);
            this.button_Test1.Name = "button_Test1";
            this.button_Test1.Size = new System.Drawing.Size(75, 23);
            this.button_Test1.TabIndex = 73;
            this.button_Test1.Text = "Test";
            this.button_Test1.UseVisualStyleBackColor = true;
            this.button_Test1.Click += new System.EventHandler(this.button_test_Click);
            // 
            // textBox_Bakup
            // 
            this.textBox_Bakup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Bakup.Location = new System.Drawing.Point(6, 6);
            this.textBox_Bakup.MaxLength = 0;
            this.textBox_Bakup.Multiline = true;
            this.textBox_Bakup.Name = "textBox_Bakup";
            this.textBox_Bakup.ReadOnly = true;
            this.textBox_Bakup.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Bakup.Size = new System.Drawing.Size(894, 475);
            this.textBox_Bakup.TabIndex = 72;
            this.textBox_Bakup.WordWrap = false;
            this.textBox_Bakup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_Bakup_MouseDown);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // timer_backgroud
            // 
            this.timer_backgroud.Enabled = true;
            this.timer_backgroud.Tick += new System.EventHandler(this.timer_backgroud_Tick);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 583);
            this.Controls.Add(this.PageTag);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Main";
            this.Text = "KCOM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.tabPage_COM.ResumeLayout(false);
            this.tabPage_COM.PerformLayout();
            this.groupBox_Uart.ResumeLayout(false);
            this.groupBox_Uart.PerformLayout();
            this.groupBox_BitCal.ResumeLayout(false);
            this.groupBox_BitCal.PerformLayout();
            this.PageTag.ResumeLayout(false);
            this.tabPage_Config.ResumeLayout(false);
            this.tabPage_Config.PerformLayout();
            this.groupBox_FastPrint.ResumeLayout(false);
            this.groupBox_FastPrint.PerformLayout();
            this.groupBox_eCMD.ResumeLayout(false);
            this.groupBox_eCMD.PerformLayout();
            this.groupBox_COMSync.ResumeLayout(false);
            this.groupBox_COMSync.PerformLayout();
            this.groupBox_CurrentSetting.ResumeLayout(false);
            this.groupBox_CurrentSetting.PerformLayout();
            this.groupBox_SavedSetting.ResumeLayout(false);
            this.groupBox_SavedSetting.PerformLayout();
            this.groupBox_NetCom.ResumeLayout(false);
            this.groupBox_NetCom.PerformLayout();
            this.tabPage_BAK.ResumeLayout(false);
            this.tabPage_BAK.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Button button_COMOpen;
        private System.Windows.Forms.Timer timer_ColorShow;
        private System.Windows.Forms.TabPage tabPage_COM;
        private System.Windows.Forms.GroupBox groupBox_Uart;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox_COMStopBit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_COMDataBit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_COMCheckBit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_COMBaudrate;
        private System.Windows.Forms.Label label_COM;
        private System.Windows.Forms.ComboBox comboBox_COMNumber;
        private System.Windows.Forms.Button button_TimeStamp;
        private System.Windows.Forms.CheckBox checkBox_EnAutoSnd;
        private System.Windows.Forms.TextBox textBox_AutoSndInterval_100ms;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_CleanSND;
        private System.Windows.Forms.Label label_Send_Bytes;
        private System.Windows.Forms.TextBox textBox_ComSnd;
        private System.Windows.Forms.Button button_SendData;
        private System.Windows.Forms.Button button_CreateLog;
        private System.Windows.Forms.Label label_ClearRec;
        private System.Windows.Forms.CheckBox checkBox_Color;
        private System.Windows.Forms.Button button_FontSize;
        private System.Windows.Forms.Button button_FontBigger;
        private System.Windows.Forms.Button button_FontSmaller;
        private System.Windows.Forms.Button button_SaveFile;
        private System.Windows.Forms.Label label_Rec_Bytes;
        private System.Windows.Forms.TextBox textBox_ComRec;
        private System.Windows.Forms.TabControl PageTag;
		private System.Windows.Forms.CheckBox checkBox_LimitRecLen;
		private System.Windows.Forms.GroupBox groupBox_BitCal;
		private System.Windows.Forms.TextBox textBox_bit;
		private System.Windows.Forms.Button button_Cal;
		private System.Windows.Forms.TextBox textBox_Console;
		private System.Windows.Forms.Label label_RealTime;
        private System.Windows.Forms.CheckBox checkBox_CursorFixed;
		private System.Windows.Forms.CheckBox checkBox_Cmdline;
		private System.Windows.Forms.TextBox textBox_custom_baudrate;
		private System.Windows.Forms.Label label_Baudrate1;
        private System.Windows.Forms.Button button_FastSave;
        private System.Windows.Forms.TabPage tabPage_Config;
        private System.Windows.Forms.CheckBox checkBox_Backgroup;
        private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.CheckBox checkBox_ClearRecvWhenFastSave;
        private System.Windows.Forms.Button button_ParmSave;
        private System.Windows.Forms.GroupBox groupBox_NetCom;
        private System.Windows.Forms.Label label_ShowIP;
        private System.Windows.Forms.Button button_NetRun;
        private System.Windows.Forms.Button button_NetRole;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_IP4;
        private System.Windows.Forms.TextBox textBox_IP3;
        private System.Windows.Forms.TextBox textBox_IP2;
		private System.Windows.Forms.TextBox textBox_IP1;
        private System.Windows.Forms.CheckBox checkBox_FastPrintf;
		private System.Windows.Forms.Button button_Test;
		public System.Windows.Forms.Button button_FPSelect_HEX;
		private System.Windows.Forms.CheckBox checkBox_WordWrap;
        private System.Windows.Forms.Button button_FastSavePath;
        private System.Windows.Forms.Label label_MissData;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.GroupBox groupBox_CurrentSetting;
        private System.Windows.Forms.GroupBox groupBox_SavedSetting;
        private System.Windows.Forms.CheckBox checkBox_Fliter;
        private System.Windows.Forms.Label label_DataRemain;
        private System.Windows.Forms.TabPage tabPage_BAK;
        private System.Windows.Forms.TextBox textBox_Bakup;
        private System.Windows.Forms.Button button_Test1;
        private System.Windows.Forms.CheckBox checkBox_EnableBakup;
        private System.Windows.Forms.CheckBox checkBox_MidMouseClear;
        private System.Windows.Forms.CheckBox checkBox_esc_clear_data;
        private System.Windows.Forms.Timer timer_backgroud;
        private System.Windows.Forms.CheckBox checkBox_ASCII_Snd;
        private System.Windows.Forms.CheckBox checkBox_ASCII_Rcv;
        private System.Windows.Forms.Label label_Speed;
        private System.Windows.Forms.CheckBox checkBox_DbgLog;
        private System.Windows.Forms.Label label_COM_Sync;
        private System.Windows.Forms.ComboBox comboBox_SyncComNum;
        private System.Windows.Forms.Button button_COMSyncOpen;
        private System.Windows.Forms.GroupBox groupBox_COMSync;
        private System.Windows.Forms.CheckBox checkBox_eCMD;
        private System.Windows.Forms.Label label_SyncBaudRate;
        private System.Windows.Forms.ComboBox comboBox_SyncBaud;
        private System.Windows.Forms.Button button_RunEXE;
        private System.Windows.Forms.Button button_SysFont;
        private System.Windows.Forms.Button button_SelectEXE;
        private System.Windows.Forms.GroupBox groupBox_eCMD;
        private System.Windows.Forms.GroupBox groupBox_FastPrint;
        private System.Windows.Forms.TextBox textBox_RunExeCode;
        private System.Windows.Forms.Label label_RunExeCode;
    }
}

