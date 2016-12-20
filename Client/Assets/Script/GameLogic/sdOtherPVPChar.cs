using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 其他玩家角色对象(用于PVP移动同步)aa
/// </summary>
public class sdOtherPVPChar : sdGameActor_Impl
{
	// 基础属性表aa
	public Hashtable mBaseProperty = null;

	// 获取角色标准属性表(继承自sdActorInterface)aa
	public override Hashtable GetBaseProperty()
	{
		return mBaseProperty;
	}

	// 初始化aa
	public new bool init(sdGameActorCreateInfo kInfo)
	{
		base.init(kInfo);

		// 设置角色初始朝向aa
		mFaceDirection = transform.rotation * Vector3.forward;

		// 初始化技能树aas
		CreateSkill_Action(kInfo.mJob);

		// 初始化基本属性表aa
		mBaseProperty = sdConfDataMgr.CloneHashTable(sdPVPManager.Instance.PVPBaseProperty);
		Property = sdConfDataMgr.CloneHashTable(sdPVPManager.Instance.PVPBaseProperty);

		// 每次进图满血满蓝aa
		if (Property.ContainsKey("HP") && Property.ContainsKey("MaxHP"))
		{
			Property["HP"] = Property["MaxHP"];
		}

		if (Property.ContainsKey("SP") && Property.ContainsKey("MaxSP"))
		{
			Property["SP"] = Property["MaxSP"];
		}

		// 初始化角色装备表aa
		SetItemInfo(sdPVPManager.Instance.PVPItemProperty);

		// 初始化角色技能表aa
		SetSkillInfo(sdPVPManager.Instance.PVPSkillProperty);

		// 宠物战队Buffaa
		List<int> kBuffList = sdPVPManager.Instance.GetPetGroupBuff(this);
		if (kBuffList != null)
		{
			foreach (int iBuffId in kBuffList)
			{
				AddBuff(iBuffId, 0, this);
			}
		}

		// 加载角色动画aa
		LoadAnimationFile(kInfo.mJob);

		// 自动战斗系统aa
		{
			PVPAutoFight kPVPAutoFight = new PVPAutoFight();

			sdSkill kSkill = skillTree.getSkill(1001);
			if (kSkill != null)
			{
				sdBaseState kState = kSkill.actionStateList[0];
				int iCastDistance = (int)kState.stateData["CastDistance"];
				kPVPAutoFight.BattleDistance = (iCastDistance) * 0.001f;
			}

			mAutoFight = kPVPAutoFight;
			mAutoFight.SetActor(this);
		}

		return true;
	}

	// 初始化(继承自sdActorInterface)
	protected override void Awake()
	{
		base.Awake();

		groupid = GroupIDType.GIT_MonsterA;
	}

	// 更新(继承自sdBaseTrigger)
	protected override void Update()
	{
		base.Update();

		tickFrame();

		// 设置战斗系统攻击目标aa
		PVPAutoFight kPVPAutoFight = mAutoFight as PVPAutoFight;
		if (kPVPAutoFight != null)
		{
			if (kPVPAutoFight.Target == null)
				kPVPAutoFight.Target = sdGameLevel.instance.mainChar;
		}
	}

	// 每帧更新aa
	private int test_tick = 0;
	public void tickFrame()
	{
		// 更新技能树aa
		if (skillTree != null)
			skillTree.tick();

		// 更新状态机aa
		if (logicTSM != null && renderNode != null)
		{
			// 必须多跑一个循环,准备好动画才行aaa
			if (test_tick > 1)
				logicTSM.UpdateSelf();
			++test_tick;
		}

		// 加载刀光
		if (mWeaponTrail == null)
			mWeaponTrail = createWeaponTrail("dummy_right_weapon_at");

		// 颜色闪白aa
		UpdateHittedLight();

		// 更新自动战斗aa
		if (IsActive() && (GetCurrentHP() > 0))
			mAutoFight.Update();
	}
}

