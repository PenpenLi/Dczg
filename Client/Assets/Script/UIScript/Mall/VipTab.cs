using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VipTab : TabBase
{
	GameObject _nextVipLevel;
	GameObject _progressBar;
	GameObject _vipLevel;
	GameObject _chargeBtn;
	GameObject _MedalNum;
	
	Dictionary<GameObject, int> _vipBuys = new Dictionary<GameObject, int>();
	Dictionary<GameObject, int> _vipGoods = new Dictionary<GameObject, int>();

	public VipTab(GameObject goTabRoot):base(goTabRoot)
	{
		if(goTabRoot==null)
			return;

		_nextVipLevel = goTabRoot.transform.FindChild("Bottom/Label/NextLevel").gameObject;
		_progressBar = goTabRoot.transform.FindChild("Progress Bar").gameObject;
		_vipLevel = goTabRoot.transform.FindChild("Bottom/VIPLevel").gameObject;
		_chargeBtn = goTabRoot.transform.FindChild("Bottom/ChargeVIP").gameObject;
		_MedalNum = goTabRoot.transform.FindChild("Bottom/MedalNum").gameObject;

		UIEventListener.Get(_chargeBtn).onClick = OnChargeBtn;
	}
	
	void OnChargeBtn(GameObject go)
	{
		sdMallManager.Instance.m_MainPanel.GetComponent<MallPanel>().ActiveTab(MallPanel.TabName.TAB_NAME_CHARGE);
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

	GameObject CreateNewItem(int index)
	{
		GameObject itemTemplate = m_goTabRoot.transform.FindChild 
			("Panel/ItemTemplate").gameObject;
		if(itemTemplate == null)		
			return null;
		
		string itemName = "Item" + index;

		try
		{
			GameObject existItem = m_goTabRoot.transform.FindChild("Panel/PanelGrid/"+itemName).gameObject;
			return existItem;
		}
		catch(Exception e)
		{
			GameObject newItem = GameObject.Instantiate (itemTemplate) as GameObject;
			if(newItem) 
			{
				newItem.transform.name = itemName;
				newItem.transform.parent = m_goTabRoot.transform.FindChild("Panel/PanelGrid");
				newItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				newItem.SetActive(true);
			}
			
			return newItem;
		}

	}

	void RefreshItemView (int slot)
	{
		
		GameObject goSlot = CreateNewItem(slot);
		goSlot.transform.localPosition = new Vector3(slot*1220, 0f, 0f);
		goSlot.gameObject.SetActive(true);
		
		RefreshVipBuy(goSlot, slot+1);
		RefreshVipDesc(goSlot, slot+1);
			
	}

	void OnGoodsBtn(GameObject go)
	{
		int goodsId = _vipGoods[go];
		Hashtable itemDB = sdConfDataMgr.Instance().GetItemById(goodsId.ToString());
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
				                               goodsId.ToString());
			}
		}

	}

	void RefreshVipBuy(GameObject slot, int vipLevel)
	{
		slot.transform.FindChild("frame1/Title").GetComponent<UILabel>().text =
			"VIP" + vipLevel.ToString() + "  尊享礼包";
		
		GameObject goodsBtn = null;
		Hashtable itemDB = null;
		Hashtable vipUpgradeDB = sdConfDataMgr.Instance().m_vipUpgradeDB;
		VipUpgrade vipUpgrade = vipUpgradeDB[vipLevel] as VipUpgrade;
		
		AwardCenterWnd.Instance.RefreshItemIcon(vipUpgrade.VIPItem1,
		                                        vipUpgrade.VipItemNumber1,
		                                        slot.transform.FindChild("frame1/Icon1/GiftPic").gameObject,
		                                        slot.transform.FindChild("frame1/Icon1/Num").gameObject,
		                                        slot.transform.FindChild("frame1/Icon1/GiftName").gameObject);
		
		goodsBtn = slot.transform.FindChild("frame1/Icon1/Button").gameObject;
		UIEventListener.Get(goodsBtn).onClick = OnGoodsBtn;
		_vipGoods.Add(goodsBtn, vipUpgrade.VIPItem1);

		itemDB = sdConfDataMgr.Instance().GetItemById(vipUpgrade.VIPItem1.ToString());
		if( itemDB != null )
		{
			slot.transform.FindChild("frame1/Icon1/Background").GetComponent<UISprite>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			slot.transform.FindChild("frame1/Icon1/GiftName").GetComponent<UILabel>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			
		}

		AwardCenterWnd.Instance.RefreshItemIcon(vipUpgrade.VIPItem2, 			
		                                        vipUpgrade.VipItemNumber2,
		                                        slot.transform.FindChild("frame1/Icon2/GiftPic").gameObject,
		                                        slot.transform.FindChild("frame1/Icon2/Num").gameObject,
		                                        slot.transform.FindChild("frame1/Icon2/GiftName").gameObject);

		goodsBtn = slot.transform.FindChild("frame1/Icon2/Button").gameObject;
		UIEventListener.Get(goodsBtn).onClick = OnGoodsBtn;
		_vipGoods.Add(goodsBtn, vipUpgrade.VIPItem2);

		itemDB = sdConfDataMgr.Instance().GetItemById(vipUpgrade.VIPItem2.ToString());
		if( itemDB != null )
		{
			slot.transform.FindChild("frame1/Icon2/Background").GetComponent<UISprite>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			slot.transform.FindChild("frame1/Icon2/GiftName").GetComponent<UILabel>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			
		}
		
		
		AwardCenterWnd.Instance.RefreshItemIcon(vipUpgrade.VIPItem3, 			
		                                        vipUpgrade.VipItemNumber3,
		                                        slot.transform.FindChild("frame1/Icon3/GiftPic").gameObject,
		                                        slot.transform.FindChild("frame1/Icon3/Num").gameObject,
		                                        slot.transform.FindChild("frame1/Icon3/GiftName").gameObject);
		
		goodsBtn = slot.transform.FindChild("frame1/Icon3/Button").gameObject;
		UIEventListener.Get(goodsBtn).onClick = OnGoodsBtn;
		_vipGoods.Add(goodsBtn, vipUpgrade.VIPItem3);

		itemDB = sdConfDataMgr.Instance().GetItemById(vipUpgrade.VIPItem3.ToString());
		if( itemDB != null )
		{
			slot.transform.FindChild("frame1/Icon3/Background").GetComponent<UISprite>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			slot.transform.FindChild("frame1/Icon3/GiftName").GetComponent<UILabel>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			
		}
		
		AwardCenterWnd.Instance.RefreshItemIcon(vipUpgrade.VIPItem4,
		                                        vipUpgrade.VipItemNumber4,
		                                        slot.transform.FindChild("frame1/Icon4/GiftPic").gameObject,
		                                        slot.transform.FindChild("frame1/Icon4/Num").gameObject,
		                                        slot.transform.FindChild("frame1/Icon4/GiftName").gameObject);
		
		goodsBtn = slot.transform.FindChild("frame1/Icon4/Button").gameObject;
		UIEventListener.Get(goodsBtn).onClick = OnGoodsBtn;
		_vipGoods.Add(goodsBtn, vipUpgrade.VIPItem4);

		itemDB = sdConfDataMgr.Instance().GetItemById(vipUpgrade.VIPItem4.ToString());
		if( itemDB != null )
		{
			slot.transform.FindChild("frame1/Icon4/Background").GetComponent<UISprite>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			slot.transform.FindChild("frame1/Icon4/GiftName").GetComponent<UILabel>().color
				= sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemDB["Quility"].ToString()));
			
		}

		_vipBuys.Add(slot.transform.FindChild("frame1/Button").gameObject, vipLevel);
		UIEventListener.Get(slot.transform.FindChild("frame1/Button").gameObject).onClick = OnVipBuyBtn;
		
		Hashtable shopTable = sdConfDataMgr.Instance().GetShopsTable();
		if(shopTable != null)
		{
			Hashtable goods =  shopTable[vipUpgrade.GoodsId.ToString()] as Hashtable;
			int levelLimit = int.Parse(goods["LevelLimit"].ToString());
			int vipLimit = int.Parse(goods["VipLimit"].ToString());

			if(sdGameLevel.instance.mainChar.Level < levelLimit)
			{
				slot.transform.FindChild("frame1/Button").gameObject.SetActive(false);
				slot.transform.FindChild("frame1/Text").gameObject.SetActive(true);

				slot.transform.FindChild("frame1/Text").GetComponent<UILabel>().text = 
					"角色等级" + levelLimit.ToString() + "才能购买";
			}
			else if((int)sdMallManager.Instance.m_iCurrentVIPLevel < vipLimit)
			{
				slot.transform.FindChild("frame1/Button").gameObject.SetActive(false);
				slot.transform.FindChild("frame1/Text").gameObject.SetActive(true);
				
				slot.transform.FindChild("frame1/Text").GetComponent<UILabel>().text = 
					"VIP等级" + vipLimit.ToString() + "才能购买";
			}
			else
			{
				slot.transform.FindChild("frame1/Button").gameObject.SetActive(true);
				slot.transform.FindChild("frame1/Text").gameObject.SetActive(false);
			}

			int boughtCount = 0;
			if (sdMallManager.Instance.m_ProductBoughtCount.ContainsKey((uint) vipUpgrade.GoodsId))
				boughtCount = (int) sdMallManager.Instance.m_ProductBoughtCount[(uint) vipUpgrade.GoodsId];
			
			int boughtLimit = int.Parse(goods["LimitationNum"].ToString());
			if( boughtLimit!=0 && boughtCount>=boughtLimit)
			{
				slot.transform.FindChild("frame1/Button").GetComponent<UIButton>().enabled = false;
				slot.transform.FindChild("frame1/Button").GetComponent<UIButton>().UpdateColor(false, true);
			}
			else
			{
				slot.transform.FindChild("frame1/Button").GetComponent<UIButton>().enabled = true;
				slot.transform.FindChild("frame1/Button").GetComponent<UIButton>().UpdateColor(true, true);
			}
		}

		slot.transform.FindChild("frame1/CoinNum/SaleCash").GetComponent<UILabel>().text = vipUpgrade.SaleCash.ToString();
		slot.transform.FindChild("frame1/CoinNum/CastCash").GetComponent<UILabel>().text = vipUpgrade.CastCash.ToString();
	}

	void RefreshVipDesc(GameObject slot, int vipLevel)
	{
		slot.transform.FindChild("frame2/Head/Title").GetComponent<UILabel>().text =
			"VIP" + vipLevel.ToString() + "  尊享特权";
		
		Hashtable vipUpgradeDB = sdConfDataMgr.Instance().m_vipUpgradeDB;
		VipUpgrade vipUpgrade = vipUpgradeDB[vipLevel] as VipUpgrade;


		GameObject pointViewList = null;
		if(slot.transform.FindChild("frame2/PointViewList"))
		{
			pointViewList = slot.transform.FindChild("frame2/PointViewList").gameObject;
			GameObject.Destroy(pointViewList);	
		}
		
		pointViewList = new GameObject("PointViewList");
		pointViewList.transform.parent = slot.transform.FindChild("frame2");
		pointViewList.transform.localPosition = Vector3.zero;
		pointViewList.transform.localScale = new Vector3(1,1,1);


		string[] items = vipUpgrade.VipDescription.Split(';');
		int lineCounter = 0;
		foreach(string item in items)
		{
			GameObject pointViewTemplate = slot.transform.FindChild("frame2/PointView").gameObject;
			
			GameObject pointView = GameObject.Instantiate (pointViewTemplate) as GameObject;
			pointView.transform.parent = pointViewList.transform;
			pointView.transform.FindChild("Text").GetComponent<UILabel>().text = item;

			pointView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			pointView.transform.localPosition = new Vector3(-320.0f, 150.0f - lineCounter * 38.0f, 0.0f);
			pointView.SetActive(true);

			lineCounter++;
		}
	}

	void OnVipBuyBtn(GameObject go)
	{
		if(go.GetComponent<UIButton>().isEnabled == false)
		{
			sdUICharacter.Instance.ShowMsgLine("抱歉，无法购买！", Color.white);
			return;
		}

		int vipBuyLevel = _vipBuys[go];
		Hashtable vipUpgradeDB = sdConfDataMgr.Instance().m_vipUpgradeDB;
		VipUpgrade vipUpgrade = vipUpgradeDB[vipBuyLevel] as VipUpgrade;
		
		sdMallMsg.Send_CS_SHOP_BUY_GOODS_REQ((uint)vipUpgrade.GoodsId, 1);

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


			_vipGoods.Clear();
			_vipBuys.Clear();
			for(int i=0; i<vipUpgradeDB.Count; i++)
			{
				RefreshItemView(i);				
			}
			
			if(resetPos)
			{
				m_goTabRoot.transform.FindChild("Panel").gameObject.SetActive(true);
				m_goTabRoot.transform.FindChild("Panel").gameObject.GetComponent<UIDraggablePanel>().ResetPosition();
				m_goTabRoot.transform.FindChild("Panel/PanelGrid").GetComponent<UICenterOnChild>().Recenter();
			}						
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