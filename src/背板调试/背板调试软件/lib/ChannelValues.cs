using System;
using System.Collections.Generic;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Geared;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;


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
        List<KeyValue<string, KeyInfo>> keyinfos = new List<KeyValue<string, KeyInfo>>();

        public byte addr;  //地址
        public int   ver;  //版本
        public byte biaoji;//标志
        public int cycle;  //循环次数
        public int vol;    //容量
        public int current=0;//电流
        public int voltage;//电压
        public int dianliang;//电量
        public int temperature;//温度
        public int ccstart; //开始计算容量
        public int ccend;   //结束计算容量
        public byte state;  //状态
        public byte warn;   //告警
        public byte error;  //错误
        public string[] id; //充电宝id

        private DateTime starttime;//开始时间

        private int channel_max=5;  //通道个数


        private int[] last_time;    //上一次时间偏移
        private int[] last_current; //上一次电流值

        private Int64[] capacity ;    //统计容量

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
            if (q.Count == 0)
            {
                starttime = DateTime.Now;
                last_time = new int[channel_max]; Array.Clear(last_time, 0, last_time.Length);
                last_current[ch] = 0;
            }
            q.Add(v);
        }

        public void Clear()
        {
            foreach(Dictionary<string, GearedValues<int>> item in keyValuePairs)
            {
                foreach (GearedValues<int> i in item.Values)
                {
                    i.Clear();
                    this.current = 0;
                    this.last_time = new int[channel_max];  Array.Clear(last_time, 0, last_time.Length);
                    this.capacity = new Int64[channel_max]; Array.Clear(capacity, 0, capacity.Length);
                }
            }
        }

        public ChannelValues(int number)
        {
            keyValuePairs = new List<Dictionary<string, GearedValues<int>>>();
            channel_max = number;
            for(int ch = 0; ch < number; ch++)
            {
                keyValuePairs.Add(new Dictionary<string, GearedValues<int>>());
                AddName(ch, "地址","地址",0,100,Brushes.Black,AxisPosition.RightTop);
                AddName(ch, "版本","版本",0,50,Brushes.Gray, AxisPosition.RightTop);
                AddName(ch, "标志","标志",0,10,Brushes.MediumBlue, AxisPosition.RightTop);
                AddName(ch, "循环次数","循环次数",0,300,Brushes.BlueViolet, AxisPosition.RightTop);
                AddName(ch, "容量","容量",0,6000,Brushes.CadetBlue, AxisPosition.RightTop);
                AddName(ch, "电量","电量",0,110,Brushes.Red, AxisPosition.LeftBottom);
                AddName(ch, "曲线容量", "容量", 0, 6000, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "电流","电流",-100,5000,Brushes.Green, AxisPosition.LeftBottom);
                AddName(ch, "电压","电压",-100,5000,Brushes.DarkOrange, AxisPosition.LeftBottom);
                AddName(ch, "温度","温度",-40,100,Brushes.Fuchsia, AxisPosition.LeftBottom);

                AddName(ch, "s充电","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s充满","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s红外", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s流通", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "s读对","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "s读错","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "w未锁", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w强弹", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w强入", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w重启","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w无5V","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w弹仓","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "w高温","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "e仓道","布尔",0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e顶针","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e宝坏","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e到位","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e红外","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e摆臂","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e电机","布尔",0,2,Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "e借宝", "布尔", 0, 2, Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "容量开始", "容量开始", 0, 2, Brushes.Black, AxisPosition.LeftBottom);
                AddName(ch, "容量结束", "容量结束", 0, 2, Brushes.Black, AxisPosition.LeftBottom);

                AddName(ch, "时间", "偏移", 0,10000, Brushes.Black, AxisPosition.LeftBottom);
                //AddName(ch, "编号","编号", 0, int.MaxValue, Brushes.Black, AxisPosition.LeftBottom);

            }

        this.id = new string[number];
        last_time = new int[number];    //上一次时间偏移
        last_current = new int[number]; //上一次电流值
        capacity = new Int64[number];    //统计容量
    }


        public void ChannelValueAdd(int ch,byte[] cdata,int offset,string cid)
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
            ccstart = ((vol & 0x8000) == 0) ? 0 : 1;
            ccend = ((vol & 0x84000) == 0) ? 0 : 1;
            vol = (vol & 0x3FFF);

            //try{
            //    int iid = int.Parse(cid.Substring(11));
            //    Add(ch, "编号", iid);
            //}catch(Exception ee){ }

            Add(ch,"地址", addr);
            Add(ch,"版本", ver);
            Add(ch,"标志", biaoji);
            Add(ch,"循环次数", cycle);
            Add(ch,"容量开始", ccstart);
            Add(ch,"容量结束", ccend);
            Add(ch,"容量", vol);
            Add(ch,"电量", dianliang);
            Add(ch,"电流", current);
            Add(ch,"电压", voltage);
            Add(ch,"温度", temperature);

            Add(ch,"s充电",ValueofByte(state,7));
            Add(ch,"s充满", ValueofByte(state, 6));
            Add(ch, "s红外", ValueofByte(state, 5));
            Add(ch, "s流通", ValueofByte(state, 4));
            Add(ch,"s读对", ValueofByte(state, 1));
            Add(ch,"s读错", ValueofByte(state, 0));

            Add(ch, "w未锁", ValueofByte(warn, 7));
            Add(ch, "w强弹", ValueofByte(warn, 6));
            Add(ch, "w强入", ValueofByte(warn, 5));
            Add(ch,"w重启",ValueofByte(warn, 4));
            Add(ch,"w无5V", ValueofByte(warn, 2));
            Add(ch,"w弹仓", ValueofByte(warn, 1));
            Add(ch,"w高温", ValueofByte(warn, 0));

            Add(ch, "e仓道", ValueofByte(error, 7));
            Add(ch,"e顶针", ValueofByte(error, 6));
            Add(ch,"e宝坏", ValueofByte(error, 5));
            Add(ch,"e到位", ValueofByte(error, 4));
            Add(ch,"e红外", ValueofByte(error, 3));
            Add(ch,"e摆臂", ValueofByte(error, 2));
            Add(ch,"e电机", ValueofByte(error, 1));
            Add(ch,"e借宝", ValueofByte(error, 0));

            int time_now = (int)(DateTime.Now.Ticks / 10000000 - starttime.Ticks / 10000000);
            Add(ch, "时间", time_now);//秒计时

            /*平滑滤波曲线容量*/
            {
                int c = current > 0 ? current : 0;
                int lc = (last_current[ch] > 0) ? (last_current[ch]) : 0;

                lc  = (c + lc) / 2;
                lc = lc * (time_now - last_time[ch]);
                last_time[ch] = time_now;
                last_current[ch] = current;
                capacity[ch] += lc;
                Add(ch, "曲线容量", (int)(this.capacity[ch]/3600));//秒计时
            }
            this.id[ch] = cid;
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


        /// <summary>
        /// 数据保存到文件
        /// </summary>
        /// <returns></returns>
        public bool SaveFile()
        {
            IWorkbook workbook = new HSSFWorkbook();     //1.创建工作簿

            //设置表的行列数据
            for(int ch = 0;ch<channel_max;ch++)
            {
                string sheetname = null;
                if (id[ch]==null||id[ch].Equals("00000000000000000000") ) sheetname = "通道" + (ch + 1);
                else sheetname = id[ch].Substring(9);
                ISheet sheet = workbook.CreateSheet(sheetname); //2.创建工作表
                Dictionary<string, GearedValues<int>> keyValues = keyValuePairs[ch];
                IRow row0 = sheet.CreateRow(0);
                int col=0;//第一列开始
                foreach(KeyValuePair<string, GearedValues<int>> kv in keyValues)//遍历每一列数据
                {
                    row0.CreateCell(col).SetCellValue(kv.Key);//第一行放Header
                    GearedValues<int> vs = kv.Value;
                    int row_number = 1;                       //row 从1 开始放数据
                    foreach (int vs_item in vs)
                    {
                        IRow row;
                        if (col == 0) row = sheet.CreateRow(row_number);//第一列，创建行
                        else row = sheet.GetRow(row_number);            //其他列，读取行
                        row.CreateCell(col).SetCellValue(vs_item);//保存数据
                        row_number++;                             //行++
                    }
                    col++;//列++
                }
            }

            //创建流对象并设置存储Excel文件的路径
            string name = Directory.GetCurrentDirectory() + "\\曲线\\"+ starttime.ToString("yyyy-MM-dd HH-mm-ss")+".xls";
            using (FileStream url = File.OpenWrite(name))
            {
                //导出Excel文件
                workbook.Write(url);
            };
            return true;
        }


        /// <summary>
        /// 从文件中加入数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ReadFile(string name)
        {
            try
            {
                using (FileStream filesrc = File.OpenRead(name))
                {
                    //工作簿对象获取Excel内容
                    IWorkbook workbook = new HSSFWorkbook(filesrc);
                    this.Clear();
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        ////获得工作簿里面的工作表
                        ISheet sheet = workbook.GetSheetAt(i);
                        Console.WriteLine(sheet.SheetName);

                        IRow row0 = sheet.GetRow(0);                //表头 
                        for (int c = 0; c < row0.LastCellNum; c++)  //遍历所有列 
                        {
                             for (int r = 1; r <= sheet.LastRowNum; r++)//遍历所有行
                            {
                                //获取每张表的信息
                                IRow row = sheet.GetRow(r);
                                ICell cell = row.GetCell(c);             //获得每行中每个单元格信息
                                string key = row0.GetCell(c).ToString(); //key
                                int val = 0;
                                int ch = i;
                                try { val = int.Parse(cell.ToString()); } catch { val = 0; }
                                
                                Add(ch, key, val);
                                
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;  
            }

            return true;   
        }
    }
}
