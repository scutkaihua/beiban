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

        //public TextBox beibanAddr;
 
        public channel()
        {
            InitializeComponent();
            state.CheckColor = Color.Coral;
            warn.CheckColor = Color.Yellow;
            error.CheckColor = Color.Red;
            old = this.BackColor;
            this.set_names(new string[] {
                                "读错"," 读对","    ","断续","流通","红外","充满","充电",
                                "高温"," 弹仓","  5V","    ","重启","强入","弹仓","未锁",
                                "借宝","电磁阀","摆臂","红外","到位","宝坏","顶针","仓道"
                            });
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

        public void set_index_name(string n)
        {
            this.LIndex.Text = n;
        }
        public string compare_states(byte s,byte w,byte e)
        {
            string result = state.CompareState(s) + warn.CompareState(w) + error.CompareState(e);
            if (result.Length == 0) return null;
            return result;
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
            if (beibanAddr == null) return;
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.租借,beibanAddr.Text,Addr + Id + time.Text);

            if (Onlease != null)
                Onlease(p, this);
            if (serialPortSetting !=null)
                serialPortSetting.WritePacket(p);

        }

        public void rent()
        {
            Button1_Click(this, null);
        }




        private void Button2_Click(object sender, EventArgs e)
        {
            if (beibanAddr == null) return;
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.归还,beibanAddr.Text, Addr + time.Text);

            if (Onreturn != null)
                Onreturn(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (beibanAddr == null) return;
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.控制,beibanAddr.Text, "02"+Addr + Id + time.Text);

            if (Onopen != null)
                Onopen(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (beibanAddr == null) return;
            Ldpacket p =  Ldpacket.Get_Ldpacket(Cmd.控制,beibanAddr.Text, "01" + Addr + Id + time.Text);
            if (Onyunwei != null)
                Onyunwei(p, this);
            if (serialPortSetting != null)
                serialPortSetting.WritePacket(p);
        }

        public bool is_address(int addr)
        {
            try
            {
                int a = int.Parse(this.Addr);
                return a == addr;
            }
            catch { return false; }
        }

        public bool is_id(string i)
        {
            try {
                if (Id.Equals(i)) return true;
                else return false;
            } catch { return false; }
        }

        public void update(byte[] d, int offset)
        {
            Int16 c = d[offset + 15];c <<= 8;c += d[offset + 16];
            sbyte wd = (sbyte)d[offset + 18];

            this.Id = Ulitily.ShareClass.hexByteArrayToString(d, offset + 4, 10).Replace("-", "");
            this.Addr = (d[offset]).ToString("X2");
            this.set_states(d[offset + 1], d[offset + 2], d[offset + 3]);
            string ver = d[offset + 14].ToString("X2");
            string current = c.ToString("D");
            string dianlian = d[offset + 17].ToString("D2");
            string wendu = wd.ToString("D2");
            string cc = ((((int)d[offset + 19]) << 8) + ((int)d[offset + 20])).ToString("D");
            int vvol = ((((int)d[offset + 21]) << 8) + ((int)d[offset + 22]));
            int ccstart = ((vvol & 0x8000) == 0) ? 0 : 1;
            int ccend = ((vvol & 0x4000) == 0) ? 0 : 1;
            string vol = ((int)(vvol & 0x3FFF)).ToString("D");
            string v = ((((int)d[offset + 23]) << 8) + ((int)d[offset + 24])).ToString("D");
            string biaoji = d[offset + 25].ToString("X2");
            this.Values1 = "版本: " + ver + "\n次数: " + cc + "\n容量: " + vol + "\n标志: " + biaoji + "\n开始: " + ccstart;
            this.Values2 = "电流: " + current + "\n电量: " + dianlian + "\n电压: " + v + "\n温度: " + wendu + "\n结束: " + ccend;
        }

    }
}
