using System;
using System.Runtime.InteropServices;

namespace LD.lib
{
    ///
    /// API串口类 叶帆修改 http://blog.csdn.net/yefanqiu
    /// 2014.1.4  zyf 修改
    /// 1.修改了串口参数形式,改为9600,N,8,1格式
    /// 2.增加了ClearCommError API函数,用于获取可读字节数
    ///
    public class CommPort
    {
        ///
        ///端口名称(COM1,COM2...COM4...)
        ///
        public string PortName = "COM3:";
        ///
        /// 波特率(9600,N,8,1)
        ///
       // public string CommSetting = "2400,E,8,1";

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate = 9600;
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits = 8;

        /// <summary>
        /// 停止位  0 :1 bit    1:1.5bits   2:2bits
        /// </summary>
        public int StopBits = 1;

        /// <summary>
        /// 校验位  0:NONE   1:ODD  2:EVEN
        /// </summary>
        public int Parity = 2;
        //serialport.DataBits = 8;
        //serialport.StopBits = 1;
        //serialport.Parity = 2;
        //serialport.BaudRate = (int)UInt32.Parse(cb_baudrate.Text,System.Globalization.NumberStyles.Number);
        ///
        ///超时长
        ///
        public int ReadTimeout = 200;
        ///
        ///串口是否已经打开
        ///
        public bool IsOpen = false;
        ///
        /// COM口句柄
        ///
        private int hComm = -1;

        #region "API相关定义"
        private const string DLLPATH = "kernel32.dll"; // "kernel32";

        ///
        /// WINAPI常量,写标志
        ///
        private const uint GENERIC_READ = 0x80000000;
        ///
        /// WINAPI常量,读标志
        ///
        private const uint GENERIC_WRITE = 0x40000000;
        ///
        /// WINAPI常量,打开已存在
        ///
        private const int OPEN_EXISTING = 3;
        ///
        /// WINAPI常量,无效句柄
        ///
        private const int INVALID_HANDLE_VALUE = -1;

        private const int PURGE_RXABORT = 0x2;
        private const int PURGE_RXCLEAR = 0x8;
        private const int PURGE_TXABORT = 0x1;
        private const int PURGE_TXCLEAR = 0x4;


        private const int FILE_FLAG_OVERLAPPED = 0x40000000;

        ///
        ///设备控制块结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            ///
            /// DCB长度
            ///
            public int DCBlength;
            ///
            ///指定当前波特率
            ///
            public int BaudRate;
            ///
            ///标志位
            ///
            public uint flags;
            ///
            ///未使用,必须为0
            ///
            public ushort wReserved;
            ///
            ///指定在XON字符发送这前接收缓冲区中可允许的最小字节数
            ///
            public ushort XonLim;
            ///
            ///指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数
            ///
            public ushort XoffLim;
            ///
            ///指定端口当前使用的数据位
            ///
            public byte ByteSize;
            ///
            ///指定端口当前使用的奇偶校验方法,可能为:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY 0-4=no,odd,even,mark,space
            ///
            public byte Parity;
            ///
            ///指定端口当前使用的停止位数,可能为:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS 0,1,2 = 1, 1.5, 2
            ///
            public byte StopBits;
            ///
            ///指定用于发送和接收字符XON的值 Tx and Rx XON character
            ///
            public byte XonChar;
            ///
            ///指定用于发送和接收字符XOFF值 Tx and Rx XOFF character
            ///
            public byte XoffChar;
            ///
            ///本字符用来代替接收到的奇偶校验发生错误时的值
            ///
            public byte ErrorChar;
            ///
            ///当没有使用二进制模式时,本字符可用来指示数据的结束
            ///
            public byte EofChar;
            ///
            ///当接收到此字符时,会产生一个事件
            ///
            public byte EvtChar;
            ///
            ///未使用
            ///
            public ushort wReserved1;
        }

        ///
        ///串口超时时间结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        ///
        ///溢出缓冲区结构体类型
        ///
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }
        ///
        /// ClearCommError的结构
        ///
        private struct COMSTAT
        {
            internal const uint fCtsHold = 0x1;
            internal const uint fDsrHold = 0x2;
            internal const uint fRlsdHold = 0x4;
            internal const uint fXoffHold = 0x8;
            internal const uint fXoffSent = 0x10;
            internal const uint fEof = 0x20;
            internal const uint fTxim = 0x40;
            internal UInt32 Flags;

            ///
            /// 输入缓存中字节个数
            ///
            public uint cbInQue;
            ///
            /// 输出缓存中字节个数
            ///
            public uint cbOutQue;

        }
        ///
        ///打开串口
        ///
        ///要打开的串口名称
        ///指定串口的访问方式，一般设置为可读可写方式
        ///指定串口的共享模式，串口不能共享，所以设置为0
        ///设置串口的安全属性，WIN9X下不支持，应设为NULL
        ///对于串口通信，创建方式只能为OPEN_EXISTING
        ///指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信
        ///对于串口通信必须设置为NULL
        [DllImport(DLLPATH)]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode,
        int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        ///
        ///得到串口状态
        ///
        ///通信设备句柄
        ///设备控制块DCB
        [DllImport(DLLPATH)]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);

        ///
        ///建立串口设备控制块(嵌入版没有)
        ///
        ///设备控制字符串
        ///设备控制块
        [DllImport(DLLPATH)]
        private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);

        ///
        ///设置串口状态
        ///
        ///通信设备句柄
        ///设备控制块
        [DllImport(DLLPATH)]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);

        ///
        ///读取串口超时时间
        ///
        ///通信设备句柄
        ///超时时间
        [DllImport(DLLPATH)]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///
        ///设置串口超时时间
        ///
        ///通信设备句柄
        ///超时时间
        [DllImport(DLLPATH)]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///
        /// 清除串口错误或者读取串口现在的状态 
        ///
        ///
        ///
        ///
        [DllImport(DLLPATH)]
        private static extern bool ClearCommError(IntPtr hFile, ref uint lpErrors, ref COMSTAT lpStat);
        ///
        ///读取串口数据
        ///
        ///通信设备句柄
        ///数据缓冲区
        ///多少字节等待读取
        ///读取多少字节
        ///溢出缓冲区
        [DllImport(DLLPATH)]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
        ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        ///
        ///写串口数据
        ///
        ///通信设备句柄
        ///数据缓冲区
        ///多少字节等待写入
        ///已经写入多少字节
        ///溢出缓冲区
        [DllImport(DLLPATH)]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite,
        ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool FlushFileBuffers(int hFile);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool PurgeComm(int hFile, uint dwFlags);

        [DllImport("kernel32.dll")]
        private static extern int CreateEvent(IntPtr lpEventAttributes,
                    bool bManualReset,
                    bool bInitialState,
                    string lpName);
        [DllImport("kernel32")]
        private static extern bool SetupComm(int hFile, int dwInQueue, int dwOutQueue);

        [DllImport("kernel32")]
        private static extern int PurgeComm(int hFile, int dwFlags);
        ///
        ///关闭串口
        ///
        ///通信设备句柄
        [DllImport(DLLPATH)]
        private static extern bool CloseHandle(int hObject);

        ///
        ///得到串口最后一次返回的错误
        ///
        [DllImport(DLLPATH)]
        private static extern uint GetLastError();
        #endregion

        ///
        ///设置DCB标志位
        ///
        ///
        ///
        ///
        internal void SetDcbFlag(int whichFlag, int setting, DCB dcb)
        {
            uint num;
            setting = setting << whichFlag;
            if ((whichFlag == 4) || (whichFlag == 12))
            {
                num = 3;
            }
            else if (whichFlag == 15)
            {
                num = 0x1ffff;
            }
            else
            {
                num = 1;
            }
            dcb.flags &= ~(num << whichFlag);
            dcb.flags |= (uint)setting;
        }

        ///
        ///建立与串口的连接,返回0为成功,非0为不成功
        ///
        public int Open()
        {
            DCB dcb = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();

            // 打开串口
            hComm = CreateFile(PortName, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0);
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return -1;
            }
            // 设置通信超时时间
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutConstant = 100;
            SetCommTimeouts(hComm, ref ctoCommPort);

            //设置串口参数
            if (!GetCommState(hComm, ref dcb))
            {
                return -1;
            }

            //串口参数
            String CommSetting = BaudRate.ToString() + ",";
            switch (Parity)
            {
                case 0: CommSetting += "N,"; break;
                case 1: CommSetting += "O,"; break;
                case 2: CommSetting += "E,"; break;
                default:
                    CommSetting += "N,";break;
            }
            CommSetting += DataBits.ToString() + ","+ StopBits.ToString();


            //修改dcb数据,将需要的波特率设置进去
            if (!BuildCommDCB(CommSetting, ref dcb))
            {
                return -1;
            }
            //设置波特率
            if (!SetCommState(hComm, ref dcb))
            {
                return -1;
            }

            if (!SetupComm(hComm, 4096, 1024))
            {
                Close();
                return -1;
            }
            Write(new byte[] { 00 }, 1);
            PurgeComm(hComm, 0x000F);
            IsOpen = true;
            return 0;
        }
        ///
        ///关闭串口,结束通讯
        ///
        public void Close()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hComm);
            }

            IsOpen = false;
        }
        ///
        /// 获取输入缓存中存在的字节数
        ///
        /// 可读字节数 ; 返回 -1 时,出错
        public long ByteToRead()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                COMSTAT lSTAT = new COMSTAT();
                uint lerror = 0;
                bool f = ClearCommError((IntPtr)hComm, ref lerror, ref lSTAT);
                return lSTAT.cbInQue;
            }
            else
            {
                return -1;
            }
        }
        ///
        ///读取串口返回的数据
        ///
        ///数据长度
        public int Read(ref byte[] bytData, int NumBytes)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                ovlCommPort.hEvent = CreateEvent(new IntPtr(0), true, false, null);
                int BytesRead = 0;
                ReadFile(hComm, bytData, NumBytes, ref BytesRead, ref ovlCommPort);
                return BytesRead;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 读取串口返回的数据
        /// </summary>
        /// <param name="NumBytes"></param>
        /// <returns>返回的数据</returns>
        public byte[] Read(int NumBytes)
        {
            if (ByteToRead() < NumBytes) NumBytes = (int)ByteToRead();
            if (NumBytes > 0)
            {
                byte[] r = new byte[NumBytes];
                Read(ref r, NumBytes);

                return r;

            }
            else return null;
        }

        ///
        ///向串口写数据
        ///
        ///数据数组
        public int Write(byte[] WriteBytes, int intSize)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                ClearSendBuf();
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                IntPtr intPtr = new IntPtr();
                ovlCommPort.hEvent = CreateEvent(intPtr,true,false,null);                /*2019-7-27必须加入这个，不然写不成功*/
                bool result = WriteFile(hComm, WriteBytes, intSize, ref BytesWritten, ref ovlCommPort);
                return BytesWritten;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 串口写数据
        /// </summary>
        /// <param name="WriteBytes"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int Write(byte[] WriteBytes, int offset, int len)
        {
            if (offset == 0)return  Write(WriteBytes, len);
            else
            {
                byte[] w = new byte[len];
                Array.Copy(WriteBytes, offset, w, 0, len);
                return Write(w, len);
            }

            return 0;
        }

        ///
        ///清除接收缓冲区
        ///
        ///
        public void ClearReceiveBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_RXABORT | PURGE_RXCLEAR);
            }
        }

        ///
        ///清除发送缓冲区
        ///
        public void ClearSendBuf()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_TXABORT | PURGE_TXCLEAR);
            }
        }
    }


}
