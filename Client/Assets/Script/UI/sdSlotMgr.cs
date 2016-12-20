using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public enum PanelType
{
	Panel_Bag = 1,
	Panel_Equip = 2,
	Panel_Warehouse = 4,
	Panel_Skill_Active = 8,
	Panel_Skill_Passive = 16,
	Panel_Jiesuan = 32,
	Panel_Treasure = 64,
	Panel_ItemUp = 128,
	Panel_ItemSelect = 256,
	Panel_PvpReward = 512,
	Panel_PVPRankReward = 1024,
	Panel_GemReplace = 2048,
	Panel_GemMerge = 4096,
	Panel_GemSet = 8192,
    Panel_ItemMake = 16384,
}

public class sdSlotMgr : Singleton<sdSlotMgr>
{
	GameObject armorBtn = null;
	GameObject weaponBtn = null;
	GameObject shipinBtn = null;
	
	private Dictionary<PanelType, Hashtable> m_PanelList = new Dictionary<PanelType, Hashtable>();
	
	GameObject copyItem = null;
	
	public List<sdSlotIcon> selectedItem = new List<sdSlotIcon>();
	
	public Hashtable GetIconList(PanelType type)
	{
		if (m_PanelList.ContainsKey(type))
		{
			return m_PanelList[type];	
		}
		return null;
	}
	
	public void RegisterSlot(sdSlotIcon slotIcon)
	{
		if (slotIcon == null) return;
		if (m_PanelList.ContainsKey(slotIcon.panel) && m_PanelList[slotIcon.panel] != null) 
		{
			if (slotIcon.panel == PanelType.Panel_Bag && m_PanelList[PanelType.Panel_Bag].Count == 0)
			{
				copyItem = slotIcon.gameObject;	
				//copyItem.SetActive(false);
			}
			if (!m_PanelList[slotIcon.panel].ContainsKey(slotIcon.index))
			{
				m_PanelList[slotIcon.panel].Add(slotIcon.index, slotIcon);
			}
			
		}
		else
		{
			Hashtable table = new Hashtable();
			table.Add(slotIcon.index, slotIcon);
			m_PanelList.Add(slotIcon.panel, table);
			if (slotIcon.panel == PanelType.Panel_Bag)
			{
				copyItem = slotIcon.gameObject;	
				//copyItem.SetActive(false);
			}
		}
	}
	
	public void RemoveSlot(sdSlotIcon icon)
	{
		if (icon == null) return;
		if (m_PanelList.ContainsKey(icon.panel) && m_PanelList[icon.panel] != null) 
		{
			m_PanelList[icon.panel].Remove(icon.index);
		}
	}
	
	private int CompareSlotIcon(sdSlotIcon icon1, sdSlotIcon icon2)
	{
		if (icon1.index > icon2.index) return 1;
		if (icon1.index == icon2.index) return 0;
		return -1;
	}

    public int itemFilter = (int)ItemFilter.Armor;
	public HeaderProto.ERoleItemEquipSlot itemLocation = HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Equip_Max;
    public bool isFirst = true;
    public int lastFilter = (int)ItemFilter.Armor;

	public void Notify(int panelType)
	{
		if (armorBtn == null)
		{
			armorBtn = GameObject.Find("tab_armor");
		}
		
		if (weaponBtn == null)
		{
			weaponBtn = GameObject.Find("tab_weapon");	
		}
		
		if (shipinBtn == null)
		{
			shipinBtn = GameObject.Find("tab_shipin");
		}
		 
		
		if ((panelType & (int)PanelType.Panel_Bag) > 0)
		{
			if (copyItem != null && Instance.m_PanelList.Count != 0 && Instance.m_PanelList.ContainsKey(PanelType.Panel_Bag)) 
			{
                float bagScrollValue = 0f;

				UIDraggablePanel panel = copyItem.transform.parent.GetComponent<UIDraggablePanel>();
				if (panel != null)
				{
                    if (isFirst)
                    {
                        bagScrollValue = 0;
                        isFirst = false;
                    }
                    else
                    {
                        bagScrollValue = panel.verticalScrollBar.value;
                    }
                    
					panel.ResetPosition();	
				}
				
				Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem(1, itemFilter);
				Hashtable list = Instance.m_PanelList[PanelType.Panel_Bag];
				if (itemTable.Count > list.Count)
				{
					if (copyItem == null)
					{
						Debug.LogWarning("NoCopyItem");	
					}
					copyItem.SetActive(true);
					for(int i = list.Count; i < itemTable.Count; ++i)
					{
						GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;	
						tempItem.transform.parent = copyItem.transform.parent;
						Vector3 pos = copyItem.transform.localPosition;
						pos.y -= i*100;
						tempItem.transform.localPosition = pos;
						tempItem.transform.localScale = copyItem.transform.localScale;
						sdSlotIcon icon = tempItem.GetComponent<sdSlotIcon>();
						if (icon != null)
						{
							icon.index = i;
							RegisterSlot(icon);
						}
					}
				}
				else if (itemTable.Count < list.Count)
				{
					foreach(DictionaryEntry item in list)
					{
						sdSlotIcon icon = item.Value as sdSlotIcon;

						if (icon != null && icon.index >= itemTable.Count)
						{
							if (icon.index == 0)
							{
								copyItem.SetActive(false);
							}
							else
							{
								icon.gameObject.SetActive(false);
							}
						}
					}
				}
				else
				{
					copyItem.SetActive(true);
				}
				if (panel != null)
				{
					panel.ResetPosition();	
				}
				IDictionaryEnumerator iter = list.GetEnumerator();
				
				ArrayList itemList = new ArrayList(itemTable.Values);
				if (itemList != null)
				{
					itemList.Sort();				
					bool needRepos = false;
					if (itemLocation != HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Equip_Max)
					{
						needRepos = true;
					}
					
					foreach(sdGameItem info in itemList)
					{
						iter.MoveNext();
						if (iter == null) break;
						//sdGameItem info = (sdGameItem)item.Value;
						if (info.bagIndex != (int)PanelType.Panel_Bag) continue;
						//if (info.slotIndex != num) continue;
						Hashtable table = new Hashtable();
						sdSlotIcon icon = (sdSlotIcon)iter.Value;	
						table.Add("uuid", info.instanceID);
						table.Add("ID", info.templateID);
						table.Add("count", info.count);
						icon.SetInfo(info.instanceID.ToString(), table);
						icon.gameObject.SetActive(true);
                        if (needRepos && sdConfDataMgr.Instance().IsItemRightType(itemLocation, info.equipPos))
                        {
                            UIDraggablePanel drag = icon.transform.parent.GetComponent<UIDraggablePanel>();
                            if (drag != null)
                            {
                                //drag.disableDragIfFits = false;
                                drag.MoveRelative(-icon.transform.localPosition);
                                drag.UpdateScrollbars(true);
                                bagScrollValue = drag.verticalScrollBar.value;
                                needRepos = false;
                                itemLocation = HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Equip_Max;
                                //drag.disableDragIfFits = true;

                                sdRadioButton btn = null;
                                if (itemFilter == (int)ItemFilter.Weapon)
                                {
                                    btn = weaponBtn.GetComponent<sdRadioButton>();
                                }
                                else if (itemFilter == (int)ItemFilter.Armor)
                                {
                                    btn = armorBtn.GetComponent<sdRadioButton>();
                                }
                                else if (itemFilter == (int)ItemFilter.Shipin)
                                {
                                    btn = shipinBtn.GetComponent<sdRadioButton>();
                                }
                                if (btn != null)
                                {
                                    sdUICharacter.Instance.ActiceRadioBtn(btn);
                                    btn.Active(true);
                                }
                            }
                        }
                        else
                        {
                            if (itemFilter == lastFilter)
                            {
                                if (panel != null)
                                {
                                    panel.verticalScrollBar.value = bagScrollValue;
                                }
                            }
                        }
					}
				}
                lastFilter = itemFilter;
			}
		}
		
		if ((panelType & (int)PanelType.Panel_Equip) > 0)
		{
			if (Instance.m_PanelList.Count > 0 && Instance.m_PanelList.ContainsKey(PanelType.Panel_Equip))
			{
				Hashtable list = Instance.m_PanelList[PanelType.Panel_Equip];
				IDictionaryEnumerator iter = list.GetEnumerator();
				Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem(2,-1);
				if (itemTable != null)
				{
					while (iter.MoveNext())
					{
						sdSlotIcon icon = (sdSlotIcon)iter.Value;	
						if(icon == null) continue;
						bool findFlag = false;
						foreach(DictionaryEntry item in itemTable)
						{
							sdGameItem info = (sdGameItem)item.Value;
							if (info.bagIndex != (int)PanelType.Panel_Equip) continue;
							Hashtable tempItem = sdConfDataMgr.Instance().GetItemById(info.templateID.ToString());
							if (tempItem != null)
							{
								int itemPos = int.Parse(tempItem["Character"].ToString());
								if (itemPos == icon.index)
								{
									findFlag = true;	
								}
							}	
							
							if (findFlag)
							{
								Hashtable table = new Hashtable();
								table.Add("uuid", info.instanceID);
								table.Add("ID", info.templateID);
								table.Add("count", info.count);
								icon.SetInfo(info.instanceID.ToString(), table);
								break;
							}
						}
						
						if (!findFlag)
						{
							icon.SetInfo("", null);
						}
					}
				}
			}
		}
	}
	
	public void Init()
	{
		Notify((int)PanelType.Panel_Bag|(int)PanelType.Panel_Equip|(int)PanelType.Panel_Warehouse);
	}
	
	void Start()
	{
		
	}
	
	public void GotoEquip(int index)
	{
		if ((index == (int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Weapon) || (index == (int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_SecondWeapon))
		{
            itemFilter = (int)ItemFilter.Weapon;	
		}
		else if (index == (int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Ring || index == (int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Adron 
			|| index == (int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Necklace)
		{
            itemFilter = (int)ItemFilter.Shipin;		
		}
		else
		{
            itemFilter = (int)ItemFilter.Armor;
		}
		
		sdSlotMgr.Instance.itemLocation = (HeaderProto.ERoleItemEquipSlot)index;

		bool hasItem = false;
		Hashtable table = sdGameItemMgr.Instance.GetAllItem(1,-1);
		foreach(DictionaryEntry info in table)
		{
			sdGameItem item = info.Value as sdGameItem;
			if (item == null) continue;
			if (sdConfDataMgr.Instance().IsItemRightType(itemLocation,item.equipPos))
			{
				hasItem = true;
				break;
			}
		}
		
		if (hasItem)
		{
			Notify((int)PanelType.Panel_Bag);
		}
	}
}
