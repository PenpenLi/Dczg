using UnityEngine;
using System.Collections;

public class TenPetObtainBtnHander : MonoBehaviour 
{
	void OnClick()
	{
		UIButton btn = GetComponent<UIButton>();
		if (btn != null && !btn.enabled) return;
		
		if (btn.name == "Close")
		{
			if (sdMallManager.Instance.TenPetObtainPanelOb != null)
			{
			//	sdMallManager.Instance.TenPetObtainPanelOb.SetActive(false);
				WndAni.HideWndAni(sdMallManager.Instance.TenPetObtainPanelOb,false,"bg_grey");
				sdMallManager.Instance.m_tenPetObtainPanelOpen = false;
			}
		}
		else if (btn.name == "ExchangeAgain")
		{
			if (sdMallManager.Instance.TenPetObtainPanelOb != null)
				sdMallManager.Instance.TenPetObtainPanelOb.SetActive(false);
		}
	}
}
