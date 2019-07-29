﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using LD.lib;
using System.Threading;

using System.Runtime.InteropServices;

namespace LD.forms
{
    public partial class SerialPortSetting : Form
    {
        static int RINGBUFFER_SIZE = 1024;
       // SerialPort serialport = new SerialPort();
        CommPort serialport;
        public System.Threading.Thread serialThread;
        RingSingleBuf ringbufserial = new RingSingleBuf(RINGBUFFER_SIZE);
        AutoResetEvent ARESerial = new AutoResetEvent(false);
        
        //事件
        public event Ulitily.onPacketTransfer onPacketReceive;
        public event Ulitily.onPacketTransfer onPacketSend;

        public SerialPortSetting()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void serialPortSetting_Load(object sender, EventArgs e)
        {
            //加载已有串口
            String[] ports = System.IO.Ports.SerialPort.GetPortNames();
            if (ports != null && ports.Length > 0)
            {
                foreach (String s in ports) this.cb_ports.Items.Add(s);
                this.cb_ports.SelectedIndex = 0;
            }

            //其它配置
            this.cb_parity.Items.Add(Parity.Even); 
            this.cb_parity.Items.Add(Parity.Odd);
            this.cb_parity.Items.Add(Parity.None);
            this.cb_parity.Text = Parity.Even.ToString();

            this.cb_stopbit.Items.Add(StopBits.One);
            this.cb_stopbit.Items.Add(StopBits.None);
            this.cb_stopbit.Items.Add(StopBits.OnePointFive);
            this.cb_stopbit.Text = StopBits.One.ToString();



        }

        private void b_open_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (b_open.Text == "打开")
                    {
                        if (serialport == null) serialport = new CommPort();
                        serialport.PortName = cb_ports.Text;
                        serialport.DataBits = 8;
                        serialport.StopBits = 1;
                        serialport.Parity = 0;
                        serialport.BaudRate = (int)UInt32.Parse(cb_baudrate.Text, System.Globalization.NumberStyles.Number);
                        serialport.ReadTimeout = 100;

                        serialport.Open();
                        if (serialport.IsOpen == false)
                        {
                            MessageBox.Show("打开失败");
                            return;
                        }
                        

                        //接收线程
                       // serialport.DataReceived += _serialPort_DataReceived;
                        if (serialThread != null) serialThread.Abort();
                        serialThread = new Thread(SerialThread);
                        serialThread.Name = "SerialThread";
                        serialThread.Start();

                        this.cb_ports.Enabled = false;
                        
                    }
                    else
                    {
                        serialport.Close();
                        if (serialport.IsOpen)
                        {
                            MessageBox.Show("关闭失败");
                            return;
                        }
                        else
                        {
                            if (serialThread != null)
                            {
                                serialThread.Abort();
                                serialThread = null;
                            }
                        }


                        this.cb_ports.Enabled = true;
                        
                    }
   
                }
                catch(Exception ex)
                {
                    MessageBox.Show("未知错误 "+ex.ToString());
                    return;
                }
            }            
            
            if (serialport.IsOpen)
            {
                this.b_open.Text = "关闭";
                return;
            }
            else
            {
                this.b_open.Text = "打开";
            }
        }


        /// <summary>
        /// 字节接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                ////throw new NotImplementedException();
                //int btr = serialport.BytesToRead;
                ////int btr = serialport.ReadBufferSize;
                //if (btr > 0)
                {
                   // byte[] sb = new byte[RINGBUFFER_SIZE];
                    //int sl = serialport.Read(sb, 0, btr);
                    byte[] sb = serialport.Read(1);
                    if (sb != null && sb.Length>0)
                    {
                        ringbufserial.WriteBuf(sb, sb.Length);
                        ARESerial.Set();
                        //Console.WriteLine(BitConverter.ToString(sb));
                    }

                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                ARESerial.Set();
            }
        }


        /// <summary>
        /// 口线程处理
        /// </summary>
        void SerialThread()
        {
            byte[] tmp = new byte[RINGBUFFER_SIZE];
            while (true)
            {
                try
                {
                    //从串口读数据----------------slip protocol--------------------//
                    _serialPort_DataReceived(null, null);
                    ARESerial.WaitOne(10, false);
                    while (ringbufserial.DataLen > 0)
                    {
                        byte c = ringbufserial.ReadByte();
                        System.Console.Write(c.ToString("X2") +" ");
                        Ldpacket packet = Ldpacket.toPackcet(c);

                        //byte[] cc = new byte[] { c };
                        //Console.Write(BitConverter.ToString(cc));

                        if (packet != null)
                        {

                            if (onPacketReceive != null)
                            {
                                Ulitily.PacketArgs args = new Ulitily.PacketArgs();
                                args.packet = packet;
                                onPacketReceive(this, args);

                                System.Console.WriteLine("call back:onPacketReceive:{0}",onPacketReceive.Method.Name);

                            }
                            if (packet.len > 0)
                                System.Console.WriteLine("Get a packet :addr:{0} cmd:{1} len:{3} data:{2}",
                                    packet.addr, packet.cmd, packet.len, Ulitily.ShareClass.hexByteArrayToString(packet.data).Replace("-", ""));
                            else
                                System.Console.WriteLine("Get a packet :addr:{0} cmd:{1} len:{2}",
                                     packet.addr, packet.cmd,packet.len);
                        }

                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("SerialThread :" + e.ToString());
                }

            }
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="outdata"></param>
        /// <param name="saveoffset">outdata[saveoffset]开始保存数据</param>
        /// <param name="len"></param>
        /// <param name="timeout">超时  ms</param>
        /// <returns></returns>
        //public int Read(byte[] outdata, int saveoffset,int len, int timeout)
        //{
        //    if (outdata == null) return 0;
        //    if (saveoffset > outdata.Length) return 0;
        //    int wantlen = (outdata.Length > len+saveoffset) ? len : outdata.Length-saveoffset;
        //    len = 0;
        //    if (serialport.IsOpen)
        //    {
        //        int index = saveoffset;
        //        serialport.ReadTimeout = timeout;

        //        while (wantlen > 0 && serialport.ReadBufferSize > 0)
        //        {
        //            outdata[saveoffset] = (byte)serialport.ReadByte();
        //            wantlen--;
        //            index++;
        //        }

        //        return index;
        //    }

        //    return len ;
        //}


        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="indata">要写入的数据</param>
        /// <param name="len">要写入的长度</param>
        /// <param name="timeout">超时</param>
        /// <returns></returns>
        public int write(byte[] indata, int offset,int len, int timeout)
        {
            if (serialport == null) return 0;
            if (serialport.IsOpen)
            {
                this.serialport.Write(indata, offset, len);
                return len;
            }

            return len;
        }


        public Ldpacket WritePacket(Ldpacket pack)
        {
            byte[] bp = pack.toBytes;
            if (bp == null) return null;
            write(bp, 0, bp.Length,100);

            if (onPacketSend != null)
            {
                Ulitily.PacketArgs args = new Ulitily.PacketArgs();
                args.packet = pack;
                onPacketSend(this, args);
            }

            return pack;
        }


        /// <summary>
        /// 帧等待时间长度
        /// </summary>
        /// <returns></returns>
        public int PacketWaitTime()
        {
            try {
                int i = int.Parse(tb_FrameWait.Text, System.Globalization.NumberStyles.Number);
                return i;
            }
            catch { return 1000; }
        }
    }
}
