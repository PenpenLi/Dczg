using UnityEngine;
using System.Collections;

public class sdMallMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_ACTION_POINT_BOUGHT_NTF,
		                            msg_SCID_SHOP_ACTION_POINT_BOUGHT_NTF);
		
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_BUY_ACTION_POINT_ACK,
		                            msg_SCID_SHOP_BUY_ACTION_POINT_ACK);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_CHEAP_NTF,
		                            msg_SCID_SHOP_BUY_PET_INFO_CHEAP_NTF);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF,
		                            msg_SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_BUY_PET_ACK,
		                            msg_SCID_SHOP_BUY_PET_ACK);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_GET_GOODSLIST_ACK,
		                            msg_SCID_SHOP_GET_GOODSLIST_ACK);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SHOP_BUY_GOODS_ACK,
		                            msg_SCID_SHOP_BUY_GOODS_ACK);

		SDNetGlobal.setCallBackFunc ((ushort)CliProto.EN_CliProto_MessageID.SCID_VIP_INFO_NTF,
		                             msg_SCID_VIP_INFO_NTF);

		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_VIP_RMBINFO_NTF,
		                            msg_SCID_VIP_RMBINFO_NTF);
	
		return true;
	}

	private static void msg_SCID_SHOP_ACTION_POINT_BOUGHT_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_ACTION_POINT_BOUGHT_NTF refMsg = (CliProto.SC_SHOP_ACTION_POINT_BOUGHT_NTF)msg;
		uint count = refMsg.m_BoughtCount;
		sdMallManager.Instance.ShowBoughtResult (count);
	}
	private static void msg_SCID_SHOP_BUY_ACTION_POINT_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_BUY_ACTION_POINT_ACK refMsg = (CliProto.SC_SHOP_BUY_ACTION_POINT_ACK)msg;
	}
	private static void msg_SCID_SHOP_BUY_PET_INFO_CHEAP_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_BUY_PET_INFO_CHEAP_NTF refMsg = (CliProto.SC_SHOP_BUY_PET_INFO_CHEAP_NTF)msg;
        sdMallManager.Instance.PetRemainTimeCheap = refMsg.m_RemainTimeCheap;
        Debug.Log(string.Format("SCID_SHOP_BUY_PET_INFO_CHEAP_NTF消息，设置低级战魂上次免费购买时间为{0}",
            refMsg.m_RemainTimeCheap));
	}
	private static void msg_SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF refMsg = (CliProto.SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF)msg;
		sdMallManager.Instance.PetRemainCount = refMsg.m_RemainCount;
        sdMallManager.Instance.PetRemainTimeExpensive = refMsg.m_RemainTimeExpensive;
        Debug.Log("SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF消息，设置高档战魂距离下次获得紫色战魂所需购买次数为 " + refMsg.m_RemainCount);
        Debug.Log(string.Format("SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF消息，设置高级战魂上次免费购买时间为{0}",
            refMsg.m_RemainTimeExpensive));
	}

    //抽战魂反馈
	private static void msg_SCID_SHOP_BUY_PET_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_BUY_PET_ACK refMsg = (CliProto.SC_SHOP_BUY_PET_ACK)msg;

        Debug.Log("SCID_SHOP_BUY_PET_ACK消息");

        if (refMsg.m_Result == 0)
        {
            sdMallManager.Instance.SetPetTemplates(refMsg.m_TemplateID, refMsg.m_Count);
            if (sdMallManager.Instance.PetResultSize == 1)
            {
                Debug.Log("SCID_SHOP_BUY_PET_ACK消息, 获得一个战魂");
                sdMallManager.Instance.ActiveOnePetObtainPanel(refMsg.m_Type, sdMallManager.Instance.GetResultTemplates(0));
            }
            else if (sdMallManager.Instance.PetResultSize > 1)
            {
                Debug.Log("SCID_SHOP_BUY_PET_ACK消息, 获得多个战魂");
                sdMallManager.Instance.ActiveTenPetObtainPanel();
            }
            else
            {
                sdUICharacter.Instance.ShowOkMsg("没有获得战魂", null);
            }
        }
        else
        {
            //todo 返回错误消息
            Debug.Log("SCID_SHOP_BUY_PET_ACK消息，没有成功获得战魂");
        }
	}

	private static void msg_SCID_SHOP_GET_GOODSLIST_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_GET_GOODSLIST_ACK refMsg = (CliProto.SC_SHOP_GET_GOODSLIST_ACK)msg;
		sdMallManager.Instance.OnMsgGoodsList(refMsg);
	}
	private static void msg_SCID_SHOP_BUY_GOODS_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SHOP_BUY_GOODS_ACK refMsg = (CliProto.SC_SHOP_BUY_GOODS_ACK)msg;
		//todo 商城购买道具反馈

        if (refMsg.m_Result == (byte)HeaderProto.EShopBuyResult.EShopBuyResult_Success)
            sdMallManager.Instance.OnMsgGoods(refMsg);
	}

	private static void msg_SCID_VIP_RMBINFO_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_VIP_RMBINFO_NTF netMsg = (CliProto.SC_VIP_RMBINFO_NTF)msg;

		sdMallManager.Instance.UpdateRMBInfo(netMsg.m_RMBCount, netMsg.m_RMBSum);
 
	}

	private static void msg_SCID_VIP_INFO_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_VIP_INFO_NTF refMsg = (CliProto.SC_VIP_INFO_NTF)msg;
		uint currentVIPLevel = refMsg.m_VIPLevel;
		
		sdMallManager.Instance.CurrentVIPLevel (currentVIPLevel);
		EverydayAwardWnd.Instance.UpdateVipSignList(currentVIPLevel);

        sdUICharacter.Instance.gmLevel = (int)refMsg.m_GMLevel;
	}

	public static void Send_CS_SHOP_BUY_ACTION_POINT_REQ()
	{
		CliProto.CS_SHOP_BUY_ACTION_POINT_REQ refMSG = new CliProto.CS_SHOP_BUY_ACTION_POINT_REQ();
		SDNetGlobal.SendMessage(refMSG);
	}

	public static void Send_CS_SHOP_BUY_PET_REQ(byte btBuyType)
	{
		CliProto.CS_SHOP_BUY_PET_REQ refMSG = new CliProto.CS_SHOP_BUY_PET_REQ();
		refMSG.m_BuyType = btBuyType;
		SDNetGlobal.SendMessage(refMSG);
	}


	public static void Send_CS_SHOP_GET_GOODSLIST_REQ()
	{
		CliProto.CS_SHOP_GET_GOODSLIST_REQ refMSG = new CliProto.CS_SHOP_GET_GOODSLIST_REQ();
		SDNetGlobal.SendMessage(refMSG);
	}

	public static void Send_CS_SHOP_BUY_GOODS_REQ(uint nGoodsId, int nGoodsCount)
	{
		CliProto.CS_SHOP_BUY_GOODS_REQ refMSG = new CliProto.CS_SHOP_BUY_GOODS_REQ();
		refMSG.m_GoodsId = nGoodsId;
		refMSG.m_GoodsCount = nGoodsCount;
		SDNetGlobal.SendMessage(refMSG);
	}
}
