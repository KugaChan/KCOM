//#define SUPPORT_RECV_LONG_HANDLE  //可以提高速度，但是会有显示错乱，而且对于1222400来说最后还是会满的，所以不要打开了
#define SUPPORT_ESC_CLEAR

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

namespace KCOM
{
    partial class COM
    {
        const int COM_BUFFER_SIZE_MAX = 4096;

        public class tyRcvFIFO
        {
            public readonly object locker = new object();
            public int top = 0;
            public int buttom = 0;
            public byte[] buffer = new byte[16 * 1024 * 1024];     //32MB的缓存，满了数据就要溢出

            public void reset()
            {
                lock(locker)
                {
                    top = 0;
                    buttom = 0;
                }
            }
        }

        public struct tyRecord
        {
            public uint miss_data;
            public uint buffer_left;
            public uint rcv_bytes;
            public uint snd_bytes;            
        }

        public class tyConfig
        {
            public bool auto_send = false;
            public bool cursor_fixed = false;
            public bool cmdline_mode = false;

            public bool ascii_rcv = false;
            public bool ascii_snd = false;

            public bool fliter_ileagal_char = false;
            public bool limiet_rcv_lenght = false;
            public bool backup_rcv_data = false;
            public bool midmouse_clear_data = false;
            public bool esc_clear_data = false;

            public int auto_send_inverval_100ms = 1;
            public int custom_baudrate = 1234567;
        }
        public tyConfig cfg = new tyConfig();

        public class tyTxt
        {
            public string send = "";
            public string receive = "";
            public string backup = "";
            public string temp = "";
        }
        public tyTxt txt = new tyTxt();

        //这些资源是com对象自己拥有的，本应定义在com对象里的，只是定义在com里面，没办法在设计中拖拽修改空间，所以只好放在里面了
        public struct tyIntResource
        {
            public ComboBox comboBox_COMNumber;
            public ComboBox comboBox_COMBaudrate;
            public ComboBox comboBox_COMCheckBit;
            public ComboBox comboBox_COMDataBit;
            public ComboBox comboBox_COMStopBit;
        }

        public struct tyExtResource
        {
            public eTCP etcp;
            public FastPrint fp;
        }

        public Cmdline cmdline = new Cmdline();
        public tyIntResource me = new tyIntResource();
        public tyExtResource fm = new tyExtResource();
        public tyRecord record;
        public SerialPort serialport = new SerialPort();
        public string log_file_name = null;

        public struct TxtOP
        {
            public const int NULL = 0;
            public const int CLEAR = 1;
            public const int ADD = 2;
            public const int EQUAL = 3;
        }

        public AutoResetEvent event_txt_update = new AutoResetEvent(false);
        public Thread thread_txt_update;
        public eFIFO_string efifo = new eFIFO_string();

        private Thread thread_recv;
        private AutoResetEvent event_recv;
        private System.Timers.Timer timer_AutoSnd;
        private tyRcvFIFO rcv_fifo = new tyRcvFIFO();
        private int[] badurate_array =
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

        public COM()
        {
            efifo.Init(COM_BUFFER_SIZE_MAX*2, 1024*1024);

            timer_AutoSnd = new System.Timers.Timer();                                          //实例化Timer类，设置间隔时间为1000毫秒
            timer_AutoSnd.Elapsed += new System.Timers.ElapsedEventHandler(timer_AutoSnd_Tick); //到达时间的时候执行事件
            timer_AutoSnd.AutoReset = true;                                                     //设置是执行一次（false）还是一直执行(true)
            timer_AutoSnd.Enabled = false;                                                      //是否执行System.Timers.Timer.Elapsed事件
            timer_AutoSnd.Interval = cfg.auto_send_inverval_100ms * 100;
        }

        public void Init(bool _cmdline_mode, bool _ascii_rcv, bool _ascii_snd, bool _fliter_ileagal_char, int custom_baudrate)
        {
            cfg.custom_baudrate = custom_baudrate;

            cfg.cmdline_mode = _cmdline_mode;
            cfg.ascii_rcv = _ascii_rcv;
            cfg.ascii_snd = _ascii_snd;

            cfg.fliter_ileagal_char = _fliter_ileagal_char;

            
            //更新串口下来列表的选项
            Ruild_ComNumberList(me.comboBox_COMNumber);

            //波特率
            Rebulid_BaudrateList(me.comboBox_COMBaudrate);

            //校验位
            me.comboBox_COMCheckBit.Items.Add("None");
            me.comboBox_COMCheckBit.Items.Add("Odd");
            me.comboBox_COMCheckBit.Items.Add("Even");

            //数据位
            me.comboBox_COMDataBit.Items.Add("8");
            me.comboBox_COMStopBit.Items.Add("0");

            //停止位
            me.comboBox_COMStopBit.Items.Add("1");
            me.comboBox_COMStopBit.Items.Add("2");
            me.comboBox_COMStopBit.Items.Add("1.5");

            if( (me.comboBox_COMNumber.Items.Count > 0) 
             && (Properties.Settings.Default._com_num_select_index < me.comboBox_COMNumber.Items.Count))    //串口列表选用号
            {
                me.comboBox_COMNumber.SelectedIndex = Properties.Settings.Default._com_num_select_index;
            }
            else
            {
                me.comboBox_COMNumber.SelectedIndex = -1;
            }

            me.comboBox_COMBaudrate.SelectedIndex = Properties.Settings.Default._baudrate_select_index;
            me.comboBox_COMCheckBit.SelectedIndex = 0;
            me.comboBox_COMDataBit.SelectedIndex = 0;
            me.comboBox_COMStopBit.SelectedIndex = 1;

            serialport.DataReceived += Func_COM_DataRec;//指定串口接收函数
			serialport.ReadBufferSize = COM_BUFFER_SIZE_MAX;
			serialport.WriteBufferSize = COM_BUFFER_SIZE_MAX;

	        event_recv = new AutoResetEvent(false);
	        thread_recv = new Thread(ThreadEntry_ComRecv);
	        thread_recv.IsBackground = true;
	        thread_recv.Start();
        }
        
		void ThreadEntry_ComRecv()
		{
            int com_recv_length;
            bool thread_sleep = false;
			while(true)
			{
                lock(rcv_fifo.locker)
                {
                    com_recv_length = rcv_fifo.top - rcv_fifo.buttom;
                    if(com_recv_length == 0)
				    {
                        rcv_fifo.top = 0;
                        rcv_fifo.buttom = 0;
					    thread_sleep = true;
				    }
                }

                if(thread_sleep == true)
                {
                    thread_sleep = false;
                    event_recv.WaitOne();	//FIFO已经空了，则在这里一直等待，直到有事件过来，可以有效降低CPU的占用率
                }
                int max_handle_size = 4096;     //4096时最快，小于1024会很慢

                if(fm.fp.is_active == true)
                {
                    max_handle_size = 128;
                }

                byte[] raw_data_buffer;
                if(com_recv_length > 0)
                {
                    if(com_recv_length > max_handle_size)
                    {
                        raw_data_buffer = new byte[max_handle_size];
                    }
                    else
                    {
                        raw_data_buffer = new byte[com_recv_length];
                    }

                    for(int i = 0; i < raw_data_buffer.Length; i++)      //从buffer中取出数据
                    {
                        raw_data_buffer[i] = rcv_fifo.buffer[rcv_fifo.buttom];
                        rcv_fifo.buttom++;
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
                    if(fm.fp.is_active == true)
                    {
                        int recv_len;
                        byte[] recv_data;
                        recv_len = fm.fp.DataConvert(raw_data_buffer, raw_data_buffer.Length, out recv_data);
                        if(recv_len > 0)
                        {
                            DataHandle(recv_data, recv_len, true);
                        }
                    }
                    else
                    {
                        DataHandle(raw_data_buffer, raw_data_buffer.Length, true);
                    }                    
                }
			}
		}

        void Func_BakupStr_Add(string tag, string str)
        {
            //勾选了才使用bakup做备份
            if(cfg.backup_rcv_data == true)
            {
                txt.backup += "[" + tag + " clear @ " + DateTime.Now.ToString() + "]\r\n" + str;

                //大于1MB时，回滚保存
                txt.backup = Func.String_Roll(txt.backup, 1 * 1024 * 1024);
            }
        }

        private void TxtRcvUpdate(string text, int op)
        {
            efifo.Input(text, op);
            if(efifo.is_full == true)
            {
                MessageBox.Show("COM fifo is full!", "Error!");
            }
            event_txt_update.Set();
        }

        public void ClearRec()
        {
            if(txt.receive.Length < 32 * 1024 * 1024)
            {
                Func_BakupStr_Add("Rec", txt.receive);
            }

            txt.receive = "";

            TxtRcvUpdate("", TxtOP.CLEAR);

            record.rcv_bytes = 0;
            record.miss_data = 0;
            //GC.Collect();   //立即释放textBox_ComRec.Text，避免占用较大内存，其实不管也可以？
        }

        public void Rebulid_BaudrateList(ComboBox _comboBox_COMBaudrate)
        {
            _comboBox_COMBaudrate.Items.Clear();
            //波特率
            for(int i = 0; i < badurate_array.Length; i++)
            {
                if(badurate_array[i] == 1234567)
                {
                    if(cfg.custom_baudrate != 0)
                    {
                        _comboBox_COMBaudrate.Items.Add(cfg.custom_baudrate.ToString());
                    }
                    else
                    {
                        _comboBox_COMBaudrate.Items.Add("1234567");
                    }
                }
                else
                {
                    _comboBox_COMBaudrate.Items.Add(badurate_array[i].ToString());
                }
            }
        }


        public void Ruild_ComNumberList(ComboBox _comboBox_COMNumber)
        {
            _comboBox_COMNumber.Items.Clear();
            string[] strArr = Func.GetHarewareInfo(Func.HardwareEnum.Win32_PnPEntity, "Name");
            foreach(string vPortName in SerialPort.GetPortNames())
            {
                string SerialIn = "";
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
                _comboBox_COMNumber.Items.Add(SerialIn);                    //将设备列表里的COM放进下拉菜单上
            }
        }

        public const int BAUDRATE_WITH_SHOW = 91;
        public const int BAUDRATE_WITH_SELECT = 320;


        bool com_is_closing = false;
        string port_name_try;   //记录当前使用的COM的名字，由于是多线程访问，这个变量必须放在外面
        public void Close()
        {
            com_is_closing = true;
        
            /****************串口异常断开则直接关闭窗体 Start**************/
            int get_port_name_cnt = 0;
            while(true)
            {
                bool get_port_name_sta = false;

                try
                {
                    port_name_try = me.comboBox_COMNumber.SelectedItem.ToString();
                    get_port_name_sta = true;
                }
                catch(Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    get_port_name_cnt++;
                    if(get_port_name_cnt % 999 == 0)
                    {
                        Dbg.Assert(false, "###TODO: Why can not close COM" + ex.Message);//发生这种情况会怎么样...
                    }
                }

                if(get_port_name_sta == true)
                {
                    break;
                }
                //System.Threading.Thread.Sleep(10);
            }

            string PortName = port_name_try;
            int end = PortName.IndexOf(":");
            PortName = PortName.Substring(0, end);                      //截取获得COM口序号

            bool current_com_exist = false;

            string[] strArr = Func.GetHarewareInfo(Func.HardwareEnum.Win32_PnPEntity, "Name");
            foreach(string vPortName in SerialPort.GetPortNames())
            {
                if(vPortName == PortName)
                {
                    current_com_exist = true;                           //当前串口还在设备列表里
                }
            }

            int temp_select_index = me.comboBox_COMNumber.SelectedIndex;
            Ruild_ComNumberList(me.comboBox_COMNumber);
            me.comboBox_COMNumber.SelectedIndex = temp_select_index;

            //关闭串口时发现正在使用的COM不见了，由于无法调用com.close()，所以只能异常退出了
            if(current_com_exist == false)
            {
                com_is_closing = false;
                Dbg.Assert(false, "###TODO: Why can not close COM");
            }
            /****************串口异常断开则直接关闭窗体 End****************/

            try
            {
                serialport.Close();
            }
            catch(Exception ex)
            {
                com_is_closing = false;
                Dbg.Assert(false, "###TODO: Why can not close COM " + ex.Message);
            }

            com_is_closing = false;
        }

        public bool Open()
        {
            serialport.BaudRate = Convert.ToInt32(me.comboBox_COMBaudrate.SelectedItem.ToString());   //获得波特率
            switch(me.comboBox_COMCheckBit.SelectedItem.ToString())                                    //获得校验位
            {
                case "None": serialport.Parity = Parity.None; break;
                case "Odd": serialport.Parity = Parity.Odd; break;
                case "Even": serialport.Parity = Parity.Even; break;
                default: serialport.Parity = Parity.None; break;
            }
            serialport.DataBits = Convert.ToInt16(me.comboBox_COMDataBit.SelectedItem.ToString());    //获得数据位
            switch(me.comboBox_COMStopBit.SelectedItem.ToString())                                     //获得停止位
            {
                case "0": serialport.StopBits = StopBits.None; break;
                case "1": serialport.StopBits = StopBits.One; break;
                case "2": serialport.StopBits = StopBits.Two; break;
                case "1.5": serialport.StopBits = StopBits.OnePointFive; break;
                default: serialport.StopBits = StopBits.One; break;
            }

            if(me.comboBox_COMNumber.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose the COM port" + Dbg.GetStack(), "Attention!");
                return false;
            }

            string PortName = me.comboBox_COMNumber.SelectedItem.ToString();
            Console.WriteLine("Port name:{0}", PortName);
            int end = PortName.IndexOf(":");
            serialport.PortName = PortName.Substring(0, end);                  //获得串口数
            try
            {
                serialport.Open();
                serialport.DiscardInBuffer();
                serialport.DiscardOutBuffer();
            }
            catch(Exception ex)
            {
                //DebugIF.Assert(false, "###TODO: Why can not open COM " + ex.Message);
                MessageBox.Show(ex.Message + Dbg.GetStack(), "Attention!");
                return false;
            }
            
            return true;
        }
        

		byte last_byte = 0xFF;
		int LastLogFileTime = 0;
		bool recv_need_add_time = false;
        
        public void DataHandle(byte[] com_recv_buffer, int com_recv_buff_size, bool snd_to_tcp)
        {
            if(cfg.cmdline_mode == true)                                    //命令行处理时，需要把特殊符号去掉
            {
                txt.receive = cmdline.HandlerRecv(com_recv_buffer, com_recv_buff_size);

                TxtRcvUpdate(txt.receive, TxtOP.EQUAL);
            }

            string SerialIn = "";											//把接收到的数据转换为字符串放在这里
            
            if(cfg.ascii_rcv == false)		                                //十六进制接收，则需要转换为ASCII显示
            {
                for(int i = 0; i < com_recv_buff_size; i++)
                {
                    SerialIn += Func.GetHexHighLow(com_recv_buffer[i], 0);
                    SerialIn += Func.GetHexHighLow(com_recv_buffer[i], 1) + " ";
                }
            }
            else
            {
                if(com_recv_buffer[com_recv_buff_size - 1] == 0x00)         //发现接收数据的最后一个字符经常会是0x00，过滤掉
                {
                    com_recv_buff_size--;
                }
                if(com_recv_buff_size == 0)
                {
                    return;
                }

                if(com_recv_buffer[com_recv_buff_size - 1] == '\r')         //最后一个是'\r'也要去掉，否则textbox容易崩
                {
                    com_recv_buff_size--;
                }
                if(com_recv_buff_size == 0)
                {
                    return;
                }

                int i;
                for(i = 0; i < com_recv_buff_size; i++)
                {
                    byte current_byte = com_recv_buffer[i];

                    if((current_byte == '\n') && (last_byte != '\r'))       //只有'\n'没有'\r'，则追加进去
                    {
                        byte add_byte = (byte)'\r';
                        SerialIn += Func.Byte_To_String(add_byte);         //System.Text.Encoding.ASCII.GetString(arrayx)
                    }
                    last_byte = current_byte;

                    if((current_byte == 0x00) || (current_byte > 0x7F))     //收到非ASCII码要显示一下
                    {
                        if (cfg.fliter_ileagal_char == false)
                        {
                            SerialIn += " ~";

                            SerialIn += Func.GetHexHighLow(current_byte, 0);
                            SerialIn += Func.GetHexHighLow(current_byte, 1);

                            SerialIn += "~ ";
                        }
                    }
                    else
                    {
                        if(Properties.Settings.Default._add_Time == 0)		//不加时间戳
                        {
                            //SerialIn = System.Text.Encoding.ASCII.GetString(com_recv_buffer, 0, com_recv_buff_size);
                            //SerialIn += System.Text.Encoding.ASCII.GetString(array);
                            SerialIn += Func.Byte_To_String(current_byte);
                        }
                        else if(Properties.Settings.Default._add_Time == 1) //时间戳加在前面
                        {
                            if(recv_need_add_time == true)
                            {
                                recv_need_add_time = false;
                                SerialIn += "[" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff") + "]";
                            }

                            //SerialIn += System.Text.Encoding.ASCII.GetString(array);
                            SerialIn += Func.Byte_To_String(current_byte);

                            if(com_recv_buffer[i] == '\n')
                            {
                                recv_need_add_time = true;
                            }
                        }
                        else                                            //时间戳加在后面
                        {
                            //SerialIn += System.Text.Encoding.ASCII.GetString(array);
                            SerialIn += Func.Byte_To_String(current_byte);

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
                StreamWriter sw_log_file = File.AppendText(log_file_name);  //在目标文件原有内容后面追加内容

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
            
            record.rcv_bytes += (uint)com_recv_buff_size;

            if(SerialIn.Length > 0)
            {
                if(cfg.cursor_fixed == true)
                {
                    txt.temp += SerialIn;
                }
                else
                {
                    txt.receive += SerialIn;                                //在接收文本中添加串口接收数据

                    TxtRcvUpdate(SerialIn, TxtOP.ADD);
                }
            }

            if((SerialIn.Length > 0) && (fm.etcp.is_active == true) && (snd_to_tcp == true))
            {
                fm.etcp.SendData(Encoding.ASCII.GetBytes(SerialIn));		//串口接收到的数据，发送给网络端
            }
        }

        void Func_COM_DataRec(object sender, SerialDataReceivedEventArgs e)  //串口接受函数
        {
			if((com_is_closing == true) || (serialport.IsOpen == false))
			{
				return;
            }

            int com_recv_top_real;
            lock(rcv_fifo.locker)
            {
                com_recv_top_real = rcv_fifo.top;
            }

            if(com_recv_top_real > rcv_fifo.buffer.Length - COM_BUFFER_SIZE_MAX*2)   //还剩最后一点点了
            {   
                record.miss_data += (uint)serialport.ReadBufferSize;

                Console.WriteLine("###com:{0} recv buffer is full:{1} {2}, data miss!!!", 
                    serialport.IsOpen, com_recv_top_real, rcv_fifo.top);

                return;
            }

            int com_recv_buff_size;

            lock(rcv_fifo.locker)
            {
                com_recv_buff_size = serialport.Read(rcv_fifo.buffer, rcv_fifo.top, serialport.ReadBufferSize);
                rcv_fifo.top += com_recv_buff_size;
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
					Console.Write(" {0:X}", rcv_fifo.buffer[v]);
				}
				Console.Write("\r\n");
			#endif

            event_recv.Set();						        //唤醒recv线程去取queue
        }
        
        public void ClearSnd()
        {   
            Func_BakupStr_Add("Snd", txt.send);
            
            txt.send = "";
            record.snd_bytes = 0;

            cfg.auto_send = false;
            timer_AutoSnd.Enabled = false;
        }

        public void Send()
        {
            if(fm.etcp.is_active == true) //网络连接上了
            {
                fm.etcp.SendData(Encoding.ASCII.GetBytes(txt.send));
                return;
            }

            const uint max_recv_length = 65535;

            if(txt.send.Length == 0)
            {
                MessageBox.Show("Please input data" + Dbg.GetStack(), "Warning!");
                return;
            }

            if(txt.send.Length > max_recv_length)
            {
                MessageBox.Show("Data too long" + Dbg.GetStack(), "Warning!");
                return;
            }
			
            if(cfg.ascii_snd == true)			                            //ASCII发送
            {
                try
                {
                    //string ss = textBox_COM.Text.Trim();
                    serialport.Write(txt.send);
                    record.snd_bytes += (uint)txt.send.Length;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + Dbg.GetStack(), "Warning!");
                }
            }
            else//16进制发送
            {
                //转换16进制的string成byte[]
                byte[] bb = new byte[max_recv_length];
                string com_send_text = txt.send;
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
                        MessageBox.Show("Error input format!" + Dbg.GetStack(), "Warning!");
                        return;
                    }
                }
                /*	需要检查输入的合法性		*/
	
                for(int i = 2; i < n; i++)//找出所有空格
                { //0x3F
                    if(chahArray[i] == ' ')
                    {
						uint hex_h = Func.CharToByte(chahArray[i - 2]);//3
						uint hex_l = Func.CharToByte(chahArray[i - 1]);//F	
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
                    serialport.Write(bb, 0, length_bb);
                    record.snd_bytes += (uint)length_bb;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + Dbg.GetStack(), "Warning!");
                }
            }
        }



        DateTime date_time_com_recv_mark;
        uint com_recv_cnt_last = 0;
        uint com_recv_cnt_mark = 0;
        public void Display()
        {
            record.buffer_left = (uint)(rcv_fifo.top - rcv_fifo.buttom);            
            
            if(com_recv_cnt_last != record.rcv_bytes)
            {
                if((record.miss_data == 0) && (com_recv_cnt_mark != 0))
                {
                    TimeSpan span_com_recv = DateTime.Now - date_time_com_recv_mark;
                    uint delta = record.rcv_bytes - com_recv_cnt_mark;
                    if(span_com_recv.Seconds > 0)
                    {
                        record.miss_data = (uint)(delta / span_com_recv.Seconds);
                    }
                }                
                
                if(cfg.limiet_rcv_lenght == true)					        //限定接收文本的长度,防止logfile接收太多东西，KCOM死掉
                {
                    int max_recv_size = 16 * 1024 * 1024;   //32MB  64 * 1024 * 1024                   
                    
                    if(txt.receive.Length >= max_recv_size)
                    {
                        #if false   //回滚式地限定长度
                            textBox_ComRec.Text = _func.String_Roll(textBox_ComRec.Text, max_recv_size);
                        #elif false //直接清空
                            textBox_ComRec.Text = "[KCOM: reset the recv text!]\r\n";
                        #else       //放到垃圾桶上
                            txt.backup = txt.receive;
                            txt.receive = "[KCOM: reset the recv text!]\r\n";

                            TxtRcvUpdate(txt.receive, TxtOP.EQUAL);
#endif
                    }
                }

                com_recv_cnt_last = record.rcv_bytes;
            }
        }

        public void CalSpeed()
        {
            if(com_recv_cnt_mark == 0)      //启动速度计算
            {
                com_recv_cnt_mark = record.rcv_bytes;
                date_time_com_recv_mark = DateTime.Now;
            }
            else                            //停止
            {
                com_recv_cnt_mark = 0;
            }
        }

        void timer_AutoSnd_Tick(object sender, EventArgs e)
        {
            Send();
        }
    }
}
