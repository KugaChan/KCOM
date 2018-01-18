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
    public partial class Form1
    {
        private bool com_is_receiving = false;
        private bool com_allow_receive = true;

        enum key_send
        {
            KEY_SEND_ASCII,
            KEY_SEND_HEX
        };

        enum key_show
        {
            KEY_SHOW_ASCII,
            KEY_SHOW_HEX
        }; 

        //功能
        private SerialPort com = new SerialPort();
        private key_send send_ASCII_HEX = key_send.KEY_SEND_ASCII;
        private key_show show_ASCII_HEX = key_show.KEY_SHOW_ASCII;
        private bool com_is_open = false;
        private u32 com_send_cnt;
        private u32 com_recv_cnt;

        private int current_column;

        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }

        /// <summary>
        /// Get the system devices information with windows api.
        /// </summary>
        /// <param name="hardType">Device type.</param>
        /// <param name="propKey">the property of the device.</param>
        /// <returns></returns>
        private static string[] Func_GetHarewareInfo(HardwareEnum hardType, string propKey)
        {
            List<string> strs = new List<string>();
            try
            {
                using(ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach(var hardInfo in hardInfos)
                    {
                        if(hardInfo.Properties[propKey].Value != null)
                        {
                            String str = hardInfo.Properties[propKey].Value.ToString();
                            strs.Add(str);
                        }
                    }
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                strs = null;
            }
        }

        private void label_ClearRec_DoubleClick(object sender, EventArgs e)
        {
            textBox_ComRec.Text = "";
            label_Rec_Bytes.Text = "0";
            com_recv_cnt = 0;
            bChangeColor = true;
            timer_ColorShow.Enabled = true;
            label_ClearRec.BackColor = System.Drawing.Color.Yellow;
        }

        private void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
			Properties.Settings.Default._baudrate_select_index = comboBox_COMBaudrate.SelectedIndex;

            if(com_is_open == true) //在串口运行的时候更改波特率，串口关闭时候修改的时候直接在按钮函数里改就行了
            {
                if(com_is_receiving == true)
                {
                    com_allow_receive = false;
                    com_change_baudrate = true;
                }
                else
                {
                    com.BaudRate = Convert.ToInt32(comboBox_COMBaudrate.SelectedItem.ToString());//赋值给串口

                    try
                    {
                        com.Close();
                        com.Open();
                    }
                    catch
                    {
                        MessageBox.Show("无法打开串口", "提示");
                    }
                }
            }
        }

        private void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)//Keys.Enter
            {
                textBox_ComSnd.Text = "";
            }

            if(e.Control && e.KeyCode == Keys.S)//Keys.Enter
            {
                Func_Com_Send();
            }
        }

        private void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
			if(e.Control && e.KeyCode == Keys.A)		//Ctrl + A 全选
            {
                ((TextBox)sender).SelectAll();
            }

			if(e.KeyCode == Keys.Escape)				//ESC清零
            {
                textBox_ComRec.Text = "";
                label_Rec_Bytes.Text = "0";
                com_recv_cnt = 0;
            }
        }

        private void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            comboBox_COMNumber.Items.Clear();

            string[] strArr = Func_GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");

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
            }

            comboBox_COMNumber.SelectedIndex = -1;
        }

		public void Func_Com_Open()
		{
			if(com_is_open == true)//关闭串口
			{
				Func_COM_Close();
				//comboBox_COMBaudrate.Enabled = true;
				comboBox_COMCheckBit.Enabled = true;
				comboBox_COMDataBit.Enabled = true;
				comboBox_COMNumber.Enabled = true;
				comboBox_COMStopBit.Enabled = true;
			}
			else//打开串口
			{
				com.BaudRate = Convert.ToInt32(comboBox_COMBaudrate.SelectedItem.ToString());   //获得波特率
				//Console.Write("@@@@{0}", com.BaudRate);
				switch(comboBox_COMCheckBit.SelectedItem.ToString())                           //获得校验位
				{
					case "None": com.Parity = Parity.None; break;
					case "Odd": com.Parity = Parity.Odd; break;
					case "Even": com.Parity = Parity.Even; break;
					default: com.Parity = Parity.None; break;
				}
				com.DataBits = Convert.ToInt16(comboBox_COMDataBit.SelectedItem.ToString());    //获得数据位
				switch(comboBox_COMStopBit.SelectedItem.ToString())                            //获得停止位
				{
					case "0": com.StopBits = StopBits.None; break;
					case "1": com.StopBits = StopBits.One; break;
					case "2": com.StopBits = StopBits.Two; break;
					case "1.5": com.StopBits = StopBits.OnePointFive; break;
					default: com.StopBits = StopBits.One; break;
				}

				if(comboBox_COMNumber.SelectedIndex == -1)
				{
					MessageBox.Show("请选择串口", "提示");
					return;
				}

				String PortName = comboBox_COMNumber.SelectedItem.ToString();
				Console.WriteLine("Port name:{0}", PortName);
				int end = PortName.IndexOf(":");
				com.PortName = PortName.Substring(0, end);                  //获得串口数
				try
				{
					com.Open();

					com.DiscardInBuffer();
					com.DiscardOutBuffer();

					com_is_open = true;
				}
				catch
				{
					com_is_open = false;
					MessageBox.Show("无法打开串口", "提示");
				}

				if(com_is_open == true)
				{
					button_COMOpen.Text = "串口已开";
					button_COMOpen.ForeColor = System.Drawing.Color.Green;

					com_allow_receive = true;
					com_is_receiving = false;

					//comboBox_COMBaudrate.Enabled = false;
					comboBox_COMCheckBit.Enabled = false;
					comboBox_COMDataBit.Enabled = false;
					comboBox_COMNumber.Enabled = false;
					comboBox_COMStopBit.Enabled = false;					
				}
			}		
		}

        private void button_ComOpen_Click(object sender, EventArgs e)
        {
			Func_Com_Open();			
        }

        private void button_SendDataClick(object sender, EventArgs e)
        {
            Func_Com_Send();
        }

        private void button_CleanSnd_Click(object sender, EventArgs e)
        {
            textBox_ComSnd.Text = "";
            label_Send_Bytes.Text = "0";
            com_send_cnt = 0;

            checkBox_EnAutoSndTimer.Checked = false;
            timer_AutoSnd.Enabled = false;
        }

        private String port_name_try;   //记录当前使用的COM的名字，由于是多线程访问，这个变量必须放在外面
        public void Func_COM_Close()
        {
            if(com_is_open == true)
            {
                /****************串口异常断开则直接关闭窗体 Start**************/                
                int get_port_name_cnt = 0;
                while(true)
                {
                    bool get_port_name_sta = false;

                    try
                    {
                        this.Invoke((EventHandler)(delegate
                        {
                            port_name_try = comboBox_COMNumber.SelectedItem.ToString();
                        }));                        
                        get_port_name_sta = true;
                    }
                    catch(Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        get_port_name_cnt++;
                        if(get_port_name_cnt % 999 == 0)
                        {
                            MessageBox.Show(ex.Message, "无法关闭串口");
                            while(true);//发生这种情况会怎么样...
                        } 
                    }

                    if(get_port_name_sta == true)
                    {
                        break;
                    }
                    //System.Threading.Thread.Sleep(10);
                }

                String PortName = port_name_try;
                int end = PortName.IndexOf(":");
                PortName = PortName.Substring(0, end);                      //截取获得COM口序号

                bool current_com_exist = false;
                string[] strArr = Func_GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");
                foreach(string vPortName in SerialPort.GetPortNames())
                {
                    if(vPortName == PortName)
                    {
                        current_com_exist = true;                           //当前串口还在设备列表里
                    }

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
                    this.Invoke((EventHandler)(delegate
                    {
                        comboBox_COMNumber.Items.Add(SerialIn);             //将设备列表里的COM放进下拉菜单上
                    }));                    
                }

                //关闭串口时发现正在使用的COM不见了，由于无法调用com.close()，所以只能异常退出了
                if(current_com_exist == false)
                {
                    MessageBox.Show("串口已失去连接，程序关闭!!!", "警告");
                    System.Environment.Exit(0);
                }
                /****************串口异常断开则直接关闭窗体 End****************/

                com_allow_receive = false;//禁止接收数据
                if(com_is_receiving == true)
                {
                    Console.WriteLine("COM_IsReceving == true, Get out!");
                    return;
                }

                try
                {
                    com.Close();
                    com_is_open = false;
                    this.Invoke((EventHandler)(delegate
                    {
                        button_COMOpen.Text = "串口已关";
                        button_COMOpen.ForeColor = System.Drawing.Color.Red;
                    }));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "无法关闭串口");
                }
            }

            if(form_is_closed == true)
            {
                Func_PropertiesSettingsSave();

                this.Invoke((EventHandler)(delegate
                {
                    this.Close();
                }));
            }

            if(com_change_baudrate == true)
            {
                com_change_baudrate = false;
                this.Invoke((EventHandler)(delegate
                {
                    com.BaudRate = Convert.ToInt32(comboBox_COMBaudrate.SelectedItem.ToString());//赋值给串口
                }));

                try
                {
                    com.Open();
                    this.Invoke((EventHandler)(delegate
                    {
                        button_COMOpen.Text = "串口已开";
                        button_COMOpen.ForeColor = System.Drawing.Color.Green;
                        com_is_open = true;
                        com_allow_receive = true;
                    }));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "无法打开串口");
                }
            }
        }


        private void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default._com_num_select_index = comboBox_COMNumber.SelectedIndex;
			Func_Set_Form_Text("", comboBox_COMNumber.SelectedItem.ToString());
        }

        public void Func_Com_Send()
        {
            const uint max_recv_length = 65535;

            if(textBox_ComSnd.Text.Length == 0)
            {
                MessageBox.Show("请输入数据", "提示");
                return;
            }

            if(textBox_ComSnd.Text.Length > max_recv_length)
            {
                MessageBox.Show("数据太长了", "提示");
                return;
            }

            if(send_ASCII_HEX == key_send.KEY_SEND_ASCII)
            {
                try
                {
                    //string ss = textBox_COM.Text.Trim();
                    com.Write(textBox_ComSnd.Text);
                    this.Invoke((EventHandler)(delegate
                    {
                        com_send_cnt += (u32)textBox_ComSnd.Text.Length;
                        label_Send_Bytes.Text = com_send_cnt.ToString();
                    }));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示");
                }
            }
            else//16进制发送
            {
                //转换16进制的string成byte[]
                byte[] bb = new byte[max_recv_length];
                string com_send_text = textBox_ComSnd.Text;
                int length_bb = 0;
                com_send_text += " ";
                int n = com_send_text.Length;
                char[] chahArray = new char[n];
                chahArray = com_send_text.ToCharArray();//将字符串转换为字符数组

                /*	需要检查输入的合法性		*/
                for(int i = 0; i < n; i++)//搜索是否有非法字符
                {
                    if((chahArray[i] != '0')
                        && (chahArray[i] != '1')
                        && (chahArray[i] != '2')
                        && (chahArray[i] != '3')
                        && (chahArray[i] != '4')
                        && (chahArray[i] != '5')
                        && (chahArray[i] != '6')
                        && (chahArray[i] != '7')
                        && (chahArray[i] != '8')
                        && (chahArray[i] != '9')
                        && (chahArray[i] != 'A')
                        && (chahArray[i] != 'B')
                        && (chahArray[i] != 'C')
                        && (chahArray[i] != 'D')
                        && (chahArray[i] != 'E')
                        && (chahArray[i] != 'F')
                        && (chahArray[i] != 'a')
                        && (chahArray[i] != 'b')
                        && (chahArray[i] != 'c')
                        && (chahArray[i] != 'd')
                        && (chahArray[i] != 'e')
                        && (chahArray[i] != 'f')
                        && (chahArray[i] != ' '))
                    {
                        MessageBox.Show("输入格式错误", "提示");
                        return;
                    }
                }
                /*	需要检查输入的合法性		*/

                for(int i = 2; i < n; i++)//找出所有空格
                { //0x3F
                    if(chahArray[i] == ' ')
                    {
                        uint hex_h = Func_CharByte(chahArray[i - 2]);//3
                        uint hex_l = Func_CharByte(chahArray[i - 1]);//F	
                        byte hex = (byte)(((uint)hex_h) << 4 | hex_l);

                        if(hex_h == 0xFF || hex_l == 0xFF)
                        {
                            continue;
                        }

                        bb[length_bb] = hex;
                        length_bb++;
                    }
                }

                try
                {
                    com.Write(bb, 0, length_bb);
                    com_send_cnt += (u32)length_bb;
                    label_Send_Bytes.Text = com_send_cnt.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示");
                }
            }
        }
		
        private void Func_COM_DataRec(object sender, SerialDataReceivedEventArgs e)  //串口接受函数
        {
            if(com_allow_receive == true)
            {
                com_is_receiving = true;
                s32 com_recv_buff_size;
                byte[] com_recv_buffer = new byte[com.ReadBufferSize + 1];
                String SerialIn = "";                                       //把接收到的数据转换为字符串放在这里

                com_recv_buff_size = com.Read(com_recv_buffer, 0, com.ReadBufferSize);

                if(com_recv_buff_size == 0)
                {
                    return;
                }

                com_recv_cnt += (u32)com_recv_buff_size;

                if(show_ASCII_HEX == key_show.KEY_SHOW_HEX)					//十六进制接收，则需要转换为ASCII显示
                {
                    for(int i = 0; i < com_recv_buff_size; i++)
                    {
                        SerialIn += Func_GetHexHigh(com_recv_buffer[i], 0);
                        SerialIn += Func_GetHexHigh(com_recv_buffer[i], 1) + " ";
                    }
                }
                else if(show_ASCII_HEX == key_show.KEY_SHOW_ASCII)
                {
                    s32 i;
                    if(Properties.Settings.Default._add_Time > 0)
                    {   
                        this.Invoke((EventHandler)(delegate
                        {
                            int index;
                            int row;
                            int coloum;

                            index = this.textBox_ComRec.GetFirstCharIndexOfCurrentLine();
                            row = this.textBox_ComRec.GetLineFromCharIndex(index);
                            coloum = this.textBox_ComRec.SelectionStart - index;
                            current_column = coloum;
                        }));

                        for(i = 0; i < com_recv_buff_size; i++)
                        {
                            byte[] array = new byte[1];

                            if(Properties.Settings.Default._add_Time == 1)  //时间戳加在前面
                            {
                                if((com_recv_buffer[i] == '\n') && ((i + 1) < com_recv_buff_size))
                                {
                                    SerialIn += "\n[";
                                    SerialIn += DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");
                                    SerialIn += "]";
                                }
                                else if(current_column == 0)
                                {
                                    SerialIn += "[";
                                    SerialIn += DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");
                                    SerialIn += "]";
                                    array[0] = com_recv_buffer[i];
                                    SerialIn += System.Text.Encoding.ASCII.GetString(array);
                                }
                                else
                                {
                                    array[0] = com_recv_buffer[i];
                                    SerialIn += System.Text.Encoding.ASCII.GetString(array);
                                }                                
                            }
                            else                                            //时间戳加在后面
                            {
                                if((com_recv_buffer[i] == '\r') && (com_recv_buffer[i + 1] == '\n'))
                                {
                                    SerialIn += "[";
                                    SerialIn += DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");
                                    SerialIn += "]\r";
                                    current_column = 0;
                                }
                                else
                                {
                                    array[0] = com_recv_buffer[i];
                                    SerialIn += System.Text.Encoding.ASCII.GetString(array);                               
                                }
                            }

                            current_column++;
                        }
                    }
                    else
                    {
                        SerialIn = System.Text.Encoding.ASCII.GetString(com_recv_buffer, 0, com_recv_buff_size);
                    }
                }

                if(bCreateLogFile == true)
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText(logFile.FileName);//在目标文件原有内容后面追加内容

                    string TimeShow;
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;
                    int currentDay = DateTime.Now.Day;
                    int currentHour = DateTime.Now.Hour;
                    int currentMinute = DateTime.Now.Minute;
                    int currentSecond = DateTime.Now.Second;
                    if(Properties.Settings.Default._add_Time == 0)
                    {
                        if(currentMinute != LastLogFileTime)
                        {
                            TimeShow = "\r\n\r\n--------------------------"
                                + "_Y" + currentYear.ToString()
                                + "_M" + currentMonth.ToString()
                                + "_D" + currentDay.ToString()
                                + "_H" + currentHour.ToString()
                                + "_M" + currentMinute.ToString()
                                + "_S" + currentSecond.ToString() + "--------------------------\r\n\r\n";

                            sw.Write(TimeShow);

                            LastLogFileTime = currentMinute;
                        }
                    }

                    sw.Write(SerialIn);
                    sw.Flush();//清空缓冲区
                    sw.Close();//关闭关键              
                }

                this.Invoke((EventHandler)(delegate
                {
                    label_Rec_Bytes.Text = Convert.ToString(com_recv_cnt);
                    this.textBox_ComRec.AppendText(SerialIn);               //在接收文本中添加串口接收数据

                    if(checkBox_LockRecLen.Checked == true)					//限定接收文本的长度,防止logfile接收太多东西，KCOM死掉
                    {
                        if(textBox_ComRec.TextLength >= 65536 * 100)
                        {
                            textBox_ComRec.Text = "";
                            //label_Rec_Bytes.Text = "0";
                        }
                    }
                }));

				Func_NetCom_SendData(SerialIn);								//串口接收到的数据，发送给网络端

                com_is_receiving = false;
            }
            else
            {
                Func_COM_Close();
            }
        }

        private void button_ASCIIShow_Click(object sender, EventArgs e)//ascii or hex show button
        {
            if(show_ASCII_HEX == key_show.KEY_SHOW_ASCII)
            {
                show_ASCII_HEX = key_show.KEY_SHOW_HEX;
                button_ASCIIShow.Text = "16进制显示";
                button_ASCIIShow.ForeColor = System.Drawing.Color.Brown;

                int n = textBox_ComRec.Text.Length;
                if(n != 0)
                {
                    char[] chahArray = new char[n];
                    chahArray = textBox_ComRec.Text.ToCharArray();//将字符串转换为字符数组
                    //Console.Write("chahArray: {0}\r\n", (byte)chahArray[0]);
                    textBox_ComRec.Text = "";
                    for(int i = 0; i < n; i++)
                    {
                        textBox_ComRec.Text += Func_GetHexHigh((byte)chahArray[i], 0);
                        textBox_ComRec.Text += Func_GetHexHigh((byte)chahArray[i], 1) + " ";
                    }
                }
            }
            else//从HEX转到ASCII 03 0
            {
                show_ASCII_HEX = key_show.KEY_SHOW_ASCII;
                button_ASCIIShow.Text = "ASCII显示";
                button_ASCIIShow.ForeColor = System.Drawing.Color.Blue;


                int n = textBox_ComRec.Text.Length;
                if(n != 0)
                {
                    textBox_ComRec.Text += " ";
                    n += 1;

                    char[] chahArray = new char[n];
                    chahArray = textBox_ComRec.Text.ToCharArray();//将字符串转换为字符数组
                    textBox_ComRec.Text = "";

                    for(int i = 2; i < n; i++)//找出所有空格
                    { //0x3F
                        if(chahArray[i] == ' ')
                        {
                            int hex_h = Func_CharByte(chahArray[i - 2]);//3
                            byte hex_l = Func_CharByte(chahArray[i - 1]);//F	
                            int hex = hex_h << 4 | hex_l;

                            if(hex_h == 0xFF || hex_l == 0xFF)
                            {
                                continue;
                            }

                            textBox_ComRec.Text += (char)hex;
                        }
                    }
                }
            }
        }

        private void button_ASCIISend_Click(object sender, EventArgs e)      //ascii or hex send button
        {
            if(send_ASCII_HEX == key_send.KEY_SEND_ASCII)//ascii -> hex
            {
                send_ASCII_HEX = key_send.KEY_SEND_HEX;
                button_ASCIISend.Text = "16进制发送";
                button_ASCIISend.ForeColor = System.Drawing.Color.Brown;

                int n = textBox_ComSnd.Text.Length;
                if(n != 0)
                {
                    char[] chahArray = new char[n];
                    chahArray = textBox_ComSnd.Text.ToCharArray();//将字符串转换为字符数组
                    textBox_ComSnd.Text = "";
                    for(int i = 0; i < n; i++)
                    {
                        textBox_ComSnd.Text += Func_GetHexHigh((byte)chahArray[i], 0);
                        textBox_ComSnd.Text += Func_GetHexHigh((byte)chahArray[i], 1) + " ";
                    }
                }
            }
            else//从HEX转到ASCII
            {
                send_ASCII_HEX = key_send.KEY_SEND_ASCII;
                button_ASCIISend.Text = "ASCII发送";
                button_ASCIISend.ForeColor = System.Drawing.Color.Blue;

                int n = textBox_ComSnd.Text.Length;
                if(n != 0)
                {
                    textBox_ComSnd.Text += " ";
                    n += 1;

                    char[] chahArray = new char[n];
                    chahArray = textBox_ComSnd.Text.ToCharArray();//将字符串转换为字符数组
                    textBox_ComSnd.Text = "";

                    for(int i = 2; i < n; i++)//找出所有空格
                    { //0x3F
                        if(chahArray[i] == ' ')
                        {
                            int hex_h = Func_CharByte(chahArray[i - 2]);//3
                            byte hex_l = Func_CharByte(chahArray[i - 1]);//F	
                            int hex = hex_h << 4 | hex_l;

                            if(hex_h == 0xFF || hex_l == 0xFF)
                            {
                                continue;
                            }

                            textBox_ComSnd.Text += (char)hex;
                        }
                    }
                }
            }
        }

        private void timer_AutoSnd_Tick(object sender, EventArgs e)
        {
            ushort CNT = (ushort)(double.Parse(textBox_N100ms.Text));

            dwTimerCount++;
            if(dwTimerCount >= CNT)
            {
                dwTimerCount = 0;
                Func_Com_Send();
            }
        }

        private void timer_renew_com_Tick(object sender, EventArgs e)
        {
			//Console.WriteLine("B:{0}|{1}", net_recv_top, net_recv_bottom);

			bool need_append_text;
			string recvmsg = "";

			if(net_recv_top > net_recv_bottom)
			{
				need_append_text = true;
			}
			else
			{
				need_append_text = false;
			}

			while(net_recv_top > net_recv_bottom)
			{
				recvmsg += net_recv_str_array[net_recv_bottom % net_recv_str_array.Length];
				net_recv_bottom++;
			}

			if(need_append_text == true)
			{
				textBox_NetRecv.AppendText(recvmsg);    //使用AppendText可以让文件光标随着文本走，而+=不行
			}
			
            //Console.WriteLine(DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff"));
            //label_com_running.Text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");
        }
    }
}
