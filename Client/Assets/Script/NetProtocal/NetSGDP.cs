using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public struct MemberInfo
{
    public enum MIType
    {
        eByte,
        eShort,
        eUShort,
        eInt,
        eUInt,
        eInt64,
        eUInt64,
        eFloat,
        eDouble,
        eString,
        eBuffer,
        eMessage
    }
    
    public string   name;
    public MIType   type;
    public int      size;
    public int      count;
}
public class CMessage
{
    public  virtual ushort          GetID()
    {
        return 0xffff;
    }
    public  virtual MemberInfo[]    GetDesc()
    {
        return null;
    }
    public  virtual object          GetMemberValue(int idx)
    {
        return (int)0;
    }
    
}

public delegate int         EncoderFunc(CMessage msg, ref CNetData poNetData);
public delegate CMessage    DecoderFunc(ref CNetData poNetData);
public delegate void        MessageCallback(int iMsgID, ref CMessage msg);

public class SGDP{
	
	
	public	static 	ushort 	PKG_MARK    	=	0xAAEE;
	public	static 	int		HEADER_LENGTH	=	14;
	public	static	int		DATA_LEN_OFFSET	=	4;
	public	static	int		MSG_OFFSET		=	8;
	public	class SGTMsgHeader
	{
		public	ushort	wCheckSum;  //校验码		=(wDataLen ^ 0xBBCC) & 0x88AA;
		public	ushort	wMark;      //包头标示	=PKG_MARK
		public	ushort	wDataLen;   //数据包长度	=SGTMsgHeader+SGSMsgHeader+Data
		public	byte	byFlags;	//			=0
		public	byte	byOptLen;	//			=0
		public	ushort  wMsgID; 	// 协议ID
		public	uint	dwTransID; 	// 传输ID	=0
		public	SGTMsgHeader()
		{
			wCheckSum	=	0;
			wMark		=	PKG_MARK;
			wDataLen	=	0;
			byFlags		=	0;
			byOptLen	=	0;
			wMsgID		=	0;
			dwTransID	=	0;
		}
		public	void	Encode(CNetData data)
		{
			data.Add(wCheckSum);
			data.Add(wMark);
			data.Add(wDataLen);
			data.Add(byFlags);
			data.Add(byOptLen);
			data.Add(wMsgID);
			data.Add(dwTransID);
		}
		public	void	Decode(CNetData data)
		{
			data.Del(ref wCheckSum);
			data.Del(ref wMark);
			data.Del(ref wDataLen);
			data.Del(ref byFlags);
			data.Del(ref byOptLen);
			data.Del(ref wMsgID);
			data.Del(ref dwTransID);
		}
	}


	public static uint	ParsePackage(ref byte[] data,uint offset,uint size){
		//包头不完整
		if(size<14){
			return 0;
		}else{
			int 	temp		=	(int)offset+DATA_LEN_OFFSET;
			ushort	iDataLen	=	CNetData.Inverse(Helper.ReadU16(data,ref temp));
			temp=(int)offset+MSG_OFFSET;
			ushort	iMsgID		=	CNetData.Inverse(Helper.ReadU16(data,ref temp));
			{
				if(iDataLen+8 <= size){//大于一个包
					return (uint)iDataLen+8;
				}else{//包内容不完整
					return 0;
				}
			}
		}
		
	}

	public	static	void	Error(uint iErrorCode)
	{
        Debug.Log(ErrorString(iErrorCode));
	}
    public static string ErrorString(uint iErrorCode)
    {
        Hashtable table = sdConfDataMgr.Instance().GetTable("errorcode");

        if (table != null)
        {
            Hashtable strError = table[iErrorCode.ToString()] as Hashtable;
            if (strError != null)
            {
                string strMessage = strError["message"] as string;
                if (strMessage != null)
                {
                    return strMessage;
                }
            }
        }
        return "ErrorID=" + iErrorCode;
    }
	
    public	static	void	DebugPrint(string str)
	{
		SDGlobal.Log(str);
        //Console.WriteLine(str);
	}
    public static SGDP instance=null;

    EncoderFunc[]       m_Encoder;
    DecoderFunc[]       m_Decoder;
    MessageCallback[]   m_MessageCallback;


	List<CMessage>		m_MessageList;
	object				m_lock	=	new object();

    //byte[] m_SendBuffer;
    public static SGDP GetInstance()
    {
        if (instance == null)
        {                  
            instance = new SGDP();
        }
        return instance;
    }

    public SGDP()
    {
        //m_SendBuffer = new byte[4096];
        m_Encoder       =   new EncoderFunc[65536];
        m_Decoder       =   new DecoderFunc[65536];
        m_MessageCallback = new MessageCallback[65536];
        instance = this;
		m_MessageList	=	new List<CMessage>();
    }

    public void AddEncode_Decode(ushort iMsgID, EncoderFunc encode, DecoderFunc decode)
    {
        m_Encoder[iMsgID] = encode;
        m_Decoder[iMsgID] = decode;
    }
    public void SetCallback(ushort iMsgID, MessageCallback cb)
    {
        m_MessageCallback[iMsgID] = cb;
    }
    public MessageCallback GetCallback(ushort iMsgID)
    {
        return m_MessageCallback[iMsgID];
    }

    public CNetData Encode(CMessage msg)
    {
        ushort id = msg.GetID();
        CNetData data = new CNetData(4096);
		
		SGTMsgHeader	head = new  SGTMsgHeader();
		head.wMsgID			=	id;
		head.Encode(data);

        EncoderFunc pfnEncode = FindEncodeFunc(id);
        if (null == pfnEncode)
            return null;
        if (-1 == pfnEncode(msg, ref data))
        {
            return null;
        }
        head.wDataLen 	= 	(ushort)(data.GetDataLen()-8);
        head.wCheckSum	=	(ushort)((head.wDataLen	^ 0xBBCC) & 0x88AA);
        data.Replace(0, BitConverter.GetBytes(CNetData.Inverse(head.wCheckSum)));
        data.Replace(4, BitConverter.GetBytes(CNetData.Inverse(head.wDataLen)));
		return data;
    }

    public CMessage Decode(ushort iMsgID, CNetData data)
    {
        DecoderFunc pfnDecode = FindDecodeFunc(iMsgID);
        if (null == pfnDecode)
            return null;

        return pfnDecode(ref data);
    }

    public EncoderFunc FindEncodeFunc(ushort iMsgID)
    {
        return m_Encoder[iMsgID];
    }

    public DecoderFunc FindDecodeFunc(ushort iMsgID)
    {
        return m_Decoder[iMsgID];
    }
	
	public void	OnRecv(byte[] buffer,int offset){
        CNetData data = new CNetData();
        data.Prepare(buffer, offset);
		
		SGTMsgHeader	head = new  SGTMsgHeader();
		head.Decode(data);

        CMessage msg = Decode((ushort)head.wMsgID, data);
        //Debug.Log("Recv=" + msg.GetType().ToString());
        if (msg != null)
        {
			lock(m_lock)
			{
				m_MessageList.Add(msg);
			}
        }
		else
		{
			Debug.Log(head.wMsgID.ToString() + " Decode Failed!");
		}
	}

	public void	Process()
	{
		List<CMessage> tempList = null;
		lock(m_lock)
		{
			if(m_MessageList.Count>0){
				tempList = m_MessageList;
				m_MessageList = new List<CMessage>();
			}
		}
		if (tempList != null)
		{
			for (int i = 0; i < tempList.Count; i++)
			{
				CMessage msg = tempList[i];
				ushort id = msg.GetID();
				MessageCallback cb = GetCallback((ushort)id);
		        if (cb != null)
		        {
					//long nBegin = System.DateTime.Now.Ticks;
		            cb((ushort)id, ref msg);
					//long nEnd = System.DateTime.Now.Ticks;
					//SDGlobal.Log("process msg:" + id.ToString() + ", elapse:" + ((nEnd - nBegin)/10000L).ToString() + " ms");
		        }
			}
		}
	}
}
