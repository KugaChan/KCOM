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
				u8 ascii_code = Func_Cmdline_Key_To_ASCII(keyData);

				int coloum;
				coloum = this.textBox_ComSnd.SelectionStart;

				Console.WriteLine(">>coloum:{0}", coloum);

				//textBox_ComSnd.AppendText(Func_Cmdline_Key_To_ASCII(keyData));
				//textBox_ComSnd.Text += ((char)ascii_code).ToString();
				//textBox_ComSnd.Text.Insert(coloum, );

				string tmp_str = textBox_ComSnd.Text;
				tmp_str = tmp_str.Insert(coloum, ((char)ascii_code).ToString());
				textBox_ComSnd.Text = tmp_str;

				textBox_ComSnd.SelectionStart = coloum + 1;
				textBox_ComSnd.Focus();
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
			u32 key_add;			

			key_func = (u32)KeyCode >> 16;
			key_code = (Keys)((u32)KeyCode & (0x0000FFFFu));

			//Console.WriteLine(">>key_code:{0} key_func:{1}", key_code, key_func);

			key_add = 0;
			if((key_func & 0x0001) != 0)   //从打印中看出来的
			{
				key_add |= KEY_KEYBOARD_Shift;
			}
			if((key_func & 0x0002) != 0)
			{
				key_add |= KEY_KEYBOARD_Ctrl;
			}
			if((key_func & 0x0004) != 0)
			{
				key_add |= KEY_KEYBOARD_Alt;
			}			

			if(key_code == Keys.A) { ascii_code = (u8)'a'; }
			if(key_code == Keys.B) { ascii_code = (u8)'b'; }
			if(key_code == Keys.C) { ascii_code = (u8)'c'; }
			if(key_code == Keys.D) { ascii_code = (u8)'d'; }
			if(key_code == Keys.E) { ascii_code = (u8)'e'; }
			if(key_code == Keys.F) { ascii_code = (u8)'f'; }
			if(key_code == Keys.G) { ascii_code = (u8)'g'; }
			if(key_code == Keys.H) { ascii_code = (u8)'h'; }
			if(key_code == Keys.I) { ascii_code = (u8)'i'; }
			if(key_code == Keys.J) { ascii_code = (u8)'j'; }
			if(key_code == Keys.K) { ascii_code = (u8)'k'; }
			if(key_code == Keys.L) { ascii_code = (u8)'l'; }
			if(key_code == Keys.M) { ascii_code = (u8)'m'; }
			if(key_code == Keys.N) { ascii_code = (u8)'n'; }
			if(key_code == Keys.O) { ascii_code = (u8)'o'; }
			if(key_code == Keys.P) { ascii_code = (u8)'p'; }
			if(key_code == Keys.Q) { ascii_code = (u8)'q'; }
			if(key_code == Keys.R) { ascii_code = (u8)'r'; }
			if(key_code == Keys.S) { ascii_code = (u8)'s'; }
			if(key_code == Keys.T) { ascii_code = (u8)'t'; }
			if(key_code == Keys.U) { ascii_code = (u8)'u'; }
			if(key_code == Keys.V) { ascii_code = (u8)'v'; }
			if(key_code == Keys.W) { ascii_code = (u8)'w'; }
			if(key_code == Keys.X) { ascii_code = (u8)'x'; }
			if(key_code == Keys.Y) { ascii_code = (u8)'y'; }
			if(key_code == Keys.Z) { ascii_code = (u8)'z'; }


			if((key_code >= Keys.A) &&
				(key_code <= Keys.Z) && 
				((key_add & KEY_KEYBOARD_Shift) != 0))
			{
				ascii_code -= 32;
			}

			return ascii_code;
		}
	}
}
