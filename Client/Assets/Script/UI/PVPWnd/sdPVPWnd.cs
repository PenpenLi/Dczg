using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class sdPVPWnd : MonoBehaviour
{
    static int ms_TotalRival = 4;
    static int ms_Hplength = 251;
	GameObject pkLeft = null;
	GameObject pkRight = null;
    GameObject pkRightSuspend = null;
	GameObject awardRight = null;
	GameObject ranklistRight = null;
	//UILabel pk_cash = null;
	//UILabel pk_cash2 = null;
	//UILabel pk_money = null;

    GameObject pkBtn = null;
    GameObject awardBtn = null;
    GameObject ranklistBtn = null;

	//pkleft
	UILabel pk_selfname = null;
    UILabel pk_selflevel = null;
    UILabel pk_selftitle = null;
	UISprite pk_titleimage = null;
    UILabel pk_reputation = null;
    UILabel pk_win = null;
    UILabel pk_lose = null;
    UILabel pk_ranklist = null;
    UILabel pk_score = null;
    UILabel pk_challenge = null;
    UISprite pk_sprite_reputation = null;

    GameObject pk_rewardsPanel = null;
    GameObject pk_rewards = null;
    UILabel pk_refreshpvpmoney = null;

    //pkSuspend
    UILabel pk_lbTimeTick = null;


    //pkrank
    GameObject rank_listPanel = null;
    GameObject rank_listitem = null;

    List<JobAtlasInfo> lstjobAtlas = new List<JobAtlasInfo>();


	void Awake()
	{
		//pk_cash = GameObject.Find("Sprite_medal_bg").GetComponentInChildren<UILabel>();
		//pk_money = GameObject.Find("Sprite_money_bg").GetComponentInChildren<UILabel>();

		pkLeft = GameObject.Find("Sprite_pkleft");
		pkRight = GameObject.Find("Sprite_pkright");
		awardRight = GameObject.Find("Sprite_awardright");
		ranklistRight = GameObject.Find("Sprite_ranklistright");
        pkRightSuspend = GameObject.Find("Sprite_pkrightsuspend");

        pkBtn = GameObject.Find("Sprite_bg_middle").transform.FindChild("bt_pk").gameObject;
        awardBtn = GameObject.Find("Sprite_bg_middle").transform.FindChild("bt_award").gameObject;
        ranklistBtn = GameObject.Find("Sprite_bg_middle").transform.FindChild("bt_ranklist").gameObject;
        
        //pk
        if (pkLeft != null)
        {
            pk_selfname = pkLeft.transform.FindChild("Label_selfname").GetComponent<UILabel>();
            pk_selflevel = pkLeft.transform.FindChild("Label_selflevel").GetComponent<UILabel>();
            pk_selftitle = pkLeft.transform.FindChild("Label_selftitle").GetComponent<UILabel>();
            pk_titleimage = pkLeft.transform.FindChild("Button_titleimage").FindChild("Background").GetComponent<UISprite>();
            pk_reputation = pkLeft.transform.FindChild("Label_reputation").GetComponent<UILabel>();
            pk_win = pkLeft.transform.FindChild("Label_win").GetComponent<UILabel>();
            pk_lose = pkLeft.transform.FindChild("Label_lose").GetComponent<UILabel>();
            pk_ranklist = pkLeft.transform.FindChild("Label_ranklist").GetComponent<UILabel>();
            pk_score = pkLeft.transform.FindChild("Label_score").GetComponent<UILabel>();
            pk_challenge = pkLeft.transform.FindChild("Label_challenge").GetComponent<UILabel>();
            pk_rewardsPanel = GameObject.Find("panel_rewards");
            pk_rewards = GameObject.Find("rewards0");
            pk_sprite_reputation = pkLeft.transform.FindChild("Sprite_reputation_fg").GetComponent<UISprite>();
        }
        if(pkRight != null)
        {
            pk_refreshpvpmoney = pkRight.transform.FindChild("Label_refreshpvpmoney").GetComponent<UILabel>();
        }
        if (pkRightSuspend != null)
        {
            pk_lbTimeTick = pkRightSuspend.transform.FindChild("Label_timetick").GetComponent<UILabel>();
        }
        //rank
        if (ranklistRight != null)
        {
            rank_listPanel = ranklistRight.transform.FindChild("panel_ranklist").gameObject;
            rank_listitem = rank_listPanel.transform.FindChild("ranklistitem0").gameObject;
            rank_listitem.SetActive(false);
        }
        sdPVPManager.Instance.RefreshPKData += ShowPK;
        sdPVPManager.Instance.RefreshRankListData += ShowRankList;
        sdPVPManager.Instance.RefreshRewardData += ShowAward;
	}

    void FixedUpdate()
    {
        for (int index = 0; index < lstjobAtlas.Count; )
        {
            UIAtlas atlas = sdConfDataMgr.Instance().GetAtlas(lstjobAtlas[index].jobname);
            if (atlas != null)
            {
                lstjobAtlas[index].sprite.atlas = atlas;
                lstjobAtlas.RemoveAt(index);
            }
            else
                ++index;
        }
    }

	public void ShowPK()
	{
        if (pkLeft == null)
            return;
        pkLeft.SetActive(true);
		awardRight.SetActive(false);
		ranklistRight.SetActive(false);
        SetCatalogStatus(1);
        RefreshPK();
	}

    void RefreshPK()
    {
        sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
        if (pk_selfname == null)
        {
            Debug.Log("pkself name == null");
            return;
        }
        lstjobAtlas.Clear();
		// military level
        Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
        int totalReputation = 0;
        int index = 1;
        if (sdPVPManager.Instance.nMilitaryLevel > 1)
        {
            for (; index < sdPVPManager.Instance.nMilitaryLevel; index++)
            {
                Hashtable militaryTable = militarylevelTable[index.ToString()] as Hashtable;
                totalReputation += int.Parse((string)militaryTable["reputation"]);
            }
        }
		Hashtable mymilitary = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel).ToString()] as Hashtable;
		if (mymilitary == null) return;
        string NameColor = mymilitary["namecolor"].ToString();
        NameColor = NameColor.Substring(1);
        pk_selfname.text = "[" + NameColor + "]" + kPlayerInfo.mRoleName;
        pk_selflevel.text = "Lv." + sdGameLevel.instance.mainChar.Level.ToString();

		pk_titleimage.spriteName = mymilitary["icon"] as string;
		Hashtable mynextmilitary = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel+1).ToString()] as Hashtable;
		if (mynextmilitary == null)
        {
            pk_reputation.text = "满级";
            pk_sprite_reputation.width = ms_Hplength;
        }
        else
        {
            int value = (int)sdPVPManager.Instance.nReputation + totalReputation;
            int needvalue = int.Parse((string)mymilitary["reputation"]) + totalReputation;
            pk_reputation.text = value.ToString() + "/" + needvalue.ToString();
            float fpercent = value / (float)needvalue;
            fpercent = fpercent > 1.0f ? 1.0f : fpercent;
            pk_sprite_reputation.width = (int)(ms_Hplength * fpercent);
        }
		pk_selftitle.text = (string)mymilitary["name"];

        pk_win.text = "胜"+sdPVPManager.Instance.nWin.ToString();
        pk_lose.text = "负" + sdPVPManager.Instance.nLose.ToString();
        pk_ranklist.text = sdPVPManager.Instance.nRank.ToString();
        pk_score.text = sdPVPManager.Instance.nScore.ToString();
        pk_challenge.text = sdPVPManager.Instance.nChallenge.ToString() + "次";

        if (sdPVPManager.Instance.m_ChallengeBuyLeft <= 0)
            pkLeft.transform.FindChild("Button_addchallenge").gameObject.SetActive(false);
        else
            pkLeft.transform.FindChild("Button_addchallenge").gameObject.SetActive(true);

        if (sdPVPManager.Instance.mMilitaryRewards)
        {
            pkLeft.transform.FindChild("Button_getrewards").FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
        }
        if (sdPVPManager.Instance.m_bPVPSuspend)
        {
            pkRight.SetActive(false);
            pkRightSuspend.SetActive(true);
        }
        else
        {
            pkRight.SetActive(true);
            pkRightSuspend.SetActive(false);
            Hashtable mainProp = (Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"];

            List<stPVPRival> rivalList = sdPVPManager.Instance.GetRivallist();
            index = 0;
            for (; index < ms_TotalRival && index < rivalList.Count; ++index)
            {
                stPVPRival rival = rivalList[index];
                Transform character = pkRight.transform.FindChild("Sprite_character" + index);
                character.gameObject.SetActive(true);
                UISprite headicon = character.FindChild("Sprite_headicon1").GetComponent<UISprite>();
                UILabel level = character.FindChild("Label_level1").GetComponent<UILabel>();
                level.text = "Lv." + rival.nLevel.ToString();
                UILabel name = character.FindChild("Label_name1").GetComponent<UILabel>();
                name.text = rival.strName;
                //职业aaa
                UISprite title = character.FindChild("Sprite_title1").GetComponent<UISprite>();
                UIAtlas jobAtlas = sdConfDataMgr.Instance().GetAtlas(rival.nProfession.ToString());
                if (jobAtlas != null)
                    title.atlas = jobAtlas;
                else
                {
                    sdConfDataMgr.Instance().LoadJobAtlas(rival.nProfession);
                    JobAtlasInfo item = new JobAtlasInfo();
                    item.jobname = rival.nProfession.ToString();
                    item.sprite = title;
                    lstjobAtlas.Add(item);
                }
                title.spriteName = sdConfDataMgr.Instance().GetJobIcon(rival.nProfession.ToString());
                UILabel titlename = character.FindChild("Label_title1").GetComponent<UILabel>();
                if (militarylevelTable.ContainsKey((rival.nMilitaryLevel).ToString()))
                {
                    Hashtable table2 = militarylevelTable[(rival.nMilitaryLevel).ToString()] as Hashtable;
                    if (table2 == null)
                        titlename.text = "满级";
                    else
                        titlename.text = (string)table2["name"];
                    //title.spriteName = table2["icon"].ToString();
                }
                UILabel score = character.FindChild("Label_score1").GetComponent<UILabel>();
                score.text = rival.nScore.ToString();

                int nwin = CalcWinPoints((int)sdPVPManager.Instance.nScore, (int)rival.nScore);
                int nlose = CalcLosePoints((int)sdPVPManager.Instance.nScore, (int)rival.nScore);

                UILabel winreputation = character.FindChild("Label_winreputation1").GetComponent<UILabel>();
                winreputation.text = "+5 声望";
                UILabel winscore = character.FindChild("Label_winscore1").GetComponent<UILabel>();
                winscore.text = "+" + nwin.ToString() + " 积分";
                UILabel losereputation = character.FindChild("Label_losereputation1").GetComponent<UILabel>();
                losereputation.text = "+2 声望";
                UILabel losescore = character.FindChild("Label_losescore1").GetComponent<UILabel>();
                losescore.text = "-" + nlose.ToString() + " 积分";

                sdConfDataMgr.Instance().SetHeadPic(rival.nSex, rival.hairstyle, rival.haircolor, character.FindChild("Sprite_headicon1").GetComponent<UISprite>());
            }
            for (; index < ms_TotalRival; index++)
            {
                Transform character = pkRight.transform.FindChild("Sprite_character" + index);
                character.gameObject.SetActive(false);
            }
            if (sdPVPManager.Instance.m_bMilitaryLevelUp)
            {
                sdUICharacter.Instance.ShowPVPTitleUpWnd(true);
                sdPVPManager.Instance.m_bMilitaryLevelUp = false;
            }

            Hashtable configTable = sdConfDataMgr.Instance().GetTable("config");
            if (configTable.ContainsKey("refreshpvp"))
            {
                Hashtable table = configTable["refreshpvp"] as Hashtable;
                pk_refreshpvpmoney.text = table["value"] as string;
            }
        }
    }

	void ShowAward()
	{
        if (pkLeft == null)
            return;
        awardRight.SetActive(true);
		pkLeft.SetActive(false);
		pkRight.SetActive(false);
        pkRightSuspend.SetActive(false);
		ranklistRight.SetActive(false);
        RefreshAward();
        SetCatalogStatus(2);
	}

    void RefreshAward()
    {
        if (awardRight == null)
            return;
        UILabel todayreputation = awardRight.transform.FindChild("Label_todayreputation").GetComponent<UILabel>();
        todayreputation.text = sdPVPManager.Instance.nDayReputation.ToString();

        List<Hashtable> Rewards = sdConfDataMgr.Instance().GetList("reputationRewards");
        for (int index = 0; index < Rewards.Count; ++index)
        {
            Hashtable table = Rewards[index];
            Transform transItem = pk_rewardsPanel.transform.FindChild("rewards" + index.ToString());
            GameObject uiItem = null;
            if (transItem == null)
            {
                uiItem = GameObject.Instantiate(pk_rewards) as GameObject;
                uiItem.name = "rewards" + index.ToString();
                uiItem.transform.parent = pk_rewardsPanel.transform;
                uiItem.transform.localPosition = new Vector3(pk_rewards.transform.localPosition.x, pk_rewards.transform.localPosition.y - 145.0f * index, 0);
                uiItem.transform.localScale = Vector3.one;
                uiItem.transform.localRotation = Quaternion.identity;
            }
            else
                uiItem = transItem.gameObject;
            uiItem.SetActive(true);
            UILabel label_rewards = uiItem.transform.FindChild("Label_rewards20").GetComponent<UILabel>();
            label_rewards.text = "今日获得" + table["reputation"] + "点声望";

            string value = table["rewards"] as string;
            string[] items = value.Split(new char[] { ';' });
            int j = 1;
            for (j = 1; j <= items.Length; ++j)
            {
                sdSlotIcon slotion = uiItem.transform.FindChild("Sprite_rewards20_" + j).GetComponent<sdSlotIcon>();
                slotion.panel = PanelType.Panel_PvpReward;
                string[] item = items[j - 1].Split(new char[]{'-'});
                if (item.Length == 2)
                {
                    int templateID = int.Parse(item[0]);
                    bool bSound = true;
                    if (templateID == 100 || templateID == 101 || templateID == 200)
                        bSound = false;
                    UIPlaySound sound = slotion.gameObject.GetComponent<UIPlaySound>();
                    if (sound)
                        sound.enabled = bSound;
					Hashtable iteminfo = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
					slotion.SetInfo(templateID.ToString(),iteminfo);

					uiItem.transform.FindChild("Sprite_rewards20_" + j).FindChild("sp_iconframe").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder( int.Parse(iteminfo["Quility"].ToString()) );

                    UILabel label_num = uiItem.transform.FindChild("Sprite_rewards20_" + j).FindChild("Label_num").GetComponent<UILabel>();
                    int nCount = int.Parse(item[1]);
					if (nCount > 1) label_num.text = nCount.ToString();
                }
            }
            for (; j <= 1; j++)
            {
                uiItem.transform.FindChild("Sprite_rewards20_" + j).gameObject.SetActive(false);
            }

            int reputationValue = int.Parse((string)table["reputation"]);
            if(reputationValue <= sdPVPManager.Instance.nDayReputation)
            {
                uint rewardFlag = sdPVPManager.Instance.mRewardFlag;
                if ((rewardFlag & (1 << index)) != 0)
                {
                    GameObject btn = uiItem.transform.FindChild("Button_rewards20").gameObject;
                    btn.SetActive(false);
                    GameObject getReward = uiItem.transform.FindChild("Sprite_getrewards").gameObject;
                    getReward.SetActive(true);
                 }
                else
                {
                    GameObject getReward = uiItem.transform.FindChild("Sprite_getrewards").gameObject;
                    getReward.SetActive(false);
                    GameObject btn = uiItem.transform.FindChild("Button_rewards20").gameObject;
                    btn.SetActive(true);
                }
            }
            else
            {
                GameObject getReward = uiItem.transform.FindChild("Sprite_getrewards").gameObject;
                getReward.SetActive(false);
                GameObject btn = uiItem.transform.FindChild("Button_rewards20").gameObject;
                btn.SetActive(true);
				uiItem.transform.FindChild("Button_rewards20").FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
            }
        }
    }

	void ShowRankList()
	{
        if (pkLeft == null)
            return;
        ranklistRight.SetActive(true);
		pkLeft.SetActive(false);
		pkRight.SetActive(false);
        pkRightSuspend.SetActive(false);
		awardRight.SetActive(false);
        RefreshRankList();
        SetCatalogStatus(3);
	}

    void RefreshRankList()
    {
        if (ranklistRight == null) return;
        Transform trans = ranklistRight.transform.FindChild("Label_rank");
        if (trans == null) return;
        UILabel cur_rank = trans.GetComponent<UILabel>();
        cur_rank.text = sdPVPManager.Instance.nRank.ToString();
        trans = ranklistRight.transform.FindChild("Label_score");
        if (trans == null) return;
        UILabel cur_score = trans.GetComponent<UILabel>();
        cur_score.text = sdPVPManager.Instance.nScore.ToString();
        lstjobAtlas.Clear();
        List<stPVPRival> ranklist = sdPVPManager.Instance.GetRanklist();
        for (int i = 0; i < ranklist.Count; ++i)
        {
            stPVPRival item = ranklist[i];
            GameObject uiItem = null;
            Transform trans_item = rank_listPanel.transform.FindChild("ranklistitem" + i.ToString());
            if (trans_item == null)
                uiItem = GameObject.Instantiate(rank_listitem) as GameObject;
            else
                uiItem = trans_item.gameObject;
            uiItem.SetActive(true);
            uiItem.name = "ranklistitem" + i.ToString();
            uiItem.transform.parent = rank_listPanel.transform;
            uiItem.transform.localPosition = new Vector3(rank_listitem.transform.localPosition.x, rank_listitem.transform.localPosition.y - 145.0f * i, 0);
            uiItem.transform.localScale = Vector3.one;
            uiItem.transform.localRotation = Quaternion.identity;
           
            UILabel label_name = uiItem.transform.FindChild("Label_ranklistname").GetComponent<UILabel>();
            label_name.text = item.strName;
            UILabel label_level = uiItem.transform.FindChild("Label_ranklistlevel").GetComponent<UILabel>();
            label_level.text = "Lv." + item.nLevel.ToString();
            UILabel label_title = uiItem.transform.FindChild("Label_ranklisttitlename").GetComponent<UILabel>();
            Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
            Hashtable table = militarylevelTable[(item.nMilitaryLevel).ToString()] as Hashtable;
            if (table == null)
                label_title.text = "满级";
            else
                label_title.text = (string)table["name"];
            UILabel label_score = uiItem.transform.FindChild("Label_ranklistscore").GetComponent<UILabel>();
            label_score.text = "积分: " + item.nScore.ToString();
            if (i <= 2)
            {
                GameObject label_rank = uiItem.transform.FindChild("Label_rankindex").gameObject;
                label_rank.SetActive(false);
				UISprite sprite = uiItem.transform.FindChild("Sprite_rankicon").GetComponent<UISprite>();
				sprite.spriteName = "if-n"+(i+1).ToString();
				sprite = uiItem.transform.FindChild("Sprite_rankframe").GetComponent<UISprite>();
				sprite.spriteName = "if-n"+(i+1).ToString()+"a";
            }
            else
            {
				GameObject label_rank = uiItem.transform.FindChild("Label_rankindex").gameObject;
				label_rank.SetActive(true);
				if( i < 9 )
					label_rank.GetComponent<UILabel>().text = "第 " + (i+1).ToString() + " 名";
				else
					label_rank.GetComponent<UILabel>().text = "第" + (i+1).ToString() + "名";
				UISprite sprite = uiItem.transform.FindChild("Sprite_rankicon").GetComponent<UISprite>();
				sprite.spriteName = "if-n4";
				sprite = uiItem.transform.FindChild("Sprite_rankframe").GetComponent<UISprite>();
				sprite.spriteName = "if-n4a";
            }
            UISprite sprite_profession = uiItem.transform.FindChild("Sprite_ranklisttitleicon").GetComponent<UISprite>();
            UIAtlas jobAtlas = sdConfDataMgr.Instance().GetAtlas(item.nProfession.ToString());
            if (jobAtlas != null)
                sprite_profession.atlas = jobAtlas;
            else
            {
                sdConfDataMgr.Instance().LoadJobAtlas(item.nProfession);
                JobAtlasInfo info = new JobAtlasInfo();
                info.jobname = item.nProfession.ToString();
                info.sprite = sprite_profession;
                lstjobAtlas.Add(info);
            }
            sprite_profession.spriteName = sdConfDataMgr.Instance().GetJobIcon(item.nProfession.ToString());
 
           //头像
            sdConfDataMgr.Instance().SetHeadPic(item.nSex, item.hairstyle, item.haircolor, uiItem.transform.FindChild("Sprite_rankheadicon").GetComponent<UISprite>());
        }
    }

    void SetCatalogStatus(int index)
    {
        ResLoadParams param = new ResLoadParams();
        param.userdata0 = index;
        sdResourceMgr.Instance.LoadResource("UI/$common/common.prefab", LoadCatalog, param, typeof(UIAtlas));
    }

    void LoadCatalog(ResLoadParams param, Object obj)
    {
        if (pkBtn == null)
            return;
        UISprite pk = pkBtn.GetComponentInChildren<UISprite>();
        if (pk == null)
            return;
		UISprite rewards = awardBtn.GetComponentInChildren<UISprite>();
		UISprite ranklist = ranklistBtn.GetComponentInChildren<UISprite>();

        UISprite pk_btn_bg = pkBtn.transform.FindChild("sp_tab").FindChild("Background").GetComponent<UISprite>();
		UISprite reward_btn_bg = awardBtn.transform.FindChild("sp_tab").FindChild("Background").GetComponent<UISprite>();
		UISprite ranklist_btn_bg = ranklistBtn.transform.FindChild("sp_tab").FindChild("Background").GetComponent<UISprite>();
        UIAtlas atlas = obj as UIAtlas;
        int index = (int)param.userdata0;
        if (index == 1)
        {
            pk.spriteName = "btn_Tab_click";
            rewards.spriteName = "btn_Tab_nml";
            ranklist.spriteName = "btn_Tab_nml";
            pk_btn_bg.spriteName = "jj";
            reward_btn_bg.spriteName = "jl2";
            ranklist_btn_bg.spriteName = "phb2";
            //pk.height = 70;
            //rewards.height = 62;
            //ranklist.height = 62;
        }
        else if (index == 2)
        {
            pk.spriteName = "btn_Tab_nml";
            rewards.spriteName = "btn_Tab_click";
            ranklist.spriteName = "btn_Tab_nml";
            pk_btn_bg.spriteName = "jj2";
            reward_btn_bg.spriteName = "jl";
            ranklist_btn_bg.spriteName = "phb2";
            //pk.height = 62;
            //rewards.height = 70;
            //ranklist.height = 62; 
        }
        else
        {
            pk.spriteName = "btn_Tab_nml";
            rewards.spriteName = "btn_Tab_nml";
            ranklist.spriteName = "btn_Tab_click";
            pk_btn_bg.spriteName = "jj2";
            reward_btn_bg.spriteName = "jl2";
            ranklist_btn_bg.spriteName = "phb";
            //pk.height = 62;
            //rewards.height = 62;
            //ranklist.height = 70; 
        }
    }

    int  CalcWinPoints( int nSelfPoints, int nOtherPoints )
    {
	    int nAddPoints = 50;
	    int nAddPointsMax = 120;
	    int nAddPointsMin = 20;
	    int percent = 500;
	    int diffPoints = nSelfPoints*percent/10000;
	
	    if (nOtherPoints >= (nSelfPoints - diffPoints) && nOtherPoints <= (nSelfPoints + diffPoints))
	    {
		    //nAddPoints = 50;
	    }
	    else if (nOtherPoints > (nSelfPoints + diffPoints))
	    {
		    //nAddPoints = 50;
		    int nSelfPointsTemp = nSelfPoints + diffPoints;
		    while (nOtherPoints > nSelfPointsTemp)
		    {
			    nAddPoints += 10;
			    if (nAddPoints >= nAddPointsMax)
			    {
				    nAddPoints = nAddPointsMax;
				    break;
			    }
			    nSelfPointsTemp += diffPoints;
		    }
	    }
	    else if (nOtherPoints < (nSelfPoints - diffPoints))
	    {
		    int nSelfPointsTemp = nSelfPoints - diffPoints;
		    while (nOtherPoints < nSelfPointsTemp)
		    {
			    nAddPoints -= 10;
			    if (nAddPoints <= nAddPointsMin)
			    {
				    nAddPoints = nAddPointsMin;
				    break;
			    }
			    nSelfPointsTemp -= diffPoints;
		    }
	    }

	    if (nAddPoints < nAddPointsMin || nAddPoints > nAddPointsMax)
	    {
		    return -1;
	    }

	    return nAddPoints;

    }

    int CalcLosePoints( int nSelfPoints, int nOtherPoints )
    {
	    int nCostPoints = 30;
	    int nCostPointsMax = 50;
	    int nCostPointsMin = 10;
	    int percent = 500;
	    int diffPoints = nSelfPoints*percent/10000;

	    if (nOtherPoints >= (nSelfPoints - diffPoints) && nOtherPoints <= (nSelfPoints + diffPoints))
	    {
		    //nCostPoints = 30;
	    }
	    else if (nOtherPoints > (nSelfPoints + diffPoints))
	    {
		    //nCostPoints = 30;
		    int nSelfPointsTemp = nSelfPoints + diffPoints;
		    while (nOtherPoints > nSelfPointsTemp)
		    {
			    nCostPoints -= 5;
			    if (nCostPoints <= nCostPointsMin)
			    {
				    nCostPoints = nCostPointsMin;
				    break;
			    }
			    nSelfPointsTemp += diffPoints;
		    }
	    }
	    else if (nOtherPoints < (nSelfPoints - diffPoints))
	    {
		    int nSelfPointsTemp = nSelfPoints - diffPoints;
		    while (nOtherPoints < nSelfPointsTemp)
		    {
			    nCostPoints += 5;
			    if (nCostPoints >= nCostPointsMax)
			    {
				    nCostPoints = nCostPointsMax;
				    break;
			    }
			    nSelfPointsTemp -= diffPoints;
		    }
	    }

	    if (nCostPoints < nCostPointsMin || nCostPoints > nCostPointsMax)
	    {
		    return -1;
	    }

	    return nCostPoints;
    }

    void Update()
    {
        if (sdPVPManager.Instance.m_bPVPSuspend == false)
            return;
        string str = "";
        long timetick = sdPVPManager.Instance.m_nTimeTick - System.DateTime.Now.Ticks / 10000000L;
        //pvp开始倒计时
        if (timetick >= 3600L)
        {
            int hour = (int)(timetick / 3600L);
            timetick = timetick % 3600;
            str = hour.ToString() + "小时";
        }
        if (timetick >= 60L)
        {
            int minute = (int)(timetick / 60L);
            timetick = timetick % 60;
            str = str + minute.ToString() + "分钟";
        }
        str = str + timetick.ToString() + "秒";
        pk_lbTimeTick.text = "竞技场开启倒计时：" + str;
    }
}
