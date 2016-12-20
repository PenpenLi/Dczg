using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public	class TaskParam
{
	public	ResLoadDelegate	_cb;
	public	ResLoadParams	_param;
}
public	class ResourceTask
{
	public	string					_name;
	public	Object					_object;
	public	ResLoadParams			_param;
	public	List<TaskParam> 		lstTask 	= new List<TaskParam>();
	public	bool					failed	=	false;
    public  uint                     Ref = 0;
	public	void	AddCB(ResLoadDelegate	cb,ResLoadParams param)
	{
		if(cb==null)
			return;
		TaskParam tp	=	new TaskParam();
		tp._cb			=	cb;
		tp._param		=	param;
		lstTask.Add(tp);
	}
	public	void	OnLoadFinished(Object obj)
	{
		_object	=	obj;
		if(_object==null)
		{
			failed	=	true;
		}
		foreach(TaskParam tp in lstTask)
		{
			if(tp._cb!=null)
			{
				tp._cb(tp._param,_object);
			}
		}
		lstTask.Clear();
	}
}

public class sdResourceMgr : Singleton<sdResourceMgr> 
{
	public Hashtable resourceDB = new Hashtable();
    List<sdGameMonster> lstDrop = new List<sdGameMonster>();
	uint	index	=	0;
	public bool init(bool testMode)
	{
        StartCoroutine(DoDropItem());
		return true;
	}

	
	void	AddTask(string orginPath,System.Type t,int priority)
	{
        //Debug.Log(orginPath);
		string path	=	orginPath;
		
		ResLoadParams	__param	=	new ResLoadParams();
		__param.info	=	orginPath;
		
		
		LoadRequest loadobj	=	new LoadRequest();
			
		loadobj.param			=	__param;
		loadobj.callbackFunc	=	resourceLoadCallback;
		loadobj.path			=	path;
		loadobj.resType			=	t;
        loadobj.priority = priority;
		int tmpId = path.LastIndexOf("/");
		string resName = path;
		if(tmpId >= 0)
		{
			resName = path.Substring(tmpId+1);
		}
		int dotIndex = resName.LastIndexOf(".");
		if(dotIndex >= 0)
		{
			resName = resName.Substring(0,dotIndex);
		}
		loadobj.resName	=	resName;
		
		int flagId  = path.LastIndexOf("$");
		if(flagId >= 0)
		{	
			int folderFlagId = path.IndexOf("/",flagId);
			string bundleName = path;
			if(folderFlagId >= 0)
			{
				bundleName = path.Substring(0,folderFlagId);
			}
			bundleName += ".unity3d";
			loadobj.bundleName	=	bundleName;	
			
			BundleGlobal.Instance.LoadObject( loadobj);
			
			
			
		}
		else
		{
			Debug.Log("bundle isn't exist!"+path);
			return;
		}

		

	}
    public void PreLoadResource(string path)
    {
        __PreLoadResource(path, 0, typeof(Object));
    }
    public void PreLoadResource(string path, System.Type t)
    {
        __PreLoadResource(path, 0, t);
    }
    public void PreLoadResourceDontUnload(string path)
    {
        __PreLoadResource(path, 1, typeof(Object));
    }
    public void PreLoadResourceDontUnload(string path, System.Type t)
    {
        __PreLoadResource(path, 1, t);
    }
    void __PreLoadResource(string path,uint refCount,System.Type t)
    {
        if (!BundleGlobal.IsMobile())
        {
            return;
        }
        if (path.Length == 0)
        {
            return;
        }
        bool resourceExist = resourceDB.ContainsKey(path);
		if(resourceExist)
		{
			ResourceTask	task	=	resourceDB[path] as ResourceTask;
			task.Ref = refCount;
		}
		else
		{
            

			ResourceTask task	=	new ResourceTask();
			task._name	=	path;
			task._param	=	null;
            task.Ref = refCount;
			resourceDB[path]	=	task;
			
			AddTask(path,t,-1);
		}
    }
	public void LoadResource(string path,ResLoadDelegate cb,ResLoadParams param)
	{
		LoadResource(path,cb,param,typeof(Object));
	}
	public void LoadResourceImmediately(string path,ResLoadDelegate cb,ResLoadParams param)
	{
		LoadResource(path,cb,param,typeof(Object),1);
	}
    public void LoadResourceImmediately(string path, ResLoadDelegate cb, ResLoadParams param, System.Type t)
    {
        LoadResource(path, cb, param, t, 1);
    }
	public void LoadResource(string path,ResLoadDelegate cb,ResLoadParams param,System.Type t)
	{
		LoadResource(path,cb,param,t,0);
	}
    public void LoadResource(string path, ResLoadDelegate cb, ResLoadParams param, System.Type t, int priority)
	{
		if(cb==null)
		{
			return;
		}
		if(param!=null)
		{
			param._reqIndex	=	index++;
		}
		else
		{
			index++;
		}
		bool resourceExist = resourceDB.ContainsKey(path);
		if(resourceExist)
		{
			ResourceTask	task	=	resourceDB[path] as ResourceTask;
			if(task._object!=null || task.failed)
			{
				cb(param,task._object);
				//BundleGlobal.Instance.DoCallback(cb,param,task._object);
			}
			else
			{
				task.AddCB(cb,param);
			}
		}
		else
		{
            //Debug.Log(path);
			ResourceTask task	=	new ResourceTask();
			task._name	=	path;
			task._param	=	param;
			task.AddCB(cb,param);
			resourceDB[path]	=	task;

            AddTask(path, t, priority);
		}
	}
    public void ClearTaskRef()
    {
        foreach (DictionaryEntry item in resourceDB)
        {
            ResourceTask task = item.Value as ResourceTask;
            task.Ref = 0;
        }
    }
	public	void	Clear()
	{
		Hashtable	table	=	resourceDB.Clone() as Hashtable;
		resourceDB.Clear();
		foreach(DictionaryEntry item in table)
		{
			ResourceTask	task	=	item.Value	as ResourceTask;
            if (task.lstTask.Count != 0 || task.Ref !=0)
			{
				resourceDB.Add(item.Key,item.Value);
			}
		}
		table.Clear();
        lstDrop.Clear();
	}
	public static void resourceLoadCallback(ResLoadParams param, Object obj)
	{
		string fileName = param.info;
		
		ResourceTask	task	=	sdResourceMgr.Instance.resourceDB[fileName] as ResourceTask;
		if(task!=null)
		{
			task.OnLoadFinished(obj);
		}
        //sdResourceMgr.Instance.resourceDB.Remove(fileName);
	}
    public void AddDropInfo(sdGameMonster dropinfo)
    {
        lstDrop.Add(dropinfo);
    }

    // 掉落偏移aa
    protected static Vector3[] msDropBias = new Vector3[]
	{
		new Vector3(2.0f,	0.0f,	0.0f),
		new Vector3(-2.0f,	0.0f,	0.0f),
		new Vector3(0.0f,	0.0f,	2.0f),
		new Vector3(0.0f,	0.0f,	-2.0f),
		new Vector3(1.0f,	0.0f,	1.0f),
		new Vector3(-1.0f,	0.0f,	1.0f),
		new Vector3(1.0f,	0.0f,	-1.0f),
		new Vector3(-1.0f,	0.0f,	-1.0f),
	};
    IEnumerator DoDropItem()
    {
        while (true)
        {
            if (lstDrop.Count > 0)
            {
                sdGameMonster monster = lstDrop[0];
                lstDrop.RemoveAt(0);

                SDMonsterDrop dropinfo = monster.DropInfo;
                if (dropinfo.items != null)
                {
                    for (int i = 0; i < dropinfo.items.Length; i++)
                    {
                        Hashtable info = sdConfDataMgr.Instance().GetItemById(dropinfo.items[i].ToString());
                        if (info == null)
                            continue;
                        int itemClass = int.Parse(info["Class"].ToString());
                        int itemSubClass = int.Parse(info["SubClass"].ToString());
                        int iQuility = int.Parse(info["Quility"].ToString());




                        Vector3 kDropPos = monster.transform.position;
                        DropPoint(monster.transform.position, ref kDropPos);
                        yield return 0;
                        DropItem(itemClass, itemSubClass, monster.transform.position, dropinfo.items[i], kDropPos, iQuility);
                        yield return 0;
                    }
                }

                if (dropinfo.money > 0)
                {
                    int isubclass = 0;


                    if (dropinfo.money < 100)
                    {
                        isubclass = 0;
                    }
                    else if (dropinfo.money < 1000)
                    {
                        isubclass = 1;
                    }
                    else
                    {
                        isubclass = 3;
                    }


                    Vector3 kDropPos = monster.transform.position;
                    DropPoint(monster.transform.position, ref kDropPos);
                    yield return 0;
                    DropItem(200, isubclass, monster.transform.position, dropinfo.money, kDropPos, 0);
                    yield return 0;
                }
            }
            else
            {
                yield return 0;
            }
        }
    }
    // 计算掉落点aa
    protected static bool DropPoint(Vector3 kPos, ref Vector3 kDropPos)
    {
        int iDropCount = 0;
        int iRandIndex = Random.Range(0, 7);
        bool bHitted = false;
        while (!bHitted)
        {
            int iIdx = (iRandIndex + iDropCount) % 8;

            Vector3 kOrigin = kPos + msDropBias[iIdx];
            kOrigin.y += 2.0f;

            Ray kRay = new Ray(kOrigin, new Vector3(0.0f, -1.0f, 0.0f));
            RaycastHit hit;
            int layerMonster = LayerMask.NameToLayer("Monster");
            int layerPlayer = LayerMask.NameToLayer("Player");
            int layerPet = LayerMask.NameToLayer("Pet");
            int mask = (1 << layerMonster) | (1 << layerPlayer) | (1 << layerPet);
            int invMask = ~mask;

            bHitted = Physics.Raycast(kRay, out hit, 100000.0f, invMask);
            if (bHitted)
            {
                kDropPos = hit.point;
                return true;
            }
            else if (iDropCount == 7)
            {
                kDropPos = kPos + msDropBias[iIdx];
                return false; ;
            }

            iDropCount++;
        }

        return true;
    }

    // 掉落aa
    protected static void DropItem(int iclass, int isubclass, Vector3 kPos, int iData, Vector3 kDropPos, int iQuility)
    {
        int id = iclass * 100 + isubclass;
        string strmodel = "";
        Hashtable table = sdConfDataMgr.Instance().GetTable("dropmodel");
        if (table != null)
        {
            strmodel = table[id] as string;
        }

        ResLoadParams kParam = new ResLoadParams();
        kParam.pos = kPos;
        kParam.userdata0 = kDropPos;
        kParam.userdata1 = iclass;
        kParam.userdata2 = iData;
        kParam.userdata3 = iQuility;

        string kPath = "$treasureDrop01/drop_treasureDrop01.prefab";
        if (strmodel != null)
        {
            if (strmodel.Length > 0)
            {
                kPath = strmodel;
            }
        }

        sdResourceMgr.Instance.LoadResource("Model/drop/" + kPath, OnDropItem, kParam);
    }

    // 掉落加载回调aa
    protected static void OnDropItem(ResLoadParams kParam, UnityEngine.Object kObj)
    {
        if (kObj == null)
            return;

        GameObject kDropItem = GameObject.Instantiate(kObj, kParam.pos, kParam.rot) as GameObject;
        if (kDropItem != null)
        {
            sdDropItem kItem = kDropItem.GetComponent<sdDropItem>();
            kItem.SetDest((Vector3)kParam.userdata0);

            int iclass = (int)kParam.userdata1;
            if (iclass == 200)
            {
                kItem.SetMoney((int)kParam.userdata2);
            }
            else
            {
                kItem.SetItemId((int)kParam.userdata2);

                Color[] QuilityColor = new Color[6]
				{
					new Color(0.5f,0.5f,0.5f,1.0f),
					new Color(0.8f,0.8f,0.8f,1.0f),
					new Color(0.1f,0.8f,0.1f,1.0f),
					new Color(0.1f,0.1f,0.8f,1.0f),
					new Color(0.8f,0.1f,0.8f,1.0f),
					new Color(0.8f,0.8f,0.1f,1.0f)
				};

                ParticleSystem[] psArray = kDropItem.GetComponentsInChildren<ParticleSystem>();
                if (psArray != null)
                {
                    foreach (ParticleSystem ps in psArray)
                    {
                        ps.startColor = QuilityColor[(int)kParam.userdata3];
                    }
                }
            }
        }
    }
}   
