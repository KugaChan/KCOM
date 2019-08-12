using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCOM
{
    class Parameter
    {
        //常量
        public const byte _VersionHSB = 9;	//重大功能更新(例如加入Netcom后，从3.0变4.0)
        public const byte _VersionMSB = 6;	//主要功能的优化
        public const byte _VersionLSB = 3;	//微小的改动
        public const byte _VersionGit = 38;	//Git版本号

        public const int _BitShift_anti_color = 0;
        public const int _BitShift_max_recv_length = 1;
        public const int _BitShift_cmdline_chk = 2;
        public const int _BitShift_run_in_backgroup = 3;
        public const int _BitShift_clear_data_when_fastsave = 4;

        public const int _BitShift_netcom_is_server = 5;    //默认为1
        public const int _BitShift_ascii_receive = 6;
        public const int _BitShift_ascii_send = 7;

        public const int _BitShift_messy_code_fliter = 8;

        public const int _BitShift_middle_mouse_clear = 9;
        public const int _BitShift_esc_clear = 10;


        public static bool GetBoolFromParameter(int parameter, int shiftbit)
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

        public static void SetBoolToParameter(ref int parameter, bool val, int shiftbit)
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
    }
}
