using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.lib
{
    public class Ldpacket
    {


        byte[] buffer = new byte[1024];

        public byte AA
        {
            get
            {
                if (buffer[0] != 0xAA) throw new Exception("no AA");
                return buffer[0];
            }
            set
            {
                buffer[0] = value;
            }
        }
        public byte BB
        {
            get
            {
                if (buffer[1] != 0xBB) throw new Exception("no BB");
                return buffer[1];
            }
            set
            {
                buffer[1] = value;
            }
        }

        public byte addr {
            get { return buffer[2]; }
            set { buffer[2] = value; }
        }

        public Cmd cmd {
            get { return (Cmd)buffer[3]; }
            set { buffer[3] = (byte)value; }
        }

        public int len
        {
            get { return ((int)(buffer[4] << 8) + ((int)buffer[5])); }
            set { buffer[5] = (byte)(value & 0xFF);buffer[4] = (byte)((value >> 8) & 0xFF); }
        }

        public byte[] data
        {
            get { return buffer.Skip(6).ToArray(); }
            set { Array.Copy(value, 0, buffer, 6, value.Length); }
        }

        public byte cs
        {
            get { int i = 0;byte c = 0; for (; i < len + 6; i++) { c += buffer[i]; }return c; }
            set { buffer[6 + len] = cs; }
        }

        public int size { get { return len + 7; } }


        public Ldpacket(byte[] b,int l)
        {
            Array.Copy(b, 0, buffer, 0, l);
        }

        public Ldpacket()
        {
            AA = 0xaa;
            BB = 0xbb;
        }
        public Ldpacket(Cmd c):this()
        {
            cmd = c;
        }
        public Ldpacket(Cmd c,string a):this(c)
        {
            addr = byte.Parse(a,System.Globalization.NumberStyles.HexNumber);
        }

        public Ldpacket (Cmd c,string a,byte[]d,int o,int s):this(c,a)
        {
            Array.Copy(d, o, buffer, 6, s);
            len = s;
        }
        static public Ldpacket Get_Ldpacket(Cmd c,string a,String s)
        {
            s = s.Replace(" ", "");
            byte[] bb = Ulitily.ShareClass.strToHexByteArray(s);
           return new Ldpacket(c, a, bb, 0, bb.Length);    
        }


        static int l = 0;
        static int line = 0;
        static int datalen = 0;
        static byte[] buf = new byte[1024];
        public static Ldpacket toPackcet(byte c,ref bool er)
        {
            er = false;
            buf[l] = c;
            l++;
            if (l >= 1000) goto Error;
            switch (line)
            {
                case 0:
                    if (c != 0xAA)
                        goto Error;
                    line++;
                    break;
                case 1:
                      if (c != 0xBB)
                        goto Error;
                    line++;
                    break;
                case 2: case 3:
                    line++;
                    break;
                case 4:
                    datalen = (int)(c << 8);
                    line++;
                    break;
                case 5:
                    datalen += c;
                    line++;
                    break;
                case 6:
                    if((datalen+7)==l)
                    {
                        byte cs = Ulitily.ShareClass.CheckSum8(buf, 0, l-1);
                        if (cs == c)
                        {
                            Ldpacket p = new Ldpacket(buf, l);
                            Array.Clear(buf,0,buf.Length);
                            l = line = datalen = 0;
                            return p;
                        }
                        else
                            goto Error;
                    }break;
                default:
                    goto Error;
            }

            return null;
        Error:
            //Console.Write("Line:"+line.ToString());
            Array.Clear(buf, 0, buf.Length);
            er = true;l = line = 0;return null;
        }

        public byte[] toBytes
        {
            get
            {
                buffer[6 + len] = Ulitily.ShareClass.CheckSum8(buffer, 0, 6 + len);
                int lll = 7 + len;
                return buffer.Take(lll).ToArray();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //return base.ToString();
            sb.Append("背板地址:" + BitConverter.ToString(new byte[] { addr }) + "  命令码 :" + cmd + "  数据长度:" + len +"  ");
            switch (cmd)
            {
                case Cmd.Heart_Break:
                    if (this.len < 10)
                    {
                        //下发
                        sb.Append("充电使能:" + ((this.data[0] == 1) ? ("使能") : ("禁止")) + "   ");
                        sb.Append("充电模式:" + ((this.data[1] == 1) ? ("强制") : ("默认")) + "   ");
                        sb.Append("使能数据:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 2, this.len-2).Replace("-", " "));
                    }
                    else
                    {
                        byte[] ids = new byte[10];
                        int counter = (this.len - 6) / 26;

                        string aad = "模组地址:" + this.data[0].ToString("X2");
                        string bbd = "仓道数:" + this.data[1].ToString("X2");
                        string ccd = "硬件版本:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 2, 2).Replace("-", "");
                        string ddd = "软件版本:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 4, 2).Replace("-", "");
                        string t = aad + new String(' ', 20 - Encoding.GetEncoding("gb2312").GetBytes(aad).Length)
                            + bbd + new String(' ', 13 - Encoding.GetEncoding("gb2312").GetBytes(bbd).Length) + "   "
                            + ccd + new String(' ', 13 - Encoding.GetEncoding("gb2312").GetBytes(ccd).Length)
                            + ddd + new String(' ', 13 - Encoding.GetEncoding("gb2312").GetBytes(ddd).Length);
                        sb.Append(t + "\n");
                        for (int i = 0; i < counter; i++)
                        {
                            int offset = i * 26 + 6;
                            Array.Copy(this.data, i * 26 + 10, ids, 0, 10);
                            string ch_addr = "仓道地址:" + (this.data[offset]).ToString("X2");
                            string ch_state = "  状态:" + (this.data[offset + 1]).ToString("X2")
                                            + "  告警:" + (this.data[offset + 2]).ToString("X2")
                                            + "  错误:" + (this.data[offset + 3]).ToString("X2");
                            string id = "id :" + Ulitily.ShareClass.hexByteArrayToString(this.data, offset + 4, 10).Replace("-", " ");

                            sb.Append(ch_addr + "   " + ch_state + "   " + id + "   ");

                            string ver = this.data[offset + 14].ToString("X2");
                            string current = ((((int)this.data[offset + 15]) << 8) + ((int)this.data[offset + 16])).ToString("D");
                            string dianlian = this.data[offset + 17].ToString("D2");
                            string wendu = this.data[offset + 18].ToString("D2");
                            string cc = ((((int)this.data[offset + 19]) << 8) + ((int)this.data[offset + 20])).ToString("D");
                            string vol = ((((int)this.data[offset + 21]) << 8) + ((int)this.data[offset + 22])).ToString("D");
                            string v = ((((int)this.data[offset + 23]) << 8) + ((int)this.data[offset + 24])).ToString("D");
                            string biaoji = this.data[offset + 25].ToString("X2");

                            string Values1 = "版本:" + ver + "   次数:" + cc + "   容量:" + vol + "   标志:" + biaoji;
                            string Values2 = "电流:" + current + "   电量:" + dianlian + "   电压:" + v + "   温度:" + wendu;
                            sb.Append(Values1 + "   " + Values2 + "\n");
                        }
                    }
                    break;

                case Cmd.Lease:
                    if (this.len == 12) {
                        //下发
                        sb.Append("宝地址:" + this.data[0].ToString("X2") + "   ");
                        sb.Append("id :" + Ulitily.ShareClass.hexByteArrayToString(this.data, 1, 10).Replace("-", " ") + "   ");
                        sb.Append("时间:" + this.data[11].ToString("X2") + "   ");
                    } else if (this.len == 13)
                    { 
                        sb.Append("子命令:" + this.data[0].ToString("X2") + "   ");
                        sb.Append("结果 :" + (Lease_Error)(this.data[1]) + "   ");
                        sb.Append("宝地址:" + this.data[2].ToString("X2") + "   ");
                        sb.Append("id :" + Ulitily.ShareClass.hexByteArrayToString(this.data, 3, 10).Replace("-", " "));
                        sb.Append("\n");
                    }
                    else
                        sb.Append("数据:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 0, this.len).Replace("-", " ") + "   ");
                    break;

                case Cmd.Ctrl:
                    if (this.len == 13)
                    {
                        //下发
                        sb.Append("命令:" + (Ctrl)this.data[0] +"   ");
                        sb.Append("宝地址:" + this.data[1].ToString("X2") + "   ");
                        sb.Append("id :" + Ulitily.ShareClass.hexByteArrayToString(this.data, 2, 10).Replace("-", " ")+ "   ");
                        sb.Append("时间:" + this.data[12].ToString("X2"));
                    }
                    else if (this.len == 3)
                    {
                        sb.Append("子命令:" + this.data[0].ToString("X2") + "   ");
                        sb.Append("命令:" + (Ctrl)this.data[1] + "   ");
                        sb.Append("结果 :" + ((this.data[1]==0)?("失败"):("成功")) + "\n");
                    }else
                        sb.Append("数据:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 0, this.len).Replace("-", " ") + "   ");
                    break;
                default:
                    sb.Append("数据:" + Ulitily.ShareClass.hexByteArrayToString(this.data, 0, this.len).Replace("-", " ")+"   ");
                    break;
            }
            
            return sb.ToString();
      
        }
    }
}
