using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SelectType
{
	ItemUp,
    ItemUpChange,
	GemOn,
    GemMerge,
    GemReplace,
    ItemSell,
    EquipSelect,
    ItemMake,
}

public class sdItemSelectWnd : MonoBehaviour
{
	public GameObject copyItem = null;
	public sdRoleWndButton btn_OK = null;
	SelectType selectType = SelectType.ItemUp;
    int equipPos = -1;

	public Dictionary<string, int> selectList = new Dictionary<string, int>();
	int selectNum = 0;
    public int sellType = 0;

    public GameObject sellPanel = null;
    public GameObject sortPanel = null;
    public List<sdGameItem> itemTable = new List<sdGameItem>();
    public GameObject moneyPanel = null;
    public UILabel moneyNum = null;
    public GameObject btnPanel = null;
    public GameObject arrow = null;

    public List<EventDelegate> onClick = new List<EventDelegate>();

    void OnSell()
    {
        if (onClick.Count > 0)
        {
            EventDelegate.Execute(onClick);
        }
        sdUICharacter.Instance.HideSelectWnd();
    }

    void OnOK()
    {
        if (selectType == SelectType.ItemSell)
        {
            bool hasSelect = false;
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                if (icon.gameObject.active && icon.isSelected)
                {
                    hasSelect = true;
                    break;
                }
            }

            if (!hasSelect)
            {
                sdUICharacter.Instance.HideSelectWnd();
                return;
            }
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("SellMsg"), new sdMsgBox.OnConfirm(OnSell), null);
        }
        else
        {
            if (onClick.Count > 0)
            {
                EventDelegate.Execute(onClick);
            }
            sdUICharacter.Instance.HideSelectWnd();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (selectType == SelectType.ItemSell)
        {
            int price = 0;
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                if (icon.isActiveAndEnabled && icon.isSelected)
                {
                    sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(icon.itemid));
                    if (item == null) continue;
                    Hashtable table = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                    if (table == null) continue;
                    price += (int.Parse(table["Value"].ToString()) * item.count);
                }
            }

            moneyNum.text = price.ToString();
        }
    }

    public void SetEquipPos(int pos)
    {
        equipPos = pos;
    }

	public bool AddSelectItem(string id)
	{
        bool isOver = true;
        if (selectType == SelectType.ItemUp)
        {
            isOver = false;
            if (selectNum >= 6) return false;
        }
        else if (selectType == SelectType.ItemUpChange)
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                icon.SetSelect(false);
            }
            selectList.Clear();
        }
        else if (selectType == SelectType.ItemMake)
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                icon.SetSelect(false);
            }
            selectList.Clear();
        }
        else if (selectType == SelectType.GemOn)
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                icon.SetSelect(false);
            }
            selectList.Clear();
        }
        else if (selectType == SelectType.GemMerge)
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                icon.SetSelect(false);
            }
            selectList.Clear();
        }
        else if (selectType == SelectType.GemReplace)
        {
            Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
            foreach (DictionaryEntry info in iconList)
            {
                sdSlotIcon icon = info.Value as sdSlotIcon;
                icon.SetSelect(false);
            }
            selectList.Clear();
        }
        else if (selectType == SelectType.ItemSell)
        {
            isOver = false;
        }
        else if (selectType == SelectType.EquipSelect)
        {
            if (selectNum >= 1) return false;
        }
		
		if (selectList.ContainsKey(id))
		{
			selectList[id]++;
		}
		else
		{
			selectList.Add(id, 1);
		}
		++selectNum;

        if (isOver)
        {
            gameObject.SetActive(false);
            if (btn_OK.onClick.Count > 0)
            {
                EventDelegate.Execute(btn_OK.onClick);
                btn_OK.onClick.Clear();
            }
        }

		return true;
	}

	public void RemoveSelectItem(string id)
	{
		if (!selectList.ContainsKey(id)) return;
		if (selectList[id] == 1)
		{
			selectList.Remove(id);
		}
		else
		{
			selectList[id] -= 1;
		}
        --selectNum;
	}

	public Dictionary<string, int> GetList()
	{
		return selectList;
	}

	public void ShowWnd(SelectType type, int SortType)
	{
        btn_OK.onClick.Add(new EventDelegate(OnOK));
		selectNum = 0;
		selectList.Clear();
        if (type == SelectType.ItemSell)
        {
            sellType |= (1 << 1);
            sellPanel.SetActive(true);
            sellPanel.transform.FindChild("btn_sellWhite").FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
            sellPanel.transform.FindChild("btn_sellWhite").FindChild("effected").GetComponent<UISprite>().spriteName = "g2";
            sellPanel.transform.FindChild("btn_sellGreen").FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2";
            sellPanel.transform.FindChild("btn_sellGreen").FindChild("effected").GetComponent<UISprite>().spriteName = "";
            sellPanel.transform.FindChild("btn_sellBlue").FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2";
            sellPanel.transform.FindChild("btn_sellBlue").FindChild("effected").GetComponent<UISprite>().spriteName = "";
            sortPanel.SetActive(false);
            moneyPanel.SetActive(true);
        }
        else
        {
            sellPanel.SetActive(false);
            sortPanel.SetActive(true);
            moneyPanel.SetActive(false);
            sdRadioButton btn = sortPanel.transform.FindChild("btn_sortLv").GetComponent<sdRadioButton>();
            btn.Active(true);
            sdUICharacter.Instance.ActiceRadioBtn(btn);
        }
		RefreshItem(type, SortType);
	}

	public void AddEventOnSelectWnd(EventDelegate OnSelect)
	{
        if (OnSelect != null) onClick.Add(OnSelect);
	}

    public void ClearEvent()
    {
        onClick.Clear();
    }

	static int OnSortQuility(sdGameItem item, sdGameItem compare)
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
            if (item.level > compare.level)
            {
                return 1;
            }
            else if (item.level < compare.level)
            {
                return -1;
            }
        }
		return 0;
	}

	static int OnSortLevel(sdGameItem item, sdGameItem compare)
	{
		if (item.level > compare.level)
		{
			return 1;
		}
        else if (item.level < compare.level)
        {
            return -1;
        }
        else
        {
            if (item.quility > compare.quility)
            {
                return 1;
            }
            else if (item.quility < compare.quility)
            {
                return -1;
            }
        }
		return 0;
	}

	static int OnSortScore(sdGameItem item, sdGameItem compare)
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
		return 0;
	}

    static int OnSortEquiped(sdGameItem item, sdGameItem compare)
    {
        sdGameItem equip = sdGameItemMgr.Instance.getEquipItemByPos(item.equipPos);
        if (equip == item)
        {
            return -1;
        }

        if (equip == compare)
        {
            return 1;
        }

        return 0;
    }

    public void SortItem(int sortType)
    {
        RefreshItem(selectType, sortType);
    }

    public void ShowBtnPanel(bool bFlag)
    {
        UIPanel panel = copyItem.transform.parent.GetComponent<UIPanel>();
        Vector3 pos = panel.transform.localPosition;
        pos.y = 126;
        panel.transform.localPosition = pos;
        //panel.transform.localScale = new Vector3(1, 1, 1);
        Vector4 rect = panel.clipRange;
        if (bFlag)
        {
            rect.x = 0;
            rect.w = 385;
            rect.y = -138;
        }
        else
        {
            rect.x = 0;
            rect.w = 455;
            rect.y = -170;
        }
        panel.clipRange = rect;
        panel.GetComponent<UIDraggablePanel>().ResetPosition();
        arrow.SetActive(!bFlag);
        btnPanel.SetActive(bFlag);
    }

	void RefreshItem(SelectType selType, int SortType)
	{
        selectType = selType;
		if (copyItem != null) 
		{
            copyItem.SetActive(false);
			UIDraggablePanel panel = copyItem.transform.parent.GetComponent<UIDraggablePanel>();
            if (panel != null)
            {
                panel.ResetPosition();
            }

			if (selType == SelectType.ItemUp)
			{
				itemTable = sdGameItemMgr.Instance.GetEquipItemsInBag(false);
                if (sdGameItemMgr.Instance.upItem != null)
                {
                    itemTable.Remove(sdGameItemMgr.Instance.upItem);
                }
                ShowBtnPanel(true);
			}
            else if (selectType == SelectType.ItemUpChange)
            {
                itemTable = sdGameItemMgr.Instance.GetAllEquipItems();
                if (sdGameItemMgr.Instance.upItem != null)
                {
                    itemTable.Remove(sdGameItemMgr.Instance.upItem);
                }
                ShowBtnPanel(false);
            }
            else if (selectType == SelectType.ItemMake)
            {
                itemTable = sdGameItemMgr.Instance.GetAllFormulaItems();
                if (sdGameItemMgr.Instance.upItem != null)
                {
                    itemTable.Remove(sdGameItemMgr.Instance.upItem);
                }
                ShowBtnPanel(false);
            }
            else if (selectType == SelectType.GemOn)
            {
                itemTable = sdGameItemMgr.Instance.GetGemOnInBag();
                ShowBtnPanel(false);
            }
            else if (selectType == SelectType.ItemSell)
            {
                itemTable = sdGameItemMgr.Instance.GetEquipByQuilityInBag(sellType, false);
                ShowBtnPanel(true);
            }
            else if (selectType == SelectType.EquipSelect)
            {
                itemTable = sdGameItemMgr.Instance.GetBagItemByEquipPos(equipPos, true);
                ShowBtnPanel(false);
            }
            else
			{
                ShowBtnPanel(false);
                if (sdGameItemMgr.Instance.selGemList.Count == 0)
                {
                    itemTable = sdGameItemMgr.Instance.GetGemInBag(0);
                }
                else
                {
                    IEnumerator itr = sdGameItemMgr.Instance.selGemList.GetEnumerator();
                    if (itr.MoveNext())
                    {
                        KeyValuePair<string, int> key = (KeyValuePair<string, int>)itr.Current;
                        sdGameItem gem = sdGameItemMgr.Instance.getItem(ulong.Parse(key.Key));
                        if (gem != null)
                        {
                            itemTable = sdGameItemMgr.Instance.GetGemInBag(gem.level);
                        }
                    }

                    itr.Reset();
                    while (itr.MoveNext())
                    {
                        KeyValuePair<string, int> key = (KeyValuePair<string, int>)itr.Current;
                        int num = key.Value;
                        for (int i = 0; i < num; ++i)
                        {
                            foreach (sdGameItem gem in itemTable)
                            {
                                if (gem.instanceID.ToString() == key.Key)
                                {
                                    if (gem.count > 1)
                                    {
                                        gem.count--;
                                    }
                                    else
                                    {
                                        itemTable.Remove(gem);
                                    }
                                    
                                    break;
                                }
                            }
                        }
                    }
                }
			}

			Hashtable list = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
			if (itemTable.Count > list.Count)
			{
				if (copyItem == null)
				{
					Debug.LogWarning("NoCopyItem");	
				}
				//copyItem.SetActive(true);
				for(int i = list.Count; i < itemTable.Count; ++i)
				{
					GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;	
					tempItem.transform.parent = copyItem.transform.parent;
					Vector3 pos = copyItem.transform.localPosition;
					if (i % 2 == 1)
					{
						pos.x = 260;
					}
					pos.y -= (i/2)*120;
					tempItem.transform.localPosition = pos;
					tempItem.transform.localScale = copyItem.transform.localScale;
					sdSlotIcon icon = tempItem.GetComponent<sdSlotIcon>();

					if (icon != null)
					{
						icon.index = i;
						list.Add(i, icon);
					}
                    icon.gameObject.SetActive(false);
				}
			}
			else if (itemTable.Count < list.Count)
			{
				foreach(DictionaryEntry item in list)
				{
					sdSlotIcon icon = item.Value as sdSlotIcon;
					
					if (icon != null && icon.index >= itemTable.Count)
					{
						if (icon.index == 0)
						{
							copyItem.SetActive(false);
						}
						else
						{
							icon.gameObject.SetActive(false);
						}
					}
				}
			}

            if (panel != null)
            {
                panel.ResetPosition();
            }
			IDictionaryEnumerator iter = list.GetEnumerator();

			if (itemTable != null)
			{
				itemTable.Sort();

                if (selectType == SelectType.ItemUpChange)
                {
                    itemTable.Sort(OnSortEquiped);
                }
                else
                {
                    if (SortType == 0)
                    {
                        itemTable.Sort(OnSortQuility);
                    }
                    else if (SortType == 1)
                    {
                        itemTable.Sort(OnSortLevel);
                    }
                    else
                    {
                        itemTable.Sort(OnSortScore);
                    }
                }
				
				foreach(sdGameItem info in itemTable)
				{
					if (!iter.MoveNext()) break;
					//sdGameItem info = (sdGameItem)item.Value;
					//if (info.bagIndex != (int)PanelType.Panel_Bag) continue;
					//if (info.slotIndex != num) continue;
					Hashtable table = new Hashtable();
					sdSlotIcon icon = (sdSlotIcon)iter.Value;	
					table.Add("uuid", info.instanceID);
					table.Add("ID", info.templateID);
					table.Add("count", info.count);
                    bool isSelect = icon.isActiveAndEnabled && icon.isSelected;
					icon.SetInfo(info.instanceID.ToString(), table);
                    if (selType == SelectType.ItemSell)
                    {
                        if (!icon.gameObject.active)
                        {
                            icon.SetSelect(true);
                        }
                    }
					icon.gameObject.SetActive(true);
                    if (selType == SelectType.ItemSell)
                    {
                        if (isSelect)
                        {
                            icon.SetSelect(true);
                        }
                    }
					if (selectType == SelectType.ItemUp)
					{
						icon.SetSelect(sdGameItemMgr.Instance.hasSelectItemUp(info.instanceID.ToString()));
                        if (icon.isSelected)
                        {
                            AddSelectItem(info.instanceID.ToString());
                        }
                        
					}
				}
			}
		}
	}
}
	
	
	