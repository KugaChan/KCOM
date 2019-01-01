//#define SUPPORT_RECV_LONG_HANDLE  //可以提高速度，但是会有显示错乱，而且对于1222400来说最后还是会满的，所以不要打开了

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
using System.Diagnostics;   //conditional
using System.Threading;     //使用线程
using System.IO;			//判断文件是否存在
using System.Collections;

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
    public partial class FormMain
    {
        private SerialPort com = new SerialPort();

        private u32 com_send_cnt;
        private u32 com_recv_cnt;
        private u32 com_miss_cnt;
		private const int COM_BUFFER_SIZE_MAX = 4096;

        static readonly object locker_recv = new object();

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
			1382400,
            1234567,
		};

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
            catch(Exception ex)
            {
                MessageBox.Show("Can't list hardware info " + ex.Message);
                return null;
            }
            finally
            {
                strs = null;
            }
        }

        public void Func_Com_Component_Init()
        {
            s32 i;

			textBox_baudrate1.Text = Properties.Settings.Default.user_baudrate;
            send_fore_color_default = textBox_ComRec.ForeColor;
            send_back_color_default = textBox_ComRec.BackColor;
            if(checkBox_Cmdline.Checked == true)
            {
                textBox_ComSnd.Enabled = false;
            }

			if(param1.com_recv_ascii == true)
			{
				button_ASCIIShow.Text = "ASCII Recv";
			}
			else
			{
				button_ASCIIShow.Text = "Hex Recv";
			}

			if(param1.com_send_ascii == true)
			{
				button_ASCIISend.Text = "ASCII Send";
			}
			else
			{
				button_ASCIISend.Text = "Hex Send";
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
            comboBox_COMBaudrate.Items.Clear();
            for(i = 0; i < badurate_array.Length; i++)
            {
                if(badurate_array[i] == 1234567)
                {
                    if(textBox_baudrate1.Text != "")
                    {
                        comboBox_COMBaudrate.Items.Add(textBox_baudrate1.Text);
                    }
                    else
                    {
                        comboBox_COMBaudrate.Items.Add("1234567");
                    }
                }
                else
                {
                    comboBox_COMBaudrate.Items.Add(badurate_array[i].ToString());
                }
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
			com.ReadBufferSize = COM_BUFFER_SIZE_MAX;
			com.WriteBufferSize = COM_BUFFER_SIZE_MAX;

	        event_com_recv = new AutoResetEvent(false);
	        thread_com_recv = new Thread(ThreadEntry_ComRecv);
	        thread_com_recv.IsBackground = true;
	        thread_com_recv.Start();
        }

		Thread thread_com_recv;
		AutoResetEvent event_com_recv;		
		private void ThreadEntry_ComRecv()
		{
			while(true)
			{
				if(com_recv_offset == 0)
				{
					event_com_recv.WaitOne();	//FIFO已经空了，则在这里一直等待，直到有事件过来，可以有效降低CPU的占用率
				}
				else
				{
                    byte[] raw_data_buffer;

                    lock (locker_recv)
                    {
                        raw_data_buffer = new byte[com_recv_offset];
                        for(int i = 0; i < com_recv_offset; i++)            //从buffer中取出数据
                        {
                            raw_data_buffer[i] = com_recv_buffer[i];
                        }
                        com_recv_offset = 0;
                    }

                    #if false	//false, true
                        //打印发送数据
                        Console.Write("com Data[{0}]:", raw_data_buffer.Length);
                        for(int i = 0; i < raw_data_buffer.Length; i++)
                        {
                            Console.Write(" {0:X}", raw_data_buffer[i]);
                        }
                        Console.Write("\r\n");
                    #endif
					if(checkBox_FastPrintf.Checked == true)
					{
                        int offset = 0;                            
                        while(true)
                        {
                            int count;
                            if(raw_data_buffer.Length - offset > 64)
                            {
                                count = 64;
                            }
                            else
                            {
                                count = raw_data_buffer.Length - offset;
                            }                                

                            int recv_len;
                            byte[] recv_data;
                            recv_len = DataConvert(raw_data_buffer, offset, count, out recv_data);

                            if(recv_len > 0)
                            {
                                Func_COM_DataHandle(recv_data, recv_len);
                            }

                            offset += count;
                            if(raw_data_buffer.Length == offset)
                            {
                                break;
                            }
                        }
					}
					else
					{
						Func_COM_DataHandle(raw_data_buffer, raw_data_buffer.Length);
					}
                    date_time_com_recv_final = DateTime.Now;
				}
			}
		}

        private void Func_ClearRec()
        {
            textBox_ComRec.Text = "";
            label_Rec_Bytes.Text = "Received: 0";
            label_MissData.Text = "Miss: 0";
            com_recv_cnt = 0;
            com_miss_cnt = 0;
            timer_ColorShow.Enabled = true;
            label_ClearRec.BackColor = Color.Yellow;
        }

        private void label_ClearRec_DoubleClick(object sender, EventArgs e)
        {
            Func_ClearRec();
        }

        private void textBox_ComRec_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                Func_ClearRec();
            }
        }

        private void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //在串口运行的时候更改波特率，串口关闭时候修改的时候直接在按钮函数里改就行了
			if(com.IsOpen == true)
			{
				com.BaudRate = Convert.ToInt32(comboBox_COMBaudrate.SelectedItem.ToString());//赋值给串口

				try
				{
					com.Close();
					com.Open();
				}
                catch(Exception ex)
				{
                    MessageBox.Show("Can't open the COM port " + ex.Message, DbgIF.GetStack("Attention!"));
				}
			}            
        }

        private const int COM_BAUDRATE_WITH_SHOW = 91;
        private const int COM_BAUDRATE_WITH_SELECT = 320;
        private void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            comboBox_COMNumber.Width = COM_BAUDRATE_WITH_SELECT;

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

        private void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_COMNumber.SelectedIndex != -1)
            {
                Func_Set_Form_Text("", comboBox_COMNumber.SelectedItem.ToString());
            }

            comboBox_COMNumber.Width = COM_BAUDRATE_WITH_SHOW;
        }

        private bool com_is_closing = false;
        private String port_name_try;   //记录当前使用的COM的名字，由于是多线程访问，这个变量必须放在外面
        public void Func_COM_Close()
        {
            com_is_closing = true;
        
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
                        DbgIF.Assert(false, "###TODO: Why can not close COM" + ex.Message);//发生这种情况会怎么样...
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
                com_is_closing = false;
                DbgIF.Assert(false, "###TODO: Why can not close COM");
            }
            /****************串口异常断开则直接关闭窗体 End****************/

            try
            {
                com.Close();
            }
            catch(Exception ex)
            {
                com_is_closing = false;
                DbgIF.Assert(false, "###TODO: Why can not close COM " + ex.Message);
            }

            com_is_closing = false;
            Func_SetComStatus(false);
        }

        private void Func_COM_Open()
        {
            com.BaudRate = Convert.ToInt32(comboBox_COMBaudrate.SelectedItem.ToString());   //获得波特率
            switch(comboBox_COMCheckBit.SelectedItem.ToString())                            //获得校验位
            {
                case "None": com.Parity = Parity.None; break;
                case "Odd": com.Parity = Parity.Odd; break;
                case "Even": com.Parity = Parity.Even; break;
                default: com.Parity = Parity.None; break;
            }
            com.DataBits = Convert.ToInt16(comboBox_COMDataBit.SelectedItem.ToString());    //获得数据位
            switch(comboBox_COMStopBit.SelectedItem.ToString())                             //获得停止位
            {
                case "0": com.StopBits = StopBits.None; break;
                case "1": com.StopBits = StopBits.One; break;
                case "2": com.StopBits = StopBits.Two; break;
                case "1.5": com.StopBits = StopBits.OnePointFive; break;
                default: com.StopBits = StopBits.One; break;
            }

            if(comboBox_COMNumber.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose the COM port", DbgIF.GetStack("Attention!"));
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
            }
            catch(Exception ex)
            {
                DbgIF.Assert(false, "###TODO: Why can not open COM " + ex.Message);
            }

            Func_SetComStatus(true);
        }

        private void Func_SetComStatus(bool IsRunning)
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


        private void button_ComOpen_Click(object sender, EventArgs e)
        {
            if((button_COMOpen.ForeColor == Color.Red) && (com.IsOpen == false))      //打开串口
            {
                Func_COM_Open();
            }
            else if((button_COMOpen.ForeColor == Color.Green) && (com.IsOpen == true))   //关闭串口
            {
                Func_COM_Close();
            }
            else if((button_COMOpen.ForeColor == Color.Green) && (com.IsOpen == false))  //串口使用中途已经丢失了
			{
                Func_SetComStatus(false);
                comboBox_COMNumber.SelectedIndex = -1;
			}
			else
			{
                DbgIF.Assert(false, "###TODO: What is this statue!");
			}
        }

		private byte last_byte = 0xFF;
		int LastLogFileTime = 0;
		bool recv_need_add_time = false;
        private void Func_COM_DataHandle(byte[] com_recv_buffer, int com_recv_buff_size)
        {
            if(checkBox_Cmdline.Checked == true)                            //命令行处理时，需要把特殊符号去掉
            {
                console_handler_recv_func(com_recv_buffer, com_recv_buff_size);
            }

            com_recv_cnt += (u32)com_recv_buff_size;

            String SerialIn = "";											//把接收到的数据转换为字符串放在这里			

            if(param1.com_recv_ascii == false)		                        //十六进制接收，则需要转换为ASCII显示
            {
                Func _func = new Func();

                for(int i = 0; i < com_recv_buff_size; i++)
                {
                    SerialIn += _func.GetHexHighLow(com_recv_buffer[i], 0);
                    SerialIn += _func.GetHexHighLow(com_recv_buffer[i], 1) + " ";
                }
            }
            else
            {
                if(com_recv_buffer[com_recv_buff_size - 1] == 0x00)         //发现接收数据的最后一个字符经常会是0x00，过滤掉
                {
                    com_recv_buff_size--;
                }

                s32 i;
                for(i = 0; i < com_recv_buff_size; i++)
                {
                    byte[] array = new byte[1];
                    array[0] = com_recv_buffer[i];

                    if((array[0] == '\n') && (last_byte != '\r'))           //只有'\n'没有'\r'，则追加进去
                    {
                        byte[] arrayx = new byte[1];
                        arrayx[0] = (byte)'\r';
                        SerialIn += System.Text.Encoding.ASCII.GetString(arrayx);
                    }
                    last_byte = array[0];

                    if((array[0] == 0x00) || (array[0] > 0x7F))             //收到非ASCII码要显示一下
                    {
                        if (checkBox_Fliter.Checked == true)
                        {
                            Func _func = new Func();

                            SerialIn += " ~";

                            SerialIn += _func.GetHexHighLow(array[0], 0);
                            SerialIn += _func.GetHexHighLow(array[0], 1);

                            SerialIn += "~ ";
                        }
                    }
                    else
                    {
                        if(Properties.Settings.Default._add_Time == 0)		    //不加时间戳
                        {
                            //SerialIn = System.Text.Encoding.ASCII.GetString(com_recv_buffer, 0, com_recv_buff_size);

                            SerialIn += System.Text.Encoding.ASCII.GetString(array);
                        }
                        else if(Properties.Settings.Default._add_Time == 1)     //时间戳加在前面
                        {
                            if(recv_need_add_time == true)
                            {
                                recv_need_add_time = false;
                                SerialIn += "[" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff") + "]";
                            }

                            SerialIn += System.Text.Encoding.ASCII.GetString(array);

                            if(com_recv_buffer[i] == '\n')
                            {
                                recv_need_add_time = true;
                            }
                        }
                        else                                            //时间戳加在后面
                        {
                            SerialIn += System.Text.Encoding.ASCII.GetString(array);

                            if(com_recv_buffer[i] == '\n')
                            {
                                SerialIn = SerialIn.Substring(0, SerialIn.Length - 2);//把'\r'和'\n'去掉

                                SerialIn += "[";
                                SerialIn += DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff");
                                SerialIn += "]\r\n";
                            }
                        }                   
                    }
                }
            }

            if(log_file_name != null)
            {
                StreamWriter sw_log_file = File.AppendText(log_file_name);//在目标文件原有内容后面追加内容

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

                        sw_log_file.Write(TimeShow);

                        LastLogFileTime = currentMinute;
                    }
                }

                sw_log_file.Write(SerialIn);
                sw_log_file.Flush();//清空缓冲区
                sw_log_file.Close();//关闭关键
            }

            this.Invoke((EventHandler)(delegate
            {
                label_Rec_Bytes.Text = "Received: " + Convert.ToString(com_recv_cnt);
                if(checkBox_CursorMove.Checked == false)
                {
                    textBox_ComRec.AppendText(SerialIn);                    //在接收文本中添加串口接收数据
                }
                else
                {
                    textBox_ComSnd.AppendText(SerialIn);
                }

                if(checkBox_LimitRecLen.Checked == true)					//限定接收文本的长度,防止logfile接收太多东西，KCOM死掉
                {
                    if(textBox_ComRec.TextLength >= 65536 * 1024)
                    {
                        textBox_ComRec.Text = "[KCOM: reset the recv windows!]\r\n";
                    }
                }
            }));

            if(SerialIn.Length > 0)
            {
                Func_NetCom_SendData(SerialIn);							//串口接收到的数据，发送给网络端
            }
            
            date_time_com_recv_final = DateTime.Now;
        }

		byte[] com_recv_buffer = new byte[COM_BUFFER_SIZE_MAX*1024];//4MB的缓存，满了数据就要溢出
		int com_recv_offset = 0;
		DateTime date_time_com_recv_final = DateTime.Now;

        private void Func_COM_DataRec(object sender, SerialDataReceivedEventArgs e)  //串口接受函数
        {
			if((com_is_closing == true) || (com.IsOpen == false))
			{
				return;
            }

            int real_len;
            lock(locker_recv)
            {
                real_len = com_recv_offset;
            }

            if(real_len > COM_BUFFER_SIZE_MAX*2)
            {
                com_miss_cnt += (uint)com.ReadBufferSize;
                this.Invoke((EventHandler)(delegate
                {
                    label_MissData.Text = "Miss: " + Convert.ToString(com_miss_cnt);
                }));

                Console.WriteLine("###com:{0} recv buffer is full:{1} {2}, data miss!!!", 
                    com.IsOpen, real_len, com_recv_offset);

                com_recv_offset = 0;
                return;
            }

            int com_recv_buff_size;

            lock(locker_recv)
            {
                com_recv_buff_size = com.Read(com_recv_buffer, com_recv_offset, com.ReadBufferSize);
                com_recv_offset += com_recv_buff_size;
            }

            if(com_recv_buff_size == 0)
            {
                return;
            }			

			#if false
				Console.Write("RECA[{0}]: in:{1}-{2} out:{3}-{4}", com_recv_buff_size,
					com_recv_fifo_top, com_recv_fifo_buttom, fp_out_top, fp_out_buttom);

				for(int v = 0; v < com_recv_buff_size; v++)
				{
					Console.Write(" {0:X}", com_recv_buffer[v]);
				}
				Console.Write("\r\n");
			#endif

            event_com_recv.Set();						        //唤醒recv线程去取queue
        }

        private void Func_ClearSnd()
        {
            textBox_ComSnd.Text = "";
            label_Send_Bytes.Text = "Sent: 0";
            com_send_cnt = 0;

            checkBox_EnAutoSndTimer.Checked = false;
            timer_AutoSnd.Enabled = false;
        }

        private void textBox_ComSnd_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                Func_ClearSnd();
            }
        }

        private void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            #if false
            if(e.KeyCode == Keys.Escape)
            {
		        Func_ClearSnd();
            }
            #endif

            if(e.Control && e.KeyCode == Keys.S)//Keys.Enter
            {
                Func_Com_Send();
            }
        }

        private void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("[KEY]:{0} {1}", e.Control, e.KeyCode);
            if (e.Control && e.KeyCode == Keys.A)		//Ctrl + A 全选
            {
                ((TextBox)sender).SelectAll();
            }

            //使用鼠标中键清空，ESC容易被切屏软件误触发
            #if false
            if (e.KeyCode == Keys.Escape)				//ESC清零
            {
                Func_ClearRec();
            }
            #endif
        }

        private void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            comboBox_COMBaudrate.Items.Clear();
            //波特率
            for (int i = 0; i < badurate_array.Length; i++)
            {
                if (badurate_array[i] == 1234567)
                {
                    if (textBox_baudrate1.Text != "")
                    {
                        comboBox_COMBaudrate.Items.Add(textBox_baudrate1.Text);
                    }
                    else
                    {
                        comboBox_COMBaudrate.Items.Add("1234567");
                    }
                }
                else
                {
                    comboBox_COMBaudrate.Items.Add(badurate_array[i].ToString());
                }
            }
        }

        private void button_SendDataClick(object sender, EventArgs e)
        {
			if(netcom_is_connected == false)	//网络没有连接上
			{
				Func_Com_Send();
			}
			else
			{
				Func_NetCom_SendData(textBox_ComSnd.Text);
			}            
        }

        private void button_CleanSnd_Click(object sender, EventArgs e)
        {
            Func_ClearSnd();
        }

        public void Func_Com_Send()
        {
            const uint max_recv_length = 65535;

            if(textBox_ComSnd.Text.Length == 0)
            {
                MessageBox.Show("Please input data", DbgIF.GetStack("Warning!"));
                return;
            }

            if(textBox_ComSnd.Text.Length > max_recv_length)
            {
                MessageBox.Show("Data too long", DbgIF.GetStack("Warning!"));
                return;
            }
			
            if(param1.com_send_ascii == true)			                    //ASCII发送
            {
                try
                {
                    //string ss = textBox_COM.Text.Trim();
                    com.Write(textBox_ComSnd.Text);
                    this.Invoke((EventHandler)(delegate
                    {
                        com_send_cnt += (u32)textBox_ComSnd.Text.Length;
                        label_Send_Bytes.Text = "Sent: " + com_send_cnt.ToString();                        
                    }));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, DbgIF.GetStack("Warning!"));
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
                    if(	(chahArray[i] != '0')
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
                        MessageBox.Show("Error input format!", DbgIF.GetStack("Warning!"));
                        return;
                    }
                }
                /*	需要检查输入的合法性		*/

				Func func = new Func();				
                for(int i = 2; i < n; i++)//找出所有空格
                { //0x3F
                    if(chahArray[i] == ' ')
                    {
						uint hex_h = func.CharToByte(chahArray[i - 2]);//3
						uint hex_l = func.CharToByte(chahArray[i - 1]);//F	
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
                    label_Send_Bytes.Text = "Sent: " + com_send_cnt.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, DbgIF.GetStack("Warning!"));
                }
            }
        }

		private void checkBox_WordWrap_CheckedChanged(object sender, EventArgs e)
		{
			textBox_ComRec.WordWrap = checkBox_WordWrap.Checked;
		}

        private void button_ASCIIShow_Click(object sender, EventArgs e)//ascii or hex show button
        {			
            if(param1.com_recv_ascii == true)	//从ASCII到HEX
            {
                param1.com_recv_ascii = false;

                textBox_ComRec.WordWrap = true;
                button_ASCIIShow.Text = "Hex Recv";

                Func _func = new Func();
                textBox_ComRec.Text = _func.TextConvert_ASCII_To_Hex(textBox_ComRec.Text);	
            }
            else//从HEX转到ASCII
            {
				param1.com_recv_ascii = true;

                textBox_ComRec.WordWrap = false;
                button_ASCIIShow.Text = "ASCII Recv";

                Func _func = new Func();
                textBox_ComRec.Text = _func.TextConvert_Hex_To_ASCII(textBox_ComRec.Text);
            }
        }

        private void button_ASCIISend_Click(object sender, EventArgs e)
        {
			checkBox_Cmdline.Enabled = false;
			
			//ascii -> hex
            if(param1.com_send_ascii == true)
            {
                param1.com_send_ascii = false;

                button_ASCIISend.Text = "Hex Send";

                Func _func = new Func();
                textBox_ComSnd.Text = _func.TextConvert_ASCII_To_Hex(textBox_ComSnd.Text);				
            }
            else//从HEX转到ASCII
            {
				param1.com_send_ascii = true;

				checkBox_Cmdline.Enabled = true;

                button_ASCIISend.Text = "ASCII Send";

                Func _func = new Func();
                textBox_ComSnd.Text = _func.TextConvert_Hex_To_ASCII(textBox_ComSnd.Text);
            }
        }

        private u32 dwTimerCount = 0;
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
    }
}
