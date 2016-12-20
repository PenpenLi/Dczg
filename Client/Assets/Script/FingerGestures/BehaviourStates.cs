using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 自动战斗系统基础状态aa
/// </summary>
public class BehaviourState
{
	// 标记当前状态是否被激活aa
	protected bool mActive = false;
	
	// 
	public bool	Active()
	{
		return mActive;
	}
	
	// 
	public void Active(bool bActive, AutoFight kAutoFight)
	{
		mActive	= bActive;
		if (bActive)
		{
			Begin(kAutoFight);
		}
		else
		{
			End(kAutoFight);
		}
	}
	
	//
	public virtual void Begin(AutoFight kAutoFight)
	{
		
	}
	
	//
	public virtual void End(AutoFight kAutoFight)
	{
		
	}
	
	//
	public virtual void Update(AutoFight kAutoFight)
	{
		
	}
}

/// <summary>
/// 施放指定技能状态(持续施放指定技能)aa
/// </summary>
public class SkillState : BehaviourState
{
	// 攻击目标aa
	protected sdActorInterface mTarget = null;
	public sdActorInterface Target
	{
		set { mTarget = value; }
		get { return mTarget; }
	}

	// 攻击技能aa
	protected int mSkillID = 0;
	public int SkillID
	{
		set { mSkillID = value; }
		get { return mSkillID; }
	}

	// 到达攻击之后是否自动取消状态aa
	protected bool mAutoDeactive = false;
	public bool AutoDeactive
	{
		get { return mAutoDeactive; }
		set { mAutoDeactive = value; }
	}

	// 攻击时间间隔(用于限制普通攻击频率)aa
	protected float mAttackDeltaTime = 0.0f;
	public float AttackDeltaTime
	{
		set { mAttackDeltaTime = value; }
		get { return mAttackDeltaTime; }
	}

	// 上次施放技能时间aa
	protected float mLastAttackTime = 0.0f;

	// 更新(继承自BehaviourState)aa
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
		{
			return;
		}

		// 检查参数aa
		if (mTarget == null || mSkillID == 0)
			return;

		// 检查时间间隔aa
		if (Time.time - mLastAttackTime < mAttackDeltaTime)
			return;

		mLastAttackTime = Time.time;

		// 调整角色方向aa
		sdGameActor kActor = kAutoFight.Actor;
		Vector3 vPos = kAutoFight.GetPosition();
		Vector3 vDst = mTarget.transform.position;
		Vector3 vDir = vDst - vPos;
		vDir.y = 0.0f;
		vDir.Normalize();

		kActor.TargetFaceDirection = vDir;

		// 等待上一级能释放完毕,释放当前技能aa
		if (kActor.logicTSM.nextState == null)
		{
			kActor.CastSkill(mSkillID, vDir);

			if (mAutoDeactive)
			{
				mTarget = null;
				mSkillID = 0;
				mAttackDeltaTime = 0.0f;
				Active(false, kAutoFight);
			}
		}
	}
}

// 到达目标点的判断方式aa
public enum EReachType
{
	RT_ByDistance,		//< 直线距离aa
	RT_ByWalkDistance,	//< 行走距离aa
}

// 寻路回调参数aa
public enum EPathFindType
{ 
    PreFind,
    Failed,
    OK
}

// 寻路回调函数aa
public delegate void NotifyPathFindDelegate(AutoFight kAutoFight, EPathFindType result);

/// <summary>
/// 移动到指定位置,并自动退出移动状态aa
/// </summary>
public class MovePointState : BehaviourState
{
	// 寻路回调aa
	public NotifyPathFindDelegate NotifyPathFind = null;
    AutoFight autoFight = null;
	// 起始点位置aa
	protected Vector3 mSrcPosition = Vector3.zero;
	public Vector3 SrcPosition
	{
		get { return mSrcPosition; }
		set { mSrcPosition = value;}
	}
	
	// 目标点位置aa
	protected Vector3 mDstPosition = Vector3.zero;
	public Vector3 DstPosition
	{
		get { return mDstPosition; }	
		set { mDstPosition = value;}
	}
	
	// 到达目标点的判断方式aa
	protected EReachType mReachType = EReachType.RT_ByDistance;
	public EReachType ReachType
	{
		get { return mReachType; }	
		set { mReachType = value;}		
	}
	
	// 到达目标点的最小距离aa
	protected float mMinDistance = 0.1f;
	public float MinDistance
	{
		get { return mMinDistance; }	
		set { mMinDistance = value;}		
	}
	
	// 移动路径和剩余移动距离aa
	protected List<Vector3> mWalkPath = new List<Vector3>();
	protected List<float> mWalkDistance = new List<float>();

	//
	public override	void Begin(AutoFight kAutoFight)
	{
		mSrcPosition = kAutoFight.GetPosition();
		if (Vector3.Distance(mSrcPosition, mDstPosition) < mMinDistance)
		{
			Active(false, kAutoFight);
			return;
		}
		
		mWalkPath.Clear();
        autoFight = kAutoFight;
        Hexagon.Manager.GetSingleton().AddTask(this);
	}
	
	//
	public override void End(AutoFight kAutoFight)
	{
		kAutoFight.StopMove();
		
		mWalkPath.Clear();
        kAutoFight.OnMovePointEnd(this);

	}
    public void FindPath()
    {
        if (autoFight.Actor == null)
        {
            return;
        }
        NavMeshAgent kNavAgent = autoFight.Actor.GetComponent<NavMeshAgent>();
        if (kNavAgent != null)
        {
            NavMeshPath kNavPath = new NavMeshPath();

            // 寻路回调aa
            if (NotifyPathFind != null)
            {
                NotifyPathFind(autoFight, EPathFindType.PreFind);
            }

            // 寻路操作aa
            List<Vector3> path = null;
            bool bRet = Hexagon.Manager.GetSingleton().FindPath(autoFight.Actor.transform.position, mDstPosition, out path, true);
            if (!bRet)
            {
                if (!kNavAgent.enabled)
                    kNavAgent.enabled = true;

                if (kNavAgent.enabled)
                {
                    bRet = kNavAgent.CalculatePath(mDstPosition, kNavPath);
                    kNavAgent.enabled = false;
                }

                if (bRet)
                {
                    for (int i = 1; i < kNavPath.corners.Length; i++)
                    {
                        mWalkPath.Add(kNavPath.corners[i]);
                    }
                }
            }
            else
            {
                mWalkPath = path;
            }

            // 寻路回调aa
            if (NotifyPathFind != null)
            {
                NotifyPathFind(autoFight, bRet ? EPathFindType.OK : EPathFindType.Failed);
            }

            if (bRet)
            {
                for (int i = 0; i < mWalkPath.Count - 1; i++)
                {
                    Vector3 vDistance = mWalkPath[i] - mWalkPath[i + 1];
                    vDistance.y = 0.0f;

                    mWalkDistance.Add(vDistance.magnitude);	//< 计算每段路径的长度aa
                }

                for (int j = mWalkDistance.Count - 1; j >= 0; --j)
                {
                    int i = j - 1;
                    if (i < 0 || j < 0)
                        break;

                    mWalkDistance[i] += mWalkDistance[j];	//< 计算到目标点的累计长度aa
                }
            }
            else
            {

                Debug.Log("Calc Nav Path Failed!");
                //防止怪物 从物理碰撞中掉落下去 导致无法继续关卡..
                Vector3 vPos = autoFight.Actor.transform.position + new Vector3(0, 100.0f, 0);
                Vector3 vDir = new Vector3(0, -1.0f, 0);
                RaycastHit info;
                int layerMonster = LayerMask.NameToLayer("Monster");
                int layerPlayer = LayerMask.NameToLayer("Player");
                int layerPet = LayerMask.NameToLayer("Pet");
                int mask = (1 << layerMonster) | (1 << layerPlayer) | (1 << layerPet);
                int invMask = ~mask;
                //尝试从上往下 产生一条射线 如果射线与物理相交，则直接把角色放置到相交点..
                if (Physics.Raycast(vPos, vDir, out info, 10000.0f, invMask))
                {
                    autoFight.Actor.transform.position = info.point;
                }
                else
                {
                    //如果无法相交 直接杀死..
                    int hp = autoFight.Actor.GetCurrentHP();
                    autoFight.Actor.AddHP(-hp);
                }
            }
        }
    }
	
	//
	public override	void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;
		
		// 判断是否到达目标点aa
		Vector3 kPosition = kAutoFight.GetPosition();
		if (mReachType == EReachType.RT_ByDistance)
		{
			if (Vector3.Distance(kPosition, mDstPosition) < mMinDistance)	
			{
				Active(false, kAutoFight);
				return;
			}
		}
		else if (mReachType == EReachType.RT_ByWalkDistance)
		{
			Vector3 vDistance = mWalkPath[0] - kPosition;
			vDistance.y = 0;
	
			if (mWalkDistance[0] + vDistance.magnitude < mMinDistance)
			{
				Active(false, kAutoFight);
				return;
			}
		}
		
		// 判断移动方向aa
		sdGameActor	mc = kAutoFight.Actor;
		if (mWalkPath.Count > 0)
		{
			Vector3 voffset = mWalkPath[0] - kPosition;
            bool result = false;
            if (mWalkPath.Count == 1)
            { 
                result  =   Vector2.Distance(new Vector2(voffset.x,voffset.z),Vector2.zero) < mc.GetMoveSpeed()*Time.deltaTime;
            }
            else
            {
                result  =   Vector2.Distance(new Vector2(voffset.x,voffset.z),Vector2.zero) < mc.GetMoveSpeed()*Time.deltaTime*4.0f;
            }
			if(result)
			{
				mWalkPath.RemoveAt(0);
				
				if (mWalkDistance.Count > 0)
					mWalkDistance.RemoveAt(0);
			}
			
			if(mWalkPath.Count>0)
			{
				Vector3 vDir	=	mWalkPath[0]-mc.transform.position;
				vDir.y=0;
				vDir.Normalize();
				mc._moveVector	=	vDir;
			}
			else
			{
				Active(false, kAutoFight);
			}
		}
	}
    public List<Vector3> GetCurrentPath()
    {
        return mWalkPath;
    }
}

/// <summary>
/// 移动到目标位置(自动跟随)aa
/// </summary>
public class MoveTargetState : BehaviourState
{
	// 移动目标aa
	public sdActorInterface target = null;
	
	// 到达目标点的判断方式aa
	public EReachType ReachType
	{
		get { return state.ReachType; }	
		set { state.ReachType = value;}		
	}
	
	// 到达目标点的最小距离aa
	public float MinDistance
	{
		get { return state.MinDistance; }	
		set { state.MinDistance = value;}		
	}
	
	// 到达目标之后是否自动取消状态aa
	protected bool mAutoDeactive = false;
	public bool AutoDeactive
	{
		get { return mAutoDeactive; }	
		set { mAutoDeactive = value;}	
	}
	
	// 移动状态aa
	protected MovePointState state = new MovePointState();
	
	// 自从上次计算路径的时间aa
	protected float fRecalcPath = 0.0f;
	
	//
	public override	void Begin(AutoFight kAutoFight)
	{
		state.NotifyPathFind = OnPathFind;
		state.DstPosition = target.transform.position;
		state.Active(true, kAutoFight);
//		if(!state.Active())
//		{
//			Active(false, kAutoFight);
//		}
	}
	
	//
	public override	void End(AutoFight kAutoFight)
	{
		if (state.Active())
		{
			state.Active(false, kAutoFight);
		}
	}
	
	//
	public override void Update(AutoFight kAutoFight)
	{
//		if(!Active())
//		{
//			return;
//		}
		
		// 确定是否取消激活状态aa
		if (mAutoDeactive)
		{
			if(!state.Active())
			{
				Active(false, kAutoFight);
				return;
			}
		}
		
		// 确定是否重新计算路径aa
		fRecalcPath	+=Time.deltaTime;
		
		bool bNeedRecalcPath = false;
		if (Vector3.Distance(state.DstPosition, target.transform.position) > state.MinDistance)
		{
			bNeedRecalcPath = true;
		}
		else
		{
			if (fRecalcPath > 1.0f)
				bNeedRecalcPath = true;
		}
		
		if (bNeedRecalcPath)
		{
			fRecalcPath	= 0.0f;
			
			state.DstPosition = target.transform.position;
			state.Active(true, kAutoFight);
			if (!state.Active())
				return;
		}
		
		//
		state.Update(kAutoFight);
	}
	
	//
	public void Stop(AutoFight kAutoFight)
	{
		state.Active(false, kAutoFight);
		kAutoFight.Actor._moveVector = Vector3.zero;
	}
    public List<Vector3> GetCurrentPath()
    {
        return state.GetCurrentPath();
    }

	// 寻路回调(用于取消与重新设置角色权重)aa
    protected void OnPathFind(AutoFight kAutoFight,EPathFindType result)
    {
        if (result == EPathFindType.PreFind)
        {
            kAutoFight.Actor.UnInject(false);
            if (target != null)
            {
                ((sdGameActor)target).UnInject(false);
            }
        }
        else
        {
            kAutoFight.Actor.Inject(false);
            if (target != null)
            {
                ((sdGameActor)target).Inject(false);
            }
        }
    }
}

/// <summary>
/// 自动追击并攻击目标到目标死亡aa
///  1.选择合适的技能进行施放(优先对目标使用手动技能,然后是自动技能,最后是普攻技能)aa
///  2.控制移动(追击和闪避)aa
/// </summary>
public class TargetState : BehaviourState
{
	// 移动到目标状态aa
	protected MoveTargetState mMoveState = new MoveTargetState();
	
	// 施放技能状态aa
	protected SkillState mSkillState = new SkillState();

	// 手动技能aa
	protected int mManualSkill = 0;
	public int ManualSkill
	{
		set { mManualSkill = value; }
		get { return mManualSkill; }
	}

	// 普通攻击技能aa
	protected int mCommonSkill = 1001;
	public int CommonSkill
	{
		set { mCommonSkill = value; }
		get { return mCommonSkill; }
	}

	// 自动技能列表aa
	protected List<int> mAutoSkillList = new List<int>();
	
	// 攻击目标aa
	public sdActorInterface target = null;

	// 攻击目标上一次造成伤害的时刻aa
	protected float mLastHurtedTime = 0.0f;
	public float LastHurtedTime
	{
		get { return mLastHurtedTime;}
		set { mLastHurtedTime = value;}	
	}
	
	// 攻击目标的反击优先级aa
	protected EBeatPriority mBeatPriority = EBeatPriority.BP_Unknown;
	public EBeatPriority BeatPriority
	{
		get { return mBeatPriority;}
		set { mBeatPriority = value;}	
	}
	
	//
	public override	void Begin(AutoFight kAutoFight)
	{
		mMoveState.target = target;
		mMoveState.MinDistance = kAutoFight.Actor.getRadius() + target.getRadius();
		mMoveState.Active(true, kAutoFight);

		mSkillState.Target = target;
		mSkillState.SkillID = 0;
		mSkillState.AutoDeactive = false;
		mSkillState.AttackDeltaTime = 0.0f;
		mSkillState.Active(true, kAutoFight);
	}
	
	//
	public override void End(AutoFight kAutoFight)
	{
		target = null;

		mMoveState.target = null;
		mMoveState.Active(false, kAutoFight);

		mSkillState.Target = null;
		mSkillState.Active(false, kAutoFight);
	}
	
	//
	public override	void Update(AutoFight kAutoFight)
	{
		if(!Active() || target == null)
		{
			return;
		}
		
		// 目标死亡或者隐身,则退出攻击状态aa
		if (target.GetCurrentHP() <= 0 || target.Hide)
		{
			target = null;
			mLastHurtedTime = 0.0f;
			mBeatPriority = EBeatPriority.BP_Unknown;
			Active(false, kAutoFight);
				
			kAutoFight.StopMove();
				
			return; 
		}

		// 目标未激活,则保持移动aa
		if (!target.IsActive())
		{
			if (!mMoveState.Active())
				mMoveState.Active(true, kAutoFight);

			mMoveState.Update(kAutoFight);

			return;
		}

        // 当前正在释放技能aa
        sdGameActor kActor = kAutoFight.Actor;

        Vector3 vTargetDir    =   target.transform.position - kActor.transform.position;
        vTargetDir.y= 0.0f;
        vTargetDir.Normalize();

        kActor.TargetFaceDirection = vTargetDir;

		if (kActor.logicTSM.nextState != null)
			return;

		// 计算与目标的距离aa
		float fSkillDistance = kAutoFight.CalcSkillDistance(kActor, target);

		// 检查是否有手动技能需要释放aa
		int iCurrentSkill = 0;
		bool bIsManualSkill = false;
		if (iCurrentSkill == 0 && mManualSkill != 0)
		{
			bIsManualSkill = true;
			iCurrentSkill = mManualSkill;
		}

		// 检查是否有连招需要释放aa
		bool bHasJoinSkill = false;
		if (iCurrentSkill == 0 && kActor.actionStateTimer._enabled)
		{
			bHasJoinSkill = GetJoinedSkill(kAutoFight.Actor, fSkillDistance, ref iCurrentSkill);
		}

		// 检查是否有自动技能需要释放aa
		bool bHasValidAutoSkill = false;
		if (iCurrentSkill == 0)
		{
			bHasValidAutoSkill = GetValidAutoSkill(kAutoFight.Actor, fSkillDistance, ref iCurrentSkill);
		}

		// 处理普通攻击技能需要释放aa
		bool bIsCommonSkill = false;
		if (iCurrentSkill == 0 && mCommonSkill != 0)
		{
			bIsCommonSkill = GetValidCommonSkill(kAutoFight.Actor, fSkillDistance, ref iCurrentSkill);
		}
	
		// 释放技能(移动到技能攻击范围内并释放技能)aa
		if (iCurrentSkill != 0)
		{
			float fCastDistance = 2.0f;
			if (kActor != null)
			{
				if (kActor.skillTree != null)
				{
					sdSkill kSkill = kActor.skillTree.getSkill(iCurrentSkill);
					if (kSkill != null)
					{
						sdBaseState kState = kSkill.actionStateList[0];
						int iCastDistance = (int)kState.stateData["CastDistance"];
						fCastDistance = (iCastDistance) * 0.001f;
					}
				}
			}

			if (fSkillDistance > fCastDistance)
			{
				if (!mMoveState.Active())
					mMoveState.Active(true, kAutoFight);

				mMoveState.Update(kAutoFight);
			}
			else
			{
				if (mMoveState.Active())
					mMoveState.Active(false, kAutoFight);

				mMoveState.Stop(kAutoFight);

				mSkillState.Target = target;
				mSkillState.SkillID = iCurrentSkill;
				mSkillState.AutoDeactive = bIsManualSkill;					//< 手动技能只放一次aa
				mSkillState.AttackDeltaTime = bIsCommonSkill ? 0.2f : 0.0f;	//< 普攻要手动设置时间间隔aa
				if (!mSkillState.Active())
				{
					mSkillState.Active(true, kAutoFight);

					// 手动技能释放成功,清除手动技能aa
					if (iCurrentSkill == mManualSkill)
						mManualSkill = 0;
				}
				mSkillState.Update(kAutoFight);
			}
		}
		else
		{
			// 没有可用技能时,使用战斗跟随距离aa
			if (fSkillDistance > kAutoFight.BattleDistance)
			{
				if (!mMoveState.Active())
					mMoveState.Active(true, kAutoFight);

				mMoveState.Update(kAutoFight);
			}
		}
	}

	// 添加自动技能aa
	public void AddAutoSkill(int iSkillId)
	{
		mAutoSkillList.Add(iSkillId);
	}

	// 检查连招aa
	protected bool GetJoinedSkill(sdGameActor kActor, float fDistance, ref int iValidSkillId)
	{
		sdBaseState kState = kActor.actionStateTimer.targetSkill.GetFirstValidAction();
		if (kState == null)
		{
			iValidSkillId = 0;
			return false;		//< 技能动作信息aa
		}

		if (kActor["SP"] < kState.GetCostSP())
		{
			iValidSkillId = 0;
			return false;		//< 技能消蓝aa
		}

		int iCastDistance = (int)kState.stateData["CastDistance"];
		float fCastDistance = (iCastDistance) * 0.001f;
		if (fCastDistance < fDistance)
		{
			iValidSkillId = kActor.actionStateTimer.targetSkill.id;
			return true;
		}
		else
		{
			iValidSkillId = 0;
			return false;
		}
	}

	// 从自动技能列表中找出技能aa
	//	1.当前距离有可用技能,则返回优先级最高的可释放用技能(冷却完成\耗蓝检查通过\攻击距离足够)aa
	//	2.当前距离无可用技能,则返回优先级最高的可用技能(冷却完成\耗蓝检查通过)aa
	//	3.以上均失败,则返回0aa
    protected bool GetValidAutoSkill(sdGameActor kActor, float fDistance, ref int iValidSkillId)
    {
		int iAvaiableSkillId = 0;
        for (int i = 0; i < mAutoSkillList.Count; i++)
        {
            int iSkillId = mAutoSkillList[i];
            sdSkill kSkill = kActor.skillTree.getSkill(iSkillId);
            if (kSkill == null)
                continue;		//< 技能信息aa

			if (!kSkill.skillEnabled)
				continue;		//< 技能是否可用aa

            if (kSkill.skillState == (int)sdSkill.State.eSS_CoolDown)
                continue;		//< 技能冷却aa

            sdBaseState kState = kSkill.GetFirstValidAction();
            if (kState == null)
                continue;		//< 技能动作信息aa

            if (kActor["SP"] < kState.GetCostSP())
                continue;		//< 技能消蓝aa

			int iCastDistance = (int)kState.stateData["CastDistance"];
			float fCastDistance = (iCastDistance) * 0.001f;
			if (fCastDistance < fDistance)
			{
				iValidSkillId = iSkillId;
				return true;
			}
			else
			{
				iAvaiableSkillId = iSkillId;
			}	
        }

		if (iAvaiableSkillId != 0)
		{
			iValidSkillId = iAvaiableSkillId;
			return false;
		}
		else
		{
			iValidSkillId = 0;
			return false;
		}
    }

	// 判断普通攻击技能是否能够释放aa
	protected bool GetValidCommonSkill(sdGameActor kActor, float fDistance, ref int iValidSkillId)
	{
		iValidSkillId = 0;

		int iCommonSkill = mCommonSkill;
		sdSkill kSkill = kActor.skillTree.getSkill(iCommonSkill);
		if (kSkill == null)
			return false;		//< 技能信息aa

		if (!kSkill.skillEnabled)
			return false;		//< 技能是否可用aa

		if (kSkill.skillState == (int)sdSkill.State.eSS_CoolDown)
			return false;		//< 技能冷却aa

		sdBaseState kState = kSkill.GetFirstValidAction();
		if (kState == null)
			return false;		//< 技能动作信息aa

		if (kActor["SP"] < kState.GetCostSP())
			return false;		//< 技能消蓝aa

		int iCastDistance = (int)kState.stateData["CastDistance"];
		float fCastDistance = (iCastDistance) * 0.001f;
		if (fCastDistance < fDistance)
			return false;

		iValidSkillId = iCommonSkill;
		return true;
	}

	//
    public List<Vector3> GetCurrentPath()
    {
        return mMoveState.GetCurrentPath();
    }
}

/// <summary>
/// 翻滚aa
/// </summary>
public	class RollState :	BehaviourState
{
	public	Vector3	vDir	=	Vector3.zero;
	AnimationState animState = null;
	AutoFight  autofight = null;
	public	override	void	Begin(AutoFight c)
	{
		autofight = c;
		sdGameActor mc = c.Actor;
		sdBaseState	state	=	mc.skillTree.getSkill(1002).actionStateList[0];
		animState	=	mc.AnimController[state.info];
		if(mc!=null)
		{
			c.Actor.TargetFaceDirection = vDir;
            int error = -1;
            
            bool bSuccess = c.Actor.CastSkill(1002, ref error);
			if(bSuccess)
			{
				state.StateEvent += RollEnd;			
				mc._moveVector	=	vDir;
			}
			else
			{
				Active(false, c);
                if (c.Actor == sdGameLevel.instance.mainChar)
                {
                    string msg = string.Format("Error_{0}", error);
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr(msg), Color.yellow);
                }
			}
		}
	}
	public	override	void	End(AutoFight c)
	{
		c.Actor._moveVector	=	Vector3.zero;
        if (c.Actor == sdGameLevel.instance.mainChar)
        {
            FingerControl auto = (FingerControl)c;
            auto.ResearchTarget();
        }
	}
	public	override	void	Update(AutoFight c)
	{
	}

    public void RollEnd(eStateEventType param)
	{
        if (param == eStateEventType.eSET_leave)
		{
			sdBaseState	state	=	autofight.Actor.skillTree.getSkill(1002).actionStateList[0];
			state.StateEvent -= RollEnd;
			Active(false,autofight);
		}
	}
}

/// <summary>
/// 评估和撤退状态aa
///  1.评估危险状态,确认是否撤退aa
///	 2.计算撤退方向,进行一次撤退aa
/// </summary>
public class RetreatState : BehaviourState
{
	// 方向划分aa
	//	1.方向必须是2的倍数aa
	//	2.起始方向作为第一个方向的中心aa
	protected int mPartitionNum = 16;
	protected List<float> mPartitionDirection = new List<float>(16);
	protected List<float> mPartitionEndDot = new List<float>(8);

	// 方向记录aa
	protected List<int> mPartitionSector = new List<int>(16);
	protected List<int> mLeftBlankSector = new List<int>(16);
	protected List<int> mRightBlankSector = new List<int>(16);

	// 主角位置与姿态aa
	protected float fMoveSpeed = 1.0f;
	protected Vector3 mPosition = new Vector3(0.0f, 0.0f, 0.0f);
	protected Vector3 mDirection = new Vector3(1.0f, 0.0f, 0.0f);
	
	// 撤退方向aa
	protected Vector3 mRetreatDirection;

	// 撤退起始时间aa
	protected float mRetreatStartTime = 0.0f;

	//
	public RetreatState()
	{
		// 预计算每个分区的中心方向aa
		float fRange = Mathf.PI * 2.0f / mPartitionNum;
		float fHalfRange = fRange * 0.5f;
		for (int i = 0; i < mPartitionNum; ++i)
		{
			float fAngle = fRange * i;
			mPartitionDirection.Add(fAngle);
		}

		// 预计算正半区每个分区的最小Cos值aa
		for (int i = 0; i < mPartitionNum; ++i)
		{
			float fAngle = fHalfRange + fRange * i;
			mPartitionEndDot.Add(Mathf.Cos(fAngle));
		}
	}

	// 开始(继承自BehaviourState)
	public override void Begin(AutoFight kAutoFight)
	{
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
		{
			Active(false, kAutoFight);
			return;
		}

		// 获取角色姿态aa
		sdActorInterface kActor = kAutoFight.Actor;
		if (kActor != null)
		{
			fMoveSpeed = kActor.GetMoveSpeed();

			mPosition = kActor.transform.position;

			mDirection = kActor.GetDirection();
			mDirection.y = 0.0f;
			mDirection.Normalize();
		}

		// 清空数据aa
		mPartitionSector.Clear();
		mLeftBlankSector.Clear();
		mRightBlankSector.Clear();
		for (int i = 0; i < mPartitionNum; ++i)
		{
			mPartitionSector.Add(0);
			mLeftBlankSector.Add(0);
			mRightBlankSector.Add(0);
		}

		// 获取周围的敌方角色aa
		List<sdActorInterface> kEnemyList = kAutoFight.FindEnemy(8.0f);
		if (kEnemyList == null)
		{
			Active(false, kAutoFight);
			return;
		}

		// 评估是否需要撤退aa
		bool bNeedRetreat = false;
		foreach (sdActorInterface kEnemy in kEnemyList)
		{
			if (kAutoFight.CalcDistance(kActor, kEnemy) < kPetAutoFight.RetreatDetectMinDistance)
			{
				bNeedRetreat = true;
				break;
			}
		}

		if (!bNeedRetreat)
		{
			Active(false, kAutoFight);
			return;
		}

		// 初始化敌方位置aa
		foreach (sdActorInterface kEnemy in kEnemyList)
		{
			AddEnemy(kEnemy);
		}

		// 计算撤退方向aa
		Vector3 kRetreatDirection = new Vector3();
		if (!CalcRetreatDirection(ref kRetreatDirection, kAutoFight))
		{
			Active(false, kAutoFight);
			return;
		}

		mRetreatDirection = kRetreatDirection;
		mRetreatStartTime = Time.time;

		//Debug.Log("Start Retreat!!");
	}

	// 结束(继承自BehaviourState)
	public override void End(AutoFight kAutoFight)
	{
		
	}

	// 更新(继承自BehaviourState)
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;

		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return;

		// 撤退时间结束aa
		if (Time.time - mRetreatStartTime > kPetAutoFight.RetreatElapseTime)
		{
//			Debug.Log("End Retreat!!");
			Active(false, kAutoFight);
			return;
		}

		// 撤退aa
		sdGameActor kActor = kAutoFight.Actor;
		kActor._moveVector = mRetreatDirection;
	}

	// 添加敌人aa
	protected void AddEnemy(sdActorInterface kEnemy)
	{
		if (kEnemy == null)
			return;

		Vector3 kEnemyPosition = kEnemy.transform.position;
		Vector3 kEnemyDirection = kEnemyPosition - mPosition;
		kEnemyDirection.y = 0.0f;
		kEnemyDirection.Normalize();

		float fDot = Vector3.Dot(mDirection, kEnemyDirection);
		int iHalfPartionNum = mPartitionNum / 2;
		int iPartionIndex = -1;
		for (int i = 0; i < iHalfPartionNum - 1; ++i)
		{
			if (fDot > mPartitionEndDot[i])
			{
				iPartionIndex = i;
				break;
			}
		}

		if (iPartionIndex == -1)
		{
			iPartionIndex = iHalfPartionNum;
		}

		Vector3 kCross = Vector3.Cross(mDirection, kEnemyDirection);
		if (kCross.z < 0.0f)
		{
			iPartionIndex = mPartitionNum - iPartionIndex;
			if (iPartionIndex >= mPartitionNum)
				iPartionIndex -= mPartitionNum;
		}

		mPartitionSector[iPartionIndex] = 1;
	}

	// 获取最可行的撤退方向(考虑物理)aa
	protected bool CalcRetreatDirection(ref Vector3 kDirection, AutoFight kAutoFight)
	{
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return false;

		for (;;)
		{
			int iRetreatIndex = CalcRetreatDirection();
			if (iRetreatIndex == -1)
				return false;

			float fRetreatAngle = mPartitionDirection[iRetreatIndex];
			float fRetreatAngleCos = Mathf.Cos(fRetreatAngle);
			float fRetreatAngleSin = Mathf.Sin(fRetreatAngle);
			Vector3 kRetreatDirection = new Vector3(
				mDirection.x * fRetreatAngleCos - mDirection.z * fRetreatAngleSin, 
				0.0f, 
				mDirection.x * fRetreatAngleSin + mDirection.z * fRetreatAngleCos);
			
			float fMaxRetreatDistance = fMoveSpeed * kPetAutoFight.RetreatElapseTime;
			Ray kRay = new Ray(mPosition, kRetreatDirection);
			if (!Physics.Raycast(kRay, fMaxRetreatDistance))
			{
				kDirection = kRetreatDirection;
				return true;
			}
			else
			{
				mPartitionSector[iRetreatIndex] = 2;
			}

			kDirection = kRetreatDirection;
			return true;
		}
	}

	// 获取最可行的撤退方向(不考虑物理)aa
	protected int CalcRetreatDirection()
	{
		// 找到第一个有怪存在的方向aa
		int iStartIndex = -1;
		for (int i = 0; i < mPartitionNum; ++i)
		{
			if (mPartitionSector[i] != 0)
			{
				iStartIndex = i;
				break;
			}
		}

		if (iStartIndex == -1)
			return -1;

		// 从左向右开始计数,计算到左侧阻挡区域的距离aa
		mLeftBlankSector[iStartIndex] = 0;
		for (int i = 0; i < mPartitionNum; ++i)
		{
			int iIndex = iStartIndex + i;
			if (iIndex >= mPartitionNum)
				iIndex -= mPartitionNum;

			if (mPartitionSector[iIndex] != 0)
			{
				mLeftBlankSector[iIndex] = 0;
			}
			else
			{
				int iLeftIndex = iIndex - 1;
				if (iLeftIndex < 0)
					iLeftIndex += mPartitionNum;

				mLeftBlankSector[iIndex] = mLeftBlankSector[iLeftIndex] + 1;
			}
		}

		// 从右向左开始计数,计算到右侧阻挡区域的距离aa
		mRightBlankSector[iStartIndex] = 0;
		for (int i = 0; i < mPartitionNum; ++i)
		{
			int iIndex = iStartIndex - i;
			if (iIndex < 0)
				iIndex += mPartitionNum;

			if (mPartitionSector[iIndex] != 0)
			{
				mRightBlankSector[iIndex] = 0;
			}
			else
			{
				int iRightIndex = iIndex + 1;
				if (iRightIndex >= mPartitionNum)
					iRightIndex -= mPartitionNum;

				mRightBlankSector[iIndex] = mRightBlankSector[iRightIndex] + 1;
			}
		}

		// 取出到左侧和右侧距离均较大的方向aa
		int iRetreatIndex = -1;
		int iMaxTotalBlankNum = -1;
		int iMinDeltaBlankNum = mPartitionNum;
		for (int i = 0; i < mPartitionNum; ++i)
		{
			if (mPartitionSector[i] != 0)
				continue;

			int iTotalBlankNum = mLeftBlankSector[i] + mRightBlankSector[i];
			int iDeltaBlankNum = Mathf.Abs(mLeftBlankSector[i] - mRightBlankSector[i]);
			if (iTotalBlankNum > iMaxTotalBlankNum)
			{
				iRetreatIndex = i;
				iMaxTotalBlankNum = iTotalBlankNum;
				iMinDeltaBlankNum = iDeltaBlankNum;
			}
			else if (iTotalBlankNum == iMaxTotalBlankNum)
			{
				if (iDeltaBlankNum < iMinDeltaBlankNum)
				{
					iRetreatIndex = i;
					iMaxTotalBlankNum = iTotalBlankNum;
					iMinDeltaBlankNum = iDeltaBlankNum;
				}
			}
		}

		return iRetreatIndex;
	}
}

/// <summary>
/// 自动战斗系统aa
/// </summary>
public class AutoState : BehaviourState
{
	// 自动战斗状态aa
	protected TargetState mTargetState = new TargetState();

	// 重新搜索目标时间aa
	protected float mAccumResearchTime = 0.0f;

	// 更新(继承自BehaviourState)
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;

		// 更新重新搜索时间aa
		mAccumResearchTime += Time.deltaTime;

		// 自动战斗状态aa
		if (mTargetState.Active())
		{
			sdActorInterface kPreviousTarget = mTargetState.target;	//< 放在Update前面是防止在自动退出状态时被清掉aa
			mTargetState.Update(kAutoFight);

			if (!mTargetState.Active())
			{
				sdActorInterface kTarget = kAutoFight.FindNearestEnemy(10000.0f, true);
				if (kTarget != null)
				{
					bool bChangeTarget = true;
					if (mTargetState.Active())
						bChangeTarget = (kPreviousTarget != kTarget);//< 定时自动切换目标则检查是否同一个目标aa

					if (bChangeTarget)
					{
						mTargetState.target = kTarget;
						mTargetState.LastHurtedTime = Time.time;
						mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
						mTargetState.Active(true, kAutoFight);

						kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
					}
				}
				else
				{
					mTargetState.target = null;
					mTargetState.LastHurtedTime = 0.0f;
					mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
					mTargetState.Active(false, kAutoFight);

					kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
				}
			}

			return;
		}

		// 每0.5s重新查找攻击目标aa
		{
			sdActorInterface kTarget = null;
			if (mAccumResearchTime > 0.5f)
			{
				mAccumResearchTime = 0.0f;
				kTarget = kAutoFight.FindNearestEnemy(10000.0f, true);
			}

			if (kTarget != null)
			{
				sdActorInterface kPreviousTarget = null;

				mTargetState.target = kTarget;
				mTargetState.LastHurtedTime = Time.time;
				mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
				mTargetState.Active(true, kAutoFight);

				kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

				return;
			}
		}
	}

	// 获取当前攻击目标aa
	public virtual sdActorInterface GetAttackTarget()
	{
		if (mTargetState.Active())
			return mTargetState.target;

		return null;
	}
	
	// 伤害回调函数aa
	public virtual void OnHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{
		// 战斗状态受到的伤害来当前攻击目标,则记录攻击目标本次造成伤害的时刻aa
		if (mTargetState.Active())
		{
			if (mTargetState.target == kAttacker)
				mTargetState.LastHurtedTime = Time.time;	
		}
	}
	
	// 队友受到伤害回调函数aa
	public virtual void OnFriendHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{

	}

	// 得到当前寻路路径aa
    public List<Vector3> GetCurrentPath()
    {
        return mTargetState.GetCurrentPath();
    }
}

/// <summary>
/// 怪物自动战斗系统aa
/// </summary>
public class MonsterAutoState : AutoState
{
	// 更新(继承自BehaviourState)
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;
		
		MonsterAutoFight kMonsterAutoFight = kAutoFight as MonsterAutoFight;
		if (kMonsterAutoFight == null)
			return;

		// 更新重新搜索时间aa
		mAccumResearchTime += Time.deltaTime;

		// 自动战斗状态aa
		//	1.自动退出战斗状态,或者3秒不受伤,则使用追击距离重新选择攻击目标aa
		//	2.找到新的攻击目标,且攻击目标与援目标不同则切换目标,否则继续攻击当前目标aa
		//	3.没有找到新的攻击目标,则自动退出战斗,发出切换目标通知aa	
		if (mTargetState.Active())
		{
			sdActorInterface kPreviousTarget = mTargetState.target;	//< 放在Update前面是防止在自动退出状态时被清掉aa
			mTargetState.Update(kAutoFight);

			if (!mTargetState.Active() || Time.time - mTargetState.LastHurtedTime > 3.0f)
			{
				sdActorInterface kTarget = kAutoFight.FindNearestEnemy(kMonsterAutoFight.ChaseDistance, true);
				if (kTarget != null)
				{
					bool bChangeTarget = true;
					if (mTargetState.Active())
						bChangeTarget = (kPreviousTarget != kTarget);//< 定时自动切换目标则检查是否同一个目标aa

					if (bChangeTarget)
					{
						mTargetState.target = kTarget;
						mTargetState.LastHurtedTime = Time.time;
						mTargetState.BeatPriority = GetBeatPriority(kTarget);
						mTargetState.Active(true, kAutoFight);

						kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
					}
				}
				else
				{
					mTargetState.target = null;
					mTargetState.LastHurtedTime = 0.0f;
					mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
					mTargetState.Active(false, kAutoFight);

					kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
				}
			}

			return;
		}

		// 每0.5s重新查找攻击目标aa
		{
			sdActorInterface kTarget = null;
			if (mAccumResearchTime > 0.5f)
			{
				mAccumResearchTime = 0.0f;
				kTarget = kAutoFight.FindNearestEnemy(kMonsterAutoFight.EyeDistance, true);
			}
			
			if (kTarget != null) 
			{
				sdActorInterface kPreviousTarget = null;
				
				mTargetState.target = kTarget;
				mTargetState.LastHurtedTime = Time.time;
				mTargetState.BeatPriority = GetBeatPriority(kTarget);
				mTargetState.Active(true, kAutoFight);
				
				kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

				return;
			}
		}
	}

	// 伤害回调函数(继承自AutoState)aa
	public override void OnHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{
		MonsterAutoFight kMonsterAutoFight = kAutoFight as MonsterAutoFight;
		if (kMonsterAutoFight == null)
			return;
		
		// 不处理伤害者是自己aa
		if (kAttacker == kMonsterAutoFight.Monster)
			return;		
		
		// 不处理伤害者是己方阵营aa
		if (kAttacker.GetGroupID() == kMonsterAutoFight.Monster.GetGroupID())
			return;
		
		// 1.战斗状态aa
		//	a.收到的伤害来自更高优先级的目标,则改变攻击目标aa
		//	b.受到的伤害来当前攻击目标,则记录攻击目标本次造成伤害的时刻aa
		// 2.非战斗状态
		//	a.被攻击则直接进入自动战斗状态aa
		if (mTargetState.Active())
		{
			EBeatPriority kBeatPriority = GetBeatPriority(kAttacker);
			if (kBeatPriority < mTargetState.BeatPriority)
			{
				sdActorInterface kPreviousTarget = mTargetState.target;
				
				mTargetState.target = kAttacker;
				mTargetState.LastHurtedTime = Time.time;
				mTargetState.BeatPriority = kBeatPriority;
				mTargetState.Active(true, kAutoFight); 	
				
				kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
			}
			else
			{
				if (mTargetState.target == kAttacker)
					mTargetState.LastHurtedTime = Time.time;	
			}
		}
		else
		{
			sdActorInterface kPreviousTarget = null;
			
			mTargetState.target = kAttacker;
			mTargetState.LastHurtedTime = Time.time;
			mTargetState.BeatPriority = GetBeatPriority(kAttacker);
			mTargetState.Active(true, kAutoFight);
			
			kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
		}
	}

	// 队友受到伤害回调函数(继承自AutoState)aa
	public override void OnFriendHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{
		MonsterAutoFight kMonsterAutoFight = kAutoFight as MonsterAutoFight;
		if (kMonsterAutoFight == null)
			return;

		// 不处理伤害者是自己aa
		if (kAttacker == kMonsterAutoFight.Monster)
			return;

		// 不处理伤害者是己方阵营aa
		if (kAttacker.GetGroupID() == kMonsterAutoFight.Monster.GetGroupID())
			return;

		// 1.战斗状态aa
		// 2.非战斗状态
		//	a.非战斗跟随状态则被攻击则直接进入自动战斗状态aa
		if (mTargetState.Active())
		{

		}
		else
		{
			sdActorInterface kPreviousTarget = null;

			mTargetState.target = kAttacker;
			mTargetState.LastHurtedTime = Time.time;
			mTargetState.BeatPriority = GetBeatPriority(kAttacker);
			mTargetState.Active(true, kAutoFight);

			kMonsterAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
		}
	}
	
	// 获取反击优先级aa
	protected EBeatPriority GetBeatPriority(sdActorInterface kAttacker)
	{
		if (kAttacker.GetActorType() == ActorType.AT_Player)
		{
			return EBeatPriority.BP_Middle;	
		}
		else if (kAttacker.GetActorType() == ActorType.AT_Monster || kAttacker.GetActorType() == ActorType.AT_Pet)
		{
			sdGameMonster kMonster = kAttacker as sdGameMonster;
			return 	kMonster.GetBeatPriority();
		}
		else
		{
			return EBeatPriority.BP_Unknown;	
		}
	}
}

// 攻击记录aa
public class AttackRecord
{
	public sdActorInterface mAttacker = null;
	public float mTime = 0.0f;
}

/// <summary>
/// 宠物自动战斗系统aa
/// </summary>
public class PetAutoState : MonsterAutoState
{
	// 自动跟随aa
	protected MoveTargetState mMoveTarget = new MoveTargetState();
	
	// 战斗自动跟随aa
	protected MoveTargetState mBattleMoveTarget = new MoveTargetState();
	
	// 手动技能移动aa
	protected MoveTargetState mXPSkillTarget = new MoveTargetState();

	// 撤退aa
	protected float mLastRetreatDetectionTime = 0.0f;
	protected RetreatState mRetreatState = new RetreatState(); 

	// 当前攻击列表aa
	protected List<AttackRecord> mAttackRecord = new List<AttackRecord>();
	
	// 手动技能IDaa
	protected int mXPSkillId = -1;

	// 更新(继承自BehaviourState)
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;
		
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return;

		// 更新重新搜索时间aa
		mAccumResearchTime += Time.deltaTime;
		
		// 更新攻击列表aa
		UpdateAttackList(kAutoFight);

		// 手动技能状态:更新状态,,如果到达目标则恢复碰撞并施放手动技能aa
		bool bForceResearchTarget = false;
		if (mXPSkillTarget.Active())
		{
			mXPSkillTarget.Update(kPetAutoFight);
			if (!mXPSkillTarget.Active())
			{
				sdGameMonster kPetMonster = kPetAutoFight.Monster;
				kPetMonster.AvtiveCollisionWithMonster(false);	//< 恢复宠物与怪物的碰撞aa
				kPetMonster.CastSkill(mXPSkillId);				//< 施放手动技能aa

				bForceResearchTarget = true;
			}
			else
			{
				return;
			}
		}

		// 战斗跟随状态:更新状态,如果到达目标则恢复碰撞aa
		if (mBattleMoveTarget.Active())
		{
			mBattleMoveTarget.Update(kPetAutoFight);
			if (!mBattleMoveTarget.Active())
			{
				sdGameMonster kPetMonster = kPetAutoFight.Monster;
				kPetMonster.AvtiveCollisionWithMonster(false);	//< 恢复宠物与怪物的碰撞aa

				bForceResearchTarget = true;
			}
			else
			{
				return;
			}
		}

		// 撤退状态:评估战场情况,如果有需要,进行自动撤退aa
		sdActorInterface kPreviousTarget = mTargetState.target;
		if (kPetAutoFight.AutoRetreat)
		{
			if (!mRetreatState.Active() && Time.time - mLastRetreatDetectionTime > kPetAutoFight.RetreatDetectInterval)
			{
				mLastRetreatDetectionTime = Time.time;
				mRetreatState.Active(true, kAutoFight);

				if (mRetreatState.Active())
				{
					if (mTargetState.Active())
					{
						mTargetState.target = null;
						mTargetState.LastHurtedTime = 0.0f;
						mTargetState.BeatPriority = EBeatPriority.BP_Unknown; ;
						mTargetState.Active(false, kPetAutoFight);					//< 强制取消战斗状态aa

						kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
					}
				}
			}
			
			if (mRetreatState.Active())
			{
				mRetreatState.Update(kAutoFight);
				if (!mTargetState.Active())
				{
					bForceResearchTarget = true;
				}
				else
				{
					return;
				}
			}
		}

		// 自动战斗状态:
		//	1.若超出战斗跟随距离,取消战斗状态,进入战斗跟随状态aa
		//	2.更新战斗状态,若是自动退出战斗状态,则设置切换目标标记aa
		bool bIsChangingTarget = false;
		sdGameActor kPet = kPetAutoFight.Actor;
		sdGameActor kMaster = kPet.Master as sdGameActor;
		if (mTargetState.Active())
		{
			if (kAutoFight.CalcDistance(kMaster, kPet) > kPetAutoFight.BattleFollowDistance)
			{
				mBattleMoveTarget.target = kPetAutoFight.Actor.Master;
				mBattleMoveTarget.AutoDeactive = true;
				mBattleMoveTarget.ReachType = EReachType.RT_ByDistance;
				mBattleMoveTarget.MinDistance = kPetAutoFight.FollowDistance;
				mBattleMoveTarget.Active(true, kPetAutoFight);

				kPetAutoFight.Monster.AvtiveCollisionWithMonster(false);	//< 取消宠物与怪物的碰撞aa

				mTargetState.target = null;
				mTargetState.LastHurtedTime = 0.0f;
				mTargetState.BeatPriority = EBeatPriority.BP_Unknown; ;
				mTargetState.Active(false, kPetAutoFight);					//< 强制取消战斗状态aa

				kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

				return;
			}
			
			mTargetState.Update(kAutoFight);
			if (!mTargetState.Active())
			{
				bForceResearchTarget = true;
				bIsChangingTarget = true;
			}
			else
			{
				return;
			}
		}
		
		// 检查攻击列表,存在并且在战斗范围内,则成为自己的攻击目标aa
		sdActorInterface kAttackTarget = null;
		if (kAttackTarget == null)
		{
			kAttackTarget = GetAttackTargetFromListByDistance(kAutoFight);
		}

		// 检查视野范围内的攻击目标aa
		if (kAttackTarget == null)
		{
			if (mAccumResearchTime > 0.5f || bIsChangingTarget || bForceResearchTarget)
			{
				mAccumResearchTime = 0.0f;

				float fSearchDistance = kPetAutoFight.EyeDistance;
				if (bIsChangingTarget)
					fSearchDistance *= 2.0f;

				sdActorInterface kTarget = kAutoFight.FindNearestEnemy(fSearchDistance, true);
				if (CanBeSetAsAttackTarget(kTarget, kAutoFight))
				{
					kAttackTarget = kTarget;
				}
			}
		}

		// 检查主人的攻击目标,存在并且在战斗范围内,则成为自己的攻击目标aa
		if (kAttackTarget == null)
		{
			if (kMaster != null)
			{
				AutoFight kMasterAutoFight = kMaster.AutoFightSystem;
				if (kMasterAutoFight != null)
				{
					sdActorInterface kTarget = kMasterAutoFight.GetAttackTarget();
					if (CanBeSetAsAttackTarget(kAttackTarget, kAutoFight))
					{
						kAttackTarget = kTarget;
					}
				}
			}
		}

		// 找到攻击目标,则切换攻击目标aa
		if (kAttackTarget != null)
		{
			mTargetState.target = kAttackTarget;
			mTargetState.LastHurtedTime = Time.time;
			mTargetState.BeatPriority = GetBeatPriority(kAttackTarget);
			mTargetState.Active(true, kAutoFight); 		//< 进入战斗状态aa

			if (!bIsChangingTarget)
				kPreviousTarget = null;					//< 非切换目标时,前一目标为空aa

			kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

			mMoveTarget.target = null;
			mMoveTarget.Active(false, kAutoFight);		//< 强制取消自动跟随状态aa

			return;
		}

		// 如果在切换目标时没有找到目标aa
		if (bIsChangingTarget)
		{
			kPetAutoFight.OnChangeTarget(kPreviousTarget, null);
		}

		// 自动跟随主角aa
		if (!mMoveTarget.Active())
		{	
			mMoveTarget.target = kPetAutoFight.Actor.Master;
			mMoveTarget.ReachType = EReachType.RT_ByDistance;
			mMoveTarget.MinDistance = kPetAutoFight.FollowDistance;
			mMoveTarget.Active(true, kPetAutoFight);
		}
		
		// 自动跟随状态:更新并返回aa
		if (mMoveTarget.Active())
		{
			mMoveTarget.Update(kPetAutoFight);	
			return;
		}
	}
	
	// 伤害回调函数(继承自AutoState)aa
	public override void OnHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return;
		
		// 不处理伤害者是自己aa
		if (kAttacker == kPetAutoFight.Monster)
			return;		
		
		// 不处理伤害者是主角aa
		if (kAttacker == kPetAutoFight.Monster.Master)
			return;	
		
		// 不处理伤害者是己方阵营aa
		if (kAttacker.GetGroupID() == kPetAutoFight.Monster.GetGroupID())
			return;
		
		// 1.战斗状态aa
		//	a.收到的伤害来自更高优先级的目标,则改变攻击目标aa
		//	b.受到的伤害来当前攻击目标,则记录攻击目标本次造成伤害的时刻aa
		// 2.非战斗状态
		//	a.非战斗跟随状态则被攻击则直接进入自动战斗状态aa
		if (mTargetState.Active())
		{
			EBeatPriority kBeatPriority = GetBeatPriority(kAttacker);
			if (kBeatPriority < mTargetState.BeatPriority)
			{
				sdActorInterface kPreviousTarget = mTargetState.target;
				
				mTargetState.target = kAttacker;
				mTargetState.LastHurtedTime = Time.time;	
				mTargetState.BeatPriority = kBeatPriority;
				mTargetState.Active(true, kAutoFight); 	
				
				kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
			}
			else
			{
				if (mTargetState.target == kAttacker)
					mTargetState.LastHurtedTime = Time.time;	
			}
		}
		else
		{
			if (!mBattleMoveTarget.Active() && !mXPSkillTarget.Active() && !mRetreatState.Active())
			{
				if (CanBeSetAsAttackTarget(kAttacker, kAutoFight))
				{
					sdActorInterface kPreviousTarget = null;

					mTargetState.target = kAttacker;
					mTargetState.LastHurtedTime = Time.time;
					mTargetState.BeatPriority = GetBeatPriority(kAttacker);
					mTargetState.Active(true, kAutoFight); 		//< 进入战斗状态aa

					kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

					mMoveTarget.target = null;
					mMoveTarget.Active(false, kAutoFight);		//< 强制取消自动跟随状态aa
				}
			}
		}
		
		// 将攻击者加入攻击列表aa
		AddToAttackList(kAttacker);
	}

	// 队友受到伤害回调函数(继承自AutoState)aa
	public override void OnFriendHurt(sdActorInterface kAttacker, AutoFight kAutoFight)
	{
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return;

		// 不处理伤害者是自己aa
		if (kAttacker == kPetAutoFight.Monster)
			return;

		// 不处理伤害者是主角aa
		if (kAttacker == kPetAutoFight.Monster.Master)
			return;

		// 1.战斗状态aa
		// 2.非战斗状态
		//	a.非战斗跟随状态则被攻击则直接进入自动战斗状态aa
		if (mTargetState.Active())
		{

		}
		else
		{
			if (!mBattleMoveTarget.Active() && !mXPSkillTarget.Active() && !mRetreatState.Active())
			{
				if (CanBeSetAsAttackTarget(kAttacker, kAutoFight))
				{
					sdActorInterface kPreviousTarget = null;

					mTargetState.target = kAttacker;
					mTargetState.LastHurtedTime = Time.time;
					mTargetState.BeatPriority = GetBeatPriority(kAttacker);
					mTargetState.Active(true, kAutoFight); 		//< 进入战斗状态aa

					kPetAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

					mMoveTarget.target = null;
					mMoveTarget.Active(false, kAutoFight);		//< 强制取消自动跟随状态aa
				}
			}
		}
	}

	// 判断是否可以被设置为攻击目标aa
	protected bool CanBeSetAsAttackTarget(sdActorInterface kActor, AutoFight kAutoFight)
	{
		if (kActor == null)
			return false;

		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return false;

		sdGameActor kPet = kPetAutoFight.Actor;
		sdGameActor kMaster = kPet.Master as sdGameActor;
		if (kMaster == null)
			return false;

		if (!kAutoFight.CanBeAttacked(kActor))
			return false;

		if (kAutoFight.CalcDistance(kMaster, kActor) > kPetAutoFight.BattleFollowDistance)
			return false;

		return true;
	}

	// 更新攻击列表aa
	protected float mLastAttackListUpdateTime = 0.0f;	//< 减少更新频率aa
	protected void UpdateAttackList(AutoFight kAutoFight)
	{
		if (Time.time - mLastAttackListUpdateTime > 0.2f)
		{
			mLastAttackListUpdateTime = Time.time;

			foreach (AttackRecord kRecord in mAttackRecord)
			{
				if (kRecord.mAttacker == null)
					continue;

				if (kRecord.mTime - Time.time > 3.0f || kAutoFight.CanBeAttacked(kRecord.mAttacker))
				{
					kRecord.mAttacker = null;	//< 3s没受到攻击或者不可被攻击则踢出攻击列表aa
					kRecord.mTime = 0.0f;
				}
			}
		}
	}
	
	// 添加到攻击列表aa
	protected void AddToAttackList(sdActorInterface kActor)
	{
		foreach (AttackRecord kRecord in mAttackRecord)
		{
			if (kRecord == null)
				continue;
			
			if (kRecord.mAttacker == kActor)
			{
				kRecord.mTime = Time.time;	//< 已经存在则更新被攻击时间aa
				return;
			}
		}
		
		foreach (AttackRecord kRecord in mAttackRecord)
		{
			if (kRecord.mAttacker == null)
			{
				kRecord.mAttacker = kActor;	//< 不存在则加入空位并记录被攻击时间aa
				kRecord.mTime = Time.time;
				return;
			}
		}
		
		if (mAttackRecord.Count < 5)
		{
			AttackRecord kRecord = new AttackRecord();
			kRecord.mAttacker = kActor;
			kRecord.mTime = Time.time;
			
			mAttackRecord.Add(kRecord);
		}
		else
		{
			AttackRecord kMaxAttackRecord = null;
			foreach (AttackRecord kRecord in mAttackRecord)
			{
				if (kRecord.mAttacker == null)
					continue;
				
				if (kMaxAttackRecord == null)
				{
					kMaxAttackRecord = kRecord;	
				}
				else
				{
					if (kRecord.mTime < kMaxAttackRecord.mTime)	
					{
						kMaxAttackRecord = kRecord;	
					}
				}
			}
			
			if (kMaxAttackRecord != null)
			{
				kMaxAttackRecord.mAttacker = kActor;
				kMaxAttackRecord.mTime = Time.time;	
			}
		}
	}
	
	// 从攻击列表获取下一个攻击目标(选取最近攻击自己的作为攻击目标)aa
	protected sdActorInterface GetAttackTargetFromListByTime(AutoFight kAutoFight)
	{
		AttackRecord kMinAttackRecord = null;
		foreach (AttackRecord kRecord in mAttackRecord)
		{
			if (kRecord.mAttacker == null)
				continue;
			
			if (kMinAttackRecord == null)
			{
				if (CanBeSetAsAttackTarget(kRecord.mAttacker, kAutoFight))
				{
					kMinAttackRecord = kRecord;	
				}
			}
			else
			{
				if (kRecord.mTime > kMinAttackRecord.mTime && CanBeSetAsAttackTarget(kRecord.mAttacker, kAutoFight))
				{
					kMinAttackRecord = kRecord;	
				}
			}
		}	
		
		if (kMinAttackRecord == null)
			return null;
	
		return kMinAttackRecord.mAttacker;
	}

	// 从攻击列表获取下一个攻击目标(选取最近距离自己最近的作为攻击目标)aa
	protected sdActorInterface GetAttackTargetFromListByDistance(AutoFight kAutoFight)
	{
		float fMinAttackDistance = 0.0f;
		AttackRecord kMinAttackRecord = null;
		foreach (AttackRecord kRecord in mAttackRecord)
		{
			if (kRecord.mAttacker == null)
				continue;

			if (kMinAttackRecord == null)
			{
				if (CanBeSetAsAttackTarget(kRecord.mAttacker, kAutoFight))
				{
					kMinAttackRecord = kRecord;
					fMinAttackDistance = kAutoFight.CalcDistance(kAutoFight.Actor, kRecord.mAttacker);
				}
			}
			else
			{
				float fDistance = kAutoFight.CalcDistance(kAutoFight.Actor, kRecord.mAttacker);
				if (fDistance < fMinAttackDistance && CanBeSetAsAttackTarget(kRecord.mAttacker, kAutoFight))
				{
					kMinAttackRecord = kRecord;
				}
			}
		}

		if (kMinAttackRecord == null)
			return null;

		return kMinAttackRecord.mAttacker;
	}
	
	// 宠物手动技能aa
	public bool DoXPSkill(int iSkillID, AutoFight kAutoFight, ref int iErrorCode)
	{
		if (mXPSkillTarget.Active())
			return false;
		
		PetAutoFight kPetAutoFight = kAutoFight as PetAutoFight;
		if (kPetAutoFight == null)
			return false;
		
		sdGameMonster kPet = kPetAutoFight.Monster;
		if (kPet == null || kPet.skillTree == null)
			return false;

		// 检查技能aa
		sdSkill kSkill = kPet.skillTree.getSkill(iSkillID);
		if (kSkill == null)
			return false;

		// 检查技能CDaa
		if (kSkill.skillState == (int)sdSkill.State.eSS_CoolDown)
			return false;

		// 检查剩余SPaa
		if (!kSkill.CheckSP(kPet))
			return false;

		// 技能范围与目标aa
		sdBaseState	kState = kSkill.actionStateList[0];
		float fDistance = (int)kState.stateData["CastDistance"] * 0.001f;
		HeaderProto.ESkillObjType eTergetType = (HeaderProto.ESkillObjType)kState.stateData["byTargetType"];

		sdActorInterface kTarget = null;
		if (eTergetType == HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY)
			kTarget = kPetAutoFight.FindNearestEnemy(8.0f, true);
		else
			kTarget = kPetAutoFight.Actor.Master;
		
		// 手动技能aa
		if (kTarget != null) 
		{
			mXPSkillTarget.target = kTarget;
			mXPSkillTarget.AutoDeactive = true;
			mXPSkillTarget.ReachType = EReachType.RT_ByDistance;
			mXPSkillTarget.MinDistance = fDistance;
			mXPSkillTarget.Active(true, kPetAutoFight);
			
			mXPSkillId = iSkillID;		//< 保存手动技能IDaa
			
			kPetAutoFight.Monster.AvtiveCollisionWithMonster(false);	//< 取消宠物与怪物的碰撞aa

			return true;
		}
		else
		{
			iErrorCode = 101;
			return false;
		}
	}
}

/// <summary>
/// 主角自动战斗系统aa
/// </summary>
public class PlayerAutoState : AutoState
{
	// 手动移动aa
	protected MovePointState mMovePoint = new MovePointState();	

	// 手动翻滚状态aa
	protected RollState mRollState = new RollState();
	
	// 搜索半径aa
	protected float mResearchDis = 5.0f;
	public float ResearchDistance
	{
		get { return mResearchDis;}
		set { mResearchDis = value;}
	}

	// 是否自动搜索非激活目标aa
	protected bool mSearchInactiveTarget = false;
	public bool SearchInactiveTarget
	{
		get { return mSearchInactiveTarget;}
		set { mSearchInactiveTarget = value;}
	}

	//
	public override void Begin(AutoFight kAutoFight)
	{
		
	}
	
	//
	public override void End(AutoFight kAutoFight)
	{
		mTargetState.Active(false, kAutoFight);
	}
	
	// 更新(继承自BehaviourState)
	public override	void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;

		FingerControl kFingerControl = kAutoFight as FingerControl;
		if (kFingerControl == null)
			return;
		
		// 更新重新搜索时间aa
		mAccumResearchTime += Time.deltaTime;
		
		// 手动翻滚状态处理aa
		if (mRollState.Active())
		{
			mRollState.Update(kAutoFight);
			return;
		}
		
		// 手动移动状态处理aa
		if (mMovePoint.Active())
		{
			if (mTargetState.ManualSkill == 0)
			{
				mMovePoint.Update(kAutoFight);
				
				// 如果已经抵达目的地,则不返回，需要执行后面的逻辑..
				if(mMovePoint.Active())
				{
					return;
				}
				else
				{
					//点击移动结束之后,需要立刻搜索敌人..
					mAccumResearchTime	= 3.0f;
				}
			}
			else
			{
				mMovePoint.Active(false, kAutoFight);
			}
		}

		// 1.自动战斗处理aa
		//	a.更新战斗状态aa
		//	b.若是自动退出战斗状态或者战斗时间超过2秒,则设置切换目标标记,否则直接返回aa
		// 2.非自动战斗状态aa
		//	a.每间隔0.5s设置切换目标标记aa
		bool kNeedSearchTarget = false;
		bool kIsChangingTarget = false;
		bool kIsRoutineSearch = false;
		sdActorInterface kPreviousTarget = mTargetState.target;
		if (mTargetState.Active())
		{
			mTargetState.Update(kAutoFight);
			if (mTargetState.Active() && mAccumResearchTime <= 2.0f)
				return;						//< 在2s以内的自动战斗状态aa

			if (mAccumResearchTime > 2.0f)
				kIsRoutineSearch = true;	//< 常规重新搜索目标aa

			mAccumResearchTime = 0.0f;
			kNeedSearchTarget = true;
			kIsChangingTarget = true;
		}
		else
		{
			if (mAccumResearchTime > 0.5f)
			{
				mAccumResearchTime = 0.0f;
				kNeedSearchTarget = true;
			}
		}
		
		// 使用搜索半径重新选择攻击目标aa
		sdActorInterface kTarget = null;
		if (kNeedSearchTarget)
		{
			kTarget = sdGameLevel.instance.actorMgr.FindNearestActorActiveFirst(
				kAutoFight.Actor,
				HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
				kAutoFight.Actor.transform.position,
				new Vector3(0, 0, 1),
				1,
				0,
				mResearchDis);
	
			if (!mSearchInactiveTarget && kTarget != null && !kTarget.IsActive())
			{
				kTarget = null;	//< 非全自动战斗状态不检索非激活的目标aa
			}
		}

		// 1.找到新的攻击目标aa
		//	a.若是切换目标,则切换到战斗状态且发出切换目标通知(例行查找需要攻击目标与当前目标不同)aa
		//	b.若是查找目标,则切换到战斗状态且发出切换目标通知aa
		// 2.没有找到新的攻击目标aa
		//	a.若是切换目标并且非例行检查,则退出战斗状态且发出切换目标通知aa
		if (kTarget != null) 
		{
			if (kIsChangingTarget)
			{
				if (kIsRoutineSearch)
				{
					if (kPreviousTarget != kTarget)
					{
						mTargetState.target = kTarget;
						mTargetState.LastHurtedTime = Time.time;
						mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
						mTargetState.Active(true, kAutoFight);

						kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
					}
				}
				else
				{
					mTargetState.target = kTarget;
					mTargetState.LastHurtedTime = Time.time;
					mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
					mTargetState.Active(true, kAutoFight);

					kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
				}
			}
			else
			{
				kPreviousTarget = null;		//< 寻找目标时,前一目标为空aa

				mTargetState.target = kTarget;
				mTargetState.LastHurtedTime = Time.time;
				mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
				mTargetState.Active(true, kAutoFight);

				kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
			}
		}
		else
		{
			if (kIsChangingTarget)
			{
				if (kIsRoutineSearch)
				{

				}
				else
				{
					mTargetState.target = null;
					mTargetState.LastHurtedTime = 0.0f;
					mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
					mTargetState.Active(false, kAutoFight);

					kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
				}
			}
		}			
	}
	
	// 手动设置攻击目标aa
	public void SetTarget(sdActorInterface kTarget, AutoFight kAutoFight)
	{
		sdActorInterface kPreviousTarget = null;
		if (mTargetState.Active())
			kPreviousTarget = mTargetState.target;
		
		mTargetState.target = kTarget;
		mTargetState.LastHurtedTime = Time.time;
		mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
		mTargetState.Active(true, kAutoFight);

		kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
		
		mAccumResearchTime = 0.0f;
	}

	// 使用指定技能攻击最近的目标(目标存在则返回,否则寻找最近的目标作为攻击目标)aa
	public bool SetNearestTarget(int skill_id, AutoFight c)
	{
		bool bHasTarget	= mTargetState.Active();	
		if (!bHasTarget)
		{
			sdMainChar mainChar = sdGameLevel.instance.mainChar;
			if(mainChar == null)
				return false;

			sdSkillTree skilltree = mainChar.skillTree;
			if(skilltree == null)
				return false;

			sdSkill skill = skilltree.getSkill(skill_id);
			if(skill == null)
				return false;

			sdBaseState	state	= skill.actionStateList[0];	
			if(state == null)
				return false;

			int TargetType	=	(int)state.stateData["byTargetType"];
			sdActorInterface obj = sdGameLevel.instance.actorMgr.FindNearestActorActiveFirst(
				c.Actor,
				(HeaderProto.ESkillObjType)TargetType,
				c.Actor.transform.position,
				new Vector3(0,0,1),
				1,
				0,
				10000.0f
				);
			if(obj!=null)
			{
				mTargetState.ManualSkill = skill_id;
				SetTarget(obj,c);
			}
		}

		return bHasTarget;
	}

	// 转向到目标aa
	public void AdjustDirection(int skillid, AutoFight kAutoFight)
	{
		bool bHasTarget = mTargetState.Active();
		if(bHasTarget && mTargetState.target != null && kAutoFight.Actor != null)
		{
			Vector3 v		=	kAutoFight.GetPosition();
			Vector3	vDst	=	mTargetState.target.transform.position;
			Vector3	vDir	=	vDst-v;
			vDir.y = 0.0f;
			vDir.Normalize();
			kAutoFight.MoveDir(vDir);
			kAutoFight.Actor.spinToTargetDirection(vDir, true);
			
			if(skillid == 1001) //1001技能切换目标aa
			{
				List<sdActorInterface> lstActor = null;
				lstActor = sdGameLevel.instance.actorMgr.FindActor(
					kAutoFight.Actor,
					HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
					kAutoFight.Actor.transform.position,
					Vector3.zero,
					1,
					0,
					mResearchDis,
					true);
				if(lstActor != null)
				{
					lstActor.Remove(mTargetState.target);
					if(lstActor.Count > 0)
					{
						int index = Random.Range(0, lstActor.Count);
						if(lstActor[index] != null)
						{
							SetTarget(lstActor[index], kAutoFight);
						}
					}
				}
			}
		}
	}

	//
	public	void	SetPoint(Vector3 point,AutoFight c)
	{
		sdTSM	tsm	=	c.Actor.logicTSM;
		if(tsm.IsAnimLoop())
		{
			tsm.ForceStop();
		}
		
		mMovePoint.DstPosition = point;
		mMovePoint.Active(true,c);
		

		mTargetState.Active(false,c);
		mTargetState.ManualSkill = 0;
		
	}

	// 手动翻滚
	public void Roll(Vector3 vDirection, AutoFight kAutoFight)
	{
		if (mRollState.Active())
			return;

		mRollState.vDir	= vDirection;
		mRollState.Active(true, kAutoFight);

		mMovePoint.Active(false, kAutoFight);
	}
	
	// 自动战斗模式aa
	public void SetFullAutoMode(bool bFullAutoMode, AutoFight kAutoFight)
	{
		if (bFullAutoMode)
		{
			mSearchInactiveTarget = true;
			mResearchDis = 10000.0f;
			mTargetState.Active(false, kAutoFight);
		}
		else
		{
			mSearchInactiveTarget = false;
			mResearchDis = 5.0f;
			mTargetState.Active(false, kAutoFight);
		}
	}

	//
	public void ClearMainCharacterMove(AutoFight kAutoFight)
	{
		mMovePoint.Active(false, kAutoFight);
	}
    public MovePointState GetMovePointState()
    {
        return mMovePoint;
    }
}

/// <summary>
/// PVP角色自动战斗系统aa
/// </summary>
public class PVPAutoState : AutoState
{
	// 更新(继承自BehaviourState)
	public override void Update(AutoFight kAutoFight)
	{
		if (!Active())
			return;

		PVPAutoFight kPVPAutoFight = kAutoFight as PVPAutoFight;
		if (kPVPAutoFight == null)
			return;

		// 目标不存在则不更新aa
		sdActorInterface kMainTarget = kPVPAutoFight.Target;
		if (kMainTarget == null)
			return;

		// 更新重新搜索时间aa
		mAccumResearchTime += Time.deltaTime;

		// 自动战斗状态aa
		if (mTargetState.Active())
		{
			sdActorInterface kPreviousTarget = mTargetState.target;	//< 放在Update前面是防止在自动退出状态时被清掉aa
			mTargetState.Update(kAutoFight);

			if (!mTargetState.Active())
			{
				sdActorInterface kTarget = null;
				if (kMainTarget.GetCurrentHP() > 0)
					kTarget = kMainTarget;

				if (kTarget != null)
				{
					bool bChangeTarget = true;
					if (mTargetState.Active())
						bChangeTarget = (kPreviousTarget != kTarget);//< 定时自动切换目标则检查是否同一个目标aa

					if (bChangeTarget)
					{
						mTargetState.target = kTarget;
						mTargetState.LastHurtedTime = Time.time;
						mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
						mTargetState.Active(true, kAutoFight);

						kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
					}
				}
				else
				{
					mTargetState.target = null;
					mTargetState.LastHurtedTime = 0.0f;
					mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
					mTargetState.Active(false, kAutoFight);

					kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);
				}
			}

			return;
		}

		// 每0.5s重新查找攻击目标aa
		{
			sdActorInterface kTarget = null;
			if (mAccumResearchTime > 0.5f)
			{
				mAccumResearchTime = 0.0f;

				if (kMainTarget.GetCurrentHP() > 0)
					kTarget = kMainTarget;
			}

			if (kTarget != null)
			{
				sdActorInterface kPreviousTarget = null;

				mTargetState.target = kTarget;
				mTargetState.LastHurtedTime = Time.time;
				mTargetState.BeatPriority = EBeatPriority.BP_Unknown;
				mTargetState.Active(true, kAutoFight);

				kAutoFight.OnChangeTarget(kPreviousTarget, mTargetState.target);

				return;
			}
		}
	}

	// 设置技能aa
	public void SetSkill(sdGameActor kActor)
	{
		if (kActor != null && kActor.skillTree != null)
		{
			List<sdSkill> kSkillList = new List<sdSkill>();
			foreach (DictionaryEntry kEntry in kActor.skillTree.AllSkill)
			{
				sdSkill kSkill = kEntry.Value as sdSkill;
				if (kSkill != null)
				{
					if (kSkill.id != 1001 && kSkill.id != 1002 && !kSkill.isPassive)
						kSkillList.Add(kSkill);	//< 不考虑普通攻击/翻滚/被动技能aa
				}
			}

			kSkillList.Sort(new SkillPriorityCompare());
			foreach (sdSkill kSkill in kSkillList)
			{
				mTargetState.AddAutoSkill(kSkill.id);
			}
		}
	}

	// 比较器(技能总伤害高的优先级高)aa
	protected class SkillPriorityCompare : IComparer<sdSkill>
	{
		public int Compare(sdSkill kLeftSkill, sdSkill kRightSkill)
		{
			int iLeftTotalAttackPower = 0;
			foreach (sdBaseState kState in kLeftSkill.actionStateList)
			{
				int [] aiAttackPowerPer = (int[])kState.stateData["dwAtkPowerPer[10]"];
				for (int i = 0; i < aiAttackPowerPer.Length; ++i)
					iLeftTotalAttackPower += aiAttackPowerPer[i];
			}

			int iRightTotalAttackPower = 0;
			foreach (sdBaseState kState in kRightSkill.actionStateList)
			{
				int[] aiAttackPowerPer = (int[])kState.stateData["dwAtkPowerPer[10]"];
				for (int i = 0; i < aiAttackPowerPer.Length; ++i)
					iRightTotalAttackPower += aiAttackPowerPer[i];
			}

			if (iLeftTotalAttackPower > iRightTotalAttackPower)
				return 1;
			else if (iLeftTotalAttackPower < iRightTotalAttackPower)
				return -1;
			else
				return 0;
		}
	}
}