using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 处理多角色同步消息
/// </summary>
public class sdActorMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDGlobal.Log("sdRoleMsg.init");
		
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_OTHER_APPEAR, msg_SCID_OTHER_APPEAR);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_OTHER_DISAPPEAR, msg_SCID_OTHER_DISAPPEAR);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_OTHER_UPDATE, msg_SCID_OTHER_UPDATE);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_MOVE_BEGIN, msg_SCID_MOVE_BEGIN);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_MOVE_STOP, msg_SCID_MOVE_STOP);
	
		return true;
	}
	
	private static void msg_SCID_OTHER_APPEAR(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_OTHER_APPEAR refMSG = (CliProto.SC_OTHER_APPEAR)msg;
		if(refMSG == null)
			return;
			
		sdGameLevel gameLevel = sdGameLevel.instance;
        if (gameLevel == null)
        {
            Debug.Log("No gamelevel");
            return;
        }
		//如果不是主城  暂时不允许创建玩家..等服务器调整好之后再做处理..
        if (gameLevel.levelType != sdGameLevel.LevelType.MainCity)
		{
            Debug.Log(gameLevel.levelType.ToString() + " don't allow create sdOtherChar.");
			return;
		}
		for (int i = 0; i < refMSG.m_Count; ++i)
		{
			CliProto.SOtherInfoWithPos kOtherRoleInfo = refMSG.m_RoleInfo[i];
			
			float fX = ServerPos2Client(kOtherRoleInfo.m_Pos.m_X);
			float fY = ServerPos2Client(kOtherRoleInfo.m_Pos.m_Y);
			float fZ = ServerPos2Client(kOtherRoleInfo.m_Pos.m_Z);
			float fDirection = ServerOri2Client(kOtherRoleInfo.m_Pos.m_Orientation);
			
			sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
			kInfo.mDBID = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_DBRoleId;
			kInfo.mObjID = kOtherRoleInfo.m_RoleInfo.m_ObjID;
			kInfo.mGender = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_Gender;
			kInfo.mHairStyle = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_HairStyle;
			kInfo.mSkinColor = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_SkinColor;
			kInfo.mBaseJob = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_BaseJob;
			kInfo.mJob = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_Job;
			kInfo.mLevel = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_Level;
			kInfo.mRoleName = System.Text.Encoding.UTF8.GetString(kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_RoleInfo.m_RoleName);
			
			sdOtherChar otherChar = sdGameLevel.instance.createOtherChar(
				kInfo, 
				//new Vector3(0.0f, -1.4f, 35.5f), 
				new Vector3(fX, fY, fZ), 
				Quaternion.Euler(0, fDirection, 0));
			
			uint uiEquipCount = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_Equip.m_ItemCount;
			if (otherChar != null && uiEquipCount > 0)
			{
				uint[] auiEquipArray = new uint[uiEquipCount];
				for (int j = 0; j < uiEquipCount; ++j)
					auiEquipArray[j] = (uint)kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_Equip.m_Items[j].m_TID;
				
				otherChar.updateAvatar(auiEquipArray, uiEquipCount);
			}
		}
	}
	
	private static void msg_SCID_OTHER_DISAPPEAR(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_OTHER_DISAPPEAR refMSG = (CliProto.SC_OTHER_DISAPPEAR)msg;
		if(refMSG == null)
			return;
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel == null)
			return;
		
		for (int i = 0; i < refMSG.m_Count; ++i)
		{
			ulong ulObj = refMSG.m_ObjID[i];
			sdGameLevel.instance.removeOtherCharByObjID(ulObj);
		}	
	}
	
	private static void msg_SCID_OTHER_UPDATE(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_OTHER_UPDATE refMSG = (CliProto.SC_OTHER_UPDATE)msg;
		if(refMSG == null)
			return;
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel == null)
			return;
		
		for (int i = 0; i < refMSG.m_Count; ++i)
		{
			CliProto.SOtherInfo kOtherRoleInfo = refMSG.m_RoleInfo[i];

			ulong ulDBID = kOtherRoleInfo.m_RoleInfo.m_RoleInfo.m_DBRoleId;
			sdOtherChar otherChar = gameLevel.findOtherCharByDBID(ulDBID);
			uint uiEquipCount = kOtherRoleInfo.m_RoleInfo.m_Equip.m_ItemCount;
			if (otherChar != null && uiEquipCount > 0)
			{
				uint[] auiEquipArray = new uint[uiEquipCount];
				for (int j = 0; j < uiEquipCount; ++j)
					auiEquipArray[j] = (uint)kOtherRoleInfo.m_RoleInfo.m_Equip.m_Items[j].m_TID;
				
				otherChar.updateAvatar(auiEquipArray, uiEquipCount);
			}
		}
	}
	
	private static void msg_SCID_MOVE_BEGIN(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_MOVE_BEGIN refMSG = (CliProto.SC_MOVE_BEGIN)msg;
		if(refMSG == null)
			return;
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel == null)
			return;	
		
		for (int i = 0; i < refMSG.m_Count; ++i)
		{
			CliProto.SMoveObj kMoveObj = refMSG.m_Objs[i];

			ulong ulObjID = kMoveObj.m_ObjectID;
			sdOtherChar otherChar = gameLevel.findOtherCharByObjID(ulObjID);
			if (otherChar != null)
			{
				sdSynMovePacket kMovePacket = new sdSynMovePacket();
				
				kMovePacket.mPosX = ServerPos2Client(kMoveObj.m_Position.m_X);
				kMovePacket.mPosY = ServerPos2Client(kMoveObj.m_Position.m_Y);
				kMovePacket.mPosZ = ServerPos2Client(kMoveObj.m_Position.m_Z);
				kMovePacket.mOrientation = ServerOri2Client(kMoveObj.m_Position.m_Orientation);
				
				ulong ulDeltaTime = kMoveObj.m_Time - sdGlobalDatabase.Instance.ServerBeginTime;
				kMovePacket.mSendTime = gameLevel.ClientTime + (float)ulDeltaTime / 1000.0f;	
				kMovePacket.mRecvTime = System.DateTime.Now.Ticks / 10000L * 0.001f;//Time.time;
				
				otherChar.PushSynPacket(kMovePacket);
				
//				string kMsg = "-> SC_MOVE_BEGIN: " 
//					+ ulObjID + ";"
//					+ kMovePacket.mPosX.ToString() + "," 
//					+ kMovePacket.mPosY.ToString() + "," 
//					+ kMovePacket.mPosZ.ToString() + ";"
//					+ kMovePacket.mOrientation.ToString();
//				SDGlobal.Log(kMsg);
			}
		}
	}
	
	private static void msg_SCID_MOVE_STOP(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_MOVE_STOP refMSG = (CliProto.SC_MOVE_STOP)msg;
		if(refMSG == null)
			return;
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel == null)
			return;	
		
		for (int i = 0; i < refMSG.m_Count; ++i)
		{
			CliProto.SMoveObj kMoveObj = refMSG.m_Objs[i];

			ulong ulObjID = kMoveObj.m_ObjectID;
			sdOtherChar otherChar = gameLevel.findOtherCharByObjID(ulObjID);
			if (otherChar != null)
			{
				sdSynMovePacket kMovePacket = new sdSynMovePacket();
				
				kMovePacket.mIsEnd = true;
				kMovePacket.mPosX = ServerPos2Client(kMoveObj.m_Position.m_X);
				kMovePacket.mPosY = ServerPos2Client(kMoveObj.m_Position.m_Y);
				kMovePacket.mPosZ = ServerPos2Client(kMoveObj.m_Position.m_Z);
				kMovePacket.mOrientation = ServerOri2Client(kMoveObj.m_Position.m_Orientation);
				
				ulong ulDeltaTime = kMoveObj.m_Time - sdGlobalDatabase.Instance.ServerBeginTime;
				kMovePacket.mSendTime = gameLevel.ClientTime + (float)ulDeltaTime / 1000.0f;	
				kMovePacket.mRecvTime = System.DateTime.Now.Ticks / 10000L * 0.001f;//Time.time;
				
				otherChar.PushSynPacket(kMovePacket);
				
//				string kMsg = "-> SC_MOVE_END: " 
//					+ ulObjID + ";"
//					+ kMovePacket.mPosX.ToString() + "," 
//					+ kMovePacket.mPosY.ToString() + "," 
//					+ kMovePacket.mPosZ.ToString() + ";"
//					+ kMovePacket.mOrientation.ToString();
//				SDGlobal.Log(kMsg);
			}
		}
	}
	
	public static void send_CSID_SCID_MOVE_BEGIN(Vector3 kPos, float fOri, ulong nTime)
	{
		CliProto.CS_MOVE_BEGIN refMSG = new CliProto.CS_MOVE_BEGIN();
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		ulong nDeltaTime = nTime - gameLevel.ClientTime;
		refMSG.m_Time = nDeltaTime + sdGlobalDatabase.Instance.ServerBeginTime;
		
		refMSG.m_Position.m_X = ClientPos2Server(kPos.x);
		refMSG.m_Position.m_Y = ClientPos2Server(kPos.y);
		refMSG.m_Position.m_Z = ClientPos2Server(kPos.z);
		refMSG.m_Position.m_Orientation = ClientOri2Server(fOri);
		
		SDNetGlobal.SendMessage(refMSG);
		
//		string kMsg = "-> CS_MOVE_BEGIN: " 
//			+ kPos.x.ToString() + "," 
//			+ kPos.y.ToString() + "," 
//			+ kPos.z.ToString() + ";"
//			+ fOri.ToString();
//		SDGlobal.Log(kMsg);
	}
	
	public static void send_CSID_SCID_MOVE_STOP(Vector3 kPos, float fOri, float fTime)
	{
		CliProto.CS_MOVE_STOP refMSG = new CliProto.CS_MOVE_STOP();
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		float fDeltaTime = fTime - gameLevel.ClientTime;
		refMSG.m_Time = (ulong)(fDeltaTime * 1000) + sdGlobalDatabase.Instance.ServerBeginTime;;
		
		refMSG.m_Position.m_X = ClientPos2Server(kPos.x);
		refMSG.m_Position.m_Y = ClientPos2Server(kPos.y);
		refMSG.m_Position.m_Z = ClientPos2Server(kPos.z);
		refMSG.m_Position.m_Orientation = ClientOri2Server(fOri);
		
		SDNetGlobal.SendMessage(refMSG);
		
//		string kMsg = "-> CS_MOVE_STOP: " 
//			+ kPos.x.ToString() + "," 
//			+ kPos.y.ToString() + "," 
//			+ kPos.z.ToString() + ";"
//			+ fOri.ToString();
//		SDGlobal.Log(kMsg);
	}
	
	private static float ServerPos2Client(int iValue)
	{
		return (float)(iValue / 1000.0f);	
	}
	
	private static int ClientPos2Server(float fValue)
	{
		return (int)(fValue * 1000.0f);	
	}
	
	private static float ServerOri2Client(int iValue)
	{
		return (float)(iValue);	
	}
	
	private static int ClientOri2Server(float fValue)
	{
		return (int)(fValue);	
	}
}

