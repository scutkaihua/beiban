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


    public partial class channel : UserControl
    {
        bool select = false;
        Color old;

        public event SelectEventHandler OnSelect;

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
            get{ return this.tv_addr.Text; }
            set{ this.tv_addr.Text = value; }
        }

        public string Id
        {
            get { return id.Text.Replace(" ", ""); }
            set { id.Text = value; }
        }

        public string Values { get { return values.Text; }set { values.Text = value; } }
    }
}
