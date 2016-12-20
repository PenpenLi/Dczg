using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdAutoDestory : MonoBehaviour {
	public	float	Life	=	1.0f;
	public	float	current	=	0.0f;
    public sdGameActor actor = null;
    public List<EventDelegate> onFinish = new List<EventDelegate>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		current+=Time.deltaTime;
		if(current>Life)
		{
            Callback();
			GameObject.Destroy(gameObject);
		}
        else if (actor != null)
        {
            if (actor.GetCurrentHP() <= 0)
            {
                Callback();
                GameObject.Destroy(gameObject);
            }
            
        }
	}
    void Callback()
    {
        EventDelegate.Execute(onFinish);
    }
}
