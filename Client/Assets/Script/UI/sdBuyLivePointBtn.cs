using UnityEngine;
using System.Collections;

public class sdBuyLivePointBtn : MonoBehaviour
{
	void OnClick()
	{
		UIButton btn = GetComponent<UIButton> ();
		if (btn != null && !btn.enabled) return;

		if (btn.name == "buyEnergy" || btn.name == "bt_addsprite")
		{

			sdMainChar mc = sdGameLevel.instance.mainChar;
			if (mc != null)
			{
				int myCurrentEP = int.Parse(mc.GetBaseProperty()["EP"].ToString ());
				int myMaxEP = int.Parse(mc.Property["MaxEP"].ToString());

				if (myCurrentEP >= myMaxEP)
				{
					sdUICharacter.Instance.ShowOkMsg("您的体力已满,无需购买", null);
					return;
				}
			}


			//sdMallManager.Instance.OpenBuyLiveWnd();
		
			BuyActionPanel.Instance.OpenPanel();

		}
		else if (btn.name == "btn_shop")
		{
			//sdUICharacter.Instance.ShowMsgLine("抱歉，商城系统暂未开放。",MSGCOLOR.Yellow); return;

			sdMallManager.Instance.ActiveMainPanel(null, false);
			//sdMallManager.Instance.m_CurrentMallPageType = sdMallManager.MallPageType.mall;

			//sdUICharacter.Instance.ShowFullScreenUI(true);
		}
		else if (btn.name == "petshop")
		{
            if (!sdLevelInfo.GetLevelValid(12011))
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("PetShopClose"), MSGCOLOR.Yellow);
                return;
            }

			//sdUICharacter.Instance.ShowMsgLine("抱歉，灵魂殿堂系统暂未开放。",MSGCOLOR.Yellow); return;

			//sdMallManager.Instance.ActiveMainPanel(null);
			sdMallManager.Instance.ActiveMagicTowerPanel(null);
			//sdMallManager.Instance.m_CurrentMallPageType = sdMallManager.MallPageType.magic;

			//sdUICharacter.Instance.ShowFullScreenUI(true);
		}
		else if (btn.name == "btn_recharge")
		{
			sdMallManager.Instance.ActiveMainPanel(null, true);
			//sdUICharacter.Instance.ShowMsgLine("充值系统暂未开放，敬请期待~",MSGCOLOR.Yellow); return;
		}
	}
}
