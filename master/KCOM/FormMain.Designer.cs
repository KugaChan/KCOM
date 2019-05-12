
namespace KCOM
{
	partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.timer_ColorShow = new System.Windows.Forms.Timer(this.components);
            this.tabPage_COM = new System.Windows.Forms.TabPage();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.textBox_ComRec = new System.Windows.Forms.TextBox();
            this.textBox_ComSnd = new System.Windows.Forms.TextBox();
            this.groupBox_Uart = new System.Windows.Forms.GroupBox();
            this.label_Speed = new System.Windows.Forms.Label();
            this.label_DataRemain = new System.Windows.Forms.Label();
            this.label_MissData = new System.Windows.Forms.Label();
            this.button_Test = new System.Windows.Forms.Button();
            this.checkBox_FastPrintf = new System.Windows.Forms.CheckBox();
            this.label_com_running = new System.Windows.Forms.Label();
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
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_COMNumber = new System.Windows.Forms.ComboBox();
            this.checkBox_Cmdline = new System.Windows.Forms.CheckBox();
            this.checkBox_LimitRecLen = new System.Windows.Forms.CheckBox();
            this.checkBox_Color = new System.Windows.Forms.CheckBox();
            this.groupBox_BitCal = new System.Windows.Forms.GroupBox();
            this.textBox_bit = new System.Windows.Forms.TextBox();
            this.button_Cal = new System.Windows.Forms.Button();
            this.textBox_Console = new System.Windows.Forms.TextBox();
            this.button_FontSize = new System.Windows.Forms.Button();
            this.button_FontBigger = new System.Windows.Forms.Button();
            this.button_FontSmaller = new System.Windows.Forms.Button();
            this.PageTag = new System.Windows.Forms.TabControl();
            this.tabPage_Config = new System.Windows.Forms.TabPage();
            this.groupBox_CurrentSetting = new System.Windows.Forms.GroupBox();
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
            this.button_FPSelect_HEX = new System.Windows.Forms.Button();
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
            this.checkBox_DbgLog = new System.Windows.Forms.CheckBox();
            this.tabPage_COM.SuspendLayout();
            this.groupBox_Uart.SuspendLayout();
            this.groupBox_BitCal.SuspendLayout();
            this.PageTag.SuspendLayout();
            this.tabPage_Config.SuspendLayout();
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
            this.tabPage_COM.Controls.Add(this.textBox_Message);
            this.tabPage_COM.Controls.Add(this.textBox_ComRec);
            this.tabPage_COM.Controls.Add(this.textBox_ComSnd);
            this.tabPage_COM.Controls.Add(this.groupBox_Uart);
            this.tabPage_COM.Location = new System.Drawing.Point(4, 22);
            this.tabPage_COM.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_COM.Name = "tabPage_COM";
            this.tabPage_COM.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_COM.Size = new System.Drawing.Size(906, 516);
            this.tabPage_COM.TabIndex = 0;
            this.tabPage_COM.Text = "KCOM";
            // 
            // textBox_Message
            // 
            this.textBox_Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Message.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_Message.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Message.ForeColor = System.Drawing.Color.Red;
            this.textBox_Message.Location = new System.Drawing.Point(2, 490);
            this.textBox_Message.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Message.Size = new System.Drawing.Size(760, 21);
            this.textBox_Message.TabIndex = 11;
            this.textBox_Message.Text = "Information";
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
            this.textBox_ComRec.Size = new System.Drawing.Size(760, 430);
            this.textBox_ComRec.TabIndex = 0;
            this.textBox_ComRec.WordWrap = false;
            this.textBox_ComRec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ComRec_KeyDown);
            this.textBox_ComRec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_ComRec_MouseDown);
            // 
            // textBox_ComSnd
            // 
            this.textBox_ComSnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ComSnd.Location = new System.Drawing.Point(2, 437);
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
            this.groupBox_Uart.Controls.Add(this.label_Speed);
            this.groupBox_Uart.Controls.Add(this.label_DataRemain);
            this.groupBox_Uart.Controls.Add(this.label_MissData);
            this.groupBox_Uart.Controls.Add(this.button_Test);
            this.groupBox_Uart.Controls.Add(this.checkBox_FastPrintf);
            this.groupBox_Uart.Controls.Add(this.label_com_running);
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
            this.groupBox_Uart.Controls.Add(this.label9);
            this.groupBox_Uart.Controls.Add(this.comboBox_COMNumber);
            this.groupBox_Uart.Location = new System.Drawing.Point(766, 4);
            this.groupBox_Uart.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Name = "groupBox_Uart";
            this.groupBox_Uart.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Uart.Size = new System.Drawing.Size(136, 508);
            this.groupBox_Uart.TabIndex = 10;
            this.groupBox_Uart.TabStop = false;
            this.groupBox_Uart.Text = "Uart Config";
            // 
            // label_Speed
            // 
            this.label_Speed.AutoSize = true;
            this.label_Speed.ForeColor = System.Drawing.Color.DarkViolet;
            this.label_Speed.Location = new System.Drawing.Point(8, 335);
            this.label_Speed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Speed.Name = "label_Speed";
            this.label_Speed.Size = new System.Drawing.Size(53, 12);
            this.label_Speed.TabIndex = 63;
            this.label_Speed.Text = "Speed: 0";
            // 
            // label_DataRemain
            // 
            this.label_DataRemain.AutoSize = true;
            this.label_DataRemain.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_DataRemain.Location = new System.Drawing.Point(8, 311);
            this.label_DataRemain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_DataRemain.Name = "label_DataRemain";
            this.label_DataRemain.Size = new System.Drawing.Size(59, 12);
            this.label_DataRemain.TabIndex = 62;
            this.label_DataRemain.Text = "Remain: 0";
            // 
            // label_MissData
            // 
            this.label_MissData.AutoSize = true;
            this.label_MissData.ForeColor = System.Drawing.Color.Red;
            this.label_MissData.Location = new System.Drawing.Point(8, 323);
            this.label_MissData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MissData.Name = "label_MissData";
            this.label_MissData.Size = new System.Drawing.Size(47, 12);
            this.label_MissData.TabIndex = 61;
            this.label_MissData.Text = "Miss: 0";
            // 
            // button_Test
            // 
            this.button_Test.Location = new System.Drawing.Point(97, 435);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(33, 23);
            this.button_Test.TabIndex = 60;
            this.button_Test.Text = "T";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // checkBox_FastPrintf
            // 
            this.checkBox_FastPrintf.AutoSize = true;
            this.checkBox_FastPrintf.Location = new System.Drawing.Point(9, 281);
            this.checkBox_FastPrintf.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_FastPrintf.Name = "checkBox_FastPrintf";
            this.checkBox_FastPrintf.Size = new System.Drawing.Size(90, 16);
            this.checkBox_FastPrintf.TabIndex = 59;
            this.checkBox_FastPrintf.Text = "Fast Printf";
            this.checkBox_FastPrintf.UseVisualStyleBackColor = true;
            this.checkBox_FastPrintf.CheckedChanged += new System.EventHandler(this.checkBox_FastPrintf_CheckedChanged);
            // 
            // label_com_running
            // 
            this.label_com_running.AutoSize = true;
            this.label_com_running.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_com_running.Location = new System.Drawing.Point(7, 486);
            this.label_com_running.Name = "label_com_running";
            this.label_com_running.Size = new System.Drawing.Size(98, 14);
            this.label_com_running.TabIndex = 41;
            this.label_com_running.Text = "_____________";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 464);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "Timing(100ms):";
            // 
            // textBox_AutoSndInterval_100ms
            // 
            this.textBox_AutoSndInterval_100ms.Location = new System.Drawing.Point(97, 460);
            this.textBox_AutoSndInterval_100ms.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_AutoSndInterval_100ms.Name = "textBox_AutoSndInterval_100ms";
            this.textBox_AutoSndInterval_100ms.Size = new System.Drawing.Size(33, 21);
            this.textBox_AutoSndInterval_100ms.TabIndex = 11;
            this.textBox_AutoSndInterval_100ms.TextChanged += new System.EventHandler(this.textBox_AutoSndInterval_100ms_TextChanged);
            // 
            // label_Rec_Bytes
            // 
            this.label_Rec_Bytes.AutoSize = true;
            this.label_Rec_Bytes.ForeColor = System.Drawing.Color.Blue;
            this.label_Rec_Bytes.Location = new System.Drawing.Point(8, 299);
            this.label_Rec_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Rec_Bytes.Name = "label_Rec_Bytes";
            this.label_Rec_Bytes.Size = new System.Drawing.Size(71, 12);
            this.label_Rec_Bytes.TabIndex = 5;
            this.label_Rec_Bytes.Text = "Received: 0";
            // 
            // checkBox_EnAutoSnd
            // 
            this.checkBox_EnAutoSnd.AutoSize = true;
            this.checkBox_EnAutoSnd.Location = new System.Drawing.Point(9, 439);
            this.checkBox_EnAutoSnd.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnAutoSnd.Name = "checkBox_EnAutoSnd";
            this.checkBox_EnAutoSnd.Size = new System.Drawing.Size(78, 16);
            this.checkBox_EnAutoSnd.TabIndex = 12;
            this.checkBox_EnAutoSnd.Text = "Auto Send";
            this.checkBox_EnAutoSnd.UseVisualStyleBackColor = true;
            this.checkBox_EnAutoSnd.CheckedChanged += new System.EventHandler(this.checkBox_EnAutoSnd_CheckedChanged);
            // 
            // button_TimeStamp
            // 
            this.button_TimeStamp.ForeColor = System.Drawing.Color.Gray;
            this.button_TimeStamp.Location = new System.Drawing.Point(7, 225);
            this.button_TimeStamp.Margin = new System.Windows.Forms.Padding(2);
            this.button_TimeStamp.Name = "button_TimeStamp";
            this.button_TimeStamp.Size = new System.Drawing.Size(52, 32);
            this.button_TimeStamp.TabIndex = 40;
            this.button_TimeStamp.Text = "Time stamp";
            this.button_TimeStamp.UseVisualStyleBackColor = true;
            this.button_TimeStamp.Click += new System.EventHandler(this.button_TimeStamp_Click);
            // 
            // checkBox_CursorFixed
            // 
            this.checkBox_CursorFixed.AutoSize = true;
            this.checkBox_CursorFixed.Location = new System.Drawing.Point(9, 262);
            this.checkBox_CursorFixed.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_CursorFixed.Name = "checkBox_CursorFixed";
            this.checkBox_CursorFixed.Size = new System.Drawing.Size(96, 16);
            this.checkBox_CursorFixed.TabIndex = 42;
            this.checkBox_CursorFixed.Text = "Cursor fixed";
            this.checkBox_CursorFixed.UseVisualStyleBackColor = true;
            this.checkBox_CursorFixed.CheckedChanged += new System.EventHandler(this.checkBox_CursorFixed_CheckedChanged);
            // 
            // button_FastSave
            // 
            this.button_FastSave.Location = new System.Drawing.Point(67, 225);
            this.button_FastSave.Margin = new System.Windows.Forms.Padding(2);
            this.button_FastSave.Name = "button_FastSave";
            this.button_FastSave.Size = new System.Drawing.Size(54, 32);
            this.button_FastSave.TabIndex = 43;
            this.button_FastSave.Text = "Fast Save";
            this.button_FastSave.UseVisualStyleBackColor = true;
            this.button_FastSave.Click += new System.EventHandler(this.button_FastSave_Click);
            // 
            // label_Baudrate1
            // 
            this.label_Baudrate1.AutoSize = true;
            this.label_Baudrate1.Location = new System.Drawing.Point(3, 132);
            this.label_Baudrate1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Baudrate1.Name = "label_Baudrate1";
            this.label_Baudrate1.Size = new System.Drawing.Size(65, 12);
            this.label_Baudrate1.TabIndex = 58;
            this.label_Baudrate1.Text = "Cstm Baud:";
            // 
            // button_CleanSND
            // 
            this.button_CleanSND.Location = new System.Drawing.Point(7, 364);
            this.button_CleanSND.Margin = new System.Windows.Forms.Padding(2);
            this.button_CleanSND.Name = "button_CleanSND";
            this.button_CleanSND.Size = new System.Drawing.Size(52, 34);
            this.button_CleanSND.TabIndex = 6;
            this.button_CleanSND.Text = "Send Clear";
            this.button_CleanSND.UseVisualStyleBackColor = true;
            this.button_CleanSND.Click += new System.EventHandler(this.button_CleanSND_Click);
            // 
            // button_CreateLog
            // 
            this.button_CreateLog.Location = new System.Drawing.Point(67, 189);
            this.button_CreateLog.Margin = new System.Windows.Forms.Padding(2);
            this.button_CreateLog.Name = "button_CreateLog";
            this.button_CreateLog.Size = new System.Drawing.Size(54, 32);
            this.button_CreateLog.TabIndex = 15;
            this.button_CreateLog.Text = "Create log";
            this.button_CreateLog.UseVisualStyleBackColor = true;
            this.button_CreateLog.Click += new System.EventHandler(this.button_CreateLog_Click);
            // 
            // button_SendData
            // 
            this.button_SendData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_SendData.ForeColor = System.Drawing.Color.Red;
            this.button_SendData.Location = new System.Drawing.Point(67, 364);
            this.button_SendData.Margin = new System.Windows.Forms.Padding(2);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(54, 34);
            this.button_SendData.TabIndex = 5;
            this.button_SendData.Text = "Data Send";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendData_Click);
            // 
            // label_Send_Bytes
            // 
            this.label_Send_Bytes.AutoSize = true;
            this.label_Send_Bytes.ForeColor = System.Drawing.Color.Green;
            this.label_Send_Bytes.Location = new System.Drawing.Point(8, 347);
            this.label_Send_Bytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Send_Bytes.Name = "label_Send_Bytes";
            this.label_Send_Bytes.Size = new System.Drawing.Size(47, 12);
            this.label_Send_Bytes.TabIndex = 7;
            this.label_Send_Bytes.Text = "Sent: 0";
            // 
            // button_SaveFile
            // 
            this.button_SaveFile.Location = new System.Drawing.Point(7, 189);
            this.button_SaveFile.Margin = new System.Windows.Forms.Padding(2);
            this.button_SaveFile.Name = "button_SaveFile";
            this.button_SaveFile.Size = new System.Drawing.Size(52, 32);
            this.button_SaveFile.TabIndex = 10;
            this.button_SaveFile.Text = "Save file";
            this.button_SaveFile.UseVisualStyleBackColor = true;
            this.button_SaveFile.Click += new System.EventHandler(this.button_SaveFile_Click);
            // 
            // textBox_custom_baudrate
            // 
            this.textBox_custom_baudrate.Location = new System.Drawing.Point(69, 129);
            this.textBox_custom_baudrate.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_custom_baudrate.Name = "textBox_custom_baudrate";
            this.textBox_custom_baudrate.Size = new System.Drawing.Size(61, 21);
            this.textBox_custom_baudrate.TabIndex = 57;
            this.textBox_custom_baudrate.Text = "1222400";
            this.textBox_custom_baudrate.TextChanged += new System.EventHandler(this.textBox_custom_baudrate_TextChanged);
            // 
            // button_COMOpen
            // 
            this.button_COMOpen.ForeColor = System.Drawing.Color.Red;
            this.button_COMOpen.Location = new System.Drawing.Point(67, 151);
            this.button_COMOpen.Margin = new System.Windows.Forms.Padding(2);
            this.button_COMOpen.Name = "button_COMOpen";
            this.button_COMOpen.Size = new System.Drawing.Size(54, 32);
            this.button_COMOpen.TabIndex = 11;
            this.button_COMOpen.Text = "COM is closed";
            this.button_COMOpen.UseVisualStyleBackColor = true;
            this.button_COMOpen.Click += new System.EventHandler(this.button_COMOpen_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 108);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 19;
            this.label13.Text = "Stop:";
            // 
            // comboBox_COMStopBit
            // 
            this.comboBox_COMStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMStopBit.FormattingEnabled = true;
            this.comboBox_COMStopBit.Location = new System.Drawing.Point(39, 105);
            this.comboBox_COMStopBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMStopBit.Name = "comboBox_COMStopBit";
            this.comboBox_COMStopBit.Size = new System.Drawing.Size(91, 20);
            this.comboBox_COMStopBit.TabIndex = 18;
            this.comboBox_COMStopBit.DropDown += new System.EventHandler(this.comboBox_COMStopBit_DropDown);
            this.comboBox_COMStopBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMStopBit_SelectedIndexChanged);
            // 
            // label_ClearRec
            // 
            this.label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
            this.label_ClearRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ClearRec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ClearRec.ForeColor = System.Drawing.Color.Magenta;
            this.label_ClearRec.Location = new System.Drawing.Point(7, 151);
            this.label_ClearRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ClearRec.Name = "label_ClearRec";
            this.label_ClearRec.Size = new System.Drawing.Size(52, 32);
            this.label_ClearRec.TabIndex = 40;
            this.label_ClearRec.Text = "Recv Clear";
            this.label_ClearRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_ClearRec.DoubleClick += new System.EventHandler(this.label_ClearRec_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "Data:";
            // 
            // comboBox_COMDataBit
            // 
            this.comboBox_COMDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMDataBit.FormattingEnabled = true;
            this.comboBox_COMDataBit.Location = new System.Drawing.Point(39, 82);
            this.comboBox_COMDataBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMDataBit.Name = "comboBox_COMDataBit";
            this.comboBox_COMDataBit.Size = new System.Drawing.Size(91, 20);
            this.comboBox_COMDataBit.TabIndex = 16;
            this.comboBox_COMDataBit.DropDown += new System.EventHandler(this.comboBox_COMDataBit_DropDown);
            this.comboBox_COMDataBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMDataBit_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 62);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "Chk:";
            // 
            // comboBox_COMCheckBit
            // 
            this.comboBox_COMCheckBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMCheckBit.FormattingEnabled = true;
            this.comboBox_COMCheckBit.Location = new System.Drawing.Point(39, 59);
            this.comboBox_COMCheckBit.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMCheckBit.Name = "comboBox_COMCheckBit";
            this.comboBox_COMCheckBit.Size = new System.Drawing.Size(91, 20);
            this.comboBox_COMCheckBit.TabIndex = 14;
            this.comboBox_COMCheckBit.DropDown += new System.EventHandler(this.comboBox_COMCheckBit_DropDown);
            this.comboBox_COMCheckBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMCheckBit_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "Baud:";
            // 
            // comboBox_COMBaudrate
            // 
            this.comboBox_COMBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COMBaudrate.FormattingEnabled = true;
            this.comboBox_COMBaudrate.Location = new System.Drawing.Point(39, 36);
            this.comboBox_COMBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMBaudrate.Name = "comboBox_COMBaudrate";
            this.comboBox_COMBaudrate.Size = new System.Drawing.Size(91, 20);
            this.comboBox_COMBaudrate.TabIndex = 12;
            this.comboBox_COMBaudrate.DropDown += new System.EventHandler(this.comboBox_COMBaudrate_DropDown);
            this.comboBox_COMBaudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMBaudrate_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 16);
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
            this.comboBox_COMNumber.Location = new System.Drawing.Point(39, 13);
            this.comboBox_COMNumber.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_COMNumber.Name = "comboBox_COMNumber";
            this.comboBox_COMNumber.Size = new System.Drawing.Size(91, 20);
            this.comboBox_COMNumber.TabIndex = 0;
            this.comboBox_COMNumber.DropDown += new System.EventHandler(this.comboBox_COMNumber_DropDown);
            this.comboBox_COMNumber.SelectedIndexChanged += new System.EventHandler(this.comboBox_COMNumber_SelectedIndexChanged);
            // 
            // checkBox_Cmdline
            // 
            this.checkBox_Cmdline.AutoSize = true;
            this.checkBox_Cmdline.Location = new System.Drawing.Point(7, 57);
            this.checkBox_Cmdline.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Cmdline.Name = "checkBox_Cmdline";
            this.checkBox_Cmdline.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Cmdline.TabIndex = 42;
            this.checkBox_Cmdline.Text = "Cmdline";
            this.checkBox_Cmdline.UseVisualStyleBackColor = true;
            this.checkBox_Cmdline.CheckedChanged += new System.EventHandler(this.checkBox_Cmdline_CheckedChanged);
            // 
            // checkBox_LimitRecLen
            // 
            this.checkBox_LimitRecLen.AutoSize = true;
            this.checkBox_LimitRecLen.Location = new System.Drawing.Point(7, 38);
            this.checkBox_LimitRecLen.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_LimitRecLen.Name = "checkBox_LimitRecLen";
            this.checkBox_LimitRecLen.Size = new System.Drawing.Size(114, 16);
            this.checkBox_LimitRecLen.TabIndex = 41;
            this.checkBox_LimitRecLen.Text = "Max recv length";
            this.checkBox_LimitRecLen.UseVisualStyleBackColor = true;
            this.checkBox_LimitRecLen.CheckedChanged += new System.EventHandler(this.checkBox_LimitRecLen_CheckedChanged);
            // 
            // checkBox_Color
            // 
            this.checkBox_Color.AutoSize = true;
            this.checkBox_Color.Location = new System.Drawing.Point(7, 19);
            this.checkBox_Color.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Color.Name = "checkBox_Color";
            this.checkBox_Color.Size = new System.Drawing.Size(84, 16);
            this.checkBox_Color.TabIndex = 14;
            this.checkBox_Color.Text = "Anti color";
            this.checkBox_Color.UseVisualStyleBackColor = true;
            this.checkBox_Color.CheckedChanged += new System.EventHandler(this.checkBox_Color_CheckedChanged);
            // 
            // groupBox_BitCal
            // 
            this.groupBox_BitCal.Controls.Add(this.textBox_bit);
            this.groupBox_BitCal.Controls.Add(this.button_Cal);
            this.groupBox_BitCal.Controls.Add(this.textBox_Console);
            this.groupBox_BitCal.Location = new System.Drawing.Point(655, 70);
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
            // button_FontSize
            // 
            this.button_FontSize.Location = new System.Drawing.Point(291, 13);
            this.button_FontSize.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSize.Name = "button_FontSize";
            this.button_FontSize.Size = new System.Drawing.Size(82, 22);
            this.button_FontSize.TabIndex = 13;
            this.button_FontSize.Text = "Courier New";
            this.button_FontSize.UseVisualStyleBackColor = true;
            this.button_FontSize.Click += new System.EventHandler(this.button_FontSize_Click);
            // 
            // button_FontBigger
            // 
            this.button_FontBigger.Location = new System.Drawing.Point(119, 13);
            this.button_FontBigger.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontBigger.Name = "button_FontBigger";
            this.button_FontBigger.Size = new System.Drawing.Size(82, 22);
            this.button_FontBigger.TabIndex = 12;
            this.button_FontBigger.Text = "Font+";
            this.button_FontBigger.UseVisualStyleBackColor = true;
            this.button_FontBigger.Click += new System.EventHandler(this.button_FontBigger_Click);
            // 
            // button_FontSmaller
            // 
            this.button_FontSmaller.Location = new System.Drawing.Point(205, 13);
            this.button_FontSmaller.Margin = new System.Windows.Forms.Padding(2);
            this.button_FontSmaller.Name = "button_FontSmaller";
            this.button_FontSmaller.Size = new System.Drawing.Size(82, 23);
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
            this.PageTag.Location = new System.Drawing.Point(0, 0);
            this.PageTag.Margin = new System.Windows.Forms.Padding(0);
            this.PageTag.Name = "PageTag";
            this.PageTag.SelectedIndex = 0;
            this.PageTag.Size = new System.Drawing.Size(914, 542);
            this.PageTag.TabIndex = 0;
            // 
            // tabPage_Config
            // 
            this.tabPage_Config.Controls.Add(this.groupBox_CurrentSetting);
            this.tabPage_Config.Controls.Add(this.groupBox_SavedSetting);
            this.tabPage_Config.Controls.Add(this.button_FastSavePath);
            this.tabPage_Config.Controls.Add(this.button_FPSelect_HEX);
            this.tabPage_Config.Controls.Add(this.groupBox_NetCom);
            this.tabPage_Config.Controls.Add(this.groupBox_BitCal);
            this.tabPage_Config.Controls.Add(this.button_FontSmaller);
            this.tabPage_Config.Controls.Add(this.button_FontBigger);
            this.tabPage_Config.Controls.Add(this.button_ParmSave);
            this.tabPage_Config.Controls.Add(this.button_FontSize);
            this.tabPage_Config.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Config.Name = "tabPage_Config";
            this.tabPage_Config.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Config.Size = new System.Drawing.Size(906, 516);
            this.tabPage_Config.TabIndex = 2;
            this.tabPage_Config.Text = "Config";
            this.tabPage_Config.UseVisualStyleBackColor = true;
            // 
            // groupBox_CurrentSetting
            // 
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_DbgLog);
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_EnableBakup);
            this.groupBox_CurrentSetting.Controls.Add(this.checkBox_WordWrap);
            this.groupBox_CurrentSetting.Location = new System.Drawing.Point(291, 69);
            this.groupBox_CurrentSetting.Name = "groupBox_CurrentSetting";
            this.groupBox_CurrentSetting.Size = new System.Drawing.Size(200, 158);
            this.groupBox_CurrentSetting.TabIndex = 70;
            this.groupBox_CurrentSetting.TabStop = false;
            this.groupBox_CurrentSetting.Text = "Current Setting";
            // 
            // checkBox_EnableBakup
            // 
            this.checkBox_EnableBakup.AutoSize = true;
            this.checkBox_EnableBakup.Location = new System.Drawing.Point(6, 37);
            this.checkBox_EnableBakup.Name = "checkBox_EnableBakup";
            this.checkBox_EnableBakup.Size = new System.Drawing.Size(96, 16);
            this.checkBox_EnableBakup.TabIndex = 62;
            this.checkBox_EnableBakup.Text = "Enable Bakup";
            this.checkBox_EnableBakup.UseVisualStyleBackColor = true;
            this.checkBox_EnableBakup.CheckedChanged += new System.EventHandler(this.checkBox_EnableBakup_CheckedChanged);
            // 
            // checkBox_WordWrap
            // 
            this.checkBox_WordWrap.AutoSize = true;
            this.checkBox_WordWrap.Location = new System.Drawing.Point(6, 20);
            this.checkBox_WordWrap.Name = "checkBox_WordWrap";
            this.checkBox_WordWrap.Size = new System.Drawing.Size(78, 16);
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
            this.groupBox_SavedSetting.Location = new System.Drawing.Point(35, 68);
            this.groupBox_SavedSetting.Name = "groupBox_SavedSetting";
            this.groupBox_SavedSetting.Size = new System.Drawing.Size(242, 323);
            this.groupBox_SavedSetting.TabIndex = 69;
            this.groupBox_SavedSetting.TabStop = false;
            this.groupBox_SavedSetting.Text = "Saved Setting";
            // 
            // checkBox_ASCII_Snd
            // 
            this.checkBox_ASCII_Snd.AutoSize = true;
            this.checkBox_ASCII_Snd.Location = new System.Drawing.Point(7, 190);
            this.checkBox_ASCII_Snd.Name = "checkBox_ASCII_Snd";
            this.checkBox_ASCII_Snd.Size = new System.Drawing.Size(114, 16);
            this.checkBox_ASCII_Snd.TabIndex = 47;
            this.checkBox_ASCII_Snd.Text = "ASCII send data";
            this.checkBox_ASCII_Snd.UseVisualStyleBackColor = true;
            this.checkBox_ASCII_Snd.CheckedChanged += new System.EventHandler(this.checkBox_ASCII_Snd_CheckedChanged);
            // 
            // checkBox_ASCII_Rcv
            // 
            this.checkBox_ASCII_Rcv.AutoSize = true;
            this.checkBox_ASCII_Rcv.Location = new System.Drawing.Point(7, 171);
            this.checkBox_ASCII_Rcv.Name = "checkBox_ASCII_Rcv";
            this.checkBox_ASCII_Rcv.Size = new System.Drawing.Size(132, 16);
            this.checkBox_ASCII_Rcv.TabIndex = 46;
            this.checkBox_ASCII_Rcv.Text = "ASCII receive data";
            this.checkBox_ASCII_Rcv.UseVisualStyleBackColor = true;
            this.checkBox_ASCII_Rcv.CheckedChanged += new System.EventHandler(this.checkBox_ASCII_Rcv_CheckedChanged);
            // 
            // checkBox_esc_clear_data
            // 
            this.checkBox_esc_clear_data.AutoSize = true;
            this.checkBox_esc_clear_data.Location = new System.Drawing.Point(7, 152);
            this.checkBox_esc_clear_data.Name = "checkBox_esc_clear_data";
            this.checkBox_esc_clear_data.Size = new System.Drawing.Size(108, 16);
            this.checkBox_esc_clear_data.TabIndex = 45;
            this.checkBox_esc_clear_data.Text = "ESC clear data";
            this.checkBox_esc_clear_data.UseVisualStyleBackColor = true;
            this.checkBox_esc_clear_data.CheckedChanged += new System.EventHandler(this.checkBox_esc_clear_data_CheckedChanged);
            // 
            // checkBox_MidMouseClear
            // 
            this.checkBox_MidMouseClear.AutoSize = true;
            this.checkBox_MidMouseClear.Location = new System.Drawing.Point(7, 133);
            this.checkBox_MidMouseClear.Name = "checkBox_MidMouseClear";
            this.checkBox_MidMouseClear.Size = new System.Drawing.Size(162, 16);
            this.checkBox_MidMouseClear.TabIndex = 44;
            this.checkBox_MidMouseClear.Text = "middle mouse clear data";
            this.checkBox_MidMouseClear.UseVisualStyleBackColor = true;
            this.checkBox_MidMouseClear.CheckedChanged += new System.EventHandler(this.checkBox_MidMouseClear_CheckedChanged);
            // 
            // checkBox_Fliter
            // 
            this.checkBox_Fliter.AutoSize = true;
            this.checkBox_Fliter.Location = new System.Drawing.Point(7, 114);
            this.checkBox_Fliter.Name = "checkBox_Fliter";
            this.checkBox_Fliter.Size = new System.Drawing.Size(138, 16);
            this.checkBox_Fliter.TabIndex = 43;
            this.checkBox_Fliter.Text = "fliter ileagal char";
            this.checkBox_Fliter.UseVisualStyleBackColor = true;
            this.checkBox_Fliter.CheckedChanged += new System.EventHandler(this.checkBox_Fliter_CheckedChanged);
            // 
            // checkBox_Backgroup
            // 
            this.checkBox_Backgroup.AutoSize = true;
            this.checkBox_Backgroup.Location = new System.Drawing.Point(7, 76);
            this.checkBox_Backgroup.Name = "checkBox_Backgroup";
            this.checkBox_Backgroup.Size = new System.Drawing.Size(120, 16);
            this.checkBox_Backgroup.TabIndex = 2;
            this.checkBox_Backgroup.Text = "Run in Backgroup";
            this.checkBox_Backgroup.UseVisualStyleBackColor = true;
            // 
            // checkBox_ClearRecvWhenFastSave
            // 
            this.checkBox_ClearRecvWhenFastSave.AutoSize = true;
            this.checkBox_ClearRecvWhenFastSave.Location = new System.Drawing.Point(7, 95);
            this.checkBox_ClearRecvWhenFastSave.Name = "checkBox_ClearRecvWhenFastSave";
            this.checkBox_ClearRecvWhenFastSave.Size = new System.Drawing.Size(228, 16);
            this.checkBox_ClearRecvWhenFastSave.TabIndex = 3;
            this.checkBox_ClearRecvWhenFastSave.Text = "Clear received data when fast save";
            this.checkBox_ClearRecvWhenFastSave.UseVisualStyleBackColor = true;
            // 
            // button_FastSavePath
            // 
            this.button_FastSavePath.Location = new System.Drawing.Point(35, 39);
            this.button_FastSavePath.Name = "button_FastSavePath";
            this.button_FastSavePath.Size = new System.Drawing.Size(644, 23);
            this.button_FastSavePath.TabIndex = 68;
            this.button_FastSavePath.Text = "Fast Save Path:(select)";
            this.button_FastSavePath.UseVisualStyleBackColor = true;
            this.button_FastSavePath.Click += new System.EventHandler(this.button_FastSavePath_Click);
            // 
            // button_FPSelect_HEX
            // 
            this.button_FPSelect_HEX.Location = new System.Drawing.Point(35, 397);
            this.button_FPSelect_HEX.Name = "button_FPSelect_HEX";
            this.button_FPSelect_HEX.Size = new System.Drawing.Size(644, 47);
            this.button_FPSelect_HEX.TabIndex = 67;
            this.button_FPSelect_HEX.Text = "FP HEX path:(Select)";
            this.button_FPSelect_HEX.UseVisualStyleBackColor = true;
            this.button_FPSelect_HEX.Click += new System.EventHandler(this.button_FPSelect_HEX_Click);
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
            this.groupBox_NetCom.Location = new System.Drawing.Point(497, 70);
            this.groupBox_NetCom.Name = "groupBox_NetCom";
            this.groupBox_NetCom.Size = new System.Drawing.Size(152, 157);
            this.groupBox_NetCom.TabIndex = 63;
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
            // button_NetRole
            // 
            this.button_NetRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NetRole.ForeColor = System.Drawing.Color.Red;
            this.button_NetRole.Location = new System.Drawing.Point(6, 51);
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
            // button_ParmSave
            // 
            this.button_ParmSave.Location = new System.Drawing.Point(35, 13);
            this.button_ParmSave.Name = "button_ParmSave";
            this.button_ParmSave.Size = new System.Drawing.Size(82, 22);
            this.button_ParmSave.TabIndex = 62;
            this.button_ParmSave.Text = "Parm Save";
            this.button_ParmSave.UseVisualStyleBackColor = true;
            this.button_ParmSave.Click += new System.EventHandler(this.button_ParmSave_Click);
            // 
            // tabPage_BAK
            // 
            this.tabPage_BAK.Controls.Add(this.button_Test1);
            this.tabPage_BAK.Controls.Add(this.textBox_Bakup);
            this.tabPage_BAK.Location = new System.Drawing.Point(4, 22);
            this.tabPage_BAK.Name = "tabPage_BAK";
            this.tabPage_BAK.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_BAK.Size = new System.Drawing.Size(906, 516);
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
            // checkBox_DbgLog
            // 
            this.checkBox_DbgLog.AutoSize = true;
            this.checkBox_DbgLog.Location = new System.Drawing.Point(6, 56);
            this.checkBox_DbgLog.Name = "checkBox_DbgLog";
            this.checkBox_DbgLog.Size = new System.Drawing.Size(120, 16);
            this.checkBox_DbgLog.TabIndex = 63;
            this.checkBox_DbgLog.Text = "Create debug log";
            this.checkBox_DbgLog.UseVisualStyleBackColor = true;
            this.checkBox_DbgLog.CheckedChanged += new System.EventHandler(this.checkBox_DbgLog_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 546);
            this.Controls.Add(this.PageTag);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
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
        private System.Windows.Forms.Label label9;
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
		private System.Windows.Forms.Label label_com_running;
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
		private System.Windows.Forms.Button button_FPSelect_HEX;
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
    }
}

