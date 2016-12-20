using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 单件aa
/// </summary>
public	class TSingleton<T>
{
	static	T	instance	=	default(T);
	public	static	T	GetSingleton()
	{
		if(instance==null)
		{
			instance	=	System.Activator.CreateInstance<T>();
		}
		return instance;
	}
	public	static	void	RleaseSingleton()
	{
		instance	=	default(T);
	}
}

// 变换攻击目标(包括找到第一个攻击目标和丢失目标)aa
public delegate void NotifyChangeTargetDelegate(sdActorInterface kActor, sdActorInterface kPreciousTarget, sdActorInterface kTarget);

/// <summary>
/// 自动战斗系统配接器,向自动战斗系统提供控制参数和角色状态,向角色提供设置接口aa
/// </summary>
public class AutoFight
{
	// 是否禁用自动战斗aa
	protected bool mEnable = true;
	public bool Enable
	{
		get { return mEnable;}	
		set { mEnable = value;}
	}

	// 自动战斗系统所属角色aa
	protected sdGameActor mActor = null;	
	public sdGameActor Actor
	{
		get { return mActor;}
	}

	// 战斗状态距离(没有可用技能施放时,与目标保持的距离)aa
	protected float mBattleDistance = 100.0f;
	public float BattleDistance
	{
		get { return mBattleDistance; }
		set { mBattleDistance = value; }
	}

	// 自动战斗系统aa
	protected AutoState kAutoState = null;

	// 变换攻击目标事件aa
	public NotifyChangeTargetDelegate NotifyChangeTarget;

	//
	public AutoFight()
	{
		kAutoState = CreateAutoState();
	}
	
	// 创建自动战斗系统aa
	protected virtual AutoState CreateAutoState()
	{
		return new AutoState();
	}
	
	// 设置所属角色aa
	public virtual void SetActor(sdGameActor _gameActor)
	{
		mActor = _gameActor;
		if (mActor != null)
			kAutoState.Active(true, this);
	}
	
	// 检查是否进入自动战斗处理aa
	protected virtual bool IsNeedUpdate()
	{
		if (mEnable == false)
			return false;		//< 自动战斗被禁用aa

		if (mActor == null)
			return false;		//< 角色被清除aa

		if (mActor.GetCurrentHP()<=0)
			return false;		//< 角色已死亡aa
		
		if (mActor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN))
			return false;		//< 角色进入昏迷状态aa

		return true;
	}
	
	// 更新aa
	public virtual void Update()
	{
		if (!IsNeedUpdate())
			return;
		
		kAutoState.Update(this);
	}

	// 角色移动aa
	public void MoveDir(Vector3 vDirection)
	{
		if (mActor != null)
		{
			mActor._moveVector = vDirection;
		}
	}
	
	// 角色停止移动aa
	public void StopMove()
	{
		if (mActor != null)
		{
			mActor._moveVector = Vector3.zero;
		}
	}

	// 角色释放技能aa
	public	bool CastSkill(int id)
	{
		if (mActor == null)
			return false;

		return mActor.CastSkill(id);
	}

	// 查找角色指定范围内的敌人aa
	public List<sdActorInterface> FindEnemy(float fRadius)
	{
		List<sdActorInterface> kTarget =
			sdGameLevel.instance.actorMgr.FindActor(
			mActor,
			HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
			mActor.transform.position,
			new Vector3(0, 0, 1),
			1,
			0,
			fRadius,
			true);

		return kTarget;
	}

	// 查找指定范围内离角色最近的敌方目标aa
	public sdActorInterface FindNearestEnemy(float fRadius, bool bActive)
	{
		sdActorInterface kTarget = 
		sdGameLevel.instance.actorMgr.FindNearestActor(
			mActor,
			HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
			mActor.transform.position,
			new Vector3(0,0,1),
			1,
			0,
			fRadius,
			bActive);

		return kTarget;
	}

	// 查找指定范围内离角色最近的友方目标aa
	public sdActorInterface FindNearestFriend(float fRadius)
	{
		sdActorInterface kTarget =
		sdGameLevel.instance.actorMgr.FindNearestActor(
			mActor,
			HeaderProto.ESkillObjType.SKILL_OBJ_FRIENDLY,
			mActor.transform.position,
			new Vector3(0, 0, 1),
			1,
			0,
			fRadius,
			true);

		return kTarget;
	}

	// 角色当前位置
	public	Vector3	GetPosition()
	{
		if (mActor == null)
			return Vector3.zero;

		return mActor.transform.position;
	}

	// 计算两个角色之间的距离aa
	public float CalcDistance(sdActorInterface kActor1, sdActorInterface kActor2)
	{
		if (kActor1 == null || kActor2 == null)
			return float.MaxValue;

		Vector3 kPosition1 = kActor1.transform.position;
		Vector3 kPosition2 = kActor2.transform.position;
		Vector3 kOffset = kPosition1 - kPosition2;
		kOffset.y = 0.0f;

		return kOffset.magnitude;
	}

	// 计算两个角色之间的技能距离(角色1到角色2边缘的距离)
	public float CalcSkillDistance(sdActorInterface kActor1, sdActorInterface kActor2)
	{
		if (kActor1 == null || kActor2 == null)
			return float.MaxValue;

		Vector3 kPosition1 = kActor1.transform.position;
		Vector3 kPosition2 = kActor2.transform.position;
		Vector3 kOffset = kPosition1 - kPosition2;
		kOffset.y = 0.0f;

		float fSkillDistance = kOffset.magnitude - kActor2.getRadius();
		if (fSkillDistance < 0.0f)
			fSkillDistance = 0.0f;

		return fSkillDistance;
	}

	// 获取当前攻击目标aa
	public sdActorInterface GetAttackTarget()
	{
		return kAutoState.GetAttackTarget();
	}

	// 判定一个目标是否可被作为攻击目标aa
	public bool CanBeAttacked(sdActorInterface kActor)
	{
		return (kActor != null) && kActor.IsActive() && (kActor.GetCurrentHP() > 0 && !kActor.Hide);
	}
	
	// 角色的伤害回调函数aa
	public virtual void OnHurt(sdActorInterface kAttacker)
	{
		if (mEnable)
			kAutoState.OnHurt(kAttacker, this);
	}

	// 队友受到伤害回调函数aa
	public virtual void OnFriendHurt(sdActorInterface kAttacker)
	{
		if (mEnable)
			kAutoState.OnFriendHurt(kAttacker, this);
	}

	// 切换攻击目标(战斗系统内部调用)aa
	public void OnChangeTarget(sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		if (NotifyChangeTarget != null)
			NotifyChangeTarget(mActor, kPreviousTarget, kTarget);
	}
    public  List<Vector3> GetCurrentPath()
    {
        return kAutoState.GetCurrentPath();
    }
    public virtual void OnMovePointEnd(MovePointState mp)
    { 
        
    }
}

/// <summary>
/// 怪物自动战斗系统配接器aa
/// </summary>
public class MonsterAutoFight : AutoFight
{
	// 所属怪物aa
	protected sdGameMonster mMonster = null;
	public sdGameMonster Monster
	{
		get { return mMonster;}	
	}

	// 视野范围(空闲状态搜索目标的距离)aa
	protected float mEyeDistance = 3.0f;
	public float EyeDistance
	{
		get { return mEyeDistance; }
		set { mEyeDistance = value; }
	}
	
	// 自动追击距离(不受目标攻击一定时间之后重新所搜目标的距离)aa
	protected float mChaseDistance = 3.0f;
	public float ChaseDistance
	{
		get { return mChaseDistance; }
		set { mChaseDistance = value; }
	}
	
	// 触发战斗系统aa
	protected sdBehaviourAdvancedState mBehaviourAdvancedState = new sdBehaviourAdvancedState();
	
	// 创建自动战斗系统(继承自AutoFight)aa
	protected override AutoState CreateAutoState()
	{
		return new MonsterAutoState();
	}
	
	// 设置所属角色(继承自AutoFight)aa
	public override	void SetActor(sdGameActor _gameActor)
	{
		base.SetActor(_gameActor);
		
		mMonster = (sdGameMonster)_gameActor;
		mBehaviourAdvancedState.SetMonsterAutoFight(this);	//< 初始化触发战斗系统aa
	}

	// 更新(继承自AutoFight)aa
	public override void Update()
	{
		base.Update ();
		mBehaviourAdvancedState.UpdateBehaviourTree();		//< 更新触发战斗系统aa
	}
}

/// <summary>
/// 宠物自动战斗系统配接器aa
/// </summary>
public class PetAutoFight : MonsterAutoFight
{
	// 自动跟随距离aa
	protected float mFollowDistance = 3.0f;
	public float FollowDistance
	{
		get { return mFollowDistance; }
		set { mFollowDistance = value; }
	}
	
	// 战斗自动跟随距离aa
	protected float mBattleFollowDistance = 8.0f;
	public float BattleFollowDistance
	{
		get { return mBattleFollowDistance; }
		set { mBattleFollowDistance = value; }
	}

	// 视野范围aa
	protected float mEyeDistance = 7.0f;
	public float EyeDistance
	{
		get { return mEyeDistance; }
		set { mEyeDistance = value; }
	}
	
	// 是否开启撤退aa
	protected bool mAutoRetreat = false;
	public bool AutoRetreat
	{
		get { return mAutoRetreat; }
		set { mAutoRetreat = value; }
	}

	// 撤退检测时间间隔aa
	protected float mRetreatDetectInterval = 2.0f;
	public float RetreatDetectInterval
	{
		get { return mRetreatDetectInterval; }
		set { mRetreatDetectInterval = value; }
	}

	// 撤退检测时允许的离敌方最小距离aa
	protected float mRetreatDetectMinDistance = 2.0f;
	public float RetreatDetectMinDistance
	{
		get { return mRetreatDetectMinDistance; }
		set { mRetreatDetectMinDistance = value; }
	}

	// 撤退持续时间aa
	protected float mRetreatElapseTime = 1.0f;
	public float RetreatElapseTime
	{
		get { return mRetreatElapseTime; }
		set { mRetreatElapseTime = value; }
	}

	// 创建自动战斗系统(继承自AutoFight)aa
	protected override AutoState CreateAutoState()
	{
		return new PetAutoState();
	}
	
	// 宠物手动技能aa
	public bool DoXPSkill(int iSkillID,ref int iErrorCode)
	{
		PetAutoState kPetAutoState = kAutoState as PetAutoState;
		return kPetAutoState.DoXPSkill(iSkillID, this, ref iErrorCode);
	}
}

/// <summary>
/// 主角自动战斗系统配接器aa
/// </summary>
public class FingerControl : AutoFight
{
	//
	protected float fStopTime =	-1.0f;

	// 自动战斗系统aa
	protected PlayerAutoState mPlayerAutoState;

    GameObject moveTarget = null;

	// 主相机aa
	protected Camera mMainCamera = null;
	public Camera MainCamera
	{
		set { mMainCamera = value;}
	}

	// 创建自动战斗系统(继承自AutoFight)aa
	protected override AutoState CreateAutoState()
	{
		mPlayerAutoState = new PlayerAutoState();
		return mPlayerAutoState;
	}

	// 更新(继承自AutoFight)aa
	public override void Update()
	{
		if (!IsNeedUpdate())
			return;
		
		if (fStopTime > 0.0f)
		{
			fStopTime -= Time.deltaTime;
			if (fStopTime < 0.0f)
			{
				StopMove();
				fStopTime = -1.0f;
			}
		}
		
		kAutoState.Update(this);
	}
    void ShowMoveTarget(Vector3 v)
    {
        

        ResLoadParams param =    new ResLoadParams();
        param.pos = v;
        sdResourceMgr.Instance.LoadResource("Effect/MainChar/FX_UI/$Fx_Jiantou/Fx_Jiantou_001_prefab.prefab", OnLoadEffect, param);
    }
    void HideMoveTarget()
    {
        if (moveTarget != null)
        {
            GameObject.Destroy(moveTarget);
        }
    }
    void OnLoadEffect(ResLoadParams param, Object obj)
    { 
        HideMoveTarget();
        moveTarget = (GameObject)GameObject.Instantiate(obj,param.pos,Quaternion.identity);
    }

	// 
	public void OnFingerUp(Vector2 v)
	{
		if (!mEnable)
			return;

		if (mMainCamera == null)
			return;

		Ray ray = mMainCamera.ScreenPointToRay(v);
		
		sdActorInterface monster	=	PickupMonster(ray);
		if(monster==null){
		
			Vector3 point = new Vector3();
			if(FingerGesturesInitializer.NavMesh_RayCast(ray,ref point,100000.0f))
			{
				
				Vector3 vNewPoint	=	point	+	new Vector3(0,1,0);
				Ray	newRay			=	new Ray(vNewPoint,new Vector3(0,-1,0));
				RaycastHit hit;
				if(Physics.Raycast(newRay,out hit))
				{
                    mPlayerAutoState.SetPoint(hit.point,this);
                    ShowMoveTarget(hit.point);
				}
				else
				{
					mPlayerAutoState.SetPoint(point,this);
                    ShowMoveTarget(point);
				}
                
			}else{

                HideMoveTarget();
			}
		}
		else
		{
			mPlayerAutoState.SetTarget(monster,this);
		}
	}
	
	// 快速转向并释放技能aa
	public	void QuickSkill(Vector2 v, int iSkillID)
	{
		if (mMainCamera == null)
			return;
		
		Ray ray = mMainCamera.ScreenPointToRay(v);
		sdActorInterface monster = PickupMonster(ray);
		if (monster == null){
		
			Vector3 terrainPosition = new Vector3();
			if (FingerGesturesInitializer.NavMesh_RayCast(ray, ref terrainPosition,100000.0f))
			{
				Vector3 charPosition = GetPosition();
				Vector3 nextDirection = terrainPosition - charPosition;
				MoveDir(nextDirection);
				fStopTime = 0.3f;
			}
		}
		else
		{
			Vector3 monsterPosition = monster.transform.position;
			Vector3 charPosition = GetPosition();
			Vector3 nextDirection = monsterPosition - charPosition;
			MoveDir(nextDirection);
			fStopTime = 0.3f;
		}
		
		CastSkill(iSkillID);
	}

	//
	static sdActorInterface PickupMonster(Ray ray)
	{
		GameObject	obj	=	null;
		int layerMask	=	1<<LayerMask.NameToLayer("Monster");
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,100000,layerMask))
		{
			//Debug.Log(hit.collider.gameObject.name);
			obj	= hit.collider.gameObject;
		}
		if(obj==null)
		{
			return null;
		}
		
		return obj.GetComponent<sdGameMonster>();
	}

	// 设置搜索半径aa
	public virtual void SetSearchDistance(float fDistance)
	{
		mPlayerAutoState.ResearchDistance = fDistance;
	}

	// 设置自动战斗模式aa
	public void SetFullAutoMode(bool bFullAutoMode)
	{
		mPlayerAutoState.SetFullAutoMode(bFullAutoMode, this);
	}

	// 使用指定技能攻击最近的目标aa
	public bool AttackNearest(int skill_id)
	{
		return mPlayerAutoState.SetNearestTarget(skill_id,this);
	}

	//
	public void AdjustDirection(int skillid)
	{
		mPlayerAutoState.AdjustDirection(skillid, this);
	}

	// 翻滚aa
	public	void Roll(Vector3 vDirection)
	{
		if (!mEnable)
			return;

		mPlayerAutoState.Roll(vDirection, this);
	}

	//
	public void  StopMove_ClearPath()
	{
		StopMove();
		mPlayerAutoState.ClearMainCharacterMove(this);
	}

	//
	public void ClearMainCharacterMove()
	{
		mPlayerAutoState.ClearMainCharacterMove(this);
	}
    public void ResearchTarget()
    {
        mPlayerAutoState.ClearMainCharacterMove(this);
        sdActorInterface actor = mPlayerAutoState.GetAttackTarget();
        if(actor!=null)
        {
            mPlayerAutoState.SetTarget(actor, this);
        }
    }
    public override void OnMovePointEnd(MovePointState mp)
    {
        PlayerAutoState pas = (PlayerAutoState)kAutoState;
        if (mp == pas.GetMovePointState())
        {
            HideMoveTarget();
        }
    }
}

/// <summary>
/// 异步PVP自动战斗系统配接器aa
/// </summary>
public class PVPAutoFight : AutoFight
{
	// 自动战斗系统aa
	protected PVPAutoState mPVPAutoState;

	// 攻击目标(PVP角色只攻击主角)aa
	protected sdActorInterface mTarget;
	public sdActorInterface Target
	{
		get { return mTarget; }
		set { mTarget = value; }
	}

	// 创建自动战斗系统(继承自AutoFight)aa
	protected override AutoState CreateAutoState()
	{
		mPVPAutoState = new PVPAutoState();
		return mPVPAutoState;
	}

	// 设置所属角色(继承自AutoFight)aa
	public override void SetActor(sdGameActor _gameActor)
	{
		base.SetActor(_gameActor);

		mPVPAutoState.SetSkill(_gameActor);
	}
}
