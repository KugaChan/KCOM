using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;			//判断文件是否存在

namespace KCOM
{
    class RunEXE
    {
        static public string str_run_exe_code = "How do you turn this on?";
        static public string str_default_exe_path = "12345678";
        
        static public void Run_EXE()
        {
            if(File.Exists(@RunEXE.str_default_exe_path) == false)
            {
                MessageBox.Show("Invalid default exe or name" + Dbg.GetStack(), "ERROR");
            }
            else
            {
               //MessageBox.Show("Run exe:" + Properties.Settings.Default._default_exe, "Information");

                System.Diagnostics.ProcessStartInfo pinfo = new System.Diagnostics.ProcessStartInfo();
                pinfo.UseShellExecute = true;
                pinfo.FileName = RunEXE.str_default_exe_path;

                //启动进程
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(pinfo);         
            }
        }

        static public void SetDefaultExePath(Button _button_SelectEXE)
        {
            OpenFileDialog default_exe_txt = new OpenFileDialog();
            default_exe_txt.Filter = "Executable file|*.exe*;*.bat*";
            default_exe_txt.ValidateNames = true;
            default_exe_txt.CheckPathExists = true;
            default_exe_txt.CheckFileExists = true;
            if(default_exe_txt.ShowDialog() == DialogResult.OK)
            {
                RunEXE.str_default_exe_path = default_exe_txt.FileName;
                _button_SelectEXE.Text = "Default exe: " + RunEXE.str_default_exe_path;
            }
        }

        static public void Init(TextBox _textBox_RunExeCode, Button _button_SelectEXE)
        {
            RunEXE.str_run_exe_code = Properties.Settings.Default._run_exe_code;
            RunEXE.str_default_exe_path = Properties.Settings.Default._default_exe_path;
            _textBox_RunExeCode.Text = RunEXE.str_run_exe_code;
            _button_SelectEXE.Text = "Default exe: " + Properties.Settings.Default._default_exe_path;
        }

        static public void Close()
        {
            Properties.Settings.Default._run_exe_code =  RunEXE.str_run_exe_code;
            Properties.Settings.Default._default_exe_path = RunEXE.str_default_exe_path;
        }

        static public string RunCMD(string cmdline)
        {
            Process dbg_cmd = new Process();

            dbg_cmd.StartInfo.FileName = "cmd.exe";
            dbg_cmd.StartInfo.UseShellExecute = false;
            dbg_cmd.StartInfo.RedirectStandardInput = true;
            dbg_cmd.StartInfo.RedirectStandardOutput = true;
            dbg_cmd.StartInfo.RedirectStandardError = true;
            dbg_cmd.StartInfo.CreateNoWindow = true;

            dbg_cmd.Start();

            dbg_cmd.StandardInput.WriteLine(cmdline);
            dbg_cmd.StandardInput.WriteLine("exit");
            //dbg_cmd.StandardInput.AutoFlush = true;
            dbg_cmd.StandardInput.Flush();

            //获取输出信息
            string str_output = dbg_cmd.StandardOutput.ReadToEnd();

            dbg_cmd.WaitForExit();
            dbg_cmd.Close();

            return str_output;
        }
    }
}
