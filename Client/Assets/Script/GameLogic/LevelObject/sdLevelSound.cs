using UnityEngine;
using System.Collections;

public class sdLevelSound : sdTriggerReceiver {
	
	private AudioSource aSource;
	
	void Awake()
	{
		aSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		if(param[3] == 0)
		{
			aSource.Stop();
		}
		else if(param[3] == 1)
		{
			aSource.Play();
		}
	}
}
