using UnityEngine;
using System.Collections;

public class WndAni : MonoBehaviour 
{
	static AnimationCurve	mWndAni1	= new AnimationCurve(new Keyframe(0f, 0f, 2f, 2f), new Keyframe(0.5f, 1.2f, 0.6f, 0.6f), new Keyframe(1f, 1f, 0f, 0f));
	static AnimationCurve	mWndAni2	= new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
	static float			mShowDur	= 0.4f;	// 这4个值必须不同..
	static float			mHideDur	= 0.3f;
	//static float			mShowDurF	= 0.32f;
	//static float			mHideDurF	= 0.31f;
	static Vector3			mStart		= new Vector3(0.5f,0.5f,0.5f);
	//static float			mStartF		= 0.5f;
	static GameObject		mWndNow		= null;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public static void ShowWndAni(GameObject objWnd, bool bFullscreen, string strBG)	{ ShowWndAni(objWnd,bFullscreen,strBG,0,0); }
	public static void ShowWndAni(GameObject objWnd, bool bFullscreen, string strBG, float startX, float startY)
	{
		if( objWnd == null ) return;
		objWnd.SetActive(true);

		if( bFullscreen )
		{
			sdUICharacter.Instance.ShowFullScreenUI(true);
			WndAniCB cb = objWnd.GetComponent<WndAniCB>();
			if( cb != null ) cb.OnFinished();
		}
		else
		{
			TweenScale ts = objWnd.GetComponent<TweenScale>();
			if( ts == null )
			{
				ts = objWnd.AddComponent<TweenScale>();
				EventDelegate.Add(ts.onFinished, onFinished);
			}

			// 出现位置..
			TweenPosition tp = null;
			if( startX!=0 || startY!=0 )
			{
				tp = objWnd.GetComponent<TweenPosition>();
				if( tp == null ) tp = objWnd.AddComponent<TweenPosition>();
			}

			if( strBG!=null && strBG!="" )
			{
				Transform t = objWnd.transform.FindChild(strBG);
				if( t != null )
				{
					// 蒙版将以逐变效果出现..
					t.localScale = new Vector3(1f/mStart.x,1f/mStart.y,1f);
					TweenAlpha ta = t.gameObject.GetComponent<TweenAlpha>();
					if( ta == null )
					{
						ta = t.gameObject.AddComponent<TweenAlpha>();
						ta.from = 0f;
						EventDelegate.Add(ta.onFinished, onAlphaFinished);
					}
					ta.duration = mShowDur;
					ta.PlayForward();
				}
			}

			ts.animationCurve = mWndAni1;
			ts.duration	= mShowDur;
			ts.from = mStart;
			ts.tweenFactor = 0;
			ts.PlayForward();
			
			if( tp != null )
			{
				tp.from = new Vector3(startX,startY,0);
				tp.duration = mShowDur;
				tp.tweenFactor = 0;
				tp.PlayForward();
			}
		}
	}

	public static void HideWndAni(GameObject objWnd, bool bFullscreen, string strBG)	{ HideWndAni(objWnd,bFullscreen,strBG,0,0); }
	public static void HideWndAni(GameObject objWnd, bool bFullscreen, string strBG, float startX, float startY)
	{
		if( objWnd == null ) return;

		if( bFullscreen )
		{
			WndAniCB cb = objWnd.GetComponent<WndAniCB>();
			if( cb != null ) cb.OnFinishedHide();
			sdUICharacter.Instance.ShowFullScreenUI(false,objWnd);
			objWnd.SetActive(false);
			//objWnd.transform.localScale = Vector3.one;
			//ta.gameObject.transform.localPosition = Vector3.zero;
		}
		else
		{
			TweenScale ts = objWnd.GetComponent<TweenScale>();
			if( ts == null )
			{
				ts = objWnd.AddComponent<TweenScale>();
				EventDelegate.Add(ts.onFinished, onFinished);
			}

			TweenPosition tp = null;
			if( startX!=0 || startY!=0 )
			{
				tp = objWnd.GetComponent<TweenPosition>();
				if( tp == null ) tp = objWnd.AddComponent<TweenPosition>();
			}

			// 处理蒙版效果，对非全屏窗口，将以逐变效果出现..
			if( strBG!=null && strBG!="" )
			{
				Transform t = objWnd.transform.FindChild(strBG);
				if( t != null )
				{
					// 蒙版将以逐变效果出现..
					t.localScale = new Vector3(1f/mStart.x,1f/mStart.y,1f);

					TweenAlpha ta = t.gameObject.GetComponent<TweenAlpha>();
					if( ta == null )
					{
						ta = t.gameObject.AddComponent<TweenAlpha>();
						ta.from = 0f;
						EventDelegate.Add(ta.onFinished, onAlphaFinished);
					}
					ta.duration = mHideDur;
					ta.tweenFactor = 1f;
					ta.PlayReverse();
				}
			}

			ts.duration	= mHideDur;
			ts.from = mStart;
			ts.animationCurve = mWndAni2;
			ts.tweenFactor = 1f;
			ts.PlayReverse();

			if( tp != null )
			{
				tp.from = new Vector3(startX,startY,0);
				tp.duration = mHideDur;
				tp.tweenFactor = 1f;
				tp.PlayReverse();
			}
		}
	}

	public static void ShowWndAni2(GameObject objWnd, string[] strBtn)
	{
		if( objWnd == null ) return;
		mWndNow = objWnd;
		objWnd.SetActive(true);

		TweenAlpha ta = objWnd.GetComponent<TweenAlpha>();
		if( ta == null )
		{
			ta = objWnd.AddComponent<TweenAlpha>();
			EventDelegate.Add(ta.onFinished, onHideFinished2);
		}
		ta.animationCurve = mWndAni2;
		ta.duration	= 0.2f;
		ta.from = 0;
		ta.to = 1f;
		ta.tweenFactor = 0;
		ta.PlayForward();

		for(int i=0;i<strBtn.Length;i++)
		{
			if( strBtn[i] == "" ) break;
			Transform t = objWnd.transform.FindChild(strBtn[i]);
			if( t == null ) continue;

			TweenPosition tp = t.GetComponent<TweenPosition>();
			if( tp == null )
			{
				tp = t.gameObject.AddComponent<TweenPosition>();
				if( i == (strBtn.Length-1) )
					EventDelegate.Add(tp.onFinished, onFinished2);
			}
			tp.animationCurve = mWndAni2;
			tp.delay	= i*0.2f;
			tp.duration	= 0.4f - i*0.03f;
			tp.from = new Vector3(t.localPosition.x + 1280f,t.localPosition.y,t.localPosition.z);
			tp.to = t.localPosition;
			t.localPosition = tp.from;
			tp.tweenFactor = 0;
			tp.PlayForward();
		}
	}

	public static void HideWndAni2(GameObject objWnd)
	{
		if( objWnd == null ) return;
		mWndNow = objWnd;
		
		TweenAlpha ta = objWnd.GetComponent<TweenAlpha>();
		if( ta == null )
		{
			ta = objWnd.AddComponent<TweenAlpha>();
			EventDelegate.Add(ta.onFinished, onHideFinished2);
		}

		ta.animationCurve = mWndAni2;
		ta.duration	= 0.3f;
		ta.from = 0;
		ta.to = 1f;
		ta.tweenFactor = 1f;
		ta.PlayReverse();
	}
	
	static void onFinished()
	{
		UITweener ts = TweenScale.current;
		WndAniCB cb = ts.gameObject.GetComponent<WndAniCB>();

		if( ts.duration == mShowDur ) 
		{
			if( cb != null ) cb.OnFinished();
		}
		else if( ts.duration == mHideDur ) 
		{
			if( cb != null ) cb.OnFinishedHide();
			ts.gameObject.SetActive(false);
			ts.gameObject.transform.localScale = Vector3.one;
			ts.gameObject.transform.localPosition = Vector3.zero;
		}
	}

	static void onAlphaFinished()
	{
		TweenScale.current.gameObject.transform.localScale = new Vector3(1f,1f,1f);
	}

	static void onFinished2()
	{
		WndAniCB cb = mWndNow.GetComponent<WndAniCB>();
		if( cb != null ) cb.OnFinished();
	}

	static void onHideFinished2()
	{
		if( TweenAlpha.current.tweenFactor != 0 ) return;

		WndAniCB cb = mWndNow.GetComponent<WndAniCB>();
		if( cb != null ) cb.OnFinishedHide();
		mWndNow.SetActive(false);
		mWndNow.transform.localPosition = Vector3.zero;
	}
}
