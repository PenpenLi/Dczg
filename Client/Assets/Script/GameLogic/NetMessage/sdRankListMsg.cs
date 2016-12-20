using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class sdRankLisrMsg : UnityEngine.Object
{
    public static void init()
    {
        SDGlobal.Log("sdRanklistmsg.init");
        /* 申请排名信息回应 */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ROLE_RANK_ACK, msg_SC_ROLE_RANK_ACK);
    }

    private static void msg_SC_ROLE_RANK_ACK(int iMsg, ref CMessage msg)
    {
        CliProto.SC_ROLE_RANK_ACK refMsg = (CliProto.SC_ROLE_RANK_ACK)msg;
        HeaderProto.SRankRoleAck rankRoleAck = refMsg.m_Ack;
        sdRankListMgr.Instance.m_Count = rankRoleAck.m_RankCount;
        sdRankListMgr.Instance.m_TotalCount = rankRoleAck.m_TotalCount;
        sdRankListMgr.Instance.m_SelfRank = rankRoleAck.m_SelfRank;
        sdRankListMgr.Instance.m_Avatar.Clear();
        for (int index = 0; index < rankRoleAck.m_RankCount; ++index)
        {
            HeaderProto.SRoleBaseInfo baseInfo = rankRoleAck.m_Rank[index].m_RoleInfo.m_BaseInfo;
            sdFriend info = new sdFriend();
            info.id = baseInfo.m_RoleDBID.ToString();
            info.gender = baseInfo.m_Sex;
            info.hairStyle = (byte)baseInfo.m_HairStyle;
            info.color = baseInfo.m_SkinColor;
            info.name = Encoding.UTF8.GetString(baseInfo.m_Name).Trim('\0');
            info.level = baseInfo.m_Level.ToString();
            info.job = baseInfo.m_Job.ToString();
            info.power = baseInfo.m_Attack.ToString();
            info.pvpwin = baseInfo.m_PVPWins;
            info.pvprepute = baseInfo.m_PVPRepute;
            info.isOnline = baseInfo.m_IsOnline == 1 ? true : false;
            HeaderProto.SRSRoleEquipData equip = rankRoleAck.m_Rank[index].m_RoleInfo.m_Equip;
            for (int i = 0; i < equip.m_Count; ++i)
            {
                info.equipList.Add((uint)equip.m_EquipInfo[i].m_TID);
            }
            HeaderProto.SRSBattlePets pet = rankRoleAck.m_Rank[index].m_RoleInfo.m_BattlePets;
            for (int j = 0; j < pet.m_Count; ++j)
            {
                SClientPetInfo petInfo = new SClientPetInfo();
                petInfo.m_uiTemplateID = (uint)(pet.m_Pets[j].m_TID);
                petInfo.m_iUp = pet.m_Pets[j].m_UP;
                petInfo.m_iLevel = pet.m_Pets[j].m_LV;
                info.petList.Add(petInfo);
            }
            sdRankListMgr.Instance.m_Avatar.Add(info);
        }
        sdRankListMgr.Instance.Refresh();
    }

    public static bool Send_CS_ROLE_RANK_REQ(UInt64 roleDBID, int rankType, int page)
    {
        sdRankListMgr.Instance.rankType = (HeaderProto.ERankType)rankType;
        CliProto.CS_ROLE_RANK_REQ msg = new CliProto.CS_ROLE_RANK_REQ();
        msg.m_Req.m_RoleDBID = roleDBID;
        msg.m_Req.m_RankType = rankType;
        msg.m_Req.m_Page = page;
        SDNetGlobal.SendMessage(msg);
        return true;
    }
}