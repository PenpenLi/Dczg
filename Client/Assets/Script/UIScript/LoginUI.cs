using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class LoginUI : MonoBehaviour 
{	
	// 启动动画资源.
	static UISprite m_spFS0 = null;
	static UISprite m_spFS1 = null;
	static UISprite m_spZS0 = null;
	static UISprite m_spZS1 = null;
	static UISprite m_spZS2 = null;
	static UISprite m_spYX0 = null;
	static UISprite m_spYX1 = null;
	static UISprite m_spYX2 = null;
	static UISprite m_spMS0 = null;
	static UISprite m_spMS1 = null;
	static UISprite m_spMS2 = null;
	static uint m_dwLastTickCount = 0;
		
	static GameObject	m_btEnterGameButton	= null;
	static GameObject  	m_gHomeAccountButton = null;
	static GameObject   m_userNameInput = null;
	static GameObject   m_btGameServer = null;
    static GameObject   m_btSystemNotice = null;
    static GameObject   m_btPlayVideo = null;

	static UISlider		m_slUpdateBar		= null;
	static UILabel 		m_lbUpdateInfo		= null;
	static int m_iInitStep = 0;
	
	// 网络通讯状态.
	static bool m_bGateReceived		= false;
	static bool m_bRoleListReceived	= false;
	static bool m_bLoginAckReceived	= false;
	
	// bundle初始化相关....
	string m_strLevelConf	= "";
	string m_strLevelLang	= "";
	string m_strLevelMeta	= "";
	string m_strLevelBox	= "";
	/*string m_strMonsterConf = "";
	string m_strMonsterLang = "";
	string m_strMonsterMeta = "";*/
	private SDCSV m_csvEffectCsv	= new SDCSV();
	private SDCSV m_csvBuffCsv		= new SDCSV();
	private SDCSV m_csvPropTransCsv	= new SDCSV();
	private SDCSV m_csvPet = new SDCSV();
	private SDCSV m_csvMonster = new SDCSV();
	
	bool monsterLoadOver = false;
	
	void Awake()
	{
		// 读取配置.
		sdConfDataMgr.Instance().LoadSetting();
		string str = sdConfDataMgr.Instance().GetSetting("CFG_Volume");
		if( str!="" ) AudioListener.volume = float.Parse(str); 
		str = sdConfDataMgr.Instance().GetSetting("CFG_Mute");
		if( str=="1" ) AudioListener.pause = true; 

		// 屏幕分辨率初始化.
		int ratio = Screen.width * 1000 / Screen.height;
		if( ratio >= 1700 )
			SDGlobal.screenAspectRatio = SCREEN_ASPECT_RATIO.r16_9;
		else if( ratio >= 1590 )
			SDGlobal.screenAspectRatio = SCREEN_ASPECT_RATIO.r16_10;
		else if( ratio >= 1490 )
			SDGlobal.screenAspectRatio = SCREEN_ASPECT_RATIO.r3_2;
		else if( ratio >= 1300 )
			SDGlobal.screenAspectRatio = SCREEN_ASPECT_RATIO.r4_3;

		// 登录窗口下对3:2做些特殊处理..
		if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r3_2 )
		{
			GameObject obj;
			obj = GameObject.Find("sp_frame1");
			if( obj )
			{
				obj.transform.localPosition = new Vector3(0,-393.0f,0);
				obj.GetComponent<UISprite>().height = 67;
			}
			obj = GameObject.Find("sp_frame2");
			if( obj )
			{
				obj.transform.localPosition = new Vector3(0,393.0f,0);
				obj.GetComponent<UISprite>().height = 67;
			}
		}

		// 读取显示配置.
		sdConfDataMgr.Instance().LoadGraphicConfig();
		sdConfDataMgr.Instance().ApplyGraphicConfig(true);
		
		// 网络初始化.
		SDNetGlobal.init();
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_LOGIN,					OnMessage_GCID_LOGIN);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ROLELIST,				OnMessage_GCID_ROLELIST);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.LGID_LOGIN_ACK,				OnMessage_LGID_LOGIN_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_LEVEL_INFO, 			sdLevelInfo.OnMessage_SC_LEVEL_INFO);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_BATTLE_GOT_STAR_NTF,	sdLevelInfo.OnMessage_SC_BATTLE_GOT_STAR_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_BATTLE_BOX_ACK,		sdLevelInfo.OnMessage_SC_GET_BATTLE_BOX_ACK);
		
		// 全局参数初始化.
		SDGlobal.editorMode = false;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Use this for initialization
	void Start () 
	{
		// 初始化登录界面动画需要的变量.
        if (m_btEnterGameButton == null)
        {
            sdUICharacter.Instance.SelectSrvWnd = GameObject.Find("SelectServerWnd");
            GameObject obj;

            m_btEnterGameButton = GameObject.Find("bt_StartGame");
            if (m_btEnterGameButton) m_btEnterGameButton.SetActive(false);

            m_gHomeAccountButton = GameObject.Find("bt_Username");
            if (m_gHomeAccountButton) m_gHomeAccountButton.SetActive(false);

            m_btGameServer = GameObject.Find("bt_Server");
            if (m_btGameServer) m_btGameServer.SetActive(false);

            m_btPlayVideo = GameObject.Find("bt_PlayVideo");
            if (m_btPlayVideo) m_btPlayVideo.SetActive(false);

            m_btSystemNotice = GameObject.Find("bt_systemnotice");
            if (m_btSystemNotice != null)
            {
                m_btSystemNotice.SetActive(false);
            }

            string verison = BundleGlobal.AppVersion()["versionName"];
            GameObject Label_Version = GameObject.Find("Label_Version");
            if (Label_Version != null)
            {
                UILabel label = Label_Version.GetComponent<UILabel>();
                if (label != null)
                {
                    label.text = "Ver " + verison;
                }
            }

            m_userNameInput = GameObject.Find("Input");
            if (m_userNameInput)
            {
				// PC环境下使用非G加账号..
                if (Application.platform != RuntimePlatform.Android &&
                   Application.platform != RuntimePlatform.IPhonePlayer)
                {
                    m_userNameInput.SetActive(true);

                    FileStream file = null;
                    try
                    {
                        file = new FileStream("Assets/account.txt", FileMode.Open);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    if (file != null)
                    {
                        byte[] data = new byte[file.Length];
                        file.Read(data, 0, (int)file.Length);
                        m_userNameInput.GetComponent<UIInput>().value = Encoding.UTF8.GetString(data);
                        file.Close();
                    }
                }
                else
                {
                    m_userNameInput.SetActive(false);
                }

            }

            obj = GameObject.Find("lb_UpdateBar");
            if (obj) m_lbUpdateInfo = obj.GetComponent<UILabel>();

            obj = GameObject.Find("pb_UpdateBar");
            if (obj) m_slUpdateBar = obj.GetComponent<UISlider>();
            if (m_slUpdateBar) m_slUpdateBar.gameObject.SetActive(false);

            obj = GameObject.Find("sp_fs0");
            if (obj) m_spFS0 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_fs1");
            if (obj) m_spFS1 = obj.GetComponent<UISprite>();

            obj = GameObject.Find("sp_zs0");
            if (obj) m_spZS0 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_zs1");
            if (obj) m_spZS1 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_zs2");
            if (obj) m_spZS2 = obj.GetComponent<UISprite>();

            obj = GameObject.Find("sp_ms0");
            if (obj) m_spMS0 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_ms1");
            if (obj) m_spMS1 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_ms2");
            if (obj) m_spMS2 = obj.GetComponent<UISprite>();

            obj = GameObject.Find("sp_yx0");
            if (obj) m_spYX0 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_yx1");
            if (obj) m_spYX1 = obj.GetComponent<UISprite>();
            obj = GameObject.Find("sp_yx2");
            if (obj) m_spYX2 = obj.GetComponent<UISprite>();
        }

        StartCoroutine(UpdateProcess());
	}
	
	public void Relogin()
	{
		SDGlobal.gHomeLoginActivate = true;
        StartCoroutine(LoginProcess());
	}
	
	// Update is called once per frame
	void Update () 
	{	
		// 更新登录画面的动画.
		uint dwTickCount = (uint)(Time.time * 10.0f);	// 决定更新速度.
		if( dwTickCount != m_dwLastTickCount )
		{
			m_dwLastTickCount = dwTickCount;
			if(m_spFS1) m_spFS1.spriteName = "fs1-" + ((dwTickCount+0)%10+1);
			if(m_spZS1) m_spZS1.spriteName = "zs1-" + ((dwTickCount+2)%10+1);
			if(m_spZS2) m_spZS2.spriteName = "zs2-" + ((dwTickCount+2)%10+1);
			if(m_spMS1) m_spMS1.spriteName = "ms1-" + ((dwTickCount+5)%10+1);
			if(m_spMS2) m_spMS2.spriteName = "ms2-" + ((dwTickCount+5)%10+1);
			if(m_spYX1) m_spYX1.spriteName = "yx1-" + ((dwTickCount+7)%10+1);
			if(m_spYX2) m_spYX2.spriteName = "yx2-" + ((dwTickCount+7)%10+1);
			
			if( m_spFS0 )
			{
				if( (dwTickCount%25) == 3 ) 
					m_spFS0.spriteName = "fs0-1"; 
				else 
					m_spFS0.spriteName = "";
			}
			if( m_spZS0 )
			{
				if( (dwTickCount%30)>=7 && (dwTickCount%30)<=8 ) 
					m_spZS0.spriteName = "zs0-1"; 
				else 
					m_spZS0.spriteName = "";
			}
			if( m_spMS0 )
			{
				if( (dwTickCount%25) == 10 ) 
					m_spMS0.spriteName = "ms0-1"; 
				else 
					m_spMS0.spriteName = "";
			}
			if( m_spYX0 )
			{
				if( (dwTickCount%30)>=15 && (dwTickCount%30)<=16 ) 
					m_spYX0.spriteName = "yx0-1"; 
				else 
					m_spYX0.spriteName = "";
			}
		}
	}

	
	private void OnMessage_GCID_LOGIN(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("gate received");
        SDNetGlobal.OnGateLogin(iMsgID, ref msg);
		m_bGateReceived = true;
	}
	
	private void OnMessage_LGID_LOGIN_ACK(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("login ack");
		CliProto.LGPKG_LOGIN_ACK refMSG = (CliProto.LGPKG_LOGIN_ACK)msg;
		SDNetGlobal.OnMessage_LGPKG_LOGIN_ACK(refMSG);
		
		string tmp = System.Text.Encoding.UTF8.GetString(refMSG.m_Reply.m_Succ.m_BundleInfo.m_Path);		
		BundleGlobal.Instance.infoPath = "";
		int count = 0;
		while(count < tmp.Length && tmp[count] != '\0')
		{
			BundleGlobal.Instance.infoPath += tmp[count];
			count++;
		}
		
		//Debug.Log("infoPath: " + BundleGlobal.Instance.infoPath);
		uint cdnCount = refMSG.m_Reply.m_Succ.m_BundleInfo.m_CDNCount;
		BundleGlobal.Instance.cdn = new string[cdnCount];
		for(int i = 0; i < cdnCount; i++)
		{
			tmp = System.Text.Encoding.UTF8.GetString(refMSG.m_Reply.m_Succ.m_BundleInfo.m_CDN[i].m_Path);
			//endIndex = BundleGlobal.Instance.cdn[i].IndexOf('\0');
			//BundleGlobal.Instance.cdn[i].Remove(endIndex);
			BundleGlobal.Instance.cdn[i] = "";
			
			count = 0;
			while(count < tmp.Length && tmp[count] != '\0')
			{
				BundleGlobal.Instance.cdn[i] += tmp[count];
				count++;
			}
		}
		m_bLoginAckReceived = true;
	}

	private void OnMessage_GCID_ROLELIST(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("rolelist received");
		
		CliProto.GC_ROLELIST refMSG = (CliProto.GC_ROLELIST)msg;
		
		SDNetGlobal.roleCount		= refMSG.m_Count;
        SDNetGlobal.lastSelectRole = SelectRole.SortRoleList(refMSG);
		
		for(int i = 0; i < SDNetGlobal.roleCount; i++)
		{
			if( SDNetGlobal.playerList[i] == null )
				SDNetGlobal.playerList[i] = new sdPlayerInfo();
			
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[i];		
			kPlayerInfo.mRoleName	= System.Text.Encoding.UTF8.GetString(refMSG.m_RoleInfoList[i].m_RoleInfo.m_RoleName).Trim('\0');
			kPlayerInfo.mDBID		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_DBRoleId;
			kPlayerInfo.mGender		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Gender;
			kPlayerInfo.mSkinColor	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_SkinColor;
			kPlayerInfo.mHairStyle	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_HairStyle;
			kPlayerInfo.mBaseJob	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_BaseJob;
			kPlayerInfo.mJob		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Job;
			kPlayerInfo.mLevel		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Level;
			kPlayerInfo.mEquipCount	= refMSG.m_RoleInfoList[i].m_Equip.m_ItemCount;			
			
			if (kPlayerInfo.mEquipCount > 0)
			{
				kPlayerInfo.mEquipID = new uint[kPlayerInfo.mEquipCount];
				for (int j = 0; j < kPlayerInfo.mEquipCount; ++j)
					kPlayerInfo.mEquipID[j] = (uint)refMSG.m_RoleInfoList[i].m_Equip.m_Items[j].m_TID;
			}
		}
		
		for(int i = SDNetGlobal.roleCount; i < 4; i++)
			SDNetGlobal.playerList[i] = null;
		
		m_bRoleListReceived = true;
	}
	
	// 加载场景配置文件.
	void LoadLevelConfig()
	{		
		ResLoadParams param = new ResLoadParams();
		param.info = "Level";
		sdResourceMgr.Instance.LoadResource("$Conf/level.txt",loadLevelCallBack,param);
		
		ResLoadParams param2 = new ResLoadParams();
		param2.info = "LevelLang";
		sdResourceMgr.Instance.LoadResource("$Conf/level_lang.txt",loadLevelCallBack,param2);
		
		ResLoadParams param3 = new ResLoadParams();
		param3.info = "LevelMeta";
		sdResourceMgr.Instance.LoadResource("$Conf/levelmeta.txt",loadLevelCallBack,param3);
		
		ResLoadParams param4 = new ResLoadParams();
		param4.info = "LevelBox";
		sdResourceMgr.Instance.LoadResource("$Conf/dmdsbattleboxconfig.boxs.txt",loadLevelCallBack,param4);
	}
			
	void loadLevelCallBack(ResLoadParams param,Object obj)
	{
		//Debug.Log("LevelInfo:"+param.info);
		
		if(param.info == "Level")
		{
			TextAsset txt = (TextAsset)obj;
			m_strLevelConf = txt.text;
		}
		else if(param.info == "LevelLang")
		{
			TextAsset txt = (TextAsset)obj;
			m_strLevelLang = txt.text;
		}
		else if(param.info == "LevelMeta")
		{
			TextAsset txt = (TextAsset)obj;
			m_strLevelMeta = txt.text;
		}
		else if(param.info == "LevelBox")
		{
			TextAsset txt = (TextAsset)obj;
			m_strLevelBox = txt.text;
		}
	}

	/*
    bool CheckAPKUpdate()
    {
            int ret = glupgrade.InitUpgradeEx(3000);
            Debug.Log("InitUpgradeEx() return: " + ret);

            if (ret == 0) // 不需要升级
            {
                Debug.Log("no app update");
                return true;

            }
            else if (ret == 1) // 强制升级
            {
                Debug.Log("startUpdate()");
                glupgrade.startUpdate();

                sdUICharacter.Instance.ShowLoginMsg("A new version find, please waiting...");

                return false;
            }
            else
            {
                return true;
            }
        
        return false;
    }
    */

    static int GHOME_CODE = -2;
    void Init_GHOME_GPUSH()
    {
        if (!SDGlobal.gHomeInitialize)
        {
            SDGlobal.phoneNumber = "+86-15801772268";
            SDGlobal.gHomeInitialize = true;

            Debug.Log("GHome Initialize");
            GHome.GetInstance().Initialize("791000015", (code, msg, data) =>
            {
                Debug.Log("Initialize callback code: " + code + " msg: " + msg);
                
                if (code == 0)
                {
                    SDGlobal.gHomeLoginActivate = true;
                    
                    GHOME_CODE = 0;
                }
                else
                {
                    SDGlobal.gHomeLoginActivate = false;
                    GHOME_CODE = 1;
                }

            });
            
        }
    }
    void LoginGHome(bool bActiveUI)
    {
        GHome.GetInstance().Login((code, msg, data) =>
        {
            Debug.Log("Login callback code: " + code + " msg: " + msg);
            if (code == 0)
            {
				GHome.GetInstance().LoginArea(BundleGlobal.AppVersion()["Area"]);

                Debug.Log("CallBack Success.");
                SDGlobal.phoneNumber = (string)data["userId"];
                SDGlobal.ticket = (string)data["ticket"];
                if (bActiveUI)
                {
                    if (m_btEnterGameButton)
                    {
                        m_btEnterGameButton.SetActive(true);
                    }

                    if (m_btGameServer)
                    {
                        m_btGameServer.SetActive(true);
                    }
                    if (m_btSystemNotice)
                        m_btSystemNotice.SetActive(true);


                    if (m_btPlayVideo)
                    {
                        string str = sdConfDataMgr.Instance().GetSetting("firstEnter");
                        m_btPlayVideo.SetActive(str.Length != 0);
                    }
                }

                if (m_gHomeAccountButton)
                {
                    if (bActiveUI)
                    {
                        m_gHomeAccountButton.SetActive(true);
                    }
                    UILabel buttonLabel = m_gHomeAccountButton.GetComponentInChildren<UILabel>();
                    if (buttonLabel)
                    {
                        //buttonLabel.text = SDGlobal.phoneNumber.Replace("+86", "");						
                    }
                }

                Debug.Log("CallBack Success. OK!");
                GHOME_CODE = 0;
            }
            else if (code == -1)
            {
                Debug.Log("code == -1");
                //GHOME_CODE = -1;
            }
            else
            {
                SDGlobal.gHomeLoginActivate = true;
                GHOME_CODE = 1;
                Debug.Log("Login Error Code=" + code);
            }
        });
    }
    void OnProcessChanged(string msg)
    {
        JsonNode node = new JsonNode();
        int i=0;
        node.Parse(msg, ref i);
        int download = int.Parse(node.Find("progress").value);
        int total = int.Parse(node.Find("total").value);
        int iPercent = 100 * download / total;
        
        if (m_slUpdateBar)	
		{
			m_slUpdateBar.value = (float)download / (float)total;
			if( m_slUpdateBar.value>0 && m_slUpdateBar.value<0.022f ) m_slUpdateBar.value = 0.022f;
		}
        if (m_lbUpdateInfo)	m_lbUpdateInfo.text = "APP更新...[" + iPercent + "%]";
    }
    void OnMD5CheckStart(string msg)
    {
        //sdUICharacter.Instance.ShowLoginMsg("APK MD5 Check Start");
    }
    void OnMD5CheckFinish(string msg)
    {
        //sdUICharacter.Instance.ShowLoginMsg("APK MD5 Check Finish");
    }
    void OnDownloadStart(string msg)
    {
        //sdUICharacter.Instance.ShowLoginMsg("Downloading");
    }
    void OnDownloadFinished(string msg)
    {
        //sdUICharacter.Instance.ShowLoginMsg("APK Download Finished");
    }

    IEnumerator UpdateProcess()
    {   
        //APK Update
#if UNITY_ANDROID

        if (Application.platform == RuntimePlatform.Android)
        {
            glupgrade.gluInit();
            glupgrade.getCallback()._onDownloadChange	= OnProcessChanged;
            glupgrade.getCallback()._onMD5CheckStart	= OnMD5CheckStart;
            glupgrade.getCallback()._onMD5CheckFinish	= OnMD5CheckFinish;
            glupgrade.getCallback()._onDownloadStart	= OnDownloadStart;
            glupgrade.getCallback()._onDownloadFinish	= OnDownloadFinished;
            if (m_slUpdateBar)
            {
                m_slUpdateBar.gameObject.SetActive(true);
                m_slUpdateBar.value = 0.0f;
            }

            bool ApkUpdate = false;
            while (true)
            {
                if (SDGlobal.apkUpdateEnable)
                {
                    if (!ApkUpdate)
                    {
                        int ret = glupgrade.checkNetworkStatus();
                        if (ret == 1)
                        {
                            Debug.Log("WiFi OK!");
                        }
                        else if (ret == 2)
                        {
                            Debug.Log("GPRS OK!");
                            //yield return new WaitForSeconds(10.0f);
                        }
                        else
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：没有可使用的网络！3秒后重试。");
                            yield return new WaitForSeconds(3.0f);
                            continue;
                        }
                    
                        ApkUpdate = true;

                        ret = glupgrade.InitUpgradeEx(3000);
                        Debug.Log("InitUpgradeEx() return: " + ret);

                        if (ret == 0) // 不需要升级
                        {
                            Debug.Log("no app update");
                            break;
                        }
                        else if (ret == 1) // 强制升级
                        {
                            //sdUICharacter.Instance.ShowLoginMsg("New Version Founded, Please Waiting...");
                            glupgrade.startUpdate();
                        }
                        else if (ret == 2)// 非强制升级
                        {
                            Debug.Log("No Force Update");
                            //break;
                        }
                        else if (ret == 3)
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：内部错误（访问升级接口httpCode非200）");
                            ApkUpdate = false;
                        }
                        else if (ret == 4)// 非强制升级
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：内部错误（访问升级接口网络出错）");
                            ApkUpdate = false;
                        }
                        else if (ret == 5)// 非强制升级
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：内部错误（JSON格式转换出错）");
                            ApkUpdate = false;
                        }
                        else if (ret == 6)// 非强制升级
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：获取渠道打包信息失败！");
                            ApkUpdate = false;
                        }
                        else if (ret == 6)// 非强制升级
                        {
							sdUICharacter.Instance.ShowLoginMsg("APP更新：手机SD卡不可用！");
                            ApkUpdate = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                    yield return new WaitForSeconds(5.0f);
                }
                else
                {
                    break;
                }
            }
        }
        if (m_slUpdateBar)
        {
            m_slUpdateBar.gameObject.SetActive(false);
        }

#endif

#if UNITY_IPHONE
			
			

#endif

        sdUICharacter.Instance.HideLoginMsg();

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

        }
        else
        {
            //读取分区信息..
            Debug.Log("Read Server Area Information");
            while (true)
            {
                string AreaID = BundleGlobal.AppVersion()["Area"];
                Debug.Log("AreaID=" + AreaID);
                WWW w = new WWW("http://180.96.39.128:8004/arealist?areaid=" + AreaID);
                yield return w;
                if (w.error == null)
                {
                    JsonParse jp = new JsonParse();
                    jp.Parse(w.text);
                    BundleGlobal.Instance.cdn = jp.cdnlist.ToArray();
                    //BundleGlobal.Instance.
                    //jp.serverlist
                    SDNetGlobal.m_lstSrvInfo    = jp.serverlist;
                    SDNetGlobal.serverNotice	= jp.notice;
                    SDNetGlobal.defaultServerID	= jp.defaultServer;

                    RefreshNotification(jp.Pushlist);
                    break;
                }
                else
                {
                    sdUICharacter.Instance.ShowLoginMsg("读取分区信息错误："+w.error);
                }
            }
            GetSaveAreaInfo();
        }
        sdUICharacter.Instance.HideLoginMsg();

        //bbs
        if (SDNetGlobal.serverNotice.Length != 0)
            sdUICharacter.Instance.ShowbbsWnd(true, SDNetGlobal.serverNotice, true, false);

        //Update Bundle
        BundleGlobal.Instance.StartUpdateAllBundles();
        if (m_slUpdateBar) m_slUpdateBar.gameObject.SetActive(true);
        while (BundleGlobal.Instance.updating)
        {
            if(m_slUpdateBar && BundleGlobal.Instance.needDownLoadNum>0) 
			{
				m_slUpdateBar.value = (float)BundleGlobal.Instance.downloadedNum / (float)BundleGlobal.Instance.needDownLoadNum;
				if( m_slUpdateBar.value>0 && m_slUpdateBar.value<0.022f ) m_slUpdateBar.value = 0.022f;
			}
            if (m_lbUpdateInfo)
            {
                int Percent = BundleGlobal.Instance.GetBundlePercent();
                if (Percent == 100)
                {
                    Percent = 99;
                }
                string s = Percent + "%][";
                if (Percent < 10)
                {
                    s = "[0" + s;
                }
                else
                {
                    s = "[" + s;
                }
                m_lbUpdateInfo.text = "更新资源..." + s + BundleGlobal.Instance.downloadedNum + "/" + BundleGlobal.Instance.needDownLoadNum + "]";
            }
            yield return 0;
        }
        if (m_slUpdateBar) m_slUpdateBar.gameObject.SetActive(false);
        if (m_lbUpdateInfo) m_lbUpdateInfo.text = "加载游戏配置文件...";

        //Load All Config File
        LoadLevelConfig();

        while ( m_strLevelConf == "" || 
                m_strLevelLang == ""||
                m_strLevelMeta == "" || 
                m_strLevelBox == "")
        {
            yield return 0;
        }

        
        sdLevelInfo.LoadLevelConfig(m_strLevelConf, m_strLevelLang, m_strLevelMeta, m_strLevelBox);
		sdResourceDependenceTable.GetSingleton().LoadMonsterTable("$Conf/MonsterDependence.xml", false);
		sdResourceDependenceTable.GetSingleton().LoadMonsterTable("$Conf/PetDependence.xml", false);
        sdAITable.GetSingleton().LoadAITable("$Conf/AITable.xml", false);
        sdConfDataMgr.Instance().Init(false);
 
        while (!sdConfDataMgr.Instance().isInitFinish())
        {
            yield return 0;
        }
        sdUICharacter.Instance.HideLoginMsg();
        //Init GHOME GPUSH
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GHOME_CODE = -2;
            while (GHOME_CODE != 0)
            {
                if (GHOME_CODE == -2)
                {
                    GHOME_CODE = -1;
                    Debug.Log("InitGHome");
                    Init_GHOME_GPUSH();
                }
                else
                {
                    if (GHOME_CODE == 1)
                    {
                        Debug.Log("InitGHome Failed!");
                        GHOME_CODE = -2;
                    }
                    else
                    {
                        yield return 0;
                    }
                }
            }
            GPUSH_API.Init("791000015", "9728werwerwc53ba8cded5a6a2227dd8");
            sdUICharacter.Instance.HideLoginMsg();
        }
        else
        {
            m_btEnterGameButton.SetActive(false);
            SDGlobal.StartGetMacAddress();
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UILabel lb_srvName = m_btGameServer.transform.FindChild("Background").FindChild("Label").GetComponent<UILabel>();
            lb_srvName.text = SDNetGlobal.serverName;
        }

        StartCoroutine(LoginProcess());
    }

    IEnumerator LoginProcess()
    {
        //GHOME LOGIN 
        if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (SDGlobal.gHomeLoginActivate)
            {
                Debug.Log("GHome Login");

                SDGlobal.gHomeLoginActivate = false;
                m_btEnterGameButton.SetActive(false);
                m_gHomeAccountButton.SetActive(false);
                m_btGameServer.SetActive(false);
                m_btSystemNotice.SetActive(false);
                m_btPlayVideo.SetActive(false);

                GHOME_CODE = -2;
                while (GHOME_CODE != 0)
                {
                    if (GHOME_CODE == -2)
                    {
                        GHOME_CODE = -1;
                        LoginGHome(true);
                    }
                    else
                    {
                        if (GHOME_CODE == 1)
                        {
                            Debug.Log("Login GHome Failed!");
                            GHOME_CODE = -2;
                        }
                        else
                        {
                            yield return 0;
                        }
                    }
                }
                
            }
        }
        else
        {
            if (m_userNameInput.GetComponent<UIInput>().value != "")
            {
                m_gHomeAccountButton.SetActive(true);
                m_btEnterGameButton.SetActive(true);
                m_btGameServer.SetActive(true);
                m_btSystemNotice.SetActive(true);
                string str = sdConfDataMgr.Instance().GetSetting("firstEnter");
                m_btPlayVideo.SetActive(str.Length != 0);
            }
        }

		if (Application.platform == RuntimePlatform.Android ||
		    Application.platform == RuntimePlatform.IPhonePlayer)
		{
			GHOME_CODE = -2;
			while (GHOME_CODE != 0)
			{
				if (GHOME_CODE == -2)
				{
					GHOME_CODE = -1;
					GetGHomeProductList();
				}
				else
				{
					if (GHOME_CODE == 1)
					{
						Debug.Log("Login GHome Failed!");
						GHOME_CODE = -2;
					}
					else
					{
						yield return 0;
					}
				}
			}
		}

        if (m_lbUpdateInfo) m_lbUpdateInfo.text =   "";
    }

	void GetGHomeProductList()
	{
		GHome.GetInstance().GetProductConfig((code, msg, data) =>
		{
			if (code == 0)
			{
				sdMallManager.Instance.m_gHomeProducts.Clear();
				string json = (string)data["data"];
				JsonNode jsonNODE = new JsonNode();
				int iPOS = 0;
				jsonNODE.Parse(json,ref iPOS);
				List<JsonNode> lstAREA = new List<JsonNode>();
				jsonNODE.FindListHasAttibuteName("product_code", lstAREA);
				foreach(JsonNode js in lstAREA)
				{
					sdMallManager.GHomeProduct gHomeProduct = new sdMallManager.GHomeProduct();
					gHomeProduct.ProductCode = js.Attribute("product_code");
					gHomeProduct.ItemName = js.Attribute("item_name");
					gHomeProduct.price = int.Parse(js.Attribute("money"));
					gHomeProduct.type = int.Parse(js.Attribute("type"));
					sdMallManager.Instance.m_gHomeProducts.Add(gHomeProduct.ProductCode, gHomeProduct);
				}
				GHOME_CODE = 1;
			}
			else
			{
				SDGlobal.Log("GHome.GetInstance().GetProductConfig() failure");
				GHOME_CODE = -2;
			}
		});
	}

    void GetSaveAreaInfo()
    {
        //本地是否有上次登录的记录aaa
            string strServerID  = sdConfDataMgr.Instance().GetSetting("serverID");
            string strIP        = sdConfDataMgr.Instance().GetSetting("IP");
            string strPort      = sdConfDataMgr.Instance().GetSetting("Port");
            string strName      = sdConfDataMgr.Instance().GetSetting("serverName");
            //没有上次登录的记录，推荐一个最新的区aaa
            if (strServerID.Length == 0 || strIP.Length == 0 || strPort.Length == 0 || strName.Length == 0)
            {
                List<JsonNode> lst = SDNetGlobal.m_lstSrvInfo;
                JsonNode js   =   null;
                foreach (JsonNode n in lst)
                {
                    string id = n.Attribute("ServerID");
                    if (id == SDNetGlobal.defaultServerID)
                    {
                        js = n;
                        break;
                    }
                }
                if (js == null)
                {
                    js = lst[0];
                }

                //JsonNode js = SDNetGlobal.m_lstSrvInfo[SDNetGlobal.lastSelectServer];

                SDNetGlobal.serverId = int.Parse(js.Attribute("ServerID"));
                SDNetGlobal.Login_IP = js.Attribute("IP");
                SDNetGlobal.Login_Port = ushort.Parse(js.Attribute("Port"));
                SDNetGlobal.serverName = js.Attribute("ServerName");
                SDNetGlobal.SaveSrvInfo();
                //SDNetGlobal.serverId = 4;
            }
            else
            {
                SDNetGlobal.serverId = int.Parse(strServerID);
                SDNetGlobal.Login_IP = strIP;
                SDNetGlobal.Login_Port = ushort.Parse(strPort);
                SDNetGlobal.serverName = strName;
            }
    }
    public void StartGame(int gate)
    {
        StartCoroutine(__StartGame(gate));
    }
    IEnumerator __StartGame(int gate)
    {
        //m_btEnterGameButton.SetActive(false);
        m_gHomeAccountButton.SetActive(false);
        m_btGameServer.SetActive(false);
        m_btSystemNotice.SetActive(false);
		m_btPlayVideo.SetActive(false);
		
        if (SDGlobal.gHomeLoginActivate)
        {
            sdUICharacter.Instance.ShowLoginMsg("GHome Login");

            SDGlobal.gHomeLoginActivate = false;

            GHOME_CODE = -2;
            while (GHOME_CODE != 0)
            {
                if (GHOME_CODE == -2)
                {
                    GHOME_CODE = -1;
                    LoginGHome(false);
                }
                else
                {
                    if (GHOME_CODE == 1)
                    {
                        Debug.Log("Login GHome Failed!");
                        GHOME_CODE = -2;
                    }
                    else
                    {
                        yield return 0;
                    }
                }
            }

        }
        sdUICharacter.Instance.HideLoginMsg();

        SDNetGlobal.connectState = 0;
        SDNetGlobal.doConnectLogin(gate, GameObject.Find("bt_StartGame"));


        while (SDNetGlobal.connectState == 0)
        {
            yield return 0;
        }
        Debug.Log("connect State == " + SDNetGlobal.connectState);
       if (SDNetGlobal.connectState == 1)
       {
           //Wait User Press Button
           while (!m_bLoginAckReceived)
           {
               yield return 0;
           }

           //Wait Receive Message..
           while (!(m_bGateReceived && m_bRoleListReceived && m_bLoginAckReceived))
           {
               yield return 0;
           }

           BundleGlobal.Instance.StartLoadBundleLevel("Level/$SelectRole.unity.unity3d", "$SelectRole");

       }
       else if (SDNetGlobal.connectState == -1)
       {
           SDGlobal.gHomeLoginActivate = true;
           if (!sdUICharacter.Instance.IsLoginMsgShow())
           {
               sdUICharacter.Instance.ShowLoginMsg("Connect Server Failed");
           }
           m_btEnterGameButton.SetActive(true);
           m_gHomeAccountButton.SetActive(true);
           m_btGameServer.SetActive(true);
       }
    }
    void RefreshNotification(List<string> lstPushInfo)
    {
        Debug.Log("RefreshNotification");
        GPUSH_API.ClearNotification();

        foreach (string s in lstPushInfo)
        {
            string[] elements = s.Split(',');
            int id = int.Parse(elements[0]);
            int repeat = int.Parse(elements[1]);
            int repeattype = int.Parse(elements[2]);
            int year = int.Parse(elements[3]);
            int month = int.Parse(elements[4]);
            int day = int.Parse(elements[5]);
            int hour = int.Parse(elements[6]);
            int minute = int.Parse(elements[7]);
            int second = int.Parse(elements[8]);

            if (repeattype == 0)
            {
                GPUSH_API.NewNotification(id, elements[9], elements[10], day, hour, minute, second);
            }
            else
            {
                GPUSH_API.NewNotificationRepeat(id, elements[9], elements[10], repeattype, hour, minute, second);
            }
        }
        /*
            GPUSH_API.NewNotification(1, "几日不见，些许怀念", "这几日在忙什么呢？", 3, 0, 0, 0);
            GPUSH_API.NewNotification(2, "勇士，我们想你了!", "很久没有在鲁米那见到你的身影了！", 7, 0, 0, 0);
            GPUSH_API.NewNotification(3, "勇士，近来身体可好？", "最近是否太忙了？", 14, 0, 0, 0);
            GPUSH_API.NewNotification(4, "版本更新啦！", "赶紧来体验体验吧！", 30, 0, 0, 0);
            GPUSH_API.NewNotification(5, "勇士，你已经二个月没有来鲁米那了!", "最近是否太忙了？", 60, 0, 0, 0);
            GPUSH_API.NewNotification(6, "勇士，你已经三个月没有来鲁米那了!", "最近是否太忙了？", 90, 0, 0, 0);
            GPUSH_API.NewNotification(7, "勇士，你已经半年没有来鲁米那了!", "最近是否太忙了？", 180, 0, 0, 0);
            GPUSH_API.NewNotification(8, "勇士，你已经一年没有来鲁米那了!", "是不是已经忘记咱了？重拾装甲，战斗起来吧！", 365, 0, 0, 0);

            GPUSH_API.NewNotificationRepeat(12, "浴血竞技场", "角逐竞技之王", 1, 12, 0, 0);
            GPUSH_API.NewNotificationRepeat(18, "黑暗之时即将来临", "勇士们，一股强大势力正向我们咆哮而来！这是我们最后的阵地了，为了自由，为了荣誉，战斗吧", 1, 18, 0, 0);
        */

        GPUSH_API.StartNotification();

    }
}
