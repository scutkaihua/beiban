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

        UpdateEntry = 0x05,
        UpdateStart = 0x06,
        UpdateData = 0x07,

        DebugInfo = 0x55,

        Shake_Hand = 0x16,
    }


    public enum IRCMD {

        READ_ID = 10,
        READ_DATA = 20,
        UNLOCK = 30,
        LOCK = 50,
        UNLOCK_1HOUR = 40,
    }

    public enum Lease_Error{
         失败 = 0,
         成功 = 1,
         宝与仓道不符 = 2,
         解使能失败 = 3,
         电磁阀失败 = 4
         
    }

    public enum Ctrl {
          重启 =0,
          运维 = 1,
          强制开仓 = 2,
    };
}
