using System;
using System.Collections.Generic;
using System.Text;
using LD.lib;

namespace LD.lib
{


    public class Ulitily
    {


        public class PacketArgs:EventArgs{
            public Ldpacket packet;
            public byte errorbyte;
        }
       public  delegate void onPacketTransfer(object sender, PacketArgs args);


       public delegate void CommDisplayDelegate(object sender, String header,byte[] buffer, int offset, int len);


        /// <summary>
        /// 打印信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msgType"></param>
        /// <param name="msgLog"></param>
        /// <param name="options"></param>
       public delegate void LogEventHandler(object sender, string msgType, string msgLog, string options);


        /// <summary>
        /// 共享类，一些常用的方法
        /// </summary>
        public class ShareClass
        {
            #region 字符串转换
            /// <summary>
            /// string  0x123456789012-->"123456789012"
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static String Hex48ToString(long data)
            {
                byte[] b = new byte[6];
                b[0] = (byte)((data >> 40) & 0xff);
                b[1] = (byte)((data >> 32) & 0xff);
                b[2] = (byte)((data >> 24) & 0xff);
                b[3] = (byte)((data >> 16) & 0xff);
                b[4] = (byte)((data >> 8) & 0xff);
                b[5] = (byte)((data >> 0) & 0xff);
                return hexByteArrayToString(b).ToString().Replace("-", "");
            }

            /// <summary>
            /// "123456789012"-->0x123456789012
            /// </summary>
            /// <param name="data"></param>
            /// <param name="outdata"></param>
            /// <returns></returns>
            public static String StringToHex48(String data, ref long outdata)
            {
                byte[] d = strToHexByteArray(data, 6 * 2);
                if (d != null)
                {
                    long dd = (long)d[0]; dd <<= 8;
                    dd += (long)d[1]; dd <<= 8;
                    dd += (long)d[2]; dd <<= 8;
                    dd += (long)d[3];dd <<= 8;
                    dd += (long)d[4];dd <<= 8;
                    dd += (long)d[5];
                    outdata = dd;
                    return Hex48ToString(dd);

                }
                return null;
            }






            /// <summary>
            /// UInt32 --> string  0x12345678-->"12345678"
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static String Hex32ToString(UInt32 data){
                byte[] b = new byte[4];
                b[0] = (byte)((data>>24)&0xff);
                b[1] = (byte)((data>>16)&0xff);
                b[2] = (byte)((data>>8)&0xff);
                b[3] = (byte)((data>>0)&0xff);
                return hexByteArrayToString(b).ToString().Replace("-","");
            }


            /// <summary>
            /// "12345678"-->0x12345678
            /// </summary>
            /// <param name="data"></param>
            /// <param name="outdata"></param>
            /// <returns>null Error,返回"12345678"</returns>
            public static String StringToHex32(String data,ref UInt32 outdata)
            {
                byte[] d = strToHexByteArray(data, 4 * 2);
                if (d != null)
                {
                    UInt32 dd = (UInt32)d[0]; dd <<= 8;
                    dd += (UInt32)d[1]; dd <<= 8;
                    dd += (UInt32)d[2]; dd <<= 8;
                    dd += (UInt32)d[3];
                    outdata = dd;
                    return Hex32ToString(dd);

                }
                return null;
            }

            /// <summary>
            ///  0x1234--->"1234"
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static String Hex16ToString(UInt16 data)
            {
                byte[] b = new byte[2];
                b[0] = (byte)((data >> 8) & 0xff);
                b[1] = (byte)((data >> 0) & 0xff);
                return hexByteArrayToString(b).ToString().Replace("-", "");
            }


            /// <summary>
            /// "1234"---0x1234
            /// </summary>
            /// <param name="data"></param>
            /// <param name="outdata"></param>
            /// <returns></returns>
            public static String StringToHex16(String data, ref UInt16 outdata)
            {
                byte[] d = strToHexByteArray(data, 2 * 2);
                if (d != null)
                {
                    UInt16 dd = (UInt16)d[0]; dd <<= 8;
                    dd += (UInt16)d[1];
                    outdata = dd;
                    return Hex16ToString(dd);

                }
                return null;
            }






            /// <summary>
            /// byte[] 转 字符串 0x11-->"00010001"
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static string ByteArrayToString2(byte[] data)
            {
                if (data == null) return null;
                try
                {
                    byte[] b = data;
                    int left = 0;
                    int index = 0;
                    StringBuilder str = new StringBuilder();
                    foreach (byte bs in b)
                    {
                        str.Append(Convert.ToString(bs, 2));
                        left = str.Length % 8;
                        index = left;

                        if (left != 0)
                        {
                            left = 8 - left;
                            while (left > 0)
                            {
                                str.Insert(str.Length - index, '0');
                                left--;
                            }
                        }
                        //str.Append(Inter.toBinaryString(bs));//转换为二进制  
                    }
                    return str.ToString();
                }
                catch { return null; }
            }
            /// <summary>
            /// 二进制字符串 to byte[] "11110000"-->0xF0
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static byte[] String2ToByteArray(string str)
            {
                if (str == null) return null;
                str = str.Replace(" ", "");

                int left = 8 - str.Length % 8;
                StringBuilder strBuilder = new StringBuilder(str);
                //补0
                while (left > 0)
                {
                    strBuilder.Append("0");
                    left--;
                }

                left = 0;

                byte[] result = new byte[strBuilder.ToString().Length / 8];
                result.Initialize();
                foreach (char c in strBuilder.ToString())
                {
                    switch (c)
                    {
                        case '1':
                        case '0':
                            break;
                        default: return null;
                    }

                    result[left / 8] += (byte)(((byte)(c == '1' ? 1 : 0)) << (7 - left % 8));

                    left++;

                }

                return result;

            }


            /// <summary>
            /// 字符串转16进制字节数组
            /// </summary>
            /// <param name="hexString"></param>
            /// <returns></returns>
            public static byte[] strToHexByteArray(string hexString)
            {
                hexString = hexString.Replace(" ", "");
                hexString = hexString.Replace("\r", "");
                hexString = hexString.Replace("\n", "");
                if ((hexString.Length % 2) != 0)
                    hexString += "0";
                byte[] returnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                return returnBytes;
            }

            public static byte[] strToBinByteArray(string binstring)
            {
                binstring = binstring.Replace(" ", "");
                binstring = binstring.Replace("\r", "");
                binstring = binstring.Replace("\n", "");
                if ((binstring.Length % 2) != 0)
                    binstring = "0"+binstring;
                byte[] returnBytes = new byte[binstring.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(binstring.Substring(i * 2, 2), 16);
                return returnBytes;

            }


            /// <summary>
            ///  字符串转16进制字节数组  "01234567"-->0x01234567
            /// </summary>
            /// <param name="hexString"></param>
            /// <param name="len">字符长度</param>
            /// <returns></returns>
            public static byte[] strToHexByteArray(string hexString, int len)
            {
                try
                {
                    string s = hexString.Substring(0, len);
                    return strToHexByteArray(s);
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }


            /// <summary>
            /// 数组转字符串
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static string hexByteArrayToString(byte[] data)
            {
                try
                {
                    return BitConverter.ToString(data);
                }
                catch { return ""; }
            }

            public static string hexByteArrayToString(byte[] data ,int len)
            {
                    if(len <=0 || data ==null)return null;
                    else{
                        byte[] datainput = new byte[len];
                        Array.Copy(data, datainput, len);
                        return hexByteArrayToString(datainput);
                    }
            }

            public static string hexByteArrayToString(byte[] data, int offset, int len) {
                if (len <= 0 || data == null) return null;
                if (offset + len > data.Length) len = data.Length - offset;
                byte[] d = new byte[len];
                Array.Copy(data, offset, d, 0, d.Length);
                return hexByteArrayToString(d);
            }

            public static void ReversedBytes(byte[] data,int len)
            {
                if(data == null )return ;
                if (len > data.Length) len = data.Length;
                int i = 0;
                while (i<len/2)
                {
                    byte tmp = data[i];
                    data[i] = data[len - 1-i];
                    data[len - 1 - i] = tmp;
                    i += 1;
                }
            }

            /// <summary>
            /// Uint16 copy to buffer[offset]
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <returns>0:error   1:OK</returns>
            public static int Uint16ToBytes(UInt16 v, byte[] buffer, int offset,byte H33)
            {
                if (buffer == null || buffer.Length < offset + 1) return 0;
                buffer[offset] = (byte)v;  buffer[offset] += H33;
                buffer[offset + 1] = (byte)(v >> 8);buffer[offset + 1] += H33;
                return 1;
            }

            /// <summary>
            /// buffer[offset] copy UInt16 to v
            /// </summary>
            /// <param name="v">输出数据</param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <returns>0:error  1:ok</returns>
            public static int BytesToUInt16(byte[] buffer, int offset, ref UInt16 v,byte H33)
            {
                if (buffer == null || buffer.Length < offset + 1) return 0;

                v = (UInt16)(buffer[offset + 1]-H33);
                v <<= 8;
                v += (UInt16)(buffer[offset] -H33);
                return 1;
            }


            /// <summary>
            /// 把 v copy 到buffer[offset]的地方
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <returns>0:error   1:OK</returns>
            public static int UInt32ToBytes(UInt32 v, byte[] buffer, int offset,byte H33)
            {
                if (buffer == null || buffer.Length < offset + 3) return 0;
                buffer[0 + offset] = (byte)(v >> 0); buffer[offset + 0] += H33;
                buffer[1 + offset] = (byte)(v >> 8); buffer[offset + 1] += H33;
                buffer[2 + offset] = (byte)(v >> 16);buffer[offset + 2] += H33;
                buffer[3 + offset] = (byte)(v >> 24);buffer[offset + 3] += H33;

                return 1;
            }

            /// <summary>
            /// 在 buffset[offset]的位置 读取一个Uint32
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <returns>0:error  1:OK</returns>
            public static int BytesToUInt32(ref UInt32 v, byte[] buffer, int offset,byte H33)
            {
                if (buffer == null || buffer.Length < offset + 3) return 0;
                v = (byte)(buffer[offset + 3] - H33); v <<= 8;
                v += (byte)(buffer[offset + 2] - H33); v <<= 8;
                v += (byte)(buffer[offset + 1] - H33); v <<= 8;
                v += (byte)(buffer[offset + 0] - H33);

                return 1;

            }

            /// <summary>
            /// 48bits--->byte[]
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="H33"></param>
            /// <returns></returns>
            public static int UInt48ToBytes(UInt64 v,byte[] buffer,int offset,byte H33)
            {
                if (buffer == null || buffer.Length < offset + 5) return 0;
                buffer[0 + offset] = (byte)(v >> 0); buffer[offset + 0] += H33;
                buffer[1 + offset] = (byte)(v >> 8); buffer[offset + 1] += H33;
                buffer[2 + offset] = (byte)(v >> 16); buffer[offset + 2] += H33;
                buffer[3 + offset] = (byte)(v >> 24); buffer[offset + 3] += H33;
                buffer[4 + offset] = (byte)(v >> 32); buffer[offset + 4] += H33;
                buffer[5 + offset] = (byte)(v >> 40); buffer[offset + 5] += H33;
                return 1;
            }


            /// <summary>
            /// byte[]-------->48bits Uint64
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="H33"></param>
            /// <returns></returns>
            public static int BytesToUInt48(ref UInt64 v, byte[] buffer, int offset, byte H33)
            {
                if (buffer == null || buffer.Length < offset + 5) return 0;
                v = 0;
                v += (byte)(buffer[offset + 5] - H33); v <<= 8;
                v += (byte)(buffer[offset + 4] - H33); v <<= 8;
                v += (byte)(buffer[offset + 3] - H33); v <<= 8;
                v += (byte)(buffer[offset + 2] - H33); v <<= 8;
                v += (byte)(buffer[offset + 1] - H33); v <<= 8;
                v += (byte)(buffer[offset + 0] - H33); 
                return 1;
            }



            /// <summary>
            /// 64bits--->byte[]
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="H33"></param>
            /// <returns></returns>
            public static int UInt64ToBytes(UInt64 v, byte[] buffer, int offset, byte H33)
            {
                if (buffer == null || buffer.Length < offset + 5) return 0;
                buffer[0 + offset] = (byte)(v >> 0); buffer[offset + 0] += H33;
                buffer[1 + offset] = (byte)(v >> 8); buffer[offset + 1] += H33;
                buffer[2 + offset] = (byte)(v >> 16); buffer[offset + 2] += H33;
                buffer[3 + offset] = (byte)(v >> 24); buffer[offset + 3] += H33;
                buffer[4 + offset] = (byte)(v >> 32); buffer[offset + 4] += H33;
                buffer[5 + offset] = (byte)(v >> 40); buffer[offset + 5] += H33;
                buffer[6 + offset] = (byte)(v >> 48); buffer[offset + 6] += H33;
                buffer[7 + offset] = (byte)(v >> 56); buffer[offset + 7] += H33;
                return 1;
            }

            /// <summary>
            /// byte[]-------->64bits Uint64
            /// </summary>
            /// <param name="v"></param>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="H33"></param>
            /// <returns></returns>
            public static int BytesToUInt64(ref UInt64 v, byte[] buffer, int offset, byte H33)
            {
                if (buffer == null || buffer.Length < offset + 5) return 0;
                v = 0;
                v += (byte)(buffer[offset + 7] - H33); v <<= 8;
                v += (byte)(buffer[offset + 6] - H33); v <<= 8;
                v += (byte)(buffer[offset + 5] - H33); v <<= 8;
                v += (byte)(buffer[offset + 4] - H33); v <<= 8;
                v += (byte)(buffer[offset + 3] - H33); v <<= 8;
                v += (byte)(buffer[offset + 2] - H33); v <<= 8;
                v += (byte)(buffer[offset + 1] - H33); v <<= 8;
                v += (byte)(buffer[offset + 0] - H33);
                return 1;
            }

            #endregion

            #region IP address
            /// <summary>
            /// 反转,data[] = {11,1,168,192}===>return 0xC0A0010B
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static UInt32 ReverseUInt32(byte[] data) {
                if (data == null || data.Length < 4) return 0;
                UInt32 tmp = data[3]; tmp <<= 8;
                tmp += data[2]; tmp <<= 8;
                tmp += data[1]; tmp <<= 8;
                tmp += data[0];
                return tmp;
            }

            /// <summary>
            /// 反转,0xC0A0010B===>data[] = {11,1,168,192}
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static byte[]  ReverseAddress(UInt32 address)
            {
                byte[] addr = new byte[4];
                addr[0] = (byte) address;
                addr[1] = (byte)(address >> 8);
                addr[2] = (byte)(address >> 16);
                addr[3] = (byte)(address >> 24);
                return addr;
            }
            #endregion

            #region 数据格式

            /// <summary>
            /// 07 97 数据帧 to di
            /// </summary>
            /// <param name="data"></param>
            /// <param name="len"></param>
            /// <returns></returns>
            public static UInt32 data2Di(byte[] data, int len)
            {
                UInt32 di = 0;
                if (len == 2) { di += data[1]; di <<= 8; di += data[0]; return di; }
                else if (len == 4)
                {
                    di += data[3]; di <<= 8;
                    di += data[2]; di <<= 8;
                    di += data[1]; di <<= 8;
                    di += data[0];
                    return di;
                }
                else return 0;
            }
            #endregion



            #region 校验和

            /// <summary>
            /// 以16 位为单位计算 data 的检验各，用来icmp tcp 检验计算,摘自 contiki kernel
            /// </summary>
            /// <param name="data"></param>
            /// <param name="len"></param>
            /// <returns></returns>
            public static UInt16 CheckSum16(UInt16 sum,byte[] data, int len) {

                int dataptr;
                int last_byte;
                UInt16 t;
                dataptr = 0;
                last_byte = len - 1;

                while (dataptr < last_byte)
                {   /* At least two more bytes */
                    t = (UInt16)((data[0 + dataptr] << 8) + data[1 + dataptr]);
                    sum += t;
                    if (sum < t)
                    {
                        sum++;      /* carry */
                    }
                    dataptr += 2;
                }

                if (dataptr == last_byte)
                {
                    t = (UInt16)((data[0 + dataptr] << 8) + 0);
                    sum += t;
                    if (sum < t)
                    {
                        sum++;      /* carry */
                    }
                }

                /* Return sum in host byte order. */
                return sum;
            }

            /// <summary>
            /// 8 位 累加和
            /// </summary>
            /// <param name="data"></param>
            /// <param name="offset"></param>
            /// <param name="len"></param>
            /// <returns></returns>
            public static byte CheckSum8(byte[] data, int offset, int len)
            {
                if (data == null) return 0;
                if (data.Length <= offset) return 0;
                if (data.Length < offset + len) len = data.Length - offset;
                int i = offset;
                byte cs = 0;
                for (; i < len; i++)
                {
                    cs += data[offset + i];
                }

                return cs;
            }
            #endregion


        }

    }
}

