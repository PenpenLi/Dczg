using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetTujianPnl : MonoBehaviour
{
	Hashtable petbookInfoList = new Hashtable();
	public GameObject copyBookItem = null;

	public GameObject m_preWnd = null;
	
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

	public void ActivePetTujianPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		OnActivePnlSetRadioButton();
		RefreshPetBookPage();
	}

	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="RbTj")
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}

	public void RefreshPetBookPage()
	{
		Hashtable list = null;
		list = sdConfDataMgr.Instance().GetPetTemplateTable();

		if (list==null)
			return;

		//将宠物图鉴装备数据填充到List中，用来排序..
		List<SPetSmallClass> listTujian = new List<SPetSmallClass>();
		foreach(DictionaryEntry info in list)
		{
			Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(info.Key.ToString());
			if (petInfo != null)
			{
				SPetSmallClass classItem = new SPetSmallClass();
				classItem.iTemplateID = int.Parse(info.Key.ToString());
				classItem.iAbility = int.Parse(petInfo["Ability"].ToString());
				listTujian.Add(classItem);
			}
		}
		listTujian.Sort(SPetSmallClass.PetTujianSortByAbilityBeginBig);

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
		int count = petbookInfoList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyBookItem) as GameObject;
				tempItem.GetComponent<sdUIPetTujianCard>().index = count;
				tempItem.transform.parent = copyBookItem.transform.parent;
				tempItem.transform.localPosition = copyBookItem.transform.localPosition;
				tempItem.transform.localScale = copyBookItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/5));
				int iX = (count%5)*225;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petbookInfoList.Add(petbookInfoList.Count, tempItem.GetComponent<sdUIPetTujianCard>());
				++count;
			}
		}	

		IDictionaryEnumerator iter = petbookInfoList.GetEnumerator();
		foreach (SPetSmallClass infoEntry in listTujian)
		{
			if (iter.MoveNext())
			{
				sdUIPetTujianCard icon = iter.Value as sdUIPetTujianCard;
				icon.ReflashPetIconUI(infoEntry.iTemplateID);
				
				if (sdNewPetMgr.Instance.IsPetHasGetted(infoEntry.iTemplateID)==true)
					icon.SetGray(false);
				else
					icon.SetGray(true);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetTujianCard icon = iter.Value as sdUIPetTujianCard;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetTujianCard icon = iter.Value as sdUIPetTujianCard;
			icon.ReflashPetIconUI(-1);
		}

		if (copyBookItem!=null)
		{
			copyBookItem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
}