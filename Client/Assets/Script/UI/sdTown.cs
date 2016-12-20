using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;


class sdTown : MonoBehaviour
{
	public GameObject worldObj		= null;
	public GameObject townObj		= null;
	public GameObject jobIcon		= null;
	public GameObject chatInput		= null;
	public GameObject PlayerHeadIcon= null;
	public UILabel	  ExpNum		= null;
	public UISprite	  ExpBar		= null;
	public bool mCheckSystemLock	= true;
    public GameObject chatMsg = null;

	float LastExp = -1;

	void Awake()
	{
		sdUICharacter.Instance.isInit = false;

		GameObject panel = GameObject.Find("Sys1");
		if( panel != null )
		{
			if(panel.GetComponent<UIAnchor>()) panel.GetComponent<UIAnchor>().enabled = false;

			int titleH = 100;
			int sysH = 620;
			int h = Screen.height * 1280 / Screen.width;
			float r = (float)(h-titleH) / (float)sysH;
			panel.transform.localScale = new Vector3(r,r,r);

			float r0 = (float)Screen.width / (float)Screen.height;
			float x;
			if( r0 >= 1.77f )		x = 0; 							// 16:9
			else if( r0 >= 1.66f ) 	x = 11.0f - 100.0f*(r0-1.66f);	// 800:480
			else if( r0 >= 1.64f ) 	x = 12.0f;						// 1184:720
			else if( r0 >= 1.60f ) 	x = 18.0f - 120.0f*(r0-1.60f);	// 16:10
			else if( r0 >= 1.50f ) 	x = 27.0f - 100.0f*(r0-1.50f);	// 3:2
			else 					x = 49.0f - 130.0f*(r0-1.33f);	// 4:3
			panel.transform.localPosition = new Vector3(751.0f+x,(float)titleH/(-2.0f),panel.transform.localPosition.z);

            Hashtable data = sdGlobalDatabase.Instance.globalData;
            if (!data.ContainsKey("OpenLevel_Index") || !data.ContainsKey("OpenLevel_FirstTime") || (int)data["OpenLevel_FirstTime"] == 0)
            {
                CheckSystemLock();
            }
		}

		sdUICharacter.Instance.SetTownUI(gameObject);
	}

    public void AddChat(string info)
    {
        chatMsg.transform.GetChild(0).GetComponent<UILabel>().text = info;
    }

    public void ShowChat()
    {
        chatMsg.SetActive(true);
    }

    public void HideChat()
    {
        chatMsg.SetActive(false);
    }

	// 新系统解锁..
	public void CheckSystemLock()
	{
		/*
		ShowSystemUnlockWnd("Btn_Pet");
		return;
		*/

		GameObject panel = GameObject.Find("Sys1");
		Hashtable table = sdConfDataMgr.Instance().GetTable("systemlock");
		if( table != null )
		{
			foreach(DictionaryEntry de in table)
			{
				string sys = de.Key as string;
				Hashtable t = de.Value as Hashtable;
				
				bool bLock = true;
				if( sdConfDataMgr.Instance().GetRoleSetting(sys) != "" )
				{
					// 之前就打开的系统..
					bLock = false;
				}
				else
				{
					// 是否新解锁系统..
					int condition = int.Parse(t["condition"] as String);
					if( condition >= 10000 )
					{
						// 关卡为解锁条件..
						for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
						{
							if( sdLevelInfo.levelInfos[i].levelID == condition )
							{
								if( sdLevelInfo.levelInfos[i].valid ) bLock = false;
								break;
							}
						}
					}
					else
					{
						// 等级为解锁条件..
                        Hashtable role = sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable;

                        if ((int)role["Level"] >= condition) bLock = false;
					}
					
					// 新解锁系统..
					if( bLock == false )
					{
						sdConfDataMgr.Instance().SetRoleSetting(sys,"1");
						// 先关掉系统解锁效果..
						 ShowSystemUnlockWnd(sys); 
					}
				}
				
				// 系统开关..
				Transform btn = panel.transform.FindChild(sys);
				Transform spLock = btn.FindChild("sp_lock");
				if( bLock == false )
				{
					if( spLock != null )
						spLock.gameObject.SetActive(false);
					btn.GetComponent<sdRoleWndButton>().mSystemLock		= false;
					UISprite sp = btn.FindChild("Background").GetComponent<UISprite>();
					sp.color = new Color(1.0f,1.0f,1.0f);
				}
				else
				{
					btn.GetComponent<sdRoleWndButton>().mSystemLock		= true;
					btn.GetComponent<sdRoleWndButton>().mSystemLockInfo	= t["lockinfo"] as String;
					UISprite sp = btn.FindChild("Background").GetComponent<UISprite>();
					sp.color = new Color(0.7f,0.7f,0.7f);
				}
			}
		}
	}

    public GameObject lockPanel = null;

	public GameObject SystemUnlockWnd = null;
	bool isLoadSystemUnlockWnd = false;
	void ShowSystemUnlockWnd(string sys)
	{
		if( SystemUnlockWnd == null ) 
		{
			if(isLoadSystemUnlockWnd) return;
			ResLoadParams param = new ResLoadParams();
			param.info = sys;
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$SystemUnlockWnd.prefab",LoadWnd,param);
			isLoadSystemUnlockWnd = true;
            if (lockPanel != null) lockPanel.SetActive(true);
		}
		else
		{
			SystemUnlockWnd.SetActive(true);
			Hashtable table = sdConfDataMgr.Instance().GetTable("systemlock");
			Hashtable table2 = table[sys] as Hashtable;

            GameObject obj_Sys1 = GameObject.Find("Sys1");
            if (obj_Sys1 == null) return;
			SystemUnlockWnd.transform.FindChild("bg").FindChild("btn_ok").GetComponent<SystemUnlockBtn>().ShowWnd( 
				sys,
				GameObject.Find("Sys1").transform.FindChild(sys).FindChild("Background").GetComponent<UISprite>().spriteName, 
				table2["unlockinfo"] as String );
		}
	}
	void LoadWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null)
		{
			Debug.Log("SystemUnlockWnd loadwnd null");
			return;
		}

		SystemUnlockWnd = GameObject.Instantiate(obj) as GameObject;
		SystemUnlockWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		SystemUnlockWnd.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		SystemUnlockWnd.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		isLoadSystemUnlockWnd = false;
		ShowSystemUnlockWnd(param.info);
        if (lockPanel != null) lockPanel.SetActive(false);
	}

	
	public string GetChatInfo()
	{
		if (chatInput != null)
		{
			return chatInput.GetComponent<UIInput>().value;
		}
		return "";
	}
	
	void Start()
	{
		if (sdUICharacter.Instance != null)
		{
			sdUICharacter.Instance.isReady = true;	
			sdUICharacter.Instance.JumpOver();
		}

        if (sdGuideMgr.Instance.HasCanRun()) lockPanel.SetActive(true);

		if (sdGameLevel.instance != null && worldObj != null && townObj != null)
		{
			if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldMap)
			{
				worldObj.SetActive(true);
				townObj.SetActive(false);
			}
			else
			{
				worldObj.SetActive(false);
				townObj.SetActive(true);
			}
		}
	}
	
	bool needUpdate = true;
	
	void Update()
	{
		if (needUpdate && jobIcon != null && sdConfDataMgr.Instance().jobAtlas != null && sdGameLevel.instance != null)
		{
			jobIcon.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().jobAtlas;
			jobIcon.GetComponent<UISprite>().spriteName = sdConfDataMgr.Instance().GetJobIcon(((int)sdGameLevel.instance.mainChar.GetJob()).ToString());
			needUpdate = false;
		}

		// exp
		if( LastExp != ExpBar.fillAmount )
		{
			LastExp = ExpBar.fillAmount;
			ExpNum.text = "EXP " + (int)(ExpBar.fillAmount*100) + "%";
		}

		/*
		if( mCheckSystemLock )
		{
			CheckSystemLock();
			mCheckSystemLock = false;
		}
		*/
	}
	
	void OnDestroy()
	{
		if (sdUICharacter.Instance != null) sdUICharacter.Instance.isReady = false;	
	}
}