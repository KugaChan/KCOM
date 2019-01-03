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

//为变量定义别名
using u64 = System.UInt64;
using u32 = System.UInt32;
using u16 = System.UInt16;
using u8 = System.Byte;
using s64 = System.Int64;
using s32 = System.Int32;
using s16 = System.Int16;
using s8 = System.SByte;


namespace KCOM
{
    //public class FirstClass
    //{ 
    
    //}

    public partial class FormMain : Form
	{
        const u32 CONSOLE_KEY_FIFO_MAX = 1024;
        u8[] consoke_key_fifo = new u8[CONSOLE_KEY_FIFO_MAX];
        u32 console_key_input = 0;
        u32 console_key_output = 0;

        u32 console_pending_char = 0;

        Color send_fore_color_default;
        Color send_back_color_default;

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

        u8 Console_FIFO_Output()
        {
            u8 KEY;
            if (console_key_input - console_key_output > 0)
            {
                KEY = consoke_key_fifo[console_key_output];
                console_key_output++;

                return KEY;
            }
            else
            {
                MessageBox.Show("FIFO is empty" + DbgIF.GetStack(), "Warning!");
                return 0xFF;
            }
        }

        bool Console_FIFO_Input(u8 code)
        {
            if (console_key_input - console_key_output < CONSOLE_KEY_FIFO_MAX)
            {
                consoke_key_fifo[console_key_input] = code;
                console_key_input++;

                return true;
            }
            else
            {
                MessageBox.Show("FIFO is full" + DbgIF.GetStack(), "Warning!");
                return false;
            }
        }

		//所有默认热键的keydown入口在这里,返回false则原先的热键处理继续走，返回true则原先的热键处理不走了
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(checkBox_Cmdline.Checked == true)
			{
                Func_Cmdline_Key_To_ASCII(keyData);

                //Console.Write("SEND>>");
                while (true)
                {
                    if (Console_FIFO_Chk() == true)
                    {
                        u8 ascii_code = Console_FIFO_Output();
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
                                MessageBox.Show(ex.Message + DbgIF.GetStack(), "Warning!");
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("\r\n");

				return true;
			}
			else
			{
				return false;
			}
		}

		const u32 KEY_KEYBOARD_Shift = 1u << 16;
		const u32 KEY_KEYBOARD_Ctrl = 1u << 17;
		const u32 KEY_KEYBOARD_Alt = 1u << 18;
		void Func_Cmdline_Key_To_ASCII(Keys KeyCode)
		{
			Keys key_code;
			u32 key_func;

            bool key_shift_en = false;
            bool key_ctrl_en = false;
            bool key_alt_en = false;

			key_func = (u32)KeyCode >> 16;
			key_code = (Keys)((u32)KeyCode & (0x0000FFFFu));

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
                 KeyCode, (u32)key_code, (u32)key_func, key_alt_en, key_ctrl_en, key_shift_en);

            if (key_code == Keys.Tab) { Console_FIFO_Input((u8)'\t'); }
            if(key_code == Keys.Space) { Console_FIFO_Input((u8)' '); }
            if (KeyCode == Keys.Escape) { Console_FIFO_Input(0x1d); }   //0x1d
            if(KeyCode == Keys.Back) { Console_FIFO_Input((u8)'\b'); } //0x08
            if(KeyCode == Keys.Enter) { Console_FIFO_Input((u8)'\r'); }//0x0d

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
                if(key_code == Keys.Q) { Console_FIFO_Input((u8)'Q'); }
                if(key_code == Keys.W) { Console_FIFO_Input((u8)'W'); }
                if(key_code == Keys.E) { Console_FIFO_Input((u8)'E'); }
                if(key_code == Keys.R) { Console_FIFO_Input((u8)'R'); }
                if(key_code == Keys.T) { Console_FIFO_Input((u8)'T'); }
                if(key_code == Keys.Y) { Console_FIFO_Input((u8)'Y'); }
                if(key_code == Keys.U) { Console_FIFO_Input((u8)'I'); }
                if(key_code == Keys.I) { Console_FIFO_Input((u8)'I'); }
                if(key_code == Keys.O) { Console_FIFO_Input((u8)'O'); }
                if(key_code == Keys.P) { Console_FIFO_Input((u8)'P'); }
                if(key_code == Keys.OemOpenBrackets) { Console_FIFO_Input((u8)'{'); }
                if(key_code == Keys.OemCloseBrackets) { Console_FIFO_Input((u8)'}'); }
            }
            else
            {
                if(key_code == Keys.Q) { Console_FIFO_Input((u8)'q'); }
                if(key_code == Keys.W) { Console_FIFO_Input((u8)'w'); }
                if(key_code == Keys.E) { Console_FIFO_Input((u8)'e'); }
                if(key_code == Keys.R) { Console_FIFO_Input((u8)'r'); }
                if(key_code == Keys.T) { Console_FIFO_Input((u8)'t'); }
                if(key_code == Keys.Y) { Console_FIFO_Input((u8)'y'); }
                if(key_code == Keys.U) { Console_FIFO_Input((u8)'u'); }
                if(key_code == Keys.I) { Console_FIFO_Input((u8)'i'); }
                if(key_code == Keys.O) { Console_FIFO_Input((u8)'o'); }
                if(key_code == Keys.P) { Console_FIFO_Input((u8)'p'); }
                if(key_code == Keys.OemOpenBrackets) { Console_FIFO_Input((u8)'['); }
                if(key_code == Keys.OemCloseBrackets) { Console_FIFO_Input((u8)']'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.A) { Console_FIFO_Input((u8)'A'); }
                if(key_code == Keys.S) { Console_FIFO_Input((u8)'S'); }
                if(key_code == Keys.D) { Console_FIFO_Input((u8)'D'); }
                if(key_code == Keys.F) { Console_FIFO_Input((u8)'F'); }
                if(key_code == Keys.G) { Console_FIFO_Input((u8)'G'); }
                if(key_code == Keys.H) { Console_FIFO_Input((u8)'H'); }
                if(key_code == Keys.J) { Console_FIFO_Input((u8)'J'); }
                if(key_code == Keys.K) { Console_FIFO_Input((u8)'K'); }
                if(key_code == Keys.L) { Console_FIFO_Input((u8)'L'); }
                if(key_code == Keys.OemSemicolon) { Console_FIFO_Input((u8)':'); }
                if(key_code == Keys.OemQuotes) { Console_FIFO_Input((u8)'\"'); }
                if(key_code == Keys.OemPipe) { Console_FIFO_Input((u8)'|'); }
            }
            else
            {
                if(key_code == Keys.A) { Console_FIFO_Input((u8)'a'); }
                if(key_code == Keys.S) { Console_FIFO_Input((u8)'s'); }
                if(key_code == Keys.D) { Console_FIFO_Input((u8)'d'); }
                if(key_code == Keys.F) { Console_FIFO_Input((u8)'f'); }
                if(key_code == Keys.G) { Console_FIFO_Input((u8)'g'); }
                if(key_code == Keys.H) { Console_FIFO_Input((u8)'h'); }
                if(key_code == Keys.J) { Console_FIFO_Input((u8)'j'); }
                if(key_code == Keys.K) { Console_FIFO_Input((u8)'k'); }
                if(key_code == Keys.L) { Console_FIFO_Input((u8)'l'); }
                if(key_code == Keys.OemSemicolon) { Console_FIFO_Input((u8)';'); }
                if(key_code == Keys.OemQuotes) { Console_FIFO_Input((u8)'\''); }
                if(key_code == Keys.OemPipe) { Console_FIFO_Input((u8)'\\'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Z) { Console_FIFO_Input((u8)'Z'); }
                if(key_code == Keys.X) { Console_FIFO_Input((u8)'X'); }
                if(key_code == Keys.C) { Console_FIFO_Input((u8)'C'); }
                if(key_code == Keys.V) { Console_FIFO_Input((u8)'V'); }
                if(key_code == Keys.B) { Console_FIFO_Input((u8)'B'); }
                if(key_code == Keys.N) { Console_FIFO_Input((u8)'N'); }
                if(key_code == Keys.M) { Console_FIFO_Input((u8)'M'); }
                if(key_code == Keys.Oemcomma) { Console_FIFO_Input((u8)'<'); }
                if(key_code == Keys.OemPeriod) { Console_FIFO_Input((u8)'>'); }
                if(key_code == Keys.OemQuestion) { Console_FIFO_Input((u8)'?'); }
            }
            else
            {
                if(key_code == Keys.Z) { Console_FIFO_Input((u8)'z'); }
                if(key_code == Keys.X) { Console_FIFO_Input((u8)'x'); }
                if(key_code == Keys.C) { Console_FIFO_Input((u8)'c'); }
                if(key_code == Keys.V) { Console_FIFO_Input((u8)'v'); }
                if(key_code == Keys.B) { Console_FIFO_Input((u8)'b'); }
                if(key_code == Keys.N) { Console_FIFO_Input((u8)'n'); }
                if(key_code == Keys.M) { Console_FIFO_Input((u8)'m'); }
                if(key_code == Keys.Oemcomma) { Console_FIFO_Input((u8)','); }
                if(key_code == Keys.OemPeriod) { Console_FIFO_Input((u8)'.'); }
                if(key_code == Keys.OemQuestion) { Console_FIFO_Input((u8)'/'); }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Oemtilde) { Console_FIFO_Input((u8)'~'); }
                if(key_code == Keys.D1) { Console_FIFO_Input((u8)'!'); }
                if(key_code == Keys.D2) { Console_FIFO_Input((u8)'@'); }
                if(key_code == Keys.D3) { Console_FIFO_Input((u8)'#'); }
                if(key_code == Keys.D4) { Console_FIFO_Input((u8)'$'); }
                if(key_code == Keys.D5) { Console_FIFO_Input((u8)'%'); }
                if(key_code == Keys.D6) { Console_FIFO_Input((u8)'^'); }
                if(key_code == Keys.D7) { Console_FIFO_Input((u8)'&'); }
                if(key_code == Keys.D8) { Console_FIFO_Input((u8)'*'); }
                if(key_code == Keys.D9) { Console_FIFO_Input((u8)'('); }
                if(key_code == Keys.D0) { Console_FIFO_Input((u8)')'); }
                if(key_code == Keys.OemMinus) { Console_FIFO_Input((u8)'_'); }
                if(key_code == Keys.Oemplus) { Console_FIFO_Input((u8)'+'); }
            }
            else
            {
                if(key_code == Keys.Oemtilde) { Console_FIFO_Input((u8)'`'); }
                if(key_code == Keys.D1) { Console_FIFO_Input((u8)'1'); }
                if(key_code == Keys.D2) { Console_FIFO_Input((u8)'2'); }
                if(key_code == Keys.D3) { Console_FIFO_Input((u8)'3'); }
                if(key_code == Keys.D4) { Console_FIFO_Input((u8)'4'); }
                if(key_code == Keys.D5) { Console_FIFO_Input((u8)'5'); }
                if(key_code == Keys.D6) { Console_FIFO_Input((u8)'6'); }
                if(key_code == Keys.D7) { Console_FIFO_Input((u8)'7'); }
                if(key_code == Keys.D8) { Console_FIFO_Input((u8)'8'); }
                if(key_code == Keys.D9) { Console_FIFO_Input((u8)'9'); }
                if(key_code == Keys.D0) { Console_FIFO_Input((u8)'0'); }
                if(key_code == Keys.OemMinus) { Console_FIFO_Input((u8)'-'); }
                if(key_code == Keys.Oemplus) { Console_FIFO_Input((u8)'='); }
            }
		}

        void checkBox_Cmdline_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Cmdline.Checked == true)
            {
                textBox_ComSnd.Enabled = false;

                //textBox_ComRec.Enabled = false;                
                //textBox_ComRec.ForeColor = Color.Yellow;
                //textBox_ComRec.BackColor = Color.Blue;

            }
            else
            {
                //textBox_ComRec.ForeColor = send_fore_color_default;
                //textBox_ComRec.BackColor = send_back_color_default;
                //textBox_ComRec.Enabled = true;

                textBox_ComSnd.Enabled = true;
            }
        }

        void console_handler_recv_func(byte[] com_recv_buffer, s32 com_recv_buff_size)
        {
            //Console.Write("RECV<<");
            for (int i = 0; i < com_recv_buff_size; i++)
            {
                //Console.Write("{0:X} ", com_recv_buffer[i]);

                if (com_recv_buffer[i] == 0x08)                    //退格键
                {
                    if (i == 0)
                    {
                        this.Invoke((EventHandler)(delegate
                        {
                            textBox_ComRec.Text = textBox_ComRec.Text.Substring(0, textBox_ComRec.Text.Length - 1);
                            textBox_ComRec.Select(textBox_ComRec.Text.Length, 0);
                            textBox_ComRec.ScrollToCaret();
                            //textBox_ComRec.Text = textBox_ComRec.Text.Remove(textBox_ComRec.Text.Length - 1, 1); //移除掉","
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
            s32 com_recv_buff_size_fix = 0;
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
                text = textBox_ComRec.Text.Substring(textBox_ComRec.Text.Length - 2, 1);                
                if (text == ">")
                {
                    break;
                }
                else
                {
                    textBox_ComRec.Text = textBox_ComRec.Text.Substring(0, textBox_ComRec.Text.Length - 1);
                }
                console_pending_char--;
            }
            //int start = textBox_ComRec.GetFirstCharIndexFromLine(0);        //第一行第一个字符的索引
            //int end = textBox_ComRec.GetFirstCharIndexFromLine(1);          //第二行第一个字符的索引
            //textBox_ComRec.Select(start, end);                              //选中第一行
            //textBox_ComRec.SelectedText = "";                               //设置第一行的内容为空
        }
	}
}
