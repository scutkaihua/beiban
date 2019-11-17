using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LD.forms;
using MySql.Data;


namespace LD.lib
{

    public delegate  void ThreadCallback(object sender, EventArgs args);

    public class RunLaoHua {

        /*串口*/
        SerialPortSetting serial;

        /*数据包*/
        Ldpacket ldpacket = null;

        /*当前仓道状态*/
        HeartBreak heartBreak = null;

        /*老化线程*/
        Thread thread = null;
        CancellationTokenSource cancel = null;
        bool pausethread = false;
        bool startthread = false;
        ManualResetEvent resetEvent=new ManualResetEvent(false);

        /*事件*/
        public event ThreadCallback start;
        public event ThreadCallback end;
        public event ThreadCallback pause;
        public event ThreadCallback laohua;

        /*老化延时:s*/
        int delays;

        /*老化次数*/
        int counter;

        /*当前老化次数*/
        int ccounter;

        /*老化通道*/
        List<KeyValues<int,int>> addrs = new List<KeyValues<int,int>>();


        public RunLaoHua(SerialPortSetting s)
        {
            delays = 0;
            counter = 0;
            ccounter = 0;
            serial = s;
            serial.onPacketReceive += Serial_onPacketReceive;
        }

        private void Serial_onPacketReceive(object sender, Ulitily.PacketArgs args)
        {
            ldpacket = args.packet;
        }


        public void SetCounter(int c,int s)
        {
            counter = c;
            delays = s;
        }
        
        /// <summary>
        /// 设置当前老化通道
        /// </summary>
        /// <param name="sets"></param>
        /// <returns></returns>
        public bool SetAddr(string[] sets)
        {
            try
            {
                int k;
                string []v;
                foreach (string s in sets)
                {
                    k = int.Parse(s.Split('=')[0]);
                    v = s.Split('=')[1].Replace(" ","").Split(',');
                    KeyValues<int,int> kv = new KeyValues<int,int>();
                    kv.key = k;
                    foreach (string vv in v) kv.AddValue(int.Parse(vv));
                    addrs.Add(kv);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        #region 线程控制
        /*开始老化*/
        public bool Start(int ds,int c)
        {
            thread = new Thread(new ThreadStart(this.Run));
            thread.Start();
            cancel = new CancellationTokenSource();
            int dc = 100;
            while( (startthread != true) && (dc-->0) )
                Thread.Sleep(100);
            if (startthread != true)
            {
                Stop();
                return false;
            }
            return true;
        }

        public bool isStart() { return startthread; }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            startthread = false;
            cancel?.Cancel();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            pausethread = true;
        }

        public bool isPause() { return pausethread; }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Recover()
        {
            pausethread = false;
            resetEvent.Set();
        }

        /// <summary>
        /// 运行线程
        /// </summary>
        void Run()
        {                 
            startthread = true;
            start?.Invoke(this, null);
            while (true)
            {

                foreach (KeyValues<int, int> kvs in addrs)
                {
                    foreach (int number in kvs.values)
                    {
                        /*线程退出*/
                        if (cancel.IsCancellationRequested)
                        {
                            end?.Invoke(this, null);
                            startthread = false;
                            return;
                        }                               
                            
                        /*线程暂停*/
                        while (pausethread)
                        {
                            pause?.Invoke(this, null);
                            resetEvent = new ManualResetEvent(false);
                            resetEvent.WaitOne();
                        }
                        LaoHuaFunction(kvs.key, number);
                        ccounter++;
                        laohua?.Invoke(this, null);
                    }
                }
                if (addrs.Count == 0)
                    Thread.Sleep(20);

                //次数够，退出线程
                if (ccounter >= counter) Stop();

            }
        }

        #endregion

        #region  老化逻辑



        #region 检测心跳
        /// <summary>
        /// 从心跳包中读取一个仓道的数据
        /// </summary>
        /// <param name="number"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        ChannelValue ChannelValueFromHeartBreak(int number,HeartBreak h)
        {
            foreach(ChannelValue cv in h.channelValues)
            {
                if (int.Parse(cv.Values["地址"]) == number) return cv;
            }
            return null;
        }

        /// <summary>
        /// 检查心跳应答
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="number"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool CheckHeartBread(int addr,int number,int timeout)
        {
            Console.WriteLine("读心跳:{0}-{1}",addr,number);
            /*读取心跳*/
            Ldpacket p = new Ldpacket(Cmd.心跳, addr.ToString());
            serial.WritePacket(p);ldpacket = null;

            //等待心跳应答
            while (timeout >0)
            {
                if(ldpacket !=null && ldpacket.cmd== Cmd.心跳)
                {
                    HeartBreak heart = new HeartBreak(ldpacket);
           
                    //记录心跳数据

                    //判断仓道是否读取正常
                    ChannelValue cv = ChannelValueFromHeartBreak(number, heart);
                    if (cv.Values["读对"] == "true") return true;
                }
                Thread.Sleep(200);
                timeout -= 200;
            }

            //没有心跳，故障

            //仓道读不到，记录一下异常

            return false;
        }

        #endregion


        #region 租借
        /// <summary>
        /// 租借过程处理
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="number"></param>
        /// <param name="ms"></param>
        void RentProcess(int addr,int number,int ms)
        {
            ChannelValue cv = ChannelValueFromHeartBreak(number, heartBreak);
            /*下发租借*/
            Ldpacket p = Ldpacket.Get_Ldpacket(Cmd.租借, addr.ToString(), number.ToString() + cv.Values["编号"] + 10.ToString());
            /*检查应答*/
            ldpacket = null;
            while (ms > 0)
            {
                if (ldpacket != null && ldpacket.cmd == Cmd.租借)
                {
                    //握手应答

                    //租借应答

                }
                Thread.Sleep(200);
                ms -= 200;
            }
        }
        #endregion

        /// <summary>
        /// 老化程序逻辑
        /// </summary>
        void LaoHuaFunction(int addr,int number)
        {

            //检查仓道心跳
            if (CheckHeartBread(addr, number, delays * 1000) == false) return;

            //租借处理
            RentProcess(addr, number, delays * 1000);
        }

        #endregion

        #region      记录处理

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        void PrintLogs(string s)
        {

        }


        void LogData(bool wubao, bool bushibie, bool timeout, bool rentfail,
            bool bufu, bool shineng, bool diancifa,
            bool cangdaomang, bool meixiangying)
        {



        }

        #endregion

    }




}
