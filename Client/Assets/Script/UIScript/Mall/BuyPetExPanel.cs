using UnityEngine;
using System.Collections;

public class BuyPetExPanel : MonoBehaviour {

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

		if (btn.name == "Close")
		{
			if(sdMallManager.Instance.BuyPetExPanelOb != null)
				sdMallManager.Instance.BuyPetExPanelOb.SetActive(false);
		}
	}
}
