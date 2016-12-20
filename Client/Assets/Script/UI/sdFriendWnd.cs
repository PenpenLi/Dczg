using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdFriendWnd : MonoBehaviour
{
	Hashtable friendInfoList = new Hashtable();
	Hashtable searchList = new Hashtable();
	public GameObject copyItem = null;
    public GameObject friCopyItem = null;
	public GameObject tab_fri = null;
	public GameObject tab_addfri = null;
	public GameObject friinfo = null;
	public GameObject avatar= null;
	public GameObject input = null;
	public GameObject friendNum = null;
	public GameObject friendName = null;
	private GameObject tempAvatar = null;
	public GameObject petAvatar = null;
	private string petId = "";
	public GameObject jobImg = null;
	public GameObject power = null;
	sdFriPetBtn[] petList = null;
	public GameObject addDragPanel = null;
	public GameObject tab_invite = null;
	public GameObject searchItem = null;

    public GameObject epPanel = null;
    public UILabel sendEpNum = null;
    public UILabel getEpNum = null;
	
	public void ShowPetAvatar(string id)
	{
		petAvatar.SetActive(true);
		avatar.SetActive(false);
		if (petId != id)
		{
			Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(id);
			if (info != null)
			{
				string kPath = info["Res"] as string;
				kPath = kPath.Replace(".prefab","_UI.prefab");
				ResLoadParams kParam = new ResLoadParams();
				sdResourceMgr.Instance.LoadResource(kPath, OnLoadPet, kParam);	
			}
			petId = id;
		}
	}
	
    public void OnAddFri()
    {
        sdFriendMsg.notifyAddFri(sdFriendMgr.Instance.searchFri.name);
    }

	public void ShowSearchFri()
	{
        sdUICharacter.Instance.ShowOkCanelMsg(string.Format(sdConfDataMgr.Instance().GetShowStr("AddFriendConfirm"),sdFriendMgr.Instance.searchFri.name) , new sdMsgBox.OnConfirm(OnAddFri), null);
// 		copyItem.GetComponent<sdFriendInfo>().SetInfo(sdFriendMgr.Instance.searchFri);
// 		copyItem.GetComponent<sdFriendInfo>().ShowAddBtn();
// 		copyItem.SetActive(true);
		//searchItem.SetActive(false);
	}
	
	public void HideSearchFri()
	{
		//copyItem.SetActive(false);
		//searchItem.SetActive(true);
	}
	
	void OnLoadPet(ResLoadParams kParam, UnityEngine.Object kObj)
	{
		if (petAvatar.transform.childCount > 0) GameObject.Destroy(petAvatar.transform.GetChild(0).gameObject);
		GameObject kPetObj = GameObject.Instantiate(kObj, Vector3.zero,Quaternion.identity) as GameObject;
		kPetObj.transform.parent = petAvatar.transform;
		kPetObj.transform.rotation = new Quaternion(0,180,0,0);
		kPetObj.transform.localScale = kPetObj.transform.localScale*0.65f;
		kPetObj.transform.localPosition = Vector3.zero;
	}
	
	public void ShowFriAvatar()
	{
		petAvatar.SetActive(false);
		avatar.SetActive(true);

	}
	
	public string GetCurSearchName()
	{
		if (input != null)
		{
			return input.GetComponent<UIInput>().value;	
		}
		
		return "";
	}
	
	public void ShowFriTab()
	{
		if (tab_fri != null)
		{
			tab_fri.SetActive(true);
            epPanel.SetActive(true);
		}
		
		if (tab_addfri != null)
		{
			tab_addfri.SetActive(false);	
		}
		
		if (tab_invite != null)
		{
			tab_invite.SetActive(false);	
		}
	}
	
	public void ShowAddFriTab()
	{
		if (tab_fri != null)
		{
			tab_fri.SetActive(false);
            epPanel.SetActive(false);
		}
		
		if (tab_addfri != null)
		{
			tab_addfri.SetActive(true);	
		}
	
		if (tab_invite != null)
		{
			tab_invite.SetActive(false);	
		}

        RefreshFriRequest();
	}
	
	public void ShowInviteTab()
	{
		if (tab_fri != null)
		{
			tab_fri.SetActive(false);
            epPanel.SetActive(false);
		}
		
		if (tab_addfri != null)
		{
			tab_addfri.SetActive(false);	
		}	
		
		if (tab_invite != null)
		{
			tab_invite.SetActive(true);	
		}
		
		
	}
	
	void FixedUpdate()
	{
		if (avatar != null && avatar.GetComponent<sdFriAvatar>() != null)
			avatar.GetComponent<sdFriAvatar>().tickFrame();
		
		if (needJobBack && jobImg != null && sdConfDataMgr.Instance().GetAtlas(baseJob.ToString()) != null)
		{
			jobImg.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().GetAtlas(baseJob.ToString());
			jobImg.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobBack(baseJob.ToString());
			needJobBack = false;
		}
	}
	
	bool needJobBack = false;
	int baseJob = 0;
	
	public void HideFriInfo()
	{
		GameObject.Destroy(avatar);
		avatar = GameObject.Instantiate(tempAvatar) as GameObject;
		avatar.transform.parent = tempAvatar.transform.parent;
		avatar.transform.localPosition = tempAvatar.transform.localPosition;
		avatar.transform.localScale = tempAvatar.transform.localScale;
		avatar.name = "friAvatar";
		avatar.SetActive(true);
		friinfo.SetActive(false);
	}
	
	public void ShowFriInfo()
	{
		if (friinfo != null)
		{
			sdFriend fri = sdFriendMgr.Instance.GetFriend(sdUICharacter.Instance.GetCurFriId());
			if (fri == null) fri = sdFriendMgr.Instance.GetTempFriend(sdUICharacter.Instance.GetCurFriId());
			if (fri ==null) return;
			friinfo.SetActive(true);
			petAvatar.SetActive(false);
			if (avatar != null)
			{
				
				sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
				kInfo.mDBID = 1;
				kInfo.mGender = fri.gender;
				kInfo.mHairStyle = fri.hairStyle;
				kInfo.mSkinColor = fri.color;
				kInfo.mBaseJob = byte.Parse(fri.job);
				kInfo.mJob = byte.Parse(fri.job);
				kInfo.mLevel = int.Parse(fri.level);
				avatar.AddComponent<sdFriAvatar>();
				avatar.GetComponent<sdFriAvatar>().init(kInfo);	
				uint[] equiplist = fri.equipList.ToArray();
				avatar.GetComponent<sdFriAvatar>().updateAvatar(equiplist, (uint)fri.equipList.Count);
                avatar.GetComponent<sdFriAvatar>().PlayIdle();
				avatar.GetComponent<SpinObject>().m_Target = avatar.GetComponent<sdFriAvatar>();
				avatar.GetComponent<SpinObject>().m_Speed = 0.5f;
				
				int maxNum = fri.petList.Count;
				int i = 0;
				for(; i < maxNum; ++i)
				{
					petList[i].SetInfo(fri.petList[i].m_uiTemplateID.ToString());	
				}
				
				maxNum = sdNewPetMgr.Instance.BattlePetNum;
				for (; i < maxNum; ++i)
				{
					petList[i].SetInfo("");
				}
				
				needJobBack = true;
				baseJob = int.Parse(fri.job);
				sdConfDataMgr.Instance().LoadJobAtlas(baseJob);
			}
			
			if (friendName != null)
			{
				friendName.GetComponent<UILabel>().text = fri.name;	
			}
			
			if (power != null)
			{
				power.GetComponent<UILabel>().text = sdConfDataMgr.Instance().GetShowStr("Score") + fri.power;
			}
		}
	}

	public void RefreshFriRequest()
	{
		int num = sdFriendMgr.Instance.GetTempFriNum();
		int count = searchList.Count;
		if (num > count)
		{
			num = num - count;
			int bgNum = 0;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;
				
				tempItem.GetComponent<sdFriendInfo>().index = count;
				tempItem.transform.parent = addDragPanel.transform;
				tempItem.GetComponent<UIDragPanelContents>().draggablePanel = addDragPanel.GetComponent<UIDraggablePanel>();
				tempItem.transform.localPosition = copyItem.transform.localPosition;
				tempItem.transform.localScale = copyItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (130 * (count));
// 				if (count % 2 == 0)
// 				{
// 					pos.x = -pos.x;
// 				}
				tempItem.transform.localPosition = pos;
				searchList.Add(tempItem.GetComponent<sdFriendInfo>().index, tempItem.GetComponent<sdFriendInfo>());
				++count;
				
				if (bgNum == 0 || bgNum == 3)
				{
					tempItem.GetComponent<sdFriendInfo>().SetHasBg(true);	
				}
				else if (bgNum == 1 || bgNum == 2)
				{
					tempItem.GetComponent<sdFriendInfo>().SetHasBg(false);	
				}
				
				++bgNum;
				if (bgNum > 3)
				{
					bgNum = 0;	
				}
			}
		}	
		
		IDictionaryEnumerator iter = searchList.GetEnumerator();
		ArrayList list = new ArrayList(sdFriendMgr.Instance.GetTempFriends().Values);
		
		list.Sort();
		int maxNum = list.Count;
		
		for (int i = 0; i < maxNum; ++i)
		{
			sdFriend fri = list[i] as sdFriend;
			if (iter.MoveNext())
			{
				sdFriendInfo item = iter.Value as sdFriendInfo;
				item.SetInfo(fri);
				item.ShowBtn();
			}
		}
	
		while (iter.MoveNext())
		{
			sdFriendInfo item = iter.Value as sdFriendInfo;
			item.SetInfo(null);	
		}
	}
	
	public void RefreshFri(bool needOnline)
	{
		if (friendNum != null)
		{
			int max = sdFriendMgr.Instance.GetFriendNum();
			int online = sdFriendMgr.Instance.GetOnlineFriendsNum();
			string str = string.Format("{0} {1}/{2}", sdConfDataMgr.Instance().GetShowStr("OnlineFriend"), online, max);
			friendNum.GetComponent<UILabel>().text = str;
		}

        getEpNum.text = string.Format("{0} / {1}", sdFriendMgr.Instance.getEpMax - sdFriendMgr.Instance.getEp, sdFriendMgr.Instance.getEpMax);
        sendEpNum.text = string.Format("{0} / {1}", sdFriendMgr.Instance.sendEpMax - sdFriendMgr.Instance.sendEp, sdFriendMgr.Instance.sendEpMax);

		Hashtable list = null;
		if (needOnline)
		{
			list = sdFriendMgr.Instance.GetOnlineFriends();
			
		}
		else
		{
			list = sdFriendMgr.Instance.GetFriends();
		}

		int num = list.Count;	
		int count = friendInfoList.Count;
		if (num > count)
		{
			num = num - count;
			
			int bgNum = 0;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(friCopyItem) as GameObject;
				tempItem.GetComponent<sdFriendInfo>().index = count;
				tempItem.transform.parent = tab_fri.transform;
                tempItem.transform.localPosition = friCopyItem.transform.localPosition;
                tempItem.transform.localScale = friCopyItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (130 * (count/2));
				if (count % 2 == 0)
				{
					pos.x = -pos.x;
				}
				tempItem.transform.localPosition = pos;
				friendInfoList.Add(friendInfoList.Count, tempItem.GetComponent<sdFriendInfo>());
				++count;
				
				if (bgNum == 0 || bgNum == 3)
				{
					tempItem.GetComponent<sdFriendInfo>().SetHasBg(true);	
				}
				else if (bgNum == 1 || bgNum == 2)
				{
					tempItem.GetComponent<sdFriendInfo>().SetHasBg(false);	
				}
				
				++bgNum;
				if (bgNum > 3)
				{
					bgNum = 0;	
				}
			}
		}	
		
		IDictionaryEnumerator iter = friendInfoList.GetEnumerator();
		
		foreach(DictionaryEntry info in list)
		{
			sdFriend fri = info.Value as sdFriend;
			if (iter.MoveNext())
			{
				sdFriendInfo item = iter.Value as sdFriendInfo;
				item.SetInfo(fri);
			}
		}
		
		while (iter.MoveNext())
		{
			sdFriendInfo item = iter.Value as sdFriendInfo;
			item.SetInfo(null);	
		}

        
	}
	
	void Start()
	{
		petList = gameObject.GetComponentsInChildren<sdFriPetBtn>();
		
		if (tab_addfri != null)
		{
			tab_addfri.SetActive(false);	
		}
		
		if (friinfo != null)
		{
			friinfo.SetActive(false);	
		}
		
		if (copyItem != null)
		{
			//friendInfoList.Add(friendInfoList.Count, copyItem.GetComponent<sdFriendInfo>());
			copyItem.SetActive(false);
		}
		
		if (avatar != null)
		{
			tempAvatar = GameObject.Instantiate(avatar) as GameObject;
			tempAvatar.transform.parent = avatar.transform.parent;
			tempAvatar.transform.localPosition = avatar.transform.localPosition;
			tempAvatar.transform.localScale = avatar.transform.localScale;
			tempAvatar.SetActive(false);
		}

		RefreshFri(false);
	}
	
}
