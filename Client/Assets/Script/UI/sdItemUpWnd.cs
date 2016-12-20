using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdItemUpWnd : MonoBehaviour
{
	public GameObject itemUpPanel = null;
	public GameObject gemSetPanel = null;
	public GameObject gemSynPanel = null;
	public GameObject gemReplacePanel = null;
    public GameObject itemMakePanel = null;

	public sdRadioButton itemUpbtn = null;
	public sdRadioButton gemSetbtn = null;
	public sdRadioButton gemSynbtn = null;
	public sdRadioButton gemReplacebtn = null;
    public sdRadioButton itemMakebtn = null;

	public UILabel curItemupLv = null;
	public UILabel nextItemupLv = null;
    public UILabel curItemupAttName = null;
	public UILabel curItemupAttValue = null;
	public UILabel nextItemupAtt = null;
	public UILabel maxItemUpLv = null;
    public UILabel maxItemUpLvChange = null;
	public UISlider expBar = null;

	public UILabel itemTitle = null;

	public UILabel	replaceName1 = null;
	public UILabel	replaceName2 = null;
    public UILabel  mergeName1 = null;
    public UILabel  mergeName2 = null;
    public UILabel  mergeName3 = null;
	public UILabel	lbl_money = null;

    public UILabel setName1 = null;
    public UILabel setName2 = null;
    public UILabel setName3 = null;
    public GameObject line1 = null;
    public GameObject line2 = null;
    public GameObject line3 = null;

    public GameObject gemSetEffect = null;
    public GameObject gemSet1 = null;
    public GameObject gemSet2 = null;
    public GameObject gemSet3 = null;

    public GameObject gemMergeEffect = null;
    public GameObject gemReplaceEffect = null;

    public GameObject moneyPanel = null;

    public UILabel gemMergeMoney1 = null;
    public UILabel gemMergeMoney2 = null;

    public GameObject showLabel = null;
    public GameObject desLabel = null;

	public void ShowItemUpPanel()
	{
		if (itemUpPanel != null) itemUpPanel.SetActive(true); 
		if (gemSetPanel != null) gemSetPanel.SetActive(false); 
		if (gemSynPanel != null) gemSynPanel.SetActive(false); 
		if (gemReplacePanel != null) gemReplacePanel.SetActive(false);
        if (itemMakePanel != null) itemMakePanel.SetActive(false);
        if (moneyPanel != null) moneyPanel.SetActive(true);
        RefreshItemUpPanel();
	}

	void ShowGemSetPanel()
	{
		if (itemUpPanel != null) itemUpPanel.SetActive(false); 
		if (gemSetPanel != null) gemSetPanel.SetActive(true); 
		if (gemSynPanel != null) gemSynPanel.SetActive(false); 
		if (gemReplacePanel != null) gemReplacePanel.SetActive(false);
        if (itemMakePanel != null) itemMakePanel.SetActive(false);
        if (moneyPanel != null) moneyPanel.SetActive(false);
        RefreshGemSetPanel();
	}

	void ShowGemSynPanel()
	{
		if (itemUpPanel != null) itemUpPanel.SetActive(false); 
		if (gemSetPanel != null) gemSetPanel.SetActive(false); 
		if (gemSynPanel != null) gemSynPanel.SetActive(true); 
		if (gemReplacePanel != null) gemReplacePanel.SetActive(false);
        if (itemMakePanel != null) itemMakePanel.SetActive(false);
        if (moneyPanel != null) moneyPanel.SetActive(false);
        RefreshGemMergePanel();
	}

	void ShowGemReplacePanel()
	{
		if (itemUpPanel != null) itemUpPanel.SetActive(false); 
		if (gemSetPanel != null) gemSetPanel.SetActive(false); 
		if (gemSynPanel != null) gemSynPanel.SetActive(false); 
		if (gemReplacePanel != null) gemReplacePanel.SetActive(true);
        if (itemMakePanel != null) itemMakePanel.SetActive(false);
        if (moneyPanel != null) moneyPanel.SetActive(true);
        RefreshGemReplacePanel();
	}

    void ShowItemMakePanel()
    {
        if (itemUpPanel != null) itemUpPanel.SetActive(false);
        if (gemSetPanel != null) gemSetPanel.SetActive(false);
        if (gemSynPanel != null) gemSynPanel.SetActive(false);
        if (gemReplacePanel != null) gemReplacePanel.SetActive(false);
        if (itemMakePanel != null) itemMakePanel.SetActive(true);
        if (moneyPanel != null) moneyPanel.SetActive(true);
        RefreshItemMakePanel();
    }

    public void SelectItemMake()
    {
        Dictionary<string, int> list = sdUICharacter.Instance.GetSelectList();
        foreach (KeyValuePair<string, int> info in list)
        {
            sdGameItemMgr.Instance.upItem = sdGameItemMgr.Instance.getItem(ulong.Parse(info.Key));
        }

        if (sdGameItemMgr.Instance.upItem != null)
        {
            Hashtable makeTable = sdConfDataMgr.Instance().GetFormula(sdGameItemMgr.Instance.upItem.templateID.ToString());
            if (makeTable != null)
            {
                Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemMake);
                if (iconList.ContainsKey(0))
                {
                    sdSlotIcon icon = iconList[0] as sdSlotIcon;
                    icon.SetInfo(sdGameItemMgr.Instance.upItem.instanceID.ToString(), 
                        sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.upItem.templateID.ToString()));
                }

                if (iconList.ContainsKey(1))
                {
                    sdSlotIcon icon = iconList[1] as sdSlotIcon;
                    string id = makeTable["P1.ItemID"].ToString();
                    icon.SetInfo(id, sdConfDataMgr.Instance().GetItemById(id));
                }

                lbl_money.text = makeTable["Front.Money"].ToString();

                int num = 2;
                for (int i = 1; i <= 5; ++i)
                {
                    string name = string.Format("U{0}.ItemID", i);
                    string count = string.Format("U{0}.Count", i);

                    string id = makeTable[name].ToString();
                    count = makeTable[count].ToString();

                    int compareCount = sdGameItemMgr.Instance.GetItemCount(int.Parse(id));
                    if (compareCount >= int.Parse(count))
                    {
                        count = string.Format("{0}/{1}", compareCount, count);
                    }
                    else
                    {
                        count = string.Format("[{0}]{1}/{2}", sdConfDataMgr.Instance().GetColorHex(Color.red), compareCount, count);
                    }
                    Hashtable info = sdConfDataMgr.Instance().GetItemById(id);
                    if (info == null) break;
                    if (info.ContainsKey("TempCount"))
                    {
                        info["TempCount"] = count;
                    }
                    else
                    {
                        info.Add("TempCount", count);
                    }
                    
                    if (iconList.ContainsKey(num))
                    {
                        sdSlotIcon icon = iconList[num] as sdSlotIcon;
                        icon.SetInfo(id, info);
                        ++num;
                    }
                }

                for (int i = num; i <= 5; ++i)
                {
                    if (iconList.ContainsKey(i))
                    {
                        sdSlotIcon icon = iconList[i] as sdSlotIcon;
                        icon.SetInfo("0", null);
                    }
                }
            }
        }
    }

    public void RefreshItemMakePanel()
    {
        if (itemMakeIcon != null)
        {
            if (sdGameItemMgr.Instance.upItem == null)
            {
                itemMakeIcon.SetInfo("0", null);

                Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemMake);
                foreach (DictionaryEntry item in iconList)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    if (icon == null) continue;
                    icon.SetInfo("0", null);
                }

                lbl_money.text = "0";
            }
            else
            {
                Hashtable makeTable = sdConfDataMgr.Instance().GetFormula(sdGameItemMgr.Instance.upItem.templateID.ToString());
                if (makeTable != null)
                {
                    Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemMake);
                    if (iconList.ContainsKey(0))
                    {
                        sdSlotIcon icon = iconList[0] as sdSlotIcon;
                        icon.SetInfo(sdGameItemMgr.Instance.upItem.instanceID.ToString(),
                            sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.upItem.templateID.ToString()));
                    }

                    lbl_money.text = makeTable["Front.Money"].ToString();

                    if (iconList.ContainsKey(1))
                    {
                        sdSlotIcon icon = iconList[1] as sdSlotIcon;
                        string id = makeTable["P1.ItemID"].ToString();
                        icon.SetInfo(id, sdConfDataMgr.Instance().GetItemById(id));
                    }

                    int num = 2;
                    for (int i = 1; i <= 5; ++i)
                    {
                        string name = string.Format("U{0}.ItemID", i);
                        string count = string.Format("U{0}.Count", i);

                        string id = makeTable[name].ToString();
                        count = makeTable[count].ToString();

                        int compareCount = sdGameItemMgr.Instance.GetItemCount(int.Parse(id));
                        if (compareCount >= int.Parse(count))
                        {
                            count = string.Format("{0}/{1}", compareCount, count);
                        }
                        else
                        {
                            count = string.Format("[{0}]{1}/{2}", sdConfDataMgr.Instance().GetColorHex(Color.red), compareCount, count);
                        }

                        
                        Hashtable info = sdConfDataMgr.Instance().GetItemById(id);
                        if (info == null) break;
                        if (info.ContainsKey("TempCount"))
                        {
                            info["TempCount"] = count;
                        }
                        else
                        {
                            info.Add("TempCount", count);
                        }

                        if (iconList.ContainsKey(num))
                        {
                            sdSlotIcon icon = iconList[num] as sdSlotIcon;
                            icon.SetInfo(id, info);
                            ++num;
                        }
                    }

                    for (int i = num; i <= 5; ++i)
                    {
                        if (iconList.ContainsKey(i))
                        {
                            sdSlotIcon icon = iconList[i] as sdSlotIcon;
                            icon.SetInfo("0", null);
                        }
                    }
                }
            }
        }
    }

    int curShowLevel = 0;
    int nextShowLevel = 0;

    int curShowValue = 0;
    int nextShowValue = 0;

    public void OnChangeLevel()
    {
        if (maxItemUpLvChange.text == curShowLevel.ToString())
        {
            maxItemUpLvChange.text = string.Format("[{0}]{1}",sdConfDataMgr.Instance().GetColorHex(Color.green),nextShowLevel.ToString());
        }
        else
        {
            maxItemUpLvChange.text = curShowLevel.ToString();
        }

        TweenAlpha[] tweenList = maxItemUpLvChange.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.Reset();
        }
    }

    public void OnChangeAttValue()
    {
        if (curItemupAttValue.text == curShowValue.ToString())
        {
            curItemupAttValue.text = string.Format("[{0}]{1}", sdConfDataMgr.Instance().GetColorHex(Color.green), nextShowValue.ToString());
        }
        else
        {
            curItemupAttValue.text = curShowValue.ToString();
        }
        TweenAlpha[] tweenList = curItemupAttValue.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.Reset();
        }
    }

	int lvUp = -1;
    int lastLv = -1;
	float nextPer = 0;
	float time = 0f;

    int upExp = 0;

	void Update()
	{
        if (!isActiveAndEnabled) lvUp = -1;

		if (lvUp >= 0)
		{
			time += Time.unscaledDeltaTime;
			if (time < 0.02f) return;
			time = 0;

			float value = expBar.value + 0.02f;
			if (lvUp == 0 && value >= nextPer)
			{
				value = nextPer;
				--lvUp;
			}
			else if (value >= 1f)
			{
				value = 0f;
				--lvUp;
                RefreshItemUpPanelByItem(sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.tempItem.templateID.ToString()), lastLv - lvUp);
			}

			expBar.value = value;
            int lv = 0;
            if (lvUp >= 0)
            {
                lv = lastLv - lvUp;
            }
            else
            {
                lv = lastLv; 
            }
            sdGameItem item = sdGameItemMgr.Instance.getItem(sdGameItemMgr.Instance.upItem.instanceID);
            Hashtable info = sdConfDataMgr.Instance().GetItemUp(item.templateID.ToString(), lv+1);
            Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
            if (info != null)
            {
                float rate = float.Parse(itemInfo["UpBaseExp"].ToString()) / 10000f;
                int needExp = (int)(float.Parse(info["UpExp"].ToString()) * rate);
                if (needExp == 0)
                {
                    expBar.GetComponentInChildren<UILabel>().text = "Max";
                }
                else
                {
                    if (lvUp < 0)
                    {
                        expBar.GetComponentInChildren<UILabel>().text = string.Format("{0} / {1}", item.upExp, (int)needExp);
                    }
                    else
                    {
                        expBar.GetComponentInChildren<UILabel>().text = string.Format("{0} / {1}", (int)(needExp * value), (int)needExp);
                    }
                }
            }
		}
	}

    public UIButton makeUpbtn = null;

	void Start()
	{
		if (expBar != null)
		{
			expBar.value = 0f;
//             if (expBar.GetComponentInChildren<UILabel>() != null)
//             {
//                 expBar.GetComponentInChildren<UILabel>().text = "0%";
//             }
            
			//expBar.gameObject.SetActive(true);
		}

		if (itemUpbtn != null)
		{
			EventDelegate click = new EventDelegate(ShowItemUpPanel);
			itemUpbtn.onClick.Add(click);
		}

		if (gemSetbtn != null)
		{
			EventDelegate click = new EventDelegate(ShowGemSetPanel);
			gemSetbtn.onClick.Add(click);
		}

		if (gemSynbtn != null)
		{
			EventDelegate click = new EventDelegate(ShowGemSynPanel);
			gemSynbtn.onClick.Add(click);
		}

		if (gemReplacebtn != null)
		{
			EventDelegate click = new EventDelegate(ShowGemReplacePanel);
			gemReplacebtn.onClick.Add(click);
		}

        if (itemMakebtn != null)
        {
            EventDelegate click = new EventDelegate(ShowItemMakePanel);
            itemMakebtn.onClick.Add(click);
        }

        if (makeUpbtn != null)
        {
            EventDelegate click = new EventDelegate(OnClickMakeUp);
            makeUpbtn.onClick.Add(click);
        }

		RefreshItemUpPanel();
	}

    void OnClickMakeUp()
    {
        if (sdGameItemMgr.Instance.upItem == null)
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NoMakeUpItem"), Color.yellow);
            return;
        }
        
        Hashtable makeTable = sdConfDataMgr.Instance().GetFormula(sdGameItemMgr.Instance.upItem.templateID.ToString());
        Hashtable mainProp = (Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"];
        if (int.Parse(mainProp["NonMoney"].ToString()) < int.Parse(makeTable["Front.Money"].ToString()))
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MakeUpNoMoney"), Color.yellow);
            return;
        }

        for (int i = 1; i <= 5; ++i)
        {
            string name = string.Format("U{0}.ItemID", i);
            string count = string.Format("U{0}.Count", i);

            string id = makeTable[name].ToString();
            count = makeTable[count].ToString();

            int compareCount = sdGameItemMgr.Instance.GetItemCount(int.Parse(id));
            if (compareCount < int.Parse(count))
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MakeUpNoItem"), Color.yellow);
                return;
            }
        }

        sdItemMsg.notifyMakeUp();
    }

	public sdSlotIcon upItemIcon = null;
    public sdSlotIcon gemSetItemIcon = null;
    public sdSlotIcon gemMergeIcon = null;
    public sdSlotIcon gemReplaceIcon = null;
    public sdSlotIcon itemMakeIcon = null;

	public void RefreshGemReplacePanel()
	{
        sdGameItemMgr.Instance.selGemList.Clear();
        if (gemReplaceIcon != null)
		{
            gemReplaceIcon.SetInfo("0", null);
			Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemReplace);
			foreach(DictionaryEntry item in iconList)
			{
				sdSlotIcon icon = item.Value as sdSlotIcon;
				if (icon == null) continue;
				icon.SetInfo("0", null);
			}

            lbl_money.text = "0";
            replaceName1.text = "";
            replaceName2.text = "";
		}
	}

    public void RefreshGemMergePanel()
    {
        sdGameItemMgr.Instance.selGemList.Clear();
        if (gemMergeIcon != null)
        {
            gemMergeIcon.SetInfo("0", null);
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemMerge);
            foreach (DictionaryEntry item in iconList)
            {
                sdSlotIcon icon = item.Value as sdSlotIcon;
                if (icon == null) continue;
                icon.SetInfo("0", null);
            }

//             itemTitle.color = Color.white;
//             itemTitle.text = sdConfDataMgr.Instance().GetShowStr("SpendGem");
            lbl_money.text = "0";
            gemMergeMoney1.text = "0";
            gemMergeMoney2.text = "0";
            mergeName1.text = "";
            mergeName2.text = "";
            mergeName3.text = "";
        }
    }

    public void RefreshGemSetPanel()
    {
        sdGameItemMgr.Instance.selGemList.Clear();
        if (gemSetItemIcon != null)
        {
            sdGameItem upItem = sdGameItemMgr.Instance.upItem;
            if (upItem == null)
            {
                gemSetItemIcon.SetInfo("0", null);
                Hashtable iconList1 = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemSet);
                foreach (DictionaryEntry item in iconList1)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    icon.SetInfo("-1", null);
                    icon.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "sd";
                    icon.transform.FindChild("color").GetComponent<UISprite>().spriteName = "";
                    icon.transform.FindChild("add").GetComponent<UISprite>().spriteName = "";
                    //icon.GetComponent<BoxCollider>().enabled = false;
                }
                line1.SetActive(false);
                line2.SetActive(false);
                line3.SetActive(false);
                return;
            }
            Hashtable info = sdConfDataMgr.Instance().GetItemById(upItem.templateID.ToString());
            gemSetItemIcon.SetInfo(upItem.instanceID.ToString(), info);
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemSet);
            if (iconList == null) return;
            line1.SetActive(upItem.gemNum == 1 ? true : false);
            line2.SetActive(upItem.gemNum == 2 ? true : false);
            line3.SetActive(upItem.gemNum == 3 ? true : false);

            for (int i = 0; i < upItem.gemNum; ++i)
            {
                int id = upItem.gemList[i];
                Hashtable gemInfo = sdConfDataMgr.Instance().GetItemById(id.ToString());
                foreach (DictionaryEntry item in iconList)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    if (icon == null) continue;
                    if (icon.index == i)
                    {
                        icon.SetInfo(id.ToString(), gemInfo);
                        string gembg = sdConfDataMgr.Instance().GetGemBg(int.Parse(info["IntExtra3"].ToString()));
                        icon.transform.FindChild("color").GetComponent<UISprite>().spriteName = gembg;
                        icon.transform.FindChild("add").GetComponent<UISprite>().spriteName = "jia";
                        icon.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "iconk";
                        //icon.GetComponent<BoxCollider>().enabled = true;
                        string att = "";
                        if (gemInfo != null)
                        {
                            Hashtable attInfo = sdConfDataMgr.Instance().GetProperty(id.ToString());
                            foreach (DictionaryEntry tempItem in attInfo)
                            {
                                att += tempItem.Key;
                                att += "+";
                                att += tempItem.Value;
                                break;
                            }
                        }
//                         if (i == 0)
//                         {
//                             setName1.text = att;
//                         }
//                         else if (i == 1)
//                         {
//                             setName2.text = att;
//                         }
//                         else if (i == 2)
//                         {
//                             setName3.text = att;
//                         }
                    }
                }
            }

            for (int i = upItem.gemNum; i < 3; ++i)
            {
                foreach (DictionaryEntry item in iconList)
                {
                    sdSlotIcon icon = item.Value as sdSlotIcon;
                    if (icon == null) continue;
                    if (icon.index == i)
                    {
                        icon.SetInfo("-1", null);
                        icon.transform.FindChild("color").GetComponent<UISprite>().spriteName = "";
                        icon.transform.FindChild("add").GetComponent<UISprite>().spriteName = "";
                        icon.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "sd";
                        //icon.GetComponent<BoxCollider>().enabled = false;
//                         if (i == 0)
//                         {
//                             setName1.text = "";
//                         }
//                         else if (i == 1)
//                         {
//                             setName2.text = "";
//                         }
//                         else if (i == 2)
//                         {
//                             setName3.text = "";
//                         }
                    }
                }
            }

//             itemTitle.color = Color.white;
//             itemTitle.text = sdConfDataMgr.Instance().GetShowStr("SpendGem");
            lbl_money.text = "0";
        }
    }

    sdGameItem tempItem = null;
    public void OnEffectFinish()
    {
        sdGameItem item = sdGameItemMgr.Instance.getItem(sdGameItemMgr.Instance.upItem.instanceID);
        Hashtable info = sdConfDataMgr.Instance().GetItemUp(item.templateID.ToString(), item.upLevel + 1);
        Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
        if (info != null)
        {
            float rate = float.Parse(itemInfo["UpBaseExp"].ToString()) / 10000f;
            int needExp = (int)(float.Parse(info["UpExp"].ToString()) * rate);
            if (needExp == 0) nextPer = 0f;
            nextPer = ((float)item.upExp) / needExp;
        }
        else
        {
            nextPer = 0f;
        }
        lastLv = item.upLevel;
        lvUp = item.upLevel - sdGameItemMgr.Instance.tempItem.upLevel;
        sdGameItemMgr.Instance.upItem = item;
        lbl_money.text = "0";
        sdGameItemMgr.Instance.ClearItemUpId();
        Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemUp);
        foreach (DictionaryEntry temp in iconList)
        {
            sdSlotIcon icon = temp.Value as sdSlotIcon;
            if (icon == null || icon.index == 0) continue;
            icon.SetInfo("0", null);
        }
        lbl_money.text = "0";
        //RefreshItemUpPanel();
        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider bc in list)
        {
            bc.enabled = true;
        }
    }

    public void RefreshItemUpPanelByItem(Hashtable info, int level)
    {
        string tid = info["ID"].ToString();
        Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(tid, level);
        Hashtable beforeInfo = sdConfDataMgr.Instance().GetItemUp(tid, sdGameItemMgr.Instance.tempItem.upLevel);
        Hashtable maxInfo = sdConfDataMgr.Instance().GetItemUp(tid, sdConfDataMgr.Instance().GetMaxItemUp(tid));

        if (curInfo != null)
        {
            curItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), level);
            maxItemUpLv.text = string.Format(" / {0}", sdConfDataMgr.Instance().GetMaxItemUp(tid));
            maxItemUpLvChange.text = level.ToString();
            string mainAtt = info["StringExtra3"].ToString();
            int value = int.Parse(info[mainAtt].ToString());
            float rate = 0;
            if (curInfo != null)
            {
                rate = float.Parse(curInfo["UpMainTypeCoe"].ToString());
            }
            int curValue = (int)(value * (1f + (rate / 10000f)));
            int upValue = curValue - value;
            curItemupAttName.text = string.Format("{0}:", sdConfDataMgr.Instance().GetShowStr(mainAtt));
            curItemupAttValue.text = curValue.ToString();
        }

        itemTitle.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(info["Quility"].ToString()));
        itemTitle.text = string.Format("{0}", info["ShowName"].ToString());
    }

    public void OnItemUpSuccess()
    {
        TweenAlpha[] tweenList = maxItemUpLvChange.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.enabled = false;
            ta.Reset();
            maxItemUpLvChange.text = curShowLevel.ToString();
        }
        tweenList = curItemupAttValue.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.enabled = false;
            ta.Reset();
            curItemupAttValue.text = curShowLevel.ToString();
        }
        
        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = false;
        }
        EventDelegate e = new EventDelegate(OnEffectFinish);
        Hashtable info = sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.tempItem.templateID.ToString());
        RefreshItemUpPanelByItem(info, sdGameItemMgr.Instance.tempItem.upLevel);
        sdGameItemMgr.Instance.ClearItemUpId();
        int num = upItemIcon.transform.GetChildCount();
        for (int i = 0; i < num; ++i)
        {
            Transform child = upItemIcon.transform.GetChild(i);
            if (child.GetComponent<sdCopyItem>() != null)
            {
                child.GetComponent<sdCopyItem>().Show();
                child.GetComponent<sdCopyItem>().onFinish.Add(e);
            }
            //sdCopyItem.ShowSuccess(e);
        }
        
        sdUICharacter.Instance.RefreshTip();
    }

    public void OnGemMerge()
    {
        string id1 = "";
        foreach (KeyValuePair<string, int> info in sdGameItemMgr.Instance.selGemList)
        {
            int num = info.Value;
            for (int i = 0; i < num; ++i)
            {
                if (id1 == "")
                {
                    id1 = info.Key;
                }
                else
                {
                    break;
                }
            }
        }
        if (id1 == "")
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MergeGemNumError"), Color.yellow);
            return;
        }

        sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id1));
        if (item == null || item.count < 3)
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MergeGemNumError"), Color.yellow);
            return;
        }

        int money = int.Parse(((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["NonMoney"].ToString());
        if (money < int.Parse(gemMergeMoney1.text))
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("GemMergeNoMoeny"), Color.yellow);
            return;
        }

        sdItemMsg.notifyGemMerge(id1, 1);
    }

    public void OnGemAllMerge()
    {
        string id1 = "";
        foreach (KeyValuePair<string, int> info in sdGameItemMgr.Instance.selGemList)
        {
            int num = info.Value;
            for (int i = 0; i < num; ++i)
            {
                if (id1 == "")
                {
                    id1 = info.Key;
                }
                else
                {
                    break;
                }
            }

        }
        if (id1 == "")
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MergeGemNumError"), Color.yellow);
            return;
        }

        sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id1));
        if (item == null || item.count < 3)
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MergeGemNumError"), Color.yellow);
            return;
        }

        int money = int.Parse(((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["NonMoney"].ToString());
        if (money < int.Parse(gemMergeMoney2.text))
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("GemMergeNoMoeny"), Color.yellow);
            return;
        }

        sdItemMsg.notifyGemMerge(id1, item.count / 3);
    }

    public void OnGemMergeEffectFinish()
    {
        foreach (KeyValuePair<string, int> itemInfo in sdGameItemMgr.Instance.selGemList)
        {
            string id = itemInfo.Key;
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
            if (item == null)
            {
                sdGameItemMgr.Instance.selGemList.Clear();
                gemMergeMoney1.text = "0";
                gemMergeMoney2.text = "0";
                RefreshGemMergePanel();
            }
            else
            {
                Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemMerge);
                foreach (DictionaryEntry temp in iconList)
                {
                    sdSlotIcon icon = temp.Value as sdSlotIcon;
                    icon.transform.FindChild("count").GetComponent<UILabel>().text = item.count.ToString();
                }
                Hashtable config = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(config["Level"].ToString());
                gemMergeMoney1.text = gemInfo["HoleMergeMoney"].ToString();
                gemMergeMoney2.text = (int.Parse(gemInfo["HoleMergeMoney"].ToString()) * item.count / 3).ToString();
            }
        }
        
        lbl_money.text = "0";

        Hashtable createGem = sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.createGemId.ToString());
        gemMergeIcon.SetInfo(sdGameItemMgr.Instance.createGemId.ToString(), createGem);
        gemMergeIcon.transform.FindChild("count").GetComponent<UILabel>().text = sdGameItemMgr.Instance.createGemCount.ToString();

        if (oldEffect != null)
        {
            GameObject.Destroy(oldEffect);
            oldEffect = null;
        }

        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = true;
        }

        if (createGem != null)
        {
            string str = string.Format(sdConfDataMgr.Instance().GetShowStr("SuccessGet1"), createGem["ShowName"].ToString());
            sdUICharacter.Instance.ShowMsgLine(str, Color.yellow);
        }
    }

    public void OnGemMergeSuccess()
    {
        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = false;
        }

        GameObject obj = GameObject.Instantiate(gemMergeEffect) as GameObject;
        obj.transform.parent = gemMergeEffect.transform.parent;
        obj.transform.localPosition = gemMergeEffect.transform.localPosition;
        obj.transform.localScale = gemMergeEffect.transform.localScale;
        gemMergeEffect.GetComponent<sdEffectFinish>().onFinish.Add(new EventDelegate(OnGemMergeEffectFinish));
        gemMergeEffect.SetActive(true);
        oldEffect = gemMergeEffect;
        gemMergeEffect = obj;

    }

    public void OnGemReplaceEffectFinish()
    {
        sdGameItemMgr.Instance.selGemList.Clear();
        lbl_money.text = "0";
        RefreshGemReplacePanel();
        Hashtable createGem = sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.createGemId.ToString());
        gemReplaceIcon.SetInfo(sdGameItemMgr.Instance.createGemId.ToString(), createGem);
        if (oldEffect != null)
        {
            GameObject.Destroy(oldEffect);
            oldEffect = null;
        }

        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = true;
        }
        if (createGem != null)
        {
            string str = string.Format(sdConfDataMgr.Instance().GetShowStr("SuccessGet1"), createGem["ShowName"].ToString());
            sdUICharacter.Instance.ShowMsgLine(str, Color.yellow);
        }
    }

    public void OnGemReplaceSuccess()
    {
        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = false;
        }

        GameObject obj = GameObject.Instantiate(gemReplaceEffect) as GameObject;
        obj.transform.parent = gemReplaceEffect.transform.parent;
        obj.transform.localPosition = gemReplaceEffect.transform.localPosition;
        obj.transform.localScale = gemReplaceEffect.transform.localScale;
        gemReplaceEffect.GetComponent<sdEffectFinish>().onFinish.Add(new EventDelegate(OnGemReplaceEffectFinish));
        gemReplaceEffect.SetActive(true);
        oldEffect = gemReplaceEffect;
        gemReplaceEffect = obj;
    }

    public void OnGemOnEffectFinish()
    {
        if (oldEffect != null)
        {
            GameObject.Destroy(oldEffect);
            oldEffect = null;
        }

        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = true;
        }
        sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("GemOnSuccess"), Color.yellow);
    }

    GameObject oldEffect = null;

    public void OnGemOnSuccess(int index)
    {
        BoxCollider[] list = GetComponentsInChildren<BoxCollider>();
        BoxCollider self = GetComponent<BoxCollider>();
        foreach (BoxCollider item in list)
        {
            if (item == self) continue;
            item.enabled = false;
        }

        GameObject obj = GameObject.Instantiate(gemSetEffect) as GameObject;
        obj.transform.parent = gemSetEffect.transform.parent;
        obj.transform.localPosition = gemSetEffect.transform.localPosition;
        obj.transform.localScale = gemSetEffect.transform.localScale;
        for(int i = 0; i < gemSetEffect.transform.GetChildCount(); ++i)
        {
            Transform child = gemSetEffect.transform.GetChild(i);
            if (child.name == "baoshi1")
            {
                child.gameObject.SetActive(index == 0 ? true : false);
            }
            else if (child.name == "baoshi2")
            {
                child.gameObject.SetActive(index == 1 ? true : false);
            }
            else if (child.name == "baoshi3")
            {
                child.gameObject.SetActive(index == 2 ? true : false);
            }
        }
        gemSetEffect.SetActive(true);
        gemSetEffect.GetComponentInChildren<sdEffectFinish>().onFinish.Add(new EventDelegate(OnGemOnEffectFinish));
        oldEffect = gemSetEffect;
        gemSetEffect = obj;

        sdGameItemMgr.Instance.selGemList.Clear();
        sdGameItemMgr.Instance.upItem = sdGameItemMgr.Instance.getItem(sdGameItemMgr.Instance.upItem.instanceID);
        lbl_money.text = "0";
        RefreshGemSetPanel();

    }

    public void OnGemOffSuccess()
    {
        sdGameItemMgr.Instance.selGemList.Clear();
        lbl_money.text = "0";
        sdGameItemMgr.Instance.upItem = sdGameItemMgr.Instance.getItem(sdGameItemMgr.Instance.upItem.instanceID);
        RefreshGemSetPanel();
    }

	public void RefreshItemUpPanel()
	{
        TweenAlpha[] tweenList = maxItemUpLvChange.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.enabled = false;
            ta.Reset();
        }
        tweenList = curItemupAttValue.GetComponents<TweenAlpha>();
        foreach (TweenAlpha ta in tweenList)
        {
            ta.enabled = false;
            ta.Reset();
        }

        sdGameItemMgr.Instance.ClearItemUpId();
        if (sdGameItemMgr.Instance.upItem != null)
        {
            showLabel.SetActive(true);
            Hashtable info = sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.upItem.templateID.ToString());
            upItemIcon.SetInfo(sdGameItemMgr.Instance.upItem.instanceID.ToString(), info);
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemUp);
            foreach (DictionaryEntry item in iconList)
            {
                sdSlotIcon icon = item.Value as sdSlotIcon;
                if (icon == null || icon.index == 0) continue;
                icon.SetInfo("0", null);
            }
            expBar.gameObject.SetActive(true);
            desLabel.SetActive(false);
            int curLv = sdGameItemMgr.Instance.upItem.upLevel;
            string tid = sdGameItemMgr.Instance.upItem.templateID.ToString();
            Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(tid, curLv);
            Hashtable nextInfo = sdConfDataMgr.Instance().GetItemUp(tid, curLv + 1);
            Hashtable maxInfo = sdConfDataMgr.Instance().GetItemUp(tid, sdConfDataMgr.Instance().GetMaxItemUp(tid));
            if (maxInfo != null)
            {
                nextItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), sdConfDataMgr.Instance().GetMaxItemUp(tid));
                string mainAtt = info["StringExtra3"].ToString();
                int value = int.Parse(info[mainAtt].ToString());
                float rate = 0;
                rate = float.Parse(maxInfo["UpMainTypeCoe"].ToString());
                int maxValue = (int)(value * (1f + (rate / 10000f)));
                nextItemupAtt.text = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr(mainAtt), maxValue);
            }
            if (curInfo != null || curLv == 0)
            {
                curItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), curLv);
                maxItemUpLv.text = string.Format(" / {0}", sdConfDataMgr.Instance().GetMaxItemUp(tid));
                maxItemUpLvChange.text = curLv.ToString();

                string mainAtt = info["StringExtra3"].ToString();
                int value = int.Parse(info[mainAtt].ToString());
                float rate = 0;
                if (curInfo != null)
                {
                    rate = float.Parse(curInfo["UpMainTypeCoe"].ToString());
                }
                int curValue = (int)(value * (1f + (rate / 10000f)));
                int upValue = curValue - value;
//                if (upValue == 0)
//                {
                curItemupAttName.text = string.Format("{0}:", sdConfDataMgr.Instance().GetShowStr(mainAtt));
                curItemupAttValue.text = curValue.ToString();
//                 if (upValue != 0)
//                 {
//                     curShowValue = value;
//                     nextShowValue = curValue;
//                     curItemupAttValue.GetComponent<TweenAlpha>().enabled = true;
//                 }
//                }
//                 else
//                 {
//                     curItemupAtt.text = string.Format("{0}: {1}[{2}]+{3}", sdConfDataMgr.Instance().GetShowStr(mainAtt), value, sdConfDataMgr.Instance().GetColorHex(Color.green), upValue);
//                 }
                
                if (nextInfo == null)
                {
                    //nextItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), "Max");
                    //nextItemupAtt.text = "";
                }
                else
                {
                    //nextItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), curLv + 1);
                    //int nextValue = (int)(value * (1f + float.Parse(nextInfo["UpMainTypeCoe"].ToString()) / 10000f));
                    //nextItemupAtt.text = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr(mainAtt), nextValue);
                    float rate1 = float.Parse(info["UpBaseExp"].ToString()) / 10000f;
                    float needExp = float.Parse(nextInfo["UpExp"].ToString()) * rate1;
                    expBar.value = ((float)sdGameItemMgr.Instance.upItem.upExp) / needExp;
					if (expBar.GetComponentInChildren<UILabel>() != null)
					{
						expBar.GetComponentInChildren<UILabel>().text = string.Format("{0} / {1}", sdGameItemMgr.Instance.upItem.upExp, (int)needExp);
					}  
                }
            }

            itemTitle.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(info["Quility"].ToString()));
            itemTitle.text = string.Format("{0}", info["ShowName"].ToString());
            lbl_money.text = "0";
        }
        else
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemUp);
            foreach (DictionaryEntry item in iconList)
            {
                sdSlotIcon icon = item.Value as sdSlotIcon;
                if (icon == null) continue;
                icon.SetInfo("0", null);
            }
            expBar.gameObject.SetActive(false);
            nextItemupLv.text = "";
            nextItemupAtt.text = "";
            curItemupAttName.text = "";
            curItemupAttValue.text = "";
            curItemupLv.text = "";
            itemTitle.text = "";
            maxItemUpLv.text = "";
            maxItemUpLvChange.text = "";
            showLabel.SetActive(false);
            desLabel.SetActive(true);
        }
	}

	public void OnSelectItemOk()
	{
		Hashtable list = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemUp);
		Dictionary<string, int> select = sdUICharacter.Instance.GetSelectList();
        sdGameItemMgr.Instance.ClearItemUpId();
		if (select != null)
		{
			foreach(string selId in select.Keys)
			{
				sdGameItemMgr.Instance.AddItemUpId(selId);
			}
		}

		ulong[] idList = sdGameItemMgr.Instance.GetItemUpId();
		int num = 0;
		float exp = sdGameItemMgr.Instance.upItem == null ? 0 : sdGameItemMgr.Instance.upItem.upExp;
		float money = 0;
		foreach(DictionaryEntry info in list)
		{
			sdSlotIcon icon = info.Value as sdSlotIcon;
			if (icon.index == 0) continue;
            if (num >= idList.Length)
            {
                icon.SetInfo("0", null);
                continue;
            }
			sdGameItem item = sdGameItemMgr.Instance.getItem(idList[num]);
			if (item != null && icon != null)
			{
				Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
				icon.SetInfo(item.instanceID.ToString(), itemInfo);
				Hashtable upInfo = sdConfDataMgr.Instance().GetItemUp(item.templateID.ToString(), item.upLevel);
				float rate = float.Parse(itemInfo["UpBaseExp"].ToString())/10000f;
				float moneyRate = float.Parse(itemInfo["UpBaseMoney"].ToString())/10000f;
				if (upInfo != null)
				{
					exp += (int)(float.Parse(upInfo["UpExp2"].ToString()) * rate);
                    money += (int)(float.Parse(upInfo["UpExpMoney"].ToString()) * moneyRate);
				}
			}
			num++;
		}

		Hashtable upItemInfo =  sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.upItem.templateID.ToString());
		lbl_money.text = ((int)money).ToString();

		int curLv = sdGameItemMgr.Instance.upItem.upLevel;
		Hashtable curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv+1);
        if (curUpInfo == null)
        {
            return;
        }
		float rate1 = float.Parse(upItemInfo["UpBaseExp"].ToString())/10000f;
		int needExp = (int)(float.Parse(curUpInfo["UpExp"].ToString())*rate1);
        upExp = (int)exp;
        if (upExp > 0)
        {
            expBar.GetComponentInChildren<UILabel>().text = string.Format("{0}+[{1}]{2} [{3}]/ {4}", sdGameItemMgr.Instance.upItem.upExp,
            sdConfDataMgr.Instance().GetColorHex(Color.green), upExp, sdConfDataMgr.Instance().GetColorHex(Color.white), needExp);
        }
        
		while (curUpInfo != null && exp > needExp)
		{
			exp -= needExp;
			++curLv;
			curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv+1);
			needExp = (int)(float.Parse(curUpInfo["UpExp"].ToString())*rate1);
		}
// 
// 		if (curLv == sdGameItemMgr.Instance.upItem.upLevel) 
// 		{
// 			++curLv;
// 			curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv);
// 		}
        if (curLv != sdGameItemMgr.Instance.upItem.upLevel) 
		{
			curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv);
		}

		string mainAtt = upItemInfo["StringExtra3"].ToString();
		int value = int.Parse(upItemInfo[mainAtt].ToString());
		//nextItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), curLv);
		int nextValue = (int)(value * (1f+float.Parse(curUpInfo["UpMainTypeCoe"].ToString())/10000f));
        Hashtable totalInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), sdGameItemMgr.Instance.upItem.upLevel);
        int totalValue = (int)(value * (1f + float.Parse(totalInfo["UpMainTypeCoe"].ToString()) / 10000f));
        if (curLv > sdGameItemMgr.Instance.upItem.upLevel)
        {
            curShowLevel = sdGameItemMgr.Instance.upItem.upLevel;
            nextShowLevel = curLv;
            TweenAlpha[] tweenList = maxItemUpLvChange.GetComponents<TweenAlpha>();
            foreach (TweenAlpha ta in tweenList)
            {
                ta.enabled = true;
            }

            if (nextValue > totalValue)
            {
                curShowValue = totalValue;
                nextShowValue = nextValue;
                tweenList = curItemupAttValue.GetComponents<TweenAlpha>();
                foreach (TweenAlpha ta in tweenList)
                {
                    ta.enabled = true;
                }
            }

        }
		//nextItemupAtt.text = string.Format("{0}: {1}", sdConfDataMgr.Instance().GetShowStr(mainAtt), nextValue);
	}

    public void SelectGemSet()
    {
        Dictionary<string, int> select =  sdUICharacter.Instance.GetSelectList();
        IEnumerator itr = select.Keys.GetEnumerator();
        if (itr.MoveNext())
        {
            if (itr.Current.ToString() == ulong.MaxValue.ToString())
            {
                sdItemMsg.notifyGemOff(sdGameItemMgr.Instance.gemIndex, gemSetItemIcon.itemid);
            }
            else
            {
                sdItemMsg.notifyGemOn(sdGameItemMgr.Instance.gemIndex, gemSetItemIcon.itemid, itr.Current.ToString());
            }
        }
    }

    public void SelectGemMerge()
    {
        Hashtable list = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemMerge);
        Dictionary<string, int> select = sdUICharacter.Instance.GetSelectList();
        if (select != null)
        {
            sdGameItemMgr.Instance.selGemList = select;
        }
        gemMergeIcon.SetInfo("0", null);
        int nameNum = 0;
        foreach (KeyValuePair<string, int> itemInfo in select)
        {
            int num = itemInfo.Value;
            string id = itemInfo.Key;
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
            Hashtable config = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
            Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(config["Level"].ToString());
            gemMergeMoney1.text = gemInfo["HoleMergeMoney"].ToString();
            gemMergeMoney2.text = (int.Parse(gemInfo["HoleMergeMoney"].ToString()) * item.count / 3).ToString();
            if (item == null || num <= 0) continue;
            foreach (DictionaryEntry info in list)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                if (icon.index == nameNum)
                {
                    if (nameNum == 0)
                    {
                        mergeName1.text = config["ShowName"].ToString();
                    }
                    else if (nameNum == 1)
                    {
                        mergeName2.text = config["ShowName"].ToString();
                    }
                    else if (nameNum == 2)
                    {
                        mergeName3.text = config["ShowName"].ToString();
                    }
                    icon.SetInfo(id, config);
                    --num;
                    ++nameNum;
                    if (num == 0) break;
                }
            }
        }
    }

    public void SetMergeMoeny(string money1, string money2)
    {
        gemMergeIcon.SetInfo("0", null);
        gemMergeMoney1.text = money1;
        gemMergeMoney2.text = money2;
    }

    public void SetMoney(string money)
    {
        gemReplaceIcon.SetInfo("0", null);
        gemMergeIcon.SetInfo("0", null);
        lbl_money.text = money;
    }

	public void OnSelectGemReplace()
	{
        
// 		Hashtable list = sdSlotMgr.Instance.GetIconList(PanelType.Panel_GemReplace);
// 		Dictionary<string, int> select = sdUICharacter.Instance.GetSelectList();
// 		if (select != null)
// 		{
//             sdGameItemMgr.Instance.selGemList = select;
// 		}
//         gemReplaceIcon.SetInfo("0", null);
//         int nameNum = 0;
//         foreach (KeyValuePair<string, int> itemInfo in select)
//         {
//             int num = itemInfo.Value;
//             string id = itemInfo.Key;
//             sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
//             Hashtable config = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
//             Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(config["Level"].ToString());
//             lbl_money.text = gemInfo["HoleRefreshMoney"].ToString();
//             if (item == null || num <= 0) continue;
//             foreach (DictionaryEntry info in list)
//             {
//                 sdSlotIcon icon = info.Value as sdSlotIcon;
//                 if (icon.index == nameNum)
//                 {
//                     if (nameNum == 0)
//                     {
//                         replaceName1.text = config["ShowName"].ToString();
//                     }
//                     else if (nameNum == 1)
//                     {
//                         replaceName2.text = config["ShowName"].ToString();
//                     }
//                     icon.SetInfo(id, config);
//                     --num;
//                     ++nameNum;
//                     if (num == 0) break;
//                 }
//             }
//         }
	}

    static int SortItem(sdGameItem item, sdGameItem compare)
    {
        if (item.quility > compare.quility)
        {
            return 1;
        }
        else if (item.quility < compare.quility)
        {
            return -1;
        }
        else
        {
            int score1 = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
            int score2 = sdConfDataMgr.Instance().GetItemScore(compare.instanceID);
            if (score1 > score2)
            {
                return 1;
            }
            else if (score1 < score2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

	public void SetAllItem()
	{
        if (sdGameItemMgr.Instance.upItem == null)
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SelectUpItem"), Color.yellow);
            return;
        }

        int existNum = sdGameItemMgr.Instance.GetItemUpId().Length;
        if (existNum >= 6) return;

        Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemUp);
        List<sdGameItem> list = sdGameItemMgr.Instance.GetEquipItemsInBag(false);
        list.Sort(SortItem);
        foreach (sdGameItem item in list)
        {
            if (item.quility > 2) continue;
            foreach (DictionaryEntry iconInfo in iconList)
            {
                sdSlotIcon icon = iconInfo.Value as sdSlotIcon;
                if (icon.itemid != "0") continue;
                if (sdGameItemMgr.Instance.hasSelectItemUp(item.instanceID.ToString())) continue;
                if (sdGameItemMgr.Instance.upItem.instanceID == item.instanceID) continue;
                icon.SetInfo(item.instanceID.ToString(), sdConfDataMgr.Instance().GetItemById(item.templateID.ToString()));
                sdGameItemMgr.Instance.AddItemUpId(item.instanceID.ToString());
                ++existNum;
                break;
            }
            if (existNum >= 6) break;
        }

        ulong[] idList = sdGameItemMgr.Instance.GetItemUpId();
        int num = 0;
        float exp = sdGameItemMgr.Instance.upItem == null ? 0 : sdGameItemMgr.Instance.upItem.upExp;
        float money = 0;
        foreach (DictionaryEntry info in iconList)
        {
            sdSlotIcon icon = info.Value as sdSlotIcon;
            if (icon.index == 0) continue;
            if (num >= idList.Length)
            {
                icon.SetInfo("0", null);
                continue;
            }
            sdGameItem item = sdGameItemMgr.Instance.getItem(idList[num]);
            if (item != null && icon != null)
            {
                Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                icon.SetInfo(item.instanceID.ToString(), itemInfo);
                Hashtable upInfo = sdConfDataMgr.Instance().GetItemUp(item.templateID.ToString(), item.upLevel);
                float rate = float.Parse(itemInfo["UpBaseExp"].ToString()) / 10000f;
                float moneyRate = float.Parse(itemInfo["UpBaseMoney"].ToString()) / 10000f;
                if (upInfo != null)
                {
                    exp += (int)(float.Parse(upInfo["UpExp2"].ToString()) * rate);
                    money += (int)(float.Parse(upInfo["UpExpMoney"].ToString()) * moneyRate);
                }
            }
            num++;
        }

        Hashtable upItemInfo = sdConfDataMgr.Instance().GetItemById(sdGameItemMgr.Instance.upItem.templateID.ToString());
        lbl_money.text = ((int)money).ToString();

        int curLv = sdGameItemMgr.Instance.upItem.upLevel;
        Hashtable curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv + 1);
        if (curUpInfo == null)
        {
            return;
        }
        float rate1 = float.Parse(upItemInfo["UpBaseExp"].ToString()) / 10000f;
        int needExp = (int)(float.Parse(curUpInfo["UpExp"].ToString()) * rate1);
        upExp = (int)exp;
        if (upExp > 0)
        {
            expBar.GetComponentInChildren<UILabel>().text = string.Format("{0}+[{1}]{2} [3]/ {4}", sdGameItemMgr.Instance.upItem.upExp,
            sdConfDataMgr.Instance().GetColorHex(Color.green), upExp, sdConfDataMgr.Instance().GetColorHex(Color.white), needExp);
        }
        
        while (curUpInfo != null && exp > needExp)
        {
            exp -= needExp;
            ++curLv;
            curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv + 1);
            needExp = (int)(float.Parse(curUpInfo["UpExp"].ToString()) * rate1);
        }

//         if (curLv == sdGameItemMgr.Instance.upItem.upLevel)
//         {
//             ++curLv;
//             curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv);
//         }
        if (curLv != sdGameItemMgr.Instance.upItem.upLevel)
        {
            curUpInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), curLv);
        }

        string mainAtt = upItemInfo["StringExtra3"].ToString();
        int value = int.Parse(upItemInfo[mainAtt].ToString());
        //nextItemupLv.text = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("ItemUpLv"), curLv);
        int nextValue = (int)(value * (1f + float.Parse(curUpInfo["UpMainTypeCoe"].ToString()) / 10000f));
        Hashtable totalInfo = sdConfDataMgr.Instance().GetItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString(), sdGameItemMgr.Instance.upItem.upLevel);
        int totalValue = (int)(value * (1f + float.Parse(totalInfo["UpMainTypeCoe"].ToString()) / 10000f));
        //nextItemupAtt.text = string.Format("{0} +{1}", sdConfDataMgr.Instance().GetShowStr(mainAtt), nextValue);
        if (curLv > sdGameItemMgr.Instance.upItem.upLevel)
        {
            curShowLevel = sdGameItemMgr.Instance.upItem.upLevel;
            nextShowLevel = curLv;
            TweenAlpha[] tweenList = maxItemUpLvChange.GetComponents<TweenAlpha>();
            foreach (TweenAlpha ta in tweenList)
            {
                ta.enabled = true;
            }

            if (nextValue > totalValue)
            {
                curShowValue = totalValue;
                nextShowValue = nextValue;
                tweenList = curItemupAttValue.GetComponents<TweenAlpha>();
                foreach (TweenAlpha ta in tweenList)
                {
                    ta.enabled = true;
                }
            }

        }
    }
}
