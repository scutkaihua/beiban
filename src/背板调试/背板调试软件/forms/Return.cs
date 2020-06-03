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
    public partial class Return : Form
    {
        SerialPortSetting serial;

        public Return()
        {
            
            InitializeComponent();
        }

        public Return(SerialPortSetting s):this()
        {
            serial = s;
            serial.onPacketReceive += Serial_onPacketReceive;
        }

        private void Serial_onPacketReceive(object sender, lib.Ulitily.PacketArgs args)
        {
            if (EN.Checked == false) return;
            Ldpacket ldpacket = args.packet;

            try
            {
                if (ldpacket.cmd == lib.Cmd.心跳 && ldpacket.len < 10 && ldpacket.addr == byte.Parse(Addr.Text, System.Globalization.NumberStyles.HexNumber))
                {
                    //发送心跳包
                    Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.心跳, Addr.Text, Addr.Text+"04"+"01050050" +
                        C1.Text + "020000" + ID1.Text + "000000000000000000000000" +
                        C3.Text + "020000" + ID3.Text + "000000000000000000000000" +
                        C5.Text + "020000" + ID5.Text + "000000000000000000000000" +
                        C7.Text + "020000" + ID7.Text + "000000000000000000000000" );
                    serial.WritePacket(p);
                }
            }
            catch (Exception ee)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // System.Diagnostics.Process.Start("iexplore.exe", this.linkLabel1.Text);
            //System.Diagnostics.Process.Start(this.linkLabel1.Text);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }
    }
}
