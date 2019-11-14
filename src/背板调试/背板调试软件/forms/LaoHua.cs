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
        RunLaoHua laoHua = null;
        public LaoHua()
        {
            InitializeComponent();
        }


        public LaoHua(SerialPortSetting serial) :this(){
            this.portSetting = serial; 
            laoHua = new RunLaoHua(portSetting);
            pause.Enabled = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (laoHua.isStart()==true)
                {
                    laoHua.Stop();
                    start.Text = "开始";
                    pause.Enabled = false;
                }
                else
                {
                    laoHua.SetAddr(addrs.Text.Replace("\n","").Split('\r'));
                    laoHua.Start(int.Parse(time.Text),int.Parse(counter.Text));
                    pause.Enabled = true;
                    start.Text = "停止";
                }

            }
            catch(Exception ee)
            {
                MessageBox.Show("开始失败 ："+ ee.ToString());
                pause.Enabled = false;
                start.Text = "开始";
            }
        }

        private void pause_Click(object sender, EventArgs e)
        {
            if (laoHua.isPause())
            {
                laoHua.Recover();
                pause.Text = "暂停";
            }
            else
            {
                laoHua.Pause();
                pause.Text = "恢复";
            }
        }
    }
}
