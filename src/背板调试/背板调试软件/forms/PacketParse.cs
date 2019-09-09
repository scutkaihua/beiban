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
    public partial class PacketParse : Form
    {
        public PacketParse()
        {
            
            InitializeComponent();
            this.DoubleClick += PacketParse_DoubleClick;
        }

        private void PacketParse_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;  
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

        private void 解析_Click(object sender, EventArgs e)
        {

            String t = this.textBox1.Text.Replace("0x", "").Replace("0X","");
            if (t.Contains(","))
            {
                string[] tt = t.Split(',');
                StringBuilder sb = new StringBuilder();
                foreach(string a  in tt)
                {
                    if (a.Length == 0) { sb.Append("00"); }
                    else if (a.Length == 1) { sb.Append("0" + a); }
                    else sb.Append(a);
                    sb.Append(" ");
                }
                t = sb.ToString();
            }
            byte[] d = Ulitily.ShareClass.strToHexByteArray(t.ToString());
            Ldpacket p = new Ldpacket(d, d.Length);
            try
            {
                this.result.Clear();
                this.result.AppendText("\n\n" + p.ToString());
            }
            catch {
                MessageBox.Show("解析失败");
            }

        }
    }
}
