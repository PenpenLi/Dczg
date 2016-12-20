using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdNewInfo : MonoBehaviour
{
	public NewInfoType type;
	
	void Awake()
	{
		if (sdNewInfoMgr.Instance)
		{
			sdNewInfoMgr.Instance.Register(this);
		}
	}
	
	void OnDestroy()
	{
		if (sdNewInfoMgr.Instance)
		{
			sdNewInfoMgr.Instance.RemoveItem(this);	
		}
	}
	
	public void ShowOrHide(bool flag)
	{
		if (type == NewInfoType.Type_All) return;
		gameObject.SetActive(flag);	
	}
	
	public void SetNew(bool flag)
	{
		gameObject.SetActive(flag);	
	}
}