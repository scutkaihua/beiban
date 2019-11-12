using System;
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

        SerialPort serialPort = null;// new SerialPort();

        //CommPort serialport;
        public System.Threading.Thread serialThread;
        AutoResetEvent ARESerial = new AutoResetEvent(false);
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        //事件
        public event Ulitily.onPacketTransfer onPacketReceive;
        public event Ulitily.onPacketTransfer onPacketSend;
        public event Ulitily.onPacketTransfer onErrorByte;

        private static readonly Mutex mutex = new Mutex();

        public SerialPortSetting()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = 10;
            timer.Start();
            this.FormBorderStyle = FormBorderStyle.None;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SerialPort_DataReceived(null, null);

            //SerialPort_DataReceived(null,null);
            while (databuffer.Count > 0)
            {
                byte c = databuffer.Dequeue();
                SerialProcessByte(c);
            }
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
            this.cb_parity.Text = Parity.None.ToString();

            this.cb_stopbit.Items.Add(StopBits.One);
            this.cb_stopbit.Items.Add(StopBits.None);
            this.cb_stopbit.Items.Add(StopBits.OnePointFive);
            this.cb_stopbit.Text = StopBits.One.ToString();



        }

        private void b_open_Click(object sender, EventArgs e)
        {
            try
            {
                if (b_open.Text == "打开")
                {
                    serialPort = new SerialPort();
                    //serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.PortName = cb_ports.Text;
                    serialPort.BaudRate = (int)UInt32.Parse(cb_baudrate.Text, System.Globalization.NumberStyles.Number);
                    serialPort.Parity = Parity.None;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = true;
                    serialPort.ReadBufferSize = 2048;
                    serialPort.Open();
                    this.cb_ports.Enabled = false;    
                }
                else
                {
                    serialPort.Close();
                    if(serialPort.IsOpen)
                    {
                        MessageBox.Show("关闭失败");
                        return;
                    }
                    this.cb_ports.Enabled = true;
                        
                }
   
            }
            catch(Exception ex)
            {
                MessageBox.Show("未知错误 "+ex.ToString());
                return;
            }
  
            //if (serialport.IsOpen)
            if(serialPort.IsOpen)
            {
                this.b_open.Text = "关闭";
                return;
            }
            else
            {
                this.b_open.Text = "打开";
            }
        }

        Queue<byte> databuffer = new Queue<byte>(1024*128);
        byte[] b = new byte[500];
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null) return;
            if (serialPort.IsOpen == false) return;
            int l = serialPort.BytesToRead;
            if (l > 0)
            {
                mutex.WaitOne();
                serialPort.Read(b, 0, l);
                mutex.ReleaseMutex();
            }      
            for (int i = 0; i < l; i++) databuffer.Enqueue(b[i]);
        }


        /// <summary>
        /// 字节接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        ////throw new NotImplementedException();
        //        //int btr = serialport.BytesToRead;
        //        ////int btr = serialport.ReadBufferSize;
        //        //if (btr > 0)
        //        {
        //           // byte[] sb = new byte[RINGBUFFER_SIZE];
        //            //int sl = serialport.Read(sb, 0, btr);
        //            byte[] sb = serialport.Read(1);
        //            if (sb != null && sb.Length>0)
        //            {
        //                ringbufserial.WriteBuf(sb, sb.Length);
        //                ARESerial.Set(); 
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.WriteLine(ex.ToString());
        //        ARESerial.Set();
        //    }
        //}


        int total_readbufferlen = 0;
        void SerialProcessByte(byte c)
        {
            System.Console.Write(c.ToString("x2") + " ");
            bool er = false;
            Ldpacket packet = Ldpacket.toPackcet(c, ref er);
            total_readbufferlen++;
            if (er == true)
            {          
                if (onErrorByte != null)
                {
                    onErrorByte(this, new Ulitily.PacketArgs { errorbyte = c });
                }
            }

            if (packet != null)
            {

                if (onPacketReceive != null)
                {
                    Ulitily.PacketArgs args = new Ulitily.PacketArgs();
                    args.packet = packet;
                    onPacketReceive(this, args);

                    System.Console.WriteLine("call back:onPacketReceive:{0}", onPacketReceive.Method.Name);

                }
                //if (packet.len > 0)
                //    System.Console.WriteLine("Get a packet :addr:{0} cmd:{1} len:{2} data:{3}",
                //        packet.addr, packet.cmd, packet.len, Ulitily.ShareClass.hexByteArrayToString(packet.data,packet.len).Replace("-", ""));
                //else
                //    System.Console.WriteLine("Get a packet :addr:{0} cmd:{1} len:{2}",
                //         packet.addr, packet.cmd,packet.len);
            }

        }

        /// <summary>
        /// 口线程处理
        /// </summary>
        void SerialThread()
        {
           
            byte c = 0;
            while (true)
            {
                try
                {
                    //从串口读数据----------------slip protocol--------------------//
                    if(serialPort.BytesToRead>0)
                    {
                        c = (byte)serialPort.ReadByte();
                        SerialProcessByte(c);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(10);
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
            mutex.WaitOne();
            if (serialPort == null) return 0;
            if (serialPort.IsOpen)
            {
                serialPort.Write(indata, offset, len);
            }
            mutex.ReleaseMutex();
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
                try { 
                Console.WriteLine("\r\n"+"==>"+Ulitily.ShareClass.hexByteArrayToString(pack.toBytes).Replace("-"," "));
                }
                catch
                {

                }
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
