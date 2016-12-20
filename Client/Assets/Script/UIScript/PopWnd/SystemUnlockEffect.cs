using UnityEngine;
using System.Collections;

public class SystemUnlockEffect : MonoBehaviour 
{
	bool 		bHaveShown = false;
	float		iStartPos;
	string		mSys;
	GameObject	mEffect;


	// Use this for initialization
	void Start () 
	{
		iStartPos = gameObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( bHaveShown == false )
		{
			if( gameObject.transform.position.x != iStartPos )
			{
				if( mEffect != null )
				{
					Transform sys = GameObject.Find("Sys1").transform.FindChild(mSys);
					mEffect.transform.parent = sys;
					mEffect.transform.localPosition = Vector3.zero;
					mEffect.SetActive(true);
				}
				bHaveShown = true;
			}
		}
	}

	public void InitEffect(string strSys,GameObject effect)
	{
		mSys = strSys;
		mEffect = effect;
	}
}
