using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuyActionTipPanel : Singleton<BuyActionTipPanel>
{
	public int m_costCash = 0;
	public int m_recoveryEP = 0;

	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;
	UIButton _cancelBtn = null;
	UIButton _confirmBtn = null;
	UIButton _tipBtn = null;

	UILabel _cash = null;
	UILabel _ep = null;

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
			m_goWndRoot.SetActive(true);
			//WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			_cash.text = m_costCash.ToString();
			_ep.text = m_recoveryEP.ToString();

			_tipBtn.gameObject.GetComponent<UIToggle>().isChecked = false;
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "BuyActionTipPanel";
			sdResourceMgr.Instance.LoadResource("UI/Shop/$BuyActionTipPanel.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "BuyActionTipPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("Close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		_cancelBtn = m_goWndRoot.transform.FindChild("CancelBtn").GetComponent<UIButton>();
		UIEventListener.Get(_cancelBtn.gameObject).onClick = OnCloseBtn;

		_confirmBtn = m_goWndRoot.transform.FindChild("ConfirmBtn").GetComponent<UIButton>();
		UIEventListener.Get(_confirmBtn.gameObject).onClick = OnConfirmBtn;

		_tipBtn = m_goWndRoot.transform.FindChild("tg_tip").GetComponent<UIButton>();
		UIEventListener.Get(_tipBtn.gameObject).onClick = OnTipBtn;

		_cash = m_goWndRoot.transform.FindChild("Text/cash").GetComponent<UILabel>();
		_ep = m_goWndRoot.transform.FindChild("Text/ep").GetComponent<UILabel>();

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();
		
	}

	void OnTipBtn(GameObject go)
	{
		sdMallManager.Instance.m_buyActionTips = _tipBtn.gameObject.GetComponent<UIToggle>().isChecked;
	}

	void OnConfirmBtn(GameObject go)
	{
		sdMallMsg.Send_CS_SHOP_BUY_ACTION_POINT_REQ ();
		ClosePanel();		
	}

	public bool IsOpen() { return _bWndOpen; }

	public void ClosePanel()
	{
		if( m_goWndRoot == null )
			return;

		_bWndOpen = false;

		m_goWndRoot.SetActive(false);

		//m_goWndRoot.SetActive(false);
		//WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

