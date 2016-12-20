using UnityEngine;
using System.Collections;

public class ReadyButton : MonoBehaviour 
{	
	public DropoutButton	m_DroupoutBtn1;
	public DropoutButton	m_DroupoutBtn2;
	public DropoutButton	m_DroupoutBtn3;
	public DropoutButton	m_DroupoutBtn4;
	public GameObject		m_FrameJY;
	public GameObject		m_FrameZJ;

	public string	m_strLevelName;
	public int		m_iLevelID;
	public int		m_iDifficulty	= 0;
	int				m_iLevelSprite;
	int				m_iLevelTimes;
	int				m_iLevelTimesLeft;
	int				m_iLevelExp;
	public string	m_strLevelLoadingTex;
	string			m_strLevelIntro;
	
	string[]		m_strDropItem	= new string[4];
	GameObject[]	m_spDropItem	= new GameObject[4];
	int[] 			m_iDropItemAtlas= {-1,-1,-1,-1};
	bool[] 			m_bHasAtlas		= {false,false,false,false};

	bool 			m_bJumped		= false;
	//bool 			m_bTuituAck		= false;
	int				m_iChooseBuff	= -1;
	ChooseBuff[]	m_ChooseBuff	= new ChooseBuff[4];

	UIDraggablePanel m_PetTeamPanel = null;
	float			m_PetTeamPanelPos;
	int				m_PetTeamIdx = 0;
	UISprite		m_spTeam1;
	UISprite		m_spTeam2;
	UISprite		m_spTeam3;
	bool			m_bPetTeamAck	= false;


	// Use this for initialization
	void Awake()
	{
		for(int i=1;i<=4;i++)
			m_spDropItem[i-1] = GameObject.Find("sp_dropout"+i);

		for(int i=0;i<4;i++)
		{
			m_ChooseBuff[i] = GameObject.Find("bt_buff"+(i+1)).GetComponentInChildren<ChooseBuff>();
			m_ChooseBuff[i].m_SelectPic.SetActive(false);
		}

		if( m_PetTeamPanel == null )
		{
			m_PetTeamPanel = GameObject.Find("PetTeamPanel").GetComponent<UIDraggablePanel>();
			m_PetTeamPanel.onDragFinished = onDragFinished;
			m_PetTeamPanelPos = m_PetTeamPanel.gameObject.transform.localPosition.x;
			m_spTeam1 = GameObject.Find("sp_team1").GetComponent<UISprite>();
			m_spTeam2 = GameObject.Find("sp_team2").GetComponent<UISprite>();
			m_spTeam3 = GameObject.Find("sp_team3").GetComponent<UISprite>();
		}

        sdGlobalDatabase.Instance.globalData["TuituAck"] = 0;
   	}
	
	void Start () 
	{	
	}

	void Update()
	{
        bool bTuituAck = false;
        if (sdGlobalDatabase.Instance.globalData.ContainsKey("TuituAck"))
            bTuituAck = ((int)sdGlobalDatabase.Instance.globalData["TuituAck"] == 1 ? true : false);

        if (!m_bJumped && bTuituAck && m_bPetTeamAck && !sdUILoading.LoadingTex())
		{
			m_bJumped = true;
			
			string bundlePath = ""; 
			string levelName = "";
			int index;
			for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
			{
				if( sdLevelInfo.levelInfos[i].levelID == m_iLevelID )
				{
					string str = (string)sdLevelInfo.levelInfos[i].levelProp["Scene"];
					bundlePath = str + ".unity.unity3d";
					index = str.LastIndexOf("/");
					levelName = str.Substring(index+1);
					break;
				}
			}
			
			BundleGlobal.Instance.StartLoadBundleLevel(bundlePath,levelName);
		}
	}

	void OnClick()
	{
		// 检查剩余次数.
		if( m_iLevelTimesLeft <= 0 )
		{
			sdUICharacter.Instance.ShowOkMsg("您已经用完此关卡的当日次数。",null);
			return;
		}
		
		// 检查剩余体力.
		if( m_iLevelSprite > (int)sdGameLevel.instance.mainChar.Property["EP"] )
		{
			sdUICharacter.Instance.ShowOkMsg("您的体力不足。",null);
			return;
		}

		// 进入战斗.
		//BundleGlobal.SetBundleDontUnload("UI/$FightUI.unity3d");
		//sdResourceMgr.Instance.LoadResource("UI/$FightUI/Fight.prefab",OnLoadFightUI,null);
		
		sdUILoading.ActiveLoadingUI(m_strLevelLoadingTex,m_strLevelName);

		// 如果需要，发送队伍切换消息..
		if( m_PetTeamIdx != sdNewPetMgr.Instance.mPetCurTeam )
		{
			sdPetMsg.Send_CS_PET_TEAM_RPT(m_PetTeamIdx);
			m_bPetTeamAck = false;
		}
		else
		{
			OnChangePetTeam();
		}

		// 进入关卡..
		CliProto.CS_LEVEL_REQ refMSG = new CliProto.CS_LEVEL_REQ();
		refMSG.m_LevelBattleType = (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_NORMAL;
		refMSG.m_LevelID = (uint)(m_iLevelID);
		sdGlobalDatabase.Instance.globalData["LevelDifficulty"] = m_iDifficulty - 1;	// 保存当前选择的难度..
		if (sdFriendMgr.Instance.fightFriend != null)
		{
			refMSG.m_FriendPetID = sdFriendMgr.Instance.fightFriend.petInfo.m_uiTemplateID;
		}
		sdLevelInfo.SetCurLevelId((int)refMSG.m_LevelID);
		if( m_iChooseBuff == 0 ) 		refMSG.m_BuffID	= 10001;
		else if( m_iChooseBuff == 1 )	refMSG.m_BuffID	= 10003;
		else if( m_iChooseBuff == 2 )	refMSG.m_BuffID	= 10002;
		else if( m_iChooseBuff == 3 )	refMSG.m_BuffID	= 10004;
		else 							refMSG.m_BuffID = 0;
		SDNetGlobal.SendMessage(refMSG);
		
		// 设置要进入的关卡的信息..
		sdUICharacter.Instance.iCurrentLevelID = m_iLevelID;
		sdUICharacter.Instance.bCampaignLastLevel = false;
		if( m_iDifficulty==1 && ((m_iLevelID/10)%10)==6 )	// 第六个关卡，且是简单难度.
		{
			for(int i=0; i<sdLevelInfo.levelInfos.Length; i++)
			{
				if( sdLevelInfo.levelInfos[i].levelID == m_iLevelID )
				{
					if( sdLevelInfo.levelInfos[i].crystal == 0 ) 
						sdUICharacter.Instance.bCampaignLastLevel = true;
					break;
				}
			}
		}
	}

	public void OnChangePetTeam()
	{
		sdNewPetMgr.Instance.OnEnterLevel();
		m_bPetTeamAck = true;
	}

	void OnLoadFightUI(ResLoadParams param,Object obj)
	{
		SDGlobal.Log("PreLoad FightUI!");
	}

	public void ChooseBuff(int idx)
	{
		if( m_iChooseBuff >= 0 )
			m_ChooseBuff[m_iChooseBuff].m_SelectPic.SetActive(false);
		
		if( m_iChooseBuff == idx )
		{
			m_iChooseBuff = -1;
		}
		else
		{
			m_iChooseBuff = idx;
			m_ChooseBuff[m_iChooseBuff].m_SelectPic.SetActive(true);
		}
	}

	public void SetTargetLevel(int iLevelID, UISprite spBG)
	{
		m_iLevelID		= iLevelID;
		m_iDifficulty	= m_iLevelID % 10;
		ShowLevelInfo(m_iLevelID,spBG);
	}

    void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
		for(int i=0;i<4;i++)
		{
			if( m_iDropItemAtlas[i]>=0 && m_bHasAtlas[i]==false )
			{
				UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas( m_iDropItemAtlas[i].ToString() );
				if( atlas != null )
				{
					m_spDropItem[i].GetComponent<UISprite>().atlas = atlas;
					m_bHasAtlas[i] = true;
				}
			}
		}
	}

	void OnSetPetAtlas()
	{
		for(int i=0;i<4;i++)
		{
			if( int.Parse(m_strDropItem[i]) < 500000 ) break;
			UISprite u = m_spDropItem[i].GetComponentInChildren<UISprite>();
			if( u.atlas != null ) break;
			u.atlas = sdConfDataMgr.Instance().PetAtlas;
		}
	}
	
	void ShowLevelInfo(int iLevelID, UISprite spBG)
	{
		for(int i=0; i<sdLevelInfo.levelInfos.Length; i++)
		{
			if( sdLevelInfo.levelInfos[i].levelID == iLevelID )
			{
				string str = "";
				m_FrameJY.SetActive(false);
				m_FrameZJ.SetActive(false);
				if( (iLevelID%10) == 2 ) 		{ str=" (精英)"; m_FrameJY.SetActive(true); }
				else if( (iLevelID%10) == 3 ) 	{ str=" (专家)"; m_FrameZJ.SetActive(true); }

				m_iLevelSprite		= (int)sdLevelInfo.levelInfos[i].levelProp["Energy"];
				m_iLevelTimes		= (int)sdLevelInfo.levelInfos[i].levelProp["DailyTimes"];
				m_iLevelTimesLeft	= m_iLevelTimes - (int)sdLevelInfo.levelInfos[i].usedTimes;
				m_iLevelExp			= (int)sdLevelInfo.levelInfos[i].levelProp["Experience"];
				m_strLevelName		= (string)sdLevelInfo.levelInfos[i].levelProp["ShowName"] + str;
				m_strLevelIntro		= (string)sdLevelInfo.levelInfos[i].levelProp["Description"];
				m_strLevelLoadingTex= (string)sdLevelInfo.levelInfos[i].levelProp["LoadingTex"];
				
				GameObject.Find("lb_levelname").GetComponent<UILabel>().text	= m_strLevelName;
				GameObject.Find("lb_levelintro").GetComponent<UILabel>().text	= m_strLevelIntro;
				GameObject.Find("lb_psprite").GetComponent<UILabel>().text		= m_iLevelSprite.ToString();
				GameObject.Find("lb_ptimes").GetComponent<UILabel>().text		= m_iLevelTimesLeft + " / " + m_iLevelTimes;
				GameObject.Find("lb_levelexp").GetComponent<UILabel>().text		= m_iLevelExp.ToString();
				
				string[] strItems = ((string)sdLevelInfo.levelInfos[i].levelProp["Drop"]).Split(';');
				int j=0,k=0,a=0;
				// 掉落配置要么是4项，要么是16项..
				if( strItems.Length <= 4 ) 
				{
					k = strItems.Length;
				}
				else
				{
					switch( (int)sdGameLevel.instance.mainChar.BaseJob )
					{
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:	a=0;  k=4;  break;
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:		a=4;  k=8;  break;
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:		a=8;  k=12; break;
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:	a=12; k=16; break;
					}
					if( k > strItems.Length ) k = strItems.Length;
				}
				for(;a<k;j++,a++)
				{
					if( strItems[a]=="0" || strItems[a]=="" ) break;

					m_strDropItem[j] = strItems[a];
					m_spDropItem[j].SetActive(true);
					//UISprite u = m_spDropItem[j].GetComponentInChildren<UISprite>();
					UISprite u = m_spDropItem[j].GetComponent<UISprite>();
					UISprite q = m_spDropItem[j].transform.FindChild("sp_quality").GetComponent<UISprite>();
					Hashtable tab = sdConfDataMgr.Instance().GetItemById(m_strDropItem[j]);
					
					int itemType = 0;
					if( int.Parse(m_strDropItem[j]) < 500000 )
					{
						// 装备图标..
						if( u!=null && tab!=null )
						{
							u.spriteName		= (string)tab["IconPath"];
							q.spriteName		= sdConfDataMgr.Instance().GetItemQuilityBorder( int.Parse(tab["Quility"].ToString()) );
							m_bHasAtlas[j]		= false;
							m_iDropItemAtlas[j]	= int.Parse((string)tab["IconID"]);
							sdConfDataMgr.Instance().LoadItemAtlas( m_iDropItemAtlas[j].ToString(),  OnSetAtlas);
						}
					}
					else
					{
						// 宠物图标..
						if( u!=null && tab!=null )
						{
							Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(tab["Expend"].ToString());
							u.spriteName	= petInfo["Icon"].ToString();
							q.spriteName	= sdConfDataMgr.Instance().GetPetQuilityBorder( int.Parse(petInfo["Ability"].ToString()) ); 
							itemType		= 1;
							if( sdConfDataMgr.Instance().PetAtlas != null )
								u.atlas		= sdConfDataMgr.Instance().PetAtlas;
							else
								sdConfDataMgr.Instance().LoadPetAtlas( new EventDelegate(OnSetPetAtlas) );
						}
					}
					
					switch(j)
					{
					case 0: m_DroupoutBtn1.m_ItemID = m_strDropItem[j]; m_DroupoutBtn1.m_ItemType=itemType; break;
					case 1: m_DroupoutBtn2.m_ItemID = m_strDropItem[j]; m_DroupoutBtn2.m_ItemType=itemType; break;
					case 2: m_DroupoutBtn3.m_ItemID = m_strDropItem[j]; m_DroupoutBtn3.m_ItemType=itemType; break;
					case 3: m_DroupoutBtn4.m_ItemID = m_strDropItem[j]; m_DroupoutBtn4.m_ItemType=itemType; break;
					}
				}
				for(;j<4;j++) m_spDropItem[j].SetActive(false);

				ResLoadParams param = new ResLoadParams();
				sdResourceMgr.Instance.LoadResource("UI/LoadingTex/$LT_"+m_strLevelLoadingTex+".png",LoadTexCallback,param,typeof(Texture));

				// 更新宠物数据.
				for(int p=1;p<=9;p++)
					GameObject.Find("bt_pet"+p).GetComponent<ChoosePetButton>().UpdatePet();
				UpdatePetTeam();

				break;
			}
		}
	}

	public void UpdatePetTeam()
	{
		if( m_PetTeamIdx != sdNewPetMgr.Instance.mPetCurTeam )
		{
			m_PetTeamIdx = sdNewPetMgr.Instance.mPetCurTeam;
			Vector3 pos = m_PetTeamPanel.gameObject.transform.localPosition;
			pos.x = m_PetTeamPanelPos;
			if( m_PetTeamIdx==0 )		{ pos.x-=  0.0f; m_spTeam1.spriteName="d1"; m_spTeam2.spriteName="d2"; m_spTeam3.spriteName="d2"; }
			else if( m_PetTeamIdx==1 )	{ pos.x-=400.0f; m_spTeam1.spriteName="d2"; m_spTeam2.spriteName="d1"; m_spTeam3.spriteName="d2"; }
			else if( m_PetTeamIdx==2 )	{ pos.x-=800.0f; m_spTeam1.spriteName="d2"; m_spTeam2.spriteName="d2"; m_spTeam3.spriteName="d1"; }
			SpringPanel.Begin(m_PetTeamPanel.gameObject,pos,13.0f);
		}
	}
	
	void onDragFinished()
	{
		Vector3 pos = m_PetTeamPanel.gameObject.transform.localPosition;
		float x = pos.x - m_PetTeamPanelPos;
		float step = 100.0f;

		if( x>=0 || x<-800.0f )
		{
			// 默认的拖动效果..
		}
		else if( m_PetTeamIdx == 0 )
		{
			if( x>-step )		 		x=0.0f;
			else if( x>-400.0f-step )	x=-400.0f;
			else 						x=-800.0f;
		}
		else if( m_PetTeamIdx == 1 )
		{
			if( x>-400.0f+step )		x=0.0f;
			else if( x>-400.0f-step )	x=-400.0f;
			else 						x=-800.0f;
		}
		else if( m_PetTeamIdx == 2 )
		{
			if( x>-400.0f+step )		x=0.0f;
			else if( x>-800.0f+step)	x=-400.0f;
			else 						x=-800.0f;
		}

		if( x >= 0 )			m_PetTeamIdx = 0;
		else if( x == -400 )	m_PetTeamIdx = 1;
		else if( x <= -800 )	m_PetTeamIdx = 2;
		if( m_PetTeamIdx==0 )		{ m_spTeam1.spriteName="d1"; m_spTeam2.spriteName="d2"; m_spTeam3.spriteName="d2"; }
		else if( m_PetTeamIdx==1 )	{ m_spTeam1.spriteName="d2"; m_spTeam2.spriteName="d1"; m_spTeam3.spriteName="d2"; }
		else if( m_PetTeamIdx==2 )	{ m_spTeam1.spriteName="d2"; m_spTeam2.spriteName="d2"; m_spTeam3.spriteName="d1"; }
		
		if( x<=0 && x>=-800.0f )
		{
			pos.x = x + m_PetTeamPanelPos;
			SpringPanel.Begin(m_PetTeamPanel.gameObject,pos,13.0f);
		}
	}
	
	void LoadTexCallback(ResLoadParams param,Object obj)
	{
		if( obj == null ) return;
		UITexture tex = GameObject.Find("tex_bg").GetComponent<UITexture>();
		tex.mainTexture = (Texture)obj;
		//tex.uvRect = new Rect(0.1f,0.05f,0.45f,0.9f);
		//tex.color = new Color(1.0f,1.0f,1.0f,1.0f);
		tex.uvRect = new Rect(0,0,1f,1f);
		tex.color = new Color(1.0f,1.0f,1.0f,0.5f);
	}
}

