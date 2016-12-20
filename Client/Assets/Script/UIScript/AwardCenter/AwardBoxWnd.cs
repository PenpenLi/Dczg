using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AwardBoxWnd : Singleton<AwardBoxWnd>
{
	public bool m_accept=false;
	public uint m_awardBoxId=0;
	public GameObject 	m_goWndRoot	= null;
	
	public int m_itemId = 0;
	public int m_itemNum = 0;
	public GameObject m_itemIcon = null;


	UIButton _getAwardBtn = null;
	UIButton _closeBtn = null;
	UILabel _descript = null;


	bool _bWndOpen = false;

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


	void OnGetAwardBtn(GameObject go)
	{	
		if(go.GetComponent<UIButton>().isEnabled)
		{
			CliProto.CS_GIFT_DAY_BOX_REQ netMsg = new CliProto.CS_GIFT_DAY_BOX_REQ();
			netMsg.m_BoxID = m_awardBoxId;
			SDNetGlobal.SendMessage(netMsg);
			ClosePanel();
		}
		else
		{
			sdUICharacter.Instance.ShowMsgLine("无法领取", Color.white);
		}
	
	}

	void OnCloseBtn(GameObject go)
	{
		ClosePanel();

		_bWndOpen = false;
	}


	void OnSetIcon(int id, int num, GameObject icon)
	{
		UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(id.ToString());
		if (atlas!=null)
		{
			/*
			goIcon.GetComponent<UISprite>().atlas = atlas;
			goIcon.GetComponent<UISprite>().spriteName = strIcon;
			*/
		}
	}


	void OnIconBtn(GameObject go)
	{
		
	}


	public void RefreshUserInterface()
	{
		if(_descript==null || _getAwardBtn==null || _closeBtn==null)
			return;

	}

	public void OpenPanel()
	{
		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			m_goWndRoot.SetActive(true);
			RefreshUserInterface();
		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "AwardBoxPanel";
			sdResourceMgr.Instance.LoadResource("UI/$AwardCenter/AwardBoxWnd.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));

			sdUILoading.ActiveSmallLoadingUI(false);
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "AwardBoxPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);

		// Get the panel controller
		_closeBtn =  m_goWndRoot.transform.FindChild("AwardBoxFrame/topbar/btn_close").GetComponent<UIButton>();
		_getAwardBtn = m_goWndRoot.transform.FindChild ("AwardBoxFrame/Button").GetComponent<UIButton>();
		_descript = m_goWndRoot.transform.FindChild("AwardBoxFrame/Descript").GetComponent<UILabel>();

		// Added event listener
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;
		UIEventListener.Get(_getAwardBtn.gameObject).onClick = OnGetAwardBtn;

		RefreshUserInterface();

	}

	public void ClosePanel()
	{
		if( m_goWndRoot == null )
			return;

		m_goWndRoot.SetActive(false);
	}
}

