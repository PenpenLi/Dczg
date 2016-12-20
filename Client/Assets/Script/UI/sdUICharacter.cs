using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public enum ChildUIType
{
	Type_Value,
	Type_Des,
}

struct stDialogueCharacter
{
	public GameObject character;
	public Vector3   pos;
	public Quaternion rotate;
	public string   name;
	public float   cameraDis;
};

public enum MSGCOLOR
{
	White=0,
	Red,
	Green,
	Blue,
	Yellow,
	Grey,
}

class sdUICharacter : Singleton<sdUICharacter>
{
	public sdUidataMgr uiData = new sdUidataMgr();
	private Hashtable m_infoCharacter = new Hashtable();
	private Dictionary<string, List<sdUICharacterChild>> m_child = new Dictionary<string, List<sdUICharacterChild>>();
	
	public List<stDialogueCharacter>  DialogueCharacterList = new List<stDialogueCharacter>();
	public delegate void NotifiDelegate(string strValue);
	public event NotifiDelegate Notify;

    public int gmLevel = 0;

	private GameObject equipPanel;
	private GameObject detailPanel;
	private GameObject detailTabBasePanel;
	private GameObject detailTabAttPanel;
	private GameObject detailTabDefPanel;
	private GameObject detailTabOtherPanel;
	private GameObject skillWnd;
	private GameObject friendWnd;
	private GameObject CampaignWnd;
	private GameObject LevelPrepareWnd;						// 鎴樻枟鍑嗗?绐楀彛.
	private GameObject LevelChoosePetWnd;					// 鎴樻枟鍑嗗?閫夋嫨瀹犵墿绐楀彛.
	private GameObject LevelChooseFriPetWnd;	
	private GameObject townUi;
	private GameObject roleWnd;
	private GameObject itemUpWnd;
	private GameObject itemSelectWnd;
	private sdMaskPanel maskPanel = null;
	private GameObject sceneMask = null;
	private GameObject arrowImg;
	private GameObject guideRoll;
	private GameObject avatar;
	private GameObject pvpMainWnd = null;  //pvp涓荤晫闈?aa
    private GameObject PVPRewards = null;  //pvp濂栧姳鐣岄潰aaa
    private GameObject PVPTitleWnd = null;    //pvpaaa
    private GameObject PVPTitleUpWnd = null;   //鍐涢樁鍗囩骇aaa
    private GameObject PVPRefreshWnd = null;   //pvp瀵规墜鍒锋柊aaa 
    private GameObject countdownWnd = null;    //鍊掕?鏃剁晫闈?aa
	public  bool       bFightInitVisible = true;  //fight鍒濆?鐨勬椂鍊欐槸鍚︽樉绀篴aa
    private GameObject ranklistWnd = null; //鎺掕?姒滅晫闈?aaa
    private GameObject mysteryShopWnd = null; //绁炵?鍟嗗簵鐣岄潰aaa
    public GameObject roleTipWnd = null; //鍏朵粬瑙掕壊鐣岄潰aaa
    public GameObject buyWnd = null;   //璐?拱绐楀彛aaaa
    private GameObject bbsWnd = null;   //鍏?憡鏉跨晫闈?a
    public GameObject SelectSrvWnd = null;  //閫夋湇鐣岄潰aa
    private GameObject scrolTextWnd = null; //璺戦┈鐏?晫闈?aa
    private GameObject countdownWnd2 = null;//灞忓箷涓?ぎ鍊掕?鏃禷aa
    private GameObject ptWnd = null;  //鎴橀瓊璇曠偧鐣岄潰aaa

    public bool m_bBBSWndFirst = true;  
    public string m_strbbs = "";  //鍏?憡鏂囧瓧aaa
    public List<string> m_lstScrollText = new List<string>(); //璺戦┈鐏?枃瀛梐aa
    public int playerScore = 0;
    public bool bBossMovie = false;//boss杩囧満鍔ㄧ敾aaaa

    public sdLootPanel lootPanel = null;

    public bool IsbbsWndActive()
    {
        if (bbsWnd != null && bbsWnd.active)
        {
            return true;
        }
        return false;
    }

	// Boss琛鏉＄被鍨婄鐩?墠鏈夊皬Boss鍜屽ぇBoss涓ょ?)aa
	protected int mMonsterHPType = -1;
	public int MonsterHPType
	{
		get { return mMonsterHPType; }
	}

	// Boss琛鏉℃暟閲廰a
	protected int mMonsterHpNum = 0;
	public int MonsterHpNum
	{
		get { return mMonsterHpNum; }
	}

	// 褰撳墠鍓╀綑琛鏉℃暟閲廰a
	protected int mCurrentMonsterHpNum = 0;
	public int CurrentMonsterHpNum
	{
		set { mCurrentMonsterHpNum = value; }
		get { return mCurrentMonsterHpNum; }
	}

	public bool isReady = false;
	private int reliveNum = 0;
	private int medicineNum = 0;
	private int freeMedicineNum = 0;
	private int freeReliveNum = 0;
	private int relivePrice = 0;
	private int medicinePrice = 0;
	private GameObject jiesuanWnd;
	CliProto.SC_TREASURE_CHEST_NTF jiesuanMsg = null;
    List<CliProto.SC_TREASURE_CHEST_NTF> sweepInfoList = new List<CliProto.SC_TREASURE_CHEST_NTF>();
	private GameObject tipWnd = null;
	
	public Hashtable lvupChange = new Hashtable();
	
	
	public int oldLevel = 0;
	public int oldExp = 0;
		
	int m_selectTreasure = 0;
	public int selectTreasure
	{
		set{m_selectTreasure = value;}	
		get{return m_selectTreasure;}
	}

	int m_fightScore = 0;
	public int fightScore
	{
		set{m_fightScore = value;}
		get{return m_fightScore;}
	}
	
	bool m_bIsFight = false;
	public bool IsFight
	{
		set{m_bIsFight = value;}
		get{return m_bIsFight;}
	}
	
	float m_fFightTime = 0f;
	public float fightTime
	{
		set{m_fFightTime = value;}
		get{return m_fFightTime;}
	}
	
	private GameObject fightUi = null;	
	public void SetFightUi(GameObject obj)
	{
		fightUi = obj;
	}
	public GameObject GetFightUi()
	{
		return fightUi;
	}
	public void       SetTownUI(GameObject town)
	{
		townUi = town;
	}
	public GameObject GetTownUI()
	{
		return townUi;
	}
		
	public string GetStrByType(ChildUIType type)
	{
		if (type == ChildUIType.Type_Value)
		{
			return "value";	
		}
		else if (type == ChildUIType.Type_Des)
		{
			return "des";	
		}
		
		return "";
	}
	
	public void SetAvatar(RenderTexture material)
	{
		if (avatar != null)
		{
			avatar.GetComponent<UITexture>().mainTexture = material;
		}
	}
	
	// 璁剧疆Boss琛鏉＄被鍨媋a
	public void SetMonsterMaxHp(int type, int hp, int num)
	{
		mMonsterHPType = type;
		mMonsterHpNum = num;
		mCurrentMonsterHpNum = num;

		Hashtable uiValueDesc = new Hashtable();
		uiValueDesc["value"] = hp;
		uiValueDesc["des"] = "";
		SetProperty("MonsterMaxHp", uiValueDesc);

		uiValueDesc["value"] = MonsterHpNum;
		SetProperty("MonsterHpNum", uiValueDesc);	
	}
	
	public void ShowRelive()
	{
		//int num = sdLevelInfo.ReliveLimit(sdLevelInfo.GetCurLevelId());
		//if (num == 0 || num == reliveNum) return;
		
		GameObject ui = GameObject.Find("FightUi");
		if (ui != null)
		{
			sdFightUi fight = ui.GetComponent<sdFightUi>();
			if (fight != null)
			{
				fight.ShowRelive();	
			}
		}
	}
	
	public void HideRelive()
	{
		GameObject ui = GameObject.Find("FightUi");
		if (ui != null)
		{
			sdFightUi fight = ui.GetComponent<sdFightUi>();
			if (fight != null)
			{
				fight.HideRelive();	
			}
		}
	}

	public void ShowProperty(string strKey, Hashtable table)
	{
		if (m_child.ContainsKey(strKey) && m_child[strKey] != null)
		{
			foreach(DictionaryEntry item in table)
			{
				if (item.Key.ToString() == GetStrByType(ChildUIType.Type_Value))
				{
					bool needNotify = false;
					foreach(sdUICharacterChild child in m_child[strKey])
					{
						if (child.type == ChildUIType.Type_Value)
						{
							Notify += new NotifiDelegate(child.Notify);
							needNotify = true;
						}
					}
					if (needNotify)
					{
						if (item.Value.GetType() == typeof(byte[]))
						{
							string str = Encoding.UTF8.GetString((byte[])item.Value);
							Notify(str.ToString());
						}
						else
						{
							Notify(item.Value.ToString());	
						}
					}
					
					foreach(sdUICharacterChild child in m_child[strKey])
					{
						if (child.type == ChildUIType.Type_Value)
						{
							Notify -= new NotifiDelegate(child.Notify);
						}
					}
				}
//				else if (item.Key.ToString() == GetStrByType(ChildUIType.Type_Des))
//				{
//					bool needNotify = false;
//					foreach(sdUICharacterChild child in m_child[strKey])
//					{
//						if (child.type == ChildUIType.Type_Des)
//						{
//							Notify += new NotifiDelegate(child.Notify);
//							needNotify = true;
//						}
//					}
//					if (needNotify)
//					{
//						Notify(item.Value.ToString());
//					}
//
//					foreach(sdUICharacterChild child in m_child[strKey])
//					{
//						if (child.type == ChildUIType.Type_Des)
//						{
//							Notify -= new NotifiDelegate(child.Notify);
//						}
//					}
//				}
			}
		}
	}
	
	public void SetProperty(string strKey, Hashtable table)
	{
		if (strKey == "Job")
		{
			table["value"] = sdConfDataMgr.Instance().GetJobName(table["value"].ToString());	
		}
		else if (strKey == "MoveSpeedModPer" || strKey == "AttSpeedModPer")
		{
			int val = (int)(float.Parse(table["value"].ToString())/100);	
			table["value"] = val.ToString();
		}
		else if (strKey == "CriDmg" || strKey == "CriDmgDef")
		{
			int val = (int)(float.Parse(table["value"].ToString())/100);	
			table["value"] = val.ToString();
		}
		
		uiData.SetRoleInfo(strKey, table);
		ShowProperty(strKey, table);
	}
	
	public void RemoveChild(sdUICharacterChild child)
	{
		if (m_child.ContainsKey(child.strKey) && m_child[child.strKey] != null) 
		{
			m_child[child.strKey].Remove(child);
		}
	}
	
	public void RegisterChild(sdUICharacterChild child)
	{
		if (child == null) return;
		if (m_child == null) m_child = new Dictionary<string, List<sdUICharacterChild>>();
		if (m_child.ContainsKey(child.strKey) && m_child[child.strKey] != null) 
		{
			m_child[child.strKey].Add(child);
		}
		else
		{
			List<sdUICharacterChild> list = new List<sdUICharacterChild>();
			list.Add(child);
			m_child.Add(child.strKey, list);
		}
	}
	
	public void LoadWnd(ResLoadParams param,UnityEngine.Object obj)
	{
        if (obj == null)
        {
            Debug.Log("loadwnd null");
            return;
        }
        if (param.info == "countdown")
        {
            countdownWnd = GameObject.Instantiate(obj) as GameObject;
            countdownWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            countdownWnd.transform.localScale = Vector3.one;
            countdownWnd.transform.localPosition = new Vector3(180,120,0);
        }
        else if(param.info == "SelectServerWnd")
        {
            SelectSrvWnd = GameObject.Instantiate(obj) as GameObject;
            SelectSrvWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            SelectSrvWnd.transform.localScale = Vector3.one;
            SelectSrvWnd.transform.localPosition = Vector3.zero;
            ShowSelectSrvWnd(true);
        }
        else if(param.info == "ptwnd")
        {
            ptWnd = GameObject.Instantiate(obj) as GameObject;
            ptWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            ptWnd.transform.localScale = Vector3.one;
            ptWnd.transform.localPosition = Vector3.zero;
            ShowPTWnd(true);
        }
        else if(param.info == "timecountdown2")
        {
            countdownWnd2 = GameObject.Instantiate(obj) as GameObject;
            countdownWnd2.transform.parent = sdGameLevel.instance.UICamera.transform;
            countdownWnd2.transform.localScale = Vector3.one;
            countdownWnd2.transform.localPosition = Vector3.zero;
            ShowCountDownTime2(true);
        }
        else if(param.info == "scrolltextwnd")
        {
            scrolTextWnd = GameObject.Instantiate(obj) as GameObject;
            scrolTextWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            scrolTextWnd.transform.localScale = Vector3.one;
            scrolTextWnd.transform.localPosition = Vector3.zero;
            scrolTextWnd.AddComponent<sdScrollTextWnd>();
            ShowScrollTextWnd(true);
        }
        else if(param.info == "roletipwnd")
        {
            roleTipWnd = GameObject.Instantiate(obj) as GameObject;
            roleTipWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            roleTipWnd.transform.localScale = Vector3.one;
            roleTipWnd.transform.localPosition = Vector3.zero;
            roleTipWnd.AddComponent<sdRoleTipWnd>();
            roleTipWnd.AddComponent<WndAniCB>();
            ShowRoleTipWnd((sdFriend)param.userdata0, true, (int)param.userdata1);
        }
        else if (param.info == "challengebuywnd")
        {
            buyWnd = GameObject.Instantiate(obj) as GameObject;
            buyWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            buyWnd.transform.localScale = Vector3.one;
            buyWnd.transform.localPosition = Vector3.zero;
            ShowChallengeBuyWnd((byte)param.userdata0);
        }
        else if (param.info == "pvpjiesuanwnd")
        {
            GameObject pvpjiesuan = GameObject.Instantiate(obj) as GameObject;
            pvpjiesuan.transform.parent = sdGameLevel.instance.UICamera.transform;
            pvpjiesuan.transform.localScale = Vector3.one;
            pvpjiesuan.transform.localPosition = Vector3.zero;
			WndAni.ShowWndAni(pvpjiesuan,false,"w_grey");
        }
        else if (param.info == "RoleWnd")
        {
            roleWnd = GameObject.Instantiate(obj) as GameObject;
            roleWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            roleWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            roleWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            GameObject item = GameObject.Find("btn_fashionTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = false;
            }

            detailTabBasePanel = GameObject.Find("tab_base");
            detailTabAttPanel = GameObject.Find("tab_att");
            detailTabDefPanel = GameObject.Find("tab_def");
            detailTabOtherPanel = GameObject.Find("tab_other");
            detailPanel = GameObject.Find("detailPanel");
            if (detailPanel != null)
            {
                ShowDetailTab(0);
                detailPanel.SetActive(false);
            }

            equipPanel = GameObject.Find("equipPanel");
            avatar = GameObject.Find("Avatar");

            ShowRoleWnd(isShowRole);
            isLoadRole = false;
        }
        else if (param.info == "SkillWnd")
        {
            skillWnd = GameObject.Instantiate(obj) as GameObject;
            skillWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            skillWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            skillWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowSkillWnd();
            isLoadSkill = false;
        }
        else if (param.info == "successPanel")
        {
            successPanel = GameObject.Instantiate(obj) as GameObject;
            successPanel.transform.parent = sdGameLevel.instance.UICamera.transform;
            successPanel.transform.localScale = new Vector3(300.0f, 300.0f, 1.0f);
            successPanel.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowSuccessPanel();
            isLoadSuccess = false;
        }
        else if (param.info == "CampaignUI")
        {
            CampaignWnd = GameObject.Instantiate(obj) as GameObject;
			CampaignWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
			CampaignWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			CampaignWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			mCampaignMgr = CampaignWnd.transform.FindChild("CullPanel/CampaignPanel/sp_campaign/sp_bottom/sp_spirit/bt_addsprite").GetComponent<CampaignMgr>();
			ShowTuitu((int)param.userdata0, (bool)param.userdata1, (float)param.userdata2, (float)param.userdata3);
			isLoadTuitu = false;
        }
        else if (param.info == "LevelPrepareUI")
        {
            LevelPrepareWnd = GameObject.Instantiate(obj) as GameObject;
            LevelPrepareWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            LevelPrepareWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            LevelPrepareWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			ShowLevelPrepare((int)param.userdata0,(float)param.userdata1,(float)param.userdata2);
			isLoadLevelPrepare = false;
        }
        else if (param.info == "jiesuanWnd")
        {
            jiesuanWnd = GameObject.Instantiate(obj) as GameObject;
            jiesuanWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            jiesuanWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            jiesuanWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            if ((int)param.userdata0 == 1)
            {
                ShowJiesuanWnd();
            }
            else
            {
                jiesuanWnd.SetActive(false);
            }
            isLoadJiesuan = false;
        }
        else if (param.info == "sweepWnd")
        {
            GameObject wnd = GameObject.Instantiate(obj) as GameObject;
            wnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            wnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            wnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            sweepWnd = wnd.GetComponent<sdSweepWnd>();
            isLoadSweep = false;
            ShowSweepWnd((bool)param.userdata0);
        }
        else if (param.info == "TipWnd")
        {
            tipWnd = GameObject.Instantiate(obj) as GameObject;
            tipWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            tipWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            tipWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -500f);
            ShowTip(lastTipType, lastTipId);
            isLoadTipWnd = false;
        }
        else if (param.info == "FriendWnd")
        {
            friendWnd = GameObject.Instantiate(obj) as GameObject;
            friendWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            friendWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            friendWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowFriendWnd();
            isLoadFriend = false;
        }
        else if (param.info == "maskPanel")
        {
            GameObject newObj = GameObject.Instantiate(obj) as GameObject;
            newObj.transform.parent = sdGameLevel.instance.UICamera.transform;
            newObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            newObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            maskPanel = newObj.GetComponent<sdMaskPanel>();
            ShowMask((bool)param.userdata0, (GameObject)param.userdata1);
            isLoadMask = false;
        }
        else if (param.info == "sceneMask")
        {
            sceneMask = GameObject.Instantiate(obj) as GameObject;
            GameObject temp = (GameObject)param.userdata0;

            ShowSceneMask(temp);
            isLoadSceneMask = false;
        }
        else if (param.info == "pvpmain")
        {
            pvpMainWnd = GameObject.Instantiate(obj) as GameObject;
            pvpMainWnd.transform.parent = sdGameLevel.instance.UICamera.transform;

            pvpMainWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            pvpMainWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowPVPMain(true);
        }
        else if(param.info == "mysteryshop")
        {
            mysteryShopWnd = GameObject.Instantiate(obj) as GameObject;
            mysteryShopWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            mysteryShopWnd.transform.localScale = Vector3.one;
            mysteryShopWnd.transform.localPosition = Vector3.zero;
            mysteryShopWnd.AddComponent<sdMysteryShopWnd>();
            ShowMysteryShop(true);
        }
        else if (param.info == "pvprefresh")
        {
            PVPRefreshWnd = GameObject.Instantiate(obj) as GameObject;
            PVPRefreshWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            PVPRefreshWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            PVPRefreshWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            PVPRefreshWnd.AddComponent<sdPVPRefreshWnd>();
            ShowPVPRefreshWnd(true);
        }
        else if (param.info == "pvptitle")
        {
            PVPTitleWnd = GameObject.Instantiate(obj) as GameObject;
            PVPTitleWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            PVPTitleWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            PVPTitleWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            PVPTitleWnd.AddComponent<sdPVPTitleWnd>();
            ShowPVPTitle(true);
        }
        else if (param.info == "pvptitleup")
        {
            PVPTitleUpWnd = GameObject.Instantiate(obj) as GameObject;
            PVPTitleUpWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            PVPTitleUpWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            PVPTitleUpWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            PVPTitleUpWnd.AddComponent<sdPVPTitleUpWnd>();
            ShowPVPTitleUpWnd(true);
        }
        else if (param.info == "pvpreward")
        {
            PVPRewards = GameObject.Instantiate(obj) as GameObject;
            PVPRewards.transform.parent = sdGameLevel.instance.UICamera.transform;
            PVPRewards.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            PVPRewards.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            sdPVPRewards rewardsWnd = PVPRewards.AddComponent<sdPVPRewards>();
            ShowPVPRewards(true);
        }
        else if(param.info == "ranklistwnd")
        {
            ranklistWnd = GameObject.Instantiate(obj) as GameObject;
            ranklistWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            ranklistWnd.transform.localScale = Vector3.one;
            ranklistWnd.transform.localPosition = Vector3.zero;
            sdRankListWnd wnd = ranklistWnd.AddComponent<sdRankListWnd>();
            ShowRankListWnd(true);
        }
        else if (param.info == "itemup")
        {
            itemUpWnd = GameObject.Instantiate(obj) as GameObject;
            itemUpWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            itemUpWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            itemUpWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowItemUpWnd( (bool)param.userdata1 );
            isLoadItemUp = false;
        }
        else if (param.info == "itemSelect")
        {
            itemSelectWnd = GameObject.Instantiate(obj) as GameObject;
            itemSelectWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            itemSelectWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            itemSelectWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowItemSelectWnd((SelectType)param.userdata0);
            isLoadItemSelect = false;
        }
        else if (param.info == "Arrow")
        {
            arrowImg = GameObject.Instantiate(obj) as GameObject;
            ShowArrow((GameObject)param.userdata0);
            isLoadArrow = false;
        }
        else if (param.info == "configWnd")
        {
            configWnd = GameObject.Instantiate(obj) as GameObject;
            configWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            configWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            configWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowConfigWnd((bool)param.userdata0);
            isLoadConfigWnd = false;
        }
		else if (param.info == "configWndF")
		{
			configWndF = GameObject.Instantiate(obj) as GameObject;
			configWndF.transform.parent = sdGameLevel.instance.UICamera.transform;
			configWndF.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			configWndF.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			ShowConfigWnd((bool)param.userdata0);
			isLoadConfigWnd = false;
		}
        else if (param.info == "guideRoll")
        {
            guideRoll = GameObject.Instantiate(obj) as GameObject;
            guideRoll.transform.parent = sdGameLevel.instance.UICamera.transform;
            guideRoll.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            guideRoll.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ShowGuideRoll(guideRollEvent);
            isLoadGuidRoll = false;
        }
        else if (param.info == "chatWnd")
        {
            chatWnd = GameObject.Instantiate(obj) as GameObject;
            chatWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
            chatWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            ShowChatWnd();
            isLoadChat = false;
        }
        else if (param.info == "scorePanel")
        {
            GameObject newObj = GameObject.Instantiate(obj) as GameObject;
            newObj.transform.parent = sdGameLevel.instance.UICamera.transform;
            newObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            scorePanel = newObj.GetComponent<sdScorePanel>();
            ShowScorePanel((int)param.userdata0, (int)param.userdata1);
            isLoadScore = false;
        }
	}

    bool isLoadScore = false;
    public sdScorePanel scorePanel = null;
    public void ShowScorePanel(int from, int to)
    {
        if (scorePanel == null)
        {
            if (isLoadScore) return;
            ResLoadParams param = new ResLoadParams();
            param.info = "scorePanel";
            param.userdata0 = from;
            param.userdata1 = to;
            sdResourceMgr.Instance.LoadResource("UI/$common/scorePanel.prefab", LoadWnd, param);
            isLoadScore = true;
        }
        else
        {
            scorePanel.Init(from, to);
            scorePanel.gameObject.SetActive(true);
        }
    }

	// RoleWnd
	bool isLoadRole = false;
	bool isShowRole = true;

    public void ShowbbsWnd(bool bShow, string strMsg, bool bMini, bool bAnimation)
    {
        if (bShow)
        {
            if (bbsWnd == null)
            {
                bbsWnd = GameObject.Instantiate(Resources.Load("msgbox/bbsWnd")) as GameObject;
                bbsWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
                bbsWnd.transform.localScale = Vector3.one;
                bbsWnd.transform.localPosition = Vector3.zero;
                bbsWnd.transform.localRotation = Quaternion.identity;
            }
            bbsWnd.SetActive(true);
            sdBBSWnd wnd = bbsWnd.GetComponent<sdBBSWnd>();
            if (wnd != null)
                wnd.ShowInfo(strMsg, bMini);
            if (bAnimation)
		        WndAni.ShowWndAni(bbsWnd,false,"sp_grey");
        }
        else
        {
			WndAni.HideWndAni(bbsWnd,false,"sp_grey");
        }
    }
	public void HidebbsWnd() { ShowbbsWnd(false,"",false,false); }
	
	public void ShowRoleWnd(bool isRole)
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(true);
        }
		isShowRole = isRole;
		if (roleWnd == null)
		{
			if (isLoadRole) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "RoleWnd";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$RoleWnd.prefab",LoadWnd, param);
			isLoadRole = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			roleWnd.SetActive(true);	
			
			sdUICharacter.Instance.SetAvatar(sdGameLevel.instance.avatarTexture);
			//GameObject mainChar = GameObject.Find("@MainCharacter");
			sdGameLevel	gl	=	sdGameLevel.instance;
			if(gl!=null)
			{
				gl.mainChar.RefreshAvatar();
			}
			uiData.RefreshRoleInfo();

			roleWnd.GetComponent<sdRoleWnd>().ShowMode(isRole);

			//ShowFullScreenUI(true);
			sdUILoading.ActiveSmallLoadingUI(false);
			WndAni.ShowWndAni(roleWnd,true,"w_black");
		}

		if(sdGameLevel.instance.avatarCamera!=null)
		{
			sdGameLevel.instance.avatarCamera.enabled	=	true;
		}
	}

	public void RefreshRoleInfo()
	{
		uiData.RefreshRoleInfo();
	}
	
	public void HideRoleWnd()
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(false);
        }
		if (roleWnd != null)
		{
			//roleWnd.SetActive(false);
            sdRoleWnd wnd = roleWnd.GetComponent<sdRoleWnd>();
            if (wnd.otherBtn.isActive)
            {
                Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Other);
                foreach (DictionaryEntry info in itemTable)
                {
                    sdGameItem temp = info.Value as sdGameItem;
                    temp.isNew = false;
                }
                wnd.otherBtn.HideRedTip();
            }

            if (wnd.armorBtn.isActive)
            {
                Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Armor);
                foreach (DictionaryEntry info in itemTable)
                {
                    sdGameItem temp = info.Value as sdGameItem;
                    temp.isNew = false;
                }
                wnd.armorBtn.HideRedTip();
            }

            if (wnd.weaponBtn.isActive)
            {
                Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Weapon);
                foreach (DictionaryEntry info in itemTable)
                {
                    sdGameItem temp = info.Value as sdGameItem;
                    temp.isNew = false;
                }
                wnd.weaponBtn.HideRedTip();
                
            }

            if (wnd.shipinBtn.isActive)
            {
                Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Shipin);
                foreach (DictionaryEntry info in itemTable)
                {
                    sdGameItem temp = info.Value as sdGameItem;
                    temp.isNew = false;
                }
                wnd.shipinBtn.HideRedTip();
            }
			
			sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_Item);
			sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
			//ShowFullScreenUI(false);
			WndAni.HideWndAni(roleWnd,true,"w_black");
		}	
		if(sdGameLevel.instance.avatarCamera!=null)
		{
			sdGameLevel.instance.avatarCamera.enabled	=	false;
		}
	}

    public GameObject GetRoleWnd()
    {
        return roleWnd;
    }

	public void ShowBag()
	{
		if (roleWnd == null) return;
		roleWnd.GetComponent<sdRoleWnd>().ShowBag();	
	}
	
	public void ShowRoleProperty()
	{
		if (roleWnd == null) return;
		roleWnd.GetComponent<sdRoleWnd>().ShowProperty();
	}

    public void ShowCountDownTime(bool bShow, eCountDownType type)
    {
        if (bShow)
        {
            if(countdownWnd != null)
            {
                countdownWnd.SetActive(true);
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "countdown";
                param.userdata1 = type;
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/timecountdown.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (countdownWnd != null)
                countdownWnd.SetActive(false);
        }
    }

    public void ShowMysteryShop(bool bShow)
    {
        if (bShow)
        {
            SetTownUI_LockPanelActive(true);
            if (mysteryShopWnd != null)
            {
                mysteryShopWnd.SetActive(true);
                sdMysteryShopWnd wnd = mysteryShopWnd.GetComponent<sdMysteryShopWnd>();
                if (wnd != null)
                    wnd.Refresh();
                sdMysteryShopMsg.Send_SHOP_SECRET_GOODS_REQ();
				WndAni.ShowWndAni(mysteryShopWnd,true,"w_black");
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "mysteryshop";
                sdResourceMgr.Instance.LoadResource("UI/$mysteryshop/mysteryshopwnd.prefab", LoadWnd, param);
            }
        }
        else
        {
            SetTownUI_LockPanelActive(false);
            if(mysteryShopWnd != null)
			    WndAni.HideWndAni(mysteryShopWnd,true,"w_black");
        }
    }

    void SetTownUI_LockPanelActive(bool bActive)
    {
        if (sdUICharacter.Instance.GetTownUI() != null)
            sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(bActive);
    }

	// PVP Wnd
	public void ShowPVPMain(bool bShow) { ShowPVPMain(bShow,true); }
	public void ShowPVPMain(bool bShow,bool bRealShow)
	{
		if(bShow)
		{
            SetTownUI_LockPanelActive(true);
			if(pvpMainWnd != null)
			{
				pvpMainWnd.SetActive(true);
                sdPVPWnd pvpWnd = pvpMainWnd.GetComponent<sdPVPWnd>();
				if( pvpWnd != null ) pvpWnd.ShowPK();
				if( bRealShow )
				{
					sdUILoading.ActiveSmallLoadingUI(false);
					WndAni.ShowWndAni(pvpMainWnd,true,"w_black");
				}
			}
			else
			{
				ResLoadParams param = new ResLoadParams();
				param.info = "pvpmain";
				sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/pvpmain.prefab", LoadWnd, param);
			}
		}
		else
		{
            SetTownUI_LockPanelActive(false);
			if(pvpMainWnd != null)
				WndAni.HideWndAni(pvpMainWnd,true,"w_black");
		}
	}

    public void ShowPVPRefreshWnd(bool bShow)
    {
        if (bShow)
        {
            if (PVPRefreshWnd != null)
            {
                PVPRefreshWnd.SetActive(true);
                PVPRefreshWnd.GetComponent<sdPVPRefreshWnd>().Refresh();
				WndAni.ShowWndAni(PVPRefreshWnd,false,"w_grey");
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "pvprefresh";
				sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/pvprefresh.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (PVPRefreshWnd != null)
            {
				WndAni.HideWndAni(PVPRefreshWnd,false,"w_grey");
            }
        }
    }

    public void ShowPVPTitleUpWnd(bool bShow)
    {
        if (bShow)
        {
            if (PVPTitleUpWnd != null)
            {                
                PVPTitleUpWnd.SetActive(true);
                PVPTitleUpWnd.GetComponent<sdPVPTitleUpWnd>().Refresh();
				WndAni.ShowWndAni(PVPTitleUpWnd,false,"w_grey");
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "pvptitleup";
				sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/titleupgrade.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (PVPTitleUpWnd != null)
            {
				WndAni.HideWndAni(PVPTitleUpWnd,false,"w_grey");
            }
        }
    }

    public void ShowRankListWnd(bool bShow)
    {
        if (bShow)
        {
            SetTownUI_LockPanelActive(true);
            if (ranklistWnd != null)
            {
                ranklistWnd.SetActive(true);
                ranklistWnd.GetComponent<sdRankListWnd>().Refresh();
				WndAni.ShowWndAni(ranklistWnd,true,"w_black");
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "ranklistwnd";
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$ranklist.prefab", LoadWnd, param);
            }
        }
        else
        {
            SetTownUI_LockPanelActive(false);
            if(ranklistWnd != null)
			    WndAni.HideWndAni(ranklistWnd,true,"w_black");
        }
    }

    public void ShowRoleTipWnd(sdFriend info, bool bShow, int nType)
    {
        if (bShow)
        {
            if (roleTipWnd != null)
            {
                WndAni.ShowWndAni(roleTipWnd, false, "Sprite_black");
                roleTipWnd.GetComponent<sdRoleTipWnd>().Refresh(info, nType);
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "roletipwnd";
                param.userdata0 = info;
                param.userdata1 = nType;
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$RoleTipWnd.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (roleTipWnd != null)
            {
                WndAni.HideWndAni(roleTipWnd, false, "Sprite_black");
                //roleTipWnd.SetActive(false);
            }
        }
    }
	public void HideRoleTipWnd() { ShowRoleTipWnd(null,false,0); } 

    public string GetOtherRoleName()
    {
        if (roleTipWnd != null)
        {
            sdRoleTipWnd wnd = roleTipWnd.GetComponent<sdRoleTipWnd>();
            if (wnd != null)
                return wnd.m_strName;
        }
        return null;
    }

	public void ShowPVPRewards(bool bShow)
	{
		if(bShow)
		{
			if(PVPRewards != null)
			{
				PVPRewards.SetActive(true);
                PVPRewards.GetComponent<sdPVPRewards>().Refresh();
				PVPRewards.transform.FindChild("Sprite/panel_pvpreward").gameObject.SetActive(false);
				WndAni.ShowWndAni(PVPRewards,false,"w_grey");
			}
			else
			{
				ResLoadParams param = new ResLoadParams();
				param.info = "pvpreward";
				sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/pvpreward.prefab", LoadWnd, param);
			}
		}
		else
		{
			if(PVPRewards != null)
			{
				//PVPRewards.SetActive(false);
				PVPRewards.transform.FindChild("Sprite/panel_pvpreward").gameObject.SetActive(false);
				WndAni.HideWndAni(PVPRewards,false,"w_grey");
			}
		}
	}

    public void ShowPVPTitle(bool bShow)
    {
        if (bShow)
        {
            if (PVPTitleWnd != null)
            {
                PVPTitleWnd.SetActive(true);
                PVPTitleWnd.GetComponent<sdPVPTitleWnd>().Refresh();
				WndAni.ShowWndAni(PVPTitleWnd,false,"w_grey");
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "pvptitle";
				sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/titleinfo.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (PVPTitleWnd != null)
				WndAni.HideWndAni(PVPTitleWnd,false,"w_grey");
        }
 
    }

    public bool tipCanEquip = true;

	// Tip Wnd
	bool isLoadTipWnd = false;
	TipType lastTipType;
	public string lastTipId = "";
	public void ShowTip(TipType type, string id)
	{
		if (tipWnd == null) 
		{
			lastTipType = type;
			lastTipId = id;
			if (isLoadTipWnd) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "TipWnd";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$TipWnd.prefab",LoadWnd, param);
			isLoadTipWnd = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(false);
			lastTipType = type;
			lastTipId = id;
			//tipWnd.SetActive(true);
			WndAni.ShowWndAni(tipWnd,false,"bg_grey");
            tipWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -500f);
			tipWnd.GetComponent<sdTipWnd>().ShowTip(type, id);
//             tipWnd.GetComponent<UIPanel>().depth = sdPVPManager.Instance.GetDepth();
//             tipWnd.transform.FindChild("itempanel").GetComponent<UIPanel>().depth = sdPVPManager.Instance.GetDepth();            
		}
	}
	public void HideTip() { HideTip(false); }
	public void HideTip(bool bImm)
	{
		if (tipWnd != null) 
		{
			if( bImm ) 
				tipWnd.SetActive(false);	
			else
				WndAni.HideWndAni(tipWnd,false,"bg_grey");
		}
	}
	
    public void RefreshTip()
    {
        if (tipWnd == null) return;
        tipWnd.GetComponent<sdTipWnd>().RefreshTip();
    }

	public void TipNextSkill()
	{
		if (tipWnd == null) return;
		tipWnd.GetComponent<sdTipWnd>().TipNextSkill();
	}

    public bool IsTipShow()
    {
        if (tipWnd != null && tipWnd.active)
        {
            return true;
        }

        return false;
    }

	// Scene Mask
	bool isLoadSceneMask = false;
	public void ShowSceneMask(GameObject obj)
	{
		if (sceneMask == null)
		{
			if (isLoadSceneMask) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "sceneMask";
			param.userdata0 = obj;
			sdResourceMgr.Instance.LoadResource("UI/$Guide/scenemask.prefab",LoadWnd, param);
			isLoadSceneMask = true;
			//sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			Vector3 pos = obj.transform.position ;
			Vector3 loaclpos = sdGameLevel.instance.mainCamera.GetComponent<Camera>().transform.InverseTransformPoint(pos);
			loaclpos /= loaclpos.z/20.0f;
			sceneMask.transform.parent = sdGameLevel.instance.mainCamera.transform;
			sceneMask.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			sceneMask.transform.localPosition	=	loaclpos;
			sceneMask.transform.localRotation = Quaternion.identity;
			sceneMask.SetActive(true);
			if (sceneMaskEvent != null)
			{
				AddSceneMaskEvent(sceneMaskEvent);	
			}
		}
	}

	public void HideSceneMask()
	{
		if (sceneMask != null) sceneMask.SetActive(false);
	}
	
    public void SetMaskPanel(sdMaskPanel panel)
    {
        maskPanel = panel;
    }

	bool isLoadMask = false;
	public void ShowMask(bool hasBg, GameObject obj)
	{
		if (maskPanel == null)
		{
            return;
// 			if (isLoadMask) return;
// 			ResLoadParams param = new ResLoadParams();
// 			param.info = "maskPanel";
// 			param.userdata0 = hasBg;
// 			param.userdata1 = obj;
// 			sdResourceMgr.Instance.LoadResource("UI/$common/mask.prefab",LoadWnd, param);
// 			isLoadMask = true;
			//sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			maskPanel.ShowPanel(hasBg, obj);
			if (maskEvent != null)
			{
				AddMaskEvent(maskEvent, maskNeedTarget);	
			}
			//ShowFullScreenUI(true);
			//sdUILoading.ActiveSmallLoadingUI(false);
			//friendWnd.GetComponent<sdFriendWnd>().RefreshFri(false);
		}
	}
	
	public void HideMask()
	{
		if (maskPanel != null) maskPanel.HidePanel();
	}

	public void HideMaskPoint()
	{
		if (maskPanel != null) maskPanel.HideMaskPoint();
	}
	
	public sdMaskPanel GetMask()
	{
		return maskPanel;	
	}

	EventDelegate sceneMaskEvent = null;
	
	public void AddSceneMaskEvent(EventDelegate e)
	{
		if (sceneMask != null) 
		{
			sceneMask.GetComponentInChildren<sdMaskClick>().onClick.Add(e);
			sceneMaskEvent = null;
		}
		else
		{
			sceneMaskEvent = e;
		}
	}
	
	
	EventDelegate maskEvent = null;
	bool maskNeedTarget = false;
	
	public void AddMaskEvent(EventDelegate e, bool needTarget)
	{
		if (maskPanel != null) 
		{
			if (!maskNeedTarget && !needTarget)
			{
				maskPanel.clickBtn.onClick.Add(e);
			}
			else
			{
				maskPanel.targetBtn.onClick.Add(e);
			}
			maskEvent = null;
			maskNeedTarget = false;
		}
		else
		{
			maskEvent = e;
			maskNeedTarget = needTarget;
		}
	}

    public GameObject chatWnd = null;
    bool isLoadChat = false;
    public void ShowChatWnd()
    {
        if (chatWnd == null)
        {
            if (isLoadChat) return;

            ResLoadParams param = new ResLoadParams();
            param.info = "chatWnd";
            sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$chatPanel.prefab", LoadWnd, param);
            isLoadChat = true;
        }
        else
        {
            chatWnd.SetActive(true);
            //chatWnd.GetComponent<sdChatWnd>().Refresh();
        }
    }

    public void HideChatWnd()
    {
        if (chatWnd == null) return;
        chatWnd.SetActive(false);
    }

    public void AddChatInfo(string txt)
    {
        if (chatWnd != null)
        {
            chatWnd.GetComponent<sdChatWnd>().AddChatInfo(txt);
        }
    }

    public bool IsChatWndActive()
    {
        if (chatWnd != null && chatWnd.active)
        {
            return true;
        }

        return false;
    }

	bool isLoadItemUp = false;

	bool bTownBtn = false;
	public void ShowItemUpWnd() { ShowItemUpWnd(false); }
	public void ShowItemUpWnd(bool bTown)
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(true);
        }
		bTownBtn = bTown;
		if (itemUpWnd == null) 
		{
			if (isLoadItemUp) return;

			ResLoadParams param = new ResLoadParams();
			param.info = "itemup";
			param.userdata1 = bTown;
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$itemUpWnd.prefab",LoadWnd, param);
			isLoadItemUp = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			itemUpWnd.SetActive(true);
            GameObject level = GameObject.Find("NGUIRoot");
            sdRadioButton[] list = level.GetComponentsInChildren<sdRadioButton>();
            foreach (sdRadioButton item in list)
            {
                if (item.index == 31 && item.gameObject.name == "tab_qh")
                {
                    ActiceRadioBtn(item);
                    item.Active(true);
                }
            }

			itemUpWnd.GetComponent<sdItemUpWnd>().ShowItemUpPanel();
           // itemUpWnd.GetComponent<UIPanel>().depth = sdPVPManager.Instance.GetDepth();
			//if( bTownBtn ) ShowFullScreenUI(true);
			sdUILoading.ActiveSmallLoadingUI(false);
			uiData.RefreshRoleInfo();
			WndAni.ShowWndAni(itemUpWnd,true,"w_black");
		}
	}

	public sdItemUpWnd GetItemUpWnd()
	{
		if (itemUpWnd != null)
		{
			return itemUpWnd.GetComponent<sdItemUpWnd>();
		}
		return null;
	}

	public void HideItemUpWnd()
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(false);
        }
		if (itemUpWnd != null)
		{
			sdGameItemMgr.Instance.upItem = null;
			sdGameItemMgr.Instance.ClearItemUpId();
			//itemUpWnd.SetActive(false);
			//if( bTownBtn ) ShowFullScreenUI(false);
			WndAni.HideWndAni(itemUpWnd,true,"w_black");
		}
	}

	bool isLoadItemSelect = false;
	EventDelegate itemSelectEvet = null;
	public void ShowItemSelectWnd(SelectType classtype)
	{
		if (itemSelectWnd == null) 
		{
			if (isLoadItemSelect) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "itemSelect";
            param.userdata0 = classtype;
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$itemSelectWnd.prefab",LoadWnd, param);
			isLoadItemSelect = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
            //WndAni.ShowWndAni(itemSelectWnd, false, "bg_grey");
            itemSelectWnd.GetComponent<sdItemSelectWnd>().SetEquipPos(selectWndPos);
            itemSelectWnd.GetComponent<sdItemSelectWnd>().ShowWnd(classtype, 0);
            itemSelectWnd.SetActive(true);
			//ShowFullScreenUI(true);
			sdUILoading.ActiveSmallLoadingUI(false);
			if (itemSelectEvet != null)
			{
				AddEventOnSelectWnd(itemSelectEvet);
				itemSelectEvet = null;
			}
		}
	}

    public void SortSelectWnd(int sortType)
    {
        if (itemSelectWnd != null)
        {
            itemSelectWnd.GetComponent<sdItemSelectWnd>().SortItem(sortType);
        }
    }

    public void AddItemByQuility(int quility)
    {
        if (itemSelectWnd != null)
        {
            if ((itemSelectWnd.GetComponent<sdItemSelectWnd>().sellType & (1 << quility)) > 0)
            {
                itemSelectWnd.GetComponent<sdItemSelectWnd>().sellType ^= (1<<quility);
            }
            else
            {
                itemSelectWnd.GetComponent<sdItemSelectWnd>().sellType |= (1 << quility);
            }
            itemSelectWnd.GetComponent<sdItemSelectWnd>().SortItem(0);
        }
    }

	public void HideSelectWnd()
	{
		if (itemSelectWnd != null)
		{
            itemSelectWnd.SetActive(false);
            //WndAni.HideWndAni(itemSelectWnd, false, "bg_grey");
            itemSelectWnd.GetComponent<sdItemSelectWnd>().sellType = 0;
            itemSelectWnd.GetComponent<sdItemSelectWnd>().ClearEvent();
		}
	}

	public void AddEventOnSelectWnd(EventDelegate OnSelect)
	{
		if (itemSelectWnd != null)
		{
			itemSelectWnd.GetComponent<sdItemSelectWnd>().AddEventOnSelectWnd(OnSelect);
		}
		else
		{
			itemSelectEvet = OnSelect;
		}
	}

    public void SetItemUpWndMoney(string moeny)
    {
        if (itemUpWnd != null)
        {
            itemUpWnd.GetComponent<sdItemUpWnd>().SetMoney(moeny);
        }
    }

    public void SetGemMergeMoney(string moeny1, string moeny2)
    {
        if (itemUpWnd != null)
        {
            itemUpWnd.GetComponent<sdItemUpWnd>().SetMergeMoeny(moeny1, moeny2);
        }
    }

	bool isLoadGuidRoll = false;
	EventDelegate guideRollEvent;
	protected bool mOldFingerControlStatus = true;
	public void ShowGuideRoll(EventDelegate e)
	{
		if (guideRoll == null) 
		{
			if (isLoadGuidRoll) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "guideRoll";
			sdResourceMgr.Instance.LoadResource("UI/$Guide/slider.prefab",LoadWnd, param);
			isLoadGuidRoll = true;
			guideRollEvent = e;
		}
		else
		{
			mOldFingerControlStatus = sdGameLevel.instance.GetFingerControl().Enable;
			sdGameLevel.instance.GetFingerControl().Enable = false;

			if (guideRollEvent != null)
			{
				guideRoll.GetComponentInChildren<sdGuideExtra>().onFinish.Add(guideRollEvent);
				guideRollEvent = null;
			}
            //sdGameLevel.instance.mainChar.AutoFightSystem.Enable = false;
			guideRoll.SetActive(true);
		}
	}
	
	public void HideGuidRoll()
	{
        //sdGameLevel.instance.mainChar.AutoFightSystem.Enable = true;
		if (guideRoll != null) guideRoll.SetActive(false);

		sdGameLevel.instance.GetFingerControl().Enable = mOldFingerControlStatus;
	}

	bool isLoadArrow = false;
	public void ShowArrow(GameObject obj)
	{
		if (arrowImg == null) 
		{
			if (isLoadArrow) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "Arrow";
			param.userdata0 = obj;
			sdResourceMgr.Instance.LoadResource("UI/$Guide/Arrow.prefab",LoadWnd, param);
			isLoadArrow = true;
		}
		else
		{
			arrowImg.transform.parent = obj.transform.parent;
			arrowImg.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			arrowImg.transform.localPosition = new Vector3(obj.transform.localPosition.x + 40,
			                                               obj.transform.localPosition.y - 40,obj.transform.localPosition.z);
			arrowImg.SetActive(true);
		}
	}

    public void ShowCountDownTime2(bool bShow)
    {
        if (bShow)
        {
            if (countdownWnd2 != null)
            {
                countdownWnd2.SetActive(true);
                sdTimeCountDown2 wnd = countdownWnd2.GetComponent<sdTimeCountDown2>();
                wnd.SetTime((int)sdPVPManager.Instance.m_fCountDown);
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "timecountdown2";
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/timecountdown2.prefab", LoadWnd, param);
            }
        }
        else
        {
            if(countdownWnd2)
                countdownWnd2.SetActive(false);
        }
    }

	public void HideArrow()
	{
		if (arrowImg != null) arrowImg.SetActive(false);
	}

	// Friend Wnd
	bool isLoadFriend = false;
	public void ShowFriendWnd()
	{
		if (friendWnd == null) 
		{
			if (isLoadFriend) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "FriendWnd";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$FriendWnd.prefab",LoadWnd, param);
			isLoadFriend = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			//friendWnd.SetActive(true);
			//ShowFullScreenUI(true);
			sdUILoading.ActiveSmallLoadingUI(false);
			friendWnd.GetComponent<sdFriendWnd>().RefreshFri(false);
			WndAni.ShowWndAni(friendWnd,true,"w_black");
		}
	}

    public void ShowChallengeBuyWnd(byte byType)
    {
        if(buyWnd)
        {
            buyWnd.SetActive(true);
            sdPVPTimeBuyWnd wnd = buyWnd.GetComponent<sdPVPTimeBuyWnd>();
            if (wnd)
                wnd.Refresh(byType);
            WndAni.ShowWndAni(buyWnd, false, "w_grey");
        }
        else
        {
            ResLoadParams param = new ResLoadParams();
            param.info = "challengebuywnd";
            param.userdata0 = byType;
            sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/pvpbuytime.prefab", LoadWnd, param);
        }
    }

    public void ShowPVPJiesuanWnd()
    {
        ResLoadParams param = new ResLoadParams();
        param.info = "pvpjiesuanwnd";
		sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$PVPPrefab/pvpjiesuan.prefab", LoadWnd, param);
    }
	
	public void ShowFriPetAvatar(string id)
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowPetAvatar(id);
		}	
	}

    public void ShowComboWnd(bool bShow, int nAddCombo)
    {
        if (fightUi == null)
            return;
        sdFightUi fight = fightUi.GetComponent<sdFightUi>();
        if (fight == null)
            return;
        if (bShow)
        {
            fight.comboRoot.gameObject.SetActive(true);
            fight.AddCombo(nAddCombo);
        }
        else
        {
            fight.ResetCombo();
            fight.comboRoot.gameObject.SetActive(false);
        }
    }
	
	public void ShowFriAvatar()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowFriAvatar();
		}	
	}
	
	public void HideFriendWnd()
	{
		if (friendWnd != null)
		{
			//friendWnd.SetActive(false);
			//ShowFullScreenUI(false);
			sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_Friend);
			WndAni.HideWndAni(friendWnd,true,"w_black");
		}
	}
	
	public void ShowFriendTab()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowFriTab();	
		}
	}
	
	public void ShowInviteFriTab()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowInviteTab();
		}
	}
	
	bool onlineFri = false;
	public void ShowFriendOnline()
	{
		if (friendWnd != null)
		{
			onlineFri = !onlineFri;
			friendWnd.GetComponent<sdFriendWnd>().RefreshFri(onlineFri);
		}
	}
	
	public void ShowSearchFri()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowSearchFri();
		}	
	}
	
	public void RefreshFri()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().RefreshFri(onlineFri);
		}
	}
	
	public void ShowAddFriTab()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowAddFriTab();	
		}
	}
	
	public void ShowFriInfo()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().ShowFriInfo();	
		}
	}
	
	public void HideFriInfo()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().HideFriInfo();
		}
	}
	
	public void RefreshFriRequest()
	{
		if (friendWnd != null)
		{
			friendWnd.GetComponent<sdFriendWnd>().RefreshFriRequest();
		}
	}
	
	string curFriId = "";
	
	public void SetCurFriId(string id)
	{
		curFriId = id;	
	}
	
	public string GetCurFriId()
	{
		return curFriId;	
	}
	
	public string GetSearchFriName()
	{
		if (friendWnd != null)
		{
			return friendWnd.GetComponent<sdFriendWnd>().GetCurSearchName();	
		}
		
		return "";
	}

    public GameObject successPanel = null;
    bool isLoadSuccess = false;

    public void ShowSuccessPanel()
    {
        //if (successPanel == null)
        //{
        //    if (isLoadSuccess) return;
        //    ResLoadParams param = new ResLoadParams();
        //    param.info = "successPanel";
        //    sdResourceMgr.Instance.LoadResource("UI/$common/successPanel.prefab", LoadWnd, param);
        //    isLoadSuccess = true;
        //}
        //else
        //{
        //    successPanel.SetActive(true);
        //    successPanel.transform.GetChild(0).GetComponent<sdCopyItem>().Show();
        //    if (SuccessPanelEvent != null)
        //    {
        //        successPanel.transform.GetChild(0).GetComponent<sdCopyItem>().onFinish.Add(SuccessPanelEvent);
        //        SuccessPanelEvent = null;
        //    }
        //}
        sdCopyItem.ShowSuccess(null);
    }

    public void HideSuccessPanel()
    {
        sdCopyItem.HideSuccess();
    }

    //EventDelegate SuccessPanelEvent = null;
    //public void AddSuccessPanelEvent(EventDelegate e)
    //{
    //    if (successPanel == null)
    //    {
    //        SuccessPanelEvent = e;
    //    }
    //    else
    //    {
    //        successPanel.transform.GetChild(0).GetComponent<sdCopyItem>().onFinish.Add(e);
    //    }
    //}

    //public void HideSuccessPanel()
    //{
    //    successPanel.transform.GetChild(0).GetComponent<sdCopyItem>().Hide();
    //}
	
	bool isLoadSkill = false;
	
	public void ShowSkillWnd()
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(true);
        }
		if (skillWnd == null) 
		{
			if (isLoadSkill) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "SkillWnd";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$SkillWnd.prefab",LoadWnd, param);
			isLoadSkill = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			//skillWnd.SetActive(true);
			//ShowFullScreenUI(true);
			sdUILoading.ActiveSmallLoadingUI(false);
			WndAni.ShowWndAni(skillWnd,true,"w_black");
            skillWnd.GetComponent<sdSkillWnd>().Refresh();
			sdSlotMgr.Instance.Init();
		}
	}
	public void HideSkillWnd()
	{
        if (townUi != null)
        {
            townUi.GetComponent<sdTown>().lockPanel.SetActive(false);
        }
		if (skillWnd != null)
		{
			//skillWnd.SetActive(false);
			//ShowFullScreenUI(false);
			WndAni.HideWndAni(skillWnd,true,"w_black");
			sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_Skill);
		}
	}
	
	// 杞藉叆鎴樺焦绐楀彛.
	bool isLoadTuitu = false;
	CampaignMgr mCampaignMgr = null;
	float fCampaignPosX = 0;
	float fCampaignPosY = 0;
	public int	iLastCampaignID		= 11;		// 褰撳墠鎴樺焦ID.aa
	public int	iCurrentLevelID		= 0;		// 褰撳墠娓告垙鍏冲崱.闈炴垬褰瑰湴鍥句粠1000寮濮媋a
	public bool	bCampaignLastLevel	= false;	// 姝ゅ叧鍗′负褰撳墠鎴樺焦鏈鍚庝竴涓?叧鍗★紝涓斾箣鍓嶆病鏈夐氬叧杩嗧aa
	public void ShowTuitu() { ShowTuitu(iLastCampaignID,false,0,0); }
	public void ShowTuitu(bool bImm) { ShowTuitu(iLastCampaignID,bImm,0,0); }
	public void ShowTuitu(int iCampaign) { ShowTuitu(iCampaign,false,0,0); }
	public void ShowTuitu(int iCampaign,bool bImm,float startX,float startY)
	{
        if (sdUICharacter.Instance.GetTownUI() != null)
        {
            sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
        }
		iLastCampaignID = iCampaign;
		if( CampaignWnd == null ) 
		{
			if (isLoadTuitu) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "CampaignUI";
			param.userdata0 = iCampaign;
			param.userdata1 = bImm;
			param.userdata2 = startX;
			param.userdata3 = startY;
			sdResourceMgr.Instance.LoadResource("UI/LevelUI/$CampaignWnd.prefab", LoadWnd, param);
			isLoadTuitu = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(false);
			CampaignWnd.SetActive(true);
			mCampaignMgr.ShowCampaign(iLastCampaignID);
			if( bImm )
				ShowFullScreenUI(true);
			else
			{
				fCampaignPosX = startX;
				fCampaignPosY = startY;
				WndAni.ShowWndAni(CampaignWnd,true,"w_black",startX,startY);
			}
            BubbleManager.Instance.OnOpenWnd();
		}
	}
	public void HideTuitu(bool bImm)
	{
        if (sdUICharacter.Instance.GetTownUI() != null)
        {
            sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
        }
		if( CampaignWnd == null ) return;
		if( mCampaignMgr.IsAni() ) return;		// 鍔ㄧ敾鐘舵佷笅涓嶈兘鍏抽棴绐楀彛.
		if( bImm )
			CampaignWnd.SetActive(false);
		else
		{
			WndAni.HideWndAni(CampaignWnd,true,"w_black",fCampaignPosX,fCampaignPosY);
			fCampaignPosX = fCampaignPosY = 0;
		}
        BubbleManager.Instance.OnCloseWnd();
	}
	
	// 杞藉叆鎴樻枟鍑嗗?绐楀彛.
	bool isLoadLevelPrepare = false;
	float fLevelPreparePosX = 0;
	float fLevelPreparePosY = 0;
	public void ShowLevelPrepare(int iLevelID,float startX,float startY)
	{
		if( LevelPrepareWnd == null ) 
		{
			if(isLoadLevelPrepare) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "LevelPrepareUI";
			param.userdata0 = iLevelID;
			param.userdata1 = startX;
			param.userdata2 = startY;
			sdResourceMgr.Instance.LoadResource("UI/LevelUI/$LevelPrepareWnd.prefab",LoadWnd, param);
			isLoadLevelPrepare = true;
			sdUILoading.ActiveSmallLoadingUI(true);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(false);
			LevelPrepareWnd.SetActive(true);
			ReadyButton bt = LevelPrepareWnd.GetComponentInChildren<ReadyButton>();
			if(bt) bt.SetTargetLevel(iLevelID,mCampaignMgr.GetCampaignBG());

			fLevelPreparePosX = startX;
			fLevelPreparePosY = startY;
			WndAni.ShowWndAni(LevelPrepareWnd,true,"w_black",startX,startY);
		}
	}
	public void HideLevelPrepare()
	{
		if( LevelPrepareWnd == null ) return;
		if( CampaignWnd != null ) CampaignWnd.SetActive(true);
		WndAni.HideWndAni(LevelPrepareWnd,true,"w_black",fLevelPreparePosX,fLevelPreparePosY);
		fLevelPreparePosX = fLevelPreparePosY = 0;
		//ShowFullScreenUI(true);
	}
	
	public void ShowDetail()
	{
		if(detailPanel != null) WndAni.ShowWndAni(detailPanel,false,"bg_grey");
	}
	public void HideDetail()
	{
		if(detailPanel != null) WndAni.HideWndAni(detailPanel,false,"bg_grey");
	}
	
	public void ShowFightUi()
	{
		if (fightUi != null) fightUi.SetActive(true);
	}
	
	public void HideFightUi()
	{
		if (fightUi != null) fightUi.SetActive(false);
	}
	
    public bool bLockTown = false;

	public void ShowTownUi()
	{
		if (townUi != null) townUi.SetActive(true);
	}
	
	public void HideTownUi()
	{
		if (townUi != null) townUi.SetActive(false);
	}
	
	public void ShowDetailTab(int index)
	{
		switch(index)
		{
		case 0:
			{
				if (detailTabBasePanel != null) detailTabBasePanel.SetActive(true);
				if (detailTabAttPanel != null) detailTabAttPanel.SetActive(false);
				if (detailTabDefPanel != null) detailTabDefPanel.SetActive(false);
				if (detailTabOtherPanel != null) detailTabOtherPanel.SetActive(false);
			}
			break;
		case 1:
			{
				if (detailTabBasePanel != null) detailTabBasePanel.SetActive(false);
				if (detailTabAttPanel != null) detailTabAttPanel.SetActive(true);
				if (detailTabDefPanel != null) detailTabDefPanel.SetActive(false);
				if (detailTabOtherPanel != null) detailTabOtherPanel.SetActive(false);
			}
			break;
		case 2:
			{
				if (detailTabBasePanel != null) detailTabBasePanel.SetActive(false);
				if (detailTabAttPanel != null) detailTabAttPanel.SetActive(false);
				if (detailTabDefPanel != null) detailTabDefPanel.SetActive(true);
				if (detailTabOtherPanel != null) detailTabOtherPanel.SetActive(false);
			}
			break;
		case 3:
			{
				if (detailTabBasePanel != null) detailTabBasePanel.SetActive(false);
				if (detailTabAttPanel != null) detailTabAttPanel.SetActive(false);
				if (detailTabDefPanel != null) detailTabDefPanel.SetActive(false);
				if (detailTabOtherPanel != null) detailTabOtherPanel.SetActive(true);
			}
			break;
		}
	}
	
	public bool Init()
	{
		sdSlotMgr.Instance.Init();
		//sdUIPet.instance().Init();
	
		return true;
	}
	
	public bool isInit = false;

    //public int nType = 0; //
    void OnLoadWorldMap()
    {
        //switch (nType)
        //{
        //    case 1:
        //        sdUICharacter.Instance.ShowPVPMain(true, true);
        //        break;
        //}
    }
	void Update()
	{
		if (!isInit)
		{
			isInit = Init();
		}

		// goto worldmap
		if( iWorldmapJumping==1 && !sdUILoading.LoadingTex() )
		{
			iWorldmapJumping = 2;
			BundleGlobal.Instance.StartLoadBundleLevel("Level/guidemap/worldmap/$worldmap_0.unity.unity3d","$worldmap_0");
            OnLoadWorldMap();
		}

		if( msgLineWnd!=null && msgLineWnd.activeSelf )
		{
			if( msgLineWnd.GetComponent<UIPanel>().alpha == 0 )
				msgLineWnd.SetActive(false);
		}

		// 澶勭悊杩炴帴淇℃伅..
		if( reconnectWnd!=null && reconnectWnd.activeSelf )
		{
			if( SDNetGlobal.gameLoginState == 2 )
			{
				// 杩炴帴鎴愬姛..
				SDNetGlobal.gameLoginState = 0;
				HideReconnectWnd();
			}
			else if( SDNetGlobal.gameLoginState == 3 )
			{
				// 杩炴帴澶辫触锛岄渶瑕佽?闂?敤鎴锋槸鍚﹁?閲嶈瘯..
				SDNetGlobal.gameLoginState = 0;
				reconnectWnd.GetComponent<sdUIReconnect>().ShowBtn(true);
			}
			else if( SDNetGlobal.gameLoginState > 100 )
			{
				reconnectWnd.GetComponent<sdUIReconnect>().lbText.text = reconnectWnd.GetComponent<sdUIReconnect>().strConnectText+" "+(SDNetGlobal.gameLoginState-100)+"/10";
				SDNetGlobal.gameLoginState = 1;
			}
		}

		// 澶勭悊鎵嬫満鐨勮繑鍥炴寜閽?.
		if( Input.GetKeyDown(KeyCode.Escape) )
		{
			DoEscape();
		}
	}

	// 鎸夋墜鏈鸿繑鍥為敭鐨勫?鐞呿.
	void DoEscape()
	{
		GameObject obj;

		if( msgBoxWnd!=null && msgBoxWnd.activeSelf )
		{	// Message Box
			sdMsgBox wnd = msgBoxWnd.GetComponent<sdMsgBox>();
			if( wnd.isOkCancelMode )
				wnd.ClickCancel();
			else
				wnd.ClickOk();
		}
		else if( reconnectWnd!=null && reconnectWnd.activeSelf )
		{
			// 閲嶈繛鐘舵佷笅锛屼笉鍝嶅簲杩斿洖鎸夐敭...
		}
		else if( Application.loadedLevelName=="login" || Application.loadedLevelName=="$SelectRole" )
		{
			// 鐧诲綍鍜岄夎?鑹茬姸鎬?.
			sdMsgBox.OnConfirm quit = new sdMsgBox.OnConfirm(ComfirmQuit);
			ShowOkCanelMsg( sdConfDataMgr.Instance().GetShowStr("QuitGameConfirm"), quit,null);
		}
		else if( sdGuideMgr.Instance.isOperation )
		{
			// 寮哄埗寮曞?鐘舵佷笅灞忚斀杩斿洖閿?.
			return;
		}
		else if( configWnd!=null && configWnd.activeSelf )
		{	// Config Wnd
			HideConfigWnd();
		}
		else if( configWndF!=null && configWndF.activeSelf )
		{	// Config Wnd Fight
			HideConfigWnd();
		}
		else if( tipWnd!=null && tipWnd.activeSelf )
		{	// Tip Wnd
			HideTip();
		}
		else if( roleTipWnd!=null && roleTipWnd.activeSelf )
		{
			HideRoleTipWnd();
		}
		else if( IsbbsWndActive() )
		{
			// bbs wnd
			HidebbsWnd();
		}
		else if( itemSelectWnd!=null && itemSelectWnd.activeSelf )
		{
			// itemSelectWnd
			HideSelectWnd();
		}
		else if( roleWnd!=null && roleWnd.activeSelf )
		{	// Role Wnd
			if( detailPanel!=null && detailPanel.activeSelf )
				HideDetail();
			else
				HideRoleWnd();
		}
		else if( skillWnd!=null && skillWnd.activeSelf )
		{	// Skill Wnd
			HideSkillWnd();
		}
		else if( friendWnd!=null && friendWnd.activeSelf )
		{	// Friend Wnd
			Transform t = friendWnd.transform.FindChild("infopanel");
			if( t!=null && t.gameObject.activeSelf )
				t.gameObject.SetActive(false);
			else
				HideFriendWnd();
		}
		else if( sdMailControl.m_UIMailWnd!=null && sdMailControl.m_UIMailWnd.activeSelf )
		{	// Mail Wnd
			if( sdMailControl.m_UIMailDetailWnd!=null && sdMailControl.m_UIMailDetailWnd.activeSelf )
				WndAni.HideWndAni(sdMailControl.m_UIMailDetailWnd,false,"bg_grey");
				//sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailDetailWnd);
			else
				WndAni.HideWndAni(sdMailControl.m_UIMailWnd,true,"w_black");
				//sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailWnd);
		}
		else if( sdActGameControl.m_UILapBossWnd!=null && sdActGameControl.m_UILapBossWnd.activeSelf )
		{	// LapBoss Wnd
			sdActGameControl.Instance.CloseGameWnd(sdActGameControl.m_UILapBossWnd);
		}
		else if( AwardCenterWnd.Instance.m_goWndRoot!=null && AwardCenterWnd.Instance.m_goWndRoot.activeSelf )
		{	// AwardCenter Wnd
			AwardCenterWnd.Instance.CloseAwardCenterPanel();
		}
		else if( sdMallManager.Instance.m_magicTowerPanel!=null && sdMallManager.Instance.m_magicTowerPanel.activeSelf )
		{	// 瀹犵墿鎶藉崱.
			if( sdMallManager.Instance.OnePetObtainPanelOb!=null && sdMallManager.Instance.OnePetObtainPanelOb.activeSelf )
				sdMallManager.Instance.OnePetObtainPanelOb.SetActive(false);
			else if( sdMallManager.Instance.TenPetObtainPanelOb!=null && sdMallManager.Instance.TenPetObtainPanelOb.activeSelf )
				sdMallManager.Instance.TenPetObtainPanelOb.SetActive(false);
			else
				WndAni.HideWndAni(sdMallManager.Instance.m_magicTowerPanel,true,"w_black");
		}
		else if( sdMallManager.Instance.MallPanelOb!=null && sdMallManager.Instance.MallPanelOb.activeSelf )
		{	// Mall Wnd
			if( sdMallManager.Instance.GoodsTipPanelOb!=null && sdMallManager.Instance.GoodsTipPanelOb.activeSelf )
				sdMallManager.Instance.GoodsTipPanelOb.SetActive(false);
			else if( sdMallManager.Instance.BuyPetExPanelOb!=null && sdMallManager.Instance.BuyPetExPanelOb.activeSelf )
				sdMallManager.Instance.BuyPetExPanelOb.SetActive(false);
			else if( sdMallManager.Instance.VIPPanelOb!=null && sdMallManager.Instance.VIPPanelOb.activeSelf )
				sdMallManager.Instance.VIPPanelOb.SetActive(false);
			else
				WndAni.HideWndAni(sdMallManager.Instance.MallPanelOb,true,"w_black");
		}

		else if( sdUIPetControl.m_UIPetSkillTip!=null && sdUIPetControl.m_UIPetSkillTip.activeSelf )
		{	// Pet Skill Tip
			WndAni.HideWndAni(sdUIPetControl.m_UIPetSkillTip,false,"bg_grey");
		}
		else if( sdUIPetControl.m_UIPetSmallTip!=null && sdUIPetControl.m_UIPetSmallTip.activeSelf )
		{	// Pet Tip
			WndAni.HideWndAni(sdUIPetControl.m_UIPetSmallTip,false,"bg_grey");
		}
		else if( sdUIPetControl.m_UIPetPropPnl!=null && sdUIPetControl.m_UIPetPropPnl.activeSelf )
		{	// pet prop
			if( sdUIPetControl.m_UIPetLevelSelectPnl!=null && sdUIPetControl.m_UIPetLevelSelectPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("levelupSelectClose");
			else if( sdUIPetControl.m_UIPetLevelPnl!=null && sdUIPetControl.m_UIPetLevelPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("petLevelClose");
			else if( sdUIPetControl.m_UIPetStrongSelectPnl!=null && sdUIPetControl.m_UIPetStrongSelectPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("strongSelectClose");
			else if( sdUIPetControl.m_UIPetStrongPnl !=null && sdUIPetControl.m_UIPetStrongPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("petStrongClose");
			else
				sdUIPetBtnClick.DoCloseClick("petPropClose");
		}
		else if( sdUIPetControl.m_UIPetListPnl!=null && sdUIPetControl.m_UIPetListPnl.activeSelf )
		{ 	// Pet List
			sdUIPetBtnClick.DoCloseClick("petListClose");
		}
		else if( sdUIPetControl.m_UIPetWarPnl!=null && sdUIPetControl.m_UIPetWarPnl.activeSelf )
		{ 	// Pet War
			if( sdUIPetControl.m_UIPetWarSelectPnl!=null && sdUIPetControl.m_UIPetWarSelectPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("warSelectClose");
			else
				sdUIPetBtnClick.DoCloseClick("petWarClose");
		}
		else if( sdUIPetControl.m_UIPetRonghePnl!=null && sdUIPetControl.m_UIPetRonghePnl.activeSelf )
		{ 	// Pet Ronghe
			if( sdUIPetControl.m_UIPetRongheSelectPnl!=null && sdUIPetControl.m_UIPetRongheSelectPnl.activeSelf )
				sdUIPetBtnClick.DoCloseClick("rongheSelectClose");
			else
				sdUIPetBtnClick.DoCloseClick("rongheClose");
		}
		else if( sdUIPetControl.m_UIPetTujianPnl!=null && sdUIPetControl.m_UIPetTujianPnl.activeSelf )
		{ 	// Pet Tujian
			sdUIPetBtnClick.DoCloseClick("petTujianClose");
		}
		else if(EverydayAwardWnd.Instance.IsOpen())
		{
			EverydayAwardWnd.Instance.ClosePanel();
		}
		else if(EverydayFoodsWnd.Instance.IsOpen ())
		{
			EverydayFoodsWnd.Instance.ClosePanel();
		}
		else if(EverydayQuestWnd.Instance.IsOpen ())
		{
			EverydayQuestWnd.Instance.ClosePanel();
		}
		else if(LevelAwardWnd.Instance.IsOpen())
		{
			LevelAwardWnd.Instance.ClosePanel();
		}
		else if(ExchangeCodeWnd.Instance.IsOpen())
		{
			ExchangeCodeWnd.Instance.ClosePanel();
		}

		else if( CampaignWnd!=null && CampaignWnd.activeSelf )
		{	// Campaign Wnd
			obj = GameObject.Find("$LevelRewardWnd(Clone)");
			if( obj!=null && obj.activeSelf )
			{
				WndAni.HideWndAni(obj,false,"sp_grey");
			}
			else
			{
				if( IsInTown() )
				{
					HideTuitu(false);
				}
				else
				{
					// 鍦ㄥ叧鍗′腑...
					obj = GameObject.Find("Fx_lingjing01");
					if( obj != null ) obj.SetActive(false);
					obj = GameObject.Find("Fx_lingjing02");
					if( obj != null ) obj.SetActive(false);
					obj = GameObject.Find("Fx_lingjing03");
					if( obj != null ) obj.SetActive(false);
					
					TuiTu_To_WorldMap();
				}
			}
		}
		else if( LevelPrepareWnd!=null && LevelPrepareWnd.activeSelf )
		{	// LevelPrepare Wnd
			obj = GameObject.Find("$ChooseFriendPetWnd(Clone)");
			if( obj!=null && obj.activeSelf )
				WndAni.HideWndAni(obj,false,"sp_grey");
			else
			{
				if( IsInTown() )
				{
					LevelPrepareWnd.SetActive(false);
					ShowFullScreenUI(false);
				}
				else
				{
					TuiTu_To_WorldMap();
				}
			}
		}
		else if( itemUpWnd!=null && itemUpWnd.activeSelf )
		{	// itemUpWnd
			HideItemUpWnd();
		}
		else if( pvpMainWnd!=null && pvpMainWnd.activeSelf )
		{	// pvpWnd
			if( PVPTitleUpWnd!=null && PVPTitleUpWnd.activeSelf )
				ShowPVPTitleUpWnd(false);
			else if( PVPTitleWnd!=null && PVPTitleWnd.activeSelf )
				ShowPVPTitle(false);
			else if( PVPRefreshWnd!=null && PVPRefreshWnd.activeSelf )
				ShowPVPRefreshWnd(false);
			else if( PVPRewards!=null && PVPRewards.activeSelf )
				ShowPVPRewards(false);
			else
				ShowPVPMain(false);
		}
		else if( ranklistWnd!=null && ranklistWnd.activeSelf )
		{	// ranklist
			ShowRankListWnd(false);
		}
		else if( mysteryShopWnd!=null && mysteryShopWnd.activeSelf )
		{	// 绁炵?鍟嗗簵.
			ShowMysteryShop(false);
		}

		else if( GameObject.Find("$TownUi")!=null )
		{
			ShowConfigWnd(true);
		}
		else if( GameObject.Find("FightUi")!=null )
		{
			ShowConfigWnd(false);
		}
		else
		{
			sdMsgBox.OnConfirm quit = new sdMsgBox.OnConfirm(ComfirmQuit);
			ShowOkCanelMsg( sdConfDataMgr.Instance().GetShowStr("QuitGameConfirm"), quit,null);
		}
	}
	void ComfirmQuit()
	{
		Application.Quit();
	}
	
	public void ActiceRadioBtn(sdRadioButton btn)
	{
		if (btn == null) return;
		GameObject level = GameObject.Find("NGUIRoot");
		sdRadioButton[] list = level.GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton item in list)
		{
			if (item.index == btn.index)
			{
				if (item != btn)
				{
					item.Active(false);
				}
			}
		}
	}
	
	public void SetShortCutCd(int id, int cd, bool isShow)
	{
		GameObject root = GameObject.Find("NGUIRoot");
		if (root != null)
		{
			sdShortCutIcon[] list = root.GetComponentsInChildren<sdShortCutIcon>();
			foreach(sdShortCutIcon item in list)
			{
				if (item.id == (UInt64)id && (item.type == ShortCutType.Type_Skill || item.type == ShortCutType.Type_PetSkill))
				{
					item.SetCoolDown(cd/1000, isShow);	
				}
			}
		}
	}
	
	public void ShowShortCutCd(int id)
	{
		GameObject root = GameObject.Find("NGUIRoot");
		if (root != null)
		{
			sdShortCutIcon[] list = root.GetComponentsInChildren<sdShortCutIcon>();
			foreach(sdShortCutIcon item in list)
			{
				if (item.id == (UInt64)id && item.type == ShortCutType.Type_Skill)
				{
					item.ShowCd();	
				}
			}
		}	
	}
	
	public void SetSkillCurStage(int cur, int max)
	{
		GameObject ui = GameObject.Find("FightUi");
		if (ui != null)
		{
			sdFightUi fight = ui.GetComponent<sdFightUi>();
			if (max <= 1 || max >= 4)
			{
				fight.comboPanel.SetActive(false);
				return;
			}
			fight.comboPanel.GetComponent<sdComboPanel>().SetIcon(cur, max);
			fight.comboPanel.SetActive(true);
		}
	}
	
	public void AddReliveNum()
	{
		reliveNum++;	
	}
	
	public int ReliveNum()
	{
		return reliveNum;	
	}
	
	public void ClearReliveNum()
	{
		reliveNum = 0;	
	}
	
	public void SetFreeReliveNum(int nNum)
	{
		freeReliveNum = nNum;	
	}
	
	public int FreeReliveNum()
	{
		return freeReliveNum;	
	}
	
	public void SetRelivePrice(int price)
	{
		relivePrice = price;	
	}
	
	public int RelivePrice()
	{
		return relivePrice;	
	}
	
	public void AddMedicineNum()
	{
		medicineNum++;	
		/*
		if (medicineNum >= freeMedicineNum)
		{
			GameObject ui = GameObject.Find("FightUi");
			if (ui != null)
			{
				sdFightUi fight = ui.GetComponent<sdFightUi>();
				if (fight != null)
				{
					fight.ShowMedicinePrice(medicinePrice);	
				}
			}
		}
		else
		*/
		{
			GameObject ui = GameObject.Find("FightUi");
			if (ui != null)
			{
				sdFightUi fight = ui.GetComponent<sdFightUi>();
				if (fight != null)
				{
					//fight.HideMedicinePrice();	
				}
			}
		}
	}
	
	public int MedicineNum()
	{
		return medicineNum;	
	}
	
	public void ClearMedicineNum()
	{
		medicineNum = 0;	
	}
	
	public void SetFreeMedicineNum(int nNum)
	{
		freeMedicineNum = nNum;	
		medicineNum = 0;
	}
	
	public int FreeMedicineNum()
	{
		return freeMedicineNum;	
	}
	
	public void SetMedicinePrice(int price)
	{
		medicinePrice = price;	
	}
	
	public int MedicinePrice()
	{
		return medicinePrice;	
	}
	
	string showSkillId = "";
	
	public void SetShowSkill(string id)
	{
		showSkillId = id;
	}
	
	public string curSkillId = "";
	
	public void ShowSkillInfo(string id)
	{
		curSkillId = id;
		sdSkillWnd wnd = skillWnd.GetComponent<sdSkillWnd>();
		if (wnd != null)
		{
			wnd.ShowSkillInfo(id);
		}
	}
	
	GameObject msgBoxWnd = null;
	public void ShowOkMsg(string msg, sdMsgBox.OnConfirm confirmMsg) { ShowOkMsg(msg,confirmMsg,"qd"); }
	public void ShowOkMsg(string msg, sdMsgBox.OnConfirm confirmMsg, string confirmName)
	{
		if (msgBoxWnd == null) 
		{
			msgBoxWnd = GameObject.Instantiate( Resources.Load("msgbox/msgBoxPrefab") ) as GameObject;
			msgBoxWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
			msgBoxWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			msgBoxWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -600.0f);
		}
		
		msgBoxWnd.SetActive(true);
		sdMsgBox obj = msgBoxWnd.GetComponent<sdMsgBox>();
		obj.okBtn.transform.FindChild("sp_btntext").GetComponent<UISprite>().spriteName = confirmName;
		obj.ShowOkMsg(msg, confirmMsg);
	}

	public void ShowOkCanelMsg(string msg, sdMsgBox.OnConfirm confirmMsg, sdMsgBox.OnCancel cancelMsg) { ShowOkCanelMsg(msg,confirmMsg,cancelMsg,"qd","qx"); }
	public void ShowOkCanelMsg(string msg, sdMsgBox.OnConfirm confirmMsg, sdMsgBox.OnCancel cancelMsg, string confirmName, string cancelName)
	{
		if (msgBoxWnd == null) 
		{
			msgBoxWnd = GameObject.Instantiate( Resources.Load("msgbox/msgBoxPrefab") ) as GameObject;
			msgBoxWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
			msgBoxWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			msgBoxWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -600.0f);
		}

		msgBoxWnd.SetActive(true);
		sdMsgBox obj = msgBoxWnd.GetComponent<sdMsgBox>();
		obj.okBtn.transform.FindChild("sp_btntext").GetComponent<UISprite>().spriteName = confirmName;
		obj.cancelBtn.transform.FindChild("sp_btntext").GetComponent<UISprite>().spriteName = cancelName;
		obj.ShowOkCancelMsg(msg, confirmMsg, cancelMsg);
	}
	
	public void MsgClickOK()
	{
		if (msgBoxWnd == null) return;
		msgBoxWnd.GetComponent<sdMsgBox>().ClickOk();
	}
	
	public void MsgClickCancel()
	{
		if (msgBoxWnd == null) return;
		msgBoxWnd.GetComponent<sdMsgBox>().ClickCancel();
	}

	// msgLine
	GameObject msgLineWnd = null;
	public void ShowMsgLine(string msg, Color msgColor)
	{
		if (msg == null || msg == "") return;

		if( msgLineWnd == null ) 
		{
			msgLineWnd = GameObject.Instantiate( Resources.Load("msgbox/msgLinePrefab") ) as GameObject;
			msgLineWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
			msgLineWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			msgLineWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -600.0f);
		}

		msgLineWnd.SetActive(true);
		UILabel lb = msgLineWnd.GetComponentInChildren<UILabel>();
		lb.text = msg;
		lb.color = msgColor;
		UISprite sp = msgLineWnd.GetComponentInChildren<UISprite>();
		sp.width = lb.width + 30;
		msgLineWnd.GetComponent<TweenAlpha>().Reset();
		msgLineWnd.GetComponent<TweenAlpha>().PlayForward();
	}
	
	public void ShowMsgLine(string msg, MSGCOLOR msgColor)
	{
		Color c = new Color(1f,1f,1f);
		if( msgColor == MSGCOLOR.White )
			c = new Color(1f,1f,1f);
		else if( msgColor == MSGCOLOR.Red )
			c = new Color(1f,0f,0f);
		else if( msgColor == MSGCOLOR.Green )
			c = new Color(0f,1f,0f);
		else if( msgColor == MSGCOLOR.Blue )
			c = new Color(0f,0f,1f);
		else if( msgColor == MSGCOLOR.Yellow )
			c = new Color(1f,1f,0f);
		else if( msgColor == MSGCOLOR.Grey )
			c = new Color(0.7f,0.7f,0.7f);
		ShowMsgLine(msg,c);
	}
	public void ShowMsgLine(string msg)	{ ShowMsgLine(msg, MSGCOLOR.White); }
    public void HideMsgLine()
    {
        if (msgLineWnd != null)
        {
            msgLineWnd.SetActive(false);
        }
    }

	// ReconnectWnd
	GameObject reconnectWnd = null;
	public void ShowReconnectWnd()
	{
		if( reconnectWnd == null ) 
		{
			reconnectWnd = GameObject.Instantiate( Resources.Load("msgbox/reconnectPrefab") ) as GameObject;
			reconnectWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
			reconnectWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			reconnectWnd.transform.localPosition = new Vector3(0.0f, 0.0f, -600.0f);
		}
		
		reconnectWnd.SetActive(true);
		reconnectWnd.GetComponent<sdUIReconnect>().ShowBtn(false);
	}
	public void HideReconnectWnd()
	{
		if( reconnectWnd == null ) return;
		reconnectWnd.SetActive(false);
	}

	// ConfigWnd
	GameObject configWnd = null;
	GameObject configWndF = null;
	bool isLoadConfigWnd = false;
	public void ShowConfigWnd(bool bTown)
	{
		if( bTown && configWnd==null )
		{
			if(isLoadConfigWnd) return;
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = bTown;
			param.info = "configWnd";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$ConfigWnd.prefab",LoadWnd,param);
			isLoadConfigWnd = true;
		}
		else if( !bTown && configWndF==null ) 
		{
			if(isLoadConfigWnd) return;
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = bTown;
			param.info = "configWndF";
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$ConfigWndF.prefab",LoadWnd,param);
			isLoadConfigWnd = true;
		}
		else if( bTown )
		{
			ConfigWndBtn.mTown = bTown;
			WndAni.ShowWndAni(configWnd,false,"bg_grey");
            BubbleManager.Instance.OnOpenWnd();
		}
		else
		{
			ConfigWndBtn.mTown = bTown;
			WndAni.ShowWndAni(configWndF,false,"bg_grey");
			if( sdGameLevel.instance.levelType != sdGameLevel.LevelType.PVP )
				Time.timeScale = 0.0f;
			BubbleManager.Instance.OnOpenWnd();
		}
	}
	public void HideConfigWnd() { HideConfigWnd(false); }
	public void HideConfigWnd(bool bImm)
	{
		if( configWndF!=null && configWndF.activeSelf )
		{
			if( bImm )
				configWndF.SetActive(false);
			else 
				WndAni.HideWndAni(configWndF,false,"bg_grey");
			BubbleManager.Instance.OnCloseWnd();
		}

		if( configWnd!=null && configWnd.activeSelf )
		{
			if( bImm )
				configWnd.SetActive(false);
			else 
				WndAni.HideWndAni(configWnd,false,"bg_grey");
            BubbleManager.Instance.OnCloseWnd();
		}

        Time.timeScale = 1.0f;
	}

    public void ShowPTWnd(bool bShow)
    {
        if (bShow)
        {
            if (ptWnd == null)
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "ptwnd";
                sdResourceMgr.Instance.LoadResource("UI/ActGame/$PTWnd.prefab", LoadWnd, param);
            }
            else
            {
                sdPTWnd wnd = ptWnd.GetComponent<sdPTWnd>();
                if (wnd)
                    wnd.Refresh();
                WndAni.ShowWndAni(ptWnd, true, "w_black");
            }
        }
        else
        {
            WndAni.HideWndAni(ptWnd, true, "w_black");
        }
    }

	public void BuffChange(List<sdBuff> buffList)
	{
		GameObject ui = GameObject.Find("FightUi");
		if (ui != null)
		{
			sdFightUi fight = ui.GetComponent<sdFightUi>();
			if (fight != null)
			{
				fight.BuffChange(buffList);	
			}
		}
	}
	

		
	private bool isLoadJiesuan = false;
	public void LoadJiesuanWnd()
	{
		if (jiesuanWnd == null) 
		{
			if (isLoadJiesuan) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "jiesuanWnd";
			param.userdata0 = 0;
			sdResourceMgr.Instance.LoadResource("UI/$result/jiesuanWnd.prefab",LoadWnd, param);
			isLoadJiesuan = true;
		}
	}

	public GameObject GetJiesuanWnd()
	{
		return jiesuanWnd;
	}

    public void ShowScrollTextWnd(bool bShow)
    {
        if (bShow)
        {
            if (scrolTextWnd == null)
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "scrolltextwnd";
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$bbs/scrolltextWnd.prefab", LoadWnd, param);
            }
            else
                scrolTextWnd.SetActive(true);
        }
        else
        {
            scrolTextWnd.SetActive(false);
        }
    }

	public void ShowJiesuanWnd()
	{
		if (jiesuanWnd == null) 
		{
			if (isLoadJiesuan) return;
			ResLoadParams param = new ResLoadParams();
			param.info = "jiesuanWnd";
			param.userdata0 = 1;
			sdResourceMgr.Instance.LoadResource("UI/$result/jiesuanWnd.prefab",LoadWnd, param);
			isLoadJiesuan = true;
		}
		else
		{
			jiesuanWnd.GetComponent<sdJiesuanWnd>().SetInfo(jiesuanMsg);
			jiesuanWnd.SetActive(true);
			sdSlotMgr.Instance.Init();
		}
	}
	
	public void SetJiesuanMsg(CliProto.SC_TREASURE_CHEST_NTF msg)
	{
		jiesuanMsg = msg;
	}
	
	public bool JiesuanCanGoNext(int step)
	{
		if (jiesuanWnd != null)
		{
			return jiesuanWnd.GetComponent<sdJiesuanWnd>().CanGoNext(step);	
		}
		
		return false;
	}

    public void JiesuanGoNext(int step)
    {
        if (jiesuanWnd != null)
        {
            jiesuanWnd.GetComponent<sdJiesuanWnd>().GoNext(step);
        }
    }
	
	public void JiesuanGetAllItem()
	{
		if (jiesuanWnd != null)
		{
			jiesuanWnd.GetComponent<sdJiesuanWnd>().GetAllItem();	
		}
		
	}
	
	public TreasureInfo GetTreasure()
	{
		if (jiesuanWnd != null)
		{
			return jiesuanWnd.GetComponent<sdJiesuanWnd>().GetTreasure();
		}
		
		return null;
	}
	
	public void RemoveTreasureInfo(int index)
	{
		if (jiesuanWnd != null)
		{
			jiesuanWnd.GetComponent<sdJiesuanWnd>().RemoveTreasure(index);
		}
	}

    sdSweepWnd sweepWnd = null;
    bool isLoadSweep = false;
    public void ShowSweepWnd(bool bIsResult)
    {
        if (sweepWnd == null)
        {
            if (isLoadSweep) return;
            ResLoadParams param = new ResLoadParams();
            param.info = "sweepWnd";
            param.userdata0 = bIsResult;
            sdResourceMgr.Instance.LoadResource("UI/$UIPrefab/$SweepWnd.prefab", LoadWnd, param);
            isLoadSweep = true;
        }
        else
        {            
            if (bIsResult)
            {
                sweepWnd.SetInfo(sweepInfoList);
                sweepWnd.ShowResult();
            }
            else
            {
                sweepWnd.ShowPrepare();
            }
        }
    }

    public void AddSweepInfo(CliProto.SC_TREASURE_CHEST_NTF info)
    {
        sweepInfoList.Add(info);
    }

    public void ClearSweepInfo()
    {
        sweepInfoList.Clear();
    }
	
	private int iWorldmapJumping = 0;
	public void TuiTu_To_WorldMap()
	{
        HideSuccessPanel();
		if( iWorldmapJumping == 0 )
		{
			iWorldmapJumping = 1;
			sdUILoading.ActiveLoadingUI(0);
		}
	}
	
	public void JumpOver()
	{
		iWorldmapJumping = 0;
	}
	
	public void AddLootItem(int id)
	{
		//GameObject ui = GameObject.Find("NGUIRoot");	
        if (lootPanel == null) return;
        lootPanel.AddItem(LootType.Item, id);	
		
	}
	
	public void AddLootMoney(int money)
	{
        if (lootPanel == null) return;
        lootPanel.AddItem(LootType.Money, money);
	}
	
	public string GetChatInfo()
	{
		townUi = GameObject.Find("$TownUi");
		if (townUi != null)
		{
			return townUi.GetComponent<sdTown>().GetChatInfo();
		}
		
		return "";
	}
	
	GameObject showLevel = null;
	
	public void SetNeedShowLevel(GameObject obj)
	{
		showLevel = obj;
	}
	
	public GameObject GetNeedShowLevel()
	{
		return showLevel;	
	}
	
	// 鏄剧ず鍏ㄩ儴UI鐣岄潰鏃跺叧闂?満鏅?拰涓籙I鏄剧ず.
	private GameObject mainScene;
	private GameObject mainUI;
	private Camera mainCamera;
	private Color mainCameraColor;
	public void ShowFullScreenUI(bool bShow) { ShowFullScreenUI(bShow,null); }
	public void ShowFullScreenUI(bool bShow, GameObject objWnd)
	{
		if( bShow )
		{
			if( Application.loadedLevelName == "$worldmap_0" )
			{
				if( mainScene == null)
					mainScene = GameObject.Find("@Scene");
				if( mainScene != null ) 
					mainScene.SetActive(false);
				if( mainCamera == null)
				{
					GameObject obj = GameObject.Find("@MainCamera");
					if( obj != null )
						mainCamera = obj.GetComponent<Camera>();
				}
				if( mainCamera != null )
				{
					mainCameraColor = mainCamera.backgroundColor;
					mainCamera.backgroundColor = new Color(0,0,0,1.0f);
				}
			}

			if( mainUI == null)
				mainUI = GameObject.Find("$TownUi");
			if( mainUI != null ) 
				mainUI.SetActive(false);
		}
		else
		{
			if( objWnd != null )
			{
				if( LevelPrepareWnd!=null && objWnd!=LevelPrepareWnd && LevelPrepareWnd.activeSelf ) return;
				else if( CampaignWnd!=null && objWnd!=CampaignWnd && CampaignWnd.activeSelf ) return;
				else if( roleWnd!=null && objWnd!=roleWnd && roleWnd.activeSelf ) return;
				else if( roleTipWnd!=null && objWnd!=roleTipWnd && roleTipWnd.activeSelf ) return;
				else if( tipWnd!=null && objWnd!=tipWnd && tipWnd.activeSelf ) return;
			}

			if( mainScene != null ) 
				mainScene.SetActive(true);
			if( mainUI != null ) 
				mainUI.SetActive(true);
			if( mainCamera != null)
				mainCamera.backgroundColor = mainCameraColor;
		}
	}
	
	// 涓栫晫鍦板浘鍜屼富鍩庝箣闂村垏鎹?紝淇濈暀閮ㄥ垎UI..
	bool bDontDestoryUI = false;
	public void DontDestoryUI()
	{
		/*
		if( roleWnd )
		{
			roleWnd.transform.parent = null;
			DontDestroyOnLoad( roleWnd );
		}
		
		if( skillWnd )
		{
			skillWnd.transform.parent = null;
			DontDestroyOnLoad( skillWnd );
		}
		
		if( LevelPrepareWnd )
		{
			LevelPrepareWnd.transform.parent = null;
			DontDestroyOnLoad( LevelPrepareWnd );
		}
		*/
		bDontDestoryUI = true;
	}
	
	public void RestoreUI()
	{
		bDontDestoryUI = false;
		
		/*
		GameObject obj = sdGameLevel.instance.UICamera.gameObject;
		if( roleWnd )
		{
			roleWnd.transform.parent = obj.transform;
		}
		
		if( skillWnd )
		{
			skillWnd.transform.parent = obj.transform;
		}
		
		if( LevelPrepareWnd )
		{
			LevelPrepareWnd.transform.parent = obj.transform;
		}
		*/
	}
	
	void OnLevelWasLoaded(int level)
	{
		if( bDontDestoryUI )
			RestoreUI();
	}
	
	public void ShowAddFriWnd()
	{
		if (jiesuanWnd != null)
		{
			jiesuanWnd.GetComponent<sdJiesuanWnd>().ShowAddFriWnd();	
		}
	}
	
	public void ShowFightChangeEffect()
	{
		if (fightUi != null)
		{
			fightUi.GetComponent<sdFightUi>().ShowChangeEffect();
		}
	}
	
	public void SetLevelChooseFriPetWnd(GameObject obj)
	{
		LevelChooseFriPetWnd = obj;	
	}
	
	public void SelectFriPet(sdFriend fightFri)
	{
		LevelChooseFriPetWnd.SetActive(false);
		sdFriendMgr.Instance.fightFriend = fightFri;
		ChoosePetButton[] btnList = LevelPrepareWnd.GetComponentsInChildren<ChoosePetButton>();
		foreach(ChoosePetButton btn in btnList)
		{
			if (btn.m_PetIdx == 100)
			{
				btn.SetInfo(fightFri.petInfo);	
			}
		}
	}

	private byte battleType = (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_NORMAL;
	public void SetBattleType(byte bySet)
	{
		battleType = bySet;
	}

	public byte GetBattleType()
	{
		return battleType;
	}
	public static void JumpToMainCity()
	{
        if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.MainCity)
        {
            SwitchScene(2, -1);
        }
	}
	public static void JumpToWorldMap()
	{
        if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.WorldMap)
        {
            SwitchScene(1, -1);
        }
	}
	public static void SwitchScene(int id,int sceneInstanceID)
	{
        SDGlobal.PrintCallStack();
		sdUILoading.ActiveLoadingUI(id-1);
		CliProto.GSPKG_SWITCHSCENE_REQ req = new CliProto.GSPKG_SWITCHSCENE_REQ();
		req.m_SceneTemplateID	=	(uint)id;
		req.m_SceneID			=	(uint)sceneInstanceID;
		SDNetGlobal.SendMessage(req);
	}
	public static bool LoadScene()
	{
		uint SceneId		=	(uint)sdGlobalDatabase.Instance.globalData["SceneId"];
		uint SceneType		=	(uint)sdGlobalDatabase.Instance.globalData["SceneType"];
        //Debug.Log("SceneId=" + SceneId + "SceneType=" + SceneType);

		switch(SceneType)
		{
			case 1:{
                sdUICharacter.Instance.iCurrentLevelID = 1000;
				sdGameLevel.notifyLevelStart += new sdGameLevel.NotifyLevelAwake(onLoadEnd);
				BundleGlobal.Instance.StartLoadBundleLevel("Level/guidemap/worldmap/$worldmap_0.unity.unity3d","$worldmap_0");
				return true;
			}break;
			case 2:{
				
				sdGameLevel.notifyLevelStart += new sdGameLevel.NotifyLevelAwake(onLoadEnd);
				BundleGlobal.Instance.StartLoadBundleLevel("Level/mainCity/mainCity_1/$mainCity_1.unity.unity3d","$mainCity_1");
				return true;
			}break;
			case 3:{
			}break;
			case 4:{
			
			}break;
			case 5:{
				sdUICharacter.Instance.iCurrentLevelID = 1;
				sdGameLevel.notifyLevelStart += new sdGameLevel.NotifyLevelAwake(onLoadEnd);
				BundleGlobal.Instance.StartLoadBundleLevel("Level/guidemap/guidemap_1/$guidemap_1.unity.unity3d","$guidemap_1");
			}break;
		}
		return false;
	}
	public static void onLoadEnd()
	{
		CliProto.CS_ENTERSCENE refMSG = new CliProto.CS_ENTERSCENE();
		refMSG.m_SceneId = (uint)sdGlobalDatabase.Instance.globalData["SceneId"];
		refMSG.m_Error = 0;
		//refMSG.szRoleName = System.Text.Encoding.Default.GetBytes(SDNetGlobal.playerList[selRole.currentSelect].name);
		SDNetGlobal.SendMessage(refMSG);
		SDGlobal.Log("-> CS_ENTERSCENE");
	}

	sdLoginMsg loginMsg = null;

	public void SetLoginMsg(sdLoginMsg msg)
	{
		loginMsg = msg;
	}

	public void ShowLoginMsg(string msg)
	{
        Debug.Log(msg);
		loginMsg.txt.text = msg;
		loginMsg.gameObject.SetActive(true);
	}
    public bool IsLoginMsgShow()
    {
        if (loginMsg == null)
        {
            return false;
        }
        return loginMsg.gameObject.active;
    }

	public void HideLoginMsg()
	{
		loginMsg.gameObject.SetActive(false);
	}

	public bool AddSelectItem(string id)
	{
		if (itemSelectWnd != null)
		{
			return itemSelectWnd.GetComponent<sdItemSelectWnd>().AddSelectItem(id);
		}

		return false;
	}

	public void RemoveSelectItem(string id)
	{
		if (itemSelectWnd != null)
		{
			itemSelectWnd.GetComponent<sdItemSelectWnd>().RemoveSelectItem(id);
		}
	}

	public Dictionary<string, int> GetSelectList()
	{
		if (itemSelectWnd != null)
		{
			return itemSelectWnd.GetComponent<sdItemSelectWnd>().GetList();
		}

		return null;
	}

    public List<sdGameItem> GetSelectWndAllItem()
    {
        if (itemSelectWnd != null)
        {
            return itemSelectWnd.GetComponent<sdItemSelectWnd>().itemTable;
        }

        return null;
    }

    int selectWndPos = -1;
    public void SetSelectWndNeedPos(int pos)
    {
        selectWndPos = pos;
        if (itemSelectWnd != null)
        {
            itemSelectWnd.GetComponent<sdItemSelectWnd>().SetEquipPos(pos);
        }
    }

	public bool IsInTown()
	{
		return ( Application.loadedLevelName=="$worldmap_0" || Application.loadedLevelName=="$mainCity_1" );
	}

    public void OnChangeEquip()
    {
        Dictionary<string, int> list = sdUICharacter.Instance.GetSelectList();
        foreach (KeyValuePair<string, int> info in list)
        {
            sdItemMsg.notifyEquipItem(info.Key, 1);
        }
    }

    public void UpdateBagNum()
    {
        Hashtable table = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.All);
        Hashtable info = new Hashtable();
        info["value"] = string.Format("{0}/{1}", table.Count,sdGameItemMgr.Instance.maxBagNum);
        SetProperty("BagNum", info);
    }

    public void OnChangeUpItem()
    {
        Dictionary<string, int> list = sdUICharacter.Instance.GetSelectList();
        foreach (KeyValuePair<string, int> info in list)
        {
            sdGameItemMgr.Instance.upItem = sdGameItemMgr.Instance.getItem(ulong.Parse(info.Key));
        }
        sdUICharacter.Instance.GetItemUpWnd().RefreshItemUpPanel();
        sdUICharacter.Instance.GetItemUpWnd().RefreshGemSetPanel();
    }

    public void RefreshLoginUI()
    {
        GameObject goUIRoot = GameObject.Find("UI Root (2D)");
        UILabel lb_servername = goUIRoot.transform.FindChild("Camera").FindChild("Anchor").FindChild("bt_Server").FindChild("Background").FindChild("Label").GetComponent<UILabel>();
        lb_servername.text = SDNetGlobal.serverName;
    }

    public void LoadAtlas(string strFileName, UISprite sprite, string spriteName)
    {
        ResLoadParams param = new ResLoadParams();
        param.userdata0 = sprite;
        param.userdata1 = spriteName;
        sdResourceMgr.Instance.LoadResource(strFileName, OnLoadAtlas, param, typeof(UIAtlas));
    }

    void OnLoadAtlas(ResLoadParams param, UnityEngine.Object obj)
    {
        UIAtlas atlas = obj as UIAtlas;
        ((UISprite)param.userdata0).atlas = atlas;
        ((UISprite)param.userdata0).spriteName = param.userdata1.ToString();
    }

    public void ShowSelectSrvWnd(bool bShow)
    {
        if (bShow)
        {
            if (SelectSrvWnd != null)
            {
                SelectSrvWnd.SetActive(true);
                WndAni.ShowWndAni(SelectSrvWnd, false, "w_grey");
                SelectSrvWnd wnd = SelectSrvWnd.GetComponent<SelectSrvWnd>();
                if(wnd != null)
                    wnd.UpdateSrvList();
            }
            else
            {
                ResLoadParams param = new ResLoadParams();
                param.info = "SelectServerWnd";
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$SelectServerWnd.prefab", LoadWnd, param);
            }
        }
        else
        {
            if (SelectSrvWnd != null)
                WndAni.HideWndAni(SelectSrvWnd, false, "w_grey");
        }
    }
}
