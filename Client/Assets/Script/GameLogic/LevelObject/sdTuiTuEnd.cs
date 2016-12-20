using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 推图结束触发器aa
/// </summary>
public class sdTuiTuEnd : sdTriggerReceiver 
{
	void Awake()
	{
		
	}

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj, param);

		// 杀死所有怪物aa
		sdMainChar kMainChar = sdGameLevel.instance.mainChar;
		List<sdActorInterface> lstActor =
		sdGameLevel.instance.actorMgr.FindActor(
			kMainChar,
			HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
			kMainChar.transform.position,
			Vector3.zero,
			1,
			0,
			10000.0f,
			true);
		if (lstActor != null)
		{
			foreach (sdActorInterface a in lstActor)
			{
				if (a.actorType == ActorType.AT_Monster)
				{
					int hp = a["HP"];
					a.AddHP(-hp);
				}
			}
		}

		// 禁用所有的按键和手势响应aa
		sdGameLevel.instance.DestroyFingerObject();
		sdGameLevel.instance.mainChar.AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY);
		sdGameLevel.instance.mainChar.AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL);
		GPUSH_API.Vibrate(500);

		// 初始化胜利动画aa
		sdGameMonster kMonster = obj.GetComponent<sdGameMonster>();
		sdGameLevel.instance.mainCamera.InitVictory(kMonster);

		// 请求结算aa
		if (sdUICharacter.Instance.GetBattleType() == (byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_WORLD_BOSS)
		{
			sdActGameMsg.Send_CS_WB_RESULT_REQ(0);
			sdActGameMsg.OnWorldBossBeKilled();
		}
		else
		{
			Debug.Log("TuituEnd and Request Jiesuan!");
            sdGameLevel.instance.tuiTuLogic.ShowFightResult(obj);
		}
	}
}
