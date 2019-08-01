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
    public partial class updata : UserControl
    {
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        SerialPortSetting serialPortSetting;
        bool StartUpdate = false;
        TextBox Addr;

        int offset=0;                         //当前发送偏移
        byte[] filedata = new byte[1024 * 64];//64k数据缓冲

        public updata(SerialPortSetting s)
        {
            InitializeComponent();
            pb.Value = 0;
            pb.Maximum = 100;
            file.DoubleClick += File_DoubleClick;
            OpenFileDialog.Title = "请选择xxx.bin升级文件";
            OpenFileDialog.Filter = "*.bin";
            serialPortSetting = s;
            serialPortSetting.onPacketReceive += SerialPortSetting_onPacketReceive;
        }

        private void SerialPortSetting_onPacketReceive(object sender, lib.Ulitily.PacketArgs args)
        {
            if(StartUpdate)
            {
                switch(args.packet.cmd)
                {
                    case Cmd.UpdateStart://发送第一包 1k
                        break;
                }
            }
        }

        private void File_DoubleClick(object sender, EventArgs e)
        {
            if(DialogResult.OK == OpenFileDialog.ShowDialog())
            {
                this.file.Text = OpenFileDialog.FileName;
            }

        }

        private void Start_Click(object sender, EventArgs e)
        {
            if(StartUpdate)
            {
                StartUpdate = false;
                this.start.Text = "开始升级";
            }
            else
            {
                StartUpdate = true;
                this.start.Text = "停止升级";
                //发送开始包
                Ldpacket packet = Ldpacket.Get_Ldpacket(Cmd.UpdateStart, Addr.Text, md5.Text);
                serialPortSetting.WritePacket(packet);
            }
        }
    }
}
