using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口

namespace KCOM
{
    partial class COM
    {
        public void button_CleanSND_Click(object sender, EventArgs e, TextBox _textBox_ComSnd)
        {
            ClearSnd();
            _textBox_ComSnd.Text = "";
        }

        public void checkBox_EnAutoSnd_CheckedChanged(object sender, EventArgs e, Button _button_COMOpen)
        {
            CheckBox _checkBox_EnAutoSnd = sender as CheckBox;
            cfg.auto_send = _checkBox_EnAutoSnd.Checked;

            if(cfg.auto_send == true)//允许定时发送
            {
                if(txt.send.Length == 0 || serialport.IsOpen == false || cfg.auto_send_inverval_100ms == 0)
                {
                    cfg.auto_send = false;
                    timer_AutoSnd.Enabled = false;
                }
                else
                {
                    timer_AutoSnd.Interval = cfg.auto_send_inverval_100ms * 100;
                    timer_AutoSnd.Enabled = true;

                    _button_COMOpen.Enabled = false;
                    me.comboBox_COMBaudrate.Enabled = false;
                }
            }
            else
            {
                timer_AutoSnd.Enabled = false;

                _button_COMOpen.Enabled = true;
                me.comboBox_COMBaudrate.Enabled = true;
            }

            _checkBox_EnAutoSnd.Checked = cfg.auto_send;
        }

        public void checkBox_CursorFixed_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_CursorFixed = sender as CheckBox;
            cfg.cursor_fixed = _checkBox_CursorFixed.Checked;

            if(cfg.cursor_fixed == false)
            {
                if(txt.temp.Length > 0)
                {
                    txt.receive += txt.temp;

                    TxtRcvUpdate(txt.temp, TxtOP.ADD);
                    txt.temp = "";
                }
            }
        }

        public void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_Cmdline = sender as CheckBox;
            cfg.cmdline_mode = _checkBox_Cmdline.Checked;
        }

        public void checkBox_ASCII_Rcv_CheckedChanged(object sender, EventArgs e, TextBox _textBox_ComRec)
        {
            CheckBox _checkBox_ASCII_Rcv = sender as CheckBox;

            cfg.ascii_rcv = _checkBox_ASCII_Rcv.Checked;
            _textBox_ComRec.WordWrap = !_checkBox_ASCII_Rcv.Checked;

            if(_checkBox_ASCII_Rcv.Checked == true)	//从ASCII到HEX
            {
                txt.receive = Func.TextConvert_Hex_To_ASCII(txt.receive);
            }
            else                                    //从HEX转到ASCII
            {
                txt.receive = Func.TextConvert_ASCII_To_Hex(txt.receive);
            }

            TxtRcvUpdate(txt.receive, TxtOP.EQUAL);
        }

        public void checkBox_ASCII_Snd_CheckedChanged(object sender, EventArgs e, TextBox _textBox_ComSnd)
        {
            CheckBox _checkBox_ASCII_Snd = sender as CheckBox;

            cfg.ascii_snd = _checkBox_ASCII_Snd.Checked;
            cfg.cmdline_mode = false;
            //ascii -> hex
            if(cfg.ascii_snd == true)
            {
                txt.send = Func.TextConvert_Hex_To_ASCII(txt.send);
            }
            else//从HEX转到ASCII
            {
                txt.send = Func.TextConvert_ASCII_To_Hex(txt.send);
            }
            _textBox_ComSnd.Text = txt.send;
        }

        public void checkBox_Fliter_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_Fliter = sender as CheckBox;

            cfg.fliter_ileagal_char = _checkBox_Fliter.Checked;
        }

        public void checkBox_LimitRecLen_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_LimitRecLen = sender as CheckBox;

            cfg.limiet_rcv_lenght = _checkBox_LimitRecLen.Checked;
        }

        public void checkBox_EnableBakup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_EnableBakup = sender as CheckBox;

            cfg.backup_rcv_data = _checkBox_EnableBakup.Checked;
        }

        public void checkBox_MidMouseClear_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_MidMouseClear = sender as CheckBox;

            cfg.midmouse_clear_data = _checkBox_MidMouseClear.Checked;
        }

        public void checkBox_esc_clear_data_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _checkBox_esc_clear = sender as CheckBox;

            cfg.esc_clear_data = _checkBox_esc_clear.Checked;
        }

        public void textBox_N100ms_TextChanged(object sender, EventArgs e)
        {
            TextBox _textBox_N100ms = sender as TextBox;

            bool res = int.TryParse(_textBox_N100ms.Text, out cfg.auto_send_inverval_100ms);
            if(res == false)
            {
                cfg.auto_send_inverval_100ms = 0;
                _textBox_N100ms.Text = "0";
            }
        }

        public void textBox_custom_baudrate_TextChanged(object sender, EventArgs e)
        {
            TextBox _textBox_custom_baudrate = sender as TextBox;

            bool res = int.TryParse(_textBox_custom_baudrate.Text, out cfg.custom_baudrate);
            if(res == false)
            {
                cfg.custom_baudrate = 0;
                _textBox_custom_baudrate.Text = "0";
            }
        }

        public void textBox_ComRec_MouseDown(object sender, MouseEventArgs e)
        {
            if( (e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (cfg.midmouse_clear_data == true))
            {
                ClearRec();
            }
        }

        public void textBox_ComRec_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("[KEY]:{0} {1}", e.Control, e.KeyCode);
            if(e.Control && e.KeyCode == Keys.A)		//Ctrl + A 全选
            {
                ((TextBox)sender).SelectAll();
            }
            
            //使用鼠标中键清空，ESC容易被切屏软件误触发
            if((e.KeyCode == Keys.Escape) && (cfg.esc_clear_data == true))
            {
                ClearRec();
            }
        }
        
        public void textBox_ComSnd_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox _textBox_ComSnd = sender as TextBox;

            if((e.Button == System.Windows.Forms.MouseButtons.Middle)
             && (cfg.midmouse_clear_data == true))
            {
                ClearSnd();
                _textBox_ComSnd.Text = "";
            }
        }

        public void textBox_ComSnd_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox _textBox_ComSnd = sender as TextBox;


            if((e.KeyCode == Keys.Escape) && (cfg.esc_clear_data == true))
            {
                ClearSnd();
                _textBox_ComSnd.Text = "";
            }

            if(e.Control && e.KeyCode == Keys.S)//Keys.Enter
            {   
                Send();
            }
        }

        public void textBox_ComSnd_TextChanged(object sender, EventArgs e)
        {
            TextBox _textBox_ComSnd = sender as TextBox;

            txt.send = _textBox_ComSnd.Text;
        }

        public void textBox_Bakup_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox _textBox_Bakup = sender as TextBox;

            if(e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                txt.backup = "";
                _textBox_Bakup.Text = "";
            }
        }

        public void button_SendData_Click(object sender, EventArgs e)
        {
            Send();
        }

        public bool button_COMOpen_Click(object sender, EventArgs e)
        {
            bool res = false;

            Button _button_COMOpen = sender as Button;

            rcv_fifo.reset();

            if((_button_COMOpen.ForeColor == Color.Red) && (serialport.IsOpen == false))         //打开串口
            {
                if(Open() == true)
                {
                    res = true;
                }
            }
            else if((_button_COMOpen.ForeColor == Color.Green) && (serialport.IsOpen == true))   //关闭串口
            {
                Close();
            }
            else if((_button_COMOpen.ForeColor == Color.Green) && (serialport.IsOpen == false))  //串口使用中途已经丢失了
            {
                me.comboBox_COMNumber.SelectedIndex = -1;
            }
            else
            {
                Dbg.Assert(false, "###TODO: What is this statue!");
            }

            return res;
        }

        public void button_Snd_Click(object sender, EventArgs e)
        {
            CalSpeed();
        }

        public void label_ClearRec_DoubleClick(object sender, EventArgs e, Timer _timer_ColorShow)
        {
            Label _label_ClearRec = sender as Label;

            _label_ClearRec.BackColor = Color.Yellow;
            _timer_ColorShow.Enabled = true;

            ClearRec();
        }

        public void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            me.comboBox_COMNumber.Width = COM.BAUDRATE_WITH_SELECT;

            Ruild_ComNumberList(me.comboBox_COMNumber);

            me.comboBox_COMNumber.SelectedIndex = -1;
        }

        public delegate void tyDelegate_Set_Form_Text(string server_name, string com_name);
        public void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e, tyDelegate_Set_Form_Text Delegate_Set_Form_Text)
        {
            if(me.comboBox_COMNumber.SelectedIndex != -1)
            {
                Delegate_Set_Form_Text("", me.comboBox_COMNumber.SelectedItem.ToString());
            }

            me.comboBox_COMNumber.Width = COM.BAUDRATE_WITH_SHOW;
        }

        public void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            Rebulid_BaudrateList(me.comboBox_COMBaudrate);
        }

        public void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //在串口运行的时候更改波特率，串口关闭时候修改的时候直接在按钮函数里改就行了
            if(serialport.IsOpen == true)
            {
                serialport.BaudRate = Convert.ToInt32(me.comboBox_COMBaudrate.SelectedItem.ToString());//赋值给串口

                try
                {
                    serialport.Close();
                    serialport.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Can't open the COM port " + ex.Message + Dbg.GetStack(), "Attention!");
                }
            }
        }
    }
}
