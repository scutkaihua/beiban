﻿using System;
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
        Color checkColor;
        public Color CheckColor{ get { return checkColor; }set { checkColor = value; } }

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
        public string getname(int offset)
        {
            offset %= 8;
            return cbs[offset].Text;
        }

        public bool GetState(int offset)
        {
            offset %= 8;
            return cbs[offset].Checked;
        }

        public string CompareState(byte b)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            for (; i < 8; i++)
            {
                bool c = ((b & (1 << i)) != 0) ? true : false;
                bool o = cbs[i].Checked;
                if (c != o)
                    sb.Append(" "+ cbs[i].Text + ": "+ o +" -> "+c+"\n");
            }
            return sb.ToString();
        }
        public void SetState(byte b)
        {
            int i = 0;
            for(;i<8;i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    cbs[i].Checked = true;
                    cbs[i].BackColor = checkColor;
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
