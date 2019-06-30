using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCOM
{
    class dummy3
    {

    }

    unsafe public partial class Form_Main : Form
    {
        public void Func_FastPrint_Init()
        {
            Dbg.WriteLine("HEX0:{0}", fp.hex0_path);
            Dbg.WriteLine("HEX1:{0}", fp.hex1_path);

            button_FPSelect_HEX.Text = "";
            button_FPSelect_HEX.Text += "FP HEX0 path: " + fp.hex0_path;
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + fp.hex1_path;
        }

        void checkBox_FastPrintf_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_FastPrintf.Checked == true)	//勾上是true
            {
                if(checkBox_ASCII_Rcv.Checked == false)
                {
                    MessageBox.Show("Showing hex format!!!", "Error");
                }
                else
                {
                    checkBox_FastPrintf.Checked = fp.Start();
                }
            }
            else
            {
                fp.Close();
            }
        }

        void button_FPSelect_HEX_Click(object sender, EventArgs e)
        {
            string fp_hex0_path_temp = fp.hex0_path;
            string fp_hex1_path_temp = fp.hex1_path;

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
            fp.hex0_path = ofd0.FileName;
            button_FPSelect_HEX.Text += "FP HEX0 path: " + ofd0.FileName;

            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "HEX文件|*.hex*";
            ofd1.ValidateNames = true;
            ofd1.CheckPathExists = true;
            ofd1.CheckFileExists = true;
            if(ofd1.ShowDialog() != DialogResult.OK)
            {
                ofd1.FileName = fp_hex1_path_temp;
            }
            fp.hex1_path = ofd1.FileName;
            button_FPSelect_HEX.Text += "\r\nFP HEX0 path: " + ofd1.FileName;
        }
    }
}
