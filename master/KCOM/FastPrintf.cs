
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


namespace KCOM
{
	public partial class FormMain : Form
	{
		private AutoResetEvent Get, Got;
		public const int ServerWaitReadMillisecs = 1000; //5s
		public const int MaxTimeout = 3;

        DateTime last_hex_time;
        Process process_calx;
		bool process_calx_running = false;

		NamedPipeClientStream pipeClient;
		public bool PipeConnection(bool en)
		{
			if(en == true)
			{
				Console.WriteLine("Create the fast_pirntf_pipe!");
				try
				{
					pipeClient = new NamedPipeClientStream("localhost", "fast_printf_pipe",
					PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None);

					//pipeClient.ReadMode = PipeTransmissionMode.Byte;
					Get = new AutoResetEvent(false);
					Got = new AutoResetEvent(false);

					bool res = TryConnect();
					if(res == false)
					{
						return false;
					}
				}
				catch(Exception ex)
				{
                    MessageBox.Show(ex.Message, Func_GetStack("Error!"));
					return false;
				}
			}
			else
			{
				Console.WriteLine("Close the fast_pirntf_pipe!");
				pipeClient.Close();
			}

			return true;
		}

		private void ConnectThread()
		{
			Get.WaitOne();
			pipeClient.Connect();
			Got.Set();
		}

		private bool TryConnect()
		{
			int TimeOutCount = 0;
			var thread_pipe_convert = new Thread(ConnectThread);
			thread_pipe_convert.Start();
			Get.Set();
			while(!Got.WaitOne(ServerWaitReadMillisecs))
			{
				if(TimeOutCount++ > MaxTimeout)
				{
					thread_pipe_convert.Abort();
					//throw new TimeoutException();
					Console.WriteLine("###Pipe connect fail");
					return false;
				}				
			}
			thread_pipe_convert.Abort();
			return true;
		}

        public int DataConvert(byte[] send_buff, int offset, int count, out byte[] recv_buff)
		{
            const int recv_max_len = 1024 * 4;
			recv_buff = new byte[recv_max_len];

            int _recv_len = 0;
			try
			{
				#if false	//false, true
					//打印发送数据
					Console.Write("Send Data[{0}]:", count);
					for(int i = 0; i < count; i++)
					{
						Console.Write(" {0:X}", send_buff[i]);
					}
					Console.Write("\r\n");
				#endif

				pipeClient.Write(send_buff, offset, count);				
				pipeClient.Flush();

				_recv_len = pipeClient.Read(recv_buff, 0, recv_max_len / 2);

				#if false	//false, true
					//打印接收数据
					Console.Write("Recv Data[{0}]:", _recv_len);
					for(int i = 0; i < _recv_len; i++)
					{
						Console.Write(" {0:X}", recv_buff[i]);
					}
					Console.Write("\r\n");
				#endif
				if((_recv_len == 1) && (recv_buff[0] == 0x00))
				{
					_recv_len = 0;
					Console.WriteLine("###No data from pipe!\r\n");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, Func_GetStack("Error!"));
			}

            return _recv_len;
		}

		private void ThreadEntry_CalxOutput()
		{
			while(true)
			{
				string output = process_calx.StandardOutput.ReadToEnd();//每次读取一行
				Console.WriteLine("EXE out:{0}\n", output);

				Thread.Sleep(10000);
			}
		}

		string resource_calx_temp_address = "D:\\KCOM_Temp_Calx.exe";
		public void Func_eProcess_Init()
		{
            process_calx = new Process();

			Console.WriteLine("EXE:{0}", Properties.Settings.Default.fp_exe_path);
            button_FPSelect_EXE.Text = "FP EXE path: " + Properties.Settings.Default.fp_exe_path + "(Select)";

			Console.WriteLine("HEX0:{0}", Properties.Settings.Default.fp_hex0_path);
            Console.WriteLine("HEX1:{0}", Properties.Settings.Default.fp_hex1_path);
			
            button_FPSelect_HEX.Text = "";
			button_FPSelect_HEX.Text += "FP HEX0 path: " + Properties.Settings.Default.fp_hex0_path + "(Select)";
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + Properties.Settings.Default.fp_hex1_path + "(Select)";
		}

		private void FP_Resource_Close()
		{
			bool res = PipeConnection(false);

			thread_Calx_output.Abort();

			process_calx.Close();//Close有可能关不干净, Kill

            Thread.Sleep(50);      //不加延时，process_calx还没关完就去删calx.exe会 没有权限

			if(Properties.Settings.Default.fp_exe_path == resource_calx_temp_address)
			{
                Console.WriteLine("Delete temp calx.exe:{0}", @resource_calx_temp_address);
                try
                {
                    File.SetAttributes(@resource_calx_temp_address, FileAttributes.Normal);//file为要删除的文件
                    File.Delete(@resource_calx_temp_address);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Func_GetStack("Error!"));
                }
			}

			process_calx_running = false;

			Console.WriteLine("FastPrintf pipe disconnect:" + res.ToString() + "\r\n");		
		}

        private void Func_FastPrintf_Run()
        {
            if(param1.com_recv_ascii == false)
            {
                MessageBox.Show("Showing hex format!!!", Func_GetStack("Error"));
                checkBox_FastPrintf.Checked = false;
                return;
            }

            //加上@可以让单斜杠也能作为路径用，否则就要用双斜杠来表达路径
            if( (File.Exists(@Properties.Settings.Default.fp_exe_path) == false) ||
                (Properties.Settings.Default.fp_exe_path == null) )
            {
                #if false
			        MessageBox.Show("EXE not exist!" + Properties.Settings.Default.fp_exe_path);
			        checkBox_FastPrintf.Checked = false;
			        return;
                #else
                    string s = "use inside Calx.exe";

                    Func_ShowMessage(s);

                    //System.Media.SystemSounds.Hand.Play();            //使用内部exe时不要报警了

                    //将内嵌的资源释放到临时目录下
                    FileStream str = new FileStream(@resource_calx_temp_address, FileMode.OpenOrCreate);
                    str.Write(Properties.Resources.Calx, 0, Properties.Resources.Calx.Length);
                    str.Close();

                    Properties.Settings.Default.fp_exe_path = resource_calx_temp_address;
                    button_FPSelect_EXE.Text = "FP EXE path: " + resource_calx_temp_address + "(Select)";
                #endif
            }

            string cpu0_hex_path = Properties.Settings.Default.fp_hex0_path;
            string cpu1_hex_path = Properties.Settings.Default.fp_hex1_path;
            if((File.Exists(@cpu0_hex_path) == false) || (File.Exists(@cpu1_hex_path) == false))
            {
                MessageBox.Show("HEX not exist!   " + cpu0_hex_path + "   " + cpu1_hex_path, Func_GetStack("Warning!"));
                checkBox_FastPrintf.Checked = false;
                return;
            }

            FileInfo fi = new FileInfo(cpu0_hex_path);
            last_hex_time = fi.LastWriteTime;                           //记录上一次打开HEX的时间
            Console.WriteLine("First. Create:" + fi.CreationTime.ToString() + "  Write:" + fi.LastWriteTime + "  Access:" + fi.LastAccessTime);

            try
            {
                process_calx.StartInfo.RedirectStandardInput = true;			//接受来自调用程序的输入信息
                process_calx.StartInfo.RedirectStandardOutput = true;			//由调用程序获取输出信息
                process_calx.StartInfo.RedirectStandardError = true;			//重定向标准错误输出

                process_calx.StartInfo.CreateNoWindow = true;					//不显示程序窗口				
                process_calx.StartInfo.UseShellExecute = false;					//是否使用操作系统shell启动
                //p.StartInfo.Arguments = "IHSUSAA_1508211711 AAAAA";			//参数以空格分隔，如果某个参数为空，可以传入””
                process_calx.StartInfo.FileName = @Properties.Settings.Default.fp_exe_path;
                process_calx.StartInfo.Arguments = cpu0_hex_path + " " + cpu1_hex_path;	//传递hex的地址
                //C : \ U s e r s \ k u G a \ D e s k t o p \ M V \ K C O M \ m a s t e r \ K C O M \ b i n \ R e l e a s e
                //exe的地址是跟随者KCOM的
                process_calx.Start();

                thread_Calx_output = new Thread(ThreadEntry_CalxOutput);
		        //thread_Calx_output.IsBackground = true;
                thread_Calx_output.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Func_GetStack("Error!"));
                checkBox_FastPrintf.Checked = false;
                return;
            }

            bool res = PipeConnection(true);
            Console.WriteLine("FastPrintf pipe connect:" + res.ToString() + "\r\n");
            if(res == false)	//pipe失败，表示calx提前结束了
            {
                MessageBox.Show("pipe connect error", Func_GetStack("Error!"));
                checkBox_FastPrintf.Checked = false;
                return;
            }

            process_calx_running = true;
        }

        Thread thread_Calx_output;
		private void checkBox_FastPrintf_CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox_FastPrintf.Checked == true)	//勾上是true
			{
                Func_FastPrintf_Run();
			}
			else
			{
				if(process_calx_running == true)
				{
					FP_Resource_Close();
				}
			}
		}
        private void Func_Check_Hex_Change()
        {
            string cpu0_hex_path = Properties.Settings.Default.fp_hex0_path;

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
                Func_ShowMessage(ex.Message + "   Can't access hex file!!!");
                return;
            }

            if (last_hex_time != hex_write_time)
            {
                string s = "Hex Change!!! Create:" + fi.CreationTime.ToString() + "  Write:" + hex_write_time + "  Access:" + fi.LastAccessTime;

                Func_ShowMessage(s);

                System.Media.SystemSounds.Hand.Play();

                last_hex_time = hex_write_time;                

                ////重新开关一次calx.exe
                //textBox_Warning.AppendText("re-run calx.exe\r\n");
                //if(process_calx_running == true)
                //{
                //    FP_Resource_Close();
                //}
                //Func_FastPrintf_Run();
            }
        }

		private void button_FPSelect_EXE_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "可执行文件|*.exe";
			ofd.ValidateNames = true;
			ofd.CheckPathExists = true;
			ofd.CheckFileExists = true;
			if(ofd.ShowDialog() == DialogResult.OK)
			{
                Properties.Settings.Default.fp_exe_path = ofd.FileName;
                button_FPSelect_EXE.Text = "FP EXE path: " + ofd.FileName + "(Select)";
			}
            else
            {
                Properties.Settings.Default.fp_exe_path = null;
                button_FPSelect_EXE.Text = "Use internal calx.exe(Select)";
            }
		}

		private void button_FPSelect_HEX_Click(object sender, EventArgs e)
		{
            string fp_hex0_path_temp = Properties.Settings.Default.fp_hex0_path;
            string fp_hex1_path_temp = Properties.Settings.Default.fp_hex1_path;

            button_FPSelect_HEX.Text = "";

            OpenFileDialog ofd0 = new OpenFileDialog();
            ofd0.Filter = "HEX文件|*.hex*";
            ofd0.ValidateNames = true;
            ofd0.CheckPathExists = true;
            ofd0.CheckFileExists = true;
            if(ofd0.ShowDialog() != DialogResult.OK)
            {
                ofd0.FileName = fp_hex0_path_temp;
            }
            Properties.Settings.Default.fp_hex0_path = ofd0.FileName;
            button_FPSelect_HEX.Text += "FP HEX0 path: " + ofd0.FileName + "(Select)";

            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "HEX文件|*.hex*";
            ofd1.ValidateNames = true;
            ofd1.CheckPathExists = true;
            ofd1.CheckFileExists = true;
            if(ofd1.ShowDialog() != DialogResult.OK)
            {
                ofd1.FileName = fp_hex1_path_temp;
            }
            Properties.Settings.Default.fp_hex1_path = ofd1.FileName;
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + ofd1.FileName + "(Select)";
		}
	}
}
