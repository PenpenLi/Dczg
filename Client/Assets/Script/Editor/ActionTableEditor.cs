//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System;
//using System.Reflection;
//using System.IO;
//
//public class ActionTableEditor : EditorWindow 
//{
//	string 	actionName = "";
//	string	enterFunc = "";
//	string	updateFunc = "";
//	string	leaveFunc = "";
//	bool	isPassive = false;
//	bool	copyProperties = false;
//	string	fromPropertyName = "";
//	//< 显示用界面
//	string 	showActionName = "";
//	string	showEnterFunc = "";
//	string	showUpdateFunc = "";
//	string	showLeaveFunc = "";
//	bool	showIsPassive = false;
//	
//	string	propertyName = "";
//	string	propertyValue = "";
//	
//	string	conditionFuncName = "";
//	string	excutionFuncName = "";
//	string	transitPriority = "";
//	
//	//string	showConditionFuncName = "";
//	//string	showExcutionFuncName = "";
//	//string	showTransitPriority = "";
//	
//	string tableFileName = "";
//	string srcTableFileName = "";
//
//	public ArrayList stateArray = new ArrayList();
//	//public SDTableStateMachine.Transit[,] transitMatrix = null;
//	//public Vector2 scrollViewVector;
//	public bool simpleDisplay = false;
//	int actionCount = 0;
//	int arraySel = 0;
//	int matrixSel = 0;
//	int typeSel = 0;
//
//	[MenuItem("Window/Action属性配置器")]
//	public static void ShowWindow()
//	{
//		//Show existing window instance. If one doesn't exist, make one.
//		EditorWindow.GetWindow(typeof(ActionTableEditor));
//	}
//	
//	void init(bool removeData)
//	{
//		actionName = "";
//		enterFunc = "";
//		updateFunc = "";
//		leaveFunc = "";
//		isPassive = false;
//		copyProperties = false;
//		fromPropertyName = "";
//		//< 显示用界面
//		showActionName = "";
//		showEnterFunc = "";
//		showUpdateFunc = "";
//		showLeaveFunc = "";
//		showIsPassive = false;
//	
//		propertyName = "";
//		propertyValue = "";
//	
//		conditionFuncName = "";
//		excutionFuncName = "";
//		transitPriority = "";
//	
//		//showConditionFuncName = "";
//		//showExcutionFuncName = "";
//		//showTransitPriority = "";
//	
//	 	//tableFileName = "";
//		if(removeData)
//		{
//			stateArray = null;
//			//SDTableStateMachine.ActionState[] stateArray = null;
//			//SDTableStateMachine.Transit[,] transitMatrix = null;
//		}
//		actionCount = 0;
//		arraySel = 0;
//		matrixSel = 0;
//	}
//	
//	void Start ()
//	{
//		//stateArray = new sdActionState[SDTableStateMachine.MAX_ACTION_COUNT];
//		//Debug.Log("ActionTableEditor");
//	}
//	
//	void CopyDataFromSrcTable(string srcTableFileName)
//	{
//		//TextAsset actionConfText = Resources.Load(srcTableFileName) as TextAsset;
//		TextAsset actionConfText = Resources.LoadAssetAtPath("Assets/$Conf/"+srcTableFileName+".txt",typeof(TextAsset)) as TextAsset;
//		if(actionConfText != null)
//		{
//			Debug.Log(actionConfText.text);
//			int srcActionCount = 0;
//			ArrayList srcStateInfo = sdActionState.deserializeStateData(actionConfText.text, out srcActionCount, true);
//			if(srcStateInfo != null)
//			{
//				Hashtable srcStateNameToIndex = new Hashtable();
//				Hashtable dstStateNameToIndex = new Hashtable();
//
//				for(int srcIndex = 0; srcIndex < srcActionCount; ++srcIndex)
//				{
//					sdActionState ss = srcStateInfo[srcIndex] as sdActionState;
//					srcStateNameToIndex[srcIndex] = ss.name;
//				}
//				
//				for(int dstIndex = 0; dstIndex < actionCount; ++dstIndex)
//				{
//					sdActionState dd = stateArray[dstIndex] as sdActionState;
//					dstStateNameToIndex[dd.name] = dstIndex;
//				}
//				//< copy state info
//				for(int srcIndex = 0; srcIndex < srcActionCount; ++srcIndex)
//				{
//					sdActionState ss = srcStateInfo[srcIndex] as sdActionState;
//					for(int dstIndex = 0; dstIndex < actionCount; ++dstIndex)
//					{
//						sdActionState dd = stateArray[dstIndex] as sdActionState;
//						
//						if(ss.name == dd.name)
//						{
//							dd.isPassive = ss.isPassive;
//							dd.enterFuncName = ss.enterFuncName;
//							dd.updateFuncName = ss.updateFuncName;
//							dd.leaveFuncName = ss.leaveFuncName;
//
//							foreach (DictionaryEntry entry in ss.properties)
//							{									
//								dd.properties[entry.Key] = entry.Value;
//							}
//							break;
//						}
//					}
//					//<
//				}				
//			}			
//			//<
//		}
//	}
//	
//	void LoadTableData(string fileName)
//	{
//		init(true);
//		//TextAsset actionConfText = Resources.Load(fileName) as TextAsset;
//		TextAsset actionConfText = Resources.LoadAssetAtPath ("Assets/$Conf/" + fileName+".txt",typeof(TextAsset)) as TextAsset;
//		if(actionConfText != null)
//		{
//			stateArray = sdActionState.deserializeStateData(actionConfText.text, out actionCount, true);
//		}
//	}	
//
//	void SaveTableData(string tableFileName)
//	{
//		string confText = SerializeStateData();
//		//StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + tableFileName + ".txt");
//		StreamWriter writer = new StreamWriter(Application.dataPath + "/$Conf/" + tableFileName + ".txt");
//		writer.WriteLine(confText);
//		writer.Close();
//	}
//	
//	string SerializeStateData()
//	{
//		if(stateArray == null)
//			return "";
//		
//		string data = actionCount.ToString();
//		foreach(sdActionState s in stateArray)
//		{
//			if(s != null)
//			{
//				data += "#";
//				data += (s.name + "$");
//				data += (s.enterFuncName + "$");
//				data += (s.updateFuncName + "$");
//				data += (s.leaveFuncName + "$");
//				data += s.isPassive.ToString();
//				foreach (DictionaryEntry entry in s.properties)
//				{
//					data += "@";
//					data += entry.Key.ToString();
//					data += "*";					
//					data += entry.Value.ToString();
//				}
//			}
//		}
//		
//		return data;
//	}	
//	
//	string GetEncodeStringByType(string value, int type)
//	{
//		return type.ToString() + "%" + value;
//	}
//	
//	string GetDecodeStringByType(string data, out int type)
//	{
//		string typeInfo = data.Substring(0, 1);
//		type = Convert.ToInt32(typeInfo);
//		return data.Substring(2);
//	}
//
//	void OnGUI()
//	{
//		GUILayout.BeginHorizontal();
//		
//			GUILayout.BeginArea(new Rect(0, 0, 400, 1600));
//			GUILayout.BeginVertical();
//				//simpleDisplay = EditorGUILayout.Toggle("Simple Display", simpleDisplay);
//				GUILayout.Label ("Action Parameter", EditorStyles.boldLabel);
//			
//				actionName = EditorGUILayout.TextField ("Action名称", actionName);
//				//enterFunc = EditorGUILayout.TextField ("Enter function", enterFunc);
//				//updateFunc = EditorGUILayout.TextField ("Update function", updateFunc);
//				//leaveFunc = EditorGUILayout.TextField ("Leave function", leaveFunc);
//				isPassive = EditorGUILayout.Toggle("是否被动技能", isPassive);
//		
//				copyProperties = EditorGUILayout.Toggle("复制Action属性", copyProperties);
//				fromPropertyName = EditorGUILayout.TextField ("源Action", fromPropertyName);
//		
//				GUILayout.BeginHorizontal();
//				//< 建立Action
//				if(GUILayout.Button("创建Action"))
//				{
//					bool valid = true;
//					foreach(sdActionState state in stateArray)
//					{
//						if(state.name == actionName)
//						{
//							valid = false;
//							break;
//						}
//					}
//				
//					if(valid)
//					{
//						sdActionState newAction = new sdActionState(actionName, enterFunc, updateFunc, leaveFunc, isPassive);
//									
//						if(copyProperties)
//						{
//							foreach(sdActionState srcState in stateArray)
//							{
//								if(srcState.name == fromPropertyName)
//								{
//									foreach (DictionaryEntry entry in srcState.properties)
//									{
//										newAction.properties[entry.Key] = entry.Value;									   
//									}
//									break;
//								}
//							}
//						}
//				
//						stateArray.Add(newAction);
//						actionCount = stateArray.Count;
//						//<
//					}	
//				}
//		
//				//< 修改Action
//				if(GUILayout.Button("修改Action"))
//				{
//					foreach(sdActionState state in stateArray)
//					{
//						if(state.name == actionName)
//						{
//							state.enterFuncName = enterFunc;
//							state.updateFuncName = updateFunc;
//							state.leaveFuncName = leaveFunc;
//							state.isPassive = isPassive;
//							break;
//						}
//					}
//				}
//		
//				//< 删除Action
//				if(GUILayout.Button("删除Action"))
//				{
//					foreach(sdActionState state in stateArray)
//					{
//						if(state.name == actionName)
//						{
//							stateArray.Remove(state);
//							actionCount = stateArray.Count;
//							arraySel = 0;
//							break;
//						}
//					}
//				}
//				GUILayout.EndHorizontal();
//		
//				GUILayout.Label("Action数量: " + actionCount.ToString(), EditorStyles.boldLabel);
//				GUILayout.Label ("Action参数", EditorStyles.boldLabel);
//				propertyName = EditorGUILayout.TextField ("Name", propertyName);
//				propertyValue = EditorGUILayout.TextField ("Value", propertyValue);
//				string[] typeStrings = new string[]{"int", "float", "string", "%"};
//				typeSel = GUILayout.SelectionGrid(typeSel, typeStrings, 4);
//
//				GUILayout.BeginHorizontal();
//				//< 新建Action参数
//				int actionIndex = arraySel / 2;				
//				if(GUILayout.Button("创建Action属性"))
//				{				
//					if(actionIndex < stateArray.Count)
//					{
//						sdActionState state1 = stateArray[actionIndex] as sdActionState;
//
//						if(state1 != null && state1.properties != null)
//						{
//							bool isPropExist = state1.properties.ContainsKey(propertyName);
//							if(!isPropExist)
//							{
//								state1.properties[propertyName] = GetEncodeStringByType(propertyValue, typeSel);
//							}
//						}
//					}
//				}
//		
//				//< 修改Action参数
//				if(GUILayout.Button("修改Action属性"))
//				{
//					sdActionState state = stateArray[actionIndex] as sdActionState;
//					if(state != null && state.properties != null)
//					{
//						bool isPropExist = state.properties.ContainsKey(propertyName);
//						if(isPropExist)
//						{
//							state.properties[propertyName] = GetEncodeStringByType(propertyValue, typeSel);
//						}
//					}
//				}
//		
//				//< 删除Action参数
//				if(GUILayout.Button("删除Action属性"))
//				{
//					sdActionState state = stateArray[actionIndex] as sdActionState;
//					if(state != null && state.properties != null)
//					{
//						bool isPropExist = state.properties.ContainsKey(propertyName);
//						if(isPropExist)
//						{
//							state.properties.Remove(propertyName);
//						}
//							
//					}
//				}
//				GUILayout.EndHorizontal();
//		
//				
//			GUILayout.EndVertical();
//			
//			GUILayout.BeginVertical();
//			GUILayout.Label ("Action列表", EditorStyles.boldLabel);
//			
//			if(actionCount > 0)
//			{
//				string[] selectionStrings = new string[2*actionCount];
//				for(int k = 0; k < actionCount; ++k)
//				{
//					selectionStrings[k*2] = k.ToString();
//					selectionStrings[k*2+1] = (stateArray[k] as sdActionState).name;
//				}
//				arraySel = GUILayout.SelectionGrid(arraySel, selectionStrings, 2);
//				sdActionState state = stateArray[actionIndex] as sdActionState;
//				showActionName = state.name;
//				showEnterFunc = state.enterFuncName;
//				showUpdateFunc = state.updateFuncName;
//				showLeaveFunc = state.leaveFuncName;
//				showIsPassive = state.isPassive;
//			}
//			else
//			{
//				showActionName = "";
//				showEnterFunc = "";
//				showUpdateFunc = "";
//				showLeaveFunc = "";
//				showIsPassive = false;
//			}
//			//< 动态显示选中界面
//			GUILayout.Label ("Action状态", EditorStyles.boldLabel);
//			
//			 EditorGUILayout.LabelField ("Action名", showActionName);
//			 //EditorGUILayout.LabelField ("Enter function", showEnterFunc);
//			 //EditorGUILayout.LabelField ("Update function", showUpdateFunc);
//			 //EditorGUILayout.LabelField ("Leave function", showLeaveFunc);
//			 EditorGUILayout.LabelField("是否被动", showIsPassive ? "True" : "False");
//		
//		
//					
//			GUILayout.EndVertical();
//			
//			GUILayout.EndArea();
//		
//			GUILayout.BeginArea(new Rect(420, 0, 650, 1000));
//				GUILayout.BeginVertical();
//				tableFileName = EditorGUILayout.TextField ("路径", tableFileName);
//				
//				if(GUILayout.Button("保存"))
//				{
//					SaveTableData(tableFileName);
//				}
//		
//				if(GUILayout.Button("加载"))
//				{
//					LoadTableData(tableFileName);
//				}
//				GUILayout.BeginHorizontal();
//				srcTableFileName = EditorGUILayout.TextField ("源数据", srcTableFileName);
//				if(GUILayout.Button("复制"))
//				{
//					CopyDataFromSrcTable(srcTableFileName);
//				}
//				GUILayout.EndHorizontal();//	
//		
//				GUILayout.Label ("Action Properties", EditorStyles.boldLabel);
//				int ii = arraySel / 2;
//				if(actionCount > 0)
//				{
//					sdActionState state0 = stateArray[ii] as sdActionState;
//					if(state0 != null && state0.properties != null)
//					{
//						foreach (DictionaryEntry entry in state0.properties)
//						{
//							int varType = 0;
//							string str = GetDecodeStringByType(entry.Value.ToString(), out varType);
//							EditorGUILayout.LabelField(entry.Key.ToString(), entry.Value.ToString());
//						}
//					}
//				}
//
//					
//				GUILayout.EndVertical();
//			GUILayout.EndArea();
//		
//		GUILayout.EndHorizontal();
//	}
//}
