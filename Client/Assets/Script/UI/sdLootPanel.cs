using UnityEngine;
using System.Collections;
using System;

public enum LootType
{
	Item,
	Money
}

public class sdLootPanel : MonoBehaviour
{
	public GameObject copyItem = null;
	private int itemNum = 0;
	
	public int MaxLootItem = 2;
	
	Hashtable itemList = new Hashtable();
	
	void Start()
	{
		if (copyItem != null)
		{
			sdLootItem item = copyItem.GetComponent<sdLootItem>();
			if (item != null)
			{
				item.index = 0;
				itemList.Add(0, item);
			}
			copyItem.SetActive(false);	
		}
		itemNum = 0;

        sdUICharacter.Instance.lootPanel = this;
	}
	
	public void AddItem(LootType type, int info)
	{
        if (type == LootType.Item)
        {
            GameObject tempItem = null;
            if (itemNum == 0)
            {
                sdLootItem item = itemList[0] as sdLootItem;
                item.SetInfo(type, info);
                item.gameObject.SetActive(true);
                itemNum++;
            }
            else if (itemNum < MaxLootItem)
            {
                tempItem = GameObject.Instantiate(copyItem) as GameObject;
                tempItem.transform.parent = copyItem.transform.parent;
                tempItem.transform.localScale = new Vector3(1, 1, 1);
                tempItem.transform.localPosition = copyItem.transform.localPosition;
                tempItem.SetActive(true);
                sdLootItem item = tempItem.GetComponent<sdLootItem>();
                item.SetInfo(type, info);
                item.index = itemNum;
                itemList.Add(itemNum, item);
                itemNum++;
            }
            else
            {
                sdLootItem changeItem = null;
                for (int i = 0; i < MaxLootItem; ++i)
                {
                    sdLootItem item = itemList[i] as sdLootItem;
                    if (i == 0)
                    {
                        item.index = MaxLootItem - 1;
                        changeItem = item;
                        item.SetInfo(type, info);
                        itemList[i] = null;
                    }
                    else if (i != MaxLootItem - 1)
                    {
                        item.index = i - 1;
                        itemList[i - 1] = item;
                        itemList[i] = null;
                    }
                    else
                    {
                        item.index = i - 1;
                        itemList[i - 1] = item;
                        itemList[i] = changeItem;
                    }
                }
            }
        }
        else
        {
            GameObject tempItem = null;
            if (itemNum == 0)
            {
                sdLootItem item = itemList[0] as sdLootItem;
                item.SetInfo(type, info);
                item.gameObject.SetActive(true);
                itemNum++;
            }
            else if (itemNum < MaxLootItem)
            {
                bool flag = false;
                for (int i = 0; i < itemNum; ++i)
                {
                    sdLootItem lootItem = itemList[i] as sdLootItem;
                    if (lootItem.lootType == LootType.Money && !lootItem.isFinish)
                    {
                        lootItem.RefreshMoney(info);
                        flag = true;
                    }
                }

                if (!flag)
                {
                    tempItem = GameObject.Instantiate(copyItem) as GameObject;
                    tempItem.transform.parent = copyItem.transform.parent;
                    tempItem.transform.localScale = new Vector3(1, 1, 1);
                    tempItem.transform.localPosition = copyItem.transform.localPosition;
                    tempItem.SetActive(true);
                    sdLootItem item = tempItem.GetComponent<sdLootItem>();
                    item.SetInfo(type, info);
                    item.index = itemNum;
                    itemList.Add(itemNum, item);
                    itemNum++;
                }
            }
            else
            {
                bool flag = false;
                for (int i = 0; i < itemNum; ++i)
                {
                    sdLootItem lootItem = itemList[i] as sdLootItem;
                    if (lootItem.lootType == LootType.Money && !lootItem.isFinish)
                    {
                        lootItem.RefreshMoney(info);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    sdLootItem changeItem = null;
                    for (int i = 0; i < MaxLootItem; ++i)
                    {
                        sdLootItem item = itemList[i] as sdLootItem;
                        if (i == 0)
                        {
                            item.index = MaxLootItem - 1;
                            changeItem = item;
                            item.SetInfo(type, info);
                            itemList[i] = null;
                        }
                        else if (i != MaxLootItem - 1)
                        {
                            item.index = i - 1;
                            itemList[i - 1] = item;
                            itemList[i] = null;
                        }
                        else
                        {
                            item.index = i - 1;
                            itemList[i - 1] = item;
                            itemList[i] = changeItem;
                        }
                    }
                }
            }
        }
		
		Reposition();
	}
	
	void Reposition()
	{
		foreach(DictionaryEntry loot in itemList)
		{
			sdLootItem item = loot.Value as sdLootItem;
			Vector3 pos = item.transform.localPosition;
			pos.y = (MaxLootItem - item.index)*90;
			item.transform.localPosition = pos;
		}
	}
	
	void Update()
	{
		if (itemNum > 0)
		{
			foreach(DictionaryEntry loot in itemList)
			{
				sdLootItem item = loot.Value as sdLootItem;
				if (item.isOut || item.lifetiem <= 0) continue;
				float deltTime = Time.time - item.lifetiem;
				if (deltTime >= 3)
				{
					item.gameObject.GetComponent<TweenAlpha>().enabled = true;
					item.gameObject.GetComponent<TweenAlpha>().Play();
					item.isOut = true;
				}
			}
		}
	}
}
