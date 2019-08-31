using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.lib
{
    public enum DType{
		IR_Error_Header = 1,
		IR_Error_Data,
		Read_Error_Clear,
		BaiBi_Invalid,

		ID_0,
		ID_Not_0,
		PCH_NULL,
		NO_MODE,
    } ;

    public enum MODE { NO_MODE=0,IIC=1,IR=2};
    public class DebugInfo
    {

        private byte[] d=new byte[7];

		public string ch {
            get
            {

                if ((d[0] & 0x80) == 0x80)
                {
                    switch (d[0] & 0x7F)
                    {
                        case 6: return "ch1";
                        case 16: return "ch2";
                        case 26: return "ch3";
                        case 36: return "ch4";
                        case 46: return "ch5";
                        case 56: return "ch6";
                        default: return d[0].ToString();
                    }
                }
                else
                {
                    return "addr:" + Ulitily.ShareClass.hexByteArrayToString(d, 1).Replace("-","");
                }
            }
        }

		public DType type { get { return (DType)(d[1] & 0xF); } set { d[1] &= 0xF0;d[1] |= (byte)value; } }

		public MODE mode { get { return (MODE)(0x03 & (d[1] >> 4)); } set { d[1] &= 0xCF; d[1] |= (byte)((byte)value<<4); } }

        public IRCMD cmd { get { return (IRCMD)d[2]; } set { d[2] = (byte)value; } }

		public string time { get {

                int i = d[6];i <<= 8;i += d[5];i <<= 8; i += d[4];i <<= 8;i += d[3];

                i /= 1000;

                return "[h:" + i / 3600 + " m:" + ((i % 3600) / 60)/10 + ((i % 3600) / 60)%10 + " s:" + (i/60)/10 + i % 10 + "]";

            } }
		public DebugInfo(byte[] dd,int offset)
        {
            if (dd == null || dd.Length < (7+offset)) return;
            Array.Copy(dd,offset, d,0, 7);
        }

        public override string ToString()
        {
            return time + " " + ch + "  type:" + type + "  mode:" + mode +"  cmd:"+cmd+"\n";
        }
    }
}
