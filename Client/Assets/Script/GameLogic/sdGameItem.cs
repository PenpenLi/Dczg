using UnityEngine;
using System.Collections;
using System;

public enum GameItemClassType
{
	Game_Item_Class_Pet = 101,
}

/// <summary>
/// 角色装备信息aa
/// </summary>
public class sdGameItem : object, IComparable
{
	public int templateID = -1;
	public UInt64 instanceID = UInt64.MaxValue;
	public string mdlPath = "";
	public string mdlPartName = "";
	public string anchorNodeName = "";
	public int equipPos = -1;
	public int bagIndex = -1;
	public int count = 0;
	public int itemClass = -1;
	public int subClass = -1;
	public int quility = 0;
	public int level = 0;
	public int upExp = 0;
	public int upLevel = 0;
	public int gemNum = 0;
	public int[] gemList = null;
    public int suitId = 0;
    public bool isLock = false;
    public int score = 0;
    public bool isFormulaItem = false;
	
	protected bool newFlag = false;
	public bool isNew
	{
		set { newFlag = value;}
		get	{ return newFlag;}
	}
	
	// 是否已经装备aaa
	public bool equiped = false;

	public sdGameItem Clone()
	{
		sdGameItem item = new sdGameItem();

		item.templateID = templateID;
		item.instanceID = instanceID;
		item.mdlPath = mdlPath;
		item.mdlPartName = mdlPartName;
		item.anchorNodeName = anchorNodeName;
		item.equipPos = equipPos;
		item.bagIndex = bagIndex;
		item.count = count;
		item.itemClass = itemClass;
		item.subClass = subClass;
		item.quility = quility;
		item.level = level;
		item.upExp = upExp;
		item.upLevel = upLevel;
		item.gemNum = gemNum;
		item.gemList = gemList;
		item.equiped = equiped;
		item.newFlag = newFlag;
        item.suitId = suitId;
        item.isLock = isLock;
        item.score = score;
        item.isFormulaItem = isFormulaItem;
		return item;
	}
	
	// 穿上装备aa
	public bool takeOn(sdGameActor_Impl kActor)
	{
		Hashtable info = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
		if (info != null)
		{
			string obj	=	info["NeedJob"].ToString();
			int job = 0;//int.Parse(.ToString());
			if(obj!="")
			{
				job=int.Parse(obj);
			}
			int current = (int)kActor.GetJob();
			if (job!= 0)
			{
				if((job&(1<<current))==0)
				{
					return false;	
				}
			}
		}
		
		//
		if(mdlPartName.Length > 0)
			kActor.changeAvatar(mdlPath, mdlPartName, anchorNodeName);

		foreach (DictionaryEntry entry in kActor.itemInfo)
		{
			sdGameItem item = entry.Value as sdGameItem;
			if(item.equipPos == equipPos)
			{
				item.equiped = false;
				kActor.itemInfo.Remove(item.instanceID);
				break;
			}
		}
		kActor.itemInfo[instanceID] = this;
		//equiped = true;
		
		// 主角需要保存属性信息aa
		if(sdGameLevel.instance != null && kActor == sdGameLevel.instance.mainChar)
		{
			sdGameLevel.instance.storeLevelInfo();
		}
		
		return true;
	}

	// 脱下装备aa
	public bool takeOff(sdGameActor_Impl kActor)
	{
		kActor.changeAvatar("", mdlPartName, "");
		foreach (DictionaryEntry entry in kActor.itemInfo)
		{
			sdGameItem item = entry.Value as sdGameItem;
			if(item.equipPos == equipPos)
			{
				item.equiped = false;
				kActor.itemInfo.Remove(item.instanceID);
				break;
			}
		}

		// 主角需要保存属性信息aa
		if (sdGameLevel.instance != null && kActor == sdGameLevel.instance.mainChar)
		{
			sdGameLevel.instance.storeLevelInfo();
		}

		return true;
	}

	// 穿上默认装备aa
	public bool takeOnNake(sdGameActor_Impl kActor)
	{
		if(mdlPartName.Length > 0)
			kActor.changeAvatar(mdlPath, mdlPartName, anchorNodeName);

		foreach (DictionaryEntry entry in kActor.itemInfo)
		{
			sdGameItem item = entry.Value as sdGameItem;
			if(item.equipPos == equipPos)
			{
				item.equiped = false;
				kActor.itemInfo.Remove(item.instanceID);
				break;
			}
		}

		// 主角需要保存属性信息aa
		if (sdGameLevel.instance != null && kActor == sdGameLevel.instance.mainChar)
		{
			sdGameLevel.instance.storeLevelInfo();
		}
		
		return true;
	}
	
	// 丢弃装备aa
	public bool drop(sdGameActor_Impl kActor)
	{
		return true;
	}
	
	// 放進背包aa
	public bool putInBag(sdGameActor_Impl kActor)
	{
		return true;
	}
	
	// 放进仓库aa
	public bool putInDepot(sdGameActor_Impl kActor)
	{
		return true;
	}

	// 
	public bool CanEquip(sdGameActor_Impl kActor, out string errorStr)
	{
		if (kActor != null)
		{
			Hashtable table = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
			byte job = kActor.Job;
			int needjob = int.Parse(table["NeedJob"].ToString());
			if (needjob != 0 && (needjob & (1<<job)) <= 0)
			{
                errorStr = sdConfDataMgr.Instance().GetShowStr("JobWrong");
				return false;
			}

			if (kActor["Level"] < int.Parse(table["NeedLevel"].ToString()))
			{
                errorStr = sdConfDataMgr.Instance().GetShowStr("LevelWrong");
				return false;	
			}

            errorStr = "";
			return true;
		}

        errorStr = "";
		return false;
	}

    public bool CanEquip(sdGameActor_Impl kActor)
    {
        if (kActor != null)
        {
            Hashtable table = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
            byte job = kActor.Job;
            int needjob = int.Parse(table["NeedJob"].ToString());
            if (needjob != 0 && (needjob & (1 << job)) <= 0)
            {
                return false;
            }

            if (kActor["Level"] < int.Parse(table["NeedLevel"].ToString()))
            {
                return false;
            }

            return true;
        }

        return false;
    }
	
	//
	public int CompareTo(object item)
	{
		sdGameItem compare = item as sdGameItem;
        if (compare.quility < quility)
        {
            return -1;
        }
        else if (compare.quility > quility)
        {
            return 1;
        }
        else
        {
            if (compare.level < level)
            {
                return -1;
            }
            else if (compare.level > level)
            {
                return 1;
            }
            else
            {
                if (compare.score < score)
                {
                    return -1;
                }
                else if (compare.score > score)
                {
                    return 1;
                }
                return 0;
            }
        }
	}
}
