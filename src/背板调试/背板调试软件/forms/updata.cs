using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
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
        StringBuilder sb = new StringBuilder(64 * 1024);//64k数据缓冲

        public updata()
        {
            InitializeComponent();
            pb.Value = 0;
            pb.Maximum = 100;
            file.DoubleClick += File_DoubleClick; 
            //OpenFileDialog.Filter = "(*.bin)";
            OpenFileDialog.Title = "请选择xxx.bin升级文件";
        }

        public updata(SerialPortSetting s):this()
        {   
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

        private bool ReadBinFile()
        {
            StreamReader sr = new StreamReader(this.file.Text);
            string r = sr.ReadToEnd();
            return false;
        }

        private void File_DoubleClick(object sender, EventArgs e)
        {
            if(DialogResult.OK == OpenFileDialog.ShowDialog())
            {
                this.file.Text = OpenFileDialog.FileName;
                sb.Length = 0;
                FileStream fs = new FileStream(this.file.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                int length = (int)fs.Length;
                while (length > 0)
                {
                    byte tempByte = br.ReadByte();
                    string tempStr = Convert.ToString(tempByte, 16);
                    sb.Append(tempStr);
                    length--;
                }
                fs.Close();
                br.Close();
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
