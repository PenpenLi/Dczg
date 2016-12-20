using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdPVPRewards : MonoBehaviour
{
	GameObject pvpRewards = null;
	GameObject pvpRewardsItem = null;

    void Awake()
    {
        pvpRewards = GameObject.Find("panel_pvpreward");
        pvpRewardsItem = GameObject.Find("pvprewarditem");
    }

	void Start()
	{
	}

    void OnClick()
    {

    }

    public void Refresh()
    {
        if (pvpRewardsItem == null) return;
        pvpRewardsItem.SetActive(false);
        List<Hashtable> rewardsTable = sdConfDataMgr.Instance().GetList("pvprewards");
        for (int index = 0; index < rewardsTable.Count; ++index)
        {
            GameObject uiItem = null;
            uiItem = GameObject.Instantiate(pvpRewardsItem) as GameObject;
            uiItem.transform.parent = pvpRewards.transform;
            uiItem.transform.localPosition = new Vector3(0, 0 - 140*index, 0);
            uiItem.transform.localScale = Vector3.one;
            uiItem.transform.localRotation = Quaternion.identity;
            uiItem.SetActive(true);
            Hashtable table = rewardsTable[index];
            if (index <= 2)
            {
                uiItem.transform.FindChild("Sprite_rank").gameObject.SetActive(true);
                UISprite sprite_rank = uiItem.transform.FindChild("Sprite_rank").GetComponent<UISprite>();
                sprite_rank.spriteName = "if-n" + (index+1).ToString();
                uiItem.transform.FindChild("Label_reward1").gameObject.SetActive(false);
            }
            else
            {
                uiItem.transform.FindChild("Label_reward1").gameObject.SetActive(true);
				UISprite sprite_rank = uiItem.transform.FindChild("Sprite_rank").GetComponent<UISprite>();
				sprite_rank.spriteName = "if-n5";
                string key = table["key"] as string;
                uiItem.transform.FindChild("Label_reward1").GetComponent<UILabel>().text = key;
            }

            string value = table["value"] as string;
            string[] items = value.Split(new char[] { ';' });
            int j = 1;
            for (j = 1; j <= items.Length; ++j)
            {
                sdSlotIcon sloticon = uiItem.transform.FindChild("Sprite_rewardicon1_" + j).GetComponent<sdSlotIcon>();
                sloticon.panel = PanelType.Panel_PVPRankReward;
                string[] item = items[j - 1].Split(new char[] { '-' });
                if (item.Length == 2)
                {
                    int templateID = int.Parse(item[0]);
					Hashtable iteminfo = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
                    if (iteminfo == null)
                        break;
                    UISprite sprite_item = uiItem.transform.FindChild("Sprite_rewardicon1_" + j).GetComponent<UISprite>();
                    sprite_item.atlas = sdConfDataMgr.Instance().commonAtlas;
                    sprite_item.spriteName = "IconFrame0";
					sloticon.SetInfo(templateID.ToString(), iteminfo);
                    //if (iteminfo.ContainsKey("Class") && int.Parse(iteminfo["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item &&
                    //    iteminfo.ContainsKey("SubClass") && int.Parse(iteminfo["SubClass"].ToString()) == 2)
                    //    uiItem.transform.FindChild("Sprite_rewardicon1_" + j).GetComponent<UISprite>().spriteName = "IconFrame0-cp";
                    //else
                    //    uiItem.transform.FindChild("Sprite_rewardicon1_"+j).GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder( int.Parse(iteminfo["Quility"].ToString()) );
                    UILabel label_num = uiItem.transform.FindChild("Sprite_rewardicon1_" + j).FindChild("Label_num").GetComponent<UILabel>();
                    int nCount = int.Parse(item[1]);
                    if (nCount > 1) label_num.text = nCount.ToString();

                    UISprite sprite_quality = uiItem.transform.FindChild("Sprite_rewardicon1_" + j).FindChild("Sprite").GetComponent<UISprite>();
                    sprite_quality.atlas = sdConfDataMgr.Instance().commonAtlas;
                    sprite_quality.spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(iteminfo["Quility"].ToString()));

                }
            }
            for (; j <= 2; j++)
            {
                uiItem.transform.FindChild("Sprite_rewardicon1_" + j).gameObject.SetActive(false);
            }
        }
    }
}