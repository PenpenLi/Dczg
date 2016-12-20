using UnityEngine;
using System.Collections;

[AddComponentMenu("SNDA/GameLogic/TriggerReceiver/LevelAnimationDoor")]
public class sdLevelAnimationDoor : sdTriggerReceiver {
	
	public	string	Open;
	public	string	Close;
	public	string	OpenSound;
	public	string	CloseSound;
	GameObject		door	=	null;
	public	GameObject collision	=	null;
	public bool bornClosed = false;
	
	// Use this for initialization
	void Start () {
		if(bornClosed)
		{
			if(collision!=null)
				collision.SetActive(true);
			PlayEffect(Close.Replace("\r\n",""),false);
			PlayAudio(CloseSound.Replace("\r\n","").Replace("\\","/"));
		}
		else
		{
			if(collision!=null)
			{
				collision.SetActive(false);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		//现在没资源，不确定出现消失是怎样的表现，先用马上出现，马上消失...
		
		if(param[3] == 1)
		{
			if(collision!=null)
				collision.SetActive(true);
			PlayEffect(Close.Replace("\r\n",""),false);
            PlayAudio(CloseSound.Replace("\r\n", "").Replace("\\", "/"));
		}
		else if(param[3] == 0)
		{
			if(collision!=null)
				collision.SetActive(false);
			PlayEffect(Open.Replace("\r\n",""),true);
            PlayAudio(OpenSound.Replace("\r\n", "").Replace("\\", "/"));
		}
	}
	
	void	PlayEffect(string str,bool open)
	{
		ResLoadParams param = new ResLoadParams();
		param.userdata0	=	transform;	
		param.userdata1	=	open;
		sdResourceMgr.Instance.LoadResource(str,OnLoadEffect,param);
	}
	void	OnLoadEffect(ResLoadParams param,Object obj)
	{
		bool open	=	(bool)param.userdata1;
		Transform parent	=	param.userdata0 as Transform;
		GameObject gObj	=	GameObject.Instantiate(obj,parent.position,parent.rotation) as GameObject;
		if(gObj!=null)
		{
			gObj.transform.parent	=	parent;
			if(open)
			{
				sdAutoDestory auto	=	gObj.AddComponent<sdAutoDestory>();
			}
		}
		
		
		if(door!=null)
		{
			GameObject.Destroy(door);
			door=null;
		}
		
		if(!open)
		{
			door	=	gObj;
		}
	}
	void PlayAudio(string path)
	{
		if(GetComponent<AudioSource>() == null)
		{
			return;
		}
		if(path.Length==0)
		{
			return;
		}
		string audioFilePath = "Music/" + path;	
		ResLoadParams param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResource(audioFilePath, OnLoadAudio, param);
	}
	
	void OnLoadAudio(ResLoadParams param, Object obj)
	{
		AudioClip clip = obj as AudioClip;
		if(clip != null)
		{
			GetComponent<AudioSource>().PlayOneShot(clip);
		}
	}
}
