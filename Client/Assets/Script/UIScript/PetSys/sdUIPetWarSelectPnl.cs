using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetWarSelectPnl : MonoBehaviour 
{
	public GameObject m_preWnd = null;
	public UInt64 m_uuSelectDBID = UInt64.MaxValue;
	public int m_iSelectPos = -1;
	
	Hashtable petIconList = new Hashtable();
	public GameObject m_spet = null;

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
	
	public void ActivePetWarSelectPnl(GameObject PreWnd, UInt64 uuDBID, int iSelectPos)
	{
		GameObject wnd = GameObject.Find("NGUIRoot");
		if (wnd)
		{
			sdUIPetWarSelectPnl petPnl = wnd.GetComponentInChildren<sdUIPetWarSelectPnl>();
			if (petPnl)
			{
				petPnl.m_uuSelectDBID = uuDBID;
				petPnl.m_iSelectPos = iSelectPos;
				petPnl.UpdatePetSelectList();
			}
		}
	}
	
	public void SetAllPetItemUnSelected()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetWarSelectCard icon = info.Value as sdUIPetWarSelectCard;
			icon.SetPetSelect(false);
		}
	}
	
	public void UpdatePetSelectList()
	{
		if (m_spet==null)
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
			if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==1)
				listBattle.Add(info1);
			else if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==2)
				listBattleHelp.Add(info1);
			else if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0)
				listOther.Add(info1);
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
		if (num<8)
		{
			iZero = 8-num;
		}
		else
		{
			int iLast = num%4;
			if (iLast>0)
			{
				iZero = 4 - iLast;
			}
		}
		
		num = num + iZero;
		int count = petIconList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(m_spet) as GameObject;
				tempItem.GetComponent<sdUIPetWarSelectCard>().index = count;
				tempItem.transform.parent = m_spet.transform.parent;
				tempItem.transform.localPosition = m_spet.transform.localPosition;
				tempItem.transform.localScale = m_spet.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/4));
				int iX = (count%4)*221;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetWarSelectCard>());
				++count;
			}
		}

		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach (SClientPetInfo infoEntry in listBattle)
		{
			if (iter.MoveNext())
			{
				sdUIPetWarSelectCard icon = iter.Value as sdUIPetWarSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				if (infoEntry.m_uuDBID==m_uuSelectDBID)
					icon.SetPetSelect(true);
			}
		}
		
		foreach (SClientPetInfo infoEntry in listBattleHelp)
		{
			if (iter.MoveNext())
			{
				sdUIPetWarSelectCard icon = iter.Value as sdUIPetWarSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				if (infoEntry.m_uuDBID==m_uuSelectDBID)
					icon.SetPetSelect(true);
			}
		}
		
		foreach (SClientPetInfo infoEntry in listOther)
		{
			if (iter.MoveNext())
			{
				sdUIPetWarSelectCard icon = iter.Value as sdUIPetWarSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				if (infoEntry.m_uuDBID==m_uuSelectDBID)
					icon.SetPetSelect(true);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetWarSelectCard icon = iter.Value as sdUIPetWarSelectCard;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetWarSelectCard icon = iter.Value as sdUIPetWarSelectCard;
			icon.ReflashPetIconUI(UInt64.MaxValue);
		}

		if (m_spet!=null)
		{
			m_spet.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
}