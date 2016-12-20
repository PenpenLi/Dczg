using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetPaperPnl : MonoBehaviour
{
	Hashtable petPaperInfoList = new Hashtable();
	public GameObject copyItem = null;

	public GameObject m_preWnd = null;

	public static int m_iNowSelectID = 0;
	
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

	public void ActivePetPaperPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		OnActivePnlSetRadioButton();
		RefreshPetPaperPage();
	}

	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="RbSp")
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}

	public void RefreshPetPaperPage()
	{
		Hashtable list = null;
		list = sdConfDataMgr.Instance().GetPetTemplateTable();

		if (list==null)
			return;

		//将宠物碎片数据填充到List中，用来排序..
		List<SPetSmallClass> listPaper1 = new List<SPetSmallClass>();//碎片足够用来合成的..
		List<SPetSmallClass> listPaper2 = new List<SPetSmallClass>();//碎片不够用来合成的..
		int iTemplateID = 0;
		int iCurNum = 0;
		int iMaxNum = 0;
		foreach(DictionaryEntry info in list)
		{
			Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(info.Key.ToString());
			if (petInfo != null)
			{
				iTemplateID = int.Parse(info.Key.ToString());
				if (iTemplateID>0)
				{
					iCurNum = sdNewPetMgr.Instance.getPetGatherCurNumByPetId(iTemplateID);
					iMaxNum = sdNewPetMgr.Instance.getPetGatherMaxNumByPetId(iTemplateID);
					if (iCurNum>0&&iMaxNum>0)
					{
						SPetSmallClass classItem = new SPetSmallClass();
						classItem.iTemplateID = int.Parse(info.Key.ToString());
						classItem.iAbility = int.Parse(petInfo["Ability"].ToString());
						classItem.iCurNum = iCurNum;
						classItem.iMaxNum = iMaxNum;
						if (iCurNum>=iMaxNum)
						{
							listPaper1.Add(classItem);
						}
						else
						{
							listPaper2.Add(classItem);
						}
					}
				}
			}
		}
		listPaper1.Sort(SPetSmallClass.PetTujianSortByAbilityBeginSmall);
		listPaper2.Sort(SPetSmallClass.PetTujianSortByAbilityBeginSmall);

		int num = listPaper1.Count + listPaper2.Count;
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
		int count = petPaperInfoList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;
				tempItem.GetComponent<sdUIPetPaperCard>().index = count;
				tempItem.transform.parent = copyItem.transform.parent;
				tempItem.transform.localPosition = copyItem.transform.localPosition;
				tempItem.transform.localScale = copyItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/5));
				int iX = (count%5)*225;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petPaperInfoList.Add(petPaperInfoList.Count, tempItem.GetComponent<sdUIPetPaperCard>());
				++count;
			}
		}	

		IDictionaryEnumerator iter = petPaperInfoList.GetEnumerator();
		foreach (SPetSmallClass infoEntry in listPaper1)
		{
			if (iter.MoveNext())
			{
				sdUIPetPaperCard icon = iter.Value as sdUIPetPaperCard;
				icon.m_iCurNum = infoEntry.iCurNum;
				icon.m_iMaxNum = infoEntry.iMaxNum;
				icon.ReflashPetIconUI(infoEntry.iTemplateID);
			}
		}

		foreach (SPetSmallClass infoEntry in listPaper2)
		{
			if (iter.MoveNext())
			{
				sdUIPetPaperCard icon = iter.Value as sdUIPetPaperCard;
				icon.m_iCurNum = infoEntry.iCurNum;
				icon.m_iMaxNum = infoEntry.iMaxNum;
				icon.ReflashPetIconUI(infoEntry.iTemplateID);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetPaperCard icon = iter.Value as sdUIPetPaperCard;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetPaperCard icon = iter.Value as sdUIPetPaperCard;
			icon.ReflashPetIconUI(-1);
		}

		if (copyItem!=null)
		{
			copyItem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
}