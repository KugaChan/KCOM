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
		private const u8 _VersionHSB = 4;	//重大功能更新(例如加入Netcom后，从3.0变4.0)
        private const u8 _VersionMSB = 1;	//主要功能的优化
        private const u8 _VersionLSB = 1;	//微小的改动
		private const u8 _VersionGit = 4;	//Git版本号

        //变量
        private bool form_is_closed = false;
        private bool com_change_baudrate = false;
        private u32 dwTimerCount = 0;

        private bool bCreateLogFile = false;
        private bool bChangeColor = false;        
        private int LastLogFileTime = 0;

        SaveFileDialog logFile = new SaveFileDialog();                      //定义新的文件保存位置控件        

		int[] badurate_array = 
		{
			4800,
			9600,
			19200,
			38400,
			115200,
            128000,
            230400,
            256000,
            460800,
            921600,
            1222400,
			1382400
		};

		protected override void OnResize(EventArgs e)                       //窗口尺寸变化函数
		{
            //最大化后的窗体宽度和高度
            int WindowsWidth;
            int WindowsHeight;            

			if(checkBox_chkWindowsSize.Checked == true)
			{
				WindowsWidth = int.Parse(windows_Width.Text);
				WindowsHeight = int.Parse(windows_Height.Text);				
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

                textBox_NetRecv.Size = new System.Drawing.Size(WindowsWidth - 192, WindowsHeight - 200);
                textBox_NetSend.Size = new System.Drawing.Size(WindowsWidth - 192, 80);				
			}
			else if(WindowState == FormWindowState.Minimized)               //最小化时所需的操作
			{
				
			}
			else if(WindowState == FormWindowState.Normal)                  //还原正常时的操作
			{
				groupBox_COMRec.Size = new System.Drawing.Size(872, 384);				
				groupBox_COMSnd.Size = new System.Drawing.Size(872, 150);

                textBox_ComRec.Size = new System.Drawing.Size(860, 320);
				textBox_ComSnd.Size = new System.Drawing.Size(860, 80);
                
                textBox_NetRecv.Size = new System.Drawing.Size(884, 438);
                textBox_NetSend.Size = new System.Drawing.Size(884, 90);

				PageTag.Size = new System.Drawing.Size(1050, 572);
			}
		} 

		public Form1()                                                      //窗体构图函数
		{
			InitializeComponent();
		}

 		private void Form1_Load(object sender, EventArgs e)                 //窗体加载函数
		{
			s32 i;			

			windows_Height.Text = Properties.Settings.Default._windows_height;
			windows_Width.Text = Properties.Settings.Default._windows_width;

            Func_NetCom_Init();			

			button_FontSize.Text = Properties.Settings.Default._font_text;

			Func_TextFont_Change();

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
            
            /********************更新串口下来列表的选项-start******************/
            string[] strArr = Func_GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            int SerialNum = 0;

            foreach(string vPortName in SerialPort.GetPortNames())
            {
                String SerialIn = "";
                SerialIn += vPortName;
                SerialIn += ':';
                foreach(string s in strArr)
                {                    
                    if(s.Contains(vPortName))
                    {
                        SerialIn += s;
                    }
                }
                Console.WriteLine(SerialIn);
                comboBox_COMNumber.Items.Add(SerialIn);
                SerialNum++;
            }
            /********************更新串口下来列表的选项-end********************/

            //波特率
			for(i = 0; i < badurate_array.Length; i++)
			{
				comboBox_COMBaudrate.Items.Add(badurate_array[i].ToString());
			}

            //校验位
			comboBox_COMCheckBit.Items.Add("None");	
			comboBox_COMCheckBit.Items.Add("Odd");
			comboBox_COMCheckBit.Items.Add("Even");

            //数据位
			comboBox_COMDataBit.Items.Add("8");
			comboBox_COMStopBit.Items.Add("0");

            //停止位
			comboBox_COMStopBit.Items.Add("1");
			comboBox_COMStopBit.Items.Add("2");
			comboBox_COMStopBit.Items.Add("1.5");

            if((SerialNum > 0) && (Properties.Settings.Default._com_num_select_index < SerialNum))    //串口列表选用号
            {
                comboBox_COMNumber.SelectedIndex = Properties.Settings.Default._com_num_select_index;
            }
			else
			{
				comboBox_COMNumber.SelectedIndex = -1;
			}

			comboBox_COMBaudrate.SelectedIndex = Properties.Settings.Default._baudrate_select_index;		
			comboBox_COMCheckBit.SelectedIndex = 0;
			comboBox_COMDataBit.SelectedIndex = 0;
			comboBox_COMStopBit.SelectedIndex = 1;
			com.DataReceived += Func_COM_DataRec;//指定串口接收函数

            label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");
            timer_renew_com.Enabled = true;

			Func_Set_Form_Text("", "");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)   //窗体关闭函数
		{ //关闭的时候保存参数

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
                    MessageBox.Show("无法关闭串口", "提示");
                }

                Func_PropertiesSettingsSave();
			}

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
            Properties.Settings.Default._netcom_ip1 = Convert.ToInt32(textBox_IP1.Text);
            Properties.Settings.Default._netcom_ip2 = Convert.ToInt32(textBox_IP2.Text);
            Properties.Settings.Default._netcom_ip3 = Convert.ToInt32(textBox_IP3.Text);
            Properties.Settings.Default._netcom_ip4 = Convert.ToInt32(textBox_IP4.Text);

			Properties.Settings.Default._windows_height = windows_Height.Text;
			Properties.Settings.Default._windows_width = windows_Width.Text;

            Properties.Settings.Default.Save();       
        }


		private void Func_TextFont_Change()
		{
			textBox_ComRec.Font = new Font(Properties.Settings.Default._font_text, Properties.Settings.Default._font_size, textBox_ComRec.Font.Style);
			textBox_NetRecv.Font = new Font(Properties.Settings.Default._font_text, Properties.Settings.Default._font_size, textBox_ComRec.Font.Style);

			if(Properties.Settings.Default._color == 1)
			{
				textBox_ComRec.BackColor = System.Drawing.Color.Black;
				textBox_ComRec.ForeColor = System.Drawing.Color.White;
				textBox_ComSnd.BackColor = System.Drawing.Color.Black;
				textBox_ComSnd.ForeColor = System.Drawing.Color.White;				

				textBox_NetRecv.BackColor = System.Drawing.Color.Black;
				textBox_NetRecv.ForeColor = System.Drawing.Color.White;
				textBox_NetSend.BackColor = System.Drawing.Color.Black;
				textBox_NetSend.ForeColor = System.Drawing.Color.White;
			}
			else
			{
				textBox_ComRec.BackColor = System.Drawing.Color.White;
				textBox_ComRec.ForeColor = System.Drawing.Color.Black;
				textBox_ComSnd.BackColor = System.Drawing.Color.White;
				textBox_ComSnd.ForeColor = System.Drawing.Color.Black;

				textBox_NetRecv.BackColor = System.Drawing.Color.White;
				textBox_NetRecv.ForeColor = System.Drawing.Color.Black;
				textBox_NetSend.BackColor = System.Drawing.Color.White;
				textBox_NetSend.ForeColor = System.Drawing.Color.Black;
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
				if(textBox_ComSnd.Text.Length == 0 || com_is_open == false || textBox_N100ms.Text.Length == 0)
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
            if(Properties.Settings.Default._font_size < 8)
			{
                Properties.Settings.Default._font_size = 8;
			}

			Func_TextFont_Change();
		}

		private void button_FontBigger_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default._font_size++;
            if(Properties.Settings.Default._font_size > 20)
			{
                Properties.Settings.Default._font_size = 20;
			}

			Func_TextFont_Change();
		}

		private void button_FontSize_Click(object sender, EventArgs e)
		{
			if(Properties.Settings.Default._font_text == "Courier New")
			{
				Properties.Settings.Default._font_text = "宋体";
			}
			else
			{
				Properties.Settings.Default._font_text = "Courier New";
							
			}
			button_FontSize.Text = Properties.Settings.Default._font_text;
			Func_TextFont_Change();
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

			SaveFileDialog Savefile = new SaveFileDialog();//定义新的文件保存位置控件
            Savefile.FileName = fileName;
			Savefile.Filter = "KCOM|*.txt";//设置文件后缀的过滤
			if(Savefile.ShowDialog() == DialogResult.OK)//如果有文件保存路径
			{
                while (true)
                {
                    messageResult = DialogResult.OK;
                    try
                    {
                        System.IO.StreamWriter sw = System.IO.File.CreateText(Savefile.FileName);
                        sw.Write(textBox_ComRec.Text);//写入文本框中的内容
                        sw.Flush();//清空缓冲区
                        sw.Close();//关闭关键                
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

        private void button_CreateLog_Click(object sender, EventArgs e)
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
                while (true)
                {
                    messageResult = DialogResult.OK;
                    try
                    {
                        System.IO.StreamWriter sw = System.IO.File.CreateText(logFile.FileName);
                        sw.Write(textBox_ComRec.Text);//写入文本框中的内容
                        sw.Flush();//清空缓冲区
                        sw.Close();//关闭关键                
                    }
                    catch (Exception ex)
                    {
                        messageResult = MessageBox.Show(ex.Message, "文件被占用！", MessageBoxButtons.RetryCancel);
                    }

                    if(messageResult != DialogResult.Retry)
                    {
                        break;
                    }
                }
                bCreateLogFile = true;
            }
        }
        
        //用于按键的色彩延时
        private void timer_ColorShow_Tick(object sender, EventArgs e)
        {
            if(timer_ColorShow.Enabled == true)
            {
                timer_ColorShow.Enabled = false;
                if(bChangeColor == true)
                {
                    bChangeColor = false;
                    label_ClearRec.BackColor = System.Drawing.Color.Gainsboro;
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
				textBox_Console.Text += "DEC:" + ans.ToString() + "\r\n\r\n";

				for(i = 0; i < 32; i++)
				{
					if(((one << i) & ans) != 0)
					{
						if(i < 10)
						{
							textBox_Console.Text += "b0" + i.ToString() + ": 1";
						}
						else
						{
							textBox_Console.Text += "b" + i.ToString() + ": 1";
						}
					}
					else
					{
						if(i < 10)
						{
							textBox_Console.Text += "b0" + i.ToString() + ": 0";
						}
						else
						{
							textBox_Console.Text += "b" + i.ToString() + ": 0";
						}
					}

					if((i % 2) == 1)
					{
						textBox_Console.Text += "\r\n";
					}
					else
					{
						textBox_Console.Text += "   ";
					}
				}
			}
			catch
			{
				MessageBox.Show("输入错误", "Error!");
			}
		}

        //限定窗体的大小
        private void checkBox_chkWindowsSize_CheckedChanged(object sender, EventArgs e)
        {
            if((int.Parse(windows_Width.Text) > 1920))
            {
                windows_Width.Text = "1920";
            }

            if((int.Parse(windows_Width.Text) < 1366))
            {
                windows_Width.Text = "1366";
            }

            if((int.Parse(windows_Height.Text) > 1080))
            {
                windows_Height.Text = "1080";
            }

            if((int.Parse(windows_Height.Text) < 768))
            {
                windows_Height.Text = "768";
            }

            if(checkBox_chkWindowsSize.Checked == true)
            {
                windows_Height.BackColor = System.Drawing.Color.Yellow;
                windows_Width.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                windows_Height.BackColor = System.Drawing.Color.White;
                windows_Width.BackColor = System.Drawing.Color.White;
            }
        }

		private void textBox_NetRecv_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control && e.KeyCode == Keys.A)//Ctrl + A 全选
			{
				((TextBox)sender).SelectAll();
			}

			if(e.KeyCode == Keys.Escape)		//ESC清零
			{
				textBox_NetRecv.Text = "";
			}
		} 
	}
}
