using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LD.lib
{

    public class ChannelValueSelectItem {

        public int r;
        public int c;

        public Color color;
        public string name;

        public ChannelValueSelectItem(int _r, int _c, Color _color, string _n) {
            r = _r;
            c = _c;
            color = _color;
            name = _n;
        }
    }

    public class ChannelValueSelectItems{
        public int channel; //通道索引 1-n
        public List<ChannelValueSelectItem> names = new List<ChannelValueSelectItem>();

        public void Add(int r,int c,string name,Color color)
        {
            foreach(ChannelValueSelectItem item in names)
            {
                if (item.name == name) return;
            }
            names.Add(new ChannelValueSelectItem(r,c,color,name));
        }

        public void Remove(string name)
        {
            foreach (ChannelValueSelectItem item in names)
            {
                if (item.name == name) { names.Remove(item); return; }
            }
        }
    }
}
