using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void CSVLoadCallBack(string key,SDCSV csv);

/// <summary>
/// CSV文件读取aa
/// </summary>
public class SDCSV{
	public Hashtable csvTable = null;
	public List<Hashtable> listTable = null;
	private CSVLoadCallBack callBack = null;
	private string path;
	
	public void LoadCSV(string path,CSVLoadCallBack backFunc)
	{
		this.path = path;
		callBack = backFunc;
		ResLoadParams param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResourceImmediately(path,CreateCSV,param);
	}
	
	public void LoadCSVList(string path, CSVLoadCallBack backFunc)
	{
		this.path = path;
		callBack = backFunc;
		ResLoadParams param = new ResLoadParams();
        sdResourceMgr.Instance.LoadResourceImmediately(path, CreateCSVList, param);

	}
    public void LoadCSVResource(string path)
    {
        this.path = path;
        callBack = null;
        Object obj = Resources.Load(path, typeof(TextAsset));
        ResLoadParams param = new ResLoadParams();
        CreateCSV(param, obj);
    }
	
	public void LoadCSVListTestMode(string path, CSVLoadCallBack backFunc)
	{
		this.path = path;
		callBack = backFunc;
		Object obj	=	UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/"+path,typeof(TextAsset));
		ResLoadParams param = new ResLoadParams();
		CreateCSVList(param,obj);
	}
	public	void	LoadCSVInTestMode(string path,CSVLoadCallBack backFunc)
	{
		this.path = path;
		callBack = backFunc;
		Object obj	=	UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/"+path,typeof(TextAsset));
		ResLoadParams param = new ResLoadParams();
		CreateCSV(param,obj);
	}
	
	protected void CreateCSVList(ResLoadParams param, Object obj)
	{
		TextAsset csv = (TextAsset)obj;
		if(csv == null)
			return;
		listTable = new List<Hashtable>();
		string csvText = csv.text;
		int headIndex = csvText.IndexOf("\n");
		string headStr = csvText.Substring(0, headIndex);
		string[] heads = headStr.Split(new char[]{','});
		int lastIndex = headIndex;
		headIndex = csvText.IndexOf("\n", lastIndex+1);
		while(headIndex >= 0)
		{
			string tempStr = csvText.Substring(lastIndex, headIndex - lastIndex);
			string[] val = tempStr.Split(new char[]{','});
			Hashtable subTable = new Hashtable();
			for(int i = 0; i < heads.Length && i < val.Length; i++)
			{
				string v = val[i];
				subTable.Add(heads[i].Replace("\r","").Replace("\n",""),v.Replace("\r","").Replace("\n",""));
			}
			listTable.Add(subTable);
			lastIndex = headIndex;
			headIndex = csvText.IndexOf("\n",lastIndex+1);
		}
		Resources.UnloadAsset(csv);
		if(callBack != null)
		{
			callBack(path,this);
		}

	}
	
	protected void CreateCSV(ResLoadParams param,Object obj)
	{
		TextAsset csv = (TextAsset)obj;
		if(csv == null)
			return;
		csvTable = new Hashtable();		
		string csvText = csv.text;		
		int headIndex = csvText.IndexOf("\n");
		string headStr = csvText.Substring(0,headIndex);
		string[] heads = headStr.Split(new char[]{','});		
		int lastIndex = headIndex;
		headIndex = csvText.IndexOf("\n",lastIndex+1);
		while(headIndex >= 0)
		{
			string tmpStr = csvText.Substring(lastIndex,headIndex-lastIndex);
			string[] val = tmpStr.Split(new char[]{','});			
			Hashtable subTable = new Hashtable();
			for(int i = 0; i < heads.Length; i++)
			{
				string v = "";
				if(i < val.Length)
					v = val[i];
				if (subTable.ContainsKey(heads[i].Replace("\r","").Replace("\n","")))
				{
					continue;
				}
				subTable.Add(heads[i].Replace("\r","").Replace("\n",""),v.Replace("\r","").Replace("\n",""));
			}
			string strKey	=	val[0].Replace("\r","").Replace("\n","");
			if(!csvTable.ContainsKey(strKey))
			{
				csvTable.Add(strKey,subTable);
			}
			lastIndex = headIndex;
			headIndex = csvText.IndexOf("\n",lastIndex+1);
		}
		Resources.UnloadAsset(csv);
		
		if(callBack != null)
		{
			callBack(path,this);
		}
	}
	
	public void CreateCSVFromStr(string str,int keyCol)
	{
		csvTable = new Hashtable();
		
		//todo:UNICODE
		string csvText = str;
		
		//read head line
		int headIndex = csvText.IndexOf("\n");
		string headStr = csvText.Substring(0,headIndex);
		string[] heads = headStr.Split(new char[]{','});
		
		int lastIndex = headIndex;
		headIndex = csvText.IndexOf("\n",lastIndex+1);
		while(headIndex >= 0)
		{
			string tmpStr = csvText.Substring(lastIndex,headIndex-lastIndex);
			string[] val = tmpStr.Split(new char[]{','});
			
			Hashtable subTable = new Hashtable();
			for(int i = 0; i < heads.Length; i++)
			{
				string v = "";
				if(i < val.Length)
					v = val[i];
				subTable.Add(heads[i].Replace("\r","").Replace("\n",""),v.Replace("\r","").Replace("\n",""));
				//Debug.Log(heads[i] + ":" + v);
			}
			csvTable.Add(val[keyCol].Replace("\r","").Replace("\n",""),
				subTable);
			
			lastIndex = headIndex;
			headIndex = csvText.IndexOf("\n",lastIndex+1);
		}
	}
}
