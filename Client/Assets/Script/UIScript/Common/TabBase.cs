using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TabBase
{
	protected bool _isActive = false;
	public GameObject 	m_goTabRoot	= null;

	public TabBase(GameObject goTabRoot)
	{
		m_goTabRoot = goTabRoot;
	}


	public bool IsActive() { return _isActive; }

	void Start () 
	{


	}

	void Update () 
	{

	}
	
	void OnDestory()
	{
	
	}
	
	virtual public void Init()
	{

	}

	virtual public void RefreshUserInterface(bool resetPos)
	{
		if(!_isActive)
			return;


	}

	virtual public void OnActive()
	{
		if(m_goTabRoot == null)
			return;

		_isActive = true;
		m_goTabRoot.SetActive(true);
	}

	virtual public void OnDeactive()
	{
		if(m_goTabRoot == null)
			return;

		_isActive = false;
		m_goTabRoot.SetActive (false);
	}

}