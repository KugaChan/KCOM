using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;    //使用ConcurrentQueue
using System.Diagnostics;

namespace KCOM
{
    class Dbg
    {
        //跨线程要使用ConcurrentQueue而不是Queue
        static public ConcurrentQueue<string> queue_message = new ConcurrentQueue<string>();

        public static string GetStack()
        {
            string str = "";

            str += "\r\n";

            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

            #if false
                string file_name;
                file_name = st.GetFrame(0).GetFileName();
                str += "  File:" + file_name;
            #endif

            string func_name;
            func_name = st.GetFrame(0).GetMethod().Name;
            str += "  Func:" + func_name;

            int line;
            line = st.GetFrame(0).GetFileLineNumber();
            str += "  Line:" + line.ToString();            

            return str;
        }

        public static void Assert(bool must_be_true, string last_words)
        {
            if(must_be_true == false)
            {
                MessageBox.Show(last_words + GetStack(), "Assert!");
                System.Environment.Exit(0);
                //while(true);  //进while 1的话程序会一直卡死!
            }
        }

        public static void WriteLogFile(string str)
        {
            Process dbg_cmd = new Process();

            dbg_cmd.StartInfo.FileName = "cmd.exe";
            dbg_cmd.StartInfo.UseShellExecute = false;
            dbg_cmd.StartInfo.RedirectStandardInput = true;
            dbg_cmd.StartInfo.RedirectStandardOutput = true;
            dbg_cmd.StartInfo.RedirectStandardError = true;
            dbg_cmd.StartInfo.CreateNoWindow = true;

            dbg_cmd.Start();

            string cmdline;
            if(str == "")
            {
                cmdline = "echo= >> ./kcom_debug_log.txt";  //输出空行
            }
            else
            {
                cmdline = "echo " + str + " >> ./kcom_debug_log.txt";
            }
            
            dbg_cmd.StandardInput.WriteLine(cmdline);
            dbg_cmd.StandardInput.WriteLine("exit");
            //dbg_cmd.StandardInput.AutoFlush = true;
            dbg_cmd.StandardInput.Flush();

            dbg_cmd.WaitForExit();
            dbg_cmd.Close();
        }

        public static string Pirnt2String(string format, params object[] arg)
        {
            int arg_cnt = 0;
            string output_string = "";

            byte[] byteArray = System.Text.Encoding.Default.GetBytes(format);

            for(int i = 0; i < byteArray.Length; i++)
            {
                if(byteArray[i] == (byte)'%')
                {
                    output_string += arg[arg_cnt].ToString();
                    arg_cnt++;
                }
                else
                {
                    output_string += ((char)byteArray[i]);
                }
            }

            return output_string;
        }

        //注意，打印到cmd时，不能存在转义字符！！！
        public static void WriteLine(bool echo_to_log_file, string format, params object[] arg)
        {
            int arg_cnt = 0;
            string output_string = "";

            byte[] byteArray = System.Text.Encoding.Default.GetBytes(format);

            for(int i = 0; i < byteArray.Length; i++)
            {
                if(byteArray[i] == (byte)'%')
                {
                    output_string += arg[arg_cnt].ToString();
                    arg_cnt++;
                }
                else
                {
                    output_string += ((char)byteArray[i]);
                }
            }

            Console.WriteLine(output_string);
            if(echo_to_log_file == true)
            {
                WriteLogFile(output_string);
            }
        }
    }
}
