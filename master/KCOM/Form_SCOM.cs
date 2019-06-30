using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KCOM
{
    class dummy2
    {

    }

    unsafe public partial class Form_Main : Form
    {
        public System.Timers.Timer timer_ScanSCOM;

        void Form_SCOM_Init()
        {
            scom.Init(com.serialport, com.Update_TextBox);

            timer_ScanSCOM = new System.Timers.Timer();
            timer_ScanSCOM.Elapsed += new System.Timers.ElapsedEventHandler(timer_ScanSCOM_Tick);
            timer_ScanSCOM.AutoReset = true;
            timer_ScanSCOM.Enabled = false;
            timer_ScanSCOM.Interval = 1000;
        }

        void timer_ScanSCOM_Tick(object sender, EventArgs e)
        {
            if((button_COMSyncOpen.ForeColor == Color.Green) && (scom.serialport.IsOpen == false))
            {
                timer_ScanSCOM.Enabled = false;

                MessageBox.Show("SCOM: " + scom.serialport.PortName + " is lost!", "Warning!");

                com.Close(scom.serialport);

                this.Invoke((EventHandler)(delegate
                {
                    com.Ruild_ComNumberList(comboBox_SyncComNum);
                    SetSComStatus(false);
                }));
            }
        }

        public void SetSComStatus(bool IsRunning)
        {
            if(IsRunning == true)
            {
                button_COMSyncOpen.Text = "COM is opened";
                button_COMSyncOpen.ForeColor = Color.Green;
            }
            else
            {
                button_COMSyncOpen.Text = "COM is closed";
                button_COMSyncOpen.ForeColor = Color.Red;
            }
        }

        private void button_COMSyncOpen_Click(object sender, EventArgs e)
        {
            bool res = false;

            if((button_COMSyncOpen.ForeColor == Color.Red) && (scom.serialport.IsOpen == false))         //打开串口
            {
                //cs.serialport.PortName = serialport.PortName;
                //cs.serialport.BaudRate = serialport.BaudRate;
                scom.serialport.Parity = com.serialport.Parity;
                scom.serialport.DataBits = com.serialport.DataBits;
                scom.serialport.StopBits = com.serialport.StopBits;

                if(com.Open(scom.serialport) == true)
                {
                    res = true;
                    timer_ScanSCOM.Enabled = true;
                }
            }
            else if((button_COMSyncOpen.ForeColor == Color.Green) && (scom.serialport.IsOpen == true))   //关闭串口
            {
                timer_ScanSCOM.Enabled = false;

                com.Close(scom.serialport);
            }

            SetSComStatus(res);
        }

        private void comboBox_SyncComNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.Update_SerialPortName(scom.serialport, comboBox_SyncComNum);
            comboBox_SyncComNum.Width = com.width_comlist;
        }

        private void comboBox_SyncComNum_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMNumber_DropDown(comboBox_SyncComNum, scom.serialport);
        }

        private void comboBox_SyncBaud_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_DropDown(comboBox_SyncBaud);
        }

        private void comboBox_SyncBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_SelectedIndexChanged(comboBox_SyncBaud, scom.serialport);
        }
    }
}
