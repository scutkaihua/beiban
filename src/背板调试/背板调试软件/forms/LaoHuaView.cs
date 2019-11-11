using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using LD.lib;
using MySql.Data.MySqlClient;

namespace LD.forms
{

    
    public partial class LaoHuaView : Form
    {


        public LaoHuaView()
        {
            InitializeComponent();
        }

        private void Query_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgv.DataSource = new MySqlDataTable().Query(this.QueryText.Text);
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
