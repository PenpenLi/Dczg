using UnityEngine;
using System.Collections;

public class OnePetObtainBtnHander : MonoBehaviour
{
	public uint m_iPetId;
	
    void OnClick()
    {
        UIButton btn = GetComponent<UIButton>();
        if (btn != null && !btn.enabled) return;

        if (btn.name == "pray_close")
        {
            if (sdMallManager.Instance.OnePetObtainPanelOb != null)
			{
				WndAni.HideWndAni(sdMallManager.Instance.OnePetObtainPanelOb,false,"bg_grey");
			}
               
        }
        else if (btn.name == "ShowAttribute")
        {
			sdUIPetControl.Instance.ActivePetSmallTip(null, (int)m_iPetId, 0 , 1, new Vector3(0f, 0f, 0f));
        }
		else if (btn.name == "BtnBuyAgain3")
		{
			if (sdMallManager.Instance.OnePetObtainPanelOb != null)
				sdMallManager.Instance.OnePetObtainPanelOb.SetActive(false);
		}
    }
}
