using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUILapBossRecordIcon : MonoBehaviour
{
	public int index = 0;
	private UInt64 actUUID = UInt64.MaxValue;
	
	public GameObject bg = null;
	public GameObject lbDesc1 = null;
	public GameObject lbDesc2 = null;
	
	public UInt64 GetId()
	{
		return actUUID;
	}
	
	void Update()
	{
	}
	
	void OnClick()
	{
	}
	
	public void SetIdAndReflashUI(UInt64 id)
	{
		if (id==UInt64.MaxValue) 
		{
			actUUID = UInt64.MaxValue;
			gameObject.SetActive(false);
			return;
		}
		
		gameObject.SetActive(true);
		actUUID = id;
		
		SAbyssOpenRecord item = sdActGameMgr.Instance.GetRecordInfo(actUUID);
		if (item!=null)
		{
			Hashtable info = sdConfDataMgr.Instance().GetLapBossTemplate(item.m_AbyTmpid.ToString());
			if (info != null)
			{
				if (lbDesc1)
				{
					string strTemp = item.m_Rolename;
					double dbTime = (double)(item.m_Opentime);
					DateTime time = sdConfDataMgr.Instance().ConvertServerTimeToClientTime(dbTime);
					string strTime = string.Format("在{0}年{1}月{2}日{3}时{4}分{5}秒",
					                               time.Year.ToString(), time.Month.ToString(), time.Day.ToString(),
					                               time.Hour.ToString(), time.Second.ToString(), time.Minute.ToString());
					string strFinal = string.Format("{0}  {1}", strTemp.ToString(), strTime.ToString());
					lbDesc1.GetComponent<UILabel>().text = strFinal;
				}

				if (lbDesc2)
				{
					string strTemp = "开启了";
					strTemp = strTemp+info["AbyLevel"].ToString();
					strTemp = strTemp+"级深渊:";
					strTemp = strTemp+info["AbyName"].ToString();
					strTemp = strTemp+"!";
					lbDesc2.GetComponent<UILabel>().text = strTemp;
				}

				if (index>=0)
				{
					int iSY = index%2;
					if (iSY==0)
						bg.SetActive(false);
					else
						bg.SetActive(true);
				}
			}
		}
	}
}
