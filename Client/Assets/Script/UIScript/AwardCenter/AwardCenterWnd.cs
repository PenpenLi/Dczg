using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AwardCenterWnd : Singleton<AwardCenterWnd>
{
	public GameObject 	m_goWndRoot	= null;
	public bool m_dirt = false;

	GameObject _goCoin = null;
	GameObject _goMedal = null;

	UIButton _closeBtn = null;
	UIButton _everydayAwardBtn = null;
	UIButton _everydayQuestBtn = null;
	UIButton _everydayFoodsBtn = null;
	UIButton _levelAwardBtn = null;
	UIButton _exchangeCodeBtn = null;

	UIButton[] _tabBtns = new UIButton[3];
	TabBase[] _tabs = new TabBase[3];
	bool _bWndOpen = false;
	
	void Start () 
	{


	}

	void Update () 
	{
		m_dirt = false;
		if(	EverydayAwardWnd.Instance.m_dirt || 
		   EverydayFoodsWnd.Instance.m_dirt ||
		   EverydayQuestWnd.Instance.m_dirt ||
		   LevelAwardWnd.Instance.m_dirt ||
		   ExchangeCodeWnd.Instance.m_dirt )	
		{
			m_dirt = true;
		}

		if(sdUICharacter.Instance.GetTownUI() != null)
		{
			sdUICharacter.Instance.GetTownUI().transform.FindChild
				("titlebg1/btn_award_center/Fx_Tishi2Prefab").gameObject.SetActive(m_dirt);
		}

		if(m_goWndRoot == null)
			return;

		_everydayAwardBtn.gameObject.transform.Find("Fx_Tishi2Prefab").gameObject.SetActive(EverydayAwardWnd.Instance.m_dirt);
		_everydayFoodsBtn.gameObject.transform.Find("Fx_Tishi2Prefab").gameObject.SetActive(EverydayFoodsWnd.Instance.m_dirt);
		_everydayQuestBtn.gameObject.transform.Find("Fx_Tishi2Prefab").gameObject.SetActive(EverydayQuestWnd.Instance.m_dirt);
		_levelAwardBtn.gameObject.transform.Find("Fx_Tishi2Prefab").gameObject.SetActive(LevelAwardWnd.Instance.m_dirt);
		_exchangeCodeBtn.gameObject.transform.Find("Fx_Tishi2Prefab").gameObject.SetActive(ExchangeCodeWnd.Instance.m_dirt);

	}
	
	void OnDestory()
	{
	
	}
	
	public void Init()
	{

	}
	
	void OnCloseBtn(GameObject go)
	{
		CloseAwardCenterPanel();
		_bWndOpen = false;
	}
	
	public void OnShowWndAniFinish()
	{
		if (sdUICharacter.Instance.GetTownUI()!=null)
		{
			sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
		}
	}

	public void OpenPanel()
	{
		if (sdUICharacter.Instance.GetTownUI()!=null)
		{
			sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
		}

		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			m_goWndRoot.SetActive(true);
			//WndAni.ShowWndAni(m_goWndRoot,false,"bg_grey");
			string[] btns = {"AwardFrame/btn_EverydayQuest","AwardFrame/btn_EverydayAward","AwardFrame/btn_LevelAward","AwardFrame/btn_EverydayFoods","AwardFrame/btn_ExchangeCode"};
			WndAni.ShowWndAni2(m_goWndRoot,btns);
		}
		else
		{

			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "AwardCenterPanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$AwardCenterWnd.prefab", 
		    	                                OnAwardCenterPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}

	void OnEverydayAwardBtn(GameObject go)
	{
		EverydayAwardWnd.Instance.OpenPanel();
	}

	void OnEverydayQuestBtn(GameObject go)
	{
		EverydayQuestWnd.Instance.OpenPanel();
	}

	void OnLevelAwardBtn(GameObject go)
	{
		LevelAwardWnd.Instance.OpenPanel();
	}

	void OnEverydayFoodsBtn(GameObject go)
	{
		EverydayFoodsWnd.Instance.OpenPanel();
	}

	void OnExchangeCodeBtn(GameObject go)
	{
		ExchangeCodeWnd.Instance.OpenPanel();
	}	


	public void OnAwardCenterPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "AwardCenterPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);

		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/btn_Close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		
		UIButton closeBtnUp = m_goWndRoot.transform.FindChild("AwardFrame/btn_Close_Up").GetComponent<UIButton>();
		UIEventListener.Get(closeBtnUp.gameObject).onClick = OnCloseBtn;

		UIButton closeBtnDown = m_goWndRoot.transform.FindChild("AwardFrame/btn_Close_Down").GetComponent<UIButton>();
		UIEventListener.Get(closeBtnDown.gameObject).onClick = OnCloseBtn;

		_everydayQuestBtn = m_goWndRoot.transform.FindChild("AwardFrame/btn_EverydayQuest").GetComponent<UIButton>();
		UIEventListener.Get(_everydayQuestBtn.gameObject).onClick = OnEverydayQuestBtn;

		_everydayAwardBtn = m_goWndRoot.transform.FindChild("AwardFrame/btn_EverydayAward").GetComponent<UIButton>();
		UIEventListener.Get(_everydayAwardBtn.gameObject).onClick = OnEverydayAwardBtn;

		_levelAwardBtn = m_goWndRoot.transform.FindChild("AwardFrame/btn_LevelAward").GetComponent<UIButton>();
		UIEventListener.Get(_levelAwardBtn.gameObject).onClick = OnLevelAwardBtn;

		_everydayFoodsBtn = m_goWndRoot.transform.FindChild("AwardFrame/btn_EverydayFoods").GetComponent<UIButton>();
		UIEventListener.Get(_everydayFoodsBtn.gameObject).onClick = OnEverydayFoodsBtn;

		_exchangeCodeBtn = m_goWndRoot.transform.FindChild("AwardFrame/btn_ExchangeCode").GetComponent<UIButton>();
		UIEventListener.Get(_exchangeCodeBtn.gameObject).onClick = OnExchangeCodeBtn;

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();

		/*
		// Get the panel controller
		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/topbar/btn_close").GetComponent<UIButton>();
		_tabBtns[0] = m_goWndRoot.transform.FindChild("AwardFrame/frame/btn_tab1").GetComponent<UIButton>();
		_tabBtns[1] = m_goWndRoot.transform.FindChild("AwardFrame/frame/btn_tab2").GetComponent<UIButton>();
		_tabBtns[2] = m_goWndRoot.transform.FindChild("AwardFrame/frame/btn_tab3").GetComponent<UIButton>();

		// Added event listener
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		UIEventListener.Get(_tabBtns[0].gameObject).onClick = OnTabBtn;
		UIEventListener.Get(_tabBtns[1].gameObject).onClick = OnTabBtn;
		UIEventListener.Get(_tabBtns[2].gameObject).onClick = OnTabBtn;

		_tabs[0] = new DailyQuestTab(m_goWndRoot.transform.FindChild("EverydayQuest").gameObject);
		_tabs[1] = new DailyAwardTab(m_goWndRoot.transform.FindChild("EverydayAward").gameObject);
		_tabs[2] = new UpgradeAwardTab(m_goWndRoot.transform.FindChild("LevelAward").gameObject);

		_goCoin = m_goWndRoot.transform.FindChild("coin").gameObject;
		_goMedal = m_goWndRoot.transform.FindChild("medal").gameObject;

		_goCoin.GetComponent<UILabel>().text = ((Hashtable)sdGlobalDatabase.Instance.globalData
		                                        ["MainCharBaseProp"])["NonMoney"].ToString();

		_goMedal.GetComponent<UILabel>().text = ((Hashtable)sdGlobalDatabase.Instance.globalData
		                                         ["MainCharBaseProp"])["Cash"].ToString();

		ActiveTab(0);

		*/

	}
	
	public void ActiveTabBtn(bool active, int idx)
	{
		GameObject gameObject = _tabBtns[idx].gameObject;
		UISprite bg = gameObject.transform.FindChild("Background").GetComponent<UISprite>();
		UISprite tx = gameObject.transform.FindChild("Sprite").GetComponent<UISprite>();
		if( bg==null || tx==null ) return;
		
		if( active )
		{
			// 选中状态.
			if( tx.spriteName[tx.spriteName.Length-1] == '2' )
			{
				tx.spriteName = tx.spriteName.Remove(tx.spriteName.Length-1);
				bg.spriteName = "btn_Tab_click";
			}
		}
		else
		{
			// 未选中状态.
			if( tx.spriteName[tx.spriteName.Length-1] != '2' )
			{
				tx.spriteName += "2";
				bg.spriteName = "btn_Tab_nml";
			}
		}
	}
	
	void ActiveTab(int tabIdx)
	{
		if( _tabBtns[0] == null ) return;
		
		for(int i=0;i<_tabBtns.Length;i++)
		{
			if( i == tabIdx )
			{
				ActiveTabBtn(true, i);
				_tabs[i].OnActive();
			}
			else
			{
				ActiveTabBtn(false, i);
				_tabs[i].OnDeactive();
			}
		}
	
	}


	void OnTabBtn(GameObject gameObject)
	{
		if( gameObject.name == "btn_tab1" )
		{
			ActiveTab(0);
		}
		else if( gameObject.name == "btn_tab2" )
		{
			ActiveTab(1);
		}
		else if( gameObject.name == "btn_tab3" )
		{
			ActiveTab(2);
		}
	}

	public void CloseAwardCenterPanel()
	{
		if( m_goWndRoot == null )
			return;

		WndAni.HideWndAni2(m_goWndRoot);
		//WndAni.HideWndAni(m_goWndRoot,false,"bg_grey");
		//m_goWndRoot.SetActive(false);
	}
	
	void OnIconLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		object[] objArray = (object[])param.userdata0;
		int itemId = (int)objArray[0];
		int itemNum = (int)objArray[1];
		GameObject itemIcon = (GameObject)objArray[2];

		string spriteName="";
		if(itemId==100 || itemId==101 || itemId==200 || (int)objArray[4]==1)
		{
			itemIcon.GetComponent<UISprite>().atlas = obj as UIAtlas;		
			spriteName = (string)objArray[3];
		}
		else
		{
			Hashtable itemDB = sdConfDataMgr.Instance().GetItemById(itemId.ToString());
			if (itemDB != null)
			{
				int isubClass = int.Parse(itemDB["SubClass"].ToString());
				int iconId = int.Parse(itemDB["IconID"].ToString());
				spriteName = itemDB["IconPath"].ToString();
			
				if (param.info == iconId.ToString())
				{
					itemIcon.GetComponent<UISprite>().atlas = obj as UIAtlas;
				}
			}
		}

		itemIcon.GetComponent<UISprite>().spriteName = spriteName;

	}
	
	public void LoadPetAtlas(ResLoadDelegate e, object userData)
	{	
		string name = "UI/Icon/$icon_pet_0/icon_pet_0.prefab";
		ResLoadParams para = new ResLoadParams();
		para.userdata0 = userData;
		sdResourceMgr.Instance.LoadResource(name,e,para,typeof(UIAtlas));
	}

	public void LoadCommonAtlas(ResLoadDelegate e, object userData)
	{
		string name = "UI/$common/common.prefab";
		ResLoadParams para = new ResLoadParams();
		para.userdata0 = userData;
		sdResourceMgr.Instance.LoadResource(name,e,para,typeof(UIAtlas));
	}

	public void RefreshItemIcon(int itemId, int itemNum, GameObject itemIcon, GameObject numLabel, GameObject nameLabel)
	{
		object[] userdata = new object[5]{itemId, itemNum, itemIcon, "", 0};
		
		Hashtable itemDB = sdConfDataMgr.Instance().GetItemById(itemId.ToString());
		if (itemDB != null)
		{
			int iClass = int.Parse(itemDB["Class"].ToString());
			if (iClass==(int)GameItemClassType.Game_Item_Class_Pet)
			{
				int iPetId = int.Parse(itemDB["Expend"].ToString());
				Hashtable petDB = sdConfDataMgr.Instance().GetPetTemplate(iPetId.ToString());
				if (petDB!=null)
				{
					userdata[3] = petDB["Icon"].ToString();
					userdata[4] = 1;
					LoadPetAtlas(OnIconLoaded, userdata);

				}
			}
			else if(itemId == 100)
			{
				userdata[3] = "icon_money";
				LoadCommonAtlas(OnIconLoaded, userdata);
			}
			else if(itemId == 101)
			{
				userdata[3] = "icon_noncash";
				LoadCommonAtlas(OnIconLoaded, userdata);
			}
			else if(itemId == 200)
			{
				userdata[3] = "icon_tili";
				LoadCommonAtlas(OnIconLoaded, userdata);
			}
			else
			{
				int iconId = int.Parse(itemDB["IconID"].ToString());
				sdConfDataMgr.Instance().LoadItemAtlas(
					iconId.ToString(),
					OnIconLoaded,
					userdata);	
			}
		}

		if(numLabel != null)
		{
			if(itemNum==1)
			{
				numLabel.GetComponent<UILabel>().text = "";
			}
			else if(itemNum>1 && itemNum<10000)
			{
				numLabel.GetComponent<UILabel>().text = itemNum.ToString();
			}
			else
			{
				itemNum /= 10000;
				numLabel.GetComponent<UILabel>().text = itemNum.ToString() + "万";
			}
		}
		
		if(nameLabel != null)
		{

			//name.GetComponent<UILabel>().text = info["ShowName"].ToString();
			//name.GetComponent<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(info["Quility"].ToString()));
			nameLabel.GetComponent<UILabel>().text = itemDB["ShowName"].ToString();
		}	
	}

}

