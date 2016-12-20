using UnityEngine;
using System.Collections;

public class sdLevelGuide : sdTriggerReceiver {
	
	public	GameObject[] 	guidePoint	=	null;
	public	int         	activeIndex	=	0;
	GameObject		effect		=	null;
	// Use this for initialization
	void Start () {
		
	}
	

	void OnLoadEffect(ResLoadParams param,Object obj)
	{
        bool bMovieDialogue = false;
        if (sdGlobalDatabase.Instance.globalData.ContainsKey("moviedialogue"))
            bMovieDialogue = (bool)sdGlobalDatabase.Instance.globalData["moviedialogue"];
		effect	=	GameObject.Instantiate(obj) as GameObject;
        if (activeIndex == 0 || bMovieDialogue)
		{
			effect.SetActive(false);
		}
	}
	void Update () {
		sdMainChar mc	=	sdGameLevel.instance.mainChar;
		if(effect!=null && activeIndex!=0)
		{
            bool bMovieDialogue = false;
            if (sdGlobalDatabase.Instance.globalData.ContainsKey("moviedialogue"))
                bMovieDialogue = (bool)sdGlobalDatabase.Instance.globalData["moviedialogue"];
            effect.SetActive(!bMovieDialogue);
			effect.transform.position	=	mc.transform.position;
			Vector3 dst	=	guidePoint[activeIndex-1].transform.position;
			Vector3 dir	=	dst	-	mc.transform.position;
			dir.y=0.0f;
			dir.Normalize();
			
			if(dir.z < -0.999f)
			{
				effect.transform.rotation	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));
			}
			else
			{
				effect.transform.rotation	=	Quaternion.FromToRotation(new Vector3(0,0,1),dir);
			}
		}
		
		
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		
		activeIndex	=	param[3];
		if(activeIndex!=0 && effect==null)
		{
			ResLoadParams p = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/FX_UI/$Fx_Zhixiang/Fx_Zhixiang_002_prefab.prefab",OnLoadEffect,p);
		}
		
		if(effect!=null)
		{
			effect.SetActive(activeIndex!=0);
		}
		
	}
}
