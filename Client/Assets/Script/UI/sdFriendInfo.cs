using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdFriendInfo : MonoBehaviour
{
	public int index = 0;
	private string id = "";
	private string strName = "";
	
	public GameObject level;
	public GameObject job;
	public GameObject name;
	public GameObject power;
	public GameObject bg;
	public GameObject img;
	//public GameObject point = null;
	public GameObject btnPanel = null;
	public GameObject addBtn = null;
	public GameObject getAp = null;
	public GameObject sendAp = null;
	
	int gender;
	int hairStyle;
	
	public string GetId()
	{
		return id;	
	}
	
	public string GetName()
	{
		return 	strName;
	}
	
	public void ShowBtn()
	{
		btnPanel.SetActive(true);
		getAp.SetActive(false);
		sendAp.SetActive(false);
		addBtn.SetActive(false);
	}
	
	public void ShowAddBtn()
	{
		addBtn.SetActive(true);
		getAp.SetActive(false);
		sendAp.SetActive(false);
		btnPanel.SetActive(false);
	}
	
	void OnAddFri()
	{
		sdFriendMsg.notifyConfirmAddFri(id, 1);
	}
	
	void OnCancelFri()
	{
		sdFriendMsg.notifyConfirmAddFri(id, 0);
	}
	
	void OnClick()
	{
		if (sdFriendMgr.Instance.GetFriend(id) != null)
		{
			sdUICharacter.Instance.SetCurFriId(id);
            sdUICharacter.Instance.ShowRoleTipWnd(sdFriendMgr.Instance.GetFriend(id), true, 0);
			//sdUICharacter.Instance.ShowFriInfo();	
		}
		else
		{
//			string str = sdConfDataMgr.Instance().GetShowStr("AddFriend");
//			sdFriend fri = sdFriendMgr.Instance.GetTempFriend(id);
//			if (fri != null)
//			{
//				char[]  array	=	fri.name.ToCharArray();
//				str = string.Format("{0}{1}", fri.name, str);
//				sdMsgBox.OnConfirm addfri = new sdMsgBox.OnConfirm(OnAddFri);
//				sdMsgBox.OnCancel cancelfri = new sdMsgBox.OnCancel(OnCancelFri); 
//				sdUICharacter.Instance.ShowOkCanelMsg(str,addfri, cancelfri);	
//			}	
		}
	}
	
	void SetAtlas(int iGender, int iHairStyle, UIAtlas atlas)
	{
		if (gender == iGender && hairStyle == iHairStyle)
		{
			img.GetComponent<UISprite>().atlas = atlas;	
		}
	}
	
	public void SetHasBg(bool flag)
	{		
// 		if (bg != null)
// 		{
// 			if (flag)
// 			{
// 				bg.SetActive(true);
// 			}
// 			else
// 			{
// 				bg.SetActive(false);	
// 			}
// 		}
	}
	
	public void SetInfo(sdFriend fri)
	{
		if (fri == null) 
		{
			gameObject.SetActive(false);
			return;
		}
		
		btnPanel.SetActive(false);
		addBtn.SetActive(false);
        getAp.SetActive(true);
        sendAp.SetActive(true);
		
		bool notFriend = (sdFriendMgr.Instance.GetFriend(fri.id) == null);
		
		gameObject.SetActive(true);
		id = fri.id;
		UIAtlas atlas = null;
		string headName;
		gender = fri.gender;
		hairStyle = fri.hairStyle;
		//sdConfDataMgr.Instance().SetHeadAtlas += new sdConfDataMgr.HeadAtlas(SetAtlas);
        sdConfDataMgr.Instance().SetHeadPic(fri.gender, fri.hairStyle, fri.color, img.GetComponent<UISprite>());
	
		strName = fri.name;
		if (img != null)
		{
            //if (atlas != null) 
            //{
            //    img.GetComponent<UISprite>().atlas = atlas;
            //}
			//img.GetComponent<UISprite>().spriteName = headName;
			
			if (fri.isOnline)
			{
				img.GetComponent<UISprite>().color = Color.white;	
			}
			else
			{
				img.GetComponent<UISprite>().color = Color.grey;	
			}
		}

		
		if (level != null)
		{
			level.GetComponent<UILabel>().text = string.Format("Lv.{0}",fri.level);	
		}
		
		if (job != null)
		{
			job.GetComponent<UILabel>().text = sdConfDataMgr.Instance().GetJobName(fri.job);		
		}
		
		if (name != null)
		{
			name.GetComponent<UILabel>().text = fri.name;					
		}
		
		if (power != null)
		{
			power.GetComponent<UILabel>().text = string.Format("{0} {1}",sdConfDataMgr.Instance().GetShowStr("Score"), fri.power);			
		}
		

		if (sdFriendMgr.Instance.GetFriend(fri.id) == null)
		{
			addBtn.SetActive(true);
		}
		else
		{
			addBtn.SetActive(false);
		}
		
		if (fri.point > 0 && (sdFriendMgr.Instance.getEpMax > sdFriendMgr.Instance.getEp))
		{
			getAp.GetComponent<UIButton>().enabled = true;
            if (getAp.transform.GetChild(0) != null && getAp.transform.GetChild(0).GetComponent<UISprite>() != null)
			{
                getAp.transform.GetChild(0).GetComponent<UISprite>().alpha = 1f;
			}
            
		}
		else
		{
			getAp.GetComponent<UIButton>().enabled = false;
            if (getAp.transform.GetChild(0) != null && getAp.transform.GetChild(0).GetComponent<UISprite>() != null)
            {
                getAp.transform.GetChild(0).GetComponent<UISprite>().alpha = 0.5f;
            }
		}

        if (fri.canSend && (sdFriendMgr.Instance.sendEpMax > sdFriendMgr.Instance.sendEp))
		{
			sendAp.GetComponent<UIButton>().enabled = true;
            if (sendAp.transform.GetChild(0) != null && sendAp.transform.GetChild(0).GetComponent<UISprite>() != null)
            {
                sendAp.transform.GetChild(0).GetComponent<UISprite>().alpha = 1f;
            }
		}
		else
		{
			sendAp.GetComponent<UIButton>().enabled = false;
            if (sendAp.transform.GetChild(0) != null && sendAp.transform.GetChild(0).GetComponent<UISprite>() != null)
            {
                sendAp.transform.GetChild(0).GetComponent<UISprite>().alpha = 0.5f;
            }
		}
		
	}
	
}
