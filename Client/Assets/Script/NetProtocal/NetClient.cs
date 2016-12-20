
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;

public  delegate    void        MsgCallback(ushort id, ref CNetData data);


public class NetThread{
	protected	Thread		m_Thread;
	protected	byte[] 		m_Buffer;
	protected	int			m_Length;
	protected	int			m_Current;
	protected 	Socket		m_Socket;
	protected 	object 		m_Lock;
	protected	NetThread(int iLength){
		m_Length	=	iLength;
		m_Current	=	0;
		m_Buffer	=	new byte[iLength];
		m_Thread	=	null;
		m_Socket	=	null;
		m_Lock		=	new object();
	}
	
	public void	Stop(){
        //if (m_Thread!=null)
		 //   m_Thread.Abort();
	}


	protected 	uint		ParsePackage(ref byte[] data,uint offset,uint size){
		//包头不完整
		if(size<sizeof(uint)){
			return 0;
		}else{
			int 	temp		=	(int)offset;
			uint	iDataLen	=	Helper.ReadU32(data,ref temp);
			{
				uint iComplateLen	=	iDataLen	+	sizeof(uint);
				if(iComplateLen <= size){//大于一个包
					return iComplateLen;
				}else{//包内容不完整
					return 0;
				}
			}
		}
	
	}
}
public class NetSendThread	: NetThread{
	public	NetSendThread():base(8192){
		
	}
	public void	Start(ref Socket rSocket){
		m_Socket	=	rSocket;
		//m_Thread	=	 new Thread(new ThreadStart(ThreadMain));  
	}
    public bool Send(ref CNetData sendData)
    {
		int iSend	=	0;
		lock(m_Lock)
		{
            try
            {
                iSend = m_Socket.Send(sendData.m_Buffer, sendData.m_iPos, 0);
            }
            catch (Exception e)
            {
                SGDP.DebugPrint(e.Message);
                return false;
            }
		}
		return true;
	}
	protected	void ThreadMain(){
		int iSendByte	=	0;
		int iSend		=	0;
		while(true){
			try{
				iSendByte = m_Socket.Send(m_Buffer,iSend,m_Current-iSend,0);
			}catch(Exception e){
				SGDP.DebugPrint(e.Message);
				return;
			}
			iSend	+=	iSendByte;
			if(iSend>=m_Current){
				
			}
			
		}
	}
}
public class NetRecvThread	: NetThread{
    NetClient m_Client;
	public NetRecvThread():base(256*1024){
		
	}
	public void	Start(ref Socket rSocket,NetClient client){
		m_Socket	=	rSocket;
        m_Client    =   client;
		m_Thread	=	 new Thread(new ThreadStart(ThreadMain));
        m_Thread.Start();
	}
	protected	void ThreadMain(){
		//byte[] temp = new byte[8192];
		int 	iSize	=	0;
		uint	iRead	=	0;
		while(true){
			try{
				int iRecv	=	m_Socket.Receive(m_Buffer,iSize,m_Length-iSize,0);
				if(iRecv <=0){
					break;
				}
				iSize	+=	iRecv;
			}catch(SocketException  e){
				SGDP.DebugPrint("recv Failed!");//e.Message);
                m_Client.DisConnect();
				return;
			}
			//反复
			while(true){
				uint iUnRead		=	(uint)iSize	-	iRead;
				uint iPackSize		=	SGDP.ParsePackage(ref m_Buffer,iRead,iUnRead);
				if(iPackSize==0){
					//复制数据到缓冲区头部
					if(iUnRead>0 && iRead>0){
						for(uint i=0;i<iUnRead;i++){
							m_Buffer[i]	=	m_Buffer[iRead+i];
						}						
					}
					iRead	=	0;
					iSize	=	(int)iUnRead;
					break;
				}else{
					//OnRecv(m_Buffer,(int)iRead);
					SGDP.GetInstance().OnRecv(m_Buffer,(int)iRead);
					iRead+=iPackSize;
				}
			}
		}
	}

}


public class NetClient {
	Socket		m_ClientSocket;
	//string		m_ServerIP;
	//int			m_ServerPort;
	bool		m_bConnect;
	EndPoint	m_IP_Port;
	NetSendThread	m_Send;
	NetRecvThread	m_Recv;
    object lockDisconnect = new object();
	
	public NetClient(){
		
		m_bConnect			=	false;
		
		m_Send				=	new NetSendThread();
		m_Recv				=	new NetRecvThread();
       
	}
    public  void Start(string strIP, int port)
    {
		m_IP_Port	=	new IPEndPoint(System.Net.IPAddress.Parse(strIP),port);	
	}
	public void	Start(long ip,int port){
		//m_IP 				= new IPAddress(ip);
		//m_ServerPort		=	port;	
		m_IP_Port	=	new IPEndPoint(ip,port);
	}
	public bool	Connect(){
		if(m_bConnect){
			DisConnect();
		}
		bool bException = false;
		try{
            Debug.Log("Connect To " + m_IP_Port.ToString());
			m_ClientSocket		=	new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			m_ClientSocket.Connect(m_IP_Port);
		}catch(System.Net.Sockets.SocketException  e){
            SGDP.DebugPrint(e.Message);
			bException	=	true;
		}
		if(!bException){
			
			m_bConnect	=	true;
        	m_Send.Start(ref m_ClientSocket);
       	 	m_Recv.Start(ref m_ClientSocket, this);
        	SGDP.DebugPrint("Connected OK\n");
		}
		return m_bConnect;
	}
	public void	Update(){
		if(!m_bConnect){
			if(m_IP_Port==null){
				Connect();
			}
		}
	}
	public bool	Send(CMessage msg){
        if (msg == null)
            return false;
        if (msg.GetID() == 0xffff)
        {
            return false;
        }
		if(m_bConnect){
            CNetData data = SGDP.GetInstance().Encode(msg);
            if (data==null)
                return false;
            //Debug.Log("Send=" + msg.GetType().ToString());
            if (!m_Send.Send(ref data))
            {
                Debug.Log("Send Message DisConnect");
                DisConnect();
                return false;
            }
		}
		return true;
	}
	public void DisConnect(){
        lock (lockDisconnect)
        {
            if (m_ClientSocket != null)
            {
                try
                {
                    m_ClientSocket.Disconnect(false);
                    //Thread.Sleep(100);
                    //m_ClientSocket.Shutdown(SocketShutdown.Both);
                    m_ClientSocket.Close(0);
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    Debug.Log("DisConnect"+e.Message);
                }
                m_ClientSocket = null;
            }
        }
		
		m_Send.Stop();
		m_Recv.Stop();
		


		m_bConnect=false;
        SGDP.DebugPrint("DisConnect\n");
	}
	public	bool	IsConnect(){
        return m_bConnect;
    }

}
