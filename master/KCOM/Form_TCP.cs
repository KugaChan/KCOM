using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCOM
{
    class dummy4
    {

    }

    unsafe public partial class Form_Main : Form
    {
        void button_NetRun_Click(object sender, EventArgs e)
        {
            if(button_NetRun.Text != "Break the eTCP")
            {
                if(etcp.ConfigNet(
                    6666,
                    textBox_IP1.Text,
                    textBox_IP2.Text,
                    textBox_IP3.Text,
                    textBox_IP4.Text) == true)
                {
                    button_NetRole.Enabled = false;
                    button_NetRun.Text = "Break the eTCP";
                }
            }
            else
            {
                button_NetRole.Enabled = true;
                etcp.Close();
                if(etcp.is_server == true)
                {
                    button_NetRun.Text = "Connect to server";
                }
                else
                {
                    button_NetRun.Text = "Wait for client";
                }
            }
        }

        void button_NetRole_Click(object sender, EventArgs e)
        {
            if(etcp.is_server == true)
            {
                if(com.serialport.IsOpen == true)
                {
                    MessageBox.Show("COM is open, client can't enable uart!", "Error");
                    return;
                }
                etcp.is_server = false;
            }
            else
            {
                etcp.is_server = true;
            }
            Func_NetCom_ChangeFont(etcp.is_server);
        }

        public void Func_NetCom_ChangeFont(bool is_server)
        {
            if(is_server == false)
            {
                Set_Form_Text("(Client)", "");
                button_NetRole.ForeColor = Color.Blue;
                button_NetRole.Text = "I am Client";
                button_NetRun.Text = "Connect to Server";
                label_IP.Text = "Server IP:";
                button_COMOpen.Enabled = false;
            }
            else
            {
                Set_Form_Text("(Server)", "");
                button_NetRole.ForeColor = Color.Red;
                button_NetRole.Text = "I am Server";
                button_NetRun.Text = "Wait for Clients";
                label_IP.Text = "Local IP:";
                button_COMOpen.Enabled = true;
            }
        }
    }
}
