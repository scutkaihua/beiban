using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.lib
{
    public enum Cmd
    {
        Heart_Break = 0x01,
        Lease = 0x02,
        Return = 0x03,
        Ctrl = 0x04,
        Set_Addr = 0x06,

        UpdateStart = 0x05,
        UpdateData = 0x06,
        UpdateEnd = 0x07,

        Shake_Hand = 0x16,
    }
}
