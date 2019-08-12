
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;     //使用线程
using System.IO.Pipes;
using System.Security.Principal;
using System.Diagnostics;	//使用Rrocess外部EXE
using System.Windows.Forms;
using System.Runtime.InteropServices;//使用外部dll

//为变量定义别名
using u64 = System.UInt64;  //ulong
using u32 = System.UInt32;  //uint
using u16 = System.UInt16;  //ushort
using u8 = System.Byte;     //byte
using s64 = System.Int64;   //long
using s32 = System.Int32;   //int
using s16 = System.Int16;   //ushort
using s8 = System.SByte;    //byte


namespace KCOM
{
	class FastPrint
	{
        public bool is_active = false;

        DateTime last_hex_time;
        
        public string hex0_path = null;
        public string hex1_path = null;

        unsafe byte* input_data;
        unsafe byte* output_data;

        /*********************引用FastPrintf dll Start**********************/
        const int MEMORY_MAX_BANK = 64;
        const int MEMORY_BANK_SIZE = 65536;

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct memory_bank
        {
            public byte* p;
            public u32 pSize;

            public u32 Address;
            public u32 BinSz;
        }
        

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct memory_desc
        {
            public memory_bank* bank;

            public u32 bank_max;                       //记录mount内存段的数量
            public u32 bank_used;                      //当前用到的第几段
            public byte* pBuff;
            public u32 bin_start_execution;            //bin文件起始执行地址
        }
        [DllImport("FastPrintf.dll", EntryPoint = "StringConvert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe static extern u32 StringConvert(byte* pSrc, u32 dwLen, byte* pOutStr, ref memory_desc mi_fa, ref memory_desc mi_fb);

        [DllImport("FastPrintf.dll", EntryPoint = "hex2bin_mount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void hex2bin_mount(ref memory_desc mi);

        [DllImport("FastPrintf.dll", EntryPoint = "hex2bin_read_hex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int hex2bin_read_hex(char[] file_name, ref memory_desc mi);

        memory_desc mi_fa;
        memory_desc mi_fb;
        /*********************引用FastPrintf dll End************************/

        public void Enter_MessageQueue(string str)
        {
            Dbg.queue_message.Enqueue(str);
        }

        public void Init(string str_fp_hex0_path, string str_fp_hex1_path)
		{ 
            hex0_path = str_fp_hex0_path;
            hex1_path = str_fp_hex1_path;
            
            Dbg.WriteLine("HEX0:{0}", hex0_path);
            Dbg.WriteLine("HEX1:{0}", hex1_path);
            
            Form_Main.main_form.button_FPSelect_HEX.Text = "";
            Form_Main.main_form.button_FPSelect_HEX.Text += "FP HEX0 path: " + hex0_path;
            Form_Main.main_form.button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + hex1_path;

#if false   //通过调用批处理去删除dll!
            try     //不使用FastPrintf的话就把dll删掉，c#没办法调用静态库，又不能卸载dll，所以做不到关闭窗体时删除dll，比较蛋疼
            {
                File.SetAttributes(fast_printf_dll_address, FileAttributes.Normal);
                File.Delete(fast_printf_dll_address);
            }
            //catch(Exception ex)
            //{
            //    Enter_MessageQueue(ex.Message + "   FastPrintf dll clears!!!");
            //}
            catch
            {
                Enter_MessageQueue("FastPrintf.dll clear failed");
            }
#endif
        }


        string fast_printf_dll_address = @".\FastPrintf.dll";
        unsafe public void Close()
		{
            Marshal.FreeHGlobal((IntPtr)mi_fa.bank);
            Marshal.FreeHGlobal((IntPtr)mi_fb.bank);
            Marshal.FreeHGlobal((IntPtr)mi_fa.pBuff);
            Marshal.FreeHGlobal((IntPtr)mi_fb.pBuff);

            Marshal.FreeHGlobal((IntPtr)input_data);
            Marshal.FreeHGlobal((IntPtr)output_data);

            is_active = false;
        }
        
        unsafe public bool Start()
        {
            string cpu0_hex_path = hex0_path;
            string cpu1_hex_path = hex1_path;
            if((File.Exists(@cpu0_hex_path) == false) || (File.Exists(@cpu1_hex_path) == false))
            {
                MessageBox.Show("HEX not exist!   " + cpu0_hex_path + "   " + cpu1_hex_path + Dbg.GetStack(), "Warning!");
                return false;
            }

            FileInfo fi = new FileInfo(cpu0_hex_path);
            last_hex_time = fi.LastWriteTime;                               //记录上一次打开HEX的时间
            Dbg.WriteLine("First. Create:" + fi.CreationTime.ToString() + "  Write:" + fi.LastWriteTime + "  Access:" + fi.LastAccessTime);

            char[] pwd_hex0 = hex0_path.ToCharArray();
            char[] pwd_hex1 = hex1_path.ToCharArray();

            Dbg.WriteLine("pwd_hex0:{0}", hex0_path);
            Dbg.WriteLine("pwd_hex1:{0}", hex1_path);

            //将内嵌的资源释放到临时目录下
            if(File.Exists(fast_printf_dll_address) == false)
            {
                FileStream str = new FileStream(fast_printf_dll_address, FileMode.OpenOrCreate);
                str.Write(Properties.Resources.FastPrintf, 0, Properties.Resources.FastPrintf.Length);
                str.Close();
            }

            mi_fa = new memory_desc();
            mi_fb = new memory_desc();

            mi_fa.pBuff = (byte*)Marshal.AllocHGlobal(MEMORY_MAX_BANK * MEMORY_BANK_SIZE);
            mi_fb.pBuff = (byte*)Marshal.AllocHGlobal(MEMORY_MAX_BANK * MEMORY_BANK_SIZE);

            mi_fa.bank = (memory_bank*)Marshal.AllocHGlobal(sizeof(memory_bank) * MEMORY_MAX_BANK);
            mi_fb.bank = (memory_bank*)Marshal.AllocHGlobal(sizeof(memory_bank) * MEMORY_MAX_BANK);

            hex2bin_mount(ref mi_fa);
            hex2bin_mount(ref mi_fb);

            if (hex2bin_read_hex(pwd_hex0, ref mi_fa) == 0)
            {
                MessageBox.Show("###open hex0 fail!", "Error!");
                Close();
                return false;
            }
            if (hex2bin_read_hex(pwd_hex1, ref mi_fb) == 0)
            {
                MessageBox.Show("###open hex1 fail!", "Error!");
                return false;
            }

            input_data = (byte*)Marshal.AllocHGlobal(1024 * 1024);
            output_data = (byte*)Marshal.AllocHGlobal(1024 * 1024);

            is_active = true;

            return true;
        }

        public void Run(CheckBox _checkBox_FastPrintf, bool is_ascii_rcv)
        {
            if(_checkBox_FastPrintf.Checked == true)	//勾上是true
            {
                if(is_ascii_rcv == false)
                {
                    MessageBox.Show("Showing hex format!!!", "Error");
                }
                else
                {
                    _checkBox_FastPrintf.Checked = Start();
                }
            }
            else
            {
                Close();
            }
        }

        public void SelectHexFile(Button _button_FPSelect_HEX)
        {
            string fp_hex0_path_temp = hex0_path;
            string fp_hex1_path_temp = hex1_path;

            _button_FPSelect_HEX.Text = "";

            OpenFileDialog ofd0 = new OpenFileDialog();
            ofd0.Filter = "HEX文件|*.hex*";
            ofd0.ValidateNames = true;
            ofd0.CheckPathExists = true;
            ofd0.CheckFileExists = true;
            if(ofd0.ShowDialog() != DialogResult.OK)
            {
                ofd0.FileName = fp_hex0_path_temp;
            }
            hex0_path = ofd0.FileName;
            _button_FPSelect_HEX.Text += "FP HEX0 path: " + ofd0.FileName;

            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "HEX文件|*.hex*";
            ofd1.ValidateNames = true;
            ofd1.CheckPathExists = true;
            ofd1.CheckFileExists = true;
            if(ofd1.ShowDialog() != DialogResult.OK)
            {
                ofd1.FileName = fp_hex1_path_temp;
            }
            hex1_path = ofd1.FileName;
            _button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + ofd1.FileName;
        }

        public void TryDeleteDll()
        {   
            if(File.Exists(fast_printf_dll_address) == true)
            {
                string del_bat_address = @".\Del.bat";

                //将内嵌的资源释放到临时目录下
                FileStream str = new FileStream(del_bat_address, FileMode.OpenOrCreate);
                str.Write(Encoding.ASCII.GetBytes(Properties.Resources.Del), 0, Properties.Resources.Del.Length);
                str.Close();

                Process proc = null;    //批处理可以删除自己！
                try
                {
                    string targetDir = string.Format(@".\");//this is where testChange.bat lies
                    proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "Del.bat";
                    //proc.StartInfo.Arguments = string.Format("10");//this is argument
                    //proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行

                    proc.Start();
                    //proc.WaitForExit();
                }
                catch(Exception ex)
                {
                    Dbg.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                }
            }
        }

        unsafe public int DataConvert(byte[] source_data, int source_length, out byte[] target_data)
        {
            int input_length = source_length;
            for(int i = 0; i < input_length; i++)
            {
                input_data[i] = source_data[i];
            }

            int output_length;

            output_length = (int)StringConvert(input_data, (u32)input_length, output_data, ref mi_fa, ref mi_fb);

            target_data = new byte[output_length];  //申请出来的，需要GC去释放

            for(int i = 0; i < output_length; i++)
            {
                target_data[i] = output_data[i];
            }

            return output_length;
        }

        public void Check_Hex_Change()
        {
            string cpu0_hex_path = hex0_path;

            //写入时间会改变
            DateTime hex_write_time;
            FileInfo fi;
            try//预防断网，或者文件被删除等情况
            {
                fi = new FileInfo(cpu0_hex_path);
                hex_write_time = fi.LastWriteTime;
            }
            catch (Exception ex)
            {
                Enter_MessageQueue(ex.Message + "   Can't access hex file!!!");
                return;
            }

            if (last_hex_time != hex_write_time)
            {
                string s = "Hex Change!!! Create:" + fi.CreationTime.ToString() + "  Write:" + hex_write_time + "  Access:" + fi.LastAccessTime;

                Enter_MessageQueue(s);

                System.Media.SystemSounds.Hand.Play();

                last_hex_time = hex_write_time;                

                ////重新开关一次calx.exe
                //textBox_Warning.AppendText("re-run calx.exe\r\n");
                //if(is_running == true)
                //{
                //    FP_Resource_Close();
                //}
                //Func_FastPrintf_Run();
            }
        }
	}
}
