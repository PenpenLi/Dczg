using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 触发AI表aa
/// </summary>
public class sdAITable : TSingleton<sdAITable>
{
	// 预生成的触发AI事件表aa
	protected List<sdBehaviourEventNode> mAITable = new List<sdBehaviourEventNode>();
	
	// 加载AI触发表aa
	public void LoadAITable(string kPath, bool bTestMode)
	{
		if (bTestMode) 
		{
			Object kObj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/"+kPath, typeof(TextAsset));
			ResLoadParams kParam = new ResLoadParams();
			OnLoadAITable(kParam, kObj);
		} 
		else 
		{
			ResLoadParams kParam = new ResLoadParams ();
			sdResourceMgr.Instance.LoadResource (kPath, OnLoadAITable, kParam);
		}
	}
	
	// 加载AI触发表回调,解析AI触发表aa
	protected void OnLoadAITable(ResLoadParams kParam, Object kObj)
	{
		mAITable.Clear();
		
		TextAsset kText = (TextAsset)kObj;
		XmlDocument kXmlDoc = new XmlDocument();
		kXmlDoc.LoadXml(kText.text);
	
		XmlNode kRoot = kXmlDoc.SelectSingleNode("AITable");	
		if (kRoot == null)
			return;

		XmlNodeList kAIItemNodeList = kRoot.ChildNodes;
		foreach (XmlNode kAIItemNode in kAIItemNodeList)
		{
			sdBehaviourEventRootNode kBehaviourEventRootNode = LoadBehaviourEventRootNode(kAIItemNode);
			if (kBehaviourEventRootNode != null)
				mAITable.Add(kBehaviourEventRootNode);
		}
	}
	
	// 加载触发行为树aa
	protected sdBehaviourEventRootNode LoadBehaviourEventRootNode(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AIEventRootNode")
			return null;
			
		sdBehaviourEventRootNode kBehaviourEventRootNode = new sdBehaviourEventRootNode();
		kBehaviourEventRootNode.MaxUseCount = -1;
		kBehaviourEventRootNode.ID = int.Parse(kXmlElement.GetAttribute("id"));
		kBehaviourEventRootNode.Name = kXmlElement.GetAttribute("name");
		
		XmlNodeList kXmlChildNodeList = kXmlNode.ChildNodes;
		foreach (XmlNode kXmlChildNode in kXmlChildNodeList)
		{
			XmlElement kXmlChildElement = (XmlElement)kXmlChildNode;
		
			if (kXmlChildElement.Name == "AITrigger")
			{
				sdBehaviourTrigger kBehaviourTrigger = LoadBehaviourTrigger(kXmlChildNode);
				if (kBehaviourTrigger != null)
					kBehaviourEventRootNode.AddBehaviourTrigger(kBehaviourTrigger);
			}
			else if (kXmlChildElement.Name == "AIAction")
			{
				sdBehaviourAction kBehaviourAction = LoadBehaviourAction(kXmlChildNode);
				if (kBehaviourAction != null)
					kBehaviourEventRootNode.AddBehaviourAction(kBehaviourAction);
			}
			else if (kXmlChildElement.Name == "AIState")
			{
				sdBehaviourState kBehaviourState = LoadBehaviourState(kXmlChildNode);
				if (kBehaviourState != null)
					kBehaviourEventRootNode.AddBehaviourState(kBehaviourState);	
			}
			else if (kXmlChildElement.Name == "AIEvent")
			{
				sdBehaviourEvent kBehaviourEvent = LoadBehaviourEvent(kXmlChildNode);
				if (kBehaviourEvent != null)
					kBehaviourEventRootNode.AddBehaviourEvent(kBehaviourEvent);	
			}
			else if (kXmlChildElement.Name == "AIEventNode")
			{
				sdBehaviourEventNode kBehaviourEventChildNode = LoadBehaviourEventNode(kXmlChildNode);
				if (kBehaviourEventChildNode != null)
					kBehaviourEventRootNode.AddChildBehaviourEventNode(kBehaviourEventChildNode);
			}
		}	
	
		return kBehaviourEventRootNode;
	}
	
	// 加载事件节点aa
	protected sdBehaviourEventNode LoadBehaviourEventNode(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AIEventNode")
			return null;
			
		sdBehaviourEventNode kBehaviourEventNode = new sdBehaviourEventNode();
		kBehaviourEventNode.ID = int.Parse(kXmlElement.GetAttribute("id"));
		kBehaviourEventNode.MaxUseCount = int.Parse(kXmlElement.GetAttribute("maxtickcount"));
		
		XmlNodeList kXmlChildNodeList = kXmlNode.ChildNodes;
		foreach (XmlNode kXmlChildNode in kXmlChildNodeList)
		{
			XmlElement kXmlChildElement = (XmlElement)kXmlChildNode;
		
			if (kXmlChildElement.Name == "AITrigger")
			{
				sdBehaviourTrigger kBehaviourTrigger = LoadBehaviourTrigger(kXmlChildNode);
				if (kBehaviourTrigger != null)
					kBehaviourEventNode.AddBehaviourTrigger(kBehaviourTrigger);
			}
			else if (kXmlChildElement.Name == "AIAction")
			{
				sdBehaviourAction kBehaviourAction = LoadBehaviourAction(kXmlChildNode);
				if (kBehaviourAction != null)
					kBehaviourEventNode.AddBehaviourAction(kBehaviourAction);
			}
			else if (kXmlChildElement.Name == "AIState")
			{
				sdBehaviourState kBehaviourState = LoadBehaviourState(kXmlChildNode);
				if (kBehaviourState != null)
					kBehaviourEventNode.AddBehaviourState(kBehaviourState);	
			}
			else if (kXmlChildElement.Name == "AIEvent")
			{
				sdBehaviourEvent kBehaviourEvent = LoadBehaviourEvent(kXmlChildNode);
				if (kBehaviourEvent != null)
					kBehaviourEventNode.AddBehaviourEvent(kBehaviourEvent);	
			}
			else if (kXmlChildElement.Name == "AIEventNode")
			{
				sdBehaviourEventNode kBehaviourEventChildNode = LoadBehaviourEventNode(kXmlChildNode);
				if (kBehaviourEventChildNode != null)
					kBehaviourEventNode.AddChildBehaviourEventNode(kBehaviourEventChildNode);
			}
		}	
	
		return kBehaviourEventNode;
	}
	
	// 加载事件aa
	protected sdBehaviourEvent LoadBehaviourEvent(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AIEvent")
			return null;
			
		sdBehaviourEvent kBehaviourEvent = new sdBehaviourEvent();
		kBehaviourEvent.ID = int.Parse(kXmlElement.GetAttribute("id"));
		kBehaviourEvent.MaxUseCount = int.Parse(kXmlElement.GetAttribute("maxtickcount"));

		XmlNodeList kXmlChildNodeList = kXmlNode.ChildNodes;
		foreach (XmlNode kXmlChildNode in kXmlChildNodeList)
		{
			XmlElement kXmlChildElement = (XmlElement)kXmlChildNode;
		
			if (kXmlChildElement.Name == "AITrigger")
			{
				sdBehaviourTrigger kBehaviourTrigger = LoadBehaviourTrigger(kXmlChildNode);
				if (kBehaviourTrigger != null)
					kBehaviourEvent.AddBehaviourTrigger(kBehaviourTrigger);
			}
			else if (kXmlChildElement.Name == "AIAction")
			{
				sdBehaviourAction kBehaviourAction = LoadBehaviourAction(kXmlChildNode);
				if (kBehaviourAction != null)
					kBehaviourEvent.AddBehaviourAction(kBehaviourAction);
			}
			else if (kXmlChildElement.Name == "AIState")
			{
				sdBehaviourState kBehaviourState = LoadBehaviourState(kXmlChildNode);
				if (kBehaviourState != null)
					kBehaviourEvent.AddBehaviourState(kBehaviourState);	
			}
		}	
	
		return kBehaviourEvent;		
	}

	// 加载触发aa
	protected sdBehaviourTrigger LoadBehaviourTrigger(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AITrigger")
			return null;
	
		sdBehaviourTrigger kBehaviourTrigger = null;
		int iID = int.Parse(kXmlElement.GetAttribute("id"));
		int iUsage = int.Parse(kXmlElement.GetAttribute("usage"));
		if (iID == (int)EBehaviourTriggerType.enBTT_LiveState)
		{
			kBehaviourTrigger = new sdLiveBehaviourTriggerState();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_BattleState)
		{
			kBehaviourTrigger = new sdBattleBehaviourTriggerState();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_HPState)
		{
			sdHPBehaviourTriggerState kHPBehaviourTriggerState = new sdHPBehaviourTriggerState();
			kHPBehaviourTriggerState.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kHPBehaviourTriggerState.MinConditionPercent = int.Parse(kXmlElement.GetAttribute("param1")) / 10000.0f;
			kHPBehaviourTriggerState.MaxConditionPercent = int.Parse(kXmlElement.GetAttribute("param2")) / 10000.0f;

			kBehaviourTrigger = kHPBehaviourTriggerState;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_IdleState)
		{
			kBehaviourTrigger = new sdIdleBehaviourTriggerState();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_TimeState)
		{
			sdTimeBehaviourTriggerState kTimeBehaviourTriggerState = new sdTimeBehaviourTriggerState();
			kTimeBehaviourTriggerState.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kTimeBehaviourTriggerState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kTimeBehaviourTriggerState.IntervalTime = int.Parse(kXmlElement.GetAttribute("param2")) / 1000.0f;
			kTimeBehaviourTriggerState.ElapseTime = int.Parse(kXmlElement.GetAttribute("param3")) / 1000.0f;

			kBehaviourTrigger = kTimeBehaviourTriggerState;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_Summoned)
		{
			kBehaviourTrigger = new sdSummonedBehaviourTrigger();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_Killed)
		{
			kBehaviourTrigger = new sdKilledBehaviourTrigger();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_ChangeTarget)
		{
			kBehaviourTrigger = new sdChangeTargetBehaviourTrigger();
			kBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_HP)
		{
			sdHPBehaviourTrigger kHPBehaviourTrigger = new sdHPBehaviourTrigger();
			kHPBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kHPBehaviourTrigger.BHPBTCType = (EHPBehaviourTriggerCompareType)(int.Parse(kXmlElement.GetAttribute("param1")));
			kHPBehaviourTrigger.ConditionPercent = int.Parse(kXmlElement.GetAttribute("param2")) / 10000.0f;

			kBehaviourTrigger = kHPBehaviourTrigger;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_HurtHP)
		{
			sdHurtHPBehaviourTrigger kHurtHPBehaviourTrigger = new sdHurtHPBehaviourTrigger();
			kHurtHPBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kHurtHPBehaviourTrigger.ConditionPercent = int.Parse(kXmlElement.GetAttribute("param1")) / 10000.0f;

			kBehaviourTrigger = kHurtHPBehaviourTrigger;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_AccumHurtHP)
		{
			sdAccumHurtHPBehaviourTrigger kAccumHurtHPBehaviourTrigger = new sdAccumHurtHPBehaviourTrigger();
			kAccumHurtHPBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kAccumHurtHPBehaviourTrigger.ConditionPercent = int.Parse(kXmlElement.GetAttribute("param1")) / 10000.0f;

			kBehaviourTrigger = kAccumHurtHPBehaviourTrigger;
		}
		else if (iID == (int)EBehaviourTriggerType.enBTT_Buff)
		{
			sdBuffBehaviourTrigger kBuffBehaviourTrigger = new sdBuffBehaviourTrigger();
			kBuffBehaviourTrigger.BTUType = (EBehaviourTriggerUsageType)iUsage;
			kBuffBehaviourTrigger.BuffState = (HeaderProto.ECreatureActionState)(int.Parse(kXmlElement.GetAttribute("param1")));

			kBehaviourTrigger = kBuffBehaviourTrigger;
		}
		
		return kBehaviourTrigger;
	}
	
	// 加载行为aa
	protected sdBehaviourAction LoadBehaviourAction(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AIAction")
			return null;
		
		sdBehaviourAction kBehaviourAction = null;
		int iID = int.Parse(kXmlElement.GetAttribute("id"));
		int iDelay = int.Parse(kXmlElement.GetAttribute("delay"));
		int iCount = int.Parse(kXmlElement.GetAttribute("count"));
		int iInterval = int.Parse(kXmlElement.GetAttribute("interval"));
		if (iID == (int)EBehaviourActionType.enBAT_Summon)
		{
			sdSummonBehaviourAction kSummonBehaviourAction = new sdSummonBehaviourAction();
			kSummonBehaviourAction.DelayTime = iDelay / 1000.0f;
			kSummonBehaviourAction.IntervalTime = iInterval / 1000.0f;
			kSummonBehaviourAction.Count = iCount;
			kSummonBehaviourAction.LevelAreaName = kXmlElement.GetAttribute("param1") as string;
		
			kBehaviourAction = kSummonBehaviourAction;
		}	
		else if (iID == (int)EBehaviourActionType.enBAT_Skill)
		{
			sdSkillBehaviourAction kSkillBehaviourAction = new sdSkillBehaviourAction();
			kSkillBehaviourAction.DelayTime = iDelay / 1000.0f;
			kSkillBehaviourAction.IntervalTime = iInterval / 1000.0f;
			kSkillBehaviourAction.Count = iCount;

			kSkillBehaviourAction.SkillGroupProbility = int.Parse(kXmlElement.GetAttribute("param1")) / 10000.0f;

			string kSkilllIDGroup = kXmlElement.GetAttribute("param2") as string;
			string kSkilllProbilityGroup = kXmlElement.GetAttribute("param3") as string;
			string[] kSkilllIDArray = kSkilllIDGroup.Split(';');
			string[] kSkilllProbilityArray = kSkilllProbilityGroup.Split(';');

			for (int i = 0; i < kSkilllIDArray.Length; ++i)
			{
				int iSkillID = int.Parse(kSkilllIDArray[i]);
				float fSkillProbility = int.Parse(kSkilllProbilityArray[i]) / 10000.0f;

				kSkillBehaviourAction.AddSkill(iSkillID, fSkillProbility);
			}

			if (kXmlElement.HasAttribute("param4"))
			{
				string kDetectDistance = kXmlElement.GetAttribute("param4") as string;
				if (kDetectDistance != null)
					kSkillBehaviourAction.DetectDistance = (int.Parse(kDetectDistance) != 0);
			}

			kBehaviourAction = kSkillBehaviourAction;
		}
		else if (iID == (int)EBehaviourActionType.enBAT_Buff)
		{
			sdBuffBehaviourAction kBuffBehaviourAction = new sdBuffBehaviourAction();
			kBuffBehaviourAction.DelayTime = iDelay / 1000.0f;
			kBuffBehaviourAction.IntervalTime = iInterval / 1000.0f;
			kBuffBehaviourAction.Count = iCount;
			kBuffBehaviourAction.BuffID = int.Parse(kXmlElement.GetAttribute("param1"));
			kBuffBehaviourAction.BuffBehaviourActionTargetType = (EBuffBehaviourActionTargetType)(int.Parse(kXmlElement.GetAttribute("param2")));

			if (kXmlElement.HasAttribute("param3"))
			{
				string kBuffProbility = kXmlElement.GetAttribute("param3") as string;
				if (kBuffProbility != null)
					kBuffBehaviourAction.BuffProbility = int.Parse(kBuffProbility) / 10000.0f;
			}

			kBehaviourAction = kBuffBehaviourAction;
		}	

		return kBehaviourAction;
	}
	
	// 加载状态aa
	protected sdBehaviourState LoadBehaviourState(XmlNode kXmlNode)
	{
		XmlElement kXmlElement = (XmlElement)kXmlNode;	
		if (kXmlElement.Name != "AIState")
			return null;
		
		sdBehaviourState kBehaviourState = null;
		int iID = int.Parse(kXmlElement.GetAttribute("id"));
		if (iID == (int)EBehaviourStateType.enBST_Summon)
		{
			sdSummonBehaviourState kSummonBehaviourState = new sdSummonBehaviourState();
			kSummonBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kSummonBehaviourState.Count = int.Parse(kXmlElement.GetAttribute("param2"));
			kSummonBehaviourState.IntervalTime = int.Parse(kXmlElement.GetAttribute("param3")) / 1000.0f;
			kSummonBehaviourState.LevelAreaName = kXmlElement.GetAttribute("param4") as string;
			
			kBehaviourState = kSummonBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Skill)
		{
			sdSkillBehaviourState kSkillBehaviourState = new sdSkillBehaviourState();
			kSkillBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kSkillBehaviourState.Count = int.Parse(kXmlElement.GetAttribute("param2"));
			kSkillBehaviourState.IntervalTime = int.Parse(kXmlElement.GetAttribute("param3")) / 1000.0f;
			kSkillBehaviourState.SkillGroupProbility = int.Parse(kXmlElement.GetAttribute("param4")) / 10000.0f;

			string kSkilllIDGroup = kXmlElement.GetAttribute("param5") as string;
			string kSkilllProbilityGroup = kXmlElement.GetAttribute("param6") as string;
			string[] kSkilllIDArray = kSkilllIDGroup.Split(';');
			string[] kSkilllProbilityArray = kSkilllProbilityGroup.Split(';');

			for (int i = 0; i < kSkilllIDArray.Length; ++i)
			{
				int iSkillID = int.Parse(kSkilllIDArray[i]);
				float fSkillProbility = int.Parse(kSkilllProbilityArray[i]) / 10000.0f;

				kSkillBehaviourState.AddSkill(iSkillID, fSkillProbility);
			}

			if (kXmlElement.HasAttribute("param7"))
			{
				string kDetectDistance = kXmlElement.GetAttribute("param7") as string;
				if (kDetectDistance != null)
					kSkillBehaviourState.DetectDistance = (int.Parse(kDetectDistance) != 0);
			}

			kBehaviourState = kSkillBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Buff)
		{
			sdBuffBehaviourState kBuffBehaviourState = new sdBuffBehaviourState();
			kBuffBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kBuffBehaviourState.Count = int.Parse(kXmlElement.GetAttribute("param2"));
			kBuffBehaviourState.IntervalTime = int.Parse(kXmlElement.GetAttribute("param3")) / 1000.0f;
			kBuffBehaviourState.BuffID = int.Parse(kXmlElement.GetAttribute("param4"));
			kBuffBehaviourState.BuffBehaviourActionTargetType = (EBuffBehaviourActionTargetType)(int.Parse(kXmlElement.GetAttribute("param5")));

			if (kXmlElement.HasAttribute("param6"))
			{
				string kBuffProbility = kXmlElement.GetAttribute("param6") as string;
				if (kBuffProbility != null)
					kBuffBehaviourState.BuffProbility = int.Parse(kBuffProbility) / 10000.0f;
			}

			kBehaviourState = kBuffBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_CD_Skill)
		{
			sdCDSkillBehaviourState kCDSkillBehaviourState = new sdCDSkillBehaviourState();
			kCDSkillBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kCDSkillBehaviourState.SkillID = int.Parse(kXmlElement.GetAttribute("param2"));

			kBehaviourState = kCDSkillBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Disable_Skill)
		{
			sdDisableSkillBehaviourState kDisableSkillBehaviourState = new sdDisableSkillBehaviourState();
			kDisableSkillBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kDisableSkillBehaviourState.SkillID = int.Parse(kXmlElement.GetAttribute("param2"));

			kBehaviourState = kDisableSkillBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Disable_Mesh)
		{
			sdDisableMeshBehaviourState kDisableMeshBehaviourState = new sdDisableMeshBehaviourState();
			kDisableMeshBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kDisableMeshBehaviourState.MeshName = kXmlElement.GetAttribute("param2") as string;

			kBehaviourState = kDisableMeshBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Force_Animation)
		{
			sdAnimationBehaviourState kAnimationBehaviourState = new sdAnimationBehaviourState();
			kAnimationBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) ;/// 1000.0f;
			kAnimationBehaviourState.AnimationName = kXmlElement.GetAttribute("param2") as string;

			kBehaviourState = kAnimationBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Change_Layer)
		{
			sdChangeLayerBehaviourState kChangeLayerBehaviourState = new sdChangeLayerBehaviourState();
			kChangeLayerBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kChangeLayerBehaviourState.LayerName = kXmlElement.GetAttribute("param2") as string;

			kBehaviourState = kChangeLayerBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Hide)
		{
			sdHideBehaviourState kHideBehaviourState = new sdHideBehaviourState();
			kHideBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;

			kBehaviourState = kHideBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Manual_Buff)
		{
			sdManualBuffBehaviourState kManualBuffBehaviourState = new sdManualBuffBehaviourState();
			kManualBuffBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kManualBuffBehaviourState.BuffID = int.Parse(kXmlElement.GetAttribute("param2"));

			kBehaviourState = kManualBuffBehaviourState;
		}
		else if (iID == (int)EBehaviourStateType.enBST_Change_BattleDistance)
		{
			sdChangeBattleDistanceBehaviourState kChangeBattleDistanceBehaviourState = new sdChangeBattleDistanceBehaviourState();
			kChangeBattleDistanceBehaviourState.DelayTime = int.Parse(kXmlElement.GetAttribute("param1")) / 1000.0f;
			kChangeBattleDistanceBehaviourState.BattleDistance = int.Parse(kXmlElement.GetAttribute("param2")) / 1000.0f;

			kBehaviourState = kChangeBattleDistanceBehaviourState;
		}

		return kBehaviourState;
	}	
	
	// 获取指定模板的怪物的触发行为树aa
	public sdBehaviourEventRootNode GetBehaviourEventTree(int iTemplateID)
	{
		sdBehaviourEventRootNode kSelectedBehaviourEventRootNode = null;
		foreach (sdBehaviourEventRootNode kBehaviourEventRootNode in mAITable)
		{
			if (kBehaviourEventRootNode.ID == iTemplateID)
			{
				kSelectedBehaviourEventRootNode = kBehaviourEventRootNode;
				break; 
			}
		}
		
		if (kSelectedBehaviourEventRootNode != null)
			return kSelectedBehaviourEventRootNode.Clone() as sdBehaviourEventRootNode;
		
		return null;
	}
}
