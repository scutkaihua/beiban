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
        public event ThreadCallback haohua;

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
        /// 
        /// </summary>
        void Run()
        { 
            while (true)
            { 
                startthread = true;
                start?.Invoke(this, null);
                foreach (KeyValues<int ,int> kvs in addrs)
                {
                    foreach(int number in kvs.values)
                    {
                        LaoHuaFunction(kvs.key, number);

                        /*线程退出*/
                        if (cancel.IsCancellationRequested)
                        {
                            end?.Invoke(this,null);
                            startthread = false;
                            return;
                        }

                        /*线程暂停*/
                        while (pausethread)
                        {
                            pause?.Invoke(this,null);
                            resetEvent = new ManualResetEvent(false);
                            resetEvent.WaitOne();
                        }
                        
                    }
                }
                if(addrs.Count==0)
                    Thread.Sleep(20);
            }
        }

        #endregion

        #region  老化逻辑

        Ldpacket

        /// <summary>
        /// 老化程序逻辑
        /// </summary>
        void LaoHuaFunction(int addr,int number)
        {

            /*读取心跳*/

            /*下发租借指令*/

            /*应答处理*/

            /*租借响应处理*/

            /*读取心跳，等待归还*/

            /*超时处理*/
            Console.WriteLine("正在老化:{0}-{1}", addr, number);
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
