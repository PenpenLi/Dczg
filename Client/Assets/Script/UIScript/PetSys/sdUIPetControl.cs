using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdUIPetControl : Singleton<sdUIPetControl>
{
	public static GameObject 	m_UIPetListPnl		= null;
	public static GameObject	m_UIPetSaleSelectPnl= null;
	public static GameObject 	m_UIPetWarPnl		= null;
	public static GameObject 	m_UIPetPaperPnl		= null;
	public static GameObject 	m_UIPetRonghePnl	= null;
	public static GameObject 	m_UIPetEquipPnl		= null;
	public static GameObject 	m_UIPetTujianPnl	= null;
	
	public static GameObject 	m_UIPetPropPnl		= null;
	public static GameObject 	m_UIPetWarSelectPnl	= null;
	public static GameObject	m_UIPetRongheSelectPnl	= null;
	public static GameObject	m_UIPetLevelPnl	= null;
	public static GameObject	m_UIPetLevelSelectPnl = null;
	public static GameObject	m_UIPetStrongPnl = null;
	public static GameObject	m_UIPetStrongSelectPnl = null;
	public static GameObject	m_UIPetChangeEquipPnl = null;
	
	public static GameObject	m_UIPetEquipTip = null;
	public static GameObject	m_UIPetSkillTip = null;
	public static GameObject	m_UIPetSmallTip = null;

	//新的宠物系统..
	public static GameObject	m_UIPetSystemPnl = null;
	
	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{

	}
	
	void OnDestory()
	{
		Debug.Log("sdUIPetControl::OnDestory..");
	}
	
	public void Init()
	{

	}
	
	// 由关卡准备界面激活宠物战队界面.
	private GameObject LevelPrepareWnd = null;
	public void ActivePetByLevelPrepare(GameObject wnd)
	{
		LevelPrepareWnd = wnd;
	}
	public bool IsReturnLevelPrepare()
	{
		if( LevelPrepareWnd != null )
		{
			DoUpdatePet bt = LevelPrepareWnd.GetComponent<DoUpdatePet>();
			if( bt != null ) bt.UpdatePet();
			LevelPrepareWnd.SetActive(true);
			LevelPrepareWnd = null;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//宠物背包..
	public void ActivePetListPnl(GameObject PreWnd, bool bNeedScale)
	{
		if( m_UIPetListPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			if (bNeedScale)
				param.petInt = 1;
			else
				param.petInt = 0;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetListPnl.prefab", LoadPetListPnl, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		CloseAllPetPnl();
		
		m_UIPetListPnl.SetActive(true);
		m_UIPetListPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetListPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetListPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		if (bNeedScale)
			WndAni.ShowWndAni(m_UIPetListPnl,true,"w_black");
		
		sdUIPetListPnl obj = m_UIPetListPnl.GetComponent<sdUIPetListPnl>();
		if( obj != null )
		{
			obj.ActivePetListPnl(PreWnd);
		}
	}
	
	public void LoadPetListPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetListPnl != null )
			return;
		
		m_UIPetListPnl = GameObject.Instantiate(obj) as GameObject;
		bool bNeedScale = false;
		if (param.petInt==1)
			bNeedScale = true;
		if( m_UIPetListPnl )
			ActivePetListPnl(param.userdata0 as GameObject, bNeedScale);
	}
	//宠物出售选择面板..
	public void ActivePetSaleSelectPnl(GameObject PreWnd)
	{
		if( m_UIPetSaleSelectPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetSaleSelectPnl.prefab",LoadPetSaleSelectPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetSaleSelectPnl.SetActive(true);
		m_UIPetSaleSelectPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetSaleSelectPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetSaleSelectPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetSaleSelectPnl,false,"bg_grey");
		
		sdUIPetSaleSelectPnl obj = m_UIPetSaleSelectPnl.GetComponentInChildren<sdUIPetSaleSelectPnl>();
		if( obj != null )
		{
			obj.ActivePetSaleSelectPnl(PreWnd);
		}
	}
	
	public void LoadPetSaleSelectPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetSaleSelectPnl != null )
			return;
		
		m_UIPetSaleSelectPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetSaleSelectPnl )
			ActivePetSaleSelectPnl(param.userdata0 as GameObject);
	}
	//宠物战队..
	public void ActivePetWarPnl(GameObject PreWnd,int TeamIdx)
	{
		if( m_UIPetWarPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			param.userdata1 = TeamIdx;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetWarPnl.prefab", LoadPetWarPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetWarPnl.SetActive(true);
		m_UIPetWarPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetWarPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetWarPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetWarPnl,true,"w_black");
		
		sdUIPetWarPnl obj = m_UIPetWarPnl.GetComponentInChildren<sdUIPetWarPnl>();
		if( obj != null )
		{
			obj.ActivePetWarPnl(PreWnd,TeamIdx);
		}
	}
	
	public void LoadPetWarPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetWarPnl != null )
			return;
		
		m_UIPetWarPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetWarPnl )
			ActivePetWarPnl(param.userdata0 as GameObject,(int)param.userdata1);
	}
	//宠物碎片..
	public void ActivePetPaperPnl(GameObject PreWnd)
	{
		if( m_UIPetPaperPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetPaperPnl.prefab", LoadPetPaperPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		CloseAllPetPnl();
		
		m_UIPetPaperPnl.SetActive(true);
		m_UIPetPaperPnl.transform.parent = GameObject.Find("UICamera").transform;
		m_UIPetPaperPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetPaperPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetTujianPnl,true,"w_black");
		
		sdUIPetPaperPnl obj = m_UIPetPaperPnl.GetComponentInChildren<sdUIPetPaperPnl>();
		if( obj != null )
		{
			obj.ActivePetPaperPnl(PreWnd);
		}
	}
	
	public void LoadPetPaperPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetPaperPnl != null )
			return;
		
		m_UIPetPaperPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetPaperPnl )
			ActivePetPaperPnl(param.userdata0 as GameObject);
	}
	//宠物融合..
	public void ActivePetRonghePnl(GameObject PreWnd)
	{
		if( m_UIPetRonghePnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetRonghePnl.prefab",  LoadPetRonghePnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		CloseAllPetPnl();
		
		m_UIPetRonghePnl.SetActive(true);
		m_UIPetRonghePnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetRonghePnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetRonghePnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetRonghePnl,true,"w_black");
		
		sdUIPetRonghePnl obj = m_UIPetRonghePnl.GetComponentInChildren<sdUIPetRonghePnl>();
		if( obj != null )
		{
			obj.ActivePetRonghePnl(PreWnd);
		}
	}
	
	public void LoadPetRonghePnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetRonghePnl != null )
			return;
		
		m_UIPetRonghePnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetRonghePnl )
			ActivePetRonghePnl(param.userdata0 as GameObject);
	}
	//宠物装备..
	public void ActivePetEquipPnl(GameObject PreWnd)
	{
		if( m_UIPetEquipPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetEquipPnl.prefab",  LoadPetEquipPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		CloseAllPetPnl();
		
		m_UIPetEquipPnl.SetActive(true);
		m_UIPetEquipPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetEquipPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetEquipPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUIPetEquipPnl obj = m_UIPetEquipPnl.GetComponentInChildren<sdUIPetEquipPnl>();
		if( obj != null )
		{
			obj.Init();
			obj.ActivePetEquipPnl(PreWnd);
		}
	}
	
	public void LoadPetEquipPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetEquipPnl != null )
			return;
		
		m_UIPetEquipPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetEquipPnl )
			ActivePetEquipPnl(param.userdata0 as GameObject);
	}
	//宠物图鉴..
	public void ActivePetTujianPnl(GameObject PreWnd)
	{
		if( m_UIPetTujianPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetTujianPnl.prefab", LoadPetTujianPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		CloseAllPetPnl();
		
		m_UIPetTujianPnl.SetActive(true);
		m_UIPetTujianPnl.transform.parent = GameObject.Find("UICamera").transform;
		m_UIPetTujianPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetTujianPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetTujianPnl,true,"w_black");
		
		sdUIPetTujianPnl obj = m_UIPetTujianPnl.GetComponentInChildren<sdUIPetTujianPnl>();
		if( obj != null )
		{
			obj.ActivePetTujianPnl(PreWnd);
		}
	}
	
	public void LoadPetTujianPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetTujianPnl != null )
			return;
		
		m_UIPetTujianPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetTujianPnl )
			ActivePetTujianPnl(param.userdata0 as GameObject);
	}
	
	//宠物属性..
	public void ActivePetPropPnl(GameObject PreWnd, UInt64 uuDBID)
	{
		if( m_UIPetPropPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuDBID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetPropPnl.prefab", LoadPetPropPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetPropPnl.SetActive(true);
		m_UIPetPropPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetPropPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetPropPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetPropPnl,false,"bg_grey");
		
		sdUIPetPropPnl obj = m_UIPetPropPnl.GetComponentInChildren<sdUIPetPropPnl>();
		if( obj != null )
		{
			obj.ActivePetPropPnl(PreWnd, uuDBID);
		}
	}
	
	public void LoadPetPropPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetPropPnl != null )
			return;
		
		m_UIPetPropPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetPropPnl )
			ActivePetPropPnl(null, UInt64.Parse(param.petData0));
	}
	//战队选择面板..
	public void ActivePetWarSelectPnl(GameObject PreWnd, UInt64 uuDBID, int iPos)
	{
		if( m_UIPetWarSelectPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuDBID.ToString();
			param.petInt = iPos;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetWarSelectPnl.prefab",LoadPetWarSelectPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetWarSelectPnl.SetActive(true);
		m_UIPetWarSelectPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetWarSelectPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetWarSelectPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetWarSelectPnl,false,"bg_grey");
		
		sdUIPetWarSelectPnl obj = m_UIPetWarSelectPnl.GetComponentInChildren<sdUIPetWarSelectPnl>();
		if( obj != null )
		{
			obj.ActivePetWarSelectPnl(PreWnd, uuDBID, iPos);
		}
	}
	
	public void LoadPetWarSelectPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetWarSelectPnl != null )
			return;
		
		m_UIPetWarSelectPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetWarSelectPnl )
			ActivePetWarSelectPnl(null, UInt64.Parse(param.petData0), param.petInt);
	}
	//融合选择面板..
	public void ActivePetRongheSelectPnl(GameObject PreWnd, UInt64 uuHasSelectID, int iPos)
	{
		if( m_UIPetRongheSelectPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuHasSelectID.ToString();
			param.petInt = iPos;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetRongheSelectPnl.prefab",LoadPetRongheSelectPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetRongheSelectPnl.SetActive(true);
		m_UIPetRongheSelectPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetRongheSelectPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetRongheSelectPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetRongheSelectPnl,false,"bg_grey");
		
		sdUIPetRongheSelectPnl obj = m_UIPetRongheSelectPnl.GetComponentInChildren<sdUIPetRongheSelectPnl>();
		if( obj != null )
		{
			obj.ActivePetRongheSelectPnl(PreWnd, uuHasSelectID, iPos);
		}
	}
	
	public void LoadPetRongheSelectPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetRongheSelectPnl != null )
			return;
		
		m_UIPetRongheSelectPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetRongheSelectPnl )
			ActivePetRongheSelectPnl(null, UInt64.Parse(param.petData0), param.petInt);
	}
	//宠物升级面板..
	public void ActivePetLevelPnl(GameObject PreWnd, UInt64 uuSelfDBID)
	{
		if( m_UIPetLevelPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			
			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuSelfDBID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetLevelPnl.prefab",LoadPetLevelPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetLevelPnl.SetActive(true);
		m_UIPetLevelPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetLevelPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetLevelPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetLevelPnl,false,"bg_grey");
		
		sdUIPetLevelPnl obj = m_UIPetLevelPnl.GetComponentInChildren<sdUIPetLevelPnl>();
		if( obj != null )
		{
			obj.ActivePetLevelPnl(PreWnd, uuSelfDBID);
		}
	}
	
	public void LoadPetLevelPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetLevelPnl != null )
			return;
		
		m_UIPetLevelPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetLevelPnl )
			ActivePetLevelPnl(null, UInt64.Parse(param.petData0));
	}
	//升级材料选择面板..
	public void ActivePetLevelSelectPnl(GameObject PreWnd, UInt64 uuSelfDBID)
	{
		if( m_UIPetLevelSelectPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuSelfDBID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetLevelSelectPnl.prefab",LoadPetLevelSelectPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetLevelSelectPnl.SetActive(true);
		m_UIPetLevelSelectPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetLevelSelectPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetLevelSelectPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetLevelSelectPnl,false,"bg_grey");
		
		sdUIPetLevelSelectPnl obj = m_UIPetLevelSelectPnl.GetComponentInChildren<sdUIPetLevelSelectPnl>();
		if( obj != null )
		{
			obj.ActivePetLevelSelectPnl(PreWnd, uuSelfDBID);
		}
	}
	
	public void LoadPetLevelSelectPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetLevelSelectPnl != null )
			return;
		
		m_UIPetLevelSelectPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetLevelSelectPnl )
			ActivePetLevelSelectPnl(null, UInt64.Parse(param.petData0));
	}
	//强化面板..
	public void ActivePetStrongPnl(GameObject PreWnd, UInt64 uuSelfDBID)
	{
		if( m_UIPetStrongPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			
			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuSelfDBID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetStrongPnl.prefab",LoadPetStrongPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetStrongPnl.SetActive(true);
		m_UIPetStrongPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetStrongPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetStrongPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetStrongPnl,false,"bg_grey");
		
		sdUIPetStrongPnl obj = m_UIPetStrongPnl.GetComponentInChildren<sdUIPetStrongPnl>();
		if( obj != null )
		{
			obj.ActivePetStrongPnl(PreWnd, uuSelfDBID);
		}
	}
	
	public void LoadPetStrongPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetStrongPnl != null )
			return;
		
		m_UIPetStrongPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetStrongPnl )
			ActivePetStrongPnl(null, UInt64.Parse(param.petData0));
	}
	//强化材料选择面板..
	public void ActivePetStrongSelectPnl(GameObject PreWnd, UInt64 uuSelfDBID, UInt64 uuMatDBID)
	{
		if( m_UIPetStrongSelectPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuSelfDBID.ToString();
			param.petData1 = uuMatDBID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetStrongSelectPnl.prefab",LoadPetStrongSelectPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetStrongSelectPnl.SetActive(true);
		m_UIPetStrongSelectPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetStrongSelectPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetStrongSelectPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		//窗口显示的效果..
		//WndAni.ShowWndAni(m_UIPetStrongSelectPnl,false,"bg_grey");
		
		sdUIPetStrongSelectPnl obj = m_UIPetStrongSelectPnl.GetComponentInChildren<sdUIPetStrongSelectPnl>();
		if( obj != null )
		{
			obj.ActivePetStrongSelectPnl(PreWnd, uuSelfDBID, uuMatDBID);
		}
	}
	
	public void LoadPetStrongSelectPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetStrongSelectPnl != null )
			return;
		
		m_UIPetStrongSelectPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetStrongSelectPnl )
			ActivePetStrongSelectPnl(null, UInt64.Parse(param.petData0), UInt64.Parse(param.petData1));
	}
	//宠物更换装备面板..
	public void ActivePetChangeEquipPnl(GameObject PreWnd, UInt64 uuDBID, int iSortSubClass)
	{
		if( m_UIPetChangeEquipPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = uuDBID.ToString();
			param.petInt = iSortSubClass;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetChangeEquipPnl.prefab", LoadPetChangeEquipPnl, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetChangeEquipPnl.SetActive(true);
		m_UIPetChangeEquipPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetChangeEquipPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetChangeEquipPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUIPetChangeEquipPnl obj = m_UIPetChangeEquipPnl.GetComponentInChildren<sdUIPetChangeEquipPnl>();
		if( obj != null )
		{
			obj.Init();
			obj.ActivePetChangeEquipPnl(PreWnd, uuDBID, iSortSubClass);
		}
	}
	
	public void LoadPetChangeEquipPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetChangeEquipPnl != null )
			return;
		
		m_UIPetChangeEquipPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetChangeEquipPnl )
			ActivePetChangeEquipPnl(null, UInt64.Parse(param.petData0), param.petInt);
	}
	//宠物装备tip..
	public void ActivePetEquipTip(GameObject PreWnd, int iID)
	{
		if( m_UIPetEquipTip == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = iID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetEquipTip.prefab", LoadPetEquipTip, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetEquipTip.SetActive(true);
		m_UIPetEquipTip.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetEquipTip.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetEquipTip.transform.localPosition = new Vector3(300.0f,-200.0f,0.0f);
		
		sdUIPetEquipTip obj = m_UIPetEquipTip.GetComponentInChildren<sdUIPetEquipTip>();
		if( obj != null )
		{
			obj.ActivePetEquipTip(PreWnd, iID);
		}
	}
	
	public void LoadPetEquipTip(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetEquipTip != null )
			return;
		
		m_UIPetEquipTip = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetEquipTip )
			ActivePetEquipTip(null, int.Parse(param.petData0));
	}
	//宠物技能Tip
	public void ActivePetSkillTip(GameObject PreWnd, int iID)
	{
		if( m_UIPetSkillTip == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);

			ResLoadParams param = new ResLoadParams();
			param.petData0 = iID.ToString();
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetSkillTip.prefab",LoadPetSkillTip, param);
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetSkillTip.SetActive(true);
		m_UIPetSkillTip.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetSkillTip.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetSkillTip.transform.localPosition = new Vector3(0.0f,0.0f,-500.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetSkillTip,false,"bg_grey");
		
		sdUIPetSkillTip obj = m_UIPetSkillTip.GetComponentInChildren<sdUIPetSkillTip>();
		if( obj != null )
		{
			obj.ActivePetSkillTip(PreWnd, iID);
		}
	}
	
	public void LoadPetSkillTip(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetSkillTip != null )
			return;
		
		m_UIPetSkillTip = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetSkillTip )
			ActivePetSkillTip(null, int.Parse(param.petData0));
	}
	//宠物属性界面Tip..
	public void ActivePetSmallTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
        ActivePetSmallTip(PreWnd, iID, iUp, iLevel, new Vector3(0.0f, 0.0f, 0.0f));
	}

    public bool IsPetTipActive()
    {
        if (m_UIPetPropPnl != null && m_UIPetPropPnl.active)
        {
            return true;
        }

        return false;
    }

    public void ActivePetSmallTip(GameObject PreWnd, int iID, int iUp, int iLevel, Vector3 wndPos)
    {
        if (m_UIPetPropPnl == null)
        {
            sdUILoading.ActiveSmallLoadingUI(true);
            ResLoadParams param = new ResLoadParams();
            param.petData0 = iID.ToString();
            param.petData1 = iUp.ToString();
            param.petInt = iLevel;
            param.pos = wndPos;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetPropPnl.prefab", LoadPetSmallTip, param);
            return;
        }

        if (PreWnd)
            PreWnd.SetActive(false);

		m_UIPetPropPnl.SetActive(true);
		m_UIPetPropPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetPropPnl.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		m_UIPetPropPnl.transform.localPosition = wndPos;
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetPropPnl,false,"bg_grey");

		sdUIPetPropPnl obj = m_UIPetPropPnl.GetComponentInChildren<sdUIPetPropPnl>();
        if (obj != null)
        {
			obj.ActivePetPropTip(PreWnd, iID, iUp, iLevel);
        }
    }

	public void LoadPetSmallTip(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetPropPnl != null )
			return;
		
		m_UIPetPropPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetPropPnl )
			ActivePetSmallTip(null, int.Parse(param.petData0), int.Parse(param.petData1), param.petInt, param.pos);
	}
	//图鉴弹出的宠物属性界面..
	public void ActivePetTujianSmallTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
		if (m_UIPetPropPnl == null)
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.petData0 = iID.ToString();
			param.petData1 = iUp.ToString();
			param.petInt = iLevel;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetPropPnl.prefab", LoadPetTujianSmallTip, param);
			return;
		}
		
		if (PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetPropPnl.SetActive(true);
		m_UIPetPropPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetPropPnl.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		m_UIPetPropPnl.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetPropPnl,false,"bg_grey");
		
		sdUIPetPropPnl obj = m_UIPetPropPnl.GetComponentInChildren<sdUIPetPropPnl>();
		if (obj != null)
		{
			obj.ActivePetPropTujianTip(PreWnd, iID, iUp, iLevel);
		}
	}
	
	public void LoadPetTujianSmallTip(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetPropPnl != null )
			return;
		
		m_UIPetPropPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetPropPnl )
			ActivePetTujianSmallTip(null, int.Parse(param.petData0), int.Parse(param.petData1), param.petInt);
	}
	//宠物组合头像弹出的Tip..
	public void ActivePetZuheSmallTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
		if (m_UIPetSmallTip == null)
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.petData0 = iID.ToString();
			param.petData1 = iUp.ToString();
			param.petInt = iLevel;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetSmallTip.prefab", LoadPetZuheSmallTip, param);
			return;
		}
		
		if (PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetSmallTip.SetActive(true);
		m_UIPetSmallTip.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetSmallTip.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		m_UIPetSmallTip.transform.localPosition = new Vector3(0.0f, 0.0f, -500.0f);
		//窗口显示的效果..
		WndAni.ShowWndAni(m_UIPetSmallTip,false,"bg_grey");
		
		sdUIPetSmallTip obj = m_UIPetSmallTip.GetComponentInChildren<sdUIPetSmallTip>();
		if (obj != null)
		{
			obj.ActivePetSmallTip(PreWnd, iID, iUp, iLevel);
		}
	}
	
	public void LoadPetZuheSmallTip(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetSmallTip != null )
			return;
		
		m_UIPetSmallTip = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetSmallTip )
			ActivePetZuheSmallTip(null, int.Parse(param.petData0), int.Parse(param.petData1), param.petInt);
	}
	//新宠物系统：主按钮界面..
	public void ActivePetSystemPnl(GameObject PreWnd)
	{
		if( m_UIPetSystemPnl == null )
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.userdata0 = PreWnd as object;
			sdResourceMgr.Instance.LoadResource("UI/PetSys/$PetSystemPnl.prefab", LoadPetSystemPnl, param, typeof(GameObject));
			return;
		}
		
		if(PreWnd)
			PreWnd.SetActive(false);
		
		m_UIPetSystemPnl.SetActive(true);
		m_UIPetSystemPnl.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_UIPetSystemPnl.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_UIPetSystemPnl.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		
		sdUIPetSystemPnl obj = m_UIPetSystemPnl.GetComponent<sdUIPetSystemPnl>();
		if( obj != null )
		{
			obj.ActivePetSystemPnl(PreWnd);
		}
	}
	
	public void LoadPetSystemPnl(ResLoadParams param,UnityEngine.Object obj)
	{
		sdUILoading.ActiveSmallLoadingUI(false);
		if( m_UIPetSystemPnl != null )
			return;
		
		m_UIPetSystemPnl = GameObject.Instantiate(obj) as GameObject;
		if( m_UIPetSystemPnl )
			ActivePetSystemPnl(param.userdata0 as GameObject);
	}

	//关闭某个宠物界面..
	public void ClosePetPnl(GameObject closeWnd)
	{
		if( closeWnd == null )
			return;
		
		closeWnd.SetActive(false);
		closeWnd.transform.parent = null;
	}

	//关闭5个宠物主界面..
	public void CloseAllPetPnl()
	{
		ClosePetPnl(m_UIPetListPnl);
		ClosePetPnl(m_UIPetRonghePnl);
		ClosePetPnl(m_UIPetTujianPnl);
		ClosePetPnl(m_UIPetPaperPnl);
	}
}