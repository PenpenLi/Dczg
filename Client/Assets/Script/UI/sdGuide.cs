
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class sdGuide : object, IComparable
{	
	public int id;
	public int classId;
	
	public int opType;
	public GuideConditionType conType;
	public GuidePlayerEvetnType eventType;
	
	public object opParam;
	public object conParam;
	public object eventParam;
	public GameObject effect = null;
	public bool isSave = false;

	bool canRun = false;
	bool isListen = false;
    public bool isSkip = false;
    public bool isLock = false;

	GameObject tempObj = null;
	
	public void OnCanRun()
	{
        if (conType == GuideConditionType.Unlock)
        {
            sdUICharacter.Instance.ShowMask(false, null);
            needShowPoint = true;
        }

		if (conType == GuideConditionType.Click && conParam.ToString() == "0")
		{
			sdUICharacter.Instance.HideMask();	
		}
		if (conType == GuideConditionType.EnterTrigger && sdGameLevel.instance != null 
		    && sdGameLevel.instance.GetFingerControl() != null)
		{
			sdGameLevel.instance.GetFingerControl().StopMove_ClearPath();
		}

		canRun = true;	
	}

	public bool CanRun()
	{
		int a = (int)conType;
		switch(conType)
		{
		case GuideConditionType.None:
		{
			return true;	
		}
		case GuideConditionType.FB:
		{
			int levelId = int.Parse(conParam.ToString());
			if (levelId == sdUICharacter.Instance.iCurrentLevelID && sdGameLevel.instance != null && sdGameLevel.instance.mainChar != null && sdUICharacter.Instance.GetFightUi() != null)
			{
				return true;	
			}
			break;
		}
		case GuideConditionType.FBPass:
		{
			int levelId = int.Parse(conParam.ToString());
			if (levelId == sdUICharacter.Instance.iCurrentLevelID && sdUICharacter.Instance.GetJiesuanWnd() != null)
			{
				return true;
			}
			break;
		}
		case GuideConditionType.GetItem:
		{
			if (sdGameItemMgr.Instance.hasItem(conParam.ToString()))
			{
				return true;	
			}
			break;
		}
		case GuideConditionType.GetPet:
		{
			if (sdNewPetMgr.Instance.hasPet(conParam.ToString()))
			{
				return true;	
			}
			break;
		}
		case GuideConditionType.Level:
		{
			if (sdGameLevel.instance != null && sdGameLevel.instance.mainChar != null 
				&& sdGameLevel.instance.mainChar.Level >= int.Parse(conParam.ToString()))
			{
				return true;	
			}
			break;
		}
		case GuideConditionType.PetLevel:
		{
			break;
		}
		case GuideConditionType.Click:
		{
			if (isListen == false)
			{
				EventDelegate finish = new EventDelegate(OnCanRun);
				if (conParam.ToString() == "0")
				{
					sdUICharacter.Instance.ShowMask(false, null);
					sdUICharacter.Instance.AddMaskEvent(finish, false);
				}
				else
				{
                    string[] nameList = conParam.ToString().Split('|');
                    foreach (string name in nameList)
                    {
                        GameObject obj = GameObject.Find(name);
                        if (obj == null) return false;
                        if (obj.GetComponent<sdRoleWndButton>() == null)
                        {
                            obj.AddComponent<sdRoleWndButton>();
                        }
                        if (!obj.GetComponent<sdRoleWndButton>().onClick.Contains(finish))
                        {
                            obj.GetComponent<sdRoleWndButton>().onClick.Add(finish);
                        }
                    }
				}
				isListen = true;
			}

			return canRun;
		}
		case GuideConditionType.UseItem:
		{
			break;	
		}
		case GuideConditionType.EnterTrigger:
		{
			if (isListen == false)
			{
				string name = conParam.ToString();
				GameObject obj = GameObject.Find(name);
				if (obj != null)
				{
					EventDelegate finish = new EventDelegate(OnCanRun);
					obj.GetComponent<sdBaseTrigger>().onEnter.Add(finish);
                    isListen = true;
				}
			}
			return canRun;
		}
		case GuideConditionType.LeaveTrigger:
		{
			if (isListen == false)
			{
				string name = conParam.ToString();
				GameObject obj = GameObject.Find(name);
				if (obj != null)
				{
					EventDelegate finish = new EventDelegate(OnCanRun);
					obj.GetComponent<sdBaseTrigger>().onLeave.Add(finish);	
				}
				isListen = true;
			}
			return canRun;
		}
		case GuideConditionType.InWorldMap:
		{
			if (sdGameLevel.instance != null && sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldMap)
			{
				return true;
			}
			break;
		}
		case GuideConditionType.ExistObj:
		{
			string name = conParam.ToString();
			GameObject obj = GameObject.Find(name);
			if (obj != null)
			{
				return true;
			}
			break;
		}
		case GuideConditionType.NoObj:
		{
			string name = conParam.ToString();
			GameObject obj = GameObject.Find(name);
			if (obj == null)
			{
				return true;
			}
			break;
		}
        case GuideConditionType.BetterEquip:
        {
            if (sdUICharacter.Instance.GetRoleWnd() == null) return false;
            if (sdGameLevel.instance == null || sdGameLevel.instance.mainChar == null) return false;
            Hashtable needEquip = new Hashtable();
            Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, -1);
            foreach (DictionaryEntry info in itemTable)
            {
                sdGameItem item = info.Value as sdGameItem;
                if (item.equipPos < 0) continue;
                if (!item.CanEquip(sdGameLevel.instance.mainChar)) continue;
                if (needEquip.ContainsKey(item.equipPos))
                {
                    sdGameItem maxItem = needEquip[item.equipPos] as sdGameItem;
                    int maxScore = sdConfDataMgr.Instance().GetItemScore(maxItem.instanceID);
                    int curScore = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                    if (curScore > maxScore)
                    {
                        needEquip[item.equipPos] = item;
                    }
                }
                else
                {
                    needEquip.Add(item.equipPos, item);
                }
            }

            foreach (DictionaryEntry info in needEquip)
            {
                sdGameItem item = info.Value as sdGameItem;
                sdGameItem compareItem = sdGameItemMgr.Instance.getEquipItemByPos(item.equipPos);
                if (compareItem == null)
                {
                    return true;
                }
                int score = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                int compareScore = sdConfDataMgr.Instance().GetItemScore(compareItem.instanceID);
                if (score > compareScore)
                {
                    return true;
                }
            }
            break;
        }
        case GuideConditionType.Unlock:
        {
            if (!isListen)
            {
                if (sdUICharacter.Instance.GetTownUI() == null) return false;
                sdTown town = sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>();
                if (town.SystemUnlockWnd == null) return false;
                SystemUnlockBtn btn = town.SystemUnlockWnd.GetComponentInChildren<SystemUnlockBtn>();
                if (btn == null) return false;
                string name = conParam.ToString();
                if (btn.mSystem != name) return false;
                EventDelegate finish = new EventDelegate(OnCanRun);
                btn.onFinish.Add(finish);
                isListen = true;
            }
            return canRun;
        }
        case GuideConditionType.LevelUnLock:
        {
            if (!isListen)
            {
                if (sdWorldMapPath.TownUI == null) return false;
                
                EventDelegate finish = new EventDelegate(OnCanRun);
                sdWorldMapPath.onFinish.Add(finish);
                isListen = true;
            }
            return canRun;
        }
		}
		return false;	
	}

	void LoadEffect(ResLoadParams param,UnityEngine.Object obj)
	{
		effect = GameObject.Instantiate(obj) as GameObject;
		GameObject item = (GameObject)param.userdata0;
		effect.transform.parent = item.transform.parent;
		effect.transform.localScale = item.transform.localScale;
		effect.transform.localPosition = item.transform.localPosition;
	}

    bool needShowPoint = false;

	void ShowPoint(string param)
	{
		string objName = param;
		string[] wordlist = objName.Split('|');
		objName = wordlist[0];
		GameObject obj = GameObject.Find(objName);
        if (obj != null && obj.transform.parent.name == "Sys1" && obj.name != "Btn_Push")
        {
            Vector3 pos = obj.transform.parent.localPosition;
            pos.x = 640.0f - 120.0f * obj.transform.parent.localScale.x;
            obj.transform.parent.localPosition = pos;
        }
		tempObj = obj;
		sdUICharacter.Instance.ShowMask(true,obj);
		EventDelegate finish = new EventDelegate(OnFinish);
		if (obj != null && obj.layer != LayerMask.NameToLayer("NGUI"))
		{
			sdUICharacter.Instance.AddSceneMaskEvent(finish);
		}
		else
		{
			sdUICharacter.Instance.AddMaskEvent(finish, true);
		}
		
		if (wordlist.Length > 1)
		{
			sdUICharacter.Instance.HideMaskPoint();
		}
        needShowPoint = true;
	}
	
	public void Run()
	{
        //sdUICharacter.Instance.HideMask();
        needShowPoint = false;
		sdUICharacter.Instance.MsgClickCancel();
		int num = 0;
		string[] param = opParam.ToString().Split(';');
		if ((opType & (int)GuideOperationType.None) > 0)
		{
			
		}

		if ((opType & (int)GuideOperationType.ShowArrow) > 0)
		{
			if (param.Length > num)
			{
				string objName = param[num];
				GameObject obj = GameObject.Find(objName);

				sdUICharacter.Instance.ShowArrow(obj);
				++num;
			}
		}

		if ((opType & (int)GuideOperationType.ShowPoint) > 0)
		{
			if (param.Length > num)
			{
				ShowPoint(param[num]);
				++num;
			}
		}

		if ((opType & (int)GuideOperationType.ShowWord) > 0)
		{
			if (param.Length > num)
			{
				string word = param[num];
                if (sdGameLevel.instance == null) return;
				sdMovieDialogue dlg = sdGameLevel.instance.gameObject.GetComponent<sdMovieDialogue>();
				if(dlg == null)
					dlg = sdGameLevel.instance.gameObject.AddComponent<sdMovieDialogue>();
				if(dlg != null)
				{
					dlg.SetMovieInfo(int.Parse(word),true, true, Vector3.one, Vector3.zero);
					sdGameLevel.instance.guideDialogueEnd += OnFinish;
					++num;
				}
			}
		}

		if ((opType & (int)GuideOperationType.ShowEffect) > 0)
		{
			if (param.Length > num)
			{
				string word = param[num];
				string[] wordlist = word.Split('|');
				GameObject item = GameObject.Find(wordlist[0]);
				ResLoadParams p = new ResLoadParams();
				p.info = "effect";
				p.userdata0 = item;
				sdResourceMgr.Instance.LoadResource(wordlist[1], LoadEffect, p);
				++num;
			}
		}

		if ((opType & (int)GuideOperationType.HideObj) > 0)
		{
			if (param.Length > num)
			{
				string objName = param[num];
				GameObject obj = GameObject.Find(objName);
				if (obj != null)
				{
					obj.SetActive(false);
                    if (!sdGuideMgr.Instance.hideList.Contains(objName))
                    {
                        sdGuideMgr.Instance.hideList.Add(objName, obj);
                    }
                    else
                    {
                        sdGuideMgr.Instance.hideList[objName] = obj;
                    }
				}

				++num;
			}
		}

		if ((opType & (int)GuideOperationType.ShowObj) > 0)
		{
			if (param.Length > num)
			{
				string objName = param[num];
				if (sdGuideMgr.Instance.hideList[objName] != null)
				{
					GameObject obj =  sdGuideMgr.Instance.hideList[objName] as GameObject;
					obj.SetActive(true);
				}	
				++num;
			}
		}

		if ((opType & (int)GuideOperationType.PointEquip) > 0)
		{
			if (param.Length > num)
			{
                sdUICharacter.Instance.tipCanEquip = true;
                if (sdGameLevel.instance == null || sdGameLevel.instance.mainChar == null) return;
				string tid = "";
				string[] wordlist = param[num].Split('|');
				if (wordlist.Length == 1)
				{
                    Hashtable needEquip = new Hashtable();
                    Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, -1);
                    foreach (DictionaryEntry itemInfo in itemTable)
                    {
                        sdGameItem item = itemInfo.Value as sdGameItem;
                        if (item.equipPos < 0) continue;
                        if (!item.CanEquip(sdGameLevel.instance.mainChar)) continue;
                        if (needEquip.ContainsKey(item.equipPos))
                        {
                            sdGameItem maxItem = needEquip[item.equipPos] as sdGameItem;
                            int maxScore = sdConfDataMgr.Instance().GetItemScore(maxItem.instanceID);
                            int curScore = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                            if (curScore > maxScore)
                            {
                                needEquip[item.equipPos] = item;
                            }
                        }
                        else
                        {
                            needEquip.Add(item.equipPos, item);
                        }
                    }

                    foreach (DictionaryEntry itemInfo in needEquip)
                    {
                        sdGameItem item = itemInfo.Value as sdGameItem;
                        sdGameItem compareItem = sdGameItemMgr.Instance.getEquipItemByPos(item.equipPos);
                        if (compareItem == null)
                        {
                            tid = item.templateID.ToString();
                            break;
                        }
                        int score = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                        int compareScore = sdConfDataMgr.Instance().GetItemScore(compareItem.instanceID);
                        if (score > compareScore)
                        {
                            tid = item.templateID.ToString();
                            break;
                        }
                    }

                    
				}
				else
				{
					int job = int.Parse(sdGameLevel.instance.mainChar.GetProperty()["Job"].ToString());
					if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior)
					{
						tid = wordlist[0];
					}
					else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic)
					{
						tid = wordlist[1];
					}
					else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue)
					{
						tid = wordlist[2];
					}
					else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister)
					{
						tid = wordlist[3];
					}
				}

                Hashtable info = sdConfDataMgr.Instance().GetItemById(tid);
                sdSlotMgr.Instance.GotoEquip(int.Parse(info["Character"].ToString()));
                Hashtable table = sdSlotMgr.Instance.GetIconList(PanelType.Panel_Bag);
                foreach (DictionaryEntry item in table)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    if (icon.tempId == tid)
                    {
                        icon.gameObject.name = "guide_item";
                        ShowPoint(icon.gameObject.name + "|1");
                    }
                }

				++num;
			}
		}

		if ((opType & (int)GuideOperationType.JumpMap) > 0)
		{
            Debug.Log(string.Format("guide:{0}",id));
			sdUICharacter.JumpToWorldMap();
		}

		if ((opType & (int)GuideOperationType.SmallWord) > 0)
		{
			if (param.Length > num)
			{
				string word = param[num];
                if (sdGameLevel.instance == null) return;
				sdGuideDialogue dlg = sdGameLevel.instance.gameObject.GetComponent<sdGuideDialogue>();
				if (dlg == null)
				{
					dlg = sdGameLevel.instance.gameObject.AddComponent<sdGuideDialogue>();
				}
				if(dlg != null)
				{
					string[] wordlist = word.Split('|');
					Vector3 pos = Vector3.zero;
					if (wordlist.Length >= 3)
					{
						string posStr = wordlist[2];
						string[] posList = posStr.Split('@');
						pos.y = int.Parse(posList[1]);
						pos.x = int.Parse(posList[0]);
					}
				
					dlg.SetMovieInfo(int.Parse(wordlist[1]),new Vector3(1f, 1f, 1f), pos);
					//sdGameLevel.instance.mainCamera.GetComponent<sdGameCamera>().zoomEnd += OnFinish;
					++num;
				}
			}
		}

		if ((opType & (int)GuideOperationType.Spec) > 0)
		{
			if (param.Length > num)
			{
				string tid = param[num];
                if (tid == "2")
                {
                    EventDelegate finish = new EventDelegate(OnFinish);
                    sdUICharacter.Instance.ShowGuideRoll(finish);

                }
                else if (tid == "3")
                {
                    if (sdGameLevel.instance != null)
                    {
                        sdGameLevel.instance.AutoMode = true;
                        sdGameLevel.instance.FullAutoMode = false;
                    }
                }
                else if (tid == "4")
                {
                    sdUICharacter.Instance.tipCanEquip = false;
                }
				++num;
			}
		}

		if ((opType & (int)GuideOperationType.OpenFrame) > 0)
		{
			GameObject panel = GameObject.Find("Sys1");
			if (panel != null)
			{
				GameObject btn = GameObject.Find("Btn_Push");
				UISprite sp = btn.transform.FindChild("Background").GetComponent<UISprite>();
				sdRoleWndButton.sysPanelPos = panel.transform.localPosition.x;
				panel.transform.localPosition = new Vector3(640.0f-120.0f*panel.transform.localScale.x, panel.transform.localPosition.y, panel.transform.localPosition.z);
				sp.spriteName = "btn_c";
			}
		}

        if ((opType & (int)GuideOperationType.PointItemUp) > 0)
        {
            if (param.Length > num)
            {
                if (sdGameLevel.instance == null || sdGameLevel.instance.mainChar == null) return;
                sdUICharacter.Instance.tipCanEquip = true;

                string tid = "";
                string[] wordlist = param[num].Split('|');
                int job = int.Parse(sdGameLevel.instance.mainChar.GetProperty()["Job"].ToString());
                if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior)
                {
                    tid = wordlist[0];
                }
                else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic)
                {
                    tid = wordlist[1];
                }
                else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue)
                {
                    tid = wordlist[2];
                }
                else if (job == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister)
                {
                    tid = wordlist[3];
                }

                Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
                foreach (DictionaryEntry item in iconList)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    if (icon.tempId == tid)
                    {
                        UIDraggablePanel panel = icon.GetComponentInParent<UIDraggablePanel>();
                        Vector3 pos = Vector3.zero;
                        pos.y = -icon.transform.localPosition.y;
                        panel.MoveRelative(pos);
                        panel.UpdateScrollbars(true);
                        icon.gameObject.name = "guide_item";
                        ShowPoint(icon.gameObject.name + "|1");
                        break;
                    }
                }

                ++num;
            }
        }

        if ((opType & (int)GuideOperationType.LockTown) > 0)
        {
            sdUICharacter.Instance.bLockTown = true;
        }
                

		WaitForEvetnt();
	}
	
	void WaitForEvetnt()
	{
        if (!needShowPoint)
        {
            sdUICharacter.Instance.HideMask();
        }
        if ((opType & (int)GuideOperationType.LockTown) <= 0)
        {
            sdUICharacter.Instance.bLockTown = false;
            if(sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
            }
        }

		switch(eventType)
		{
		case GuidePlayerEvetnType.None:
		{
			if ((opType & (int)GuideOperationType.ShowWord) > 0)
			{

			}
			else if ((opType & (int)GuideOperationType.Spec) > 0 && opParam.ToString() == "2")
			{

			}
			else
			{
				OnFinish();
			}


			break;
		}
		case GuidePlayerEvetnType.Click:
		{
			EventDelegate finish = new EventDelegate(OnFinish);
			if (eventParam.ToString() == "0")
			{
				sdUICharacter.Instance.ShowMask(false, null);
				sdUICharacter.Instance.AddMaskEvent(finish, false);
			}
			else
			{
                if ((opType & (int)GuideOperationType.ShowPoint) > 0 || (opType & (int)GuideOperationType.PointEquip) > 0 || (opType & (int)GuideOperationType.PointItemUp) > 0)
				{
					return;
				}
				else
				{
					string name = eventParam.ToString();
					GameObject obj = GameObject.Find(name);
					if (obj != null)
					{
						if (obj.GetComponent<sdRoleWndButton>() == null)
						{
							obj.GetComponent<sdLevelItem>().onClick.Add(finish);
						}
						else
						{
							obj.GetComponent<sdRoleWndButton>().onClick.Add(finish);
						}

					}
				}
			}
			break;
		}
		case GuidePlayerEvetnType.EnterTrigger:
		{
			string name = eventParam.ToString();
			GameObject obj = GameObject.Find(name);
			if (obj != null)
			{
				EventDelegate finish = new EventDelegate(OnFinish);
				obj.GetComponent<sdBaseTrigger>().onEnter.Add(finish);	
			}
			break;
		}
		case GuidePlayerEvetnType.LeaveTrigger:
		{
			string name = eventParam.ToString();
			GameObject obj = GameObject.Find(name);
			if (obj != null)
			{
				EventDelegate finish = new EventDelegate(OnFinish);
				obj.GetComponent<sdBaseTrigger>().onLeave.Add(finish);	
			}
			break;
		}
        case GuidePlayerEvetnType.Timer:
        {
            sdUICharacter.Instance.ShowMask(false, null);
            string time = eventParam.ToString();
            ResLoadParams para = new ResLoadParams();
            if (sdGameLevel.instance == null) return;
            sdGameLevel.OnTime finish = new sdGameLevel.OnTime(OnFinish);
            sdGameLevel.instance.AddTimeEvent(float.Parse(time), para, finish);
            break;
        }
		}
	}

    void OnFinish(ResLoadParams para)
    {
        OnFinish();
    }

	void OnFinish()
	{
		if (eventType == GuidePlayerEvetnType.EnterTrigger && sdGameLevel.instance != null 
		    && sdGameLevel.instance.GetFingerControl() != null)
		{
			sdGameLevel.instance.GetFingerControl().StopMove_ClearPath();
		}

		sdGameLevel.instance.guideDialogueEnd -= OnFinish;

        if (isLock)
        {
            sdUICharacter.Instance.ShowMask(false, null);
        }
        else
        {
            sdUICharacter.Instance.HideMask();
        }
        
		sdUICharacter.Instance.HideArrow();
		if (opType != (int)GuideOperationType.SmallWord && sdGameLevel.instance != null && sdGameLevel.instance.gameObject.GetComponent<sdGuideDialogue>() != null)
		{
			sdGameLevel.instance.gameObject.GetComponent<sdGuideDialogue>().Hide();
		}
		
		if ((eventType == GuidePlayerEvetnType.Click && eventParam.ToString() != "0") 
		    && (opType == (int)GuideOperationType.ShowPoint || opType == (int)GuideOperationType.PointEquip || opType == (int)GuideOperationType.PointItemUp))
		{
			GameObject obj = null;
            if (opType == (int)GuideOperationType.PointEquip || opType == (int)GuideOperationType.PointItemUp)
			{
				obj = tempObj;
			}
			else
			{
				obj = GameObject.Find(eventParam.ToString());
			}
			 
			if (obj != null)
			{
				if (obj.GetComponent<sdShortCutIcon>() != null)
				{
					UICamera.Notify(obj, "OnPress", true);
					UICamera.Notify(obj, "OnPress", false);
				}
				else if (obj.GetComponent<sdSkillIcon>() != null)
				{
					UICamera.Notify(obj, "OnPress", true);
					UICamera.Notify(obj, "OnPress", false);
				}
				else if (obj.GetComponent<sdSlotIcon>() != null && obj.GetComponent<sdSlotIcon>().panel == PanelType.Panel_Bag)
				{
					UICamera.Notify(obj, "OnGuideClick", null);
				}
				else
				{
					UICamera.Notify(obj, "OnClick", null);
				}

			}
		}
		
		sdGuideMgr.Instance.isOperation = false;
		if (isSave)
		{
			sdConfDataMgr.Instance().SetRoleSetting("guide"+classId.ToString(), id.ToString());
            if (classId == 1)
            {
                sdConfDataMgr.Instance().SetSetting("FinishGuide", "1");
            }
		}
	}
	
	public int CompareTo(object item)
	{
		sdGuide compare = item as sdGuide;
		if (compare.id > id) 
		{
			return -1;
		}
		else if (compare.id < id)
		{
			return 1;
		}
		else
		{
			return 0;	
		}
	}
}
