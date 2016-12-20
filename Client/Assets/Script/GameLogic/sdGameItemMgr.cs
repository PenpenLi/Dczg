using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum ItemFilter
{
    All = -1,
    Weapon = 0,
    Armor,
    Shipin,
    Other,
}

public class sdGameItemMgr : Singleton<sdGameItemMgr>
{
	public const int testIndex = 999999;
	public Hashtable itemDB = new Hashtable();			//< 主角装备表aa

    public sdGameItem tempItem = null;
	public sdGameItem upItem = null;
	List<string> itemUpList = new List<string>();

	public Hashtable suitInfo = new Hashtable();

	public Dictionary<string, int> selGemList = new Dictionary<string, int>();
    public int gemIndex = -1;
    public int createGemId = 0;
    public int createGemCount = 0;
    public int maxBagNum = 0;

	public bool init(bool testMode)
	{
		if(testMode)
		{
			//< 测试套装Iti
			sdGameItem item = null;
			
			item = new sdGameItem();
			item.templateID = 0 + testIndex;
			item.instanceID = 1000 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$warrior_heavy_050lv_hero/heavy_050lv_hero.fbx";
			item.mdlPartName = "upper";
			item.anchorNodeName = "";
			item.equipPos = 0;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 1 + testIndex;
			item.instanceID = 1001 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$warrior_heavy_050lv_hero/heavy_050lv_hero.fbx";
			item.mdlPartName = "lower";
			item.anchorNodeName = "";
			item.equipPos = 1;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 2 + testIndex;
			item.instanceID = 1002 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$warrior_heavy_050lv_hero/heavy_050lv_hero.fbx";
			item.mdlPartName = "face";
			item.anchorNodeName = "";
			item.equipPos = 2;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 3 + testIndex;
			item.instanceID = 1003 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$warrior_heavy_050lv_hero/heavy_050lv_hero.fbx";
			item.mdlPartName = "upperback";
			item.anchorNodeName = "";
			item.equipPos = 3;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 4 + testIndex;
			item.instanceID = 1004 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$warrior_heavy_050lv_hero/heavy_050lv_hero.fbx";
			item.mdlPartName = "hands";
			item.anchorNodeName = "";
			item.equipPos = 4;
			itemDB[item.instanceID] = item;
			
			//< 测试武器
			item = new sdGameItem();
			item.templateID = 5 + testIndex;
			item.instanceID = 1005 + testIndex;
			item.mdlPath = "Model/Weapons/Warrior/$heavy_050lv_hero_weapon/heavy_050lv_hero_weapon.fbx";
			item.mdlPartName = "longsword_050lv_01_o01";
			item.anchorNodeName = "dummy_right_weapon_at";
			item.equipPos = 5;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 6 + testIndex;
			item.instanceID = 1006 + testIndex;
			item.mdlPath = "Model/Weapons/Warrior/$heavy_050lv_hero_weapon/heavy_050lv_hero_weapon.fbx";
			item.mdlPartName = "heavyshield_050lv_01_o01";
			item.anchorNodeName = "dummy_left_shield_at";
			item.equipPos = 6;
			itemDB[item.instanceID] = item;
			
			//< 测试裸模
			item = new sdGameItem();
			item.templateID = 7 + testIndex;
			item.instanceID = 1007 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$base_hero/base_hero.fbx";
			item.mdlPartName = "upper";
			item.anchorNodeName = "";
			item.equipPos = 0;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 8 + testIndex;
			item.instanceID = 1008 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$base_hero/base_hero.fbx";
			item.mdlPartName = "lower";
			item.anchorNodeName = "";
			item.equipPos = 1;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 9 + testIndex;
			item.instanceID = 1009 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$base_hero/base_hero.fbx";
			item.mdlPartName = "face";
			item.anchorNodeName = "";
			item.equipPos = 2;
			itemDB[item.instanceID] = item;
			
			item = new sdGameItem();
			item.templateID = 10 + testIndex;
			item.instanceID = 1010 + testIndex;
			item.mdlPath = "Model/Mdl_mainChar_0/$base_hero/base_hero.fbx";
			item.mdlPartName = "hands";
			item.anchorNodeName = "";
			item.equipPos = 4;
			itemDB[item.instanceID] = item;
		}
		
		return true;
	}

    public void Clear()
    {
        itemDB.Clear();

	    upItem = null;
	    itemUpList.Clear();

	    suitInfo.Clear();

	    selGemList.Clear();
        gemIndex = -1;
        createGemId = 0;
    }
	
	public bool finl()
	{
		return true;
	}
	
	public sdGameItem createItem(HeaderProto.SXITEM itemInfo, int bagIndex)
	{
        bool isUpdate = false;
		sdGameItem item = getItem(itemInfo.m_UID);
        if (item == null)
        {
            item = new sdGameItem();
        }
        else
        {
            isUpdate = true;
        }
		//item.slotIndex = GetAllItem(bagIndex,-1).Count;
		itemDB[itemInfo.m_UID] = item;
		item.templateID = itemInfo.m_TID;
		item.instanceID = itemInfo.m_UID;
		item.bagIndex = bagIndex;
		item.count = itemInfo.m_CT;
		item.upExp = itemInfo.m_EXP;
		item.upLevel = itemInfo.m_UP;
		int gemNum = itemInfo.m_GEMCount;
		item.gemNum = gemNum;
		item.gemList = new int[gemNum];
		for (int i = 0; i < gemNum; ++i)
		{
			item.gemList[i] = itemInfo.m_GEM[i].m_TID;
		}

		Hashtable info = sdConfDataMgr.Instance().GetItemById(itemInfo.m_TID.ToString());
		if (info != null)
		{
			item.mdlPath = info["Filename"].ToString();
			item .mdlPartName = info["FilePart"].ToString();
			
			item.anchorNodeName	=	sdGameActor.WeaponDummy(info["Character"].ToString());

			
			item.itemClass = int.Parse(info["Class"].ToString());
			item.subClass = int.Parse(info["SubClass"].ToString());
			item.level = int.Parse(info["Level"].ToString());
			item.quility = int.Parse(info["Quility"].ToString());
			item.equipPos = int.Parse(info["Character"].ToString());
            item.suitId = int.Parse(info["SuitID"].ToString());

            if (isUpdate)
            {
                bool toLock = (itemInfo.m_LK==1 ? true : false);
                if (toLock != item.isLock)
                {
                    if (toLock)
                    {
                        sdUICharacter.Instance.ShowMsgLine(string.Format("[{0}]{1}", info["ShowName"].ToString(), sdConfDataMgr.Instance().GetShowStr("HasLock")), Color.yellow);
                    }
                    else
                    {
                        sdUICharacter.Instance.ShowMsgLine(string.Format("[{0}]{1}", info["ShowName"].ToString(), sdConfDataMgr.Instance().GetShowStr("HasUnLock")), Color.yellow);
                    }
                }
            }
		}

        item.isLock = itemInfo.m_LK==1 ? true : false;
        item.isFormulaItem = sdConfDataMgr.Instance().CanItemLevelUp(item.templateID.ToString());
		if (bagIndex == (int)PanelType.Panel_Equip)
		{
			sdMainChar mc	=	sdGameLevel.instance.mainChar;
			if (mc != null)
			{
				item.takeOn(mc);
			}
			
		}

        item.score = sdConfDataMgr.Instance().GetItemScore(itemInfo.m_UID);
//		sdItemDesc desc = sdCSVMgr.instance.getItemInfo(item.templateID);
//		item.mdlPath = desc.mdlPath;
//		//...
//			
		return item;
	}	
	public sdGameItem getItem(UInt64 instanceID)
	{
		if(itemDB.ContainsKey(instanceID))
			return itemDB[instanceID] as sdGameItem;
		else
			return null;
	}
	
	public bool hasItem(string tid)
	{
		foreach(DictionaryEntry info in itemDB)
		{	
			sdGameItem item = info.Value as sdGameItem;
			if (item != null && item.templateID.ToString() == tid)
			{
				return true;	
			}
		}
		return false;
	}
    public bool HasItemCount(int id, int nNeedCount)
    {
        int nTmpCount = 0;

        foreach (DictionaryEntry info in itemDB)
        {
            sdGameItem item = info.Value as sdGameItem;
            if (item != null && item.templateID == id)
            {
                nTmpCount += item.count;
                if (nTmpCount >= nNeedCount)
                    return true;
            }
        }

        return false;
    }

    public int GetItemCount(int id)
    {
        int nCount = 0;
        foreach (DictionaryEntry info in itemDB)
        {
            sdGameItem item = info.Value as sdGameItem;
            if(item != null && item.templateID == id)
            {
                nCount += item.count;
            }
        }
        return nCount;
    }
	
	public UInt64 getItemUIDByTID(int iTid)
	{
		foreach(DictionaryEntry info in itemDB)
		{	
			sdGameItem item = info.Value as sdGameItem;
			if (item != null && item.templateID == iTid)
			{
				return item.instanceID;	
			}
		}
		return UInt64.MaxValue;
	}
	
	public sdGameItem getEquipItemByPos(int pos)
	{
		foreach(DictionaryEntry item in itemDB)
		{
			sdGameItem info = (sdGameItem)item.Value;
			if (info.equipPos == pos && info.bagIndex == (int)PanelType.Panel_Equip)
			{
				return info;	
			}
		}
		return null;	
	}
	
	public void RemoveItem(UInt64 uuId)
	{
		sdGameItem item = getItem(uuId);
		if (item.bagIndex == (int)PanelType.Panel_Equip)
		{
			sdMainChar mc	=	sdGameLevel.instance.mainChar;
			if (mc != null)
			{
				item.takeOff(mc);
				Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
				sdGameItem dummy = mc.getStartItem(int.Parse(info["Character"].ToString()));
				if(dummy != null)
				{
					dummy.takeOnNake(mc);	
				}
			}
		}
		itemDB.Remove(uuId);		
	}

	public void MoveItem(int fromPanel, int toPanel, UInt64 uuId, UInt64 uuOtherId)
	{
		sdMainChar mc	=	sdGameLevel.instance.mainChar;
		
		sdGameItem itemFrom = getItem(uuId);
		sdGameItem itemTo = getItem(uuOtherId);
		if(itemFrom!=null)
		{
			itemFrom.bagIndex	=	toPanel;
			if (toPanel == 0)
			{
				itemDB.Remove(uuId);	
			}
		}
		if(itemTo!=null)
		{
			itemTo.bagIndex		=	fromPanel;
		}
		
		if(fromPanel == (int)PanelType.Panel_Bag)
		{
			//从背包穿装备..
			if(toPanel ==	(int)PanelType.Panel_Equip)
			{
				if(itemTo!=null)
				{
					itemTo.takeOff(mc);
				}
				if(itemFrom!=null)
				{
					itemFrom.takeOn(mc);
				}
			}
			//出售装备..
			else if(toPanel == 0)
			{
				
			}
		}
		else if(fromPanel == (int)PanelType.Panel_Equip)
		{
			if(itemFrom!=null)
			{
				itemFrom.takeOff(mc);
			}
			Hashtable info = sdConfDataMgr.Instance().GetItemById(itemFrom.templateID.ToString());
			sdGameItem dummy = mc.getStartItem(int.Parse(info["Character"].ToString()));
			if(dummy != null)
			{
				dummy.takeOnNake(mc);	
			}
		}

		int type = 0;
		type |= fromPanel;
		type |= toPanel;
		sdSlotMgr.Instance.Notify(type);
	}
	
	public Hashtable GetAllItem(int bagIndex, int itemFilter)
	{
		Hashtable table = new Hashtable();
		foreach(DictionaryEntry item in itemDB)
		{
			sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == bagIndex)
			{
                if (itemFilter == (int)ItemFilter.All)
                {
                    table[item.Key] = info;	
                }
                else if (itemFilter == (int)ItemFilter.Other)
                {
                    if (info.itemClass != 0 && info.itemClass != 1 && info.itemClass != 2)
                    {
                        table[item.Key] = info;	
                    }
                }
                else if (itemFilter == info.itemClass)
                {
                    table[item.Key] = info;	
                }
			}
		}
		
		return table;	
	}

	public bool AddItemUpId(string id)
	{
		if (itemUpList.Count >= 6) return false;
		itemUpList.Add(id);
		return true;
	}

	public void RemoveItemUpId(string id)
	{
		itemUpList.Remove(id);
	}

	public void ClearItemUpId()
	{
		itemUpList.Clear();
	}

	public bool hasSelectItemUp(string id)
	{
		return itemUpList.Contains(id);
	}

	public ulong[] GetItemUpId()
	{
		int num = itemUpList.Count;
		ulong[] list = new ulong[num];
		for(int i = 0; i < num; ++i)
		{
			list[i] = ulong.Parse(itemUpList[i]);
		}

		return list;
	}

	public List<sdGameItem> GetEquipItemsInBag(bool ignoreLock)
	{
		List<sdGameItem> table = new List<sdGameItem>();
		foreach(DictionaryEntry item in itemDB)
		{
			sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == (int)PanelType.Panel_Bag && (info.itemClass == 0 || info.itemClass == 1 || info.itemClass == 2))
			{
                if (!ignoreLock && info.isLock) continue;
				table.Add(info);
			}
		}
		
		return table;
	}

    public List<sdGameItem> GetAllEquipItems()
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            if (info.itemClass == 0 || info.itemClass == 1 || info.itemClass == 2)
            {
                table.Add(info);
            }
        }

        return table;
    }

    public List<sdGameItem> GetAllFormulaItems()
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            if (info.isFormulaItem)
            {
                table.Add(info);
            }
        }

        return table;
    }

    public List<sdGameItem> GetEquipByQuilityInBag(int quility, bool isLock)
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == (int)PanelType.Panel_Bag && (quility & (1 << info.quility)) > 0)
            {
                if (info.isLock != isLock) continue;
                if (info.itemClass != 0 && info.itemClass == 1 && info.itemClass == 2) continue;
                table.Add(info);
            }
        }
        return table;
    }

    public List<sdGameItem> GetItemByQuility(int quility, bool isLock)
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == (int)PanelType.Panel_Bag && (quility & (1<<info.quility)) > 0)
            {
                if (info.isLock != isLock) continue;
                table.Add(info);
            }
        }
        return table;
    }

    public List<sdGameItem> GetBagItemByEquipPos(int pos, bool needCanEquip)
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == (int)PanelType.Panel_Bag && info.equipPos == pos)
            {
                if (!needCanEquip || info.CanEquip(sdGameLevel.instance.mainChar))
                {
                    table.Add(info);
                }
            }
        }
        return table;
    }

    public List<sdGameItem> GetGemOnInBag()
    {
        List<sdGameItem> table = new List<sdGameItem>();
        foreach (DictionaryEntry item in itemDB)
        {
            sdGameItem info = (sdGameItem)item.Value;
            Hashtable gemInfo = sdConfDataMgr.Instance().GetItemById(info.templateID.ToString());
            if (info.bagIndex == (int)PanelType.Panel_Bag && info.itemClass == 51 && info.subClass == 1)
            {
                int[] equipPos = new int[]{int.Parse(gemInfo["HolePos"].ToString())};
                BitArray ary = new BitArray(equipPos);
                if (ary[upItem.equipPos])
                {
                    table.Add(info.Clone());
                }
            }
        }

        return table;
    }

	public List<sdGameItem> GetGemInBag(int gemLevel)
	{
		List<sdGameItem> table = new List<sdGameItem>();
		foreach(DictionaryEntry item in itemDB)
		{
			sdGameItem info = (sdGameItem)item.Value;
            if (info.bagIndex == (int)PanelType.Panel_Bag && info.itemClass == 51 
                && info.subClass == 1 && (gemLevel == 0 || gemLevel == info.level))
			{
			    table.Add(info.Clone());	
			}
		}
		
		return table;
	}
}
