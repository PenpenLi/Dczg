using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewInfoType
{
	Type_All = 0,
	Type_Item,
	Type_Skill,
	Type_Pet,
	Type_Friend,
	Type_PetEquip,
	Type_LapBoss,
}

public class sdNewInfoMgr : Singleton<sdNewInfoMgr>
{
	Hashtable infoList = new Hashtable();
	private Dictionary<NewInfoType, List<sdNewInfo>> childList = new Dictionary<NewInfoType, List<sdNewInfo>>();
	
	public void Register(sdNewInfo info)
	{
		if (info == null) return;
		if (childList.ContainsKey(info.type) && childList[info.type] != null) 
		{
			childList[info.type].Add(info);
		}
		else
		{
			List<sdNewInfo> table = new List<sdNewInfo>();
			table.Add(info);
			childList.Add(info.type, table);
		}
		
		if (infoList[info.type] != null)
		{
			info.ShowOrHide((bool)infoList[info.type]);
		}
		else
		{
			info.ShowOrHide(false);
		}
		
		if (info.type == NewInfoType.Type_All)
		{
			info.SetNew(hasNew);	
		}
	}
	
	public void RemoveItem(sdNewInfo info)
	{
		if (info == null) return;
		if (childList.ContainsKey(info.type) && childList[info.type] != null) 
		{
			childList[info.type].Remove(info);
		}
	}
	
	public void CreateNewInfo(NewInfoType type)
	{
		if (infoList[type] == null)
		{
			infoList.Add(type, true);	
		}
		else
		{
			infoList[type] = true;
		}
		
		ChangeInfo(type, true);
	}
	
	public void ClearNewInfo(NewInfoType type)
	{
		if (infoList[type] == null)
		{
			infoList.Add(type, false);	
		}
		else
		{
			infoList[type] = false;
		}
		
		ChangeInfo(type, false);
	}
	
	private bool hasNew = false;
	
	void ChangeInfo(NewInfoType type, bool flag)
	{
		if (!childList.ContainsKey(type) || childList[type] == null) return;
		List<sdNewInfo> list = childList[type];
		foreach(sdNewInfo item in list)
		{
			item.ShowOrHide(flag);
		}
		
		hasNew = false;
		foreach(string item in Enum.GetNames(typeof(NewInfoType)))
		{
			NewInfoType temp = (NewInfoType)Enum.Parse(typeof(NewInfoType), item);
			if (temp == NewInfoType.Type_All) continue;
			if (infoList.ContainsKey(temp) && infoList[temp] != null)
			{
				if ((bool)infoList[temp] == true)
				{
					hasNew = true;
					break;
				}
			}
		}
		
		if (childList.ContainsKey(NewInfoType.Type_All) && childList[NewInfoType.Type_All] != null)
		{
			List<sdNewInfo> alllist = childList[NewInfoType.Type_All];
			foreach(sdNewInfo item in alllist)
			{
				item.SetNew(hasNew);
			}
		}
	}
}