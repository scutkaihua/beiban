using System;
using System.Collections.Generic;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Geared;

namespace LD.lib
{

    public class KeyInfo {
        public string axie;
        public float min;
        public float max;
        public Brush brush;
        public AxisPosition position;


        public KeyInfo(string a,float m,float z,Brush b, AxisPosition p) { axie = a;min = m;max = z; if (b != null) brush = b; position = p; }
    }

    public class ChannelValues
    {
        List<Dictionary<string, GearedValues<int>>> keyValuePairs = null;
        GearedValues<DateTime> X = new GearedValues<DateTime>();
        List<KeyValue<string, KeyInfo>> keyinfos = new List<KeyValue<string, KeyInfo>>();

        public byte addr;  //地址
        public int   ver;  //版本
        public byte biaoji;//标志
        public int cycle;  //循环次数
        public int vol;    //容量
        public int current;//电流
        public int voltage;//电压
        public int dianliang;//电量
        public int temperature;//温度

        public byte state;  //状态
        public byte warn;   //告警
        public byte error;  //错误


        public int ValueofByte(byte d,int offset) { return (d >> offset) & 0x01; }


        void AddName(int ch,string key,string type,int min ,int max,Brush brush,AxisPosition p)
        {
            Dictionary<string, GearedValues<int>> keyValues = keyValuePairs[ch];
            if (keyValues.ContainsKey(key) == false) keyValues.Add(key, new GearedValues<int>());
            keyinfos.Add(new KeyValue<string, KeyInfo>(key, new KeyInfo(type, min, max,brush,p)));
        }

        public KeyInfo GetKeyInfo(string name)
        {
            foreach(KeyValue<string,KeyInfo> kv in keyinfos)
            {
                if (kv.key == name) return kv.val;
            }
            return null;
        }

        void Add(int ch,string key,int v)
        {
            Dictionary<string, GearedValues<int>> keyValues = keyValuePairs[ch];
            GearedValues<int> q = null;
            if(keyValues.TryGetValue(key,out q) == false) { throw new Exception("没有对应的key:"+key); }
            q.Add(v);
            X.Add(DateTime.Now);
        }

        public void Clear()
        {
            foreach(Dictionary<string, GearedValues<int>> item in keyValuePairs)
            {
                foreach (GearedValues<int> i in item.Values)
                {
                    i.Clear();
                }
            }
        }

        public ChannelValues(int number)
        {
            keyValuePairs = new List<Dictionary<string, GearedValues<int>>>();
            for(int ch = 0; ch < number; ch++)
            {
                keyValuePairs.Add(new Dictionary<string, GearedValues<int>>());
                AddName(ch, "地址","地址",0,100,Brushes.Black,AxisPosition.RightTop);
                AddName(ch, "版本","版本",0,50,Brushes.Gray, AxisPosition.RightTop);
                AddName(ch, "标志","标志",0,10,Brushes.MediumBlue, AxisPosition.RightTop);
                AddName(ch, "循环次数","循环次数",0,300,Brushes.BlueViolet, AxisPosition.RightTop);
                AddName(ch, "容量","容量",0,6000,Brushes.CadetBlue, AxisPosition.RightTop);
                AddName(ch, "电量","电量",0,110,Brushes.Red, AxisPosition.LeftBottom);
                AddName(ch, "电流","电流",-100,5000,Brushes.Green, AxisPosition.LeftBottom);
                AddName(ch, "电压","电压",-100,5000,Brushes.DarkOrange, AxisPosition.LeftBottom);
                AddName(ch, "温度","温度",-40,100,Brushes.Fuchsia, AxisPosition.LeftBottom);

                AddName(ch, "s充电","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s充满","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s红外" ,"布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s读对","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s读错","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "w重启","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w无5V","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w弹仓","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w高温","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "e顶针","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e宝坏","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e到位","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e红外","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e摆臂","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e电机","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e借宝","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
            }
        }


        public void ChannelValueAdd(int ch,byte[] cdata,int offset)
        {
            addr = cdata[offset];
            state=cdata[offset + 1];
            warn=(cdata[offset + 2]);
            error=(cdata[offset + 3]);
            ver = cdata[offset + 14];
            current = ((((int)cdata[offset + 15]) << 8) + ((int)cdata[offset + 16]));
            dianliang = cdata[offset + 17];
            temperature = cdata[offset + 18];
            cycle = ((((int)cdata[offset + 19]) << 8) + ((int)cdata[offset + 20]));
            vol = ((((int)cdata[offset + 21]) << 8) + ((int)cdata[offset + 22]));
            voltage = ((((int)cdata[offset + 23]) << 8) + ((int)cdata[offset + 24]));
            biaoji = cdata[offset + 25];

            Add(ch,"地址", addr);
            Add(ch,"版本", ver);
            Add(ch,"标志", biaoji);
            Add(ch,"循环次数", cycle);
            Add(ch,"容量", vol);
            Add(ch,"电量", dianliang);
            Add(ch,"电流", current);
            Add(ch,"电压", voltage);
            Add(ch,"温度", temperature);

            Add(ch,"s充电",ValueofByte(state,7));
            Add(ch,"s充满", ValueofByte(state, 6));
            Add(ch,"s红外", ValueofByte(state, 5));
            Add(ch,"s读对", ValueofByte(state, 1));
            Add(ch,"s读错", ValueofByte(state, 0));

            Add(ch,"w重启",ValueofByte(warn, 4));
            Add(ch,"w无5V", ValueofByte(warn, 2));
            Add(ch,"w弹仓", ValueofByte(warn, 1));
            Add(ch,"w高温", ValueofByte(warn, 0));

            Add(ch,"e顶针", ValueofByte(error, 6));
            Add(ch,"e宝坏", ValueofByte(error, 5));
            Add(ch,"e到位", ValueofByte(error, 4));
            Add(ch,"e红外", ValueofByte(error, 3));
            Add(ch,"e摆臂", ValueofByte(error, 2));
            Add(ch,"e电机", ValueofByte(error, 1));
            Add(ch,"e借宝", ValueofByte(error, 0));
        }

        public string[] ChannelValueNames()
        {
            string[] r = new string[keyValuePairs[0].Keys.Count];
            keyValuePairs[0].Keys.CopyTo(r, 0);
            return r;
        }

        /// <summary>
        /// Y轴
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public GearedValues<int> ChannelValue(int ch,string name)
        {
            if (ch <= 0 || ch > keyValuePairs.Count) return null;
            ch -= 1;
            return keyValuePairs[ch][name];
        }


        public GearedValues<DateTime> ChannelValueTime()
        {
            return X;
        }
    }
}
