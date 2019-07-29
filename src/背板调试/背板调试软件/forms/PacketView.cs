using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LD.lib;

namespace LD.forms
{
    public partial class PacketView : Form//UserControl
    {
        int maxsize = 1000 * 200;
        public PacketView()
        {
            InitializeComponent();
        
        }

        private void 清屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtb_view.Clear();
        }



        public void PacketSend(object sender, Ulitily.PacketArgs args)
        {
            if (this.显示数据帧ToolStripMenuItem.Checked == false
                && this.显示解析ToolStripMenuItem.Checked==false) return;
            if (rtb_view.InvokeRequired)
            {
                Ulitily.onPacketTransfer trans = new Ulitily.onPacketTransfer(PacketSend);
                rtb_view.Invoke(trans, new object[] { sender, args });
            }
            else
            {
                if (rtb_view.Text.Length > maxsize) { rtb_view.Text = null; }
                String Date = System.DateTime.Now.ToString("yy/MM/dd hh:mm:ss.fff") + "-> "; 
                if (this.显示数据帧ToolStripMenuItem.Checked) Date += Ulitily.ShareClass.hexByteArrayToString(args.packet.toBytes).Replace("-", " ");
                if (this.显示解析ToolStripMenuItem.Checked)
                {
                    Date += String.Format("->地址:{0} 命令:{1} 长度:{2} 数据:{3}",
                                    args.packet.addr, args.packet.cmd, args.packet.len, Ulitily.ShareClass.hexByteArrayToString(args.packet.data,args.packet.len).Replace("-", " "));
                }

                this.rtb_view.AppendText(Date + "\r\n");
            }
        }





        public void PacketGet(object sender, Ulitily.PacketArgs args)
        {
            if (this.显示数据帧ToolStripMenuItem.Checked == false
                && this.显示解析ToolStripMenuItem.Checked == false) return;
            if (rtb_view.InvokeRequired)
            {
                Ulitily.onPacketTransfer trans = new Ulitily.onPacketTransfer(PacketGet);
                rtb_view.Invoke(trans, new object[] { sender, args });
            }
            else{
                if (rtb_view.Text.Length > maxsize) { rtb_view.Text = null; }
                String Date = System.DateTime.Now.ToString("yy/MM/dd hh:mm:ss.fff") + "<- ";
                if (this.显示数据帧ToolStripMenuItem.Checked) Date += Ulitily.ShareClass.hexByteArrayToString(args.packet.toBytes).Replace("-", " ");
                if (this.显示解析ToolStripMenuItem.Checked)
                {
                    Date += String.Format("<-地址:{0} 命令:{1} 长度:{2} 数据:{3}",
                                    args.packet.addr, args.packet.cmd, args.packet.len, Ulitily.ShareClass.hexByteArrayToString(args.packet.data,args.packet.len).Replace("-", " "));
                }

                this.rtb_view.AppendText(Date+"\r\n");
               
            }
        }


        public void CommDisplay(object sender, String header, byte[] buffer, int offset, int len)
        {
            if (this.rtb_view.InvokeRequired)
            {
                Ulitily.CommDisplayDelegate d = new Ulitily.CommDisplayDelegate(CommDisplay);
                this.rtb_view.BeginInvoke(d, new object[] { sender, header, buffer, offset, len });
            }
            else
            {
                String Date = System.DateTime.Now.ToString("yy/MM/dd hh:mm:ss");
                this.rtb_view.AppendText(Date+ header + Ulitily.ShareClass.hexByteArrayToString(buffer,offset,len).Replace("-"," "));
            }

        }



        private void 显示数据帧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.显示数据帧ToolStripMenuItem.Checked == true) this.显示数据帧ToolStripMenuItem.Checked = false;
            else this.显示数据帧ToolStripMenuItem.Checked = true;
        }

        private void 显示解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.显示解析ToolStripMenuItem.Checked == true) this.显示解析ToolStripMenuItem.Checked = false;
            else this.显示解析ToolStripMenuItem.Checked = true ;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }

    }
}
