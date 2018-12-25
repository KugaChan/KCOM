using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCOM
{
    class Func
    {
        public char GetHexHighLow(byte n, byte mode)
        {
            char result = ' ';
            int check;
            if(mode == 0)
            {
                check = n >> 4;//返回高位
            }
            else
            {
                check = n & 0x0F;
            }

            switch(check)
            {
                case 0: result = '0'; break;
                case 1: result = '1'; break;
                case 2: result = '2'; break;
                case 3: result = '3'; break;
                case 4: result = '4'; break;
                case 5: result = '5'; break;
                case 6: result = '6'; break;
                case 7: result = '7'; break;
                case 8: result = '8'; break;
                case 9: result = '9'; break;
                case 10: result = 'A'; break;
                case 11: result = 'B'; break;
                case 12: result = 'C'; break;
                case 13: result = 'D'; break;
                case 14: result = 'E'; break;
                case 15: result = 'F'; break;
            }

            return result;
        }

		public byte CharToByte(char n)      //把字符转换为数字
		{
			byte result;
			switch(n)
			{
				case '0': result = 0; break;
				case '1': result = 1; break;
				case '2': result = 2; break;
				case '3': result = 3; break;
				case '4': result = 4; break;
				case '5': result = 5; break;
				case '6': result = 6; break;
				case '7': result = 7; break;
				case '8': result = 8; break;
				case '9': result = 9; break;
				case 'A': result = 10; break;
				case 'B': result = 11; break;
				case 'C': result = 12; break;
				case 'D': result = 13; break;
				case 'E': result = 14; break;
				case 'F': result = 15; break;
				case 'a': result = 10; break;
				case 'b': result = 11; break;
				case 'c': result = 12; break;
				case 'd': result = 13; break;
				case 'e': result = 14; break;
				case 'f': result = 15; break;
				default: result = 0xFF; break;
			}

			return result;
		}

        public string TextConvert_ASCII_To_Hex(string ascii_text)
        {
            string hex_show = "";                   //不要直接操作textBox的文本，操作内存变量要快很多!

            int n = ascii_text.Length;
            if(n != 0)
            {
                char[] chahArray = new char[n];
                chahArray = ascii_text.ToCharArray();  //将字符串转换为字符数组
                
                for(int i = 0; i < n; i++)
                {
                    char high_char = GetHexHighLow((byte)chahArray[i], 0);
                    char low_char = GetHexHighLow((byte)chahArray[i], 1);
                    //Console.WriteLine("i:{0}|{1} H:{2} L:{3}", i, n, high_char, low_char);

                    hex_show += high_char;
                    hex_show += low_char;
                    hex_show += " ";
                }

                ascii_text = hex_show;
            }

            return hex_show;
        }

        public string TextConvert_Hex_To_ASCII(string hex_text)
        {
            string ascii_show = hex_text;    //不要直接操作textBox的文本，操作内存变量要快很多!

            int n = ascii_show.Length;
            if(n != 0)
            {
                char[] chahArray = new char[n];
                chahArray = ascii_show.ToCharArray();//将字符串转换为字符数组
                ascii_show = "";

                Func func = new Func();
                for(int i = 2; i < n; i++)//找出所有空格，0x3F
                {
                    if(chahArray[i] == ' ')
                    {
                        byte hex_h = func.CharToByte(chahArray[i - 2]);//3
                        byte hex_l = func.CharToByte(chahArray[i - 1]);//F	
                        int hex = hex_h << 4 | hex_l;

                        if((hex == 0x00) || (hex > 0x7F))   //不输入ASCII码的，要保留的原始值的，变成'？'不应该
                        {
                            ascii_show += '?';
                        }
                        else
                        {
                            ascii_show += (char)hex;
                        }
                    }
                }                
            }

            return ascii_show;
        }
    }
}
