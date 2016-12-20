using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 触发动作类型aa
/// </summary>
public enum EBehaviourActionType
{
	enBAT_Unknown   = 0,
	enBAT_Summon	= 2,
	enBAT_Skill     = 4,
	enBAT_Buff      = 5,
}

/// <summary>
/// 触发动作aa
/// </summary>
public class sdBehaviourAction : System.ICloneable
{
	// 动作类型aa
	protected EBehaviourActionType mBAType = EBehaviourActionType.enBAT_Unknown;
	public EBehaviourActionType BAType
	{
		get { return mBAType;}
	}
	
	// 动作执行次数aa
	protected int mCount = 1;
	public int Count
	{
		set { mCount = value;}	
		get { return mCount;}
	}
	
	// 动作延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value;}	
		get { return mDelayTime;}
	}
	
	// 动作执行间隔aa
	protected float mIntervalTime = 2.0f;
	public float IntervalTime
	{
		set { mIntervalTime = value;}	
		get { return mIntervalTime;}
	}
	
	// 触发时间戳aa
	protected float mTimeStamp = 0.0f;
	public float TimeStamp
	{
		set { mTimeStamp = value;}	
		get { return mTimeStamp;}
	}
	
	// 触发模块aa
	protected sdBehaviourAdvancedState mBehaviourAdvancedState = null;
	public sdBehaviourAdvancedState BehaviourAdvancedState
	{
		set { mBehaviourAdvancedState = value;}	
	}
	
	// 接口实现(ICloneable的接口)
	public virtual object Clone()
	{
		return null;
	}
	
	// 触发动作(触发条件调用)aa
	public virtual void DoSyncAction() 
	{
		
	}
	
	// 触发动作(延迟调用)aa
	public virtual void DoAsyncAction() 
	{
		
	}
}

/// <summary>
/// 召唤事件aa
/// </summary>
public class sdSummonBehaviourAction : sdBehaviourAction
{
	// 召唤IDaa
	protected string mLevelAreaName;
	public string LevelAreaName
	{
		set { mLevelAreaName = value;}	
		get { return mLevelAreaName;}
	}

	// 召唤区域节点aa
	protected sdLevelArea mSummonLevelArea = null;
	
	//
	public sdSummonBehaviourAction()
	{
		mBAType = EBehaviourActionType.enBAT_Summon;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdSummonBehaviourAction kSummonBehaviourAction = new sdSummonBehaviourAction();
		kSummonBehaviourAction.mCount = this.Count;
		kSummonBehaviourAction.mDelayTime = this.mDelayTime;
		kSummonBehaviourAction.mIntervalTime = this.mIntervalTime;
		kSummonBehaviourAction.mTimeStamp = this.mTimeStamp;
		kSummonBehaviourAction.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kSummonBehaviourAction.LevelAreaName = this.LevelAreaName;
		
		return kSummonBehaviourAction;
	}
	
	// 触发动作(触发条件调用,继承自sdBehaviourAction)aa
	public override void DoSyncAction() 
	{
        if (!BundleGlobal.IsMobile())
        {
            //Debug.Log("sdSummonBehaviourAction::DoSyncAction");
        }

		if (mSummonLevelArea == null)
		{
			GameObject kSummonLevelAreaObject = GameObject.Find(mLevelAreaName);
			if (kSummonLevelAreaObject)
				mSummonLevelArea = kSummonLevelAreaObject.GetComponent<sdLevelArea>();

			if (mSummonLevelArea == null)
				return;
		}
		
		sdSummonBehaviourAction kDelaySummonBehaviourAction = this.Clone() as sdSummonBehaviourAction;
		kDelaySummonBehaviourAction.TimeStamp = Time.time;
		kDelaySummonBehaviourAction.mSummonLevelArea = this.mSummonLevelArea;
		
		mBehaviourAdvancedState.AddBehaviourActionToDelayList(kDelaySummonBehaviourAction);
	}
	
	// 触发动作(延迟调用,继承自sdBehaviourAction)aa
	public override void DoAsyncAction() 
	{
		if (mCount == 0)
			return;

		mDelayTime -= Time.deltaTime;
		if (mDelayTime <= 0)
		{
			mSummonLevelArea.OnTriggerHitted(null, new int[4] { 0, 0, 0, 0 });

			mDelayTime = mIntervalTime;

			--mCount;
		}	
	}
}

/// <summary>
/// 释放技能事件aa
///	 1.按照指定的概率计算需要释放的技能aa
///	 2.目标在攻击范围内时释放该技能aa
/// </summary>
public class sdSkillBehaviourAction : sdBehaviourAction
{
	//
	protected class SkillInfo : System.ICloneable
	{
		protected int mSkillID;
		public int SkillID
		{
			set { mSkillID = value; }
			get { return mSkillID; }
		}

		protected float mSkillProbility;
		public float SkillProbility
		{
			set { mSkillProbility = value; }
			get { return mSkillProbility; }
		}

		public virtual object Clone()
		{
			SkillInfo kSkillInfo = new SkillInfo();
			kSkillInfo.mSkillID = this.mSkillID;
			kSkillInfo.mSkillProbility = this.mSkillProbility;

			return kSkillInfo;
		}
	}

	// 技能组概率aa
	protected float mSkillGroupProbility = 1.0f;
	public float SkillGroupProbility
	{
		set { mSkillGroupProbility = value;}
		get { return mSkillGroupProbility;}
	}

	// 执行时是否判断施法距离aa
	protected bool mDetectDistance = false;
	public bool DetectDistance
	{
		set { mDetectDistance = value; }
		get { return mDetectDistance; }
	}

	// 技能列表aa
	protected List<SkillInfo> mSkillInfoGroup = new List<SkillInfo>();

	//
	public sdSkillBehaviourAction()
	{
		mBAType = EBehaviourActionType.enBAT_Skill;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdSkillBehaviourAction kSkillBehaviourAction = new sdSkillBehaviourAction();
		kSkillBehaviourAction.mCount = this.Count;
		kSkillBehaviourAction.mDelayTime = this.mDelayTime;
		kSkillBehaviourAction.mIntervalTime = this.mIntervalTime;
		kSkillBehaviourAction.mTimeStamp = this.mTimeStamp;
		kSkillBehaviourAction.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kSkillBehaviourAction.mSkillGroupProbility = this.mSkillGroupProbility;
		kSkillBehaviourAction.mDetectDistance = this.mDetectDistance;

		foreach (SkillInfo kSkillInfo in mSkillInfoGroup)
		{
			kSkillBehaviourAction.mSkillInfoGroup.Add(kSkillInfo.Clone() as SkillInfo);
		}
		
		return kSkillBehaviourAction;
	}

	// 加入新的技能aa
	public void AddSkill(int iSkillID, float fSkillProbility)
	{
		SkillInfo kSkillInfo = new SkillInfo();
		kSkillInfo.SkillID = iSkillID;
		kSkillInfo.SkillProbility = fSkillProbility;

		mSkillInfoGroup.Add(kSkillInfo);
	}

	// 随机一个技能aa
	public int RandomSkill()
	{
		int iSkillID = 0;
		float fRandom = Random.Range(0, 10000) / 10000.0f;
		if (fRandom <= mSkillGroupProbility)
		{
			float fProbility = fRandom;
			foreach (SkillInfo kSkillInfo in mSkillInfoGroup)
			{
				if (fProbility < kSkillInfo.SkillProbility)
				{
					iSkillID = kSkillInfo.SkillID;
					break;
				}
				else
				{
					fProbility -= kSkillInfo.SkillProbility;
				}
			}
		}

		return iSkillID;
	}
	
	// 触发动作(触发条件调用,继承自sdBehaviourAction)aa
	public override void DoSyncAction() 
	{
        if (!BundleGlobal.IsMobile())
        {
            //Debug.Log("sdSkillBehaviourAction::DoSyncAction");
        }

		sdDelaySkillBehaviourAction kDelaySkillBehaviourAction = new sdDelaySkillBehaviourAction();
		kDelaySkillBehaviourAction.Count = this.Count;
		kDelaySkillBehaviourAction.DelayTime = this.mDelayTime;
		kDelaySkillBehaviourAction.IntervalTime = this.mIntervalTime;
		kDelaySkillBehaviourAction.TimeStamp = this.mTimeStamp;
		kDelaySkillBehaviourAction.BehaviourAdvancedState = this.mBehaviourAdvancedState;
		
		kDelaySkillBehaviourAction.SkillBehavuourAction = this;

		mBehaviourAdvancedState.AddBehaviourActionToDelayList(kDelaySkillBehaviourAction);
	}
	
	// 触发事件aa
	public override void DoAsyncAction() 
	{

	}
}

/// <summary>
/// 延迟释放技能aa
/// </summary>
public class sdDelaySkillBehaviourAction : sdBehaviourAction
{
	// 所属技能事件aa
	protected sdSkillBehaviourAction mSkillBehavuourAction =  null;
	public sdSkillBehaviourAction SkillBehavuourAction
	{
		set { mSkillBehavuourAction = value; }
		get { return mSkillBehavuourAction; }
	}

	// 当前待释放的技能aa
	protected int mCurrentSkillID = -1;

	// 触发事件aa
	public override void DoAsyncAction()
	{
		if (mCount == 0)
			return;

		mDelayTime -= Time.deltaTime;
		if (mDelayTime <= 0)
		{
			// 当前技能不存在则随机一个技能aa
			if (mCurrentSkillID == -1)
				mCurrentSkillID = mSkillBehavuourAction.RandomSkill();

			// 检查技能范围,尝试释放技能aa
			bool bCastSkill = false;
			if (mCurrentSkillID > 0)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				sdGameMonster kMonster = kMonsterAutoFight.Monster;

				if (mSkillBehavuourAction.DetectDistance)
				{
					sdSkill kSkill = kMonster.skillTree.getSkill(mCurrentSkillID);
                    if (kSkill != null)
                    {
                        sdBaseState kState = kSkill.actionStateList[0];
                        float fDistance = (int)kState.stateData["CastDistance"] * 0.001f;
                        HeaderProto.ESkillObjType eTergetType = (HeaderProto.ESkillObjType)kState.stateData["byTargetType"];

                        float fCurrentDistance = 0.0f;
                        sdBehaviourStateBlock kBehaviourStateBlock = mBehaviourAdvancedState.BehaviourStateBlock;
                        if (eTergetType == HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY)
                            fCurrentDistance = kBehaviourStateBlock.UpdateDistanceOfNearestEnemy(mBehaviourAdvancedState);
                        else
                            fCurrentDistance = kBehaviourStateBlock.UpdateDistanceOfNearestFriend(mBehaviourAdvancedState);

                        if (fCurrentDistance < fDistance)
                        {
                            kMonster.CastSkill(mCurrentSkillID);
                            bCastSkill = true;
                        }
                    }
                    else
                    {
                        if (Application.isEditor)
                        {
                            Debug.Log("skilltree can't find skill=" + mCurrentSkillID);
                        }
                    }
				}
				else
				{
					kMonster.CastSkill(mCurrentSkillID);
					bCastSkill = true;
				}
			}

			// 释放技能成功则延迟一个mIntervalTime,否则仅仅延迟2s检查一次直到释放成功aa
			if (bCastSkill)
			{
				mCurrentSkillID = -1;
				mDelayTime = mIntervalTime;

				--mCount;
			}
			else
			{
				mDelayTime = 2.0f;
			}	
		}
	}
}

/// <summary>
/// 触发动作类型aa
/// </summary>
public enum EBuffBehaviourActionTargetType
{
	enBBATT_Self,
	enBBATT_Target,
}

/// <summary>
/// 状态事件aa
/// </summary>
public class sdBuffBehaviourAction : sdBehaviourAction
{
	// 状态IDaa
	protected int mBuffID;
	public int BuffID
	{
		get { return mBuffID;}
		set { mBuffID = value;}
	}

	// 状态概率aa
	protected float mBuffProbility = 1.0f;
	public float BuffProbility
	{
		get { return mBuffProbility; }
		set { mBuffProbility = value; }
	}

	// 状态目标类型aa
	protected EBuffBehaviourActionTargetType mBuffBehaviourActionTargetType = EBuffBehaviourActionTargetType.enBBATT_Self;
	public EBuffBehaviourActionTargetType BuffBehaviourActionTargetType
	{
		get { return mBuffBehaviourActionTargetType; }
		set { mBuffBehaviourActionTargetType = value; }
	}

	//
	public sdBuffBehaviourAction()
	{
		mBAType = EBehaviourActionType.enBAT_Buff;	
	}
	
	// 接口实现(ICloneable的接口)aa
	public override object Clone()
	{
		sdBuffBehaviourAction kBuffBehaviourAction = new sdBuffBehaviourAction();
		kBuffBehaviourAction.mCount = this.Count;
		kBuffBehaviourAction.mDelayTime = this.mDelayTime;
		kBuffBehaviourAction.mIntervalTime = this.mIntervalTime;
		kBuffBehaviourAction.mTimeStamp = this.mTimeStamp;
		kBuffBehaviourAction.mBehaviourAdvancedState = this.mBehaviourAdvancedState;
		
		kBuffBehaviourAction.mBuffID = this.mBuffID;
		kBuffBehaviourAction.mBuffProbility = this.mBuffProbility;
		kBuffBehaviourAction.mBuffBehaviourActionTargetType = this.mBuffBehaviourActionTargetType;
		
		return kBuffBehaviourAction;
	}
	
	// 触发动作(触发条件调用,继承自sdBehaviourAction)aa
	public override void DoSyncAction() 
	{
//		if (!BundleGlobal.IsMobile())
//		{
//			Debug.Log ("sdBuffBehaviourAction::DoSyncAction");
//		}

		float fRandom = UnityEngine.Random.Range(0, 10000) / 10000.0f;
		if (fRandom <= mBuffProbility)
		{
			sdBuffBehaviourAction kDelayBuffBehaviourAction = this.Clone() as sdBuffBehaviourAction;
			kDelayBuffBehaviourAction.TimeStamp = Time.time;

			mBehaviourAdvancedState.AddBehaviourActionToDelayList(kDelayBuffBehaviourAction);
		}
	}
	
	// 触发事件aa
	public override void DoAsyncAction() 
	{
		if (mCount == 0)
			return;

		mDelayTime -= Time.deltaTime;
		if (mDelayTime <= 0)
		{
			MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
			if (mBuffBehaviourActionTargetType == EBuffBehaviourActionTargetType.enBBATT_Self)
			{
				kMonsterAutoFight.Actor.AddBuff(mBuffID, -1, kMonsterAutoFight.Actor);
			}
			else
			{
				if (mBehaviourAdvancedState.BehaviourStateBlock.Target != null)
					mBehaviourAdvancedState.BehaviourStateBlock.Target.AddBuff(mBuffID, -1, kMonsterAutoFight.Actor);
			}

			mDelayTime = mIntervalTime;

			--mCount;
		}
	}
}

