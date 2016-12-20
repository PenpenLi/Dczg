using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class sdRoleWndButton : MonoBehaviour
{	
	public bool		mSystemLock		= false;
	public string	mSystemLockInfo = null;

	public static float sysPanelPos;
	private static AudioSource asWin = null;
	
	void Enabled()
	{
		
	}

	void OnSell()
	{
		sdUICharacter.Instance.HideTip();
		sdItemMsg.notifyProcessItem(sdUICharacter.Instance.lastTipId, (int)HeaderProto.ERoleItemEvent.ROLE_ITEM_EVENT_SELL);
		//sdItemMsg.notifyMoveItem((byte)HeaderProto.EItemPos.ItemPos_Bag, (byte)HeaderProto.EItemPos.ItemPos_None, sdUICharacter.Instance.lastTipId);
        sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SellSuccess"), Color.yellow);
    }
	
	void OnSellAll()
	{
        Hashtable iconList = sdSlotMgr.Instance.GetIconList(PanelType.Panel_ItemSelect);
        foreach (DictionaryEntry info in iconList)
        {
            sdSlotIcon icon = info.Value as sdSlotIcon;
            if (icon.gameObject.active && icon.isSelected)
            {
                sdItemMsg.notifyProcessItem(icon.itemid.ToString(), (int)HeaderProto.ERoleItemEvent.ROLE_ITEM_EVENT_SELL);
                
            }
        }
        sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SellSuccess"), Color.yellow);
// 		Hashtable sellList = new Hashtable();
// 		Hashtable list = sdSlotMgr.Instance.GetIconList(PanelType.Panel_Bag);
// 		foreach(DictionaryEntry temp in list)
// 		{
// 			sdSlotIcon icon = temp.Value as sdSlotIcon;
// 			if (icon != null && icon.isSelected)
// 			{
// 				sellList.Add(icon.itemid, icon.itemid);
// 			}
// 		}
// 		
// 		foreach(DictionaryEntry item in sellList)
// 		{
// 			sdItemMsg.notifyProcessItem(item.Key.ToString(), (int)HeaderProto.ERoleItemEvent.ROLE_ITEM_EVENT_SELL);
// 		}	
// 		
// 		sdSlotMgr.Instance.selectedItem.Clear();
	}
	
	void OnDelFriend()
	{	
		sdFriendMsg.notifyRemoveFri(sdUICharacter.Instance.GetCurFriId());
        sdUICharacter.Instance.ShowRoleTipWnd(null, false, 0);
	}

    void OnResetSkill()
    {
        int cash = int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["Cash"].ToString()) + int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["NonCash"].ToString());
        if (cash >= 50)
        {
            sdSkillMsg.notifyResetSkill();
        }
        else
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SkillResetNoMoney"));
        }
    }

    void OnItemUp()
    {
        sdItemMsg.notifyItemUp(sdGameItemMgr.Instance.upItem.instanceID.ToString(), sdGameItemMgr.Instance.GetItemUpId());
    }
	
	public List<EventDelegate> onClick = new List<EventDelegate>();


    void OnClick()
    {
		UIButton btn = GetComponent<UIButton>();
		if (btn != null && !btn.enabled) return;
		
		EventDelegate.Execute(onClick);
		onClick.Clear();

		// 系统是否还在锁定状态..
        if (mSystemLock == true)
        {
            if (mSystemLockInfo != null)
                sdUICharacter.Instance.ShowMsgLine(mSystemLockInfo, MSGCOLOR.Yellow);
            return;
        }

        if (gameObject.name == "btn_award_center")
        {
            AwardCenterWnd.Instance.OpenPanel();
        }
        else if (gameObject.name == "bt_systemnotice")
        {
            sdUICharacter.Instance.ShowbbsWnd(true, SDNetGlobal.serverNotice, true, true);
        }
        else if (gameObject.name == "Btn_roletipwnd_addfriend")
        {
            string strName = sdUICharacter.Instance.GetOtherRoleName();
            if (strName != null)
            {
                sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
                if (sdFriendMgr.Instance.IsFriend(strName))
                    sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("Friendalready"), null);
                else if (strName == kPlayerInfo.mRoleName)
                    sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("Addselffriend"), null);
                else
                    sdFriendMsg.notifyAddFri(strName);
            }
        }
        else if (gameObject.name == "Btn_roletipwnd_sendmail")
        {
            //sdMailControl.Instance.ActiveMailWnd(sdUICharacter.Instance.roleTipWnd);
            sdMailControl.Instance.ActiveMailWnd(null);
        }
        else if (gameObject.name == "Btn_selectserverwnd_close")
        {
            sdUICharacter.Instance.ShowSelectSrvWnd(false);
        }
        else if (gameObject.name == "Btn_bbswnd_close")
        {
            sdUICharacter.Instance.ShowbbsWnd(false, "", false, false);
            sdUICharacter.Instance.m_strbbs = "";
        }
        else if (gameObject.name == "sp_grey")
        {
            Transform parent = gameObject.transform.parent;
            if (parent && parent.name == "bbsWnd(Clone)")
            {
                sdUICharacter.Instance.ShowbbsWnd(false, "", false, false);
                sdUICharacter.Instance.m_strbbs = "";
            }
        }
        else if (gameObject.name == "bt_pk")
        {
            sdPVPManager.Instance.RefreshPK();
        }
        else if (gameObject.name == "bt_award")
        {
            sdPVPManager.Instance.RefreshReward();
        }
        else if (gameObject.name == "bt_ranklist")
        {
            sdPVPManager.Instance.RefreshRankList();
            sdPVPMsg.Send_CS_GET_RANK_LIST_REQ();
        }
        else if (gameObject.name == "Button_getrewards")
        {
            if (sdPVPManager.Instance.mMilitaryRewards == false)
                sdPVPMsg.Send_CSID_GET_PVP_MILITARY_REWARD_REQ();
        }
        else if (gameObject.name == "Button_pk1")
        {
            if (sdPVPManager.Instance.nChallenge <= 0)
                sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("PVPTimesError"), null);
            else
            {
                string parentname = gameObject.transform.parent.name;
                int index = int.Parse(parentname.Substring(parentname.Length - 1));
                List<stPVPRival> rivalList = sdPVPManager.Instance.GetRivallist();
                if (index < rivalList.Count)
                    sdPVPMsg.Send_CSID_ENTER_PVP_REQ(rivalList[index].roleID);
            }
        }
        else if (gameObject.name == "Button_titleimage")
        {
            sdUICharacter.Instance.ShowPVPTitle(true);
        }
        else if (gameObject.name == "Button_refresh")
        {
            sdUICharacter.Instance.ShowPVPRefreshWnd(true);
        }
        else if (gameObject.name == "Button_rewards20")
        {
            string parentname = gameObject.transform.parent.name;
            uint index = uint.Parse(parentname.Substring(parentname.Length - 1));
            sdPVPMsg.Send_CS_GET_PVP_REPUTE_REWARD_REQ(index + 1);
        }
        else if (gameObject.name == "Button_addchallenge")
        {
            sdUICharacter.Instance.ShowChallengeBuyWnd(0);
        }
        else if (gameObject.name == "btn_ranklist_pk")
        {
            sdRankLisrMsg.Send_CS_ROLE_RANK_REQ(SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID, (int)HeaderProto.ERankType.RANKTYPE_ATTACK, 0);
        }
        else if (gameObject.name == "btn_ranklist_level")
        {
            sdRankLisrMsg.Send_CS_ROLE_RANK_REQ(SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID, (int)HeaderProto.ERankType.RANKTYPE_LEVEL, 0);
        }
        else if (gameObject.name == "btn_ranklist_pvp")
        {
            sdRankLisrMsg.Send_CS_ROLE_RANK_REQ(SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID, (int)HeaderProto.ERankType.RANKTYPE_PVPWINS, 0);
        }
        else if (gameObject.name == "btn_ranklist_reputation")
        {
            sdRankLisrMsg.Send_CS_ROLE_RANK_REQ(SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID, (int)HeaderProto.ERankType.RANKTYPE_PVPREPUTE, 0);
        }
        else if (gameObject.name == "Btn_Rank")
        {
            sdRankLisrMsg.Send_CS_ROLE_RANK_REQ(SDNetGlobal.playerList[SDNetGlobal.lastSelectRole].mDBID, (int)HeaderProto.ERankType.RANKTYPE_ATTACK, 0);
            sdUICharacter.Instance.ShowRankListWnd(true);
        }
        else if (gameObject.name == "btn_ranklist_close")
        {
            sdUICharacter.Instance.ShowRankListWnd(false);
        }
        else if (gameObject.name == "btn_ranklist_view")
        {
            string parentname = gameObject.transform.parent.name;
            parentname = parentname.Substring(4);
            sdFriend roleInfo = sdRankListMgr.Instance.m_Avatar[int.Parse(parentname)];
            sdUICharacter.Instance.ShowRoleTipWnd(roleInfo, true, 1);
        }
        else if (gameObject.name == "Button_buy")
        {
            string parentName = gameObject.transform.parent.name;
            parentName = parentName.Substring(4);
            sdMysteryShopMgr.Instance.Buy(int.Parse(parentName));
        }
        else if (gameObject.name == "Button_fresh")
        {
            sdMysteryShopMsg.Send_SHOP_SECRET_REFRESH_REQ();
        }
        else if (gameObject.name == "btn_equipTab")
        {
            //			GameObject itemPanel = GameObject.Find("fashionPanel");
            //			if (itemPanel != null)
            //			{
            //				itemPanel.SetActive(false);	
            //			}
        }
        else if (gameObject.name == "tab_fashion")
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"), Color.yellow);
            //			if (equipPanel != null)
            //			{
            //				equipPanel.SetActive(false);	
            //			}
            //			
            //			GameObject itemPanel = GameObject.Find("fashionPanel");
            //			if (itemPanel != null)
            //			{
            //				itemPanel.SetActive(true);	
            //			}
        }
        else if (gameObject.name == "btn_detail")
        {
            sdUICharacter.Instance.ShowDetail();
        }
        else if (gameObject.name == "close_detail")
        {
            sdUICharacter.Instance.HideDetail();
        }
        else if (gameObject.name == "btn_equip")
        {
            if (sdSlotMgr.Instance.selectedItem.Count == 0) return;
            sdSlotIcon icon = sdSlotMgr.Instance.selectedItem[0];
            if (icon == null) return;
            sdItemMsg.notifyEquipItem(icon.itemid, 1);
            sdSlotMgr.Instance.selectedItem.Remove(icon);
        }
        else if (gameObject.name == "btn_tipEquip")
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(sdUICharacter.Instance.lastTipId));
            if (item == null) return;
            string msg = "";
            if (!item.CanEquip(sdGameLevel.instance.mainChar, out msg))
            {
                sdUICharacter.Instance.ShowMsgLine(msg, Color.red);
                return;
            }

            sdUICharacter.Instance.HideTip();
            sdItemMsg.notifyEquipItem(sdUICharacter.Instance.lastTipId, 1);
        }
        else if (gameObject.name == "btn_lock")
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(sdUICharacter.Instance.lastTipId));
            if (item == null) return;
            sdItemMsg.notifyLockItem(sdUICharacter.Instance.lastTipId, item.isLock == true ? 0 : 1);
        }
        else if (gameObject.name == "btn_unequip")
        {
            sdUICharacter.Instance.HideTip();
            sdItemMsg.notifyEquipItem(sdUICharacter.Instance.lastTipId, 0);
        }
        else if (gameObject.name == "close_tip")
        {
            sdUICharacter.Instance.HideTip();
        }
        else if (gameObject.name == "close_role")
        {
            sdUICharacter.Instance.HideRoleWnd();
        }
        else if (gameObject.name == "close_skill")
        {
            sdUICharacter.Instance.HideSkillWnd();
        }
        else if (gameObject.name == "close_skilltip" || gameObject.name == "btn_skillcancel")
        {
            GameObject panel = GameObject.Find("skillTipPanel");
            if (panel)
            {
                panel.SetActive(false);
            }
        }
        else if (gameObject.name == "close_passivetip")
        {
            GameObject panel = GameObject.Find("passiveTipPanel");
            if (panel)
            {
                panel.SetActive(false);
            }
        }
        else if (gameObject.name == "btn_skilllearn")
        {
            Hashtable skill = sdConfDataMgr.Instance().GetSkill(sdUICharacter.Instance.lastTipId);
            if (skill == null) return;
            int requestPoint = int.Parse(skill["dwCostSkillPoint"].ToString());
            if (requestPoint > sdGameSkillMgr.Instance.GetSkillPoint())
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NoSkillPoint"), Color.yellow);
                return;
            }
            sdUICharacter.Instance.HideTip();
            sdSkillMsg.notifyLearnSkill(int.Parse(sdUICharacter.Instance.lastTipId));
        }
        else if (gameObject.name == "btn_passivelearn")
        {
            //sdUICharacter.Instance.HideTip();
            Hashtable skill = sdConfDataMgr.Instance().GetSkill(sdUICharacter.Instance.lastTipId);
            if (skill == null) return;
            int requestPoint = int.Parse(skill["dwCostSkillPoint"].ToString());
            if (requestPoint > sdGameSkillMgr.Instance.GetSkillPoint())
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NoSkillPoint"), Color.yellow);
                return;
            }
            sdSkillMsg.notifyLearnSkill(int.Parse(sdUICharacter.Instance.lastTipId));
        }
        else if (gameObject.name == "btn_baseDetailTab")
        {
            sdUICharacter.Instance.ShowDetailTab(0);
        }
        else if (gameObject.name == "btn_attDetailTab")
        {
            sdUICharacter.Instance.ShowDetailTab(1);
        }
        else if (gameObject.name == "btn_defDetailTab")
        {
            sdUICharacter.Instance.ShowDetailTab(2);
        }
        else if (gameObject.name == "btn_otherDetailTab")
        {
            sdUICharacter.Instance.ShowDetailTab(3);
        }
        else if (gameObject.name == "Btn_Role" || gameObject.name == "ShowPlayer")
        {
            sdUICharacter.Instance.ShowRoleWnd(true);
        }
        else if (gameObject.name == "Btn_Skill")
        {
            sdUICharacter.Instance.ShowSkillWnd();
        }
        else if (gameObject.name == "Btn_Push")
        {
            GameObject panel = GameObject.Find("Sys1");
            if (panel != null)
            {
                TweenPosition tp = panel.GetComponent<TweenPosition>();
                Transform t = sdUICharacter.Instance.GetTownUI().transform.FindChild("bg3");
                TweenAlpha ta = t.GetComponent<TweenAlpha>();
                UISprite sp = gameObject.transform.FindChild("Background").GetComponent<UISprite>();
                if (sp.spriteName == "btn_c")
                {
                    //panel.transform.localPosition = new Vector3(sysPanelPos, panel.transform.localPosition.y, panel.transform.localPosition.z);
                    tp.to = tp.from = panel.transform.localPosition;
                    tp.to.x = sysPanelPos;
                    tp.Reset();
                    tp.PlayForward();
                    EventDelegate.Add(tp.onFinished, onFinished, true);

                    //sp.spriteName = "btn_o";
                    if (t != null)
                    {
                        t.gameObject.SetActive(true);
                        ta.from = 0;
                        ta.to = 1f;
                        ta.Reset();
                        ta.PlayForward();
                    }
                }
                else
                {
                    sysPanelPos = panel.transform.localPosition.x;
                    //panel.transform.localPosition = new Vector3(640.0f - 120.0f * panel.transform.localScale.x, panel.transform.localPosition.y, panel.transform.localPosition.z);
                    tp.to = tp.from = panel.transform.localPosition;
                    tp.to.x = 640.0f - 120.0f * panel.transform.localScale.x;
                    tp.Reset();
                    tp.PlayForward();
                    EventDelegate.Add(tp.onFinished, onFinished, true);

                    //sp.spriteName = "btn_c";
                    if (t != null)
                    {
                        //t.gameObject.SetActive(false);
                        ta.from = 1f;
                        ta.to = 0;
                        ta.Reset();
                        ta.PlayForward();
                    }
                }
            }
        }
        else if (gameObject.name == "Btn_Pet")
        {
            if (sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
            }
            sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_Pet);
            sdUIPetControl.Instance.ActivePetListPnl(null, true);

        }
        else if (gameObject.name == "Btn_PetTeam")
        {
            if (sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
            }
            sdUIPetControl.Instance.ActivePetWarPnl(null, -1);
        }
        else if (gameObject.name == "Btn_Act")
        {
            if (sdUICharacter.Instance.GetTownUI() != null)
            {
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(true);
            }
            sdActGameControl.Instance.ActiveActBaseWnd(null);
        }
        else if (gameObject.name == "sercetshop")
        {
            if (sdGameLevel.instance.mainChar["Level"] <= 20)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SercetShopClose"), MSGCOLOR.Yellow);
                return;
            }
            sdUICharacter.Instance.ShowMysteryShop(true);
            //sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SercetShopError"), Color.yellow);
        }
        else if (gameObject.name == "btn_mysteryshopclose")
        {
            sdUICharacter.Instance.ShowMysteryShop(false);
        }
        else if (gameObject.name == "btn_sell")
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(sdUICharacter.Instance.lastTipId));
            if (item == null) return;
            if (item.isLock)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ItemIsLock"), Color.red);
                return;
            }
            sdMsgBox.OnConfirm sell = new sdMsgBox.OnConfirm(OnSell);
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("SellMsg"), sell, null);

        }
        else if (gameObject.name == "btn_equipBagTab")
        {
            GameObject item = GameObject.Find("Text_Equipment_Bag");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Equipment";
            }

            item = GameObject.Find("Text_Use");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Use_nml";
            }

            item = GameObject.Find("Text_Material");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Material_nml";
            }

            item = GameObject.Find("Text_Other");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Other_nml";
            }

            item = GameObject.Find("btn_MaterialBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_equipBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = false;
            }

            item = GameObject.Find("btn_UseBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_OtherBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }
        }
        else if (gameObject.name == "btn_MaterialBagTab")
        {
            GameObject item = GameObject.Find("Text_Equipment_Bag");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Equipment_nml";
            }

            item = GameObject.Find("Text_Use");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Use_nml";
            }

            item = GameObject.Find("Text_Material");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Material";
            }

            item = GameObject.Find("Text_Other");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Other_nml";
            }

            item = GameObject.Find("btn_MaterialBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = false;
            }

            item = GameObject.Find("btn_equipBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_UseBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_OtherBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }
        }
        else if (gameObject.name == "btn_UseBagTab")
        {
            GameObject item = GameObject.Find("Text_Equipment_Bag");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Equipment_nml";
            }

            item = GameObject.Find("Text_Use");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Use";
            }

            item = GameObject.Find("Text_Material");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Material_nml";
            }

            item = GameObject.Find("Text_Other");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Other_nml";
            }

            item = GameObject.Find("btn_MaterialBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_equipBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_UseBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = false;
            }

            item = GameObject.Find("btn_OtherBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }
        }
        else if (gameObject.name == "btn_OtherBagTab")
        {
            GameObject item = GameObject.Find("Text_Equipment_Bag");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Equipment_nml";
            }

            item = GameObject.Find("Text_Use");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Use_nml";
            }

            item = GameObject.Find("Text_Material");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Material_nml";
            }

            item = GameObject.Find("Text_Other");
            if (item != null)
            {
                item.GetComponent<UISprite>().spriteName = "EquipmentSystem_Text_Other";
            }

            item = GameObject.Find("btn_MaterialBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_equipBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_UseBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = true;
            }

            item = GameObject.Find("btn_OtherBagTab");
            if (item != null)
            {
                item.GetComponent<UIButton>().isEnabled = false;
            }
        }
        else if (gameObject.name == "Button_openreward" || gameObject.name == "Button_rewards")
        {
            sdUICharacter.Instance.ShowPVPRewards(true);
        }
        else if (gameObject.name == "btn_townset")
        {
            sdUICharacter.Instance.ShowConfigWnd(true);
        }
        else if (gameObject.name == "btn_fightsetting")
        {
            sdUICharacter.Instance.ShowConfigWnd(false);
        }
        else if (gameObject.name == "btn_anglemode")
        {
            UISprite sp = gameObject.GetComponentInChildren<UISprite>();
            if (sp.spriteName == "sj1")
            {
                sp.spriteName = "sj2";
                if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.MainCity || sdGameLevel.instance.levelType == sdGameLevel.LevelType.Fight || sdGameLevel.instance.levelType == sdGameLevel.LevelType.None)
                    sdGameLevel.instance.mainCamera.ChangeCameraDistance();
                sdConfDataMgr.Instance().SetSetting("CFG_Camera", "1");
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("FightAngle2"), MSGCOLOR.Yellow);
            }
            else
            {
                sp.spriteName = "sj1";
                if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.MainCity || sdGameLevel.instance.levelType == sdGameLevel.LevelType.Fight || sdGameLevel.instance.levelType == sdGameLevel.LevelType.None)
                    sdGameLevel.instance.mainCamera.ChangeCameraDistance();
                sdConfDataMgr.Instance().SetSetting("CFG_Camera", "0");
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("FightAngle1"), MSGCOLOR.Yellow);
            }
            ConfigWndBtn.UpdateAngleAndControl();
        }
        else if (gameObject.name == "btn_controlmode")
        {
            UISprite sp = gameObject.GetComponentInChildren<UISprite>();
            if (sp.spriteName == "cz1")
            {
                sp.spriteName = "cz2";
                GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().AutoMode = false;
                sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Move", "1");
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("FightControl2"), MSGCOLOR.Yellow);
            }
            else
            {
                sp.spriteName = "cz1";
                GameObject.Find("@GameLevel").GetComponent<sdGameLevel>().AutoMode = true;
                sdConfDataMgr.Instance().SetSettingNoWrite("CFG_Move", "0");
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("FightControl1"), MSGCOLOR.Yellow);
            }
            ConfigWndBtn.UpdateAngleAndControl();
        }
        else if (gameObject.name == "Btn_Pvp")
        {
            sdUICharacter.Instance.ShowPVPMain(true);
            if (sdPVPManager.Instance.m_bRequestMatch == true)
            {
                sdPVPMsg.Send_CSID_GET_PVP_MATCH_REQ();
                sdPVPManager.Instance.m_bRequestMatch = false;
            }
            sdPVPMsg.Send_CSID_SELF_PVP_PRO_REQ();
        }
        else if (gameObject.name == "btn_backtown" || gameObject.name == "btn_reliveclose")
        {
            sdMsgBox.OnConfirm town = new sdMsgBox.OnConfirm(OnBackTown);
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("BackTown"), town, null);
        }
        else if (gameObject.name == "btn_retry")
        {
            sdMsgBox.OnConfirm town = new sdMsgBox.OnConfirm(OnFightRetry);
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("FightRetry"), town, null);
        }
        else if (gameObject.name == "btn_relive")
        {
            sdMainChar mainChar = sdGameLevel.instance.mainChar;
            if (mainChar != null)
            {
                if (sdUICharacter.Instance.ReliveNum() >= sdLevelInfo.ReliveLimit(sdUICharacter.Instance.iCurrentLevelID))
                {
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ReliveNoNum"));
                    return;
                }

                int cash = int.Parse(mainChar.GetBaseProperty()["Cash"].ToString()) + int.Parse(mainChar.GetBaseProperty()["NonCash"].ToString());
                int num = sdUICharacter.Instance.ReliveNum() + 1;
                if (num > 0)
                {
                    int price = sdUICharacter.Instance.RelivePrice() * num;
                    if (cash < price)
                    {
                        sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ReliveNoCash"));
                        return;
                    }
                }

                mainChar["HP"] = mainChar["MaxHP"];
                Hashtable hpDesc = new Hashtable();
                hpDesc["value"] = mainChar["MaxHP"];
                hpDesc["des"] = "";
                sdUICharacter.Instance.SetProperty("HP", hpDesc);
                mainChar["SP"] = mainChar["MaxSP"];
                Hashtable spDesc = new Hashtable();
                hpDesc["value"] = mainChar["MaxSP"];
                hpDesc["des"] = "";
                sdUICharacter.Instance.SetProperty("SP", hpDesc);
                sdUICharacter.Instance.HideRelive();
                sdUICharacter.Instance.AddReliveNum();

                mainChar.AddBuff(10005, 0, mainChar);
            }
        }
        else if (gameObject.name == "btn_at")
        {
            sdUICharacter.Instance.ShowFightChangeEffect();
            sdGameLevel level = GameObject.Find("@GameLevel").GetComponent<sdGameLevel>();
            if (level != null)
            {
                if (level.AutoMode == false) return;

                if (level.FullAutoMode == false)
                {
                    level.FullAutoMode = true;
                }
                else
                {
                    level.FullAutoMode = false;
                }
            }
        }
        else if (gameObject.name == "btn_mt")
        {
            sdUICharacter.Instance.ShowFightChangeEffect();
            sdGameLevel level = GameObject.Find("@GameLevel").GetComponent<sdGameLevel>();
            if (level != null)
            {
                if (level.AutoMode == false) return;

                if (level.FullAutoMode == false)
                {
                    level.FullAutoMode = true;
                }
                else
                {
                    level.FullAutoMode = false;
                }
            }
        }
        else if (gameObject.name == "Btn_Bag")
        {
            sdUICharacter.Instance.ShowRoleWnd(false);
        }
        else if (gameObject.name == "btn_learn")
        {
            string id = gameObject.transform.parent.GetComponent<sdSlotIcon>().itemid;
            Hashtable skillinfo = sdConfDataMgr.Instance().GetSkill(id);
            if (skillinfo == null) return;
            int requestPoint = int.Parse(skillinfo["dwCostSkillPoint"].ToString());
            if (requestPoint > sdGameSkillMgr.Instance.GetSkillPoint())
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NoSkillPoint"), Color.yellow);
                return;
            }

            sdGameSkill skill = sdGameSkillMgr.Instance.GetSkill(int.Parse(id));
            if (skill != null && skill.nextlv != 0)
            {
                sdSkillMsg.notifyLearnSkill(skill.nextlv);
            }
            else
            {
                sdSkillMsg.notifyLearnSkill(int.Parse(id));
            }
        }
        else if (gameObject.name == "tab_equip")
        {
            sdUICharacter.Instance.ShowBag();
        }
        else if (gameObject.name == "tab_roleatt")
        {
            sdUICharacter.Instance.ShowRoleProperty();
        }
        else if (gameObject.name == "tab_bagother")
        {
            sdSlotMgr.Instance.itemFilter = (int)ItemFilter.Other;
            sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
            sdSlotMgr.Instance.selectedItem.Clear();
        }
        else if (gameObject.name == "tab_armor")
        {
            sdSlotMgr.Instance.itemFilter = (int)ItemFilter.Armor;
            sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
            sdSlotMgr.Instance.selectedItem.Clear();
        }
        else if (gameObject.name == "tab_weapon")
        {
            sdSlotMgr.Instance.itemFilter = (int)ItemFilter.Weapon;
            sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
            sdSlotMgr.Instance.selectedItem.Clear();
        }
        else if (gameObject.name == "tab_shipin")
        {
            sdSlotMgr.Instance.itemFilter = (int)ItemFilter.Shipin;
            sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
            sdSlotMgr.Instance.selectedItem.Clear();
        }
        else if (gameObject.name == "btn_allsell")
        {
            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.ItemSell);
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(OnSellAll));
            // 			sdMsgBox.OnConfirm sell = new sdMsgBox.OnConfirm(OnSellAll);
            // 			sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("SellMsg"), sell, null);
            // 			sdSlotMgr.Instance.selectedItem.Clear();
        }
        else if (gameObject.name == "btn_medicine")
        {
            sdMainChar mainChar = sdGameLevel.instance.mainChar;
            if (mainChar != null)
            {
                if (int.Parse(mainChar["HP"].ToString()) <= 0)
                {
                    return;
                }

                if (mainChar["HP"].ToString() == mainChar["MaxHP"].ToString() &&
                    mainChar["SP"].ToString() == mainChar["MaxSP"].ToString())
                {
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MedicineError2"), MSGCOLOR.Yellow);
                    return;
                }

                int cash = int.Parse(mainChar.GetBaseProperty()["Cash"].ToString());
                int num = sdUICharacter.Instance.MedicineNum() + 1;
                if (num > sdUICharacter.Instance.FreeMedicineNum())
                {
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MedicineError"), MSGCOLOR.Yellow);
                    return;
                    //int price = sdUICharacter.Instance.MedicinePrice() * num;
                    //if (cash < price) return;
                }

                int maxHp = (int)mainChar["MaxHP"];
                int hp = (int)mainChar["HP"];
                if ((maxHp - hp) > 0)
                {
                    Vector3 kBubblePos = mainChar.transform.position;
                    kBubblePos.y += 2.0f;
                    Bubble.AddHp((maxHp - hp), kBubblePos, false);

                }
                mainChar["HP"] = mainChar["MaxHP"];
                Hashtable hpDesc = new Hashtable();
                hpDesc["value"] = mainChar["HP"];
                hpDesc["des"] = "";
                sdUICharacter.Instance.SetProperty("HP", hpDesc);

                int maxSp = (int)mainChar["MaxSP"];
                int sp = (int)mainChar["SP"];
                if ((maxSp - sp) > 0)
                {
                    Bubble.AddSp((maxSp - sp), Vector3.zero, true);
                }
                mainChar["SP"] = mainChar["MaxSP"];
                Hashtable spDesc = new Hashtable();
                hpDesc["value"] = mainChar["MaxSP"];
                hpDesc["des"] = "";
                sdUICharacter.Instance.SetProperty("SP", hpDesc);
                sdUICharacter.Instance.AddMedicineNum();
            }
        }
        else if (gameObject.name == "world")
        {
            int LevelID = 21011;	// 主城的解锁看第二章第一个战役第一个关卡是否解锁..
            bool LevelValid = false;
            for (int i = 0; i < sdLevelInfo.levelInfos.Length; i++)
            {
                if (sdLevelInfo.levelInfos[i].levelID == LevelID)
                {
                    LevelValid = sdLevelInfo.levelInfos[i].valid;
                    break;
                }
            }

            if (LevelValid)
            {
                sdUICharacter.Instance.DontDestoryUI();
                sdUICharacter.Instance.ShowFullScreenUI(true);
                sdUICharacter.JumpToMainCity();
            }
            else
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MainCityError"), MSGCOLOR.Yellow);
            }
        }
        else if (gameObject.name == "town")
        {
            // 第一次从主城返回，则播放下个关卡解锁效果..
            Hashtable data = sdGlobalDatabase.Instance.globalData;
            if (data.ContainsKey("OpenLevel_MainCity") && (int)data["OpenLevel_MainCity"] == 1)
            {
                data["OpenLevel_MainCity"] = 0;
                sdWorldMapPath.SetLevel(21, true);
            }

            sdUICharacter.Instance.DontDestoryUI();
            sdUICharacter.Instance.ShowFullScreenUI(true);
            sdUICharacter.JumpToWorldMap();
            //BundleGlobal.Instance.StartLoadBundleLevel("Level/guildmap/worldmap/$worldmap.unity3d","$worldmap");

            //sdWorldMapPath.SetLevel(20,true);
        }
        else if (gameObject.name == "btn_changeitem")
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(ulong.Parse(sdUICharacter.Instance.lastTipId));
            sdUICharacter.Instance.SetSelectWndNeedPos(item.equipPos);
            sdUICharacter.Instance.ShowItemSelectWnd(SelectType.EquipSelect);
            sdUICharacter.Instance.AddEventOnSelectWnd(new EventDelegate(sdUICharacter.Instance.OnChangeEquip));
        }
        else if (gameObject.name == "msg_confirm")
        {
            sdUICharacter.Instance.MsgClickOK();
        }
        else if (gameObject.name == "msg_cancel")
        {
            sdUICharacter.Instance.MsgClickCancel();
        }
        else if (gameObject.name == "btn_chat")
        {
            if (sdUICharacter.Instance.IsChatWndActive())
            {
                sdUICharacter.Instance.HideChatWnd();
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().ShowChat();
            }
            else
            {
                sdUICharacter.Instance.ShowChatWnd();
                sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().HideChat();
            }
            //             sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ChatError"), Color.yellow);
            // 
            //             string chat = sdUICharacter.Instance.GetChatInfo();
            //             if (sdGMCmd.GMCommand(chat) == false)
            //                 sdChatMsg.SendChat(HeaderProto.EChatType.CHAT_TYPE_SYSTEM, chat, "");
        }
        else if (gameObject.name == "Btn_Comm")
        {
            sdUICharacter.Instance.ShowFriendWnd();
        }
        else if (gameObject.name == "close_friend")
        {
            sdUICharacter.Instance.HideFriendWnd();
        }
        else if (gameObject.name == "close_friendinfo")
        {
            sdUICharacter.Instance.HideFriInfo();
        }
        else if (gameObject.name == "tab_add")
        {
            sdUICharacter.Instance.ShowAddFriTab();
        }
        else if (gameObject.name == "tab_friend")
        {
            sdUICharacter.Instance.ShowFriendTab();
        }
        else if (gameObject.name == "tab_invite")
        {
            sdUICharacter.Instance.ShowInviteFriTab();
        }
        else if (gameObject.name == "btn_searchfri")
        {
            sdFriendMsg.notifyQueryRole(sdUICharacter.Instance.GetSearchFriName(), 0);
        }
        else if (gameObject.name == "btn_delfri")
        {
            sdMsgBox.OnConfirm del = new sdMsgBox.OnConfirm(OnDelFriend);
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("DelFriConfirm"), del, null);
        }
        else if (gameObject.name == "btn_getsta")
        {
            sdFriendMsg.notifyReceiveAp(sdUICharacter.Instance.GetCurFriId());
        }
        else if (gameObject.name == "btn_sendsta")
        {
            sdFriendMsg.notifySendAp(sdUICharacter.Instance.GetCurFriId());
        }
        else if (gameObject.name == "btn_online")
        {
            sdUICharacter.Instance.ShowFriendOnline();
        }
        else if (gameObject.name == "btn_detailinfo")
        {
            sdUICharacter.Instance.ShowFriAvatar();
        }
        else if (gameObject.name == "btn_cfAddFri")
        {
            sdFriendMsg.notifyConfirmAddFri(transform.parent.parent.parent.GetComponent<sdFriendInfo>().GetId(), 1);
        }
        else if (gameObject.name == "btn_rfAddFri")
        {
            sdFriendMsg.notifyConfirmAddFri(transform.parent.parent.parent.GetComponent<sdFriendInfo>().GetId(), 0);
        }
        else if (gameObject.name == "btn_jiesuanNofri")
        {
            FinishTuitu();
        }
        else if (gameObject.name == "btn_jiesuanAddfri")
        {
            sdFriendMsg.notifyAddFri(sdUICharacter.Instance.GetSearchFriName());
            FinishTuitu();
        }
        else if (gameObject.name == "btn_AddFri")
        {
            sdFriendMsg.notifyAddFri(transform.parent.GetComponent<sdFriendInfo>().GetName());
        }
        else if (gameObject.name == "btn_getAp")
        {
            sdFriendMsg.notifyReceiveAp(transform.parent.parent.GetComponent<sdFriendInfo>().GetId());
        }
        else if (gameObject.name == "btn_sendAP")
        {
            sdFriendMsg.notifySendAp(transform.parent.parent.GetComponent<sdFriendInfo>().GetId());
        }
        else if (gameObject.name == "btn_itemup")
        {
            sdGameItemMgr.Instance.upItem = sdGameItemMgr.Instance.getItem(ulong.Parse(sdUICharacter.Instance.lastTipId));
            sdUICharacter.Instance.ShowItemUpWnd(false);
            sdUICharacter.Instance.HideTip(true);
        }
        else if (gameObject.name == "Btn_DoItemup")
        {
            sdUICharacter.Instance.ShowItemUpWnd(true);
        }
        else if (gameObject.name == "btn_shop")
        {
            sdUICharacter.Instance.iCurrentLevelID = 1;
            BundleGlobal.Instance.StartLoadBundleLevel("Level/guidemap/guidemap_1/$guidemap_1.unity.unity3d", "$guidemap_1");
        }
        else if (gameObject.name == "btn_selectItemOK")
        {

        }
        else if (gameObject.name == "btn_selectItemCancel")
        {
            sdUICharacter.Instance.HideSelectWnd();
        }
        else if (gameObject.name == "btn_beginup")
        {
            if (sdGameItemMgr.Instance.upItem == null)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SelectUpItem"), Color.yellow);
                return;
            }

            if (sdGameItemMgr.Instance.GetItemUpId().Length <= 0)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SelectUpUseItem"), Color.yellow);
                return;
            }
            int max = sdConfDataMgr.Instance().GetMaxItemUp(sdGameItemMgr.Instance.upItem.templateID.ToString());
            if (sdGameItemMgr.Instance.upItem.upLevel >= max)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ItemUpMax"), Color.yellow);
                return;
            }

            if (uint.Parse(sdUICharacter.Instance.GetItemUpWnd().lbl_money.text) > uint.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["NonMoney"].ToString()))
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ItemUpNoMoney"), Color.yellow);
                return;
            }

            ulong[] list = sdGameItemMgr.Instance.GetItemUpId();
            foreach (ulong id in list)
            {
                sdGameItem item = sdGameItemMgr.Instance.getItem(id);
                if (item == null) continue;
                if (item.gemList.Length > 0)
                {
                    foreach (int gemId in item.gemList)
                    {
                        if (gemId > 0)
                        {
                            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("ItemHasGem"), new sdMsgBox.OnConfirm(OnItemUp), null);
                            return;
                        }
                    }
                }
            }

            sdItemMsg.notifyItemUp(sdGameItemMgr.Instance.upItem.instanceID.ToString(), sdGameItemMgr.Instance.GetItemUpId());
        }
        else if (gameObject.name == "close_itemup")
        {
            sdUICharacter.Instance.HideItemUpWnd();
        }
        else if (gameObject.name == "btn_setallitem")
        {
            sdUICharacter.Instance.GetItemUpWnd().SetAllItem();
        }
        else if (gameObject.name == "btn_beginmerge")
        {
            
        }
        else if (gameObject.name == "btn_allmerge")
        {
            
        }
        else if (gameObject.name == "btn_skillreset")
        {
            if (sdGameSkillMgr.Instance.GetTotalPoint() <= 0)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SkillResetNoNeed"));
                return;
            }
            sdUICharacter.Instance.ShowOkCanelMsg(sdConfDataMgr.Instance().GetShowStr("SkillReset"), new sdMsgBox.OnConfirm(OnResetSkill), null);
        }
        else if (gameObject.name == "btn_beginreplace")
        {
            string id1 = "";
            string id2 = "";
            foreach (KeyValuePair<string, int> info in sdGameItemMgr.Instance.selGemList)
            {
                int num = info.Value;
                for (int i = 0; i < num; ++i)
                {
                    if (id1 == "")
                    {
                        id1 = info.Key;
                    }
                    else if (id2 == "")
                    {
                        id2 = info.Key;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            if (id1 == "" || id2 == "")
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("MergeGemNumError"), Color.yellow);
                return;
            }

            sdItemMsg.notifyGemReplace(id1, id2);
        }
        else if (gameObject.name == "btn_gem1off")
        {
            sdItemMsg.notifyGemOff(0, sdGameItemMgr.Instance.upItem.instanceID.ToString());
        }
        else if (gameObject.name == "btn_gem2off")
        {
            sdItemMsg.notifyGemOff(1, sdGameItemMgr.Instance.upItem.instanceID.ToString());
        }
        else if (gameObject.name == "btn_gem3off")
        {
            sdItemMsg.notifyGemOff(2, sdGameItemMgr.Instance.upItem.instanceID.ToString());
        }
        else if (gameObject.name == "btn_allgemoff")
        {
            sdItemMsg.notifyGemOff(0, sdGameItemMgr.Instance.upItem.instanceID.ToString());
            sdItemMsg.notifyGemOff(1, sdGameItemMgr.Instance.upItem.instanceID.ToString());
            sdItemMsg.notifyGemOff(2, sdGameItemMgr.Instance.upItem.instanceID.ToString());
        }
        else if (gameObject.name == "btn_sortLv")
        {
            sdUICharacter.Instance.SortSelectWnd(1);
        }
        else if (gameObject.name == "btn_sortQu")
        {
            sdUICharacter.Instance.SortSelectWnd(0);
        }
        else if (gameObject.name == "btn_sellWhite")
        {
            if (transform.FindChild("effected").GetComponent<UISprite>().spriteName == "")
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "g2";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
            }
            else
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2";
            }
            sdUICharacter.Instance.AddItemByQuility(1);
        }
        else if (gameObject.name == "btn_sellGreen")
        {
            if (transform.FindChild("effected").GetComponent<UISprite>().spriteName == "")
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "g2";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
            }
            else
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2";
            }
            sdUICharacter.Instance.AddItemByQuility(2);
        }
        else if (gameObject.name == "btn_sellBlue")
        {
            if (transform.FindChild("effected").GetComponent<UISprite>().spriteName == "")
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "g2";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2dis";
            }
            else
            {
                transform.FindChild("effected").GetComponent<UISprite>().spriteName = "";
                transform.FindChild("Background").GetComponent<UISprite>().spriteName = "btn_2";
            }
            sdUICharacter.Instance.AddItemByQuility(3);
        }
        else if (gameObject.name == "btn_allequip")
        {
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
                    sdItemMsg.notifyEquipItem(item.instanceID.ToString(), 1);
                    continue;
                }
                int score = sdConfDataMgr.Instance().GetItemScore(item.instanceID);
                int compareScore = sdConfDataMgr.Instance().GetItemScore(compareItem.instanceID);
                if (score > compareScore)
                {
                    sdItemMsg.notifyEquipItem(item.instanceID.ToString(), 1);
                }
            }
        }
        else if (PVPOnClick(gameObject))
            return;
        else if (SelectSrv(gameObject))
            return;
        else if (RoleTipOnClick(gameObject))
            return;
        else if (HudOnClick(gameObject))
            return;
        else if (PTOnClick(gameObject))
            return;
    }

	void onFinished()
	{
		UISprite sp = GameObject.Find("Btn_Push").transform.FindChild("Background").GetComponent<UISprite>();
		if (sp.spriteName == "btn_c")
			sp.spriteName = "btn_o";
		else
			sp.spriteName = "btn_c";
	}

    bool SelectSrv(GameObject obj)
    {
        if (obj.transform.parent != null && obj.transform.parent.name == "Panel_srvlist")
        {
            string str = obj.name;
            str = str.Substring(4);
            int index = int.Parse(str);
            if(index >= 0 && index < SDNetGlobal.m_lstSrvInfo.Count)
            {
                JsonNode SERVER =   SDNetGlobal.m_lstSrvInfo[index];
                SDNetGlobal.serverId = int.Parse(SERVER.Attribute("ServerID"));
                SDNetGlobal.Login_IP = SERVER.Attribute("IP");
                SDNetGlobal.Login_Port = ushort.Parse(SERVER.Attribute("Port"));
                SDNetGlobal.serverName = SERVER.Attribute("ServerName");
                SDNetGlobal.SaveSrvInfo();
                sdUICharacter.Instance.ShowSelectSrvWnd(false);
                sdUICharacter.Instance.RefreshLoginUI();
                return true;
            }
        }
        return false;
    }

    bool RoleTipOnClick(GameObject obj)
    {
        if (obj.name == "tab_equip_roletip")
        {
            GameObject wnd = sdUICharacter.Instance.roleTipWnd;
            if (wnd != null)
            {
                sdRoleTipWnd roletip = wnd.GetComponent<sdRoleTipWnd>();
                if (roletip != null)
                    roletip.ShowEquip();
            }
            return true;
        }
        else if (obj.name == "tab_fashion_roletip")
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("InDevelop"), Color.yellow);
            GameObject wnd = sdUICharacter.Instance.roleTipWnd;
            if (wnd != null)
            {
                sdRoleTipWnd roletip = wnd.GetComponent<sdRoleTipWnd>();
                if (roletip != null)
                    roletip.Keep();
            }
            return true;
        }
        else if (obj.name == "tab_pet_roletip")
        {
            GameObject wnd = sdUICharacter.Instance.roleTipWnd;
            if (wnd != null)
            {
                sdRoleTipWnd roletip = wnd.GetComponent<sdRoleTipWnd>();
                if (roletip != null)
                    roletip.ShowPet();
            }
            return true;
        }
        else if (gameObject.name == "btn_roletipwnd_close")
        {
            sdUICharacter.Instance.ShowRoleTipWnd(null, false, 0);
            return true;
        }
        return false;
    }

	bool PVPOnClick(GameObject obj)
	{
		if(obj.name == "Button_pvpmainclose")
		{
			sdUICharacter.Instance.ShowPVPMain(false);
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PVP)
            {
                sdUICharacter.Instance.TuiTu_To_WorldMap();
            }
			return true;
		}
		else if(obj.name == "Button_pvprewardclose")
		{
			sdUICharacter.Instance.ShowPVPRewards(false);
			return true;
		}
        else if (obj.name == "Sprite_rankheadicon")
        {
            string str = obj.transform.parent.name;
            str = str.Substring(12);
            int index = int.Parse(str);
            List<stPVPRival> ranklist = sdPVPManager.Instance.GetRanklist();
            if (index >= 0 && index < ranklist.Count)
            {
                sdPVPMsg.ms_Index = index;
                stPVPRival rival = ranklist[index];
                sdPVPMsg.Send_GET_PVP_ROLE_INFO_REQ(rival.roleID);
            }
            return true;
        }
        else if (gameObject.name == "Button_buyTime_quit")
        {
            GameObject root = GameObject.Find("Sprite_pvpbuytimewnd_bg");
            switch (sdPVPManager.Instance.m_buyWndType)
            {
                case 0:
                    sdUICharacter.Instance.ShowPVPMain(true, false);
                    break;
                case 1:
                    sdUICharacter.Instance.ShowPTWnd(true);
                    break;
            }
            WndAni.HideWndAni(root.transform.parent.gameObject, false, "w_grey");
            return true;
        }
        else if (gameObject.name == "Button_onetime")
        {
            GameObject root = GameObject.Find("Sprite_pvpbuytimewnd_bg");
            int times = 1;
            switch (sdPVPManager.Instance.m_buyWndType)
            {
                case 0:
                    sdPVPMsg.Send_CS_PVP_BUY_CHALLENGE_TIMES_REQ(times);
                    sdUICharacter.Instance.ShowPVPMain(true, false);
                    break;
                case 1:
                    sdActGameMsg.Send_CSID_BUY_PT_TIMES_REQ(times);
                    sdUICharacter.Instance.ShowPTWnd(true);
                    break;
            }
            WndAni.HideWndAni(root.transform.parent.gameObject, false, "w_grey");
            return true;
        }
        else if (gameObject.name == "Button_all")
        {
            GameObject root = GameObject.Find("Sprite_pvpbuytimewnd_bg");
            switch (sdPVPManager.Instance.m_buyWndType)
            {
                case 0:
                    if (sdPVPManager.Instance.m_ChallengeBuyLeft > 0)
                    {
                        sdPVPMsg.Send_CS_PVP_BUY_CHALLENGE_TIMES_REQ(sdPVPManager.Instance.m_ChallengeBuyLeft);
                        sdUICharacter.Instance.ShowPVPMain(true, false);
                    }
                    break;
                case 1:
                    if (sdPTManager.Instance.m_BuyTimes > 0)
                    {
                        sdActGameMsg.Send_CSID_BUY_PT_TIMES_REQ((int)sdPTManager.Instance.m_BuyTimes);
                        sdUICharacter.Instance.ShowPTWnd(true);
                    }
                    break;
            }
            WndAni.HideWndAni(root.transform.parent.gameObject, false, "w_grey");
            return true;
        }
		return false;
	}

    bool HudOnClick(GameObject obj)
    {
        if (obj.name == "1")
        {
            sdUICharacter.Instance.ShowPTWnd(true);
            sdActGameMsg.Send_CSID_GET_PT_BASEINFO_REQ();
            //sdUICharacter.Instance.ShowPVPMain(true);
            //if (sdPVPManager.Instance.m_bRequestMatch == true)
            //{
            //    sdPVPMsg.Send_CSID_GET_PVP_MATCH_REQ();
            //    sdPVPManager.Instance.m_bRequestMatch = false;
            //}
            //sdPVPMsg.Send_CSID_SELF_PVP_PRO_REQ();
            return true;
        }
        return false;
    }

    void OnPTBuy()
    {
        
    }

    bool PTOnClick(GameObject obj)
    {
        if (obj.name == "btn_pt_close")
        {
            sdUICharacter.Instance.ShowPTWnd(false);
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PET_TRAIN)
                sdUICharacter.JumpToMainCity();
            return true;
        }
        else if (obj.name == "Button_PT_buytimes")
        {
            if (sdPTManager.Instance.m_BuyTimes <= 0)
            {
                string str = sdConfDataMgr.Instance().GetShowStr("ptbuy");
                str = str + "\n";
                str = str + sdConfDataMgr.Instance().GetShowStr("upvip");
                sdUICharacter.Instance.ShowOkCanelMsg(str, new sdMsgBox.OnConfirm(OnPTBuy), null);
            }
            else
                sdUICharacter.Instance.ShowChallengeBuyWnd(1);
            return true;
        }
        else if (obj.name == "Button_PT_enter1" || obj.name == "Button_PT_enter2" || obj.name == "Button_PT_enter3")
        {
            if (sdPTManager.Instance.m_Times <= 0)
            {
                sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("ptnotime"), null);
                return true;
            }
            string str = obj.name.Substring(15);
            byte byLevelID = byte.Parse(str);
            sdGlobalDatabase.Instance.globalData["LevelDifficulty"] = byLevelID - 1;
            sdPTManager.Instance.m_ChoiceLevel = byLevelID;
            if (byLevelID > sdPTManager.Instance.m_PassLevel + 1)
            {
                sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("pt"), null);
                return true;
            }
            sdUILoading.ActiveLoadingUI("worldmap", sdConfDataMgr.Instance().GetShowStr("ptloading"));
            Hashtable ptTable = sdConfDataMgr.Instance().GetTable("dmdsxactivitytemplateconfig.pttemplates");
            if (ptTable.ContainsKey(str))
            {
                Hashtable table = ptTable[str] as Hashtable;

                CliProto.CS_LEVEL_REQ refMSG = new CliProto.CS_LEVEL_REQ();
                refMSG.m_LevelBattleType = (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_PET_TRAIN;
                refMSG.m_LevelID = uint.Parse(table["LevelID"] as string);
                refMSG.m_AbyssDBID = ulong.Parse(str);
                sdLevelInfo.SetCurLevelId((int)refMSG.m_LevelID);
                sdPTManager.Instance.m_ChoiceLevel = (byte)refMSG.m_AbyssDBID;
                SDNetGlobal.SendMessage(refMSG);
            }
            return true;
        }
        return false;
    }
	
	void FinishTuitu()
	{
		// 客户端打开后续关卡.
		int nextlevel = sdUICharacter.Instance.iCurrentLevelID;
		for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
		{
			if( sdLevelInfo.levelInfos[i].levelID == sdUICharacter.Instance.iCurrentLevelID )
				sdLevelInfo.levelInfos[i].crystal = 1;
			if( (int)sdLevelInfo.levelInfos[i].levelProp["PrecedentID"] == sdUICharacter.Instance.iCurrentLevelID )
			{
				sdLevelInfo.levelInfos[i].valid = true;
				if( sdLevelInfo.levelInfos[i].levelID > nextlevel ) 
					nextlevel = sdLevelInfo.levelInfos[i].levelID;
			}
		}
			
		// 判断是否是第一次完成这个关卡...
		GameObject obj = GameObject.Find("jiesuanWnd(Clone)");
		if(obj) obj.SetActive(false);
		if( sdUICharacter.Instance.bCampaignLastLevel )
		{
			if( nextlevel == 21011 )
			{
				// 判断是是否是主城之后第一个关卡被解锁，则表现为解锁主城..
				nextlevel = 20000;
				sdGlobalDatabase.Instance.globalData["OpenLevel_MainCity"] = 1;
			}
			sdWorldMapPath.SetLevel(nextlevel/1000,true);
			sdUICharacter.Instance.TuiTu_To_WorldMap();
		}
		else
		{
			AudioSource[] audios = GameObject.Find("@GameLevel").GetComponentsInChildren<AudioSource>(false);
			foreach(AudioSource au in audios)
			{
				if( au.isPlaying ) 
				{
					ResLoadParams param = new ResLoadParams();
					sdResourceMgr.Instance.LoadResource("Music/$camp_theme01.ogg",LoadMusic,param);
					asWin = au;
					break;
				}
			}
			
			sdUICharacter.Instance.ShowTuitu();
		}	
	}
	
	void LoadMusic(ResLoadParams param, Object obj)
	{
		AudioClip au = GameObject.Instantiate(obj) as AudioClip; 
		if( asWin )
		{
			asWin.Stop();
			asWin.clip = au;
			asWin.loop = true;
			asWin.Play();
		}
	}

	void OnBackTown()
	{
		if(sdGameLevel.instance.levelType == sdGameLevel.LevelType.PVP)
		{
			sdPVPManager.Instance.KillMe(null);
		}
		else
		{
			CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
			refMSG.m_Result = 1; // 主动放弃当前关卡.
			SDNetGlobal.SendMessage(refMSG);
            sdUICharacter.Instance.TuiTu_To_WorldMap();
		}
	}

    void OnFightRetry()
    {
        if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PVP)
        {
            sdPVPManager.Instance.KillMe(null);
        }
        else
        {
            CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
            refMSG.m_Result = 1; // 主动放弃当前关卡.
            SDNetGlobal.SendMessage(refMSG);
            

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
    }
}
