using System;
using System.Collections.Generic;
using System.Text;

namespace LD.lib
{

    /// <summary>
    /// 环形缓冲类
    /// </summary>
    class RingSingleBuf
    {
        private byte[] m_ring_buf;
        private int m_read_index;
        private int m_write_index;
        private int m_buf_size;
        private int m_mask;
        System.Object m_lock = new System.Object();


        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="BufSize">ring buffer 缓冲大小</param>
        public RingSingleBuf(int BufSize)
        {
            m_ring_buf = new byte[BufSize];
            m_buf_size = BufSize;
            m_mask = BufSize - 1;
            m_read_index = 0;
            m_write_index = 0;
        }


        /// <summary>
        /// 索引++
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        private int AddIndex(int Index)
        {
            return ((Index + 1) & m_mask);
        }

        /// <summary>
        /// 缓冲是否满
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return (((m_write_index-m_read_index)&m_mask) == m_mask);
        }

        /// <summary>
        /// 缓冲是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (m_write_index == m_read_index);
        }

        /// <summary>
        /// 写入Len长度数据 到 ringbuffer
        /// </summary>
        /// <param name="pBuf"></param>
        /// <param name="Len"></param>
        public void WriteBuf(byte[] pBuf, int Len)
        {
            for (int i = 0; i < Len; ++i)
            {
                if (this.IsFull())
                {
                    //m_read_index = AddIndex(m_read_index);
                    break;
                }
                m_ring_buf[m_write_index] = pBuf[i];
                m_write_index = AddIndex(m_write_index);
            }
        }


        /// <summary>
        /// 接收数据长度
        /// </summary>
        public int DataLen
        {
            get
            {
                return ((m_write_index - m_read_index) & m_mask);
            }
        }

        /// <summary>
        /// 读取ReadSize字节到pBuf
        /// </summary>
        /// <param name="pBuf"></param>
        /// <param name="ReadSize"></param>
        /// <returns></returns>
        public int ReadBuf(byte[] pBuf, int ReadSize)
        {
            if (this.IsEmpty())
                return 0;

            int len = DataLen;
            if (ReadSize > len)
                ReadSize = len;
            for (int i = 0; i < ReadSize; i++)
            {
                pBuf[i] = m_ring_buf[m_read_index];
                m_read_index = AddIndex(m_read_index);
            }

            return ReadSize;
        }


        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            byte c = 0xff;
            if (((m_write_index-m_read_index)&m_mask) > 0)
            {
                c = m_ring_buf[m_read_index];
                m_read_index = AddIndex(m_read_index);
            }
            return c;
        }
    }
}
