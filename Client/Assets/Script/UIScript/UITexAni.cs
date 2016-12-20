using UnityEngine;
using System.Collections;

public class UITexAni : MonoBehaviour 
{
	public float aniSpeed	= 0.05f;
	public float startX		= 0;
	public float startY		= 0;
	public float stepX		= 0;
	public float stepY		= 0;
	public int framesX		= 0;
	public int framesY		= 0;

	private float lastCheckTime		= 0;
	private float startCheckTime	= 0;


	// Use this for initialization
	void Start () 
	{
		startCheckTime = Time.unscaledTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( aniSpeed==0 || framesX==0 || framesY==0 ) return;

		if( (Time.unscaledTime-lastCheckTime) > aniSpeed )
		{
			lastCheckTime = Time.unscaledTime;

			int fn = (int)((lastCheckTime-startCheckTime)/aniSpeed) % (framesX*framesY);
			int y = (framesX-1) - (fn / framesX);
			int x = fn % framesX;

			UITexture tex = gameObject.GetComponent<UITexture>();
			tex.uvRect = new Rect(startX+stepX*x, startY+stepY*y, tex.uvRect.width,tex.uvRect.height);
		}
	}
}
