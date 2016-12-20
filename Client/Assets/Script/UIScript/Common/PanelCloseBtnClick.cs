using UnityEngine;
using System.Collections;

public class PanelCloseBtnClick : MonoBehaviour 
{
	public GameObject m_panelRoot;

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
			WndAni.HideWndAni(m_panelRoot,true,"w_black");
			//m_panelRoot.SetActive(false);
			//sdUICharacter.Instance.ShowFullScreenUI(false);
		}
	}
}
