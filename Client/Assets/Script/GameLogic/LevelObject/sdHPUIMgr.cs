using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Boss的UI血条aa
/// </summary>
public class sdHPUIMgr : sdTriggerReceiver 
{
	public sdGameMonster[] monsters;
	
	int currentActiveId = -1;
	
	sdFightUi fightUi = null;

	void Start () 
	{
	
	}

	void Update () 
	{
		if(currentActiveId >= 0 && currentActiveId < monsters.Length && monsters[currentActiveId] != null)
		{
			if(fightUi == null)
			{
				GameObject ui = UnityEngine.GameObject.Find("FightUi");
				if (ui != null)
				{
					fightUi = ui.GetComponent<sdFightUi>();	
				}
			}
			
			if(fightUi != null)
			{
				if(monsters[currentActiveId].GetAbility() >= HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss)
				{
					Hashtable uiValueDesc = new Hashtable();
					uiValueDesc["value"] = monsters[currentActiveId].GetCurrentHP();
					uiValueDesc["des"] = "";
					sdUICharacter.Instance.SetProperty("MonsterHp", uiValueDesc);
					sdActGameMgr.Instance.m_uuLapBossNowBlood = monsters[currentActiveId].GetCurrentHP();

					//结算之后，需要重新刷BOSS的血量..
					if (sdActGameMsg.bNeedResetWorldBossHP)
					{
						monsters[currentActiveId].SetCurrentHP(sdActGameMgr.Instance.m_uuWorldBossNowBlood);
						sdActGameMsg.bNeedResetWorldBossHP = false;
					}
					else
					{
						sdActGameMgr.Instance.m_uuWorldBossNowBlood = monsters[currentActiveId].GetCurrentHP();
					}
				
					if(monsters[currentActiveId].GetCurrentHP() <= 0)
					{
						fightUi.HideMonsterHp();
					}
				}
			}
		}
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		if(param[3] < 0)
			return;
		
		currentActiveId = param[3];
		
		if(fightUi == null)
		{
			GameObject ui = UnityEngine.GameObject.Find("FightUi");
			if (ui != null)
			{
				fightUi = ui.GetComponent<sdFightUi>();	
			}
		}
		
		if (fightUi != null && currentActiveId < monsters.Length && monsters[currentActiveId] != null)
		{
			if (monsters[currentActiveId].GetAbility() >= HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss)
			{
				sdTuiTuLogic ttLogic = sdGameLevel.instance.tuiTuLogic;
				//ttLogic.cameraShaker.AddRandomCameraShake(0.5f,0.5f,80.0f,1.0f);

				// 小BOSS和大BOSS的血条样式不同aa
				int iMonsterHPType = 0;
				if (monsters[currentActiveId].GetAbility() == HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss_large)
					iMonsterHPType = 1;
				// 呼出血条，血条初始化..
				int iHpBarNum = monsters[currentActiveId].GetHPBarNum();
				int iMaxHP = monsters[currentActiveId].GetMaxHP();
				sdUICharacter.Instance.SetMonsterMaxHp(iMonsterHPType, iMaxHP, iHpBarNum);
				fightUi.ShowMonsterHp();
				//深渊boss需要再设置一下当前血量，因为Boss有残血的可能..
				if (monsters[currentActiveId].isLapBoss)
				{
					Hashtable uiValueDesc = new Hashtable();
					uiValueDesc["value"] = monsters[currentActiveId].GetCurrentHP();
					uiValueDesc["des"] = "";
					sdUICharacter.Instance.SetProperty("MonsterHp", uiValueDesc);
				}
				//设置boss名字..
				fightUi.SetBossName(monsters[currentActiveId].GetName());
			}
		}
	}
}
