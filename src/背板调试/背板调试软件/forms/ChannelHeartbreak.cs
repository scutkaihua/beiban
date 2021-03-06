﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LD.lib;


namespace LD.forms
{
    public partial class channelHeartbreak : Form
    {

        //信号量
        ManualResetEvent mre = null;

        //串口
        SerialPortSetting serialPort;

        //曲线缓存
        ChannelValues channelValues;
        //曲线选项
        ChartDataSelect dataSelect;
        //曲线显示窗口
        ChartView chartView = null;

        //曲线通道个数
        int chart_channels_max = 4;

        //定时发送
        System.Threading.Timer t = null;
        int timer_interval = 0;

        //定时租借
        System.Threading.Timer lt = null;
        int current_offset = 0;

        //排队读心跳
        string[] list_addresses = null;//地址列表
        int channels_per_beiban;       //一个背板通道数
        int beiban_counts;             //背板个数

        //通道控件
        channel[] chs = null;

        //显示
        PacketView pv;

        //接收心跳包个数
        double rx_counter=0;
        double tx_counter=0;

        public channelHeartbreak(SerialPortSetting s,PacketView p)
        {     
            InitializeComponent();
            serialPort = s;
            serialPort.onPacketReceive += Serial_onPacketReceive;

            //默认 12 个仓道 3 个背板
            rebuild_channels();
            pv = p;

            ltx.DoubleClick += Ltx_DoubleClick;
            lrx.DoubleClick += Ltx_DoubleClick;
        }


        delegate void r();
        private void LRXTX_Refresh()
        {
            if (ltx.InvokeRequired)
            {
                r rr = new r(LRXTX_Refresh);
                ltx.BeginInvoke(rr,null);
            }
            else
            {
                ltx.Text = "TX:"+tx_counter;
            }

            if (lrx.InvokeRequired)
            {
                r rr = new r(LRXTX_Refresh);
                lrx.BeginInvoke(rr, null);
            }
            else
            {
                lrx.Text = "RX:" + rx_counter;
            }            
        }

        private void Ltx_DoubleClick(object sender, EventArgs e)
        {
            tx_counter = rx_counter = 0;
            LRXTX_Refresh();
        }

        private int List_index(byte beiban)
        {
            for(int i = 0;i<list_addresses.Length;i++)
            {
                try
                {
                    byte b = byte.Parse(list_addresses[i], System.Globalization.NumberStyles.HexNumber);
                    if (b == beiban) return i;
                }
                catch (Exception ee) { }
            }
            return -1;
        }

        /// <summary>
        /// 根据心跳包，更新列表
        /// </summary>
        /// <param name="heartbreak"></param>
        void ListView_Add(Ldpacket p)
        {
            int c = List_index(p.addr);
            if (c < 0 || c>= list_addresses.Length) return;
            {
                int counter = (p.len - 6) / 26;
                for (int i = 0; i < counter; i++)
                {
                    int offset = i * 26 + 6;
                    int cindex = c * channels_per_beiban;
                    chs[i + cindex].update(p.data,offset);
                    chs[i + cindex].set_index_name("通道"+(i+cindex+1));
                    //if (chs[i + cindex].beibanAddr == null) {
                    //    //chs[i + cindex].beibanAddr = new TextBox();
                    //    chs[i + cindex].beibanAddr.Text =  p.addr.ToString("X2");
                    //}
                    chs[i + cindex].beibanAddr.Text = p.addr.ToString("X2");

                    //保存曲线数据
                    channelValues.ChannelValueAdd(i+cindex, p.data, offset, chs[i+cindex].Id);
                }
            }
        }


        /// <summary>
        /// 应答心跳包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private delegate void FlushClient(object sender, Ulitily.PacketArgs args); //代理
        private void Serial_onPacketReceive(object sender, Ulitily.PacketArgs args)
        {
            if (this.InvokeRequired)
            {
                FlushClient fc = new FlushClient(Serial_onPacketReceive);
                this.Invoke(fc, new object[] { sender, args });
            }
            else
            {
                if (checkBox1.Checked == false && CB_LISTEN.Checked==false) return;
                Ldpacket p = args.packet;
                pv.PacketGet(sender,args);
                switch (p.cmd)
                {
                    case Cmd.心跳:
                                    rx_counter++;
                                    LRXTX_Refresh();
                                    ListView_Add(p);
                                    panel.Refresh();
                                    if(null!=mre)mre.Set();
                                    break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 排队读取心跳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object state)
        {
            foreach (string beiban in list_addresses)
            {
                
                Ldpacket packet = Ldpacket.Get_Ldpacket(Cmd.心跳, beiban, xintiao.Text);
                byte[] ss = packet.toBytes;
                if(serialPort.WritePacket(packet)!=null)
                { tx_counter++;  LRXTX_Refresh();}
                //等待心跳应答 
                int div = list_addresses.Count();
                mre.Reset();
                mre.WaitOne(timer_interval/(div>0?div:1),true);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }

        /// <summary>
        /// 选择显示的曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.chartView.Visible)
            {
                this.chartView.Hide();
            }
            else {
                this.chartView.Show();
            }
        }

        /// <summary>
        /// 重新加载一次控件
        /// </summary>
        private void rebuild_channels()
        {

            try {
                int c  = int.Parse(this.channel_counts.Text.Replace(" ", ""));
                string[] la = this.addr_list.Text.Replace("  ", "").Split(' ');
                int l = la.Count();
                channels_per_beiban = c;
                this.list_addresses = la;
                chart_channels_max = c * l;
            }catch(Exception ee){}

            channelValues = new ChannelValues(chart_channels_max);
            dataSelect = new ChartDataSelect(chart_channels_max);
            chartView = new ChartView(channelValues, dataSelect);

            chs = new channel[chart_channels_max];
            for (int k = 0; k < chart_channels_max; k++) {
                channel c = new channel();
                c.Size = new Size(721, 120);
                c.serialPortSetting = this.serialPort;
                chs[k] = c;
            } 
            foreach (channel c in chs) { panel.Controls.Add(c); }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                channels_per_beiban = int.Parse(this.channel_counts.Text.Replace(" ", "")); 
                this.list_addresses =  this.addr_list.Text.Replace("  ", "").Split(' ');
                
                if (this.list_addresses == null || this.list_addresses.Count() <= 0)
                {
                    MessageBox.Show("请填写正确的地址列表!");
                    checkBox1.Checked = false;
                    return;
                }
                //通道总数改变==>清队曲线，再配置
                if (chart_channels_max < (list_addresses.Count() * channels_per_beiban))
                {
                    DialogResult result = MessageBox.Show("通道数增加，曲线数据会清0", "确定清0?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        chart_channels_max = (list_addresses.Count() * channels_per_beiban);
                        rebuild_channels();
                    }
                    else { checkBox1.Checked = false; return; }
                }
                beiban_counts = this.list_addresses.Count();//背板个数

                this.channel_counts.Enabled = false;
                this.addr_list.Enabled = false;
                this.textbox_timer.Enabled = false;
                timer_interval = Int32.Parse(textbox_timer.Text);
                t = new System.Threading.Timer(new TimerCallback(T_Tick), null, 0, timer_interval);
                mre = new ManualResetEvent(false);
            }
            else
            {
                this.channel_counts.Enabled = true;
                this.addr_list.Enabled = true;
                this.textbox_timer.Enabled = true;
                if (t != null) t.Dispose();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定删除历史数据吗??", "确定删除?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.panel.Controls.Clear();
                channelValues.Clear();
                rebuild_channels();
            }

        }

        private void CB_LANT_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_LANT.Checked)
            {
                current_offset = 0;
                timer_interval = Int32.Parse(textbox_timer.Text);
                lt = new System.Threading.Timer(new TimerCallback(LT_Tick), null, 0, timer_interval);
            }
            else
            {
                if (lt != null) lt.Dispose();
            }
        }

        private void LT_Tick(object state)
        {
            current_offset %= chs.Length;
            channel cc = chs[current_offset];
            while (cc.Id.Equals("00000000000000000000") && current_offset<(chs.Length))
            {
                cc = chs[current_offset];
                current_offset++;
            }
            if (cc.Id.Equals("00000000000000000000")) return;
            else { cc.rent(); current_offset++; }

        }

        private void 重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定删除历史数据吗??", "确定删除?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.panel.Controls.Clear();
                channelValues.Clear();
                rebuild_channels();
            }
        }
    }
}
