using UnityEngine;
using System.Collections;
using System;

public class sdGlobalDatabase : Singleton<sdGlobalDatabase>
{
	protected ulong mTimeFromServerBeginTime;
	public ulong TimeFromServerBeginTime
	{
		get { return mTimeFromServerBeginTime; }
	}

	protected ulong mServerBeginTime = 0;
	public ulong ServerBeginTime
	{
		set 
		{
			mServerBeginTime = value;
			mTimeFromServerBeginTime = 0;
		}
		get { return mServerBeginTime;}	
	}

	static private long s_LastTime;
	void Update() 		
	{
		mTimeFromServerBeginTime += (ulong)((DateTime.Now.Ticks - s_LastTime)/10000L);
		s_LastTime = DateTime.Now.Ticks;

	}

	public void Start()
	{
		s_LastTime = DateTime.Now.Ticks;		
	}

	public Hashtable globalData = new Hashtable();
	public	sdGlobalDatabase()
	{
		globalData["AutoMode"]		= true;
		globalData["FullAutoMode"]	= false;
	}
	public bool init(bool testMode)
	{
		
		if(testMode)
		{
			Hashtable itemInfo = new Hashtable();
			globalData["MainCharItemInfo"] = itemInfo;
			
			sdGameItem item = null;
			item = sdGameItemMgr.Instance.getItem(1000 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
			
			item = sdGameItemMgr.Instance.getItem(1001 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
			
			item = sdGameItemMgr.Instance.getItem(1002 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
			
			item = sdGameItemMgr.Instance.getItem(1003 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
			
			item = sdGameItemMgr.Instance.getItem(1005 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
			
			item = sdGameItemMgr.Instance.getItem(1006 + sdGameItemMgr.testIndex);
			itemInfo[item.instanceID] = item;
		}		
		
		return true;
	}
}
