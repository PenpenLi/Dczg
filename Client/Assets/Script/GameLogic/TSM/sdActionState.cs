using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class sdActionState : object
{
	
	
	public sdActionState(string _name, string _enter, string _update, string _leave, bool _isPassive)
	{
		name = _name;
		enterFuncName = _enter;
		updateFuncName = _update;
		leaveFuncName = _leave;
		isPassive = _isPassive;
		properties = new Hashtable();
	}		
	//public delegate bool ExcuteFuncType(SDUnit self, ActionState state);
	
	public string 		name;
	public bool 		isPassive;
	public string 		enterFuncName, updateFuncName, leaveFuncName;
	public Hashtable 	properties;
	public int			index;
	
	public MethodInfo 	enterFunc;
	public MethodInfo 	updateFunc;
	public MethodInfo 	leaveFunc;		
	
	public static ArrayList deserializeStateData(string data, out int actionCount, bool editorMode)
	{
		int countLenth = data.IndexOf("#");
		int stateCount = Convert.ToInt32(data.Substring(0, countLenth));
		ArrayList stateConf = new ArrayList();
		
		string[] stateString = data.Split('#');
		int index = 0;
		int actionIndex = 0;
		actionCount = stateCount;
		foreach (string word in stateString)
		{
	    	if(index > 0)
			{
				int i0 = word.IndexOf("@");
				string objInfo = word;
				if(i0 > 0)
				{
					objInfo = word.Substring(0, i0);
				}
				
				//< 解析action属性
				string[] statePropInfo = objInfo.Split('$');
				sdActionState newState = new sdActionState(statePropInfo[0], statePropInfo[1],
									statePropInfo[2], statePropInfo[3], Convert.ToBoolean(statePropInfo[4]));
				newState.index = actionIndex;
				stateConf.Add(newState);

				//< 解析property
				if(i0 > 0)
				{
					string attrInfo = word.Substring(i0+1);
					string[] s2 = attrInfo.Split('@');
					foreach (string s10 in s2)
					{
						string[] attri = s10.Split('*');
						if(editorMode)
							newState.properties[attri[0]] = attri[1];
						else
							addKeyValue(newState.properties, attri[0], attri[1]);
					}
				}
				
				++actionIndex;
			}
			++index;
		}
		
		return stateConf;
	}
	
	public static void addKeyValue(Hashtable dataTable, string key, string val)
	{		
		int varType = Convert.ToInt32(val.Substring(0, 1));
		string varValues = val.Substring(2);
		bool isArray = varValues.Contains(";");
		if(isArray)
		{	
			string[] singleValues = varValues.Split(';');
			switch(varType)
			{
				case 0:
				{
					int[] varArray = new int[singleValues.Length];
					for(int iii = 0; iii < singleValues.Length; ++iii)
					{
						varArray[iii] = Convert.ToInt32(singleValues[iii]);
					}
					dataTable[key] = varArray;
				}
				break;
				case 1:
				{
					float[] varArray = new float[singleValues.Length];
					for(int iii = 0; iii < singleValues.Length; ++iii)
					{
						varArray[iii] = Convert.ToSingle(singleValues[iii]);
					}
					dataTable[key] = varArray;
				}
				break;
				case 2:
				{
					string[] varArray = new string[singleValues.Length];
					for(int iii = 0; iii < singleValues.Length; ++iii)
					{
						varArray[iii] = singleValues[iii];
					}
					dataTable[key] = varArray;
				}
				break;
				case 3:
				{
					int[] varArray = new int[singleValues.Length];
					for(int iii = 0; iii < singleValues.Length; ++iii)
					{
						varArray[iii] = Convert.ToInt32(singleValues[iii]);
					}
					dataTable[key] = varArray;
				}
				break;
				case 4:
				break;
			}
							//<
		}
		else
		{	
			switch(varType)
			{
				case 0:
				{
					//Debug.Log (key);
					dataTable[key] = Convert.ToInt32(varValues);
				}
				break;
				case 1:
				{
					dataTable[key] = Convert.ToSingle(varValues);
				}
				break;
				case 2:
				{
					dataTable[key] = varValues;
				}
				break;
				case 3:
				{
					dataTable[key] = Convert.ToInt32(varValues);
				}
				break;
				case 4:
				break;
			}
							//<
		}
	}
	
	public static Hashtable loadAudioConfData(object audioConf)
	{
		if(audioConf == null)
			return null;
		
		Hashtable subTable = new Hashtable();
		if(audioConf.GetType().IsArray)
		{
			string[] audioData = (string[])audioConf;
			foreach(string str in audioData)
			{
				string[] param = str.Split('^');
				float tp = Convert.ToSingle(param[1]);
				subTable[tp] = param[0];
			}
		}
		else
		{
			string str = audioConf as string;
			string[] param = str.Split('^');
			float tp = Convert.ToSingle(param[1]);
			subTable[tp] = param[0];
		}
		return subTable;		
	}	
}
