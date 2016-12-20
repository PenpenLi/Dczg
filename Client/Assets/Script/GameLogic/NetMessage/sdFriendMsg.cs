using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class sdFriendMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_FRIENDS_LIST_NTF, msg_SCID_FRIENDS_LIST_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_REQUEST_ADD_FRIEND_NTF, msg_SCID_REQUEST_ADD_FRIEND_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ADD_FRIEND_ACK, msg_SCID_ADD_FRIEND_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_REMOVE_FRIEND_ACK, msg_SCID_REMOVE_FRIEND_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_RECEIVE_ACTION_POINT_ACK, msg_SCID_RECEIVE_ACTION_POINT_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_OTHER_NTF, msg_SCID_DONATE_ACTION_POINT_OTHER_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_FRIEND_REMOVE_INVITE_NTF, msg_SCID_FRIEND_REMOVE_INVITE_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_QUERY_ROLE_ACK, msg_SCID_QUERY_ROLE_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_FRIEND_ASSIST_LIST_ACK, msg_SCID_FRIEND_ASSIST_LIST_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELECT_FRIEND_ASSIST_ACK, msg_SCID_SELECT_FRIEND_ASSIST_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_FRIEND_INFO_CHANGE_NTF, msg_SCID_FRIEND_INFO_CHANGE_NTF);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_ACK, msg_SCID_DONATE_ACTION_POINT_ACK);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_FRIEND_EPINFO_NTF, msg_SCID_FRIEND_EPINFO_NTF);
		
        return true;
	}
	
    private static void msg_SCID_FRIEND_EPINFO_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_FRIEND_EPINFO_NTF refMSG = (CliProto.SC_FRIEND_EPINFO_NTF)msg;
        sdFriendMgr.Instance.sendEp = refMSG.m_EPSent;
        sdFriendMgr.Instance.sendEpMax = refMSG.m_EPSendMax;
        sdFriendMgr.Instance.getEp = refMSG.m_EPRecv;
        sdFriendMgr.Instance.getEpMax = refMSG.m_EPRecvMax;
        sdUICharacter.Instance.RefreshFri();
    }

	private static void msg_SCID_FRIENDS_LIST_NTF(int iMsgID, ref CMessage msg)
	{
        sdFriendMgr.Instance.Clear();
		CliProto.SC_FRIENDS_LIST_NTF refMSG = (CliProto.SC_FRIENDS_LIST_NTF)msg;
		int num = (int)refMSG.m_Info.m_Count;
		for (int i = 0; i < num; ++i)
		{
			HeaderProto.SFriendInfo info = refMSG.m_Info.m_List[i];
			sdFriendMgr.Instance.CreateFriend(info);
		}
		
		num = (int)refMSG.m_Info.m_InviteList.m_InviteCount;
		for (int i = 0; i < num; ++i)
		{
			sdFriendMgr.Instance.CreateTempFriend(refMSG.m_Info.m_InviteList.m_InviteList[i].m_Info);	
		}
		if(num > 0)
		{
			sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Friend);	
		}
		
		notifyGetFriAssList();
	}
	
	public static bool notifyQueryRole(string name, int page)
	{
//		if (name == sdFriendMgr.Instance.lastSearchName)
//		{
//			if (page >= sdFriendMgr.Instance.maxPage) return false;
//		}
		
		sdFriendMgr.Instance.lastSearchName = name;
		
		CliProto.CS_QUERY_ROLE_REQ refMSG = new CliProto.CS_QUERY_ROLE_REQ();
		refMSG.m_Info.m_KeyWords = Encoding.UTF8.GetBytes(name);
		refMSG.m_Info.m_Page = (uint)page;

		SDNetGlobal.SendMessage(refMSG);
		sdFriendMgr.Instance.isReceive = false;
		return true;
	}
	
	private static void msg_SCID_QUERY_ROLE_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_QUERY_ROLE_ACK refMSG = (CliProto.SC_QUERY_ROLE_ACK)msg;
		//sdFriendMgr.Instance.curPage = (int)refMSG.m_Info.m_Page;
		//sdFriendMgr.Instance.maxPage = (int)refMSG.m_Info.m_TotalPage;

		int num = (int)refMSG.m_Info.m_Count;
		if (num == 0)
		{
			sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("SearchNoFri"),null);
			return;
		}
		
		for (int i = 0; i < num; ++i)
		{
			HeaderProto.SFriendInfo info = refMSG.m_Info.m_Lists[i];
			sdFriendMgr.Instance.CreateSearchFri(info);
		}
		
		sdFriendMgr.Instance.isReceive = true;
		sdUICharacter.Instance.ShowSearchFri();
	}
	
	public static bool notifyAddFri(string name)
	{
		CliProto.CS_ADD_FRIEND_REQ refMSG = new CliProto.CS_ADD_FRIEND_REQ();
		refMSG.m_Info.m_Name = Encoding.UTF8.GetBytes(name);

		SDNetGlobal.SendMessage(refMSG);
		
		sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("SendFriMsg"), Color.yellow);
		return true;
	}
	
	private static void msg_SCID_ADD_FRIEND_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_ADD_FRIEND_ACK refMSG = (CliProto.SC_ADD_FRIEND_ACK)msg;
		if (refMSG.m_Info.m_Result == 1)
		{
			HeaderProto.SFriendInfo info = refMSG.m_Info.m_Friends[0];
			sdFriendMgr.Instance.CreateFriend(info);
			sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Friend);
		}
		sdUICharacter.Instance.RefreshFriRequest();
		sdUICharacter.Instance.RefreshFri();
	}
	
	public static bool notifyRemoveFri(string id)
	{
		CliProto.CS_REMOVE_FRIEND_REQ refMSG = new CliProto.CS_REMOVE_FRIEND_REQ();
		refMSG.m_Info.m_RoleDBID = ulong.Parse(id);
		sdFriendMgr.Instance.RemoveFriend(id);
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	private static void msg_SCID_REMOVE_FRIEND_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_REMOVE_FRIEND_ACK refMSG = (CliProto.SC_REMOVE_FRIEND_ACK)msg;
		if (refMSG.m_Info.m_Result == 1)
		{
            sdFriendMgr.Instance.RemoveFriend(refMSG.m_Info.m_RoleDBID.ToString());
            sdFriendMgr.Instance.ConfirmRemove();
		}
		sdUICharacter.Instance.RefreshFri();
	}
	
	public static bool notifyReceiveAp(string id)
	{
		CliProto.CS_RECEIVE_ACTION_POINT_REQ refMSG = new CliProto.CS_RECEIVE_ACTION_POINT_REQ();
		refMSG.m_Info.m_RoleDBID = ulong.Parse(id);
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	public static bool notifySendAp(string id)
	{
		CliProto.CS_DONATE_ACTION_POINT_REQ refMSG = new CliProto.CS_DONATE_ACTION_POINT_REQ();
		refMSG.m_Info.m_RoleDBID = ulong.Parse(id);
		SDNetGlobal.SendMessage(refMSG);
		
		sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("SendAPMsg"), null);
		
		return true;
	}
	
	private static void msg_SCID_RECEIVE_ACTION_POINT_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_RECEIVE_ACTION_POINT_ACK refMSG = (CliProto.SC_RECEIVE_ACTION_POINT_ACK)msg;
		if (refMSG.m_Info.m_Result == 1)
		{
            sdFriendMgr.Instance.getEp++;
			sdFriend fri = sdFriendMgr.Instance.GetFriend(refMSG.m_Info.m_RoleDBID.ToString());
			if (fri != null)
			{
				fri.point = 0;	
			}
			
			string str = string.Format(sdConfDataMgr.Instance().GetShowStr("GetAcPoint"), fri.name);
			sdUICharacter.Instance.ShowOkMsg(str, null);
			
			sdUICharacter.Instance.RefreshFri();
		}
	}
	
	private static void msg_SCID_DONATE_ACTION_POINT_OTHER_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_DONATE_ACTION_POINT_OTHER_NTF refMSG = (CliProto.SC_DONATE_ACTION_POINT_OTHER_NTF)msg;
		sdFriend fri = sdFriendMgr.Instance.GetFriend(refMSG.m_Info.m_RoleDBID.ToString());
		if (fri != null)
		{
			fri.point = 1;	
		}
		
		sdUICharacter.Instance.RefreshFri();
	}
	
	private static void msg_SCID_REQUEST_ADD_FRIEND_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_REQUEST_ADD_FRIEND_NTF refMSG = (CliProto.SC_REQUEST_ADD_FRIEND_NTF)msg;
		HeaderProto.SFriendInfo info = refMSG.m_Info.m_NewInfo.m_Info;
		sdFriendMgr.Instance.CreateTempFriend(info);
		sdUICharacter.Instance.RefreshFriRequest();
		sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Friend);
	}
	
	public static bool notifyConfirmAddFri(string id, int flag)
	{
		CliProto.CS_RESPONSE_ADD_FRIEND_REQ refMSG = new CliProto.CS_RESPONSE_ADD_FRIEND_REQ();
		refMSG.m_Info.m_Agree = (byte)flag;
		refMSG.m_Info.m_RoleDBID = ulong.Parse(id);
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	public static void msg_SCID_FRIEND_REMOVE_INVITE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_FRIEND_REMOVE_INVITE_NTF refMSG = (CliProto.SC_FRIEND_REMOVE_INVITE_NTF)msg;
		sdFriendMgr.Instance.RemoveInviteFri(refMSG.m_Info.m_RoleDBID.ToString());
		sdUICharacter.Instance.RefreshFriRequest();
	}
		
	public static void msg_SCID_FRIEND_ASSIST_LIST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_FRIEND_ASSIST_LIST_ACK refMSG = (CliProto.SC_FRIEND_ASSIST_LIST_ACK)msg;
		int num = refMSG.m_Count;
		for(int i = 0; i < num; ++i)
		{
			sdFriendMgr.Instance.CreateFightFri(refMSG.m_Assistors[i]);
		}
	}
	
	public static bool notifyGetFriAssList()
	{
		CliProto.CS_FRIEND_ASSIST_LIST_REQ refMSG = new CliProto.CS_FRIEND_ASSIST_LIST_REQ();
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	public static bool notifySelectFriAss(string id)
	{
		CliProto.CS_SELECT_FRIEND_ASSIST_REQ refMSG = new CliProto.CS_SELECT_FRIEND_ASSIST_REQ();
		refMSG.m_Info.m_RoleDBID = ulong.Parse(id);
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	public static void msg_SCID_SELECT_FRIEND_ASSIST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELECT_FRIEND_ASSIST_ACK refMSG = (CliProto.SC_SELECT_FRIEND_ASSIST_ACK)msg;
		sdFriendMgr.Instance.RemoveFriend(refMSG.m_Info.m_RoleDBID.ToString());
		if (sdFriendMgr.Instance.GetFightList().Count < 15)
		{
			notifyGetFriAssList();	
		}
	}

    public static void msg_SCID_FRIEND_INFO_CHANGE_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_FRIEND_INFO_CHANGE_NTF refMSG = (CliProto.SC_FRIEND_INFO_CHANGE_NTF)msg;

        sdFriend fri = sdFriendMgr.Instance.GetFriend(refMSG.m_Info.m_RoleDBID.ToString());
        if (fri != null)
        {
            if (refMSG.m_Info.m_Info.m_Type == (byte)HeaderProto.RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_NULL)
            {
                return;
            }
            else if (refMSG.m_Info.m_Info.m_Type == (byte)HeaderProto.RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_BASE)
            {
                fri.level = refMSG.m_Info.m_Info.m_Property.m_Base.m_Level.ToString();
                fri.power = refMSG.m_Info.m_Info.m_Property.m_Base.m_Attack.ToString();
            }
            else if (refMSG.m_Info.m_Info.m_Type == (byte)HeaderProto.RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_PET)
            {
                fri.petList.Clear();
                HeaderProto.SRSRolePetData pet = refMSG.m_Info.m_Info.m_Property.m_Pet;
                int petNum = pet.m_Count;
                for (int i = 0; i < petNum; ++i)
                {
                    SClientPetInfo petInfo = new SClientPetInfo();
                    petInfo.m_uiTemplateID = (uint)(pet.m_PetInfo[i].m_TemplateID);
                    fri.petList.Add(petInfo);
                }
            }
            else if (refMSG.m_Info.m_Info.m_Type == (byte)HeaderProto.RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_EQUIP)
            {
                fri.equipList.Clear();
                int itemNum = refMSG.m_Info.m_Info.m_Property.m_Equip.m_Count;
                for (int i = 0; i < itemNum; ++i)
                {
                    fri.equipList.Add((uint)refMSG.m_Info.m_Info.m_Property.m_Equip.m_EquipInfo[i].m_TID);
                }
            }
            else if (refMSG.m_Info.m_Info.m_Type == (byte)HeaderProto.RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_ONLINE)
            {
                fri.isOnline = refMSG.m_Info.m_Info.m_Property.m_Online.m_IsOnline == 1 ? true : false;
            }
            
            sdUICharacter.Instance.RefreshFri();
        }
    }

    public static void msg_SCID_DONATE_ACTION_POINT_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_DONATE_ACTION_POINT_ACK refMSG = (CliProto.SC_DONATE_ACTION_POINT_ACK)msg;
        if (refMSG.m_Info.m_Result == 1)
        {
            sdFriendMgr.Instance.sendEp++;
            sdFriend fri = sdFriendMgr.Instance.GetFriend(refMSG.m_Info.m_RoleDBID.ToString());
            if (fri != null)
            {
                fri.canSend = false;
                sdUICharacter.Instance.RefreshFri();
            }
        }
    }
}
