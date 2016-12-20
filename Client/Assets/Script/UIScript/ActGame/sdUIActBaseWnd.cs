using UnityEngine;
using System.Collections;
using System;

public class sdUIActBaseWnd : MonoBehaviour
{
	public GameObject m_preWnd = null;

	void Awake () 
	{
	}
	
	void Start () 
	{
	}

	void Update () 
	{
	}
	
	void OnClick()
    {
	}
	
	public void ActiveActBaseWnd(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
	}
}