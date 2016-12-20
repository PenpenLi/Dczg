using UnityEngine;
using System.Collections;

public class sdTabControl : MonoBehaviour 
{
	public bool			mDefaultTab			= false;
	public GameObject	mActiveBG			= null;
	public GameObject	mUnactiveBG			= null;
	public GameObject	mMyTab				= null;
	public sdTabControl	mOtherTabBT1		= null;
	public sdTabControl	mOtherTabBT2		= null;
	public sdTabControl	mOtherTabBT3		= null;
	public sdTabControl	mOtherTabBT4		= null;
	public sdTabControl	mOtherTabBT5		= null;


	// Use this for initialization
	void Start () 
	{
		if( mDefaultTab )
			ActiveTab();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	// 点击TAB按钮
	void OnClick()
	{
		if( mMyTab == null )
		{
			sdUICharacter.Instance.ShowMsgLine("此功能暂未开放");
			return;
		}

		if( mMyTab.activeSelf ) return;
		ActiveTab();
	}

	// 激活当前TAB页.
	public void ActiveTab()
	{
		if( mMyTab != null )		mMyTab.SetActive(true);
		if( mOtherTabBT1 != null )	mOtherTabBT1.UnactiveTab();
		if( mOtherTabBT2 != null )	mOtherTabBT2.UnactiveTab();
		if( mOtherTabBT3 != null )	mOtherTabBT3.UnactiveTab();
		if( mOtherTabBT4 != null )	mOtherTabBT4.UnactiveTab();
		if( mOtherTabBT5 != null )	mOtherTabBT5.UnactiveTab();

		if( mActiveBG!=null && mUnactiveBG!=null )
		{
			mActiveBG.SetActive(true);
			mUnactiveBG.SetActive(false);
			gameObject.GetComponent<UIButton>().tweenTarget = mActiveBG;
		}
	}

	// 取消激活当前TAB页.
	public void UnactiveTab()
	{
		if( mMyTab == null ) return;

		if( mMyTab.activeSelf )
			mMyTab.SetActive(false);

		if( mActiveBG!=null && mUnactiveBG!=null )
		{
			mActiveBG.SetActive(false);
			mUnactiveBG.SetActive(true);
			gameObject.GetComponent<UIButton>().tweenTarget = mUnactiveBG;
		}
	}
}


