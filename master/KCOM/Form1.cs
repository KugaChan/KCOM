using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口
using System.Runtime.InteropServices;//隐藏光标的
using System.Management;



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
	public partial class Form1 : Form
	{
        //常量
		private const u8 _VersionHSB = 5;	//重大功能更新(例如加入Netcom后，从3.0变4.0)
        private const u8 _VersionMSB = 3;	//主要功能的优化
        private const u8 _VersionLSB = 0;	//微小的改动
		private const u8 _VersionGit = 13;	//Git版本号

        //变量
        private bool form_is_closed = false;

        private bool bCreateLogFile = false;
        private bool bClearRec_ChangeColor = false;
        private bool bFastSave_ChangeColor = false;        

        SaveFileDialog logFile = new SaveFileDialog();                      //定义新的文件保存位置控件        

		protected override void OnResize(EventArgs e)                       //窗口尺寸变化函数
		{
            //最大化后的窗体宽度和高度
            int WindowsWidth;
            int WindowsHeight;            

			if(checkBox_chkWindowsSize.Checked == true)
			{
				WindowsWidth = int.Parse(testBox_WindowsWidth.Text);
				WindowsHeight = int.Parse(textBox_WindowsHeight.Text);				
			}
			else
			{				
				WindowsWidth = SystemInformation.WorkingArea.Width;
				WindowsHeight = SystemInformation.WorkingArea.Height;
			}

			if(WindowState == FormWindowState.Maximized)                    //最大化时所需的操作
			{
                PageTag.Size = new System.Drawing.Size(WindowsWidth, WindowsHeight);    //主分页

				groupBox_COMRec.Size = new System.Drawing.Size(WindowsWidth - 180, WindowsHeight - 200);
                groupBox_COMSnd.Size = new System.Drawing.Size(WindowsWidth - 180, 130);

				textBox_ComRec.Size = new System.Drawing.Size(WindowsWidth - 192, WindowsHeight - 260);
				textBox_ComSnd.Size = new System.Drawing.Size(WindowsWidth - 192, 60);			
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
				groupBox_COMRec.Size = new System.Drawing.Size(872, 384);				
				groupBox_COMSnd.Size = new System.Drawing.Size(872, 150);

                textBox_ComRec.Size = new System.Drawing.Size(860, 320);
				textBox_ComSnd.Size = new System.Drawing.Size(860, 80);

				PageTag.Size = new System.Drawing.Size(1050, 572);
			}
		} 

		public Form1()                                                      //窗体构图函数
		{
			InitializeComponent();
		}

 		private void Form1_Load(object sender, EventArgs e)                 //窗体加载函数
		{
            if(Properties.Settings.Default._add_Time == 0)
            {
                button_AddTime.ForeColor = System.Drawing.Color.Red;
            }
            else if(Properties.Settings.Default._add_Time == 1)
            {
                button_AddTime.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                button_AddTime.ForeColor = System.Drawing.Color.Blue;
            }

			textBox_FastSaveLocation.Text = Properties.Settings.Default.fastsave_location;
            checkBox_Backgroup.Checked = Properties.Settings.Default.run_in_backgroup;
			checkBox_ClearRecvWhenFastSave.Checked = Properties.Settings.Default.clear_data_when_fastsave;            

            textBox_baudrate1.Text = Properties.Settings.Default.user_baudrate;
            checkBox_chkWindowsSize.Checked = Properties.Settings.Default.win_size_chk;
			textBox_WindowsHeight.Text = Properties.Settings.Default._windows_height;
			testBox_WindowsWidth.Text = Properties.Settings.Default._windows_width;
            if(checkBox_chkWindowsSize.Checked == true)
            {
                if((textBox_WindowsHeight.Text != "") && (testBox_WindowsWidth.Text != ""))
                {
                    checkBox_chkWindowsSize.Checked = true;
                    textBox_WindowsHeight.Enabled = false;
                    testBox_WindowsWidth.Enabled = false;
                }
                else
                {
                    checkBox_chkWindowsSize.Checked = false;
                }
            }

            Func_NetCom_Init();			

			Func_TextFont_Change();

            Func_Com_Component_Init();

            label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");        

			Func_Set_Form_Text("", "");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)   //窗体关闭函数
		{
			if(com_is_receiving == true)
			{
                com_allow_receive = false;
                form_is_closed = true;
				e.Cancel = true;//取消窗体的关闭
			}
			else
			{
                try
                {
                    com.Close();
                }
                catch
                {
                    MessageBox.Show("Can't close the COM poart", "Attention!");
                }

                Func_PropertiesSettingsSave();//关闭的时候保存参数
			}

            if(bCreateLogFile == true)
            {
                MessageBox.Show(logFile.FileName, "Log生成完成");
                bCreateLogFile = false;        
            }

            notifyIcon.Dispose();//释放notifyIcon1的所有资源，以保证托盘图标在程序关闭时立即消失

            System.Environment.Exit(0);     //把netcom线程也结束了
			//MessageBox.Show("是否关闭KCOM", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
		}

		string _NetRole = "(NetRole)";
		string _COM_Name = "COM_Name";
		private void Func_Set_Form_Text(string server_name, string com_name)
		{
			Console.WriteLine("server:{0} com:{1}", server_name, com_name);

			this.Text = "KCOM V";
			this.Text += _VersionHSB.ToString() + "." + 
						 _VersionMSB.ToString() + "." +
						 _VersionLSB.ToString() + "    ";
			this.Text += "Git" + _VersionGit.ToString() + "    ";

			if(server_name.Length > 0)
			{
				_NetRole = server_name;
			}

			this.Text += _NetRole + "    ";
			this.Text += this.GetType().Assembly.Location + "    ";			//显示当前EXE的文件路径
			if(com_name.Length > 0)
			{
				_COM_Name = com_name;
			}
			this.Text += "<" + _COM_Name + ">";
		}

		private char Func_GetHexHigh(byte n, byte mode)
		{
			char result = ' ';
			int check;
			if(mode == 0)
			{
				check = n >> 4;//返回高位
			}
			else
			{
				check = n & 0x0F;
			}

			switch (check)
			{
				case 0: result = '0'; break;
				case 1: result = '1'; break;
				case 2: result = '2'; break;
				case 3: result = '3'; break;
				case 4: result = '4'; break;
				case 5: result = '5'; break;
				case 6: result = '6'; break;
				case 7: result = '7'; break;
				case 8: result = '8'; break;
				case 9: result = '9'; break;
				case 10: result = 'A'; break;
				case 11: result = 'B'; break;
				case 12: result = 'C'; break;
				case 13: result = 'D'; break;
				case 14: result = 'E'; break;
				case 15: result = 'F'; break;
			}

			return result;
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

			Properties.Settings.Default._windows_height = textBox_WindowsHeight.Text;
			Properties.Settings.Default._windows_width = testBox_WindowsWidth.Text;
            Properties.Settings.Default.win_size_chk = checkBox_chkWindowsSize.Checked;

            Properties.Settings.Default.user_baudrate = textBox_baudrate1.Text;

            Properties.Settings.Default.fastsave_location = textBox_FastSaveLocation.Text;
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
		        
		private byte Func_CharByte(char n)      //把字符转换为数字
		{
			byte result;
			switch (n)
			{ 
				case '0': result = 0;break;
				case '1': result = 1; break;
				case '2': result = 2; break;
				case '3': result = 3; break;
				case '4': result = 4; break;
				case '5': result = 5; break;
				case '6': result = 6; break;
				case '7': result = 7; break;
				case '8': result = 8; break;
				case '9': result = 9; break;
				case 'A': result = 10; break;
				case 'B': result = 11; break;
				case 'C': result = 12; break;
				case 'D': result = 13; break;
				case 'E': result = 14; break;
				case 'F': result = 15; break;
				case 'a': result = 10; break;
				case 'b': result = 11; break;
				case 'c': result = 12; break;
				case 'd': result = 13; break;
				case 'e': result = 14; break;
				case 'f': result = 15; break;
				default: result = 0xFF; break;
			}

			return result;
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
            if (textBox_FastSaveLocation.Text.Length == 0)
            {
                MessageBox.Show("Invalid File location or name", "ERROR");
                return;
            }
            DialogResult messageResult;
            SaveFileDialog Savefile = new SaveFileDialog(); //定义新的文件保存位置控件
            Savefile.FileName = textBox_FastSaveLocation.Text;

            while (true)
            {
                messageResult = DialogResult.OK;
                try
                {
                    System.IO.StreamWriter sw_fast_save = System.IO.File.CreateText(Savefile.FileName);
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
				label_Rec_Bytes.Text = "0";
				com_recv_cnt = 0;
			}
        }

		private void button_SaveLog_Click(object sender, EventArgs e)
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
                        System.IO.StreamWriter sw_save_file = System.IO.File.CreateText(Savefile.FileName);
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

        System.IO.StreamWriter sw_log_file;

        private void button_CreateLog_Click(object sender, EventArgs e)
        {
            if(bCreateLogFile == false)
            {
                string fileName;
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                int currentDay = DateTime.Now.Day;
                int currentHour = DateTime.Now.Hour;
                int currentMinute = DateTime.Now.Minute;
                int currentSecond = DateTime.Now.Second;
                DialogResult messageResult;

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
                        messageResult = DialogResult.OK;
                        try
                        {
                            //用CreateText无法制定编码格式，如果出现乱码则使用StreamWriter
                            //sw_log_file = new System.IO.StreamWriter(logFile.FileName, true, System.Text.Encoding.Default);
                            sw_log_file = System.IO.File.CreateText(logFile.FileName);                            
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
                    bCreateLogFile = true;
                    button_CreateLog.Text = "Creating......";
                }
            }
            else
            {
                MessageBox.Show(logFile.FileName, "Log生成完成");
                bCreateLogFile = false;
                button_CreateLog.Text = "Creat a log";
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

		private void button_AddTime_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default._add_Time++;
            if(Properties.Settings.Default._add_Time > 2)
            {
                Properties.Settings.Default._add_Time = 0;
            }

            if(Properties.Settings.Default._add_Time == 0)
            {
                button_AddTime.ForeColor = System.Drawing.Color.Red;
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

        private void checkBox_LockRecLength_CheckedChanged(object sender, EventArgs e) 
        {
            if(checkBox_LockRecLen.Checked == true)
            {
                Console.WriteLine("bLockRecLength == true");
            }
            else
            {
                Console.WriteLine("bLockRecLength == false");
            }
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

        //限定窗体的大小
        private void checkBox_chkWindowsSize_CheckedChanged(object sender, EventArgs e)
        {
			//int SH = Screen.PrimaryScreen.Bounds.Height;
			//int SW = Screen.PrimaryScreen.Bounds.Width;
	
            if(checkBox_chkWindowsSize.Checked == true)
            {
				if((textBox_WindowsHeight.Text == "") || (testBox_WindowsWidth.Text == ""))
				{
					checkBox_chkWindowsSize.Checked = false;
					return;
				}

				if((int.Parse(testBox_WindowsWidth.Text) > 1920))
				{
					testBox_WindowsWidth.Text = "1920";
				}

				if((int.Parse(testBox_WindowsWidth.Text) < 1366))
				{
					testBox_WindowsWidth.Text = "1366";
				}

				if((int.Parse(textBox_WindowsHeight.Text) > 1080))
				{
					textBox_WindowsHeight.Text = "1080";
				}

				if((int.Parse(textBox_WindowsHeight.Text) < 768))
				{
					textBox_WindowsHeight.Text = "768";
				}

                textBox_WindowsHeight.BackColor = System.Drawing.Color.Yellow;
                testBox_WindowsWidth.BackColor = System.Drawing.Color.Yellow;

				textBox_WindowsHeight.Enabled = false;
				testBox_WindowsWidth.Enabled = false;
            }
            else
            {
                textBox_WindowsHeight.BackColor = System.Drawing.Color.White;
                testBox_WindowsWidth.BackColor = System.Drawing.Color.White;

				textBox_WindowsHeight.Enabled = true;
				testBox_WindowsWidth.Enabled = true;
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

                //if(tmp_str.Length > 0)
                //{
                //    textBox_ComRec.AppendText(tmp_str);
                //    tmp_str = "";
                //}
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
	}
}
