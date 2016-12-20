using UnityEngine;
using System.Collections;

public class sdLevelCameraControl : sdBaseTrigger
{
    float       fTarget = 0;
    float       fTime   = -1.0f;
    public  float       TotalTime = 1.0f;
	float 				FadeTime	=	1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fTime > 0.0f)
        {
            sdGameCamera cam    =   sdGameLevel.instance.mainCamera;
            if (cam != null)
            {
                float old   =   cam.OrginZommIn;

				cam.OrginZommIn = Mathf.Lerp(old, fTarget, Mathf.Pow(1.0f - fTime / FadeTime,0.5f));
            }
            float t =   Time.unscaledDeltaTime;
            if(t>0.02)
            {
                t=0.02f;    
            }
            fTime -= t;
            Debug.Log(cam.OrginZommIn);
        }
        else
        {
            fTime = -1.0f;
        }
	}
    public override void OnTriggerHitted(GameObject obj, int[] param)
    {
        if (param[2] > 0)
        {
			FadeTime = param[2] * 0.001f;
        }
		else
		{
			FadeTime = TotalTime;
		}
        fTarget = (param[3]) * 0.0001f;

    }
}
