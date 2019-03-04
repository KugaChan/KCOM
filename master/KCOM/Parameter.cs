using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class Parameter
    {
        //常量
        public const u8 _VersionHSB = 7;	//重大功能更新(例如加入Netcom后，从3.0变4.0)
        public const u8 _VersionMSB = 8;	//主要功能的优化
        public const u8 _VersionLSB = 7;	//微小的改动
        public const u8 _VersionGit = 25;	//Git版本号

        public const int _BitShift_anti_color = 0;
        public const int _BitShift_max_recv_length = 1;
        public const int _BitShift_cmdline_chk = 2;
        public const int _BitShift_run_in_backgroup = 3;
        public const int _BitShift_clear_data_when_fastsave = 4;

        public const int _BitShift_netcom_is_server = 5;    //默认为1
        public const int _BitShift_com_send_ascii = 6;      //默认为1
        public const int _BitShift_com_recv_ascii = 7;      //默认为1

        public const int _BitShift_messy_code_fliter = 8;

        public const int _BitShift_middle_mouse_clear = 9;
        public const int _BitShift_ESC_clear = 10;

        public bool GetBoolFromParameter(int parameter, int shiftbit)
        {
            bool res;
            
            if((parameter & (1 << shiftbit)) != 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }

            //Console.WriteLine("Get:{0:X} {1:X} res:{2:X}", parameter, shiftbit, res);

            return res;
        }

        public void SetBoolToParameter(ref int parameter, bool val, int shiftbit)
        {
            int res;

            if (val == true)
            {
                parameter |= 1 << shiftbit;
            }
            else
            {
                parameter &= ~(1 << shiftbit);
            }

            res = parameter;
        }

        public bool netcom_is_server;
        public bool com_send_ascii;
        public bool com_recv_ascii;
    }
}
