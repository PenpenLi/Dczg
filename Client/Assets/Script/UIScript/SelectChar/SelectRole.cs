using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 进入游戏按钮(执行脚本)
/// </summary>
public class SelectRole : MonoBehaviour 
{	
	public GameObject m_SelectCharWnd;	//< 角色选择窗口对象aa
	protected SelectChar m_SelectChar;	//< 角色选择窗口脚本对象aa
	
	public RoleSelButton	m_RoleSelButton1;
	public RoleSelButton	m_RoleSelButton2;
	public RoleSelButton	m_RoleSelButton3;
	public RoleSelButton	m_RoleSelButton4;
	public GameObject		m_NeedHideObject1;
	public GameObject		m_NeedHideObject2;
	public GameObject		m_NeedHideObject3;
	public GameObject		m_NeedHideObject4;
	public int				m_CurrentSelect	= -1;
	
	static GameObject	m_CreateCharWnd 	= null;		//< 创建角色界面aa
	static bool			m_isLoadWnd			= false;
	static bool			m_isCreateChar		= false;
	
	void Awake()
	{
		if (m_SelectCharWnd)
			m_SelectChar = m_SelectCharWnd.GetComponent<SelectChar>();
		
		// 默认选择上次选中的角色aa
		int iCurrentSelect = -1;
		if (SDNetGlobal.roleCount > 0 && SDNetGlobal.playerList[SDNetGlobal.lastSelectRole] != null)
			iCurrentSelect = SDNetGlobal.lastSelectRole;
		doSelect(iCurrentSelect);	
		
		//
		EnterSelectUI();
	}

	void Start () 
	{

	}

	void Update () 
	{
		// 是否处于等待加载创建角色界面状态..
		if( m_isCreateChar==true && m_isLoadWnd==false && m_CreateCharWnd!=null && m_SelectChar.IsLeaveSelectUIFinish()==true )
		{
			m_CreateCharWnd.SetActive(true);
			m_SelectChar.LeaveSelectUI();
		
			// 进入创建角色界面第一步aa
			CreateChar kCreateChar = m_CreateCharWnd.GetComponent<CreateChar>();
			if (kCreateChar)
			{
				kCreateChar.SetSelectRoleWnd(m_SelectCharWnd);
				kCreateChar.EnterCreateUI1((int)(UnityEngine.Random.value*100.0f) % 4);
			}
			
			m_isCreateChar = false;
		}
	}
	
	// 进入角色选择界面aa
	public void EnterSelectUI()
	{
		// 服务器刷新角色列表的消息回调aa
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ROLELIST, OnMessage_GCID_ROLELIST);
		
		// 服务器返回角色选择成功的消息回调aa
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_SELECTROLE, OnMessage_GCID_SELECTROLE);
	}
	
	public void onLoadEnd()
	{
		//sdEnterScene.notifyWantEnterScene(0);	//< 通知场景加载完成aa
	}
	
	// 角色被选中aa
	public void doSelect(int id)
	{
		m_RoleSelButton1.unSelect();
		m_RoleSelButton2.unSelect();
		m_RoleSelButton3.unSelect();
		m_RoleSelButton4.unSelect();
		
		m_CurrentSelect = id;
		if( m_CurrentSelect == 0 ) 
			m_RoleSelButton1.doSelect();

		if( m_CurrentSelect == 1 ) 
			m_RoleSelButton2.doSelect();

		if( m_CurrentSelect == 2 ) 
			m_RoleSelButton3.doSelect();
		
		if( m_CurrentSelect == 3 ) 
			m_RoleSelButton4.doSelect();
		
		if (m_SelectChar)
			m_SelectChar.DoSelect(m_CurrentSelect);
	}
	
	private static void OnMessage_GCID_SELECTROLE(int iMsgID, ref CMessage msg)
	{
         SDGlobal.Log("GC_SELECTROLE");
         if (SDNetGlobal.bReConnectGate)
         {
             SDNetGlobal.OnMessage_GCID_SELECTROLE(iMsgID, ref msg);
             return;
         }
         
         //游戏中宠物，邮件，深渊活动等管理器，数据在这里清空一下..
         sdNewPetMgr.Instance.ClearData();
         SDGlobal.Log("sdNewPetMgr.ClearData success!");
         sdActGameMgr.Instance.ClearData();
         SDGlobal.Log("sdActGameMgr.ClearData success!");
         sdMailMgr.Instance.ClearData();
         SDGlobal.Log("sdMailMgr.ClearData success!");
         sdGuideMgr.Instance.ClearData();
        
        sdLevelInfo.Clear();


        string rolename = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mRoleName;
        GPUSH_API.SetUserInfo(0, rolename);
	}
	
	public static void OnMessage_GCID_ROLELIST(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("rolelist received");

		CliProto.GC_ROLELIST refMSG = (CliProto.GC_ROLELIST)msg;
		
		SDNetGlobal.roleCount		= refMSG.m_Count;
        if (!SDNetGlobal.bReConnectGate)
        {
            SDNetGlobal.lastSelectRole = SelectRole.SortRoleList(refMSG);
        }
		
		for(int i = 0; i < SDNetGlobal.roleCount; i++)
		{
			if(SDNetGlobal.playerList[i] == null)
				SDNetGlobal.playerList[i] = new sdPlayerInfo();
			
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[i];		
			kPlayerInfo.mRoleName	= System.Text.Encoding.UTF8.GetString(refMSG.m_RoleInfoList[i].m_RoleInfo.m_RoleName);
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

        SelectRole sr = sdGameLevel.instance.NGUIRoot.GetComponentInChildren<SelectRole>();
        if (sr != null)
        {
            sr.OnRoleList();
        }

        if (SDNetGlobal.bReConnectGate)
        {
            SDNetGlobal.OnMessage_GCID_ROLELIST(iMsgID, ref msg);
        }
	}
	
	public void OnClick()
	{
		// 播放动画阶段点击无效..
		if( m_SelectChar.IsLeaveSelectUIFinish() == false )
			return;
		
		// 如果角色名为空,则直接返回aa
		if( SDNetGlobal.playerList[m_CurrentSelect].mRoleName.Length <= 0 )
			return;


		string first = sdConfDataMgr.Instance().GetSetting("firstEnter");
		if(first.Length == 0 && 
		   (Application.platform == RuntimePlatform.Android || 
		 Application.platform == RuntimePlatform.IPhonePlayer))
		{
			
			GameObject camera = GameObject.Find("@MainCamera");
			if(camera != null)
			{
				sdMovieVideo movie = camera.AddComponent<sdMovieVideo>();
				if(movie != null)
				{	
					if(movie.PlayMovie("DS_intro.mp4") == false)
						Debug.Log("play movie error");
				}
			}
			sdConfDataMgr.Instance().SetSetting("firstEnter", "0");
		}

		
		SDNetGlobal.lastSelectRole = m_CurrentSelect;
		
		// 隐藏预览的角色aa
		if(m_SelectChar) 
			m_SelectChar.DoSelect(-1);
		
		// 显示LOADING窗口aa
		if(m_NeedHideObject1) m_NeedHideObject1.SetActive(false);
		if(m_NeedHideObject2) m_NeedHideObject2.SetActive(false);
		if(m_NeedHideObject3) m_NeedHideObject3.SetActive(false);
		if(m_NeedHideObject4) m_NeedHideObject4.SetActive(false);
		sdUILoading.ActiveLoadingUI(0);
		
		// 通知服务器当前选中的角色aa
		CliProto.CG_SELECTROLE refMSG = new CliProto.CG_SELECTROLE();
		refMSG.m_RoleDBID = SDNetGlobal.playerList[m_CurrentSelect].mDBID;
		SDNetGlobal.SendMessage(refMSG);
		SDGlobal.Log("CG_SELECTROLE");
	}

	public void CreateChar()
	{
		if(m_SelectChar) m_SelectChar.StartLeaveSelectUI();
		m_isCreateChar = true;
		
		if (m_CreateCharWnd == null)
		{
			if (m_isLoadWnd) 
				return;

			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource(
				"UI/SelectChar/$CreateChar/CreateCharWnd.prefab", 
				LoadWnd, 
				param);
			
			m_isLoadWnd = true;
		}
	}
	
	public void LoadWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null) return;
		m_CreateCharWnd = GameObject.Instantiate(obj) as GameObject;
		m_CreateCharWnd.transform.parent		= sdGameLevel.instance.UICamera.transform;
		m_CreateCharWnd.transform.localScale	= new Vector3(1.0f,1.0f,1.0f);
		m_CreateCharWnd.transform.localPosition	= new Vector3(0.0f,0.0f,0.0f);
		m_CreateCharWnd.SetActive(false);
		m_isLoadWnd = false;
	}
    public void OnRoleList()
    {
        // 刷新角色列表aa
        if (m_RoleSelButton1) m_RoleSelButton1.InitRole();
        if (m_RoleSelButton2) m_RoleSelButton2.InitRole();
        if (m_RoleSelButton3) m_RoleSelButton3.InitRole();
        if (m_RoleSelButton4) m_RoleSelButton4.InitRole();

        // 选中上次选中角色..
        int iCurrentSelect = -1;
        if (SDNetGlobal.roleCount > 0 && SDNetGlobal.playerList[SDNetGlobal.lastSelectRole] != null)
            iCurrentSelect = SDNetGlobal.lastSelectRole;
        doSelect(iCurrentSelect);
    }
    public static int GetLastLoginRoleIndex(CliProto.GC_ROLELIST refRoleList)
    {
        UInt64 t = 0;
        int idx = 0;
        for (int i = 0; i < refRoleList.m_Count; i++)
        {
            UInt64 lastTime = refRoleList.m_RoleInfoList[i].m_RoleInfo.m_LastLoginTime;
            if(lastTime==0)
            {
                continue;
            }
            if (lastTime > t)
            {
                t = lastTime;
                idx = i;
            }
        }
        return idx;
    }
    public static void SwitchPosIfLess(CliProto.GC_ROLELIST refRoleList,int x,int y,UInt64[] arrayInfo)
    {
        if (arrayInfo[x] > arrayInfo[y])
        {
            HeaderProto.SRoleInfoWithEquip role = refRoleList.m_RoleInfoList[x];
            refRoleList.m_RoleInfoList[x] = refRoleList.m_RoleInfoList[y];
            refRoleList.m_RoleInfoList[y] = role;

            UInt64 tempTime = arrayInfo[y];
            arrayInfo[y] = arrayInfo[x];
            arrayInfo[x] = tempTime;
        }
    }
    public static int SortRoleList(CliProto.GC_ROLELIST refRoleList)
    {
        if (refRoleList.m_Count == 0)
        {
            return -1;
        }
        if (refRoleList.m_Count == 1)
        {
            return 0;
        }
        UInt64[] arrayInfo = new UInt64[4];
        for (int i = 0; i < refRoleList.m_Count; i++)
        {
            arrayInfo[i] = UInt64.Parse(Encoding.UTF8.GetString(refRoleList.m_RoleInfoList[i].m_RoleInfo.m_createtime));
            
        }
        if (refRoleList.m_Count == 2)
        {
            SwitchPosIfLess(refRoleList, 0, 1, arrayInfo);
        }
        else
        {
            for (int i = 0; i < refRoleList.m_Count-1; i++)
            {
                for (int j = i+1; j < refRoleList.m_Count; j++)
                {
                    SwitchPosIfLess(refRoleList, i, j, arrayInfo);
                }
            }
        }

        return GetLastLoginRoleIndex(refRoleList);
    }
}
