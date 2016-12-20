using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public	enum 	ETimingCondition
{
	Action_Enter,
	Action_Leave,
	Action_HiPoint
}
public enum EHitCondition
{
	HC_Always,
	HC_Hit,
	HC_Critical,
	HC_Miss
}
public	class sdSkillEffect
{
	/*
	 * 	
	CHECK_BUFF_NONE        = 0,
	CHECK_BUFF_CLASS,
	CHECK_BUFF_SUB_CLASS,
	CHECK_BUFF_CLASS_ID,
	CHECK_BUFF_TEMPLATE_ID,
	CHECK_BUFF_DAMAGE_PRO,
	*/
	

	
	public	static	bool	Add(
		sdActorInterface castActor,
		sdActorInterface target,
		SkillEffect effect,
		Transform	customTrans,
		int skillID)
	{
		if(!castActor.CheckBuff(effect.bySelfCheckType,effect.dwSelfBuffID))
		{	
			return false;
		}
		if(target!=null)
		{
			if(!target.CheckBuff(effect.byTgtCheckType,effect.dwTgtBuffID))
			{	
				return false;
			}
			int monsterTypeFlag = effect.byMonsterBodyTypeFlag;
			ActorType actorType = target.GetActorType();
			if(actorType == ActorType.AT_Monster || actorType == ActorType.AT_Pet)
			{
				sdGameMonster  monster = (sdGameMonster)target;
				int monsterType = (int)monster.Property["BodyType"];
				if((monsterTypeFlag&(1<<monsterType)) == 0)
					return false;
			}
		}
		
		if(Random.Range(0,10000)> effect.dwProbability)
		{
			return false;
		}
		
		sdEffectOperation.OpParameter param = new sdEffectOperation.OpParameter();
		param.id			=	effect.dwOperationID;
		param.data			=	effect.dwOperationData;
		param.data1			=	effect.dwOperationData1;
		param.data2         =   skillID;
		param.layer			=	1;
		param.doType		=	(int)HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START;
		param.attackActor	=	castActor;
		param.targetActor	=	null;
		
		if(effect.byFlag!=0)
		{
			if(effect.byFlag==1)
			{
				param.targetActor	=	castActor;
			}
			else if(effect.byFlag==2)
			{
				param.targetActor	=	target;
			}
		}
		if(castActor!=null)
		{
			param.trans	=	castActor.transform;
		}
		if(customTrans!=null)
		{
			param.trans	=	customTrans;
		}
		
		
		return sdEffectOperation.Do(param.targetActor,param);
	}
}