using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Geared;

namespace LD.lib
{

    public class ChannelValue
    {
        public Dictionary<string, string> Values = new Dictionary<string, string>();
        public static string[] keys = {
            "地址","编号",
            "充电","充满","红外","读取","读错",
            "重启","5V警告","弹仓","高温",
            "顶针故障","来电宝故障","到位开关故障","红外故障","摆臂开关故障","电磁阀打开失败","借宝故障",
            "标志","版本",
            "循环次数","容量","电量","电流","电压","温度"
        };
        public ChannelValue()
        {
            foreach (string k in keys)
            {
                Values.Add(k, "");
            }
        }

        private string ValueofByte(byte d, int offset) { return (((d >> offset) & 0x01)==0x01)?"true":"false"; }

        public ChannelValue(int ch, byte[] cdata, int offset):this()
        {
            byte addr = cdata[offset];
            byte state = cdata[offset + 1];
            byte warn = (cdata[offset + 2]);
            byte error = (cdata[offset + 3]);
            string id = Ulitily.ShareClass.hexByteArrayToString(cdata, 4, 10);
            byte ver = cdata[offset + 14];
            int current = ((((int)cdata[offset + 15]) << 8) + ((int)cdata[offset + 16]));
            byte dianliang = cdata[offset + 17];
            int temperature = cdata[offset + 18];
            int cycle = ((((int)cdata[offset + 19]) << 8) + ((int)cdata[offset + 20]));
            int vol = ((((int)cdata[offset + 21]) << 8) + ((int)cdata[offset + 22]));
            int voltage = ((((int)cdata[offset + 23]) << 8) + ((int)cdata[offset + 24]));
            byte biaoji = cdata[offset + 25];
            Values["编号"] = id;
            Values["地址"] = addr.ToString();
            Values[ "版本"]= ver.ToString();
            Values[ "标志"]= biaoji.ToString();
            Values[ "循环次数"]= cycle.ToString();
            Values[ "容量"]= vol.ToString();
            Values[ "电量"]= dianliang.ToString();
            Values[ "电流"]= current.ToString();
            Values[ "电压"]= voltage.ToString();
            Values[ "温度"]= temperature.ToString();

            Values[ "充电"]= ValueofByte(state, 7);
            Values[ "充满"]= ValueofByte(state, 6);
            Values[ "红外"]= ValueofByte(state, 5);
            Values[ "读对"]= ValueofByte(state, 1);
            Values[ "读错"]= ValueofByte(state, 0);

            Values[ "重启"]= ValueofByte(warn, 4);
            Values["5V警告"] = ValueofByte(warn, 2);
            Values[ "弹仓"]= ValueofByte(warn, 1);
            Values[ "高温"]= ValueofByte(warn, 0);

            Values["顶针故障"] = ValueofByte(error, 6);
            Values["来电宝故障"] = ValueofByte(error, 5);
            Values["到位开关故障"] = ValueofByte(error, 4);
            Values["红外故障"] = ValueofByte(error, 3);
            Values["摆臂开关故障"] = ValueofByte(error, 2);
            Values["电磁阀打开失败"] = ValueofByte(error, 1);
            Values["借宝故障"] = ValueofByte(error, 0);
        }

        /// <summary>
        /// 打印输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in keys)
            {
                sb.Append(string.Format("{0}:{1},", k, Values[k]));
            }
            return sb.ToString();
        }
    }


}
