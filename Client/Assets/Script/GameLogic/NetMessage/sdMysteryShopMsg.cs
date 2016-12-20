using UnityEngine;
using System;
using System.Collections;


public class sdMysteryShopMsg : UnityEngine.Object
{
    public static bool init()
    {
        SDGlobal.Log("sdMysteryshopmsg.init");
        /* 神秘商店购买物品返回 */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_SECRET_BUY_ACK, msg_SC_SHOP_SECRET_BUY_ACK);
        /* 神秘商店物品列表返回 */
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_SECRET_GOODS_ACK, msg_SC_SHOP_SECRET_GOODS_ACK);
        return true;
    }
    /* 神秘商店购买物品返回 */
    private static void msg_SC_SHOP_SECRET_BUY_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_SHOP_SECRET_BUY_ACK refMsg = (CliProto.SC_SHOP_SECRET_BUY_ACK)msg;
        for (int index = 0; index < sdMysteryShopMgr.Instance.m_lstItem.Count; ++index)
        {
            CliProto.SSecretItemInfo item = sdMysteryShopMgr.Instance.m_lstItem[index];
            if (item.m_UID == refMsg.m_UID)
            {
                if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_Success)
                {
                    sdUICharacter.Instance.ShowOkMsg("兑换成功！", null);
                    item.m_Bought = 1;
                }
                else if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_BagFull)
                    sdUICharacter.Instance.ShowOkMsg("背包已满，无法兑换！", null);
                else if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_NoCrystal)
                    sdUICharacter.Instance.ShowOkMsg("神秘水晶不足，无法兑换！", null);
                else if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_NoCash)
                    sdUICharacter.Instance.ShowOkMsg("徽章不足，无法兑换", null);
                else if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_NoMoney)
                    sdUICharacter.Instance.ShowOkMsg("金钱不足，无法兑换", null);
                else if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_Fail)
                    sdUICharacter.Instance.ShowOkMsg("兑换失败！", null);
                break;
            }
        }
        sdMysteryShopMgr.Instance.Refresh();
    }
    /* 神秘商店物品列表返回 */
    private static void msg_SC_SHOP_SECRET_GOODS_ACK(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_SHOP_SECRET_GOODS_ACK refMsg = (CliProto.SC_SHOP_SECRET_GOODS_ACK)msg;
        sdMysteryShopMgr.Instance.m_freeRefreshTime = refMsg.m_FreeRefreshRemainCount;
        sdMysteryShopMgr.Instance.m_nTimeTick = (long)refMsg.m_RefreshRemainTimeMS/1000 + System.DateTime.Now.Ticks/10000000L;
        sdMysteryShopMgr.Instance.m_lstItem.Clear();
        for (int index = 0; index < refMsg.m_ItemCount; ++index)
        {
            CliProto.SSecretItemInfo item = new CliProto.SSecretItemInfo();
            item.m_UID = refMsg.m_SecretItems[index].m_UID;
            item.m_Bought = refMsg.m_SecretItems[index].m_Bought;
            sdMysteryShopMgr.Instance.m_lstItem.Add(item);
        }
        sdMysteryShopMgr.Instance.Refresh();
    }
    /* 神秘商店物品列表请求 */
    public static bool Send_SHOP_SECRET_GOODS_REQ()
    {
        CliProto.CS_SHOP_SECRET_GOODS_REQ msg = new CliProto.CS_SHOP_SECRET_GOODS_REQ();
        SDNetGlobal.SendMessage(msg);
        return true;
    }
    /* 神秘商店购买物品请求 */
    public static bool Send_SHOP_SECRET_BUY_REQ(int uid)
    {
        CliProto.CS_SHOP_SECRET_BUY_REQ msg = new CliProto.CS_SHOP_SECRET_BUY_REQ();
        msg.m_UID = uid;
        SDNetGlobal.SendMessage(msg);
        return true;
    }
    /* 神秘商店刷新请求*/
    public static bool Send_SHOP_SECRET_REFRESH_REQ()
    {
        CliProto.CS_SHOP_SECRET_REFRESH_REQ msg = new CliProto.CS_SHOP_SECRET_REFRESH_REQ();
        SDNetGlobal.SendMessage(msg);
        return true;
    }
}