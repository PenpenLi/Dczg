using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 技能伤害类型aa
/// </summary>
public	enum EPeriodicDamage    
{
    ///每波伤害相同...
    PERIODIC_DAMAGE_SAME              = 0,
	///固定值递增：Data + DataPeriodic * (Index)...
	PERIODIC_DAMAGE_INCREASE          = 1,
	///百分比递增：HP*(Data + DataPeriodic * (Index))...
    PERIODIC_DAMAGE_INCREASE_PERCENT  = 2,
    ///固定值递减：Data - DataPeriodic * (Index)...
	PERIODIC_DAMAGE_DECREASE          = 3,  
    ///百分比递减：HP*(Data - DataPeriodic * (Index))...
	PERIODIC_DAMAGE_DECREASE_PERCENT  = 4,  
	///第一波伤害最大,其余伤害相同,第一波伤害计算公式：...
	///固定值方式：Data - DataPeriodic...
	PERIODIC_DAMAGE_FIRST             = 5, 
	///百分比方式：HP*(Data + DataPeriodic)...
    PERIODIC_DAMAGE_FIRST_PERCENT     = 6, 
    ///最后一波伤害最大,其余伤害相同,第一波伤害计算公式：
    ///固定值方式：Data + DataPeriodic
    PERIODIC_DAMAGE_END               = 7, 
	///百分比方式：HP*(Data + DataPeriodic)...
	PERIODIC_DAMAGE_END_PERCENT       = 8,
}

/// <summary>
/// 技能类型aa
/// </summary>
public enum SkillType
{
	Skill_Physic,
	Skill_Ice,
	Skill_Fire,
	Skill_Poison,
	Skill_Thunder,
}

/// <summary>
/// 技能属性(攻击属性)aa
/// </summary>
public struct SkillProperty
{
	//每秒攻击次数..职业不同数值不同...
	public float attPerSecond;
	//命中...从人物属性...
	public float hitRate;
	//暴击...从人物属性...
	public float superRate;
	//暴击伤害...从人物属性...
	public float superAttack;
	//物攻...从人物属性...
	public int atk;
	//冰攻...从人物属性...
	public int iceAtk;
	//火攻...从人物属性...
	public int fireAtk;
	//毒攻...从人物属性...
	public int poisonAtk;
	//雷攻 从人物属性...
	public int thunderAtk;	
	//穿透...  从人物属性...
	public float passThrough;
	//总体伤害修正 ....从人物属性...
	public float hurtOtherModify; 
	//技能伤害总体修正aaa 从技能表
	public float damagePer;	
	//技能类型...
	public SkillType type;	
	//调用攻击百分比..
	public float Y;
	//直接伤害...
	public int Z;
	//百分比修正..
	public float YZFix;
	//数值修正...
	public int ZFix;
	//属性伤害修正aaa
	public float PropertyModify;
	//体型伤害修正aaa
	public float BodyTypeModify;
	//控制状态伤害修正aaaa
	public float StateModify;
}

/// <summary>
/// 技能防御属性aa
/// </summary>
public struct SkillDefProperty
{
	//闪避...
	public float dodgeRate;
	//韧性...
	public float toughness;
	//暴击防御...
	public float superDef;
	//物防...
	public int def;
	//冰防...
	public int iceDef;
	//火防...
	public int fireDef;
	//毒防...
	public int poisonDef;
	//雷防...
	public int thunderDef;
	//等级...
	public int level;
	//受到的伤害修正
	public float beHurtModify;
}

/// <summary>
/// 伤害计算结果aa
/// </summary>
public	struct DamageResult
{
	public	int 				damage;			//< 伤害值aa
	public	Bubble.BubbleType	bubbleType;		//< 伤害冒泡类型aa
	public 	sdActorInterface	attracker;		//< 攻击者aa
}

/// <summary>
/// 角色阵营aa
/// </summary>
public	enum GroupIDType
{
	GIT_PlayerA = 1,
	GIT_PlayerB,
	GIT_PlayerC,
	GIT_NPCA,
	GIT_NPCB,
	GIT_MonsterA,
	GIT_MonsterB,
	GIT_MAX,

}

/// <summary>
/// 角色阵营之间关系类型(由外部表格配置)aa
/// </summary>
public	enum GroupType
{
	GT_Friend,
	GT_Enemy,
}

/// <summary>
/// 战斗伤害计算回调aa
/// </summary>
public	class sdAttackCB
{
	public virtual void OnAttack(sdActorInterface _gameActor,int index)
	{
	}
	public virtual int OnHit(sdActorInterface _gameActor,List<sdActorInterface>	lstMonster,int iHitPointIndex,object userdata, HeaderProto.ESkillEffect skilleffect)
	{
        return 0;
	}
}

/// <summary>
/// 战斗伤害计算系统接口aa
/// </summary>
public class sdBattleSystem : object
{
    static float fPropertyScale = 0.001f;
	public sdBattleSystem()
	{
	}
	
	public virtual bool init()
	{
		return true;
	}
	
	public virtual void finl()
	{
		return;
	}
	
	public int CalculateAttack(SkillProperty skill,SkillDefProperty receiver,bool isNormalAtk, HeaderProto.ESkillEffect byskilleffect, out bool isSuper, out bool bDodge)
	{
		//首先判断是否命中...		
		isSuper = false;
        bDodge = false;
		float hitRate = skill.hitRate - receiver.dodgeRate;
        if (hitRate <= 0.0f)
        {
            bDodge = true;
            return 0;
        }
		int hit = Random.Range(0,10001);
        if (hit > (int)(hitRate * 10000))
        {
            bDodge = true;
            return 0;
        }
		//判断是否暴击...
		float superAttack = 0.0f;
		float superRate = skill.superRate - receiver.toughness;		
		if(superRate > 0.0f)
		{
			int super = Random.Range(0,10001);
			
			if(super <= (int)(superRate*10000))
			{
				isSuper = true;
				superAttack = skill.superAttack - receiver.superDef;
				if(superAttack < 1.0f)
					superAttack = 1.0f;
				skill.atk 			=	(int)((float)skill.atk *superAttack);
				skill.iceAtk 		=	(int)((float)skill.iceAtk *superAttack);
				skill.fireAtk 		=	(int)((float)skill.fireAtk *superAttack);
				skill.thunderAtk 	=	(int)((float)skill.thunderAtk *superAttack);
				skill.poisonAtk 	=	(int)((float)skill.poisonAtk *superAttack);
				
			}
		}
		float attPerSec	=	skill.attPerSecond;
		int def = receiver.def;
		if(skill.type == SkillType.Skill_Ice)
			def = receiver.iceDef;
		else if(skill.type == SkillType.Skill_Fire)
			def = receiver.fireDef;
		else if(skill.type == SkillType.Skill_Poison)
			def = receiver.poisonDef;
		else if(skill.type == SkillType.Skill_Thunder)
			def = receiver.thunderDef;
		
		def = (int)(def * (1.0f - skill.passThrough));		
		//计算减伤...
		float hurtDec = def / ((float)def + receiver.level*270 + 2700);
		//计算最终伤害...	
		int damage = (int)((((skill.atk + skill.iceAtk + skill.fireAtk + skill.poisonAtk + skill.thunderAtk)*skill.Y + skill.Z)*
			      (1.0f + skill.YZFix) + skill.ZFix)*skill.damagePer);	
		//伤害附加 减伤
		int hurt = 0;
		if(byskilleffect == HeaderProto.ESkillEffect.SKILL_EFFECT_CURE_HP 
			|| byskilleffect == HeaderProto.ESkillEffect.SKILL_EFFECT_CURE_MP)
			hurt = damage;
		else
			hurt = (int)(damage*(1.0f + skill.hurtOtherModify + skill.PropertyModify + skill.BodyTypeModify + skill.StateModify)*(1.0f - hurtDec)*(1.0f + receiver.beHurtModify));
		if(hurt <= 0)
			hurt = Random.Range(1,6);
		return hurt;
	}
	
	public DamageResult testHurt(sdActorInterface _gameActor,Hashtable _state, sdActorInterface targetActor,int hitPointIndex, HeaderProto.ESkillEffect bySkillEffect)
	{
		DamageResult dr;
		dr.damage	=	0;
		dr.bubbleType	=	Bubble.BubbleType.eBT_BaseHurt;
		dr.attracker = _gameActor;
		
		Hashtable recProp = targetActor.GetProperty();
		if(recProp==null)
		{
			return dr;
		}
		SkillProperty skillProp = new SkillProperty();
		
		int dmgMin = (int)_gameActor["AtkDmgMin"];
		int dmgMax = (int)_gameActor["AtkDmgMax"];		
		int iceAtt = (int)_gameActor["IceAtt"];		
		int fireAtt = (int)_gameActor["FireAtt"];		
		int poisonAtt = (int)_gameActor["PoisonAtt"];		
		int thunerAtt = (int)_gameActor["ThunderAtt"];
		int intervalPerAttack = (int)_gameActor["AttSpeed"]; // ms per attack
		float attackPerSecond = 1000.0f / (float)intervalPerAttack;
		int d10000 			= ((int[])_state["dwAtkPowerPer[10]"])[hitPointIndex];
		int dx 				= ((int[])_state["dwDmg[10]"])[hitPointIndex];
		int byPeriodicDamageType	=	0;//(int)_state.stateData["byPeriodicDamage"];
		
		Hashtable attTable = new Hashtable();
        int phyAtt = dmgMin;
        //如果最小攻击力 大于 最大攻击力 直接取最小攻击力...
        if (dmgMax > dmgMin)
        {
            phyAtt = Random.Range(dmgMin, dmgMax);
        }
		
		float 	iPercentFix		=	0.0f;
		int 	iDamageFix		=	0;
		switch((EPeriodicDamage)byPeriodicDamageType)
		{
		    ///每波伤害相同...
			case	EPeriodicDamage.PERIODIC_DAMAGE_SAME:{}break;
			///固定值递增：Data + DataPeriodic * (Index)...
			case	EPeriodicDamage.PERIODIC_DAMAGE_INCREASE:{
				iDamageFix	=	(int)_state["dwPeriodicTraumaHP"]*(hitPointIndex+1);
			}break;
			///百分比递增：HP*(Data + DataPeriodic * (Index))...
		    case	EPeriodicDamage.PERIODIC_DAMAGE_INCREASE_PERCENT:{
				iPercentFix	=	((int)_state["dwPeriodicTraumaHP"]/10000.0f)*(hitPointIndex+1);
			}break;
		    ///固定值递减：Data - DataPeriodic * (Index)...
			case	EPeriodicDamage.PERIODIC_DAMAGE_DECREASE:{
				iDamageFix	=	-(int)_state["dwPeriodicTraumaHP"]*(hitPointIndex+1);
			}break;
		    ///百分比递减：HP*(Data - DataPeriodic * (Index))...
			case	EPeriodicDamage.PERIODIC_DAMAGE_DECREASE_PERCENT:{
				iPercentFix	=	-((int)_state["dwPeriodicTraumaHP"]/10000.0f)*(hitPointIndex+1);
			}break;
			///第一波伤害最大,其余伤害相同,第一波伤害计算公式：...
			///固定值方式：Data - DataPeriodic...
			case	EPeriodicDamage.PERIODIC_DAMAGE_FIRST:{
				if(hitPointIndex==0)
				{
					iDamageFix	=	(int)_state["dwPeriodicTraumaHP"];
				}
			}break;
			///百分比方式：HP*(Data + DataPeriodic)...
		   	case	EPeriodicDamage.PERIODIC_DAMAGE_FIRST_PERCENT:{
				if(hitPointIndex==0)
				{
					iPercentFix	=	((int)_state["dwPeriodicTraumaHP"]/10000.0f);
				}
			}break;
		    ///最后一波伤害最大,其余伤害相同,第一波伤害计算公式：
		    ///固定值方式：Data + DataPeriodic
		    case	EPeriodicDamage.PERIODIC_DAMAGE_END:{
				if(hitPointIndex==-1)
				{
					iPercentFix	=	(int)_state["dwPeriodicTraumaHP"];
				}
			}break;
			///百分比方式：HP*(Data + DataPeriodic)...
			case	EPeriodicDamage.PERIODIC_DAMAGE_END_PERCENT:{
				if(hitPointIndex==-1)
				{
					iPercentFix	=	((int)_state["dwPeriodicTraumaHP"]/10000.0f);
				}
			}break;
		}		
		int skillHit	=	(int)_state["dwHitPer"];
		int skillCri	=	(int)_state["dwCriticalPer"];
		int	skillCriDmg	=	(int)_state["dwCriticalDmgPer"];
		
		skillProp.atk = phyAtt;
		skillProp.iceAtk = iceAtt;
		skillProp.fireAtk = fireAtt;
		skillProp.poisonAtk = poisonAtt;
		skillProp.thunderAtk = thunerAtt;
		skillProp.Y = (float)d10000/10000.0f;
		skillProp.Z = dx;
		skillProp.YZFix = iPercentFix;
		skillProp.ZFix = iDamageFix;
		skillProp.hitRate = (float)_gameActor["Hit"]*fPropertyScale+(float)_gameActor["HitPer"]*0.0001f+(skillHit)*0.0001f;
		int pierce	=	_gameActor["Pierce"];
		skillProp.passThrough = (float)pierce / (float)(pierce + (int)recProp["Level"]*270 + 2700);
		
		skillProp.superAttack = (float)_gameActor["CriDmg"]*0.0001f+(skillCriDmg)*0.0001f;
        skillProp.superRate = (float)_gameActor["Cri"] * fPropertyScale + (float)_gameActor["CriPer"] * 0.0001f + (skillCri) * 0.0001f;
		skillProp.attPerSecond	=	attackPerSecond;
		if(_state.ContainsKey("dwDamagePer"))
			skillProp.damagePer = ((int)_state["dwDamagePer"])*0.0001f;
		else 
			skillProp.damagePer = 1.0f;
		if(_gameActor.Property.ContainsKey("HurtOtherModify"))
			skillProp.hurtOtherModify = ((int)_gameActor["HurtOtherModify"])*0.0001f;
		else
			skillProp.hurtOtherModify = 0.0f;
		
		//属性修正aaa
		int attAttr = (int)_state["byDamegePro"];	
		if(attAttr == 0)
		{
			skillProp.type = SkillType.Skill_Physic;
		}
		else if(attAttr == 1)
		{
			skillProp.type = SkillType.Skill_Ice;
		}
		else if(attAttr == 2)
		{
			skillProp.type = SkillType.Skill_Fire;
		}
		else if(attAttr == 3)
		{
			skillProp.type = SkillType.Skill_Poison;
		}
		else if(attAttr == 4)
		{
			skillProp.type = SkillType.Skill_Thunder;
		}
		skillProp.PropertyModify = CalulatePropertyModify(_gameActor, attAttr);
		///体型修正aaaa
		skillProp.BodyTypeModify = CalulateBodyModify(_state, targetActor);
		//控制状态修正aaa
		skillProp.StateModify = CalulateStateModify(_gameActor, targetActor);
		
		SkillDefProperty defProp = new SkillDefProperty();		
		defProp.level = (int)recProp["Level"];
		defProp.def = (int)recProp["Def"];
        int dodge = (int)recProp["Dodge"];
        defProp.dodgeRate = dodge * fPropertyScale;
        defProp.dodgeRate += ((int)recProp["DodgePer"]) * 0.0001f;
		defProp.fireDef = (int)recProp["FireDef"];
		defProp.iceDef = (int)recProp["IceDef"];
		defProp.poisonDef = (int)recProp["PoisonDef"];
		int criDmgDef = (int)recProp["CriDmgDef"];
		defProp.superDef = (float)criDmgDef*0.0001f;
		defProp.thunderDef = (int)recProp["ThunderDef"];
		int flex = (int)recProp["Flex"];
        defProp.toughness = (float)flex * fPropertyScale;
		if(recProp.ContainsKey("BeHurtModify"))
			defProp.beHurtModify = ((int)recProp["BeHurtModify"])*0.0001f;
		else
			defProp.beHurtModify = 0.0f;
		
		bool isNormalAtk = false;
		
		if(attAttr == 0)
			isNormalAtk = true;
		
		bool isSuper;
        bool bDodge;
		dr.damage	=	CalculateAttack(skillProp,defProp,isNormalAtk, bySkillEffect, out isSuper, out bDodge);	
		CalculateBubbleType(_state, _gameActor, targetActor, isSuper, bDodge,ref dr);
		if(bySkillEffect == HeaderProto.ESkillEffect.SKILL_EFFECT_CURE_HP)
			targetActor.AddHP(dr.damage);
		else if(bySkillEffect == HeaderProto.ESkillEffect.SKILL_EFFECT_CURE_MP)
			targetActor.AddSP(dr.damage);
        else if (bySkillEffect == HeaderProto.ESkillEffect.SKILL_EFFECT_DAMAGE_SP)
            targetActor.AddSP(-dr.damage);
        else
        {
            if (targetActor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_UNBEAT))
            {
                dr.bubbleType = Bubble.BubbleType.eBT_Dodge;
                dr.damage = 0;
            }
            targetActor.OnHurt(dr);
        }
		if(_state.Contains("WeaponHitType"))
			targetActor.DoHittedAudio((string)_state["WeaponHitType"]);
		if(targetActor.actorType == ActorType.AT_Monster && dr.damage > 0)
		{
			sdGameMonster monster = (sdGameMonster)targetActor;
			int monsterType = (int)monster.Property["BodyType"];
			if(_state.Contains("ParentID") && _state.Contains("dwTemplateID")) //临时代码判断是技能伤害aa
			{
				int id = (int)_state["dwTemplateID"];
				_gameActor.OnAttackRestore(id, dr.damage, monsterType);
			}
		}	
		//是否打断被击目标的某些buffer
		if(_state.ContainsKey("BreakState"))
		{
			int[] buffID = (int[])(_state["BreakState"]);
			if(buffID.Length == 2)
				targetActor.RemoveBuff(buffID[0], buffID[1]);		
		}
		return dr;
	}
		
	void  CalculateBubbleType(Hashtable table, sdActorInterface actor, sdActorInterface targetActor, bool isSuper, bool bDodge, ref DamageResult dr)
	{
        if (actor.GetActorType() == ActorType.AT_Pet)
            dr.bubbleType = Bubble.BubbleType.eBT_PetHurt;
        else if (targetActor.GetInstanceID() == sdGameLevel.instance.mainChar.GetInstanceID() && dr.damage != 0)
            dr.bubbleType = Bubble.BubbleType.eBT_SelfHurt;
        else
        {
            if (bDodge)
            {
                if (targetActor.GetInstanceID() == sdGameLevel.instance.mainChar.GetInstanceID())
                    dr.bubbleType = Bubble.BubbleType.eBT_Dodge;
                else
                    dr.bubbleType = Bubble.BubbleType.eBT_Miss;
            }
            //else if (dr.damage == 0)
            //{
            //    dr.bubbleType = Bubble.BubbleType.eBT_Miss;
            //}
            else if (isSuper)
            {
                int skillID = ((int)table["ParentID"]) / 100;
                if (skillID == 1001)
                    dr.bubbleType = Bubble.BubbleType.eBT_CriticalBaseHurt;
                else
                    dr.bubbleType = Bubble.BubbleType.eBT_CriticalSkillHurt;
            }
            else
            {
                int skillID = ((int)table["ParentID"]) / 100;
                if (skillID == 1001)
                {
                    dr.bubbleType = Bubble.BubbleType.eBT_BaseHurt;
                }
                else
                {
                    dr.bubbleType = Bubble.BubbleType.eBT_SkillHurt;
                }
            }
        }
	}

	float CalulatePropertyModify(sdActorInterface actor, int property)
	{
		float nResult = 0.0f;
		switch(property)
		{
			case 0:
			{
				if(actor.Property.ContainsKey("PhysicsModify"))
					nResult = ((int)actor["PhysicsModify"])*0.0001f;
			}break;
			case 1:
			{
				if(actor.Property.ContainsKey("IceModify"))
					nResult = ((int)actor["IceModify"])*0.0001f;
			}break;
			case 2:
			{
				if(actor.Property.ContainsKey("FireModify"))
					nResult = ((int)actor["FireModify"])*0.0001f;
			}break;
			case 3:
			{
				if(actor.Property.ContainsKey("PoisonModify"))
					nResult = ((int)actor["PoisonModify"])*0.0001f;
			}break;
			case 4:
			{
				if(actor.Property.ContainsKey("ThunderModify"))
					nResult = ((int)actor["ThunderModify"])*0.0001f;
			}break;
		}
		return nResult;
	}	
	
	float CalulateBodyModify(Hashtable table, sdActorInterface target)
	{
		float nResult = 0.0f;
		if(!table.ContainsKey("naMoreDamagePer[MONSTER_BODY_TYPE_max]"))
			return nResult;
		if(table["naMoreDamagePer[MONSTER_BODY_TYPE_max]"].GetType() != typeof(int[]))
			return nResult;
		if(target.GetActorType() == ActorType.AT_Monster || target.GetActorType() == ActorType.AT_Pet)
		{
			sdGameMonster monster = (sdGameMonster)target;
			int monsterType = (int)monster.Property["BodyType"];
			int[] percentArray = (int[])table["naMoreDamagePer[MONSTER_BODY_TYPE_max]"];
			if(monsterType >=0 && monsterType < percentArray.Length)
			{
				nResult = (float)(percentArray[monsterType]*0.0001f);
			}
		}
		return nResult;
	}
	float CalulateStateModify(sdActorInterface attack, sdActorInterface target)
	{
		float nResult = 0.0f;
		if(!attack.Property.ContainsKey("StayModify") 
			|| !attack.Property.ContainsKey("HoldModify") 
			|| !attack.Property.ContainsKey("StunModify"))
			return nResult;
		if(target.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY))
			nResult += ((int)attack.Property["StayModify"])*0.0001f;
		if(target.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD))
			nResult += ((int)attack.Property["HoldModify"])*0.0001f;
		if(target.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN))
			nResult += ((int)attack.Property["StunModify"])*0.0001f;
		return nResult;
	}
	
	// @describe 统一的攻击判定(包括主角对怪物\宠物对怪物\怪物对主角\怪物对宠物)aa
	// @param[in] 		kAttacker		攻击者aa
	// @param[in]		kSkillInfo		攻击技能属性表aa	
	// @param[in] 		kStrikeCenter 	攻击中心aa
	// @param[in] 		iHitPointIndex
	// @param[in,out] 	kCallback		攻击回调aa
	public virtual int DoSDAttack(sdActorInterface kAttacker, 
		Hashtable kSkillAction, 
		Vector3 kStrikeCenter, 
		int iHitPointIndex, 
		sdAttackCB kCallback)
	{
		int iStrikeType = (int)kSkillAction["byAoeAreaType"];
		int iStrikeAngle = (int)kSkillAction["nAoeAreaData"];
		float fStrikeDistance = (float)kSkillAction["nAoeArea"] * 0.001f;
		//int iStrikeCenterType = (int)kSkillAction["byAoeCenterType"];

		int iStrikeCenterOffset = 0;//
		if(kSkillAction.ContainsKey("nAoeCenterData"))
		{
			iStrikeCenterOffset = (int)kSkillAction["nAoeCenterData"];
		}

		Vector3 vFixedCenter = kStrikeCenter+kAttacker.GetDirection()*(float)iStrikeCenterOffset*0.001f;
		return DoSDAttack(kAttacker,
			kSkillAction,
			iStrikeType,
		    vFixedCenter,
			fStrikeDistance,
			iStrikeAngle,
			iHitPointIndex,
			kCallback);
	}
	
	// @describe 统一的攻击判定(包括主角对怪物\宠物对怪物\怪物对主角\怪物对宠物)aa
	// @param[in] 		kAttacker		攻击者aa
	// @param[in]		kSkillInfo		攻击技能属性表aa	
	// @param[in] 		iStrikeType		攻击类型aa
	// @param[in] 		kStrikeCenter 	攻击中心aa
	// @param[in] 		fStrikeDisance	攻击范围aa
	// @param[in] 		iStrikeAngle	攻击角度(用于扇形区域)aa
	// @param[in] 		iHitPointIndex
	// @param[in,out] 	kCallback		攻击回调aa	
	public virtual int DoSDAttack(sdActorInterface kAttacker, 
		Hashtable kSkillAction,
		int iStrikeType, 
		Vector3 kStrikeCenter, 
		float fStrikeDistance, 
		int iStrikeAngle, 
		int iHitPointIndex,
		sdAttackCB kCallback)
	{
        return 0;
	}
	
	//< miss 判定aaaaa
	public static bool missPredication(int hit, int hitPer10000, int dodge, int dodgePer10000)
	{
		int totalHit = hit*10000/10 + hitPer10000;
		int totalDodge = dodge*10000/10 + dodgePer10000;
		int delta = totalHit - totalDodge;
		if(delta <= 0)
		{
			return true;
		}
		else if(delta > 0 && delta < 10000)
		{
			int seed = Random.Range(1, 10000 - 1);
			if(seed <= delta)
				return false;
			else
				return true;
		}
		else
		{
			return false;
		}
	}
	
	//< criDmg 判定aaaaa
	public static bool criPredication(int cri, int criPer10000, int flex, int flexPer10000)
	{
		int totalCri = cri*10000/10 + criPer10000;
		int totalFlex = flex*10000/10 + flexPer10000;
		int delta = totalCri - totalFlex;
		if(delta <= 0)
		{
			return true;
		}
		else if(delta > 0 && delta < 10000)
		{
			int seed = Random.Range(1, 10000 - 1);
			if(seed <= delta)
				return false;
			else
				return true;
		}
		else
		{
			return false;
		}
	}
	
	public static int calcCriCoefficient()
	{
		return 0;
	}
}
