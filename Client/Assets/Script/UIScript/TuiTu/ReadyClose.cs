using UnityEngine;
using System.Collections;

public class ReadyClose : MonoBehaviour 
{	
	public GameObject ReadyUI;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnClick()
	{
		if( gameObject.name == "bt_close" )
		{
			if( Application.loadedLevelName=="$worldmap_0" || Application.loadedLevelName=="$mainCity_1" )
			{	// 在城镇中...
				//WndAni.HideWndAni(ReadyUI,true,"w_black");
				ReadyUI.SetActive(false);
				sdUICharacter.Instance.ShowFullScreenUI(false);
			}
			else
			{	// 在关卡中...
				sdUICharacter.Instance.TuiTu_To_WorldMap();
			}
		}
		else if( gameObject.name == "bt_back0" )
		{
			sdUICharacter.Instance.HideLevelPrepare();
		}
	}
}
