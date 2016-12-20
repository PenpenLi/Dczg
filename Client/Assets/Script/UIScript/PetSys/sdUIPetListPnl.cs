using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetListPnl : MonoBehaviour
{
	public GameObject m_preWnd = null;

	public GameObject m_txtnum = null;
	public GameObject m_sort_level = null;
	public GameObject m_sort_color = null;

	Hashtable petIconList = new Hashtable();
	public GameObject m_PetCard = null;

	public int m_iSortType = (int)PetSortType.Pet_SortBy_Color;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnClick()
    {
	}
	
	public void ActivePetListPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		OnActivePnlSetRadioButton();
		OnSetSortType();
		RefreshPetListPage();
	}

	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="RbZh")
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}
	
	public void OnSetSortType()
	{
		if (m_sort_level)
		{
			if (m_sort_level.GetComponent<sdRadioButton>().isActive==true)
				m_iSortType = (int)PetSortType.Pet_SortBy_Level;
		}

		if (m_sort_color)
		{
			if (m_sort_color.GetComponent<sdRadioButton>().isActive==true)
				m_iSortType = (int)PetSortType.Pet_SortBy_Color;
		}
	}

	public void SetAllPetItemUnSelected()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetCard icon = info.Value as sdUIPetCard;
			icon.SetPetSelect(false);
		}
	}
	
	public void RefreshPetListPage()
	{
		if (m_PetCard==null)
			return;
		
		Hashtable list = null;
		list = sdNewPetMgr.Instance.GetPetList();

		//将宠物数据填充到List中，用来排序..
		List<SClientPetInfo> listBattle = new List<SClientPetInfo>();
		List<SClientPetInfo> listBattleHelp = new List<SClientPetInfo>();
		List<SClientPetInfo> listOther = new List<SClientPetInfo>();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			SClientPetInfo info1 = info.Value as SClientPetInfo;
			uint uiPetID = info1.m_uiTemplateID;

			if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==1)
			{
				listBattle.Add(info1);
			}
			else if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==2)
			{
				listBattleHelp.Add(info1);
			}
			else if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0)
			{
				listOther.Add(info1);
			}
		}
		if (m_iSortType==(int)PetSortType.Pet_SortBy_Level)
		{
			listBattle.Sort(SClientPetInfo.PetSortByLevel);
			listBattleHelp.Sort(SClientPetInfo.PetSortByLevel);
			listOther.Sort(SClientPetInfo.PetSortByLevel);
		}
		else if (m_iSortType==(int)PetSortType.Pet_SortBy_Color)
		{
			listBattle.Sort(SClientPetInfo.PetSortByAbility);
			listBattleHelp.Sort(SClientPetInfo.PetSortByAbility);
			listOther.Sort(SClientPetInfo.PetSortByAbility);
		}

		int num = list.Count;
		int iZero = 0;
		if (num<10)
		{
			iZero = 10-num;
		}
		else
		{
			int iLast = num%5;
			if (iLast>0)
			{
				iZero = 5 - iLast;
			}
		}

		num = num + iZero;
		int count = petIconList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(m_PetCard) as GameObject;
				tempItem.GetComponent<sdUIPetCard>().index = count;
				tempItem.transform.parent = m_PetCard.transform.parent;
				tempItem.transform.localPosition = m_PetCard.transform.localPosition;
				tempItem.transform.localScale = m_PetCard.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/5));
				int iX = (count%5)*225;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetCard>());
				++count;
			}
		}	

		int itemCount = 0;

		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach (SClientPetInfo infoEntry in listBattle)
		{
			if (iter.MoveNext())
			{
				sdUIPetCard icon = iter.Value as sdUIPetCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				itemCount++;
			}
		}

		foreach (SClientPetInfo infoEntry in listBattleHelp)
		{
			if (iter.MoveNext())
			{
				sdUIPetCard icon = iter.Value as sdUIPetCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				itemCount++;
			}
		}

		foreach (SClientPetInfo infoEntry in listOther)
		{
			if (iter.MoveNext())
			{
				sdUIPetCard icon = iter.Value as sdUIPetCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				itemCount++;
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetCard icon = iter.Value as sdUIPetCard;
				icon.ReflashPetIconUI(0);
				itemCount++;
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetCard icon = iter.Value as sdUIPetCard;
			icon.ReflashPetIconUI(UInt64.MaxValue);
		}
		
		if (m_txtnum!= null)
		{
			int iMax = (int)HeaderProto.MAX_PET_COUNT;
			iMax = iMax - 10;
			string txt = string.Format("{0}/{1}", list.Count.ToString(), iMax.ToString());
			m_txtnum.GetComponent<UILabel>().text = txt;
		}

		if (m_PetCard!=null)
		{
			m_PetCard.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}

		// 获得对象列表..
		GameObject[] items = new GameObject[itemCount];
		iter = petIconList.GetEnumerator();
		for(int i=0 ; i<itemCount; i++ )
		{
			iter.MoveNext();
			items[i] = (iter.Value as sdUIPetCard).gameObject;
		}
		gameObject.transform.FindChild("ArrowBar").GetComponent<sdArrowBar>().SetItems(items);
	}
}
