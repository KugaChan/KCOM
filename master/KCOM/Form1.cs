
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

//using System.IO.StreamWriter;
//using System.IO.File;

//为变量定义别名
using u64 = System.UInt64;
using u32 = System.UInt32;
using u16 = System.UInt16;
using u8 = System.Byte;
using s64 = System.Int64;
using s32 = System.Int32;
using s16 = System.Int16;
using s8 = System.SByte;

namespace KCOM
{
	public partial class FormMain : Form
	{
        //常量
		private const u8 _VersionHSB = 7;	//重大功能更新(例如加入Netcom后，从3.0变4.0)
        private const u8 _VersionMSB = 5;	//主要功能的优化
        private const u8 _VersionLSB = 0;	//微小的改动
		private const u8 _VersionGit = 17;	//Git版本号
		
        private string log_file_name = null;
        private bool program_is_close = false;

        private bool bClearRec_ChangeColor = false;
        private bool bFastSave_ChangeColor = false;

		protected override void OnResize(EventArgs e)                       //窗口尺寸变化函数
		{
			if(WindowState == FormWindowState.Maximized)                    //最大化时所需的操作
			{
                //PageTag.Size = new System.Drawing.Size(SystemInformation.WorkingArea.Width, 
						//SystemInformation.WorkingArea.Height);			//主分页			
			}
			else if(WindowState == FormWindowState.Minimized)               //最小化时所需的操作
			{
                if (checkBox_Backgroup.Checked == true)
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

		public FormMain()                                                      //窗体构图函数
		{
			InitializeComponent();
		}

 		private void FormMain_Load(object sender, EventArgs e)              //窗体加载函数
		{
            Func_Set_AddTime_Color();
            
			button_FastSavePath.Text = "Fast save path: " + Properties.Settings.Default.fastsave_path + "(Select)";
            checkBox_Backgroup.Checked = Properties.Settings.Default.run_in_backgroup;
			checkBox_ClearRecvWhenFastSave.Checked = Properties.Settings.Default.clear_data_when_fastsave;            

            Func_NetCom_Init();			

			Func_TextFont_Change();

            Func_Com_Component_Init();

			Func_Set_Form_Text("", "");

            Func_eProcess_Init();
		}

        private void Func_ProgramClose()
        {
            Func_PropertiesSettingsSave();

            if(process_calx_running == true)
            {
                FP_Resource_Close();
            }

			if(netcom_is_connected == true)
			{
				Func_NetCom_Close();
			}

            notifyIcon.Dispose();//释放notifyIcon1的所有资源，以保证托盘图标在程序关闭时立即消失

			//后台线程，不需要关闭了
			//thread_com_recv.Abort();
			//thread_Calx_output.Abort();
			//thread_net.Abort();

            System.Environment.Exit(0);     //把netcom线程也结束了
            //MessageBox.Show("是否关闭KCOM", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)   //窗体关闭函数
		{
            if(log_file_name != null)
            {
                MessageBox.Show(log_file_name, "Log create done!");
                log_file_name = null;

                program_is_close = true;

                //取消窗体的关闭，必须关闭2次，不知为何关闭窗体时，如果弹窗，就会关闭失败（要先任意点一个按钮）
                e.Cancel = true;
                return;
            }

            Func_ProgramClose();
		}

		string _NetRole = "(NetRole)";
		string _COM_Name = "COM_Name";
		private void Func_Set_Form_Text(string server_name, string com_name)
		{
			//Console.WriteLine("server:{0} com:{1}", server_name, com_name);

			this.Text = "KCOM V";
			this.Text += _VersionHSB.ToString() + "." + 
						 _VersionMSB.ToString() + "." +
						 _VersionLSB.ToString() + "  ";
			this.Text += "Git" + _VersionGit.ToString() + "  ";

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

        private void Func_PropertiesSettingsSave()
        {
            Properties.Settings.Default._com_num_select_index = comboBox_COMNumber.SelectedIndex;

            Properties.Settings.Default._baudrate_select_index = comboBox_COMBaudrate.SelectedIndex;

            Properties.Settings.Default.console_chk = checkBox_Cmdline.Checked;

            Properties.Settings.Default._netcom_ip1 = Convert.ToInt32(textBox_IP1.Text);
            Properties.Settings.Default._netcom_ip2 = Convert.ToInt32(textBox_IP2.Text);
            Properties.Settings.Default._netcom_ip3 = Convert.ToInt32(textBox_IP3.Text);
            Properties.Settings.Default._netcom_ip4 = Convert.ToInt32(textBox_IP4.Text);

            Properties.Settings.Default.user_baudrate = textBox_baudrate1.Text;

            Properties.Settings.Default.run_in_backgroup = checkBox_Backgroup.Checked;
			Properties.Settings.Default.clear_data_when_fastsave = checkBox_ClearRecvWhenFastSave.Checked;

            Properties.Settings.Default.Save();
        }


		private void Func_TextFont_Change()
		{
            Properties.Settings.Default._font_text = Properties.Settings.Default._font_text % 3;

            string font_text;
            switch(Properties.Settings.Default._font_text)
            { 
                case 0: font_text = "Courier New"; break;
                case 1: font_text = "宋体"; break;
                case 2: font_text = "Calibri"; break;
                default: font_text = "Courier New"; break;
            }

            //该表按钮上的文字显示
            button_FontSize.Text = font_text;

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

			if(Properties.Settings.Default._color == 1)
			{
				textBox_ComRec.BackColor = System.Drawing.Color.Black;
				textBox_ComRec.ForeColor = System.Drawing.Color.White;
				textBox_ComSnd.BackColor = System.Drawing.Color.Black;
				textBox_ComSnd.ForeColor = System.Drawing.Color.White;
			}
			else
			{
				textBox_ComRec.BackColor = System.Drawing.Color.White;
				textBox_ComRec.ForeColor = System.Drawing.Color.Black;
				textBox_ComSnd.BackColor = System.Drawing.Color.White;
				textBox_ComSnd.ForeColor = System.Drawing.Color.Black;
			}
		}

        //勾选是否定时发送
        private void checkBox_EnAutoSndTimer_CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox_EnAutoSndTimer.Checked == true)//允许定时发送
			{
                if(textBox_ComSnd.Text.Length == 0 || com.IsOpen == false || textBox_N100ms.Text.Length == 0)
				{
					checkBox_EnAutoSndTimer.Checked = false;
					timer_AutoSnd.Enabled = false;
				}
				else
				{
					timer_AutoSnd.Enabled = true;
										
					button_COMOpen.Enabled = false;
					comboBox_COMBaudrate.Enabled = false;
				}
			}
			else
			{
				timer_AutoSnd.Enabled = false;

				button_COMOpen.Enabled = true;
				comboBox_COMBaudrate.Enabled = true;
			}
		}

		private void button_FontSmaller_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default._font_size--;
			Func_TextFont_Change();
		}

		private void button_FontBigger_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default._font_size++;
			Func_TextFont_Change();
		}

		private void button_FontSize_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default._font_text++;
			Func_TextFont_Change();
		}


        private void button_FastSave_Click(object sender, EventArgs e)
        {
            if(File.Exists(@Properties.Settings.Default.fastsave_path) == false)
            {
                MessageBox.Show("Invalid FastSave path or name", "ERROR");
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
                    messageResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.RetryCancel);
                }

                if (messageResult != DialogResult.Retry)
                {
                    break;
                }
            }

            bFastSave_ChangeColor = true;
            timer_ColorShow.Enabled = true;
            button_FastSave.BackColor = System.Drawing.Color.Yellow;

			if(checkBox_ClearRecvWhenFastSave.Checked == true)
			{
				textBox_ComRec.Text = "";
                label_Rec_Bytes.Text = "Received: 0";
				com_recv_cnt = 0;
			}
        }

		private void button_SaveFile_Click(object sender, EventArgs e)
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
                        messageResult = MessageBox.Show(ex.Message, "文件被占用！", MessageBoxButtons.RetryCancel);
                    }

                    if(messageResult != DialogResult.Retry)
                    {
                        break;
                    }
                }
			}			
		}

		private void checkBox_Color_CheckedChanged(object sender, EventArgs e)
		{
            if(Properties.Settings.Default._color == 0)
			{
                Properties.Settings.Default._color = 1;
			}
			else
			{
                Properties.Settings.Default._color = 0;
			}
			Func_TextFont_Change();
		}

        

        public bool LimitRecLen_last = false;
        private void button_CreateLog_Click(object sender, EventArgs e)
        {
            if(log_file_name == null)
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
                            messageResult = MessageBox.Show(ex.Message, "文件被占用！", MessageBoxButtons.RetryCancel);
                        }

                        if(messageResult != DialogResult.Retry)
                        {
                            break;
                        }
                    }

                    log_file_name = logFile.FileName;
                    button_CreateLog.Text = "Creating......";
                    LimitRecLen_last = checkBox_LimitRecLen.Checked;
                    checkBox_LimitRecLen.Checked = true;
                    MessageBox.Show("Log path: " + logFile.FileName +
                    "\r\nLimit receive length enable!", "Log Creating...");
                }
            }
            else
            {
                MessageBox.Show(log_file_name, "Log create done!");
                log_file_name = null;
                checkBox_LimitRecLen.Checked = LimitRecLen_last;
                button_CreateLog.Text = "Create log";
            }
        }
        
        //用于按键的色彩延时
        private void timer_ColorShow_Tick(object sender, EventArgs e)
        {
            if(timer_ColorShow.Enabled == true)
            {
                timer_ColorShow.Enabled = false;
                if(bClearRec_ChangeColor == true)
                {
                    bClearRec_ChangeColor = false;
                    label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
                }

                if (bFastSave_ChangeColor == true)
                {
                    bFastSave_ChangeColor = false;
                    button_FastSave.BackColor = System.Drawing.Color.Gainsboro;
                }
            }
        }

		private void Func_Set_AddTime_Color()
		{
			if(Properties.Settings.Default._add_Time == 0)
			{
				button_AddTime.ForeColor = System.Drawing.Color.Gray;
			}
			else if(Properties.Settings.Default._add_Time == 1)
			{
				button_AddTime.ForeColor = System.Drawing.Color.Green;
			}
			else
			{
				button_AddTime.ForeColor = System.Drawing.Color.Blue;
			}
		}

		private void button_AddTime_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default._add_Time++;
            if(Properties.Settings.Default._add_Time > 2)
            {
                Properties.Settings.Default._add_Time = 0;
            }

			Func_Set_AddTime_Color();
        }

		private void button_Cal_Click(object sender, EventArgs e)
		{
			int i;
			textBox_Console.Text = "";
			Int64 one = 1;

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
				MessageBox.Show("Input error", "Error!");
			}
		}        

        private void checkBox_CursorMove_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_CursorMove.Checked == false)
            {
                if(textBox_ComSnd.Text.Length > 0)
                {
                    textBox_ComRec.AppendText(textBox_ComSnd.Text);
                    textBox_ComSnd.Text = "";              
                }
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;	//还原窗体显示 
            this.Activate();						//激活窗体并给予它焦点 
            this.ShowInTaskbar = true;				//任务栏区显示图标 
            notifyIcon.Visible = false;				//托盘区图标隐藏 
        }

        private void button_ParmSave_Click(object sender, EventArgs e)
        {
            Func_PropertiesSettingsSave();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)       //调整分页大小
        {
            PageTag.Size = new System.Drawing.Size(this.Size.Width - 20, this.Size.Height - 30);
        }
        
        private void timer_ShowTicks_Tick(object sender, EventArgs e)
        {
            label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");

            if(process_calx_running == true)
            {
                 Func_Check_Hex_Change();
            }

            if(program_is_close == true)
            {
                program_is_close = false;
                Func_ProgramClose();
            }

			TimeSpan span_com_recv = DateTime.Now - date_time_com_recv_final;
			if((span_com_recv.TotalMilliseconds >= 100) && (com_recv_offset > 0))
			{
				Func_COM_EnterQueue(com_recv_buffer, com_recv_offset);
				com_recv_offset = 0;
			}
        }

        private void button_FastSavePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fase_save_txt = new OpenFileDialog();
            fase_save_txt.Filter = "TXT文件|*.txt*";
            fase_save_txt.ValidateNames = true;
            fase_save_txt.CheckPathExists = true;
            fase_save_txt.CheckFileExists = true;
            if(fase_save_txt.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.fastsave_path = fase_save_txt.FileName;
                button_FastSavePath.Text = "Fast save path: " + Properties.Settings.Default.fastsave_path + "(Select)";
            }
        }

		int aa = 0;
		private void button_Snd_Click(object sender, EventArgs e)
		{
			aa++;
		}
	}
}
