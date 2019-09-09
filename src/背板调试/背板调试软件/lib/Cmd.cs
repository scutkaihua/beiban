using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD.lib
{
    public enum Cmd
    {
        心跳 = 0x01,
        租借 = 0x02,
        归还 = 0x03,
        控制 = 0x04,
        设置地址 = 0x06,

        进入升级 = 0x05,
        开始升级 = 0x06,
        升级数据 = 0x07,

        调试信息 = 0x55,

        握手 = 0x16,
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
