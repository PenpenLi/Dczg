using UnityEngine;
using System.Collections;

[AddComponentMenu("SNDA/GameLogic/Trigger/TriggerAreaTimer")]
public class sdTriggerAreaTimer : sdBaseTrigger {
	
	float areaTime = 0.0f;
	float prevTime = 0.0f;
	int areaId;
	
	bool started = false;
	
	protected override void Awake ()
	{
		base.Awake ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	protected override void Update ()
	{
		base.Update ();
		
		if(started)
		{
			prevTime = Time.deltaTime;
			areaTime += Time.deltaTime;	
			
			WhenEnterTrigger(gameObject,new int[4]{0,0,0,0});
		}
	}
	
	protected override void WhenEnterTrigger (GameObject obj,int[] param)
	{
		base.WhenEnterTrigger (obj,param);
		
		for(int i = 0; i < enterReceivers.Length; i++)
		{
			if(enterReceivers[i] != null)
			{
				
					enterReceivers[i].OnTriggerHitted(gameObject,iEnterParams[i].v);
				
			}
		}
	}
	
	protected override void WhenLeaveTrigger (GameObject obj,int[] param)
	{
		base.WhenLeaveTrigger (obj,param);
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		started = true;
		areaId = param[3];
		areaTime = 0.0f;
		prevTime = 0.0f;
	}
}
