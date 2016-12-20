using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TreasureInfo
{
	public	int id;
	public	int	count;
	public	int	index;
	public	bool isTemp;
}

public class sdJiesuanWnd : MonoBehaviour
{
	int money = 0;
	int exp = 0;
	int curExp = 0;
	
	List<TreasureInfo> realList = new List<TreasureInfo>();
	List<TreasureInfo> tempList = new List<TreasureInfo>();
	public GameObject moneyObj = null;
	public GameObject moneyPanel = null;
	public GameObject expPanel = null;
	public GameObject itemPanel = null;
	bool moneyStop = false;
	bool expStop = false;
	bool itemStop = false;
	int curIndex = 0;
	public GameObject curLevel = null;
	public GameObject nextLevel = null;
	public GameObject lvPic = null;
	int m_level = 0;
	int m_job = 0;
	public GameObject lvUp = null;
	bool lvMax = false;
	public GameObject scorePanel = null;
	public GameObject timeEffect = null;
	public GameObject getEffect = null;
	public GameObject timeObj = null;
	public GameObject firstPanel = null;
	public GameObject secPanel = null;
	public GameObject lastPanel = null;
	public GameObject levelupEffect = null;
	public GameObject lvupPanel = null;
	public GameObject mainName = null;
	public GameObject mainPro = null;
	public GameObject attPro = null;
	public GameObject defPro = null;
	public GameObject hpPro = null;
	public GameObject skillPro = null;
	public GameObject lvB0 = null;
	public GameObject lvB1 = null;
	public GameObject lvA0 = null;
	public GameObject lvA1 = null;
	public GameObject scoreTip = null;
	public GameObject pinji = null;
	GameObject scoreEffect = null;
	
	public GameObject friIcon = null;
	public GameObject petIcon = null;
	public GameObject friName = null;
	public GameObject petName = null;
	public GameObject friLevel = null;
	public GameObject petLevel = null;
    public GameObject petFrame = null;
    public UILabel expValue = null;
	bool bStep1 = false;
	bool bStep2 = false;

    public GameObject levelS = null;
    public GameObject levelA = null;
    public GameObject levelB = null;
	
	public void GetAllItem()
	{
		bStep2 = true;
	}

    public void GoNext(int step)
    {
        if (step == 1)
        {
            bStep1 = true;
        }
    }
	
	public bool CanGoNext(int step)
	{
		if (step == 1)
		{
			if (itemStop && !showLv) 
			{
				return true;
			}
		}
		else if (step == 2)
		{
			return bStep2;
		}
	
		return false;
	}
	
	public TreasureInfo GetTreasure()
	{
		if (realList.Count <= sdUICharacter.Instance.selectTreasure)
		{
			int index = sdUICharacter.Instance.selectTreasure - realList.Count;
			if(index >= tempList.Count)
				return null;
			return tempList[index];
		}
		return realList[sdUICharacter.Instance.selectTreasure];
	}
	
	public void RemoveTreasure(int index)
	{
		foreach(TreasureInfo info in realList)
		{
			if (info.index == index)
			{
				realList.Remove(info);
				return;
			}
		}
	}
	
	void LoadScoreEffect(int score)
	{
		string level = "B";
		int num = 1;
		if (score == (int)HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_S)
		{
			level = "S";
            levelS.SetActive(true);
		}
        else if (score == (int)HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_A)
        {
            level = "A";
            levelA.SetActive(true);
        }
        else
        {
            levelB.SetActive(true);
        }
		
		if (scoreTip != null)
		{
			scoreTip.GetComponent<UILabel>().text = sdConfDataMgr.Instance().GetShowStr("ScoreTip");	
		}
		
		if (pinji != null)
		{
			pinji.GetComponent<UISprite>().spriteName = level;
            pinji.GetComponent<UISprite>().MakePixelPerfect();
            pinji.SetActive(true);
		}
		
		//string prefabname = string.Format("Effect/MainChar/FX_UI/$Fx_JiesuanZi/Fx_Ui_{0}.prefab", level.ToUpper());
		
		//ResLoadParams param = new ResLoadParams();
		//param.info = "score";
		//sdResourceMgr.Instance.LoadResource(prefabname,OnLoadPrefab,param);
	}
	
	void OnLoadPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null) return;
		if (param.info == "score")
		{
			scoreEffect = GameObject.Instantiate(obj) as GameObject;
			scoreEffect.transform.parent = scorePanel.transform;
			scoreEffect.transform.localPosition = Vector3.zero;
			scoreEffect.transform.localScale = new Vector3(100,100,100);
		}
	}
	
    public void CardRepositionFinish()
    {
        sdTreasureBtn[] list = GetComponentsInChildren<sdTreasureBtn>();
        foreach (sdTreasureBtn btn in list)
        {
            btn.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

	static int CompareItem(sdGameItem item, sdGameItem compare)
	{
		if (compare.quility < item.quility)
		{
			return -1;	
		}
		else if (compare.quility > item.quility)
		{
			return 1;	
		}	
		else
		{
			if (compare.itemClass > item.itemClass) 
			{
				return -1;
			}
			else if (compare.itemClass < item.itemClass)
			{
				return 1;
			}
			else
			{
				if (compare.subClass > item.subClass)
				{
					return -1;	
				}
				else if (compare.subClass < item.subClass)
				{
					return 1;	
				}
				else
				{
					if (compare.level < item.level)
					{
						return -1;	
					}
					else if (compare.level > item.level)
					{
						return 1;	
					}
					else
					{
						return 0;	
					}
				}	
			}
		}	
	}
	
	float fightTime = 0f;

    public UIButton btn_CloseLvPanel = null;
	
	public void Start()
	{
		sdUICharacter.Instance.HideFightUi();
		sdUICharacter.Instance.selectTreasure = 0;
		fightTime = sdUICharacter.Instance.fightTime;
		if (moneyObj != null)
		{
			moneyObj.GetComponent<UILabel>().text = "0";
		}
		
		if (expPanel != null)
		{
			expPanel.SetActive(false);
		}
		
		if (moneyPanel != null)
		{
			moneyPanel.SetActive(false);	
		}
		
		if (itemPanel != null)
		{
			itemPanel.SetActive(false);	
		}
		
		sdGameLevel level = GameObject.Find("@GameLevel").GetComponent<sdGameLevel>();
		if (level != null)
		{
			level.FullAutoMode = false;
		}

        if (btn_CloseLvPanel != null)
        {
            btn_CloseLvPanel.onClick.Add(new EventDelegate(OnLvPanelClose));
        }
		
		sdUICharacter.Instance.HideFightUi();

		//深渊BOSS结算，只需要显示评级那个界面..
		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
			isMoveStop = false;
			isTimeEffectStop = true;
			isTimeStop = true;
			isMoneyEffectStop = true;
			moneyStop = true;
			lvMax = true;
			expStop = true;
			bLvup = false;
			itemShowTime = 0;
			itemStop = true;
			bStep1 = true;
			isMoveStop = true;

			scorePanel.GetComponent<sdAnchor>().enabled = false;
			scorePanel.GetComponent<UIAnchor>().enabled = false;

			scorePanel.transform.localPosition = new Vector3(810.0f, 216.0f, 0.0f);
			firstPanel.transform.localPosition = new Vector3(-1350.0f, 0.0f, 0.0f);
			secPanel.transform.localPosition = new Vector3(0.0f, -240.0f, 0.0f);
		}
		else
		{
			moneyStop = false;
			expStop = false;
			itemStop = false;
		}

		curIndex = 0;
// 		if (lvUp != null)
// 		{
// 			lvUp.SetActive(false);	
// 		}
		lvMax = false;
		
		if (timeObj != null)
		{
			timeObj.GetComponent<UILabel>().text = "";	
		}
		
		if (getEffect != null)
		{
			getEffect.SetActive(false);	
		}
		
	}
	
	public void ShowAddFriWnd()
	{
		if (!bStep2) return;
		lastPanel.SetActive(true);	
	}
	
	void SetAtlas(int iGender, int iHairStyle, UIAtlas atlas)
	{
		friIcon.GetComponent<UISprite>().atlas = atlas;	
	}

    int itemCount = 0;

	public void SetInfo(CliProto.SC_TREASURE_CHEST_NTF msg)
	{
		LoadScoreEffect(sdUICharacter.Instance.fightScore);
        if (sdUICharacter.Instance.GetBattleType()!=(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
            sdUICharacter.Instance.fightScore = 1;
        }
        
		if (sdFriendMgr.Instance.fightFriend != null)
		{
			sdFriend fri = sdFriendMgr.Instance.fightFriend;
			friName.GetComponent<UILabel>().text = fri.name;
			petName.GetComponent<UILabel>().text = fri.petInfo.m_strName;
            petFrame.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetPetQuilityBorder(fri.petInfo.m_iAbility);
			UIAtlas atlas = null;
			string headName;
			//sdConfDataMgr.Instance().SetHeadAtlas += new sdConfDataMgr.HeadAtlas(SetAtlas);
            sdConfDataMgr.Instance().SetHeadPic(fri.gender, fri.hairStyle, fri.color, friIcon.GetComponent<UISprite>());
			//friIcon.GetComponent<UISprite>().atlas = atlas;
			//friIcon.GetComponent<UISprite>().spriteName = headName;

            petIcon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
			petIcon.GetComponent<UISprite>().spriteName = fri.petInfo.m_strIcon;
			friLevel.GetComponent<UILabel>().text = "Lv." + fri.level;
			petLevel.GetComponent<UILabel>().text = "Lv." + fri.petInfo.m_iLevel.ToString();
		}

        float expRate = 1;
        float moneyRate = 1;
        Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
        if (militarylevelTable.ContainsKey(sdPVPManager.Instance.nMilitaryLevel.ToString()))
        {
            Hashtable military = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel).ToString()] as Hashtable;
            expRate = float.Parse(military["experience"].ToString())/100 + 1;
            moneyRate = float.Parse(military["money"].ToString()) /100 + 1;
        }

        exp = (int)(((float)msg.m_Experience) * expRate);
        money = (int)(((float)msg.m_Money) * moneyRate);
		Hashtable table = sdSlotMgr.Instance.GetIconList(PanelType.Panel_Jiesuan);
		int slotNum = 0;
        int num = (int)msg.m_Items.m_ItemCount;
		List<sdGameItem> itemList = new List<sdGameItem>();
        List<sdGamePetItem> petItemList = new List<sdGamePetItem>();
        List<sdGameItem> petCardList = new List<sdGameItem>();
		for (int i = 0; i < num; ++i)
		{
            sdGameItem item = sdGameItemMgr.Instance.getItem(msg.m_Items.m_Items[i].m_UID);
            if (item == null)
            {
                sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(msg.m_Items.m_Items[i].m_UID);
                if (petItem == null)
                {
                    sdGameItem temp = new sdGameItem();
                    temp.templateID = msg.m_Items.m_Items[i].m_TID;
                    Hashtable tempInfo = sdConfDataMgr.Instance().GetItemById(msg.m_Items.m_Items[i].m_TID.ToString());
                    if (tempInfo == null) continue;
                    if (int.Parse(tempInfo["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
                    {
                        petCardList.Add(temp);
                    }
                    else
                    {
                        itemList.Add(temp);
                    }
                    
                    continue;
                }
                petItemList.Add(petItem);
                continue;
            }
			item.isNew = true;
			itemList.Add(item);
		}

        itemCount = num + msg.m_PetAboutCount;
		num = msg.m_PetAboutCount;
		for(int i = 0; i < num; ++i)
		{
			if (table.ContainsKey(slotNum) && table[slotNum] != null)
			{
				sdSlotIcon icon = table[slotNum] as sdSlotIcon;
				if (icon != null)
				{
					Hashtable pet = sdConfDataMgr.Instance().GetItemById(msg.m_PetAbout[i].ToString());
					if (pet == null) continue;
					if (int.Parse(pet["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Equip)
					{
						icon.jiesuanType = JiesuanSlotType.PetItem;
						icon.SetInfo(msg.m_PetAbout[i].ToString(), pet);
						icon.gameObject.SetActive(false);
						icon.GetComponent<BoxCollider>().enabled = true;
					}
					else if (int.Parse(pet["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
					{
						icon.jiesuanType = JiesuanSlotType.Pet;
						icon.SetInfo(msg.m_PetAbout[i].ToString(), pet);
						icon.gameObject.SetActive(false);
						icon.GetComponent<BoxCollider>().enabled = true;
					}
				}
			}
			++slotNum;
		}

        foreach (sdGameItem item in petCardList)
        {
            if (table.ContainsKey(slotNum) && table[slotNum] != null)
            {
                sdSlotIcon icon = table[slotNum] as sdSlotIcon;
                if (icon != null)
                {
                    if (item != null)
                    {
                        Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                        icon.panel = PanelType.Panel_Jiesuan;
                        icon.jiesuanType = JiesuanSlotType.Pet;
                        icon.SetInfo(item.instanceID.ToString(), info);
                        icon.gameObject.SetActive(false);
                    }
                    icon.enable = false;
                    icon.GetComponent<BoxCollider>().enabled = true;
                }
            }
            ++slotNum;
        }

        foreach(sdGamePetItem item in petItemList)
        {
            if (table.ContainsKey(slotNum) && table[slotNum] != null)
            {
                sdSlotIcon icon = table[slotNum] as sdSlotIcon;
                if (icon != null)
                {
                    if (item != null)
                    {
                        Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                        icon.panel = PanelType.Panel_Jiesuan;
                        icon.jiesuanType = JiesuanSlotType.PetItem;
                        icon.SetInfo(item.instanceID.ToString(), info);
                        icon.gameObject.SetActive(false);
                    }
                    icon.enable = false;
                    icon.GetComponent<BoxCollider>().enabled = true;
                }
            }
            ++slotNum;
        }

		itemList.Sort(CompareItem);
		
		foreach(sdGameItem item in itemList)
		{
			if (table.ContainsKey(slotNum) && table[slotNum] != null)
			{
				sdSlotIcon icon = table[slotNum] as sdSlotIcon;
				if (icon != null)
				{
					if (item != null)
					{
						Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
						icon.panel = PanelType.Panel_Jiesuan;
						icon.SetInfo(item.instanceID.ToString(), info);
						icon.gameObject.SetActive(false);
					}
					icon.enable = false;
					icon.GetComponent<BoxCollider>().enabled = true;
				}
			}
			++slotNum;
		}

		while(table.ContainsKey(slotNum) && table[slotNum] != null)
		{
			sdSlotIcon icon = table[slotNum] as sdSlotIcon;
			if (icon != null)
			{
				icon.SetInfo("0",null);	
				icon.enable = false;
				icon.GetComponent<BoxCollider>().enabled = false;
			}
			++slotNum;
		}
		
		int treasureNum = msg.m_GetCount;
		for (int i = 0; i < treasureNum; ++i)
		{
			TreasureInfo info = new TreasureInfo();
			info.index = msg.m_CardInfos[i].m_IndexID;
			info.id = (int)msg.m_CardInfos[i].m_ItemID;
			info.count = (int)msg.m_CardInfos[i].m_Count;
			info.isTemp = false;
			realList.Add(info);
		}
		
		treasureNum = msg.m_TempCount;
		List<int> ranlist = new List<int>();
		for (int i = 0; i < treasureNum; ++i)
		{
			ranlist.Add(i);	
		}
		
		System.Random ran = new System.Random();
		while(ranlist.Count > 0)
		{
			int index = ran.Next(ranlist.Count);
			int indexNum = ranlist[index];
			TreasureInfo info = new TreasureInfo();
			info.index = msg.m_TempCardInfos[indexNum].m_IndexID;
			info.id = (int)msg.m_TempCardInfos[indexNum].m_ItemID;
			info.count = (int)msg.m_TempCardInfos[indexNum].m_Count;
			info.isTemp = true;
			
			tempList.Add(info);
			
			ranlist.RemoveAt(index);
		}
		
		if (sdGameLevel.instance !=  null && sdGameLevel.instance.mainChar != null)
		{
			if (sdGameLevel.instance.mainChar.Property != null)
			{
				curExp = sdUICharacter.Instance.oldExp;
				m_level = sdUICharacter.Instance.oldLevel;
				m_job = int.Parse(sdGameLevel.instance.mainChar.Property["Job"].ToString());
				int maxExp = sdConfDataMgr.Instance().GetLevelExp(m_job.ToString(), m_level.ToString());
				if (maxExp == 0)
				{
					lvMax = true;	
				}
				else
				{
					lvMax = false;	
				}
				
				if (curLevel != null)
				{
					if (maxExp == 0)
					{
						curLevel.GetComponent<UILabel>().text = string.Format("Max");
					}
					else
					{
						curLevel.GetComponent<UILabel>().text = string.Format("Lv.{0}", m_level.ToString());
					}	
				}
				
				if (nextLevel != null)
				{
					if (maxExp == 0)
					{
						nextLevel.GetComponent<UILabel>().text = string.Format("");
					}
					else
					{
						nextLevel.GetComponent<UILabel>().text = string.Format("Lv.{0}", (m_level+1).ToString());
					}
				}
				
				if (lvPic != null)
				{
					if (maxExp == 0)
					{
						lvPic.GetComponent<UISprite>().fillAmount = 0;
					}
					else
					{
						lvPic.GetComponent<UISprite>().fillAmount = (float)((float)curExp/(float)maxExp);	
					}
				}
			}
		}
	}
	
	float nowTime = 0f;
	
	void ShowTime()
	{
		nowTime += fightTime/40;
		if (nowTime >= fightTime)
		{
			if (scoreEffect != null) scoreEffect.SetActive(true);
			pinji.SetActive(true);
			nowTime = fightTime;
			isTimeStop = true;
		}
		
		string hour = ((int)(nowTime/3600)).ToString();
		if (hour.Length == 1)
		{
			hour = "0" + hour;
		}
		string min = ((int)((nowTime % 3600)/60)).ToString();
		if (min.Length == 1)
		{
			min = "0" + min;
		}
		string sec = ((int)((nowTime % 3600)%60)).ToString();
		if (sec.Length == 1)
		{
			sec = "0" + sec;
		}
		
		string time = string.Format("{0} : {1} : {2}", hour, min, sec);
		timeObj.GetComponent<UILabel>().text = time;
	}
	
	void LevelUp()
	{
        //lvUp.SetActive(true);
        scorePanel.SetActive(false); 
		//lvupPanel.SetActive(true);
		WndAni.ShowWndAni(lvupPanel,false,"");

		string main = "";
		int job = int.Parse(sdGameLevel.instance.mainChar.BaseJob.ToString());
		switch (job)
		{
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:
		{
			main = "Str";
			break;	
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:
		{
			main = "Int";	
			break;	
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:
		{
			main = "Dex";	
			break;		
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:
		{
			main = "Fai";	
			break;		
		}	
		}
		mainName.GetComponent<UILabel>().text = sdConfDataMgr.Instance().GetShowStr(main) + ":";
		mainPro.GetComponent<UILabel>().text = "+" + sdUICharacter.Instance.lvupChange[main].ToString();	
		
		attPro.GetComponent<UILabel>().text = "+" + sdUICharacter.Instance.lvupChange["AttDmg"].ToString();	
		defPro.GetComponent<UILabel>().text = "+" + sdUICharacter.Instance.lvupChange["Def"].ToString();	
		hpPro.GetComponent<UILabel>().text = "+" + sdUICharacter.Instance.lvupChange["HP"].ToString();
		skillPro.GetComponent<UILabel>().text = "+" + sdUICharacter.Instance.lvupChange["Skill"].ToString();
	
		int compare = int.Parse(sdUICharacter.Instance.lvupChange["Level"].ToString());
		if (compare >= 10)
		{
			lvB0.GetComponent<UISprite>().spriteName = compare.ToString()[0].ToString();
			lvB1.GetComponent<UISprite>().spriteName = compare.ToString()[1].ToString();
		}
		else
		{
			lvB0.GetComponent<UISprite>().spriteName = compare.ToString()[0].ToString();
			lvB1.GetComponent<UISprite>().spriteName = "";
		}
		
		Hashtable info = sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable;
		int level = int.Parse(info["Level"].ToString());
		if (level >= 10)
		{
			lvA0.GetComponent<UISprite>().spriteName = level.ToString()[0].ToString();
			lvA1.GetComponent<UISprite>().spriteName = level.ToString()[1].ToString();
		}
		else
		{
			lvA1.GetComponent<UISprite>().spriteName = level.ToString()[0].ToString();
			lvA0.GetComponent<UISprite>().spriteName = "";
		}
	}
	
    public void OnLvPanelClose()
    {
        //lvupPanel.SetActive(false);
		WndAni.HideWndAni(lvupPanel,false,"");
        showLv = false;
        scorePanel.SetActive(true); 
    }

    public void ScoreFinish()
    {
        isTimeEffectStop = true;
    }

	float itemShowTime = 0;
	bool isTimeEffectStop = false;
	bool isTimeStop = false;
	bool isMoneyEffectStop = false;
	float moveSpeed = 1f;
	bool isMoveStop = false;
	float nowexp = 0;
	bool bLvup = false;
    bool showLv = false;
	public void Update()
	{
        if (sdUICharacter.Instance.IsTipShow() || lvupPanel.active || sdUIPetControl.Instance.IsPetTipActive())
        {
            if (scorePanel.active)
            {
                scorePanel.SetActive(false);
            }
            
        }
        else
        {
            if (!scorePanel.active)
            {
                scorePanel.SetActive(true);
            } 
        }

		if (!isMoveStop)
		{	
// 			if (timeEffect != null && !isTimeEffectStop)
// 			{
// 				if (!timeEffect.GetComponentInChildren<Animation>().isPlaying)
// 				{
// 					isTimeEffectStop = true;
// 				}
// 			}
			
			if (isTimeEffectStop && !isTimeStop)
			{
				ShowTime();	
			}
			
// 			if (isTimeStop && !isMoneyEffectStop)
// 			{
// 				getEffect.SetActive(true);
// 				if (!getEffect.GetComponentInChildren<Animation>().isPlaying)
// 				{
// 					isMoneyEffectStop = true;
// 				}
// 			}

            if (isTimeStop && moneyObj != null && !moneyStop)
			{
				moneyPanel.SetActive(true);
				int nowMoney = int.Parse(moneyObj.GetComponent<UILabel>().text.ToString());
                int addMoney = (money / 40);
                if (addMoney <= 0) addMoney = 1;
                nowMoney += addMoney;
				if (nowMoney >= money)
				{
					nowMoney = money;	
					moneyStop = true;
				}
				
				moneyObj.GetComponent<UILabel>().text = nowMoney.ToString();
			}
			
			if (moneyStop)
			{
				expPanel.SetActive(true);
				
				if (lvMax)
				{
					//expObj.GetComponent<UILabel>().text = "0";
					expStop = true;
				}
				else
				{
                    int addExp = (exp/40);
                    if (addExp <= 0) addExp = 1;
                    nowexp += addExp;
					if (nowexp >= exp)
					{
						nowexp = exp;	
						expStop = true;
					}
                    expValue.text = "+" + nowexp.ToString();
					if (lvPic != null && !expStop)
					{
						curExp += (exp/40);
						int maxExp = sdConfDataMgr.Instance().GetLevelExp(m_job.ToString(), m_level.ToString());
						if (maxExp == 0) 
						{
							lvMax = true;
							lvPic.GetComponent<UISprite>().fillAmount = 0;
						}
						else
						{
							if (curExp >= maxExp)
							{
								//levelupEffect.SetActive(true);
								bLvup = true;
								++m_level;
								curExp -= maxExp;
								maxExp = sdConfDataMgr.Instance().GetLevelExp(m_job.ToString(), m_level.ToString());
								if (curLevel != null)
								{
									curLevel.GetComponent<UILabel>().text = string.Format("Lv.{0}", m_level.ToString());
								}
								
								if (nextLevel != null)
								{
									nextLevel.GetComponent<UILabel>().text = string.Format("Lv.{0}", (m_level+1).ToString());
								}
								
// 								if (lvUp != null)
// 								{
// 									lvUp.SetActive(true);	
// 								}
							}
							lvPic.GetComponent<UISprite>().fillAmount = (float)curExp/(float)maxExp;
						}
					}
				}
			}
			
			itemShowTime += Time.deltaTime;
			//if (expStop && bLvup)
			//{
				//Animator ani = levelupEffect.GetComponentInChildren<Animator>();
				//if (ani != null)
				//{
				//	itemShowTime = 0;
				//}
				//LevelUp();
			//}
			
			if (expStop && !itemStop && itemShowTime >= 0.1)
			{
				Hashtable table = sdSlotMgr.Instance.GetIconList(PanelType.Panel_Jiesuan);
				if (table.ContainsKey(curIndex) && table[curIndex] != null)
				{
					sdSlotIcon icon = table[curIndex] as sdSlotIcon;
					if (icon.itemid != "0" && icon.itemid != "")
					{
						icon.gameObject.SetActive(true);
						icon.enable = true;
					}
					
					++curIndex;
				}
				else
				{
					itemStop = true;
				}
				itemShowTime = 0;
                Vector3 pos = itemPanel.transform.localPosition;

                if (itemCount == 3)
                {
                    pos.x = -115;    
                }
                else if (itemCount == 2)
                {
                    pos.x = -60;
                }
                else
                {
                    pos.x = 0;
                }
                itemPanel.transform.localPosition = pos;
                itemPanel.SetActive(true);
			}

            if (bLvup)
            {
                LevelUp();
                bLvup = false;
                showLv = true;
            }

            if (!showLv && bStep1 && itemStop && !isMoveStop)
			{
                firstPanel.GetComponent<TweenPosition>().enabled = true;
                secPanel.SetActive(true);
				isMoveStop = true;	
			}
		}
	}
}