using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口
using System.Runtime.InteropServices;//隐藏光标的
using System.Management;


namespace KCOM
{
    class Cmdline
	{
        const UInt32 CONSOLE_KEY_FIFO_MAX = 1024;
        Byte[] consoke_key_fifo = new Byte[CONSOLE_KEY_FIFO_MAX];
        UInt32 console_key_input = 0;
        UInt32 console_key_output = 0;

        UInt32 console_pending_char = 0;

        FormMain form_main;
        TextBox textbox_show;

        public Cmdline(FormMain fm, TextBox tb_show)
        {
            form_main = fm;
            textbox_show = tb_show;
        }

        void Console_FIFO_Clear()
        {
            console_key_input = 0;
            console_key_output = 0;
        }

        bool Console_FIFO_Chk()
        {
            if (console_key_input - console_key_output > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Byte Console_FIFO_Output()
        {
            Byte KEY;
            if (console_key_input - console_key_output > 0)
            {
                KEY = consoke_key_fifo[console_key_output];
                console_key_output++;

                return KEY;
            }
            else
            {
                MessageBox.Show("FIFO is empty" + Dbg.GetStack(), "Warning!");
                return 0xFF;
            }
        }

        bool Console_FIFO_Input(Byte code)
        {
            if (console_key_input - console_key_output < CONSOLE_KEY_FIFO_MAX)
            {
                consoke_key_fifo[console_key_input] = code;
                console_key_input++;

                return true;
            }
            else
            {
                MessageBox.Show("FIFO is full" + Dbg.GetStack(), "Warning!");
                return false;
            }
        }


        public void HandleKeyData(SerialPort com, Keys keyData)
        {
            Func_Cmdline_Key_To_ASCII(keyData);

            //Console.Write("SEND>>");
            while (true)
            {
                if (Console_FIFO_Chk() == true)
                {
                    Byte ascii_code = Console_FIFO_Output();
                    //Console.Write("{0:X}", ascii_code);

                    if (ascii_code != 0)
                    {
                        string str = "";
                        str += (char)ascii_code;

                        try
                        {
                            com.Write(str);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + Dbg.GetStack(), "Warning!");
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("\r\n");
        }

		const UInt32 KEY_KEYBOARD_Shift = 1u << 16;
		const UInt32 KEY_KEYBOARD_Ctrl = 1u << 17;
		const UInt32 KEY_KEYBOARD_Alt = 1u << 18;
		void Func_Cmdline_Key_To_ASCII(Keys KeyCode)
		{
			Keys key_code;
			UInt32 key_func;

            bool key_shift_en = false;
            bool key_ctrl_en = false;
            bool key_alt_en = false;

			key_func = (UInt32)KeyCode >> 16;
			key_code = (Keys)((UInt32)KeyCode & (0x0000FFFFu));

			if((key_func & 0x0001) != 0)   //从打印中看出来的
			{
                key_shift_en = true;
			}
			if((key_func & 0x0002) != 0)
			{
                key_ctrl_en = true;
			}
			if((key_func & 0x0004) != 0)
			{
                key_alt_en = true;
			}

            Console.WriteLine("(KEY):{0}|code:{1}|func:{2}|alt:{3}|ctrl:{4}|shft:{5}",
                 KeyCode, (UInt32)key_code, (UInt32)key_func, key_alt_en, key_ctrl_en, key_shift_en);

            if (key_code == Keys.Tab) { Console_FIFO_Input((Byte)'\t'); }
            if(key_code == Keys.Space) { Console_FIFO_Input((Byte)' '); }
            if (KeyCode == Keys.Escape) { Console_FIFO_Input(0x1d); }   //0x1d
            if(KeyCode == Keys.Back) { Console_FIFO_Input((Byte)'\b'); } //0x08
            if(KeyCode == Keys.Enter) { Console_FIFO_Input((Byte)'\r'); }//0x0d

            if (KeyCode == Keys.Left)   //left: 1b 5b 44
            {
                Console_FIFO_Input(0x1b); Console_FIFO_Input(0x5b); Console_FIFO_Input(0x44);
            }
            if (KeyCode == Keys.Right)  //right:1b 5b 43
            {
                Console_FIFO_Input(0x1b); Console_FIFO_Input(0x5b); Console_FIFO_Input(0x43);
            }
            if (KeyCode == Keys.Up)     //up:   1b 5b 41
            {
                //console_dir_func();
                Console_FIFO_Input(0x1b); Console_FIFO_Input(0x5b); Console_FIFO_Input(0x41);                
            }
            if (KeyCode == Keys.Down)   //down: 1b 5b 42
            {
                //console_dir_func();
                Console_FIFO_Input(0x1b); Console_FIFO_Input(0x5b); Console_FIFO_Input(0x42);
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Q) { Console_FIFO_Input((Byte)'Q'); }
                if(key_code == Keys.W) { Console_FIFO_Input((Byte)'W'); }
                if(key_code == Keys.E) { Console_FIFO_Input((Byte)'E'); }
                if(key_code == Keys.R) { Console_FIFO_Input((Byte)'R'); }
                if(key_code == Keys.T) { Console_FIFO_Input((Byte)'T'); }
                if(key_code == Keys.Y) { Console_FIFO_Input((Byte)'Y'); }
                if(key_code == Keys.U) { Console_FIFO_Input((Byte)'I'); }
                if(key_code == Keys.I) { Console_FIFO_Input((Byte)'I'); }
                if(key_code == Keys.O) { Console_FIFO_Input((Byte)'O'); }
                if(key_code == Keys.P) { Console_FIFO_Input((Byte)'P'); }
                if(key_code == Keys.OemOpenBrackets) { Console_FIFO_Input((Byte)'{'); }
                if(key_code == Keys.OemCloseBrackets) { Console_FIFO_Input((Byte)'}'); }
            }
            else
            {
                if(key_code == Keys.Q) { Console_FIFO_Input((Byte)'q'); }
                if(key_code == Keys.W) { Console_FIFO_Input((Byte)'w'); }
                if(key_code == Keys.E) { Console_FIFO_Input((Byte)'e'); }
                if(key_code == Keys.R) { Console_FIFO_Input((Byte)'r'); }
                if(key_code == Keys.T) { Console_FIFO_Input((Byte)'t'); }
                if(key_code == Keys.Y) { Console_FIFO_Input((Byte)'y'); }
                if(key_code == Keys.U) { Console_FIFO_Input((Byte)'u'); }
                if(key_code == Keys.I) { Console_FIFO_Input((Byte)'i'); }
                if(key_code == Keys.O) { Console_FIFO_Input((Byte)'o'); }
                if(key_code == Keys.P) { Console_FIFO_Input((Byte)'p'); }
                if(key_code == Keys.OemOpenBrackets) { Console_FIFO_Input((Byte)'['); }
                if(key_code == Keys.OemCloseBrackets) { Console_FIFO_Input((Byte)']'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.A) { Console_FIFO_Input((Byte)'A'); }
                if(key_code == Keys.S) { Console_FIFO_Input((Byte)'S'); }
                if(key_code == Keys.D) { Console_FIFO_Input((Byte)'D'); }
                if(key_code == Keys.F) { Console_FIFO_Input((Byte)'F'); }
                if(key_code == Keys.G) { Console_FIFO_Input((Byte)'G'); }
                if(key_code == Keys.H) { Console_FIFO_Input((Byte)'H'); }
                if(key_code == Keys.J) { Console_FIFO_Input((Byte)'J'); }
                if(key_code == Keys.K) { Console_FIFO_Input((Byte)'K'); }
                if(key_code == Keys.L) { Console_FIFO_Input((Byte)'L'); }
                if(key_code == Keys.OemSemicolon) { Console_FIFO_Input((Byte)':'); }
                if(key_code == Keys.OemQuotes) { Console_FIFO_Input((Byte)'\"'); }
                if(key_code == Keys.OemPipe) { Console_FIFO_Input((Byte)'|'); }
            }
            else
            {
                if(key_code == Keys.A) { Console_FIFO_Input((Byte)'a'); }
                if(key_code == Keys.S) { Console_FIFO_Input((Byte)'s'); }
                if(key_code == Keys.D) { Console_FIFO_Input((Byte)'d'); }
                if(key_code == Keys.F) { Console_FIFO_Input((Byte)'f'); }
                if(key_code == Keys.G) { Console_FIFO_Input((Byte)'g'); }
                if(key_code == Keys.H) { Console_FIFO_Input((Byte)'h'); }
                if(key_code == Keys.J) { Console_FIFO_Input((Byte)'j'); }
                if(key_code == Keys.K) { Console_FIFO_Input((Byte)'k'); }
                if(key_code == Keys.L) { Console_FIFO_Input((Byte)'l'); }
                if(key_code == Keys.OemSemicolon) { Console_FIFO_Input((Byte)';'); }
                if(key_code == Keys.OemQuotes) { Console_FIFO_Input((Byte)'\''); }
                if(key_code == Keys.OemPipe) { Console_FIFO_Input((Byte)'\\'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Z) { Console_FIFO_Input((Byte)'Z'); }
                if(key_code == Keys.X) { Console_FIFO_Input((Byte)'X'); }
                if(key_code == Keys.C) { Console_FIFO_Input((Byte)'C'); }
                if(key_code == Keys.V) { Console_FIFO_Input((Byte)'V'); }
                if(key_code == Keys.B) { Console_FIFO_Input((Byte)'B'); }
                if(key_code == Keys.N) { Console_FIFO_Input((Byte)'N'); }
                if(key_code == Keys.M) { Console_FIFO_Input((Byte)'M'); }
                if(key_code == Keys.Oemcomma) { Console_FIFO_Input((Byte)'<'); }
                if(key_code == Keys.OemPeriod) { Console_FIFO_Input((Byte)'>'); }
                if(key_code == Keys.OemQuestion) { Console_FIFO_Input((Byte)'?'); }
            }
            else
            {
                if(key_code == Keys.Z) { Console_FIFO_Input((Byte)'z'); }
                if(key_code == Keys.X) { Console_FIFO_Input((Byte)'x'); }
                if(key_code == Keys.C) { Console_FIFO_Input((Byte)'c'); }
                if(key_code == Keys.V) { Console_FIFO_Input((Byte)'v'); }
                if(key_code == Keys.B) { Console_FIFO_Input((Byte)'b'); }
                if(key_code == Keys.N) { Console_FIFO_Input((Byte)'n'); }
                if(key_code == Keys.M) { Console_FIFO_Input((Byte)'m'); }
                if(key_code == Keys.Oemcomma) { Console_FIFO_Input((Byte)','); }
                if(key_code == Keys.OemPeriod) { Console_FIFO_Input((Byte)'.'); }
                if(key_code == Keys.OemQuestion) { Console_FIFO_Input((Byte)'/'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Oemtilde) { Console_FIFO_Input((Byte)'~'); }
                if(key_code == Keys.D1) { Console_FIFO_Input((Byte)'!'); }
                if(key_code == Keys.D2) { Console_FIFO_Input((Byte)'@'); }
                if(key_code == Keys.D3) { Console_FIFO_Input((Byte)'#'); }
                if(key_code == Keys.D4) { Console_FIFO_Input((Byte)'$'); }
                if(key_code == Keys.D5) { Console_FIFO_Input((Byte)'%'); }
                if(key_code == Keys.D6) { Console_FIFO_Input((Byte)'^'); }
                if(key_code == Keys.D7) { Console_FIFO_Input((Byte)'&'); }
                if(key_code == Keys.D8) { Console_FIFO_Input((Byte)'*'); }
                if(key_code == Keys.D9) { Console_FIFO_Input((Byte)'('); }
                if(key_code == Keys.D0) { Console_FIFO_Input((Byte)')'); }
                if(key_code == Keys.OemMinus) { Console_FIFO_Input((Byte)'_'); }
                if(key_code == Keys.Oemplus) { Console_FIFO_Input((Byte)'+'); }
            }
            else
            {
                if(key_code == Keys.Oemtilde) { Console_FIFO_Input((Byte)'`'); }
                if(key_code == Keys.D1) { Console_FIFO_Input((Byte)'1'); }
                if(key_code == Keys.D2) { Console_FIFO_Input((Byte)'2'); }
                if(key_code == Keys.D3) { Console_FIFO_Input((Byte)'3'); }
                if(key_code == Keys.D4) { Console_FIFO_Input((Byte)'4'); }
                if(key_code == Keys.D5) { Console_FIFO_Input((Byte)'5'); }
                if(key_code == Keys.D6) { Console_FIFO_Input((Byte)'6'); }
                if(key_code == Keys.D7) { Console_FIFO_Input((Byte)'7'); }
                if(key_code == Keys.D8) { Console_FIFO_Input((Byte)'8'); }
                if(key_code == Keys.D9) { Console_FIFO_Input((Byte)'9'); }
                if(key_code == Keys.D0) { Console_FIFO_Input((Byte)'0'); }
                if(key_code == Keys.OemMinus) { Console_FIFO_Input((Byte)'-'); }
                if(key_code == Keys.Oemplus) { Console_FIFO_Input((Byte)'='); }
            }
		}

        public void HandlerRecv(byte[] com_recv_buffer, int com_recv_buff_size)
        {
            //Console.Write("RECV<<");
            for (int i = 0; i < com_recv_buff_size; i++)
            {
                //Console.Write("{0:X} ", com_recv_buffer[i]);

                if (com_recv_buffer[i] == 0x08)                    //退格键
                {
                    if (i == 0)
                    {
                        form_main.Invoke((EventHandler)(delegate
                        {
                            textbox_show.Text = textbox_show.Text.Substring(0, textbox_show.Text.Length - 1);
                            textbox_show.Select(textbox_show.Text.Length, 0);
                            textbox_show.ScrollToCaret();
                            //textbox_show.Text = textbox_show.Text.Remove(textbox_show.Text.Length - 1, 1); //移除掉","
                        }));
                    }
                    else
                    {
                        com_recv_buffer[i - 1] = 0x00;
                    }

					com_recv_buffer[i] = 0x00;
                }

				if(com_recv_buffer[i] == 0x1d)
                {
					com_recv_buffer[i] = 0x00;
                }
            }
            Console.WriteLine(" ");

			byte[] com_recv_buffer_fixed = new byte[com_recv_buff_size + 1];
            int com_recv_buff_size_fix = 0;
            //Console.Write("recv<<");
            for (int i = 0; i < com_recv_buff_size; i++)             //把非0数据复制到fix数组上
            {
                if (com_recv_buffer[i] != 0x00)
                {
                    //Console.Write("{0:X} ", com_recv_buffer[i]);
                    com_recv_buffer_fixed[com_recv_buff_size_fix] = com_recv_buffer[i];
                    com_recv_buff_size_fix++;
                }
            }
            Console.WriteLine(" ");

            if (com_recv_buff_size_fix == 0)
            {
                Console.WriteLine("LEAVE");
                //this.Invoke((EventHandler)(delegate
                //{
                //    textBox_ComSnd.AppendText("...");        //跳转一下光标位置
                //}));
                return;
            }
            else
            {
                for (int i = 0; i < com_recv_buff_size_fix; i++)     //从fix数组还原到原本数组上
                {
                    com_recv_buffer[i] = com_recv_buffer_fixed[i];
                }
                com_recv_buff_size = com_recv_buff_size_fix;
            }
        }

        void console_dir_func()
        {
            string text;

            Console.WriteLine("pending char:{0}\n", console_pending_char);

            while (console_pending_char > 0)
            {
                text = textbox_show.Text.Substring(textbox_show.Text.Length - 2, 1);                
                if (text == ">")
                {
                    break;
                }
                else
                {
                    textbox_show.Text = textbox_show.Text.Substring(0, textbox_show.Text.Length - 1);
                }
                console_pending_char--;
            }
            //int start = textbox_show.GetFirstCharIndexFromLine(0);        //第一行第一个字符的索引
            //int end = textbox_show.GetFirstCharIndexFromLine(1);          //第二行第一个字符的索引
            //textbox_show.Select(start, end);                              //选中第一行
            //textbox_show.SelectedText = "";                               //设置第一行的内容为空
        }
	}
}
