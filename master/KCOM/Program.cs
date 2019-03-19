using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace KCOM
{
    static class Program
	{
        /// <summary>k
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
		static void Main()
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            Form form_main = new FormMain();
            Application.Run(form_main);
        }
	}
}
