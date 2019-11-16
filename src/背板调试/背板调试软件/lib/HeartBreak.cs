using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Geared;

namespace LD.lib
{

    public class HeartBreak
    {
        public List<ChannelValue> channelValues = new List<ChannelValue>();

        public int addr;//背板地址

        public int soft;//软件版本

        public int hard;//硬件版本


        public HeartBreak(Ldpacket p)
        {
            addr = p.addr;
            hard = (int)( p.data[2] << 8 + p.data[3]);
            soft = (int)( p.data[4] << 8 + p.data[5]);
            int counter = (p.len - 6) / 26;
            for (int i = 0; i < counter; i++)
            {
                int offset = i * 26 + 6;
                channelValues.Add(new ChannelValue(i, p.data, offset));
            }
        }

    }

}
