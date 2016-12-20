using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdJiesuanClick : MonoBehaviour 
{
	bool haveNext = false;
	private static AudioSource asWin = null;
	void LoadMusic(ResLoadParams param, Object obj)
	{
		AudioClip au = obj as AudioClip; 
		if( asWin )
		{
			asWin.Stop();
			asWin.clip = au;
			asWin.loop = true;
			asWin.Play();
		}
	}

	public void Start()
	{
		//深渊BOSS结算，只需要显示评级那个界面..
		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
			haveNext = true;
		}
		else
		{
			haveNext = false;
		}

        if (nextBtn != null)
        {
            nextBtn.onClick.Add(new EventDelegate(OnClickNext));
            nextBtn.gameObject.SetActive(false);
        }

        if (goOnBtn != null)
        {
            goOnBtn.onClick.Add(new EventDelegate(OnClickGoOn));
            goOnBtn.gameObject.SetActive(false);
        }

        if (backBtn != null)
        {
            backBtn.onClick.Add(new EventDelegate(OnClickBack));
            backBtn.gameObject.SetActive(false);
        }
	}

    void OnClickNext()
    {
        if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PET_TRAIN)//战魂试炼aaa
        {
            sdUICharacter.Instance.ShowPTWnd(true);
            if (sdUICharacter.Instance.GetJiesuanWnd() != null)
                sdUICharacter.Instance.GetJiesuanWnd().SetActive(false);           
        }
        else
        {
            sdUICharacter.Instance.JiesuanGoNext(1);
            haveNext = true;
            nextBtn.gameObject.SetActive(false);
        }
    }

    void OnClickGoOn()
    {
        GameObject obj = GameObject.Find("jiesuanWnd(Clone)");
        if (obj) obj.SetActive(false);
        AudioSource[] audios = GameObject.Find("@GameLevel").GetComponentsInChildren<AudioSource>(false);
        foreach (AudioSource au in audios)
        {
            if (au.isPlaying)
            {
                ResLoadParams param = new ResLoadParams();
                sdResourceMgr.Instance.LoadResource("Music/$camp_theme01.ogg", LoadMusic, param);
                asWin = au;
                break;
            }
        }

        sdUICharacter.Instance.ShowTuitu();
    }

    void OnClickBack()
    {
        GameObject obj = GameObject.Find("jiesuanWnd(Clone)");
        if (obj) obj.SetActive(false);
        sdUICharacter.Instance.TuiTu_To_WorldMap();
    }

    bool isFinish = false;

    void Update()
    {
        if (!haveNext)
        {
            if (sdUICharacter.Instance.JiesuanCanGoNext(1))
            {
                nextBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            if (sdUICharacter.Instance.JiesuanCanGoNext(2) && !isFinish)
            {
                isFinish = true;
                goOnBtn.gameObject.SetActive(true);
                backBtn.gameObject.SetActive(true);
                
                if (sdFriendMgr.Instance.fightFriend != null && sdActGameMgr.Instance.m_bTiggerLapBossWnd == false)
                {
                    if (sdFriendMgr.Instance.GetFriend(sdFriendMgr.Instance.fightFriend.id) == null)
                    {
                        sdUICharacter.Instance.ShowAddFriWnd();
                    }

                    sdFriendMgr.Instance.RemoveFightFri(sdFriendMgr.Instance.fightFriend.id);
                    sdFriendMgr.Instance.fightFriend = null;
                }

                // 客户端打开后续关卡..
                int nextlevel = sdUICharacter.Instance.iCurrentLevelID;
                //Debug.Log(nextlevel + " Current Level ");
                for (int i = 0; i < sdLevelInfo.levelInfos.Length; i++)
                {
                    if (sdLevelInfo.levelInfos[i].levelID == sdUICharacter.Instance.iCurrentLevelID)
                    {
                        if (sdLevelInfo.levelInfos[i].crystal < 1)
                        {
                            sdLevelInfo.levelInfos[i].crystal = 1;
                        }
                    }
                    else if ((int)sdLevelInfo.levelInfos[i].levelProp["PrecedentID"] == sdUICharacter.Instance.iCurrentLevelID)
                    {
                        sdLevelInfo.levelInfos[i].valid = true;
                        if (sdLevelInfo.levelInfos[i].levelID > nextlevel)
                            nextlevel = sdLevelInfo.levelInfos[i].levelID;
                    }
                }
                sdUICharacter.Instance.iCurrentLevelID = 0;

                // 判断是否是第一次完成这个关卡...
                GameObject obj = GameObject.Find("jiesuanWnd(Clone)");
//                 if (obj) obj.SetActive(false);
//                 if (onClick.Count > 0)
//                 {
//                     sdUICharacter.Instance.GetJiesuanWnd().SetActive(false);
//                     EventDelegate.Execute(onClick);
//                     onClick.Clear();
//                     return;
//                 }
                if (sdUICharacter.Instance.bCampaignLastLevel ||
                   sdUICharacter.Instance.GetBattleType() == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
                {
                    // 深渊BOSS打完后，直接回worldMap，并且显示深渊界面..
                    if (sdUICharacter.Instance.GetBattleType() == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
                    {
                        sdActGameControl.Instance.ActiveLapBossWnd(null, 1);
                    }
                    else
                    {
                        if (nextlevel == 21011)
                        {
                            // 判断是是否是主城之后第一个关卡被解锁，则表现为解锁主城..
                            nextlevel = 20000;
                            sdGlobalDatabase.Instance.globalData["OpenLevel_MainCity"] = 1;
                        }
                        sdWorldMapPath.SetLevel(nextlevel / 1000, true);
                    }
                }
                else
                {
                   
                }

                //如果触发了深渊，需要立即显示界面..
//                 if (sdActGameMgr.Instance.m_bTiggerLapBossWnd == true)
//                 {
//                     sdActGameControl.Instance.ActiveLapBossWnd(null, 2);
//                     sdActGameMgr.Instance.m_bTiggerLapBossWnd = false;
//                 }
            }
        }
    }

    public UIButton nextBtn = null;
    public UIButton goOnBtn = null;
    public UIButton backBtn = null;

	public List<EventDelegate> onClick = new List<EventDelegate>();
	
	void OnClick()
	{
// 		if (!haveNext)
// 		{
// 			haveNext = sdUICharacter.Instance.JiesuanCanGoNext(1);	
// 		}
// 		else
// 		{
// 			if (sdUICharacter.Instance.JiesuanCanGoNext(2))
// 			{
// 				
// 				
// 
// 			}
// 
// 		}
	}
}
