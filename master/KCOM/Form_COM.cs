using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;     //使用线程

namespace KCOM
{
    class dummy_form_com
    {

    }

    unsafe public partial class Form_Main : Form
    {
        void Form_COM_Init()
        {
            main_com.fm.etcp = etcp;
            main_com.fm.fp = fp;

            main_com.Init(checkBox_Cmdline.Checked, checkBox_ASCII_Rcv.Checked, checkBox_ASCII_Snd.Checked,
                checkBox_Fliter.Checked, int.Parse(textBox_custom_baudrate.Text));

            main_com.thread_txt_update = new Thread(ThreadEntry_TxtUpdate);
            main_com.thread_txt_update.IsBackground = true;
            main_com.thread_txt_update.Start();

            COM_Op.Timer_ScanCOM_Add(main_com.serialport, comboBox_COMNumber, button_COMOpen, SetComStatus);
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

                int backup_tex_length = main_com.txt.backup.Length;
                this.Invoke((EventHandler)(delegate
                {
                    try
                    {
                        backup_tex_length = textBox_Bakup.Text.Length;
                    }
                    catch
                    {
                        backup_tex_length = main_com.txt.backup.Length;
                    }
                }));

                if(main_com.txt.backup.Length != backup_tex_length)
                {
                    step_thread_txtupdate = 2;
                    this.Invoke((EventHandler)(delegate
                    {
                        textBox_Bakup.Text = main_com.txt.backup;
                    }));
                    step_thread_txtupdate = 3;
                }
                else if(main_com.efifo_str_2_show.GetValidNum() > 0)
                {
                    step_thread_txtupdate = 4;
                    COM.tyShowOp show_node = main_com.efifo_str_2_show.Output();
                    step_thread_txtupdate = 5;
                    this.Invoke((EventHandler)(delegate
                    {
                        if(show_node.op == COM.tyShowOp.ADD)
                        {
                            step_thread_txtupdate = 6;
                            main_com.record.show_bytes += (uint)show_node.text.Length;
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
                    main_com.epool_show.Put(show_node.pnode);
                    step_thread_txtupdate = 13;
                }
                else
                {
                    step_thread_txtupdate = 14;
                    main_com.event_txt_update.WaitOne(1000);
                    step_thread_txtupdate = 15;
                }

                step_thread_txtupdate = 16;
            }
        }

        private void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.cmdline_mode = checkBox_Cmdline.Checked;

            if(main_com.cfg.cmdline_mode == true)
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
            bool res = int.TryParse(textBox_AutoSndInterval_100ms.Text, out main_com.cfg.auto_send_inverval_100ms);
            if(res == false)
            {
                main_com.cfg.auto_send_inverval_100ms = 0;
                textBox_AutoSndInterval_100ms.Text = "0";
            }
        }

        private void checkBox_CursorFixed_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.cursor_fixed = checkBox_CursorFixed.Checked;

            if(main_com.cfg.cursor_fixed == false)
            {
                if(main_com.txt.temp.Length > 0)
                {
                    main_com.txt.receive += main_com.txt.temp;

                    main_com.Update_TextBox(main_com.txt.temp, COM.tyShowOp.ADD);
                    main_com.txt.temp = "";
                }
            }
        }

        private void checkBox_EnAutoSnd_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.auto_send = checkBox_EnAutoSnd.Checked;

            if(main_com.cfg.auto_send == true)//允许定时发送
            {
                if(main_com.txt.send.Length == 0 || main_com.serialport.IsOpen == false || main_com.cfg.auto_send_inverval_100ms == 0)
                {
                    main_com.cfg.auto_send = false;
                    main_com.timer_AutoSnd.Enabled = false;
                }
                else
                {
                    main_com.timer_AutoSnd.Interval = main_com.cfg.auto_send_inverval_100ms * 100;
                    main_com.timer_AutoSnd.Enabled = true;

                    button_COMOpen.Enabled = false;
                    comboBox_COMBaudrate.Enabled = false;
                }
            }
            else
            {
                main_com.timer_AutoSnd.Enabled = false;

                button_COMOpen.Enabled = true;
                comboBox_COMBaudrate.Enabled = true;
            }

            checkBox_EnAutoSnd.Checked = main_com.cfg.auto_send;
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
            main_com.ClearSnd();
            textBox_ComSnd.Text = "";
        }

        private void checkBox_WordWrap_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ComRec.WordWrap = checkBox_WordWrap.Checked;
        }

        private void textBox_ComRec_MouseDown(object sender, MouseEventArgs e)
        {
            if((e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (main_com.cfg.midmouse_clear_data == true))
            {
                main_com.ClearRec();
            }
        }

        private void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
            Dbg.WriteLine("[KEY]:{0} {1}", e.Control, e.KeyCode);
            if(e.Control && e.KeyCode == Keys.A)		//Ctrl + A 全选
            {
                textBox_ComRec.SelectAll();
            }

            //使用鼠标中键清空，ESC容易被切屏软件误触发
            if((e.KeyCode == Keys.Escape) && (main_com.cfg.esc_clear_data == true))
            {
                main_com.ClearRec();
            }
        }

        private void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Escape) && (main_com.cfg.esc_clear_data == true))
            {
                main_com.ClearSnd();
                textBox_ComSnd.Text = "";
            }

            if(e.Control && e.KeyCode == Keys.S)//Keys.Enter
            {
                main_com.Send();
            }
        }

        private void textBox_ComSnd_MouseDown(object sender, MouseEventArgs e)
        {
            if((e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (main_com.cfg.midmouse_clear_data == true))
            {
                main_com.ClearSnd();
                textBox_ComSnd.Text = "";
            }
        }

        private void textBox_Bakup_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                main_com.txt.backup = "";
                textBox_Bakup.Text = "";
            }
        }

        private void button_SendData_Click(object sender, EventArgs e)
        {
            main_com.Send();
        }

        private void button_COMOpen_Click(object sender, EventArgs e)
        {
            main_com.efifo_raw_2_str.Reset();

            bool res = COM_Op.button_COMOpen_Click(main_com.serialport);
            SetComStatus(res);
        }

        private void label_ClearRec_DoubleClick(object sender, EventArgs e)
        {
            label_ClearRec.BackColor = Color.Yellow;
            timer_ColorShow.Enabled = true;

            main_com.ClearRec();
        }

        private void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMNumber_DropDown(comboBox_COMNumber, main_com.serialport);
        }

        private void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.Update_SerialPortName(main_com.serialport, comboBox_COMNumber);

            if(comboBox_COMNumber.SelectedIndex != -1)
            {
                Set_Form_Text("", comboBox_COMNumber.SelectedItem.ToString());
            }
        }

        private void comboBox_COMNumber_DropDownClosed(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMNumber_DropDownClosed(comboBox_COMNumber);  
        }

        private void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMBaudrate_DropDown(comboBox_COMBaudrate, main_com.cfg.custom_baudrate);
        }

        private void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMBaudrate_SelectedIndexChanged(comboBox_COMBaudrate, main_com.serialport);
        }

        private void checkBox_ASCII_Rcv_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.ascii_rcv = checkBox_ASCII_Rcv.Checked;
            textBox_ComRec.WordWrap = !checkBox_ASCII_Rcv.Checked;

            if(checkBox_ASCII_Rcv.Checked == true)	//从ASCII到HEX
            {
                main_com.txt.receive = Func.TextConvert_Hex_To_ASCII(main_com.txt.receive);
            }
            else                                    //从HEX转到ASCII
            {
                main_com.txt.receive = Func.TextConvert_ASCII_To_Hex(main_com.txt.receive);
            }

            main_com.Update_TextBox(main_com.txt.receive, COM.tyShowOp.EQUAL);
        }

        private void checkBox_ASCII_Snd_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.ascii_snd = checkBox_ASCII_Snd.Checked;
            main_com.cfg.cmdline_mode = false;
            //ascii -> hex
            if(main_com.cfg.ascii_snd == true)
            {
                main_com.txt.send = Func.TextConvert_Hex_To_ASCII(main_com.txt.send);
            }
            else//从HEX转到ASCII
            {
                main_com.txt.send = Func.TextConvert_ASCII_To_Hex(main_com.txt.send);
            }
            textBox_ComSnd.Text = main_com.txt.send;
        }

        private void checkBox_Fliter_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.fliter_ileagal_char = checkBox_Fliter.Checked;
        }

        private void checkBox_LimitRecLen_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.limiet_rcv_lenght = checkBox_LimitRecLen.Checked;
        }

        private void checkBox_EnableBakup_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.backup_rcv_data = checkBox_EnableBakup.Checked;
        }

        private void checkBox_MidMouseClear_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.midmouse_clear_data = checkBox_MidMouseClear.Checked;
        }

        private void checkBox_esc_clear_data_CheckedChanged(object sender, EventArgs e)
        {
            main_com.cfg.esc_clear_data = checkBox_esc_clear_data.Checked;
        }

        private void textBox_ComSnd_TextChanged(object sender, EventArgs e)
        {
            main_com.txt.send = textBox_ComSnd.Text;
        }

        private void textBox_custom_baudrate_TextChanged(object sender, EventArgs e)
        {
            bool res = int.TryParse(textBox_custom_baudrate.Text, out main_com.cfg.custom_baudrate);
            if(res == false)
            {
                main_com.cfg.custom_baudrate = 0;
                textBox_custom_baudrate.Text = "0";
            }
        }

        private void comboBox_COMCheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.Update_SerialParityBit(main_com.serialport, comboBox_COMCheckBit);
        }

        private void comboBox_COMDataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.Update_SerialDataBit(main_com.serialport, comboBox_COMDataBit);
        }

        private void comboBox_COMStopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.Update_SerialStopBit(main_com.serialport, comboBox_COMStopBit);
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

        /*****************************SCOM Start***************************/
        void Form_SCOM_Init()
        {
            sync_com.Init(main_com.serialport, main_com.Update_TextBox);
            COM_Op.Timer_ScanCOM_Add(sync_com.serialport, comboBox_SyncComNum, button_COMSyncOpen, SetSComStatus);
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
            if((button_COMSyncOpen.ForeColor == Color.Red) && (sync_com.serialport.IsOpen == false))         //打开串口
            {
                sync_com.serialport.Parity = main_com.serialport.Parity;
                sync_com.serialport.DataBits = main_com.serialport.DataBits;
                sync_com.serialport.StopBits = main_com.serialport.StopBits;
            }

            bool res = COM_Op.button_COMOpen_Click(sync_com.serialport);
            SetSComStatus(res);
        }

        private void comboBox_SyncComNum_DropDown(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMNumber_DropDown(comboBox_SyncComNum, sync_com.serialport);
        }

        private void comboBox_SyncComNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.Update_SerialPortName(sync_com.serialport, comboBox_SyncComNum);
        }

        private void comboBox_SyncComNum_DropDownClosed(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMNumber_DropDownClosed(comboBox_SyncComNum);
        }

        private void comboBox_SyncBaud_DropDown(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMBaudrate_DropDown(comboBox_SyncBaud, 0);
        }

        private void comboBox_SyncBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM_Op.comboBox_COMBaudrate_SelectedIndexChanged(comboBox_SyncBaud, sync_com.serialport);
        }
        /******************************SCOM End****************************/
    }
}
