using UnityEngine;
using System.Collections;

public class sdBuyPetButton : MonoBehaviour
{
    public static int BUY_PET_EXPENSIVE_CHOISE = -1;
    public int m_BuyPetType = -1;

    void OnClick()
    {
        if (sdNewPetMgr.Instance.GetMyPetCount() >= sdNewPetMgr.Instance.GetMyPetMaxCount())
        {
            sdUICharacter.Instance.ShowMsgLine("战魂背包已满，无法祈祷！", MSGCOLOR.Red);
            return;
        }

		/*
        if (m_BuyPetType == BUY_PET_EXPENSIVE_CHOISE)
        {
            //弹出购买高级选择战魂界面.
            sdMallManager.Instance.ActiveBuyPetExPanel();
            return;
        }
        */

        if (m_BuyPetType == (int)HeaderProto.EBuyPetType.EBuyPetType_Exchange)
        {
            int nItemID = 104;
            int nNeedCount = 1;
            if (!sdGameItemMgr.Instance.HasItemCount(nItemID, nNeedCount))
            {
				sdUICharacter.Instance.ShowMsgLine("灵魂石币不足，无法祈祷！", MSGCOLOR.Red);
                return;
            }
        }
        else
        {
			uint nCash = (uint)sdGameLevel.instance.mainChar.Property["Cash"] + (uint)sdGameLevel.instance.mainChar.Property["NonCash"];
            bool bCashNotEnough = false;
            bool bCloseBuyPetExWnd = false;
            if (m_BuyPetType == (int)HeaderProto.EBuyPetType.EBuyPetType_Cheap)
            {
                if (nCash < 100)
                {
                    bCashNotEnough = true;
                }
            }
            else if (m_BuyPetType == (int)HeaderProto.EBuyPetType.EBuyPetType_Expensive)
            {
                if (nCash < 300)
                {
                    bCashNotEnough = true;
                }
                //bCloseBuyPetExWnd = true;
            }
            else if (m_BuyPetType == (int)HeaderProto.EBuyPetType.EBuyPetType_Expensive10)
            {
                if (nCash < 3000)
                {
                    bCashNotEnough = true;
                }
                bCloseBuyPetExWnd = true;
            }
            else
            {
                //参数错误.
                return;
            }

            if (bCloseBuyPetExWnd)
            {
                if (sdMallManager.Instance.BuyPetExPanelOb != null)
				{
					sdMallManager.Instance.BuyPetExPanelOb.SetActive(false);
				}
            }

            //提示钱不够，无法购买.
            if (bCashNotEnough)
            {
                //sdMallManager.Instance.ActiveVIPPanel(null);
				sdUICharacter.Instance.ShowMsgLine("勋章不足，无法购买用于祈祷的灵魂玉币！", MSGCOLOR.Red);
				return;
            }
        }

        sdMallMsg.Send_CS_SHOP_BUY_PET_REQ((byte)m_BuyPetType);
    }
}

