using UnityEngine;
using System.Collections;

public class BackToSelServer : MonoBehaviour 
{
	void OnClick()
	{
		SDNetGlobal.disConnect();
		Application.LoadLevel("login");
	}
}
