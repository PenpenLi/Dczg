using UnityEngine;
using System.Collections;
using System;

public class sdUIMailWndBtn : MonoBehaviour
{
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnClick()
    {
		if (gameObject.name=="wndClose")
		{
			if( sdMailControl.m_UIMailWnd != null )
				WndAni.HideWndAni(sdMailControl.m_UIMailWnd,true,"w_black");
				//sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailWnd);

		}
		else if (gameObject.name=="detailClose")
		{
			if( sdMailControl.m_UIMailDetailWnd != null )
				WndAni.HideWndAni(sdMailControl.m_UIMailDetailWnd,false,"bg_grey");
				//sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailDetailWnd);
		}
		else if (gameObject.name=="btnGetItem")
		{
			UInt64 mailID = gameObject.transform.parent.transform.parent.transform.parent.transform.GetComponent<sdUIMailIcon>().GetId();
			HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailID);
			if (detail!=null)
			{
				if (detail.m_ItemCount>0)
					sdMailMsg.Send_CS_GET_ITEM_FROM_MAIL_REQ(mailID);

				if (detail.m_Money>0)
					sdMailMsg.Send_CS_GET_MONEY_FROM_MAIL_REQ(mailID);

				if (detail.m_ReadTime==0)
					sdMailMsg.Send_CS_READ_MAIL_REQ(mailID);
			}
		}
		else if (gameObject.name=="btnDelOld")
		{
			Hashtable list = null;
			list = sdMailMgr.Instance.m_MailList;
			ushort index = 0;
			HeaderProto.MAIL_ID_LIST mailIDList = new HeaderProto.MAIL_ID_LIST();
			foreach(DictionaryEntry info in list)
			{
				UInt64 key1 = UInt64.Parse(info.Key.ToString());
				HeaderProto.SMailDetail detail = info.Value as HeaderProto.SMailDetail;
				if (detail.m_ItemCount==0&&detail.m_ReadTime>0)
				{
					mailIDList.m_MailIDs[index] = key1;
					index++;
				}
			}
			mailIDList.m_Count = index;

			if (index>0)
				sdMailMsg.Send_CS_DELETE_MAIL_REQ(mailIDList);
		}
		else if (gameObject.name=="btnSendMail")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailWnd mailWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
				if (mailWnd)
					mailWnd.OnSendMail();
			}
		}
		else if (gameObject.name=="TabMailList")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailWnd mailWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
				if (mailWnd)
				{
					mailWnd.ShowMailWndPanel(1);
					mailWnd.RefreshMailList();
				}
			}
		}
		else if (gameObject.name=="TabSendList")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailWnd mailWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
				if (mailWnd)
				{
					mailWnd.ShowMailWndPanel(2);
				}
			}
		}
		else if (gameObject.name=="btnGetDetailItem")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailDetailWnd mailDetailWnd = wnd.GetComponentInChildren<sdUIMailDetailWnd>();
				if (mailDetailWnd)
				{
					HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailDetailWnd.m_uuMailID);
					if (detail!=null)
					{
						if (detail.m_ItemCount>0)
							sdMailMsg.Send_CS_GET_ITEM_FROM_MAIL_REQ(mailDetailWnd.m_uuMailID);
						
						if (detail.m_Money>0)
							sdMailMsg.Send_CS_GET_MONEY_FROM_MAIL_REQ(mailDetailWnd.m_uuMailID);

						if (detail.m_ReadTime==0)
							sdMailMsg.Send_CS_READ_MAIL_REQ(mailDetailWnd.m_uuMailID);
					}
				}
			}
		}
		else if (gameObject.name=="btnDelDetailMail")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailDetailWnd mailDetailWnd = wnd.GetComponentInChildren<sdUIMailDetailWnd>();
				if (mailDetailWnd)
				{
					HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailDetailWnd.m_uuMailID);
					if (detail!=null&&detail.m_ItemCount<=0&&detail.m_Money<=0)
					{
						HeaderProto.MAIL_ID_LIST mailIDList = new HeaderProto.MAIL_ID_LIST();
						mailIDList.m_Count = 1;
						mailIDList.m_MailIDs[0] = mailDetailWnd.m_uuMailID;
						sdMailMsg.Send_CS_DELETE_MAIL_REQ(mailIDList);

						if( sdMailControl.m_UIMailDetailWnd != null )			
							sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailDetailWnd);

						return;
					}

					if (detail!=null&&(detail.m_ItemCount>0||detail.m_Money>0))
					{
						sdMsgBox.OnConfirm btn_Delete = new sdMsgBox.OnConfirm(OnClickDel);
						sdUICharacter.Instance.ShowOkCanelMsg("该邮件存在物品或金钱,确定要删除么?",btn_Delete,null);
						return;
					}
				}
			}
		}
		else if (gameObject.name=="mailBtn")
		{
			sdMailControl.Instance.ActiveMailWnd(null);
		}
	}

	void OnClickDel()
	{
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIMailDetailWnd mailDetailWnd = wnd.GetComponentInChildren<sdUIMailDetailWnd>();
			if (mailDetailWnd)
			{
				HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailDetailWnd.m_uuMailID);
				if (detail!=null)
				{
					HeaderProto.MAIL_ID_LIST mailIDList = new HeaderProto.MAIL_ID_LIST();
					mailIDList.m_Count = 1;
					mailIDList.m_MailIDs[0] = mailDetailWnd.m_uuMailID;
					sdMailMsg.Send_CS_DELETE_MAIL_REQ(mailIDList);

					if( sdMailControl.m_UIMailDetailWnd != null )			
						sdMailControl.Instance.CloseGameWnd(sdMailControl.m_UIMailDetailWnd);

					return;
				}
			}
		}
	}
}