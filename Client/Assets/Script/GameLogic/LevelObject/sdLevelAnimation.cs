using UnityEngine;
using System.Collections;

public class sdLevelAnimation : sdTriggerReceiver {
	
	public bool bornActive = false;
	public	string	OpenAnimation;
	public	string	CloseAnimation;
	// Use this for initialization
	void Start () {
		CloseAnimation	=	CloseAnimation.Replace("\r\n","");
		OpenAnimation	=	OpenAnimation.Replace("\r\n","");
		if(!bornActive)
		{
			if(GetComponent<Collider>()!=null)
			{
				GetComponent<Collider>().enabled	=	true;
			}
			if(CloseAnimation.Length>0 && GetComponent<Animation>()!=null)
			{
				GetComponent<Animation>().Play(CloseAnimation);
			}
		}
		else
		{
			if(OpenAnimation.Length>0 && GetComponent<Animation>()!=null)
			{
				GetComponent<Animation>().Play(OpenAnimation);
			}
		}
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		//现在没资源，不确定出现消失是怎样的表现，先用马上出现，马上消失...
		
		if(param[3] == 0)
		{
			if(GetComponent<Collider>()!=null)
			{
				GetComponent<Collider>().enabled	=	false;
			}
			if(OpenAnimation.Length>0 && GetComponent<Animation>()!=null)
			{
				GetComponent<Animation>().Play(OpenAnimation);
			}
		}
		else
		{
			if(GetComponent<Collider>()!=null)
			{
				GetComponent<Collider>().enabled	=	true;
			}
			if(CloseAnimation.Length>0 && GetComponent<Animation>()!=null)
			{
				GetComponent<Animation>().Play(CloseAnimation);
			}
		}
		if(GetComponent<AudioSource>()!=null)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
