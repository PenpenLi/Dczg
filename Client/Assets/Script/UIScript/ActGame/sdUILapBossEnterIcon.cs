using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUILapBossEnterIcon : MonoBehaviour
{
	public int index = 0;
	private UInt64 actUUID = UInt64.MaxValue;
	private Int64 iTime = 0;
	private string strTime = "00:00:00";
	
	public GameObject bg = null;
	public GameObject bgLine = null;
	public GameObject select = null;
	public GameObject lbName = null;
	public GameObject lbDesc = null;
	public GameObject lbFindV = null;
	public GameObject lbTimeV = null;
	
	public bool bSelect = false;
	
	public UInt64 GetId()
	{
		return actUUID;
	}
	
	void Update()
	{
		float fTime = Time.deltaTime*1000.0f;
		if (iTime>0)
		{
			Int64 iHours = iTime/3600000;
			Int64 iLeft = iTime%3600000;
			Int64 iMin = iLeft/60000;
			iLeft = iLeft%60000;
			iLeft = iLeft/1000;

			if (lbTimeV)
			{
				lbTimeV.GetComponent<UILabel>().color = Color.white;

				if (iHours<10)
					strTime = "0"+iHours.ToString();
				else
					strTime = iHours.ToString();

				if (iMin<10)
					strTime = strTime+":0"+iMin.ToString();
				else
					strTime = strTime+":"+iMin.ToString();

				if (iLeft<10)
					strTime = strTime+":0"+iLeft.ToString();
				else
					strTime = strTime+":"+iLeft.ToString();

				lbTimeV.GetComponent<UILabel>().text = strTime;
			}

			iTime = (Int64)((float)iTime - fTime);
		}
		else
		{
			iTime = 0;
			lbTimeV.GetComponent<UILabel>().color = Color.red;
			lbTimeV.GetComponent<UILabel>().text = "已过期";
		}

		sdActGameMgr.Instance.SetEnterExistTime(actUUID, (UInt64)iTime);
	}
	
	void OnClick()
	{
		if (gameObject)
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (bossWnd)
				{
					bossWnd.SetAllLBItemUnSelected();
					bSelect = true;
					ReflashSelectUI();
					bossWnd.m_SelectUUID = actUUID;

					SAbyssLockInfo item = sdActGameMgr.Instance.GetEnterInfo(actUUID);
					if (item!=null)
						bossWnd.OnSelectBossSetUI(item);
				}
			}
		}
	}
	
	public void SetIdAndReflashUI(UInt64 id)
	{
		if (id==UInt64.MaxValue) 
		{
			actUUID = UInt64.MaxValue;
			gameObject.SetActive(false);
			iTime = 0;
			return;
		}
		
		gameObject.SetActive(true);
		actUUID = id;
		bSelect = false;
		ReflashSelectUI();

		SAbyssLockInfo item = sdActGameMgr.Instance.GetEnterInfo(actUUID);
		if (item!=null)
		{
			iTime = (Int64)item.m_AbyssExistTime;
			Hashtable info = sdConfDataMgr.Instance().GetLapBossTemplate(item.m_ActTmpId.ToString());
			if (info != null)
			{
				if (lbName)
					lbName.GetComponent<UILabel>().text = info["AbyName"].ToString();
				if (lbDesc)
					lbDesc.GetComponent<UILabel>().text = info["AbyDes"].ToString();
				if (lbFindV)
					lbFindV.GetComponent<UILabel>().text = item.m_RoleName;
				if (lbTimeV)
					lbTimeV.GetComponent<UILabel>().text = "00:00:00";
			}
		}
	}

	public void SetSelect(bool bSet)
	{
		bSelect = bSet;
		ReflashSelectUI();
	}
	
	public void ReflashSelectUI()
	{
		if (bgLine)
			bgLine.SetActive(bSelect);

		if (select)
			select.SetActive(bSelect);

//		if (bSelect==true)
//			gameObject.transform.localPosition = new Vector3(-20.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
//		else
//			gameObject.transform.localPosition = new Vector3(22.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
	}
}
