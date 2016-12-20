using UnityEngine;
using System.Collections;
using System;

public class sdUIWorldBossWndBtn : MonoBehaviour
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
			//直接关闭界面..
			if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.MainCity 
			    ||sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldMap)
			{
				if( sdActGameControl.m_UIWorldBossWnd != null )			
					sdActGameControl.Instance.CloseGameWnd(sdActGameControl.m_UIWorldBossWnd);
			}
			else
			{
				if( sdActGameControl.m_UIWorldBossWnd != null )			
					sdActGameControl.Instance.CloseGameWnd(sdActGameControl.m_UIWorldBossWnd);

				//退出世界Boss场景..
				sdUICharacter.Instance.TuiTu_To_WorldMap();
			}
		}
		else if (gameObject.name=="btnMoney")
		{
			if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2)
			{
				sdActGameMsg.Send_CS_WB_ADD_BUF_REQ(0);
			}
		}
		else if (gameObject.name=="btnRmb")
		{
			if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2)
			{
				sdActGameMsg.Send_CS_WB_ADD_BUF_REQ(1);
			}
		}
		else if (gameObject.name=="TabRank")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
				if (bossWnd)
				{
					bossWnd.ShowRightUIPanel(false);
				}
			}
		}
		else if (gameObject.name=="TabDesc")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIWorldBossWnd bossWnd = wnd.GetComponentInChildren<sdUIWorldBossWnd>();
				if (bossWnd)
				{
					bossWnd.ShowRightUIPanel(true);
				}
			}
		}
		else if (gameObject.name=="btnBegin")
		{
			//可以进入BOSS场景..
			if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.MainCity 
			    ||sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldMap)
			{
				if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2)
				{
					int iLevelID = sdActGameMgr.Instance.GetWorldBossLevelID();
					if (iLevelID>0)
					{
						//发协议..
						if (sdActGameMgr.Instance.m_WorldBossInfo.m_ReliveTime>0)
							sdActGameMsg.Send_CS_WB_RELIVE_REQ();
						else
							sdActGameMsg.Send_CS_WB_ENTER_REQ();
					}
				}
			}
			//已经在BOSS场景中，请求复活..
			else
			{
				if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2&&sdActGameMgr.Instance.m_WorldBossInfo.m_ReliveTime>0)
				{
					sdActGameMsg.Send_CS_WB_RELIVE_REQ();
				}
				else if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2&&sdActGameMgr.Instance.m_WorldBossInfo.m_ReliveTime<=0)
				{
					sdActGameMsg.Send_CS_WB_ENTER_REQ();
				}
			}
		}
		else if (gameObject.name=="worldBossBtn")
		{
			sdActGameControl.Instance.ActiveWorldBossWnd(null);
		}
		//活动平台的按钮事件..
		else if (gameObject.name=="btnActClose")
		{
			if (sdUICharacter.Instance.GetTownUI() != null)
			{
				sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>().lockPanel.SetActive(false);
			}

			if( sdActGameControl.m_UIActBaseWnd != null )			
				sdActGameControl.Instance.CloseGameWnd(sdActGameControl.m_UIActBaseWnd);
		}
		else if (gameObject.name=="btnLapBoss")
		{
			sdActGameControl.Instance.ActiveLapBossWnd(null, 1);
			sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_LapBoss);
		}
		else if (gameObject.name=="btnWorldBoss")
		{
			sdActGameControl.Instance.ActiveWorldBossWnd(null);
		}
	}

	public static void OnLoadFightUI(ResLoadParams param, UnityEngine.Object obj)
	{
		SDGlobal.Log("PreLoad FightUI!");
	}
}