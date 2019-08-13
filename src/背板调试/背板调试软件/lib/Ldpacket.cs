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
            switch(line)
            {
                case 0: if (c != 0xAA)
                        goto Error;
                    line++;
                    break;
                case 1:if (c != 0xBB)
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
                    if((datalen+7)<=l)
                    {
                        byte cs = Ulitily.ShareClass.CheckSum8(buf, 0, l-1);
                        if (cs == c)
                        {
                            Ldpacket p = new Ldpacket(buf, l);
                            l = line = datalen = 0;
                            return p;
                        }
                        else goto Error;
                    }break;
                default:goto Error;
            }

            return null;
        Error:
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
    }
}
