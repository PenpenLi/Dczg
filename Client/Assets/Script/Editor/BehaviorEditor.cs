//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System;
//using System.Reflection;
//using System.IO;
//
//public class sdEditorActionState : object
//{
//	public const int displayWidth = 50;
//	public const int displayHeight = 50;
//	
//	
//
//	public string enterFunc = "";
//	public string updateFunc = "";
//	public string leaveFunc = null;
//	public Hashtable stateData = new Hashtable();
//	public ArrayList transitPath = new ArrayList();	
//	public string info = "unknown_action";
//	public int id = -1;
//	public float fadeTime = 0.0f;
//	
//	public float x = 0;
//	public float y = 0;
//}
//
//public class sdEditorTransitNode : object
//{
//}
//
//public class BehaviorEditor : EditorWindow 
//{
//	[MenuItem("Window/BehaviorEditor")]
//	public static void ShowWindow()
//	{
//		EditorWindow.GetWindow(typeof(BehaviorEditor));
//	}
//	
//	private Rect windowRect = new Rect(20, 20, 120, 50);
//	public GUIStyle stateRectStyle = new GUIStyle();
//	public GUIContent testContent = new GUIContent();
//	
//	//< 编辑器背景色
//	public Texture2D 	backGroundTexture = null;
//	//< 按钮控件的样式
//	public GUIStyle 	buttonStyle0 = new GUIStyle();
//	//< Action State显示颜色
//	public Color 		stateDisplayColor = Color.red;
//	
//	public Hashtable 	stateDB = new Hashtable();	
//	
//	
//	
//	//< 内部逻辑使用变量
//	private Vector2		rightMbDownPos 			= Vector2.zero;
//	private int 		idIndex 				= 0;
//	private Hashtable	uiTextures 				= new Hashtable();
//	private Matrix4x4	displayMatrix			= Matrix4x4.identity;
//	private float		displayScale			= 1.0f;
//	private Matrix4x4	displayScaleMatrix		= Matrix4x4.identity;
//	private Vector2		centerTranslate			= Vector2.zero;
//	public BehaviorEditor()
//	{
//		backGroundTexture = makeTexture(1, 1, new Color(0.6f, 0.851f, 0.918f, 1.0f));
//		loadUIResources("Editor");
//		buttonStyle0.normal.textColor = Color.black;
//		buttonStyle0.normal.background = uiTextures["b5_n"] as Texture2D;
//		buttonStyle0.hover.textColor = Color.yellow;
//		buttonStyle0.hover.background = uiTextures["b5_h"] as Texture2D;
//		buttonStyle0.alignment = TextAnchor.MiddleCenter;
//		
//		///<
//		stateRectStyle.normal.textColor = Color.white;
//		stateRectStyle.normal.background = makeTexture(100, 100, Color.green);
//		
//		testContent.text = "fuck you!";
//		testContent.image = makeTexture(50, 50, Color.red);
//		testContent.tooltip = "see what!";
//		
//		
//		this.wantsMouseMove = true;
//	}
//	
//	void DoWindow(int windowID)
//	{
//		GUILayout.Box("Hi", stateRectStyle);
//		GUI.DragWindow();
//	}
//	
//	static Texture2D makeTexture(int w, int h, Color clr)
//	{
//		Color[] pix = new Color[w*h];
//		for(int i = 0; i < pix.Length; i++)
//		{
//			pix[i] = clr;
//		}
//		
//		Texture2D tex = new Texture2D(w, h);
//		tex.SetPixels(pix);
//		tex.Apply();
//		return tex;
//	}
//	
//	void loadUIResources(string resourcePath)
//	{
//		string[] fileNames = Directory.GetFiles(Application.dataPath + "/" + resourcePath, 
//									"*.*", SearchOption.AllDirectories);
//		foreach(string str in fileNames)
//		{
//			if(!str.EndsWith(".meta"))
//			{
//				int idx = str.IndexOf("Assets");
//				if(idx != -1)
//				{
//					string p = str.Substring(idx);
//					string normalPath = p.Replace('\\', '/');
//					UnityEngine.Object obj = Resources.LoadAssetAtPath(normalPath, 
//									typeof(UnityEngine.Object)) as UnityEngine.Object;
//					if(obj != null)
//					{
//						storeResourceByType(obj);
//					}
//				}
//			}
//		}
//		//Texture2D tex = Resources.LoadAssetAtPath("Assets/Editor/b5_h.png", typeof(Texture2D)) as Texture2D;
//		
//	}
//	
//	void storeResourceByType(UnityEngine.Object obj)
//	{
//		if(obj.GetType().Name == "Texture2D")
//		{
//			uiTextures[obj.name] = obj;
//			//Debug.Log (obj.name);
//		}
//	}
//	
//	void OnGUI()
//	{
//		//GUI.matrix = Matrix4x4.TRS(new Vector3(200, 200, 0) ,Quaternion.identity, Vector3.one);
//		
//		//GUI.backgroundColor = Color.yellow;
//		//BeginWindows();
//		//windowRect = GUILayout.Window (1, windowRect, DoWindow, "Hi There");
//		//EndWindows();
//		drawBackground();
//		Vector2 mousePos = Event.current.mousePosition;
//		
//		
//		if(GUI.Button(new Rect(10, 10, 150, 30), "Create New State", buttonStyle0))
//		{
//			sdEditorActionState s = new sdEditorActionState();
//			stateDB[idIndex] = s;
//			
//			s.x = UnityEngine.Random.Range(-centerTranslate.x, this.position.width - centerTranslate.x);
//			s.y = UnityEngine.Random.Range(-centerTranslate.y, this.position.height - centerTranslate.y);
//			++idIndex;
//			
//			//Debug.Log(s.x.ToString() + ":" + s.y.ToString());
//		}
//		
//		GUI.matrix = displayMatrix;
//		
////		if(Event.current.type == EventType.ScrollWheel)
////		{
////			float scrollValue = Event.current.delta.y;
////			if(scrollValue > 0.0f)
////				displayScale += 0.06f;
////			else
////				displayScale -= 0.06f;
////			
////			displayScaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(displayScale, displayScale, displayScale));
////			Repaint();
////			
////			//Debug.Log(displayScale);
////			//float scrollValue = Event.current.delta.y;
////			//GUIUtility.ScaleAroundPivot(new Vector2(2, 2), mousePos);
////			//displayMatrix = tm*displayMatrix;
////			//Debug.Log(Event.current.delta.x.ToString() + ":" + Event.current.delta.y.ToString());
////		}
//		
//		if(Event.current.type == EventType.MouseDrag)
//		{		
//			centerTranslate += new Vector2(Event.current.delta.x, Event.current.delta.y);
//			Matrix4x4 tm = Matrix4x4.TRS(new Vector3(centerTranslate.x, centerTranslate.y, 0), Quaternion.identity, Vector3.one);
//			displayMatrix = tm;
//			Repaint();
//		}	
//		
//		
//		//this.wantsMouseMove = EditorGUI.Toggle(new Rect(10, 10, 600, 20), "Echo Mouse Move", this.wantsMouseMove);
//		
//		//EditorGUI.LabelField(new Rect(10, 50, 600, 20), "Mouse Position: ", "abc");
//		if(Event.current.type == EventType.MouseDown)
//		{
//			if(Event.current.button == 1)
//			{
//				rightMbDownPos = mousePos;
//				Debug.Log ("Right button down");
//			}
//		}
//		
//		Drawing.DrawLine(new Vector2(0, 0), new Vector2(200, 100), Color.yellow, 1, true);
//		
//		foreach(DictionaryEntry entry in stateDB)
//		{
//			sdEditorActionState state = entry.Value as sdEditorActionState;
//			EditorGUI.DrawRect(new Rect(state.x, state.y, sdEditorActionState.displayWidth, 
//						sdEditorActionState.displayHeight), stateDisplayColor);
//		}
//		
//		
//		//GUIUtility.ScaleAroundPivot(new Vector2(2, 2), new Vector2(0, 0));
//		
//		GUI.Label(new Rect(25, 100, 100, 100), testContent, stateRectStyle);
//		EditorGUI.DrawRect(new Rect(300, 100, 100, 100), Color.cyan);
//		//EditorGUI.
//	}
//	
//	private void drawBackground()
//	{
//		if(backGroundTexture != null)
//		{
//			GUI.DrawTexture(new Rect(0, 0, this.position.width, this.position.height), 
//				backGroundTexture,ScaleMode.StretchToFill); 
//		
//		}
//	}
//
//	
//}