//using UnityEngine;
using System.Collections;
using System.Text;
using System;
public class Helper{
    public static   void    StringToByte(string str, ref byte[] outbyte,ref int length)
    {
		outbyte = System.Text.Encoding.UTF8.GetBytes(str);
        length  = outbyte.Length;
    }
    public static void StringToByte(string str, ref byte[] outbyte, ref byte length)
    {
		outbyte = System.Text.Encoding.UTF8.GetBytes(str);
        length = (byte)outbyte.Length;
    }
    public static void StringToByte(string str, ref byte[] outbyte, ref ushort length)
    {
		outbyte = System.Text.Encoding.UTF8.GetBytes(str);
        length = (ushort)outbyte.Length;
    }
    public static   string  ByteToString(byte[] outbyte, int length)
    {
		return System.Text.Encoding.UTF8.GetString(outbyte, 0, length);
    }
    public static void Write(byte[] buffer, int value, ref int offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static int ReadI32(byte[] buffer, ref int offset)
    {
        int value = BitConverter.ToInt32(buffer, offset);

        offset += 4;

        return value;
    }
    public static uint ReadU32(byte[] buffer, ref int offset)
    {
        uint value = BitConverter.ToUInt32(buffer, offset);

        offset += 4;

        return value;
    }
    public static short ReadI16(byte[] buffer, ref int offset)
    {
        short value = BitConverter.ToInt16(buffer, offset);

        offset += 2;

        return value;
    }
    public static ushort ReadU16(byte[] buffer, ref int offset)
    {
        ushort value = BitConverter.ToUInt16(buffer, offset);

        offset += 2;

        return value;
    }

    public static void Write(byte[] buffer, float value, ref int offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static float ReadFloat(byte[] buffer, ref int offset)
    {
        float value = BitConverter.ToSingle(buffer, offset);

        offset += 4;

        return value;
    }
    public static double ReadDouble(byte[] buffer, ref int offset)
    {
        double value = BitConverter.ToDouble(buffer, offset);

        offset += 8;

        return value;
    }

    public static void Write(byte[] buffer, bool value, ref int offset)
    {
        byte[] bTemp = BitConverter.GetBytes(value);
        for (Int32 i = 0; i < bTemp.Length; i++)
        {
            buffer[offset++] = bTemp[i];
        }
    }

    public static bool ReadBoolean(byte[] buffer, ref int offset)
    {
        Boolean value = BitConverter.ToBoolean(buffer, offset);

        offset += 1;

        return value;
    }

    public static void Write(byte[] buffer, string value, ref int offset)
    {
        if (!String.IsNullOrEmpty(value))
        {
            byte[] bTemp = Encoding.UTF8.GetBytes(value);

            Write(buffer, bTemp.Length, ref offset);
            for (Int32 i = 0; i < bTemp.Length; i++)
            {
                buffer[offset++] = bTemp[i];
            }
        }
        else
        {
            Write(buffer, 0, ref offset);
        }
    }

    public static string ReadString(byte[] buffer, ref int offset)
    {
        int len = ReadI32(buffer, ref offset);

        string str = String.Empty;

        if (len > 0)
        {
            byte[] bTemp = new byte[len];
            Buffer.BlockCopy(buffer, offset, bTemp, 0, len);
            offset += len;

            str = Encoding.UTF8.GetString(bTemp);
        }
        return str;
    }

}
