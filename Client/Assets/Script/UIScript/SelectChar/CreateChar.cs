using UnityEngine;
using System.Collections;

/// <summary>
/// 创建角色窗口(执行脚本)
/// 1.更新预览窗口
/// 2.更新头发样式和颜色控件
/// </summary>
public class CreateChar : MonoBehaviour 
{
	public GameObject m_SelectRoleWndObject;		//< 选择角色窗口UI对象aa 
	protected SelectChar mSelectChar;				//< 选择角色窗口UI对象脚本aa 
	
	public GameObject m_CreateRoleWndObject1;		//< 创建角色第一步窗口对象aa
	public GameObject m_CreateRoleWndObject2;		//< 创建角色第二步窗口对象aa
	
	// 创建角色第一步窗口控件aa
	public GameObject m_CreateRoleRaceWndObject;	//< 创建角色种族控件对象aa
	protected CreateRoleRace mCreateRoleRace;		//< 创建角色种族控件脚本对象aa
	public bool m_ReturnModel = false;				// 标志当前为从创建角色界面回到选择职业界面，不要做SHOW动作..
	
	// 创建角色第二步窗口控件aa
	public GameObject m_HairStyleObject;			//< 头发样式控件面板对象aa
	public GameObject[] m_HairStyleObjectArray;		//< 头发样式控件图标对象aa
	
	public GameObject m_HairColorObject;			//< 头发颜色控件面板对象aa
	public GameObject[] m_HairColorObjectArray;		//< 头发颜色控件图标对象aa
	 	
	protected SpinObject mSpinObject; 		//< 主窗口旋转预览角色脚本对象aa
	protected GameObject mLoginCharObject;	//< 左侧预览角色对象aa
	public sdLoginChar mLoginChar;		//< 左侧预览角色脚本对象aa
	
	// 当前选中的种族索引aa
	protected int mSelectedRace = 0;		
	public int SelectedRace
	{
		get { return mSelectedRace;}
	}
	
	protected bool m_bCreateRec	= false;	//< 
	protected bool m_bRoleListRec = false;	//<
	
	protected UIAtlas [,] mHairAtlasArray = new UIAtlas[2,8];	//< 头像图集aa
	
	public string m_strNewCharName = "";
	
	// 性别aa
	protected int mGender = 0;
	public int Gender
	{
		get { return mGender;}	
	}
	
	// 头发样式aa
	protected int mHairStyle = 0;
	public int HairStyle
	{
		get { return mHairStyle;}	
	}
	
	// 头发颜色aa
	protected int mHairColor = 0;
	public int HairColor
	{
		get { return mHairColor;}	
	}
	
	// 职业aa
	protected int mJob = 0;
	public int Job
	{
		get { return mJob;}	
	}
	
	
	void Awake()
	{
		if (m_CreateRoleRaceWndObject)
			mCreateRoleRace = m_CreateRoleRaceWndObject.GetComponent<CreateRoleRace>();
		
		mSpinObject = this.gameObject.GetComponent<SpinObject>();	
		
		foreach (GameObject kGameObject in m_HairStyleObjectArray)
		{
			if (kGameObject == null)
				continue;
			
			DragPanelIcon kIcon = kGameObject.GetComponent<DragPanelIcon>();
			if (kIcon != null)
			{
				kIcon.onDragFinished += onHairStyleSelected;
			}
		}
		
		foreach (GameObject kGameObject in m_HairColorObjectArray)
		{
			if (kGameObject == null)
				continue;
			
			DragPanelIcon kIcon = kGameObject.GetComponent<DragPanelIcon>();
			if (kIcon != null)
			{
				kIcon.onDragFinished += onHairColorSelected;
			}
		}
		
//		LoadHairAtlas(0, 0, 0);
//		LoadHairAtlas(0, 1, 0);
//		LoadHairAtlas(0, 2, 0);
//		LoadHairAtlas(0, 3, 0);
//		LoadHairAtlas(0, 4, 0);
//		LoadHairAtlas(0, 5, 0);
//		LoadHairAtlas(0, 6, 0);
//		LoadHairAtlas(0, 7, 0);
//		LoadHairAtlas(1, 0, 0);
//		LoadHairAtlas(1, 1, 0);
//		LoadHairAtlas(1, 2, 0);
//		LoadHairAtlas(1, 3, 0);
//		LoadHairAtlas(1, 4, 0);
//		LoadHairAtlas(1, 5, 0);
//		LoadHairAtlas(1, 6, 0);
//		LoadHairAtlas(1, 7, 0);
		
		// 根据分辨率调整显示...
		GameObject obj = GameObject.Find("sp_link1");
		if( obj != null )
		{
			float r = 1.777f / ((float)Screen.width/(float)Screen.height) - 1.0f;
			float y = (1280.0f / (float)Screen.width * (float)Screen.height - 720.0f) / 2.0f + 1030.0f;	// 16:9下的y为1030，4:3下的y为1150
			obj.GetComponent<TweenPosition>().from.y	= y;
			obj.GetComponent<TweenPosition>().to.y		= y - 620.0f - 300.0f*r;	// 对4:3屏幕，r=0.33，锁链长度一共会增长100
		}
		obj = GameObject.Find("sp_link2");
		if( obj != null )
		{
			float r = 1.777f / ((float)Screen.width/(float)Screen.height) - 1.0f;
			float y = (1280.0f / (float)Screen.width * (float)Screen.height - 720.0f) / 2.0f + 980.0f;	// 16:9下的y为980，4:3下的y为1100
			obj.GetComponent<TweenPosition>().from.y	= y;
			obj.GetComponent<TweenPosition>().to.y		= y - 570.0f - 300.0f*r;	// 对4:3屏幕，r=0.33，锁链长度一共会增长100
		}
	}

	void Start () 
	{
		GameObject randomNameBtn = gameObject.transform.FindChild
			("CreateCharWnd2/sp_link2/sp_username/bt_random_username").gameObject;
		UIEventListener.Get(randomNameBtn).onClick = OnRandomNameBtn;

	}

	void OnRandomNameBtn(GameObject go)
	{
		GameObject userName = gameObject.transform.FindChild
			("CreateCharWnd2/sp_link2/sp_username/ip_username").gameObject;

		int size = sdConfDataMgr.Instance().m_randomNameDB.Count;
		int idx = Random.Range(0, size);

		RandomName randomName = sdConfDataMgr.Instance().m_randomNameDB[idx+1] as RandomName;
		string firstName = randomName.FirstName;

		idx = Random.Range(0, size);
		randomName = sdConfDataMgr.Instance().m_randomNameDB[idx+1] as RandomName;
		string lastName = randomName.LastName;

		userName.GetComponent<UIInput>().value = lastName + firstName;
	}

	void Update () 
	{
		// 创建角色成功则返回角色选择界面,并选中当前角色进入游戏aa
		if (m_bCreateRec && m_bRoleListRec)
		{
			this.gameObject.SetActive(false);
		
			if (m_SelectRoleWndObject)
			{
				m_SelectRoleWndObject.SetActive(true);
				m_SelectRoleWndObject.GetComponentInChildren<SelectRole>().m_CurrentSelect = SDNetGlobal.roleCount - 1;
				for(int i=0;i<SDNetGlobal.roleCount;i++)
				{
					string str = SDNetGlobal.playerList[i].mRoleName;
					int idx = SDNetGlobal.playerList[i].mRoleName.IndexOf("\0");
					if( idx > 0 )
						str = SDNetGlobal.playerList[i].mRoleName.Substring(0,idx);
					if( m_strNewCharName == str )
					{
						m_SelectRoleWndObject.GetComponentInChildren<SelectRole>().m_CurrentSelect = i;
						break;
					}
				}
						
				m_SelectRoleWndObject.GetComponentInChildren<SelectRole>().OnClick();
			}
		}
	}
	
	void FixedUpdate()
	{
		if (mLoginChar)
			mLoginChar.tickFrame();
	}
	
	public void SetSelectRoleWnd(GameObject obj)
	{
		m_SelectRoleWndObject = obj;
		if (m_SelectRoleWndObject)
			mSelectChar = m_SelectRoleWndObject.GetComponent<SelectChar>();
	}
	
	// 进入创建界面(Step1)aa
	public void EnterCreateUI1(int iRace)
	{
		this.gameObject.SetActive(true);

		if (m_CreateRoleWndObject1)
			m_CreateRoleWndObject1.SetActive(true);	

		if (m_CreateRoleWndObject2)
			m_CreateRoleWndObject2.SetActive(false);
		
		SelectRace(iRace);
		
		if( mLoginCharObject != null )
			mLoginCharObject.SetActive(true);
	
		// 重新播放启动动画..
		GameObject obj = GameObject.Find("sp_link1");
		if( obj!=null && obj.GetComponent<TweenPosition>().enabled==false )
		{
			obj.GetComponent<TweenPosition>().PlayForward();
		}
	}
	
	// 进入创建界面(Step2)aa
	public void EnterCreateUI2(int iRace)
	{
		this.gameObject.SetActive(true);
		
		if (m_CreateRoleWndObject1)
			m_CreateRoleWndObject1.SetActive(false);	
		
		if (m_CreateRoleWndObject2)
			m_CreateRoleWndObject2.SetActive(true);
		
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_CREATEROLE, OnMessage_GCID_CREATEROLE);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_ROLELIST, OnMessage_GCID_ROLELIST);

		CreateRace(iRace);
		
		for (int i = 0; i < 2; ++i)
		{
			for (int j = 0; j < 8; ++j)
			{
				if (mHairAtlasArray[i,j] == null)
					LoadHairAtlas(i, j, 0);
			}	
		}
		
		// 重新播放启动动画..
		GameObject obj = GameObject.Find("sp_link2");
		if( obj!=null && obj.GetComponent<TweenPosition>().enabled==false )
		{
			obj.GetComponent<TweenPosition>().PlayForward();
		}
	}
	
	// 离开创建界面aa
	public void LeaveCreateUI()
	{
		this.gameObject.SetActive(false);	
		if( mLoginCharObject != null )
			mLoginCharObject.SetActive(false);
	}

	public void StartLeaveCreateUI()
	{
		GameObject obj = GameObject.Find("sp_link1");
		if(obj) obj.GetComponent<TweenPosition>().PlayReverse();
	}
	
	public bool IsLeaveCreateUIFinish()
	{
		GameObject obj = GameObject.Find("sp_link1");
		if( obj == null ) return true;
		return !(obj.GetComponent<TweenPosition>().enabled);
	}
	
	public void StartLeaveCreateUI2()
	{
		GameObject obj = GameObject.Find("sp_link2");
		if(obj) obj.GetComponent<TweenPosition>().PlayReverse();
	}
	
	public bool IsLeaveCreateUIFinish2()
	{
		GameObject obj = GameObject.Find("sp_link2");
		if( obj == null ) return true;
		return !(obj.GetComponent<TweenPosition>().enabled);
	}
	
	// 获取头发图集aa
	public UIAtlas GetHairAtlas(int iGender, int iHairStyle)
	{
		if (iGender < 0 || iGender > 1)
			return null;
		
		if (iHairStyle < 0 || iHairStyle > 7)
			return null;
		
		return mHairAtlasArray[iGender, iHairStyle];
	}
	
	// 选定种族aa
	public void SelectRace(int iRace)
	{
		mSelectedRace = iRace;
		
		//
		if (iRace == 0)	
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior;
			mGender = 0;
		}
		else if (iRace == 1) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Magic;
			mGender = 1;
		}
		else if (iRace == 2) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue;
			mGender = 0;
		}
		else if (iRace == 3) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Minister;
			mGender = 1;
		}
		else 
		{
			return;
		}
		
		// 更新右侧种族属性介绍aa
		if (mCreateRoleRace)
			mCreateRoleRace.SelectRace(mSelectedRace);
		
		// 删除之前角色aa
		if (mLoginCharObject != null)
		{
			if (mSpinObject)
				mSpinObject.m_Target = null;
			
			GameObject.Destroy(mLoginCharObject);
			
			mLoginChar = null;	
			mLoginCharObject = null;
		}
		
		// 创建角色aa
		GameObject obj = GameObject.Find("city_construct_halidom02");
		mLoginCharObject = new GameObject();
		mLoginCharObject.name = "@CreateChar1";
		mLoginCharObject.transform.parent = GameObject.Find("SelectSceneWnd").transform;
		mLoginCharObject.transform.localPosition = new Vector3(obj.transform.localPosition.x, -0.76f, obj.transform.localPosition.z);	
		mLoginCharObject.transform.localRotation = Quaternion.Euler(0.0f, 158.1343f, 0.0f);
		mLoginCharObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);	
		mLoginCharObject.AddComponent<AudioSource>();
		//
		sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
		kInfo.mGender		= (byte)mGender;
		kInfo.mHairStyle	= (byte)mHairStyle;
		kInfo.mSkinColor	= (byte)mHairColor;
		kInfo.mBaseJob		= (byte)mJob;
		kInfo.mJob			= (byte)mJob;
		
		mLoginChar = mLoginCharObject.AddComponent<sdLoginChar>();
		mLoginChar.SetVisiable_WhenLoading(false);
		mLoginChar.init(kInfo);
		
		// 高级套装.
		uint [] uiEquipment = null;
		uint uiEquipmentCount = 0;
		if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior )
		{
			uiEquipment = new uint[7] {1100, 1101, 1102, 1103, 1104, 1105, 1106};
			uiEquipmentCount = 7;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic )
		{
			uiEquipment = new uint[6] {4100, 4102, 4103, 4104, 4105, 4106};
			uiEquipmentCount = 6;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue )
		{
			uiEquipment = new uint[6] {8100, 8102, 8103, 8104, 8105, 8106};
			uiEquipmentCount = 6;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister )
		{
			uiEquipment = new uint[7] {6100, 6101, 6102, 6103, 6104, 6105, 6106};
			uiEquipmentCount = 7;	
		}		
		mLoginChar.updateAvatar(uiEquipment, uiEquipmentCount);
		if( m_ReturnModel )
		{
			m_ReturnModel = false;
		}
			
		// 设置给旋转脚本aa
		if (mSpinObject)
			mSpinObject.m_Target = mLoginChar;

        mLoginChar.RequestEnd();
	}
	
	// 创建种族,展示裸装aa
	protected void CreateRace(int iRace)
	{
		mSelectedRace = iRace;
		
		//
		if (iRace == 0)	
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior;
			mGender = 0;
		}
		else if (iRace == 1) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Magic;
			mGender = 1;
		}
		else if (iRace == 2) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue;
			mGender = 0;
		}
		else if (iRace == 3) 
		{
			mJob = (int)HeaderProto.ERoleJob.ROLE_JOB_Minister;
			mGender = 1;
		}
		else 
		{
			return;
		}

		// 设置头发样式图标aa
		foreach (GameObject kGameObject in m_HairStyleObjectArray)
		{
			DragPanelHairIcon kIcon = kGameObject.GetComponent<DragPanelHairIcon>();
			if (kIcon)
			{
				int iIndex = kIcon.m_IconId;	//< 1,2,3,4,5,6,7,8
				int iHairStyle = iIndex-1;		//< 0,1,2,3,4,5,6,7
				
				kIcon.Gender = mGender;
				kIcon.HairStyle = iHairStyle;
				kIcon.HairColor = 0;
			}
		}
		
		// 更新头发颜色图标aa
		foreach (GameObject kGameObject in m_HairColorObjectArray)
		{
			DragPanelHairIcon kIcon = kGameObject.GetComponent<DragPanelHairIcon>();
			if (kIcon)
			{
				int iIndex = kIcon.m_IconId;	//< 1,2,3,4,5,6,7,8
				int iHairColor = iIndex-1;		//< 0,1,2,3,4,5,6,7
				
				kIcon.Gender = mGender;
				kIcon.HairColor = iHairColor;
			}
		}
		
		// 重置头发样式UIaa
		if (m_HairStyleObject)
		{
			mHairStyle = (int)(Random.value*100.0f) % 8;	//< 随机头发样式aa 

			DragPanelIcon kSelIcon = null;
			foreach (GameObject kGameObject in m_HairStyleObjectArray)
			{
				if (kGameObject == null)
					continue;
					
				DragPanelIcon kIcon = kGameObject.GetComponent<DragPanelIcon>();
				if (kIcon != null && (kIcon.m_IconId == mHairStyle+1))
				{
					kSelIcon = kIcon;
					break;
				}
			}
				
			if (kSelIcon != null)
				kSelIcon.Select();
		}
		
		// 重置头发颜色UIaa
		if (m_HairColorObject)
		{
			mHairColor = (int)(Random.value*100.0f) % 8;	//< 随机头发颜色aa 

			DragPanelIcon kSelIcon = null;
			foreach (GameObject kGameObject in m_HairColorObjectArray)
			{
				if (kGameObject == null)
					continue;
					
				DragPanelIcon kIcon = kGameObject.GetComponent<DragPanelIcon>();
				if (kIcon != null && (kIcon.m_IconId == mHairColor+1))
				{
					kSelIcon = kIcon;
					break;
				}
			}
				
			if (kSelIcon != null)
				kSelIcon.Select();
		}
		
		// 删除之前角色aa
		if (mLoginCharObject != null)
		{
			if (mSpinObject)
				mSpinObject.m_Target = null;
			
			GameObject.Destroy(mLoginCharObject);
			
			mLoginChar = null;	
			mLoginCharObject = null;
		}
		
		// 创建角色aa
		GameObject obj = GameObject.Find("city_construct_halidom02");
		mLoginCharObject = new GameObject();
		mLoginCharObject.name = "@CreateChar2";
		mLoginCharObject.transform.parent = GameObject.Find("SelectSceneWnd").transform;
		mLoginCharObject.transform.localPosition = new Vector3(obj.transform.localPosition.x, -0.76f, obj.transform.localPosition.z);	
		mLoginCharObject.transform.localRotation = Quaternion.Euler(0.0f, 158.1343f, 0.0f);
		mLoginCharObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);		
		mLoginCharObject.AddComponent<AudioSource>();
		//
		sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
		kInfo.mGender		= (byte)mGender;
		kInfo.mHairStyle 	= (byte)mHairStyle;
		kInfo.mSkinColor 	= (byte)mHairColor;
		kInfo.mBaseJob		= (byte)mJob;
		kInfo.mJob			= (byte)mJob;
		
		mLoginChar = mLoginCharObject.AddComponent<sdLoginChar>();
		mLoginChar.SetVisiable_WhenLoading(false);
		mLoginChar.init(kInfo);
		
		// 新手套装，不显示头盔.
		uint [] uiEquipment = null;
		uint uiEquipmentCount = 0;
		if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior )
		{
			uiEquipment = new uint[6] {1000, 1001, 1003, 1004, 1005, 1006};
			uiEquipmentCount = 6;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Magic )
		{
			uiEquipment = new uint[5] {4000, 4003, 4004, 4005, 4006};
			uiEquipmentCount = 5;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue )
		{
			uiEquipment = new uint[5] {8000, 8003, 8004, 8005, 8006};
			uiEquipmentCount = 5;	
		}
		else if( mJob == (int)HeaderProto.ERoleJob.ROLE_JOB_Minister )
		{
			uiEquipment = new uint[6] {6000, 6001, 6003, 6004, 6005, 6006};
			uiEquipmentCount = 6;	
		}		
		mLoginChar.updateAvatar(uiEquipment, uiEquipmentCount);
        mLoginChar.RequestEnd();
		// 设置给旋转脚本aa
		if (mSpinObject)
			mSpinObject.m_Target = mLoginChar;
	}
	
	// 选定头发样式(回调函数)aa
	protected void onHairStyleSelected(DragPanelIcon kDragPanelIcon)
	{
		if (kDragPanelIcon == null)
			return;
		
		// 头发样式aa
		int iHairStyle = kDragPanelIcon.m_IconId - 1;
		if (iHairStyle < 0) iHairStyle = 0;
		if (iHairStyle > 7) iHairStyle = 7;
		
		//
		mHairStyle = iHairStyle;
		
		// 更新预览角色aa
		int iGender = 0;
		if (mLoginChar != null)
		{
			mLoginChar.setHairStyle(mHairStyle);
			iGender = mLoginChar.Gender;
		}
		
		// 更新头发颜色图标aa
		foreach (GameObject kGameObject in m_HairColorObjectArray)
		{
			DragPanelHairIcon kIcon = kGameObject.GetComponent<DragPanelHairIcon>();
			if (kIcon)
			{
				int iIndex = kIcon.m_IconId;	//< 1,2,3,4,5,6,7,8
				int iHairColor = iIndex-1;		//< 0,1,2,3,4,5,6,7
				
				kIcon.Gender = iGender;
				kIcon.HairStyle = iHairStyle;
				kIcon.HairColor = iHairColor;
			}
		}
	}
	
	// 选定头发颜色(回调函数)aa
	protected void onHairColorSelected(DragPanelIcon kDragPanelIcon)
	{
		if (kDragPanelIcon == null)
			return;
		
		// 头发颜色aa
		int iHairColor = kDragPanelIcon.m_IconId - 1;
		if (iHairColor < 0) iHairColor = 0;
		if (iHairColor > 7) iHairColor = 7;
		
		//
		mHairColor = iHairColor;
		
		// 更新预览角色aa
		if (mLoginChar != null)
		{
			mLoginChar.setHairColor(iHairColor);
		}
	}
	
	// 加载头像图集aa
	protected void LoadHairAtlas(int iGender, int iHairStyle, int iHairColor)
	{
		int iHairStyleIndex = 0;
		if (iGender == 1)
		{
			iHairStyleIndex = iHairStyle+1;		//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 1,2,3,4,5,6,7,8
		}
		else
		{
			iHairStyleIndex = 8 - iHairStyle;	//< HairStyle: 0,1,2,3,4,5,6,7
												//< HairIndex: 8,7,6,5,4,3,2,1		
		}
	
		string kAssertPath = string.Format("UI/CharHair/$mhair{0}/mhair{0}_prefab.prefab", iHairStyleIndex); 
		if (iGender == 1)
			kAssertPath = string.Format("UI/CharHair/$whair{0}/whair{0}_prefab.prefab", iHairStyleIndex); 


		ResLoadParams kParam = new ResLoadParams();
		kParam.userdata0 = iGender;
		kParam.userdata1 = iHairStyle;
		kParam.userdata2 = iHairColor;
		sdResourceMgr.Instance.LoadResource(
			kAssertPath, 
			NotifyHairAtlas, 
			kParam,
			typeof(UIAtlas));
	}
	
	// 头像图集回调aa
	protected void NotifyHairAtlas(ResLoadParams kParam, UnityEngine.Object kObj)
	{		
		if (kObj == null)
			return;
		
		UIAtlas kAtlas = kObj as UIAtlas;
		
		int iGender = (int)kParam.userdata0;
		int iHairStyle = (int)kParam.userdata1;
		mHairAtlasArray[iGender,iHairStyle] = kAtlas;
	}
	
	//
	protected void OnMessage_GCID_CREATEROLE(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("create role received");

		CliProto.GC_CREATEROLE netMsg = (CliProto.GC_CREATEROLE)msg;
		if(netMsg.m_ErrCode != 0)
		{
			if(netMsg.m_ErrCode == 8104)
				sdUICharacter.Instance.ShowOkMsg("角色名有非法字符！",null);
			else
				sdUICharacter.Instance.ShowOkMsg(SGDP.ErrorString((uint)netMsg.m_ErrCode),null);
		}

		m_bCreateRec = true;
	}
	
	//
	protected void OnMessage_GCID_ROLELIST(int iMsgID, ref CMessage msg)
	{
		SDGlobal.Log("rolelist received");
		
		CliProto.GC_ROLELIST refMSG = (CliProto.GC_ROLELIST)msg;
		
		SDNetGlobal.roleCount = refMSG.m_Count;
        SDNetGlobal.lastSelectRole = SelectRole.SortRoleList(refMSG);
		
		for(int i = 0; i < SDNetGlobal.roleCount ; i++)
		{
			if(SDNetGlobal.playerList[i] == null)
				SDNetGlobal.playerList[i] = new sdPlayerInfo();
			
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[i];		
			kPlayerInfo.mRoleName	= System.Text.Encoding.UTF8.GetString(refMSG.m_RoleInfoList[i].m_RoleInfo.m_RoleName).Trim('\0');
			kPlayerInfo.mDBID		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_DBRoleId;
			kPlayerInfo.mGender		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Gender;
			kPlayerInfo.mSkinColor	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_SkinColor;
			kPlayerInfo.mHairStyle	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_HairStyle;
			kPlayerInfo.mBaseJob	= refMSG.m_RoleInfoList[i].m_RoleInfo.m_BaseJob;
			kPlayerInfo.mJob		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Job;
			kPlayerInfo.mLevel		= refMSG.m_RoleInfoList[i].m_RoleInfo.m_Level;
			kPlayerInfo.mEquipCount	= refMSG.m_RoleInfoList[i].m_Equip.m_ItemCount;			

			if (kPlayerInfo.mEquipCount > 0)
			{
				kPlayerInfo.mEquipID = new uint[kPlayerInfo.mEquipCount];
				for (int j = 0; j < kPlayerInfo.mEquipCount; ++j)
					kPlayerInfo.mEquipID[j] = (uint)refMSG.m_RoleInfoList[i].m_Equip.m_Items[j].m_TID;
			}
		}
		
		for(int i = SDNetGlobal.roleCount; i<4; i++)
		{
			SDNetGlobal.playerList[i] = null;	
		}
		
		m_bRoleListRec = true;
	}
}
