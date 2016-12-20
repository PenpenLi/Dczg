using UnityEngine;
using System.Collections;
using System;

public class sdUIMailDetailWnd : MonoBehaviour
{
	static private GameObject	m_preWnd = null;

	public GameObject lbTitle = null;
	public GameObject lbDesc = null;
	public GameObject item1 = null;
	public GameObject item2 = null;
	public GameObject item3 = null;
	public GameObject item4 = null;
	public GameObject head = null;
	public GameObject fromv = null;
	public GameObject timev = null;
	public GameObject btnGetDetailItem = null;
	public GameObject btnDelMail = null;
	public GameObject money = null;
	public GameObject moneyv = null;

	public UInt64 m_uuMailID = UInt64.MaxValue;

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
	}
	
	public void ActiveMailDetailWnd(GameObject PreWnd, UInt64 uuID)
	{
		m_preWnd = PreWnd;
		m_uuMailID = uuID;
		ShowMailDetailWndUI();
	}

	public void ResetMailDetailUI()
	{
		lbTitle.GetComponent<UILabel>().text = "...";
		lbDesc.GetComponent<UILabel>().text = "...";
		item1.SetActive(false);
		item2.SetActive(false);
		item3.SetActive(false);
		item4.SetActive(false);
		head.transform.FindChild("head2").GetComponent<UISprite>().spriteName = "head3";
		fromv.GetComponent<UILabel>().text = "...";
		timev.GetComponent<UILabel>().text = "...";
		btnGetDetailItem.SetActive(true);
		btnDelMail.SetActive(true);
		btnDelMail.transform.localPosition = new Vector3(135, -259, 0);
		money.SetActive(true);
		moneyv.GetComponent<UILabel>().text = "0";
	}

	public void ShowMailDetailWndUI()
	{
		if (m_uuMailID==UInt64.MaxValue)
		{
			ResetMailDetailUI();
		}
		else
		{
			HeaderProto.SMailDetail detail = sdMailMgr.Instance.GetMailInfo(m_uuMailID);
			if (detail!=null)
			{
				string strSender = System.Text.Encoding.UTF8.GetString(detail.m_Sender);
				string strHead = sdConfDataMgr.Instance().GetMailHeadTex(strSender);
				if (strHead=="")
					strHead = "head3";
				
				if (head)
					head.transform.FindChild("head2").GetComponent<UISprite>().spriteName = strHead;

				if (lbTitle)
					lbTitle.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(detail.m_Title);

				if (lbDesc)
					lbDesc.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(detail.m_Content);

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

				if (fromv)
					fromv.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(detail.m_Sender);

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

				if (detail.m_Money>0)
				{
					if (btnGetDetailItem)
						btnGetDetailItem.SetActive(true);
					
					if (btnDelMail)
					{
						btnDelMail.SetActive(true);
						btnDelMail.transform.localPosition = new Vector3(135, -259, 0);
					}
					
					if (money)
						money.SetActive(true);

					if (moneyv)
						moneyv.GetComponent<UILabel>().text = detail.m_Money.ToString();
				}
				else
				{
					if (money)
						money.SetActive(false);

					if (moneyv)
					{
						moneyv.GetComponent<UILabel>().text = "0";
						moneyv.SetActive(false);
					}
					
					if (detail.m_ItemCount>0)
					{
						if (btnGetDetailItem)
							btnGetDetailItem.SetActive(true);
						
						if (btnDelMail)
						{
							btnDelMail.SetActive(true);
							btnDelMail.transform.localPosition = new Vector3(135, -259, 0);
						}
					}
					else
					{
						if (btnGetDetailItem)
							btnGetDetailItem.SetActive(false);
						
						if (btnDelMail)
						{
							btnDelMail.SetActive(true);
							btnDelMail.transform.localPosition = new Vector3(0, -259, 0);
						}
					}
				}
			}
			else
			{
				ResetMailDetailUI();
			}
		}
	}
}