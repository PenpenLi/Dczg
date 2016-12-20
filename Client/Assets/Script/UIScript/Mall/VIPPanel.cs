using UnityEngine;
using System.Collections;

public class VIPPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		UIButton btn = GetComponent<UIButton> ();
		if (btn != null && !btn.enabled) return;

		if (btn.name == "CancelBtn")
		{
			if( sdMallManager.Instance.VIPPanelOb != null )
				sdMallManager.Instance.VIPPanelOb.SetActive(false);
		}
		
		else if (btn.name == "ChargeBtn")
		{
			//sdMallManager.Instance.ActiveMainPanel(sdMallManager.Instance.VIPPanelOb);
			//sdMallManager.Instance.m_CurrentMallPageType = sdMallManager.MallPageType.charge;
		}
	}
}
