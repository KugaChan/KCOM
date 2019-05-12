using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口

namespace KCOM
{
    partial class COM
    {
        private void Update_SerialPortName(ComboBox _comboBox_COMNumber)
        {
            if(_comboBox_COMNumber.SelectedIndex == -1)
            {
                Console.WriteLine("Serial port not select!");
                serialport.PortName = "null";
            }
            else
            {
                string str = _comboBox_COMNumber.SelectedItem.ToString();
                int end = str.IndexOf(":");
                serialport.PortName = str.Substring(0, end);               //获得串口数
            }
        }

        private void Update_SerialBaudrate(ComboBox _comboBox_COMBaudrate)
        {   
            if(_comboBox_COMBaudrate.SelectedIndex == -1)
            {
                serialport.BaudRate = 1;
            }
            else
            {
                serialport.BaudRate = Convert.ToInt32(_comboBox_COMBaudrate.SelectedItem.ToString());
            }
        }

        private void Update_SerialParityBit(ComboBox _comboBox_COMCheckBit)
        {   
            if(_comboBox_COMCheckBit.SelectedIndex == -1)
            {
                serialport.Parity = Parity.Space;
            }
            else
            {
                switch(_comboBox_COMCheckBit.SelectedItem.ToString())           //获得校验位
                {
                    case "None":
                    serialport.Parity = Parity.None;
                    break;
                    case "Odd":
                    serialport.Parity = Parity.Odd;
                    break;
                    case "Even":
                    serialport.Parity = Parity.Even;
                    break;
                    default:
                    serialport.Parity = Parity.None;
                    break;
                }
            }
        }

        private void Update_SerialDataBit(ComboBox _comboBox_COMDataBit)
        {   
            if(_comboBox_COMDataBit.SelectedIndex == -1)
            {
                serialport.DataBits = 1;
            }
            else
            {
                serialport.DataBits = Convert.ToInt32(_comboBox_COMDataBit.SelectedItem.ToString());
            }
        }

        private void Update_SerialStopBit(ComboBox _comboBox_COMStopBit)
        {
            if(_comboBox_COMStopBit.SelectedIndex == -1)
            {
                serialport.StopBits = StopBits.None;
            }
            else
            {
                switch(_comboBox_COMStopBit.SelectedItem.ToString())        //获得停止位
                {
                    case "0":
                    serialport.StopBits = StopBits.None;
                    break;
                    case "1":
                    serialport.StopBits = StopBits.One;
                    break;
                    case "2":
                    serialport.StopBits = StopBits.Two;
                    break;
                    case "1.5":
                    serialport.StopBits = StopBits.OnePointFive;
                    break;
                    default:
                    serialport.StopBits = StopBits.One;
                    break;
                }
            }
        }

        public void ControlModule_Init(ComboBox _comboBox_COMNumber, ComboBox _comboBox_COMBaudrate,
            ComboBox _comboBox_COMCheckBit, ComboBox _comboBox_COMDataBit , ComboBox _comboBox_COMStopBit)
        {
            //更新串口下来列表的选项
            Ruild_ComNumberList(_comboBox_COMNumber);

            //波特率
            Rebulid_BaudrateList(_comboBox_COMBaudrate);

            //校验位
            _comboBox_COMCheckBit.Items.Add("None");
            _comboBox_COMCheckBit.Items.Add("Odd");
            _comboBox_COMCheckBit.Items.Add("Even");

            //数据位
            _comboBox_COMDataBit.Items.Add("8");
            _comboBox_COMDataBit.Items.Add("7");
            _comboBox_COMDataBit.Items.Add("6");
            _comboBox_COMDataBit.Items.Add("5");

            //停止位
            _comboBox_COMStopBit.Items.Add("0");
            _comboBox_COMStopBit.Items.Add("1");
            _comboBox_COMStopBit.Items.Add("2");
            _comboBox_COMStopBit.Items.Add("1.5");

            if( (_comboBox_COMNumber.Items.Count > 0) 
             && (Properties.Settings.Default._com_num_select_index < _comboBox_COMNumber.Items.Count))    //串口列表选用号
            {
                _comboBox_COMNumber.SelectedIndex = Properties.Settings.Default._com_num_select_index;
            }
            else
            {
                _comboBox_COMNumber.SelectedIndex = -1;
            }
            Update_SerialPortName(_comboBox_COMNumber);

            _comboBox_COMBaudrate.SelectedIndex = Properties.Settings.Default._baudrate_select_index;
            Update_SerialBaudrate(_comboBox_COMBaudrate);

            _comboBox_COMCheckBit.SelectedIndex = 0;
            Update_SerialParityBit(_comboBox_COMCheckBit);

            _comboBox_COMDataBit.SelectedIndex = 0;
            Update_SerialDataBit(_comboBox_COMDataBit);

            _comboBox_COMStopBit.SelectedIndex = 1;
            Update_SerialStopBit(_comboBox_COMStopBit);
        }

        public void button_CleanSND_Click(object sender, EventArgs e, TextBox _textBox_ComSnd)
        {
            ClearSnd();
            _textBox_ComSnd.Text = "";
        }

        public void checkBox_EnAutoSnd_CheckedChanged(object sender, EventArgs e, Button _button_COMOpen, ComboBox _comboBox_COMBaudrate)
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
                    _comboBox_COMBaudrate.Enabled = false;
                }
            }
            else
            {
                timer_AutoSnd.Enabled = false;

                _button_COMOpen.Enabled = true;
                _comboBox_COMBaudrate.Enabled = true;
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

                    Update_TextBox(txt.temp, tyShowOp.ADD);
                    txt.temp = "";
                }
            }
        }

        public void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e, TextBox _textBox_ComSnd)
        {
            CheckBox _checkBox_Cmdline = sender as CheckBox;
            cfg.cmdline_mode = _checkBox_Cmdline.Checked;

            if(cfg.cmdline_mode == true)
            {
                _textBox_ComSnd.Enabled = false;
            }
            else
            {
                _textBox_ComSnd.Enabled = true;
            }
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

            Update_TextBox(txt.receive, tyShowOp.EQUAL);
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

        public void textBox_AutoSndInterval_100ms_TextChanged(object sender, EventArgs e)
        {
            TextBox _textBox_AutoSndInterval_100ms = sender as TextBox;

            bool res = int.TryParse(_textBox_AutoSndInterval_100ms.Text, out cfg.auto_send_inverval_100ms);
            if(res == false)
            {
                cfg.auto_send_inverval_100ms = 0;
                _textBox_AutoSndInterval_100ms.Text = "0";
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

        public bool button_COMOpen_Click(object sender, EventArgs e, ComboBox _comboBox_COMNumber)
        {
            bool res = false;

            Button _button_COMOpen = sender as Button;

            efifo_raw_2_str.Reset();

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

                int temp_select_index = _comboBox_COMNumber.SelectedIndex;  //重建一次com number 列表，因为可能COM口有变化，同样的select下次打不开了
                Ruild_ComNumberList(_comboBox_COMNumber);
                _comboBox_COMNumber.SelectedIndex = temp_select_index;
            }
            //关闭窗口的时候，如果串口已经掉了，则会进来这里，触发comboBox_COMNumber_SelectedIndexChanged，不走关闭串口的路径，列表会重新建立
            else if((_button_COMOpen.ForeColor == Color.Green) && (serialport.IsOpen == false))
            {
                _comboBox_COMNumber.SelectedIndex = -1;
            }
            //拔掉COM之后，is open会变成false 的
            else
            {
                Dbg.Assert(false, "###TODO: What is this statue!");
            }

            return res;
        }

        public void label_ClearRec_DoubleClick(object sender, EventArgs e, Timer _timer_ColorShow)
        {
            Label _label_ClearRec = sender as Label;

            _label_ClearRec.BackColor = Color.Yellow;
            _timer_ColorShow.Enabled = true;

            ClearRec();
        }

        public const int BAUDRATE_WITH_SHOW = 91;
        public const int BAUDRATE_WITH_SELECT = 320;

        public void comboBox_COMNumber_DropDown(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMNumber = sender as ComboBox;

            _comboBox_COMNumber.Width = COM.BAUDRATE_WITH_SELECT;

            Ruild_ComNumberList(_comboBox_COMNumber);

            _comboBox_COMNumber.SelectedIndex = -1;
            Update_SerialPortName(_comboBox_COMNumber);
        }

        public delegate void tyDelegate_Set_Form_Text(string server_name, string com_name);
        public void comboBox_COMNumber_SelectedIndexChanged(object sender, EventArgs e, tyDelegate_Set_Form_Text Delegate_Set_Form_Text)
        {
            ComboBox _comboBox_COMNumber = sender as ComboBox;

            if(_comboBox_COMNumber.SelectedIndex != -1)
            {
                Delegate_Set_Form_Text("", _comboBox_COMNumber.SelectedItem.ToString());
            }
            Update_SerialPortName(_comboBox_COMNumber);

            _comboBox_COMNumber.Width = COM.BAUDRATE_WITH_SHOW;
        }

        public void comboBox_COMBaudrate_DropDown(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMBaudrate = sender as ComboBox;
            Rebulid_BaudrateList(_comboBox_COMBaudrate);
        }

        public void comboBox_COMBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMBaudrate = sender as ComboBox;

            Update_SerialBaudrate(_comboBox_COMBaudrate);

            //在串口运行的时候更改波特率，串口关闭时候修改的时候直接在按钮函数里改就行了
            if(serialport.IsOpen == true)
            {
                try
                {
                    serialport.Close();
                    serialport.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Can't re-open the COM port " + ex.Message + Dbg.GetStack(), "Attention!");
                }
            }
        }

        public void comboBox_COMCheckBit_DropDown(object sender, EventArgs e)
        {
            //ComboBox _comboBox_COMCheckBit = sender as ComboBox;
            //Update_SerialParityBit(_comboBox_COMCheckBit);
        }

        public void comboBox_COMCheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMCheckBit = sender as ComboBox;
            Update_SerialParityBit(_comboBox_COMCheckBit);
        }

        public void comboBox_COMDataBit_DropDown(object sender, EventArgs e)
        {
            //ComboBox _comboBox_COMDataBit = sender as ComboBox;
            //Update_SerialStopBit(_comboBox_COMDataBit);
        }

        public void comboBox_COMDataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMDataBit = sender as ComboBox;
            Update_SerialDataBit(_comboBox_COMDataBit);
        }

        public void comboBox_COMStopBit_DropDown(object sender, EventArgs e)
        {
            //ComboBox _comboBox_COMStopBit = sender as ComboBox;
            //Update_SerialStopBit(_comboBox_COMStopBit);
        }

        public void comboBox_COMStopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox_COMStopBit = sender as ComboBox;
            Update_SerialStopBit(_comboBox_COMStopBit);
        }

        public void ShowDebugInfo()
        {
            Console.WriteLine("epool_rcv:{0}|{1}", epool_rcv.nr_got, epool_rcv.nr_ent);
            Console.WriteLine("epool_show:{0}|{1}", epool_show.nr_got, epool_show.nr_ent);

            Console.WriteLine("efifo_raw_2_str:{0}", efifo_raw_2_str.GetValidNum());
            Console.WriteLine("eFIFO_str_2_show:{0}", efifo_str_2_show.GetValidNum());

            Console.WriteLine("handle_data_thresdhold:{0}", handle_data_thresdhold);            
        }
    }
}
