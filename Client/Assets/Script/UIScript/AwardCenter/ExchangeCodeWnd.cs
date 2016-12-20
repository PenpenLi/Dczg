using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ExchangeCodeWnd : Singleton<ExchangeCodeWnd>
{
	public bool m_dirt = false;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;
	UIButton _exchangeBtn = null;
	UIInput _exchangeInput = null;
	
	void Start () 
	{

	}

	void Update () 
	{

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
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "ExchangeCodePanel";
			sdResourceMgr.Instance.LoadResource("UI/AwardCenter/$ExchangeCodeWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "ExchangeCodePanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("AwardFrame/topbar/btn_close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		_exchangeBtn = m_goWndRoot.transform.FindChild("ExchangeCode/Exchange").GetComponent<UIButton>();
		UIEventListener.Get(_exchangeBtn.gameObject).onClick = OnExchangeBtn;

		_exchangeInput = m_goWndRoot.transform.FindChild("ExchangeCode/Input").GetComponent<UIInput>();

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();
		
	}

	void OnExchangeBtn(GameObject go)
	{
		CliProto.CS_GIFT_JIHUOMA_REQ netMsg = new CliProto.CS_GIFT_JIHUOMA_REQ();
		netMsg.m_JiHuoMa = System.Text.Encoding.UTF8.GetBytes(_exchangeInput.value);
		SDNetGlobal.SendMessage(netMsg);

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

