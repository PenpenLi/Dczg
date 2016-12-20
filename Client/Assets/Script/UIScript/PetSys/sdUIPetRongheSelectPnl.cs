using System;
using UnityEngine;
using System.Collections;

public class sdUIPetRongheSelectPnl : MonoBehaviour 
{
	public GameObject		m_preWnd			= null;
	public UInt64 m_uuHasSelectID = UInt64.MaxValue;
	public int m_iSelectPos = -1;
	
	Hashtable petIconList = new Hashtable();
	public GameObject m_spet = null;
	
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
	
	public void ActivePetRongheSelectPnl(GameObject PreWnd, UInt64 uuHasSelectID, int iSelectPos)
	{
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetRongheSelectPnl petPnl = wnd.GetComponentInChildren<sdUIPetRongheSelectPnl>();
			if (petPnl)
			{
				petPnl.m_preWnd = PreWnd;
				petPnl.m_uuHasSelectID = uuHasSelectID;
				petPnl.m_iSelectPos = iSelectPos;
				petPnl.UpdatePetSelectList();
			}
		}
	}
	
	public void SetAllPetItemUnSelected()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetRongheSelectCard icon = info.Value as sdUIPetRongheSelectCard;
			icon.SetPetSelect(false);
		}
	}
	
	public void UpdatePetSelectList()
	{
		if (m_spet==null)
			return;
		
		Hashtable listPet = null;
		listPet = sdNewPetMgr.Instance.GetPetList();
		
		Hashtable list = new Hashtable();
		if (m_uuHasSelectID!=UInt64.MaxValue)
		{
			SClientPetInfo hasSInfo = sdNewPetMgr.Instance.GetPetInfo(m_uuHasSelectID);
			if (hasSInfo != null)
			{
				foreach(DictionaryEntry info in listPet)
				{
					string key1 = info.Key.ToString();
					SClientPetInfo petvalue = info.Value as SClientPetInfo;
					if (petvalue.m_uuDBID!=m_uuHasSelectID && petvalue.m_iAbility==hasSInfo.m_iAbility 
					    && sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0 && petvalue.m_iUp==3
					    && petvalue.m_Lock!=1 && petvalue.m_iAbility<4)
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
				if (sdNewPetMgr.Instance.GetIsInBattleTeam(UInt64.Parse(key1))==0 && petvalue.m_iUp==3 && petvalue.m_Lock!=1 && petvalue.m_iAbility<4)
					list.Add(key1,petvalue);
			}
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
				tempItem.GetComponent<sdUIPetRongheSelectCard>().index = count;
				tempItem.transform.parent = m_spet.transform.parent;
				tempItem.transform.localPosition = m_spet.transform.localPosition;
				tempItem.transform.localScale = m_spet.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/4));
				int iX = (count%4)*221;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetRongheSelectCard>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			if (iter.MoveNext())
			{
				sdUIPetRongheSelectCard icon = iter.Value as sdUIPetRongheSelectCard;
				icon.ReflashPetIconUI(UInt64.Parse(key1));
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetRongheSelectCard icon = iter.Value as sdUIPetRongheSelectCard;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetRongheSelectCard icon = iter.Value as sdUIPetRongheSelectCard;
			icon.ReflashPetIconUI(UInt64.MaxValue);
		}

		if (m_spet!=null)
		{
			m_spet.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
}