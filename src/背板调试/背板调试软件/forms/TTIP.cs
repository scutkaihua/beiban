using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD.forms
{
    public partial class TTIP : Form
    {
        public TTIP()
        {
            InitializeComponent();
        }

        public void Set(string v)
        {
            this.label1.Text = v;
        }
    }
}
