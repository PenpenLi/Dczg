using UnityEngine;
using System.Collections;
using System;

public class sdUILapBossWndBtn : MonoBehaviour
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
		if (gameObject.name=="TabPlayList")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd lapBossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (lapBossWnd)
				{
					lapBossWnd.SetShowPanelType(1);
					lapBossWnd.OnHideBossUI();
				}

				sdActGameMsg.Send_CS_GET_ABYSS_OPEN_LIST_REQ();
			}
		}
		else if (gameObject.name=="TabLockList")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd lapBossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (lapBossWnd)
				{
					lapBossWnd.SetShowPanelType(2);
					lapBossWnd.OnHideBossUI();
				}

				sdActGameMsg.Send_CS_GET_ABYSS_TRIGGER_LIST_REQ();
			}
		}
		else if (gameObject.name=="TabLogList")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd lapBossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (lapBossWnd)
				{
					lapBossWnd.SetShowPanelType(3);
					lapBossWnd.OnHideBossUI();
				}

				sdActGameMsg.Send_CS_GET_ABYSS_OPEN_REC_REQ();
			}
		}
		else if (gameObject.name=="TabDesc")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd lapBossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (lapBossWnd)
				{
					lapBossWnd.SetShowPanelType(4);
					lapBossWnd.OnHideBossUI();
				}
			}
		}
		else if (gameObject.name=="wndClose")
		{
			if( sdActGameControl.m_UILapBossWnd != null )			
				sdActGameControl.Instance.CloseGameWnd(sdActGameControl.m_UILapBossWnd);
		}
		else if (gameObject.name=="btnEnter")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (bossWnd)
				{
					if (bossWnd.m_SelectUUID == UInt64.MaxValue)
					{
						sdUICharacter.Instance.ShowOkMsg("请选择合适的深渊", null);
					}
					else
					{
						SAbyssLockInfo info = sdActGameMgr.Instance.GetEnterInfo(bossWnd.m_SelectUUID);
						if (info!=null)
						{
							int iTmpID = (int)info.m_ActTmpId;
							string strLevelID = sdConfDataMgr.Instance().GetLapBossTemplateValueByStringKey(iTmpID, "AbyssLevelID");
							int iLevelID = int.Parse(strLevelID);

							bool bSelf = false;
							//string strName = sdGameLevel.instance.mainChar.GetProperty()["Name"].ToString();
							UInt64 uuMyID = UInt64.Parse(sdGameLevel.instance.mainChar.GetProperty()["DBID"].ToString());
							if (uuMyID==info.m_Roleid)
								bSelf=true;
							
							if (bSelf)
							{
								int iEP = int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["EP"].ToString());
								int iNeedEP = int.Parse(sdConfDataMgr.Instance().GetLapBossTemplateValueByStringKey(iTmpID, "CostEP"));
								if (iNeedEP>iEP)
								{
									sdUICharacter.Instance.ShowOkMsg("您的体力值不足", null);
									return;
								}
							}
							else
							{
								int iAP = int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["AP"].ToString());
								int iNeedAP = int.Parse(sdConfDataMgr.Instance().GetLapBossTemplateValueByStringKey(iTmpID, "CostAP"));
								if (iNeedAP>iAP)
								{
									sdUICharacter.Instance.ShowOkMsg("您的协助点数不足", null);
									return;
								}
							}

							if (info.m_AbyssExistTime==0)
							{
								sdUICharacter.Instance.ShowOkMsg("该深渊已经过期", null);
								return;
							}

							//发送协议
							CliProto.CS_LEVEL_REQ refMSG = new CliProto.CS_LEVEL_REQ();
							refMSG.m_LevelBattleType = (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS;
							refMSG.m_LevelID = (uint)iLevelID;
							sdLevelInfo.SetCurLevelId((int)refMSG.m_LevelID);
							refMSG.m_BuffID = 0;
							refMSG.m_AbyssDBID = info.m_ActDBID;
							SDNetGlobal.SendMessage(refMSG);
							
							// 通知宠物管理器..
							sdNewPetMgr.Instance.OnEnterLevel();	
							sdUICharacter.Instance.iCurrentLevelID = iLevelID;
							sdUICharacter.Instance.bCampaignLastLevel = false;

							bossWnd.m_LevelID = (uint)iLevelID;

							sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_LapBoss);
						}
						else
						{
							sdUICharacter.Instance.ShowOkMsg("请选择合适的深渊", null);
						}
					}
				}
			}
		}
		else if (gameObject.name=="btnUnlock")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUILapBossWnd bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
				if (bossWnd)
				{
					if (bossWnd.m_SelectUUID == UInt64.MaxValue)
					{
						sdUICharacter.Instance.ShowOkMsg("请选择合适的深渊", null);
					}
					else
					{
						SAbyssLockInfo info = sdActGameMgr.Instance.GetLockInfo(bossWnd.m_SelectUUID);
						if (info!=null)
						{
							if (info.m_EntranceExistTime==0)
								sdUICharacter.Instance.ShowOkMsg("该深渊入口已经过期", null);
							else
								sdActGameMsg.Send_CS_ABYSS_OPEN_REQ(bossWnd.m_SelectUUID);
						}
						else
						{
							sdUICharacter.Instance.ShowOkMsg("请选择合适的深渊", null);
						}
					}

					sdNewInfoMgr.Instance.ClearNewInfo(NewInfoType.Type_LapBoss);
				}
			}
		}
	}

	public static void OnLoadFightUI(ResLoadParams param, UnityEngine.Object obj)
	{
		SDGlobal.Log("PreLoad FightUI!");
	}
}