using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Net.Security;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// 角色基础信息
/// </summary>
public class sdPlayerInfo
{
	public string mRoleName = null;			//< 名称aa
	public ulong mDBID = ulong.MaxValue;	//< DBIDaa
	public byte mGender = 0;				//< 性别aa
	public byte mSkinColor = 0;				//< 肤色aa
	public byte mHairStyle = 0;				//< 发型aa
	public byte mBaseJob = 0;				//< 基础职业aa
	public byte mJob = 0;					//< 职业aa
	public int mLevel = 0;					//< 等级aa	
	public ushort mEquipCount = 0;			//< 穿的装备数量aa
	public uint [] mEquipID = null;		//< 穿的装备列表aa  
}

//服务器信息aaa
public struct stSrvInfo
{
    public int ID;
    public string strName;
    public byte onlineStatus;
    public string SrvStatus;
    public string IP;
    public ushort port;
};

/// <summary>
/// SD net global.
/// </summary>
public class SDNetGlobal 
{
	public static bool 		connecting = false;

    public static string Login_IP;
    public static ushort Login_Port = 0;


	public static uint		Gate_IP         =   0;
	public static ushort	Gate_Port        =   0;
	public static NetClient	client = new NetClient();
	
	public static int		serverId = 0;
    public static string    serverName = "";
	private static byte[]	gateSession = new byte[64];
	private static byte[]	username;

    public static bool bInternalNet = false;
	
	// 角色列表aa
	public static int		roleCount = 0;
	public static int		lastSelectRole = 0;
	public static sdPlayerInfo[] playerList = new sdPlayerInfo[4];

    public static bool bReConnectGate = false;
    static List<CMessage> cacheMessage = new List<CMessage>();
    static object cacheLock = new object();

    public static List<JsonNode> m_lstSrvInfo = new List<JsonNode>();  //选择服务器列表aaa
    public static string defaultServerID = "4";
    public static string serverNotice = "";
    public static int connectState = 0;
	
 	public static void init()
	{
		CliProto.AddEncode_Decode();
		if( Application.platform != RuntimePlatform.WindowsEditor )
		{
            Login_IP = "127.0.0.1";
		}
		else
		{
			// 只有在内网172网段才使用内网服务器地址.. 并且不是移动平台..
			string ip = Network.player.ipAddress;
			int idx = ip.IndexOf('.');
			// !!!
			// 请在login场景的bt_startgame按钮的属性表里设置Serverip，不要直接修改这个脚本
			// !!!
            if (idx > 0 && ip.Substring(0, idx) == "172" && (!BundleGlobal.IsMobile() || BundleGlobal.InternalNetDebug))
            {
                Login_IP = "172.30.10.124";
                bInternalNet = true;
            }
            else
                Login_IP = "127.0.0.1";
		}
        Login_Port = 8000;

        setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ERROR, OnMessage_GCID_ERROR);
		
		SDGlobal.Log("before enterscene");
		sdEnterScene.init();
		SDGlobal.Log("before item Msg");
		sdItemMsg.init();
		SDGlobal.Log("itemMsg");
		sdSkillMsg.init();
		SDGlobal.Log("actorMsg");
		sdActorMsg.init();
		SDGlobal.Log("petMsg");
		sdPetMsg.init();
		SDGlobal.Log("chat");
		sdChatMsg.init();
		SDGlobal.Log("friend");
		sdFriendMsg.init();
		SDGlobal.Log("ActGameMsg");
		sdActGameMsg.init();
		sdMallMsg.init ();
		SDGlobal.Log("MailMsg");
		sdMailMsg.init();
		SDGlobal.Log("pvpMsg");
		sdPVPMsg.init();
		SDGlobal.Log("AwardCenterMsg");
		sdAwardCenterMsg.init();
		SDGlobal.Log("RoleCreateMsg");
		sdRoleCreateMsg.init();
        sdMysteryShopMsg.init();
        sdRankLisrMsg.init();
        sdBBSMsg.init();
        sdLevelMsg.init();
	}
	
	public static void SendMessage(CMessage msg)
	{
        if (client.IsConnect())
        {
            if(bReConnectGate)
            {
                 lock (cacheLock)
                 {
                     cacheMessage.Add(msg);
                 }
            }
            else
            {
                if (!client.Send(msg))
                {
                    //发送消息失败 并且原因是连接断开 则尝试重新连接 ..
                    if (!client.IsConnect())
                    {
                        SendMessage(msg);
                    }
                }
            }
        }
        else
        {
            if (sdGameLevel.instance.testMode)
            {
                return;
            }
            lock (cacheLock)
            {
                //如果没有重新连接 或者重新连接失败时 要把之前存放的cache消息全部清空..
                if (!bReConnectGate)
                {
                    cacheMessage.Clear();
                }
                //把要发送的消息存放到cache中..
                cacheMessage.Add(msg);
                //如果没有重新连接Gate 开始重新连接..
                if (!bReConnectGate)
                {
                    doGateLogin(true);
                    //sdUICharacter.Instance.ShowMsgLine("重新连接服务器中...");
                }
            }
            
        }
	}
	
	public static void setCallBackFunc(ushort id,MessageCallback func)
	{
		SGDP.GetInstance().SetCallback(id,func);
	}
    public static MessageCallback getCallBackFunc(ushort id)
    {
        return SGDP.GetInstance().GetCallback(id);
    }
	
	public static void OnMessage_LGPKG_LOGIN_ACK(CliProto.LGPKG_LOGIN_ACK refMSG)
	{
        client.DisConnect();
		if(refMSG.m_AckType == CliProto.LG_FAIL)
		{
            sdUICharacter.Instance.ShowLoginMsg(SGDP.ErrorString((uint)refMSG.m_Reply.m_Fail.m_ErrCode));
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.Login)
            {
                SDNetGlobal.connectState = -1;
            }
			return;
		}
		SDGlobal.Log("received");
        Gate_IP = refMSG.m_Reply.m_Succ.m_GateInfo.m_ListenIP;
        Gate_Port = refMSG.m_Reply.m_Succ.m_GateInfo.m_ListenPort;
		byte[] session = refMSG.m_Reply.m_Succ.m_SessionID;
		username = new byte[refMSG.m_Reply.m_Succ.m_Username.Length];
		for(int i = 0; i < refMSG.m_Reply.m_Succ.m_Username.Length; i++)
		{
			username[i] = refMSG.m_Reply.m_Succ.m_Username[i];
		}
		for(int i = 0; i < 8; i++)
		{
			gateSession[i] = session[i];
		}
		
		doGateLogin(false);

        Thread threadHeart = new Thread(new ThreadStart(HeartBeatMain));
        threadHeart.Start();
	}
	
	private static void ConnectLogin()
	{

        while (true)
        {
            client.DisConnect();
            client.Start(Login_IP, Login_Port);
            client.Connect();
            connecting = false;
            if (client.IsConnect())
            {
                SDGlobal.Log("send");
                CliProto.LGPKG_LOGIN_REQ MSG = new CliProto.LGPKG_LOGIN_REQ();
                MSG.m_ServerGroup = (uint)serverId;
                Debug.Log("ServerGroup= " + serverId.ToString());


                //MSG.m_Type = System.Text.Encoding.ASCII.GetBytes("mac");
                //SDGlobal.Log("before getMac");

                if (Application.platform == RuntimePlatform.Android ||
                    Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    MSG.m_Type = System.Text.Encoding.UTF8.GetBytes("gplus");
                    MSG.m_Token = System.Text.Encoding.UTF8.GetBytes(SDGlobal.ticket);
                    MSG.m_TokenLen = (uint)SDGlobal.ticket.Length;
                }
                else
                {
                    MSG.m_Type = System.Text.Encoding.UTF8.GetBytes("pt");
                    string ptAccount = SDGlobal.ptUserName + ";" + "12345";
                    MSG.m_Token = System.Text.Encoding.UTF8.GetBytes(ptAccount);
                    MSG.m_TokenLen = (uint)ptAccount.Length;
                }


                SDGlobal.Log("before send");
                if (Application.platform == RuntimePlatform.Android)
                {
                    MSG.m_ResourceLevel = 0;
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    MSG.m_ResourceLevel = 10;
                }
                else if (Application.platform == RuntimePlatform.WP8Player)
                {
                    MSG.m_ResourceLevel = 20;
                }
                else if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    MSG.m_ResourceLevel = 30;
                }
                else
                {
                    MSG.m_ResourceLevel = 0;
                }
                Debug.Log("ResourceLevel = " + MSG.m_ResourceLevel);
                client.Send(MSG);
                return;
            }
            else
            {
                if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.Login)
                {
                    SDNetGlobal.connectState = -1;
                    return;
                }
            }
            //编辑器..
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                break;
            }
            Debug.Log("Retry Connect Login Server in 5 Second!");
            Thread.Sleep(5000);
            
        }
	}
	
	private static void ConnectGateLogin()
	{
        int Retry = 0;
        while (Retry<10)
        {
            client.DisConnect();
            client.Start(Gate_IP, Gate_Port);
            client.Connect();
            connecting = false;
            if (client.IsConnect())
            {
                SDGlobal.Log("Send gate");
                connectState = 1;
                CliProto.CG_LOGIN MSG = new CliProto.CG_LOGIN();
                for (int i = 0; i < 8; i++)
                    MSG.m_LoginData[i] = gateSession[i];
                MSG.m_PTID = username;
                MSG.m_LoginDataLen = 8;
                client.Send(MSG);
				gameLoginState = 2;

				return;
            }
            else
            {
                //if (!bReConnectGate)
                {
                    if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.Login)
                    {
                        connectState = -1;
                        return;
                    }
                    Retry++;
					gameLoginState = 100 + Retry;
                    Debug.Log("Retry Time = " + Retry);
                }
            }
            //编辑器..
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //break;
            }
            Debug.Log("Retry Connect Gate Server in 3 Second!");
            Thread.Sleep(3000);
        }
        //connecting = false;
		gameLoginState = 3;	// 连接失败，需要弹框问用户是否要重新连接..
        Debug.Log("Connect Over");
	}
	
	
	public static void doConnectLogin(int serId, GameObject enterGameBtn)
	{
		if(enterGameBtn==null)
			return;
		
		serverId = serId;

		//GHome.GetInstance().LoginArea(serverId.ToString());		
		if(!connecting)
		{
				if(!client.IsConnect())
				{
					connecting	=	true;
					enterGameBtn.SetActive(false);
					Thread t = new Thread(new ThreadStart(ConnectLogin));
					t.Start();								
				}
		}
	}

	public static int gameLoginState = 0;		// GATE连接状态：0非连接，1正在连接，2连接成功，3连接失败...
	public static void doGateLogin(bool bReConnect)
	{
        bReConnectGate = bReConnect;
        if (bReConnectGate)
        {
            SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ROLELIST, SelectRole.OnMessage_GCID_ROLELIST);
        }

		gameLoginState = 1;
		sdUICharacter.Instance.ShowReconnectWnd();

		connecting	=	true;
		Thread t = new Thread(new ThreadStart(ConnectGateLogin));
		t.Start();
	}
	
	public static void disConnect()
	{
		if(client.IsConnect())
			client.DisConnect();
	}
    public static void OnGateLogin(int iMsgID, ref CMessage msg)
    {
        CliProto.GC_LOGIN refMsg = (CliProto.GC_LOGIN)msg;
        if (refMsg.m_ErrCode == CliProto.LG_SUCCESS)
        {
           
        }
        else
        {
            SGDP.Error((uint)refMsg.m_ErrCode);
            if (bReConnectGate)
            {
                doGateLogin(true);
            }
        }
    }
    public static void SendEnterScene()
    {
        CliProto.CS_ENTERSCENE refMSG = new CliProto.CS_ENTERSCENE();
        refMSG.m_SceneId = (uint)sdGlobalDatabase.Instance.globalData["SceneId"];
        refMSG.m_Error = 0;
        //refMSG.szRoleName = System.Text.Encoding.Default.GetBytes(SDNetGlobal.playerList[selRole.currentSelect].name);
        client.Send(refMSG);
    }
    public static void SendCache()
    {
        
        lock (cacheLock)
        {
            bReConnectGate = false;
            foreach (CMessage c in cacheMessage)
            {
                client.Send(c);
            }
            cacheMessage.Clear();
            
        }

        sdUICharacter.Instance.HideMsgLine();
    }

    public static bool bHeartBeat = false;
    public static void HeartBeatMain()
    {
        while (true)
        {
            Thread.Sleep(5000);
            if (bHeartBeat)
            {
                if (client != null)
                {
                    CliProto.CG_HEART_BEAT hb = new CliProto.CG_HEART_BEAT();
                    if (client.IsConnect())
                    {
                        client.Send(hb);
                        //Debug.Log("HeartBeat");
                    }
                }
            }
            
        }
    }

    public static void OnMessage_GCID_ROLELIST(int iMsgID, ref CMessage msg)
    {

        Debug.Log("OnMessage_GCID_ROLELIST");


        if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.SelectRole)
        {
            CliProto.CG_SELECTROLE refMSG = new CliProto.CG_SELECTROLE();
            refMSG.m_RoleDBID = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID;
            if (client != null)
            {
                client.Send(refMSG);
            }
            SDGlobal.Log("Send CG_SELECTROLE");
        }
        else
        {
            SendCache();
        }
    
    }
    public static void OnMessage_GCID_SELECTROLE(int iMsgID, ref CMessage msg)
    {
        Debug.Log("OnMessage_GCID_SELECTROLE");
        //SendCache();
    }
    public static void OnMessage_GCID_ERROR(int iMsgID, ref CMessage msg)
    {
        CliProto.GC_ERROR error = (CliProto.GC_ERROR)msg;
        if (error.m_ErrCode == 86)
        {
            if (bReConnectGate)
            {
                doGateLogin(true);
            }
        }
        SGDP.Error((uint)error.m_ErrCode);

    }

    public static HttpWebResponse CreateGetHttpResponse(string url, int timeout)
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = timeout;
            return request.GetResponse() as HttpWebResponse;
        }
        catch (System.Exception ex)
        {
            sdUICharacter.Instance.ShowOkMsg("网络无法连接！", null);
            return null;
        }
    }
    /*
    public static List<stSrvInfo>  GetServerInfo()
    {
        m_lstSrvInfo.Clear();
        HttpWebResponse webResponse = SDNetGlobal.CreateGetHttpResponse(bInternalNet ? "http://172.30.10.124:8004/arealist?areaid=1" : "http://180.96.39.127:8004/arealist?areaid=1", 10 * 1000);
        if (webResponse != null)
        {
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(Encoding.UTF8.CodePage));
            string str = reader.ReadToEnd();
            int start = str.IndexOf('[');
            int end = str.IndexOf(']');
            str = str.Substring(start + 1, end - start);
            List<string> strArray = new List<string>();
            while (true)
            {
                start = str.IndexOf('{');
                end = str.IndexOf('}');
                if (start >= 0 && end >= 0)
                {
                    strArray.Add(str.Substring(start + 1, end - start - 1));
                    str = str.Substring(end + 1);
                }
                else
                {
                    break;
                }
            }
            for (int index = 0; index < strArray.Count; ++index)
            {
                string[] srvInfo = strArray[index].Split(',');
                stSrvInfo srv = new stSrvInfo();
                for (int j = 0; j < srvInfo.Length; j++)
                {
                    string[] keyvalue = srvInfo[j].Split(':');
                    if (keyvalue.Length == 2)
                    {
                        //去掉“”aaa
                        keyvalue[0] = keyvalue[0].Substring(1, keyvalue[0].Length - 2);
                        keyvalue[1] = keyvalue[1].Substring(1, keyvalue[1].Length - 2);

                        if (keyvalue[0] == "ServerID")
                            srv.ID = int.Parse(keyvalue[1]);
                        else if (keyvalue[0] == "ServerName")
                            srv.strName = keyvalue[1];
                        else if (keyvalue[0] == "OnlineStatus")
                            srv.onlineStatus = (byte)int.Parse(keyvalue[1]);
                        else if (keyvalue[0] == "ServerStatus")
                            srv.SrvStatus = keyvalue[1];
                        else if (keyvalue[0] == "IP")
                            srv.IP = keyvalue[1];
                        else if (keyvalue[0] == "Port")
                            srv.port = ushort.Parse(keyvalue[1]);
                    }
                }
                m_lstSrvInfo.Add(srv);
            }
        }
        return m_lstSrvInfo;
    }
    */
    public static void SaveSrvInfo()
    {
        sdConfDataMgr.Instance().SetSetting("serverID", serverId.ToString());
        sdConfDataMgr.Instance().SetSetting("IP", Login_IP);
        sdConfDataMgr.Instance().SetSetting("Port", Login_Port.ToString());
        sdConfDataMgr.Instance().SetSetting("serverName", serverName);
    }
}