using UnityEngine;
using System.Collections;
using System;

public class sdActGameMsg : UnityEngine.Object
{
	private static bool bNeedShowFirstEnterIcon = true;
	private static UInt64 uuShowID = UInt64.MaxValue;
	public static bool bNeedResetWorldBossHP = false;

	public static bool init()
	{
		SDGlobal.Log("sdActGameMsg.init");
		//触发深渊..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ABYSS_TRIGGER_ACK, msg_SC_ABYSS_TRIGGER_ACK);
		//开启深渊ack..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ABYSS_OPEN_ACK, msg_SC_ABYSS_OPEN_ACK);
		//开启深渊ntf..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ABYSS_OPEN_NTF, msg_SC_ABYSS_OPEN_NTF);
		//可开启的深渊列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_ABYSS_TRIGGER_LIST_ACK, msg_SC_GET_ABYSS_TRIGGER_LIST_ACK);
		//可进入的深渊列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_LIST_ACK, msg_SC_GET_ABYSS_OPEN_LIST_ACK);
		//深渊开启记录列表..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_REC_ACK, msg_SC_GET_ABYSS_OPEN_REC_ACK);
		//请求进入深渊的返回ack..
		//SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_LEVEL_ACK, msg_SC_LEVEL_ACK);
		//收到此协议，可进入深渊副本了..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ENTER_ABYSS_ACK, msg_SC_ENTER_ABYSS_ACK);
		//深渊结束,收到该协议需请求结算..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ABYSS_RUN_NTF, msg_SC_ABYSS_RUN_NTF);
		//返回角色选择界面..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GSID_LEAVEGAME_ACK, msg_SC_LOGOUT);

		//世界BOSS相关协议..
		//世界boss信息刷新反馈..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_INFO_REFRESH_ACK, msg_SC_WB_INFO_REFRESH_ACK);
		//世界boss鼓舞buff反馈..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_ADD_BUF_ACK, msg_SC_WB_ADD_BUF_ACK);
		//世界boss立即复活反馈..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_RELIVE_ACK, msg_SC_WB_RELIVE_ACK);
		//世界boss被杀通知..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_KILLED_NTF, msg_SC_WB_KILLED_NTF);
		//世界boss逃跑通知..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_RUN_NTF, msg_SC_WB_RUN_NTF);
		//进入世界boss反馈..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_ENTER_ACK, msg_SC_WB_ENTER_ACK);
		//世界boss结算反馈..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_WB_RESULT_ACK, msg_SC_WB_RESULT_ACK);

        //战魂试炼aa
        /* 获取战魂试炼基本信息回应 */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_PT_BASEINFO_ACK, msg_SC_GET_PT_BASEINFO_ACK);
        /* 买挑战次数反馈  */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_BUY_PT_TIMES_ACK, msg_SC_BUY_PT_TIMES_ACK);

        return true;
	}
	
	//触发深渊..
	private static void msg_SC_ABYSS_TRIGGER_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ABYSS_TRIGGER_ACK refMsg = (CliProto.SC_ABYSS_TRIGGER_ACK)msg;
		if (refMsg.m_Info.m_IfTrigger==1)
		{
			sdActGameMgr.Instance.SetLapBossLockInfo(refMsg);
			sdActGameMgr.Instance.m_bTiggerLapBossWnd = true;
			sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_LapBoss);
		}
	}
	
	//请求开启深渊..
	public static bool Send_CS_ABYSS_OPEN_REQ(UInt64 abyssDBID)
	{
		CliProto.CS_ABYSS_OPEN_REQ refMSG = new CliProto.CS_ABYSS_OPEN_REQ();
		UInt64 openTime = (UInt64)DateTime.Now.ToFileTime();
		refMSG.m_Info.m_AbyssDBID = abyssDBID;
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//开启深渊ack..
	private static void msg_SC_ABYSS_OPEN_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ABYSS_OPEN_ACK refMsg = (CliProto.SC_ABYSS_OPEN_ACK)msg;
		sdActGameMgr.Instance.SetLapBossEnterAckInfo(refMsg);


		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshLBItemListPage();
				bossWnd.RefreshRKItemListPage();
				bossWnd.RefreshRecordItemListPage();

				bossWnd.SetShowPanelType(1);
				bossWnd.OnActivePnlSetRadioButton();
				sdActGameMsg.Send_CS_GET_ABYSS_OPEN_LIST_REQ();

				bNeedShowFirstEnterIcon = false;
				uuShowID = refMsg.m_Info.m_AbyssInfo.m_ActDBID;
			}
		}
	}
	
	//开启深渊ntf..
	private static void msg_SC_ABYSS_OPEN_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ABYSS_OPEN_NTF refMsg = (CliProto.SC_ABYSS_OPEN_NTF)msg;
		sdActGameMgr.Instance.SetLapBossEnterNtfInfo(refMsg);
		sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_LapBoss);

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshLBItemListPage();
				bossWnd.RefreshRKItemListPage();
				bossWnd.RefreshRecordItemListPage();
			}
		}
	}
	
	//请求可开启的深渊列表..
	public static bool Send_CS_GET_ABYSS_TRIGGER_LIST_REQ()
	{
		CliProto.CS_GET_ABYSS_TRIGGER_LIST_REQ refMSG = new CliProto.CS_GET_ABYSS_TRIGGER_LIST_REQ();

		SDNetGlobal.SendMessage(refMSG);		
		return true;
	}
	
	//可开启的深渊列表ack..
	private static void msg_SC_GET_ABYSS_TRIGGER_LIST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_ABYSS_TRIGGER_LIST_ACK refMsg = (CliProto.SC_GET_ABYSS_TRIGGER_LIST_ACK)msg;
		sdActGameMgr.Instance.ResetLapBossLockInfo(refMsg);

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshLBItemListPage();
				bossWnd.RefreshRKItemListPage();
				bossWnd.RefreshRecordItemListPage();

				bossWnd.SelectFirstLockIcon();
			}
		}
	}
	
	//请求可进入的深渊列表..
	public static bool Send_CS_GET_ABYSS_OPEN_LIST_REQ()
	{
		CliProto.CS_GET_ABYSS_OPEN_LIST_REQ refMSG = new CliProto.CS_GET_ABYSS_OPEN_LIST_REQ();

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//可进入的深渊列表ack..
	private static void msg_SC_GET_ABYSS_OPEN_LIST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_ABYSS_OPEN_LIST_ACK refMsg = (CliProto.SC_GET_ABYSS_OPEN_LIST_ACK)msg;
		sdActGameMgr.Instance.ResetLapBossEnterInfo(refMsg);

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshLBItemListPage();
				bossWnd.RefreshRKItemListPage();
				bossWnd.RefreshRecordItemListPage();

				bossWnd.SelectEnterIcon(bNeedShowFirstEnterIcon, uuShowID);
				bNeedShowFirstEnterIcon = true;
				uuShowID = UInt64.MaxValue;
			}
		}
	}
	
	//请求深渊开启记录列表..
	public static bool Send_CS_GET_ABYSS_OPEN_REC_REQ()
	{
		CliProto.CS_GET_ABYSS_OPEN_REC_REQ refMSG = new CliProto.CS_GET_ABYSS_OPEN_REC_REQ();

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//深渊开启记录列表ack..
	private static void msg_SC_GET_ABYSS_OPEN_REC_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_ABYSS_OPEN_REC_ACK refMsg = (CliProto.SC_GET_ABYSS_OPEN_REC_ACK)msg;
		sdActGameMgr.Instance.ResetLapBossOpenRecordInfo(refMsg);

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshLBItemListPage();
				bossWnd.RefreshRKItemListPage();
				bossWnd.RefreshRecordItemListPage();
			}
		}
	}

	//请求进入深渊的返回ack..
    //private static void msg_SC_LEVEL_ACK(int iMsgID, ref CMessage msg)
    //{
    //    CliProto.SC_LEVEL_ACK refMSG = (CliProto.SC_LEVEL_ACK)msg;

    //    if (refMSG.m_LevelBattleType==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
    //    {
    //        GameObject wnd = sdGameLevel.instance.NGUIRoot;
    //        sdUILapBossWnd bossWnd = null;
    //        if (wnd)
    //            bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
    //        if (bossWnd==null)
    //            return;
			
    //        if(refMSG.m_Result==0)
    //        {
    //            //申请进入场景..
    //            BundleGlobal.SetBundleDontUnload("UI/$FightUI.unity3d");
    //            sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$Fight.prefab", sdUILapBossWndBtn.OnLoadFightUI, null);
    //            bossWnd.setbossmodelVisible(false);
    //            sdUILoading.ActiveLoadingUI("forest","欢迎进入深渊BOSS领地");

    //            bossWnd.m_bTuituAck = true;
    //        }
    //        else
    //        {
    //            sdUILoading.UnactiveLoadingUI();
    //            bossWnd.setbossmodelVisible(true);
    //            SGDP.Error(refMSG.m_Result);
    //            return;
    //        }
			
    //        Debug.Log("tuitu ack");
    //        sdUICharacter.Instance.SetBattleType(refMSG.m_LevelBattleType);
    //        sdUICharacter.Instance.SetFreeMedicineNum((int)refMSG.m_FreePotionCount);
    //        sdUICharacter.Instance.SetFreeReliveNum((int)refMSG.m_FreeReliveCount);
    //        sdUICharacter.Instance.SetMedicinePrice((int)refMSG.m_PotionPrice);
    //        sdUICharacter.Instance.SetRelivePrice((int)refMSG.m_RelivePrice);
			
    //        // 初始化掉落表aa
    //        CliProto.SMonsterInfos kSrcMonsterInfoList = refMSG.m_Monster;
    //        SDGlobal.msMonsterDropTable = new Hashtable();
    //        for(int i = 0; i < kSrcMonsterInfoList.m_Count; i++)
    //        {
    //            CliProto.SMonsterInfo kSrcMonsterInfo = kSrcMonsterInfoList.m_List[i];
    //            CliProto.SDropInfos kSrcMonsterDrops = kSrcMonsterInfo.m_Drop;	
				
    //            SDMonsterDrop kMonsterDropItem = new SDMonsterDrop();
    //            kMonsterDropItem.money = (int)kSrcMonsterDrops.m_Money;
    //            kMonsterDropItem.items = new int[kSrcMonsterDrops.m_Count];
    //            kMonsterDropItem.itemCount = new int[kSrcMonsterDrops.m_Count];
				
    //            CliProto.SDropInfo[] kSrcMonsterDropInfo = kSrcMonsterDrops.m_List;
    //            for(int j = 0; j < kSrcMonsterDrops.m_Count; j++)
    //            {
    //                kMonsterDropItem.items[j] = (int)kSrcMonsterDropInfo[j].m_TemplateID;
    //                kMonsterDropItem.itemCount[j] = kSrcMonsterDropInfo[j].m_Count;
    //            }
				
    //            SDGlobal.msMonsterDropTable[(uint)kSrcMonsterInfo.m_Index] = kMonsterDropItem;
    //        }
			
    //        int iCount = refMSG.m_InitialBuffCount;
    //        if( iCount != 0 )
    //        {
    //            int[] buffArray	=	new int[iCount];
    //            for(int i=0;i<iCount;i++)
    //            {
    //                buffArray[i] = (int)refMSG.m_InitialBuffID[i];
    //            }
    //            sdGlobalDatabase.Instance.globalData["InitBuff"] =	buffArray;
    //        }
    //    }
    //}

	//收到此协议，可进入深渊副本了..
	private static void msg_SC_ENTER_ABYSS_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ENTER_ABYSS_ACK refMsg = (CliProto.SC_ENTER_ABYSS_ACK)msg;

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		sdUILapBossWnd bossWnd = null;
		if (wnd)
			bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
		if (bossWnd==null)
			return;

		if(refMsg.m_Result==(uint)HeaderProto.EReturnType.GS_RESULT_SUCCESS)
		{
			bossWnd.m_bLapBossAck = true;
			sdActGameMgr.Instance.m_uuLapBossLastBlood = (int)refMsg.m_Info.m_AbyssInfo[0].m_Blood;
			sdActGameMgr.Instance.m_uuLapBossNowBlood = (int)refMsg.m_Info.m_AbyssInfo[0].m_Blood;
		}
		else
		{
			SGDP.Error(refMsg.m_Result);
			return;
		}
	}
	
	//深渊结束,收到该协议需请求结算..
	private static void msg_SC_ABYSS_RUN_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ABYSS_RUN_NTF refMsg = (CliProto.SC_ABYSS_RUN_NTF)msg;

		Send_CS_LEVEL_RESULT_NTF();
	}

	//发送结算协议..
	public static void Send_CS_LEVEL_RESULT_NTF()
	{
		sdUICharacter.Instance.oldExp = int.Parse(sdGameLevel.instance.mainChar.Property["Experience"].ToString());
		sdUICharacter.Instance.oldLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
		
		CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
		refMSG.m_Result = 0;
		
		refMSG.m_Money = (uint)SDGlobal.tmpBag.money;
		if(SDGlobal.tmpBag.itemList.Count > 0)
		{
			refMSG.m_Item = new CliProto.SDropInfo[SDGlobal.tmpBag.itemList.Count];
			refMSG.m_ItemCount = (ushort)SDGlobal.tmpBag.itemList.Count;
			
			for(int i = 0; i < refMSG.m_Item.Length; i++)
			{
				refMSG.m_Item[i] = new CliProto.SDropInfo();
				refMSG.m_Item[i].m_TemplateID = (uint)SDGlobal.tmpBag.itemList[i].itemId;
				refMSG.m_Item[i].m_Count = (ushort)SDGlobal.tmpBag.itemList[i].itemCount;
			}
		}

		//refMSG.m_Potion0Count = (ushort)sdUICharacter.Instance.MedicineNum();
		refMSG.m_ReliveCount = (ushort)sdUICharacter.Instance.ReliveNum();

		//深渊结算评级需要特殊处理..
		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
			//消耗Boss的血量..
			refMSG.m_ActivityAbyssTotalDamage = (int)(sdActGameMgr.Instance.m_uuLapBossLastBlood - sdActGameMgr.Instance.m_uuLapBossNowBlood);
			sdUICharacter.Instance.fightScore = (int)sdConfDataMgr.Instance().GetLapBossResult(sdLevelInfo.GetCurLevelId(),
			                                                                                   sdActGameMgr.Instance.m_uuLapBossLastBlood,
			                                                                                   refMSG.m_ActivityAbyssTotalDamage);
		}
		else
		{
			sdUICharacter.Instance.fightScore = (int)sdConfDataMgr.Instance().GetResult(sdLevelInfo.GetCurLevelId(),sdUICharacter.Instance.fightTime);
		}

		refMSG.m_CompleteResult = (byte)sdUICharacter.Instance.fightScore;
		//结算类型..
		refMSG.m_LevelBattleType = sdUICharacter.Instance.GetBattleType();
		//深渊BOSS消耗的血量..
		refMSG.m_ActivityAbyssTotalDamage = (int)(sdActGameMgr.Instance.m_uuLapBossLastBlood - sdActGameMgr.Instance.m_uuLapBossNowBlood);
		
		SDNetGlobal.SendMessage(refMSG);
	}

	public static void msg_SC_LOGOUT(int iMsgID, ref CMessage msg)
	{
        sdConfDataMgr.Instance().skilliconAtlas = null;
        sdConfDataMgr.Instance().jobAtlas = null;
        sdGameSkillMgr.Instance.Clear();
        sdGameItemMgr.Instance.Clear();
        sdPVPManager.Instance.Clear();
		BundleGlobal.Instance.StartLoadBundleLevel("Level/$SelectRole.unity.unity3d","$SelectRole");
	}

	//世界boss信息刷新请求..
	public static bool Send_CS_WB_INFO_REFRESH_REQ()
	{
		CliProto.CS_WB_INFO_REFRESH_REQ refMSG = new CliProto.CS_WB_INFO_REFRESH_REQ();
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//世界boss信息刷新反馈..
	private static void msg_SC_WB_INFO_REFRESH_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_INFO_REFRESH_ACK refMsg = (CliProto.SC_WB_INFO_REFRESH_ACK)msg;
		HeaderProto.SWorldBossInfo info = refMsg.m_Info;
		sdActGameMgr.Instance.BuildWorldBossInfo(info);

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshWorldBossUI();
				bossWnd.RefreshLBItemListPage();
			}
		}
	}

	//世界boss鼓舞buff请求(0金币1徽章)..
	public static bool Send_CS_WB_ADD_BUF_REQ(int iType)
	{
		CliProto.CS_WB_ADD_BUF_REQ refMSG = new CliProto.CS_WB_ADD_BUF_REQ();

		refMSG.m_Type = iType;
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//世界boss鼓舞buff反馈..
	private static void msg_SC_WB_ADD_BUF_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_ADD_BUF_ACK refMsg = (CliProto.SC_WB_ADD_BUF_ACK)msg;

		sdActGameMgr.Instance.m_iWBMyBuff = refMsg.m_Result;
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
			if (bossWnd)
			{
				bossWnd.RefreshWorldBossUI();
			}
		}
	}

	//世界boss立即复活请求..
	public static bool Send_CS_WB_RELIVE_REQ()
	{
		CliProto.CS_WB_RELIVE_REQ refMSG = new CliProto.CS_WB_RELIVE_REQ();

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//世界boss立即复活反馈..
	private static void msg_SC_WB_RELIVE_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_RELIVE_ACK refMsg = (CliProto.SC_WB_RELIVE_ACK)msg;

		if (refMsg.m_Result == 0)
			Send_CS_WB_ENTER_REQ();
	}

	//世界boss被杀通知..
	private static void msg_SC_WB_KILLED_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_KILLED_NTF refMsg = (CliProto.SC_WB_KILLED_NTF)msg;
		Send_CS_WB_RESULT_REQ(0);

		OnWorldBossBeKilled();
	}

	//世界boss逃跑通知..
	private static void msg_SC_WB_RUN_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_RUN_NTF refMsg = (CliProto.SC_WB_RUN_NTF)msg;
		Send_CS_WB_RESULT_REQ(0);

		sdMsgBox.OnConfirm btn_ok = new sdMsgBox.OnConfirm(OnClickOk);
		sdUICharacter.Instance.ShowOkMsg("规定时间内世界Boss未被杀死,现在退出么？",btn_ok);
		Time.timeScale = 0.0f;
	}

	//进入世界boss请求..
	public static bool Send_CS_WB_ENTER_REQ()
	{
		CliProto.CS_WB_ENTER_REQ refMSG = new CliProto.CS_WB_ENTER_REQ();
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//进入世界boss反馈..
	private static void msg_SC_WB_ENTER_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_ENTER_ACK refMsg = (CliProto.SC_WB_ENTER_ACK)msg;

		if (refMsg.m_Result==0)
		{
			HeaderProto.SWorldBossInfo info = refMsg.m_Info;
			sdActGameMgr.Instance.BuildWorldBossInfo(info);

			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
				if (bossWnd)
				{
					bossWnd.RefreshWorldBossUI();
					bossWnd.RefreshLBItemListPage();

					int iLevelID = sdActGameMgr.Instance.GetWorldBossLevelID();
					if (iLevelID>0)
					{
						sdLevelInfo.SetCurLevelId(iLevelID);
						// 通知宠物管理器..
						sdNewPetMgr.Instance.OnEnterLevel();	
						sdUICharacter.Instance.iCurrentLevelID = iLevelID;
						sdUICharacter.Instance.bCampaignLastLevel = false;
						sdUICharacter.Instance.SetBattleType((byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_WORLD_BOSS);
						//申请进入场景..
						BundleGlobal.SetBundleDontUnload("UI/$FightUI.unity3d");
						sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$Fight.prefab", sdUIWorldBossWndBtn.OnLoadFightUI, null);
						sdUILoading.ActiveLoadingUI("cave1","凶恶的世界BOSS");
						bossWnd.setbossmodelVisible(false);
						bossWnd.m_bWorldBossAck = true;
						
						sdActGameMgr.Instance.m_uuWorldBossLastBlood = sdActGameMgr.Instance.m_WorldBossInfo.m_Blood;
						sdActGameMgr.Instance.m_uuWorldBossNowBlood = sdActGameMgr.Instance.m_WorldBossInfo.m_Blood;
					}
				}
			}
		}
	}

	//世界boss请求结算..
	public static bool Send_CS_WB_RESULT_REQ(int iResult)
	{
		CliProto.CS_WB_RESULT_REQ refMSG = new CliProto.CS_WB_RESULT_REQ();

		refMSG.m_Result = iResult;
		refMSG.m_Damage = sdActGameMgr.Instance.m_uuWorldBossLastBlood-sdActGameMgr.Instance.m_uuWorldBossNowBlood;
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}

	//世界boss结算反馈..
	private static void msg_SC_WB_RESULT_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_WB_RESULT_ACK refMsg = (CliProto.SC_WB_RESULT_ACK)msg;

		if (refMsg.m_Result==0)
		{
			HeaderProto.SWorldBossInfo info = refMsg.m_Info;
			sdActGameMgr.Instance.BuildWorldBossInfo(info);

			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
				if (bossWnd)
				{
					bossWnd.RefreshWorldBossUI();
					bossWnd.RefreshLBItemListPage();

					sdActGameMgr.Instance.m_uuWorldBossLastBlood = sdActGameMgr.Instance.m_WorldBossInfo.m_Blood;
					sdActGameMgr.Instance.m_uuWorldBossNowBlood = sdActGameMgr.Instance.m_WorldBossInfo.m_Blood;
					bNeedResetWorldBossHP = true;
				}
			}
		}
	}

	public static void OnWorldBossBeKilled()
	{
		sdMsgBox.OnConfirm btn_ok = new sdMsgBox.OnConfirm(OnClickOk);
		sdUICharacter.Instance.ShowOkMsg("世界Boss已经死亡,现在退出么？",btn_ok);
		Time.timeScale = 0.0f;
	}

	public static void OnClickOk()
	{
		//退出世界Boss场景..
		sdUICharacter.Instance.TuiTu_To_WorldMap();
		Time.timeScale = 1.0f;
	}

    //武魂试炼aaa
    private static void msg_SC_GET_PT_BASEINFO_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_GET_PT_BASEINFO_ACK refMsg = (CliProto.SC_GET_PT_BASEINFO_ACK)msg;
        sdPTManager.Instance.m_Times = (ushort)refMsg.m_Times;
        sdPTManager.Instance.m_PassLevel = (byte)refMsg.m_Pass;
        sdPTManager.Instance.m_BuyTimes = (ushort)refMsg.m_Buys;
        sdPTManager.Instance.Refresh();
    }

    private static void msg_SC_BUY_PT_TIMES_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_BUY_PT_TIMES_ACK refMsg = (CliProto.SC_BUY_PT_TIMES_ACK)msg;
        //sdPTManager.Instance.m_Times += (ushort)refMsg.m_Result;
        sdPTManager.Instance.m_BuyTimes = (ushort)refMsg.m_BuyTimesLeft;
        sdPTManager.Instance.Refresh();
    }
    /* 获取战魂试炼基本信息请求 */
    public static bool Send_CSID_GET_PT_BASEINFO_REQ()
    {
        CliProto.CS_GET_PT_BASEINFO_REQ refMsg = new CliProto.CS_GET_PT_BASEINFO_REQ();
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    /* 买挑战次数请求  */
    public static bool Send_CSID_BUY_PT_TIMES_REQ(int times)
    {
        CliProto.CS_BUY_PT_TIMES_REQ refMsg = new CliProto.CS_BUY_PT_TIMES_REQ();
        refMsg.m_BuyTimes = times;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
}

