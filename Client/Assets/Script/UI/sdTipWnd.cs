using System;
using System.Collections;
using UnityEngine;

public enum TipType
{
	Item,
	TempItem,
	Skill,
	PassiveSkill,
}

public class sdTipWnd : MonoBehaviour
{
	public string CurrentId = "";
    TipType tipType;
	public GameObject skillPanel = null;
	public GameObject passivePanel = null;
	public GameObject itemPanel = null;
	public GameObject itemArrow = null;
	public GameObject itemQuality = null;
	public GameObject equipBtn = null;
	public GameObject unEquipBtn = null;
	public GameObject sellBtn = null;
    public UIButton useBtn = null;
    public GameObject itemUpBtn = null;
	public GameObject replaceBtn = null;
	public GameObject learnBtn = null;
	public GameObject nextSkill = null;
	public GameObject skillBg = null;
    public GameObject lockBtn = null;
    public GameObject labelFuwen = null;
    public GameObject labelAtt = null;
    public GameObject labelDes = null;

    public GameObject suitPanel1 = null;
    public GameObject suitPanel2 = null; 

	public UILabel suitName = null;
	public UILabel suitName1 = null;
	public UILabel suitName2 = null;
	public UILabel suitName3 = null;
	public UILabel suitEffect = null;
	public UILabel suitEffect1 = null;
	public UILabel suitEffect2 = null;
	public UILabel suitEffect3 = null;

    public GameObject gemPanel = null;
    public UISprite gemBg1 = null;
    public UISprite gemBg2 = null;
    public UISprite gemBg3 = null;
    public UISprite gemIcon1 = null;
    public UISprite gemIcon2 = null;
    public UISprite gemIcon3 = null;
    public UILabel gemAtt1 = null;
    public UILabel gemAtt2 = null;
    public UILabel gemAtt3 = null;

    public GameObject gemBtn1 = null;
    public GameObject gemBtn2 = null;
    public GameObject gemBtn3 = null;

    public UISprite scoreDelt = null;
    public UISprite itemIcon = null;
    public UISprite itemIconBg = null;

    public sdGrid grid = null;
    public UIDraggablePanel dragPanel = null;
    public UILabel errorMsg = null;
    public GameObject equiped = null;

    GameObject comparePanel = null;
    public GameObject scoreLabel = null;
    public GameObject lockImg = null;

    public void RefreshTip()
    {
        if (!isActiveAndEnabled) return;
        ShowTip(tipType, CurrentId);
    }

    void Start()
    {
        useBtn.onClick.Add(new EventDelegate(OnUseItem));
    }

    void OnUseItem()
    {
        sdItemMsg.notifyUseItem();
    }

	public void ShowTip(TipType type , string id)
	{
        if (comparePanel != null)
        {
            GameObject.Destroy(comparePanel);
            comparePanel = null;
        }
		CurrentId = id;
        tipType = type;
		switch(type)
		{
			case TipType.Item:
			{
				if (skillPanel != null) skillPanel.SetActive(false);
				if (passivePanel != null) passivePanel.SetActive(false);
				if (itemPanel != null) itemPanel.SetActive(true);
				if (skillBg != null) skillBg.SetActive(false);
				
                sdGameItem compareItem = null;
                sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
                if (item == null)
                {
                    Hashtable info = sdConfDataMgr.Instance().GetItemById(id);
                    if (info != null)
                    {
                        compareItem = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(info["Character"].ToString()));
                    }
                }
                else
                {
                    compareItem = sdGameItemMgr.Instance.getEquipItemByPos(item.equipPos);
                }
                if (compareItem != null && compareItem != item)
                {
                    ShowCompareTip(compareItem.instanceID.ToString());
                }
                else
                {
                    if (isMove)
                    {
                        Vector3 pos = itemPanel.transform.localPosition;
                        pos.x -= 200;
                        itemPanel.transform.localPosition = pos;
                        isMove = false;
                    }
                }
                ShowItemTip(id, false);
				break;	
			}
		    case TipType.TempItem:
			{
				if (skillPanel != null) skillPanel.SetActive(false);
				if (passivePanel != null) passivePanel.SetActive(false);
				if (itemPanel != null) itemPanel.SetActive(true);
				if (skillBg != null) skillBg.SetActive(false);
				
                sdGameItem compareItem = null;
                Hashtable info = sdConfDataMgr.Instance().GetItemById(id);
                if (info != null)
                {
                    compareItem = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(info["Character"].ToString()));
                }

                if (compareItem != null)
                {
                    ShowCompareTip(compareItem.instanceID.ToString());
                }
                else
                {
                    if (isMove)
                    {
                        Vector3 pos = itemPanel.transform.localPosition;
                        pos.x -= 200;
                        itemPanel.transform.localPosition = pos;
                        isMove = false;
                    }
                }
                ShowItemTip(id, true);
				break;	
			}
			case TipType.Skill:
			{
				if (skillBg != null) skillBg.SetActive(true);
				if (skillPanel != null) skillPanel.SetActive(true);
				if (passivePanel != null) passivePanel.SetActive(false);
				if (itemPanel != null) itemPanel.SetActive(false);
				
				ShowSkillTip(id);
				break;	
			}
			case TipType.PassiveSkill:
			{
				if (skillBg != null) skillBg.SetActive(true);
				if (skillPanel != null) skillPanel.SetActive(false);
				if (passivePanel != null) passivePanel.SetActive(true);
				if (itemPanel != null) itemPanel.SetActive(false);
				
				ShowPassiveTip(id);
				break;	
			}
		}
	}

    void Update()
    {

    }

    bool isMove = false;
    void ShowCompareTip(string id)
    {
        ShowItemTip(id, true);
        if (comparePanel != null)
        {
            GameObject.Destroy(comparePanel);
        }

        comparePanel = GameObject.Instantiate(itemPanel) as GameObject;
        Transform close = comparePanel.transform.FindChild("close_tip");
        if (close != null)
        {
            close.gameObject.SetActive(false);
        }
       
        comparePanel.transform.parent = itemPanel.transform.parent;
        comparePanel.transform.localScale = itemPanel.transform.localScale;
        Vector3 pos = itemPanel.transform.localPosition;
        if (isMove)
        {
            pos.x -= 400;
        }
        else
        {
            pos.x -= 200;
        }
        comparePanel.transform.localPosition = pos;
        if (!isMove)
        {
            pos = itemPanel.transform.localPosition;
            pos.x += 200;
            itemPanel.transform.localPosition = pos;
        }

        isMove = true;
        sdUICharacterChild[] list = comparePanel.GetComponentsInChildren<sdUICharacterChild>();
        foreach (sdUICharacterChild child in list)
        {
            child.enabled = false;
        }
    }
	
	public void ShowSkillTip(string id)
	{
		if (skillPanel == null) return;
		Hashtable table = sdConfDataMgr.Instance().GetSkill(id);
		if (table == null) return;
		if (skillPanel.transform.FindChild("tipSlot") != null)
		{
			string iconname = table["icon"].ToString();
			if (sdConfDataMgr.Instance().skilliconAtlas != null)
			{
				skillPanel.transform.FindChild("tipSlot").transform.Find("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().skilliconAtlas;
			}
			skillPanel.transform.FindChild("tipSlot").transform.Find("icon").GetComponent<UISprite>().spriteName = iconname;
		}	
		
		Hashtable showInfo = new Hashtable();
		showInfo["value"] = table["strName"].ToString();
		sdUICharacter.Instance.SetProperty("Tip_Skill_Name", showInfo);
		
		showInfo["value"] = sdConfDataMgr.Instance().GetShowStr("Active");
		sdUICharacter.Instance.SetProperty("Tip_Skill_IsPassive", showInfo);
		
		string str = table["byShape"].ToString();
		SkillForm form = (SkillForm)Enum.Parse(typeof(SkillForm), str);
		showInfo["value"] = sdConfDataMgr.Instance().GetShowStr(form.ToString());
		sdUICharacter.Instance.SetProperty("Skill_Tip_Form", showInfo);
		
		str = table["byDamegePro"].ToString();
		Skill_ElementType elem = (Skill_ElementType)Enum.Parse(typeof(Skill_ElementType), str);
		showInfo["value"] = sdConfDataMgr.Instance().GetShowStr(elem.ToString());
		sdUICharacter.Instance.SetProperty("Skill_Tip_Att", showInfo);
		
		showInfo["value"] = table["dwCostSP"].ToString();
		sdUICharacter.Instance.SetProperty("Skill_Tip_Consume", showInfo);
		
		showInfo["value"] = sdConfDataMgr.Instance().GetShowStr("AllWeapon");
		sdUICharacter.Instance.SetProperty("Skill_Tip_Request", showInfo);
		
		showInfo["value"] = string.Format("{0}{1}",int.Parse(table["dwCooldown"].ToString())/1000, sdConfDataMgr.Instance().GetShowStr("Second"));
		sdUICharacter.Instance.SetProperty("Skill_Tip_CD", showInfo);

        string des = table["Description"].ToString();
        string[] list = des.Split('[', ']');
        des = "";
        int count = list.Length;
        for (int i = 0; i < count; ++i)
        {
            if (i % 2 == 1)
            {
                des += string.Format("[{0}]{1}", sdConfDataMgr.Instance().GetColorHex(Color.yellow), list[i]);
            }
            else
            {
                des += string.Format("[{0}]{1}", sdConfDataMgr.Instance().GetColorHex(Color.white), list[i]);
            }
        }
        showInfo["value"] = des;
		sdUICharacter.Instance.SetProperty("Tip_SKill_Des", showInfo);

		sdGameSkill tempSkill = sdGameSkillMgr.Instance.GetSkill(int.Parse(id));
		Transform skillTipImg = skillPanel.transform.FindChild("Img_Learn");
		if (tempSkill != null)
		{
			if (skillTipImg != null)
			{
				skillTipImg.GetComponent<UISprite>().spriteName = "SkillSystem_Img_Learned";
			}
            if (skillPanel.transform.FindChild("lock") != null)
            {
                skillPanel.transform.FindChild("lock").GetComponent<UILabel>().text = "";
            }
		}
		else
		{
			if (skillTipImg != null)
			{
				skillTipImg.GetComponent<UISprite>().spriteName = "SkillSystem_Img_Locked";
			}

			int needLevel = int.Parse(table["nLearnLevel"].ToString());
			string tipshow = string.Format("{0}{1}{2}", needLevel.ToString(), 
				sdConfDataMgr.Instance().GetShowStr("SimpleLevel"), 
				sdConfDataMgr.Instance().GetShowStr("UnLock"));
			if (skillPanel.transform.FindChild("lock") != null)
			{
				skillPanel.transform.FindChild("lock").GetComponent<UILabel>().text = tipshow;
			}
		}
		
	}
	
	public void TipNextSkill()
	{
		Hashtable table = sdConfDataMgr.Instance().GetSkill(CurrentId);
		if (table == null) return;
		string nextLevel = table["NextLevel"].ToString();
		if (sdGameSkillMgr.Instance.GetSkill(int.Parse(nextLevel)) == null)
		{
			ShowTip(TipType.PassiveSkill, CurrentId);
		}
		else
		{
			ShowTip(TipType.PassiveSkill, nextLevel);
		}
	}

    void SetItemAtlas1(ResLoadParams param, UnityEngine.Object obj)
    {
        gemIcon1.atlas = obj as UIAtlas;
    }

    void SetItemAtlas2(ResLoadParams param, UnityEngine.Object obj)
    {
        gemIcon2.atlas = obj as UIAtlas;
    }

    void SetItemAtlas3(ResLoadParams param, UnityEngine.Object obj)
    {
        gemIcon3.atlas = obj as UIAtlas;
    }
	
	void ShowItemTip(string id, bool isTemp)
	{
		if (itemPanel == null) return;
		Hashtable table;
		sdGameItem gameItem = null;
		if (isTemp)
		{
            gameItem = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
            if (gameItem == null)
            {
                table = sdConfDataMgr.Instance().GetItemById(id);
            }
            else
            {
                table = sdConfDataMgr.Instance().GetItemById(gameItem.templateID.ToString());
            }
		}
		else
		{
			gameItem = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
			if (gameItem == null) return;
			table = sdConfDataMgr.Instance().GetItemById(gameItem.templateID.ToString());
		}

		if (table == null) return;

        equiped.SetActive(false);
        lockImg.SetActive(false);
        if (labelFuwen != null) labelFuwen.SetActive(false);

        if (itemIcon != null)
		{
            itemIcon.atlas = sdConfDataMgr.Instance().GetItemAtlas(table["IconID"].ToString());
            itemIcon.spriteName = table["IconPath"].ToString();
		}

		Hashtable showInfo = new Hashtable();
		string strName = table["ShowName"].ToString();
		if (gameItem != null)
		{
			if (gameItem.upLevel > 0)
			{
				strName += string.Format("+{0}", gameItem.upLevel);
			}
		}
        int quility = int.Parse(table["Quility"].ToString());
        if (itemIconBg != null)
        {
            itemIconBg.spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(quility);
        }
		
		string job = table["NeedJob"].ToString();
		string jobname = sdConfDataMgr.Instance().GetJobNameByIndex(job);
		jobname = string.Format("{0} {1}",sdConfDataMgr.Instance().GetShowStr(StringTable.Job.ToString()), jobname);
		showInfo["value"] = jobname;
		sdUICharacter.Instance.SetProperty("Tip_Item_Job", showInfo);

        bool isJiesuan = sdUICharacter.Instance.GetJiesuanWnd() == null ? false : true;
        bool isGem = false;
        if (table["Class"].ToString() == "51" && table["SubClass"].ToString() == "1")
        {
            isGem = true;
        }

        string level = "";
        if (isGem)
        {
            level = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("GemLevel"), table["Level"].ToString());
        }
        else
        {
            level = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("NeedLevel"), table["NeedLevel"].ToString());
        }
		
		showInfo["value"] = level;
		sdUICharacter.Instance.SetProperty("Tip_Item_Level", showInfo);
			
		string price = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr(StringTable.Price.ToString()), table["Value"].ToString());
		showInfo["value"] = price;
		sdUICharacter.Instance.SetProperty("Tip_Item_Price", showInfo);
		
		showInfo["value"] = sdConfDataMgr.Instance().GetItemClassName(table["Class"].ToString(), table["SubClass"].ToString());
		sdUICharacter.Instance.SetProperty("Tip_Item_Part", showInfo);

        showInfo["value"] = sdConfDataMgr.Instance().GetItemQuilityName(quility);
		sdUICharacter.Instance.SetProperty("Tip_Item_Quality", showInfo);
		
        if (labelDes != null)
        {
            if (table["Description"].ToString() == "")
            {
                labelDes.SetActive(false);
            }
            else
            {
                labelDes.SetActive(true);
            }
        }

		showInfo["value"] = table["Description"].ToString();
		sdUICharacter.Instance.SetProperty("Tip_Item_Des", showInfo);
		
		if (itemQuality != null)
		{
			itemQuality.GetComponent<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(table["Quility"].ToString()));
		}
		
		string mainAtt = table["StringExtra3"].ToString();
        string mainAttStr = sdConfDataMgr.Instance().GetShowStr(mainAtt);
        string txtColor = "";

		if (mainAtt == "" || mainAtt == "0")
		{
			showInfo["value"] = "";
			sdUICharacter.Instance.SetProperty("Tip_Item_MainAttValue", showInfo);
			sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);
			if (itemArrow != null)
			{
				itemArrow.GetComponent<UISprite>().spriteName = "";	
			}
			
			if (equipBtn != null) equipBtn.SetActive(false);
			if (unEquipBtn != null) unEquipBtn.SetActive(false);

            if (scoreDelt != null)
            {
                scoreDelt.GetComponent<UISprite>().spriteName = "";
            }
            sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
            if(scoreLabel != null)scoreLabel.SetActive(false);
			if (replaceBtn != null) replaceBtn.SetActive(false);
            sdUICharacter.Instance.SetProperty("Tip_Item_Score", showInfo);
		}
		else
		{
            int curScore = 0;
            if (gameItem != null)
            {
                curScore = sdConfDataMgr.Instance().GetItemScore(gameItem.instanceID);
            }
            else
            {
                curScore = sdConfDataMgr.Instance().GetItemScore(table["ID"].ToString(), 0);
            }
            if (curScore == 0)
            {
                scoreLabel.SetActive(false);
            }
            else
            {
                scoreLabel.SetActive(true);
            }
            showInfo["value"] = string.Format("{0}", curScore);
            sdUICharacter.Instance.SetProperty("Tip_Item_Score", showInfo);
            string value = table[mainAtt].ToString();
			
            if (gameItem != null && gameItem.upLevel > 0)
            {
                Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(gameItem.templateID.ToString(), gameItem.upLevel);
				if (curInfo != null)
				{
                    float rate = float.Parse(curInfo["UpMainTypeCoe"].ToString());
                    int curValue = (int)(float.Parse(value) * (1f + (rate / 10000f)));
                    value = curValue.ToString();
                }
            }
            showInfo["value"] = string.Format("{0}+{1}", mainAttStr, value);
			sdUICharacter.Instance.SetProperty("Tip_Item_MainAttValue", showInfo);

			sdGameItem item = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(table["Character"].ToString()));
			if (item == null)
			{
                showInfo["value"] = string.Format("+{0}", value);
				sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);
				if (itemArrow != null)
				{
					itemArrow.GetComponent<UISprite>().spriteName = "up1";	
					itemArrow.GetComponentInChildren<UILabel>().color = Color.green;
				}
				if (equipBtn != null) equipBtn.SetActive(true);
				if (unEquipBtn != null) unEquipBtn.SetActive(false);
                if (scoreDelt != null)
                {
                    scoreDelt.GetComponent<UISprite>().spriteName = "up1";
                }
                txtColor = sdConfDataMgr.Instance().GetColorHex(Color.green);
                showInfo["value"] = string.Format("[{0}]{1}", txtColor,curScore.ToString());
               
                sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
				if (replaceBtn != null) replaceBtn.SetActive(false);
			}
			else if (item == gameItem)
			{
                equiped.SetActive(true);
                strName = string.Format("{0}", strName);
				if (itemArrow != null)
				{
					itemArrow.GetComponent<UISprite>().spriteName = "";	
				}
				showInfo["value"] = "";
				sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);
				if (equipBtn != null) equipBtn.SetActive(false);
                if (unEquipBtn != null)
                {
                    if (isJiesuan)
                    {
                        unEquipBtn.SetActive(false);
                    }
                    else
                    {
                        unEquipBtn.SetActive(true);
                    }                  
                }
                if (scoreDelt != null)
                {
                    scoreDelt.GetComponent<UISprite>().spriteName = "";
                }
                sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
				if (replaceBtn != null) replaceBtn.SetActive(true);
			}
			else
			{
				Hashtable compare = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(item.templateID.ToString(), item.upLevel);
                int compareValue = int.Parse(compare[mainAtt].ToString());
                if (curInfo != null)
                {
                    float rate = float.Parse(curInfo["UpMainTypeCoe"].ToString());
                    compareValue = (int)(float.Parse(compare[mainAtt].ToString()) * (1f + (rate / 10000f)));
                }

                int changeValue = int.Parse(value) - compareValue;
				
				if (changeValue == 0)
				{
					showInfo["value"] = "";
					sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);	
					if (itemArrow != null)
					{
						itemArrow.GetComponent<UISprite>().spriteName = "";	
					}
				}
				else if (changeValue > 0)
				{
					showInfo["value"] = Math.Abs(changeValue).ToString();
					sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);	
					if (itemArrow != null)
					{
						itemArrow.GetComponent<UISprite>().spriteName = "up1";	
						itemArrow.GetComponentInChildren<UILabel>().color = Color.green;
					}
				}
				else if (changeValue < 0)
				{
					showInfo["value"] = Math.Abs(changeValue).ToString();
					sdUICharacter.Instance.SetProperty("Tip_Item_MainAttDelt", showInfo);	
					if (itemArrow != null)
					{
						itemArrow.GetComponent<UISprite>().spriteName = "down1";	
						itemArrow.GetComponentInChildren<UILabel>().color = Color.red;
					}
				}
				
				if (equipBtn != null) equipBtn.SetActive(true);
				if (unEquipBtn != null) unEquipBtn.SetActive(false);

                int compareScore = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                changeValue = curScore - compareScore;

                if (changeValue == 0)
                {
                    showInfo["value"] = "";
                    sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
                    if (scoreDelt != null)
                    {
                        scoreDelt.GetComponent<UISprite>().spriteName = "";
                    }
                }
                else if (changeValue > 0)
                {
                    txtColor = sdConfDataMgr.Instance().GetColorHex(Color.green);
                    showInfo["value"] = string.Format("[{0}]{1}", txtColor, Math.Abs(changeValue).ToString());
                    sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
                    if (scoreDelt != null)
                    {
                        scoreDelt.GetComponent<UISprite>().spriteName = "up1";
                    }
                }
                else if (changeValue < 0)
                {
                    txtColor = sdConfDataMgr.Instance().GetColorHex(Color.red);
                    showInfo["value"] = string.Format("[{0}]{1}", txtColor, Math.Abs(changeValue).ToString());
                    sdUICharacter.Instance.SetProperty("Tip_Item_deltScore", showInfo);
                    if (scoreDelt != null)
                    {
                        scoreDelt.GetComponent<UISprite>().spriteName = "down1";
                    }
                }
				if (replaceBtn != null) replaceBtn.SetActive(false);
			}
		}

        txtColor = sdConfDataMgr.Instance().GetColorHex(sdConfDataMgr.Instance().GetItemQuilityColor(quility));
        showInfo["value"] = string.Format("[{0}]{1}", txtColor, strName);
        sdUICharacter.Instance.SetProperty("Tip_Item_Name", showInfo);

		Hashtable info;
		if (isTemp)
		{
			info = sdConfDataMgr.Instance().GetProperty(id);
		}
		else
		{
			info = sdConfDataMgr.Instance().GetProperty(gameItem.templateID.ToString());	
		}
		
		string att = "";
		int num = 0;
		foreach(DictionaryEntry tempItem in info)
		{
            string value = tempItem.Value.ToString();
			if ((string)tempItem.Key == sdConfDataMgr.Instance().GetShowStr(mainAtt))
			{
                continue;
//                 if (gameItem != null && gameItem.upLevel > 0)
//                 {
//                     Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(gameItem.templateID.ToString(), gameItem.upLevel);
//                     if (curInfo != null)
//                     {
//                         
//                         float rate = float.Parse(curInfo["UpMainTypeCoe"].ToString());
//                         int curValue = (int)(float.Parse(value) * (1f + (rate / 10000f)));
//                         value = curValue.ToString();
//                     }
//                 }
//                 string main = string.Format("[{0}]", sdConfDataMgr.Instance().GetColorHex(Color.white));
//                 main += tempItem.Key;
//                 main += "+";
//                 main += value;
//                 main += "\n";
//                 att = main + at			
            }
            else
            {
                att += string.Format("[{0}]", sdConfDataMgr.Instance().GetColorHex(Color.green));
                att += tempItem.Key;
                att += "+";
                att += value;
                att += "\n";
            }

			num++;
			if (num == 6) break;
		}

        if (labelAtt != null)
        {
            if (num == 0)
            {
                labelAtt.SetActive(false);
            }
            else
            {
                labelAtt.SetActive(true);
            }
        }

		showInfo["value"] = att;
		sdUICharacter.Instance.SetProperty("Tip_Item_Att", showInfo);	

        if (gameItem != null)
        {
            if (gameItem.gemNum > 0)
            {
                string gembg = sdConfDataMgr.Instance().GetGemBg(int.Parse(table["IntExtra3"].ToString()));
                gemPanel.SetActive(true);
                for (int i = 0; i < gameItem.gemNum; ++i)
                {
                    int gemId = gameItem.gemList[i];
                    if (i == 0)
                    {
                        gemBtn1.SetActive(true);
                        gemBg1.spriteName = gembg;
                        if (gemId == 0)
                        {
                            gemIcon1.spriteName = "";
                            gemAtt1.text = "";
                        }
                        else
                        {
                            Hashtable gemInfo = sdConfDataMgr.Instance().GetItemById(gemId.ToString());
                            sdConfDataMgr.Instance().LoadItemAtlas(gemInfo["IconID"].ToString(), SetItemAtlas1);
                            gemIcon1.spriteName = gemInfo["IconPath"].ToString();
                            string gemAtt = "";
                            Hashtable attInfo = sdConfDataMgr.Instance().GetProperty(gemId.ToString());
                            foreach (DictionaryEntry tempItem in attInfo)
                            {
                                gemAtt += tempItem.Key;
                                gemAtt += "+";
                                gemAtt += tempItem.Value;
                                break;
                            }
                            gemAtt1.text = gemAtt;
                        }
                    }
                    else if (i == 1)
                    {
                        gemBtn2.SetActive(true);
                        gemBg2.spriteName = gembg;
                        if (gemId == 0)
                        {
                            gemIcon2.spriteName = "";
                            gemAtt2.text = "";
                        }
                        else
                        {
                            Hashtable gemInfo = sdConfDataMgr.Instance().GetItemById(gemId.ToString());
                            sdConfDataMgr.Instance().LoadItemAtlas(gemInfo["IconID"].ToString(), SetItemAtlas2);
                            gemIcon2.spriteName = gemInfo["IconPath"].ToString();
                            string gemAtt = "";
                            Hashtable attInfo = sdConfDataMgr.Instance().GetProperty(gemId.ToString());
                            foreach (DictionaryEntry tempItem in attInfo)
                            {
                                gemAtt += tempItem.Key;
                                gemAtt += "+";
                                gemAtt += tempItem.Value;
                                break;
                            }
                            gemAtt2.text = gemAtt;
                        }
                    }
                    else if (i == 2)
                    {
                        gemBtn3.SetActive(true);
                        gemBg3.spriteName = gembg;
                        if (gemId == 0)
                        {
                            gemIcon3.spriteName = "";
                            gemAtt3.text = "";
                        }
                        else
                        {
                            Hashtable gemInfo = sdConfDataMgr.Instance().GetItemById(gemId.ToString());
                            sdConfDataMgr.Instance().LoadItemAtlas(gemInfo["IconID"].ToString(), SetItemAtlas3);
                            gemIcon3.spriteName = gemInfo["IconPath"].ToString();
                            string gemAtt = "";
                            Hashtable attInfo = sdConfDataMgr.Instance().GetProperty(gemId.ToString());
                            foreach (DictionaryEntry tempItem in attInfo)
                            {
                                gemAtt += tempItem.Key;
                                gemAtt += "+";
                                gemAtt += tempItem.Value;
                                break;
                            }
                            gemAtt3.text = gemAtt;
                        }
                    }
                }

                for (int i = gameItem.gemNum; i < 3; ++i)
                {
                    if (i == 0)
                    {
                        gemBtn1.SetActive(false);
                        gemAtt1.text = "";
                        gemIcon1.spriteName = "";
                    }
                    else if (i == 1)
                    {
                        gemBtn2.SetActive(false);
                        gemAtt2.text = "";
                        gemIcon2.spriteName = "";
                    }
                    else if (i == 2)
                    {
                        gemBtn3.SetActive(false);
                        gemAtt3.text = "";
                        gemIcon3.spriteName = "";
                    }
                }

            }
            else
            {
                gemPanel.SetActive(false);
            }
        }
        else
        {
            gemPanel.SetActive(false);
        }

		if (table["SuitID"].ToString() != "0")
		{
            suitPanel1.SetActive(true);
            suitPanel2.SetActive(true);

			suitEffect.gameObject.SetActive(true);
			Hashtable suitInfo = sdConfDataMgr.Instance().GetSuit(table["SuitID"].ToString());
			if (suitInfo != null)
			{
				suitName.text = string.Format("[{0}]", suitInfo["Name"].ToString());
				Hashtable suitItem = sdConfDataMgr.Instance().GetItemById(suitInfo["Item1.ItemID"].ToString());
				if (suitItem != null)
				{
					sdGameItem exist = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(suitItem["Character"].ToString()));
                    if (exist == null || exist.templateID.ToString() != suitInfo["Item1.ItemID"].ToString())
					{
						suitName1.color = Color.gray;
					}
					else
					{
						suitName1.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(suitItem["Quility"].ToString()));
					}
					suitName1.text = suitItem["ShowName"].ToString();
				}

				suitItem = sdConfDataMgr.Instance().GetItemById(suitInfo["Item2.ItemID"].ToString());
				if (suitItem != null)
				{
					sdGameItem exist = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(suitItem["Character"].ToString()));
                    if (exist == null || exist.templateID.ToString() != suitInfo["Item2.ItemID"].ToString())
					{
						suitName2.color = Color.gray;
					}
					else
					{
						suitName2.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(suitItem["Quility"].ToString()));
					}
					suitName2.text = suitItem["ShowName"].ToString();
				}

				suitItem = sdConfDataMgr.Instance().GetItemById(suitInfo["Item3.ItemID"].ToString());
				if (suitItem != null)
				{
					sdGameItem exist = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(suitItem["Character"].ToString()));
                    if (exist == null || exist.templateID.ToString() != suitInfo["Item3.ItemID"].ToString())
					{
						suitName3.color = Color.gray;
					}
					else
					{
						suitName3.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(suitItem["Quility"].ToString()));
					}
					suitName3.text = suitItem["ShowName"].ToString();
				}

				int i = 0;
				for (int j = 0; j < 5; ++j)
				{
					string key = string.Format("SuitSap{0}.SapID", j+1);
					if (!suitInfo.ContainsKey(key))
					{
						continue;
					}
					
					string sapId = suitInfo[key].ToString();
					Hashtable sapInfo = sdConfDataMgr.Instance().GetSap(sapId);
					if (sapInfo == null)
					{
						continue;
					}
					
					for (int k = 0; k < 6; ++k)
					{
						key = string.Format("Skill{0}.SkillID", k+1);
						if (!sapInfo.ContainsKey(key)) continue;
						if (sapInfo[key].ToString() == "0") continue;
						if (i == 0)
						{
							key = string.Format("Skill{0}.Desc", k+1);
							suitEffect1.text = string.Format("[{0}]{1}",j+1,sapInfo[key].ToString());
							if (sdGameItemMgr.Instance.suitInfo.ContainsKey(int.Parse(suitInfo["SID"].ToString())))
							{
								if (j+1 <= (int)(sdGameItemMgr.Instance.suitInfo[int.Parse(suitInfo["SID"].ToString())]))
								{
									suitEffect1.color = Color.green;
								}
								else
								{
									suitEffect1.color = Color.gray;
								}
							}
							else
							{
								suitEffect1.color = Color.gray;
							}
						}
						else if (i == 1)
						{
							key = string.Format("Skill{0}.Desc", k+1);
							suitEffect2.text = string.Format("[{0}]{1}",j+1,sapInfo[key].ToString());
							if (sdGameItemMgr.Instance.suitInfo.ContainsKey(int.Parse(suitInfo["SID"].ToString())))
							{
								if (j+1 <= (int)(sdGameItemMgr.Instance.suitInfo[int.Parse(suitInfo["SID"].ToString())]))
								{
									suitEffect2.color = Color.green;
								}
								else
								{
									suitEffect2.color = Color.gray;
								}
							}
							else
							{
								suitEffect2.color = Color.gray;
							}
						}
						else if (i == 2)
						{
							key = string.Format("Skill{0}.Desc", k+1);
							suitEffect3.text = string.Format("[{0}]{1}",j+1,sapInfo[key].ToString());
							if (sdGameItemMgr.Instance.suitInfo.ContainsKey(int.Parse(suitInfo["SID"].ToString())))
							{
								if (j+1 <= (int)(sdGameItemMgr.Instance.suitInfo[int.Parse(suitInfo["SID"].ToString())]))
								{
									suitEffect3.color = Color.green;
								}
								else
								{
									suitEffect3.color = Color.gray;
								}
							}
							else
							{
								suitEffect3.color = Color.gray;
							}
						}
						++i;
					}
				}
			}
		}
		else
		{
            suitPanel1.SetActive(false);
            suitPanel2.SetActive(false);
			suitName.text = "";
			suitName1.text = "";
			suitName2.text = "";
			suitName3.text = "";
			suitEffect.gameObject.SetActive(false);
			suitEffect1.text = "";
			suitEffect2.text = "";
			suitEffect3.text = "";
		}

//         if (gameItem == null)
//         {
//             int curScore = sdConfDataMgr.Instance().GetItemScore(table["ID"].ToString(), 0);
//             showInfo["value"] = string.Format("{0}{1}", sdConfDataMgr.Instance().GetShowStr("Score"), curScore);
//             sdUICharacter.Instance.SetProperty("Tip_Item_Score", showInfo);
//         }
//         else
//         {
//             if (gameItem.equipPos < 0)
//             {
//                 showInfo["value"] = "";
//                 sdUICharacter.Instance.SetProperty("Tip_Item_Score", showInfo);
//             }
//             else
//             {
//                 int curScore = sdConfDataMgr.Instance().GetItemScore(gameItem.instanceID);
//                 showInfo["value"] = string.Format("{0}{1}", sdConfDataMgr.Instance().GetShowStr("Score"), curScore);
//                 sdUICharacter.Instance.SetProperty("Tip_Item_Score", showInfo);
//             }
//         }

        bool canUse = int.Parse(table["CanUse"].ToString()) == 1 ? true : false;

        if (canUse)
        {
            if (equipBtn != null) equipBtn.SetActive(false);
            if (unEquipBtn != null) unEquipBtn.SetActive(false);
            if (isTemp)
            {
                if (sellBtn != null) sellBtn.SetActive(false);
                if (lockBtn != null) lockBtn.SetActive(false);
            }
            else
            {
                if (lockBtn != null) lockBtn.SetActive(true);
            }
            if (itemUpBtn != null) itemUpBtn.SetActive(false);
            if (replaceBtn != null) replaceBtn.SetActive(false);
            if (useBtn != null) useBtn.gameObject.SetActive(true);
        }
		else if (isTemp || isGem)
		{
			if (equipBtn != null) equipBtn.SetActive(false);
			if (unEquipBtn != null) unEquipBtn.SetActive(false);
            if (isGem && !isTemp)
            {
                if (sellBtn != null) sellBtn.SetActive(true);
            }
            else
            {
                if (sellBtn != null) sellBtn.SetActive(false);
            }
            if (itemUpBtn != null) itemUpBtn.SetActive(false);
            if (replaceBtn != null) replaceBtn.SetActive(false);
            if (lockBtn != null) lockBtn.SetActive(false);
            if (useBtn != null) useBtn.gameObject.SetActive(false);
		}
        else if (isJiesuan)
        {
            if (equipBtn != null) equipBtn.SetActive(false);
            if (itemUpBtn != null) itemUpBtn.SetActive(false);
            if (unEquipBtn != null) unEquipBtn.SetActive(false);
            if (sellBtn != null) sellBtn.SetActive(false);
            if (itemUpBtn != null) itemUpBtn.SetActive(false);
            if (lockBtn != null) lockBtn.SetActive(false);
            if (useBtn != null) useBtn.gameObject.SetActive(false);
        }
        else
        {
            if (useBtn != null) useBtn.gameObject.SetActive(false);
            if (!sdUICharacter.Instance.tipCanEquip)
            {
                if (equipBtn != null) equipBtn.SetActive(false);
                if (unEquipBtn != null) unEquipBtn.SetActive(false);
                if (itemUpBtn != null) itemUpBtn.SetActive(false);
            }

            if (sellBtn != null) sellBtn.SetActive(true);
            if (itemUpBtn != null)
            {
                if (gameItem.itemClass != 0 && gameItem.itemClass != 1 && gameItem.itemClass != 2)
                {
                    itemUpBtn.SetActive(false);
                }
                else
                {
                    itemUpBtn.SetActive(true);
                }
                
            }
            if (gameItem == null)
            {
                lockBtn.SetActive(false);
            }
            else
            {
                lockBtn.SetActive(true);
                if (gameItem.isLock)
                {
                    lockBtn.transform.FindChild("word").GetComponent<UISprite>().spriteName = "BTjs";
                    lockImg.SetActive(true);
                }
                else
                {
                    lockBtn.transform.FindChild("word").GetComponent<UISprite>().spriteName = "BTjas";
                }
            }
        }

        if (grid != null) grid.Reposstion();
        if (dragPanel != null) dragPanel.ResetPosition();
	}
	
	void ShowPassiveTip(string id)
	{
		if (passivePanel == null) return;
		Hashtable table = sdConfDataMgr.Instance().GetSkill(id);
		if (table == null) return;
		if (passivePanel.transform.FindChild("curSkill").FindChild("tipSlot") != null)
		{
			string iconname = table["icon"].ToString();
			if (sdConfDataMgr.Instance().skilliconAtlas != null)
			{
				passivePanel.transform.FindChild("curSkill").FindChild("tipSlot").transform.Find("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().skilliconAtlas;
			}
			passivePanel.transform.FindChild("curSkill").FindChild("tipSlot").transform.Find("icon").GetComponent<UISprite>().spriteName = iconname;
		}	
		
		Hashtable showInfo = new Hashtable();
		showInfo["value"] = table["strName"].ToString();
		sdUICharacter.Instance.SetProperty("Tip_Skill_Name", showInfo);
		
		showInfo["value"] = sdConfDataMgr.Instance().GetShowStr("Passive");
		sdUICharacter.Instance.SetProperty("Tip_Skill_IsPassive", showInfo);

        string des = table["Description"].ToString();
        string[] list = des.Split('[', ']');
        des = "";
        int count = list.Length;
        for (int i = 0; i < count; ++i)
        {
            if (i % 2 == 1)
            {
                des += string.Format("[{0}]{1}", sdConfDataMgr.Instance().GetColorHex(Color.yellow), list[i]);
            }
            else
            {
                des += string.Format("[{0}]{1}", sdConfDataMgr.Instance().GetColorHex(Color.white), list[i]);
            }
        }
        showInfo["value"] = des;
		sdUICharacter.Instance.SetProperty("Tip_Skill_Des", showInfo);
		
		string level = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr("CurLevel"), table["byLevel"].ToString());
		showInfo["value"] = level;
		sdUICharacter.Instance.SetProperty("Tip_Skill_Level", showInfo);
		
		string cost = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr("LearnCost"), table["dwCostSkillPoint"].ToString());
		showInfo["value"] = cost;
		sdUICharacter.Instance.SetProperty("Tip_Skill_Spend", showInfo);
		
		sdGameSkill tempSkill = sdGameSkillMgr.Instance.GetSkill(int.Parse(id));
		if (tempSkill == null)
		{
			passivePanel.transform.FindChild("Img_passiveLearn").GetComponent<UISprite>().spriteName = "";
			if (learnBtn != null) 
			{
				string parentId = table["parentid"].ToString();
				int compareLevel = 1;
				if (sdGameLevel.instance.mainChar != null)
				{
					compareLevel = sdGameLevel.instance.mainChar["Level"];
				}
				int needLevel = int.Parse(table["nLearnLevel"].ToString());
				int needPoint = int.Parse(table["dwTotalSkillTreePoint"].ToString());
				int requestPoint = int.Parse(table["dwCostSkillPoint"].ToString());
                bool hasNeedSkill = false;
                if (table.ContainsKey("NeedSkill"))
                {
                    int needSkill = int.Parse(table["NeedSkill"].ToString());
                    Hashtable needInfo = sdConfDataMgr.Instance().GetSkill(needSkill.ToString());
                    if (needInfo != null)
                    {
                        if (int.Parse(needInfo["byIsPassive"].ToString()) == 0)
                        {
                            hasNeedSkill = true;
                        }
                        else
                        {
                            sdGameSkill skillneed = sdGameSkillMgr.Instance.GetSkillByClassId(needInfo["dwClassID"].ToString());
                            if (skillneed != null && skillneed.level >= int.Parse(needInfo["byLevel"].ToString()))
                            {
                                hasNeedSkill = true;
                            }
                        }

                    }
                    else
                    {
                        hasNeedSkill = true;
                    }
                }
                learnBtn.SetActive(false);
                errorMsg.gameObject.SetActive(true);
                if (compareLevel < needLevel)
                {
                    errorMsg.text = sdConfDataMgr.Instance().GetShowStr("LevelLow");
                }
                else if (sdGameSkillMgr.Instance.GetSkill(int.Parse(parentId)) == null)
                {
                    Hashtable parentInfo = sdConfDataMgr.Instance().GetSkill(parentId);
                    errorMsg.text = string.Format(sdConfDataMgr.Instance().GetShowStr("NeedParentSkill"),parentInfo["strName"].ToString());
                }
                else if (!hasNeedSkill)
                {
                    errorMsg.text = sdConfDataMgr.Instance().GetShowStr("NoPreSkill");
                }
                else
                {
                    learnBtn.SetActive(true);
                    learnBtn.GetComponentInChildren<UILabel>().text = sdConfDataMgr.Instance().GetShowStr("Learn");
                    errorMsg.gameObject.SetActive(false);
                }
			}
			
			if (nextSkill != null) nextSkill.SetActive(false);
			return;
		}
		
		string nextLevel = table["NextLevel"].ToString();
		if (nextLevel == "0")
		{
			passivePanel.transform.FindChild("Img_passiveLearn").GetComponent<UISprite>().spriteName = "max";
			if (learnBtn != null) learnBtn.SetActive(false);
			if (nextSkill != null) nextSkill.SetActive(false);
            if (errorMsg != null)
            {
                errorMsg.gameObject.SetActive(true);
                errorMsg.text = sdConfDataMgr.Instance().GetShowStr("SkillLvMax");
            }

		}
		else
		{
			passivePanel.transform.FindChild("Img_passiveLearn").GetComponent<UISprite>().spriteName = "";
			if (nextSkill != null) nextSkill.SetActive(true);
			
			Hashtable nextInfo = sdConfDataMgr.Instance().GetSkill(nextLevel);

            if (learnBtn != null)
            {
                string parentId = nextInfo["parentid"].ToString();
                int compareLevel = 1;
                if (sdGameLevel.instance.mainChar != null)
                {
                    compareLevel = sdGameLevel.instance.mainChar["Level"];
                }
                int needLevel = int.Parse(nextInfo["nLearnLevel"].ToString());
                int needPoint = int.Parse(nextInfo["dwTotalSkillTreePoint"].ToString());
                int requestPoint = int.Parse(nextInfo["dwCostSkillPoint"].ToString());
                bool hasNeedSkill = false;
                if (table.ContainsKey("NeedSkill"))
                {
                    int needSkill = int.Parse(table["NeedSkill"].ToString());
                    Hashtable needInfo = sdConfDataMgr.Instance().GetSkill(needSkill.ToString());
                    if (needInfo != null)
                    {
                        if (int.Parse(needInfo["byIsPassive"].ToString()) == 0)
                        {
                            hasNeedSkill = true;
                        }
                        else
                        {
                            sdGameSkill skillneed = sdGameSkillMgr.Instance.GetSkillByClassId(needInfo["dwClassID"].ToString());
                            if (skillneed != null && skillneed.level >= int.Parse(needInfo["byLevel"].ToString()))
                            {
                                hasNeedSkill = true;
                            }
                        }
                    }
                    else
                    {
                        hasNeedSkill = true;
                    }
                }
                learnBtn.SetActive(false);
                errorMsg.gameObject.SetActive(true);
                if (compareLevel < needLevel)
                {
                    errorMsg.text = sdConfDataMgr.Instance().GetShowStr("LevelLow");
                }
                else if (sdGameSkillMgr.Instance.GetSkill(int.Parse(parentId)) == null)
                {
                    Hashtable parentInfo = sdConfDataMgr.Instance().GetSkill(parentId);
                    errorMsg.text = string.Format(sdConfDataMgr.Instance().GetShowStr("NeedParentSkill"), parentInfo["strName"].ToString());
                }
                else if (!hasNeedSkill)
                {
                    errorMsg.text = sdConfDataMgr.Instance().GetShowStr("NoPreSkill");
                }
                else
                {
                    learnBtn.SetActive(true);
                    learnBtn.GetComponentInChildren<UILabel>().text = sdConfDataMgr.Instance().GetShowStr("LvUp");
                    errorMsg.gameObject.SetActive(false);
                }
            }

			
			showInfo["value"] = nextInfo["Description"].ToString();
			sdUICharacter.Instance.SetProperty("Tip_NextSkill_Des", showInfo);
			
			level = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr("NextLevel"), nextInfo["byLevel"].ToString());
			showInfo["value"] = level;
			sdUICharacter.Instance.SetProperty("Tip_NextSkill_Level", showInfo);
			sdUICharacter.Instance.lastTipId = nextLevel;
		}
	}
}