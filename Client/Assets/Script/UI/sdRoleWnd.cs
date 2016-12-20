using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class sdRoleWnd : MonoBehaviour
{
	public GameObject detailPanel = null;
	public GameObject propPanel = null;
	public GameObject bagPanel = null;
	public GameObject jobBack = null;
	
	public GameObject bagTab = null;
	public GameObject proTab = null;
	
	public GameObject equipBtn = null;

    public sdRadioButton armorBtn = null;
    public sdRadioButton weaponBtn = null;
    public sdRadioButton shipinBtn = null;
    public sdRadioButton otherBtn = null;
	
    void OnUnActiveArmorBtn()
    {
        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Armor);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem temp = info.Value as sdGameItem;
            temp.isNew = false;
        }
        armorBtn.HideRedTip();
    }

    void OnUnActiveWeaponBtn()
    {
        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Weapon);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem temp = info.Value as sdGameItem;
            temp.isNew = false;
        }
        weaponBtn.HideRedTip();
    }

    void OnUnActiveShipinBtn()
    {
        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Shipin);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem temp = info.Value as sdGameItem;
            temp.isNew = false;
        }
        shipinBtn.HideRedTip();
    }

    void OnUnActiveOtherBtn()
    {

        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Other);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem temp = info.Value as sdGameItem;
            temp.isNew = false;
        }
        otherBtn.HideRedTip();
    }

	void Start()
	{
        sdSlotMgr.Instance.isFirst = true;
		if (detailPanel != null) detailPanel.SetActive(false);
		//if (propPanel != null) propPanel.SetActive(false);
		
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.isActive)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
		
		Hashtable valueDesc = new Hashtable();
		string score = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("Score"), sdConfDataMgr.Instance().PlayerScore());
		valueDesc["value"] = score;
		valueDesc["des"] = "";
		sdUICharacter.Instance.SetProperty("Score", valueDesc);
		sdSlotMgr.Instance.itemFilter = (int)ItemFilter.Armor;
		sdSlotMgr.Instance.selectedItem.Clear();
        sdSlotMgr.Instance.Init();

        sdMainChar mainChar = sdGameLevel.instance.mainChar;
        if (mainChar == null) return;
        float def = (float)mainChar["Def"];
        int level = mainChar["Level"];
        string rate = GetPropertyPercent(def,level);
        string str = string.Format(sdConfDataMgr.Instance().GetShowStr("DefDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("DefDes", valueDesc);

        float pierce = (float)mainChar["Pierce"];
        rate = GetPropertyPercent(pierce, level);
        str = string.Format(sdConfDataMgr.Instance().GetShowStr("PierceDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("PierceDesc", valueDesc);

        float iceDef = (float)mainChar["IceDef"];
        rate = GetPropertyPercent(iceDef, level);
        str = string.Format(sdConfDataMgr.Instance().GetShowStr("IceDefDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("IceDefDesc", valueDesc);

        float fireDef = (float)mainChar["FireDef"];
        rate = GetPropertyPercent(fireDef, level);
        str = string.Format(sdConfDataMgr.Instance().GetShowStr("FireDefDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("FireDefDesc", valueDesc);

        float PoisonDef = (float)mainChar["PoisonDef"];
        rate = GetPropertyPercent(PoisonDef, level);
        str = string.Format(sdConfDataMgr.Instance().GetShowStr("PoisonDefDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("PoisonDefDesc", valueDesc);

        float ThunderDef = (float)mainChar["ThunderDef"];
        rate = GetPropertyPercent(ThunderDef, level);
        str = string.Format(sdConfDataMgr.Instance().GetShowStr("ThunderDefDesc"), rate);
        valueDesc["value"] = str;
        sdUICharacter.Instance.SetProperty("ThunderDefDesc", valueDesc);

        armorBtn.onUnActive.Add(new EventDelegate(OnUnActiveArmorBtn));
        weaponBtn.onUnActive.Add(new EventDelegate(OnUnActiveWeaponBtn));
        shipinBtn.onUnActive.Add(new EventDelegate(OnUnActiveShipinBtn));
        otherBtn.onUnActive.Add(new EventDelegate(OnUnActiveOtherBtn));
	}

    string GetPropertyPercent(float value, float level)
    {
        if (value == 0) return "0";

        float ret = value * 100 / (value + level * 270 + 2700);
        return ret.ToString("0.00");
    }
	
	bool needUpdate = true;
	
	void Update()
	{
		if (needUpdate && jobBack != null && sdConfDataMgr.Instance().jobAtlas != null)
		{
			jobBack.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().jobAtlas;
			jobBack.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobBack(sdGameLevel.instance.mainChar.Property["Job"].ToString());
			needUpdate = false;
		}
		
		if (equipBtn != null)
		{
			if (sdSlotMgr.Instance.selectedItem.Count == 1)
			{
				sdSlotIcon icon = sdSlotMgr.Instance.selectedItem[0];
				sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(icon.itemid));
				if (item != null && item.CanEquip(sdGameLevel.instance.mainChar));
				{
					equipBtn.GetComponent<UIButton>().enabled = true;		
				}
			}
			else
			{
				equipBtn.GetComponent<UIButton>().enabled = false;
			}
		}
	}
	
	public void ShowBag()
	{
		if (bagPanel != null) bagPanel.SetActive(true);
		if (propPanel != null) propPanel.SetActive(false);
        List<int> list = new List<int>();
        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.All);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem item = info.Value as sdGameItem;
            if (item.isNew)
            {
                if (item.itemClass != 0 && item.itemClass != 1 && item.itemClass != 2)
                {
                    if (list.Contains((int)ItemFilter.Other)) continue;
                    list.Add((int)ItemFilter.Other);
                }
                else
                {
                    if (list.Contains(item.itemClass)) continue;
                    list.Add(item.itemClass);
                }
            }
        }

        if (list.Contains((int)ItemFilter.Other))
        {
            otherBtn.ShowRedTip();
        }
        else
        {
            otherBtn.HideRedTip();
        }

        if (list.Contains((int)ItemFilter.Weapon))
        {
            weaponBtn.ShowRedTip();
        }
        else
        {
            weaponBtn.HideRedTip();
        }

        if (list.Contains((int)ItemFilter.Armor))
        {
            armorBtn.ShowRedTip();
        }
        else
        {
            armorBtn.HideRedTip();
        }

        if (list.Contains((int)ItemFilter.Shipin))
        {
            shipinBtn.ShowRedTip();
        }
        else
        {
            shipinBtn.HideRedTip();
        }

	}
	
	public void ShowProperty()
	{
		if (propPanel != null) propPanel.SetActive(true);
		if (bagPanel != null) bagPanel.SetActive(false);
	}

	public void ShowMode(bool isRole)
	{
		if (isRole)
		{
			if (proTab != null)
			{
				proTab.GetComponent<sdRadioButton>().Active(true);	
				sdUICharacter.Instance.ActiceRadioBtn(proTab.GetComponent<sdRadioButton>());
			}
			ShowProperty();
		}
		else
		{
			if (bagTab != null)
			{
				bagTab.GetComponent<sdRadioButton>().Active(true);	
				sdUICharacter.Instance.ActiceRadioBtn(bagTab.GetComponent<sdRadioButton>());
			}	
			ShowBag();
		}
	}
}