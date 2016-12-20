using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class sdLevelMsg : UnityEngine.Object
{
    public static bool init()
    {
        SDGlobal.Log("sdLevelMsg.init");
        /* 推图数据 */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_LEVEL_ACK, OnMessage_SCID_LEVEL_ACK);
        return true;
    }

    private static void OnMessage_SCID_LEVEL_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_LEVEL_ACK refMSG = (CliProto.SC_LEVEL_ACK)msg;

        if (refMSG.m_LevelBattleType == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_NORMAL)
        {
            if (refMSG.m_Result == 0)
            {
                sdGlobalDatabase.Instance.globalData["TuituAck"] = 1;
                sdLevelInfo.BeginLevel();
            }
            else
            {
                sdUILoading.UnactiveLoadingUI();
                sdUICharacter.Instance.ShowOkMsg(SGDP.ErrorString(refMSG.m_Result), null);
                return;
            }
        }
        else if (refMSG.m_LevelBattleType == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
        {
            GameObject wnd = sdGameLevel.instance.NGUIRoot;
            sdUILapBossWnd bossWnd = null;
            if (wnd)
                bossWnd = wnd.GetComponentInChildren<sdUILapBossWnd>();
            if (bossWnd == null)
                return;

            if (refMSG.m_Result == 0)
            {
                //申请进入场景..
                BundleGlobal.SetBundleDontUnload("UI/$FightUI.unity3d");
                sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$Fight.prefab", sdUILapBossWndBtn.OnLoadFightUI, null);
                bossWnd.setbossmodelVisible(false);
                sdUILoading.ActiveLoadingUI("forest", "欢迎进入深渊BOSS领地");

                bossWnd.m_bTuituAck = true;
            }
            else
            {
                sdUILoading.UnactiveLoadingUI();
                bossWnd.setbossmodelVisible(true);
                SGDP.Error(refMSG.m_Result);
                return;
            }
        }
        else if (refMSG.m_LevelBattleType == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_PET_TRAIN)
        {
            if (refMSG.m_Result == 0)
            {
                sdPTManager.Instance.m_nAttack = 0;
                sdNewPetMgr.Instance.OnEnterLevel();
 
                string bundlePath = "";
                string levelName = "";
                int index;
                Hashtable ptTable = sdConfDataMgr.Instance().GetTable("dmdsxactivitytemplateconfig.pttemplates");
                Hashtable table = ptTable[sdPTManager.Instance.m_ChoiceLevel.ToString()] as Hashtable;
                int level = int.Parse(table["LevelID"].ToString());
                for (int i = 0; i < sdLevelInfo.levelInfos.Length; i++)
                {
                    if (sdLevelInfo.levelInfos[i].levelID == level)
                    {
                        string str = (string)sdLevelInfo.levelInfos[i].levelProp["Scene"];
                        bundlePath = str + ".unity.unity3d";
                        index = str.LastIndexOf("/");
                        levelName = str.Substring(index + 1);
                        break;
                    }
                }
                BundleGlobal.Instance.StartLoadBundleLevel(bundlePath, levelName);
            }
            else
            {
                sdUILoading.UnactiveLoadingUI();
                sdUICharacter.Instance.ShowOkMsg(SGDP.ErrorString(refMSG.m_Result), null);
                return;
            }
        }

        Debug.Log("tuitu ack");
        sdUICharacter.Instance.SetBattleType(refMSG.m_LevelBattleType);
        //sdUICharacter.Instance.SetFreeMedicineNum((int)refMSG.m_FreePotionCount);
        sdUICharacter.Instance.SetFreeReliveNum((int)refMSG.m_FreeReliveCount);
        //sdUICharacter.Instance.SetMedicinePrice((int)refMSG.m_PotionPrice);
        sdUICharacter.Instance.SetRelivePrice((int)refMSG.m_RelivePrice);

        // 初始化掉落表..
        CliProto.SMonsterInfos kSrcMonsterInfoList = refMSG.m_Monster;
        SDGlobal.msMonsterDropTable = new Hashtable();
        for (int i = 0; i < kSrcMonsterInfoList.m_Count; i++)
        {
            CliProto.SMonsterInfo kSrcMonsterInfo = kSrcMonsterInfoList.m_List[i];
            CliProto.SDropInfos kSrcMonsterDrops = kSrcMonsterInfo.m_Drop;

            SDMonsterDrop kMonsterDropItem = new SDMonsterDrop();
            kMonsterDropItem.money = (int)kSrcMonsterDrops.m_Money;
            kMonsterDropItem.items = new int[kSrcMonsterDrops.m_Count];
            kMonsterDropItem.itemCount = new int[kSrcMonsterDrops.m_Count];

            CliProto.SDropInfo[] kSrcMonsterDropInfo = kSrcMonsterDrops.m_List;
            for (int j = 0; j < kSrcMonsterDrops.m_Count; j++)
            {
                kMonsterDropItem.items[j] = (int)kSrcMonsterDropInfo[j].m_TemplateID;
                kMonsterDropItem.itemCount[j] = kSrcMonsterDropInfo[j].m_Count;
            }

            SDGlobal.msMonsterDropTable[(uint)kSrcMonsterInfo.m_Index] = kMonsterDropItem;
        }

        int iCount = refMSG.m_InitialBuffCount;
        if (iCount != 0)
        {
            int[] buffArray = new int[iCount];
            for (int i = 0; i < iCount; i++)
            {
                buffArray[i] = (int)refMSG.m_InitialBuffID[i];
            }
            sdGlobalDatabase.Instance.globalData["InitBuff"] = buffArray;
        }
    }
}