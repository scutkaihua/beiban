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
    public partial class LaoHua : Form
    {
        SerialPortSetting portSetting;

        public LaoHua()
        {
            InitializeComponent();
            pause.Enabled = false;
        }


        public LaoHua(SerialPortSetting serial) :this(){ this.portSetting = serial; }

        private void Start_Click(object sender, EventArgs e)
        {

        }
    }
}
