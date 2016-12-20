using UnityEngine;
using System.Collections;
using System;

public class sdUIMailWnd : MonoBehaviour
{
	static private GameObject	m_preWnd = null;
	
	public GameObject mailList = null;
	public GameObject sendMail = null;

	public GameObject sendTo = null;
	public GameObject sendTitle = null;
	public GameObject sendTxt = null;
	public GameObject sendMoney = null;
	public GameObject sendItem = null;

	public Hashtable mailitemList = new Hashtable();
	public GameObject mailitem = null;

	void Awake () 
	{
	}
	
	void Start () 
	{
		Init();
	}
	
	void Update () 
	{
	}
	
	public void Init()
	{
	}
	
	void OnClick()
    {
	}
	
	public void ActiveMailWnd(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		OnActivePnlSetRadioButton();
		ShowMailWndPanel(1);
		RefreshMailList();
	}
	
	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="TabMailList")
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}

	public void RefreshMailList()
	{
		Hashtable list = null;
		list = sdMailMgr.Instance.m_MailList;
		
		int num = list.Count;
		int iZero = 0;
		if (num<4)
		{
			iZero = 4-num;
		}
		
		num = num + iZero;
		int count = mailitemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(mailitem) as GameObject;
				tempItem.GetComponent<sdUIMailIcon>().index = count;
				tempItem.transform.parent = mailitem.transform.parent;
				tempItem.transform.localPosition = mailitem.transform.localPosition;
				tempItem.transform.localScale = mailitem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (127*count);
				tempItem.transform.localPosition = pos;
				mailitemList.Add(mailitemList.Count, tempItem.GetComponent<sdUIMailIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = mailitemList.GetEnumerator();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			if (iter.MoveNext())
			{
				sdUIMailIcon icon = iter.Value as sdUIMailIcon;
				icon.SetIdAndReflashUI(UInt64.Parse(key1));
			}
		}

		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIMailIcon icon = iter.Value as sdUIMailIcon;
				icon.SetIdAndReflashUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIMailIcon icon = iter.Value as sdUIMailIcon;
			icon.SetIdAndReflashUI(UInt64.MaxValue);
		}
	}

	public void OnDeleteMail()
	{
		if (mailitem!=null)
		{
			mailitem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}

	public void OnSendMail()
	{
		HeaderProto.SEND_MAIL sendMail = new HeaderProto.SEND_MAIL();

		if (sendTo)
		{
			string strSendTo = sendTo.GetComponent<UIInput>().value;
			sendMail.m_Receiver = System.Text.Encoding.UTF8.GetBytes(strSendTo);
		}

		if (sendTitle)
		{
			string strSendTitle = sendTitle.GetComponent<UIInput>().value;
			sendMail.m_Title = System.Text.Encoding.UTF8.GetBytes(strSendTitle);
		}

		if (sendTxt)
		{
			string strSendTxt = sendTxt.GetComponent<UIInput>().value;
			sendMail.m_Content = System.Text.Encoding.UTF8.GetBytes(strSendTxt);
		}

		if (sendMoney)
		{
			string strSendMoney = sendMoney.GetComponent<UIInput>().value;
			sendMail.m_Money = int.Parse(strSendMoney);
		}

		if (sendItem)
		{
			string strSendItem = sendItem.GetComponent<UIInput>().value;
			int iItemID = int.Parse(strSendItem);
			if (iItemID>0)
			{
				UInt64 uuID = sdGameItemMgr.Instance.getItemUIDByTID(iItemID);
				if (uuID!=UInt64.MaxValue)
				{
					sendMail.m_ItemCount = 1;
					sendMail.m_Items[0] = uuID;
				}
			}
		}

		sdMailMsg.Send_CS_SEND_MAIL_REQ(sendMail);
	}

	public void ShowMailMoney()
	{

	}

	public void ShowMailWndPanel(int iType)
	{
		if (iType==1)
		{
			if (mailList)
				mailList.SetActive(true);
			if (sendMail)
				sendMail.SetActive(false);
		}
		else if (iType==2)
		{
			if (mailList)
				mailList.SetActive(false);
			if (sendMail)
				sendMail.SetActive(true);
		}
	}
}