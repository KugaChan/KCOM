using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口
using System.Collections.Generic;

namespace KCOM
{
    partial class COM_Op
    {
        static private int[] badurate_array =
        {
            4800,
            9600,
            19200,
            38400,
            115200,
            128000,
            230400,
            256000,
            460800,
            921600,
            1222400,
            1382400,
            1234567,
        };

        static public bool Open(SerialPort sp)
        {
            Dbg.WriteLine("PortName:{0}", sp.PortName);
            Dbg.WriteLine("Baudrate:{0}", sp.BaudRate);
            Dbg.WriteLine("Parity:{0}", sp.Parity);
            Dbg.WriteLine("Data:{0}", sp.DataBits);
            Dbg.WriteLine("Stop:{0}", sp.StopBits);

            if((sp.PortName == "null") ||
                (sp.BaudRate == 1) ||
                (sp.Parity == Parity.Space) ||
                (sp.DataBits == 1))
            {
                MessageBox.Show("Please choose the COM port" + Dbg.GetStack(), "Attention!");
                return false;
            }

            try
            {
                sp.Open();
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
            }
            catch(Exception ex)
            {
                //DebugIF.Assert(false, "###TODO: Why can not open COM " + ex.Message);
                MessageBox.Show(ex.Message + Dbg.GetStack(), "Attention!");
                return false;
            }

            return true;
        }

        static public bool com_is_closing = false;
        static void Close(SerialPort sp)
        {
            com_is_closing = true;

            /****************串口异常断开则直接关闭窗体 Start**************/
            bool current_com_exist = false;
            string[] strArr = Func.GetHarewareInfo(Func.HardwareEnum.Win32_PnPEntity, "Name");
            foreach(string vPortName in SerialPort.GetPortNames())
            {
                if(vPortName == sp.PortName)
                {
                    current_com_exist = true;                               //当前串口还在设备列表里
                }
            }

            //关闭串口时发现正在使用的COM不见了，由于无法调用com.close()，所以只能异常退出了
            if(current_com_exist == false)
            {
                com_is_closing = false;
                //Dbg.Assert(false, "###TODO: Why can not close COM");
            }
            else
            {
                Dbg.WriteLine("COM is still here");
            }
            /****************串口异常断开则直接关闭窗体 End****************/

            try
            {
                sp.Close();
                Dbg.WriteLine("COM close ok");
            }
            catch(Exception ex)
            {
                com_is_closing = false;
                Dbg.Assert(false, "###TODO: Why can not close COM " + ex.Message);
            }

            com_is_closing = false;
        }

        static private List<MyTimer> list_timer_scan_com = new List<MyTimer>();
        static public void Timer_ScanCOM_Add(SerialPort _serialport, ComboBox _comboBox_COMNumber, 
            Button _button_COMOpen, ty_delegate_SetComStatus _delegate_SetComStatus)
        {
            MyTimer timer_ScanCOM = new MyTimer();

            timer_ScanCOM.serialport = _serialport;
            timer_ScanCOM.comboBox_COMNumber = _comboBox_COMNumber;
            timer_ScanCOM.button_COMOpen = _button_COMOpen;
            timer_ScanCOM.delegate_SetComStatus = _delegate_SetComStatus;

            timer_ScanCOM.Elapsed += new System.Timers.ElapsedEventHandler(Timer_ScanCOM_Tick);
            timer_ScanCOM.AutoReset = true;
            timer_ScanCOM.Enabled = false;
            timer_ScanCOM.Interval = 1000;

            list_timer_scan_com.Add(timer_ScanCOM);
        }

        static void Timer_ScanCOM_Tick(object sender, EventArgs e)
        {
            MyTimer timer_ScanCOM = sender as MyTimer;
            if((timer_ScanCOM.button_COMOpen.ForeColor == Color.Green) && (timer_ScanCOM.serialport.IsOpen == false))
            {
                timer_ScanCOM.Enabled = false;

                MessageBox.Show("COM: " + timer_ScanCOM.serialport.PortName + " is lost!", "Warning!");

                Close(timer_ScanCOM.serialport);

                //this.Invoke((EventHandler)(delegate
                //{
                //    Ruild_ComNumberList(timer_ScanCOM.comboBox_COMNumber);
                //    timer_ScanCOM.delegate_SetComStatus(false);
                //}));

                Ruild_ComNumberList(timer_ScanCOM.comboBox_COMNumber);
                timer_ScanCOM.delegate_SetComStatus(false);
            }
        }

        static public void Rebulid_BaudrateList(ComboBox _comboBox_COMBaudrate, int custom_baudrate)
        {
            _comboBox_COMBaudrate.Items.Clear();
            //波特率
            for(int i = 0; i < badurate_array.Length; i++)
            {
                if(badurate_array[i] == 1234567)
                {
                    if(custom_baudrate != 0)
                    {
                        _comboBox_COMBaudrate.Items.Add(custom_baudrate.ToString());
                    }
                    else
                    {
                        _comboBox_COMBaudrate.Items.Add("1234567");
                    }
                }
                else
                {
                    _comboBox_COMBaudrate.Items.Add(badurate_array[i].ToString());
                }
            }
        }

        static public void Ruild_ComNumberList(ComboBox _comboBox_COMNumber)
        {
            _comboBox_COMNumber.Items.Clear();
            string[] strArr = Func.GetHarewareInfo(Func.HardwareEnum.Win32_PnPEntity, "Name");
            foreach(string vPortName in SerialPort.GetPortNames())
            {
                string SerialIn = "";
                SerialIn += vPortName;
                SerialIn += ':';
                foreach(string s in strArr)
                {
                    if(s.Contains(vPortName))
                    {
                        SerialIn += s;
                    }
                }
                Dbg.WriteLine(SerialIn);
                _comboBox_COMNumber.Items.Add(SerialIn);                    //将设备列表里的COM放进下拉菜单上
            }
        }

        static public void Update_SerialPortName(SerialPort sp, ComboBox _comboBox_COMNumber)
        {
            if(_comboBox_COMNumber.SelectedIndex == -1)
            {
                Console.WriteLine("Serial port not select!");
                sp.PortName = "null";
            }
            else
            {
                string str = _comboBox_COMNumber.SelectedItem.ToString();
                int end = str.IndexOf(":");
                sp.PortName = str.Substring(0, end);               //获得串口数
            }
        }

        static private void Update_SerialBaudrate(SerialPort sp, ComboBox _comboBox_COMBaudrate)
        {   
            if(_comboBox_COMBaudrate.SelectedIndex == -1)
            {
                sp.BaudRate = 1;
            }
            else
            {
                sp.BaudRate = Convert.ToInt32(_comboBox_COMBaudrate.SelectedItem.ToString());
            }
        }

        static public void Update_SerialParityBit(SerialPort sp, ComboBox _comboBox_COMCheckBit)
        {   
            if(_comboBox_COMCheckBit.SelectedIndex == -1)
            {
                sp.Parity = Parity.Space;
            }
            else
            {
                switch(_comboBox_COMCheckBit.SelectedItem.ToString())           //获得校验位
                {
                    case "None":
                    sp.Parity = Parity.None;
                    break;
                    case "Odd":
                    sp.Parity = Parity.Odd;
                    break;
                    case "Even":
                    sp.Parity = Parity.Even;
                    break;
                    default:
                    sp.Parity = Parity.None;
                    break;
                }
            }
        }

        static public void Update_SerialDataBit(SerialPort sp, ComboBox _comboBox_COMDataBit)
        {   
            if(_comboBox_COMDataBit.SelectedIndex == -1)
            {
                sp.DataBits = 1;
            }
            else
            {
                sp.DataBits = Convert.ToInt32(_comboBox_COMDataBit.SelectedItem.ToString());
            }
        }

        static public void Update_SerialStopBit(SerialPort sp, ComboBox _comboBox_COMStopBit)
        {
            if(_comboBox_COMStopBit.SelectedIndex == -1)
            {
                sp.StopBits = StopBits.None;
            }
            else
            {
                switch(_comboBox_COMStopBit.SelectedItem.ToString())        //获得停止位
                {
                    case "0":
                    sp.StopBits = StopBits.None;
                    break;
                    case "1":
                    sp.StopBits = StopBits.One;
                    break;
                    case "2":
                    sp.StopBits = StopBits.Two;
                    break;
                    case "1.5":
                    sp.StopBits = StopBits.OnePointFive;
                    break;
                    default:
                    sp.StopBits = StopBits.One;
                    break;
                }
            }
        }

        static public void ControlModule_Init(ComboBox _comboBox_COMNumber, ComboBox _comboBox_COMBaudrate,
            ComboBox _comboBox_COMCheckBit, ComboBox _comboBox_COMDataBit , ComboBox _comboBox_COMStopBit, 
            int custom_baudrate, SerialPort sp)
        {
            //更新串口下来列表的选项
            Ruild_ComNumberList(_comboBox_COMNumber);

            //波特率
            Rebulid_BaudrateList(_comboBox_COMBaudrate, custom_baudrate);

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
            Update_SerialPortName(sp, _comboBox_COMNumber);

            _comboBox_COMBaudrate.SelectedIndex = Properties.Settings.Default._baudrate_select_index;
            Update_SerialBaudrate(sp, _comboBox_COMBaudrate);

            _comboBox_COMCheckBit.SelectedIndex = 0;
            Update_SerialParityBit(sp, _comboBox_COMCheckBit);

            _comboBox_COMDataBit.SelectedIndex = 0;
            Update_SerialDataBit(sp, _comboBox_COMDataBit);

            _comboBox_COMStopBit.SelectedIndex = 1;
            Update_SerialStopBit(sp, _comboBox_COMStopBit);

            width_comlist = _comboBox_COMNumber.Width;
        }
                
        static public int width_comlist = 90;
        public const int BAUDRATE_WITH_SELECT = 320;

        static public void comboBox_COMNumber_DropDown(ComboBox _comboBox_COMNumber, SerialPort sp)
        {
            width_comlist = _comboBox_COMNumber.Width;
            _comboBox_COMNumber.Width = BAUDRATE_WITH_SELECT;

            Ruild_ComNumberList(_comboBox_COMNumber);

            _comboBox_COMNumber.SelectedIndex = -1;

            Update_SerialPortName(sp, _comboBox_COMNumber);
        }

        static public void comboBox_COMNumber_DropDownClosed(ComboBox _comboBox_COMNumber)
        {
            _comboBox_COMNumber.Width = width_comlist;
        }

        static public void comboBox_COMBaudrate_DropDown(ComboBox _comboBox_COMBaudrate, int custom_baudrate)
        {
            Rebulid_BaudrateList(_comboBox_COMBaudrate, custom_baudrate);
        }

        static public void comboBox_COMBaudrate_SelectedIndexChanged(ComboBox _comboBox_COMBaudrate, SerialPort sp)
        {
            Update_SerialBaudrate(sp, _comboBox_COMBaudrate);

            //在串口运行的时候更改波特率，串口关闭时候修改的时候直接在按钮函数里改就行了
            if(sp.IsOpen == true)
            {
                try
                {
                    sp.Close();
                    sp.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Can't re-open the COM port " + ex.Message + Dbg.GetStack(), "Attention!");
                }
            }
        }

        static public bool button_COMOpen_Click(SerialPort sp)
        {
            MyTimer MyTmr = null;

            for(int i = 0; i < list_timer_scan_com.Count; i++)
            {
                if(list_timer_scan_com[i].serialport == sp)
                {
                    MyTmr = list_timer_scan_com[i];
                    break;
                }
            }
            
            Dbg.Assert(MyTmr != null, "###Why can not find SP!?");

            bool res = false;

            if((MyTmr.button_COMOpen.ForeColor == Color.Red) && (sp.IsOpen == false))         //打开串口
            {
                if(Open(sp) == true)
                {
                    MyTmr.Enabled = true;
                    res = true;
                }
            }
            else if((MyTmr.button_COMOpen.ForeColor == Color.Green) && (sp.IsOpen == true))   //关闭串口
            {
                MyTmr.Enabled = false;

                Close(sp);

                //重建一次com number 列表，因为可能COM口有变化，同样的select下次打不开了
                int temp_select_index = MyTmr.comboBox_COMNumber.SelectedIndex;
                Ruild_ComNumberList(MyTmr.comboBox_COMNumber);
                if(MyTmr.comboBox_COMNumber.Items.Count > temp_select_index)
                {
                    MyTmr.comboBox_COMNumber.SelectedIndex = temp_select_index;
                }
            }
            //关闭窗口的时候，如果串口已经掉了，则会进来这里，触发comboBox_COMNumber_SelectedIndexChanged，不走关闭串口的路径，列表会重新建立
            else if((MyTmr.button_COMOpen.ForeColor == Color.Green) && (sp.IsOpen == false))
            {
                MyTmr.comboBox_COMNumber.SelectedIndex = -1;
            }
            //拔掉COM之后，is open会变成false 的
            else
            {
                Dbg.Assert(false, "###TODO: What is this statue?!");
            }

            return res;
        }
    }

    public delegate void ty_delegate_SetComStatus(bool en);

    public class MyTimer : System.Timers.Timer //集成Timer，可以传递其他参数
    {
        public ComboBox comboBox_COMNumber;
        public Button button_COMOpen;
        public SerialPort serialport;
        public ty_delegate_SetComStatus delegate_SetComStatus;
    }
}
