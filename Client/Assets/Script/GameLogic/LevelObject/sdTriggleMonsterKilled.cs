using UnityEngine;
using System.Collections;

[AddComponentMenu("SNDA/GameLogic/Trigger/TriggerMonsterKilled")]
public class sdTriggleMonsterKilled : sdBaseTrigger {
	
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
		base.WhenLeaveTrigger (obj, param);
	}
	
	public void MonsterKilled(int areaId,int templateId,int num)
	{
		int[] param = new int[4]{areaId,templateId,num,0};
		WhenEnterTrigger(gameObject,param);
	}
}
