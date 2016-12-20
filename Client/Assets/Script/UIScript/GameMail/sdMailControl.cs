using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdMailControl : Singleton<sdMailControl>
{
	public static GameObject 	m_UIMailWnd				= null;
	public static GameObject 	m_UIMailDetailWnd		= null;
	
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
	
	//邮件界面..
	public void ActiveMailWnd(GameObject PreWnd)
	{
		if( m_UIMailWnd == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$MailWnd.prefab", LoadMailWnd, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIMailWnd.SetActive(true);
		m_UIMailWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIMailWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIMailWnd.transform.localPosition = new Vector3(0.0f,0.0f,-500.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIMailWnd,true,"w_black");
		
		sdUIMailWnd obj = m_UIMailWnd.GetComponent<sdUIMailWnd>();
		if( obj != null )
		{
			obj.Init();
			obj.ActiveMailWnd(PreWnd);
		}
	}
	
	public void LoadMailWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIMailWnd != null )
			return;
		
		m_UIMailWnd = GameObject.Instantiate(obj) as GameObject;
		if( m_UIMailWnd )
		{
			ActiveMailWnd(param.userdata0 as GameObject);
			sdUICharacter.Instance.RefreshRoleInfo();
		}
	}

	//邮件详细界面..
	public void ActiveMailDetailWnd(GameObject PreWnd, UInt64 uuID)
	{
		if( m_UIMailDetailWnd == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			param.info = uuID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/UIPrefab/$MailDetailWnd.prefab", LoadMailDetailWnd, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIMailDetailWnd.SetActive(true);
		m_UIMailDetailWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIMailDetailWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIMailDetailWnd.transform.localPosition = new Vector3(0.0f,0.0f,-500.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIMailDetailWnd,false,"bg_grey");
		
		sdUIMailDetailWnd obj = m_UIMailDetailWnd.GetComponent<sdUIMailDetailWnd>();
		if( obj != null )
		{
			obj.ActiveMailDetailWnd(PreWnd, uuID);
		}
	}
	
	public void LoadMailDetailWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIMailDetailWnd != null )
			return;
		
		m_UIMailDetailWnd = GameObject.Instantiate(obj) as GameObject;
		if( m_UIMailDetailWnd )
			ActiveMailDetailWnd(param.userdata0 as GameObject, UInt64.Parse(param.info));
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