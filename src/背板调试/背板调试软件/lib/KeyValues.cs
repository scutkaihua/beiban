using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.lib
{
    public class KeyValues<KT,VT>
    {
        public KT key;
        public List<VT> values = new List<VT>();

        public KeyValues() { }

        public KT Key{get{ return key; }set { key = value; } }
        public List<VT> Values { get { return values; } }

        public void AddValue(VT v)
        {
            values.Add(v);
        }

    }


    public class KeyValue<KT,VT>
    {
        public KT key;
        public VT val;

        public KeyValue(KT k,VT v) { key = k;val = v; }
    }
}
