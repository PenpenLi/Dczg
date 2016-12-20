using UnityEngine;
using System.Collections;

public class sdLevelMutilCondition : sdBaseTrigger {
	public	int conditionCount	=	1;


	public override void OnTriggerHitted(GameObject obj,int[] param)
	{
		conditionCount--;
		if(conditionCount==0)
		{
			WhenLeaveTrigger(obj,param);
			if(OnceLife)
			{
				GameObject.Destroy(gameObject);
			}
		}
	}
	
	protected override void WhenLeaveTrigger (GameObject obj,int[] param)
	{
		base.WhenLeaveTrigger (obj,param);
		
		for(int i = 0; i < leaveReceivers.Length; i++)
		{
			if(leaveReceivers[i] != null)
			{
				leaveReceivers[i].OnTriggerHitted(gameObject,iLeaveParams[i].v);
			}
		}
	}
}
