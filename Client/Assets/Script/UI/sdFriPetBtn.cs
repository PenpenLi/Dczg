using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sdFriPetBtn : MonoBehaviour
{
	public string tid = "";
	public GameObject bg = null;
	
	public void SetInfo(string id)
	{
		if (id == "")
		{
			gameObject.SetActive(false);	
		}
		else
		{
			gameObject.SetActive(true);	
		}
		
		tid = id;
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(id);
		if (bg != null && info != null)
		{
			bg.GetComponent<UISprite>().spriteName = info["Icon"].ToString();	
		}
	}
	
	void OnClick()
	{
		sdUICharacter.Instance.ShowFriPetAvatar(tid);
	}
	
	
	
//	void ChangeLayer(GameObject obj)
//	{
//		int num = obj.transform.childCount;
//		for(int i = 0; i < num; ++i)
//		{
//			Transform child = obj.transform.GetChild(i);
//			child.gameObject.layer = LayerMask.NameToLayer("UI");
//			ChangeLayer(child.gameObject);	
//		}
//	}
}