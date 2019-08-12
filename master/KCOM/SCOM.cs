using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCOM
{
    class SCOM
    {
        public static bool run_ecmd = false;

        public SerialPort serialport = new SerialPort();        

        const int COM_BUFFER_SIZE_MAX = 4096;
        const int MAX_RECV_TEXT_LENGTH = 8 * 1024 * 1024;   //保存最后8MB数据

        private System.Timers.Timer timer_RcvTO;

        public delegate void tyDelegate_Update_TextBox(string text, int op);
        private tyDelegate_Update_TextBox Update_TextBox;
        private SerialPort main_port;

        public void Init(SerialPort _main_port, tyDelegate_Update_TextBox _Update_TextBox)
        {
            serialport.DataReceived += ISR_COMSync_DataRec;
            serialport.ReadBufferSize = COM_BUFFER_SIZE_MAX;
            serialport.WriteBufferSize = COM_BUFFER_SIZE_MAX;

            timer_RcvTO = new System.Timers.Timer();                                            //实例化Timer类，设置间隔时间为1000毫秒
            timer_RcvTO.Elapsed += new System.Timers.ElapsedEventHandler(timer_RcvTO_Tick);     //到达时间的时候执行事件
            timer_RcvTO.AutoReset = false;                                                      //设置是执行一次（false）还是一直执行(true)
            timer_RcvTO.Enabled = false;                                                        //是否执行System.Timers.Timer.Elapsed事件
            timer_RcvTO.Interval = 32;

            Update_TextBox = _Update_TextBox;

            main_port = _main_port;
        }

        void RcvData_Handle(byte[] buffer, int length)
        {
            string str = System.Text.Encoding.ASCII.GetString(sync_rcv_buffer, 0, sync_rcv_count);

            Dbg.WriteLine("SyncRcv handle. Len:% Str:%\n", sync_rcv_count, str);

            string append_str = "";

            append_str += "[" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff") + "]";
            append_str += "SyncCOM Rcv: " + sync_rcv_count.ToString() + "\r\n";
            append_str += str;
            append_str += "\r\n\r\n";

            Update_TextBox(append_str, COM.tyShowOp.APPEND);

            if(SCOM.run_ecmd == false)
            {
                if(main_port.IsOpen == true)                                //通过串口1透传出去
                {
                    try
                    {
                        main_port.Write(sync_rcv_buffer, 0, sync_rcv_count);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message + Dbg.GetStack(), "#Sync send data error!");
                    }
                }
            }
            else if(str == RunEXE.str_run_exe_code)
            {
                RunEXE.Run_EXE();
            }
            else
            {
                string ext_name = Func.Get_ExternName(str);
                if( (ext_name != null) && 
                    (Func.Char_String_compare(ext_name.ToCharArray(), "cmd", 3) == true) )
                {
                    string str_cmd = Func.Get_FileName(str);

                    Dbg.WriteLine("ext name:%", ext_name);
                    Dbg.WriteLine("file name:%", str_cmd);
                    
                    Update_TextBox("Run CMD\r\n", COM.tyShowOp.APPEND);

                    str = RunEXE.RunCMD(str_cmd);

                    Update_TextBox(str, COM.tyShowOp.APPEND);
                }
            }
        }

        void timer_RcvTO_Tick(object sender, EventArgs e)
        {
#if false
            for(int i = 0; i < sync_rcv_count; i++)
            {
                Dbg.WriteLine("[%]:%", i, sync_rcv_buffer[i]);
            }
#endif

            RcvData_Handle(sync_rcv_buffer, sync_rcv_count);

            sync_rcv_count = 0;
        }


        byte[] sync_rcv_buffer = new byte[MAX_RECV_TEXT_LENGTH];
        int sync_rcv_count = 0;
        void ISR_COMSync_DataRec(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] sync_cache_buffer = new byte[COM_BUFFER_SIZE_MAX];
            int com_recv_buff_length = serialport.Read(sync_cache_buffer, 0, serialport.ReadBufferSize);

            for(int i = 0; i < com_recv_buff_length; i++)
            {
                sync_rcv_buffer[sync_rcv_count] = sync_cache_buffer[i];
                sync_rcv_count++;
            }

            //复位接收超时定时器
            timer_RcvTO.Stop();
            timer_RcvTO.Enabled = false;
            timer_RcvTO.Enabled = true;
        }
    }
}
