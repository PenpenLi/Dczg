using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MallPanel : MonoBehaviour
{
    public UIDraggablePanel m_MallPanelMove;
	public GameObject m_MallPanelGrid;
    
	public UILabel m_LblPetRemainCount;
    public UILabel m_LblPetRemainTimeCheap;
    public UILabel m_LblPetRemainTimeExpensive;

	public bool m_isPanelOpen = false;
	
	private bool m_isStart = false;
	private Transform m_BtnGift = null;
	private Transform m_BtnMall = null;
	private Transform m_BtnMagic = null;
	private Transform m_BtnCharge = null;
	private Transform m_BtnVIP = null;
	private Transform m_CloseBtn = null;

	public enum TabName
	{
		TAB_NAME_MALL,
		TAB_NAME_VIP,
		TAB_NAME_CHARGE,
		TAB_LENTH,
	}

	TabBase[] _tabs = new TabBase[(int)TabName.TAB_LENTH];


	TabName _activeTab;
	public void ActiveTab(TabName activeTab)  { _activeTab = activeTab; }

	void Awake()
	{
	
	}

	// Use this for initialization
	void Start ()
	{
		m_isStart = true;
		
		// Added tabs
		_tabs[(int)TabName.TAB_NAME_MALL] = new MallTab(transform.FindChild("Mall/PanelMall").gameObject);
		_tabs[(int)TabName.TAB_NAME_VIP] = new VipTab(transform.FindChild("VIP/PanelVIP").gameObject);
		_tabs[(int)TabName.TAB_NAME_CHARGE] = new ChargeTab(transform.FindChild("Charge/PanelCharge").gameObject);

		if(_activeTab == TabName.TAB_NAME_CHARGE)
		{
			this.transform.FindChild("Charge/ButtonCharge").GetComponent<UIToggle>().value = true;
			ActiveTab((int)TabName.TAB_NAME_CHARGE);
		}
		else if(_activeTab == TabName.TAB_NAME_MALL)
		{
			this.transform.FindChild("Mall/ButtonMall").GetComponent<UIToggle>().value = true;
			ActiveTab((int)TabName.TAB_NAME_MALL);
		}
		else if(_activeTab == TabName.TAB_NAME_VIP)
		{
			this.transform.FindChild("VIP/Button").GetComponent<UIToggle>().value = true;
			ActiveTab((int)TabName.TAB_NAME_VIP);
		}
	}
	
	public void OnShowWndAniFinish()
	{
		/*
		if (sdUICharacter.Instance.GetTownUI()!=null)
		{
			sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
		}
		*/


	}

	static TabName s_lastActiveTab;
	// Update is called once per frame
	void Update ()
	{
		if(m_isStart && m_isPanelOpen)
		{
			if(s_lastActiveTab == _activeTab)
				return;

			if(_activeTab == TabName.TAB_NAME_CHARGE)
			{
				this.transform.FindChild("Charge/ButtonCharge").GetComponent<UIToggle>().value = true;
				ActiveTab((int)TabName.TAB_NAME_CHARGE);
			}
			else if(_activeTab == TabName.TAB_NAME_MALL)
			{
				this.transform.FindChild("Mall/ButtonMall").GetComponent<UIToggle>().value = true;
				ActiveTab((int)TabName.TAB_NAME_MALL);
			}
			else if(_activeTab == TabName.TAB_NAME_VIP)
			{
				this.transform.FindChild("VIP/Button").GetComponent<UIToggle>().value = true;
				ActiveTab((int)TabName.TAB_NAME_VIP);
			}

			s_lastActiveTab = _activeTab;
		}	
	}
	
	private void OnEnable()
	{
		/*
		if (sdUICharacter.Instance.GetTownUI()!=null)
		{
			sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
		}
		*/

		if (m_CloseBtn == null)
		{
			m_CloseBtn = transform.FindChild("Close");
			if (m_CloseBtn != null)
			{
				UIButton btn = m_CloseBtn.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnCloseBtnClicked(); });
			}
		}

		if (m_BtnGift == null)
		{
			Transform giftTransform = transform.FindChild("Gift");
			if (giftTransform != null)
				m_BtnGift = giftTransform.FindChild("ButtonGift");
			if (m_BtnGift != null)
			{
				UIButton btn = m_BtnGift.GetComponent<UIButton>();
				//EventDelegate.Add(btn.onClick, delegate(){ OnTabBtnClicked(sdMallManager.MallPageType.gift); });
			}
		}
		
		if (m_BtnMall == null)
		{
			Transform mallTransform = transform.FindChild("Mall");
			if (mallTransform != null)
				m_BtnMall = mallTransform.FindChild("ButtonMall");
			if (m_BtnMall != null)
			{
				UIButton btn = m_BtnMall.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate() { OnTabBtnClicked(TabName.TAB_NAME_MALL); });
			}
		}
		if (m_BtnMagic == null)
		{
			Transform magicTransform = transform.FindChild("Magic");
			if (magicTransform != null)
				m_BtnMagic = magicTransform.FindChild("ButtonMagic");
			if (m_BtnMagic != null)
			{
				UIButton btn = m_BtnMagic.GetComponent<UIButton>();
				//EventDelegate.Add(btn.onClick, delegate(){ OnTabBtnClicked(sdMallManager.MallPageType.magic); });
			}
		}
		
		if (m_BtnCharge == null)
		{
			Transform chargeTransform = transform.FindChild("Charge");
			if (chargeTransform != null)
				m_BtnCharge = chargeTransform.FindChild("ButtonCharge");
			if (m_BtnCharge != null)
			{
				UIButton btn = m_BtnCharge.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate(){ OnTabBtnClicked(TabName.TAB_NAME_CHARGE); });
			}
		}
		
		if (m_BtnVIP == null)
		{
			Transform vipTransform = transform.FindChild("VIP");
			if (vipTransform != null)
				m_BtnVIP = vipTransform.FindChild("Button");
			if (m_BtnVIP != null)
			{
				UIButton btn = m_BtnVIP.GetComponent<UIButton>();
				EventDelegate.Add(btn.onClick, delegate() { OnTabBtnClicked(TabName.TAB_NAME_VIP); });
			}
		}

		m_isPanelOpen = true;

		//transform.FindChild("Mall/PanelMall/Panel").gameObject.SetActive(false);
		//transform.FindChild("VIP/PanelVIP/Panel").gameObject.SetActive(false);

		sdMallMsg.Send_CS_SHOP_GET_GOODSLIST_REQ();

	}
	
	void ActiveTab(int tabIdx)
	{
		for (int i=0; i<_tabs.Length; i++) 
		{
			if(i == tabIdx)
			{
				_tabs[i].OnActive();
			}
			else
			{
				_tabs[i].OnDeactive();
			}
		}
	}


	public void RefreshTabPanel(int tabIdx, bool restPos){ _tabs[tabIdx].RefreshUserInterface(restPos); }

	private void OnCloseBtnClicked()
	{
		m_isPanelOpen = false;

		//transform.FindChild("Mall/PanelMall/Panel").gameObject.SetActive(false);
		//transform.FindChild("VIP/PanelVIP/Panel").gameObject.SetActive(false);
		
		WndAni.HideWndAni(this.gameObject,true,"w_black");

		//gameObject.SetActive (false);
		//sdUICharacter.Instance.ShowFullScreenUI (false);
	}


	private void OnTabBtnClicked(TabName tabName)
	{
		//ActiveTab((int)tabName);
		ActiveTab(tabName);
	}

}
