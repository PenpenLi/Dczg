using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EverydayAwardWnd : Singleton<EverydayAwardWnd>
{
	public bool m_dirt = false;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;

	GameObject _goSignBlockTemplate = null;
	GameObject _goDraggableArea = null;

	public class SignInfo
	{
		public uint Index;
		public int Month;
		public int LoginDays;
		public int ItemID;
		public int ItemNum;
		public int VIPlevel;
		public int IfTips;
		
		public bool signFinished;
		public bool signAccept;
		public bool signVip;
		
		public GameObject goButton;
		public GameObject goDay;
		public GameObject goFinished;
		public GameObject goFx;
	}

	public int m_signCount = 0;

	public List<SignInfo> m_signInfos = new List<SignInfo>();
	bool _signListInitialed = false;
	
	void InitSignList()
	{
		if(_signListInitialed)
			return;
		
		_signListInitialed = true;
		m_signInfos.Clear();
		Hashtable dailyAwardDB = sdConfDataMgr.Instance().m_dailyAwardDB;
		foreach(DictionaryEntry item in dailyAwardDB)
		{
			DailyAward dailyAward = item.Value as DailyAward;
			if(System.DateTime.Now.Month == dailyAward.Month)
			{
				SignInfo signInfo = new SignInfo();
				signInfo.Index = dailyAward.Index;
				signInfo.Month = dailyAward.Month;
				signInfo.LoginDays = dailyAward.LoginDays;
				signInfo.ItemID = dailyAward.ItemID;
				signInfo.ItemNum = dailyAward.ItemNum;
				signInfo.VIPlevel = dailyAward.VIPlevel;
				signInfo.IfTips = dailyAward.IfTips;
				
				signInfo.signFinished = false;
				signInfo.signAccept = false;
				signInfo.signVip = false;
				
				signInfo.goButton = null;
				signInfo.goDay = null;
				signInfo.goFinished = null;
				signInfo.goFx = null;
				
				m_signInfos.Add (signInfo);
				
			}
		}
		
		m_signInfos.Sort(delegate(SignInfo x, SignInfo y) 			                 
		{
			return x.LoginDays.CompareTo(y.LoginDays);
		});

	}
	
	public void UpdateSignList(CliProto.SC_GIFT_SIGN_NTF netMsg)
	{
		
		InitSignList();
		
		int counter = 0;
		foreach(SignInfo item in m_signInfos)
		{
			if(netMsg.m_SignCount > counter)
				item.signFinished = true;
			else
				item.signFinished = false;
			
			counter++;
		}
		
		if(m_signInfos.Count > 0)
		{
			m_signCount = (int)netMsg.m_SignCount;
			if(netMsg.m_CanSign == 0)
			{
				m_signInfos[m_signCount].signAccept = false;
			}
			else
			{
				m_signInfos[m_signCount].signAccept = true;
			}
		}
		else
		{
			Debug.Log("m_signInfos size is " + m_signInfos.Count.ToString());
		}
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshDailyAwardList(false);
		}

	}
	
	public void UpdateVipSignList(uint vipLevel)
	{
		InitSignList();
		
		int counter=0;
		foreach(SignInfo item in m_signInfos)
		{
			if(m_signCount>counter)
			{
				if(item.VIPlevel > 0)
				{
					item.signVip = true;
					
					if(vipLevel>=item.VIPlevel)
					{
						item.signAccept = true;
						item.signFinished = false;
					}
				}
			}
			
			counter++;
		}
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshDailyAwardList(false);
		}
	}
	
	public void UpdateVipSignList(CliProto.SC_GIFT_SIGN_VIP_NTF netMsg)
	{
		InitSignList();
		
		UpdateVipSignList(sdMallManager.Instance.m_iCurrentVIPLevel);
		
		for(int i=0; i<netMsg.m_Count; i++)
		{
			foreach(SignInfo item in m_signInfos)
			{
				if(netMsg.m_Day[i] == item.LoginDays)
				{
					item.signFinished = true;
					break;
				}
			}
		}
		
		if(m_goWndRoot!=null && _bWndOpen)
		{
			RefreshDailyAwardList(false);
		}

	}

	public void RefreshDailyAwardList(bool resetPos)
	{
	

		m_goWndRoot.transform.FindChild("EverydayAward/sp_npctalk/lb_npctalk1").gameObject.
			GetComponent<UILabel>().text = "本月已经累计签到" + m_signCount.ToString() + "次";
		
		_goSignBlockTemplate = m_goWndRoot.transform.FindChild
			("EverydayAward/DailySignView/DraggableArea/SignBlock").gameObject;
		
		_goSignBlockTemplate.SetActive(false);
		
		_goDraggableArea = m_goWndRoot.transform.FindChild
			("EverydayAward/DailySignView/DraggableArea").gameObject;
		
		GameObject signList = null;
		if(m_goWndRoot.transform.FindChild("EverydayAward/DailySignView/DraggableArea/SignList"))
		{
			signList = m_goWndRoot.transform.FindChild
				("EverydayAward/DailySignView/DraggableArea/SignList").gameObject;
			GameObject.DestroyImmediate(signList);	
		}
		
		signList = new GameObject("SignList");
		signList.transform.parent = _goDraggableArea.transform;
		signList.transform.localPosition = Vector3.zero;
		signList.transform.localScale = new Vector3(1,1,1);
		
		int col = 0;
		int row = 0;
		int counter = 0;
		int curMonth = 0;
		
		foreach(SignInfo item in m_signInfos)
		{
			GameObject goSignBlock = GameObject.Instantiate(_goSignBlockTemplate) as GameObject;
			goSignBlock.transform.parent = signList.transform;
			
			goSignBlock.transform.localPosition = new Vector3(-295.0f + (col * 145), 
			                                                  130.0f - (row * 170), 
			                                                  0.0f);
			goSignBlock.transform.localScale = new Vector3(1,1,1);
			goSignBlock.SetActive(true);
			
			goSignBlock.transform.FindChild("Day/Label").
				GetComponent<UILabel>().text = "第 " + item.LoginDays.ToString() + " 天";
			
			AwardCenterWnd.Instance.RefreshItemIcon(item.ItemID, 			
			                                        item.ItemNum,
			                                        goSignBlock.transform.FindChild("Button/Background").gameObject,
			                                        goSignBlock.transform.FindChild ("Num").gameObject,
			                                        null);
			
			item.goButton = goSignBlock.transform.FindChild("Button").gameObject;
			UIEventListener.Get(item.goButton).onClick = OnSignBtn;
			
			item.goFx = goSignBlock.transform.FindChild("Fx_Tishi1Prefab").gameObject;
			item.goFx.SetActive(false);
				
			if(item.VIPlevel>0)
				goSignBlock.transform.FindChild("vip").gameObject.SetActive(true);
			else
				goSignBlock.transform.FindChild("vip").gameObject.SetActive(false);
			
			if(item.signFinished==false)
			{
				goSignBlock.transform.FindChild("Finished").gameObject.SetActive(false);
				
				if(item.signAccept==true)
				{
					item.goButton.GetComponent<UIButton>().enabled = true;
					item.goButton.GetComponent<UIButton>().UpdateColor(true, true);
					item.goFx.SetActive(true);
				}
				else
				{
					item.goButton.GetComponent<UIButton>().enabled = false;
					item.goButton.GetComponent<UIButton>().UpdateColor(true, true);
					item.goFx.SetActive(false);
				}
			}
			else
			{
				goSignBlock.transform.FindChild("Finished").gameObject.SetActive(true);
				//goSignBlock.transform.FindChild("sign").gameObject.SetActive(false);
				item.goButton.GetComponent<UIButton>().enabled = false;
				item.goButton.GetComponent<UIButton>().UpdateColor(false, true);
			}
			
			col++;
			if(col>4)
			{
				row++;
				col=0;
			}
		}
		
		//_goDraggableArea.GetComponent<UIWidget> ().CalculateBounds ();
		_goDraggableArea.GetComponent<UIWidget>().height = row * 60;

		m_goWndRoot.transform.FindChild("EverydayAward/DailySignView").gameObject.SetActive(true);

		if(resetPos)
		{
			m_goWndRoot.transform.FindChild("EverydayAward/DailySignView").gameObject.GetComponent
				<UIDraggablePanel>().ResetPosition();
		}
	}
	
	void OnSignBtn(GameObject go)
	{
		foreach(SignInfo item in m_signInfos)
		{
			if(item.goButton==go)
			{
				if(item.goButton.GetComponent<UIButton>().isEnabled)
				{
					if(item.signFinished == false)
					{
						item.signFinished = true;
						if(item.signVip)
						{
							CliProto.CS_GIFT_SIGN_VIP netMsg = new CliProto.CS_GIFT_SIGN_VIP();
							netMsg.m_SignDate = (uint)item.LoginDays;
							SDNetGlobal.SendMessage(netMsg);					
						}
						else
						{
							CliProto.CS_GIFT_SIGN netMsg = new CliProto.CS_GIFT_SIGN();
							SDNetGlobal.SendMessage(netMsg);
						}
						
						sdUICharacter.Instance.ShowSuccessPanel();
						//sdUICharacter.Instance.ShowMsgLine("签到成功！", Color.white);
					}
				}
				else if(item.IfTips==1)
				{
					
					Hashtable itemDB = sdConfDataMgr.Instance().GetItemById(item.ItemID.ToString());
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
							                               item.ItemID.ToString());
						}
					}
				}
				
				return;
			}
		}
		
	}


	void Start () 
	{

	}

	float m_lastTime = 0;
	void Update () 
	{
		if(Time.time <= m_lastTime + 1.0f)
			return;
		
		m_lastTime = Time.time;
		m_dirt = false;
		foreach(SignInfo item in m_signInfos)
		{
			if(item.signFinished==false)
			{
				if(item.signAccept==true)
					m_dirt = true;
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
		
		//m_goWndRoot.transform.FindChild("EverydayAward/DailySignView").gameObject.SetActive(true);
	}

	public void OpenPanel()
	{
		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			AwardCenterWnd.Instance.m_goWndRoot.SetActive(false);

			m_goWndRoot.SetActive(true);
			
			//m_goWndRoot.transform.FindChild("EverydayAward/DailySignView").gameObject.SetActive(false);
			WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			RefreshDailyAwardList(true);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "EverydayAwardPanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$EverydayAwardWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "EverydayAwardPanel")
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
		m_goWndRoot.transform.FindChild("EverydayAward/DailySignView").gameObject.SetActive(false);
		WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

