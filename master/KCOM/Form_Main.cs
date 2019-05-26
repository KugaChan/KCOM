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
	unsafe public partial class Form_Main : Form
	{
        bool program_is_close = false;

        eTCP etcp = new eTCP();
        FastPrint fp = new FastPrint();
        COM com  = new COM();
        SCOM scom = new SCOM();

        public Form_Main()                                                   //窗体构图函数
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
            Form_COM_Init();
            Form_SCOM_Init();
            /*************串口初始化 End******************/


            /*************FastPrint start****************/
            fp.Init(Properties.Settings.Default.fp_hex0_path,
                    Properties.Settings.Default.fp_hex1_path);
            Func_FastPrint_Init();
            /*************FastPrint End******************/

            string current_com_str = "";
            if(comboBox_COMNumber.SelectedIndex != -1)
            {
                current_com_str = comboBox_COMNumber.SelectedItem.ToString();
            }
            Set_Form_Text("", current_com_str);
        }


        void Func_ProgramClose()
        {
            Func_PropertiesSettingsSave();

            if(com.serialport.IsOpen == true)
            {
                com.Close(com.serialport);
            }

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
            com.thread_txt_update.Abort();  //必须要关闭该线程，否则关闭窗体时会失败

            //System.Environment.Exit(0);   //把netcom线程也结束了
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

        private void checkBox_eCMD_CheckedChanged(object sender, EventArgs e)
        {
            SCOM.run_ecmd = checkBox_eCMD.Checked;
        }

        private void checkBox_DbgLog_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_DbgLog.Checked == true)
            {
                Dbg.WriteLogFile("Echo debug log at " + DateTime.Now.ToString("yy/MM/dd HH:mm:ss"));
            }

            Dbg.echo_to_log_file = checkBox_DbgLog.Checked;
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
				button_TimeStamp.ForeColor = Color.Gray;
			}
			else if(Properties.Settings.Default._add_Time == 1)
			{
				button_TimeStamp.ForeColor = Color.Green;
			}
			else
			{
				button_TimeStamp.ForeColor = Color.Blue;
			}
		}
        
		void button_TimeStamp_Click(object sender, EventArgs e)
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
        
        unsafe private void button_test_Click(object sender, EventArgs e)
        {
            fp.TryDeleteDll();
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            com.ShowDebugInfo();
        }

        uint check_hex_change_cnt = 0;
        uint OneSecondCount = 0;
        //为了提高串口显示刷新时间，定时器的周期调整为100ms
        private void timer_backgroud_Tick(object sender, EventArgs e)
        {
            label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");

            if((fp.is_active == true) && (check_hex_change_cnt % 10 == 0))  //1s检查一次
            {
                fp.Check_Hex_Change();
            }
            check_hex_change_cnt++;
            
            if(OneSecondCount % 100 == 99)
            {
                com.ShowDebugInfo();
            }
            OneSecondCount++;

            if(program_is_close == true)
            {
                program_is_close = false;
                Func_ProgramClose();
            }

            com.Display(label_Rec_Bytes, label_DataRemain, label_MissData,
                label_Send_Bytes, label_Speed, timer_backgroud.Interval);

            if(Dbg.queue_message.Count > 0)
            {
                string message;
                if(Dbg.queue_message.TryDequeue(out message))
                {
                    textBox_Message.AppendText("\r\n" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + message);
                    fp.message_cnt++;
                }
            }

            /***********************网络相关 START***************************/
            int _recv_length = 0;
            byte[] rcv_data = etcp.GetRcvBuffer(ref _recv_length);
            if(_recv_length > 0)
            {
                com.DataHandle(rcv_data, _recv_length, false);
            }
            /***********************网络相关 END*****************************/
        }
    }
}
