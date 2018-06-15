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
	public partial class Form1
	{
		//所有默认热键的keydown入口在这里,返回false则原先的热键处理继续走，返回true则原先的热键处理不走了
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(checkBox_Cmdline.Checked == true)
			{
                textBox_ComSnd.Enabled = false;
                //textBox_ComSnd.ReadOnly = true;                

				u8 ascii_code = Func_Cmdline_Key_To_ASCII(keyData);

                if(ascii_code != 0)
                {
                    string str = "";
                    str += (char)ascii_code;

                    try
                    {
                        com.Write(str);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示");
                    }
                }

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
		private u8 Func_Cmdline_Key_To_ASCII(Keys KeyCode)
		{
			u8 ascii_code = 0;

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

            Console.WriteLine(">>KEY:{0}|code:{1}|func:{2}|alt:{3}|ctrl:{4}|shft:{5}",
                 KeyCode, (u32)key_code, (u32)key_func, key_alt_en, key_ctrl_en, key_shift_en);
            
            if(key_code == Keys.Tab) { ascii_code = (u8)'\t'; }
            if(key_code == Keys.Space) { ascii_code = (u8)' '; }
            if(KeyCode == Keys.Escape) { ascii_code = 0x1d; }   //0x1d
            if(KeyCode == Keys.Back) { ascii_code = (u8)'\b'; } //0x08
            if(KeyCode == Keys.Enter) { ascii_code = (u8)'\r'; }//0x0d
            //left: 1b 5b 44
            //right:1b 5b 43
            //up:   1b 5b 41
            //down: 1b 5b 42

            if(key_shift_en == true)
            {
                
                if(key_code == Keys.Q) { ascii_code = (u8)'Q'; }
                if(key_code == Keys.W) { ascii_code = (u8)'W'; }
                if(key_code == Keys.E) { ascii_code = (u8)'E'; }
                if(key_code == Keys.R) { ascii_code = (u8)'R'; }
                if(key_code == Keys.T) { ascii_code = (u8)'T'; }
                if(key_code == Keys.Y) { ascii_code = (u8)'Y'; }
                if(key_code == Keys.U) { ascii_code = (u8)'I'; }
                if(key_code == Keys.I) { ascii_code = (u8)'I'; }
                if(key_code == Keys.O) { ascii_code = (u8)'O'; }
                if(key_code == Keys.P) { ascii_code = (u8)'P'; }
                if(key_code == Keys.OemOpenBrackets) { ascii_code = (u8)'{'; }
                if(key_code == Keys.OemCloseBrackets) { ascii_code = (u8)'}'; }
            }
            else
            {
                if(key_code == Keys.Q) { ascii_code = (u8)'q'; }
                if(key_code == Keys.W) { ascii_code = (u8)'w'; }
                if(key_code == Keys.E) { ascii_code = (u8)'e'; }
                if(key_code == Keys.R) { ascii_code = (u8)'r'; }
                if(key_code == Keys.T) { ascii_code = (u8)'t'; }
                if(key_code == Keys.Y) { ascii_code = (u8)'y'; }
                if(key_code == Keys.U) { ascii_code = (u8)'u'; }
                if(key_code == Keys.I) { ascii_code = (u8)'i'; }
                if(key_code == Keys.O) { ascii_code = (u8)'o'; }
                if(key_code == Keys.P) { ascii_code = (u8)'p'; }
                if(key_code == Keys.OemOpenBrackets) { ascii_code = (u8)'['; }
                if(key_code == Keys.OemCloseBrackets) { ascii_code = (u8)']'; }
            }


            if(key_shift_en == true)
            {
                if(key_code == Keys.A) { ascii_code = (u8)'A'; }
                if(key_code == Keys.S) { ascii_code = (u8)'S'; }
                if(key_code == Keys.D) { ascii_code = (u8)'D'; }
                if(key_code == Keys.F) { ascii_code = (u8)'F'; }
                if(key_code == Keys.G) { ascii_code = (u8)'G'; }
                if(key_code == Keys.H) { ascii_code = (u8)'H'; }
                if(key_code == Keys.J) { ascii_code = (u8)'J'; }
                if(key_code == Keys.K) { ascii_code = (u8)'K'; }
                if(key_code == Keys.L) { ascii_code = (u8)'L'; }
                if(key_code == Keys.OemSemicolon) { ascii_code = (u8)':'; }
                if(key_code == Keys.OemQuotes) { ascii_code = (u8)'\"'; };
                if(key_code == Keys.OemPipe) { ascii_code = (u8)'|'; }
            }
            else
            {
                if(key_code == Keys.A) { ascii_code = (u8)'a'; }
                if(key_code == Keys.S) { ascii_code = (u8)'s'; }
                if(key_code == Keys.D) { ascii_code = (u8)'d'; }
                if(key_code == Keys.F) { ascii_code = (u8)'f'; }
                if(key_code == Keys.G) { ascii_code = (u8)'g'; }
                if(key_code == Keys.H) { ascii_code = (u8)'h'; }
                if(key_code == Keys.J) { ascii_code = (u8)'j'; }
                if(key_code == Keys.K) { ascii_code = (u8)'k'; }
                if(key_code == Keys.L) { ascii_code = (u8)'l'; }
                if(key_code == Keys.OemSemicolon) { ascii_code = (u8)';'; }
                if(key_code == Keys.OemQuotes) { ascii_code = (u8)'\''; };
                if(key_code == Keys.OemPipe) { ascii_code = (u8)'\\'; }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Z) { ascii_code = (u8)'Z'; }
                if(key_code == Keys.X) { ascii_code = (u8)'X'; }
                if(key_code == Keys.C) { ascii_code = (u8)'C'; }
                if(key_code == Keys.V) { ascii_code = (u8)'V'; }
                if(key_code == Keys.B) { ascii_code = (u8)'B'; }
                if(key_code == Keys.N) { ascii_code = (u8)'N'; }
                if(key_code == Keys.M) { ascii_code = (u8)'M'; }
                if(key_code == Keys.Oemcomma) { ascii_code = (u8)'<'; }
                if(key_code == Keys.OemPeriod) { ascii_code = (u8)'>'; }
                if(key_code == Keys.OemQuestion) { ascii_code = (u8)'?'; }
            }
            else
            {
                if(key_code == Keys.Z) { ascii_code = (u8)'z'; }
                if(key_code == Keys.X) { ascii_code = (u8)'x'; }
                if(key_code == Keys.C) { ascii_code = (u8)'c'; }
                if(key_code == Keys.V) { ascii_code = (u8)'v'; }
                if(key_code == Keys.B) { ascii_code = (u8)'b'; }
                if(key_code == Keys.N) { ascii_code = (u8)'n'; }
                if(key_code == Keys.M) { ascii_code = (u8)'m'; }
                if(key_code == Keys.Oemcomma) { ascii_code = (u8)','; }
                if(key_code == Keys.OemPeriod) { ascii_code = (u8)'.'; }
                if(key_code == Keys.OemQuestion) { ascii_code = (u8)'/'; }
            }

            if(key_shift_en == true)
            {
                if(key_code == Keys.Oemtilde) { ascii_code = (u8)'~'; }
                if(key_code == Keys.D1) { ascii_code = (u8)'!'; }
                if(key_code == Keys.D2) { ascii_code = (u8)'@'; }
                if(key_code == Keys.D3) { ascii_code = (u8)'#'; }
                if(key_code == Keys.D4) { ascii_code = (u8)'$'; }
                if(key_code == Keys.D5) { ascii_code = (u8)'%'; }
                if(key_code == Keys.D6) { ascii_code = (u8)'^'; }
                if(key_code == Keys.D7) { ascii_code = (u8)'&'; }
                if(key_code == Keys.D8) { ascii_code = (u8)'*'; }
                if(key_code == Keys.D9) { ascii_code = (u8)'('; }
                if(key_code == Keys.D0) { ascii_code = (u8)')'; }
                if(key_code == Keys.OemMinus) { ascii_code = (u8)'_'; }
                if(key_code == Keys.Oemplus) { ascii_code = (u8)'+'; }
            }
            else
            {
                if(key_code == Keys.Oemtilde) { ascii_code = (u8)'`'; }
                if(key_code == Keys.D1) { ascii_code = (u8)'1'; }
                if(key_code == Keys.D2) { ascii_code = (u8)'2'; }
                if(key_code == Keys.D3) { ascii_code = (u8)'3'; }
                if(key_code == Keys.D4) { ascii_code = (u8)'4'; }
                if(key_code == Keys.D5) { ascii_code = (u8)'5'; }
                if(key_code == Keys.D6) { ascii_code = (u8)'6'; }
                if(key_code == Keys.D7) { ascii_code = (u8)'7'; }
                if(key_code == Keys.D8) { ascii_code = (u8)'8'; }
                if(key_code == Keys.D9) { ascii_code = (u8)'9'; }
                if(key_code == Keys.D0) { ascii_code = (u8)'0'; }
                if(key_code == Keys.OemMinus) { ascii_code = (u8)'-'; }
                if(key_code == Keys.Oemplus) { ascii_code = (u8)'='; }
            }

            Console.WriteLine("         Val:{0}", ascii_code);

			return ascii_code;
		}
	}
}
