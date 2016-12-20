using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ChargeTab : TabBase
{
	GameObject _nextVipLevel;
	GameObject _progressBar;
	GameObject _vipLevel;
	GameObject _vipBtn;
	GameObject _MedalNum;

	class ChargeInfo
	{
		public int monthCardId;
		public string productCode;
		public string itemName;
		public int price;
		public GameObject chargeBtn;
	}
	List<ChargeInfo> _chargeInfos = new List<ChargeInfo>();

	public ChargeTab(GameObject goTabRoot):base(goTabRoot)
	{
		if(goTabRoot==null)
			return;
		
		_nextVipLevel = goTabRoot.transform.FindChild("Bottom/Label/NextLevel").gameObject;
		_progressBar = goTabRoot.transform.FindChild("Progress Bar").gameObject;
		_vipLevel = goTabRoot.transform.FindChild("Bottom/VIPLevel").gameObject;
		_vipBtn = goTabRoot.transform.FindChild("Bottom/ChargeVIP").gameObject;
		_MedalNum = goTabRoot.transform.FindChild("Bottom/MedalNum").gameObject;
		
		UIEventListener.Get(_vipBtn).onClick = OnVipBtn;

		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item1/Button").gameObject).onClick = OnChargeBtn;
		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item2/Button").gameObject).onClick = OnChargeBtn;
		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item3/Button").gameObject).onClick = OnChargeBtn;
		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item4/Button").gameObject).onClick = OnChargeBtn;
		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item5/Button").gameObject).onClick = OnChargeBtn;
		UIEventListener.Get(goTabRoot.transform.FindChild("Panel/frame2/Item6/Button").gameObject).onClick = OnChargeBtn;

		_chargeInfos.Clear();
		foreach( string key in sdMallManager.Instance.m_gHomeProducts.Keys)
		{
			string productCode = sdMallManager.Instance.m_gHomeProducts[key].ProductCode;
			RmbCharge rmbCharge = sdConfDataMgr.Instance().m_actionPointDB[productCode] as RmbCharge;
			GameObject chargeItem = goTabRoot.transform.FindChild("Panel/frame2/Item" + rmbCharge.Position.ToString()).gameObject;
			if(chargeItem != null)
			{
				ChargeInfo chargeInfo = new ChargeInfo();
				chargeInfo.chargeBtn = chargeItem.transform.FindChild("Button").gameObject;
				chargeInfo.itemName = sdMallManager.Instance.m_gHomeProducts[key].ItemName;
				chargeInfo.price = rmbCharge.Rmb;
				chargeInfo.productCode = productCode;
				chargeInfo.monthCardId = rmbCharge.MonthCardId;

				chargeItem.transform.FindChild("Cash").GetComponent<UILabel>().text = "徽章 x" + rmbCharge.Cash.ToString();
				chargeItem.transform.FindChild("Button/BadgePrice").GetComponent<UILabel>().text = rmbCharge.Rmb.ToString() + " 元";
				if(rmbCharge.IsOnSale == 0)
					chargeItem.transform.FindChild("Outfit/Sprite (tj)").gameObject.SetActive(false);
				else
					chargeItem.transform.FindChild("Outfit/Sprite (tj)").gameObject.SetActive(true);

				if(rmbCharge.Double == 0)
				{

				}
			}		
		}
	}
	
	void OnVipBtn(GameObject go)
	{
		sdMallManager.Instance.m_MainPanel.GetComponent<MallPanel>().ActiveTab(MallPanel.TabName.TAB_NAME_VIP);
	}


	void OnChargeBtn(GameObject go)
	{
		foreach(ChargeInfo chargeInfo in _chargeInfos)
		{
			if(go == chargeInfo.chargeBtn)
			{
				if(chargeInfo.monthCardId!=0)
				{

					return;
				}

				//
				GHome.GetInstance().Pay("", BundleGlobal.AppVersion()["Area"], chargeInfo.productCode, "", (code, msg, data) =>
				{
					if(code== Constants.ERROR_CODE_SUCCESS)
					{

					}
					else
					{

					}
				});

				return;
			}
		}
	}

	void Start () 
	{
		
	}

	void Update () 
	{

	}
	
	void OnDestory()
	{
	
	}
	
	override public void Init()
	{

	}

	override public void RefreshUserInterface(bool resetPos)
	{
		if(_isActive)
		{
			Hashtable vipUpgradeDB = sdConfDataMgr.Instance().m_vipUpgradeDB;
			int maxMedal = 0;
			int sumMedal = 0;
			
			if(sdMallManager.Instance.m_iCurrentVIPLevel < 10)
			{
				m_goTabRoot.transform.FindChild("Bottom/Label/Sprite (icon_xz)").gameObject.SetActive(true);
				
				VipUpgrade vipUpgrade = vipUpgradeDB[(int)sdMallManager.Instance.m_iCurrentVIPLevel+1] as VipUpgrade;
				int needMedal = (vipUpgrade.RMB - sdMallManager.Instance.m_RMBSum) * 10;
				int nextVip = (int)sdMallManager.Instance.m_iCurrentVIPLevel+1;
				_nextVipLevel.GetComponent<UILabel>().text = "x"+needMedal.ToString()+" ，即可升至VIP"+nextVip.ToString()+"。";
				
				maxMedal = vipUpgrade.RMB * 10;
				sumMedal = sdMallManager.Instance.m_RMBSum * 10;
				
			}
			else
			{
				_nextVipLevel.GetComponent<UILabel>().text = "";
				m_goTabRoot.transform.FindChild("Bottom/Label/Sprite (icon_xz)").gameObject.SetActive(false);
				m_goTabRoot.transform.FindChild("Bottom/Label").GetComponent<UILabel>().text = "恭喜您，已到最高级！";
				
				maxMedal = sdMallManager.Instance.m_RMBSum * 10;
				sumMedal = sdMallManager.Instance.m_RMBSum * 10;
			}
			
			_vipLevel.GetComponent<UILabel>().text = sdMallManager.Instance.m_iCurrentVIPLevel.ToString();
			_MedalNum.GetComponent<UILabel>().text = sumMedal.ToString()+"/"+maxMedal.ToString();
			_progressBar.GetComponent<UISlider>().value = (float)sumMedal /(float)maxMedal;
			
		}
	}

	override public void OnActive()
	{
		base.OnActive();

		RefreshUserInterface (true);
	}
	
	override public void OnDeactive()
	{
		
		base.OnDeactive();
	}

}