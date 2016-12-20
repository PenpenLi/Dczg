using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdUIPetSystemPnl : MonoBehaviour
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
	
	public void ActivePetSystemPnl(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
	}
}