using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class sdItemMgr : object
{
	private static sdItemMgr instance = new sdItemMgr();
	public static sdItemMgr inst() 
	{
		return instance;
	}
	private Hashtable m_ItemInfo = new Hashtable();
	public HeaderProto.SItemList GetItemInfoByType(PanelType type)
	{
		return 	(HeaderProto.SItemList)m_ItemInfo[type];
	}
	
	public Hashtable GetItemInfo()
	{
		return m_ItemInfo;
	}
	public void SetItemInfo(byte panelType, HeaderProto.SItemList itemList)
	{
		if (m_ItemInfo[panelType] == null)
		{
			m_ItemInfo.Add(panelType, itemList);
		}
		else
		{
			m_ItemInfo[panelType] = itemList;
		}		
		sdSlotMgr.Instance.Notify((int)panelType);
	}
	public void MoveItem(byte fromPanel, byte toPanel, UInt64 uuId, UInt64 uuOtherId)
	{
		if (m_ItemInfo[fromPanel] != null)
		{
			HeaderProto.SItemList fromList = (HeaderProto.SItemList)m_ItemInfo[fromPanel];
			HeaderProto.SUUItemInfo item = null;
			int count = fromList.m_Count;
			for (int i = 0; i < count; i++)
			{
				if (item != null)
				{
					fromList.m_ItemList[i-1] = fromList.m_ItemList[i];
					if (i == count-1)
					{
						fromList.m_ItemList[i] = null;
					}
				}
				if (item == null && fromList.m_ItemList[i] != null && fromList.m_ItemList[i].m_UUID == uuId)
				{
					item = fromList.m_ItemList[i];
					fromList.m_ItemList[i] = null;	
					fromList.m_Count--;
				}
			}
			if (item != null)
			{
				HeaderProto.SItemList toList = (HeaderProto.SItemList)m_ItemInfo[toPanel];
				if (toList == null)
				{
					toList = new HeaderProto.SItemList();
					toList.m_Count = 0;
					m_ItemInfo.Add(toPanel, toList);
				}
				toList.m_ItemList.SetValue(item, toList.m_Count);
				toList.m_Count++;
				
				if (uuOtherId != UInt64.MaxValue)
				{
					item = null;
					count = toList.m_Count;
					for (int i = 0; i < count; i++)
					{
						if (item != null)
						{
							toList.m_ItemList[i-1] = toList.m_ItemList[i];
							if (i == count-1)
							{
								toList.m_ItemList[i] = null;
							}
						}
						if (item == null && toList.m_ItemList[i] != null && toList.m_ItemList[i].m_UUID == uuOtherId)
						{
							item = toList.m_ItemList[i];
							toList.m_ItemList[i] = null;
							fromList.m_ItemList.SetValue(item, fromList.m_Count);
							fromList.m_Count++;
							toList.m_Count--;
						}
					}
				}
				
				int type = 0;
				type |= fromPanel;
				type |= toPanel;
				sdSlotMgr.Instance.Notify(type);
			}
		}
	}
}


