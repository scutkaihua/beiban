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
        }

        public Function(SerialPortSetting serialPortSetting):this()
        {
            serial = serialPortSetting;
            serial.onPacketReceive += Serial_onPacketReceive;
            serial.onPacketSend += Serial_onPacketSend;
            chs[0] = channel1;
            chs[1] = channel2;
            chs[2] = channel3;
            chs[3] = channel4;
            chs[4] = channel5;

            foreach (channel cc in chs)
            {
                cc.set_names(new string[] {
                                "读错","读对","null","null","null","红外","充满","正充电",
                                "温度高","弹仓","5V无","null","重启","null","null","null",
                                "借宝","电机","摆臂","红外","到位","来电宝","顶针","null"

                            });
                cc.OnSelect += Cc_OnSelect;
            }

            serial.onPacketSend += Serial_onPacketSend1;

            result.DoubleClick += Result_DoubleClick;

            timer.CheckedChanged += Timer_CheckedChanged;
            t.Tick += T_Tick;
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

        private void Cc_OnSelect(MouseEventArgs a, object sender)
        {
            if(this.InvokeRequired)
            {
                SelectEventHandler handler = new SelectEventHandler(Cc_OnSelect);
                this.Invoke(handler, new object[] { a, sender });
            }
            else
            {
                channel c = (channel)sender;
                //if (c.is_select())
                {
                    l_addr.Text = c.Addr;
                    l_id.Text = c.Id;
                }
            }
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

                                c.Values = "版本:" + ver + "\n电流:" + current + "\n电量:" + dianlian + ",温度:" + wendu + "\n次数:" + cc + ",容量:" + vol + "\n电压:" + v + ",标志:" + biaoji;
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
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Ctrl,Addr.Text, "00"+"0000000000000000000000"+ctrl_time.Text);
            serial.WritePacket(p);
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Ctrl, Addr.Text, "01" + ctrl_addr.Text + ctrl_id.Text + ctrl_time.Text);
            serial.WritePacket(p);
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Ctrl, Addr.Text, "02"+ ctrl_addr.Text + ctrl_id.Text + ctrl_time.Text);
            serial.WritePacket(p);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Return, Addr.Text, r_addr.Text + r_time.Text);
            serial.WritePacket(p);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Lease, Addr.Text, l_addr.Text + l_id.Text+ l_time.Text);
            serial.WritePacket(p);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            result.Clear();
            string[] lines = update.Lines;
            string t="";
            foreach (string c in lines) t = t + c;
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Updata, Addr.Text, t);
            serial.WritePacket(p);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Return, Addr.Text, r1_addr.Text + r1_time.Text);
            serial.WritePacket(p);

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Return, Addr.Text, r2_addr.Text + r2_time.Text);
            serial.WritePacket(p);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Lease, Addr.Text, l1_addr.Text + l1_id.Text + l1_time.Text);
            serial.WritePacket(p);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Lease, Addr.Text, l2_addr.Text + l2_id.Text + l2_time.Text);
            serial.WritePacket(p);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Lease, Addr.Text, l3_addr.Text + l3_id.Text + l3_time.Text);
            serial.WritePacket(p);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            result.Clear();
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.Lease, Addr.Text, l4_addr.Text + l4_id.Text + l4_time.Text);
            serial.WritePacket(p);
        }
    }
}
