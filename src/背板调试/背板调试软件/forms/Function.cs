using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LD.lib;

namespace LD.forms
{
    public partial class Function : Form
    {

        SerialPortSetting serial;

        Timer t = new Timer();

        channel select;

        channel[] chs=new channel[5];

        public Function()
        {
            InitializeComponent();
            this.debug.DoubleClick += Debug_DoubleClick;
        }

        private void Debug_DoubleClick(object sender, EventArgs e)
        {
            this.debug.Clear();
        }

        public Function(SerialPortSetting serialPortSetting):this()
        {
            serial = serialPortSetting;
            serial.onPacketReceive += Serial_onPacketReceive;
            serial.onPacketSend += Serial_onPacketSend;
            serial.onErrorByte += Serial_onErrorByte;
            chs[0] = channel1;
            chs[1] = channel2;
            chs[2] = channel3;
            chs[3] = channel4;
            chs[4] = channel5;

            foreach (channel cc in chs)
            {
                cc.set_names(new string[] {
                                "读错","读对","    ","    ","    ","红外","充满","充电",
                                "高温","弹仓","无5V","    ","重启","    ","    ","    ",
                                "借宝","电机","摆臂","红外","到位","宝坏","顶针","    "

                            });
                cc.serialPortSetting = this.serial;
                cc.beibanAddr = this.Addr;
                cc.Addr = "02";
                cc.Id = "00000000000000000000";
                cc.Onlease += Cc_Onlease;
                cc.Onreturn += Cc_Onlease;
                cc.Onopen += Cc_Onlease;
                cc.Onyunwei += Cc_Onlease;
            }

            serial.onPacketSend += Serial_onPacketSend1;

            result.DoubleClick += Result_DoubleClick;

            timer.CheckedChanged += Timer_CheckedChanged;
            t.Tick += T_Tick;
            this.updata1.Addr = this.Addr;
            this.updata1.SetSerialPort(serial);
        }

        private void Serial_onErrorByte(object sender, Ulitily.PacketArgs args)
        {
            if (this.InvokeRequired)
            {
                FlushClient fc = new FlushClient(Serial_onErrorByte);
                this.Invoke(fc, new object[] { sender, args });
            }
            else
            {
                this.result.AppendText(args.errorbyte.ToString("x2")+" ");
            }
        }

        private void Cc_Onlease(Ldpacket packet, object sender)
        {
            this.result.Clear();
        }

        private void Serial_onPacketSend1(object sender, Ulitily.PacketArgs args)
        {
            Ldpacket p = args.packet;
            if(p.cmd != Cmd.Heart_Break)
            {
                if (this.InvokeRequired)
                {
                    Ulitily.onPacketTransfer handler = new Ulitily.onPacketTransfer(Serial_onPacketSend1);
                    this.Invoke(handler, new object[] { sender,args });

                }
                else
                {
                    if (byte.Parse(this.Addr.Text, System.Globalization.NumberStyles.HexNumber) != p.addr) return;
                    string result = String.Format("->地址:{0} 命令:{1} 长度:{2} 数据:{3}\n",
                             p.addr, p.cmd, p.len, Ulitily.ShareClass.hexByteArrayToString(p.data, p.len).Replace("-", " "));
                    this.result.AppendText(result);
                }
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Button1_Click(null, null);
        }

        private void Timer_CheckedChanged(object sender, EventArgs e)
        {
            if (timer.Checked == false) t.Stop();
            else {
                t.Interval = Int32.Parse(Inter.Text);
                t.Start();
            }
        }

        private void Result_DoubleClick(object sender, EventArgs e)
        {
            result.Clear();
        }


        private void Serial_onPacketSend(object sender, Ulitily.PacketArgs args)
        {
            pv.PacketSend(sender, args);
        }


        private delegate void FlushClient(object sender, Ulitily.PacketArgs args); //代理
        private void Serial_onPacketReceive(object sender, Ulitily.PacketArgs args)
        {

            if (this.InvokeRequired)
            {
                FlushClient fc = new FlushClient(Serial_onPacketReceive);
                this.Invoke(fc,new object[] { sender,args});
            }
            else
            {
                Ldpacket p = args.packet;
                pv.PacketGet(sender, args);
                if (byte.Parse(this.Addr.Text, System.Globalization.NumberStyles.HexNumber) != p.addr)
                {
                    return;
                }
                switch(p.cmd)
                {
                    case Cmd.Heart_Break:
                         byte[] ids = new byte[10];
                        {
                       
                            int counter = (p.len - 6) / 26;
                            for(int i=0;i<counter;i++)
                            {
                                int offset = i * 26 + 6;
                                Array.Copy(p.data, i * 26 + 10, ids, 0, 10);

                                channel c = chs[i];

 
                                c.Addr = (p.data[offset]).ToString("X2");
                                c.set_states(p.data[offset+1], p.data[offset+2], p.data[offset+3]);
                                c.Id = Ulitily.ShareClass.hexByteArrayToString(p.data, offset + 4, 10).Replace("-","");
                                string ver = p.data[offset+14].ToString("X2");
                                string current = ( (((int)p.data[offset + 15]) << 8) + ((int)p.data[offset + 16])).ToString("D");
                                string dianlian = p.data[offset + 17].ToString("D2");
                                string wendu = p.data[offset + 18].ToString("D2");
                                string cc = ((((int)p.data[offset + 19]) << 8) + ((int)p.data[offset + 20])).ToString("D");
                                string vol = ((((int)p.data[offset + 21]) << 8) + ((int)p.data[offset + 22])).ToString("D");
                                string v = ((((int)p.data[offset + 23]) << 8) + ((int)p.data[offset + 24])).ToString("D");
                                string biaoji = p.data[offset + 25].ToString("X2");

                                c.Values1 = "版本:" + ver + "\n次数:" + cc + "\n容量:" + vol   + "\n标志:" + biaoji;
                                c.Values2 = "电流:" + current + "\n电量:" + dianlian + "\n电压:"+ v + "\n温度:" + wendu;

                                string aad = "模组地址:" + p.data[0].ToString("X2");
                                string bbd = "仓道数:" + p.data[1].ToString("X2");
                                string ccd = "硬件版本:" + Ulitily.ShareClass.hexByteArrayToString(p.data, 2, 2).Replace("-", "");
                                string ddd = "软件版本:" + Ulitily.ShareClass.hexByteArrayToString(p.data, 4, 2).Replace("-", "");
                                break_ack.Text = aad + new String(' ', 20 - Encoding.GetEncoding("gb2312").GetBytes(aad).Length)
                                    + bbd + new String(' ', 20 - Encoding.GetEncoding("gb2312").GetBytes(bbd).Length) + "\r\n"
                                    + ccd + new String(' ', 19 - Encoding.GetEncoding("gb2312").GetBytes(ccd).Length)
                                    + ddd + new String(' ', 20 - Encoding.GetEncoding("gb2312").GetBytes(ddd).Length);

                            }
                        }break;

                    case Cmd.DebugInfo:
                        {
                            if (p.data[0] == 0x03) {

                                DebugInfo debugInfo = new DebugInfo(p.data, 1);
                                this.debug.AppendText(debugInfo.ToString());
                            }
                        }break;
                    default:
                        string result = String.Format("<-地址:{0} 命令:{1} 长度:{2} 数据:{3}\n",
                                 p.addr, p.cmd, p.len, Ulitily.ShareClass.hexByteArrayToString(p.data, p.len).Replace("-", " "));
                        this.result.AppendText(result);
                        break;
                }
            }
        }

        public PacketView pv { get; internal set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            Ldpacket packet =  Ldpacket.Get_Ldpacket(Cmd.Heart_Break,Addr.Text, xintiao.Text);
            byte[] ss = packet.toBytes;
            serial.WritePacket(packet);
        }

        private void C_OnSelect(MouseEventArgs a, object sender)
        {
            if (select!=null) select.LostSelect();
            select = (channel)sender;
        }

        //返回选择的仓道
        channel find_a_select()
        {
            return select;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Set_Addr, "FE", tb_set_addr.Text);
            serial.WritePacket(p);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Ctrl,Addr.Text, "00"+"0000000000000000000000"+ "0A");
            serial.WritePacket(p);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.UpdateEntry, Addr.Text,"00000000000000000000000000000000000000000000");
            serial.WritePacket(p);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.DebugInfo, Addr.Text, "01"+Start.Text+Counter.Text);
            serial.WritePacket(p);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.DebugInfo, Addr.Text, "02");
            serial.WritePacket(p);
        }

    }
}
