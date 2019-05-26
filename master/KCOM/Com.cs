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
        const int MAX_RECV_TEXT_LENGTH = 8 * 1024 * 1024;   //保存最后8MB数据

        public class tyRecord
        {
            public int speed_cnt = 0;
            public int speed_sum = 0;

            public uint rcv_bytes = 0;
            public uint rcv_mark = 0;
            public uint show_bytes = 0;
            public uint miss_data = 0;
            public uint snd_bytes = 0;
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

        public struct tyExtResource
        {
            public eTCP etcp;
            public FastPrint fp;
        }

        public Cmdline cmdline = new Cmdline();
        public tyExtResource fm = new tyExtResource();
        public tyRecord record = new tyRecord();
        public SerialPort serialport = new SerialPort();

        public string log_file_name = null;

        public class tyRcvNode
        {
            //改变该值可以改变现实的平滑性，但是越小速度会越低
            public const int RCV_CACHE_SIZE = COM_BUFFER_SIZE_MAX;//COM_BUFFER_SIZE_MAX * 2
            public const int RCV_NODE_NUM = 8 * 1024;//8k * 4k = 32MB的缓存, 8 * 1024

            public byte[] buffer;
            public int length;
            public PNode<tyRcvNode> pnode;

#if false
            public int[] log_sz = new int[128];
            public int[] log_len = new int[128];
            public int log_cnt = 0;
#endif

            public tyRcvNode(int max_sz)
            {
                buffer = new byte[max_sz];
                length = 0;
            }
        }

        public class tyShowOp
        {
            public const int SHOW_NODE_NUM = 8 * 1024;//8 * 1024

            public const int NULL = 0;
            public const int CLEAR = 1;
            public const int ADD = 2;
            public const int EQUAL = 3;
            public const int APPEND = 4;

            public int op;
            public string text;
            public PNode<tyShowOp> pnode;

            public tyShowOp()
            {
                op = NULL;
                text = "";
            }
        }
        
        public AutoResetEvent event_txt_update = new AutoResetEvent(false);
        public Thread thread_txt_update;
        public eFIFO<tyShowOp> efifo_str_2_show = new eFIFO<tyShowOp>();
        public ePool<tyShowOp> epool_show = new ePool<tyShowOp>();

        private Thread thread_recv;
        private AutoResetEvent event_recv;
        public System.Timers.Timer timer_AutoSnd;
        private System.Timers.Timer timer_RcvFlush;
        private bool rcv_flushing = false;
        private bool rcv_recving = false;
        private tyRcvNode current_rnode = null;
        public eFIFO<tyRcvNode> efifo_raw_2_str = new eFIFO<tyRcvNode>();
        private ePool<tyRcvNode> epool_rcv = new ePool<tyRcvNode>();
        private int handle_data_thresdhold = 0;

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
            efifo_raw_2_str.Init(tyRcvNode.RCV_NODE_NUM);                   //eFIFO能管理8K个元素
            for(int i = 0; i < tyRcvNode.RCV_NODE_NUM; i++)                 //每个元素8K大小，一共64MB，如果收的比做得快，那只能丢失了
            {
                //第一次收到10个，不满攒住，第二次收到4096，则会溢出！
                tyRcvNode rnode = new tyRcvNode(tyRcvNode.RCV_CACHE_SIZE + COM_BUFFER_SIZE_MAX);
                PNode<tyRcvNode> pnode = new PNode<tyRcvNode>();
                
                rnode.pnode = pnode;
                epool_rcv.Add(pnode, rnode);
                //Console.WriteLine("Add node:{0} to ePool", rnode.GetHashCode());
            }

            efifo_str_2_show.Init(tyShowOp.SHOW_NODE_NUM);                  //上面采用eFIFO搬运
            for(int i = 0; i < tyShowOp.SHOW_NODE_NUM; i++)
            {
                tyShowOp snode = new tyShowOp();
                PNode<tyShowOp> pnode = new PNode<tyShowOp>();

                snode.pnode = pnode;                
                epool_show.Add(pnode, snode);
                //Console.WriteLine("Add node:{0} to ePool", rnode.GetHashCode());
            }

            timer_AutoSnd = new System.Timers.Timer();                                          //实例化Timer类，设置间隔时间为1000毫秒
            timer_AutoSnd.Elapsed += new System.Timers.ElapsedEventHandler(timer_AutoSnd_Tick); //到达时间的时候执行事件
            timer_AutoSnd.AutoReset = true;                                                     //设置是执行一次（false）还是一直执行(true)
            timer_AutoSnd.Enabled = false;                                                      //是否执行System.Timers.Timer.Elapsed事件
            timer_AutoSnd.Interval = cfg.auto_send_inverval_100ms * 100;

            timer_RcvFlush = new System.Timers.Timer();
            timer_RcvFlush.Elapsed += new System.Timers.ElapsedEventHandler(timer_RcvFlush_Tick);
            timer_RcvFlush.AutoReset = false;
            timer_RcvFlush.Enabled = false;
            timer_RcvFlush.Interval = 500;
        }

        public void Init(bool _cmdline_mode, bool _ascii_rcv, bool _ascii_snd, bool _fliter_ileagal_char, int custom_baudrate)
        {
            cfg.custom_baudrate = custom_baudrate;

            cfg.cmdline_mode = _cmdline_mode;
            cfg.ascii_rcv = _ascii_rcv;
            cfg.ascii_snd = _ascii_snd;

            cfg.fliter_ileagal_char = _fliter_ileagal_char;
            
            serialport.DataReceived += ISR_COM_DataRec;//指定串口接收函数
			serialport.ReadBufferSize = COM_BUFFER_SIZE_MAX;
			serialport.WriteBufferSize = COM_BUFFER_SIZE_MAX;

            event_recv = new AutoResetEvent(false);
	        thread_recv = new Thread(ThreadEntry_ComRecv);
	        thread_recv.IsBackground = true;
	        thread_recv.Start();
        }

        public uint check_thread_ComRecv = 0;
        public uint step_thread_ComRecv = 0;
        void ThreadEntry_ComRecv()
		{
			while(true)
			{
                check_thread_ComRecv++;
                step_thread_ComRecv = 0;
                if(efifo_raw_2_str.GetValidNum() == 0)
                {
                    step_thread_ComRecv = 1;
                    event_recv.WaitOne(1000);	    //FIFO已经空了，则在这里一直等待，直到有事件过来，可以有效降低CPU的占用率
                }
                else
                {
                    step_thread_ComRecv = 2;
                    tyRcvNode output_rnode = efifo_raw_2_str.Output();

                    if(output_rnode.length > 0)
                    {
                        step_thread_ComRecv = 3;
#if false  //false, true
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
                            step_thread_ComRecv = 4;
                            int recv_len;
                            byte[] recv_data;
                            recv_len = fm.fp.DataConvert(output_rnode.buffer, output_rnode.length, out recv_data);
                            if(recv_len > 0)
                            {
                                DataHandle(recv_data, recv_len, true);
                            }
                        }
                        else
                        {
                            step_thread_ComRecv = 5;
                            DataHandle(output_rnode.buffer, output_rnode.length, true);
                        }
                    }
                    else
                    {
                        step_thread_ComRecv = 6;
                        Dbg.Assert(false, "###why output data is zero length?");
                    }

                    step_thread_ComRecv = 7;
                    epool_rcv.Put(output_rnode.pnode);
                }
                step_thread_ComRecv = 8;
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

        public void Update_TextBox(string text, int op)
        {
            PNode<tyShowOp> pnode = epool_show.Get();

            tyShowOp show_node = pnode.obj;
            show_node.op = op;
            show_node.text = text;

            efifo_str_2_show.Input(show_node);
            event_txt_update.Set();
        }

        public void ClearRec()
        {
            if(txt.receive.Length < 32 * 1024 * 1024)
            {
                Func_BakupStr_Add("Rec", txt.receive);
            }

            Update_TextBox("", tyShowOp.CLEAR);

            txt.receive = "";

            record.speed_cnt = 0;
            record.speed_sum = 0;
            record.rcv_mark = 0;

            record.rcv_bytes = 0;
            record.show_bytes = 0;
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

        bool com_is_closing = false;
        public void Close(SerialPort sp)
        {
            com_is_closing = true;
        
            /****************串口异常断开则直接关闭窗体 Start**************/
            bool current_com_exist = false;
            string[] strArr = Func.GetHarewareInfo(Func.HardwareEnum.Win32_PnPEntity, "Name");
            foreach(string vPortName in SerialPort.GetPortNames())
            {
                if(vPortName == sp.PortName)
                {
                    current_com_exist = true;                               //当前串口还在设备列表里
                }
            }

            //关闭串口时发现正在使用的COM不见了，由于无法调用com.close()，所以只能异常退出了
            if(current_com_exist == false)
            {
                com_is_closing = false;
                //Dbg.Assert(false, "###TODO: Why can not close COM");
            }
            else
            {
                Console.WriteLine("COM is still here");
            }
            /****************串口异常断开则直接关闭窗体 End****************/

            try
            {
                sp.Close();
                Console.WriteLine("COM close ok");
            }
            catch(Exception ex)
            {
                com_is_closing = false;
                Dbg.Assert(false, "###TODO: Why can not close COM " + ex.Message);
            }

            com_is_closing = false;
        }

        public bool Open(SerialPort sp)
        {
            Console.WriteLine("PortName:{0}", sp.PortName);
            Console.WriteLine("Baudrate:{0}", sp.BaudRate);
            Console.WriteLine("Parity:{0}", sp.Parity);
            Console.WriteLine("Data:{0}", sp.DataBits);
            Console.WriteLine("Stop:{0}", sp.StopBits);

            if( (sp.PortName == "null") ||
                (sp.BaudRate == 1) ||
                (sp.Parity == Parity.Space) ||
                (sp.DataBits == 1))
            {
                MessageBox.Show("Please choose the COM port" + Dbg.GetStack(), "Attention!");
                return false;
            }

            try
            {
                sp.Open();
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
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
        
        public void DataHandle(byte[] com_recv_buffer, int com_recv_buff_size, bool send_to_tcp)
        {
            if(cfg.cmdline_mode == true)                                    //命令行处理时，需要把特殊符号去掉
            {
                cmdline.HandlerRecv(com_recv_buffer, com_recv_buff_size, ref txt.receive);

                Update_TextBox(txt.receive, tyShowOp.EQUAL);
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

            if(SerialIn.Length > 0)
            {
                record.rcv_bytes += (uint)SerialIn.Length;

                if(cfg.cursor_fixed == true)
                {
                    txt.temp += SerialIn;
                }
                else
                {
                    txt.receive += SerialIn;                                //在接收文本中添加串口接收数据
                    if(cfg.limiet_rcv_lenght == true)                       //限定接收文本的长度,防止logfile接收太多东西，KCOM死掉
                    {
                        if(txt.receive.Length >= MAX_RECV_TEXT_LENGTH)
                        {
#if false   //回滚式地限定长度
                            textBox_ComRec.Text = _func.String_Roll(textBox_ComRec.Text, MAX_RECV_TEXT_LENGTH);
#elif false //直接清空
                            textBox_ComRec.Text = "[KCOM: reset the recv text!]\r\n";
#else       //放到垃圾桶上
                            txt.backup = txt.receive;
                            txt.receive = "[KCOM: reset the recv text!]\r\n";
                            Update_TextBox(txt.receive, tyShowOp.EQUAL);
#endif
                        }
                    }

                    Update_TextBox(SerialIn, tyShowOp.ADD);
                }
            }

            if((SerialIn.Length > 0) && (fm.etcp.is_active == true) && (send_to_tcp == true))
            {
                fm.etcp.SendData(Encoding.ASCII.GetBytes(SerialIn));		//串口接收到的数据，发送给网络端
            }
        }
        
        //临时读取一下串口数据，统计在丢失数据里
        void UpdateMissData()
        {
            byte[] temp_buffer = new byte[COM_BUFFER_SIZE_MAX];
            int temp_length;
            temp_length = serialport.Read(temp_buffer, 0, serialport.ReadBufferSize);
            record.miss_data += (uint)temp_length;
        }
        

        DateTime last_rcv_data_time;
        DateTime bbbb;
        void ISR_COM_DataRec(object sender, SerialDataReceivedEventArgs e)  //串口接受函数
        {
            rcv_recving = true;
            last_rcv_data_time = DateTime.Now;

            timer_RcvFlush.Stop();
            timer_RcvFlush.Enabled = false;//timer重新计时
            timer_RcvFlush.Enabled = true;

            if((com_is_closing == true) || (serialport.IsOpen == false) || (rcv_flushing == true))
			{
                rcv_recving = false;
                return;
            }

            event_recv.Set();                                               //无论有没有资源，都唤醒recv线程去取FIFO

            //如果FIFO已经满了，最后一个current_rnode会一直接一直接，然后突破了buffer的长度
            if(efifo_raw_2_str.is_full == true)
            {
                Console.WriteLine("###1.COM:{0} recv fifo is full:{1}, data miss!!!",
                    serialport.IsOpen, efifo_raw_2_str.GetValidNum());

                UpdateMissData();

                rcv_recving = false;
                return;
            }

            if(current_rnode == null)
            {
                PNode<tyRcvNode> pnode = epool_rcv.Get();
                if(pnode == null)
                {
                    Console.WriteLine("###COM:{0} recv pool is full:{1}({2}), data miss!!!", 
                        serialport.IsOpen, epool_rcv.nr_got, epool_rcv.nr_ent);

                    UpdateMissData();

                    rcv_recving = false;
                    return;
                }
                else
                {
                    current_rnode = pnode.obj;
                }

                current_rnode = pnode.obj;
                current_rnode.length = 0;               //把长度清零，避免长度越界
            }

            int com_recv_buff_length = serialport.Read(current_rnode.buffer, current_rnode.length, serialport.ReadBufferSize);
            if(com_recv_buff_length > 0)
            {
                bbbb = DateTime.Now;

                current_rnode.length += com_recv_buff_length;

#if false
                current_rnode.log_len[current_rnode.log_cnt % 128] = com_recv_buff_length;
                current_rnode.log_sz[current_rnode.log_cnt % 128] = current_rnode.length;
                current_rnode.log_cnt++;
#endif
                //当缓存较多的时候，优先提高平滑性(收得太慢/显示得太慢)
                if((epool_rcv.nr_got < epool_rcv.nr_ent / 128) &&       //64
                    (epool_show.nr_got < epool_show.nr_ent / 128))
                {
                    handle_data_thresdhold = 0;
                }
                else if((epool_rcv.nr_got < epool_rcv.nr_ent / 64) &&   //128
                        (epool_show.nr_got < epool_show.nr_ent / 64))
                {
                    handle_data_thresdhold = 1024;
                }
                else if((epool_rcv.nr_got < epool_rcv.nr_ent / 32) ||  //256
                        (epool_show.nr_got < epool_show.nr_ent / 32))
                {
                    handle_data_thresdhold = tyRcvNode.RCV_CACHE_SIZE;
                }

                if(current_rnode.length >= handle_data_thresdhold)
                {
                    if(efifo_raw_2_str.is_full == true)
                    {
                        Console.WriteLine("###2.COM:{0} recv fifo is full:{1}, data miss!!!",
                            serialport.IsOpen, efifo_raw_2_str.GetValidNum());
                        record.miss_data += (uint)current_rnode.length;

                        rcv_recving = false;
                        return;
                    }

                    efifo_raw_2_str.Input(current_rnode);
                    current_rnode = null;
                }
                else
                {
                    rcv_recving = false;
                    return;
                }

                event_recv.Set();
#if false
				Console.Write("RECA[{0}]: in:{1}-{2} out:{3}-{4}", com_recv_buff_length,
					com_recv_fifo_top, com_recv_fifo_buttom, fp_out_top, fp_out_buttom);

				for(int v = 0; v < com_recv_buff_length; v++)
				{
					Console.Write(" {0:X}", rcv_fifo.buffer[v]);
				}
				Console.Write("\r\n");
#endif

                rcv_recving = false;
            }
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
        

        public void Display(Label _label_Rec_Bytes, Label _label_BufferLeft, Label _label_MissData, 
            Label _label_Send_Bytes, Label _label_Speed, int interval)
        {
            //计算speed的数据
            int delta = (int)(record.rcv_bytes - record.rcv_mark);
            int speed_bytes_per_second = delta * 1000 / interval;
            int speed_KB_per_second = speed_bytes_per_second / 1024;

            int speed_avg = 0;
            if(speed_KB_per_second > 0)
            {
                record.speed_cnt++;
                record.speed_sum += speed_KB_per_second;
                speed_avg = record.speed_sum / record.speed_cnt;
            }

            //Console.WriteLine("Rcv:{0}", record.rcv_bytes);
            //Console.WriteLine("Show:{0}", record.show_bytes);
            //Console.WriteLine("Miss:{0}", record.miss_data);
            //Console.WriteLine("Sent:{0}", record.snd_bytes);

            record.rcv_mark = record.rcv_bytes;

            _label_Rec_Bytes.Text = "Received: " + record.rcv_bytes.ToString();
            _label_BufferLeft.Text = "Remain: " + (record.rcv_bytes - record.show_bytes).ToString();
            _label_MissData.Text = "Miss: " + record.miss_data.ToString();            
            _label_Speed.Text = "Speed: " + speed_KB_per_second.ToString() + "(" 
                + speed_avg.ToString() + ")" + "KB/S";
            _label_Send_Bytes.Text = "Sent: " + record.snd_bytes.ToString();

            event_txt_update.Set();
        }

        void timer_AutoSnd_Tick(object sender, EventArgs e)
        {
            Send();
        }

        void timer_RcvFlush_Tick(object sender, EventArgs e)
        {
            timer_RcvFlush.Enabled = false;
                        
            //空闲的时候把最后的数据刷到窗体上
            if((current_rnode != null) && (rcv_recving == false))
            {
                rcv_flushing = true;
                
                Console.WriteLine("Flush rcv data:{0} rcv:{1} sp:{2}", current_rnode.length, rcv_recving,
                    Func.RTC_TimeSpan_MS(last_rcv_data_time));

                if(current_rnode.length > 0)
                {
                    if(efifo_raw_2_str.is_full == true)
                    {
                        Console.WriteLine("###3.COM:{0} recv fifo is full:{1}, data miss!!!",
                            serialport.IsOpen, efifo_raw_2_str.GetValidNum());
                        return;
                    }
                                        
                    efifo_raw_2_str.Input(current_rnode);
                    current_rnode = null;

                    event_recv.Set();
                }

                rcv_flushing = false;
            }
        }

        public void ShowDebugInfo()
        {
            Dbg.WriteLine("****************Dump KCOM status(%):********************", DateTime.Now.ToString("yy/MM/dd HH:mm:ss"));
            Dbg.WriteLine("check_thread_txtupdate:% Step:% Active:%", Form_Main.check_thread_txtupdate, Form_Main.step_thread_txtupdate, thread_txt_update.IsAlive);
            Dbg.WriteLine("epool_show.got:%", epool_show.nr_got);
            Dbg.WriteLine("eFIFO_str_2_show. Top:% Bottom:% Full:%", efifo_str_2_show.top, efifo_str_2_show.bottom, efifo_str_2_show.is_full);
            Dbg.WriteLine("check_thread_ComRecv:% Step:% Active:%", check_thread_ComRecv, step_thread_ComRecv, thread_recv.IsAlive);
            Dbg.WriteLine("epool_rcv.got:%", epool_rcv.nr_got);
            Dbg.WriteLine("efifo_raw_2_str. Top:% Bottom:% Full:%", efifo_raw_2_str.top, efifo_raw_2_str.bottom, efifo_raw_2_str.is_full);
            Dbg.WriteLine("*****************************************************");
            Dbg.WriteLine("");
        }
    }
}
