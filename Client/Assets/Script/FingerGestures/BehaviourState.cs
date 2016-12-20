using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 触发状态类型aa
/// </summary>
public enum EBehaviourStateType
{
	enBST_Unknown				= 0,
	enBST_Summon				= 2,
	enBST_Skill					= 4,
	enBST_Buff					= 5,
	enBST_CD_Skill				= 6,
	
	enBST_Disable_Skill			= 101,
	enBST_Disable_Mesh			= 102,
	enBST_Force_Animation		= 103,
	enBST_Change_Layer			= 104,
	enBST_Hide					= 105,
	enBST_Manual_Buff			= 106,
	enBST_Change_BattleDistance = 107,
}

/// <summary>
/// 触发状态aa
/// </summary>
public class sdBehaviourState : System.ICloneable
{
	// 状态类型aa
	protected EBehaviourStateType mBSType = EBehaviourStateType.enBST_Unknown;
	public EBehaviourStateType BSType
	{
		get { return mBSType;}
	}
	
	// 触发时间戳aa
	protected float mTimeStamp = 0.0f;
	public float TimeStamp
	{
		set { mTimeStamp = value;}	
		get { return mTimeStamp;}
	}
	
	// 当前是否处于状态aa
	protected bool mIsInState = false;
	public bool IsInState
	{
		get { return mIsInState;}
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
	
	// 进入触发状态aa
	public virtual void DoEnterState() 
	{
		if (mIsInState != true)
		{
			mIsInState = true;
		}
	}
	
	// 离开触发状态aa
	public virtual void DoLeaveState() 
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}
	
	// 更新触发状态aa
	public virtual void UpdateState()
	{
		if (mIsInState == true)
		{
			
		}
	}
	
	// 强制重新设置触发状态aa
	public virtual void ForceReset()
	{
		mTimeStamp = 0.0f;
		mIsInState = false;
	}
}

/// <summary>
/// 召唤状态aa
/// </summary>
public class sdSummonBehaviourState : sdBehaviourState
{
	// 召唤IDaa
	protected string mLevelAreaName;
	public string LevelAreaName
	{
		set { mLevelAreaName = value;}	
		get { return mLevelAreaName;}
	}
	
	// 动作执行次数(为负表示一直执行)aa
	protected int mCount = -1;
	public int Count
	{
		set { mCount = value; }
		get { return mCount; }
	}
	
	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}
	
	// 执行间隔aa
	protected float mIntervalTime = 2.0f;
	public float IntervalTime
	{
		set { mIntervalTime = value; }
		get { return mIntervalTime; }
	}
	
	// 当前执行次数aa
	protected int mCurrentCount = 0;
	
	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 召唤区域节点aa
	protected sdLevelArea mSummonLevelArea = null;
	
	//
	public sdSummonBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Summon;
	}
	
	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdSummonBehaviourState kSummonBehaviourState = new sdSummonBehaviourState();
		kSummonBehaviourState.mTimeStamp = this.mTimeStamp;
		kSummonBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;
		
		kSummonBehaviourState.mLevelAreaName = this.mLevelAreaName;
		kSummonBehaviourState.mCount = this.mCount;
		kSummonBehaviourState.mDelayTime = this.DelayTime;
		kSummonBehaviourState.mIntervalTime = this.IntervalTime;
		
		return kSummonBehaviourState;
	}
	
	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;
			
			mCurrentCount = mCount;
			mCurrentDelayTime = mDelayTime;

			mSummonLevelArea = null;
			GameObject kSummonLevelAreaObject = GameObject.Find(mLevelAreaName);
			if (kSummonLevelAreaObject)
				mSummonLevelArea = kSummonLevelAreaObject.GetComponent<sdLevelArea>();
		}
	}
	
	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}
	
	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			if (mCurrentCount != 0)
			{
				mCurrentDelayTime -= Time.deltaTime;
				if (mCurrentDelayTime <= 0.0f)
				{
					if (mSummonLevelArea != null)
						mSummonLevelArea.OnTriggerHitted(null, new int[4]{0,0,0,0});
					
					mCurrentDelayTime = mIntervalTime;
					
					--mCurrentCount;
				}
			}
		}
	}
}

/// <summary>
/// 释放技能状态aa
///	 1.按照指定的概率计算需要释放的技能aa
///	 2.目标在攻击范围内时释放该技能aa
/// </summary>
public class sdSkillBehaviourState : sdBehaviourState
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
		set { mSkillGroupProbility = value; }
		get { return mSkillGroupProbility; }
	}

	// 技能列表aa
	protected List<SkillInfo> mSkillInfoGroup = new List<SkillInfo>();

	// 动作执行次数(为负表示一直执行)aa
	protected int mCount = -1;
	public int Count
	{
		set { mCount = value; }
		get { return mCount; }
	}
	
	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value;}	
		get { return mDelayTime;}
	}
	
	// 执行间隔aa
	protected float mIntervalTime = 2.0f;
	public float IntervalTime
	{
		set { mIntervalTime = value;}	
		get { return mIntervalTime;}
	}

	// 执行时是否判断施法距离aa
	protected bool mDetectDistance = false;
	public bool DetectDistance
	{
		set { mDetectDistance = value; }
		get { return mDetectDistance; }
	}

	// 当前执行次数aa
	protected int mCurrentCount = 0;

	// 当前延迟执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 当前待释放的技能aa
	protected int mCurrentSkillID = -1;
	
	//
	public sdSkillBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Skill;
	}
	
	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdSkillBehaviourState kSkillBehaviourState = new sdSkillBehaviourState();
		kSkillBehaviourState.mTimeStamp = this.mTimeStamp;
		kSkillBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kSkillBehaviourState.mSkillGroupProbility = this.mSkillGroupProbility;
		kSkillBehaviourState.mCount = this.mCount;
		kSkillBehaviourState.mDelayTime = this.mDelayTime;
		kSkillBehaviourState.mIntervalTime = this.mIntervalTime;
		kSkillBehaviourState.mDetectDistance = this.mDetectDistance;

		foreach (SkillInfo kSkillInfo in mSkillInfoGroup)
		{
			kSkillBehaviourState.mSkillInfoGroup.Add(kSkillInfo.Clone() as SkillInfo);
		}
		
		return kSkillBehaviourState;
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
	protected int RandomSkill()
	{
		int iSkillID = -1;
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
	
	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState() 
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentCount = mCount;
			mCurrentDelayTime = mDelayTime;
			mCurrentSkillID = -1;
		}
	}
	
	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState() 
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}
	
	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState != true)
			return;

		if (mCurrentCount == 0)
			return;

		mCurrentDelayTime -= Time.deltaTime;
		if (mCurrentDelayTime <= 0.0f)
		{
			// 当前技能不存在则随机一个技能aa
			if (mCurrentSkillID == -1)
				mCurrentSkillID = RandomSkill();

			// 检查技能范围,尝试释放技能aa
			bool bCastSkill = false;
			if (mCurrentSkillID > 0)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				sdGameMonster kMonster = kMonsterAutoFight.Monster;

				if (mDetectDistance)
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
				mCurrentDelayTime = mIntervalTime;

				--mCurrentCount;
			}
			else
			{
				mCurrentDelayTime = 2.0f;
			}
		}
	}
}

/// <summary>
/// 添加Buff状态aa
/// </summary>
public class sdBuffBehaviourState : sdBehaviourState
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

	// 状态目标aa
	protected EBuffBehaviourActionTargetType mBuffBehaviourActionTargetType = EBuffBehaviourActionTargetType.enBBATT_Self;
	public EBuffBehaviourActionTargetType BuffBehaviourActionTargetType
	{
		get { return mBuffBehaviourActionTargetType; }
		set { mBuffBehaviourActionTargetType = value; }
	}

	// 动作执行次数(为负表示一直执行)aa
	protected int mCount = -1;
	public int Count
	{
		set { mCount = value; }
		get { return mCount; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 执行间隔aa
	protected float mIntervalTime = 2.0f;
	public float IntervalTime
	{
		set { mIntervalTime = value; }
		get { return mIntervalTime; }
	}

	// 当前执行次数aa
	protected int mCurrentCount = 0;

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	//
	public sdBuffBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Buff;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdBuffBehaviourState kBuffBehaviourState = new sdBuffBehaviourState();
		kBuffBehaviourState.mTimeStamp = this.mTimeStamp;
		kBuffBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kBuffBehaviourState.mBuffID = this.mBuffID;
		kBuffBehaviourState.mBuffProbility = this.mBuffProbility;
		kBuffBehaviourState.mBuffBehaviourActionTargetType = this.mBuffBehaviourActionTargetType;
		kBuffBehaviourState.mCount = this.mCount;
		kBuffBehaviourState.mDelayTime = this.DelayTime;
		kBuffBehaviourState.mIntervalTime = this.IntervalTime;

		return kBuffBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;
			mCurrentCount = mCount;
			mCurrentDelayTime = mDelayTime;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			if (mCurrentCount != 0)
			{
				mCurrentDelayTime -= Time.deltaTime;
				if (mCurrentDelayTime <= 0.0f)
				{
					float fRandom = Random.Range(0, 10000) / 10000.0f;
					if (fRandom <= mBuffProbility)
					{
						MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
						if (kMonsterAutoFight != null)
						{
							if (mBuffBehaviourActionTargetType == EBuffBehaviourActionTargetType.enBBATT_Self)
							{
								kMonsterAutoFight.Actor.AddBuff(mBuffID, -1, kMonsterAutoFight.Actor);
							}
							else
							{
								if (mBehaviourAdvancedState.BehaviourStateBlock.Target != null)
									mBehaviourAdvancedState.BehaviourStateBlock.Target.AddBuff(mBuffID, -1, kMonsterAutoFight.Actor);
							}
						}
					}

					mCurrentDelayTime = mIntervalTime;

					--mCurrentCount;
				}
			}
		}
	}
}

/// <summary>
/// 释放技能状态:延迟指定时间之后,每当CD结束即释放大招aa
/// </summary>
public class sdCDSkillBehaviourState : sdBehaviourState
{
	// 状态IDaa
	protected int mSkillID;
	public int SkillID
	{
		get { return mSkillID; }
		set { mSkillID = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	//
	public sdCDSkillBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_CD_Skill;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdCDSkillBehaviourState kCDSkillBehaviourState = new sdCDSkillBehaviourState();
		kCDSkillBehaviourState.mTimeStamp = this.mTimeStamp;
		kCDSkillBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kCDSkillBehaviourState.mSkillID = this.mSkillID;
		kCDSkillBehaviourState.mDelayTime = this.mDelayTime;

		return kCDSkillBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;
			mCurrentDelayTime = mDelayTime;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				PetAutoFight kPetAutoFight = kMonsterAutoFight as PetAutoFight;
				if (kPetAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						int iSkillID = mSkillID;
						if (iSkillID == -1 || iSkillID == 0)
							iSkillID = (int)kMonster.Property["SpSkill"];	//< 尝试获取手动技能aa

						int iErroeCode = 0;
						kPetAutoFight.DoXPSkill(iSkillID, ref iErroeCode);

					}
				}

				mCurrentDelayTime = 0.0f;
			}
		}
	}
}

/// <summary>
/// 禁止施放指定技能状态aa
/// </summary>
public class sdDisableSkillBehaviourState : sdBehaviourState
{
	// 技能IDaa
	protected int mSkillID;
	public int SkillID
	{
		get { return mSkillID; }
		set { mSkillID = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 执行状态aa
	protected bool mExecuteStatus = false;
	protected bool mSkillStatus = true;

	//
	public sdDisableSkillBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Disable_Skill;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdDisableSkillBehaviourState kDisableSkillBehaviourState = new sdDisableSkillBehaviourState();
		kDisableSkillBehaviourState.mTimeStamp = this.mTimeStamp;
		kDisableSkillBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kDisableSkillBehaviourState.mSkillID = this.mSkillID;
		kDisableSkillBehaviourState.mDelayTime = this.mDelayTime;

		return kDisableSkillBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						if (kMonster.skillTree != null)
						{
							sdSkill kSkill = kMonster.skillTree.getSkill(mSkillID);
							if (kSkill != null)
							{
								kSkill.skillEnabled = mSkillStatus;
							}
						}
					}
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						if (kMonster.skillTree != null)
						{
							sdSkill kSkill = kMonster.skillTree.getSkill(mSkillID);
							if (kSkill != null)
							{
								mSkillStatus = kSkill.skillEnabled;
								kSkill.skillEnabled = false;
							}
						}
					}
				}
			}
		}
	}
}

/// <summary>
/// 隐藏模型状态aa
/// </summary>
public class sdDisableMeshBehaviourState : sdBehaviourState
{
	// 需要隐藏的模型名称aa
	protected string mMeshName;
	public string MeshName
	{
		get { return mMeshName; }
		set { mMeshName = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 原有的模型状态aa
	protected bool mExecuteStatus = false;
	protected bool mMeshStatus = true;

	//
	public sdDisableMeshBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Disable_Mesh;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdDisableMeshBehaviourState kDisableMeshBehaviourState = new sdDisableMeshBehaviourState();
		kDisableMeshBehaviourState.mTimeStamp = this.mTimeStamp;
		kDisableMeshBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kDisableMeshBehaviourState.mMeshName = this.mMeshName;
		kDisableMeshBehaviourState.mDelayTime = this.mDelayTime;

		return kDisableMeshBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						Renderer[] akRenderer = kMonster.RenderNode.GetComponentsInChildren<Renderer>();
						if (akRenderer != null)
						{
							for (int i = 0; i < akRenderer.GetLength(0); ++i)
							{
								Renderer kRenderer = akRenderer[i];
								if (kRenderer.gameObject.name.Equals(mMeshName))
								{
									kRenderer.enabled = mMeshStatus;
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						Renderer[] akRenderer = kMonster.RenderNode.GetComponentsInChildren<Renderer>();
						if (akRenderer != null)
						{
							for (int i = 0; i < akRenderer.GetLength(0); ++i)
							{
								Renderer kRenderer = akRenderer[i];
								if (kRenderer.gameObject.name.Equals(mMeshName))
								{
									mMeshStatus = kRenderer.enabled;
									kRenderer.enabled = false;
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}

/// <summary>
/// 强制播放指定动画状态aa
/// </summary>
public class sdAnimationBehaviourState : sdBehaviourState
{
	// 技能IDaa
	protected string mAnimationName;
	public string AnimationName
	{
		get { return mAnimationName; }
		set { mAnimationName = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 执行状态aa
	protected bool mExecuteStatus = false;

	//
	public sdAnimationBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Force_Animation;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdAnimationBehaviourState kAnimationBehaviourState = new sdAnimationBehaviourState();
		kAnimationBehaviourState.mTimeStamp = this.mTimeStamp;
		kAnimationBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kAnimationBehaviourState.mAnimationName = this.mAnimationName;
		kAnimationBehaviourState.mDelayTime = this.mDelayTime;

		return kAnimationBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						AnimationState kAnimationState = kMonster.AnimController[mAnimationName];
						if (kAnimationState != null)
						{
							kMonster.logicTSM.ForceStop();	//< 强制停止当前技能aa

							kAnimationState.layer = 11;
							kAnimationState.wrapMode = WrapMode.Once;
							kMonster.AnimController.Play(mAnimationName);
						}
					}
				}
			}
		}
	}
}

/// <summary>
/// 改变对象图层状态aa
/// </summary>
public class sdChangeLayerBehaviourState : sdBehaviourState
{
	// 需要改变图层名称aa
	protected string mLayerName;
	public string LayerName
	{
		get { return mLayerName; }
		set { mLayerName = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 原有的图层状态aa
	protected bool mExecuteStatus = false;
	protected int mOldLayerIndex = -1;

	//
	public sdChangeLayerBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Change_Layer;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdChangeLayerBehaviourState kChangeLayerBehaviourState = new sdChangeLayerBehaviourState();
		kChangeLayerBehaviourState.mTimeStamp = this.mTimeStamp;
		kChangeLayerBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kChangeLayerBehaviourState.mLayerName = this.mLayerName;
		kChangeLayerBehaviourState.mDelayTime = this.mDelayTime;

		return kChangeLayerBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
			mOldLayerIndex = -1;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						kMonster.gameObject.layer = mOldLayerIndex;
					}
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						mOldLayerIndex = kMonster.gameObject.layer;
						kMonster.gameObject.layer = LayerMask.NameToLayer(mLayerName);
					}
				}
			}
		}
	}
}

/// <summary>
/// 隐藏自身状态aa
/// </summary>
public class sdHideBehaviourState : sdBehaviourState
{
	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 原有的图层状态aa
	protected bool mExecuteStatus = false;
	protected bool mOldHideStatus = false;

	//
	public sdHideBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Hide;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdHideBehaviourState kHideBehaviourState = new sdHideBehaviourState();
		kHideBehaviourState.mTimeStamp = this.mTimeStamp;
		kHideBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kHideBehaviourState.mDelayTime = this.mDelayTime;

		return kHideBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
			mOldHideStatus = true;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						kMonster.Hide = mOldHideStatus;
					}
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						mOldHideStatus = kMonster.Hide;
						kMonster.Hide = true;
					}
				}
			}
		}
	}
}

/// <summary>
/// 手动给对象添加和移除Buff状态aa
/// </summary>
public class sdManualBuffBehaviourState : sdBehaviourState
{
	// 需要改变图层名称aa
	protected int mBuffID;
	public int BuffID
	{
		get { return mBuffID; }
		set { mBuffID = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 执行状态aa
	protected bool mExecuteStatus = false;

	//
	public sdManualBuffBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Manual_Buff;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdManualBuffBehaviourState kManualBuffBehaviourState = new sdManualBuffBehaviourState();
		kManualBuffBehaviourState.mTimeStamp = this.mTimeStamp;
		kManualBuffBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kManualBuffBehaviourState.mBuffID = this.mBuffID;
		kManualBuffBehaviourState.mDelayTime = this.mDelayTime;

		return kManualBuffBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						kMonster.AddBuff(mBuffID, 0, kMonster);
					}
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					sdGameMonster kMonster = kMonsterAutoFight.Actor as sdGameMonster;
					if (kMonster != null)
					{
						kMonster.RemoveBuff(mBuffID);
					}
				}
			}
		}
	}
}

/// <summary>
/// 修改对象战斗距离状态aa
/// </summary>
public class sdChangeBattleDistanceBehaviourState : sdBehaviourState
{
	// 需要改变图层名称aa
	protected float mBattleDistance;
	public float BattleDistance
	{
		get { return mBattleDistance; }
		set { mBattleDistance = value; }
	}

	// 延迟时间aa
	protected float mDelayTime = 0.0f;
	public float DelayTime
	{
		set { mDelayTime = value; }
		get { return mDelayTime; }
	}

	// 上次执行时间aa
	protected float mCurrentDelayTime = 0.0f;

	// 执行状态aa
	protected bool mExecuteStatus = false;
	protected float mOldBattleDistance = 100.0f;

	//
	public sdChangeBattleDistanceBehaviourState()
	{
		mBSType = EBehaviourStateType.enBST_Change_BattleDistance;
	}

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdChangeBattleDistanceBehaviourState kChangeBattleDistanceBehaviourState = new sdChangeBattleDistanceBehaviourState();
		kChangeBattleDistanceBehaviourState.mTimeStamp = this.mTimeStamp;
		kChangeBattleDistanceBehaviourState.mBehaviourAdvancedState = this.mBehaviourAdvancedState;

		kChangeBattleDistanceBehaviourState.mBattleDistance = this.mBattleDistance;
		kChangeBattleDistanceBehaviourState.mDelayTime = this.mDelayTime;

		return kChangeBattleDistanceBehaviourState;
	}

	// 进入触发状态(继承自sdBehaviourState)aa
	public override void DoEnterState()
	{
		if (mIsInState != true)
		{
			mIsInState = true;

			mCurrentDelayTime = mDelayTime;
			mExecuteStatus = false;
		}
	}

	// 离开触发状态(继承自sdBehaviourState)aa
	public override void DoLeaveState()
	{
		if (mIsInState != false)
		{
			mIsInState = false;

			if (mExecuteStatus)
			{
				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					kMonsterAutoFight.BattleDistance = mOldBattleDistance; 
				}
			}
		}
	}

	// 更新触发状态(继承自sdBehaviourState)aa
	public override void UpdateState()
	{
		if (mIsInState == true)
		{
			mCurrentDelayTime -= Time.deltaTime;
			if (mCurrentDelayTime <= 0.0f)
			{
				if (mExecuteStatus)
					return;

				mExecuteStatus = true;

				MonsterAutoFight kMonsterAutoFight = mBehaviourAdvancedState.MonsterAutoFightComponent;
				if (kMonsterAutoFight != null)
				{
					mOldBattleDistance = kMonsterAutoFight.BattleDistance;
					kMonsterAutoFight.BattleDistance = mBattleDistance; 
				}
			}
		}
	}
}