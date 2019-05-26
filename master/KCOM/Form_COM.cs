using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;     //使用线程

namespace KCOM
{
    class dummy1
    {

    }

    unsafe public partial class Form_Main : Form
    {
        public System.Timers.Timer timer_ScanCOM;
        

        void Form_COM_Init()
        {
            com.fm.etcp = etcp;
            com.fm.fp = fp;

            com.ControlModule_Init(comboBox_COMNumber, comboBox_COMBaudrate,
                comboBox_COMCheckBit, comboBox_COMDataBit, comboBox_COMStopBit);
            com.Init(checkBox_Cmdline.Checked, checkBox_ASCII_Rcv.Checked, checkBox_ASCII_Snd.Checked,
                checkBox_Fliter.Checked, int.Parse(textBox_custom_baudrate.Text));

            com.thread_txt_update = new Thread(ThreadEntry_TxtUpdate);
            com.thread_txt_update.IsBackground = true;
            com.thread_txt_update.Start();

            timer_ScanCOM = new System.Timers.Timer();
            timer_ScanCOM.Elapsed += new System.Timers.ElapsedEventHandler(timer_ScanCOM_Tick);
            timer_ScanCOM.AutoReset = true;
            timer_ScanCOM.Enabled = false;
            timer_ScanCOM.Interval = 1000;
        }

        void timer_ScanCOM_Tick(object sender, EventArgs e)
        {
            //运行中途串口丢失，则弹窗报警！
            if((button_COMOpen.ForeColor == Color.Green) && (com.serialport.IsOpen == false))
            {
                timer_ScanCOM.Enabled = false;

                MessageBox.Show("COM: " + com.serialport.PortName + " is lost!", "Warning!");

                com.Close(com.serialport);

                this.Invoke((EventHandler)(delegate
                {
                    com.Ruild_ComNumberList(comboBox_COMNumber);
                    SetComStatus(false);
                }));
            }
        }

        /******************************串口 START***************************/

        static public uint check_thread_txtupdate = 0;
        static public uint step_thread_txtupdate = 0;
        void ThreadEntry_TxtUpdate()
        {
            while(true)
            {
                step_thread_txtupdate = 1;
                check_thread_txtupdate++;

                if(com.txt.backup.Length != textBox_Bakup.Text.Length)
                {
                    step_thread_txtupdate = 2;
                    this.Invoke((EventHandler)(delegate
                    {
                        textBox_Bakup.Text = com.txt.backup;
                    }));
                    step_thread_txtupdate = 3;
                }
                else if(com.efifo_str_2_show.GetValidNum() > 0)
                {
                    step_thread_txtupdate = 4;
                    COM.tyShowOp show_node = com.efifo_str_2_show.Output();
                    step_thread_txtupdate = 5;
                    this.Invoke((EventHandler)(delegate
                    {
                        if(show_node.op == COM.tyShowOp.ADD)
                        {
                            step_thread_txtupdate = 6;
                            com.record.show_bytes += (uint)show_node.text.Length;
                            textBox_ComRec.AppendText(show_node.text);
                            step_thread_txtupdate = 7;
                        }
                        if(show_node.op == COM.tyShowOp.APPEND)
                        {
                            textBox_ComRec.AppendText(show_node.text);
                        }
                        else if(show_node.op == COM.tyShowOp.EQUAL)
                        {
                            step_thread_txtupdate = 8;
                            textBox_ComRec.Text = show_node.text;
                            step_thread_txtupdate = 9;
                        }
                        else if(show_node.op == COM.tyShowOp.CLEAR)
                        {
                            step_thread_txtupdate = 10;
                            textBox_ComRec.Text = "";
                            step_thread_txtupdate = 11;
                        }
                    }));

                    step_thread_txtupdate = 12;
                    com.epool_show.Put(show_node.pnode);
                    step_thread_txtupdate = 13;
                }
                else
                {
                    step_thread_txtupdate = 14;
                    com.event_txt_update.WaitOne(1000);
                    step_thread_txtupdate = 15;
                }

                step_thread_txtupdate = 16;
            }
        }

        private void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.cmdline_mode = checkBox_Cmdline.Checked;

            if(com.cfg.cmdline_mode == true)
            {
                textBox_ComSnd.Enabled = false;
            }
            else
            {
                textBox_ComSnd.Enabled = true;
            }
        }

        private void textBox_AutoSndInterval_100ms_TextChanged(object sender, EventArgs e)
        {
            bool res = int.TryParse(textBox_AutoSndInterval_100ms.Text, out com.cfg.auto_send_inverval_100ms);
            if(res == false)
            {
                com.cfg.auto_send_inverval_100ms = 0;
                textBox_AutoSndInterval_100ms.Text = "0";
            }
        }

        private void checkBox_CursorFixed_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.cursor_fixed = checkBox_CursorFixed.Checked;

            if(com.cfg.cursor_fixed == false)
            {
                if(com.txt.temp.Length > 0)
                {
                    com.txt.receive += com.txt.temp;

                    com.Update_TextBox(com.txt.temp, COM.tyShowOp.ADD);
                    com.txt.temp = "";
                }
            }
        }

        private void checkBox_EnAutoSnd_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.auto_send = checkBox_EnAutoSnd.Checked;

            if(com.cfg.auto_send == true)//允许定时发送
            {
                if(com.txt.send.Length == 0 || com.serialport.IsOpen == false || com.cfg.auto_send_inverval_100ms == 0)
                {
                    com.cfg.auto_send = false;
                    com.timer_AutoSnd.Enabled = false;
                }
                else
                {
                    com.timer_AutoSnd.Interval = com.cfg.auto_send_inverval_100ms * 100;
                    com.timer_AutoSnd.Enabled = true;

                    button_COMOpen.Enabled = false;
                    comboBox_COMBaudrate.Enabled = false;
                }
            }
            else
            {
                com.timer_AutoSnd.Enabled = false;

                button_COMOpen.Enabled = true;
                comboBox_COMBaudrate.Enabled = true;
            }

            checkBox_EnAutoSnd.Checked = com.cfg.auto_send;
        }

        public void SetComStatus(bool IsRunning)
        {
            if(IsRunning == true)
            {
                button_COMOpen.Text = "COM is opened";
                button_COMOpen.ForeColor = Color.Green;
                comboBox_COMCheckBit.Enabled = false;
                comboBox_COMDataBit.Enabled = false;
                comboBox_COMNumber.Enabled = false;
                comboBox_COMStopBit.Enabled = false;
            }
            else
            {
                button_COMOpen.Text = "COM is closed";
                button_COMOpen.ForeColor = Color.Red;
                comboBox_COMCheckBit.Enabled = true;
                comboBox_COMDataBit.Enabled = true;
                comboBox_COMNumber.Enabled = true;
                comboBox_COMStopBit.Enabled = true;
            }
        }

        private void button_CleanSND_Click(object sender, EventArgs e)
        {
            com.ClearSnd();
            textBox_ComSnd.Text = "";
        }

        private void checkBox_WordWrap_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ComRec.WordWrap = checkBox_WordWrap.Checked;
        }

        private void textBox_ComRec_MouseDown(object sender, MouseEventArgs e)
        {
            if((e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (com.cfg.midmouse_clear_data == true))
            {
                com.ClearRec();
            }
        }

        private void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("[KEY]:{0} {1}", e.Control, e.KeyCode);
            if(e.Control && e.KeyCode == Keys.A)		//Ctrl + A 全选
            {
                textBox_ComRec.SelectAll();
            }

            //使用鼠标中键清空，ESC容易被切屏软件误触发
            if((e.KeyCode == Keys.Escape) && (com.cfg.esc_clear_data == true))
            {
                com.ClearRec();
            }
        }

        private void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Escape) && (com.cfg.esc_clear_data == true))
            {
                com.ClearSnd();
                textBox_ComSnd.Text = "";
            }

            if(e.Control && e.KeyCode == Keys.S)//Keys.Enter
            {
                com.Send();
            }
        }

        private void textBox_ComSnd_MouseDown(object sender, MouseEventArgs e)
        {
            if((e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (com.cfg.midmouse_clear_data == true))
            {
                com.ClearSnd();
                textBox_ComSnd.Text = "";
            }
        }

        private void textBox_Bakup_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                com.txt.backup = "";
                textBox_Bakup.Text = "";
            }
        }

        private void button_SendData_Click(object sender, EventArgs e)
        {
            com.Send();
        }

        private void button_COMOpen_Click(object sender, EventArgs e)
        {
            bool res = false;

            com.efifo_raw_2_str.Reset();

            if((button_COMOpen.ForeColor == Color.Red) && (com.serialport.IsOpen == false))         //打开串口
            {
                if(com.Open(com.serialport) == true)
                {
                    timer_ScanCOM.Enabled = true;
                    res = true;
                }
            }
            else if((button_COMOpen.ForeColor == Color.Green) && (com.serialport.IsOpen == true))   //关闭串口
            {
                com.Close(com.serialport);

                //重建一次com number 列表，因为可能COM口有变化，同样的select下次打不开了
                int temp_select_index = comboBox_COMNumber.SelectedIndex;
                com.Ruild_ComNumberList(comboBox_COMNumber);
                if(comboBox_COMNumber.Items.Count > temp_select_index)
                {
                    comboBox_COMNumber.SelectedIndex = temp_select_index;
                }
            }
            //关闭窗口的时候，如果串口已经掉了，则会进来这里，触发comboBox_COMNumber_SelectedIndexChanged，不走关闭串口的路径，列表会重新建立
            else if((button_COMOpen.ForeColor == Color.Green) && (com.serialport.IsOpen == false))
            {
                comboBox_COMNumber.SelectedIndex = -1;
            }
            //拔掉COM之后，is open会变成false 的
            else
            {
                Dbg.Assert(false, "###TODO: What is this statue?!");
            }

            SetComStatus(res);
        }

        private void label_ClearRec_DoubleClick(object sender, EventArgs e)
        {
            label_ClearRec.BackColor = Color.Yellow;
            timer_ColorShow.Enabled = true;

            com.ClearRec();
        }

        private void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMNumber_DropDown(comboBox_COMNumber, com.serialport);
        }

        private void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.Update_SerialPortName(com.serialport, comboBox_COMNumber);
            comboBox_COMNumber.Width = com.width_comlist;

            if(comboBox_COMNumber.SelectedIndex != -1)
            {
                Set_Form_Text("", comboBox_COMNumber.SelectedItem.ToString());
            }
        }

        private void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_DropDown(comboBox_COMBaudrate);
        }

        private void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.comboBox_COMBaudrate_SelectedIndexChanged(comboBox_COMBaudrate, com.serialport);
        }



        private void checkBox_ASCII_Rcv_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.ascii_rcv = checkBox_ASCII_Rcv.Checked;
            textBox_ComRec.WordWrap = !checkBox_ASCII_Rcv.Checked;

            if(checkBox_ASCII_Rcv.Checked == true)	//从ASCII到HEX
            {
                com.txt.receive = Func.TextConvert_Hex_To_ASCII(com.txt.receive);
            }
            else                                    //从HEX转到ASCII
            {
                com.txt.receive = Func.TextConvert_ASCII_To_Hex(com.txt.receive);
            }

            com.Update_TextBox(com.txt.receive, COM.tyShowOp.EQUAL);
        }

        private void checkBox_ASCII_Snd_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.ascii_snd = checkBox_ASCII_Snd.Checked;
            com.cfg.cmdline_mode = false;
            //ascii -> hex
            if(com.cfg.ascii_snd == true)
            {
                com.txt.send = Func.TextConvert_Hex_To_ASCII(com.txt.send);
            }
            else//从HEX转到ASCII
            {
                com.txt.send = Func.TextConvert_ASCII_To_Hex(com.txt.send);
            }
            textBox_ComSnd.Text = com.txt.send;
        }

        private void checkBox_Fliter_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.fliter_ileagal_char = checkBox_Fliter.Checked;
        }

        private void checkBox_LimitRecLen_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.limiet_rcv_lenght = checkBox_LimitRecLen.Checked;
        }

        private void checkBox_EnableBakup_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.backup_rcv_data = checkBox_EnableBakup.Checked;
        }

        private void checkBox_MidMouseClear_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.midmouse_clear_data = checkBox_MidMouseClear.Checked;
        }

        private void checkBox_esc_clear_data_CheckedChanged(object sender, EventArgs e)
        {
            com.cfg.esc_clear_data = checkBox_esc_clear_data.Checked;
        }

        private void textBox_ComSnd_TextChanged(object sender, EventArgs e)
        {
            com.txt.send = textBox_ComSnd.Text;
        }

        private void textBox_custom_baudrate_TextChanged(object sender, EventArgs e)
        {
            bool res = int.TryParse(textBox_custom_baudrate.Text, out com.cfg.custom_baudrate);
            if(res == false)
            {
                com.cfg.custom_baudrate = 0;
                textBox_custom_baudrate.Text = "0";
            }
        }

        private void comboBox_COMCheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.Update_SerialParityBit(comboBox_COMCheckBit);
        }

        private void comboBox_COMDataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.Update_SerialDataBit(comboBox_COMDataBit);
        }

        private void comboBox_COMStopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.Update_SerialStopBit(comboBox_COMStopBit);
        }

        private void comboBox_COMCheckBit_DropDown(object sender, EventArgs e)
        {
        }

        private void comboBox_COMDataBit_DropDown(object sender, EventArgs e)
        {
        }

        private void comboBox_COMStopBit_DropDown(object sender, EventArgs e)
        {
        }
        /******************************串口 END*****************************/
    }
}
