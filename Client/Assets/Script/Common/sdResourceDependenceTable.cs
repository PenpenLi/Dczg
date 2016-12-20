using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 怪物宠物资源依赖信息表aa
/// </summary>
public class sdResourceDependenceTable : TSingleton<sdResourceDependenceTable>
{
	protected Hashtable mMonsterTable = new Hashtable();
	protected Hashtable mPetTable = new Hashtable();

	// 加载怪物表格aa
	public void LoadMonsterTable(string kPath, bool bTestMode)
	{
		if (bTestMode)
		{
			Object kObj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/" + kPath, typeof(TextAsset));
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "Monster";
			OnLoadTable(kParam, kObj);
		}
		else
		{
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "Monster";
			sdResourceMgr.Instance.LoadResource(kPath, OnLoadTable, kParam);
		}
	}

	// 加载宠物表格aa
	public void LoadPetTable(string kPath, bool bTestMode)
	{
		if (bTestMode)
		{
			Object kObj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/" + kPath, typeof(TextAsset));
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "Pet";
			OnLoadTable(kParam, kObj);
		}
		else
		{
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "Pet";
			sdResourceMgr.Instance.LoadResource(kPath, OnLoadTable, kParam);
		}
	}

	// 加载表格回调,解析表格aa
	protected void OnLoadTable(ResLoadParams kParam, Object kObj)
	{
		Hashtable kHashTable = null;
		if (kParam.info.Equals("Monster"))
		{
			mMonsterTable.Clear();
			kHashTable = mMonsterTable;
		}
		else if (kParam.info.Equals("Pet"))
		{
			mPetTable.Clear();
			mMonsterTable = mPetTable;
		}
		else
		{
			return;
		}

		TextAsset kText = (TextAsset)kObj;
		XmlDocument kXmlDoc = new XmlDocument();
		kXmlDoc.LoadXml(kText.text);

		XmlNode kRoot = kXmlDoc.SelectSingleNode("DependenceInformation");
		if (kRoot == null)
			return;

		XmlNodeList kXmlChildNodeList = kRoot.ChildNodes;
		foreach (XmlNode kXmlChildNode in kXmlChildNodeList)
		{
			XmlElement kXmlChildElement = (XmlElement)kXmlChildNode;
			if (kXmlChildElement.Name.Equals("Actor"))
			{
				List<string> kResourceList = new List<string>();
				XmlNodeList kXmlChild2NodeList = kXmlChildElement.ChildNodes;
				foreach (XmlNode kXmlChild2Node in kXmlChild2NodeList)
				{
					XmlElement kXmlChild2Element = (XmlElement)kXmlChild2Node;
					if (kXmlChild2Element.Name.Equals("Resource"))
					{
						kResourceList.Add(kXmlChildElement.GetAttribute("Path"));
					}
				}

				int iTemplateId = int.Parse(kXmlChildElement.GetAttribute("TemplateID"));
				kHashTable.Add(iTemplateId, kResourceList);
			}
		}	
	}

	// 
	public List<string> GetResourceForMonster(int iTemplateId)
	{
		object kActorObject = mMonsterTable[iTemplateId];
		if (kActorObject != null)
			return kActorObject as List<string>;
		else
			return null;
	}

	// 
	public List<string> GetResourceForPet(int iTemplateId)
	{
		object kActorObject = mPetTable[iTemplateId];
		if (kActorObject != null)
			return kActorObject as List<string>;
		else
			return null;
	}
}