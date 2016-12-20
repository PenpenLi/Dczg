using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 邮件管理器..
public class sdMailMgr : Singleton<sdMailMgr>
{
	// 邮件列表..
	public Hashtable m_MailList = new Hashtable();

	public void ClearData()
	{
		m_MailList.Clear();
	}

	public HeaderProto.SMailDetail GetMailInfo(UInt64 uuDBID)
	{
		if (m_MailList[uuDBID] != null)
		{
			return (HeaderProto.SMailDetail)m_MailList[uuDBID];	
		}
		else
		{
			return null;	
		}
	}

	public void DeleteMail(HeaderProto.MAIL_ID_LIST mailIDList)
	{
		if (mailIDList.m_Count>0)
		{
			for (int i=0;i<mailIDList.m_Count;i++)
			{
				UInt64 uuID = mailIDList.m_MailIDs[i];
				if (uuID!=UInt64.MaxValue)
				{
					if(m_MailList.ContainsKey(uuID))
						m_MailList.Remove(uuID);
				}
			}
		}
	}

	public void ResetMailList(CliProto.SC_MAIL_LIST_NTF msg)
	{
		m_MailList.Clear();
		
		CliProto.SC_MAIL_LIST_NTF refMSG = msg;
		int iCount = refMSG.m_List.m_Count;
		for (int i=0; i<iCount; i++)
		{
			UInt64 uuDBID = refMSG.m_List.m_Mails[i].m_UniqueID;
			if (uuDBID!=UInt64.MaxValue)
			{
				HeaderProto.SMailDetail info = new HeaderProto.SMailDetail();

				info.m_Type = refMSG.m_List.m_Mails[i].m_Type;			/// 邮件类型..
				info.m_UniqueID = refMSG.m_List.m_Mails[i].m_UniqueID;	/// 邮件唯一id..
				info.m_Money = refMSG.m_List.m_Mails[i].m_Money;		/// 金钱..
				info.m_SendTime = refMSG.m_List.m_Mails[i].m_SendTime;	/// 发送时间..
				info.m_ReadTime = refMSG.m_List.m_Mails[i].m_ReadTime;	/// 阅读时间..
				info.m_Sender = refMSG.m_List.m_Mails[i].m_Sender;		/// 发件人姓名..
				info.m_Receiver = refMSG.m_List.m_Mails[i].m_Receiver;	/// 收件人姓名..
				info.m_Title = refMSG.m_List.m_Mails[i].m_Title;		/// 标题..
				info.m_Content = refMSG.m_List.m_Mails[i].m_Content;	/// 内容..
				info.m_ItemCount = refMSG.m_List.m_Mails[i].m_ItemCount;/// 物品数量..
				for (int j=0; j<info.m_ItemCount; j++)
				{
					HeaderProto.SXITEM item = new HeaderProto.SXITEM();
					info.m_Items[j] = item;
					info.m_Items[j].m_UID = refMSG.m_List.m_Mails[i].m_Items[j].m_UID;
					info.m_Items[j].m_TID = refMSG.m_List.m_Mails[i].m_Items[j].m_TID;
					info.m_Items[j].m_UP = refMSG.m_List.m_Mails[i].m_Items[j].m_UP;
					info.m_Items[j].m_CT = refMSG.m_List.m_Mails[i].m_Items[j].m_CT;
					info.m_Items[j].m_EXP = refMSG.m_List.m_Mails[i].m_Items[j].m_EXP;
					info.m_Items[j].m_LK = refMSG.m_List.m_Mails[i].m_Items[j].m_LK;
					info.m_Items[j].m_GEMCount = refMSG.m_List.m_Mails[i].m_Items[j].m_GEMCount;
					for (int k=0; k<info.m_Items[j].m_GEMCount; k++)
					{
						HeaderProto.SXGEM gem = new HeaderProto.SXGEM();
						info.m_Items[j].m_GEM[k] = gem;
						info.m_Items[j].m_GEM[k].m_TID = refMSG.m_List.m_Mails[i].m_Items[j].m_GEM[k].m_TID;
					}
				}

				m_MailList[uuDBID] = info;
			}
		}
	}

	public void UpdateMailList(HeaderProto.MAIL_LIST mailList)
	{
		if (mailList.m_Count>0)
		{
			for (int i=0;i<mailList.m_Count;i++)
			{
				UInt64 uuMailID = mailList.m_Mails[i].m_UniqueID;
				if (m_MailList[uuMailID] != null)
				{
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Type = mailList.m_Mails[i].m_Type;			/// 邮件类型..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_UniqueID = mailList.m_Mails[i].m_UniqueID;	/// 邮件唯一id..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Money = mailList.m_Mails[i].m_Money;		/// 金钱..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_SendTime = mailList.m_Mails[i].m_SendTime;	/// 发送时间..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_ReadTime = mailList.m_Mails[i].m_ReadTime;	/// 阅读时间..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Sender = mailList.m_Mails[i].m_Sender;		/// 发件人姓名..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Receiver = mailList.m_Mails[i].m_Receiver;	/// 收件人姓名..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Title = mailList.m_Mails[i].m_Title;		/// 标题..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Content = mailList.m_Mails[i].m_Content;	/// 内容..
					((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_ItemCount = mailList.m_Mails[i].m_ItemCount;/// 物品数量..
					for (int j=0; j<((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_ItemCount; j++)
					{
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_UID = mailList.m_Mails[i].m_Items[j].m_UID;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_TID = mailList.m_Mails[i].m_Items[j].m_TID;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_UP = mailList.m_Mails[i].m_Items[j].m_UP;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_CT = mailList.m_Mails[i].m_Items[j].m_CT;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_EXP = mailList.m_Mails[i].m_Items[j].m_EXP;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_LK = mailList.m_Mails[i].m_Items[j].m_LK;
						((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_GEMCount = mailList.m_Mails[i].m_Items[j].m_GEMCount;
						for (int k=0; k<((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_GEMCount; k++)
						{
							((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_Items[j].m_GEM[k].m_TID = mailList.m_Mails[i].m_Items[j].m_GEM[k].m_TID;
						}
					}
				}
				else
				{
					HeaderProto.SMailDetail info = new HeaderProto.SMailDetail();

					info.m_Type = mailList.m_Mails[i].m_Type;			/// 邮件类型..
					info.m_UniqueID = mailList.m_Mails[i].m_UniqueID;	/// 邮件唯一id..
					info.m_Money = mailList.m_Mails[i].m_Money;		/// 金钱..
					info.m_SendTime = mailList.m_Mails[i].m_SendTime;	/// 发送时间..
					info.m_ReadTime = mailList.m_Mails[i].m_ReadTime;	/// 阅读时间..
					info.m_Sender = mailList.m_Mails[i].m_Sender;		/// 发件人姓名..
					info.m_Receiver = mailList.m_Mails[i].m_Receiver;	/// 收件人姓名..
					info.m_Title = mailList.m_Mails[i].m_Title;		/// 标题..
					info.m_Content = mailList.m_Mails[i].m_Content;	/// 内容..
					info.m_ItemCount = mailList.m_Mails[i].m_ItemCount;/// 物品数量..
					for (int j=0; j<info.m_ItemCount; j++)
					{
						HeaderProto.SXITEM item = new HeaderProto.SXITEM();
						info.m_Items[j] = item;

						info.m_Items[j].m_UID = mailList.m_Mails[i].m_Items[j].m_UID;
						info.m_Items[j].m_TID = mailList.m_Mails[i].m_Items[j].m_TID;
						info.m_Items[j].m_UP = mailList.m_Mails[i].m_Items[j].m_UP;
						info.m_Items[j].m_CT = mailList.m_Mails[i].m_Items[j].m_CT;
						info.m_Items[j].m_EXP = mailList.m_Mails[i].m_Items[j].m_EXP;
						info.m_Items[j].m_LK = mailList.m_Mails[i].m_Items[j].m_LK;
						info.m_Items[j].m_GEMCount = mailList.m_Mails[i].m_Items[j].m_GEMCount;
						for (int k=0; k<info.m_Items[j].m_GEMCount; k++)
						{
							HeaderProto.SXGEM gem = new HeaderProto.SXGEM();
							info.m_Items[j].m_GEM[k] = gem;
							info.m_Items[j].m_GEM[k].m_TID = mailList.m_Mails[i].m_Items[j].m_GEM[k].m_TID;
						}
					}

					m_MailList[uuMailID] = info;
				}
			}
		}
	}

	public void OnReadMail(UInt64 uuMailID, uint uiTime)
	{
		if (m_MailList[uuMailID] != null)
			((HeaderProto.SMailDetail)m_MailList[uuMailID]).m_ReadTime = uiTime;
	}

	public void ResetMailNeedFlash()
	{
		bool bResult = false;

		foreach(DictionaryEntry info in m_MailList)
		{
			string key1 = info.Key.ToString();
			HeaderProto.SMailDetail detail = info.Value as HeaderProto.SMailDetail;
			//存在未读邮件..
			if (detail!=null&&detail.m_ReadTime==0)
			{
				bResult = true;
				break;
			}
			//邮件存在物品未领取..
			if (detail!=null&&detail.m_ItemCount>0)
			{
				bResult = true;
				break;
			}
			//邮件存在金钱未领取..
			if (detail!=null&&detail.m_Money>0)
			{
				bResult = true;
				break;
			}
		}

		sdUIMailAlphaAnm.bMailNeedFlash = bResult;
	}
}
