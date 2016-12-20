using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EverydayFoodsWnd : Singleton<EverydayFoodsWnd>
{
	public bool m_dirt = false;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;
	UIButton _eatBtn = null;

	bool _chickenEated = false;
	bool _cakeEated = false;
	bool _receiveEpAck = false;

	enum FoodsType
	{
		FOODS_TYPE_EMPTY,
		FOODS_TYPE_CHICKEN,
		FOODS_TYPE_CAKE,
	}

	FoodsType _foodsType = FoodsType.FOODS_TYPE_EMPTY;

	void Start () 
	{
		_receiveEpAck = false;
		CliProto.CS_GIFT_EP_INFO_REQ netMsg = new CliProto.CS_GIFT_EP_INFO_REQ();
		SDNetGlobal.SendMessage(netMsg);
	}

	float m_lastTime = 0;
	void Update () 
	{
		if(_receiveEpAck!=true)
			return;
		
		if(Time.time <= m_lastTime + 1.0f)
			return;

		m_lastTime = Time.time;

		ulong serverTime = sdGlobalDatabase.Instance.ServerBeginTime + 
			sdGlobalDatabase.Instance.TimeFromServerBeginTime;
		DateTime time = sdConfDataMgr.Instance().ConvertServerTimeToClientTime(serverTime);

		m_dirt = false;

		if(time.Hour>=14)
			_chickenEated = true;
		
		 if(time.Hour>=20)
			_cakeEated = true;
		
		if(time.Hour>=12 && time.Hour<=13)
		{
			if(_chickenEated)
			{
				_foodsType =  FoodsType.FOODS_TYPE_EMPTY;
			}
			else
			{
				_foodsType =  FoodsType.FOODS_TYPE_CHICKEN;
				m_dirt = true;
			}
		}
		else if(time.Hour>=18 && time.Hour<=19)
		{
			if(_cakeEated)
			{
				_foodsType =  FoodsType.FOODS_TYPE_EMPTY;
			}
			else
			{
				_foodsType =  FoodsType.FOODS_TYPE_CAKE;
				m_dirt = true;
			}
		}
		else
		{
			_foodsType =  FoodsType.FOODS_TYPE_EMPTY;
		}
		
		if(m_goWndRoot == null)
			return;

		if(_foodsType == FoodsType.FOODS_TYPE_EMPTY)
		{
			
			//m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().color = 
			//	new Color(200.0f/255.0f, 200.0f/255.0f, 150.0f/255.0f);
			
			if(!_chickenEated)
			{
				int hour = 11 - time.Hour;
				int minute = 59 - time.Minute;
				int second = 59 - time.Second;

				m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().text =
					hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2") + " 后可享用";
			}
			else if(!_cakeEated)
			{
				int hour = 17 - time.Hour;
				int minute = 59 - time.Minute;
				int second = 59 - time.Second;

				m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().text =
					hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2") + " 后可享用";
			}
			else
			{
				m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().text =
					"请明天在来！";
			}

			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/Empty").gameObject.SetActive(true);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullCake").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullChicken").gameObject.SetActive(false);
			_eatBtn.gameObject.SetActive(false);

			m_goWndRoot.transform.FindChild("EverydayFoods/Title/Text").GetComponent<UILabel>().text = "大餐正在准备中";
		}
		else if(_foodsType == FoodsType.FOODS_TYPE_CHICKEN)
		{
			m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().text = "";
			_eatBtn.gameObject.SetActive(true);

			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/Empty").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullCake").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullChicken").gameObject.SetActive(true);

			m_goWndRoot.transform.FindChild("EverydayFoods/Title/Text").GetComponent<UILabel>().text = "美味鸡腿已上桌";
			

		}
		else if(_foodsType == FoodsType.FOODS_TYPE_CAKE)
		{
			m_goWndRoot.transform.FindChild("EverydayFoods/Time").GetComponent<UILabel>().text = "";
			_eatBtn.gameObject.SetActive(true);

			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/Empty").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullCake").gameObject.SetActive(true);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullChicken").gameObject.SetActive(false);

			m_goWndRoot.transform.FindChild("EverydayFoods/Title/Text").GetComponent<UILabel>().text = "美味蛋糕已上桌";
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

	public void OpenPanel()
	{
		_bWndOpen = true;

		if( m_goWndRoot != null )
		{
			AwardCenterWnd.Instance.m_goWndRoot.SetActive(false);

			m_goWndRoot.SetActive(true);
			WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/Empty").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullCake").gameObject.SetActive(false);
			m_goWndRoot.transform.FindChild("EverydayFoods/Eat/FullChicken").gameObject.SetActive(false);

			_receiveEpAck = false;
			CliProto.CS_GIFT_EP_INFO_REQ netMsg = new CliProto.CS_GIFT_EP_INFO_REQ();
			SDNetGlobal.SendMessage(netMsg);
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "EverydayFoodsPanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$EverydayFoodsWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}
	
	public void OnReceiveEpAck(CliProto.SC_GIFT_RECEIVE_EP_ACK netMsg)
	{
		/*
		EReceiveEPResult_Success    = 1,
		EReceiveEPResult_NotInTime  = 2,
		EReceiveEPResult_AlreadyGet = 3,
		EReceiveEPResult_SysError   = 4,
		*/

		if(netMsg.m_Result == (byte)HeaderProto.EReceiveEPResult.EReceiveEPResult_Success)
		{
			if(netMsg.m_DayFoodID == (int)FoodsType.FOODS_TYPE_CHICKEN)
			{
				_chickenEated = true;
			}
			else if(netMsg.m_DayFoodID == (int)FoodsType.FOODS_TYPE_CAKE)
			{
				_cakeEated = true;
			}

			sdUICharacter.Instance.ShowSuccessPanel();
			//sdUICharacter.Instance.ShowMsgLine("获取成功！", Color.white);

		}
		else if(netMsg.m_Result == (byte)HeaderProto.EReceiveEPResult.EReceiveEPResult_NotInTime)
		{
			sdUICharacter.Instance.ShowMsgLine("获取失败！", Color.white);
		}
		else if(netMsg.m_Result == (byte)HeaderProto.EReceiveEPResult.EReceiveEPResult_AlreadyGet)
		{
			sdUICharacter.Instance.ShowMsgLine("获取失败！", Color.white);
		}
		else if(netMsg.m_Result == (byte)HeaderProto.EReceiveEPResult.EReceiveEPResult_SysError)
		{
			sdUICharacter.Instance.ShowMsgLine("获取失败！", Color.white);
		}

		//m_goWndRoot.transform.FindChild("EverydayFoods/Eat").gameObject.SetActive(true);
	}

	public void UpdateEverydayFoods(CliProto.SC_GIFT_EP_INFO_ACK netMsg)
	{

		_receiveEpAck = true;

		_chickenEated = false;
		_cakeEated = false;
		
		for(int i =0; i<netMsg.m_Count; i++)
		{
			if(netMsg.m_DayFoodID[i] == (int)FoodsType.FOODS_TYPE_CHICKEN)
			{
				_chickenEated = true;
			}
			else if(netMsg.m_DayFoodID[i] == (int)FoodsType.FOODS_TYPE_CAKE)
			{
				_cakeEated = true;
			}
		}
	}

	void OnEatBtn(GameObject go)
	{
		if(_foodsType == FoodsType.FOODS_TYPE_EMPTY )
		{

		}
		else if(_foodsType == FoodsType.FOODS_TYPE_CHICKEN)
		{
			CliProto.CS_GIFT_RECEIVE_EP_REQ netMsg = new CliProto.CS_GIFT_RECEIVE_EP_REQ();
			netMsg.m_DayFoodID = 1;
			SDNetGlobal.SendMessage(netMsg);	
		}
		else if(_foodsType == FoodsType.FOODS_TYPE_CAKE)
		{
			CliProto.CS_GIFT_RECEIVE_EP_REQ netMsg = new CliProto.CS_GIFT_RECEIVE_EP_REQ();
			netMsg.m_DayFoodID = 2;
			SDNetGlobal.SendMessage(netMsg);	
		}
	}

	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "EverydayFoodsPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/topbar/btn_close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		_eatBtn = m_goWndRoot.transform.FindChild("EverydayFoods/EatBtn").GetComponent<UIButton>();
		_eatBtn.gameObject.SetActive(false);
		UIEventListener.Get(_eatBtn.gameObject).onClick = OnEatBtn;

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
		WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

