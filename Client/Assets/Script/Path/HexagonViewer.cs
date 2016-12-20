using UnityEngine;
using System.Collections;

/// <summary>
/// 显示整个Hexagon网格aa
/// </summary>
public class HexagonViewer : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
        Hexagon.Manager.GetSingleton().DebugRender();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
