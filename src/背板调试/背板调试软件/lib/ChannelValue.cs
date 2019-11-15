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
            "地址","id",
            "充电","充满","红外","读取","读错",
            "重启","5V警告","弹仓","高温",
            "顶针故障","来电宝故障","到位开关故障","红外故障","摆臂开关故障","电磁阀打开失败","借宝故障",
            "标志","版本","标志",
            "循环次数","容量","电量","电流","电压","温度"
        };
        public ChannelValue()
        {
            foreach (string k in keys)
            {
                Values.Add(k, "");
            }
        }

        public ChannelValue(Ldpacket p):this()
        {

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
