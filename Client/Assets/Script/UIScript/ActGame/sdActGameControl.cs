using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdActGameControl : Singleton<sdActGameControl>
{
	public static GameObject 	m_UIActBaseWnd		= null;
	public static GameObject 	m_UILapBossWnd		= null;
	public static GameObject	m_UIWorldBossWnd	= null;
	
	void Start () 
	{
	}

	void Update () 
	{

	}
	
	void OnDestory()
	{
	
	}
	
	public void Init()
	{

	}

	//活动主界面..
	public void ActiveActBaseWnd(GameObject PreWnd)
	{
		if( m_UIActBaseWnd == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/ActGame/$ActBase/ActBaseWnd.prefab", LoadActBaseWnd, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIActBaseWnd.SetActive(true);
		m_UIActBaseWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIActBaseWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIActBaseWnd.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUIActBaseWnd obj = m_UIActBaseWnd.GetComponent<sdUIActBaseWnd>();
		if( obj != null )
		{
			obj.ActiveActBaseWnd(PreWnd);
		}
	}
	
	public void LoadActBaseWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIActBaseWnd != null )
			return;
		
		m_UIActBaseWnd = GameObject.Instantiate(obj) as GameObject;
		if( m_UIActBaseWnd )
			ActiveActBaseWnd(param.userdata0 as GameObject);
	}
	
	//舔怪界面..
	public void ActiveLapBossWnd(GameObject PreWnd, int iType)
	{
		if( m_UILapBossWnd == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			param.petInt = iType;
			sdResourceMgr.Instance.LoadResource("UI/ActGame/$LapBoss/LapBossWnd.prefab", LoadLapBossWnd, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UILapBossWnd.SetActive(true);
		m_UILapBossWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UILapBossWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UILapBossWnd.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUILapBossWnd obj = m_UILapBossWnd.GetComponent<sdUILapBossWnd>();
		if( obj != null )
		{
			obj.Init();
			obj.ActiveLapBossWnd(PreWnd, iType);
		}
	}
	
	public void LoadLapBossWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UILapBossWnd != null )
			return;
		
		m_UILapBossWnd = GameObject.Instantiate(obj) as GameObject;
		if( m_UILapBossWnd )
			ActiveLapBossWnd(param.userdata0 as GameObject, param.petInt);
	}

	//世界Boss界面..
	public void ActiveWorldBossWnd(GameObject PreWnd)
	{
		if( m_UIWorldBossWnd == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/ActGame/$WorldBoss/WorldBossWnd.prefab", LoadWorldBossWnd, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIWorldBossWnd.SetActive(true);
		m_UIWorldBossWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIWorldBossWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIWorldBossWnd.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUIWorldBossWnd obj = m_UIWorldBossWnd.GetComponent<sdUIWorldBossWnd>();
		if( obj != null )
		{
			obj.ActiveWorldBossWnd(PreWnd);
		}
	}
	
	public void LoadWorldBossWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIWorldBossWnd != null )
			return;
		
		m_UIWorldBossWnd = GameObject.Instantiate(obj) as GameObject;
		if( m_UIWorldBossWnd )
			ActiveWorldBossWnd(param.userdata0 as GameObject);
	}

	//关闭某个界面..
	public void CloseGameWnd(GameObject closeWnd)
	{
		if( closeWnd == null )
			return;
		
		closeWnd.SetActive(false);
		closeWnd.transform.parent = null;
	}
}