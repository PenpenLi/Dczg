using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemUnlockBtn : MonoBehaviour 
{
	public GameObject	mWnd	= null;
	public UISprite		mSprite	= null;
	public UILabel		mLabel	= null;
	public GameObject	mEffect	= null;
	public string mSystem = "";

    public List<EventDelegate> onFinish = new List<EventDelegate>();

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void ShowWnd(string strSystem,string strSprite,string strInfo)
	{
		mSprite.spriteName	= strSprite;
		mLabel.text			= strInfo;
		mSystem				= strSystem;
	}

	void OnClick()
	{
		GameObject btn = GameObject.Find("Btn_Push");
		if( btn != null )
		{
			mSprite.gameObject.GetComponent<TweenPosition>().to.y = btn.transform.position.y - 50;
			//btn.AddComponent<SystemUnlockEffect>();
			//btn.GetComponent<SystemUnlockEffect>().InitEffect(mSystem,mEffect);
		}

		mWnd.transform.FindChild("bg").GetComponent<TweenAlpha>().PlayForward();
		mSprite.gameObject.GetComponent<TweenPosition>().PlayForward();
		mEffect.GetComponent<TweenPosition>().PlayForward();
		mWnd.GetComponent<TweenAlpha>().PlayForward();

		EventDelegate.Add(mWnd.GetComponent<TweenAlpha>().onFinished, onFinished,true);
	}

	void onFinished()
	{
        if (onFinish.Count > 0)
        {
            EventDelegate.Execute(onFinish);
            onFinish.Clear();
        }
		mWnd.SetActive(false);
	}
}
