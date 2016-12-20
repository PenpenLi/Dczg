using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




public class CNetData
{
    public  static UInt64 Inverse(UInt64 i)
    {
		UInt64	low		=	Inverse((uint)(i>>32));
		UInt64	high	=	((UInt64)Inverse((uint)(i&0xffffffff)))<<32;
        return low|high;    
    }
	public  static Int64 Inverse(Int64 i)
    {
		return (Int64)Inverse((UInt64)i);
	}
	public static float Inverse(float i)
    {
		byte[] data	= BitConverter.GetBytes(i);
		
		byte temp = data[3];
		data[3] = data[0];
		data[0] = temp;
		
		temp = data[2];
		data[2] = data[1];
		data[1] = temp;	
		
		return BitConverter.ToSingle(data, 0);
	}
	public	static	Double Inverse(Double i)
    {
		
		byte[] data	=	BitConverter.GetBytes(i);
		
		byte temp = data[7];
		data[7] = data[0];
		data[0] = temp;
		
		temp = data[6];
		data[6] = data[1];
		data[1] = temp;
		
		temp = data[5];
		data[5] = data[2];
		data[2] = temp;		
		
		temp = data[4];
		data[4] = data[3];
		data[3] = temp;		
		
		return BitConverter.ToDouble(data, 0);
	}
    public static uint Inverse(uint i)
    {
        return (i << 24) |
               ((i >> 8) & 0x00ff00) |
               ((i << 8) & 0xff0000) |
               (i >> 24);
    }
    public static int Inverse(int i)
    {
        return (int)Inverse((uint)i);
    }
    public static ushort Inverse(ushort i)
    {
        uint x = (((uint)i << 8) & 0xff00) | (((uint)i >> 8) & 0x00ff);
        return (ushort)x;
    }
    public static short Inverse(short i)
    {
        uint x = (((uint)i << 8) & 0xff00) | (((uint)i >> 8) & 0x00ff);
        return (short)x;
    }

        public CNetData()
        {
            
            m_iPos = 0;
            m_iSize = 0;
            m_iOffset = 0;
            m_Buffer    = null;
        }
        public CNetData(uint iSize)
        {

            m_iPos = 0;
            m_iSize = (int)iSize;
            m_iOffset = 0;
            m_Buffer = new byte[iSize];
        }
        ~CNetData()
        {

        }
        public void Prepare(byte[] data,int offset)
        {
            m_Buffer    = data;
            m_iSize     = data.Length;
            m_iPos      = offset;
            m_iOffset   = offset;
        }

        public byte[] GetData() { return m_Buffer; }
        public int GetDataLen() { return m_iPos; }
        public void Replace(int offset, byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                m_Buffer[offset + i] = data[i];
            }
        }

        public int Add(byte val)
        {
            if (m_iPos + (int)sizeof(byte) > m_iSize) return -1;

            m_Buffer[m_iPos] = val;
            m_iPos += 1;
            return m_iPos;
        }
        public int Del(ref byte val)
        {
            if (m_iPos + (int)sizeof(byte) > m_iSize) return -1;

            val = m_Buffer[m_iPos];
            m_iPos += 1;
            return m_iPos;
        }
        public int Add(char val)
        {
            if (m_iPos + (int)sizeof(char) > m_iSize) return -1;

            m_Buffer[m_iPos] = (byte)val;
            m_iPos += 1;
            return m_iPos;
        }
        public int Del(ref char val)
        {
            if (m_iPos + (int)sizeof(char) > m_iSize) return -1;

            val = (char)m_Buffer[m_iPos];
            m_iPos += 1;
            return m_iPos;
        }
        public int Add(ushort val)
        {
            if (m_iPos + (int)sizeof(ushort) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref ushort val)
        {
            if (m_iPos + (int)sizeof(ushort) > m_iSize) return -1;


            val = Inverse(BitConverter.ToUInt16(m_Buffer, m_iPos));
            m_iPos += sizeof(ushort);
            return m_iPos;
        }
        public int Add(short val)
        {
            if (m_iPos + (int)sizeof(short) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref short val)
        {
            if (m_iPos + (int)sizeof(short) > m_iSize) return -1;

            val = Inverse(BitConverter.ToInt16(m_Buffer, m_iPos));
            m_iPos += sizeof(short);
            return m_iPos;
        }
        public int Add(uint val)
        {
            if (m_iPos + (int)sizeof(uint) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref uint val)
        {
            if (m_iPos + (int)sizeof(uint) > m_iSize) return -1;

            val = Inverse(BitConverter.ToUInt32(m_Buffer, m_iPos));
            m_iPos += sizeof(uint);
            return m_iPos;
        }
        public int Add(int val)
        {
            if (m_iPos + (int)sizeof(int) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref int val)
        {
            if (m_iPos + (int)sizeof(int) > m_iSize) return -1;

            val = Inverse(BitConverter.ToInt32(m_Buffer, m_iPos));
            m_iPos += sizeof(int);
            return m_iPos;
        }
        public int Add(UInt64 val)
        {
            if (m_iPos + (int)sizeof(UInt64) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref UInt64 val)
        {
            if (m_iPos + (int)sizeof(UInt64) > m_iSize) return -1;

            val = Inverse(BitConverter.ToUInt64(m_Buffer, m_iPos));
            m_iPos += sizeof(UInt64);
            return m_iPos;
        }
        public int Add(Int64 val)
        {
            if (m_iPos + (int)sizeof(Int64) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }
        public int Del(ref Int64 val)
        {
            if (m_iPos + (int)sizeof(Int64) > m_iSize) return -1;

            val = Inverse(BitConverter.ToInt64(m_Buffer, m_iPos));
            m_iPos += sizeof(Int64);
            return m_iPos;
        }
        public int Add(float val)
        {
            if (m_iPos + (int)sizeof(float) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }//08-10-06 liujunhui add: 增加解析float
        public int Del(ref float val)
        {
            if (m_iPos + (int)sizeof(float) > m_iSize) return -1;

            float value = Inverse(BitConverter.ToSingle(m_Buffer, m_iPos));

            m_iPos += sizeof(float);
            return m_iPos;
        }//08-10-06 liujunhui add: 增加解析float
        public int Add(double val)
        {
            if (m_iPos + (int)sizeof(double) > m_iSize) return -1;

            byte[] data = BitConverter.GetBytes(Inverse(val));
            return Add(data);
        }		/// 09-09-07 cwy add 
        public int Del(ref double val)
        {
            if (m_iPos + (int)sizeof(double) > m_iSize) return -1;

            val = Inverse(BitConverter.ToDouble(m_Buffer, m_iPos));

            m_iPos += sizeof(double);
            return m_iPos;
        }	/// 09-09-07 cwy add
        public int Add(char[] val)
        {
            byte[] data = Encoding.UTF8.GetBytes(val);
            Add(data.Length);
            return Add(data);
        }
        public int Del(ref char[] val)
        {
            int size    =   0;
            Del(ref size);
            m_iPos += sizeof(int);
            if (size > 0)
            {
                val = Encoding.UTF8.GetChars(m_Buffer, m_iPos, size);
                m_iPos += size;
            }
            
            return m_iPos;
        }
        public int Add(String val)
        {
            if (!String.IsNullOrEmpty(val))
            {
                byte[] data = Encoding.UTF8.GetBytes(val);
               
                Add(data.Length);
                return Add(data);
            }
            else
            {
                int len = 0;
                return Add(len);
            }
        }
        public int Del(ref string val)
        {
            int len = 0;
            if (Del(ref len) == -1)
                return -1;

            if (len > 0)
            {
                val = Encoding.UTF8.GetString(m_Buffer, m_iPos, len);
            }
            return m_iPos;
        }
        //public int Strnlen(ref char[] val) { }
        public int Add(byte[] val)
        {
            for (Int32 i = 0; i < val.Length; i++)
            {
                m_Buffer[m_iPos++] = val[i];
            }
            return m_iPos;
        }
        public int Del(ref byte[] val)
        {
            for (Int32 i = 0; i < val.Length; i++)
            {
                val[i] = m_Buffer[m_iPos++];
            }
            return m_iPos;
        }
        public int Add(byte[] val,int iSize)
        {
            for (Int32 i = 0; i < iSize; i++)
            {
                m_Buffer[m_iPos++] = val[i];
            }
            return m_iPos;
        }
        public int Del(ref byte[] val, int iSize)
        {
            for (Int32 i = 0; i < iSize; i++)
            {
                 val[i] =   m_Buffer[m_iPos++];
            }
            return m_iPos;
        }
        public int AddString(byte[] val)
        {
            ushort StrLength = 0;
            for (; StrLength < val.Length; StrLength++)
            {
                if (val[StrLength] == 0x0)
                {
                    break;
                }
            }
            Add(StrLength);
            return Add(val, StrLength);
        }
        public int DelString(ref byte[] val)
        {
            ushort StrLength = 0;
            Del(ref StrLength);
            for (ushort i = 0; i < StrLength; i++)
            {
                val[i] = m_Buffer[m_iPos++];
            }

            return m_iPos;
        }


        public byte[]   m_Buffer;
        public int      m_iPos;
        public int      m_iOffset;
        protected int   m_iSize;
    }

