
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuideOperationType
{
	None = 0,
	ShowArrow = 1<<0,
	ShowPoint = 1<<1,
	ShowWord = 1<<2,
	ShowEffect = 1<<3,
	HideObj = 1<<4,
	ShowObj = 1<<5,
	PointEquip = 1<<6, 
	JumpMap = 1<<7,
	SmallWord = 1<<8,
	Spec = 1<<9,
	OpenFrame = 1<<10,
    PointItemUp = 1<<11,
    LockTown = 1<<12,
}

public enum GuideConditionType
{
	None = 0,
	Level,
	FB,
	FBPass,
	GetItem,
	GetPet,
	Click,
	UseItem,
	PetLevel,
	EnterTrigger,
	LeaveTrigger,
	InWorldMap,
	ExistObj,
	NoObj,
    BetterEquip,
    Unlock,
    LevelUnLock,
}

public enum GuidePlayerEvetnType
{
	None,
	Click,
	EnterTrigger,
	LeaveTrigger,
	Slide,
    Timer,
}

public class sdGuideMgr : Singleton<sdGuideMgr>
{
	GameObject guidePanel = null;
	
	Hashtable guideList = new Hashtable();

	public Hashtable hideList = new Hashtable();
	
	public bool isOperation = false;
    bool isInit = false;
	
    public void ClearData()
    {
        isInit = false;
        guideList.Clear();
        hideList.Clear();
    }

    void OnGetGuideList()
    {
        sdUICharacter.Instance.ShowFightUi();
        foreach (DictionaryEntry item in guideList)
        {
            List<sdGuide> list = item.Value as List<sdGuide>;
            list.Sort();
            bool bStop = false;
            while (!bStop)
            {
                if (list.Count <= 0)
                {
                    bStop = true;
                    break;
                }
                else
                {
                    sdGuide guide = list[0];
                    if (guide.isSkip)
                    {
                        list.Remove(guide);
                    }
                    else
                    {
                        bStop = true;
                        break;
                    }
                }
            }
        }
        isOperation = false;
    }

    void OnNoGuide()
    {
        guideList.Clear();
        sdConfDataMgr.Instance().SetRoleSetting("NoGuide", "1");
        sdUICharacter.JumpToWorldMap();
    }

    string playerName = "";
	public void Init(string roleName)
	{
		//int num = 0;
		//ResLoadParams para = new ResLoadParams();
		//para.info = "guide";
		
		//string namePreb = string.Format("UI/Icon/$guide/guide.prefab", name); 
		//sdResourceMgr.Instance.LoadResource(namePreb,SetAtlas,para,typeof(UIAtlas));

        if (isInit) return;
        isInit = true;
        playerName = roleName;
        if (sdConfDataMgr.Instance().GetRoleSetting("NoGuide") == "1")
        {
            return;
        }
        isOperation = true;
        guideList = sdConfDataMgr.Instance().GetGuideList(playerName);
        //List<sdGuide> list = guideList["1"] as List<sdGuide>;
        if (sdUICharacter.Instance.iCurrentLevelID == 1 && sdConfDataMgr.Instance().GetSetting("FinishGuide") == "1")
        {
            sdUICharacter.Instance.HideFightUi();
            sdMsgBox.OnConfirm ok = new sdMsgBox.OnConfirm(OnNoGuide);
            sdMsgBox.OnCancel cancel = new sdMsgBox.OnCancel(OnGetGuideList);
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("SkipGuide"), ok, cancel);
            return;
        }

		OnGetGuideList();
	}
	
    public bool HasCanRun()
    {
        foreach (DictionaryEntry item in guideList)
        {
            List<sdGuide> list = item.Value as List<sdGuide>;
            if (list.Count > 0)
            {
                if (list[0].CanRun())
                {
                    return true;
                }
            }
        }

        return false;
    }

	void Update()
	{
        if (!isOperation && sdGameLevel.instance != null && !sdUICharacter.Instance.IsbbsWndActive() 
            && (sdGameLevel.instance.gameObject.GetComponent<sdMovieDialogue>() == null || !sdGameLevel.instance.gameObject.GetComponent<sdMovieDialogue>().IsWndActive())) 
		{
			foreach(DictionaryEntry item in guideList)	
			{
				List<sdGuide> list = item.Value as List<sdGuide>;	
				if (list.Count > 0)
				{
					if (list[0].CanRun())
					{
						isOperation = true;	
						list[0].Run();
						list.RemoveAt(0);
						return;
					}
				}
			}
		}
	}
}