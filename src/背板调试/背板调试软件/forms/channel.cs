using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LD.lib;

namespace LD.forms
{


    public delegate void SelectEventHandler(MouseEventArgs a,object sender);

    public delegate void SendClickHandler(Ldpacket packet, object sender);

    public partial class channel : UserControl
    {
        bool select = false;
        Color old;

        public event SelectEventHandler OnSelect;

        public event SendClickHandler Onlease;
        public event SendClickHandler Onreturn;
        public event SendClickHandler Onopen;
        public event SendClickHandler Onyunwei;

        public SerialPortSetting serialPortSetting;

        public TextBox beibanAddr;
 
        public channel()
        {
            InitializeComponent();
            old = this.BackColor;
        }
        public bool is_select() { return select; }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            select = !select;
            //if (select) this.BackColor = Color.DeepPink;
            //else this.BackColor = old;
            if (select && OnSelect != null)
                OnSelect(e, this);
        }

        public void LostSelect()
        {
            select = false;
            this.BackColor = old;
        }

        public channel(byte[]ids)
        {
            id.Text = Ulitily.ShareClass.hexByteArrayToString(ids);
        }

        public void set_states(byte s,byte w,byte e)
        {
            state.SetState(s);
            warn.SetState(w);
            error.SetState(e);
        }
        public void set_names(string [] n)
        {
            string[] a = new string[8];
            Array.Copy(n, 0, a, 0, 8);
            state.setnames(a);
            Array.Copy(n, 8, a, 0, 8);
            warn.setnames(a);
            Array.Copy(n, 16, a, 0, 8);
            error.setnames(a);
        }


        public string Addr
        {
            get{ return this.addr.Text; }
            set{ this.addr.Text = value; }
        }

        public string Id
        {
            get { return id.Text.Replace(" ", ""); }
            set { id.Text = value; }
        }

        public string Values1 { get { return values1.Text; }set { values1.Text = value; } }
        public string Values2
        {
            get { return values2.Text; }
            set { values2.Text = value; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.Lease,beibanAddr.Text,Addr + Id + time.Text);

            if (Onlease != null)
                Onlease(p, this);
            if (serialPortSetting !=null)
                serialPortSetting.WritePacket(p);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.Return,beibanAddr.Text, Addr + time.Text);

            if (Onreturn != null)
                Onreturn(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.Ctrl,beibanAddr.Text, "02"+Addr + Id + time.Text);

            if (Onopen != null)
                Onopen(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.Ctrl,beibanAddr.Text, "01" + Addr + Id + time.Text);
            if (Onyunwei != null)
                Onyunwei(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        private void Channel_Load(object sender, EventArgs e)
        {

        }

    }
}
