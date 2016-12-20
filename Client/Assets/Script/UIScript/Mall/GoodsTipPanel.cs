using UnityEngine;
using System.Collections;

public class GoodsTipPanel : MonoBehaviour 
{
    public void ShowGoodsConfirmInfo(int id)
    {
        Transform confirmBtnTransform = transform.FindChild("ConfirmBtn");
		if (confirmBtnTransform != null)
		{
			GoodsTipBtnHandler handler = confirmBtnTransform.GetComponent<GoodsTipBtnHandler> ();
			handler.m_iProductId = id;
		}

        Hashtable productInfo = sdConfDataMgr.Instance().GetShopProduct(id.ToString());
        Transform tProductName0 = transform.FindChild("ProductName0");
        if (tProductName0 != null)
        {
            UILabel productName0 = tProductName0.GetComponent<UILabel>();
            productName0.text = productInfo["GoodsName"].ToString();
            int nameColor = int.Parse(productInfo["NameColour"].ToString());
            if (nameColor == 0)
                productName0.color = Color.white;
            else if (nameColor == 1)
                productName0.color = Color.green;
            else if (nameColor == 2)
                productName0.color = Color.blue;
            else if (nameColor == 3)
                productName0.color = Color.magenta;
            else if (nameColor == 4)
                productName0.color = Color.yellow;
        }

        Transform tProductName1 = transform.FindChild("ProductName1");
        if (tProductName1 != null)
        {
            UILabel productName1 = tProductName1.GetComponent<UILabel>();
            productName1.text = string.Format("购买{0}需要花费", productInfo["GoodsName"]);
        }

        Transform tDescription = transform.FindChild("Description");
        if (tDescription != null)
        {
            UILabel description = tDescription.GetComponent<UILabel>();
            description.text = productInfo["ItemDisplay"].ToString();
        }

        Transform tIcon = transform.FindChild("Icon");
        if (tIcon != null)
        {
            UISprite icon = tIcon.GetComponent<UISprite>();

            int itemID = int.Parse(productInfo["ItemId"].ToString());
            Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(itemID.ToString());
            if (itemInfo != null)
            {
                sdProductIcon iconSprite = icon.gameObject.GetComponent<sdProductIcon>();
                iconSprite.SetInfo(id, productInfo);

                // 取图片资源信息
                UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(itemInfo["IconID"].ToString());
                if (atlas != null)
                {
                    icon.gameObject.SetActive(true);
                    icon.atlas = atlas;
                    icon.spriteName = itemInfo["IconPath"].ToString();
                }
                else
                {
                    iconSprite.LoadIcon(itemInfo["IconID"].ToString());
                    icon.gameObject.SetActive(false);
                }
            }
            else
                icon.gameObject.SetActive(false);
        }

        Transform tBadge = transform.FindChild("Badge");
        if (tBadge != null)
        {
            UISprite coinSprite = tBadge.GetComponent<UISprite>();
            int costType = int.Parse(productInfo["CostType"].ToString());
            if (costType == 0)
                coinSprite.spriteName = sdMallManager.HUIZHANG_SPRITE;
            else
                coinSprite.spriteName = sdMallManager.JINBI_SPRITE;
        }

        Transform tTotalPrice = transform.FindChild("TotalPrice");
        if (tTotalPrice != null)
        {
            UILabel totalPrice = tTotalPrice.GetComponent<UILabel>();
            int ifSale = int.Parse(productInfo["IfSale"].ToString());
            if (ifSale == 1)
                totalPrice.text = productInfo["SalePrice"].ToString();
            else
                totalPrice.text = productInfo["Price"].ToString();
        }
    }
}
