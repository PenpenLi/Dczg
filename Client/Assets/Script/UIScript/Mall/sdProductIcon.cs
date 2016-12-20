using UnityEngine;
using System.Collections;

public class sdProductIcon : MonoBehaviour {

	private int m_iProductId;
	private Hashtable m_productInfo;

	public void SetInfo(int id, Hashtable info)
	{
		m_iProductId = id;
		m_productInfo = info;
	}

    public void LoadIcon(string id)
    {
        sdConfDataMgr.Instance().LoadItemAtlas(id, OnAtalasLoadFinished);
    }

    public void OnAtalasLoadFinished(ResLoadParams param, UnityEngine.Object obj)
	{
		if (m_productInfo == null)
			return;

		Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(m_productInfo["ItemId"].ToString());
		if (itemInfo == null)
			return;

		UIAtlas atlas = obj as UIAtlas;
		if (atlas == null)
			return;
		
		UISprite productSprite = gameObject.GetComponent<UISprite> ();
		productSprite.atlas = atlas;
		productSprite.gameObject.SetActive(true);
		productSprite.spriteName = itemInfo["IconPath"].ToString();
	}

	void OnClick()
	{
		if (m_productInfo == null)
			return;

		if (int.Parse(m_productInfo["GoodsClass"].ToString()) == (int) sdMallManager.GoodsClass.normalGoods)
			sdUICharacter.Instance.ShowTip(TipType.TempItem, m_productInfo["ItemId"].ToString());
	}
}
