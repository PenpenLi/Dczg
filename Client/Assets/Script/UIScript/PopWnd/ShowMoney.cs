using UnityEngine;
using System.Collections;

public class ShowMoney : MonoBehaviour 
{
	UILabel		mCash1;
	UILabel		mCash2;
	UILabel		mMoney;

	// Use this for initialization
	void Start () 
	{
		mCash1 = transform.FindChild("bg_cash1").GetComponentInChildren<UILabel>();
		mCash2 = transform.FindChild("bg_cash2").GetComponentInChildren<UILabel>();
		mMoney = transform.FindChild("bg_money").GetComponentInChildren<UILabel>();

		Hashtable mainProp = (Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"];
		if( mainProp == null ) return;
        if (mCash1 != null)
		    mCash1.text = mainProp["Cash"].ToString();
       	if(mCash2 != null)
	    	mCash2.text = mainProp["NonCash"].ToString();
       	if(mMoney != null)
	    	mMoney.text = mainProp["NonMoney"].ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
