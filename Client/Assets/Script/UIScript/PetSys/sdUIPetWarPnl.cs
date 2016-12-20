using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdPetZuheData
{
	public int iZuheID = 0;
	public int iPetID1 = 0;
	public int iPetEnableType1 = 0;//0 id不存在，1 灰显，2 激活..
	public int iPetID2 = 0;
	public int iPetEnableType2 = 0;
	public int iPetID3 = 0;
	public int iPetEnableType3 = 0;
	public int iPetID4 = 0;
	public int iPetEnableType4 = 0;

	public int GetEnableCount()
	{
		int count = 0;

		if (iPetEnableType1==2)
			count++;
		if (iPetEnableType2==2)
			count++;
		if (iPetEnableType3==2)
			count++;
		if (iPetEnableType4==2)
			count++;

		return count;
	}

	static public int SortPetZuheDataByEnableCount(sdPetZuheData item, sdPetZuheData compare)
	{
		int iCompareCount = compare.GetEnableCount();
		int iItemCount = item.GetEnableCount();
		if (iCompareCount > iItemCount) 
		{
			return 1;
		}
		else if (iCompareCount < iItemCount)
		{
			return -1;
		}
		else
		{
			return 0;
		}
	}
}

public class sdUIPetWarPnl : MonoBehaviour 
{
	public GameObject m_preWnd = null;

	public GameObject m_wpet0 = null;
	public GameObject m_wpet1 = null;
	public GameObject m_wpet2 = null;
	public GameObject m_wzpet0 = null;
	public GameObject m_wzpet1 = null;
	public GameObject m_wzpet2 = null;
	public GameObject m_wzpet3 = null;

	public GameObject btnAccept = null;
	public GameObject btnBackground = null;
	public GameObject btnWord = null;

	public GameObject rbsel_one;
	public GameObject rbsel_two;
	public GameObject rbsel_three;

	Hashtable petzuheInfoList = new Hashtable();
	public GameObject copyListItem = null;

	public int mCurTeamIndex = 0;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public void Init()
	{
	}
	
	void OnClick()
    {
	}
	
	public void ActivePetWarPnl(GameObject PreWnd,int TeamIdx)
	{
		m_preWnd = PreWnd;
		if( TeamIdx < 0 )
			mCurTeamIndex = sdNewPetMgr.Instance.mPetCurTeam;
		else
			mCurTeamIndex = TeamIdx;
		OnActivePnlSetRadioButton();
		ReflashPetBattleTeam();
		RefreshPetZuhePage();
	}
	
	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton rb0 = null;
		sdRadioButton rb1 = null;
		sdRadioButton rb2 = null;
		if (rbsel_one)
			rb0 = rbsel_one.GetComponent<sdRadioButton>();
		if (rbsel_two)
			rb1 = rbsel_two.GetComponent<sdRadioButton>();
		if (rbsel_three)
			rb2 = rbsel_three.GetComponent<sdRadioButton>();
		if (mCurTeamIndex==0)
		{
			rb0.Active(true);
			sdUICharacter.Instance.ActiceRadioBtn(rb0);	
		}
		else if (mCurTeamIndex==1)
		{
			rb1.Active(true);
			sdUICharacter.Instance.ActiceRadioBtn(rb1);	
		}
		else if (mCurTeamIndex==2)
		{
			rb2.Active(true);
			sdUICharacter.Instance.ActiceRadioBtn(rb2);	
		}
	}
	
	public void ReflashPetBattleTeam()
	{
		//刷新宠物卡牌图标..
		if (m_wpet0)
		{
			sdUIPetWarCard pIcon = m_wpet0.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 0;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+0]);
			}
		}
		
		if (m_wpet1)
		{
			sdUIPetWarCard pIcon = m_wpet1.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 1;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+1]);
			}
		}
		
		if (m_wpet2)
		{
			sdUIPetWarCard pIcon = m_wpet2.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 2;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+2]);
			}
		}
		
		if (m_wzpet0)
		{
			sdUIPetWarCard pIcon = m_wzpet0.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 3;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+3]);
			}
		}
		
		if (m_wzpet1)
		{
			sdUIPetWarCard pIcon = m_wzpet1.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 4;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+4]);
			}
		}
		
		if (m_wzpet2)
		{
			sdUIPetWarCard pIcon = m_wzpet2.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 5;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+5]);
			}
		}
		
		if (m_wzpet3)
		{
			sdUIPetWarCard pIcon = m_wzpet3.GetComponent<sdUIPetWarCard>();
			if (pIcon)
			{
				pIcon.index = 6;
				pIcon.ReflashPetIconUI(sdNewPetMgr.Instance.mPetAllTeam[mCurTeamIndex*7+6]);
			}
		}
		//刷新激活按钮UI..
		if (mCurTeamIndex==sdNewPetMgr.Instance.mPetCurTeam)
		{
			if (btnBackground)
				btnBackground.GetComponent<UISprite>().spriteName = "btn_dis";
			
			if (btnWord)
				btnWord.GetComponent<UISprite>().spriteName = "Tyjh";
			
			if (btnAccept)
				btnAccept.GetComponent<UIButton>().enabled = false;
		}
		else
		{
			if (btnBackground)
				btnBackground.GetComponent<UISprite>().spriteName = "btn";
			
			if (btnWord)
				btnWord.GetComponent<UISprite>().spriteName = "Tjh";
			
			if (btnAccept)
				btnAccept.GetComponent<UIButton>().enabled = true;
		}
	}
	
	public void RefreshPetZuhePage()
	{
		List<sdPetZuheData> listData1 = new List<sdPetZuheData>();
		List<sdPetZuheData> listData2 = new List<sdPetZuheData>();

		Hashtable kTable = sdConfDataMgr.Instance().GetPetGroupsTable();
		if (kTable != null)
		{
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				int iPetID1 = int.Parse(valTable["Data1.PetID"].ToString());
				int iPetID2 = int.Parse(valTable["Data2.PetID"].ToString());
				int iPetID3 = int.Parse(valTable["Data3.PetID"].ToString());
				int iPetID4 = int.Parse(valTable["Data4.PetID"].ToString());

				int iPetEnable1 = 0;
				if (iPetID1>0)
				{
					if (sdNewPetMgr.Instance.GetPetTemplateIsInCurPageBattleTeam(iPetID1, mCurTeamIndex)==0)
						iPetEnable1 = 1;
					else
						iPetEnable1 = 2;
				}

				int iPetEnable2 = 0;
				if (iPetID2>0)
				{
					if (sdNewPetMgr.Instance.GetPetTemplateIsInCurPageBattleTeam(iPetID2, mCurTeamIndex)==0)
						iPetEnable2 = 1;
					else
						iPetEnable2 = 2;
				}

				int iPetEnable3 = 0;
				if (iPetID3>0)
				{
					if (sdNewPetMgr.Instance.GetPetTemplateIsInCurPageBattleTeam(iPetID3, mCurTeamIndex)==0)
						iPetEnable3 = 1;
					else
						iPetEnable3 = 2;
				}

				int iPetEnable4 = 0;
				if (iPetID4>0)
				{
					if (sdNewPetMgr.Instance.GetPetTemplateIsInCurPageBattleTeam(iPetID4, mCurTeamIndex)==0)
						iPetEnable4 = 1;
					else
						iPetEnable4 = 2;
				}

				//四个ID都属于：不存在，未激活..
				if (iPetEnable1!=2 && iPetEnable2!=2 && iPetEnable3!=2 && iPetEnable3!=2)
				{

				}
				else
				{
					sdPetZuheData data = new sdPetZuheData();
					data.iZuheID = int.Parse(key1);
					data.iPetID1 = iPetID1;
					data.iPetEnableType1 = iPetEnable1;
					data.iPetID2 = iPetID2;
					data.iPetEnableType2 = iPetEnable2;
					data.iPetID3 = iPetID3;
					data.iPetEnableType3 = iPetEnable3;
					data.iPetID4 = iPetID4;
					data.iPetEnableType4 = iPetEnable4;

					if (iPetEnable1!=1 && iPetEnable2!=1 && iPetEnable3!=1 && iPetEnable4!=1)
						listData1.Add(data);
					else
						listData2.Add(data);
				}
			}
		}

		int num = listData1.Count + listData2.Count;
		int iZero = 0;
		if (num<2)
		{
			iZero = 2-num;
		}
		
		num = num + iZero;
		int count = petzuheInfoList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyListItem) as GameObject;
				tempItem.GetComponent<sdUIPetZuheIcon>().index = count;
				tempItem.transform.parent = copyListItem.transform.parent;
				tempItem.transform.localPosition = copyListItem.transform.localPosition;
				tempItem.transform.localScale = copyListItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (295*count);
				tempItem.transform.localPosition = pos;
				petzuheInfoList.Add(petzuheInfoList.Count, tempItem.GetComponent<sdUIPetZuheIcon>());
				++count;
			}
		}
		
		IDictionaryEnumerator iter = petzuheInfoList.GetEnumerator();
		foreach (sdPetZuheData infoEntry in listData1)
		{
			if (iter.MoveNext())
			{
				sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
				icon.bpetGray0 = false;
				icon.bpetGray1 = false;
				icon.bpetGray2 = false;
				icon.bpetGray3 = false;
				icon.SetIdAndReflashUI(infoEntry.iZuheID);
			}
		}

		foreach (sdPetZuheData infoEntry in listData2)
		{
			if (iter.MoveNext())
			{
				sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
				if (infoEntry.iPetEnableType1==1)
					icon.bpetGray0 = true;
				else
					icon.bpetGray0 = false;

				if (infoEntry.iPetEnableType2==1)
					icon.bpetGray1 = true;
				else
					icon.bpetGray1 = false;

				if (infoEntry.iPetEnableType3==1)
					icon.bpetGray2 = true;
				else
					icon.bpetGray2 = false;

				if (infoEntry.iPetEnableType4==1)
					icon.bpetGray3 = true;
				else
					icon.bpetGray3 = false;

				icon.SetIdAndReflashUI(infoEntry.iZuheID);
				icon.SetGray(true);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
				icon.SetIdAndReflashUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
			icon.SetIdAndReflashUI(-1);
		}

		if (copyListItem!=null)
		{
			copyListItem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
}