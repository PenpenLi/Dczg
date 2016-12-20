using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// 辅助导出Level对象(Monster和Trigger)对动态资源(特效和音效)依赖的脚本aa
/// </summary>
public class InformationGenerator : MonoBehaviour
{
	/// <summary>
	/// 生成当前场景的依赖信息aa
	/// </summary>
	[MenuItem("Monster/Generate Dependence Information")]
	static void GenerateDependenceInformation()
	{
		GameObject kGameLevelObject = GameObject.Find("@GameLevel");
		if (kGameLevelObject == null)
			return;

		sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();
		if (kTuituLogic == null)
			return;

		// 加载配置文件aa
		bool bNeedClear = false;
		if (!sdConfDataMgr.Instance().IsInitialized)
		{
			bNeedClear = true;
			sdConfDataMgr.Instance().Init(true);
		}

		// 生成怪物的依赖信息aa
		if (GenerateDependenceInformationForMonster(false))
		{
			Debug.LogWarning("怪物的依赖信息生成成功!");
		}

		// 生成宠物的依赖信息aa
		if (GenerateDependenceInformationForPet(false))
		{
			Debug.LogWarning("宠物的依赖信息生成成功!");
		}

		// 清除被指文件aa
		if (bNeedClear)
		{
			sdConfDataMgr.Instance().Clear();
		}
	}

	/// <summary>
	/// 生成当前场景的依赖信息aa
	/// </summary>
	[MenuItem("Monster/Generate Dependence Information For Game")]
	static void GenerateDependenceInformationForGame()
	{
		GameObject kGameLevelObject = GameObject.Find("@GameLevel");
		if (kGameLevelObject == null)
			return;

		sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();
		if (kTuituLogic == null)
			return;

		// 加载配置文件aa
		bool bNeedClear = false;
		if (!sdConfDataMgr.Instance().IsInitialized)
		{
			bNeedClear = true;
			sdConfDataMgr.Instance().Init(true);
		}

		// 生成怪物的依赖信息aa
		if (GenerateDependenceInformationForMonster(true))
		{
			Debug.LogWarning("怪物的依赖信息生成成功!");
		}

		// 生成宠物的依赖信息aa
		if (GenerateDependenceInformationForPet(true))
		{
			Debug.LogWarning("宠物的依赖信息生成成功!");
		}

		// 清除被指文件aa
		if (bNeedClear)
		{
			sdConfDataMgr.Instance().Clear();
		}
	}

	// 怪物对动态资源(特效和音效)依赖信息的表格aa
	static bool GenerateDependenceInformationForMonster(bool bForGame)
	{
		XmlDocument kXmlDoc = new XmlDocument();
		XmlElement kXmlRoot = kXmlDoc.CreateElement("DependenceInformation");
		kXmlDoc.AppendChild(kXmlRoot);

		Hashtable kMonsterTable = sdConfDataMgr.Instance().GetTable("MonsterProperty");
		if (kMonsterTable != null)
		{
			foreach (DictionaryEntry kMonsterEntry in kMonsterTable)
			{
				Hashtable kMonsterProperty = kMonsterEntry.Value as Hashtable;
				if (kMonsterProperty == null)
					continue;

				XmlElement kXmlMonster = GenerateDependenceInformationForMonster(kMonsterProperty, kXmlDoc, bForGame);
				kXmlRoot.AppendChild(kXmlMonster);
			}
		}

		kXmlDoc.Save(Application.dataPath + "/$Conf/MonsterDependence.xml");

		return true;
	}

	// 宠物对动态资源(特效和音效)依赖信息的表格aa
	static bool GenerateDependenceInformationForPet(bool bForGame)
	{
		XmlDocument kXmlDoc = new XmlDocument();
		XmlElement kXmlRoot = kXmlDoc.CreateElement("DependenceInformation");
		kXmlDoc.AppendChild(kXmlRoot);

		Hashtable kPetTable = sdConfDataMgr.Instance().GetTable("PetProperty");
		if (kPetTable != null)
		{
			foreach (DictionaryEntry kPetEntry in kPetTable)
			{
				Hashtable kProperty = kPetEntry.Value as Hashtable;
				if (kProperty == null)
					continue;

				XmlElement kXmlPet = GenerateDependenceInformationForPet(kProperty, kXmlDoc, bForGame);
				kXmlRoot.AppendChild(kXmlPet);
			}
		}

		kXmlDoc.Save(Application.dataPath + "/$Conf/PetDependence.xml");

		return true;
	}

	// 对指定怪物生成依赖信息aa
	static XmlElement GenerateDependenceInformationForMonster(Hashtable kPropertyTable, XmlDocument kXmlDoc, bool bForGame)
	{
		XmlElement kXmlActor = kXmlDoc.CreateElement("Actor");
		kXmlActor.SetAttribute("TemplateID", kPropertyTable["TemplateID"].ToString());

		// 普通攻击技能和触发技能aa
		List<int> kSkillIdList = new List<int>();
		{
			int iDfSkillId = (int)kPropertyTable["DfSkill"];
			kSkillIdList.Add(iDfSkillId);

			int iSkillNum = 10;	//< 怪物有10个技能aa
			for (int iIndex = 1; iIndex <= iSkillNum; ++iIndex)
			{
				string kSkillKey = "Skill" + iIndex.ToString();
				int iSkillId = (int)kPropertyTable[kSkillKey];
				if (iSkillId > 0)
				{
					kSkillIdList.Add(iSkillId);
				}
			}
		}

		//
		if (bForGame)
		{
			HashSet<string> kResourceSet = new HashSet<string>();
			foreach (int iSkillId in kSkillIdList)
			{
				GenerateDependenceInformationForSkillAction(iSkillId, null, kResourceSet);
			}

			foreach (string kResource in kResourceSet)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Resource");
				kXmlDependence.SetAttribute("Path", kResource);
				kXmlActor.AppendChild(kXmlDependence);
			}
		}
		else
		{
			foreach (int iSkillId in kSkillIdList)
			{
				XmlElement kXmlSkillAction = GenerateDependenceInformationForSkillAction(iSkillId, kXmlDoc, null);
				kXmlActor.AppendChild(kXmlSkillAction);
			}
		}

		return kXmlActor;
	}

	// 对指定宠物生成依赖信息aa
	static XmlElement GenerateDependenceInformationForPet(Hashtable kPropertyTable, XmlDocument kXmlDoc, bool bForGame)
	{
		XmlElement kXmlActor = kXmlDoc.CreateElement("Actor");
		kXmlActor.SetAttribute("TemplateID", kPropertyTable["TemplateID"].ToString());

		// 普通攻击技能,大招技能和触发技能aa
		List<int> kSkillIdList = new List<int>();
		{
			int iDfSkillId = (int)kPropertyTable["DfSkill"];
			if (iDfSkillId != 0)
				kSkillIdList.Add(iDfSkillId);

			int iSpSkillId = (int)kPropertyTable["SpSkill"];
			if (iSpSkillId != 0)
				kSkillIdList.Add(iSpSkillId);

			int iSkillNum = 4;	//< 宠物有4个技能aa
			for (int iIndex = 1; iIndex <= iSkillNum; ++iIndex)
			{
				string kSkillKey = "Skill" + iIndex.ToString();
				int iSkillId = (int)kPropertyTable[kSkillKey];
				if (iSkillId > 0)
				{
					kSkillIdList.Add(iSkillId);
				}
			}
		}

		//
		if (bForGame)
		{
			HashSet<string> kResourceSet = new HashSet<string>();
			foreach (int iSkillId in kSkillIdList)
			{
				GenerateDependenceInformationForSkillAction(iSkillId, null, kResourceSet);
			}

			foreach (string kResource in kResourceSet)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Resource");
				kXmlDependence.SetAttribute("Path", kResource);
				kXmlActor.AppendChild(kXmlDependence);
			}
		}
		else
		{
			foreach (int iSkillId in kSkillIdList)
			{
				XmlElement kXmlSkillAction = GenerateDependenceInformationForSkillAction(iSkillId, kXmlDoc, null);
				kXmlActor.AppendChild(kXmlSkillAction);
			}
		}

		return kXmlActor;
	}

	// 对指定技能生成依赖信息aa
	static XmlElement GenerateDependenceInformationForSkillAction(int iSkillId, XmlDocument kXmlDoc, HashSet<string> kResourceSet)
	{
		XmlElement kXmlSkillAction = null;
		if (kXmlDoc != null)
		{
			kXmlSkillAction = kXmlDoc.CreateElement("Skill");
			kXmlSkillAction.SetAttribute("Id", iSkillId.ToString());
		}

		Hashtable kSkillAction = sdConfDataMgr.Instance().m_MonsterSkillAction[iSkillId] as Hashtable;
		if (kSkillAction == null)
			return kXmlSkillAction;

		// 施放音效aa
		string[] kAudioConfArray = (string[])kSkillAction["AudioConf"];
		if (kAudioConfArray != null && kAudioConfArray.Length != 0)
		{
			for (int i = 0; i < kAudioConfArray.Length; ++i)
			{
				if (kXmlDoc != null)
				{
					XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
					kXmlDependence.SetAttribute("Source", "AudioConf");
					kXmlDependence.SetAttribute("Type", "Audio");
					kXmlDependence.SetAttribute("Path", kAudioConfArray[i].ToString());
					kXmlSkillAction.AppendChild(kXmlDependence);
				}

				if (kResourceSet != null && !kResourceSet.Contains("Music/" + kAudioConfArray[i].ToString()))
					kResourceSet.Add("Music/" + kAudioConfArray[i].ToString());
			}
		}

		// 释放特效aa
		string[] kEffectFileArray = (string[])kSkillAction["EffectFile"];
		if (kEffectFileArray != null && kEffectFileArray.Length != 0)
		{
			for (int i = 0; i < kEffectFileArray.Length; ++i)
			{
				if (kXmlDoc != null)
				{
					XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
					kXmlDependence.SetAttribute("Source", "EffectFile");
					kXmlDependence.SetAttribute("Type", "Effect");
					kXmlDependence.SetAttribute("Path", kEffectFileArray[i].ToString());
					kXmlSkillAction.AppendChild(kXmlDependence);
				}

				if (kResourceSet != null && !kResourceSet.Contains(kEffectFileArray[i].ToString()))
					kResourceSet.Add(kEffectFileArray[i].ToString());
			}
		}

		// 命中特效aa
		string kHitEffect = kSkillAction["HitEffect"] as string;
		if (kHitEffect != null && kHitEffect.Length > 0)
		{
			if (kXmlDoc != null)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
				kXmlDependence.SetAttribute("Source", "HitEffect");
				kXmlDependence.SetAttribute("Type", "Effect");
				kXmlDependence.SetAttribute("Path", kHitEffect.ToString());
				kXmlSkillAction.AppendChild(kXmlDependence);
			}

			if (kResourceSet != null && !kResourceSet.Contains(kHitEffect.ToString()))
				kResourceSet.Add(kHitEffect.ToString());
		}

		// 技能触发效果aa
		Hashtable kSkillEffectTable = sdConfDataMgr.Instance().m_BaseSkillEffect;
		if (kSkillEffectTable == null)
			return kXmlSkillAction;

		Hashtable kOperationTable = sdConfDataMgr.Instance().GetTable("operation");
		if (kOperationTable == null)
			return kXmlSkillAction;

		int[] kEffectIdArray = (int[])kSkillAction["sSkillEffect[8]"];
		if (kEffectIdArray != null && kEffectIdArray.Length > 0)
		{
			for (int i = 0; i < kEffectIdArray.Length; ++i)
			{
				int iEffectId = kEffectIdArray[i];

				SkillEffect kSkillEffect = kSkillEffectTable[iEffectId] as SkillEffect;
				sdEffectOperation.Operation kOperation = kOperationTable[kSkillEffect.dwOperationID] as sdEffectOperation.Operation;

				if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_NONE)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_FOREVER)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_STATE)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_ADD_BUFF)
				{
					XmlElement kXmlBuff = GenerateDependenceInformationForBuff(kSkillEffect.dwOperationData, kXmlDoc, kResourceSet);
					if (kXmlBuff != null)
						kXmlSkillAction.AppendChild(kXmlBuff);
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_DISPEL)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_DO_BUFF_DAMAGE)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_ADD_SKILL_POINT)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_DO_SKILL_COOLDOWN)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_CLEAR_SKILL_COOLDOWN)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_INTERUPT_SKILL)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_SUMMON_MONSTER)
				{
					XmlElement kXmlSummon = GenerateDependenceInformationForSummon(kSkillEffect.dwOperationData, kXmlDoc, kResourceSet);
					if (kXmlSummon != null)
						kXmlSkillAction.AppendChild(kXmlSummon);
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_TELEPORT)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_DOACTION)
				{
					XmlElement kXmlSecondSkillAction = GenerateDependenceInformationForSecondSkillAction(kSkillEffect.dwOperationData, kXmlDoc, kResourceSet);
					if (kXmlSecondSkillAction != null)
						kXmlSkillAction.AppendChild(kXmlSecondSkillAction);	
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_COMBO)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPREATION_TYPE_ATTACKGETHP)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPERATION_TYPE_HURTMODIFYBUFF)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPERATION_TYPE_HURTMODIFY)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPERATION_TYPE_SUMMON)
				{
					XmlElement kXmlSummon = GenerateDependenceInformationForSummon(kSkillEffect.dwOperationData, kXmlDoc, kResourceSet);
					if (kXmlSummon != null)
						kXmlSkillAction.AppendChild(kXmlSummon);	
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPERATION_TYPE_HIDESHOW)
				{
					// 无aa
				}
				else if (kOperation.byOperationType == (int)HeaderProto.EOpreationType.OPERATION_TYPE_FLASH)
				{
					// 无aa
				}
			}
		}

		return kXmlSkillAction;
	}

	// 对指定触发技能生成依赖信息aa
	static XmlElement GenerateDependenceInformationForSecondSkillAction(int iSkillId, XmlDocument kXmlDoc, HashSet<string> kResourceSet)
	{
		XmlElement kXmlSkillAction = null;
		if (kXmlDoc != null)
		{
			kXmlSkillAction = kXmlDoc.CreateElement("SecondSkill");
			kXmlSkillAction.SetAttribute("Id", iSkillId.ToString());
		}

		Hashtable kSkillAction = sdConfDataMgr.Instance().m_MonsterSkillAction[iSkillId] as Hashtable;
		if (kSkillAction == null)
			return kXmlSkillAction;

		// 施放音效aa
		string[] kAudioConfArray = (string[])kSkillAction["AudioConf"];
		if (kAudioConfArray != null && kAudioConfArray.Length != 0)
		{
			for (int i = 0; i < kAudioConfArray.Length; ++i)
			{
				if (kXmlDoc != null)
				{
					XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
					kXmlDependence.SetAttribute("Source", "AudioConf");
					kXmlDependence.SetAttribute("Type", "Audio");
					kXmlDependence.SetAttribute("Path", kAudioConfArray[i].ToString());
					kXmlSkillAction.AppendChild(kXmlDependence);
				}

				if (kResourceSet != null && !kResourceSet.Contains("Music/" + kAudioConfArray[i].ToString()))
					kResourceSet.Add("Music/" + kAudioConfArray[i].ToString());
			}
		}

		// 释放特效aa
		string[] kEffectFileArray = (string[])kSkillAction["EffectFile"];
		if (kEffectFileArray != null && kEffectFileArray.Length != 0)
		{
			for (int i = 0; i < kEffectFileArray.Length; ++i)
			{
				if (kXmlDoc != null)
				{
					XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
					kXmlDependence.SetAttribute("Source", "EffectFile");
					kXmlDependence.SetAttribute("Type", "Effect");
					kXmlDependence.SetAttribute("Path", kEffectFileArray[i].ToString());
					kXmlSkillAction.AppendChild(kXmlDependence);
				}

				if (kResourceSet != null && !kResourceSet.Contains(kEffectFileArray[i].ToString()))
					kResourceSet.Add(kEffectFileArray[i].ToString());
			}
		}

		// 命中特效aa
		string kHitEffect = kSkillAction["HitEffect"] as string;
		if (kHitEffect != null && kHitEffect.Length > 0)
		{
			if (kXmlDoc != null)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
				kXmlDependence.SetAttribute("Source", "HitEffect");
				kXmlDependence.SetAttribute("Type", "Effect");
				kXmlDependence.SetAttribute("Path", kHitEffect.ToString());
				kXmlSkillAction.AppendChild(kXmlDependence);
			}

			if (kResourceSet != null && !kResourceSet.Contains(kHitEffect.ToString()))
				kResourceSet.Add(kHitEffect.ToString());
		}

		return kXmlSkillAction;
	}

	// 对指定召唤生成依赖信息aa
	static XmlElement GenerateDependenceInformationForSummon(int iSummonId, XmlDocument kXmlDoc, HashSet<string> kResourceSet)
	{
		XmlElement kXmlSummon = null;
		if (kXmlDoc != null)
		{
			kXmlSummon = kXmlDoc.CreateElement("Summon");
			kXmlSummon.SetAttribute("Id", iSummonId.ToString());
		}

		Hashtable kSummonTable =  sdConfDataMgr.Instance().m_BaseSummon;
		if (kSummonTable == null)
			return kXmlSummon;

		Hashtable kSummonItem = kSummonTable[iSummonId] as Hashtable;
		if (kSummonItem == null)
			return kXmlSummon;

		string kSummonEffect = kSummonItem["SummonEffect"] as string;
		if (kSummonEffect != null && kSummonEffect.Length > 0)
		{
			if (kXmlDoc != null)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
				kXmlDependence.SetAttribute("Source", "SummonEffect");
				kXmlDependence.SetAttribute("Type", "Effect");
				kXmlDependence.SetAttribute("Path", kSummonEffect.ToString());
				kXmlSummon.AppendChild(kXmlDependence);
			}

			if (kResourceSet != null && !kResourceSet.Contains(kSummonEffect.ToString()))
				kResourceSet.Add(kSummonEffect.ToString());
		}

		string kHitEffect = kSummonItem["HitEffect"] as string;
		if (kHitEffect != null && kHitEffect.Length > 0)
		{
			if (kXmlDoc != null)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
				kXmlDependence.SetAttribute("Source", "HitEffect");
				kXmlDependence.SetAttribute("Type", "Effect");
				kXmlDependence.SetAttribute("Path", kHitEffect.ToString());
				kXmlSummon.AppendChild(kXmlDependence);
			}

			if (kResourceSet != null && !kResourceSet.Contains(kHitEffect.ToString()))
			{
				kResourceSet.Add(kHitEffect.ToString());
			}
		}

		return kXmlSummon;
	}

	// 对指定Buff生成依赖信息aa
	static XmlElement GenerateDependenceInformationForBuff(int iBuffId, XmlDocument kXmlDoc, HashSet<string> kResourceSet)
	{
		XmlElement kXmlBuff = null;
		if (kXmlDoc != null)
		{
			kXmlBuff = kXmlDoc.CreateElement("Buff");
			kXmlBuff.SetAttribute("Id", iBuffId.ToString());
		}

		Hashtable kBuffTable = sdConfDataMgr.Instance().GetTable("buff");
		if (kBuffTable == null)
			return kXmlBuff;

		Hashtable kSummonItem = kBuffTable[iBuffId.ToString()] as Hashtable;
		if (kSummonItem == null)
			return kXmlBuff;

		string kBuffEffect = kSummonItem["Effect"] as string;
		if (kBuffEffect != null && kBuffEffect.Length > 0)
		{
			if (kXmlDoc != null)
			{
				XmlElement kXmlDependence = kXmlDoc.CreateElement("Dependence");
				kXmlDependence.SetAttribute("Source", "Effect");
				kXmlDependence.SetAttribute("Type", "Effect");
				kXmlDependence.SetAttribute("Path", kBuffEffect.ToString());
				kXmlBuff.AppendChild(kXmlDependence);
			}

			if (kResourceSet != null && !kResourceSet.Contains(kBuffEffect.ToString()))
				kResourceSet.Add(kBuffEffect.ToString());
		}

		return kXmlBuff;
	}
}