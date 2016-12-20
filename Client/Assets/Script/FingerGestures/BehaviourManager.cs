using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 行为管理器,负责AI整体行为aa
/// </summary>
public class sdBehaviourManager : TSingleton<sdBehaviourManager>
{
	// 逻辑节点列表aa
	protected List<sdBehaviourNode> mBehaviourNodeList = new List<sdBehaviourNode>();

	// 获取指定逻辑节点aa
	public sdBehaviourNode GetBehaviourNode(int iID)
	{
		foreach (sdBehaviourNode kBehaviourNode in mBehaviourNodeList)
		{
			if (kBehaviourNode.ID == iID)
				return kBehaviourNode;
		}

		sdBehaviourNode kNewBehaviourNode = new sdBehaviourNode();
		kNewBehaviourNode.ID = iID;

		mBehaviourNodeList.Add(kNewBehaviourNode);

		return kNewBehaviourNode;
	}

	// 进入地图消息回调aa
	public void OnEnterLevel()
	{

	}

	// 离开地图消息回调aa
	public void OnLeaveLevel()
	{
		foreach (sdBehaviourNode kBehaviourNode in mBehaviourNodeList)
		{
			kBehaviourNode.RemoveAllActor();
		}

		mBehaviourNodeList.Clear();
	}
}

/// <summary>
/// 群体行为逻辑节点(通信节点)aa
///	 1.接收成员角色受伤消息并发布aa
///	 2.接收成员角色切换目标消息并发布aa
/// </summary>
public class sdBehaviourNode
{
	// 逻辑节点IDaa
	protected int mID = -1;
	public int ID
	{
		get { return mID; }
		set { mID = value; }
	}

	// 所属角色列表aa
	protected List<sdGameActor> mActorList = new List<sdGameActor>();

	// 所属角色切换目标aa
	public NotifyChangeTargetDelegate NotifyMemberChangeTarget;

	// 所属角色掉血事件aa
	public NotifyHurtDelegate NotifyMemberHurt; 

	// 添加角色到角色列表aa
	public void AddActor(sdGameActor kActor)
	{
		if (!mActorList.Contains(kActor))
		{
			mActorList.Add(kActor);

			kActor.NotifyChangeTarget += NotifyChangeTarget;
			kActor.NotifyHurt += NotifyHurt;

			kActor.OnAddToBehaviourNode(this);
		}
	}

	// 从角色列表移除角色aa
	public void RemoveActor(sdGameActor kActor)
	{
		if (mActorList.Contains(kActor))
		{
			kActor.OnRemoveFromBehaviourNode(this);

			kActor.NotifyChangeTarget -= NotifyChangeTarget;
			kActor.NotifyHurt -= NotifyHurt;

			mActorList.Remove(kActor);	
		}
	}

	// 从角色列表移除所有角色aa
	public void RemoveAllActor()
	{
		foreach (sdGameActor kActor in mActorList)
		{
			kActor.OnRemoveFromBehaviourNode(this);

			kActor.NotifyChangeTarget -= NotifyChangeTarget;
			kActor.NotifyHurt -= NotifyHurt;
		}

		mActorList.Clear();
	}

	// 节点中的怪物掉血事件回调aa
	protected void NotifyChangeTarget(sdActorInterface kActor, sdActorInterface kPreciousTarget, sdActorInterface kTarget)
	{
		if (NotifyMemberChangeTarget != null)
			NotifyMemberChangeTarget(kActor, kPreciousTarget, kTarget);
	}

	// 节点中的怪物掉血事件回调aa
	protected void NotifyHurt(sdActorInterface kActor, sdActorInterface kAttacker, int iHP)
	{
		if (NotifyMemberHurt != null)
			NotifyMemberHurt(kActor, kAttacker, iHP);
	}
}