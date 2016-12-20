
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;


class sdTreasureBtn : MonoBehaviour
{
	public GameObject itemIcon = null;
	
	float lifeTime = 0;
	bool goNext = false;
	void OnClick()
	{
		if (sdUICharacter.Instance.selectTreasure >= sdUICharacter.Instance.fightScore) return;
		ShowItem();
		//if (sdUICharacter.Instance.selectTreasure >= sdUICharacter.Instance.fightScore) goNext = true;
		
	}

    public GameObject cardEffect = null;

    public UISprite bg = null;
	public UISprite bgGlow = null;

    public void OnChangeBg()
    {
        bg.spriteName = "k1";
		bgGlow.spriteName = "js-d1";
		bgGlow.gameObject.transform.localPosition = new Vector3(0,108,0);
		bgGlow.width = 20;
		bgGlow.height = 14;

        itemIcon.SetActive(true);
        cardEffect.transform.parent = cardEffect.transform.parent.parent;
        cardEffect.transform.localScale = new Vector3(300, 300, 1);
        cardEffect.SetActive(true);
        TreasureInfo info = sdUICharacter.Instance.GetTreasure();
        sdUICharacter.Instance.selectTreasure++;
        if (info == null) return;

        int color = 0;
        if (info.id == (int)HeaderProto.ESpecialItemID.SPECIALITEMID_nonMoney)
        {
            //itemIcon.GetComponent<UISprite>().spriteName = "";
            itemIcon.transform.Find("icon").GetComponent<UISprite>().spriteName = "icon_jinb";
            itemIcon.transform.FindChild("name").GetComponent<UILabel>().text = info.count.ToString();
            color = 1;
        }
        else if (info.id == (int)HeaderProto.ESpecialItemID.SPECIALITEMID_nonCash)
        {
            //itemIcon.GetComponent<UISprite>().spriteName = "";
            itemIcon.transform.Find("icon").GetComponent<UISprite>().spriteName = "icon_xz";
            itemIcon.transform.FindChild("name").GetComponent<UILabel>().text = info.count.ToString();
            color = 4;
        }
        else if (info.id == (int)HeaderProto.ESpecialItemID.SPECIALITEMID_cash)
        {
            //itemIcon.GetComponent<UISprite>().spriteName = "";
            itemIcon.transform.Find("icon").GetComponent<UISprite>().spriteName = "icon_xzb";
            itemIcon.transform.FindChild("name").GetComponent<UILabel>().text = info.count.ToString();
            color = 3;
        }
        else
        {
            Hashtable item = sdConfDataMgr.Instance().GetItemById(info.id.ToString());
            if (item == null) return;
            color = int.Parse(item["Quility"].ToString());
            itemIcon.GetComponent<sdSlotIcon>().SetInfo(info.id.ToString(), item);
            string name = item["ShowName"].ToString();
            itemIcon.transform.FindChild("name").GetComponent<UILabel>().text = name;
            sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Item);
        }

        itemIcon.GetComponentInChildren<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(color);
        if (!info.isTemp)
        {
            CliProto.CS_OPEN_TREASURE_CHEST_REQ refMSG = new CliProto.CS_OPEN_TREASURE_CHEST_REQ();
            refMSG.m_ItemIndexID = (byte)info.index;
            SDNetGlobal.SendMessage(refMSG);
        }
        else
        {
            itemIcon.transform.FindChild("Label").gameObject.SetActive(false);
        }

        color = color - 1;
        if (color < 0) color = 0;

    }
	
	bool hasPlay = false;
	public void ShowItem()
	{
		if (hasPlay) return;
		GetComponent<BoxCollider>().enabled = false;
        TweenScale[] list = GetComponents<TweenScale>();
        foreach (TweenScale ts in list)
        {
            ts.enabled = true;
        }
		hasPlay = true;
	}
	
	bool hasShow = false;
	bool hasfinish = false;
	void Update()
	{
        if (sdUICharacter.Instance.selectTreasure >= sdUICharacter.Instance.fightScore)
		{
			lifeTime += Time.deltaTime;
			
			if (lifeTime >= 1.2 && !hasShow)
			{
				sdTreasureBtn[] list = transform.parent.parent.GetComponentsInChildren<sdTreasureBtn>();	
				foreach(sdTreasureBtn btn in list)
				{
					btn.ShowItem();
				}
				hasShow = true;
			}

            if (lifeTime >= 2.5)
            {
                sdUICharacter.Instance.JiesuanGetAllItem();
                sdUICharacter.Instance.selectTreasure = 0;
            }
		}
	}
}