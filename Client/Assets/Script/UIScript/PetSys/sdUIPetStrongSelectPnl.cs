using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetStrongSelectPnl : MonoBehaviour 
{
	public GameObject m_preWnd			= null;
	
	Hashtable petIconList = new Hashtable();
	public GameObject m_spet = null;
	
	public UInt64 m_uuSelfDBID = UInt64.MaxValue;		//强化宠物本身..
	public UInt64 m_uuSelectDBID = UInt64.MaxValue;		//强化宠物材料..
	
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
	
	public void ActivePetStrongSelectPnl(GameObject PreWnd, UInt64 uuSelfDBID, UInt64 uuMatDBID)
	{
		m_preWnd = PreWnd;
		m_uuSelfDBID = uuSelfDBID;
		m_uuSelectDBID = uuMatDBID;
		UpdatePetStrongSelectList();
	}
	
	public void UpdatePetStrongSelectList()
	{
		if (m_spet==null)
			return;
		
		Hashtable listPet = null;
		listPet = sdNewPetMgr.Instance.GetPetList();
		
		Hashtable list = new Hashtable();
		if (m_uuSelfDBID!=UInt64.MaxValue)
		{
			SClientPetInfo selfSInfo = sdNewPetMgr.Instance.GetPetInfo(m_uuSelfDBID);
			if (selfSInfo != null)
			{
				foreach(DictionaryEntry info in listPet)
				{
					string key1 = info.Key.ToString();
					SClientPetInfo petvalue = info.Value as SClientPetInfo;
					if (petvalue.m_uuDBID!=m_uuSelfDBID && petvalue.m_iAbility==selfSInfo.m_iAbility 
						&& petvalue.m_iUp==selfSInfo.m_iUp && sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0
					    && petvalue.m_Lock!=1 && petvalue.m_CanMerge==1)
					{
						list.Add(key1,petvalue);
					}
				}
			}
		}
		else
		{
			foreach(DictionaryEntry info in listPet)
			{
				string key1 = info.Key.ToString();
				SClientPetInfo petvalue = info.Value as SClientPetInfo;
				if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0 && petvalue.m_Lock!=1 && petvalue.m_CanMerge==1)
					list.Add(key1,petvalue);
			}
		}

		//将宠物数据填充到List中，用来排序..
		List<SClientPetInfo> listOther = new List<SClientPetInfo>();
		foreach(DictionaryEntry info in list)
		{
			SClientPetInfo info1 = info.Value as SClientPetInfo;
			listOther.Add(info1);
		}
		listOther.Sort(SClientPetInfo.PetSortByLevelBeginBig);

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
				tempItem.GetComponent<sdUIPetStrongIcon>().index = count;
				tempItem.transform.parent = m_spet.transform.parent;
				tempItem.transform.localPosition = m_spet.transform.localPosition;
				tempItem.transform.localScale = m_spet.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/4));
				int iX = (count%4)*221;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetStrongIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach (SClientPetInfo infoEntry in listOther)
		{
			if (iter.MoveNext())
			{
				sdUIPetStrongIcon icon = iter.Value as sdUIPetStrongIcon;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				if (icon.m_uuDBID!=UInt64.MaxValue && icon.m_uuDBID==m_uuSelectDBID)
					icon.SetPetSelect(true);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetStrongIcon icon = iter.Value as sdUIPetStrongIcon;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetStrongIcon icon = iter.Value as sdUIPetStrongIcon;
			icon.ReflashPetIconUI(UInt64.MaxValue);
		}

		if (m_spet!=null)
		{
			m_spet.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
	
	public void SetAllPetItemUnSelected()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetStrongIcon icon = info.Value as sdUIPetStrongIcon;
			icon.SetPetSelect(false);
		}
	}
}

