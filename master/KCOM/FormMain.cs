using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;	//使用MessageBox
using System.IO.Ports;		//使用串口
using System.Runtime.InteropServices;//隐藏光标的
using System.Management;
using System.Diagnostics;	//使用Rrocess外部EXE
using System.Threading;     //使用线程
using System.IO;			//判断文件是否存在

using System.Reflection;
using System.Collections;

namespace KCOM
{
	unsafe public partial class FormMain : Form
	{
        bool program_is_close = false;

        eTCP etcp = new eTCP();
        FastPrint fp = new FastPrint();
        COM com  = new COM();

		public FormMain()                                                   //窗体构图函数
		{
            InitializeComponent();
		}
        
 		void FormMain_Load(object sender, EventArgs e)                      //窗体加载函数
		{
            /*************恢复参数 Start****************/
            int _parameter1 = Properties.Settings.Default._parameter1;

            checkBox_Color.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_anti_color);
            checkBox_LimitRecLen.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_max_recv_length);
            checkBox_Cmdline.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_cmdline_chk);
            checkBox_Backgroup.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_run_in_backgroup);
            checkBox_ClearRecvWhenFastSave.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_clear_data_when_fastsave);
            checkBox_Fliter.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_messy_code_fliter);
            checkBox_MidMouseClear.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_middle_mouse_clear);
            checkBox_esc_clear_data.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_esc_clear);
            checkBox_ASCII_Rcv.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_ascii_receive);
            checkBox_ASCII_Snd.Checked = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_ascii_send);
            
            etcp.is_server = Parameter.GetBoolFromParameter(_parameter1, Parameter._BitShift_netcom_is_server);
            textBox_custom_baudrate.Text = Properties.Settings.Default.user_baudrate;
            button_FastSavePath.Text = "Fast save path: " + Properties.Settings.Default.fastsave_path;

            Func_Set_AddTime_Color();

            TextFont_Change();
            /*************恢复参数 End******************/


            /*************网络初始化 Start****************/
            label_ShowIP.Text = etcp.ShowLocalIP();
            etcp.Init();
            Func_NetCom_ChangeFont(etcp.is_server);
            /*************网络初始化 End******************/

            
            /*************串口初始化 Start****************/
            com.fm.etcp = etcp;
            com.fm.fp = fp;

            com.ControlModule_Init(comboBox_COMNumber, comboBox_COMBaudrate, 
                comboBox_COMCheckBit, comboBox_COMDataBit, comboBox_COMStopBit);
            com.Init(checkBox_Cmdline.Checked, checkBox_ASCII_Rcv.Checked, checkBox_ASCII_Snd.Checked, 
                checkBox_Fliter.Checked, int.Parse(textBox_custom_baudrate.Text));
            
            com.thread_txt_update = new Thread(ThreadEntry_TxtUpdate);
            com.thread_txt_update.IsBackground = true;
            com.thread_txt_update.Start();

            /*************串口初始化 End******************/


            /*************FastPrint start****************/
            fp.Init(Properties.Settings.Default.fp_hex0_path,
                    Properties.Settings.Default.fp_hex1_path);
            Func_FastPrint_Init();
            /*************FastPrint End******************/

            Set_Form_Text("", "");
		}


        void Func_ProgramClose()
        {
            Func_PropertiesSettingsSave();

            //if(com.IsOpen == true)//关闭串口
            //{
            //    Func_COM_Close();
            //}

            fp.TryDeleteDll();
            if(fp.is_active == true)
            {
                fp.Close();
            }

            if(etcp.is_active == true)
			{
				etcp.Close();
			}

            notifyIcon.Dispose();//释放notifyIcon1的所有资源，以保证托盘图标在程序关闭时立即消失

			//后台线程，不需要关闭了
			//thread_com_recv.Abort();
			//thread_Calx_output.Abort();
			//thread_net.Abort();

            //System.Environment.Exit(0);     //把netcom线程也结束了
            //MessageBox.Show("是否关闭KCOM", Func_GetStack("Attention"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        protected override void OnResize(EventArgs e)                       //窗口尺寸变化函数
        {
            if(WindowState == FormWindowState.Maximized)                    //最大化时所需的操作
            {
                //PageTag.Size = new System.Drawing.Size(SystemInformation.WorkingArea.Width, 
                //SystemInformation.WorkingArea.Height);			//主分页			
            }
            else if(WindowState == FormWindowState.Minimized)               //最小化时所需的操作
            {
                if(checkBox_Backgroup.Checked == true)
                {
                    this.ShowInTaskbar = false; //不显示在系统任务栏
                    notifyIcon.Visible = true;  //托盘图标可见
                }
            }
            else if(WindowState == FormWindowState.Normal)                  //还原正常时的操作
            {
                //PageTag.Size = new System.Drawing.Size(1050, 572);
            }
        }

		void FormMain_FormClosing(object sender, FormClosingEventArgs e)   //窗体关闭函数
		{
            if(com.log_file_name != null)
            {
                MessageBox.Show(com.log_file_name, "Log create done!");
                com.log_file_name = null;

                program_is_close = true;

                //取消窗体的关闭，必须关闭2次，不知为何关闭窗体时，如果弹窗，就会关闭失败（要先任意点一个按钮）
                e.Cancel = true;
                return;
            }

            Func_ProgramClose();
		}

        
        [assembly: AssemblyVersion("1.0.*")]
        public static System.Version Version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public static System.DateTime Date()
        {
            System.Version version = Version();
            System.DateTime startDate = new System.DateTime(2000, 1, 1, 0, 0, 0);
            System.TimeSpan span = new System.TimeSpan(version.Build, 0, 0, version.Revision * 2);
            System.DateTime buildDate = startDate.Add(span);
            return buildDate;
        }

        public void Set_Form_Text(string server_name, string com_name)
		{
            string _NetRole = "(NetRole)";
            string _COM_Name = "COM_Name";

            FileInfo fi = new FileInfo(".//KCOM.exe");
            //Console.WriteLine(fi.CreationTime.ToString());  //文件的创建            
            //Console.WriteLine(fi.LastWriteTime.ToString()); //文件的修改            
            //Console.WriteLine(fi.LastAccessTime.ToString());//文件的访问时间
			//Console.WriteLine("server:{0} com:{1}", server_name, com_name);

			this.Text = "KCOM V";

			this.Text += Parameter._VersionHSB.ToString() + "." + 
						 Parameter._VersionMSB.ToString() + "." +
						 Parameter._VersionLSB.ToString() + "  ";
			this.Text += "Git" + Parameter._VersionGit.ToString() + "  ";

            this.Text += "(" + fi.LastWriteTime.ToString() + ") ";

            if(server_name.Length > 0)
			{
				_NetRole = server_name;
			}

			this.Text += _NetRole + "  ";
			this.Text += this.GetType().Assembly.Location + "  ";			//显示当前EXE的文件路径
			if(com_name.Length > 0)
			{
				_COM_Name = com_name;
			}
			this.Text += "<" + _COM_Name + ">";
		}

        void Func_PropertiesSettingsSave()
        {
            Properties.Settings.Default._com_num_select_index = comboBox_COMNumber.SelectedIndex;

            Properties.Settings.Default._baudrate_select_index = comboBox_COMBaudrate.SelectedIndex;

            Properties.Settings.Default._netcom_ip1 = Convert.ToInt32(textBox_IP1.Text);
            Properties.Settings.Default._netcom_ip2 = Convert.ToInt32(textBox_IP2.Text);
            Properties.Settings.Default._netcom_ip3 = Convert.ToInt32(textBox_IP3.Text);
            Properties.Settings.Default._netcom_ip4 = Convert.ToInt32(textBox_IP4.Text);

            Properties.Settings.Default.user_baudrate = textBox_custom_baudrate.Text;
            
            Properties.Settings.Default.fp_hex0_path = fp.hex0_path;
            Properties.Settings.Default.fp_hex1_path = fp.hex1_path;

            int _parameter1 = 0;

            Parameter.SetBoolToParameter(ref _parameter1, checkBox_Color.Checked, Parameter._BitShift_anti_color);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_LimitRecLen.Checked, Parameter._BitShift_max_recv_length);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_Cmdline.Checked, Parameter._BitShift_cmdline_chk);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_Backgroup.Checked, Parameter._BitShift_run_in_backgroup);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_ClearRecvWhenFastSave.Checked, Parameter._BitShift_clear_data_when_fastsave);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_Fliter.Checked, Parameter._BitShift_messy_code_fliter);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_MidMouseClear.Checked, Parameter._BitShift_middle_mouse_clear);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_esc_clear_data.Checked, Parameter._BitShift_esc_clear);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_ASCII_Rcv.Checked, Parameter._BitShift_ascii_receive);
            Parameter.SetBoolToParameter(ref _parameter1, checkBox_ASCII_Snd.Checked, Parameter._BitShift_ascii_send);
            Parameter.SetBoolToParameter(ref _parameter1, etcp.is_server, Parameter._BitShift_netcom_is_server);

            Properties.Settings.Default._parameter1 = _parameter1;

            Properties.Settings.Default.Save();
        }

		void button_FontSmaller_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default._font_size--;
			TextFont_Change();
		}

		void button_FontBigger_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default._font_size++;
			TextFont_Change();
		}

		void button_FontSize_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default._font_text++;
			TextFont_Change();
		}

        void button_FastSave_Click(object sender, EventArgs e)
        {
            if(File.Exists(@Properties.Settings.Default.fastsave_path) == false)
            {
                MessageBox.Show("Invalid FastSave path or name" + Dbg.GetStack(), "ERROR");
                return;
            }
            DialogResult messageResult;
            SaveFileDialog Savefile = new SaveFileDialog(); //定义新的文件保存位置控件
            Savefile.FileName = Properties.Settings.Default.fastsave_path;

            while (true)
            {
                messageResult = DialogResult.OK;
                try
                {
                    StreamWriter sw_fast_save = File.CreateText(Savefile.FileName);
                    sw_fast_save.Write(textBox_ComRec.Text);//写入文本框中的内容
                    sw_fast_save.Flush();//清空缓冲区
                    sw_fast_save.Close();//关闭关键
                }
                catch (Exception ex)//RetryCancel
                {
                    messageResult = MessageBox.Show(ex.Message + Dbg.GetStack(), "ERROR", MessageBoxButtons.RetryCancel);
                }

                if (messageResult != DialogResult.Retry)
                {
                    break;
                }
            }

            timer_ColorShow.Enabled = true;
            button_FastSave.BackColor = Color.Yellow;

			if(checkBox_ClearRecvWhenFastSave.Checked == true)
			{
				com.ClearRec();
			}
        }

		void button_SaveFile_Click(object sender, EventArgs e)
		{
            DialogResult messageResult;
            string fileName;
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;
            int currentSecond = DateTime.Now.Second;

            fileName = "SaveFile_" + currentYear.ToString()
                + "_" + currentMonth.ToString()
                + "_" + currentDay.ToString()
                + "_" + currentHour.ToString()
                + "_" + currentMinute.ToString()
                + "_" + currentSecond.ToString();

			SaveFileDialog Savefile = new SaveFileDialog(); //定义新的文件保存位置控件
            Savefile.FileName = fileName;
			Savefile.Filter = "KCOM|*.txt";                 //设置文件后缀的过滤
			if(Savefile.ShowDialog() == DialogResult.OK)    //如果有文件保存路径
			{
                while (true)
                {
                    messageResult = DialogResult.OK;
                    try
                    {
                        StreamWriter sw_save_file = File.CreateText(Savefile.FileName);
                        sw_save_file.Write(textBox_ComRec.Text);//写入文本框中的内容
                        sw_save_file.Flush();//清空缓冲区
                        sw_save_file.Close();//关闭关键
                    }
                    catch (Exception ex)//RetryCancel
                    {
                        messageResult = MessageBox.Show(ex.Message, "File is using!", MessageBoxButtons.RetryCancel);
                    }

                    if(messageResult != DialogResult.Retry)
                    {
                        break;
                    }
                }
			}
		}

        public void TextFont_Change()
        {
            Properties.Settings.Default._font_text = Properties.Settings.Default._font_text % 3;

            string font_text;
            switch(Properties.Settings.Default._font_text)
            {
                case 0:
                font_text = "Courier New";
                break;
                case 1:
                font_text = "宋体";
                break;
                case 2:
                font_text = "Calibri";
                break;
                default:
                font_text = "Courier New";
                break;
            }

            //设置字体
            textBox_ComRec.Font = new Font(font_text, Properties.Settings.Default._font_size, textBox_ComRec.Font.Style);
            textBox_ComSnd.Font = new Font(font_text, Properties.Settings.Default._font_size, textBox_ComRec.Font.Style);

            if(Properties.Settings.Default._font_size > 20)
            {
                Properties.Settings.Default._font_size = 20;
            }

            if(Properties.Settings.Default._font_size < 8)
            {
                Properties.Settings.Default._font_size = 8;
            }

            if(checkBox_Color.Checked == true)
            {
                textBox_ComRec.BackColor = Color.Black;
                textBox_ComRec.ForeColor = Color.White;
                textBox_ComSnd.BackColor = Color.Black;
                textBox_ComSnd.ForeColor = Color.White;
            }
            else
            {
                textBox_ComRec.BackColor = Color.White;
                textBox_ComRec.ForeColor = Color.Black;
                textBox_ComSnd.BackColor = Color.White;
                textBox_ComSnd.ForeColor = Color.Black;
            }

            button_FontSize.Text = font_text;   //该表按钮上的文字显示
        }

        void checkBox_Color_CheckedChanged(object sender, EventArgs e)
		{
			TextFont_Change();
		}

        public bool LimitRecLen_last = false;
        void button_CreateLog_Click(object sender, EventArgs e)
        {
            if(com.log_file_name == null)
            {
                string fileName;
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                int currentDay = DateTime.Now.Day;
                int currentHour = DateTime.Now.Hour;
                int currentMinute = DateTime.Now.Minute;
                int currentSecond = DateTime.Now.Second;
                SaveFileDialog logFile = new SaveFileDialog();              //定义新的文件保存位置控件

                fileName = "LogFile_Y" + currentYear.ToString()
                    + "_M" + currentMonth.ToString()
                    + "_D" + currentDay.ToString()
                    + "_H" + currentHour.ToString()
                    + "_M" + currentMinute.ToString()
                    + "_S" + currentSecond.ToString();

                logFile.FileName = fileName;
                logFile.Filter = "txt文件|*.txt|所有文件|*.*";
                if(logFile.ShowDialog() == DialogResult.OK)//如果有文件保存路径
                {
                    while(true)
                    {
                        DialogResult messageResult = DialogResult.OK;
                        try
                        {
                            StreamWriter sw_log_file;

                            //用CreateText无法制定编码格式，如果出现乱码则使用StreamWriter
                            //sw_log_file = new StreamWriter(logFile.FileName, true, System.Text.Encoding.Default);                            

                            sw_log_file = File.CreateText(logFile.FileName);
                            sw_log_file.Write(textBox_ComRec.Text);//写入文本框中的内容
                            sw_log_file.Flush();//清空缓冲区
                            sw_log_file.Close();//关闭关键
                        }
                        catch(Exception ex)
                        {
                            messageResult = MessageBox.Show(ex.Message, "File is using", MessageBoxButtons.RetryCancel);
                        }

                        if(messageResult != DialogResult.Retry)
                        {
                            break;
                        }
                    }

                    com.log_file_name = logFile.FileName;
                    button_CreateLog.Text = "Creating......";
                    LimitRecLen_last = checkBox_LimitRecLen.Checked;
                    checkBox_LimitRecLen.Checked = true;
                    MessageBox.Show("Log path: " + logFile.FileName +
                    "\r\nLimit receive length enable!", "Log Creating...");
                }
            }
            else
            {
                MessageBox.Show(com.log_file_name, "Log create done!");
                com.log_file_name = null;
                checkBox_LimitRecLen.Checked = LimitRecLen_last;
                button_CreateLog.Text = "Create log";
            }
        }
        
        //用于按键的色彩延时
        void timer_ColorShow_Tick(object sender, EventArgs e)
        {
            if(timer_ColorShow.Enabled == true)
            {
                timer_ColorShow.Enabled = false;

                if(button_FastSave.BackColor != Color.Gainsboro)
                {
                    button_FastSave.BackColor = Color.Gainsboro;
                }

                if(label_ClearRec.BackColor != Color.Gainsboro)
                {
                    label_ClearRec.BackColor = Color.Gainsboro;
                }
            }
        }

		void Func_Set_AddTime_Color()
		{
			if(Properties.Settings.Default._add_Time == 0)
			{
				button_AddTime.ForeColor = Color.Gray;
			}
			else if(Properties.Settings.Default._add_Time == 1)
			{
				button_AddTime.ForeColor = Color.Green;
			}
			else
			{
				button_AddTime.ForeColor = Color.Blue;
			}
		}

		void button_AddTime_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default._add_Time++;
            if(Properties.Settings.Default._add_Time > 2)
            {
                Properties.Settings.Default._add_Time = 0;
            }

			Func_Set_AddTime_Color();
        }

		void button_Cal_Click(object sender, EventArgs e)
		{
			int i;
			textBox_Console.Text = "";
			long one = 1;

			try
			{
				//UInt32 ans = Convert.ToUInt32(textBox_bit.Text);  //输入十进制
				UInt32 ans = Convert.ToUInt32(textBox_bit.Text, 16);  //输入十六
				textBox_Console.Text += "DEC:" + ans.ToString() + "\r\n";

				for(i = 0; i < 32; i++)
				{
					if(((one << i) & ans) != 0)
					{
						if(i < 10)
						{
							textBox_Console.Text += "0" + i.ToString() + ":1";
						}
						else
						{
							textBox_Console.Text += "" + i.ToString() + ":1";
						}
					}
					else
					{
						if(i < 10)
						{
							textBox_Console.Text += "0" + i.ToString() + ":0";
						}
						else
						{
							textBox_Console.Text += "" + i.ToString() + ":0";
						}
					}

					if((i % 4) == 3)
					{
                        if(i != 31)
                        {
                            textBox_Console.Text += "\r\n";
                        }						
					}
					else
					{
						textBox_Console.Text += " ";
					}
				}
			}
			catch
			{
                MessageBox.Show("Input error" + Dbg.GetStack(), "Error!");
			}
		}

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;	//还原窗体显示 
            this.Activate();						//激活窗体并给予它焦点 
            this.ShowInTaskbar = true;				//任务栏区显示图标 
            notifyIcon.Visible = false;				//托盘区图标隐藏 
        }

        void button_ParmSave_Click(object sender, EventArgs e)
        {
            Func_PropertiesSettingsSave();
        }

        void FormMain_SizeChanged(object sender, EventArgs e)       //调整分页大小
        {
            PageTag.Size = new System.Drawing.Size(this.Size.Width - 20, this.Size.Height - 30);
        }
        
        uint check_hex_change_cnt = 0;
        //为了提高串口显示刷新时间，定时器的周期调整为100ms
        void timer_ShowTicks_Tick(object sender, EventArgs e)
        {
            label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");            

            if((fp.is_active == true) && (check_hex_change_cnt % 10 == 0))  //1s检查一次
            {
                 fp.Check_Hex_Change();
            }
            check_hex_change_cnt++;

            if(program_is_close == true)
            {
                program_is_close = false;
                Func_ProgramClose();
            }
            
            com.Display();
        }

        void button_FastSavePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fase_save_txt = new OpenFileDialog();
            fase_save_txt.Filter = "TXT文件|*.txt*";
            fase_save_txt.ValidateNames = true;
            fase_save_txt.CheckPathExists = true;
            fase_save_txt.CheckFileExists = true;
            if(fase_save_txt.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.fastsave_path = fase_save_txt.FileName;
                button_FastSavePath.Text = "Fast save path: " + Properties.Settings.Default.fastsave_path;
            }
        }
        
        unsafe private void button_test_Click(object sender, EventArgs e)
        {
            fp.TryDeleteDll();
        }

        /***************************FastPrinf START**************************/
        public void Func_FastPrint_Init()
        {
            Console.WriteLine("HEX0:{0}", fp.hex0_path);
            Console.WriteLine("HEX1:{0}", fp.hex1_path);

            button_FPSelect_HEX.Text = "";
            button_FPSelect_HEX.Text += "FP HEX0 path: " + fp.hex0_path;
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + fp.hex1_path;
        }

        void checkBox_FastPrintf_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_FastPrintf.Checked == true)	//勾上是true
            {
                if(checkBox_ASCII_Rcv.Checked == false)
                {
                    MessageBox.Show("Showing hex format!!!", "Error");
                }
                else
                {
                    checkBox_FastPrintf.Checked = fp.Start();
                }
            }
            else
            {
                fp.Close();
            }
        }

        void button_FPSelect_HEX_Click(object sender, EventArgs e)
        {
            string fp_hex0_path_temp = fp.hex0_path;
            string fp_hex1_path_temp = fp.hex1_path;

            button_FPSelect_HEX.Text = "";

            OpenFileDialog ofd0 = new OpenFileDialog();
            ofd0.Filter = "HEX文件|*.hex*";
            ofd0.ValidateNames = true;
            ofd0.CheckPathExists = true;
            ofd0.CheckFileExists = true;
            if(ofd0.ShowDialog() != DialogResult.OK)
            {
                ofd0.FileName = fp_hex0_path_temp;
            }
            fp.hex0_path = ofd0.FileName;
            button_FPSelect_HEX.Text += "FP HEX0 path: " + ofd0.FileName;

            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "HEX文件|*.hex*";
            ofd1.ValidateNames = true;
            ofd1.CheckPathExists = true;
            ofd1.CheckFileExists = true;
            if(ofd1.ShowDialog() != DialogResult.OK)
            {
                ofd1.FileName = fp_hex1_path_temp;
            }
            fp.hex1_path = ofd1.FileName;
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + ofd1.FileName;
        }
        /***************************FastPrinf END**************************/


        /***************************命令行 START**************************/

        //所有默认热键的keydown入口在这里,返回false则原先的热键处理继续走，返回true则原先的热键处理不走了
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(com.cfg.cmdline_mode == true)
            {
                com.cmdline.HandleKeyData(com.serialport, keyData);

                return true;
            }
            else
            {
                return false;
            }
        }
        /***************************命令行 END******************************/


        /***************************网络串口 START**************************/  
        void button_NetRun_Click(object sender, EventArgs e)
        {
            if(button_NetRun.Text != "Break the eTCP")
            {
                if(etcp.ConfigNet(
                    6666,
                    textBox_IP1.Text,
                    textBox_IP2.Text,
                    textBox_IP3.Text,
                    textBox_IP4.Text) == true)
                {
                    button_NetRole.Enabled = false;
                    button_NetRun.Text = "Break the eTCP";
                }
            }
            else
            {
                button_NetRole.Enabled = true;
                etcp.Close();
                if(etcp.is_server == true)
                {
                    button_NetRun.Text = "Connect to server";
                }
                else
                {
                    button_NetRun.Text = "Wait for client";
                }
            }
        }

        void button_NetRole_Click(object sender, EventArgs e)
        {
            if(etcp.is_server == true)
            {
                if(com.serialport.IsOpen == true)
                {
                    MessageBox.Show("COM is open, client can't enable uart!", "Error");
                    return;
                }
                etcp.is_server = false;
            }
            else
            {
                etcp.is_server = true;
            }
            Func_NetCom_ChangeFont(etcp.is_server);
        }

        public void Func_NetCom_ChangeFont(bool is_server)
        {
            if(is_server == false)
            {
                Set_Form_Text("(Client)", "");
                button_NetRole.ForeColor = Color.Blue;
                button_NetRole.Text = "I am Client";
                button_NetRun.Text = "Connect to Server";
                label_IP.Text = "Server IP:";
                button_COMOpen.Enabled = false;
            }
            else
            {
                Set_Form_Text("(Server)", "");
                button_NetRole.ForeColor = Color.Red;
                button_NetRole.Text = "I am Server";
                button_NetRun.Text = "Wait for Clients";
                label_IP.Text = "Local IP:";
                button_COMOpen.Enabled = true;
            }
        }
        /***************************网络串口 END****************************/

        /******************************串口 START***************************/
        void ThreadEntry_TxtUpdate()
        {
            while(true)
            {
                if(com.txt.backup.Length != textBox_Bakup.Text.Length)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        textBox_Bakup.Text = com.txt.backup;
                    }));
                }
                else if(com.efifo.GetValidNum() > 0)
                {
                    int op = COM.TxtOP.NULL;
                    string str = com.efifo.Output(ref op);
                    this.Invoke((EventHandler)(delegate
                    {
                        if(op == COM.TxtOP.ADD)
                        {
                            textBox_ComRec.AppendText(str);
                        }
                        else if(op == COM.TxtOP.EQUAL)
                        {
                            textBox_ComRec.Text = str;
                        }
                        else if(op == COM.TxtOP.CLEAR)
                        {
                            textBox_ComRec.Text = "";
                        }
                    }));
                }
                else
                {
                    com.event_txt_update.WaitOne(1000);
                }
            }
        }

        private void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_Cmdline_CheckedChanged(sender, e);
            if(com.cfg.cmdline_mode == true)
            {
                textBox_ComSnd.Enabled = false;
            }
            else
            {
                textBox_ComSnd.Enabled = true;
            }
        }

        private void textBox_N100ms_TextChanged(object sender, EventArgs e)
        {
            com.textBox_N100ms_TextChanged(sender, e);
        }

        private void checkBox_CursorFixed_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_CursorFixed_CheckedChanged(sender, e);
        }

        private void checkBox_EnAutoSnd_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_EnAutoSnd_CheckedChanged(sender, e, button_COMOpen, comboBox_COMBaudrate);
        }

        public void SetComStatus(bool IsRunning)
        {
            if(IsRunning == true)
            {
                button_COMOpen.Text = "COM is opened";
                button_COMOpen.ForeColor = Color.Green;
                comboBox_COMCheckBit.Enabled = false;
                comboBox_COMDataBit.Enabled = false;
                comboBox_COMNumber.Enabled = false;
                comboBox_COMStopBit.Enabled = false;
            }
            else
            {
                button_COMOpen.Text = "COM is closed";
                button_COMOpen.ForeColor = Color.Red;
                comboBox_COMCheckBit.Enabled = true;
                comboBox_COMDataBit.Enabled = true;
                comboBox_COMNumber.Enabled = true;
                comboBox_COMStopBit.Enabled = true;
            }
        }

        private void button_CleanSND_Click(object sender, EventArgs e)
        {
            com.button_CleanSND_Click(sender, e, textBox_ComSnd);
        }

        private void checkBox_WordWrap_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ComRec.WordWrap = checkBox_WordWrap.Checked;
        }

        private void textBox_ComRec_MouseDown(object sender, MouseEventArgs e)
        {
            com.textBox_ComRec_MouseDown(sender, e);
        }

        private void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
            com.textBox_ComRec_KeyDown(sender, e);
        }

        private void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            com.textBox_ComSnd_KeyDown(sender, e);
        }

        private void textBox_ComSnd_MouseDown(object sender, MouseEventArgs e)
        {
            com.textBox_ComSnd_MouseDown(sender, e);
        }

        private void textBox_Bakup_MouseDown(object sender, MouseEventArgs e)
        {
            com.textBox_Bakup_MouseDown(sender, e);
        }

        private void button_SendData_Click(object sender, EventArgs e)
        {
            com.button_SendData_Click(sender, e);
        }

        private void button_COMOpen_Click(object sender, EventArgs e)
        {
            bool res;
            res = com.button_COMOpen_Click(sender, e, comboBox_COMNumber);
            SetComStatus(res);
        }

        private void label_ClearRec_DoubleClick(object sender, EventArgs e)
        {
            com.label_ClearRec_DoubleClick(sender, e, timer_ColorShow);
        }

        private void button_Snd_Click(object sender, EventArgs e)
        {
            com.button_Snd_Click(sender, e);
        }

        private void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMNumber_DropDown(sender, e);
        }

        private void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMNumber_SelectedIndexChanged(sender, e, Set_Form_Text);
        }

        private void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_DropDown(sender, e);
        }

        private void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_SelectedIndexChanged(sender, e);
        }

        private void checkBox_ASCII_Rcv_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_ASCII_Rcv_CheckedChanged(sender, e, textBox_ComRec);
        }

        private void checkBox_ASCII_Snd_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_ASCII_Snd_CheckedChanged(sender, e, textBox_ComSnd);
        }

        private void checkBox_Fliter_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_Fliter_CheckedChanged(sender, e);
        }

        private void checkBox_LimitRecLen_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_LimitRecLen_CheckedChanged(sender, e);
        }

        private void checkBox_EnableBakup_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_EnableBakup_CheckedChanged(sender, e);
        }

        private void checkBox_MidMouseClear_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_MidMouseClear_CheckedChanged(sender, e);
        }

        private void checkBox_esc_clear_data_CheckedChanged(object sender, EventArgs e)
        {
            com.checkBox_esc_clear_data_CheckedChanged(sender, e);
        }

        private void textBox_ComSnd_TextChanged(object sender, EventArgs e)
        {
            com.textBox_ComSnd_TextChanged(sender, e);
        }

        private void textBox_custom_baudrate_TextChanged(object sender, EventArgs e)
        {
            com.textBox_custom_baudrate_TextChanged(sender, e);
        }

        /******************************串口 END*****************************/

        private void timer_message_backgroud_Tick(object sender, EventArgs e)
        {
            label_Rec_Bytes.Text = "Received: " + com.record.rcv_bytes.ToString();
            label_BufferLeft.Text = "Buffer: " + com.record.buffer_left.ToString();
            label_MissData.Text = "Miss: " + com.record.miss_data.ToString();            
            label_Send_Bytes.Text = "Sent: " + com.record.snd_bytes.ToString();
            
            if(Dbg.queue_message.Count > 0)
            {
                string message;
                if(Dbg.queue_message.TryDequeue(out message))
                {
                    textBox_Message.AppendText("\r\n" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + message);
                    fp.message_cnt++;
                }
            }

            /***********************串口相关 START***************************/
            int _recv_length = 0;
            byte[] rcv_data = etcp.GetRcvBuffer(ref _recv_length);
            if(_recv_length > 0)
            {
                //this.Invoke((EventHandler)(delegate
                //{
                //    textBox_ComRec.AppendText(Encoding.ASCII.GetString(rcv_data));
                //}));
                com.DataHandle(rcv_data, _recv_length, false);
            }
            /***********************串口相关 END*****************************/
        }

        private void comboBox_COMCheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMCheckBit_SelectedIndexChanged(sender, e);
        }

        private void comboBox_COMDataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMDataBit_SelectedIndexChanged(sender, e);
        }

        private void comboBox_COMStopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMStopBit_SelectedIndexChanged(sender, e);
        }

        private void comboBox_COMCheckBit_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMCheckBit_DropDown(sender, e);
        }

        private void comboBox_COMDataBit_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMDataBit_DropDown(sender, e);
        }

        private void comboBox_COMStopBit_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMStopBit_DropDown(sender, e);
        }
    }
}
