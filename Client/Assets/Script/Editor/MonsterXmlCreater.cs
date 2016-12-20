using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// 辅助导出Level的Monster信息的脚本aa
/// </summary>
public class MonsterXmlCreater : MonoBehaviour 
{
	/// <summary>
	/// 生成当前场景Monster的XML信息aa
	/// </summary>
	[MenuItem("Monster/Create Xml")]
	static void CreateMonsterXml()
	{
		GameObject kGameLevelObject = GameObject.Find("@GameLevel");
		if (kGameLevelObject == null)
			return;

		sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();
		if (kTuituLogic == null)
			return;

		if (CreateMonsterXmlByLevelID(kTuituLogic.levelID))
		{
			Debug.LogWarning("配置文件生成完毕");
		}
	}

	/// <summary>
	/// 生成当前场景所有Monster的唯一ID,并保存场景aa
	/// </summary>
    [MenuItem("Monster/Generate UniqueID")]
    static void GenerateUniqueID()
    {
		GameObject kGameLevelObject = GameObject.Find("@GameLevel");
		if (kGameLevelObject == null)
			return;

		sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();
		if (kTuituLogic == null)
			return;

        List<sdGameMonster> kMonsterList = FindMonster(7);
		if (kMonsterList != null)
        {
            int iUniqueId = 1;
			for (int i = 0; i < kMonsterList.Count; i++)
            {
				sdGameMonster kMonster = kMonsterList[i];
				if (kMonster != null)
                {
					kMonster.uniqueId = iUniqueId;
					EditorUtility.SetDirty(kMonster);
					iUniqueId++;
                }
            }
        }

        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        Debug.LogWarning("Gen Monster Unique ID OK!");  
    }

//	static List<string> EmurateFilesUnderPath(string basePath, string extension)
// 	{
// 		List<string> files = new List<string>();
// 		foreach(string file in System.IO.Directory.GetFiles(basePath))
// 		{
// 			var ext = Path.GetExtension(file);
// 			if (extension.Length == 0 || extension == ext)
// 			{
// 				files.Add(file.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
// 			}
// 		}
// 		foreach(string dir in System.IO.Directory.GetDirectories(basePath))
// 		{
// 			files.AddRange(EmurateFilesUnderPath(dir, extension));
// 		}
// 		return files;
// 	}

	// 场景CSV文件加载回调aa
	static void OnCSVLoad(string key, SDCSV csv)
	{
		string kCurrentScene = EditorApplication.currentScene;
		foreach(DictionaryEntry de in csv.csvTable)
		{
			string id = de.Key.ToString();
			Hashtable table	= de.Value as Hashtable;
			string levelPath = table["Scene"] as string;
			string strLevelPath = "Assets/" + levelPath + ".unity";
			if (EditorApplication.OpenScene(strLevelPath))
			{
				Debug.LogWarning("exporting scene " + strLevelPath);
				if (!CreateMonsterXmlByLevelID(id))
				{
					Debug.LogError("failed to create monster xml of scene " + strLevelPath);
				}
			}
			else
			{
				Debug.LogError("failed to open scene " + strLevelPath);
			}
		}
		EditorApplication.OpenScene(kCurrentScene);
	}

	/// <summary>
	/// 生成所有场景所有Monster的唯一ID,并保存场景aa
	/// </summary>
	[MenuItem("Monster/Create Xml of All Tuitu Level")]
	static void CreateMonsterXmlAllTuituLevel()
	{
		SDCSV kCSV = new SDCSV();
		kCSV.LoadCSVInTestMode("$Conf/level.txt", OnCSVLoad);
	}

	// 查找指定节点下的Monster对象aa
    static void FindMonster(Transform kTransform, List<sdGameMonster> kMonsterList)
    {
		if (kTransform.gameObject.tag == "Monster")
        {
			sdGameMonster kMonster = kTransform.GetComponent<sdGameMonster>();
			if (kMonster != null)
            {
				kMonsterList.Add(kMonster);
                return;
            }
        }

		for (int i = 0; i < kTransform.childCount; i++)
        {
			FindMonster(kTransform.GetChild(i), kMonsterList);
        }
    }

	// 查找指定难度等级的Monster对象aa
    static List<sdGameMonster> FindMonster(int diffcultFlag)
    {
        List<sdGameMonster> kMonsterList = new List<sdGameMonster>();
        GameObject kDesignNode = GameObject.Find("@Design");
		if (kDesignNode != null)
        {
			for (int i = 0; i < kDesignNode.transform.childCount; i++)
            {
				Transform child = kDesignNode.transform.GetChild(i);
                if (child.name == "@Easy" && (diffcultFlag & 1) != 0)
                {
					FindMonster(child, kMonsterList);
                }
                else if (child.name == "@Hard" && (diffcultFlag & 2) != 0)
                {
					FindMonster(child, kMonsterList);
                }
                else if (child.name == "@Nightmare" && (diffcultFlag & 4) != 0)
                {
					FindMonster(child, kMonsterList);
                }
            }
        }

		return kMonsterList;
    }
	
	// 查找场景中的怪物,生成唯一ID和模板ID的表格aa
	static bool CreateMonsterXmlByLevelID(string levelID)
	{
		XmlDocument xmldoc = new XmlDocument();		
		XmlElement root = xmldoc.CreateElement("Monsters");

        int id = (int.Parse(levelID)%10) -1;
		List<sdGameMonster> kMonsterList = FindMonster(1 << id);
		if (kMonsterList != null)
		{
			for (int i = 0; i < kMonsterList.Count; i++)
			{
				sdGameMonster kMonster = kMonsterList[i];
				if (kMonster != null)
				{
					XmlElement monElement = xmldoc.CreateElement("Monster");
					monElement.SetAttribute("ID", kMonster.uniqueId.ToString());
					monElement.SetAttribute("TemplateID", kMonster.templateId.ToString());
					root.AppendChild(monElement);
				}
			}
						
			xmldoc.AppendChild(root);
			xmldoc.Save(Application.dataPath + "/MonsterConf/" + levelID + ".xml");

			return true;
		}
		
		return false;
	}
}
