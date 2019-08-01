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
using System.Security.Cryptography;

namespace LD.forms
{
    public partial class updata : UserControl
    {
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        public SerialPortSetting serialPortSetting;
        bool StartUpdate = false;
        public TextBox Addr;

        CodeView codeView  = new CodeView();

        byte[] code = new byte[1024 * 128];
        int codelen = 0;

        int offset=0;                         //当前发送偏移

        int len_per_packet = 200;

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


        private void WriteAPacket()
        {
            int datalen = (codelen - offset) >= len_per_packet ? (len_per_packet) : (codelen - offset);
            if (datalen > 0)
            {
                Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.UpdateData, Addr.Text,
                    offset.ToString("x4") + Ulitily.ShareClass.hexByteArrayToString(code, offset,datalen).Replace("-", "")+datalen.ToString("x2"));
                offset += datalen;
                serialPortSetting.WritePacket(p);
            }
        }

        private void SerialPortSetting_onPacketReceive(object sender, lib.Ulitily.PacketArgs args)
        {
            if(StartUpdate)
            {
                switch(args.packet.cmd)
                {
                    case Cmd.UpdateStart://发送第一包 1k
                        if (args.packet.data[0] == 1)
                        {
                            this.pb.Value = offset * 100 / codelen;
                            WriteAPacket();
                        }
                        break;

                    case Cmd.UpdateData://发送数据
                        if (args.packet.data[0] == 0)
                        {
                            WriteAPacket();
                        }
                        else
                        {
                            this.pb.Value = 100;
                            MessageBox.Show("发送完成");
                        }
                        break;
                    default:break;
                }
            }
        }

        private void File_DoubleClick(object sender, EventArgs e)
        {
            if(DialogResult.OK == OpenFileDialog.ShowDialog())
            {
                this.file.Text = OpenFileDialog.FileName;
                FileStream fs = new FileStream(this.file.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                int length = (int)fs.Length;
                codelen = 0;
                while (length > 0)
                {
                     code [codelen] = br.ReadByte();
                     codelen++;length--;
                }
                fs.Close();
                br.Close();

                //长度必须是4的整数倍
                codelen += 4 - ((codelen % 4) == 0 ? 4 : (codelen % 4));

                // 获得MD5摘要算法的 MessageDigest 对象
                MD5 mdInst = System.Security.Cryptography.MD5.Create();
                // 使用指定的字节更新摘要
                mdInst.ComputeHash(code,0,codelen);
                // 获得密文
                byte[] md = mdInst.Hash;
                this.md5.Text = Ulitily.ShareClass.hexByteArrayToString(md).Replace("-","");
                this.len.Text = codelen.ToString("x8");

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
                Ldpacket packet = Ldpacket.Get_Ldpacket(Cmd.UpdateStart, Addr.Text, ver.Text+ md5.Text+this.len.Text);
                serialPortSetting.WritePacket(packet);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            codeView.View(codelen>0?Ulitily.ShareClass.hexByteArrayToString(code, codelen).Replace("-", " "):"");
            codeView.Show();
        }
    }
}
