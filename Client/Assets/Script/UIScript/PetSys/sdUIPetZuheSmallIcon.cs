using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIPetZuheSmallIcon : MonoBehaviour
{
	private int tempId = 0;
	public bool bGray = false;
	
	public GameObject bg = null;
	public GameObject icon = null;
	public GameObject star0 = null;
	public GameObject star1 = null;
	public GameObject star2 = null;
	public GameObject star3 = null;
	public GameObject star4 = null;
	
	public int GetId()
	{
		return tempId;
	}
	
	void OnClick()
	{
		if (gameObject&&gameObject.activeSelf==true)
		{
			if (tempId>0)
				sdUIPetControl.Instance.ActivePetZuheSmallTip(null, tempId, 0, 1);
		}
	}
	
	public void SetIdAndReflashUI(int id)
	{
		if (id<=0) 
		{
			tempId = 0;
			bGray = false;
			gameObject.SetActive(false);
			return;
		}
		
		gameObject.SetActive(true);
		tempId = id;
		bGray = false;
		ReflashGrayUI();

		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(tempId.ToString());
		if (info != null)
		{
			int iAbility = int.Parse(info["Ability"].ToString());
			SetPetStar(iAbility);

			if (bg)
			{
				if (iAbility==1)
					bg.GetComponent<UISprite>().spriteName = "IconL2w";
				else if (iAbility==2)
					bg.GetComponent<UISprite>().spriteName = "IconL2g";
				else if (iAbility==3)
					bg.GetComponent<UISprite>().spriteName = "IconL2b";
				else if (iAbility==4)
					bg.GetComponent<UISprite>().spriteName = "IconL2p";
				else if (iAbility==5)
					bg.GetComponent<UISprite>().spriteName = "IconL2y";
			}
			
			if (icon)
				icon.GetComponent<UISprite>().spriteName = info["Icon"].ToString();
		}
	}
	
	public void SetPetStar(int iStar)
	{
		if (star0==null || star1==null || star2==null || star3==null || star4==null)
			return;
		
		float fWidth = (float)star0.GetComponent<UISprite>().width*0.75f;
		
		if (iStar==1)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(false);
			star4.SetActive(false);
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			star0.SetActive(true);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			star0.SetActive(true);
			star1.SetActive(false);
			star2.SetActive(false);
			star3.SetActive(false);
			star4.SetActive(false);
		}
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
			Color grayColor = new Color (0.3f, 0.3f, 0.3f, 1f);

			if (bg)
				bg.GetComponent<UISprite>().color = grayColor;

			if (icon)
				icon.GetComponent<UISprite>().color = grayColor;

			if (star0)
				star0.GetComponent<UISprite>().color = grayColor;
			
			if (star1)
				star1.GetComponent<UISprite>().color = grayColor;
			
			if (star2)
				star2.GetComponent<UISprite>().color = grayColor;
			
			if (star3)
				star3.GetComponent<UISprite>().color = grayColor;
			
			if (star4)
				star4.GetComponent<UISprite>().color = grayColor;
		}
		else
		{
			if (bg)
				bg.GetComponent<UISprite>().color = Color.white;
			
			if (icon)
				icon.GetComponent<UISprite>().color = Color.white;
			
			if (star0)
				star0.GetComponent<UISprite>().color = Color.white;
			
			if (star1)
				star1.GetComponent<UISprite>().color = Color.white;
			
			if (star2)
				star2.GetComponent<UISprite>().color = Color.white;
			
			if (star3)
				star3.GetComponent<UISprite>().color = Color.white;
			
			if (star4)
				star4.GetComponent<UISprite>().color = Color.white;
		}
	}
}
