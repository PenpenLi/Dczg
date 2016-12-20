using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class sdItemMsg : UnityEngine.Object 
{
	const int MaxBuffNum = 6;

	public static bool init()
	{
		SDGlobal.Log("sdItemMsg.init");
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_BAG_NTF, msg_SCID_ROLE_BAG_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_EQUIP_NTF, msg_SCID_ROLE_EQUIP_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_LEAVE_NTF, msg_SCID_ROLE_BAG_SINGLE_LEAVE_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_ENTER_NTF, msg_SCID_ROLE_BAG_SINGLE_ENTER_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF, msg_SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_ENTER_NTF, msg_SCID_ROLE_EQUIP_SINGLE_ENTER_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_ITEMUP_ACK, msg_SCID_ROLE_ITEMUP_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_SUIT_NTF, msg_SCID_ROLE_SUIT_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_HOLE_ON_ACK, msg_SCID_ROLE_HOLE_ON_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_HOLE_OFF_ACK, msg_SCID_ROLE_HOLE_OFF_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_HOLE_REFRESH_ACK, msg_SCID_ROLE_HOLE_REFRESH_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_HOLE_MERGE2_ACK, msg_SCID_ROLE_HOLE_MERGE_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_LIMIT_NTF, msg_SCID_ROLE_LIMIT_NTF);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_PROD_ACK, msg_SCID_ROLE_PROD_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_USEITEM_START_ACK, msg_SCID_ROLE_USEITEM_START_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_USEITEM_END_ACK, msg_SCID_ROLE_USEITEM_END_ACK);
		return true;
	}

    private static void msg_SCID_ROLE_USEITEM_START_ACK(int iMsgID, ref CMessage msg)
    {

    }

    private static void msg_SCID_ROLE_USEITEM_END_ACK(int iMsgID, ref CMessage msg)
    {

    }

    public static bool notifyUseItem()
    {
        CliProto.CS_ROLE_USEITEM_REQ refMsg = new CliProto.CS_ROLE_USEITEM_REQ();
        refMsg.m_UUID = ulong.Parse(sdUICharacter.Instance.lastTipId);
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }

    private static void msg_SCID_ROLE_PROD_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_ROLE_PROD_ACK refMSG = (CliProto.SC_ROLE_PROD_ACK)msg;
        sdGameItemMgr.Instance.upItem = null;
        sdUICharacter.Instance.GetItemUpWnd().RefreshItemMakePanel();
    }

    public static bool notifyMakeUp()
    {
        CliProto.CS_ROLE_PROD_REQ refMsg = new CliProto.CS_ROLE_PROD_REQ();
        refMsg.m_DBID = sdGameItemMgr.Instance.upItem.instanceID;
        Hashtable makeTable = sdConfDataMgr.Instance().GetFormula(sdGameItemMgr.Instance.upItem.templateID.ToString());

        if (makeTable != null)
        {
            List<UInt64> list = new List<UInt64>();
            int count = 0;
            for (int i = 1; i <= 5; ++i)
            {
                string name = string.Format("U{0}.ItemID", i);
                string id = makeTable[name].ToString();
                if (id == "0") break;
                UInt64 insId = sdGameItemMgr.Instance.getItemUIDByTID(int.Parse(id));
                ++count;
                list.Add(insId);
            }

            refMsg.m_Count = count;
            refMsg.m_OtherDBID = new ulong[count];
            int num = 0;
            foreach (UInt64 id in list)
            {
                refMsg.m_OtherDBID[num] = id;
                ++num;
            }
        }
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }

    private static void msg_SCID_ROLE_LIMIT_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_ROLE_LIMIT_NTF refMSG = (CliProto.SC_ROLE_LIMIT_NTF)msg;
        sdGameItemMgr.Instance.maxBagNum = refMSG.m_MaxRoleBagCount - 10;
        sdUICharacter.Instance.UpdateBagNum();
    }

	// 背包物品列表
	private static void msg_SCID_ROLE_BAG_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_BAG_NTF refMSG = (CliProto.SC_ROLE_BAG_NTF)msg;
		int count = refMSG.m_Items.m_ItemCount;
		for(int i = 0; i < count; ++i)
		{
			sdGameItemMgr.Instance.createItem(refMSG.m_Items.m_Items[i],(int)PanelType.Panel_Bag);
		}
        //sdGameItemMgr.Instance.maxBagNum = refMSG.m_MaxRoleBagCount-10;
		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
		sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
        sdUICharacter.Instance.UpdateBagNum();
//		if(sdGameLevel.instance.mainChar!=null)
//			sdGameLevel.instance.mainChar.SetItemInfo(sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] as Hashtable);
//	
	}
	
	private static void msg_SCID_ROLE_EQUIP_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_EQUIP_NTF refMSG = (CliProto.SC_ROLE_EQUIP_NTF)msg;
		int count = refMSG.m_Items.m_ItemCount;
		for(int i = 0; i < count; ++i)
		{
			sdGameItemMgr.Instance.createItem(refMSG.m_Items.m_Items[i],(int)PanelType.Panel_Equip);
		}
		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
		sdSlotMgr.Instance.Notify((int)PanelType.Panel_Equip);
        RefreshSuit();
//		if(sdGameLevel.instance.mainChar!=null)
//			sdGameLevel.instance.mainChar.SetItemInfo(sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] as Hashtable);
//	
	}
	
	private static void msg_SCID_ROLE_BAG_SINGLE_LEAVE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_BAG_SINGLE_LEAVE_NTF refMsg = (CliProto.SC_ROLE_BAG_SINGLE_LEAVE_NTF)msg;
		sdGameItemMgr.Instance.RemoveItem(refMsg.m_ItemUUID);
		sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
		sdSlotMgr.Instance.selectedItem.Clear();
        sdUICharacter.Instance.UpdateBagNum();
	}
	
	private static void msg_SCID_ROLE_BAG_SINGLE_ENTER_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_BAG_SINGLE_ENTER_NTF refMsg = (CliProto.SC_ROLE_BAG_SINGLE_ENTER_NTF)msg;
        bool isExist = sdGameItemMgr.Instance.getItem(refMsg.m_Item.m_UID) == null ? false : true;
		sdGameItem item = sdGameItemMgr.Instance.createItem(refMsg.m_Item,(int)PanelType.Panel_Bag);
        if (refMsg.m_EnterType == (int)HeaderProto.EItemEnterType.ITEM_ENTER_TYPE_DEFAULT && !isExist)
        {
            item.isNew = true;
            sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Item);
        }
		sdSlotMgr.Instance.Notify((int)PanelType.Panel_Bag);
		sdSlotMgr.Instance.selectedItem.Clear();
        sdUICharacter.Instance.RefreshTip();
        sdUICharacter.Instance.UpdateBagNum();
	}
	
	private static void msg_SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_EQUIP_SINGLE_LEAVE_NTF refMsg = (CliProto.SC_ROLE_EQUIP_SINGLE_LEAVE_NTF)msg;
		sdGameItemMgr.Instance.RemoveItem(refMsg.m_ItemUUID);
		sdSlotMgr.Instance.selectedItem.Clear();
		sdSlotMgr.Instance.Notify((int)PanelType.Panel_Equip);
		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
        RefreshSuit();
        sdUICharacter.Instance.HideTip(true);
    }

    private static void msg_SCID_ROLE_EQUIP_SINGLE_ENTER_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_ROLE_EQUIP_SINGLE_ENTER_NTF refMsg = (CliProto.SC_ROLE_EQUIP_SINGLE_ENTER_NTF)msg;
        sdGameItemMgr.Instance.createItem(refMsg.m_Item, (int)PanelType.Panel_Equip);
        sdSlotMgr.Instance.selectedItem.Clear();
        sdSlotMgr.Instance.Notify((int)PanelType.Panel_Equip | (int)PanelType.Panel_Bag);
        sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2, -1);
        RefreshSuit();
        sdUICharacter.Instance.HideTip(true);
        if (sdUICharacter.Instance.GetJiesuanWnd() != null)
        {
            sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("EquipSuccess"), Color.yellow);
        }
    }

    static void RefreshSuit()
    {
        Hashtable suitInfo = new Hashtable();
        Hashtable itemTable = sdGameItemMgr.Instance.GetAllItem((int)PanelType.Panel_Equip, (int)ItemFilter.All);
        foreach (DictionaryEntry info in itemTable)
        {
            sdGameItem item = info.Value as sdGameItem;
            int id = item.suitId;
            if (id == 0) continue;
            
            if (suitInfo.ContainsKey(id))
            {
                int num = (int)suitInfo[id];
                suitInfo[id] = ++num;
            }
            else
            {
                suitInfo.Add(id, 1);
            }
        }
        sdGameItemMgr.Instance.suitInfo.Clear();
        List<int> list = new List<int>();
	    foreach (DictionaryEntry info in suitInfo)
	    {
            string id = info.Key.ToString();
            Hashtable table = sdConfDataMgr.Instance().GetSuit(id);
            if (table == null)
                continue;
            if (table == null) continue;
            int num = (int)info.Value;

            sdGameItemMgr.Instance.suitInfo.Add(int.Parse(id), num);
            for (int j = 0; j < num; ++j)
            {
                string key = string.Format("SuitSap{0}.SapID", j + 1);
                if (!table.ContainsKey(key))
                {
                    continue;
                }

                string sapId = table[key].ToString();
                Hashtable sapInfo = sdConfDataMgr.Instance().GetSap(sapId);
                if (sapInfo == null)
                {
                    continue;
                }

                for (int k = 0; k < MaxBuffNum; ++k)
                {
                    key = string.Format("Skill{0}.SkillID", k + 1);
                    if (!sapInfo.ContainsKey(key)) continue;
                    if (sapInfo[key].ToString() == "0") continue;
                    list.Add(int.Parse(sapInfo[key].ToString()));
                }
            }
	    }
        if (sdGameLevel.instance != null && sdGameLevel.instance.mainChar != null)
        {
            sdGameLevel.instance.mainChar.ClearSuitBuff();
        }
        if (sdGlobalDatabase.Instance.globalData.ContainsKey("suitBuff"))
        {
            sdGlobalDatabase.Instance.globalData["suitBuff"] = list;
        }
        else
        {
            sdGlobalDatabase.Instance.globalData.Add("suitBuff", list);
        }
    }

	private static void msg_SCID_ROLE_ITEMUP_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_ITEMUP_ACK refMsg = (CliProto.SC_ROLE_ITEMUP_ACK)msg;
		if (sdUICharacter.Instance.GetItemUpWnd() != null)
		{
			sdUICharacter.Instance.GetItemUpWnd().OnItemUpSuccess();
		}
	}

	private static void msg_SCID_ROLE_SUIT_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_SUIT_NTF refMsg = (CliProto.SC_ROLE_SUIT_NTF)msg;
		int count = refMsg.m_Count;
		
		for(int i = 0; i < count; ++i)
		{
			int id = refMsg.m_Suit[i].m_SuitID;
			
		}

		
	}
	
//	private static void msg_SCID_SELF_ITEM(int iMsgID, ref CMessage msg)
//	{
//		CliProto.SC_SELF_ITEM_NTF refMSG = (CliProto.SC_SELF_ITEM_NTF)msg;
//		SDGlobal.Log("<- SCID_SELF_ITEM_NTF : ");
//		
//		int count = refMSG.m_ItemList.m_Count;
//		for(int i = 0; i < count; ++i)
//		{
//			sdGameItemMgr.Instance.createItem((int)refMSG.m_ItemList.m_ItemList[i].m_TemplateId, refMSG.m_ItemList.m_ItemList[i].m_UUID, (int)refMSG.m_Pos, refMSG.m_ItemList.m_ItemList[i].m_Count);
//		}
//		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
//		sdSlotMgr.Instance.Notify((int)refMSG.m_Pos);
//		if(sdGameLevel.instance.mainChar!=null)
//			sdGameLevel.instance.mainChar.SetItemInfo(sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] as Hashtable);
//	}
	
	
//	private static void msg_SCID_MOVE_ITEM_ACK(int iMsgID, ref CMessage msg)
//	{
//		CliProto.SC_MOVE_ITEM_ACK refMsg = (CliProto.SC_MOVE_ITEM_ACK)msg;
//		sdGameItemMgr.Instance.MoveItem(refMsg.m_Info.m_FromPos, refMsg.m_Info.m_ToPos, refMsg.m_Info.m_UUID, refMsg.m_Info.m_AnotherUUID);//		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
//		sdSlotMgr.Instance.selectedItem.Clear();
//		//sdGameLevel.instance.mainChar.SetItemInfo(sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] as Hashtable);
//	}
	
	public static bool notifyEquipItem(string uuid, int isEquip)
	{
        sdUICharacter.Instance.playerScore = sdConfDataMgr.Instance().PlayerScore();
		CliProto.CS_ROLE_EQUIP_REQ refMsg = new CliProto.CS_ROLE_EQUIP_REQ();
		refMsg.m_ItemUUID = ulong.Parse(uuid);
		refMsg.m_IsEquip = (byte)isEquip;
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}
	
	public static bool notifyProcessItem(string uuid, int type)
	{
		CliProto.CS_ROLE_EVENT_REQ refMsg = new CliProto.CS_ROLE_EVENT_REQ();
		refMsg.m_ItemUUID = ulong.Parse(uuid);
		refMsg.m_EventType = (byte)type;
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}
	
//	public static bool notifyMoveItem(byte From, byte To, string id)
//	{
//		CliProto.CS_MOVE_ITEM_REQ refMSG = new CliProto.CS_MOVE_ITEM_REQ();
//		refMSG.m_FromPos = From;
//		refMSG.m_ToPos = To;
//		refMSG.m_UUID = UInt64.Parse(id);
//		refMSG.m_AnotherUUID = UInt64.MaxValue;
//
//		SDNetGlobal.SendMessage(refMSG);
//		return true;
//	}
//	
	private static void msg_SCID_OPEN_TREASURE_CHEST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_OPEN_TREASURE_CHEST_ACK refMsg = (CliProto.SC_OPEN_TREASURE_CHEST_ACK)msg;
		if (refMsg.m_Result == 1)
		{
			//sdUICharacter.Instance.RemoveTreasureInfo(refMsg.m_ItemIndexID);	
		}
		
	}

	public static bool notifyItemUp(string uuid, ulong[] idList)
	{
        sdGameItemMgr.Instance.tempItem = sdGameItemMgr.Instance.upItem.Clone();
		CliProto.CS_ROLE_ITEMUP_RPT refMsg = new CliProto.CS_ROLE_ITEMUP_RPT();
		refMsg.m_ItemUUID = ulong.Parse(uuid);
		refMsg.m_UpType = 0;
		refMsg.m_Count = (byte)idList.Length;
		refMsg.m_OtherItemUUID = idList;
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}

	public static bool notifyGemOn(int index, string itemId, string uuid)
	{
		CliProto.CS_ROLE_HOLE_ON_REQ refMsg = new CliProto.CS_ROLE_HOLE_ON_REQ();
		refMsg.m_Hole.m_HoleIndex = (byte)index;
		refMsg.m_Hole.m_ItemUUID = ulong.Parse(itemId);
		refMsg.m_Hole.m_OtherUUID = ulong.Parse(uuid);
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}

	private static void msg_SCID_ROLE_HOLE_ON_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_HOLE_ON_ACK refMsg = (CliProto.SC_ROLE_HOLE_ON_ACK)msg;
		if (refMsg.m_Error == 0)
		{
            if (sdUICharacter.Instance.GetItemUpWnd() != null)
            {
                sdUICharacter.Instance.GetItemUpWnd().OnGemOnSuccess((int)refMsg.m_Hole.m_HoleIndex);
            }
		}
	}

	public static bool notifyGemOff(int index, string itemId)
	{
		CliProto.CS_ROLE_HOLE_OFF_REQ refMsg = new CliProto.CS_ROLE_HOLE_OFF_REQ();
		refMsg.m_Hole.m_HoleIndex = (byte)index;
		refMsg.m_Hole.m_ItemUUID = ulong.Parse(itemId);
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}

	private static void msg_SCID_ROLE_HOLE_OFF_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_HOLE_OFF_ACK refMsg = (CliProto.SC_ROLE_HOLE_OFF_ACK)msg;
		if (refMsg.m_Error == 0)
		{
            if (sdUICharacter.Instance.GetItemUpWnd() != null)
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("GemOffSuccess"), Color.yellow);
                sdUICharacter.Instance.GetItemUpWnd().OnGemOffSuccess();
            }
		}
		
	}

	public static bool notifyGemReplace(string uuid1, string uuid2)
	{
		CliProto.CS_ROLE_HOLE_REFRESH_REQ refMsg = new CliProto.CS_ROLE_HOLE_REFRESH_REQ();
		refMsg.m_Hole.m_ItemUUID = ulong.Parse(uuid1);
		refMsg.m_Hole.m_Item2UUID = ulong.Parse(uuid2);
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}

	private static void msg_SCID_ROLE_HOLE_REFRESH_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_HOLE_REFRESH_ACK refMsg = (CliProto.SC_ROLE_HOLE_REFRESH_ACK)msg;
		if (refMsg.m_Error == 0)
		{
            if (refMsg.m_Drops.m_Count <= 0)
            {
                return;
            }

            sdGameItemMgr.Instance.createGemId = (int)refMsg.m_Drops.m_List[0].m_TemplateID;
            if (sdUICharacter.Instance.GetItemUpWnd() != null)
            {
                sdUICharacter.Instance.GetItemUpWnd().OnGemReplaceSuccess();
            } 
		}
		
	}

	public static bool notifyGemMerge(string uuid1, int num)
	{
        CliProto.CS_ROLE_HOLE_MERGE2_REQ refMsg = new CliProto.CS_ROLE_HOLE_MERGE2_REQ();
		refMsg.m_Hole.m_ItemUUID = ulong.Parse(uuid1);
        refMsg.m_Hole.m_Count = num;
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}

	private static void msg_SCID_ROLE_HOLE_MERGE_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ROLE_HOLE_MERGE2_ACK refMsg = (CliProto.SC_ROLE_HOLE_MERGE2_ACK)msg;
		if (refMsg.m_Error == 0)
		{
            if (refMsg.m_Drops.m_Count <= 0)
            {
                return;
            }
            sdGameItemMgr.Instance.createGemId = (int)refMsg.m_Drops.m_List[0].m_TemplateID;
            sdGameItemMgr.Instance.createGemCount = (int)refMsg.m_Drops.m_List[0].m_Count;
            if (sdUICharacter.Instance.GetItemUpWnd() != null)
            {
                sdUICharacter.Instance.GetItemUpWnd().OnGemMergeSuccess();
            }
            
		}
		
	}

    public static bool notifyLockItem(string uuid, int isLock)
    {
        CliProto.CS_LOCK_RPT refMsg = new CliProto.CS_LOCK_RPT();
        refMsg.m_UUID = ulong.Parse(uuid);
        refMsg.m_LockType = (int)HeaderProto.ELockType.ROLE_ITEM_LOCK;
        refMsg.m_Lock = isLock;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
}
