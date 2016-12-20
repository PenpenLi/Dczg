using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIPetZuheIcon : MonoBehaviour
{
	public bool bGray = false;
	public int index = 0;
	private int zuheId = -1;
	
	public GameObject dot = null;
	public GameObject txtName = null;
	public GameObject txtItem0 = null;
	public GameObject txtItem1 = null;
	public GameObject txtItem2 = null;
	public GameObject zhpet0 = null;
	public bool bpetGray0 = false;
	public GameObject zhpet1 = null;
	public bool bpetGray1 = false;
	public GameObject zhpet2 = null;
	public bool bpetGray2 = false;
	public GameObject zhpet3 = null;
	public bool bpetGray3 = false;
	public string strName = "";
	
	public int GetId()
	{
		return zuheId;
	}
	
	void OnClick()
	{

	}
	
	public void SetIdAndReflashUI(int id)
	{
		if (id<0) 
		{
			zuheId = -1;
			bGray = false;
			ReflashGrayUI();
			ShowZuheUIDetail(false);
			gameObject.SetActive(false);
			strName = "";
			return;
		}
		else if (id==0)
		{
			zuheId = 0;
			bGray = false;
			ReflashGrayUI();
			ShowZuheUIDetail(false);
			gameObject.GetComponent<UISprite>().spriteName = "zhbgkong";
			gameObject.SetActive(true);
			strName = "";
			return;
		}

		ShowZuheUIDetail(true);
		gameObject.GetComponent<UISprite>().spriteName = "zhbg";
		gameObject.SetActive(true);
		zuheId = id;

		string strTemp = sdConfDataMgr.Instance().GetPetGroupsValueByStringKey(zuheId, "SapID");
		int iSapId = int.Parse(strTemp);
		int iPetID0 = int.Parse( sdConfDataMgr.Instance().GetPetGroupsValueByStringKey(zuheId, "Data1.PetID") );
		int iPetID1 = int.Parse( sdConfDataMgr.Instance().GetPetGroupsValueByStringKey(zuheId, "Data2.PetID") );
		int iPetID2 = int.Parse( sdConfDataMgr.Instance().GetPetGroupsValueByStringKey(zuheId, "Data3.PetID") );
		int iPetID3 = int.Parse( sdConfDataMgr.Instance().GetPetGroupsValueByStringKey(zuheId, "Data4.PetID") );

		if (iSapId>0)
		{
			strTemp = sdConfDataMgr.Instance().GetPetSapsValueByStringKey(iSapId, "Desc");
			strName = strTemp;
			if (txtName)
				txtName.GetComponent<UILabel>().text = strTemp;

			bGray = false;
			ReflashGrayUI();
			
			strTemp = sdConfDataMgr.Instance().GetPetSapsValueByStringKey(iSapId, "Skill1.Desc");
			if (txtItem0)
			{
				if (strTemp=="0")
					txtItem0.GetComponent<UILabel>().text = "";
				else
					txtItem0.GetComponent<UILabel>().text = strTemp;
			}
			
			strTemp = sdConfDataMgr.Instance().GetPetSapsValueByStringKey(iSapId, "Skill2.Desc");
			if (txtItem1)
			{
				if (strTemp=="0")
					txtItem1.GetComponent<UILabel>().text = "";
				else
					txtItem1.GetComponent<UILabel>().text = strTemp;
			}
			
			strTemp = sdConfDataMgr.Instance().GetPetSapsValueByStringKey(iSapId, "Skill3.Desc");
			if (txtItem2)
			{
				if (strTemp=="0")
					txtItem2.GetComponent<UILabel>().text = "";
				else
					txtItem2.GetComponent<UILabel>().text = strTemp;
			}
		}
		
		if (zhpet0)
		{
			sdUIPetZuheSmallIcon smallIcon = zhpet0.GetComponent<sdUIPetZuheSmallIcon>();
			if (smallIcon)
			{
				smallIcon.SetIdAndReflashUI(iPetID0);
				if (bpetGray0)
					smallIcon.SetGray(true);
			}
		}
		
		if (zhpet1)
		{
			sdUIPetZuheSmallIcon smallIcon = zhpet1.GetComponent<sdUIPetZuheSmallIcon>();
			if (smallIcon)
			{
				smallIcon.SetIdAndReflashUI(iPetID1);
				if (bpetGray1)
					smallIcon.SetGray(true);
			}
		}
		
		if (zhpet2)
		{
			sdUIPetZuheSmallIcon smallIcon = zhpet2.GetComponent<sdUIPetZuheSmallIcon>();
			if (smallIcon)
			{
				smallIcon.SetIdAndReflashUI(iPetID2);
				if (bpetGray2)
					smallIcon.SetGray(true);
			}
		}
		
		if (zhpet3)
		{
			sdUIPetZuheSmallIcon smallIcon = zhpet3.GetComponent<sdUIPetZuheSmallIcon>();
			if (smallIcon)
			{
				smallIcon.SetIdAndReflashUI(iPetID3);
				if (bpetGray3)
					smallIcon.SetGray(true);
			}
		}
	}

	public void ShowZuheUIDetail(bool bShow)
	{
		if (dot)
			dot.SetActive(bShow);

		if (txtName)
			txtName.SetActive(bShow);

		if (txtItem0)
			txtItem0.SetActive(bShow);

		if (txtItem1)
			txtItem1.SetActive(bShow);

		if (txtItem2)
			txtItem2.SetActive(bShow);

		if (zhpet0)
			zhpet0.SetActive(bShow);

		if (zhpet1)
			zhpet1.SetActive(bShow);

		if (zhpet2)
			zhpet2.SetActive(bShow);

		if (zhpet3)
			zhpet3.SetActive(bShow);
	}

	public void SetGray(bool bValue)
	{
		bGray = bValue;
		ReflashGrayUI();
	}
	
	public void ReflashGrayUI()
	{
		if (bGray)
		{
			gameObject.GetComponent<UISprite>().color = new Color (0.3f, 0.3f, 0.3f, 1f);

			if (dot)
				dot.GetComponent<UISprite>().spriteName = "jh2";

			if (txtName)
			{
				txtName.GetComponent<UILabel>().color = new Color(150,150,150,255)/255.0f;
				txtName.GetComponent<UILabel>().text = strName ;
			}

			if (txtItem0)
				txtItem0.GetComponent<UILabel>().color = new Color(88,130,88,255)/255.0f;
			
			if (txtItem1)
				txtItem1.GetComponent<UILabel>().color = new Color(88,130,88,255)/255.0f;
			
			if (txtItem2)
				txtItem2.GetComponent<UILabel>().color = new Color(88,130,88,255)/255.0f;
		}
		else
		{
			gameObject.GetComponent<UISprite>().color = Color.white;

			if (dot)
				dot.GetComponent<UISprite>().spriteName = "jh1";

			if (txtName)
			{
				txtName.GetComponent<UILabel>().color = Color.white;
				txtName.GetComponent<UILabel>().text = strName ;
			}

			if (txtItem0)
				txtItem0.GetComponent<UILabel>().color = new Color(149,255,84,255)/255.0f;
			
			if (txtItem1)
				txtItem1.GetComponent<UILabel>().color = new Color(149,255,84,255)/255.0f;
			
			if (txtItem2)
				txtItem2.GetComponent<UILabel>().color = new Color(149,255,84,255)/255.0f;
		}
	}
}
