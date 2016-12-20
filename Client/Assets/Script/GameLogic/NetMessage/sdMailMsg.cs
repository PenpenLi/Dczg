using UnityEngine;
using System.Collections;
using System;

public class sdMailMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDGlobal.Log("sdMailMsg.init");
		//邮件列表通知..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_MAIL_LIST_NTF, msg_SC_MAIL_LIST_NTF);
		//发送邮件ack..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SEND_MAIL_ACK, msg_SC_SEND_MAIL_ACK);
		//读邮件ack..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_READ_MAIL_ACK, msg_SC_READ_MAIL_ACK);
		//删除邮件ack..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_DELETE_MAIL_ACK, msg_SC_DELETE_MAIL_ACK);
		//更新邮件通知..
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_UPDATE_MAIL_NTF, msg_SC_UPDATE_MAIL_NTF);
		
		return true;
	}
	
	//邮件列表..
	private static void msg_SC_MAIL_LIST_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_MAIL_LIST_NTF refMsg = (CliProto.SC_MAIL_LIST_NTF)msg;
		sdMailMgr.Instance.ResetMailList(refMsg);
		sdMailMgr.Instance.ResetMailNeedFlash();

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIMailWnd uiWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
			if (uiWnd)
				uiWnd.RefreshMailList();
		}
	}
	
	//请求发送邮件..
	public static bool Send_CS_SEND_MAIL_REQ(HeaderProto.SEND_MAIL sendMail)
	{
		CliProto.CS_SEND_MAIL_REQ refMSG = new CliProto.CS_SEND_MAIL_REQ();
		refMSG.m_MailInfo = sendMail;
		
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//发送邮件ack..
	private static void msg_SC_SEND_MAIL_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SEND_MAIL_ACK refMsg = (CliProto.SC_SEND_MAIL_ACK)msg;
		if (refMsg.m_Result==1)
			sdUICharacter.Instance.ShowOkMsg("邮件发送成功..", null);
		else
			sdUICharacter.Instance.ShowOkMsg("邮件发送失败..", null);
	}
	
	//请求提取邮件物品..
	public static bool Send_CS_GET_ITEM_FROM_MAIL_REQ(UInt64 uuMailID)
	{
		CliProto.CS_GET_ITEM_FROM_MAIL_REQ refMSG = new CliProto.CS_GET_ITEM_FROM_MAIL_REQ();
		refMSG.m_MailID = uuMailID;

		SDNetGlobal.SendMessage(refMSG);		
		return true;
	}

	//请求提取邮件金钱..
	public static bool Send_CS_GET_MONEY_FROM_MAIL_REQ(UInt64 uuMailID)
	{
		CliProto.CS_GET_MONEY_FROM_MAIL_REQ refMSG = new CliProto.CS_GET_MONEY_FROM_MAIL_REQ();
		refMSG.m_MailID = uuMailID;
		
		SDNetGlobal.SendMessage(refMSG);		
		return true;
	}

	//请求读邮件..
	public static bool Send_CS_READ_MAIL_REQ(UInt64 uuMailID)
	{
		CliProto.CS_READ_MAIL_REQ refMSG = new CliProto.CS_READ_MAIL_REQ();
		refMSG.m_MailID = uuMailID;
		
		SDNetGlobal.SendMessage(refMSG);		
		return true;
	}
	
	//读邮件ack..
	private static void msg_SC_READ_MAIL_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_READ_MAIL_ACK refMsg = (CliProto.SC_READ_MAIL_ACK)msg;
		UInt64 uuMailID = refMsg.m_MailID;
		uint uiTime = 100;
		sdMailMgr.Instance.OnReadMail(uuMailID, uiTime);
		sdMailMgr.Instance.ResetMailNeedFlash();

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIMailWnd uiWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
			if (uiWnd)
				uiWnd.RefreshMailList();
			
			sdUIMailDetailWnd uiWnd2 = wnd.GetComponentInChildren<sdUIMailDetailWnd>();
			if (uiWnd2)
				uiWnd2.ShowMailDetailWndUI();
		}
	}

	//请求删除邮件..
	public static bool Send_CS_DELETE_MAIL_REQ(HeaderProto.MAIL_ID_LIST mailList)
	{
		CliProto.CS_DELETE_MAIL_REQ refMSG = new CliProto.CS_DELETE_MAIL_REQ();
		refMSG.m_Mails = mailList;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
	//删除邮件ack..
	private static void msg_SC_DELETE_MAIL_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_DELETE_MAIL_ACK refMsg = (CliProto.SC_DELETE_MAIL_ACK)msg;
		sdMailMgr.Instance.DeleteMail(refMsg.m_Mails);
		sdMailMgr.Instance.ResetMailNeedFlash();

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIMailWnd uiWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
			if (uiWnd)
			{
				uiWnd.RefreshMailList();
				uiWnd.OnDeleteMail();
			}

			if( sdMailControl.m_UIMailDetailWnd != null )			
				sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailDetailWnd);
		}
	}
	
	//更新邮件通知..
	private static void msg_SC_UPDATE_MAIL_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_UPDATE_MAIL_NTF refMsg = (CliProto.SC_UPDATE_MAIL_NTF)msg;
		sdMailMgr.Instance.UpdateMailList(refMsg.m_List);
		sdMailMgr.Instance.ResetMailNeedFlash();

		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIMailWnd uiWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
			if (uiWnd)
				uiWnd.RefreshMailList();

			sdUIMailDetailWnd uiWnd2 = wnd.GetComponentInChildren<sdUIMailDetailWnd>();
			if (uiWnd2)
				uiWnd2.ShowMailDetailWndUI();
		}
	}
}

