using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetChangeEquipPnl : MonoBehaviour 
{
	static private GameObject m_preWnd = null;
	public UInt64 m_uuSelectDBID = UInt64.MaxValue;
	public GameObject peticon = null;
	public GameObject equip0 = null;
	public GameObject equip1 = null;
	public GameObject equip2 = null;
	public GameObject equipK0 = null;
	public GameObject equipK1 = null;
	public GameObject equipK2 = null;
	public GameObject txtName = null;
	public GameObject txtUp = null;
	
	public Hashtable petItemList = new Hashtable();
	public GameObject copyItem = null;

	public int m_iSortSubClass = (int)PetEquipType.Pet_EquipType_all;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
		Init();
	}
	
	void Update () 
	{
	}
	
	public void Init()
	{

	}
	
	void OnClick()
    {
		if ( gameObject.name=="BT_petClose")
		{
			if( sdUIPetControl.m_UIPetChangeEquipPnl != null )
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetChangeEquipPnl);
			
			if( m_preWnd )
				m_preWnd.SetActive(true);
			
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetPropPnl petPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPnl)
					petPnl.SetPetModelVisible(true);
			}
		}
		else if (gameObject.name=="btnEquip")
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetChangeEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
				if (petPnl)
				{
					foreach(DictionaryEntry info in petPnl.petItemList)
					{
						sdUIPetEquipIcon icon = info.Value as sdUIPetEquipIcon;
						if (icon!=null)
						{
							UInt64 uuID = icon.GetId();
							if (uuID!=UInt64.MaxValue&&icon.bSelect==true)
							{
								sdPetMsg.Send_CS_PET_EQUIP_REQ(uuID, petPnl.m_uuSelectDBID, 1);
							}
						}
					}
				}
			}
		}
		else if (gameObject.name=="btnUnEquip")
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetChangeEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
				if (petPnl)
				{
					SClientPetInfo Info = null;
					if (petPnl.m_uuSelectDBID!=UInt64.MaxValue)
					{
						Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuSelectDBID);
						if (Info == null)
							return;
					}
					else
					{
						return;
					}
					
					foreach(DictionaryEntry equipInfo in Info.m_EquipedDB)
					{
						sdGamePetItem petItem = (sdGamePetItem)equipInfo.Value;
						UInt64 uuID = petItem.instanceID;
						if (uuID!=UInt64.MaxValue)
						{
							sdPetMsg.Send_CS_PET_EQUIP_REQ(uuID, petPnl.m_uuSelectDBID, 0);
						}
					}
				}
			}
		}
	}
	
	public void ActivePetChangeEquipPnl(GameObject PreWnd, UInt64 uuDBID, int iSortSubClass)
	{
		GameObject wnd = GameObject.Find("NGUIRoot");
		if (wnd)
		{
			sdUIPetChangeEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
			if (petPnl)
			{
				petPnl.m_uuSelectDBID = uuDBID;
				petPnl.m_iSortSubClass = iSortSubClass;
				petPnl.ReflashPetIcon();
				petPnl.ReflashPetEquipUI();
				petPnl.RefreshPetItemListPage();
			}
		}
	}
	
	public void ReflashPetIcon()
	{
		if (peticon)
		{
			sdUIPetZuheSmallIcon icon = peticon.GetComponent<sdUIPetZuheSmallIcon>();
			SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuSelectDBID);
			
			if (icon!=null && Info!=null)
				icon.SetIdAndReflashUI((int)Info.m_uiTemplateID);
			
			if (txtName!=null && Info!=null)
			{
				sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, txtName);
				txtName.GetComponent<UILabel>().text = Info.m_strName;
			}
			
			if (txtUp!=null && Info!=null)
			{
				if (Info.m_iUp>0)
				{
					txtUp.GetComponent<UILabel>().text = "+" + Info.m_iUp.ToString();
					txtUp.SetActive(true);
				}
				else
				{
					txtUp.SetActive(false);
				}
			}
		}
	}
	
	public void ReflashPetEquipUI()
	{
		if (equip0)
			equip0.SetActive(false);
		
		if (equip1)
			equip1.SetActive(false);
		
		if (equip2)
			equip2.SetActive(false);
		
		if (equipK0)
			equipK0.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = 0;

		if (equipK1)
			equipK1.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = 0;
		
		if (equipK2)
			equipK2.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = 0;
		
		if (m_uuSelectDBID==UInt64.MaxValue)
			return;
		SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuSelectDBID);
		if (Info==null)
			return;
		
		foreach(DictionaryEntry equipInfo in Info.m_EquipedDB)
		{
			sdGamePetItem petItem = (sdGamePetItem)equipInfo.Value;
			Hashtable info = sdConfDataMgr.Instance().GetItemById(petItem.templateID.ToString());
			if (info != null)
			{
				int isubClass = int.Parse(info["SubClass"].ToString());
				int iconId = int.Parse(info["IconID"].ToString());
				string strIcon = info["IconPath"].ToString();
				if (iconId >= 0)
				{
					UIAtlas atlas = sdConfDataMgr.Instance().GetItemAtlas(iconId.ToString());
					if (atlas!=null)
					{
						if (isubClass==(int)PetEquipType.Pet_EquipType_fj)
						{
							if (equip0)
							{
								equip0.GetComponent<UISprite>().atlas = atlas;
								equip0.GetComponent<UISprite>().spriteName = strIcon;
								equip0.SetActive(true);
								
								if (equipK0)
									equipK0.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = petItem.templateID;
							}
						}
						else if (isubClass==(int)PetEquipType.Pet_EquipType_wq)
						{
							if (equip1)
							{
								equip1.GetComponent<UISprite>().atlas = atlas;
								equip1.GetComponent<UISprite>().spriteName = strIcon;
								equip1.SetActive(true);
								
								if (equipK1)
									equipK1.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = petItem.templateID;
							}
						}
						else if (isubClass==(int)PetEquipType.Pet_EquipType_sp)
						{
							if (equip2)
							{
								equip2.GetComponent<UISprite>().atlas = atlas;
								equip2.GetComponent<UISprite>().spriteName = strIcon;
								equip2.SetActive(true);
								
								if (equipK2)
									equipK2.GetComponent<sdUIPetEquipButtonClick>().m_iEquipID = petItem.templateID;
							}
						}
					}
				}
			}
		}
	}
	
	public void RefreshPetItemListPage()
	{
		Hashtable list = null;
		list = sdNewPetMgr.Instance.petItemDB;

		//将宠物装备数据填充到List中，用来排序..
		List<sdGamePetItem> listEquip1 = new List<sdGamePetItem>();
		List<sdGamePetItem> listEquip2 = new List<sdGamePetItem>();
		foreach(DictionaryEntry info in list)
		{
			sdGamePetItem item = info.Value as sdGamePetItem;
			if (item.iCharacter==m_iSortSubClass || m_iSortSubClass==(int)PetEquipType.Pet_EquipType_all)
				listEquip1.Add(item);
			else if (item.iCharacter!=m_iSortSubClass && m_iSortSubClass!=(int)PetEquipType.Pet_EquipType_all)
				listEquip2.Add(item);
		}
		listEquip1.Sort(sdGamePetItem.PetItemSortByValue);
		listEquip2.Sort(sdGamePetItem.PetItemSortByValue);

		int num = list.Count;	
		int count = petItemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;
				tempItem.GetComponent<sdUIPetEquipIcon>().index = count;
				tempItem.transform.parent = copyItem.transform.parent;
				tempItem.transform.localPosition = copyItem.transform.localPosition;
				tempItem.transform.localScale = copyItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (114 * (count/2));
				int iX = (count%2)*400;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				tempItem.GetComponent<sdUIPetEquipIcon>().bSelect = false;
				petItemList.Add(petItemList.Count, tempItem.GetComponent<sdUIPetEquipIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petItemList.GetEnumerator();
		foreach (sdGamePetItem infoEntry in listEquip1)
		{
			if (iter.MoveNext())
			{
				sdUIPetEquipIcon icon = iter.Value as sdUIPetEquipIcon;
				icon.SetIdAndReflashUI(infoEntry.instanceID);
			}
		}

		foreach (sdGamePetItem infoEntry in listEquip2)
		{
			if (iter.MoveNext())
			{
				sdUIPetEquipIcon icon = iter.Value as sdUIPetEquipIcon;
				icon.SetIdAndReflashUI(infoEntry.instanceID);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetEquipIcon icon = iter.Value as sdUIPetEquipIcon;
			icon.SetIdAndReflashUI(UInt64.MaxValue);
		}

		if (copyItem!=null)
		{
			copyItem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}

	public void SetAllItemUnSelectByCharacter(int iCharacter)
	{
		foreach(DictionaryEntry info in petItemList)
		{
			sdUIPetEquipIcon icon = info.Value as sdUIPetEquipIcon;
			UInt64 uuID = icon.GetId();
			if (uuID!=UInt64.MaxValue)
			{
				sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(uuID);
				if (petItem!=null&&petItem.iCharacter == iCharacter)
				{
					icon.bSelect = false;
					icon.ReflashSelectUI();
				}
			}
		}
	}
}

