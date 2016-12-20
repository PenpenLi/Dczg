using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetLevelSelectPnl : MonoBehaviour 
{
	public GameObject		m_preWnd			= null;
	
	Hashtable petIconList = new Hashtable();
	public GameObject m_spet = null;
	
	public UInt64 m_uuSelfDBID = UInt64.MaxValue;
	
	public UInt64 m_uuSelectDBID0 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID1 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID2 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID3 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID4 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID5 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID6 = UInt64.MaxValue;
	public UInt64 m_uuSelectDBID7 = UInt64.MaxValue;
	
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
	
	public void ActivePetLevelSelectPnl(GameObject PreWnd, UInt64 uuSelfDBID)
	{
		m_preWnd = PreWnd;
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetLevelSelectPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelSelectPnl>();
			if (petPnl)
			{
				petPnl.m_uuSelfDBID = uuSelfDBID;
				petPnl.m_uuSelectDBID0 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[0];
				petPnl.m_uuSelectDBID1 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[1];
				petPnl.m_uuSelectDBID2 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[2];
				petPnl.m_uuSelectDBID3 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[3];
				petPnl.m_uuSelectDBID4 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[4];
				petPnl.m_uuSelectDBID5 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[5];
				petPnl.m_uuSelectDBID6 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[6];
				petPnl.m_uuSelectDBID7 = sdNewPetMgr.Instance.m_uuPetLevelSelectID[7];
				petPnl.UpdatePetLevelSelectList();
			}
		}
	}
	
	public void UpdatePetLevelSelectList()
	{
		if (m_spet==null)
			return;
		
		Hashtable listPet = null;
		listPet = sdNewPetMgr.Instance.GetPetList();
		
		Hashtable list = new Hashtable();
		foreach(DictionaryEntry info in listPet)
		{
			string key1 = info.Key.ToString();
			SClientPetInfo petvalue = info.Value as SClientPetInfo;
			if ( sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0 && key1!=m_uuSelfDBID.ToString() && petvalue.m_Lock!=1 )
				list.Add(key1,petvalue);
		}

		//将宠物数据填充到List中，用来排序..
		List<SClientPetInfo> listOther = new List<SClientPetInfo>();
		List<SClientPetInfo> listExp = new List<SClientPetInfo>();
		foreach(DictionaryEntry info in list)
		{
			SClientPetInfo info1 = info.Value as SClientPetInfo;
			if (sdNewPetMgr.Instance.IsExpPet(info1.m_uiTemplateID))
				listExp.Add (info1);
			else
				listOther.Add(info1);
		}
		listExp.Sort(SClientPetInfo.PetSortByAbilityBeginBig);
		listOther.Sort(SClientPetInfo.PetSortByAbilityBeginBig);

		int num = listExp.Count + listOther.Count;
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
				tempItem.GetComponent<sdUIPetLevelSelectCard>().index = count;
				tempItem.transform.parent = m_spet.transform.parent;
				tempItem.transform.localPosition = m_spet.transform.localPosition;
				tempItem.transform.localScale = m_spet.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/4));
				int iX = (count%4)*221;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetLevelSelectCard>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach (SClientPetInfo infoEntry in listExp)
		{
			if (iter.MoveNext())
			{
				sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				icon.ReflashGrayUI(false);
				
				if (icon.m_uuDBID!=UInt64.MaxValue &&
				    (icon.m_uuDBID==m_uuSelectDBID0 ||
				 icon.m_uuDBID==m_uuSelectDBID1 ||
				 icon.m_uuDBID==m_uuSelectDBID2 ||
				 icon.m_uuDBID==m_uuSelectDBID3 ||
				 icon.m_uuDBID==m_uuSelectDBID4 ||
				 icon.m_uuDBID==m_uuSelectDBID5 ||
				 icon.m_uuDBID==m_uuSelectDBID6 ||
				 icon.m_uuDBID==m_uuSelectDBID7))
				{
					icon.SetPetSelect(true);
				}
			}
		}

		foreach (SClientPetInfo infoEntry in listOther)
		{
			if (iter.MoveNext())
			{
				sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
				icon.ReflashGrayUI(false);

				if (icon.m_uuDBID!=UInt64.MaxValue &&
				    (icon.m_uuDBID==m_uuSelectDBID0 ||
				 icon.m_uuDBID==m_uuSelectDBID1 ||
				 icon.m_uuDBID==m_uuSelectDBID2 ||
				 icon.m_uuDBID==m_uuSelectDBID3 ||
				 icon.m_uuDBID==m_uuSelectDBID4 ||
				 icon.m_uuDBID==m_uuSelectDBID5 ||
				 icon.m_uuDBID==m_uuSelectDBID6 ||
				 icon.m_uuDBID==m_uuSelectDBID7))
				{
					icon.SetPetSelect(true);
				}
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
				icon.ReflashPetIconUI(0);
				icon.ReflashGrayUI(false);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
			icon.ReflashPetIconUI(UInt64.MaxValue);
			icon.ReflashGrayUI(false);
		}

		if (m_spet!=null)
		{
			m_spet.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
	
	public void ReflashPetIconSelectUI()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetLevelSelectCard icon = info.Value as sdUIPetLevelSelectCard;
			if (icon.m_uuDBID!=UInt64.MaxValue &&
				(icon.m_uuDBID==m_uuSelectDBID0 ||
				 icon.m_uuDBID==m_uuSelectDBID1 ||
				 icon.m_uuDBID==m_uuSelectDBID2 ||
				 icon.m_uuDBID==m_uuSelectDBID3 ||
				 icon.m_uuDBID==m_uuSelectDBID4 ||
				 icon.m_uuDBID==m_uuSelectDBID5 ||
				 icon.m_uuDBID==m_uuSelectDBID6 ||
				 icon.m_uuDBID==m_uuSelectDBID7))
			{
				icon.SetPetSelect(true);
			}
			else
			{
				icon.SetPetSelect(false);
			}
		}
	}
	
	public void SetSelectDBID(UInt64 uuDBID)
	{
		if (uuDBID==UInt64.MaxValue)
			return;
		
		if (m_uuSelectDBID0==uuDBID || m_uuSelectDBID1==uuDBID || m_uuSelectDBID2==uuDBID || m_uuSelectDBID3==uuDBID
			 || m_uuSelectDBID4==uuDBID || m_uuSelectDBID5==uuDBID || m_uuSelectDBID6==uuDBID || m_uuSelectDBID7==uuDBID)
			return;
		
		if (m_uuSelectDBID0==UInt64.MaxValue)
		{
			m_uuSelectDBID0 = uuDBID;
		}
		else if (m_uuSelectDBID1==UInt64.MaxValue)
		{
			m_uuSelectDBID1 = uuDBID;
		}
		else if (m_uuSelectDBID2==UInt64.MaxValue)
		{
			m_uuSelectDBID2 = uuDBID;
		}
		else if (m_uuSelectDBID3==UInt64.MaxValue)
		{
			m_uuSelectDBID3 = uuDBID;
		}
		else if (m_uuSelectDBID4==UInt64.MaxValue)
		{
			m_uuSelectDBID4 = uuDBID;
		}
		else if (m_uuSelectDBID5==UInt64.MaxValue)
		{
			m_uuSelectDBID5 = uuDBID;
		}
		else if (m_uuSelectDBID6==UInt64.MaxValue)
		{
			m_uuSelectDBID6 = uuDBID;
		}
		else if (m_uuSelectDBID7==UInt64.MaxValue)
		{
			m_uuSelectDBID7 = uuDBID;
		}
		else
		{
			m_uuSelectDBID0 = m_uuSelectDBID1;
			m_uuSelectDBID1 = m_uuSelectDBID2;
			m_uuSelectDBID2 = m_uuSelectDBID3;
			m_uuSelectDBID3 = m_uuSelectDBID4;
			m_uuSelectDBID4 = m_uuSelectDBID5;
			m_uuSelectDBID5 = m_uuSelectDBID6;
			m_uuSelectDBID6 = m_uuSelectDBID7;
			m_uuSelectDBID7 = uuDBID;
		}
	}
	
	public void RemoveSelectDBID(UInt64 uuDBID)
	{
		if (uuDBID==UInt64.MaxValue)
			return;
		
		if (m_uuSelectDBID0==uuDBID)
			m_uuSelectDBID0 = UInt64.MaxValue;
		else if (m_uuSelectDBID1==uuDBID)
			m_uuSelectDBID1 = UInt64.MaxValue;
		else if (m_uuSelectDBID2==uuDBID)
			m_uuSelectDBID2 = UInt64.MaxValue;
		else if (m_uuSelectDBID3==uuDBID)
			m_uuSelectDBID3 = UInt64.MaxValue;
		else if (m_uuSelectDBID4==uuDBID)
			m_uuSelectDBID4 = UInt64.MaxValue;
		else if (m_uuSelectDBID5==uuDBID)
			m_uuSelectDBID5 = UInt64.MaxValue;
		else if (m_uuSelectDBID6==uuDBID)
			m_uuSelectDBID6 = UInt64.MaxValue;
		else if (m_uuSelectDBID7==uuDBID)
			m_uuSelectDBID7 = UInt64.MaxValue;
	}

	public int GetSelectIconNum()
	{
		int iNum = 0;
		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		while (iter.MoveNext())
		{
			sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
			if (icon.m_uuDBID!=UInt64.MaxValue&&icon.m_bSelect==true)
				iNum++;
		}

		return iNum;
	}

	public void OnClickSelectSetGray()
	{
		int iNum = GetSelectIconNum();
		if (iNum<8)
		{
			IDictionaryEnumerator iter = petIconList.GetEnumerator();
			while (iter.MoveNext())
			{
				sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
				if (icon.m_uuDBID!=UInt64.MaxValue&&icon.m_uuDBID!=0)
					icon.ReflashGrayUI(false);
			}
		}
		else
		{
			IDictionaryEnumerator iter = petIconList.GetEnumerator();
			while (iter.MoveNext())
			{
				sdUIPetLevelSelectCard icon = iter.Value as sdUIPetLevelSelectCard;
				if (icon.m_uuDBID!=UInt64.MaxValue&&icon.m_uuDBID!=0&&icon.m_bSelect==false)
					icon.ReflashGrayUI(true);
				else if (icon.m_uuDBID!=UInt64.MaxValue&&icon.m_uuDBID!=0&&icon.m_bSelect==true)
					icon.ReflashGrayUI(false);
			}
		}
	}
}

