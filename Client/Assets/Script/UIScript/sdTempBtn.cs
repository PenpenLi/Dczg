using UnityEngine;
using System.Collections;

public class sdTempBtn : MonoBehaviour 
{

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
		if( gameObject.name == "job2" )
		{
		}
		else if( gameObject.name == "job1" || gameObject.name == "job3" )
		{
			sdUICharacter.Instance.ShowMsgLine("转职系统暂未开放，敬请期待~",MSGCOLOR.Yellow);
		}
	}
}
