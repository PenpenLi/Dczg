using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIMailIcon : MonoBehaviour
{
	public int index = 0;
	private UInt64 mailUUID = UInt64.MaxValue;

	public GameObject bg = null;
	public GameObject bgLine = null;
	public GameObject head = null;
	public GameObject sendv = null;
	public GameObject timev = null;
	public GameObject mailTitle = null;
	public GameObject item1 = null;
	public GameObject item2 = null;
	public GameObject item3 = null;
	public GameObject item4 = null;
	public GameObject btnGetItem = null;
	public GameObject bz = null;
	public GameObject money = null;
	
	public UInt64 GetId()
	{
		return mailUUID;
	}
	
	void Update()
	{
	}
	
	void OnClick()
	{
		if (gameObject)
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIMailWnd mailWnd = wnd.GetComponentInChildren<sdUIMailWnd>();
				if (mailWnd)
				{
					//请求读邮件..
					HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailUUID);
					if (detail!=null&&detail.m_ReadTime==0)
					{
						sdMailMsg.Send_CS_READ_MAIL_REQ(mailUUID);
					}
					//弹出邮件界面..
					if (detail!=null)
					{
						sdMailControl.Instance.ActiveMailDetailWnd(null, mailUUID);
					}
				}
			}
		}
	}

	public void SetIdAndReflashUI(UInt64 id)
	{
		if (id==UInt64.MaxValue) 
		{
			mailUUID = UInt64.MaxValue;
			gameObject.SetActive(false);
			return;
		}
		else if (id==0)
		{
			mailUUID = 0;
			gameObject.SetActive(true);

			if (bg)
				bg.SetActive(true);

			if (bgLine)
				bgLine.SetActive(false);

			return;
		}
		
		gameObject.SetActive(true);
		mailUUID = id;

		if (bg)
			bg.SetActive(false);
		
		if (bgLine)
			bgLine.SetActive(true);

		HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(mailUUID);
		if (detail!=null)
		{
			string strSender = System.Text.Encoding.UTF8.GetString(detail.m_Sender);
			string strHead = sdConfDataMgr.Instance().GetMailHeadTex(strSender);
			if (strHead=="")
				strHead = "head3";

			if (head)
				head.transform.FindChild("head2").GetComponent<UISprite>().spriteName = strHead;
			if (sendv)
				sendv.GetComponent<UILabel>().text = strSender;
			if (timev)
			{
				double dwTime = (double)detail.m_SendTime*1000;
				DateTime timeSend = sdConfDataMgr.Instance().ConvertServerTimeToClientTime(dwTime);
				TimeSpan ts1 = new TimeSpan(timeSend.Ticks);
				TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
				TimeSpan ts = ts2.Subtract(ts1).Duration();
				UInt64 uuPassMinutes = (UInt64)ts.TotalMinutes;
				UInt64 uuMinutes = 0;
				if (detail.m_ItemCount>0)
				{
					if (detail.m_ReadTime==0)
						uuMinutes = 129601;
					else
						uuMinutes = 10081;
				}
				else
				{
					if (detail.m_ReadTime==0)
						uuMinutes = 10081;
					else
						uuMinutes = 1441;
				}

				if (uuPassMinutes>uuMinutes)
				{
					timev.GetComponent<UILabel>().text = "可删除";
				}
				else
				{
					uuMinutes = uuMinutes - uuPassMinutes;
					UInt64 uuDays = uuMinutes/1440;
					UInt64 uuTemp = uuMinutes%1440;
					UInt64 uuHours = uuTemp/60;
					uuTemp = uuTemp%60;
					if (uuDays>0)
					{
						timev.GetComponent<UILabel>().text = string.Format("{0}天", (int)uuDays);
					}
					else
					{
						if (uuHours>0)
							timev.GetComponent<UILabel>().text = string.Format("{0}小时", (int)uuHours);
						else
							timev.GetComponent<UILabel>().text = string.Format("{0}分钟", (int)uuTemp);
					}
				}
			}
			if (mailTitle)
				mailTitle.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(detail.m_Title);

			item1.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(0,0);
			item2.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(0,0);
			item3.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(0,0);
			item4.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(0,0);
			for (int i=0;i<detail.m_ItemCount&&i<4;i++)
			{
				int iItemID = detail.m_Items[i].m_TID;
				int iCount = detail.m_Items[i].m_CT;
				if (i==0)
					item1.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(iItemID, iCount);
				else if (i==1)
					item2.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(iItemID, iCount);
				else if (i==2)
					item3.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(iItemID, iCount);
				else if (i==3)
					item4.GetComponent<sdUIMailItemIcon>().SetIdAndReflashUI(iItemID, iCount);
			}

			if (detail.m_Money>0)
			{
				if (bz)
					bz.SetActive(false);
				
				if (btnGetItem)
					btnGetItem.SetActive(true);

				if (money)
					money.SetActive(true);
			}
			else
			{
				if (money)
					money.SetActive(false);

				if (detail.m_ItemCount>0)
				{
					if (bz)
						bz.SetActive(false);
					
					if (btnGetItem)
						btnGetItem.SetActive(true);
				}
				else
				{
					if (detail.m_ReadTime>0)
					{
						if (bz)
						{
							bz.GetComponent<UISprite>().spriteName = "yd";
							bz.SetActive(true);
						}
					}
					else
					{
						if (bz)
							bz.SetActive(false);
					}
					
					if (btnGetItem)
						btnGetItem.SetActive(false);
				}
			}
		}
	}
}
