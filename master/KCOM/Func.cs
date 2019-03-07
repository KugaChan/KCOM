using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCOM
{
    class Func
    {
        public static char GetHexHighLow(byte n, byte mode)
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

		public static byte CharToByte(char n)      //把字符转换为数字
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

        public static string TextConvert_ASCII_To_Hex(string ascii_text)
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

        public static string TextConvert_Hex_To_ASCII(string hex_text)
        {
            string ascii_show = hex_text;    //不要直接操作textBox的文本，操作内存变量要快很多!

            int n = ascii_show.Length;
            if(n != 0)
            {
                char[] chahArray = new char[n];
                chahArray = ascii_show.ToCharArray();//将字符串转换为字符数组
                ascii_show = "";

                for(int i = 2; i < n; i++)//找出所有空格，0x3F
                {
                    if(chahArray[i] == ' ')
                    {
                        byte hex_h = Func.CharToByte(chahArray[i - 2]);//3
                        byte hex_l = Func.CharToByte(chahArray[i - 1]);//F	
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

        public static string Byte_To_String(byte value)
        {
            string str = "";

            switch(value)
            {
                case 0x20: str += " "; break;
                case 0x21: str += "!"; break;
                case 0x22: str += "\""; break;
                case 0x23: str += "#"; break;
                case 0x24: str += "$"; break;
                case 0x25: str += "%"; break;
                case 0x26: str += "&"; break;
                case 0x27: str += "'"; break;
                case 0x28: str += "("; break;
                case 0x29: str += ")"; break;
                case 0x2A: str += "*"; break;
                case 0x2B: str += "+"; break;
                case 0x2C: str += ","; break;
                case 0x2D: str += "-"; break;
                case 0x2E: str += "."; break;
                case 0x2F: str += "/"; break;
                case 0x30: str += "0"; break;
                case 0x31: str += "1"; break;
                case 0x32: str += "2"; break;
                case 0x33: str += "3"; break;
                case 0x34: str += "4"; break;
                case 0x35: str += "5"; break;
                case 0x36: str += "6"; break;
                case 0x37: str += "7"; break;
                case 0x38: str += "8"; break;
                case 0x39: str += "9"; break;
                case 0x3A: str += ":"; break;
                case 0x3B: str += ";"; break;
                case 0x3C: str += "<"; break;
                case 0x3D: str += "="; break;
                case 0x3E: str += ">"; break;
                case 0x3F: str += "?"; break;
                case 0x40: str += "@"; break;
                case 0x41: str += "A"; break;
                case 0x42: str += "B"; break;
                case 0x43: str += "C"; break;
                case 0x44: str += "D"; break;
                case 0x45: str += "E"; break;
                case 0x46: str += "F"; break;
                case 0x47: str += "G"; break;
                case 0x48: str += "H"; break;
                case 0x49: str += "I"; break;
                case 0x4A: str += "J"; break;
                case 0x4B: str += "K"; break;
                case 0x4C: str += "L"; break;
                case 0x4D: str += "M"; break;
                case 0x4E: str += "N"; break;
                case 0x4F: str += "O"; break;
                case 0x50: str += "P"; break;
                case 0x51: str += "Q"; break;
                case 0x52: str += "R"; break;
                case 0x53: str += "S"; break;
                case 0x54: str += "T"; break;
                case 0x55: str += "U"; break;
                case 0x56: str += "V"; break;
                case 0x57: str += "W"; break;
                case 0x58: str += "X"; break;
                case 0x59: str += "Y"; break;
                case 0x5A: str += "Z"; break;
                case 0x5B: str += "["; break;
                case 0x5C: str += "\\"; break;
                case 0x5D: str += "]"; break;
                case 0x5E: str += "^"; break;
                case 0x5F: str += "_"; break;
                case 0x60: str += "`"; break;
                case 0x61: str += "a"; break;
                case 0x62: str += "b"; break;
                case 0x63: str += "c"; break;
                case 0x64: str += "d"; break;
                case 0x65: str += "e"; break;
                case 0x66: str += "f"; break;
                case 0x67: str += "g"; break;
                case 0x68: str += "h"; break;
                case 0x69: str += "i"; break;
                case 0x6A: str += "j"; break;
                case 0x6B: str += "k"; break;
                case 0x6C: str += "l"; break;
                case 0x6D: str += "m"; break;
                case 0x6E: str += "n"; break;
                case 0x6F: str += "o"; break;
                case 0x70: str += "p"; break;
                case 0x71: str += "q"; break;
                case 0x72: str += "r"; break;
                case 0x73: str += "s"; break;
                case 0x74: str += "t"; break;
                case 0x75: str += "u"; break;
                case 0x76: str += "v"; break;
                case 0x77: str += "w"; break;
                case 0x78: str += "x"; break;
                case 0x79: str += "y"; break;
                case 0x7A: str += "z"; break;
                case 0x7B: str += "{"; break;
                case 0x7C: str += "|"; break;
                case 0x7D: str += "}"; break;
                case 0x7E: str += "~"; break;
                default:
                {
                    byte[] array = new byte[1];
                    array[0] = value;
                    str += System.Text.Encoding.ASCII.GetString(array);
                    break;
                }
            }

            return str;
        }

        //截取字符串最后max_size长度的数据
        public static string String_Roll(string str_in, int max_size)
        {
            if(str_in.Length > max_size)
            {
                str_in = str_in.Substring(str_in.Length - max_size, max_size);
            }

            return str_in;
        }
    }
}
