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
    public partial class CodeView : Form
    {
        public CodeView()
        {
            InitializeComponent();
        }


        public CodeView(String d):this()
        {
            View(d);
            view.ScrollBars = ScrollBars.Both;
        }

        public void View(String d)
        {
            int i = 0;
            int len = 64*3;
            StringBuilder sb = new StringBuilder();
            do {
                if(i+len<=d.Length)
                {
                    sb.Append(d, i, len);sb.Append("\r\n");
                    i += len;
                }
                else
                {
                    sb.Append(d, i, d.Length - i);
                    break;
                }
            } while (len < d.Length);

            view.Text = sb.ToString();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }
    }
}
