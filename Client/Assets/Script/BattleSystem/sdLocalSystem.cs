using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 本地战斗伤害计算系统aa
/// </summary>
public class sdLocalSystem : sdBattleSystem
{
	public sdGameLevel gameLevel = null;
	public sdTuiTuLogic tuiTuLogic = null;
	
	public sdLocalSystem()
	{
	}
	
	public override bool init()
	{
		return true;
	}
	
	public override void finl()
	{
		return;
	}
	// @describe 统一的攻击判定aa
	public override int DoSDAttack(sdActorInterface kAttacker, 
		Hashtable kSkillInfo,
		int iStrikeType, 
		Vector3 kStrikeCenter, 
		float fStrikeDistance, 
		int iStrikeAngle, 
		int iHitPointIndex,
		sdAttackCB kCallback)
	{
        int nRet = 0;
		if (tuiTuLogic == null)
            return nRet;
		
		if (gameLevel == null)
            return nRet;
		
		if (kAttacker == null)
            return nRet;
		
		if (kSkillInfo == null)
            return nRet;

		HeaderProto.ESkillObjType objType = (HeaderProto.ESkillObjType)kSkillInfo["byTargetType"];
		List<sdActorInterface> kTargetList = gameLevel.actorMgr.FindActor(
			kAttacker,
			objType,
			kStrikeCenter,
			kAttacker.GetDirection(),
			iStrikeType,
			iStrikeAngle,
			fStrikeDistance,
			true);	
		if(kTargetList==null)
		{
            return nRet;
		}

		// AOE技能最大攻击目标aa
		if(kSkillInfo.ContainsKey("wAoeAimNum"))
		{
			int nMaxTarget = (int)kSkillInfo["wAoeAimNum"];
			if(kTargetList.Count > nMaxTarget)
			{
				kTargetList.RemoveRange(nMaxTarget, kTargetList.Count - nMaxTarget);
			}
		}

		// 技能特效aa
		HeaderProto.ESkillEffect skilleffect = HeaderProto.ESkillEffect.SKILL_EFFECT_DAMAGE_HP;
		if(kSkillInfo.ContainsKey("bySkillEffect"))
		{
			skilleffect = (HeaderProto.ESkillEffect)(kSkillInfo["bySkillEffect"]);
		}

		// 判定伤害aa
		if (kCallback != null)
		{
			return kCallback.OnHit(kAttacker, kTargetList, iHitPointIndex, null, skilleffect);	
		}
		else
		{
			sdBattleSystem kBattleSystem = sdGameLevel.instance.battleSystem;
			kTargetList.ForEach(delegate(sdActorInterface kActor) 
			{
				DamageResult dr = kBattleSystem.testHurt(
					kAttacker,
					kSkillInfo,
					kActor,
					iHitPointIndex,
					skilleffect
					);
                if (Bubble.IsHurtOther(dr.bubbleType))
                    ++nRet;
			});
            return nRet;
		}
	}	
}
