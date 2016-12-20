using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 处理触发AIaa
/// </summary>
public class sdBehaviourAdvancedState : object
{
	// 战斗系统配接器aa
	protected MonsterAutoFight mMonsterAutoFight; 
	public MonsterAutoFight MonsterAutoFightComponent
	{
		get { return mMonsterAutoFight;}	
	}
	
	// 触发状态块aa
	protected sdBehaviourStateBlock mBehaviourStateBlock = new sdBehaviourStateBlock();
	public sdBehaviourStateBlock BehaviourStateBlock
	{
		get { return mBehaviourStateBlock;}	
	}
	
	// 触发行为树aa
	protected sdBehaviourEventNode mBehaviourEventTree = null;
	
	// 延迟行为列表aa
	protected List<sdBehaviourAction> mDelayhaviourActionList = new List<sdBehaviourAction>();
	
	// 设置战斗系统配接器aa
	public void SetMonsterAutoFight(MonsterAutoFight kMonsterAutoFight)
	{
		if (mMonsterAutoFight == kMonsterAutoFight)
			return;

		if (mMonsterAutoFight != null)	
		{
			sdGameMonster kMonster = mMonsterAutoFight.Monster;
			kMonster.NotifySummoned -= NotifySummoned;
			kMonster.NotifyKilled -= NotifyKilled;
			kMonster.NotifyAddHP -= NotifyAddHP;
			kMonster.NotifyChangeTarget -= NotifyChangeTarget;
			kMonster.NotifyAddDebuffState -= NotifyAddDebuffState;
		}

		if (kMonsterAutoFight != null)
		{
			sdGameMonster kMonster = kMonsterAutoFight.Monster;	
			kMonster.NotifySummoned += NotifySummoned;
			kMonster.NotifyKilled += NotifyKilled;	
			kMonster.NotifyAddHP += NotifyAddHP;
			kMonster.NotifyChangeTarget += NotifyChangeTarget;
			kMonster.NotifyAddDebuffState += NotifyAddDebuffState;
		}

		mMonsterAutoFight = kMonsterAutoFight;
		
		int iAIID = (int)kMonsterAutoFight.Monster.Property["AIID"];
		mBehaviourEventTree = sdAITable.GetSingleton().GetBehaviourEventTree(iAIID);
        if (mBehaviourEventTree != null)
            mBehaviourEventTree.SetBehaviourAdvancedState(this);
	}
	
	// 被召唤回调aa
	protected void NotifySummoned(sdActorInterface kActor)
	{
		if (mBehaviourEventTree == null)
			return;

		mBehaviourStateBlock.OnSummoned(kActor);
		SyncUpdateBehaviourTree();
	}
	
	// 死亡回调aa
	protected void NotifyKilled(sdActorInterface kActor)
	{
		if (mBehaviourEventTree == null)
			return;

		mBehaviourStateBlock.OnKilled(kActor);
		SyncUpdateBehaviourTree();
	}
	
	// 血量改变回调aa
	protected void NotifyAddHP(sdActorInterface kActor, int iHP)
	{
		if (mBehaviourEventTree == null)
			return;

		mBehaviourStateBlock.OnAddHP(kActor, iHP);
		SyncUpdateBehaviourTree();
	}
	
	// 切换目标回调(包括第一次发现目标和丢失目标)aa
	protected void NotifyChangeTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		if (mBehaviourEventTree == null)
			return;

		if (kPreviousTarget != kTarget)
		{
			mBehaviourStateBlock.OnChangeTarget(kActor, kPreviousTarget, kTarget);
			SyncUpdateBehaviourTree();
		}
	}
	
	// 获取状态aa
	protected void NotifyAddDebuffState(sdActorInterface kActor, HeaderProto.ECreatureActionState eState)
	{
		if (mBehaviourEventTree == null)
			return;

		mBehaviourStateBlock.OnAddDebuffState(kActor, eState);
		SyncUpdateBehaviourTree();
	}
		
	// 触发状态发生改变,更新触发行为树aa
	protected void SyncUpdateBehaviourTree()
	{
		if (mBehaviourEventTree == null)
			return;
		
		mBehaviourEventTree.SyncUpdateBehaviourTrigger(mBehaviourStateBlock);
	}
	
	// 更新触发行为树aa
	public void UpdateBehaviourTree()
	{
		if (mBehaviourEventTree == null)
			return;
		
		mBehaviourEventTree.UpdateBehaviourEvent();
		
		foreach (sdBehaviourAction kBehaviourAction in mDelayhaviourActionList)
		{
			kBehaviourAction.DoAsyncAction();
		}

		for (int i= 0; i < mDelayhaviourActionList.Count;)
		{
			sdBehaviourAction kBehaviourAction = mDelayhaviourActionList[i];
			if (kBehaviourAction.Count <= 0)
			{
				mDelayhaviourActionList.RemoveAt(i);	
			}
			else
			{
				 ++i;
			}	
		}
	}
	
	// 添加触发事件到延迟执行列表aa
	public void AddBehaviourActionToDelayList(sdBehaviourAction kBehaviourAction)
	{
		mDelayhaviourActionList.Add(kBehaviourAction);
	}
}

/// <summary>
/// 触发事件,包含多个触发条件和多个触发动作aa
/// </summary>
public class sdBehaviourEvent : System.ICloneable
{
	// 触发事件IDaa
	protected int mID = 0;
	public int ID
	{
		set { mID = value;}
		get { return mID;}	
	}

	// 触发事件父节点aa
	protected sdBehaviourEventNode mParentBehaviourEventNode = null;
	public sdBehaviourEventNode ParentBehaviourEventNode
	{
		set { mParentBehaviourEventNode = value; }
		get { return mParentBehaviourEventNode; }	
	}
	
	// 触发条件列表aa
	protected List<sdBehaviourTrigger> mBehaviourTriggerList = new List<sdBehaviourTrigger>();
	
	// 触发动作列表aa
	protected List<sdBehaviourAction> mBehaviourActionList = new List<sdBehaviourAction>();
	
	// 触发状态列表aa
	protected List<sdBehaviourState> mBehaviourStateList = new List<sdBehaviourState>();
	
	// 最大触发次数aa
	protected int mMaxUseCount = -1;
	public int MaxUseCount
	{
		set { mMaxUseCount = value;}
		get { return mMaxUseCount;}
	}
	
	// 已触发次数aa
	protected int mUseCount = 0;

	// 事件进入时间aa
	protected float mTriggerEnterTime = 0.0f;
	public float TriggerEnterTime
	{
		get { return mTriggerEnterTime; }
	}
	
	// 标记当前是否进入事件节点aa
	protected bool mIsTriggered = false;
	public bool IsTriggered
	{
		get { return mIsTriggered;}
	}

	// 触发模块aa
	protected sdBehaviourAdvancedState mBehaviourAdvancedState = null;
	
	// 接口实现(ICloneable的接口)
	public virtual object Clone()
	{
		sdBehaviourEvent kBehaviourEvent = new sdBehaviourEvent();
		
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourEvent.AddBehaviourTrigger(kBehaviourTrigger.Clone() as sdBehaviourTrigger);
		}
		
		foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
		{
			kBehaviourEvent.AddBehaviourAction(kBehaviourAction.Clone() as sdBehaviourAction);	
		}
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourEvent.AddBehaviourState(kBehaviourState.Clone() as sdBehaviourState);	
		}
		
		kBehaviourEvent.ID = this.ID;
		kBehaviourEvent.MaxUseCount = this.MaxUseCount;
		kBehaviourEvent.mBehaviourAdvancedState = this.mBehaviourAdvancedState;
		
		return kBehaviourEvent;
	}
	
	// 设置模块指针aa
	public virtual void SetBehaviourAdvancedState(sdBehaviourAdvancedState kBehaviourAdvancedState)
	{
		mBehaviourAdvancedState = kBehaviourAdvancedState;
		
		foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
		{
			kBehaviourAction.BehaviourAdvancedState = kBehaviourAdvancedState;
		}
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourState.BehaviourAdvancedState = kBehaviourAdvancedState;
		}
	}
	
	// 添加触发条件aa
	public void AddBehaviourTrigger(sdBehaviourTrigger kBehaviourTrigger)
	{
		kBehaviourTrigger.ParentBehaviourEvent = this;
		kBehaviourTrigger.NotifyEnterTrigger += NotifyEnterTrigger;
		kBehaviourTrigger.NotifyLeaveTrigger += NotifyLeaverTrigger;
		mBehaviourTriggerList.Add(kBehaviourTrigger);
	}
	
	// 添加触发行为aa
	public void AddBehaviourAction(sdBehaviourAction kBehaviourAction)
	{
		mBehaviourActionList.Add(kBehaviourAction);	
	}
	
	// 添加触发状态aa
	public void AddBehaviourState(sdBehaviourState kBehaviourState)
	{
		mBehaviourStateList.Add(kBehaviourState);	
	}
	
	// 更新触发条件(发生触发事件时调用)aa
	public virtual void SyncUpdateBehaviourTrigger(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourTrigger.SyncUpdate(kBehaviourStateBlock);
		}
	}
	
	// 更新触发行为树(每帧调用)aa
	public virtual void UpdateBehaviourEvent()
	{
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourTrigger.Update();
		}

		if (mIsTriggered)
		{
			foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
			{
				kBehaviourState.UpdateState();
			}
		}
	}

	// 进入触发条件被触发(所有的进入条件都被触发)aa
	protected bool mIsInNotifyEnterTrigger = false;	//< 标记是否进入触发处理,防止递归调用aa
	protected void NotifyEnterTrigger(sdBehaviourTrigger kCurrentBehaviourTrigger)
	{
		if (mIsInNotifyEnterTrigger)
			return;

		mIsInNotifyEnterTrigger = true;

		bool bIsAllTriggered = true;
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			if (kBehaviourTrigger.IsEnterTrigger)	
			{
				if (!kBehaviourTrigger.IsTriggered)
				{
					bIsAllTriggered = false;
					break;
				}
			}
		}

		bool bReachMaxCount = (mMaxUseCount > 0 ) && (mUseCount >= mMaxUseCount);
		if (bIsAllTriggered && !bReachMaxCount)
		{
			++mUseCount;
			EnterBehaviourEvent();
		}

		mIsInNotifyEnterTrigger = false;
	}
	
	// 离开触发条件(某一个离开条件被触发)aa
	protected bool mIsInNotifyLeaveTrigger = false;	//< 标记是否进入触发处理,防止递归调用aa
	protected void NotifyLeaverTrigger(sdBehaviourTrigger kCurrentBehaviourTrigger)
	{
		if (mIsInNotifyLeaveTrigger)
			return;

		mIsInNotifyLeaveTrigger = true;
		
		bool bIsAllTriggered = true;
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			if (kBehaviourTrigger.IsLeaveTrigger)
			{
				if (!kBehaviourTrigger.IsTriggered)	
				{
					bIsAllTriggered = false;
					break;
				}
			}
		}
		
		if (!bIsAllTriggered)
		{
			LeaveBehaviourEvent();
		}

		mIsInNotifyLeaveTrigger = false;
	}
	
	// 进入触发事件aa
	public virtual void EnterBehaviourEvent()
	{
		if (mIsTriggered != true)
		{
			mIsTriggered = true;
			mTriggerEnterTime = Time.time;
			OnEnterBehaviourEvent();
			
			foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
			{
				kBehaviourAction.DoSyncAction();
			}	
			
			foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
			{
				kBehaviourState.DoEnterState();
			}	
		}	
	}
	
	// 离开触发事件aa
	public virtual void LeaveBehaviourEvent()
	{
		if (mIsTriggered != false)
		{
			foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
			{
				kBehaviourState.DoLeaveState();	//< 离开状态aa
			}

			OnLeaveBehaviourEvent();
			mIsTriggered = false;
		}
	}
	
	// 事件进入回调aa
	protected virtual void OnEnterBehaviourEvent()
	{
//		if (!BundleGlobal.IsMobile())
//		{
//			Debug.Log("sdBehaviourEvent::EnterBehaviourEvent-->" + mID.ToString());
//		}
	}
	
	// 事件离开回调aa
	protected virtual void OnLeaveBehaviourEvent()
	{
//		if (!BundleGlobal.IsMobile())
//		{
//			Debug.Log("sdBehaviourEvent::LeaveBehaviourEvent-->" + mID.ToString());
//		}
	}
	
	// 重新设置触发事件aa
	public virtual void ForceReset()
	{
		mUseCount = 0;
		mIsTriggered = false;

		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourTrigger.ForceReset();
		}	
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourState.ForceReset();
		}
	}

	// 重新检查触发事件但不触发aa
	public virtual void ForceRecheck(bool bTrigger)
	{
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{		
			kBehaviourTrigger.ForceRecheck(mBehaviourAdvancedState.BehaviourStateBlock, bTrigger);
		}
	}
}

/// <summary>
/// 触发状态节点aa
/// </summary>
public class sdBehaviourEventNode : sdBehaviourEvent
{	
	// 子节点aa
	protected List<sdBehaviourEventNode> mChildBehaviourEventNodeList = new List<sdBehaviourEventNode>();
	
	// 触发事件列表aa
	protected List<sdBehaviourEvent> mBehaviourEventList = new List<sdBehaviourEvent>();

	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdBehaviourEventNode kBehaviourEventNode = new sdBehaviourEventNode();
		
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourEventNode.AddBehaviourTrigger(kBehaviourTrigger.Clone() as sdBehaviourTrigger);
		}
		
		foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
		{
			kBehaviourEventNode.AddBehaviourAction(kBehaviourAction.Clone() as sdBehaviourAction);	
		}
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourEventNode.AddBehaviourState(kBehaviourState.Clone() as sdBehaviourState);	
		}
		
		foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
		{
			kBehaviourEventNode.AddBehaviourEvent(kBehaviourEvent.Clone() as sdBehaviourEvent);	
		}
		
		foreach (sdBehaviourEventNode kChildBehaviourEventNode in mChildBehaviourEventNodeList)
		{
			kChildBehaviourEventNode.AddChildBehaviourEventNode(kChildBehaviourEventNode.Clone() as sdBehaviourEventNode);	
		}
		
		kBehaviourEventNode.ID = this.ID;
		kBehaviourEventNode.MaxUseCount = this.MaxUseCount;
		kBehaviourEventNode.mBehaviourAdvancedState = this.mBehaviourAdvancedState;
		
		return kBehaviourEventNode;
	}	
	
	// 设置模块指针(继承自sdBehaviourEvent)aa
	public override void SetBehaviourAdvancedState(sdBehaviourAdvancedState kBehaviourAdvancedState)
	{
		base.SetBehaviourAdvancedState(kBehaviourAdvancedState);

		foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
		{
			kBehaviourEvent.SetBehaviourAdvancedState(kBehaviourAdvancedState);
		}
		
		foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
		{
			kBehaviourEventNode.SetBehaviourAdvancedState(kBehaviourAdvancedState);
		}
	}

	// 添加子节点aa
	public virtual void AddChildBehaviourEventNode(sdBehaviourEventNode kBehaviourEventNode)
	{
		kBehaviourEventNode.ParentBehaviourEventNode = this;
		mChildBehaviourEventNodeList.Add(kBehaviourEventNode);	
	}
	
	// 添加触发事件aa
	public virtual void AddBehaviourEvent(sdBehaviourEvent kBehaviourEvent)
	{
		kBehaviourEvent.ParentBehaviourEventNode = this;
		mBehaviourEventList.Add(kBehaviourEvent);	
	}
	
	// 更新触发条件(发生触发事件时调用)aa
	public override void SyncUpdateBehaviourTrigger(sdBehaviourStateBlock kBehaviourStateBlock)
	{
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourTrigger.SyncUpdate(kBehaviourStateBlock);
		}
		
		if (mIsTriggered)
		{
			foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
			{
				kBehaviourEvent.SyncUpdateBehaviourTrigger(mBehaviourAdvancedState.BehaviourStateBlock);	
			}	
			
			foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
			{
				kBehaviourEventNode.SyncUpdateBehaviourTrigger(mBehaviourAdvancedState.BehaviourStateBlock);
			}
		}
	}
	
	// 更新触发行为树(继承自sdBehaviourEvent)aa
	public override void UpdateBehaviourEvent()
	{
		base.UpdateBehaviourEvent();
		
		if (mIsTriggered)
		{
			foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
			{
				kBehaviourEvent.UpdateBehaviourEvent();	
			}	
			
			foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
			{
				kBehaviourEventNode.UpdateBehaviourEvent();
			}
		}
	}

	// 进入触发事件,强制检查所有子节点(继承自sdBehaviourEvent)aa
	public override void EnterBehaviourEvent()
	{
		if (mIsTriggered != true)
		{
			mIsTriggered = true;
			mTriggerEnterTime = Time.time;
			OnEnterBehaviourEvent();

			// 触发节点所属Action和State对象aa
			foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
			{
				kBehaviourAction.DoSyncAction();
			}

			foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
			{
				kBehaviourState.DoEnterState();
			}

			// 检查节点子树(状态类型的触发事件)aa
			ForceRecheckSubtree(true);
		}
	}

	// 离开触发事件,强制离开所有子节点(继承自sdBehaviourEvent)aa
	public override void LeaveBehaviourEvent()
	{
		if (mIsTriggered != false)
		{
			foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
			{
				kBehaviourTrigger.ForceReset();				//< 重设触发条件aa
			}
			
			foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
			{
				kBehaviourState.DoLeaveState();				//< 离开触发状态aa
				kBehaviourState.ForceReset();				//< 重新设置触发状态aa
			}
			
			foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
			{
				kBehaviourEvent.LeaveBehaviourEvent();		//< 离开触发事件aa
				kBehaviourEvent.ForceReset();				//< 重新设置触发事件aa
			}	
			
			foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
			{
				kBehaviourEventNode.LeaveBehaviourEvent();	//< 离开触发节点aa
				kBehaviourEventNode.ForceReset();			//< 重新设置触发节点aa
			}

			OnLeaveBehaviourEvent();
			mIsTriggered = false;
		}
	}

	// 重新设置触发事件(继承自sdBehaviourEvent)aa
	public override void ForceReset()
	{
		mUseCount = 0;
		mIsTriggered = false;

		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourTrigger.ForceReset();
		}	
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourState.ForceReset();
		}
		
		foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
		{
			kBehaviourEvent.ForceReset();
		}	
			
		foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
		{
			kBehaviourEventNode.ForceReset();
		}
	}
	
	// 重新检查触发事件(继承自sdBehaviourEvent)aa
	public override void ForceRecheck(bool bTrigger)
	{
		base.ForceRecheck(bTrigger);

		ForceRecheckSubtree(bTrigger);
	}

	// 重新检查子事件和子事件节点aa
	protected void ForceRecheckSubtree(bool bTrigger)
	{
		if (mIsTriggered)
		{
			foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
			{
				kBehaviourEvent.ForceRecheck(bTrigger);
			}

			foreach (sdBehaviourEventNode kBehaviourEventNode in mChildBehaviourEventNodeList)
			{
				kBehaviourEventNode.ForceRecheck(bTrigger);
			}
		}
	}
}

/// <summary>
/// 触发行为树根节点aa
/// </summary>
public class sdBehaviourEventRootNode : sdBehaviourEventNode
{
	// 行为树名称aa
	protected string mName;
	public string Name
	{
		set { mName = value;}
		get { return mName;}	
	}	
	
	// 接口实现(ICloneable的接口)
	public override object Clone()
	{
		sdBehaviourEventRootNode kBehaviourEventRootNode = new sdBehaviourEventRootNode();
		
		foreach (sdBehaviourTrigger kBehaviourTrigger in mBehaviourTriggerList)
		{
			kBehaviourEventRootNode.AddBehaviourTrigger(kBehaviourTrigger.Clone() as sdBehaviourTrigger);
		}
		
		foreach (sdBehaviourAction kBehaviourAction in mBehaviourActionList)
		{
			kBehaviourEventRootNode.AddBehaviourAction(kBehaviourAction.Clone() as sdBehaviourAction);	
		}
		
		foreach (sdBehaviourState kBehaviourState in mBehaviourStateList)
		{
			kBehaviourEventRootNode.AddBehaviourState(kBehaviourState.Clone() as sdBehaviourState);	
		}
		
		foreach (sdBehaviourEvent kBehaviourEvent in mBehaviourEventList)
		{
			kBehaviourEventRootNode.AddBehaviourEvent(kBehaviourEvent.Clone() as sdBehaviourEvent);	
		}
		
		foreach (sdBehaviourEventNode kChildBehaviourEventNode in mChildBehaviourEventNodeList)
		{
            kBehaviourEventRootNode.AddChildBehaviourEventNode(kChildBehaviourEventNode.Clone() as sdBehaviourEventNode);	
		}
		
		kBehaviourEventRootNode.ID = this.ID;
		kBehaviourEventRootNode.mBehaviourAdvancedState = this.mBehaviourAdvancedState;
		kBehaviourEventRootNode.Name = this.Name;
		
		return kBehaviourEventRootNode;
	}	
}