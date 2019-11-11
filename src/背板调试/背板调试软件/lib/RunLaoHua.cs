using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LD.forms;
using MySql.Data;


namespace LD.lib
{

    public delegate void ThreadCallback(RunLaoHua laohua);

    public class RunLaoHua {

        /*串口*/
        SerialPortSetting serial;

        /*老化线程*/
        Thread thread = null;
        CancellationTokenSource cancel = new CancellationTokenSource();

        /*事件*/
        public ThreadCallback start;
        public ThreadCallback end;

        /*老化延时:s*/
        int delays;

        /*老化次数*/
        int counter;

        /*当前老化次数*/
        int ccounter;

        /*老化通道*/
        List<KeyValues<int,int>> addr = new List<KeyValues<int,int>>();


        public RunLaoHua(SerialPortSetting s)
        {
            thread = new Thread(new ThreadStart(this.Run));
            delays = 0;
            counter = 0;
            ccounter = 0;
            serial = s;
        }

        /*设置当前老化通道*/
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
                    addr.Add(kv);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        /*开始老化*/
        public bool Start(int dms,int c)
        {
            if(thread.ThreadState != ThreadState.Running)
            {
                delays = dms;
                counter = c;
                ccounter = 0;
                if(start != null)
                {
                    start(this);
                }
                thread.Start();
            }
            if (thread.ThreadState != ThreadState.Running) return false;
            return true;
        }

        public void Stop()
        {
            cancel.Cancel();
        }

        /// <summary>
        /// 
        /// </summary>
        void Run()
        {
            while (true)
            {
                /*线程退出*/
                if (cancel.IsCancellationRequested || LaoHuaFunction() < 0)
                {
                    if(end != null)
                    {
                        end(this);
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// 老化程序逻辑
        /// <return> 错误码</return>
        /// </summary>
        int LaoHuaFunction()
        {

            return 0;
        }


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
