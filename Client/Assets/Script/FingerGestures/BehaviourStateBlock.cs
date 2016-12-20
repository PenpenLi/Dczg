using UnityEngine;
using System.Collections;

/// <summary>
/// 触发系统的状态事件类型aa
/// </summary>
public enum EBehaviourStateEventType
{
	enBSET_Unknown = 0,
	enBSET_Summoned,
	enBSET_Killed,
	enBSET_Target,
	enBSET_HP,
	enBSET_Buff,
}

/// <summary>
/// 触发系统所需状态集合aa
/// </summary>
public class sdBehaviourStateBlock : object
{	
	// 召唤次数aa
	protected int mSummonedCount = 0;
	public int SummonedCount
	{
		get { return mSummonedCount;}	
	}
	
	// 死亡次数aa
	protected int mKilledCount = 0;
	public int KilledCount
	{
		get { return mKilledCount;}	
	}
	
	// 总血量aa
	protected int mMaxHP = 0;
	public int MaxHP
	{
		get { return mMaxHP;}	
	}
	
	// 改变前血量aa
	protected int mPreviousHP = 0;
	public int PreviousHP
	{
		get { return mPreviousHP;}		
	}
	
	// 当次改变的血量aa
	protected int mChangeHP = 0;
	public int ChangeHP
	{
		get { return mChangeHP;}		
	}	
	
	// 累计伤害血量aa
	protected int mAccumHurtHP = 0;
	public int AccumHurtHP
	{
		get { return mAccumHurtHP;}		
	}

    // 上一次攻击目标aa
    protected sdActorInterface mPreviousTarget = null;
    public sdActorInterface PreviousTarget
    {
        get { return mPreviousTarget; }		 
    }

    // 本次攻击目标aa
    protected sdActorInterface mTarget = null;
    public sdActorInterface Target
    {
        get { return mTarget; }
    }

	// 当次事件操作的Buff类型aa
	protected HeaderProto.ECreatureActionState mState;
	public HeaderProto.ECreatureActionState State
	{
		get { return mState; }
	}

	// 角色到最近友方的距离aa
	protected float mDistanceOfNearestFriendUpdateTime = 0.0f;	///< 更新时间aa
	protected float mDistanceOfNearestFriend = float.MaxValue;
	public float DistanceOfNearestFriend
	{
		get { return mDistanceOfNearestFriend; }
	}

	// 角色到攻击目标的距离aa
	protected float mDistanceOfNearestEnemyUpdateTime = 0.0f;	///< 更新时间aa
	protected float mDistanceOfNearestEnemy = float.MaxValue;
	public float DistanceOfNearestEnemy
	{
		get { return mDistanceOfNearestEnemy; }
	}

	
	

	// 当次事件类型aa
	protected EBehaviourStateEventType mBehaviourStateEventType = EBehaviourStateEventType.enBSET_Unknown;
	public EBehaviourStateEventType BehaviourStateEventType
	{
		get { return mBehaviourStateEventType; }
	}

	// 被召唤回调aa
	public void OnSummoned(sdActorInterface kActor)
	{
		mSummonedCount += 1;
		
		mMaxHP = kActor.GetMaxHP();
		mPreviousHP = mMaxHP;
		mAccumHurtHP = 0;

		mBehaviourStateEventType = EBehaviourStateEventType.enBSET_Summoned;//< 当次事件类型aa
	}
	
	// 死亡回调aa
	public void OnKilled(sdActorInterface kActor)
	{
		mKilledCount += 1;

		mBehaviourStateEventType = EBehaviourStateEventType.enBSET_Killed;	//< 当次事件类型aa
	}
	
	// 血量改变回调aa
	public void OnAddHP(sdActorInterface kActor, int iHP)
	{	
		mPreviousHP = kActor.GetCurrentHP();
		mChangeHP = iHP;
		
		if (iHP < 0)
			mAccumHurtHP += iHP;	//< 只计算伤害量aa

		mBehaviourStateEventType = EBehaviourStateEventType.enBSET_HP;		//< 当次事件类型aa
	}
	
	// 切换目标aa
	public void OnChangeTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		mPreviousTarget = kPreviousTarget;
		mTarget = kTarget;

		mBehaviourStateEventType = EBehaviourStateEventType.enBSET_Target;	//< 当次事件类型aa
	}
	
	// 获取状态aa
	public void OnAddDebuffState(sdActorInterface kActor, HeaderProto.ECreatureActionState eState)
	{
		mState = eState;

		mBehaviourStateEventType = EBehaviourStateEventType.enBSET_Buff;	//< 当次事件类型aa
	}

	// 更新最近的友方的距离(这里的距离是到对方边缘的距离)aa
	public float UpdateDistanceOfNearestFriend(sdBehaviourAdvancedState kBehaviourAdvancedState)
	{
		if (Time.time - mDistanceOfNearestFriendUpdateTime > 1.0f)
		{
			float fSkillDistance = float.MaxValue;

			MonsterAutoFight kMonsterAutoFight = kBehaviourAdvancedState.MonsterAutoFightComponent;
			sdActorInterface kActor = kMonsterAutoFight.Monster;
			sdActorInterface kTarget = kMonsterAutoFight.FindNearestFriend(10.0f);
			if (kActor != null && kTarget != null)
			{
				fSkillDistance = kMonsterAutoFight.CalcSkillDistance(kActor, kTarget);
			}

			mDistanceOfNearestFriendUpdateTime = Time.time;
			mDistanceOfNearestFriend = fSkillDistance;
		}

		return mDistanceOfNearestFriend;
	}

	// 更新最近的敌方的距离(这里的距离是到对方边缘的距离)aa
	public float UpdateDistanceOfNearestEnemy(sdBehaviourAdvancedState kBehaviourAdvancedState)
	{
		if (Time.time - mDistanceOfNearestEnemyUpdateTime > 1.0f)
		{
			float fSkillDistance = float.MaxValue;

			MonsterAutoFight kMonsterAutoFight = kBehaviourAdvancedState.MonsterAutoFightComponent;
			sdActorInterface kActor = kMonsterAutoFight.Monster;
			sdActorInterface kTarget = kMonsterAutoFight.GetAttackTarget();
			if (kActor != null && kTarget != null)
			{
				fSkillDistance = kMonsterAutoFight.CalcSkillDistance(kActor, kTarget);
			}

			mDistanceOfNearestEnemyUpdateTime = Time.time;
			mDistanceOfNearestEnemy = fSkillDistance;
		}

		return mDistanceOfNearestEnemy;
	}
}
