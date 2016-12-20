using UnityEngine;
using System.Collections;
using System;

public class sdUIPetAlphaAnm : MonoBehaviour
{
	public float fTime = 0.0f;

	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
		if (gameObject.activeSelf==false)
			return;

		fTime += Time.deltaTime;

		if (fTime>=0.0f&& fTime<=0.8f)
		{
			gameObject.GetComponent<UISprite>().alpha = 1.0f;
		}
		else if (fTime>0.8f&&fTime<=1.4f)
		{
			float fa = fTime -0.8f;
			float fAlpha = fa/0.6f;
			fAlpha = 1.0f - fAlpha;
			if (fAlpha<0.0f)
				fAlpha = 0.0f;
			gameObject.GetComponent<UISprite>().alpha = fAlpha;
		}
		else if (fTime>1.4f&&fTime<=2.0f)
		{
			float fa = fTime -1.4f;
			float fAlpha = fa/0.6f;
			if (fAlpha>1.0f)
				fAlpha = 1.0f;
			gameObject.GetComponent<UISprite>().alpha = fAlpha;
		}
		else
		{
			fTime = 0.0f;
		}
	}
	
	void OnClick()
	{

	}
}