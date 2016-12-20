using UnityEngine;
using System.Collections;
using System;

public class sdAwardCenterMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDGlobal.Log("sdAwardCenterMsg.init");

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_DAY_NTF, msg_SC_GIFT_DAY_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_DAY_UPD, msg_SC_GIFT_DAY_UPDATE);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_DAY_BOX_NTF, msg_SC_GIFT_DAY_BOX_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_SIGN_NTF, msg_SC_GIFT_SIGN_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_SIGN_VIP_NTF, msg_SC_GIFT_SIGN_VIP_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_LEVEL_NTF, msg_SC_GIFT_LEVEL_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_JIHUOMA_ACK, msg_SC_GIFT_JIHUOMA_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_EP_INFO_ACK, msg_SC_GIFT_EP_INFO_ACK);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_GIFT_RECEIVE_EP_ACK, msg_SC_GIFT_RECEIVE_EP_ACK);
		
		return true;
	}

	private static void msg_SC_GIFT_RECEIVE_EP_ACK(int sdItemMsg, ref CMessage msg)
	{
		CliProto.SC_GIFT_RECEIVE_EP_ACK netMsg = (CliProto.SC_GIFT_RECEIVE_EP_ACK)msg;
		EverydayFoodsWnd.Instance.OnReceiveEpAck(netMsg);

	}

	private static void msg_SC_GIFT_EP_INFO_ACK(int iMsgId, ref CMessage msg)
	{
		CliProto.SC_GIFT_EP_INFO_ACK netMsg = (CliProto.SC_GIFT_EP_INFO_ACK)msg;
		EverydayFoodsWnd.Instance.UpdateEverydayFoods(netMsg);
	}


	private static void msg_SC_GIFT_JIHUOMA_ACK(int iMsgID, ref CMessage msg)
	{
		/*
		public enum EJihuoma
		{
			EJihuomaSuccess       = 1,
			EJihuomaCodeLenExceed = 2,
			EJihuomaGDSysError    = 3,
			EJihuomaSysError      = 4,
			EJihuomaMsgFieldError = 5,
			EJihuomaCodeIllegal   = 6,
			EJihuomaAlreadyUsed   = 7,
			EJihuomaAddItemFailed = 8,
		}
		*/

		CliProto.SC_GIFT_JIHUOMA_ACK netMsg = (CliProto.SC_GIFT_JIHUOMA_ACK)msg;
		if(netMsg.m_Ret == (byte)HeaderProto.EJihuoma.EJihuomaSuccess)
		{
			sdUICharacter.Instance.ShowSuccessPanel();
			//sdUICharacter.Instance.ShowOkMsg("兑换码兑换成功！", null);
		}
		else if(netMsg.m_Ret == (byte)HeaderProto.EJihuoma.EJihuomaAlreadyUsed)
		{
			sdUICharacter.Instance.ShowOkMsg("兑换码已经被兑换！", null);
			
		}
		else if(netMsg.m_Ret == (byte)HeaderProto.EJihuoma.EJihuomaAddItemFailed)
		{
			sdUICharacter.Instance.ShowOkMsg("道具添加失败，可能背包已满！", null);
		}
		else 
		{
			sdUICharacter.Instance.ShowOkMsg("兑换码兑换失败！", null);
		}	

	}

	private static void msg_SC_GIFT_LEVEL_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_LEVEL_NTF netMsg = (CliProto.SC_GIFT_LEVEL_NTF)msg;
		LevelAwardWnd.Instance.UpdateLevelList(netMsg);
	}

	private static void msg_SC_GIFT_SIGN_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_SIGN_NTF netMsg = (CliProto.SC_GIFT_SIGN_NTF)msg;
		EverydayAwardWnd.Instance.UpdateSignList(netMsg);
	}

	private static void msg_SC_GIFT_SIGN_VIP_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_SIGN_VIP_NTF netMsg = (CliProto.SC_GIFT_SIGN_VIP_NTF)msg;
		EverydayAwardWnd.Instance.UpdateVipSignList(netMsg);

		AwardCenterWnd.Instance.Init();
	}

	private static void msg_SC_GIFT_DAY_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_DAY_NTF netMsg = (CliProto.SC_GIFT_DAY_NTF)msg;
		EverydayQuestWnd.Instance.UpdateQuestList(netMsg);
	}

	private static void msg_SC_GIFT_DAY_UPDATE(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_DAY_UPD netMsg = (CliProto.SC_GIFT_DAY_UPD)msg;
		EverydayQuestWnd.Instance.UpdateQuestList(netMsg);
	}

	private static void msg_SC_GIFT_DAY_BOX_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GIFT_DAY_BOX_NTF netMsg = (CliProto.SC_GIFT_DAY_BOX_NTF)msg;
		EverydayQuestWnd.Instance.UpdateAwardBox(netMsg);
	}

}

