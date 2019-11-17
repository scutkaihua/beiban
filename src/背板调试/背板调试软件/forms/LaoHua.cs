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
            laoHua.start += LaoHua_start;
            laoHua.end += LaoHua_end;
            pause.Enabled = false;
        }

        private void LaoHua_end(object sender, EventArgs args)
        {
            if (this.InvokeRequired)
            {
                ThreadCallback callback = new ThreadCallback(LaoHua_end);
                this.Invoke(callback, new object[] { sender, args });
            }
            else
            {
                start.Text = "开始";
                pause.Enabled = false;
                start.Enabled = true;
            }

        }

        private void LaoHua_start(object sender, EventArgs args)
        {
            if (this.InvokeRequired)
            {
                ThreadCallback callback = new ThreadCallback(LaoHua_start);
                this.Invoke(callback,new object[] { sender,args});
            }
            else
            {
                start.Enabled = true; ;
                pause.Enabled = true;
                start.Text = "停止";
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (laoHua.isStart()==true)
                {
                    start.Enabled = false;
                    laoHua.Recover();
                    laoHua.Stop();
                }
                else
                {
                    start.Enabled = false;
                    laoHua.SetCounter(int.Parse(counter.Text), int.Parse(time.Text));
                    laoHua.SetAddr(addrs.Text.Replace("\n","").Split('\r'));
                    laoHua.Start(int.Parse(time.Text),int.Parse(counter.Text));
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
