using UnityEngine;
using System.Collections;
using System;

public enum PetItemSortType
{
	PetItem_SortBy_Value = 0,
	PetItem_SortBy_Color = 1,
}

public class sdGamePetItem
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
	public int iCharacter = -1;
	public int quility = 0;
	public int level = 0;
	public bool isNew = false;
	
	//< 是否已经装备aaa
	public bool equiped = false;

	static public int PetItemSortByValue(sdGamePetItem item, sdGamePetItem compare)
	{
		int scoreCompare = sdConfDataMgr.Instance().GetItemScore(compare.templateID.ToString(), 0);
		int scoreItem = sdConfDataMgr.Instance().GetItemScore(item.templateID.ToString(), 0);
		if (scoreCompare > scoreItem)
		{
			return 1;	
		}
		else if (scoreCompare < scoreItem)
		{
			return -1;	
		}	
		else
		{
			if (compare.quility > item.quility) 
			{
				return 1;
			}
			else if (compare.quility < item.quility)
			{
				return -1;
			}
			else
			{

				return 0;
			}
		}
	}

	static public int PetItemSortByColor(sdGamePetItem item, sdGamePetItem compare)
	{
		int scoreCompare = sdConfDataMgr.Instance().GetItemScore(compare.templateID.ToString(), 0);
		int scoreItem = sdConfDataMgr.Instance().GetItemScore(item.templateID.ToString(), 0);
		if (compare.quility > item.quility)
		{
			return 1;	
		}
		else if (compare.quility < item.quility)
		{
			return -1;	
		}	
		else
		{
			if (scoreCompare > scoreItem) 
			{
				return 1;
			}
			else if (scoreCompare < scoreItem)
			{
				return -1;
			}
			else
			{
				
				return 0;
			}
		}
	}
}
