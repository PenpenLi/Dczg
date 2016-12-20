using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LevelAwardWnd : Singleton<LevelAwardWnd>
{
	public bool m_dirt = false;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;
	
	GameObject _goUpgradeBlockTemplate = null;
	GameObject _goDraggableArea = null;

	
	public class UpgradeInfo
	{
		public uint Level;
		public int ItemId1;
		public int ItemNum1;
		public int Item1IfTips;
		public int ItemId2;
		public int ItemNum2;
		public int Item2IfTips;
		public int ItemId3;
		public int ItemNum3;
		public int Item3IfTips;
		public int ItemId4;
		public int ItemNum4;
		public int Item4IfTips;
		
		public bool finished;
		public GameObject goButton;
		public GameObject goIconBtn1;
		public GameObject goIconBtn2;
		public GameObject goIconBtn3;
		public GameObject goIconBtn4;
	}
	
	public List<UpgradeInfo> m_upgradeInfos = new List<UpgradeInfo>();

	public void UpdateLevelList(CliProto.SC_GIFT_LEVEL_NTF netMsg)
	{
		
		m_upgradeInfos.Clear();
		Hashtable upgradeAwardDB = sdConfDataMgr.Instance().m_upgradeAwardDB;
		foreach (DictionaryEntry item in upgradeAwardDB)
		{
			UpgradeAward upgradeAward = item.Value as UpgradeAward;
			
			UpgradeInfo upgradeInfo = new UpgradeInfo();
			upgradeInfo.Level = upgradeAward.Level;
			upgradeInfo.ItemId1 = upgradeAward.ItemId1;
			upgradeInfo.ItemNum1 = upgradeAward.ItemNum1;
			upgradeInfo.Item1IfTips = upgradeAward.Item1IfTips;
			upgradeInfo.ItemId2 = upgradeAward.ItemId2;
			upgradeInfo.ItemNum2 = upgradeAward.ItemNum2;
			upgradeInfo.Item2IfTips = upgradeAward.Item2IfTips;
			upgradeInfo.ItemId3 = upgradeAward.ItemId3;
			upgradeInfo.ItemNum3 = upgradeAward.ItemNum3;
			upgradeInfo.Item3IfTips = upgradeAward.Item3IfTips;
			upgradeInfo.ItemId4 = upgradeAward.ItemId4;
			upgradeInfo.ItemNum4 = upgradeAward.ItemNum4;
			upgradeInfo.Item4IfTips = upgradeAward.Item4IfTips;
			
			m_upgradeInfos.Add (upgradeInfo);
		}
		
		m_upgradeInfos.Sort(delegate(UpgradeInfo x, UpgradeInfo y) 	        		                     		             
		                    {
			return x.Level.CompareTo(y.Level);
		});
		
		for(int i=0; i<netMsg.m_Count; i++)
		{
			foreach(UpgradeInfo item in m_upgradeInfos)
			{
				if(netMsg.m_Level[i] == item.Level)
				{
					item.finished = true;
					break;
				}
			}
		}
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshUpgradeAwardList(false);
		}
	
	}
	
	void RefreshUpgradeAwardList(bool resetPos)
	{
		m_goWndRoot.transform.FindChild("LevelAward/sp_npctalk/lb_npctalk1").gameObject.
			GetComponent<UILabel>().text = "勇士您已达到" + 
				sdGameLevel.instance.mainChar.Level.ToString() + "级";
		
		_goUpgradeBlockTemplate = m_goWndRoot.transform.FindChild
			("LevelAward/UpgradeAwardView/DraggableArea/UpgradeBlock").gameObject;
		
		_goUpgradeBlockTemplate.SetActive(false);
		
		_goDraggableArea = m_goWndRoot.transform.FindChild
			("LevelAward/UpgradeAwardView/DraggableArea").gameObject;
		
		GameObject upgradeList = null;
		if(m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView/DraggableArea/UpgradeList"))
		{
			upgradeList = m_goWndRoot.transform.FindChild
				("LevelAward/UpgradeAwardView/DraggableArea/UpgradeList").gameObject;
			GameObject.DestroyImmediate(upgradeList);	
		}
		
		upgradeList = new GameObject("UpgradeList");
		upgradeList.transform.parent = _goDraggableArea.transform;
		upgradeList.transform.localPosition = Vector3.zero;
		upgradeList.transform.localScale = new Vector3(1,1,1);
		
		int col = 0;
		int row = 0;
		int counter = 0;
		int curMonth = 0;
		
		foreach(UpgradeInfo item in m_upgradeInfos)
		{
			GameObject goUpgradeBlock = GameObject.Instantiate(_goUpgradeBlockTemplate) as GameObject;
			goUpgradeBlock.transform.parent = upgradeList.transform;
			
			goUpgradeBlock.transform.localPosition = new Vector3(0.0f + (col * 600), 
			                                                     330.0f - (row * 140), 
			                                                     0.0f);
			goUpgradeBlock.transform.localScale = new Vector3(1,1,1);
			goUpgradeBlock.SetActive(true);
			
			goUpgradeBlock.transform.FindChild("Descript").
				GetComponent<UILabel>().text = item.Level.ToString() + "级可领取";
			
			
			AwardCenterWnd.Instance.RefreshItemIcon(item.ItemId1, 			
			                                        item.ItemNum1,
			                                        goUpgradeBlock.transform.FindChild("icon1").gameObject,
			                                        goUpgradeBlock.transform.FindChild("icon1/Num").gameObject,
			                                        null);
			
			Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.ItemId1.ToString());
			goUpgradeBlock.transform.FindChild("icon1/iconbg").GetComponent<UISprite>().color = 
				sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemInfo["Quility"].ToString()));

			AwardCenterWnd.Instance.RefreshItemIcon(item.ItemId2, 			
			                                        item.ItemNum2,
			                                        goUpgradeBlock.transform.FindChild("icon2").gameObject,
			                                        goUpgradeBlock.transform.FindChild("icon2/Num").gameObject,
			                                        null);
			
			itemInfo = sdConfDataMgr.Instance().GetItemById(item.ItemId2.ToString());
			goUpgradeBlock.transform.FindChild("icon2/iconbg").GetComponent<UISprite>().color = 
				sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemInfo["Quility"].ToString()));

			AwardCenterWnd.Instance.RefreshItemIcon(item.ItemId3, 			
			                                        item.ItemNum3,
			                                        goUpgradeBlock.transform.FindChild("icon3").gameObject,
			                                        goUpgradeBlock.transform.FindChild("icon3/Num").gameObject,
			                                        null);

			itemInfo = sdConfDataMgr.Instance().GetItemById(item.ItemId3.ToString());
			goUpgradeBlock.transform.FindChild("icon3/iconbg").GetComponent<UISprite>().color = 
				sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemInfo["Quility"].ToString()));
			
			item.goIconBtn1 = goUpgradeBlock.transform.FindChild("icon1/iconBtn").gameObject;
			item.goIconBtn2 = goUpgradeBlock.transform.FindChild("icon2/iconBtn").gameObject;
			item.goIconBtn3 = goUpgradeBlock.transform.FindChild("icon3/iconBtn").gameObject;
			
			UIEventListener.Get(item.goIconBtn1).onClick = OnIconBtn;
			UIEventListener.Get(item.goIconBtn2).onClick = OnIconBtn;
			UIEventListener.Get(item.goIconBtn3).onClick = OnIconBtn;
			
			if(item.finished)
			{
				goUpgradeBlock.transform.FindChild("Finished").gameObject.SetActive(true);
				goUpgradeBlock.transform.FindChild("Button").gameObject.SetActive (false);
			}
			else
			{
				goUpgradeBlock.transform.FindChild("Finished").gameObject.SetActive(false);
				item.goButton = goUpgradeBlock.transform.FindChild("Button").gameObject;
				UIEventListener.Get(item.goButton).onClick = OnUpgradeBtn;
				
				if(sdGameLevel.instance.mainChar.Level >= item.Level)
				{
					item.goButton.GetComponent<UIButton>().enabled = true;
					item.goButton.GetComponent<UIButton>().UpdateColor(true, true);
					item.goButton.transform.FindChild("Background/Label").gameObject.
						GetComponent<UILabel>().color = Color.white;
					
				}
				else
				{
					item.goButton.GetComponent<UIButton>().enabled = false;
					item.goButton.GetComponent<UIButton>().UpdateColor(false, true);
					item.goButton.transform.FindChild("Background/Label").gameObject.
						GetComponent<UILabel>().color = Color.gray;
				}
			}
			
			col++;
			if(col>0)
			{
				row++;
				col=0;
			}
		}
		
		_goDraggableArea.GetComponent<UIWidget>().height = row * 30;

		m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView").gameObject.SetActive(true);

		if(resetPos)
		{
			m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView").gameObject.GetComponent
				<UIDraggablePanel>().ResetPosition();
		}
	}
	
	
	void ShowTips(int itemId)
	{
		Hashtable itemDB = sdConfDataMgr.Instance().GetItemById(itemId.ToString());
		if (itemDB != null)
		{
			int iClass = int.Parse(itemDB["Class"].ToString());
			if (iClass==(int)GameItemClassType.Game_Item_Class_Pet)
			{
				sdUIPetControl.Instance.ActivePetSmallTip(null, 
				                                          int.Parse(itemDB["Expend"].ToString()), 
				                                          0, 1);
				
			}
			else
			{
				sdUICharacter.Instance.ShowTip(TipType.TempItem, 
				                               itemId.ToString());
			}
		}
	}
	
	void OnIconBtn(GameObject go)
	{
		foreach(UpgradeInfo item in m_upgradeInfos)
		{
			if(item.goIconBtn1==go)
			{
				if(item.Item1IfTips == 1)
				{	
					ShowTips(item.ItemId1);
				}
				return;
			}
			else if(item.goIconBtn2==go)
			{
				if(item.Item2IfTips == 1)
				{	
					ShowTips(item.ItemId2);
				}
				return;
			}
			else if(item.goIconBtn3==go)
			{
				if(item.Item3IfTips == 1)
				{	
					ShowTips(item.ItemId3);
				}
				return;
			}
			else if(item.goIconBtn4==go)
			{
				if(item.Item4IfTips == 1)
				{	
					ShowTips(item.ItemId4);
				}
				return;
			}
		}
	}
	
	void OnUpgradeBtn(GameObject go)
	{
		foreach(UpgradeInfo item in m_upgradeInfos)
		{
			if(item.goButton==go)
			{
				if(item.goButton.GetComponent<UIButton>().isEnabled)
				{	
					CliProto.CS_GIFT_LEVEL netMsg = new CliProto.CS_GIFT_LEVEL();
					netMsg.m_Level = (int)item.Level;
					SDNetGlobal.SendMessage(netMsg);
					
					sdUICharacter.Instance.ShowSuccessPanel();
					//sdUICharacter.Instance.ShowMsgLine("恭喜您，领奖成功！", Color.white);
				}
				else
				{
					sdUICharacter.Instance.ShowMsgLine("还未达到等级要求，" +
					                                   "请加油升级后再来领取吧！", Color.white);
				}
			}
		}
	}
	
	void Start () 
	{

	}

	float m_lastTime = 0;
	void Update () 
	{
		if(sdGameLevel.instance.mainChar == null)
			return;

		if(Time.time <= m_lastTime + 1.0f)
			return;

		m_lastTime = Time.time;
		m_dirt = false;
		foreach(UpgradeInfo item in m_upgradeInfos)
		{
			if(item.finished == false)
			{
				if(sdGameLevel.instance.mainChar.Level >= item.Level)
				{
					m_dirt = true;
				}
			}
		}

	}
	
	void OnDestory()
	{
	
	}
	
	public void Init()
	{

	}

	void OnCloseBtn(GameObject go)
	{
		ClosePanel();

		
	}

	public void OnShowWndAniFinish()
	{
		//m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView").gameObject.SetActive(true);

	}

	public void OpenPanel()
	{
		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			AwardCenterWnd.Instance.m_goWndRoot.SetActive(false);

			m_goWndRoot.SetActive(true);
			WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			RefreshUpgradeAwardList(true);

			//m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView").gameObject.SetActive(false);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "LevelAwardPanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$LevelAwardWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));

			
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "LevelAwardPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/topbar/btn_close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();
	}

	public bool IsOpen() { return _bWndOpen; }

	public void ClosePanel()
	{
		if( m_goWndRoot == null )
			return;

		_bWndOpen = false;

		AwardCenterWnd.Instance.m_goWndRoot.SetActive(true);

		//m_goWndRoot.SetActive(false);

		m_goWndRoot.transform.FindChild("LevelAward/UpgradeAwardView").gameObject.SetActive(false);
		WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

