using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KCOM
{
    class eCMD
    {
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
