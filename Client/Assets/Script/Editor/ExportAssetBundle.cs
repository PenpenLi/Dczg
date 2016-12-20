using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class ExportAssetBundle : MonoBehaviour {
	
	/*static List<string> pushInfo = new List<string>();
	
	
	static void pushDepend(string name)
	{
		pushInfo.Add(name);
	}
	
	static void popDepend()
	{
		int length = pushInfo.Count;
		
		if(length > 0)
			pushInfo.RemoveAt(length-1);
	}*/
    static string ReadText(string name)
    { 
        return File.ReadAllText("Assets/Resources/AppVersion.txt");
        
    }
    static Dictionary<string,string> ReadINI(string name)
    {
        Dictionary<string, string> table = new Dictionary<string, string>();
        string content = ReadText(name);
        if(content.Length==0)
        {
            return table;
        }
        string[] lines = content.Replace("\r", "").Split('\n');
        
        
        foreach (string s in lines)
        {
            if (s.Length > 0)
            {
                string[] element    =   s.Split('=');
                char[] data1 = element[0].ToCharArray();
                table[element[0]] = element[1];
            }
        }
        return table;
    }
    [MenuItem("Assets/AssetBundle/Build Android Player")]
    static void BuildAndroidPlayer()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        string[] levels = {
			"Assets/Level/login.unity",
			//"Assets/Level/$selectSever.unity",
		};
		//PlayerSettings.keyaliasPass = "ds_mobile";
		//PlayerSettings.keystorePass = "ds_mobile";
		PlayerSettings.Android.keyaliasName = "ds_mobile";
		PlayerSettings.Android.keyaliasPass = "ds_mobile";
		PlayerSettings.Android.keystoreName = Application.dataPath + "/../tools/DS_MOBILE.keystore";
		PlayerSettings.Android.keystorePass = "ds_mobile";

        Dictionary<string, string> ini = ReadINI("Assets/Resources/AppVersion.txt");

        PlayerSettings.Android.bundleVersionCode    = int.Parse(ini["versionCode"]);
        PlayerSettings.bundleVersion                = ini["versionName"];

        Debug.Log(ini["versionCode"]);
        Debug.Log(ini["versionName"]);

        string resVersion = PlayerSettings.bundleVersion.Replace(".","_");

        string apkName = ini["appName"] + resVersion + ".apk";

        BuildPipeline.BuildPlayer(levels, Application.dataPath + "/../bundle/" + apkName, BuildTarget.Android, BuildOptions.None);
    }
	[MenuItem("Assets/AssetBundle/Build IOS Player")]
	static void BuildIOSPlayer()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
		string[] levels = {
			"Assets/Level/login.unity",
			//"Assets/Level/$selectSever.unity",
		};
        Dictionary<string, string> ini = ReadINI("Assets/Resources/AppVersion.txt");

        //PlayerSettings.Android.bundleVersionCode = int.Parse(ini["versionCode"]);
        PlayerSettings.bundleVersion = ini["versionName"];

        Debug.Log(ini["versionCode"]);
        Debug.Log(ini["versionName"]);
		BuildPipeline.BuildPlayer(levels, Application.dataPath + "/../IOS", BuildTarget.iOS, BuildOptions.None);
	}
		
	[MenuItem("Assets/AssetBundle/Build AssetBundle")]
	static void ExportResource()
	{
		string path = "rtlAssetBundle.unity3d";
		Object[] selection =  Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildPipeline.BuildAssetBundle(null, selection, path,
			BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
			BuildTarget.Android);
	}
	
	[MenuItem("Assets/AssetBundle/Clean Cache")]
	static void CleanCache()
	{
		Caching.CleanCache();
	}
	
	[MenuItem("Assets/AssetBundle/Single Build/Build Scene")]
	static void ExportScene()
	{
		Caching.CleanCache();
		AssetDatabase.Refresh();
		
		TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath("Assets/bundleInfo.xml",typeof(TextAsset)) as TextAsset;
		
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(xmlAsset.text);
		
		XmlNode root = xmldoc.SelectSingleNode("Bundles");
		
		List<BundleItem> bundles = new List<BundleItem>();
		
		if(root != null)
		{
			XmlNodeList nodelist = root.ChildNodes;
			foreach(XmlNode node in nodelist)
			{
				XmlElement xmlElement = (XmlElement)node;
				
				BundleItem item = new BundleItem();
				item.bundlePath = xmlElement.GetAttribute("url");
				item.localPath = xmlElement.GetAttribute("path");
				item.version = uint.Parse(xmlElement.GetAttribute("version"));
                item.compress_crc = uint.Parse(xmlElement.GetAttribute("compress_crc"));
				item.isScene = bool.Parse(xmlElement.GetAttribute("scene"));
				item.isGlobal = bool.Parse(xmlElement.GetAttribute("isGlobal"));
				
				XmlNodeList dependList = node.ChildNodes;
					
				if(dependList.Count > 0)
				{
					item.dependency = new int[dependList.Count];
					item.dependStr = new string[dependList.Count];
						
					for(int i = 0; i < dependList.Count; i++)
					{
						XmlElement dependElement = (XmlElement)dependList[i];
						string dependPath = dependElement.GetAttribute("url");
						item.dependStr[i] = dependPath;
					}
				}
				bundles.Add(item);
			}
		}
		
		
		
		for(int i = 0; i < bundles.Count; i++)
		{
			int dependSize = 0;
			if(bundles[i].dependency != null)
				dependSize = bundles[i].dependency.Length;
			
			for(int j = 0; j < dependSize; j++)
			{
				string dependPath = bundles[i].dependStr[j];
				for(int k =0; k < bundles.Count; k++)
				{
					if(bundles[k].bundlePath == dependPath)
					{
						bundles[i].dependency[j] = k;
						break;
					}
				}
			}
		}
		BuildAssetBundleOptions options = 
			BuildAssetBundleOptions.CollectDependencies | 
			BuildAssetBundleOptions.CompleteAssets | 
			BuildAssetBundleOptions.DeterministicAssetBundle;
		
		foreach(Object obj in Selection.objects)
		{
			string path = AssetDatabase.GetAssetPath(obj);
			
			if(path.Contains("$"))
			{
				for(int i = 0; i < bundles.Count; i++)
				{
					if(bundles[i].localPath == path)
					{
						int pushCount = 0;
						
						BuildPipeline.PushAssetDependencies();
						pushCount++;
						BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath(bundles[0].localPath),
							null,
							"bundle/"+bundles[0].bundlePath,
							options,
							BuildTarget.Android);
						
						doExportScene(bundles[i].localPath,
							"bundle/"+bundles[i].bundlePath,
							options,BuildTarget.Android,
							bundles,i,ref pushCount);
						
						for(int j = 0; j < pushCount; j++)
						{
							BuildPipeline.PopAssetDependencies();
						}
						break;
					}
				}
			}
			
		}
	}
	
	[MenuItem("Assets/AssetBundle/Single Build/Build Bundles")]
	static void ExportFolder()
	{
		Caching.CleanCache();
		AssetDatabase.Refresh();
		
		TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath("Assets/bundleInfo.xml",typeof(TextAsset)) as TextAsset;
		
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(xmlAsset.text);
		
		XmlNode root = xmldoc.SelectSingleNode("Bundles");
		
		List<BundleItem> bundles = new List<BundleItem>();
		
		if(root != null)
		{
			XmlNodeList nodelist = root.ChildNodes;
			foreach(XmlNode node in nodelist)
			{
				XmlElement xmlElement = (XmlElement)node;
				
				BundleItem item = new BundleItem();
				item.bundlePath = xmlElement.GetAttribute("url");
				item.localPath = xmlElement.GetAttribute("path");
				item.version = uint.Parse(xmlElement.GetAttribute("version"));
                item.compress_crc = uint.Parse(xmlElement.GetAttribute("compress_crc"));
				item.isScene = bool.Parse(xmlElement.GetAttribute("scene"));
				item.isGlobal = bool.Parse(xmlElement.GetAttribute("isGlobal"));
				
				XmlNodeList dependList = node.ChildNodes;
					
				if(dependList.Count > 0)
				{
					item.dependency = new int[dependList.Count];
					item.dependStr = new string[dependList.Count];
						
					for(int i = 0; i < dependList.Count; i++)
					{
						XmlElement dependElement = (XmlElement)dependList[i];
						string dependPath = dependElement.GetAttribute("url");
						item.dependStr[i] = dependPath;
					}
				}
				bundles.Add(item);
			}
		}
		
		for(int i = 0; i < bundles.Count; i++)
		{
			int dependSize = 0;
			if(bundles[i].dependency != null)
				dependSize = bundles[i].dependency.Length;
			
			for(int j = 0; j < dependSize; j++)
			{
				string dependPath = bundles[i].dependStr[j];
				for(int k =0; k < bundles.Count; k++)
				{
					if(bundles[k].bundlePath == dependPath)
					{
						bundles[i].dependency[j] = k;
						break;
					}
				}
			}
		}
		BuildAssetBundleOptions options = 
			BuildAssetBundleOptions.CollectDependencies | 
			BuildAssetBundleOptions.CompleteAssets | 
			BuildAssetBundleOptions.DeterministicAssetBundle;
		
		foreach(Object obj in Selection.objects)
		{
			string path = AssetDatabase.GetAssetPath(obj);
			if(path.Contains("$"))
			{
				Debug.Log(path);
				string dummyPath = System.IO.Path.Combine(path,"fake.asset");
				
				string assetPath = AssetDatabase.GenerateUniqueAssetPath(dummyPath);
				
				if(assetPath != "")
				{
					path += "/";
				}
				for(int i = 0; i < bundles.Count; i++)
				{
					//Debug.Log(bundles[i].localPath);
					if(bundles[i].localPath == path)
					{
						int pushCount = 0;
						
						BuildPipeline.PushAssetDependencies();
						pushCount++;
						BuildCompressAssetBundle(bundles[i],
                            AssetDatabase.LoadMainAssetAtPath(bundles[0].localPath),
							null,
							"bundle/"+bundles[0].bundlePath,
							options,
							BuildTarget.Android);
						
						doExportBundle(bundles[i].localPath,
							"bundle/"+bundles[i].bundlePath,
							options,
							BuildTarget.Android,
							bundles,i,ref pushCount);
						
						for(int j = 0; j < pushCount; j++)
						{
							BuildPipeline.PopAssetDependencies();
						}
						break;
					}
				}
			}
		}
	}
	
	
	[MenuItem("Assets/AssetBundle/Build Android Bundle")]
	static void ExportAndroid()
	{
		exportBundles(BuildTarget.Android,true);
	}
    [MenuItem("Assets/AssetBundle/Build Android Bundle Add")]
    static void ExportAndroidAdd()
    {
        exportBundles(BuildTarget.Android, false);
    }
	
	[MenuItem("Assets/AssetBundle/Build IOS Bundle")]
	static void ExportIOS()
	{
        exportBundles(BuildTarget.iOS, true);
	}
	
	[MenuItem("Assets/AssetBundle/Build PC Bundle")]
	static void ExportPC()
	{
        exportBundles(BuildTarget.StandaloneWindows, true);
	}
	
	static void buildBundleByDirectory(string path,List<Object> objs)
	{	
		path = path.Replace("Assets/", "");
		string [] fileEntries = Directory.GetFiles(Application.dataPath+"/"+path);
		
		foreach(string fileName in fileEntries)
		{
			string filePath = fileName.Replace("\\", "/");
            if (filePath.EndsWith(".meta"))
            {
                continue;
            }
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index);
			string localPath = "Assets/" + path;
			if (index > 0)
				localPath += filePath;
			Object t = AssetDatabase.LoadMainAssetAtPath(localPath);
			if (t != null)
			{
				objs.Add(t);
			}
		}
		
		string[] dirs = Directory.GetDirectories(Application.dataPath+"/"+path);
		
		foreach(string dir in dirs)
		{
			string filePath = dir.Replace("\\", "/");
            if (filePath.EndsWith(".svn"))
            {
                continue;
            }
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index);
			string localPath = "Assets/" + path;
			if (index > 0)
				localPath += filePath;
			buildBundleByDirectory(localPath,objs);
		}
		
		//AssetDatabase.Refresh();
	}
	
	static void doExportBundle(string path,string url,BuildAssetBundleOptions options,
		BuildTarget target,List<BundleItem> bundles, int index,ref int pushCount)
	{
		//if(bundles[index].isPushed)
		//	return;
		
		//int pushC = 0;
		//bundles[index].pushCount = 0;
		if(bundles[index].dependency != null)
			for(int i = 0; i < bundles[index].dependency.Length; i++)
			{
				int dependId = bundles[index].dependency[i];
				//doExportBundle(bundles[dependId].localPath,"bundle/" + bundles[dependId].bundlePath,
				//	options,target,bundles,dependId,ref pushCount);
				
				BuildAssetBundleOptions op =	BuildAssetBundleOptions.CompleteAssets | 
										BuildAssetBundleOptions.DeterministicAssetBundle |
										BuildAssetBundleOptions.UncompressedAssetBundle;
			
				string tmpPath = bundles[dependId].localPath;
				if(tmpPath[tmpPath.Length-1] == '/')
				{
					tmpPath = tmpPath.Substring(0,tmpPath.Length-1);
					List<Object> objs = new List<Object>();
					buildBundleByDirectory(tmpPath,objs);
					Object[] objArray = objs.ToArray();
					if(objArray.Length > 0)
					{
						BuildPipeline.PushAssetDependencies();
						pushCount++;
						//bundles[index].pushCount++;
						createDirectory("tmpBundle/" + bundles[dependId].bundlePath);
						BuildPipeline.BuildAssetBundle(null, objArray,"tmpBundle/" + bundles[dependId].bundlePath, op,target);
					}
				}
				else
				{
					BuildPipeline.PushAssetDependencies();
					pushCount++;
					//bundles[index].pushCount++;
					createDirectory("tmpBundle/" + bundles[dependId].bundlePath);
					BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath(tmpPath), null,"tmpBundle/" + bundles[dependId].bundlePath, op,target);
				}
				
			}
		
		if(path[path.Length-1] == '/')
		{
			path = path.Substring(0,path.Length-1);
			List<Object> objs = new List<Object>();
			buildBundleByDirectory(path,objs);
			Object[] objArray = objs.ToArray();
			if(objArray.Length > 0)
			{
				BuildPipeline.PushAssetDependencies();
				pushCount++;
				//bundles[index].pushCount++;
				createDirectory(url);
                BuildCompressAssetBundle(bundles[index],null, objArray,url, options,target);
      
			}
		}
		else
		{
			BuildPipeline.PushAssetDependencies();
			pushCount++;
			//bundles[index].pushCount++;
			createDirectory(url);
            BuildCompressAssetBundle(bundles[index],AssetDatabase.LoadMainAssetAtPath(path), null, url, options, target);
		}
		
		bundles[index].isPushed = true;
		
		/*for(int i = 0; i < pushC; i++)
		{
			BuildPipeline.PopAssetDependencies();
		}*/
	}
	
	static void doExportScene(string path,string url,BuildAssetBundleOptions options,BuildTarget target,List<BundleItem> bundles, int index,ref int pushCount)
	{
		if(bundles[index].isPushed)
			return;
		
		//int pushC = 0;
		
		if(bundles[index].dependency != null)
		for(int i = 0; i < bundles[index].dependency.Length; i++)
		{
			int dependId = bundles[index].dependency[i];
			//Debug.Log("depend:"+bundles[dependId].bundlePath);
			//doExportBundle(bundles[dependId].localPath,"bundle/" + bundles[dependId].bundlePath,
			//	options,target,bundles,dependId,ref pushCount);
			
			BuildAssetBundleOptions op =	BuildAssetBundleOptions.CompleteAssets | 
										BuildAssetBundleOptions.DeterministicAssetBundle |
										BuildAssetBundleOptions.UncompressedAssetBundle;
			
			string tmpPath = bundles[dependId].localPath;
			if(tmpPath[tmpPath.Length-1] == '/')
			{
				tmpPath = tmpPath.Substring(0,tmpPath.Length-1);
				List<Object> objs = new List<Object>();
				buildBundleByDirectory(tmpPath,objs);
				Object[] objArray = objs.ToArray();
				if(objArray.Length > 0)
				{
					BuildPipeline.PushAssetDependencies();
					pushCount++;
					//bundles[index].pushCount++;
					createDirectory("tmpBundle/" + bundles[dependId].bundlePath);
					BuildPipeline.BuildAssetBundle(null, objArray,"tmpBundle/" + bundles[dependId].bundlePath, op,target);
				}
			}
			else
			{
				BuildPipeline.PushAssetDependencies();
				pushCount++;
				//bundles[index].pushCount++;
				createDirectory("tmpBundle/" + bundles[dependId].bundlePath);
				BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath(tmpPath), null,"tmpBundle/" + bundles[dependId].bundlePath, op,target);
			}
		}
		
		createDirectory(url);
		
		BuildPipeline.PushAssetDependencies();
		string[] levels = new string[1];
		levels[0] = path;

        BuildCompressScene(bundles[index],levels, url, target);


		BuildPipeline.PopAssetDependencies();
		
		/*for(int i = 0; i < pushC; i++)
		{
			BuildPipeline.PopAssetDependencies();
		}*/
		
		bundles[index].isPushed = true;
	}
	
	static void createDirectory(string path)
	{
		int lastIndex = -1;
		int index = path.IndexOf("/");
		while(index >= 0)
		{
			//int nextIndex = path.IndexOf("/",index+1);
			//if(nextIndex < 0)
			//	break;
			string foldname = path.Substring(0,index);
			Debug.Log(Application.dataPath.Replace("Assets","") + foldname);
			foldname = Application.dataPath.Replace("Assets","") + foldname;
			//foldname.Replace("/","\\");
			Directory.CreateDirectory(foldname);
			lastIndex = index;
			index = path.IndexOf("/",lastIndex+1);
		}
	}
    static int iState = 0;
    static List<BundleItem> lstCompress = new List<BundleItem>();
    static object lockObj = new object();
    static void CompressThreadMain()
    {
        Debug.Log("Begin Compress");
        byte[] tempData = new byte[1024 * 1024];
        while (true)
        { 
            
            
            BundleItem item = null;
            lock (lockObj)
            {
                if (lstCompress.Count > 0)
                {
                    //Debug.Log("Compress Item Count = "+lstCompress.Count);
                    item = lstCompress[0];
                    lstCompress.RemoveAt(0);
                }
            }
            if (item == null)
            {
                if (iState == 1)
                {
                    break;
                }
                else 
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            else
            {
                //Debug.Log("Compress item 1");
                uint[] crc = Compress("bundle/" + item.bundlePath, ref tempData);
                item.version = crc[0];
                item.compress_crc = crc[1];
            }
        }
        Debug.Log("End Compress");
        iState  =   2;
    }
    static void BuildBundleItem(BundleItem item)
    {
        lock (lockObj)
        {
            lstCompress.Add(item);
        }
    }
	static void exportBundles(BuildTarget target,bool bReBuild_If_Exist)
	{
        //System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(CompressThreadMain));
        //t.Start();

		Caching.CleanCache();
		
		TextAsset xmlAsset = AssetDatabase.LoadAssetAtPath("Assets/bundleInfo.xml",typeof(TextAsset)) as TextAsset;
		
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(xmlAsset.text);
		
		XmlNode root = xmldoc.SelectSingleNode("Bundles");
		
		List<BundleItem> bundles = new List<BundleItem>();
		
		if(root != null)
		{
			XmlNodeList nodelist = root.ChildNodes;
			foreach(XmlNode node in nodelist)
			{
				XmlElement xmlElement = (XmlElement)node;
				
				BundleItem item = new BundleItem();
				item.bundlePath = xmlElement.GetAttribute("url");
				item.localPath = xmlElement.GetAttribute("path");
				item.version = uint.Parse(xmlElement.GetAttribute("version"));
                item.compress_crc = uint.Parse(xmlElement.GetAttribute("compress_crc"));
				item.isScene = bool.Parse(xmlElement.GetAttribute("scene"));
				item.isGlobal = bool.Parse(xmlElement.GetAttribute("isGlobal"));
				item.isCompress	=	bool.Parse(xmlElement.GetAttribute("isCompress"));
				//if(item.isScene)
				{
					XmlNodeList dependList = node.ChildNodes;
					
					if(dependList.Count > 0)
					{
						item.dependency = new int[dependList.Count];
						item.dependStr = new string[dependList.Count];
						
						for(int i = 0; i < dependList.Count; i++)
						{
							XmlElement dependElement = (XmlElement)dependList[i];
							string dependPath = dependElement.GetAttribute("url");
							/*for(int j =0; j < bundles.Count; j++)
							{
								if(bundles[j].bundlePath == dependPath)
								{
									item.dependency[i] = j;
									break;
								}
							}*/
							item.dependStr[i] = dependPath;
						}
					}
				}
				bundles.Add(item);
			}
		}

        
		for(int i = 0; i < bundles.Count; i++)
		{
			int dependSize = 0;
			if(bundles[i].dependency != null)
				dependSize = bundles[i].dependency.Length;
			
			for(int j = 0; j < dependSize; j++)
			{
				string dependPath = bundles[i].dependStr[j];
				for(int k =0; k < bundles.Count; k++)
				{
					if(bundles[k].bundlePath == dependPath)
					{
						bundles[i].dependency[j] = k;
						break;
					}
				}
			}
		}

		BuildAssetBundleOptions options = 
			BuildAssetBundleOptions.CollectDependencies | 
			BuildAssetBundleOptions.CompleteAssets | 
			BuildAssetBundleOptions.DeterministicAssetBundle;

		int pushCount = 0;
		string path = "Assets/bundleInfo.xml";
		//BuildPipeline.PushAssetDependencies();
		//pushCount++;
		//BuildCompressAssetBundle(AssetDatabase.LoadMainAssetAtPath(path), null, "bundle/bundleInfo.unity3d", options,target);
		
		BuildPipeline.PushAssetDependencies();
		pushCount++;
        BuildCompressAssetBundle(bundles[0],AssetDatabase.LoadMainAssetAtPath(bundles[0].localPath), null, "bundle/" + bundles[0].bundlePath, options, target);

	

		for(int i = 1; i < bundles.Count; i++)
		{
            if (!bReBuild_If_Exist)
            {
                if (File.Exists("bundle/" + bundles[i].bundlePath))
                {
                    continue;
                }
            }
			if(!bundles[i].isScene)
			{
				doExportBundle(bundles[i].localPath,"bundle/" + bundles[i].bundlePath,
					options,target,bundles,i,ref pushCount);
				//BuildPipeline.PopAssetDependencies();
				//瀵箂hader鐨勪緷璧栬?鐣欑潃..
				for(int j = 0; j < pushCount-1; j++)
				{
					BuildPipeline.PopAssetDependencies();
				}
				pushCount = 1;
			}
			else
			{
				doExportScene(bundles[i].localPath,"bundle/" + bundles[i].bundlePath,options,target,bundles,i,ref pushCount);
				for(int j = 0; j < pushCount-1; j++)
				{
					BuildPipeline.PopAssetDependencies();
				}
				pushCount = 1;
			}
		}
		
		
		for(int i = 0; i < pushCount; i++)
		{
			BuildPipeline.PopAssetDependencies();
		}
        byte[] tempCompressData2 = new byte[10];
        for (int i = 0; i < bundles.Count; i++)
        {
            BundleItem item = bundles[i];

            if (item != null)
            {
                //Debug.Log("Compress item 1");
                uint[] crc = Compress("bundle/" + item.bundlePath, ref tempCompressData2);
                item.version = crc[0];
                item.compress_crc = crc[1];
            }
        }


		if(root != null)
		{
			XmlNodeList nodelist = root.ChildNodes;
            List<XmlNode> lstEmptyNode    =   new List<XmlNode>();
            //remove empty bundle
            
			foreach(XmlNode node in nodelist)
			{
				XmlElement xmlElement = (XmlElement)node;
				string buildpath	=	xmlElement.GetAttribute("url");
				uint version = 0;
                uint compress_crc = 0;
				for(int i=0;i<bundles.Count;i++)
				{
					if(bundles[i].bundlePath == buildpath)
					{
						version	=	bundles[i].version;
                        compress_crc = bundles[i].compress_crc;
                        string name = "bundle/" + buildpath;
                        if (!File.Exists(name.Replace("$", "__") + version.ToString()))
                        {
                            lstEmptyNode.Add(node);
                        }
						break;
					}
				}
				xmlElement.SetAttribute("version",version.ToString());
                xmlElement.SetAttribute("compress_crc", compress_crc.ToString());
                
			}

            foreach (XmlNode n in lstEmptyNode)
            {
                root.RemoveChild(n);
            }
            lstEmptyNode.Clear();
		}

        string content = File.ReadAllText("Assets/Resources/AppVersion.txt");
        Dictionary<string, string> appversion = ReadINI(content);
        string bundle_version = appversion["versionName"].Split('.')[3];
        //xmldoc.Save("bundle/bundleinfo.xml" + bundle_version.ToString());
        
		//Export Movie..
		string[] movieFiles	=	Directory.GetFiles("Assets/Movie/");
		Directory.CreateDirectory("bundle/Movie/");
        byte[] tempData = new byte[10];
		foreach(string file in movieFiles)
		{
			
			if(!file.EndsWith(".meta"))
			{
				XmlElement ele = xmldoc.CreateElement("Bundle");
				string name = file.Replace("Assets/","");
				File.Copy(file,"bundle/"+name,true);
				
				uint crc = 0;
                uint compress_crc = 0;
				bool bCompress = !file.EndsWith(".mp4");
				if(bCompress)
				{

                    uint[] crc_array_ = Compress("bundle/" + name, ref tempData);
                    crc = crc_array_[0];
                    compress_crc = crc_array_[1];
				}
				else
				{
					crc	=	CalcCRC("bundle/"+name);
                    compress_crc = crc;
                    string dst = "bundle/" + name + crc.ToString();
                    if (File.Exists(dst))
                    {
                        File.Delete(dst);
                    }
                    File.Move("bundle/" + name, "bundle/" + name + crc.ToString());
				}
				
				ele.SetAttribute("url",name);
				ele.SetAttribute("path","Assets/"+name);
				ele.SetAttribute("version",crc.ToString());
                ele.SetAttribute("compress_crc", compress_crc.ToString());
				ele.SetAttribute("scene","False");
				ele.SetAttribute("isGlobal","False");
				ele.SetAttribute("isCompress",bCompress.ToString());
				root.AppendChild(ele);
			}
		}
		
        string xmlFileName  =   "bundle/bundleinfo.xml" + bundle_version.ToString();
        xmldoc.Save(xmlFileName);

        string csvContent = "id,url,path,version,compress_crc,scene,isGlobal,isCompress,depend\n";
        XmlNodeList bundleNodeList = root.ChildNodes;
        
        Hashtable table=    new Hashtable();
        int idx =   0;
        foreach (XmlNode node in bundleNodeList)
        {
            XmlElement ele = (XmlElement)node;
            string url = ele.GetAttribute("url");
            table[url] = idx;
            idx++;
        }
        idx = 0;
        foreach (XmlNode node in bundleNodeList)
        {
            XmlElement ele = (XmlElement)node;
            csvContent += idx.ToString() + "," +
            ele.GetAttribute("url") + "," +
            ele.GetAttribute("path") + "," +
            ele.GetAttribute("version") + "," +
            ele.GetAttribute("compress_crc") + ",";


            if (ele.GetAttribute("scene") == "False")
            {
                csvContent += "0,";
            }
            else
            {
                csvContent += "1,";
            }
            if (ele.GetAttribute("isGlobal") == "False")
            {
                csvContent += "0,";
            }
            else
            {
                csvContent += "1,";
            }
            if (ele.GetAttribute("isCompress") == "False")
            {
                csvContent += "0,";
            }
            else
            {
                csvContent += "1,";
            }


            XmlNodeList dependList = node.ChildNodes;

            if (dependList.Count > 0)
            {

                for (int j = 0; j < dependList.Count; j++)
                {
                    XmlElement dependElement = (XmlElement)dependList[j];
                    string dependPath = dependElement.GetAttribute("url");

                    int id = 0;
                    if (table.ContainsKey(dependPath))
                    {
                        id = (int)table[dependPath];
                    }
                    else 
                    {
                        Debug.Log(ele.GetAttribute("url"));
                        Debug.Log(dependPath + " doesn't exist!");
                        continue;
                    }
                    if (j == dependList.Count - 1)
                    {
                        csvContent += id.ToString();
                    }
                    else
                    {
                        csvContent += id.ToString()+";";
                    }
                }
            }
            csvContent += "\n";
            idx++;
        }

        File.WriteAllText(xmlFileName, csvContent);

        int len =   ReadFile(xmlFileName, ref tempData);
        if (len > 0)
        {
            File.Delete(xmlFileName);
            FileStream fileWrite = new FileStream(xmlFileName, FileMode.CreateNew);
            MemoryStream ms = new MemoryStream(tempData, 0, len);
            SD.Compress(ms, fileWrite);
            fileWrite.Close();
        }
		AssetDatabase.Refresh();
	}
		
	[MenuItem("Assets/AssetBundle/CreateBundleXML")]
	static void CreateBundleXML()
	{
		string[] dirs = Directory.GetDirectories(Application.dataPath);
		
		List<BundleItem> bundles = new List<BundleItem>();
		
		foreach(string dir in dirs)
		{
			string filePath = dir.Replace("\\", "/");
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index+1);
			string localPath = "";
			if (index > 0)
				localPath = filePath;
			PushXML(localPath,bundles);
		}
		
		//Create Dependencies
		
		for(int i = 0; i < bundles.Count; i++)
		{
			Debug.Log(bundles[i].localPath);
			if(bundles[i].localPath[bundles[i].localPath.Length-1] == '/')
			{
				int index = bundles[i].localPath.LastIndexOf("/");
				string folderPath = bundles[i].localPath.Substring(0,index);
				List<string> allFiles = new List<string>();
				CreatePathDependence(folderPath,allFiles);
				CreateFileDependence(allFiles.ToArray(),bundles,i);
			}
			else
			{
				string[] itemPath = new string[1];
				itemPath[0] = bundles[i].localPath;
				CreateFileDependence(itemPath,bundles,i);
			}
		}
		
		//Create XML
		
		XmlDocument xmldoc = new XmlDocument();
		
		XmlElement root = xmldoc.CreateElement("Bundles");
		
		//add shader lib bundle
		XmlElement shaderLib = xmldoc.CreateElement("Bundle");
		shaderLib.SetAttribute("url","shaderLib.unity3d");
		shaderLib.SetAttribute("path","Assets/Prefab/ShaderLib.prefab");
		shaderLib.SetAttribute("version","1");
        shaderLib.SetAttribute("compress_crc", "1");
		shaderLib.SetAttribute("scene","False");
		shaderLib.SetAttribute("isGlobal","True");
		shaderLib.SetAttribute("isCompress","True");
		root.AppendChild(shaderLib);
		
		foreach(BundleItem item in bundles)
		{
			XmlElement dbItem = xmldoc.CreateElement("Bundle");
			dbItem.SetAttribute("url",item.bundlePath);
			dbItem.SetAttribute("path",item.localPath);
			dbItem.SetAttribute("version","1");
            dbItem.SetAttribute("compress_crc", "1");
			dbItem.SetAttribute("scene",item.isScene.ToString());
			dbItem.SetAttribute("isGlobal","False");
			dbItem.SetAttribute("isCompress","True");
			//dbItem.SetAttribute("refOnly",item.refOnly.ToString());
			if(item.dependency != null)
			{
				foreach(int depend in item.dependency)
				{
					XmlElement dependItem = xmldoc.CreateElement("Dependency");
					dependItem.SetAttribute("url",bundles[depend].bundlePath);
					dbItem.AppendChild(dependItem);
				}
			}
			root.AppendChild(dbItem);
		}
		
		xmldoc.AppendChild(root);
        string xmlFileName = Application.dataPath + "/" + "bundleInfo.xml";
        xmldoc.Save(xmlFileName);

	}
	
	static void PushXML(string path,List<BundleItem> objs)
	{
		path = path.Replace("Assets/", "");
		int flagId  = path.LastIndexOf("$");
		
		if(path.Contains("Resources") || path.Contains(".svn") || path.Contains("StreamingAssets"))
		{
			return;
		}
		if(flagId >= 0)
		{
			BundleItem item = new BundleItem();
			item.bundlePath = path + ".unity3d";
			item.localPath = "Assets/" + path + "/";
			item.isScene = false;
			objs.Add(item);
		}
		else	
		{
			string [] fileEntries = Directory.GetFiles(Application.dataPath+"/"+path);
		
			foreach(string fileName in fileEntries)
			{
				string filePath = fileName.Replace("\\", "/");
				int index = filePath.LastIndexOf("/");
				filePath = filePath.Substring(index);
				int fileFlagId = filePath.LastIndexOf("$");
				if(fileFlagId >= 0)
				{
					string localPath = "Assets/" + path;
					if (index > 0)
						localPath += filePath;
					
					int dotIndex = filePath.LastIndexOf(".");
					string ext = filePath.Substring(dotIndex);
					if(ext == ".meta")
						continue;
					
					BundleItem item = new BundleItem();
					item.localPath = localPath;
					
					if(ext == ".unity")
					{
						item.isScene = true;
					}
					else
						item.isScene = false;
					item.bundlePath = path + filePath.Replace("." + ext,"") + ".unity3d";
					objs.Add(item);
				}
			}
			
		
			string[] dirs = Directory.GetDirectories(Application.dataPath+"/"+path);
			foreach(string dir in dirs)
			{
				string filePath = dir.Replace("\\", "/");
				int index = filePath.LastIndexOf("/");
				filePath = filePath.Substring(index);
				string localPath = "Assets/" + path;
				if (index > 0)
					localPath += filePath;
				PushXML(localPath,objs);
			}
		}
	}
	
	static void CreatePathDependence(string path,List<string> allFiles)
	{
		path = path.Replace("Assets/", "");
		
		string [] fileEntries = Directory.GetFiles(Application.dataPath+"/"+path);
		
		for(int i = 0; i < fileEntries.Length; i++)
		{
			string filePath = fileEntries[i].Replace("\\", "/");
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index);
			
			string localPath = "Assets/" + path;
			if (index > 0)
				localPath += filePath;
			
			allFiles.Add(localPath);
		}
		
		string[] dirs = Directory.GetDirectories(Application.dataPath+"/"+path);
		foreach(string dir in dirs)
		{
			string filePath = dir.Replace("\\", "/");
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index);
			if( filePath == "/.svn" ) continue;
			string localPath = "Assets/" + path;
			if (index > 0)
				localPath += filePath;
			CreatePathDependence(localPath,allFiles);
		}
	}
	
	static void CreateFileDependence(string[] allFiles,List<BundleItem> bundles,int index)
	{
		string[] dependencies = AssetDatabase.GetDependencies(allFiles);
		List<string> dependency = new List<string>();
		foreach(string depend in dependencies)
		{
			string depend2 = depend.Replace("\\", "/");
			int dotIndex = depend2.LastIndexOf(".");
			string ext = depend2.Substring(dotIndex+1);
					
			if(ext == "cs")
				continue;
					
			int flagIndex = depend2.IndexOf("$");
					
			if(flagIndex < 0)
			{
				Debug.LogWarning("reference asset doesn't packed in any bundle: " + depend2);
			}
			else
			{
				int cutIndex = depend2.IndexOf("/",flagIndex);
				string bundlePath = depend2;
				
				if(cutIndex >= 0)
				{
					bundlePath = depend2.Substring(0,cutIndex+1);	
				}
				dependency.Add(bundlePath.Replace("Assets/",""));
			}
		}
				
		if(dependency.Count > 0)
		{
			//item.dependency = new int[dependency.Count];
			List<int> deps = new List<int>();
			for(int i = 0; i < dependency.Count; i++)
			{
				for(int j = 0; j < bundles.Count; j++)
				{
					if(j == index)
						continue;
					
					int result = string.Compare(bundles[j].localPath,"Assets/" + dependency[i],true);
					if(result == 0)
					{
						bool have = false;
						foreach(int dep in deps)
						{
							if(dep == j)
							{
								have = true;
								break;
							}
						}
						if(!have)
						{
							deps.Add(j);
						}
						break;
					}
				}
			}
			bundles[index].dependency = deps.ToArray();
		}
	}
	
	
	[MenuItem("Assets/AssetBundle/CheckBundleDependence")]
	static void CheckBundleDependence()
	{
		string[] dirs = Directory.GetDirectories(Application.dataPath);
		List<BundleItem> bundles = new List<BundleItem>();
		
		foreach(string dir in dirs)
		{
			string filePath = dir.Replace("\\", "/");
			int index = filePath.LastIndexOf("/");
			filePath = filePath.Substring(index+1);
			string localPath = "";
			if (index > 0)
				localPath = filePath;
			PushXML(localPath,bundles);
		}
	
		// Check bundle dependence
		FileInfo f = new FileInfo("c://CheckBundleDependence.txt");
		StreamWriter sw = f.CreateText();
		FileInfo f2 = new FileInfo("c://CheckBundleSameName.txt");
		StreamWriter sw2 = f2.CreateText();
	
		for(int i = 0; i < bundles.Count; i++)
		{
			if(bundles[i].localPath[bundles[i].localPath.Length-1] == '/')
			{
				int index = bundles[i].localPath.LastIndexOf("/");
				string folderPath = bundles[i].localPath.Substring(0,index);
				List<string> allFiles = new List<string>();
				CreatePathDependence(folderPath,allFiles);
				CreateFileDependenceCheck(allFiles.ToArray(),sw,sw2);
			}
			else
			{
				string[] itemPath = new string[1];
				itemPath[0] = bundles[i].localPath;
				CreateFileDependenceCheck(itemPath,sw,sw2);
			}
		}
		
		sw.Close();
		sw.Dispose();
		sw2.Close();
		sw2.Dispose();
	}
	
	static void CreateFileDependenceCheck(string[] allFiles,StreamWriter sw,StreamWriter sw2)
	{
		foreach(string onefile in allFiles)
		{
			string[] s1 = {onefile};	
			int idx1 = onefile.IndexOf("$");
			if( idx1 < 0 ) continue;
			int idx2 = onefile.IndexOf("/",idx1);
			if( idx2 < 0 ) idx2 = onefile.Length;
			string s0 = onefile.Substring(idx1,idx2-idx1);
			
			string[] s2 = AssetDatabase.GetDependencies(s1);
			bool b = true;
			foreach(string s in s2)
			{
				string ext = "";
				idx1 = s.LastIndexOf(".");
				if( idx1 >= 0 ) ext = s.Substring(idx1);
				if( ext==".cs" || ext==".shader" ) continue;
				
				idx1 = s.IndexOf("$");
				if( idx1 < 0 ) 
				{
					sw.WriteLine("  [ERR]: "+s0+"->"+s);
					continue;
				}
				idx2 = s.IndexOf("/",idx1);
				if( idx2 < 0 ) idx2 = s.Length;
				string sb = s.Substring(idx1,idx2-idx1);
				
				if( s0 != sb )
				{
					if( b )
					{
						sw.WriteLine(onefile+" :");
						b = false;
					}
					sw.WriteLine("  -> "+s);
				}
			}
			
			// Check Same Name in one bundle
			/*
			string ext2 = "";
			idx1 = onefile.LastIndexOf(".");
			if( idx1 >= 0 ) ext2 = onefile.Substring(idx1);
			if( ext2 == ".meta" ) continue;
			
			idx1 = onefile.LastIndexOf("/");
			if( idx1 < 0 ) idx1 = 0; else idx1++;
			idx2 = onefile.LastIndexOf(".");
			if( idx2 < 0 ) idx2 = onefile.Length;
			string sfn1 = onefile.Substring(idx1,idx2-idx1);
			
			foreach(string onefile2 in allFiles)
			{
				string ext3 = "";
				idx1 = onefile2.LastIndexOf(".");
				if( idx1 >= 0 ) ext3 = onefile2.Substring(idx1);
				if( ext3 == ".meta" ) continue;
				
				idx1 = onefile2.LastIndexOf("/");
				if( idx1 < 0 ) idx1 = 0; else idx1++;
				idx2 = onefile2.LastIndexOf(".");
				if( idx2 < 0 ) idx2 = onefile2.Length;
				string sfn2 = onefile2.Substring(idx1,idx2-idx1);
				
				if( sfn1==sfn2 && onefile!=onefile2 )
				{
					sw2.WriteLine(onefile+" == "+onefile2);
				}
			}
			*/
		}
	}
	static void BuildCompressAssetBundle(BundleItem item,UnityEngine.Object mainAsset,
	                             UnityEngine.Object[] assets,
	                             string pathName,
	                             UnityEditor.BuildAssetBundleOptions assetBundleOptions,
	                             UnityEditor.BuildTarget targetPlatform)
	{
		uint crc= 0;
		string bundlename	=	pathName;
#if UNITY_4_5_2
		assetBundleOptions|=UnityEditor.BuildAssetBundleOptions.UncompressedAssetBundle;
		BuildPipeline.BuildAssetBundle(mainAsset,assets,bundlename,out crc,assetBundleOptions,targetPlatform);
#else
		BuildPipeline.BuildAssetBundle(mainAsset,assets,bundlename,assetBundleOptions,targetPlatform);
#endif


        BuildBundleItem(item);
	}
    static void BuildCompressScene(BundleItem item, 
                                    string[] levels,
	                               string locationPath,
	                               UnityEditor.BuildTarget target)
	{
		uint crc = 0;
		string bundlename	=	locationPath;
#if UNITY_4_5_2
		BuildPipeline.BuildStreamedSceneAssetBundle(levels,bundlename,target,out crc,BuildOptions.UncompressedAssetBundle);
#else
		BuildPipeline.BuildStreamedSceneAssetBundle(levels,bundlename,target);
#endif

        BuildBundleItem(item);
	}
    static int ReadFile(string str,ref byte[] data)
    {
        int len = 0;
         try
         {
             FileStream f = new FileStream(str, FileMode.Open);
             len = (int)f.Length;
             if (data.Length < f.Length)
             {
                 data = new byte[(int)f.Length];
             }

             f.Read(data, 0, len);
             f.Close();
         }
         catch (System.Exception e)
         {
             //Debug.Log("Read File Error");
             Debug.Log(e.Message);
         }
         return len;
    }
    static uint[] Compress(string pathName,ref byte[] data)
	{
#if UNITY_4_5_2
        //Debug.Log("Read"+pathName);
        int len =   ReadFile(pathName, ref data);
        if (len == 0)
        {
            return new uint[] { 0, 0 };
        }

        uint crc = SevenZip.CRC.CalculateDigest(data, 0, (uint)len);

        MemoryStream ms = new MemoryStream(data, 0, len);
        string compressname =   pathName.Replace("$","__")+crc.ToString();
        if (File.Exists(compressname))
        {
            File.Delete(compressname);
        }
        FileStream fileWrite = new FileStream(compressname, FileMode.Create);
        try
        {
            SD.Compress(ms, fileWrite);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
		fileWrite.Close();
		
		File.Delete(pathName);

        len = ReadFile(compressname, ref data);
        uint crc_compress = SevenZip.CRC.CalculateDigest(data, 0, (uint)len);
		
		return new uint[]{crc,crc_compress};
#else
        return new uint[] { CalcCRC(pathName) };
#endif
	}
	static uint CalcCRC(string pathName)
	{

		byte[] data = File.ReadAllBytes(pathName);
        uint    crc = SevenZip.CRC.CalculateDigest(data,0,(uint)data.Length);
        return crc;
	}
}
