using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public enum JiesuanSlotType
{
	Item = 0,
	Pet,
	PetItem
}

public class sdSlotIcon : MonoBehaviour
{
	public int index = 0; 
	public PanelType panel = 0;
	public string tempId = "";
	
	public string itemid = "";
	public Hashtable itemInfo;
	
	GameObject pointBack = null;
	GameObject isLearn = null;
	GameObject isLock = null;
	
	public bool enable = true;
	public bool isSelected = false;
	
	bool hasAtlas = false;
	
	int iconId = -1;
	
	public JiesuanSlotType jiesuanType = JiesuanSlotType.Item;

	public List<EventDelegate> onLoad = new List<EventDelegate>();

	void Update()
	{
		if (!hasAtlas)
		{
			if ((panel == PanelType.Panel_Skill_Active || panel == PanelType.Panel_Skill_Passive))	
			{
				if (sdConfDataMgr.Instance().skilliconAtlas != null)
				{
					Transform icon = transform.FindChild("icon");
					icon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().skilliconAtlas;
					hasAtlas = true;
					EventDelegate.Execute(onLoad);
					onLoad.Clear();
				}
			}
		}

        if (panel == PanelType.Panel_Equip && itemInfo == null)
        {
            List<sdGameItem> list = sdGameItemMgr.Instance.GetBagItemByEquipPos(index, true);
            if (gameObject.transform.FindChild("canequip") != null)
            {
                if (list.Count > 0)
                {
                    gameObject.transform.FindChild("canequip").GetComponent<UILabel>().text = sdConfDataMgr.Instance().GetShowStr("CanEquip");
                }
                else
                {
                    gameObject.transform.FindChild("canequip").GetComponent<UILabel>().text = "";
                }
            }

        }
	}

	void OnGuideClick()
	{
		sdUICharacter.Instance.ShowTip(TipType.Item, itemid);
	}

    public void OnSelectGemReplace()
    {
        Dictionary<string, int> select = sdUICharacter.Instance.GetSelectList();
        if (select != null)
        {
            IEnumerator itr = select.GetEnumerator();
            if (itr.MoveNext())
            {
                if (itemid != "" && itemid != "0")
                {
                    sdGameItemMgr.Instance.selGemList[itemid]--;
                }

                KeyValuePair<string, int> key = (KeyValuePair<string, int>)itr.Current;
                string id = key.Key.ToString();
                if (sdGameItemMgr.Instance.selGemList.ContainsKey(id))
                {
                    sdGameItemMgr.Instance.selGemList[id]++;
                }
                else
                {
                    sdGameItemMgr.Instance.selGemList.Add(id, 1);
                }
                sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
                Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(info["Level"].ToString());
                sdUICharacter.Instance.SetItemUpWndMoney(gemInfo["HoleRefreshMoney"].ToString());
                SetInfo(id, info);
            }
            else
            {
                SetInfo("", null);
            }
        }
    }

    public void SelectGemMerge()
    {
        Dictionary<string, int> select = sdUICharacter.Instance.GetSelectList();
        if (select != null)
        {
            IEnumerator itr = select.GetEnumerator();
            if (itr.MoveNext())
            {
                if (itemid != "" && itemid != "0")
                {
                    sdGameItemMgr.Instance.selGemList[itemid]--;
                }
                KeyValuePair<string, int> key = (KeyValuePair<string, int>)itr.Current;
                string id = key.Key.ToString();
                if (sdGameItemMgr.Instance.selGemList.ContainsKey(id))
                {
                    sdGameItemMgr.Instance.selGemList[id]++;
                }
                else
                {
                    sdGameItemMgr.Instance.selGemList.Add(id, 1);
                }
                sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
                Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
                Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(info["Level"].ToString());
                int moeny = int.Parse(gemInfo["HoleMergeMoney"].ToString());
                sdUICharacter.Instance.SetGemMergeMoney(moeny.ToString(), (moeny*item.count/3).ToString());
                SetInfo(id, info);
            }
            else
            {
                SetInfo("", null);
            }
        }
    }

    void OnGemOff()
    {
        sdItemMsg.notifyGemOff(index, sdGameItemMgr.Instance.upItem.instanceID.ToString());
    }

	void OnClick()
    {
        if (panel == PanelType.Panel_ItemUp)
        {
            if (index == 0)
            {
                sdUICharacter.Instance.ShowItemSelectWnd(SelectType.ItemUpChange);
                sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.OnChangeUpItem));
                return;
            }
            else if (index == -1)
            {
                return;
            }

            if (sdGameItemMgr.Instance.upItem == null)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SelectUpItem"), Color.yellow);
                return;
            }

            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.ItemUp);
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.GetItemUpWnd().OnSelectItemOk));
            return;
        }
        else if (panel == PanelType.Panel_ItemMake)
        {
            if (index == 0)
            {
                sdUICharacter.Instance.ShowItemSelectWnd(SelectType.ItemMake);
                sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.GetItemUpWnd().SelectItemMake));
                return;
            }
            else
            {
                sdUICharacter.Instance.ShowTip(TipType.TempItem, tempId);
                return;
            }

        }
        else if (panel == PanelType.Panel_GemReplace)
        {
            if (itemid != "0" && itemid != "")
            {
                if (sdGameItemMgr.Instance.selGemList.ContainsKey(itemid))
                {
                    int num = sdGameItemMgr.Instance.selGemList[itemid];
                    if (num == 1)
                    {
                        sdGameItemMgr.Instance.selGemList.Remove(itemid);
                    }
                    else
                    {
                        sdGameItemMgr.Instance.selGemList[itemid]--;
                    }
                }
                SetInfo("0", null);
                return;
            }

            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.GemReplace);
            //sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.GetItemUpWnd().OnSelectGemReplace));
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(OnSelectGemReplace));
            return;
        }
        else if (panel == PanelType.Panel_GemMerge)
        {
            if (index > 2) return;
            if (itemid != "0" && itemid != "")
            {
                if (sdGameItemMgr.Instance.selGemList.ContainsKey(itemid))
                {
                    int num = sdGameItemMgr.Instance.selGemList[itemid];
                    if (num == 1)
                    {
                        sdGameItemMgr.Instance.selGemList.Remove(itemid);
                    }
                    else
                    {
                        sdGameItemMgr.Instance.selGemList[itemid]--;
                    }
                }
                SetInfo("0", null);
                return;
            }
            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.GemMerge);
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(SelectGemMerge));
            return;
        }
        else if (panel == PanelType.Panel_GemSet)
        {
            if (index > 2) return;
            if (sdGameItemMgr.Instance.upItem == null)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SelectGemItem"), Color.yellow);
                return;
            }
            if (itemid == "-1")
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("GemOnError1"), Color.yellow);
                return;
            }
            if (itemid != "0" && itemid != "")
            {
                //sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(itemid));
                Hashtable info = sdConfDataMgr.Instance().GetItemById(tempId);
                Hashtable gemInfo = sdConfDataMgr.Instance().GetGemLevel(info["Level"].ToString());
                string needMoney = gemInfo["HoleOffMoney"].ToString();
                sdMsgBox.OnConfirm ok = new sdMsgBox.OnConfirm(OnGemOff);
                sdUICharacter.Instance.ShowOkCanelMsg(string.Format(sdConfDataMgr.Instance().GetShowStr("GemOffConfirm"), needMoney), ok, null);
                return;
            }
            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.GemOn);
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.GetItemUpWnd().SelectGemSet));
            sdGameItemMgr.Instance.gemIndex = index;
            return;
        }
        else if (panel == PanelType.Panel_PvpReward || panel == PanelType.Panel_PVPRankReward)
        {
            if (itemid == "100" || itemid == "101" || itemid == "200" || itemid.Length == 0)
                return;
            Hashtable info = sdConfDataMgr.Instance().GetItemById(itemid);
            if (info != null  && info.ContainsKey("Class") && int.Parse(info["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
            {
                sdUIPetControl.Instance.ActivePetSmallTip(null, int.Parse(itemInfo["Expend"].ToString()), 0, 1);
                return;
            }
            else
            {
                sdUICharacter.Instance.ShowTip(TipType.TempItem, tempId);
                return;
            }
        }

		if (itemid == "") 
		{
			if (panel == PanelType.Panel_Equip)
			{
                if (sdGameItemMgr.Instance.GetBagItemByEquipPos(index, true).Count == 0)
                {
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NoEquip"), Color.yellow);
                    return;
                }
                //sdUICharacter.Instance.ShowBag();
                sdUICharacter.Instance.SetSelectWndNeedPos(index);
                sdUICharacter.Instance.ShowItemSelectWnd(SelectType.EquipSelect);
                sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.OnChangeEquip));			
                //sdSlotMgr.Instance.GotoEquip(index);
			}
			return;
		}

		if (!enable) return;
		
		if (panel == PanelType.Panel_Skill_Active || panel == PanelType.Panel_Skill_Passive)
		{	
			Hashtable item = sdConfDataMgr.Instance().GetSkill(itemid);
			sdGameSkill skill = sdGameSkillMgr.Instance.GetSkill(int.Parse(itemid));
			TipType type = (panel == PanelType.Panel_Skill_Active)?TipType.Skill:TipType.PassiveSkill;
			sdUICharacter.Instance.ShowTip(type, itemid);
			return;
			
		}
		
		if (panel == PanelType.Panel_Treasure)
		{
            return;
			sdUICharacter.Instance.ShowTip(TipType.TempItem, itemid);	
			return;
		}
		
		if (panel == PanelType.Panel_Jiesuan )
		{
			if (jiesuanType == JiesuanSlotType.Pet)
			{
				Hashtable info = sdConfDataMgr.Instance().GetItemById(itemid);
                if (info == null) info = itemInfo;
                if (info != null)
                {
                    sdUIPetControl.Instance.ActivePetSmallTip(null, int.Parse(itemInfo["Expend"].ToString()), 0, 1);
                }

				return;	
			}
			else if (jiesuanType == JiesuanSlotType.PetItem)
			{
                sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(ulong.Parse(itemid));
                if (petItem != null)
                {
                    sdUIPetControl.Instance.ActivePetEquipTip(null, petItem.templateID);
                }
                
				return;
			}
			
		}

		if (panel == PanelType.Panel_ItemSelect)
		{
			if (!isSelected)
			{
				if (sdUICharacter.Instance.AddSelectItem(itemid))
				{
					isSelected = true;
                    if (gameObject.transform.FindChild("bgselect") != null)
					{
                        gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "g";
					}
				}
			}
			else
			{
				isSelected = false;
                if (gameObject.transform.FindChild("bgselect") != null)
				{
                    gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "";
				}

				sdUICharacter.Instance.RemoveSelectItem(itemid);
			}
			return;
		}
		
        if (panel == PanelType.Panel_Bag)
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(itemid));
            if (item != null) item.isNew = false;
            SetNew(false);
            sdRoleWnd wnd = sdUICharacter.Instance.GetRoleWnd().GetComponent<sdRoleWnd>();
            if (wnd != null)
            {
                if (wnd.otherBtn.isActive)
                {
                    bool hasNew = false;
                    Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Other);
                    foreach(DictionaryEntry info in itemTable)
                    {
                        sdGameItem temp = info.Value as sdGameItem;
                        if (temp.isNew) 
                        {
                            hasNew = true;
                            break;
                        }
                    }

                    if (!hasNew)
                    {
                        wnd.otherBtn.HideRedTip();
                    }
                }

                if (wnd.armorBtn.isActive)
                {
                    bool hasNew = false;
                    Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Armor);
                    foreach(DictionaryEntry info in itemTable)
                    {
                        sdGameItem temp = info.Value as sdGameItem;
                        if (temp.isNew) 
                        {
                            hasNew = true;
                            break;
                        }
                    }

                    if (!hasNew)
                    {
                        wnd.armorBtn.HideRedTip();
                    }
                }

                if (wnd.weaponBtn.isActive)
                {
                    bool hasNew = false;
                    Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Weapon);
                    foreach (DictionaryEntry info in itemTable)
                    {
                        sdGameItem temp = info.Value as sdGameItem;
                        if (temp.isNew)
                        {
                            hasNew = true;
                            break;
                        }
                    }

                    if (!hasNew)
                    {
                        wnd.weaponBtn.HideRedTip();
                    }
                }

                if (wnd.shipinBtn.isActive)
                {
                    bool hasNew = false;
                    Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Bag, (int)ItemFilter.Shipin);
                    foreach (DictionaryEntry info in itemTable)
                    {
                        sdGameItem temp = info.Value as sdGameItem;
                        if (temp.isNew)
                        {
                            hasNew = true;
                            break;
                        }
                    }

                    if (!hasNew)
                    {
                        wnd.shipinBtn.HideRedTip();
                    }
                }
            }
        }

		sdUICharacter.Instance.ShowTip(TipType.Item, itemid);
	}

	public void SetSelect(bool flag)
	{
		isSelected = flag;
		if (flag)
		{
            if (gameObject.transform.FindChild("bgselect") != null)
			{
                gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "g";
			}
		}
		else
		{
            if (gameObject.transform.FindChild("bgselect") != null)
			{
                gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "";
			}
		}
	}

	void Awake()
	{
		if (sdSlotMgr.Instance)
		{
			sdSlotMgr.Instance.RegisterSlot(this);
		}
		
		if (gameObject.transform.FindChild("label_back") != null)
		{
			pointBack = gameObject.transform.FindChild("label_back").gameObject;
		}
		
		if (gameObject.transform.FindChild("islearn") != null)
		{
			isLearn = gameObject.transform.FindChild("islearn").gameObject;
		}
		
		if (gameObject.transform.FindChild("state") != null)
		{
			isLock = gameObject.transform.FindChild("state").gameObject;	
		}
	}
	
	public void SetMax(bool flag)
	{
        if (flag)
        {

        }
        else
        {

        }	
	}
	
	public void SetHighLight(bool flag)
	{
       
        UISprite back = gameObject.transform.FindChild("bg").GetComponent<UISprite>();
        if (flag)
        {
            back.spriteName = "icon_bg3";
        }
        else
        {
            back.spriteName = "icon_bg";
        }
        
	}

	public void ShowLock()
	{
		if (isLock != null)
		{
			isLock.SetActive(true);	
		}

        UISprite icon = gameObject.transform.FindChild("icon").GetComponent<UISprite>();
        icon.alpha = 0.5f;
        UISprite back = gameObject.transform.FindChild("bg").GetComponent<UISprite>();
        back.alpha = 0.5f;

        Transform jindu = gameObject.transform.FindChild("jindu");
        if (jindu != null)
        {
            jindu.GetComponent<UISprite>().spriteName = "jt2";
        }
	}
	
	public void HideLock()
	{
		if (isLock != null)
		{
			isLock.SetActive(false);	
		}

        UISprite icon = gameObject.transform.FindChild("icon").GetComponent<UISprite>();
        icon.alpha = 1;
        UISprite back = gameObject.transform.FindChild("bg").GetComponent<UISprite>();
        back.alpha = 1;

        Transform jindu = gameObject.transform.FindChild("jindu");
        if (jindu != null)
        {
            jindu.GetComponent<UISprite>().spriteName = "jt";
        }
        
	}
	
	void OnDestroy()
	{
		if (sdSlotMgr.Instance)
		{
			sdSlotMgr.Instance.RemoveSlot(this);	
		}
	}
	
	public void SetNew(bool flag)
	{
		if (flag)
		{
			if (gameObject.transform.FindChild("new") != null)
			{
				gameObject.transform.FindChild("new").GetComponent<UISprite>().spriteName = "new";		
			}	
		}
		else
		{
			if (gameObject.transform.FindChild("new") != null)
			{
				gameObject.transform.FindChild("new").GetComponent<UISprite>().spriteName = "";		
			}	
		}
	}

    void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
	{
//		gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().GetItemAtlas(iconId.ToString());
//		if (panel == PanelType.Panel_Bag || panel == PanelType.Panel_Equip 
//		    || panel == PanelType.Panel_Jiesuan || panel == PanelType.Panel_Treasure 
//		    || panel == PanelType.Panel_ItemUp || panel == PanelType.Panel_ItemSelect
//		    || panel == PanelType.Panel_PVPRankReward)
//		{
		if (iconId >= 0)
		{
			UIAtlas atlas = obj as UIAtlas;
			if (atlas != null)
			{
				gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = atlas;
				hasAtlas = true;
				EventDelegate.Execute(onLoad);
				onLoad.Clear();
			}
		}
		//}
	}

    void OnSetPetAtlas()
    {
        gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;	
    }
	
	public void SetInfo(string id, Hashtable info)
	{
		isSelected = false;
		if (gameObject.transform.FindChild("active") != null)
		{
			gameObject.transform.FindChild("active").GetComponent<UISprite>().spriteName = "";		
		}

		itemInfo = info;	
		itemid = id;

        if (panel == PanelType.Panel_ItemSelect)
        {
            if (!isSelected)
            {
                if (gameObject.transform.FindChild("bgselect") != null)
                {
                    gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "";
                }
            }
            else
            {
                if (gameObject.transform.FindChild("bgselect") != null)
                {
                    gameObject.transform.FindChild("bgselect").GetComponent<UISprite>().spriteName = "g";
                }
            }
        }

		if (info != null)
		{
            if (info.ContainsKey("Class") && int.Parse(info["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
            {
                Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(info["Expend"].ToString());
				gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = petInfo["Icon"].ToString();
                if (sdConfDataMgr.Instance().PetAtlas != null)
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;	
                else
                    sdConfDataMgr.Instance().LoadPetAtlas(new EventDelegate(OnSetPetAtlas));

                if (int.Parse(info["SubClass"].ToString()) == 2 && GetComponent<UISprite>() != null)
                {
                    GetComponent<UISprite>().spriteName = "IconFrame0-cp";
                }

                return;
            }

            if (panel == PanelType.Panel_Equip)
            {
                if (gameObject.transform.FindChild("canequip") != null)
                {
                    gameObject.transform.FindChild("canequip").GetComponent<UILabel>().text = "";
                }
            }

			if (panel == PanelType.Panel_Skill_Active || panel == PanelType.Panel_Skill_Passive)
			{
				gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = info["icon"].ToString();	
//				if(pointBack != null)
//				{
//					pointBack.transform.FindChild("point").GetComponent<UILabel>().text = info["CostSkillPoint"].ToString();
//				}
			}
            else if (panel == PanelType.Panel_ItemMake)
            {
                string tid = info["ID"].ToString();
                tempId = tid;
                gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = info["IconPath"].ToString();
                iconId = int.Parse(info["IconID"].ToString());
                sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
                if (gameObject.transform.FindChild("name") != null)
                {
                    gameObject.transform.FindChild("name").GetComponent<UILabel>().text = info["ShowName"].ToString();
                }

                if (gameObject.transform.FindChild("count") != null)
                {
                    gameObject.transform.FindChild("count").GetComponent<UILabel>().text = info["TempCount"].ToString();
                }
                
            }
            else if (panel == PanelType.Panel_Jiesuan || panel == PanelType.Panel_Treasure || (panel == PanelType.Panel_ItemUp && index != -1))
            {
                string tid = info["ID"].ToString();
                tempId = tid;
                //if(panel != PanelType.Panel_ItemUp)GetComponent<UISprite>().spriteName = "bg_icon2";

                if (gameObject.transform.FindChild("iconbg") != null)
                {
                    gameObject.transform.FindChild("iconbg").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(info["Quility"].ToString()));
                }

                if (jiesuanType == JiesuanSlotType.Pet)
                {
                    Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(info["Expend"].ToString());
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = petInfo["Icon"].ToString();
                    if (sdConfDataMgr.Instance().PetAtlas != null)
                        gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
                    else
                        sdConfDataMgr.Instance().LoadPetAtlas(new EventDelegate(OnSetPetAtlas));

                    if (int.Parse(info["SubClass"].ToString()) == 2)
                    {
                        GetComponent<UISprite>().spriteName = "IconFrame0-cp";
                    }

                }
                else
                {
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = info["IconPath"].ToString();
                    iconId = int.Parse(info["IconID"].ToString());
                    sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
                }
            }
            else
            {
                if (gameObject.transform.FindChild("bg") != null)
                {
                    gameObject.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "List_bg";
                }
                string tid = info["ID"].ToString();
                tempId = tid;
                if (id == "100") //½ð±Òaa
                {
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = "icon_money";
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().commonAtlas;
                }
                else if (id == "101") //Ñ«ÕÂaa
                {
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = "icon_noncash";
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().commonAtlas;
                }
                else if (id == "200") //ÌåÁ¦aaa
                {
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = "xin";
                    gameObject.transform.FindChild("icon").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().commonAtlas;
                }
                else
                {
                    Hashtable target = sdConfDataMgr.Instance().GetItemById(tid);
                    if (target != null)
                    {

                        if (panel == PanelType.Panel_GemSet || (panel == PanelType.Panel_ItemUp && index == -1))
                        {
                            if (gameObject.transform.FindChild("bgx") != null)
                            {
                                gameObject.transform.FindChild("bgx").GetComponent<UISprite>().spriteName = "bg-xq";
                            }

                            if (gameObject.transform.FindChild("name") != null)
                            {
                                string name = target["ShowName"].ToString();
                                gameObject.transform.FindChild("name").GetComponent<UILabel>().text = name;
                            }

                            if (gameObject.transform.FindChild("att") != null)
                            {
                                Hashtable pro = sdConfDataMgr.Instance().GetProperty(tid);
                                foreach (DictionaryEntry gemInfo in pro)
                                {
                                    gameObject.transform.FindChild("att").GetComponent<UILabel>().text = string.Format("{0} +{1}", gemInfo.Key.ToString(), gemInfo.Value.ToString());
                                }
                            }
                        }

                        bool isGem = false;
                        if (target["Class"].ToString() == "51" && target["SubClass"].ToString() == "1")
                        {
                            isGem = true;
                        }

                        if (gameObject.transform.FindChild("icon") != null)
                        {
                            gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = target["IconPath"].ToString();
                            iconId = int.Parse(target["IconID"].ToString());
                            sdConfDataMgr.Instance().LoadItemAtlas(iconId.ToString(), OnSetAtlas);
                        }

                        if (gameObject.transform.FindChild("active") != null)
                        {
                            gameObject.transform.FindChild("active").GetComponent<UISprite>().spriteName = "";
                        }

                        if (gameObject.transform.FindChild("bg") != null)
                        {
                            gameObject.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "List_bg";
                        }

                        if (gameObject.transform.FindChild("lb_price") != null)
                        {
                            string jobnam = string.Format("{0}:{1}", sdConfDataMgr.Instance().GetShowStr("Price"), target["Value"].ToString());
                            gameObject.transform.FindChild("lb_price").GetComponent<UILabel>().text = jobnam;
                        }

                        sdGameItem tempItem = sdGameItemMgr.Instance.getItem(ulong.Parse(id));
                        if (tempItem != null)
                        {
                            SetNew(tempItem.isNew);
                            if (gameObject.transform.FindChild("count") != null)
                            {
                                gameObject.transform.FindChild("count").GetComponent<UILabel>().text = sdGameItemMgr.Instance.GetItemCount(int.Parse(tempId)).ToString();
                            }
                        }

                        if (gameObject.transform.FindChild("lb_lv") != null)
                        {
                            int itemLevel = 0;
                            if (tempItem != null)
                            {
                                itemLevel = tempItem.level;
                            }
                            else
                            {
                                itemLevel = int.Parse(target["NeedLevel"].ToString());
                            }
                            if (sdGameLevel.instance.mainChar["Level"] < itemLevel && !isGem)
                            {
                                gameObject.transform.FindChild("lb_lv").GetComponent<UILabel>().color = Color.red;
                            }
                            else
                            {
                                gameObject.transform.FindChild("lb_lv").GetComponent<UILabel>().color = new Color(128, 128, 128, 255);
                            }
                            string level = string.Format("{0}:{1}", sdConfDataMgr.Instance().GetShowStr("Level"), itemLevel);
                            gameObject.transform.FindChild("lb_lv").GetComponent<UILabel>().text = level;
                        }

                        if (gameObject.transform.FindChild("lb_name") != null)
                        {
                            string strName = target["ShowName"].ToString();
                            if (tempItem != null)
                            {
                                if (tempItem.upLevel > 0)
                                {
                                    strName += string.Format("[{0}]+{1}", sdConfDataMgr.Instance().GetColorHex(Color.yellow), tempItem.upLevel);
                                }
                            }
                            gameObject.transform.FindChild("lb_name").GetComponent<UILabel>().color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(target["Quility"].ToString()));
                            gameObject.transform.FindChild("lb_name").GetComponent<UILabel>().text = strName;
                        }

                        if (gameObject.transform.FindChild("iconbg") != null)
                        {
                            gameObject.transform.FindChild("iconbg").GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(target["Quility"].ToString()));
                        }

                        if (gameObject.transform.FindChild("lb_type") != null)
                        {
                            if (target["Class"].ToString() == "51" && target["SubClass"].ToString() == "1")
                            {
                                string partname = sdConfDataMgr.Instance().GetGemEquipPosName(target["HolePos"].ToString());
                                gameObject.transform.FindChild("lb_type").GetComponent<UILabel>().text =
                                    string.Format("{0}:{1}", sdConfDataMgr.Instance().GetShowStr("GemPart"), partname);
                            }
                            else
                            {
                                string partname = sdConfDataMgr.Instance().GetItemClassName(target["Class"].ToString(), target["SubClass"].ToString());
                                if (partname != "")
                                {
                                    gameObject.transform.FindChild("lb_type").GetComponent<UILabel>().text =
                                    string.Format("{0}:{1}", sdConfDataMgr.Instance().GetShowStr("Part"), partname);
                                }
                                else
                                {
                                    gameObject.transform.FindChild("lb_type").GetComponent<UILabel>().text = "";

                                }
                            }
                        }

                        if (tempItem != null && transform.FindChild("lb_count") != null)
                        {
                            sdGameItem gi = tempItem.Clone();
                            if (panel == PanelType.Panel_ItemSelect)
                            {
                                IEnumerator itr = sdGameItemMgr.Instance.selGemList.GetEnumerator();
                                while (itr.MoveNext())
                                {
                                    KeyValuePair<string, int> key = (KeyValuePair<string, int>)itr.Current;
                                    int num = key.Value;
                                    for (int i = 0; i < num; ++i)
                                    {
                                        if (gi.instanceID.ToString() == key.Key)
                                        {
                                            if (gi.count > 0)
                                            {
                                                gi.count--;
                                            }
                                        }
                                    }
                                }
                            }

                            if (gi.count <= 1)
                            {
                                transform.FindChild("lb_count").GetComponent<UILabel>().text = "";
                            }
                            else
                            {
                                transform.FindChild("lb_count").GetComponent<UILabel>().text = gi.count.ToString();
                            }
                        }

                        int score = 0;
                        if (tempItem == null)
                        {
                            score = sdConfDataMgr.Instance().GetItemScore(tid, 0);
                        }
                        else
                        {
                            score = sdConfDataMgr.Instance().GetItemScore(tempItem.instanceID);
                        }
                        if (gameObject.transform.FindChild("lb_point") != null)
                        {
                            string txt = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("Score"), score.ToString());
                            if (isGem || score == 0)
                            {
                                gameObject.transform.FindChild("lb_point").GetComponent<UILabel>().text = "";
                            }
                            else
                            {
                                gameObject.transform.FindChild("lb_point").GetComponent<UILabel>().text = txt;
                            }
                        }

                        sdGameItem item = sdGameItemMgr.Instance.getEquipItemByPos(int.Parse(target["Character"].ToString()));
                        Transform itemChangeFlag = gameObject.transform.FindChild("Lb_defence_row");
                        Transform itemChangNum = gameObject.transform.FindChild("Lb_defence_diff");
                        if (itemChangeFlag == null || itemChangNum == null) return;

                        if (isGem || score == 0)
                        {
                            itemChangeFlag.GetComponent<UISprite>().spriteName = "";
                            itemChangNum.GetComponent<UILabel>().text = "";
                        }
                        else
                        {
                            if (item == null)
                            {
                                itemChangeFlag.GetComponent<UISprite>().spriteName = "up2";
                                itemChangNum.GetComponent<UILabel>().color = Color.green;
                                itemChangNum.GetComponent<UILabel>().text = score.ToString();
                            }
                            else
                            {
                                int comScore = sdConfDataMgr.Instance().GetItemScore(item.instanceID);

                                int changeValue = score - comScore;

                                if (changeValue != 0)
                                {
                                    if (changeValue > 0)
                                    {
                                        itemChangeFlag.GetComponent<UISprite>().spriteName = "up2";
                                        itemChangNum.GetComponent<UILabel>().color = Color.green;
                                    }
                                    else
                                    {
                                        itemChangeFlag.GetComponent<UISprite>().spriteName = "down2";
                                        itemChangNum.GetComponent<UILabel>().color = Color.red;
                                    }

                                    itemChangNum.GetComponent<UILabel>().text = Math.Abs(changeValue).ToString();
                                }
                                else
                                {
                                    itemChangNum.GetComponent<UILabel>().text = "";
                                    itemChangeFlag.GetComponent<UISprite>().spriteName = "";
                                }
                            }
                        }

                    }
                }
            }
			
			return;
		}
		else
		{
			gameObject.transform.FindChild("icon").GetComponent<UISprite>().spriteName = "";	
			if (panel == PanelType.Panel_Jiesuan || panel == PanelType.Panel_Treasure)
			{
				GetComponent<UISprite>().spriteName = "";	
			}

            if (gameObject.transform.FindChild("iconbg") != null)
            {
                if (panel == PanelType.Panel_Equip || panel == PanelType.Panel_Jiesuan)
                {
                    gameObject.transform.FindChild("iconbg").GetComponent<UISprite>().spriteName = "";
                }
            }

            if (gameObject.transform.FindChild("count") != null)
            {
                gameObject.transform.FindChild("count").GetComponent<UILabel>().text = "";
            }

            if (gameObject.transform.FindChild("name") != null)
            {
                gameObject.transform.FindChild("name").GetComponent<UILabel>().text = "";
            }

            if (panel == PanelType.Panel_GemSet || (panel == PanelType.Panel_ItemUp && index == -1))
            {
                if (gameObject.transform.FindChild("bgx") != null)
                {
                    gameObject.transform.FindChild("bgx").GetComponent<UISprite>().spriteName = "";
                }

                if (gameObject.transform.FindChild("name") != null)
                {
                    gameObject.transform.FindChild("name").GetComponent<UILabel>().text = "";
                }

                if (gameObject.transform.FindChild("att") != null)
                {
                    gameObject.transform.FindChild("att").GetComponent<UILabel>().text = "";
                }
            }
		}
	}
}