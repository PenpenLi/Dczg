using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MallTab : TabBase
{

	public MallTab(GameObject goTabRoot):base(goTabRoot)
	{

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
			GameObject existItem = m_goTabRoot.transform.FindChild("Panel/MallPanelGrid/"+itemName).gameObject;
			return existItem;
		}
		catch(Exception e)
		{
			GameObject newItem = GameObject.Instantiate (itemTemplate) as GameObject;
			if(newItem)
			{
				newItem.transform.name = itemName;
				newItem.transform.parent = m_goTabRoot.transform.FindChild("Panel/MallPanelGrid");
				newItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				newItem.SetActive(true);
			}
			
			return newItem;
		}
	}

	void RefreshItemView (int slot, int productID)
	{

		Hashtable productDB = sdConfDataMgr.Instance().GetShopProduct(productID.ToString());
		if (productDB == null)
			return;

		// 设置item位置并显示
		GameObject item = CreateNewItem(slot);
		item.transform.localPosition = new Vector3(slot*305, 0f, 0f);
		item.gameObject.SetActive(true);

		UILabel name = item.transform.FindChild("LabelName").GetComponent<UILabel>();
		name.text = productDB["GoodsName"].ToString();
		int nameColor = int.Parse(productDB["NameColour"].ToString());
		if (nameColor == 0)
			name.color = Color.white;
		else if (nameColor == 1)
			name.color = Color.green;
		else if (nameColor == 2)
			name.color = Color.blue;
		else if (nameColor == 3)
			name.color = Color.magenta;
		else if (nameColor == 4)
			name.color = Color.yellow;
		
		item.transform.FindChild("LabelDescription").GetComponent<UILabel>().text =
			 productDB["ItemDisplay"].ToString();
		
		GameObject bestSell = item.transform.FindChild("BestSell").gameObject;
		GameObject buyLimited = item.transform.FindChild("BuyLimited").gameObject;
		int goodState = int.Parse(productDB["GoodsState"].ToString());
		if (goodState == 1)
		{
			bestSell.SetActive(true);
			buyLimited.SetActive(false);
		}
		else if (goodState == 2)
		{
			bestSell.SetActive(false);
			buyLimited.SetActive(true);
		}
		else if (goodState == 0)
		{
			bestSell.SetActive(false);
			buyLimited.SetActive(false);
		}
		
		GameObject specialPrice = item.transform.FindChild("SpecialPrice").gameObject;
		UILabel priceNormal = item.transform.FindChild("PriceNormal").GetComponent<UILabel>();
		UILabel priceSpecial = item.transform.FindChild("PriceSpecial").GetComponent<UILabel>();
		UISprite line = priceNormal.transform.FindChild("Line").GetComponent<UISprite>();
		int saleState = int.Parse(productDB["IfSale"].ToString());
		if (saleState == 0)
		{
			specialPrice.SetActive(false);
			priceSpecial.gameObject.SetActive(false);
			priceNormal.gameObject.SetActive(true);
			line.gameObject.SetActive(false);
			priceNormal.text = productDB["Price"].ToString();
			priceNormal.color = new Color(234/255f, 204/255f, 161/255f);
		}
		else if (saleState == 1)
		{
			specialPrice.SetActive(true);
			priceSpecial.gameObject.SetActive(true);
			priceNormal.gameObject.SetActive(true);
			priceNormal.text = productDB["Price"].ToString();
			priceNormal.color = new Color(135/255f, 116/255f ,90/255f);
			priceSpecial.text = productDB["SalePrice"].ToString();
			priceSpecial.color = new Color(234/255f, 204/255f, 161/255f);
			line.gameObject.SetActive(true);
			line.width = priceNormal.width;
		}
		
		// 图标，表示使用哪种货币购买
		UISprite coinSprite = item.transform.FindChild("Coin").GetComponent<UISprite>();
		int costType = int.Parse(productDB["CostType"].ToString());
		if (costType == 0)
			coinSprite.spriteName = sdMallManager.HUIZHANG_SPRITE;
		else
			coinSprite.spriteName = sdMallManager.JINBI_SPRITE;
		
		UILabel limitInfo = item.transform.Find("LimitInfo").GetComponent<UILabel>();
		UILabel limitToday = item.transform.Find("LimitToday").GetComponent<UILabel>();
		int limitationType = int.Parse(productDB["LimitationType"].ToString());
		int limitationNum = int.Parse(productDB["LimitationNum"].ToString());
		if (limitationType == 0)
		{
			limitInfo.gameObject.SetActive(false);
			limitToday.gameObject.SetActive(false);
		}
		else
		{
			int boughtCount = 0;
			if (sdMallManager.Instance.m_ProductBoughtCount.ContainsKey((uint) productID))
				boughtCount = (int) sdMallManager.Instance.m_ProductBoughtCount[(uint) productID];
			
			if (limitationType == 1)
			{
				limitToday.gameObject.SetActive(false);
				limitInfo.gameObject.SetActive(true);
				limitInfo.text = "仅限购买： " + (limitationNum - boughtCount).ToString() 
					+ " / " + limitationNum.ToString();
			}
			else if (limitationType == 2)
			{
				limitToday.gameObject.SetActive(true);
				limitInfo.gameObject.SetActive(false);
				limitToday.text = "今日限购： " + (int.Parse(productDB["LimitationNum"].ToString()) - boughtCount).ToString() 
					+ " / " + productDB["LimitationNum"].ToString();
			}
		}
		
		UIButton buyBtn = item.transform.FindChild("ButtonBuy").GetComponent<UIButton>();
		UILabel buyLbl = item.transform.FindChild("LabelBuy").GetComponent<UILabel>();
		UILabel boughtLbl = item.transform.FindChild("LabelBought").GetComponent<UILabel>();
		int levelLimit = int.Parse(productDB["LevelLimit"].ToString());
		int VIPLimit = int.Parse(productDB["VipLimit"].ToString());
		int isBatch = int.Parse(productDB["IfBatch"].ToString());
		
		bool buyBtnShow = true;
		if (levelLimit != 0 && levelLimit > sdGameLevel.instance.mainChar["Level"])
		{
			// 等级不足
			buyBtn.gameObject.SetActive(false);
			boughtLbl.gameObject.SetActive(false);
			buyLbl.gameObject.SetActive(true);
			buyLbl.text = productDB["LevelLimit"].ToString() + "级才能购买";
			buyBtnShow = false;
		}
		else if (limitationType != 0)
		{
			// 已经购买的该商品数量
			int boughtCount = 0;
			if (sdMallManager.Instance.m_ProductBoughtCount.ContainsKey((uint) productID))
				boughtCount = (int) sdMallManager.Instance.m_ProductBoughtCount[(uint) productID];
			
			if (boughtCount >= limitationNum)
			{
				// 购买数量超出限购数量
				buyBtn.gameObject.SetActive(false);
				buyLbl.gameObject.SetActive(false);
				boughtLbl.gameObject.SetActive(true);
				buyBtnShow = false;
			}
		}
		else if (VIPLimit != 0 && VIPLimit > sdMallManager.Instance.m_iCurrentVIPLevel)
		{
			//VIP等级不够
			buyBtn.gameObject.SetActive(false);
			boughtLbl.gameObject.SetActive(false);
			buyLbl.gameObject.SetActive(true);
			buyLbl.text = "VIP" + productDB["VipLimit"].ToString() + "才能购买";
			buyBtnShow = false;
		}
		
		if (buyBtnShow)
		{
			buyBtn.gameObject.SetActive(true);
			buyBtn.gameObject.GetComponent<sdMallButton>().SetInfo(productID, productDB);
			buyLbl.gameObject.SetActive(false);
			boughtLbl.gameObject.SetActive(false);
		}
		
		UISprite productSprite = item.transform.FindChild("Product").GetComponent<UISprite>();
		
		int itemID = int.Parse(productDB["ItemId"].ToString());
		Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(itemID.ToString());
		if (itemInfo != null) 
		{			
			sdProductIcon icon = productSprite.gameObject.GetComponent<sdProductIcon> ();
			icon.SetInfo (productID, productDB);
			
			// 取图片资源信息
			UIAtlas atlas = sdConfDataMgr.Instance ().GetItemAtlas (itemInfo ["IconID"].ToString ());
			if (atlas != null) 
			{
				productSprite.gameObject.SetActive (true);
				productSprite.atlas = atlas;
				productSprite.spriteName = itemInfo ["IconPath"].ToString ();
			} 
			else 
			{
				icon.LoadIcon (itemInfo ["IconID"].ToString ());
				productSprite.gameObject.SetActive (false);
			}

			item.transform.FindChild("Product/qualityFrame").GetComponent<UISprite>().color = 
				sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemInfo["Quility"].ToString()));
		} 
		else 
		{
			productSprite.gameObject.SetActive (false);
		}

	
	}

	override public void RefreshUserInterface(bool resetPos)
	{
		if(_isActive)
		{
			for(int i=0; i<sdMallManager.Instance.m_aNormalProducts.Count; i++)
			{
				int key = int.Parse(sdMallManager.Instance.m_aNormalProducts[i].ToString ());
				RefreshItemView(i, key);
			}

			if(resetPos)
			{
				m_goTabRoot.transform.FindChild("Panel").gameObject.SetActive(true);
				m_goTabRoot.transform.FindChild("Panel").gameObject.GetComponent
					<UIDraggablePanel>().ResetPosition();
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