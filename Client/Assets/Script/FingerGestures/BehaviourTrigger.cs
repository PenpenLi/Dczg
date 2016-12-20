using UnityEngine;
using System.Collections;

/// <summary>
/// 触发动作和触发状态类型aa
/// </summary>
public enum EBehaviourTriggerType
{
	enBTT_Unknown       = 0,

	enBTT_LiveState		= 101,
	enBTT_BattleState	= 102,
	enBTT_HPState		= 103,
	enBTT_IdleState		= 104,
	enBTT_TimeState		= 105,

	enBTT_Summoned		= 1,
	enBTT_Killed		= 2,
    enBTT_ChangeTarget  = 3,
	enBTT_HP            = 4,
	enBTT_HurtHP        = 5,
	enBTT_AccumHurtHP   = 6,
    enBTT_Buff          = 7,
}

/// <summary>
/// 触发条件的使用类型aa
/// </summary>
public enum EBehaviourTriggerUsageType
{
	enBTUT_None,		//< 不作任何触发aa
	enBTUT_Enter,		//< 仅仅用作进入触发aa
	enBTUT_Leave,		//< 仅仅用作离开触发aa
	enBTUT_Both,		//< 同时用作进入和离开触发aa
}

// 进入触发条件事件aa
public delegate void NotifyEnterTriggerDelegate(sdBehaviourTrigger kBehaviourTrigger);

// 离开触发条件事件aa
public delegate void NotifyLeaveTriggerDelegate(sdBehaviourTrigger kBehaviourTrigger);

/// <summary>
/// 触发条件aa
/// </summary>
public class sdBehaviourTrigger : System.ICloneable
{
	// 触发条件类型aa
	protected EBehaviourTriggerType mBTType = EBehaviourTriggerType.enBTT_Unknown;
	public EBehaviourTriggerType BTType
	{
		get { return mBTType;}
	}

	// 触发条件的使用方式aa
	protected EBehaviourTriggerUsageType mBTUType = EBehaviourTriggerUsageType.enBTUT_Both;
	public EBehaviourTriggerUsageType BTUType
	{
		set { mBTUType = value; }
		get { return mBTUType; }
	}

	// 触发条件是否作为进入条件aa
	public bool IsEnterTrigger
	{
		get { return mBTUType != EBehaviourTriggerUsageType.enBTUT_Leave; }
	}

	// 触发条件是否作为离开条件aa
	public bool IsLeaveTrigger
	{
		get { return mBTUType != EBehaviourTriggerUsageType.enBTUT_Enter; }
	}
	
	// 进入触发条件事件aa
	public NotifyEnterTriggerDelegate NotifyEnterTrigger;
	
	// 离开触发条件事件aa
	public NotifyLeaveTriggerDelegate NotifyLeaveTrigger;
	
	// 标记触发条件是否被触发aa
	protected bool mIsTriggered = false;
	public bool IsTriggered
	{
		get { return mIsTriggered;} 
	}
	
	// 接口实现(ICloneable的接口)
	public virtual object Clone()
	{
		return null;
	}

	// 触发模块aa
	protected sdBehaviourEvent mParentBehaviourEvent = null;
	public sdBehaviourEvent ParentBehaviourEvent
	{
		set { mParentBehaviourEvent = value; }
	}

	
	// 同步更新触发条件(相关事件发生的时候才被调用)aa
	public virtual void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		
	}
	
	// 更新触发条件(每帧被调用)aa
	public virtual void Update() 
	{
		
	}
	
	// 强制重新设置触发状态aa
	public virtual void ForceReset()
	{
		mIsTriggered = false;
	}
	
	// 强制重新检查触发状态aa
	public virtual void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		
	}
}

/// <summary>
/// 活着的状态:角色召唤次数大于死亡次数为真aa
/// </summary>
public class sdLiveBehaviourTriggerState : sdBehaviourTrigger
{
	//
    public sdLiveBehaviourTriggerState()
	{
		mBTType = EBehaviourTriggerType.enBTT_LiveState;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
        sdLiveBehaviourTriggerState kLiveBehaviourTriggerState = new sdLiveBehaviourTriggerState();
        kLiveBehaviourTriggerState.mBTUType = this.mBTUType;

        return kLiveBehaviourTriggerState;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Summoned &&	kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Killed)
			return;

		CheckAndSet(kBehaviourStateBlock, true);
	}
	
	// 强制重新检查触发状态(继承自BehaviourTrigger)aa
	public override void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		base.ForceRecheck(kBehaviourStateBlock, bTrigger);

		CheckAndSet(kBehaviourStateBlock, bTrigger);
	}
	
	// 检查并设置触发状态aa
	protected void CheckAndSet(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		if (kBehaviourStateBlock.SummonedCount > kBehaviourStateBlock.KilledCount)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null && bTrigger)
				{
					NotifyEnterTrigger(this);
				}
			}
		}
		else
		{
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null && bTrigger)
				{
					NotifyLeaveTrigger(this);	
				}
			}
		}	
	}
}

/// <summary>
/// 战斗状态:有攻击目标视作战斗状态aa
/// </summary>
public class sdBattleBehaviourTriggerState : sdBehaviourTrigger
{
	//
    public sdBattleBehaviourTriggerState()
	{
		mBTType = EBehaviourTriggerType.enBTT_BattleState;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
        sdBattleBehaviourTriggerState kBattleBehaviourTriggerState = new sdBattleBehaviourTriggerState();
        kBattleBehaviourTriggerState.mBTUType = this.mBTUType;

        return kBattleBehaviourTriggerState;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Target)
			return;
		
		CheckAndSet(kBehaviourStateBlock, true);
	}
	
	// 强制重新检查触发状态(继承自BehaviourTrigger)aa
	public override void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		base.ForceRecheck(kBehaviourStateBlock, bTrigger);

		CheckAndSet(kBehaviourStateBlock, bTrigger);
	}
	
	// 检查并设置触发状态aa
	protected void CheckAndSet(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		if (kBehaviourStateBlock.Target != null)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null && bTrigger)
				{
					NotifyEnterTrigger(this);
			    }  
            }
        }
        else
        {
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null && bTrigger)
				{
					NotifyLeaveTrigger(this);
				}
			}
		}	
	}
}

/// <summary>
/// 血量状态: 血量在最大血量指定百分比范围内为真aa
/// </summary>
public class sdHPBehaviourTriggerState : sdBehaviourTrigger
{
	// 血量比例下限aa
	protected float mMinConditionPercent = 0.0f;
	public float MinConditionPercent
	{
		set { mMinConditionPercent = value; }
		get { return mMinConditionPercent; }
	}

	// 血量比例上限aa
	protected float mMaxConditionPercent = 0.5f;
	public float MaxConditionPercent
	{
		set { mMaxConditionPercent = value; }
		get { return mMaxConditionPercent; }
	}

	//
	public sdHPBehaviourTriggerState()
	{
		mBTType = EBehaviourTriggerType.enBTT_HPState;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdHPBehaviourTriggerState kHPBehaviourTriggerState = new sdHPBehaviourTriggerState();
		kHPBehaviourTriggerState.mBTUType = this.mBTUType;
		kHPBehaviourTriggerState.mMinConditionPercent = this.mMinConditionPercent;
		kHPBehaviourTriggerState.mMaxConditionPercent = this.mMaxConditionPercent;

		return kHPBehaviourTriggerState;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_HP)
			return;
		
		CheckAndSet(kBehaviourStateBlock, true);
	}
	
	// 强制重新检查触发状态(继承自BehaviourTrigger)aa
	public override void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		base.ForceRecheck(kBehaviourStateBlock, bTrigger);

		CheckAndSet(kBehaviourStateBlock, bTrigger);
	}
	
	// 检查并设置触发状态aa
	protected void CheckAndSet(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		int iMaxHP = kBehaviourStateBlock.MaxHP;
		int iLastHP = kBehaviourStateBlock.PreviousHP;
		int iCurrentHP = iLastHP + kBehaviourStateBlock.ChangeHP;
		float fCurrentHPPercentage = (float)iCurrentHP / (float)iMaxHP;
		if (fCurrentHPPercentage >= mMinConditionPercent && fCurrentHPPercentage < mMaxConditionPercent)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;

				if (IsEnterTrigger && NotifyEnterTrigger != null && bTrigger)
				{
					NotifyEnterTrigger(this);
				}
			}
		}
		else
		{
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null && bTrigger)
				{
					NotifyLeaveTrigger(this);
				}
			}
		}
	}
}

/// <summary>
/// 空闲状态:无攻击目标视作空闲状态aa
/// </summary>
public class sdIdleBehaviourTriggerState : sdBehaviourTrigger
{
	//
    public sdIdleBehaviourTriggerState()
	{
		mBTType = EBehaviourTriggerType.enBTT_IdleState;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
        sdIdleBehaviourTriggerState kIdleBehaviourTriggerState = new sdIdleBehaviourTriggerState();
        kIdleBehaviourTriggerState.mBTUType = this.mBTUType;

        return kIdleBehaviourTriggerState;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Target)
			return;
		
		CheckAndSet(kBehaviourStateBlock, true);
	}
	
	// 强制重新检查触发状态(继承自BehaviourTrigger)aa
	public override void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		base.ForceRecheck(kBehaviourStateBlock, bTrigger);

		CheckAndSet(kBehaviourStateBlock, bTrigger);
	}
	
	// 检查并设置触发状态aa
	protected void CheckAndSet(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		if (kBehaviourStateBlock.Target == null)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null && bTrigger)
				{
					NotifyEnterTrigger(this);
			    }  
            }
        }
        else
        {
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null && bTrigger)
				{
					NotifyLeaveTrigger(this);
				}
			}
		}	
	}
}

/// <summary>
/// 父节点(EventNode)触发后每隔指定时间触发,持续指定时间aa
/// </summary>
public class sdTimeBehaviourTriggerState : sdBehaviourTrigger
{
	// 动作延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 间隔时间aa
	protected float mIntervalTime = 10.0f;
	public float IntervalTime
	{
		set { mIntervalTime = value; }
		get { return mIntervalTime; }
	}

	// 持续时间aa
	protected float mElapseTime = 10.0f;
	public float ElapseTime
	{
		set { mElapseTime = value; }
		get { return mElapseTime; }
	}

	// 持续时间aa
	public sdTimeBehaviourTriggerState()
	{
		mBTType = EBehaviourTriggerType.enBTT_TimeState;
	}

	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdTimeBehaviourTriggerState kTimeBehaviourTriggerState = new sdTimeBehaviourTriggerState();
		kTimeBehaviourTriggerState.mBTUType = this.mBTUType;
		kTimeBehaviourTriggerState.mDelayTime = this.mDelayTime;
		kTimeBehaviourTriggerState.mIntervalTime = this.mIntervalTime;
		kTimeBehaviourTriggerState.mElapseTime = this.mElapseTime;

		return kTimeBehaviourTriggerState;
	}

	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		base.SyncUpdate(kBehaviourStateBlock);
	}

	// 更新触发条件(继承自BehaviourTrigger)aa
	public override void Update()
	{
		if (mParentBehaviourEvent == null)
			return;

		sdBehaviourEventNode mParentBehaviourEventNode = mParentBehaviourEvent.ParentBehaviourEventNode;
		if (mParentBehaviourEventNode == null)
			return;

		float fEventNodeStartTime = mParentBehaviourEventNode.TriggerEnterTime;
		float fEventNodeElapseTime = Time.time - fEventNodeStartTime;
		if (fEventNodeElapseTime > mDelayTime)
		{
			fEventNodeElapseTime -= mDelayTime;
			while (fEventNodeElapseTime > mIntervalTime + mElapseTime)
			{
				fEventNodeElapseTime -= mIntervalTime + mElapseTime;
			}

			if (fEventNodeElapseTime <= mElapseTime)
			{
				if (mIsTriggered != true)
				{
					mIsTriggered = true;

					if (IsEnterTrigger && NotifyEnterTrigger != null)
					{
						NotifyEnterTrigger(this);
					}
				}
			}
			else
			{
				if (mIsTriggered != false)
				{
					mIsTriggered = false;
					if (IsLeaveTrigger && NotifyLeaveTrigger != null)
					{
						NotifyLeaveTrigger(this);
					}
				}
			}
		}
		else
		{

		}
	}

	// 重设触发条件(继承自BehaviourTrigger)aa
	public override void ForceReset()
	{
		base.ForceReset();
	}

	// 强制重新检查触发状态(继承自BehaviourTrigger)aa
	public override void ForceRecheck(sdBehaviourStateBlock kBehaviourStateBlock, bool bTrigger)
	{
		base.ForceRecheck(kBehaviourStateBlock, bTrigger);
	}
}

/// <summary>
/// 被召唤触发aa
/// </summary>
public class sdSummonedBehaviourTrigger : sdBehaviourTrigger
{
	//
	public sdSummonedBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_Summoned;
	}

	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdSummonedBehaviourTrigger kSummonedBehaviourTrigger = new sdSummonedBehaviourTrigger();
		kSummonedBehaviourTrigger.mBTUType = this.mBTUType;

		return kSummonedBehaviourTrigger;
	}

	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Summoned)
			return;

		if (mIsTriggered != true)
		{
			mIsTriggered = true;
			if (IsEnterTrigger && NotifyEnterTrigger != null)
			{
                if (!BundleGlobal.IsMobile())
                {
                    //Debug.Log("sdSummonedBehaviourTrigger::NotifyEnterTrigger");
                }
				NotifyEnterTrigger(this);
			}
		}

		if (mIsTriggered != false)
		{
			mIsTriggered = false;
			if (IsLeaveTrigger && NotifyLeaveTrigger != null)
			{
                if (!BundleGlobal.IsMobile())
                {
                    //Debug.Log("sdSummonedBehaviourTrigger::NotifyLeaveTrigger");
                }
				NotifyLeaveTrigger(this);
			}
		}
	}
}

/// <summary>
/// 被杀死唤触发aa
/// </summary>
public class sdKilledBehaviourTrigger : sdBehaviourTrigger
{
	//
	public sdKilledBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_Killed;
	}

	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdKilledBehaviourTrigger kKilledBehaviourTrigger = new sdKilledBehaviourTrigger();
		kKilledBehaviourTrigger.mBTUType = this.mBTUType;

		return kKilledBehaviourTrigger;
	}

	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Killed)
			return;

		if (mIsTriggered != true)
		{
			mIsTriggered = true;
			if (IsEnterTrigger && NotifyEnterTrigger != null)
			{
                if (!BundleGlobal.IsMobile())
                {
                    //Debug.Log("sdKilledBehaviourTrigger::NotifyEnterTrigger");
                }
				NotifyEnterTrigger(this);
			}
		}

		if (mIsTriggered != false)
		{
			mIsTriggered = false;
			if (IsLeaveTrigger && NotifyLeaveTrigger != null)
			{
                if (!BundleGlobal.IsMobile())
                {
                    //Debug.Log("sdKilledBehaviourTrigger::NotifyLeaveTrigger");
                }
				NotifyLeaveTrigger(this);
			}
		}
	}
}

/// <summary>
/// 切换目标动作触发aa
/// </summary>
public class sdChangeTargetBehaviourTrigger : sdBehaviourTrigger
{
	//
	public sdChangeTargetBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_ChangeTarget;
	}

	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdChangeTargetBehaviourTrigger kChangeTargetBehaviourTriggerState = new sdChangeTargetBehaviourTrigger();
		kChangeTargetBehaviourTriggerState.mBTUType = this.mBTUType;

		return kChangeTargetBehaviourTriggerState;
	}

	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Target)
			return;

		if (kBehaviourStateBlock.PreviousTarget != kBehaviourStateBlock.Target)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null)
				{
                    if (!BundleGlobal.IsMobile())
                    {
                        //Debug.Log("sdChangeTargetBehaviourTrigger::NotifyEnterTrigger");
                    }
					NotifyEnterTrigger(this);
				}
			}

			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null)
				{
                    if (!BundleGlobal.IsMobile())
                    {
                        //Debug.Log("sdChangeTargetBehaviourTrigger::NotifyLeaveTrigger");
                    }
					NotifyLeaveTrigger(this);
				}
			}
		}
	}
}

/// <summary>
/// 血量触发的内部枚举值aa
/// </summary>
public enum EHPBehaviourTriggerCompareType
{
	enBHPBTCT_Less,			//< 小于指定值触发aa
	enBHPBTCT_EqualBigger,	//< 大于等于指定值触发aa
}

/// <summary>
/// 血量变化跨越最大血量指定百分比触发aa
/// </summary>
public class sdHPBehaviourTrigger : sdBehaviourTrigger
{
	// 血量触发子类型aa
	protected EHPBehaviourTriggerCompareType mBHPBTCType = EHPBehaviourTriggerCompareType.enBHPBTCT_Less;
	public EHPBehaviourTriggerCompareType BHPBTCType
	{
		set { mBHPBTCType = value; }
		get { return mBHPBTCType; }
	}

	// 血量比例aa
	protected float mConditionPercent = 0.3f;
	public float ConditionPercent
	{
		set { mConditionPercent = value; }
		get { return mConditionPercent; }
	}

	//
	public sdHPBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_HP;
	}

	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdHPBehaviourTrigger kHPBehaviourTrigger = new sdHPBehaviourTrigger();
		kHPBehaviourTrigger.mBTUType = this.mBTUType;
		kHPBehaviourTrigger.mBHPBTCType = this.mBHPBTCType;
		kHPBehaviourTrigger.mConditionPercent = this.mConditionPercent;

		return kHPBehaviourTrigger;
	}

	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_HP)
			return;

		int iMaxHP = kBehaviourStateBlock.MaxHP;
		int iLastHP = kBehaviourStateBlock.PreviousHP;
		int iCurrentHP = iLastHP + kBehaviourStateBlock.ChangeHP;
		float fLastHPPercentage = (float)iLastHP / (float)iMaxHP;
		float fCurrentHPPercentage = (float)iCurrentHP / (float)iMaxHP;
		if (fCurrentHPPercentage < mConditionPercent)
		{
			if (fLastHPPercentage >= mConditionPercent)
			{
				if (mBHPBTCType == EHPBehaviourTriggerCompareType.enBHPBTCT_Less)
				{
					if (mIsTriggered != true)
					{
						mIsTriggered = true;
						if (IsEnterTrigger && NotifyEnterTrigger != null)
						{
                            if (!BundleGlobal.IsMobile())
                            {
                                //Debug.Log("sdHPBehaviourTrigger::NotifyEnterTrigger");
                            }
							NotifyEnterTrigger(this);
						}
					}

					if (mIsTriggered != false)
					{
						mIsTriggered = false;
						if (IsLeaveTrigger && NotifyLeaveTrigger != null)
						{
                            if (!BundleGlobal.IsMobile())
                            {
                                //Debug.Log("sdHPBehaviourTrigger::NotifyLeaveTrigger");
                            }
							NotifyLeaveTrigger(this);
						}
					}
				}
			}
		}
		else
		{
			if (fLastHPPercentage < mConditionPercent)
			{
				if (mBHPBTCType == EHPBehaviourTriggerCompareType.enBHPBTCT_EqualBigger)
				{
					if (mIsTriggered != true)
					{
						mIsTriggered = true;
						if (IsEnterTrigger && NotifyEnterTrigger != null)
						{
                            if (!BundleGlobal.IsMobile())
                            {
                                //Debug.Log("sdHPBehaviourTrigger::NotifyEnterTrigger");
                            }
							NotifyEnterTrigger(this);
						}
					}

					if (mIsTriggered != false)
					{
						mIsTriggered = false;
						if (IsLeaveTrigger && NotifyLeaveTrigger != null)
						{
                            if (!BundleGlobal.IsMobile())
                            {
                                //Debug.Log("sdHPBehaviourTrigger::NotifyLeaveTrigger");
                            }
							NotifyLeaveTrigger(this);
						}
					}
				}
			}
		}
	}
}

/// <summary>
/// 单次损失高于最大血量指定百分比触发aa
/// </summary>
public class sdHurtHPBehaviourTrigger : sdBehaviourTrigger
{
	// 血量比例aa
	protected float mConditionPercent = 0.3f;
	public float ConditionPercent
	{
		set { mConditionPercent = value;}
		get { return mConditionPercent;}
	}
	
	//
	public sdHurtHPBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_HurtHP;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdHurtHPBehaviourTrigger kHurtHPBehaviourTrigger = new sdHurtHPBehaviourTrigger();
		kHurtHPBehaviourTrigger.mBTUType = this.mBTUType;
		kHurtHPBehaviourTrigger.mConditionPercent = this.mConditionPercent;
		
		return kHurtHPBehaviourTrigger;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_HP)
			return;
		
		if (kBehaviourStateBlock.ChangeHP > 0)
			return;

		int iMaxHP = kBehaviourStateBlock.MaxHP;
		int iHurtHP = -kBehaviourStateBlock.ChangeHP;
		float fHurtHPPerctage = (float)iHurtHP / (float)iMaxHP;
		if (fHurtHPPerctage >= mConditionPercent)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null)
					NotifyEnterTrigger(this);
			}
			
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null)
					NotifyLeaveTrigger(this);
			}
		}
	}
}

/// <summary>
/// 累计损失高于最大血量指定百分比触发aa
/// </summary>
public class sdAccumHurtHPBehaviourTrigger : sdBehaviourTrigger
{
	// 血量比例aa
	protected float mConditionPercent = 0.3f;
	public float ConditionPercent
	{
		set { mConditionPercent = value;}
		get { return mConditionPercent;}
	}
	
	// 上次触发时候的累计损失血量百分比aa
	protected float mLastConditionPercent = 0.0f;
	
	//
	public sdAccumHurtHPBehaviourTrigger()
	{
		mBTType = EBehaviourTriggerType.enBTT_AccumHurtHP;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdAccumHurtHPBehaviourTrigger kAccumHurtHPBehaviourTrigger = new sdAccumHurtHPBehaviourTrigger();
		kAccumHurtHPBehaviourTrigger.mBTUType = this.mBTUType;
		kAccumHurtHPBehaviourTrigger.mConditionPercent = this.mConditionPercent;
		
		return kAccumHurtHPBehaviourTrigger;
	}
	
	// 触发事件(继承自BehaviourTrigger)aa
	public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock) 
	{
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_HP)
			return;

		int iMaxHP = kBehaviourStateBlock.MaxHP;
		int iAccumHurtHP = -kBehaviourStateBlock.AccumHurtHP;
		float fAccumHurtHPPerctage = (float)iAccumHurtHP / (float)iMaxHP;
		if (fAccumHurtHPPerctage >= mConditionPercent + mLastConditionPercent)
		{
			if (mIsTriggered != true)
			{
				mIsTriggered = true;
				if (IsEnterTrigger && NotifyEnterTrigger != null)
					NotifyEnterTrigger(this);
				
				mLastConditionPercent = fAccumHurtHPPerctage;
			}
			
			if (mIsTriggered != false)
			{
				mIsTriggered = false;
				if (IsLeaveTrigger && NotifyLeaveTrigger != null)
					NotifyLeaveTrigger(this);
			}
		}
	}
	
	// 重设触发条件(继承自BehaviourTrigger)aa
	public override void ForceReset()
	{
		base.ForceReset();
		
		mLastConditionPercent = 0.0f;	
	}
}

/// <summary>
/// 获取指定Buff时被触发aa
/// </summary>
public class sdBuffBehaviourTrigger : sdBehaviourTrigger
{
	// Buff类型aa
	protected HeaderProto.ECreatureActionState mBuffState;
	public HeaderProto.ECreatureActionState BuffState
	{
		set { mBuffState = value; }
		get { return mBuffState; }
	}

    //
	public sdBuffBehaviourTrigger()
    {
        mBTType = EBehaviourTriggerType.enBTT_Buff;
    }

    // 接口实现(ICloneable的接口)aa
    public override object Clone()
    {
		sdBuffBehaviourTrigger kBuffBehaviourTriggerState = new sdBuffBehaviourTrigger();
		kBuffBehaviourTriggerState.mBTUType = this.mBTUType;
		kBuffBehaviourTriggerState.mBuffState = this.mBuffState;

		return kBuffBehaviourTriggerState;
    }

    // 触发事件(继承自BehaviourTrigger)aa
    public override void SyncUpdate(sdBehaviourStateBlock kBehaviourStateBlock)
    {
		if (kBehaviourStateBlock.BehaviourStateEventType != EBehaviourStateEventType.enBSET_Buff)
			return;

		if (mIsTriggered != true)
        {
             mIsTriggered = true;
             if (IsEnterTrigger && NotifyEnterTrigger != null)
             {
                 if (!BundleGlobal.IsMobile())
                 {
                     //Debug.Log("sdBuffBehaviourTrigger::NotifyEnterTrigger");
                 }
                 NotifyEnterTrigger(this);
             }
        }

        if (mIsTriggered != false)
        {
            mIsTriggered = false;
            if (IsLeaveTrigger && NotifyLeaveTrigger != null)
            {
                if (!BundleGlobal.IsMobile())
                {
                    //Debug.Log("sdBuffBehaviourTrigger::NotifyLeaveTrigger");
                }
                 NotifyLeaveTrigger(this);
            }
        }
    }
}