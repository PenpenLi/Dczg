using UnityEngine;
using System.Collections;
using System;

public class sdPetMsg : UnityEngine.Object
{
	public static bool bPetLeveling = false;
	public static int m_iPetSaleNum = 0;
	public static bool init()
	{
		SDGlobal.Log("sdPetMsg.init");
		//角色拥有过得宠物模板ID列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PETS_RECORD_NTF, msg_SC_PETS_RECORD_NTF);
		//角色宠物列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_USER_PETS_NTF, msg_SCID_USER_PETS_NTF);
		//宠物属性更新..
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_SINGLE_ENTER_NTF, msg_SCID_SINGLE_ENTER_NTF);
        //宠物属性更新..
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_SINGLE_LEAVE_NTF, msg_SCID_PET_SINGLE_LEAVE_NTF);
		//宠物吃宠物ack..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_GET_EXP_ACK, msg_SC_PET_GET_EXP_ACK);
		//宠物升级..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_LEVEL_NTF, msg_SCID_PET_LEVEL_NTF);
		//宠物强化..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_UP_ACK, msg_SCID_PET_UP_ACK);
		//通知宠物出战..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SET_BATTLE_PET_NTF, msg_SCID_SET_BATTLE_PET_NTF);
		//通知战队切换..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_TEAM_NTF, msg_SC_PET_TEAM_NTF);
		//获得新宠物..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_ADD_NTF, msg_SC_PET_ADD_NTF);
		//宠物装备列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_BAG_NTF, msg_SC_PET_BAG_NTF);
		//宠物装备单件失去..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_LEAVE_NTF, msg_SC_PET_BAG_SINGLE_LEAVE_NTF);
		//宠物装备单件得到或更新..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_ENTER_NTF, msg_SC_PET_BAG_SINGLE_ENTER_NTF);
		//物品碎片列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GATHER_INFO_NTF, msg_SC_GATHER_INFO_NTF);
		//更新单个碎片信息..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GATHER_SINGLE_ENTER_NTF, msg_SC_GATHER_SINGLE_ENTER_NTF);
		//消耗单个碎片..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GATHER_SINGLE_LEAVE_NTF, msg_SC_GATHER_SINGLE_LEAVE_NTF);
		//碎片合成的回应..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GATHER_ITEM_MERGE_ACK, msg_SC_GATHER_ITEM_MERGE_ACK);
		return true;
	}

	//角色拥有过得宠物模板ID列表..
	private static void msg_SC_PETS_RECORD_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PETS_RECORD_NTF refMsg = (CliProto.SC_PETS_RECORD_NTF)msg;
		sdNewPetMgr.Instance.CreatePetRecord(refMsg);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetTujianPnl petPnl = wnd.GetComponentInChildren<sdUIPetTujianPnl>();
			if (petPnl) petPnl.RefreshPetBookPage();
		}
	}
	
	//角色宠物列表..
	private static void msg_SCID_USER_PETS_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_USER_PETS_NTF refMsg = (CliProto.SC_USER_PETS_NTF)msg;
		sdNewPetMgr.Instance.CreatePetInfo(refMsg);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetListPnl petListPnl = wnd.GetComponentInChildren<sdUIPetListPnl>();
			if (petListPnl) petListPnl.RefreshPetListPage();

			sdUIPetSaleSelectPnl petSalePnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
			if (petSalePnl) petSalePnl.UpdatePetSaleSelectList();
		}
	}
	
	//宠物属性更新..
    private static void msg_SCID_SINGLE_ENTER_NTF(int iMsgID, ref CMessage msg)
	{
        CliProto.SC_PET_SINGLE_ENTER_NTF refMsg = (CliProto.SC_PET_SINGLE_ENTER_NTF)msg;
		sdNewPetMgr.Instance.UpdatePetInfo(refMsg);
		
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetListPnl petListPnl = wnd.GetComponentInChildren<sdUIPetListPnl>();
			if (petListPnl) petListPnl.RefreshPetListPage();
			
			sdUIPetPropPnl petPropPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
			if (petPropPnl)
				petPropPnl.ReflashPetPropUI();

//			sdUIPetLevelPnl petLevelPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
//			if (petLevelPnl && bPetLeveling==false)
//			{
//				if (bPetLeveling==false)
//					petLevelPnl.ReflashPetLevelUI();
//				bPetLeveling = false;
//			}
			
			sdUIPetChangeEquipPnl petCEquipPnl = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
			if (petCEquipPnl) petCEquipPnl.ReflashPetEquipUI();
		}
	}

	//宠物单个失去..
    public static void msg_SCID_PET_SINGLE_LEAVE_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_PET_SINGLE_LEAVE_NTF refMsg = (CliProto.SC_PET_SINGLE_LEAVE_NTF)msg;
        sdNewPetMgr.Instance.DeletePetInfo(refMsg);
        GameObject wnd = sdGameLevel.instance.NGUIRoot;
        if (wnd)
        {
			if (bIsSalePet==false)
			{
				sdUIPetListPnl petListPnl = wnd.GetComponentInChildren<sdUIPetListPnl>();
				if (petListPnl) petListPnl.RefreshPetListPage();
				
				sdUIPetSaleSelectPnl petSalePnl = wnd.GetComponentInChildren<sdUIPetSaleSelectPnl>();
				if (petSalePnl) petSalePnl.UpdatePetSaleSelectList();
			}
			else
			{
				if (m_iPetSaleNum>3)
				{
					m_iPetSaleNum--;
				}
				else
				{
					bIsSalePet = false;
					m_iPetSaleNum = 0;
					sdUICharacter.Instance.ShowMsgLine("战魂出售成功",MSGCOLOR.Yellow);
				}
			}
        }
    }
	
	//宠物经验获取..
	public static bool Send_CS_PET_GET_EXP_RPT(UInt64 ownerDBID)
	{
		CliProto.CS_PET_GET_EXP_RPT refMSG = new CliProto.CS_PET_GET_EXP_RPT();
		refMSG.m_OwnerDBID = ownerDBID;
		
		int iCount = 0;
		for (int x = 0; x < 8; ++x)
		{
			if (sdNewPetMgr.Instance.m_uuPetLevelSelectID[x]!=UInt64.MaxValue && ownerDBID!=sdNewPetMgr.Instance.m_uuPetLevelSelectID[x])
			{
				refMSG.m_OtherDBID[iCount] = sdNewPetMgr.Instance.m_uuPetLevelSelectID[x];
				iCount++;
			}
		}
		
		if (iCount>0)
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petLevelPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petLevelPnl)
				{
					//停止闪烁..
					petLevelPnl.m_iFlashBz = 2;
					//设置升级特效的老属性值..
					petLevelPnl.SetFirstLevelUpValue();
					bPetLeveling = true;
				}
			}

			refMSG.m_OtherCount = iCount;
			SDNetGlobal.SendMessage(refMSG);
			return true;
		}
		
		return false;
	}

	private static void msg_SC_PET_GET_EXP_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_GET_EXP_ACK refMsg = (CliProto.SC_PET_GET_EXP_ACK)msg;

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetLevelPnl petLevelPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
			if (petLevelPnl)
				petLevelPnl.m_iLevelUpingSetp = 1;
		}

		bPetLeveling = false;
	}
	
	//宠物升级..
	private static void msg_SCID_PET_LEVEL_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_LEVEL_NTF refMsg = (CliProto.SC_PET_LEVEL_NTF)msg;
		sdNewPetMgr.Instance.UpdatePetLevel((UInt64)refMsg.m_DBID, refMsg.m_Level);
	}
	
	//宠物强化..
	public static bool Send_CS_PET_UP_REQ(UInt64 ownerDBID, UInt64 otherDBID)
	{
		CliProto.CS_PET_UP_REQ refMSG = new CliProto.CS_PET_UP_REQ();
		refMSG.m_OwnerDBID = ownerDBID;
		refMSG.m_OtherDBID = otherDBID;

		SDNetGlobal.SendMessage(refMSG);
		
		return true;
	}
	
	//宠物强化结果..
	private static void msg_SCID_PET_UP_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_UP_ACK refMsg = (CliProto.SC_PET_UP_ACK)msg;
		if (refMsg.m_Ok==1)
		{
			sdNewPetMgr.Instance.UpdatePetUp((UInt64)refMsg.m_DBID, refMsg.m_Up);

			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetPropPnl petPropPnl = wnd.GetComponentInChildren<sdUIPetPropPnl>();
				if (petPropPnl)
					petPropPnl.ReflashPetPropUI();

				sdUIPetStrongPnl petStrongPnl = wnd.GetComponentInChildren<sdUIPetStrongPnl>();
				if (petStrongPnl)
				{
					petStrongPnl.ReflashPetStrongIcon();
					petStrongPnl.OnStrongSuccess();
				}
			}
		}
	}
	
	//设置出战宠物..
	public static bool Send_CS_SET_BATTLE_PET_REQ(int iBattlePos, UInt64 uuDBID)
	{
		CliProto.CS_SET_BATTLE_PET_REQ refMSG = new CliProto.CS_SET_BATTLE_PET_REQ();
		refMSG.m_BattlePos = iBattlePos;
		refMSG.m_DBID = uuDBID;
		refMSG.m_Ok = 1;
		//当前位置的宠物与选择宠物相同，取消出战..
		UInt64 curDBID = sdNewPetMgr.Instance.mPetAllTeam[iBattlePos];
		if (uuDBID==curDBID)
			refMSG.m_Ok = 0;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//通知宠物出战..
	private static void msg_SCID_SET_BATTLE_PET_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SET_BATTLE_PET_NTF refMsg = (CliProto.SC_SET_BATTLE_PET_NTF)msg;
		UInt64 uuDBID = refMsg.m_DBID;
		//-1表示不出战，0队长位，1,2出战位，4,5,6,7助战位..
		int iPos = refMsg.m_BattlePos;
		
		sdNewPetMgr.Instance.SetPetBattlePos(uuDBID, iPos);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
			if (petWarPnl)
			{
				petWarPnl.ReflashPetBattleTeam();
				petWarPnl.RefreshPetZuhePage();
			}
		}
	}
	
	//切换宠物参战组..
	public static bool Send_CS_PET_TEAM_RPT(int iTeam)
	{
		if (iTeam<0&&iTeam>2)
			return false;
		
		CliProto.CS_PET_TEAM_RPT refMSG = new CliProto.CS_PET_TEAM_RPT();
		refMSG.m_Team = iTeam;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//切换宠物参战组回应..
	private static void msg_SC_PET_TEAM_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_TEAM_NTF refMsg = (CliProto.SC_PET_TEAM_NTF)msg;
		sdNewPetMgr.Instance.mPetCurTeam = refMsg.m_Team;
		for (int i=0; i<21; i++)
		{
			sdNewPetMgr.Instance.mPetAllTeam[i] = refMsg.m_PetUUID[i];
		}
		sdNewPetMgr.Instance.SetCurBattleTeamPetDBID();
		
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
			if (petWarPnl)
			{
				petWarPnl.ReflashPetBattleTeam();
				petWarPnl.RefreshPetZuhePage();
			}

			// 需要通知关卡选择界面，切换宠物战队成功..
			GameObject lp = GameObject.Find("$LevelPrepareWnd(Clone)");
			if( lp != null )
			{
				ReadyButton readyBtn = lp.transform.FindChild("PrepareWnd/bt_ok").GetComponent<ReadyButton>();
				readyBtn.OnChangePetTeam();
			}
		}
	}
	
	//宠物融合..
	public static bool bPetMerging = false;
	public static bool Send_CS_PET_MERGE_REQ(UInt64 ownerDBID, UInt64 otherDBID)
	{
		CliProto.CS_PET_MERGE_REQ refMSG = new CliProto.CS_PET_MERGE_REQ();
		refMSG.m_OwnerDBID = ownerDBID;
		refMSG.m_OtherDBID = otherDBID;

		SDNetGlobal.SendMessage(refMSG);
		bPetMerging = true;
		return true;
	}
	
	//获得宠物..
	private static void msg_SC_PET_ADD_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_ADD_NTF refMsg = (CliProto.SC_PET_ADD_NTF)msg;
		
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetRonghePnl petPnl = wnd.GetComponentInChildren<sdUIPetRonghePnl>();
			if (petPnl!=null && bPetMerging==true)
			{
				petPnl.m_bBeginRonghe = true;
				petPnl.m_iPetIDNew = refMsg.m_TemplateID;
				petPnl.m_uuPetID0 = UInt64.MaxValue;
				petPnl.m_uuPetID1 = UInt64.MaxValue;
				petPnl.LoadRongheEffect();
				bPetMerging = false;
			}
		}
		
		sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Pet);
	}
	
	//宠物物品列表..
	private static void msg_SC_PET_BAG_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_BAG_NTF refMsg = (CliProto.SC_PET_BAG_NTF)msg;
		SDGlobal.Log("<- SCID_PET_BAG_NTF : ");
		
		sdNewPetMgr.Instance.petItemDB.Clear();
		int count = refMsg.m_Items.m_ItemCount;
		for(int i = 0; i < count; ++i)
		{
			sdNewPetMgr.Instance.createItem((int)refMsg.m_Items.m_Items[i].m_TID, (UInt64)refMsg.m_Items.m_Items[i].m_UID, (int)refMsg.m_Items.m_Items[i].m_CT);
		}
		
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetEquipPnl petEquipPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
			if (petEquipPnl)
			{
				petEquipPnl.RefreshPetItemListPage();
			}
			
			sdUIPetChangeEquipPnl petEquipPnl2 = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
			if (petEquipPnl2)
			{
				petEquipPnl2.RefreshPetItemListPage();
			}
		}
	}
	
	//宠物物品单件失去..
	private static void msg_SC_PET_BAG_SINGLE_LEAVE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_BAG_SINGLE_LEAVE_NTF refMsg = (CliProto.SC_PET_BAG_SINGLE_LEAVE_NTF)msg;
		SDGlobal.Log("<- SCID_PET_BAG_SINGLE_LEAVE_NTF : ");
		
		sdNewPetMgr.Instance.removePetItem(refMsg.m_ItemUUID);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetEquipPnl petEquipPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
			if (petEquipPnl)
			{
				petEquipPnl.RefreshPetItemListPage();
			}
			
			sdUIPetChangeEquipPnl petEquipPnl2 = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
			if (petEquipPnl2)
			{
				petEquipPnl2.RefreshPetItemListPage();
			}
		}
	}
	
	//宠物物品单件得到或者更新属性..
	private static void msg_SC_PET_BAG_SINGLE_ENTER_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PET_BAG_SINGLE_ENTER_NTF refMsg = (CliProto.SC_PET_BAG_SINGLE_ENTER_NTF)msg;
		SDGlobal.Log("<- SCID_PET_BAG_SINGLE_ENTER_NTF : ");
		
		sdNewPetMgr.Instance.addOrUpdatePetItem(refMsg.m_Item.m_TID, refMsg.m_Item.m_UID, refMsg.m_Item.m_CT);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetEquipPnl petEquipPnl = wnd.GetComponentInChildren<sdUIPetEquipPnl>();
			if (petEquipPnl)
			{
				petEquipPnl.RefreshPetItemListPage();
			}
			
			sdUIPetChangeEquipPnl petEquipPnl2 = wnd.GetComponentInChildren<sdUIPetChangeEquipPnl>();
			if (petEquipPnl2)
			{
				petEquipPnl2.RefreshPetItemListPage();
			}
		}
	}
	
	//宠物脱穿装备1装，0脱..
	public static bool Send_CS_PET_EQUIP_REQ(UInt64 itemDBID, UInt64 petDBID, byte byEquip)
	{
		CliProto.CS_PET_EQUIP_REQ refMSG = new CliProto.CS_PET_EQUIP_REQ();
		refMSG.m_ItemUUID = itemDBID;
		refMSG.m_PetUUID = petDBID;
		refMSG.m_IsEquip = byEquip;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//处理宠物，宠物装备(0宠物装备卖掉，1宠物卖掉)..
	public static bool bIsSalePet = false;
	public static bool Send_CS_PET_EVENT_REQ(byte byEventType, UInt64 itemDBID)
	{
		if (byEventType==(byte)HeaderProto.EPetEvent.PET_EVENT_SELL)
			bIsSalePet = true;

		CliProto.CS_PET_EVENT_REQ refMSG = new CliProto.CS_PET_EVENT_REQ();
		refMSG.m_EventType = byEventType;
        refMSG.m_UUID = itemDBID;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//宠物上锁请求..
	public static bool Send_CS_LOCK_RPT(byte byLockType, UInt64 uuDBID, int iLock)
	{
		CliProto.CS_LOCK_RPT refMSG = new CliProto.CS_LOCK_RPT();
		refMSG.m_LockType = byLockType;
		refMSG.m_UUID = uuDBID;
		refMSG.m_Lock = iLock;
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//物品碎片列表..
	private static void msg_SC_GATHER_INFO_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GATHER_INFO_NTF refMsg = (CliProto.SC_GATHER_INFO_NTF)msg;
		SDGlobal.Log("<- SCID_GATHER_INFO_NTF : ");
		sdNewPetMgr.Instance.CreatePetGatherList(refMsg);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetTujianPnl petPnl = wnd.GetComponentInChildren<sdUIPetTujianPnl>();
			if (petPnl) petPnl.RefreshPetBookPage();

			sdUIPetPropPnl petProp = wnd.GetComponentInChildren<sdUIPetPropPnl>();
			if (petProp) petProp.ReflashGatherUI();

			sdUIPetPaperPnl petPaper = wnd.GetComponentInChildren<sdUIPetPaperPnl>();
			if (petPaper) petPaper.RefreshPetPaperPage();
		}
	}

	//更新单个碎片信息..
	private static void msg_SC_GATHER_SINGLE_ENTER_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GATHER_SINGLE_ENTER_NTF refMsg = (CliProto.SC_GATHER_SINGLE_ENTER_NTF)msg;
		sdNewPetMgr.Instance.addOrUpdatePetGatherItem(refMsg.m_ID, refMsg.m_CT);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetTujianPnl petPnl = wnd.GetComponentInChildren<sdUIPetTujianPnl>();
			if (petPnl) petPnl.RefreshPetBookPage();

			sdUIPetPropPnl petProp = wnd.GetComponentInChildren<sdUIPetPropPnl>();
			if (petProp) petProp.ReflashGatherUI();

			sdUIPetPaperPnl petPaper = wnd.GetComponentInChildren<sdUIPetPaperPnl>();
			if (petPaper) petPaper.RefreshPetPaperPage();
		}
	}

	//消耗单个碎片..
	private static void msg_SC_GATHER_SINGLE_LEAVE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GATHER_SINGLE_LEAVE_NTF refMsg = (CliProto.SC_GATHER_SINGLE_LEAVE_NTF)msg;
		sdNewPetMgr.Instance.removePetGatherItem(refMsg.m_ID);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetTujianPnl petPnl = wnd.GetComponentInChildren<sdUIPetTujianPnl>();
			if (petPnl) petPnl.RefreshPetBookPage();

			sdUIPetPropPnl petProp = wnd.GetComponentInChildren<sdUIPetPropPnl>();
			if (petProp) petProp.ReflashGatherUI();

			sdUIPetPaperPnl petPaper = wnd.GetComponentInChildren<sdUIPetPaperPnl>();
			if (petPaper) petPaper.RefreshPetPaperPage();
		}
	}

	//碎片合成请求..
	public static bool Send_CS_GATHER_ITEM_MERGE_REQ(int iGId)
	{
		CliProto.CS_GATHER_ITEM_MERGE_REQ refMSG = new CliProto.CS_GATHER_ITEM_MERGE_REQ();
		refMSG.m_TID = iGId;
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//碎片合成的回应..
	private static void msg_SC_GATHER_ITEM_MERGE_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GATHER_ITEM_MERGE_ACK refMsg = (CliProto.SC_GATHER_ITEM_MERGE_ACK)msg;
		Hashtable info = sdConfDataMgr.Instance().GetItemById(refMsg.m_TID.ToString());
		if (info != null)
		{
			int iPetID = int.Parse(info["GatherItemID"].ToString());
			Hashtable infoPet = sdConfDataMgr.Instance().GetItemById(iPetID.ToString());
			if (infoPet != null)
			{
				string strName = "碎片合成成功，你获得了 " + infoPet["ShowName"].ToString();
				sdUICharacter.Instance.ShowOkMsg(strName, null);
			}
		}
	}
}

