using UnityEngine;
using System.Collections;

public class sdUIReconnect : MonoBehaviour 
{
	public UILabel		lbText;
	public GameObject	spLoading;
	public GameObject	btReconnect;
	public GameObject	btQuit;

	public string		strConnectText = "努力连接中...";


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( gameObject.name == "sp_loading" )
		{
			gameObject.transform.Rotate(0,0,-360.0f*Time.deltaTime);
		}
	}

	void OnClick()
	{
		if( gameObject.name == "bt_reconnet" )
		{
			SDNetGlobal.doGateLogin(true);
		}
		else if( gameObject.name == "bt_quit" )
		{
			Application.Quit();
		}
	}

	public void ShowBtn(bool bShow)
	{
		if( spLoading == null ) return;
		if( bShow )
		{
			lbText.text = "连接服务器失败，请检查您的网络设置。";
			spLoading.SetActive(false);
			btReconnect.SetActive(true);
			btQuit.SetActive(true);
		}
		else
		{
			lbText.text = strConnectText;
			spLoading.SetActive(true);
			btReconnect.SetActive(false);
			btQuit.SetActive(false);
		}
	}
}
