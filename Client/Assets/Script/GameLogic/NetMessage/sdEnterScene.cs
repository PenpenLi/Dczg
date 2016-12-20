using UnityEngine;
using System.Collections;
using System.Threading;
using System.Text;

public class sdEnterScene : Object
{

	private static bool beginLoadSceneFromMainThread = false;
	private static object lockObject = new object();
	
	public static bool getLoadFlag()
	{
		lock(lockObject)
		{
			return beginLoadSceneFromMainThread;
		}
	}
	
	public static void setLoadFlag(bool val)
	{
		lock(lockObject)
		{
			beginLoadSceneFromMainThread = val;
		}
	}
	
	public static bool init()
	{
		SDGlobal.Log("sdEnterScene.init");
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_LOADSCENE, msg_SCID_SELF_LOADSCENE);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_BASE_PRO, msg_SCID_SELF_BASE_PRO);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_DATA_END, msg_SCID_SELF_DATA_END);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_ENTERSCENE, msg_SCID_SELF_ENTERSCENE);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_TREASURE_CHEST_NTF, msg_SC_TREASURE_CHEST_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SELF_VOLATILE_PRO, msg_SCID_SELF_VOLATILE_PRO);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GSID_SWITCHSCENE_REQ, msg_GSID_SWITCHSCENE);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_ERROR_NTF, msg_SCID_ERROR_NTF);
        return true;
	}

    public static void finl()
    {
    }
	
	public static bool notifyWantEnterScene(uint sceneId)
	{
		CliProto.CS_ENTERSCENE refMSG = new CliProto.CS_ENTERSCENE();
		refMSG.m_SceneId = (uint)sdGlobalDatabase.Instance.globalData["SceneId"];
		refMSG.m_Error = 0;
		//refMSG.szRoleName = System.Text.Encoding.Default.GetBytes(SDNetGlobal.playerList[selRole.currentSelect].name);
		SDNetGlobal.SendMessage(refMSG);

		SDGlobal.Log("-> CS_ENTERSCENE");

		return true;
	}

    private static void msg_SCID_ERROR_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_ERROR_NTF refMSG = (CliProto.SC_ERROR_NTF)msg;
        if (refMSG.m_Data.m_Type == 1)
        {
            Debug.Log(Encoding.UTF8.GetString(refMSG.m_Data.m_Error));
        }
        else if (refMSG.m_Data.m_Type == (int)HeaderProto.EErrorType.ERROR_TYPE_SHOWBOX)
        {
            sdUICharacter.Instance.ShowOkMsg(System.Text.Encoding.UTF8.GetString(refMSG.m_Data.m_Error), null);
        }
        else
        {
            sdUICharacter.Instance.ShowMsgLine(System.Text.Encoding.UTF8.GetString(refMSG.m_Data.m_Error), Color.yellow);
        }
	}
	
	private static void msg_SCID_SELF_LOADSCENE(int iMsgID, ref CMessage msg)
	{
        
		CliProto.SC_SELF_LOADSCENE refMSG = (CliProto.SC_SELF_LOADSCENE)msg;

        if (SDNetGlobal.bReConnectGate)
        {
            Debug.Log("Scene Type " + refMSG.m_SceneType);
            return;
        }

		SDGlobal.Log("<- SCID_SELF_LOADSCENE : server finished scene create and load, notify client " +
			"start to load scene and server start to send init data to client!");
		sdGlobalDatabase.Instance.globalData["SceneId"]		=	(uint)refMSG.m_SceneId;
		sdGlobalDatabase.Instance.globalData["SceneType"]	=	(uint)refMSG.m_SceneType;

        SDNetGlobal.bHeartBeat = true;
	}
	
	private static void msg_SCID_SELF_VOLATILE_PRO(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELF_VOLATILE_PRO refMSG = (CliProto.SC_SELF_VOLATILE_PRO)msg;
		if (sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] != null)
		{
			((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["NonMoney"] = refMSG.m_NonMoney;
            ((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["NonCash"] = refMSG.m_NonCash;
            ((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["Cash"] = refMSG.m_Cash;
			((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["EP"] = refMSG.m_EP;
			((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["Experience"] = refMSG.m_Experience;
			((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["AP"] = refMSG.m_AP;
			sdGameLevel level = sdGameLevel.instance;
			if (level != null)
			{
				if (level.mainChar != null)
				{
					Hashtable prop =	sdConfDataMgr.CloneHashTable(((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"]));
                    prop["HP"] = level.mainChar["HP"];
                    prop["SP"] = level.mainChar["SP"];
                    level.mainChar.Property = prop;
					level.mainChar.RefreshProp();
                    Debug.Log("msg_SCID_SELF_VOLATILE_PRO");
				}

				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUILapBossWnd lapBossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
					if (lapBossWnd)
					{
						lapBossWnd.UpdateLapBossUI();
					}
				}
			}
		}

		sdMallManager.Instance.RefreshAttribute();
	}
	private static void msg_GSID_SWITCHSCENE(int iMsgID, ref CMessage msg)
	{
		CliProto.GSPKG_SWITCHSCENE_ACK refMSG = (CliProto.GSPKG_SWITCHSCENE_ACK)msg;
		SGDP.Error((uint)refMSG.m_ErrCode);
		//sdUICharacter.Instance.SHOW
	}


	
	private static void msg_SCID_SELF_BASE_PRO(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELF_BASE_PRO refMSG = (CliProto.SC_SELF_BASE_PRO)msg;
		
		if(refMSG != null)
		{
			
			refMSG.m_HP	=	refMSG.m_MaxHP;
			refMSG.m_SP	=	refMSG.m_MaxSP;
			Hashtable charProps = new Hashtable();
			charProps["HurtOtherModify"] = 0;
			charProps["PhysicsModify"] = 0;
			charProps["IceModify"] = 0;
			charProps["FireModify"] = 0;
			charProps["PoisonModify"] = 0;
			charProps["ThunderModify"] = 0;
			charProps["StayModify"] = 0;
			charProps["HoldModify"] = 0;
			charProps["StunModify"] = 0;	
			charProps["BeHurtModify"] = 0;	

			MemberInfo[] propInfo = refMSG.GetDesc();
			for(int i = 0; i< propInfo.Length; ++i)
			{
				object val = refMSG.GetMemberValue(i);
                if (propInfo[i].name == "RoleIndex")
                {
                    int value = (int)val;
                    value += 100000;
                    charProps[propInfo[i].name] = value;
                }
                else
                {
                    charProps[propInfo[i].name] = val;
                }

                
			}
            //charProps["NonCash"] = uint.MinValue;
			Hashtable compareTable = sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable;
			if (compareTable != null)
			{
				if (compareTable["Level"].ToString() != charProps["Level"].ToString())
				{
					if(sdUICharacter.Instance.lvupChange.Contains("Level"))
					{
						sdUICharacter.Instance.lvupChange["Level"] = compareTable["Level"].ToString();
					}
					else
					{
						sdUICharacter.Instance.lvupChange.Add("Level", compareTable["Level"].ToString());
					}
					
					int change = 0;
					
					int attdmg = int.Parse(charProps["AtkDmgMin"].ToString()) + int.Parse(charProps["AtkDmgMax"].ToString());
					int attcompare = int.Parse(compareTable["AtkDmgMin"].ToString()) + int.Parse(compareTable["AtkDmgMax"].ToString());
					change = (attdmg - attcompare)/2;
					if(sdUICharacter.Instance.lvupChange.Contains("AttDmg"))
					{
						sdUICharacter.Instance.lvupChange["AttDmg"] = change;
					}
					else
					{
						sdUICharacter.Instance.lvupChange.Add("AttDmg", change);
					}
					
					int def = int.Parse(charProps["Def"].ToString());
					int defcompare = int.Parse(compareTable["Def"].ToString());
					change = def - defcompare;
					if(sdUICharacter.Instance.lvupChange.Contains("Def"))
					{
						sdUICharacter.Instance.lvupChange["Def"] = change;
					}
					else
					{
						sdUICharacter.Instance.lvupChange.Add("Def", change);
					}
					
					int hp = int.Parse(charProps["HP"].ToString());
					int hpcompare = int.Parse(compareTable["HP"].ToString());
					change = hp - hpcompare;
					if(sdUICharacter.Instance.lvupChange.Contains("HP"))
					{
						sdUICharacter.Instance.lvupChange["HP"] = change;
					}
					else
					{
						sdUICharacter.Instance.lvupChange.Add("HP", change);
					}
					
					string main = "";
					int job = int.Parse(charProps["BaseJob"].ToString());
					switch (job)
					{
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:
					{
						main = "Str";
						break;	
					}
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:
					{
						main = "Int";	
						break;	
					}
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:
					{
						main = "Dex";	
						break;		
					}
					case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:
					{
						main = "Fai";	
						break;		
					}
					}
					int mainValue = int.Parse(charProps[main].ToString());
					int maincompare = int.Parse(compareTable[main].ToString());
					change = mainValue - maincompare;
					
					sdUICharacter.Instance.lvupChange[main] = change;
				}
			}
			
			sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] = charProps;
			
			sdGameLevel level = sdGameLevel.instance;
			if (level != null)
			{
				if (level.mainChar != null)
				{
					Hashtable prop =	sdConfDataMgr.CloneHashTable(charProps);
                    prop["HP"] = level.mainChar["HP"];
                    prop["SP"] = level.mainChar["SP"];
					level.mainChar.Property = prop;
					level.mainChar.RefreshProp();	
				}
			}
			
		}

		if (sdGameLevel.instance != null && sdGameLevel.instance.mainChar != null)
		{
			sdGameLevel.instance.mainChar.AddSuitBuff();
			sdGameLevel.instance.mainChar.RefreshProp();
		}

        if (sdUICharacter.Instance.playerScore > 0)
        {
            int score = sdConfDataMgr.Instance().PlayerScore();
            if (score > sdUICharacter.Instance.playerScore)
            {
                sdUICharacter.Instance.ShowScorePanel(sdUICharacter.Instance.playerScore, score);
                sdUICharacter.Instance.playerScore = score;
            }
        }
	}
	
	private static void msg_SCID_SELF_DATA_END(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELF_DATA_END refMSG = (CliProto.SC_SELF_DATA_END)msg;
		SDGlobal.Log("<- SCID_SELF_DATA_END : server finished init data sending!");
		//notifyWantEnterScene(0);
		
		// 加载场景(非主线程中启动场景加载会有问题)
		//setLoadFlag(true);
        if (SDNetGlobal.bReConnectGate)
        {
            SDNetGlobal.SendEnterScene();
            SDNetGlobal.SendCache();
            return;
        }
		sdUICharacter.LoadScene();
	}
	
	private static void msg_SCID_SELF_ENTERSCENE(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SELF_ENTERSCENE refMSG = (CliProto.SC_SELF_ENTERSCENE)msg;
		SDGlobal.Log("<- SCID_SELF_ENTERSCENE : client start load scene!@ " + (System.DateTime.Now.Ticks / 10000L).ToString());
		
		// 获取同步时间aa
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel != null)
		{
			//gameLevel.ServerTime = refMSG.m_ServerTime;
			//gameLevel.ClientTime = (ulong)(System.DateTime.Now.Ticks / 10000L);//Time.time;
			sdGlobalDatabase.Instance.ServerBeginTime = refMSG.m_ServerTime;
		}
	}
	
	private static void msg_SC_TREASURE_CHEST_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_TREASURE_CHEST_NTF refMSG = (CliProto.SC_TREASURE_CHEST_NTF)msg;
        if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.WorldMap)
        {
            sdUICharacter.Instance.AddSweepInfo(refMSG);
        }
        else
        {
            // 保存结算信息aa
            sdUICharacter.Instance.SetJiesuanMsg(refMSG);
        }
//		int count = refMSG.m_ItemList.m_Count;
//		for(int i = 0; i < count; ++i)
//		{
//			sdGameItemMgr.Instance.createItem((int)refMSG.m_ItemList.m_ItemList[i].m_TemplateId, refMSG.m_ItemList.m_ItemList[i].m_UUID, (int)refMSG.m_Pos, refMSG.m_ItemList.m_ItemList[i].m_Count);
//		}
//		sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = sdGameItemMgr.Instance.GetAllItem(2,-1);
//		sdSlotMgr.Instance.Notify((int)refMSG.m_Pos);

		// 关卡结束aa
		sdLevelInfo.EndLevel(refMSG.m_LevelID,refMSG.m_CurGetStar);

		// 显示胜利动画aa
		if (sdGameLevel.instance != null)
		{
			if (sdGameLevel.instance.mainCamera != null)
				sdGameLevel.instance.mainCamera.ShowVictory();
		}
	}
}
