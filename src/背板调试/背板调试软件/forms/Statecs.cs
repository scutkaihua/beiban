using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LD.forms
{
    public partial class State : UserControl
    {


        CheckBox[] cbs = new CheckBox[8];
        Color color;

        public State()
        {
            InitializeComponent();
            cbs[0] = bit0;
            cbs[1] = bit1;
            cbs[2] = bit2;
            cbs[3] = bit3;
            cbs[4] = bit4;
            cbs[5] = bit5;
            cbs[6] = bit6;
            cbs[7] = bit7;
            color = this.BackColor;

        }


        public State(string[] names):base()
        {
            setnames(names);
        }

        public void setnames(string[] names)
        {
            int i = 0;
            for(;i<names.Length; i++)
            {
                cbs[i].Text = names[i];
                if("".Equals(names[i].Replace(" ","")))
                {
                    cbs[i].Visible = false;
                }
            }
        }

        public void SetState(byte b)
        {
            int i = 0;
            for(;i<8;i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    cbs[i].Checked = true;
                    cbs[i].BackColor = Color.Red;
                    if ("".Equals(cbs[i].Text.Replace(" ", "")))
                    {
                        cbs[i].Visible = true;
                    }
                }
                else
                {
                    cbs[i].Checked = false;
                    cbs[i].BackColor = color;
                    if ("".Equals(cbs[i].Text.Replace(" ", "")))
                    {
                        cbs[i].Visible = false;
                    }
                }
            }
        }
    }
}
