using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetSaleSelectPnl : MonoBehaviour 
{
	public GameObject		m_preWnd			= null;
	public GameObject		m_moneyNum			= null;
	public GameObject		m_craystalNum		= null;
	public GameObject		SaleOne				= null;
	public GameObject		SaleTwo				= null;
	public GameObject		SaleThree			= null;
	public GameObject		SaleFour			= null;

	public bool m_bSelectOne = false;
	public bool m_bSelectTwo = false;
	public bool m_bSelectThree = false;
	public bool m_bSelectFour = false;
	
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
	
	public void ActivePetSaleSelectPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		UpdatePetSaleSelectList();
		ReflashPetSaleMoneyUI();
		ResetSelectBtnUI();
	}
	
	public void UpdatePetSaleSelectList()
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
			if ( sdNewPetMgr.Instance.GetIsInBattleAllTeam(UInt64.Parse(key1))==0 && petvalue.m_Lock!=1 )
				list.Add(key1,petvalue);
		}

		//将宠物数据填充到List中，用来排序..
		List<SClientPetInfo> listOther = new List<SClientPetInfo>();
		foreach(DictionaryEntry info in list)
		{
			SClientPetInfo info1 = info.Value as SClientPetInfo;
			listOther.Add(info1);
		}
		listOther.Sort(SClientPetInfo.PetSortByAbilityBeginBig);

		int num = listOther.Count;
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
				tempItem.GetComponent<sdUIPetSaleSelectCard>().index = count;
				tempItem.transform.parent = m_spet.transform.parent;
				tempItem.transform.localPosition = m_spet.transform.localPosition;
				tempItem.transform.localScale = m_spet.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (276 * (count/4));
				int iX = (count%4)*221;
				pos.x += iX;
				tempItem.transform.localPosition = pos;
				petIconList.Add(petIconList.Count, tempItem.GetComponent<sdUIPetSaleSelectCard>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = petIconList.GetEnumerator();
		foreach (SClientPetInfo infoEntry in listOther)
		{
			if (iter.MoveNext())
			{
				sdUIPetSaleSelectCard icon = iter.Value as sdUIPetSaleSelectCard;
				icon.ReflashPetIconUI(infoEntry.m_uuDBID);
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetSaleSelectCard icon = iter.Value as sdUIPetSaleSelectCard;
				icon.ReflashPetIconUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetSaleSelectCard icon = iter.Value as sdUIPetSaleSelectCard;
			icon.ReflashPetIconUI(UInt64.MaxValue);
		}

		if (m_spet!=null)
		{
			m_spet.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
	
	public void ReflashPetSaleMoneyUI()
	{
		int iMoney = 0;
		int iCraystal = 0;
		int iTemp = 0;
		int iUp = 0;
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetSaleSelectCard icon = info.Value as sdUIPetSaleSelectCard;
			if (icon.m_bSelect==true && icon.m_uuDBID!=UInt64.MaxValue && icon.m_uuDBID!=0)
			{
				SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(icon.m_uuDBID);
				if (Info == null)
					continue;

				Hashtable Info2 = sdConfDataMgr.Instance().GetPetTemplate(Info.m_uiTemplateID.ToString());
				if (Info2 == null)
					continue;

				iTemp = int.Parse(Info2["SellMoney"].ToString());
				iMoney += iTemp;
				iTemp = int.Parse(Info2["Crystal"].ToString());
				iUp = Info.m_iUp;
				if (Info.m_iUp==0)
					iUp = 1;
				else if (Info.m_iUp==1)
					iUp = 2;
				else if (Info.m_iUp==2)
					iUp = 4;
				else if (Info.m_iUp==3)
					iUp = 8;

				iTemp = iTemp*iUp;
				iCraystal += iTemp;
			}
		}

		if (m_moneyNum)
			m_moneyNum.GetComponent<UILabel>().text = iMoney.ToString();

		if (m_craystalNum)
			m_craystalNum.GetComponent<UILabel>().text = iCraystal.ToString();
	}

	public bool IsSelectedWonderfulPet()
	{
		bool bResult = false;
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetSaleSelectCard icon = info.Value as sdUIPetSaleSelectCard;
			if (icon.m_bSelect==true && icon.m_uuDBID!=UInt64.MaxValue && icon.m_uuDBID!=0)
			{
				SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(icon.m_uuDBID);
				if (Info == null)
					continue;

				if (Info.m_iAbility>=3)
				{
					bResult = true;
					break;
				}
			}
		}

		return bResult;
	}

	public void PetBeginSale()
	{
		//存在三星以上战魂出售，需要给提示..
		if (IsSelectedWonderfulPet()==true)
		{
			sdMsgBox.OnConfirm btn_ok = new sdMsgBox.OnConfirm(OnClickPetSaleOk);
			sdUICharacter.Instance.ShowOkCanelMsg("你出售的战魂中存在3星以上的战魂,确定出售吗?", btn_ok, null);
		}
		else
		{
			OnClickPetSaleOk();
		}
	}

	public void OnClickPetSaleOk()
	{
		int iCount = 0;
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetSaleSelectCard icon = info.Value as sdUIPetSaleSelectCard;
			if (icon.m_bSelect==true && icon.m_uuDBID!=UInt64.MaxValue && icon.m_uuDBID!=0)
			{
				sdPetMsg.Send_CS_PET_EVENT_REQ((byte)HeaderProto.EPetEvent.PET_EVENT_SELL, icon.m_uuDBID);
				iCount++;
			}
		}

		sdPetMsg.m_iPetSaleNum = iCount;
		if (iCount==0)
			sdUICharacter.Instance.ShowOkMsg("请选择需要出售的战魂", null);
		else
			OnClickPetSaleCancel();
	}
	
	public void OnClickPetSaleCancel()
	{
		SetAllPetItemUnSelected();
		ReflashPetSaleMoneyUI();
		ResetSelectBtnUI();
	}

	public void SetAllPetItemUnSelected()
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetSaleSelectCard icon = info.Value as sdUIPetSaleSelectCard;
			icon.SetPetSelect(false);
		}
	}

	public void OnClickPetAutoSale(int iAbility, bool bAllSelect)
	{
		foreach(DictionaryEntry info in petIconList)
		{
			sdUIPetSaleSelectCard icon = info.Value as sdUIPetSaleSelectCard;
			if (icon.m_uuDBID!=UInt64.MaxValue && icon.m_uuDBID!=0)
			{
				SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(icon.m_uuDBID);
				if (Info == null)
					continue;
				
				if (Info.m_iAbility==iAbility)
					icon.SetPetSelect(bAllSelect);
			}
		}

		ReflashPetSaleMoneyUI();
	}

	public void OnClickSaleSelectBtn(int iDx)
	{
		if (iDx==1)
		{
			m_bSelectOne = !m_bSelectOne;
			OnClickPetAutoSale(iDx, m_bSelectOne);
		}
		else if (iDx==2)
		{
			m_bSelectTwo = !m_bSelectTwo;
			OnClickPetAutoSale(iDx, m_bSelectTwo);
		}
		else if (iDx==3)
		{
			m_bSelectThree = !m_bSelectThree;
			OnClickPetAutoSale(iDx, m_bSelectThree);
		}
		else if (iDx==4)
		{
			m_bSelectFour = !m_bSelectFour;
			OnClickPetAutoSale(iDx, m_bSelectFour);
		}
		ShowSelectBtnUI();
	}

	public void ShowSelectBtnUI()
	{
		if (SaleOne)
		{
			//选中..
			if (m_bSelectOne)
			{
				SaleOne.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2dis";
				SaleOne.transform.FindChild("click").gameObject.SetActive(true);
			}
			else
			{
				SaleOne.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2";
				SaleOne.transform.FindChild("click").gameObject.SetActive(false);
			}
		}

		if (SaleTwo)
		{
			//选中..
			if (m_bSelectTwo)
			{
				SaleTwo.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2dis";
				SaleTwo.transform.FindChild("click").gameObject.SetActive(true);
			}
			else
			{
				SaleTwo.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2";
				SaleTwo.transform.FindChild("click").gameObject.SetActive(false);
			}
		}

		if (SaleThree)
		{
			//选中..
			if (m_bSelectThree)
			{
				SaleThree.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2dis";
				SaleThree.transform.FindChild("click").gameObject.SetActive(true);
			}
			else
			{
				SaleThree.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2";
				SaleThree.transform.FindChild("click").gameObject.SetActive(false);
			}
		}

		if (SaleFour)
		{
			//选中..
			if (m_bSelectFour)
			{
				SaleFour.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2dis";
				SaleFour.transform.FindChild("click").gameObject.SetActive(true);
			}
			else
			{
				SaleFour.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "btn_2";
				SaleFour.transform.FindChild("click").gameObject.SetActive(false);
			}
		}
	}

	public void ResetSelectBtnUI()
	{
		m_bSelectOne = false;
		m_bSelectTwo = false;
		m_bSelectThree = false;
		m_bSelectFour = false;

		ShowSelectBtnUI();
	}
}

