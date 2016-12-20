using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class sdPVPMsg : UnityEngine.Object
{

    public static int ms_Index;
	public static bool init()
	{
		SDGlobal.Log("sdPVPMsg.init");
		/* 角色PVP信息  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_PVP_PRO_ACK, msg_SC_SELF_PVP_PRO_ACK);
		/* 获取pvp对手回应  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_PVP_MATCH_ACK, msg_SC_GET_PVP_MATCH_ACK);
		/* 挑战反馈  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ENTER_PVP_ACK, msg_SC_ENTER_PVP_ACK);
		/* PVP结算反馈  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PVP_RETULT_ACK, msg_SC_PVP_RESULT_ACK);
		/* 获取军阶奖励反馈  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_PVP_MILITARY_REWARD_ACK, msg_SC_GET_PVP_MILITARY_REWARD_ACK);
		/* 获取声望奖励反馈  */
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_PVP_REPUTE_REWARD_ACK, msg_SC_GET_PVP_RETURN_REWARD_ACK);
        /* 获取排行榜反馈  */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_RANK_LIST_ACK, msg_SC_GET_RANK_LIST_ACK);
        /* 买挑战次数反馈  */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PVP_BUY_CHALLENGE_TIMES_ACK, msg_SC_PVP_BUY_CHALLENGE_TIMES_ACK);
        /* 通知pvp时间  */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_PVP_TIME_NTF, msg_SC_PVP_TIME_ACK);
        /* 获取PVP角色信息请求  */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GET_PVP_ROLE_INFO_ACK, msg_SC_GET_PVP_ROLE_INFO_ACK);
        return true;
	}

	public	int              m_MilitaryLevel;	/// 军阶等级         .
	public	uint             m_Repute;	/// 声望             .
	public	uint             m_Points;	/// 积分             .
	public	uint             m_Times;
	public	uint             m_Wins;
	public	uint             m_Loses;
	public	uint             m_Ranks;


    private static void MilitaryLevelUp(int last, int now)
    {
        if (now > last && last > 0)
            sdPVPManager.Instance.m_bMilitaryLevelUp = true;
    }
    /* 获取PVP角色信息请求  */
    private static void msg_SC_GET_PVP_ROLE_INFO_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_GET_PVP_ROLE_INFO_ACK refMsg = (CliProto.SC_GET_PVP_ROLE_INFO_ACK)msg;
        if(refMsg.m_Result != 0)
            return;
        List<stPVPRival> rivalList = sdPVPManager.Instance.GetRanklist();
        if(ms_Index >=0 && ms_Index < rivalList.Count)
        {
            stPVPRival rival = rivalList[ms_Index];
            sdFriend roleInfo = new sdFriend();
            roleInfo.id = rival.roleID.ToString();
            roleInfo.gender = (byte)rival.nSex;
            roleInfo.hairStyle = (byte)rival.hairstyle;
            roleInfo.color = (byte)rival.haircolor;
            roleInfo.name = rival.strName;
            roleInfo.level = rival.nLevel.ToString();
            roleInfo.job = rival.nProfession.ToString();
            roleInfo.power = refMsg.m_Attack.ToString();

            for(int index = 0; index < refMsg.m_Equip.m_Items.m_ItemCount; ++index)
                roleInfo.equipList.Add((uint)refMsg.m_Equip.m_Items.m_Items[index].m_TID);
            for (int i = 0; i < refMsg.m_Pet.m_PetCount; ++i)
            {
                SClientPetInfo petInfo = new SClientPetInfo();
                petInfo.m_uiTemplateID = refMsg.m_Pet.m_PetsInfo[i].m_TemplateID;
                petInfo.m_iLevel = refMsg.m_Pet.m_PetsInfo[i].m_Level;
                petInfo.m_iUp = refMsg.m_Pet.m_PetsInfo[i].m_Up;
                roleInfo.petList.Add(petInfo);
            }
            sdUICharacter.Instance.ShowRoleTipWnd(roleInfo, true, 1);
        }
    }
	/* 角色PVP信息  */
	private static void msg_SC_SELF_PVP_PRO_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELF_PVP_PRO_ACK refMsg = (CliProto.SC_SELF_PVP_PRO_ACK)msg;
        MilitaryLevelUp(sdPVPManager.Instance.nMilitaryLevel, refMsg.m_MilitaryLevel);
 		sdPVPManager.Instance.nMilitaryLevel = refMsg.m_MilitaryLevel;
        sdPVPManager.Instance.nReputation = refMsg.m_Repute;
        sdPVPManager.Instance.nScore = refMsg.m_Points;
        sdPVPManager.Instance.nChallenge = refMsg.m_Times;
        sdPVPManager.Instance.nWin = refMsg.m_Wins;
        sdPVPManager.Instance.nLose = refMsg.m_Loses;
        sdPVPManager.Instance.nRank = refMsg.m_Ranks;
        sdPVPManager.Instance.mRewardFlag = (uint)refMsg.m_DayReFlag;
        sdPVPManager.Instance.nDayReputation = refMsg.m_DayRepute;
        sdPVPManager.Instance.mMilitaryRewards = (refMsg.m_MiFlag == 1);
        sdPVPManager.Instance.m_ChallengeBuyLeft = refMsg.m_BuyTimesLeft;
        sdPVPManager.Instance.RefreshPK();
	}
	/* 获取pvp对手回应  */
	private static void msg_SC_GET_PVP_MATCH_ACK(int iMsgID, ref CMessage msg)
	{
        sdPVPManager.Instance.m_bPVPSuspend = false;
        sdPVPManager.Instance.ClearRival();
		CliProto.SC_GET_PVP_MATCH_ACK refMsg = (CliProto.SC_GET_PVP_MATCH_ACK)msg;
        HeaderProto.PVP_MATCH_LIST pvpList = refMsg.m_PVPMatchList;
        for (int index = 0; index < pvpList.m_Count; ++index)
        {
            HeaderProto.PVP_MATCH match =  pvpList.m_PVPMatchList[index];
            stPVPRival rival = new stPVPRival();
            rival.strName = System.Text.Encoding.UTF8.GetString(match.m_RoleName).Trim('\0');
            rival.nLevel = (uint)match.m_RoleLevel;
            rival.nMilitaryLevel = match.m_MilitaryLevel;
            rival.nScore = (uint)match.m_Points;
            rival.nSex = (int)match.m_Sex;
            rival.hairstyle = (int)match.m_HairStyle;
            rival.haircolor = (int)match.m_HairColor;
            rival.roleID = match.m_Roleid;
            rival.nProfession = (int)match.m_BaseJob;
            sdPVPManager.Instance.AddRival(rival);
        }
        sdPVPManager.Instance.RefreshPK();
	}
	/* 挑战反馈  */
	private static void msg_SC_ENTER_PVP_ACK(int iMsgID, ref CMessage msg)
	{
		// 保存PVP角色信息aa
		CliProto.SC_ENTER_PVP_ACK refMsg = (CliProto.SC_ENTER_PVP_ACK)msg;
		sdPVPManager.Instance.SetPVPRoleInfo(refMsg);

		// 加载场景aa
        if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.PVP)
        {
            sdUILoading.ActiveLoadingUI(0);
        }
        BundleGlobal.Instance.StartLoadBundleLevel("Level/guidemap/Abattoir/$Abattoir_1.unity.unity3d", "$Abattoir_1");
        
		// 通知宠物管理器aa
        sdNewPetMgr.Instance.OnEnterLevel();
    } 
	/* PVP结算反馈  */
	private static void msg_SC_PVP_RESULT_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_PVP_RETULT_ACK refMsg = (CliProto.SC_PVP_RETULT_ACK)msg;        
        CliProto.SC_SELF_PVP_PRO_ACK refMsg2 = (CliProto.SC_SELF_PVP_PRO_ACK)refMsg.m_Pro;
        sdPVPManager.Instance.mbWin = (refMsg.m_Result == 0);
        MilitaryLevelUp(sdPVPManager.Instance.nMilitaryLevel, refMsg2.m_MilitaryLevel);
        sdPVPManager.Instance.nMilitaryLevel = refMsg2.m_MilitaryLevel;
        sdPVPManager.Instance.nReputation = refMsg2.m_Repute;
        sdPVPManager.Instance.nScore = refMsg2.m_Points;
        sdPVPManager.Instance.nChallenge = refMsg2.m_Times;
        sdPVPManager.Instance.nWin = refMsg2.m_Wins;
        sdPVPManager.Instance.nLose = refMsg2.m_Loses;
        sdPVPManager.Instance.nRank = refMsg2.m_Ranks;
        sdPVPManager.Instance.m_ChallengeBuyLeft = refMsg2.m_BuyTimesLeft;
        sdPVPManager.Instance.nJiesuan_reputation = (uint)refMsg.m_Repute;
        sdPVPManager.Instance.nJiesuan_score = refMsg.m_Points;
        sdPVPManager.Instance.mbWin = refMsg.m_Points < 0 ? false : true;
        sdUICharacter.Instance.ShowPVPJiesuanWnd();
    } 
	/* 获取军阶奖励反馈  */
	private static void msg_SC_GET_PVP_MILITARY_REWARD_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_PVP_MILITARY_REWARD_ACK refMsg = (CliProto.SC_GET_PVP_MILITARY_REWARD_ACK)msg;
        sdPVPManager.Instance.mMilitaryRewards = (refMsg.m_Result == 0);
        sdPVPManager.Instance.RefreshPK();
	} 
	/* 获取声望奖励反馈  */
	private static void msg_SC_GET_PVP_RETURN_REWARD_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_PVP_REPUTE_REWARD_ACK refMsg = (CliProto.SC_GET_PVP_REPUTE_REWARD_ACK)msg;
        if (refMsg.m_Result == 0)//播放成功特效aaa
            sdUICharacter.Instance.ShowSuccessPanel();
        sdPVPManager.Instance.mRewardFlag = (uint)(refMsg.m_DayReFlag);
        sdPVPManager.Instance.RefreshReward();
	}

    private static void msg_SC_GET_RANK_LIST_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_GET_RANK_LIST_ACK refMsg = (CliProto.SC_GET_RANK_LIST_ACK)msg;
        sdPVPManager.Instance.ClearRank();
        for (int i = 0; i < refMsg.m_RankList.m_Count; ++i)
        {
            HeaderProto.PVP_RANK item = refMsg.m_RankList.m_PVPRankList[i];
            stPVPRival rank = new stPVPRival();
            rank.strName = System.Text.Encoding.UTF8.GetString(item.m_RoleName).Trim('\0');
            rank.nLevel = (uint)item.m_RoleLevel;
            rank.nMilitaryLevel = item.m_MilitaryLevel;
            rank.nScore = (uint)item.m_Points;
            rank.nProfession = item.m_BaseJob;
            rank.nSex = item.m_Sex;
            rank.hairstyle = item.m_HairStyle;
            rank.haircolor = item.m_HairColor;
            rank.roleID = item.m_Roleid;
            sdPVPManager.Instance.AddRank(rank);
        }
        sdPVPManager.Instance.RefreshRankList();        
    }

    private static void msg_SC_PVP_BUY_CHALLENGE_TIMES_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_PVP_BUY_CHALLENGE_TIMES_ACK refMsg = (CliProto.SC_PVP_BUY_CHALLENGE_TIMES_ACK)msg;
        sdPVPManager.Instance.m_ChallengeBuyLeft = refMsg.m_BuyTimesLeft;
        sdPVPManager.Instance.nChallenge += refMsg.m_Result;
    }

    private static void msg_SC_PVP_TIME_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_PVP_TIME_NTF refMsg = (CliProto.SC_PVP_TIME_NTF)msg;
        sdPVPManager.Instance.m_bPVPSuspend = true;
        sdPVPManager.Instance.m_nTimeTick = (long)refMsg.m_Time / 1000 + System.DateTime.Now.Ticks / 10000000L;
        sdPVPManager.Instance.RefreshPK();
    }

    public static bool Send_GET_PVP_ROLE_INFO_REQ(ulong roleID)
    {
        CliProto.CS_GET_PVP_ROLE_INFO_REQ refMsg = new CliProto.CS_GET_PVP_ROLE_INFO_REQ();
        refMsg.m_RoleID = roleID;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }

    /* 角色PVP信息  */
	public static bool Send_CSID_SELF_PVP_PRO_REQ()
	{
		CliProto.CS_SELF_PVP_PRO_REQ refMsg = new CliProto.CS_SELF_PVP_PRO_REQ();
		SDNetGlobal.SendMessage(refMsg);
		return true;
	}
    /* 获取pvp对手请求  */
    public static bool Send_CSID_GET_PVP_MATCH_REQ()
    {
        CliProto.CS_GET_PVP_MATCH_REQ refMsg = new CliProto.CS_GET_PVP_MATCH_REQ();
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    /* 挑战请求  */
    public static bool Send_CSID_ENTER_PVP_REQ(ulong roleID)
    {
        CliProto.CS_ENTER_PVP_REQ refMsg = new CliProto.CS_ENTER_PVP_REQ();
        refMsg.m_RoleID = roleID;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    /* PVP结算请求  */
    public static bool Send_CSID_PVP_RETULT_REQ(uint result, int timeout)
    {
        CliProto.CS_PVP_RETULT_REQ refMsg = new CliProto.CS_PVP_RETULT_REQ();
        refMsg.m_Result = result;
        refMsg.m_Timeout = timeout;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    /* 获取军阶奖励请求  */
    public static bool Send_CSID_GET_PVP_MILITARY_REWARD_REQ()
    {
        CliProto.CS_GET_PVP_MILITARY_REWARD_REQ refMsg = new CliProto.CS_GET_PVP_MILITARY_REWARD_REQ();
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    /* 获取声望奖励请求  */
    public static bool Send_CS_GET_PVP_REPUTE_REWARD_REQ(uint rewardID)
    {
        CliProto.CS_GET_PVP_REPUTE_REWARD_REQ refMsg = new CliProto.CS_GET_PVP_REPUTE_REWARD_REQ();
        refMsg.m_RewardId = rewardID;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }

    /* 获取排行榜请求  */
    public static bool Send_CS_GET_RANK_LIST_REQ()
    {
        CliProto.CS_GET_RANK_LIST_REQ refMsg = new CliProto.CS_GET_RANK_LIST_REQ();
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
    public static bool Send_CS_PVP_BUY_CHALLENGE_TIMES_REQ(int times)
    {
        CliProto.CS_PVP_BUY_CHALLENGE_TIMES_REQ refMsg = new CliProto.CS_PVP_BUY_CHALLENGE_TIMES_REQ();
        refMsg.m_BuyTimes = times;
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }
}
