using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetEquipPnl : MonoBehaviour
{
	static private sdRadioButton	m_tab2_all			= null;
	static private sdRadioButton	m_tab2_armor		= null;
	static private sdRadioButton	m_tab2_shipin		= null;
	static private sdRadioButton	m_tab2_weapon		= null;
	
	public GameObject m_preWnd = null;
	
	Hashtable petItemList = new Hashtable();
	public GameObject copyItem = null;
	
	public PetEquipType m_Type =  PetEquipType.Pet_EquipType_all;

	public GameObject m_sort_value = null;
	public GameObject m_sort_color = null;
	public int m_iSortType = (int)PetItemSortType.PetItem_SortBy_Value;
	
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
		if( m_tab2_all == null )
		{
			GameObject obj;
			obj = GameObject.Find("tab2_all");
			if(obj)
				m_tab2_all = obj.GetComponent<sdRadioButton>();
			
			obj = GameObject.Find("tab2_armor");
			if(obj)
				m_tab2_armor = obj.GetComponent<sdRadioButton>();
			
			obj = GameObject.Find("tab2_shipin");
			if(obj)
				m_tab2_shipin = obj.GetComponent<sdRadioButton>();
			
			obj = GameObject.Find("tab2_weapon");
			if(obj)
				m_tab2_weapon = obj.GetComponent<sdRadioButton>();
		}
	}
	
	void OnClick()
    {
		if ( gameObject.name=="BT_petClose")
		{
			if (sdUIPetControl.m_UIPetEquipPnl!=null)
			{
				sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetEquipPnl);
				if(!sdUIPetControl.Instance.IsReturnLevelPrepare())
				{
					sdUIPetEquipPnl obj = sdUIPetControl.m_UIPetEquipPnl.GetComponent<sdUIPetEquipPnl>();
					if( obj!=null && obj.m_preWnd!=null )
					{
						obj.m_preWnd.SetActive(true);
					}
				}
				
			}
		}
		else if ( gameObject.name=="tab2_all" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					petPnl.m_Type = PetEquipType.Pet_EquipType_all;
					petPnl.RefreshPetItemListPage();
				}
			}
		}
		else if ( gameObject.name=="tab2_armor" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					petPnl.m_Type = PetEquipType.Pet_EquipType_fj;
					petPnl.RefreshPetItemListPage();
				}
			}
		}
		else if ( gameObject.name=="tab2_shipin" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					petPnl.m_Type = PetEquipType.Pet_EquipType_sp;
					petPnl.RefreshPetItemListPage();
				}
			}
		}
		else if ( gameObject.name=="tab2_weapon" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					petPnl.m_Type = PetEquipType.Pet_EquipType_wq;
					petPnl.RefreshPetItemListPage();
				}
			}
		}
		else if ( gameObject.name=="btnSale" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
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
								sdMsgBox.OnConfirm btn_sale = new sdMsgBox.OnConfirm(OnClickSale);
								sdUICharacter.Instance.ShowOkCanelMsg("您确定要将已选择的宠物装备出售么?",btn_sale,null);
								break;
							}
						}
					}
				}
			}
		}
		else if ( gameObject.name=="sort_value" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					if (petPnl.m_iSortType != (int)PetItemSortType.PetItem_SortBy_Value)
					{
						petPnl.m_iSortType = (int)PetItemSortType.PetItem_SortBy_Value;
						petPnl.RefreshPetItemListPage();
					}
				}
			}
		}
		else if ( gameObject.name=="sort_color" )
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
				if (petPnl)
				{
					if (petPnl.m_iSortType != (int)PetItemSortType.PetItem_SortBy_Color)
					{
						petPnl.m_iSortType = (int)PetItemSortType.PetItem_SortBy_Color;
						petPnl.RefreshPetItemListPage();
					}
				}
			}
		}
	}

	void OnClickSale()
	{
		GameObject wnd = GameObject.Find("NGUIRoot");
		if (wnd)
		{
			sdUIPetEquipPnl petPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
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
							sdPetMsg.Send_CS_PET_EVENT_REQ((byte)HeaderProto.EPetEvent.PET_ITEM_EVENT_SELL, uuID);
						}
					}
				}
			}
		}
	}
	
	public void ActivePetEquipPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		m_Type = PetEquipType.Pet_EquipType_all;
		OnActivePnlSetRadioButton();
		RefreshPetItemListPage();
	}
	
	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn==m_tab2_all)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);
			}
		}

		if (m_sort_value)
		{
			if (m_sort_value.GetComponent<sdRadioButton>().isActive==true)
				m_iSortType = (int)PetItemSortType.PetItem_SortBy_Value;
		}
		
		if (m_sort_color)
		{
			if (m_sort_color.GetComponent<sdRadioButton>().isActive==true)
				m_iSortType = (int)PetItemSortType.PetItem_SortBy_Color;
		}
	}
	
	public void RefreshPetItemListPage()
	{
		if (copyItem==null)
			return;
		
		Hashtable list = null;
		list = sdNewPetMgr.Instance.getAllPetItemByCharacter((int)m_Type);

		//将宠物装备数据填充到List中，用来排序..
		List<sdGamePetItem> listEquip = new List<sdGamePetItem>();
		foreach(DictionaryEntry info in list)
		{
			sdGamePetItem info1 = info.Value as sdGamePetItem;
			listEquip.Add(info1);
		}
		if (m_iSortType==(int)PetItemSortType.PetItem_SortBy_Value)
			listEquip.Sort(sdGamePetItem.PetItemSortByValue);
		else if (m_iSortType==(int)PetItemSortType.PetItem_SortBy_Color)
			listEquip.Sort(sdGamePetItem.PetItemSortByColor);

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
				pos.y = pos.y - (114 * (count/3));
				int iX = (count%3)*400;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				tempItem.GetComponent<sdUIPetEquipIcon>().bSelect = false;
				petItemList.Add(petItemList.Count, tempItem.GetComponent<sdUIPetEquipIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petItemList.GetEnumerator();
		foreach (sdGamePetItem infoEntry in listEquip)
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
	}
}