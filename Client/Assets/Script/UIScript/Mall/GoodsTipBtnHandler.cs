using UnityEngine;
using System.Collections;

public class GoodsTipBtnHandler : MonoBehaviour 
{
    public int m_iProductId;

    void OnClick()
    {
        UIButton btn = GetComponent<UIButton>();
        if (btn != null && !btn.enabled) return;

        if (btn.name == "Close" || btn.name == "CancelBtn")
        {
            if (sdMallManager.Instance.GoodsTipPanelOb != null)
                sdMallManager.Instance.GoodsTipPanelOb.SetActive(false);
        }
        else if (btn.name == "ConfirmBtn")
        {
            if (sdMallManager.Instance.GoodsTipPanelOb != null)
            {
                sdMallManager.Instance.BuyProduct(m_iProductId, 1);
                sdMallManager.Instance.GoodsTipPanelOb.SetActive(false);
            }
        }
    }
}
