//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System;
//using System.Reflection;
//
//public class CharControllerEditor : EditorWindow 
//{
//	[MenuItem("Window/CharControllerEditor")]
//	public static void ShowWindow()
//	{
//		EditorWindow.GetWindow(typeof(BehaviorEditor));
//	}
//	
//	private Rect windowRect = new Rect(20, 20, 120, 50);
//	
//	void DoWindow(int windowID)
//	{
//		GUILayout.Button("Hi");
//		GUI.DragWindow();
//	}
//	
//	void OnGUI()
//	{
//		BeginWindows();
//		windowRect = GUILayout.Window (1, windowRect, DoWindow, "Hi There");
//		EndWindows();
//	}
//
//	
//}