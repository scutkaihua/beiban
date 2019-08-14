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
        private long m_read_index;
        private long m_write_index;
        private long m_buf_size;
        private long m_mask;
        System.Object m_lock = new System.Object();


        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="BufSize">ring buffer 缓冲大小</param>
        public RingSingleBuf(long BufSize)
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
        private long AddIndex(long Index)
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
        public long DataLen
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
        public long ReadBuf(byte[] pBuf, long ReadSize)
        {
            if (this.IsEmpty())
                return 0;

            long len = DataLen;
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
