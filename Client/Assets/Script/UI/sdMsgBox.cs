using System;
using UnityEngine;

public class sdMsgBox : MonoBehaviour
{
	public GameObject okBtn;
	public GameObject cancelBtn;
	public GameObject txt;
	public bool isOkCancelMode;
	
	public void ShowOkMsg(string text, OnConfirm confirm)
	{
		if (cancelBtn != null) cancelBtn.SetActive(false);
		if (okBtn != null) 
		{
			Vector3 vec = okBtn.transform.localPosition;
			vec.x = 0;
			okBtn.transform.localPosition = vec;
		}
		if (txt != null) txt.GetComponent<UILabel>().text = text;
		m_confirm = confirm;
		isOkCancelMode = false;
	}
	
	public void ShowOkCancelMsg(string text, OnConfirm confirm, OnCancel cancel)
	{
		if (cancelBtn != null) cancelBtn.SetActive(true);
		if (okBtn != null) 
		{
			Vector3 vec = cancelBtn.transform.localPosition;
			vec.x = -vec.x;
			okBtn.transform.localPosition = vec;
		}
		if (txt != null) txt.GetComponent<UILabel>().text = text;
		
		m_confirm = confirm;
		m_cancel = cancel;
		isOkCancelMode = true;
	}
	
	public void ClickOk()
	{
		if (m_confirm != null)
		{
			m_confirm();	
			m_confirm = null;
		}
		gameObject.SetActive(false);
	}
	
	public void ClickCancel()
	{
		if (m_cancel != null)
		{
			m_cancel();	
			m_cancel = null;
		}
		gameObject.SetActive(false);
	}
	
	OnConfirm m_confirm = null;
	OnCancel m_cancel= null;
	
	public delegate void OnConfirm();
	public delegate void OnCancel();
}