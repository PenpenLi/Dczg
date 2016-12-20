using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// 关卡对象aa
/// </summary>
[AddComponentMenu("DsMobile/sdGameLevel")]
public class sdGameLevel : sdGameObject
{	
	// 关卡类型aa
	public enum LevelType
	{
		None,
		MainCity,
		WorldMap,
		Fight,
		Login,
		SelectRole,
        PVP,
		LapBoss,
		WorldBoss,
        PET_TRAIN,
	}
	
	public LevelType 		levelType = LevelType.None;
	public GameObject 		levelRoot = null;
	public sdMainChar 		mainChar = null;
	public sdGameCamera 	mainCamera = null;
	public	sdTuiTuLogic	tuiTuLogic	=	null;
	public	sdActorManager	actorMgr	=	new sdActorManager();
	
	public Vector3			cameraRelativeDistance = Vector3.zero;
	Vector3  		charToCameraDirection 	= Vector3.zero;
	Vector3			birthPosition = Vector3.zero;
	Quaternion		birthDirection = Quaternion.identity;
	Vector3			moveFrontDirection = Vector3.forward;
	Vector3			moveRightDirection = Vector3.right;
	
	public bool isFollow = true;
	
	public Camera	avatarCamera = null;
	public RenderTexture avatarTexture = null;

	public Camera petCamera = null;
	public RenderTexture petTexture = null;

//	public Hashtable gameEffectTable = null;
//	public Material avatarMaterial = null;
			
	protected GameObject otherCharacterRoot = null;		//< 其他角色根节点aa
	
	//public bool  bMovieDialogueInit = false;

	public delegate  void  OnGuideDialogueEnd();
	public event OnGuideDialogueEnd  guideDialogueEnd;
	
	// 主角头像图集aa
	protected UIAtlas mAvatarHeadAtlas = null;
	public UIAtlas AvatarHeadAtlas
	{
		get { return mAvatarHeadAtlas; }	
	}
	
	// 主角头像图集索引aa
	protected string mAvatarHeadSprite = null;
	public string AvatarHeadSprite
	{
		get { return mAvatarHeadSprite; }	
	}	

	// 进入场景时的服务器同步时间(单位ms,服务器给的是绝对时间,不要使用float)aa
	/*
    protected ulong mTimeFromLastServerTime;
    public ulong TimeFromLastServerTime
    {
        get { return mTimeFromLastServerTime; }
    }
	protected ulong mServerTime = 0;
	public ulong ServerTime
	{
		set 
        {
            mServerTime = value;
            mTimeFromLastServerTime = 0;
        }
		get { return mServerTime;}	
	}
	*/

	
	// 进入场景时的客户端同步时间(单位s)aa
	protected ulong mClientTime = 0;
	public ulong ClientTime
	{
		set { mClientTime = value;}
		get { return mClientTime;}	
	}
	// 世界地图关卡开启相机最后位置aaa
	protected Vector3 ptWorldMapCameraPos = Vector3.zero;
	public Vector3 WordMapCameraPos
	{
		set {ptWorldMapCameraPos = value;}
		get {return ptWorldMapCameraPos;}
	}
	
	protected GameObject 	joyStickObject 		= null;
	public GUITexture 		joyStickTexture		= null;
	protected Joystick 		joyStickController	= null;
	protected GameObject 	joyBack 			= null;
	public GUITexture 		joyBackTexture 		= null;
	
	public	GameObject		NGUIRoot			=	null;	
	public Camera 			UICamera 			= 	null;
	protected int 			uiLayer				= 0;
	
	public GameObject		effectNode			= null;
	public	GameObject 		birthObject			=	null;
	public Color MajorLight = new Color(1.0f,1.0f,1.0f,1.0f);
	public float MajorLightIntensity = 1.0f;
	public float MajorLightRadius = 5.0f;
	public float MajorLightAttenuation = 1.0f;

	public bool changeAavatar = true;
	
	public bool hasMainChar = false;
	public bool testMode = 	false;
	public bool testAuto =	true;
	
	
	public sdLocalSystem battleSystem = null;
	
	public static sdGameLevel instance = null;
	
	public delegate void NotifyLevelAwake();
	public static event NotifyLevelAwake notifyLevelStart;

	// 主角操控对象aa
	protected GameObject mFingerObject = null;

    //pvp level
    bool m_bDoorOpen = false;
    GameObject m_blockDoor1 = null;
    GameObject m_blockDoor2 = null;

	// 主角是否处于自动战斗状态aa
	protected bool mAutoMode = false;
	public bool AutoMode
	{
		get
		{
			return mAutoMode;
		}
		set
		{
			mAutoMode = value;
			sdGlobalDatabase.Instance.globalData["AutoMode"] =	value;

			if (mFingerObject != null)
			{
				FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
				if (kFingerGesturesInitializer != null)
				{
					kFingerGesturesInitializer.enabled = mAutoMode;

					FingerControl kFingerControl = kFingerGesturesInitializer.CharFingerControl;
					kFingerControl.Enable = mAutoMode;
					kFingerControl.SetFullAutoMode(mFullAutoMode);
				}
			}
		}
	}

	// 主角是否属于全自动战斗状态aa
	protected bool mFullAutoMode = false;
	public bool FullAutoMode
	{
		get
		{
			return mFullAutoMode;
		}

		set
		{
			mFullAutoMode = value;
			sdGlobalDatabase.Instance.globalData["FullAutoMode"] = value;

			if (mFingerObject != null)
			{
				FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
				if (kFingerGesturesInitializer != null)
				{
					kFingerGesturesInitializer.enabled = mAutoMode;

					FingerControl kFingerControl = kFingerGesturesInitializer.CharFingerControl;
					kFingerControl.Enable = mAutoMode;
					kFingerControl.SetFullAutoMode(mFullAutoMode);
				}
			}
		}
	}

	// 加载主角战斗控制器aa
	protected void LoadFingerObject()
	{
		if (mFingerObject == null)
		{
			mFingerObject = GameObject.Instantiate(Resources.Load("FingerGestures/Prefabs/@FingerGestures")) as GameObject;

			FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
			if (kFingerGesturesInitializer != null)
				kFingerGesturesInitializer.MainCamera = mainCamera;
		}
	}

	// 销毁主角战斗控制器aa
	public void DestroyFingerObject()
	{
		if (mFingerObject!=null)
		{
			GameObject.Destroy(mFingerObject);
			mFingerObject = null;
		}
	}
	
	// 获取主角战斗控制器aa
	public FingerControl GetFingerControl()
	{
		if (mFingerObject == null)
			return null;
		
		FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
		if (kFingerGesturesInitializer == null)
			return null;
		
		return kFingerGesturesInitializer.CharFingerControl;
	}

	// 启动与禁用主角战斗控制器aa
	public void SetFingerObjectActive(bool bActive)
	{
		if (mFingerObject != null)
		{
			if (!mFingerObject.active)
			{
				mFingerObject.SetActive(bActive);
			}
		}
	}
	
	public void 	ClearMainCharacterMove()
	{
		if (mFingerObject != null)
		{
			FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
			if (kFingerGesturesInitializer != null)
			{
				kFingerGesturesInitializer.ClearMainCharacterMove();
			}
		}
	}


	public void GuideDialogueNotify()
	{
		if (guideDialogueEnd != null)
		{
			guideDialogueEnd();
		}
	}

	void Awake() 
	{
		// 登陆界面:清理所有已经加载的bundle重新开始更新和加载aa
		if(levelType==LevelType.Login)
		{
            if (!Application.isEditor)
            {
                DumpManager.Instance.Initialize();
            }


			
			if(BundleGlobal.bundles!=null)
			{
				foreach(BundleGlobalItem item in BundleGlobal.bundles)
				{
					if(item.bundle!=null)
					{
						item.bundle.Unload(false);
						item.bundle=null;
					}
				}
			}
			BundleGlobal.bundles=null;
			BundleGlobal.bundleTable.Clear();
		}

		// 切换角色界面:清理保存的WorldMap中的相机位置aa
		if (levelType == LevelType.SelectRole)
		{
			sdGlobalDatabase.Instance.globalData.Remove("worldmapcamerapos");
		}

		Debug.Log("QualityLevel="+QualitySettings.GetQualityLevel());

		//从登录场景开始游戏 禁用所有场景的testMode..
		if(levelType	==	LevelType.Login)
		{
			sdGlobalDatabase.Instance.globalData["testMode"]	=	false;
		}
		if(sdGlobalDatabase.Instance.globalData.ContainsKey("testMode"))
		{
			testMode	=	(bool)sdGlobalDatabase.Instance.globalData["testMode"];
		}
		//如果这个场景开启testMode 则设置自动战斗模式 还是 手动..
		if(testMode)
		{
			AutoMode = testAuto;
		}
		initGlobalResource();
		//游戏初始化 时间速度恢复...
		Time.timeScale	=	1.0f;
		
		levelRoot = GameObject.Find("@GameLevel");
		if(levelRoot == null)
		{
			Debug.LogError("Must have a @GameLevel top level gameobject!");
			return;
		}
		
		birthObject = GameObject.Find("@BirthPoint");
		if(birthObject == null)
		{
			Debug.LogError("Must have a @BirthPoint gameobject!");
			return;
		}
			
		birthPosition = birthObject.transform.position;
		birthDirection = birthObject.transform.rotation;
		mainCamera = createMainCamera();
		Vector3 originCharPos = birthPosition;
		Vector3 originCameraPos = mainCamera.transform.position;
		charToCameraDirection = originCameraPos - originCharPos;
		charToCameraDirection.Normalize();
		
		moveFrontDirection = new Vector3(-charToCameraDirection.x, 0.0f, -charToCameraDirection.z);
		moveFrontDirection.Normalize();
		moveRightDirection = Vector3.Cross(Vector3.up, moveFrontDirection);
		moveRightDirection.Normalize();	
		
		createJoyStickCtrl();
		
		if(hasMainChar)
		{			
			createAvatarRenderTexture();
			CreatePetRenderTexture();
		}
		
		//< for test
		battleSystem = new sdLocalSystem();
		battleSystem.gameLevel = this;
		battleSystem.init();
		
		//< create effect node
		effectNode = new GameObject();
		effectNode.name = "@GameEffect";
		effectNode.transform.parent = this.transform;
		effectNode.transform.localPosition = Vector3.zero;
		effectNode.transform.localRotation = Quaternion.identity;
		effectNode.transform.localScale = Vector3.one;
		
		tuiTuLogic	=	GetComponent<sdTuiTuLogic>();
		
		NGUIRoot	=	GameObject.Find("NGUIRoot");
		GameObject cameraObject = GameObject.Find("UICamera");
		if(cameraObject != null)
		{			
			UICamera = cameraObject.GetComponent<Camera>();
		}
	
		instance = this;
		
		GameObject	obj = GameObject.Instantiate(Resources.Load("AvatarLight")) as GameObject;
		obj.transform.parent	=	transform;
		
		MarkUnloadBundle();

		createBubbleSystem();

		// 初始化AI管理器aa
		sdBehaviourManager.GetSingleton().OnEnterLevel();

		// 初始化PVP管理器aa
        if(levelType == LevelType.PVP)
        {
            m_blockDoor1 = GameObject.Find("Multi_Door_Shamo_1");
            m_blockDoor2 = GameObject.Find("Multi_Door_Shamo_2");
            sdPVPManager.Instance.SetOtherBirthPoint(GameObject.Find("@BirthPoint2").transform.position);
        }

        GameObject Design = GameObject.Find("@Design");
        //设置地下城关卡难度..
        if (Design != null)
        {
            if (sdGlobalDatabase.Instance.globalData.ContainsKey("LevelDifficulty"))
            {
                int difficulty = (int)sdGlobalDatabase.Instance.globalData["LevelDifficulty"];
                string NodeName = "@Easy";
                if (difficulty == 0)
                {
                    NodeName = "@Easy";
                }
                else if (difficulty == 1)
                {
                    NodeName = "@Hard";
                }
                else if (difficulty == 2)
                {
                    NodeName = "@Nightmare";
                }
                for (int i = 0; i < Design.transform.childCount; i++)
                {
                    Transform node = Design.transform.GetChild(i);
                    if (node.name == NodeName)
                    {
                        node.gameObject.SetActive(true);
                        continue;
                    }
                    if (   node.name == "@Easy" ||
                                node.name == "@Hard" ||
                                node.name == "@Nightmare")
                    {
                        node.gameObject.SetActive(false);
                    }

                }

                sdGlobalDatabase.Instance.globalData.Remove("LevelDifficulty");
            }
        }

        
        //Hexagon.Manager.GetSingleton().DebugRender();
	}
		
	public void Start()
	{
		if (notifyLevelStart != null)
		{
			notifyLevelStart();
			notifyLevelStart = (NotifyLevelAwake)Delegate.RemoveAll(notifyLevelStart, notifyLevelStart);
			
			ClientTime = (ulong)(System.DateTime.Now.Ticks / 10000L);//Time.time;
		}

		// 加载操控与自动战斗系统aa
		if (hasMainChar)
		{
			if (sdGlobalDatabase.Instance.globalData.ContainsKey("AutoMode"))
				mAutoMode = (bool)sdGlobalDatabase.Instance.globalData["AutoMode"];

			if (sdGlobalDatabase.Instance.globalData.ContainsKey("FullAutoMode"))
				mFullAutoMode = (bool)sdGlobalDatabase.Instance.globalData["FullAutoMode"];

			LoadFingerObject();
		}

        // 设置主摄像机模式.
        if (mainCamera != null && (levelType == LevelType.MainCity || levelType == LevelType.Fight || levelType == LevelType.None))
        {
            string s = sdConfDataMgr.Instance().GetSetting("CFG_Camera");
            if (s == "1")
                mainCamera.ChangeCameraDistance();
        }

		//
		sdUICharacter uiChar = sdUICharacter.Instance;

		// 创建主角aa
		if (sdGlobalDatabase.Instance.globalData.ContainsKey("MainCharBaseProp") && hasMainChar)
		{
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
			sdGameActorCreateInfo kCreateInfo = new sdGameActorCreateInfo();
			if (kPlayerInfo != null)
			{
				kCreateInfo.mGender = kPlayerInfo.mGender;
				kCreateInfo.mHairStyle = kPlayerInfo.mHairStyle;
				kCreateInfo.mSkinColor = kPlayerInfo.mSkinColor;
				kCreateInfo.mBaseJob = kPlayerInfo.mBaseJob;
				kCreateInfo.mJob = kPlayerInfo.mJob;
				kCreateInfo.mLevel = kPlayerInfo.mLevel;
				kCreateInfo.mRoleName = kPlayerInfo.mRoleName;
			}
			else
			{			
				kCreateInfo.mJob = 7
					;
			}
            // 加载角色头像aa..
            if (kPlayerInfo != null)
                LoadAvatarHeadAtlas(kPlayerInfo.mGender, kPlayerInfo.mHairStyle, kPlayerInfo.mSkinColor);
	
			// 创建主角色aa
			createMainChar(kCreateInfo, birthPosition, birthDirection);

			// 主角控制器aa
			if (mFingerObject != null)
			{
				FingerGesturesInitializer kFingerGesturesInitializer = mFingerObject.GetComponent<FingerGesturesInitializer>();
				if (kFingerGesturesInitializer != null)
					kFingerGesturesInitializer.MainChar = mainChar;
			}

			//aa


			if (kPlayerInfo != null)
			{
                string movieDialogue = sdConfDataMgr.Instance().GetRoleSetting("MovieDialogue_" + kPlayerInfo.mRoleName);
                if (movieDialogue.Length == 0 || sdUICharacter.Instance.iCurrentLevelID > int.Parse(movieDialogue))
				{
					Hashtable movieTable = sdConfDataMgr.Instance().m_Movie;
					if(movieTable.ContainsKey(sdUICharacter.Instance.iCurrentLevelID))
					{
						mainCamera.MainCharFollow = false;
                        sdConfDataMgr.Instance().SetRoleSetting("MovieDialogue_" + kPlayerInfo.mRoleName, sdUICharacter.Instance.iCurrentLevelID.ToString());

                        sdMovieDialogue dialogue = sdGameLevel.instance.mainCamera.GetComponent<Camera>().gameObject.GetComponent<sdMovieDialogue>();
                        if(dialogue == null)
                            dialogue = sdGameLevel.instance.mainCamera.GetComponent<Camera>().gameObject.AddComponent<sdMovieDialogue>();
                        if(dialogue)
						    dialogue.SetMovieInfo(sdUICharacter.Instance.iCurrentLevelID, true, false, Vector3.one, Vector3.zero);
					}
				}
			}
		}

		// 创建PVP角色(PVP地图)aa
		if (levelType == LevelType.PVP)
		{
            // 通知PVP管理器aa
            //sdUICharacter.Instance.ShowCountDownTime(true, eCountDownType.eCDT_pvpReady);
            sdPVPManager.Instance.m_eCountDownType = eCountDownType.eCDT_pvpReady;
            sdPVPManager.Instance.m_fCountDown = sdPVPManager.ms_ReadyTime;
            sdUICharacter.Instance.ShowCountDownTime2(true);
			if ((sdPVPManager.Instance.PVPBaseProperty != null) && (sdPVPManager.Instance.PVPItemProperty != null) && (sdPVPManager.Instance.PVPSkillProperty != null))
			{
				sdGameActorCreateInfo kInfo = new sdGameActorCreateInfo();
				kInfo.mDBID = (ulong)sdPVPManager.Instance.PVPBaseProperty["DBID"];
				kInfo.mObjID = (ulong)sdPVPManager.Instance.PVPBaseProperty["ObjID"];
				kInfo.mGender = (byte)sdPVPManager.Instance.PVPBaseProperty["Sex"];
				kInfo.mBaseJob = (byte)sdPVPManager.Instance.PVPBaseProperty["BaseJob"];
				kInfo.mJob = (byte)sdPVPManager.Instance.PVPBaseProperty["Job"];
				kInfo.mLevel = (int)sdPVPManager.Instance.PVPBaseProperty["Level"];
				kInfo.mRoleName = sdPVPManager.Instance.PVPBaseProperty["Name"] as string;

				sdOtherPVPChar pvpChar = createOtherPVPChar(kInfo,
					sdPVPManager.Instance.GetOtherBirthPoint(),
					Quaternion.identity);
				sdPVPManager.Instance.InitPVPRival(pvpChar);
			}
		}
        BubbleManager.Instance.mOpenWndCount = 0;

		// 设置操作模式.
		{
			string s = sdConfDataMgr.Instance().GetSetting("CFG_Move");
			if( s == "1" )
				AutoMode = false;
		}

		// 初始化寻路aa
        Hexagon.Manager.GetSingleton().Init();

		//
        if(levelType == LevelType.WorldMap || levelType == LevelType.MainCity)
        {
            if (sdUICharacter.Instance.m_bBBSWndFirst && sdUICharacter.Instance.m_strbbs.Length > 0)
            {
                sdUICharacter.Instance.ShowbbsWnd(true,sdUICharacter.Instance.m_strbbs, false, true);
                sdUICharacter.Instance.m_bBBSWndFirst = false;
            }
        } 
	}
		
	public void OnDestroy()
	{
		// 销毁AI管理器aa
		sdBehaviourManager.GetSingleton().OnLeaveLevel();

		instance = null;
	}
	
	public void initGlobalResource()
	{
		bool result = false;
		if(testMode)
		{
			sdResourceDependenceTable.GetSingleton().LoadMonsterTable("$Conf/MonsterDependence.xml", true);
			sdResourceDependenceTable.GetSingleton().LoadMonsterTable("$Conf/PetDependence.xml", true);
			sdAITable.GetSingleton().LoadAITable("$Conf/AITable.xml", true);
			sdConfDataMgr.Instance().Init(testMode);
		}
		result = sdResourceMgr.Instance.init(testMode);
		result = sdGameItemMgr.Instance.init(testMode);
		result = sdGlobalDatabase.Instance.init(testMode);
		
		if(testMode)
		{
			if(!sdGlobalDatabase.Instance.globalData.ContainsKey("MainCharBaseProp"))
			{
				Hashtable baseProTable = new Hashtable();
				sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] = baseProTable;
				
				baseProTable["Name"] = "GoldenYear";
				baseProTable["Dead"] = 0;
				baseProTable["Level"] = 300;
				baseProTable["BaseJob"] = 0;
				baseProTable["Job"] = (byte)7;
				baseProTable["Sex"] = (byte)1;
				baseProTable["Experience"] = 12345;
				baseProTable["BodySize"] = 1;
				baseProTable["AttSize"] = 1;
				
				baseProTable["MoveSpeed"] = 5000;
				baseProTable["MoveSpeedModPer"] = 0;
				
				baseProTable["Str"] = 100;
				baseProTable["Int"] = 200;
				baseProTable["Dex"] = 300;
				baseProTable["Sta"] = 400;				
				baseProTable["Fai"] = 500;
				
				baseProTable["HP"] = 121000;		//< 12100
				baseProTable["MaxHP"] = 300000;		//< 30000
				baseProTable["SP"] = 60000;
				baseProTable["MaxSP"] = 60000;				
				baseProTable["HPTick"] = 50;
				baseProTable["SPTick"] = 40;
				baseProTable["AttSpeed"] = 200;
				baseProTable["AttSpeedModPer"] = 0;
				
				baseProTable["IceAtt"] = 100;
				baseProTable["FireAtt"] = 80;
				baseProTable["PoisonAtt"] = 60;
				baseProTable["ThunderAtt"] = 40;
				baseProTable["AtkDmgMin"] = 40000;	//< 4000
				baseProTable["AtkDmgMax"] = 50000;	//< 5000
				
				//< attak-defense
				baseProTable["Hit"] = 950; //< -> x% + HitPer from equ + base, Per = Hit/10
				baseProTable["HitPer"] = 0; //< -> equ + buff + skill
				baseProTable["Dodge"] = 0; //< Per = Dodge/10
				baseProTable["DodgePer"] = 0;
				//< totalHitPer - totalDodgePer
				
				baseProTable["Cri"] = 50;
				baseProTable["CriPer"] = 0;
				baseProTable["Flex"] = 0;
				baseProTable["FlexPer"] = 0;
				
				baseProTable["CriDmg"] = 15000; //< Per10000 CriDmg + skillCriDmg - CriDmgDef
				baseProTable["CriDmgDef"] = 0; //< Per10000
				
				baseProTable["Pierce"] = 0;//< Per = Pie/(Pie+Lvl*270+2700)
				baseProTable["PiercePer"] = 0;	//< 	Per + PiercePer	
				baseProTable["Def"] = 200; //< Per
				
				baseProTable["IceDef"] = 1000; //<
				baseProTable["FireDef"] = 800;
				baseProTable["PoisonDef"] = 600;
				baseProTable["ThunderDef"] = 400;				
			}
		}
	}
	
	public void createJoyStickCtrl()
	{
		joyStickObject = GameObject.Find("@JoyStick");
		if(joyStickObject == null)
			return;
		
		joyStickController = joyStickObject.GetComponent<Joystick>();
		joyBack = joyStickObject.transform.GetChild(0).gameObject;
		joyStickTexture = joyStickObject.GetComponent<GUITexture>();
		joyBackTexture = joyBack.GetComponent<GUITexture>();
		
		uiLayer = LayerMask.NameToLayer("NGUI");
	}
	
	public void createBubbleSystem()
	{
		GameObject bubbleNode = Resources.Load("Bubble/BubbleSystem") as GameObject;
		GameObject.Instantiate(bubbleNode);		
		//Bubble.
	}
	
	public void CreatePetRenderTexture()
	{
		GameObject petCameraObject = new GameObject();

		petCameraObject.name = "@PetCamera";
		petCameraObject.transform.parent = levelRoot.transform;

		petCamera = petCameraObject.AddComponent<Camera>();
		petCamera.orthographic = true;
		petCamera.depth = -10;
		petCamera.nearClipPlane = 0.1f;
		petCamera.farClipPlane = 10.0f;
		petCamera.aspect = 1.0f;
		petCamera.orthographicSize = 1.0f;
		petCamera.backgroundColor = new Color(0, 0, 0, 0);
		
		int petLayer = LayerMask.NameToLayer("PetNode");
		petCamera.cullingMask = 1 << petLayer;
		
		petCameraObject.transform.localPosition = new Vector3(0, 0, 0);
		petCameraObject.transform.localRotation = Quaternion.identity;
		petCameraObject.transform.localScale =  Vector3.one;
		
		petTexture = new RenderTexture(512,512,1,RenderTextureFormat.ARGB32,RenderTextureReadWrite.Linear);
		petTexture.Create();
		petCamera.targetTexture = petTexture;
		
		petCamera.enabled = false;
	}

	public void createAvatarRenderTexture()
	{
		GameObject avatarCameraObject = new GameObject();
		avatarCameraObject.name = "@AvatarCamera";
		avatarCameraObject.transform.parent = levelRoot.transform;
		avatarCamera = avatarCameraObject.AddComponent<Camera>();
		avatarCamera.orthographic = true;
		avatarCamera.depth = -10;
		avatarCamera.nearClipPlane = 0.1f;
		avatarCamera.farClipPlane = 10.0f;
		avatarCamera.aspect = 1.0f;
		avatarCamera.orthographicSize = 1.0f;
		avatarCamera.backgroundColor = new Color(0, 0, 0, 0);
		int avatarLayer = LayerMask.NameToLayer("AvatarNode");
		avatarCamera.cullingMask = 1 << avatarLayer;
		
		avatarCameraObject.transform.localPosition = new Vector3(0, 0, 0);
		avatarCameraObject.transform.localRotation = Quaternion.identity;
		avatarCameraObject.transform.localScale =  Vector3.one;
		
		avatarTexture = new RenderTexture(512,512,1,RenderTextureFormat.ARGB32,RenderTextureReadWrite.Linear);//Resources.Load("$CharAvatarTexture") as RenderTexture;
		avatarTexture.Create();
		avatarCamera.targetTexture = avatarTexture;
		
		avatarCamera.enabled	=	false;
	}
	
	// 加载主角头像图集aa
	protected void LoadAvatarHeadAtlas(int iGender, int iHairStyle, int iHairColor)
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
		sdResourceMgr.Instance.LoadResourceImmediately(
			kAssertPath, 
			NotifyAvatarHeadAtlas, 
			kParam,
			typeof(UIAtlas));
	}

	// 加载主角头像图集回调aa
	protected void NotifyAvatarHeadAtlas(ResLoadParams kParam, UnityEngine.Object kObj)
	{		
		if (kObj == null)
			return;
		
		UIAtlas kAtlas = kObj as UIAtlas;
		
		int iGender = (int)kParam.userdata0;
		int iHairStyle = (int)kParam.userdata1;
		int iHairColor = (int)kParam.userdata2;
		
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
		
		int iHairColorIndex = iHairColor+1;	//< 图标索引:
		string kSpirteName = string.Format("{0}-{1}", iHairStyleIndex, iHairColorIndex);
		
		mAvatarHeadAtlas = kAtlas;			//< 
		mAvatarHeadSprite = kSpirteName;	//< 
		
		GameObject townUI = sdUICharacter.Instance.GetTownUI();
		if(townUI!=null)
		{
			sdTown town = townUI.GetComponent<sdTown>();
			if(town!=null)
			{
				if (town.PlayerHeadIcon != null)
				{
					UISprite kUISprite = town.PlayerHeadIcon.GetComponent<UISprite>();	
					if (kUISprite)
					{
						kUISprite.atlas = kAtlas;
						kUISprite.spriteName = kSpirteName;	
					}	
				}
			}
		}
		GameObject fightUI = sdUICharacter.Instance.GetFightUi();
		if(fightUI!=null)
		{
			sdFightUi fight = fightUI.GetComponent<sdFightUi>();
			if(fight!=null)
			{
				GameObject kHeadIcon = fight.PlayerHeadIcon;
				if (kHeadIcon != null)
				{
					UISprite kUISprite = kHeadIcon.GetComponent<UISprite>();	
					if (kUISprite)
					{
						kUISprite.atlas = kAtlas;
						kUISprite.spriteName = kSpirteName;	
					}	
				}
			}
		}
	}

	void Update() 
	{
        //CheckGC();
		SGDP.GetInstance().Process();				
		string path = "";
		string partName = "";
		string anchorName = "";
		if (timeList.Count != 0)
		{
			TimeEvent te = timeList[0];
			if (te.time <= Time.time)
			{
				te.timeEvent(te.rlp);	
				timeList.Remove(te);
			}
		}
		
		if(changeAavatar && mainChar != null && mainChar.RenderNode != null && sdDataExBuffer.mainEquipInfo != null)
		{
			if(sdDataExBuffer.mainEquipInfo.Count > 0)
			{
				foreach(EquipInfo ei in sdDataExBuffer.mainEquipInfo)
				{
					//Debug.Log ("->" + ei.filename);
					path = ei.filename;
					partName = ei.partname;
					anchorName = ei.dummyname;
					mainChar.changeAvatar(path, partName, anchorName);
				}
			}
			changeAavatar = false;
		}

		// 手动模式,处理移动aa
		if (!mAutoMode)
		{
			float xbias = 0.0f;
			float ybias = 0.0f;
			mainCharMoveControl(out xbias, out ybias);
			
			if(mainChar != null)
			{
				if(Mathf.Abs(xbias) > 0.1f || Mathf.Abs(ybias) > 0.1f)
				{
					Vector3 worldDir = moveRightDirection * xbias + moveFrontDirection * ybias;
                    worldDir.y=0.0f;
					worldDir.Normalize();
                    //正在释放技能，就不允许移动aaa
                    if (mainChar.SkillEndTime > Time.time)
                    {
                        sdActorInterface actor = sdGameLevel.instance.actorMgr.FindNearestAngle(mainChar, HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY, mainChar.GetDirection(), 5.0f, 60.0f);
                        if(actor != null)
                            mainChar._moveVector = Vector3.zero;
                        else
                        {
                            mainChar._moveVector = worldDir;
                            mainChar.TargetFaceDirection = worldDir;
                        }
                    }
                    else
                    {
                        mainChar._moveVector = worldDir;
                        mainChar.TargetFaceDirection = worldDir;
                    }

				}
				else
				{
					mainChar._moveVector = Vector3.zero;
				}
			}
		}

		//< 更新相机位置
		if(mainCamera != null && mainChar != null && cameraRelativeDistance != Vector3.zero)
		{			
			if (isFollow)
			{
				mainCamera.tick();
			}
		}
		
		//< 更新特效生命周期
		//tickGameEffect();
		
		if(sdDataExBuffer.mainItemData != null)
		{
			sdItemMgr.inst().SetItemInfo(sdDataExBuffer.mainItemData.m_Pos, sdDataExBuffer.mainItemData.m_ItemList);
			sdDataExBuffer.mainItemData = null;
		}
		
		if(sdDataExBuffer.mainEquipData != null)
		{
			sdItemMgr.inst().SetItemInfo(sdDataExBuffer.mainEquipData.m_Pos, sdDataExBuffer.mainEquipData.m_ItemList);
			sdDataExBuffer.mainEquipData = null;
		}
		
		if(sdDataExBuffer.mainWarehouseData != null)
		{
			sdItemMgr.inst().SetItemInfo(sdDataExBuffer.mainWarehouseData.m_Pos, sdDataExBuffer.mainWarehouseData.m_ItemList);
			sdDataExBuffer.mainWarehouseData = null;
		}
		
		if(sdDataExBuffer.mainMoveData != null)
		{
			sdItemMgr.inst().MoveItem(sdDataExBuffer.mainMoveData.m_Info.m_FromPos, sdDataExBuffer.mainMoveData.m_Info.m_ToPos, sdDataExBuffer.mainMoveData.m_Info.m_UUID,sdDataExBuffer.mainMoveData.m_Info.m_AnotherUUID);
			sdDataExBuffer.mainMoveData = null;
		}
		if(mainChar!=null && mainCamera!=null){
			Vector3	vPos = mainChar.gameObject.transform.localPosition;
			Shader.SetGlobalVector("MajorPos",new Vector4(vPos.x,vPos.y,vPos.z,MajorLightAttenuation));
			Shader.SetGlobalVector("MajorLightColor",new Vector4(MajorLight.r*MajorLightIntensity,
				MajorLight.g*MajorLightIntensity,
				MajorLight.b*MajorLightIntensity,MajorLightRadius));
			Vector3 dir = mainCamera.transform.position;// - mainChar.transform.position;
			//dir.Normalize();
			Shader.SetGlobalVector("cameraDir",new Vector4(dir.x,dir.y,dir.z,0));
		}
		else
		{
			Vector3	vPos = Vector3.zero;
			Shader.SetGlobalVector("MajorPos",new Vector4(vPos.x,vPos.y,vPos.z,MajorLightAttenuation));
			Shader.SetGlobalVector("MajorLightColor",new Vector4(MajorLight.r*MajorLightIntensity,
				MajorLight.g*MajorLightIntensity,
				MajorLight.b*MajorLightIntensity,MajorLightRadius));
			Vector3 dir = mainCamera.transform.position;// - mainChar.transform.position;
			//dir.Normalize();
			Shader.SetGlobalVector("cameraDir",new Vector4(dir.x,dir.y,dir.z,0));
		}

        if (levelType == LevelType.PVP && sdPVPManager.Instance.m_eCountDownType == eCountDownType.eCDT_pvp && m_bDoorOpen == false)
        {
            int[] param = new int[] { 0,0,0,0};
            if (m_blockDoor1 != null)
            {
                sdLevelAnimationDoor levelAnimationDoor1 = m_blockDoor1.GetComponent<sdLevelAnimationDoor>();
                if (levelAnimationDoor1 != null)
                {
                    levelAnimationDoor1.OnTriggerHitted(null, param);
                }
            }
            if (m_blockDoor2 != null)
            {
                sdLevelAnimationDoor levelAnimationDoor2 = m_blockDoor2.GetComponent<sdLevelAnimationDoor>();
                if (levelAnimationDoor2 != null)
                {
                    levelAnimationDoor2.OnTriggerHitted(null, param);
                }
            }
            m_bDoorOpen = true;
        }

        Hexagon.Manager.GetSingleton().Update();
	}
	
	//
	void FixedUpdate()
	{
		//if (otherCharacterRoot != null)
		//{
		//    sdOtherChar [] otherCharsTable = otherCharacterRoot.GetComponentsInChildren<sdOtherChar>();
		//    foreach (sdOtherChar otherChar in otherCharsTable)
		//        otherChar.tickFrame();
		//}
	}
	
	[ContextMenu("- Check level structure -")]
	void checkLevelStructure()
	{
		GameObject level = GameObject.Find("@GameLevel");
		if(level == null)
		{
			Debug.LogError("Must have a @GameLevel game object!");
			return;
		}
		
		if(level.transform.parent != null)
		{
			Debug.LogError("@GameLevel must be the only top level object");
			return;
		}
		
		int totalCount = level.transform.childCount;
		GameObject camera = null;
		GameObject sever = null;
		for(int i = 0; i < totalCount; ++i)
		{
			Transform trans = level.transform.GetChild(i);
			if(trans.gameObject.name == "@MainCamera")
			{
				camera = trans.gameObject;
				continue;
			}
			
			if(trans.gameObject.name == "@Server")
			{
				sever = trans.gameObject;
				continue;
			}
		}

		
		if(!(camera != null && camera.GetComponent<Camera>()))
			Debug.LogError("@GameLevel must have a @MainCamera object with a Camera component");
		
		//< 检查@Server
		if(sever == null)
		{
			Debug.LogError("@GameLevel must have a @Server object");
			return;
		}
		
		totalCount = sever.transform.childCount;
		GameObject birthPoint = null;
		for(int i = 0; i < totalCount; ++i)
		{
			Transform trans = sever.transform.GetChild(i);
			if(trans.gameObject.name == "@BirthPoint")
			{
				birthPoint = trans.gameObject;
				continue;
			}
		}
		
		if(birthPoint == null)
		{
			Debug.LogError("@Server must have a @BirthPoint object");
			return;
		}
		
	}
	
	// 创建主角aa
	public sdMainChar createMainChar(sdGameActorCreateInfo kInfo, Vector3 birthPos, Quaternion direction)
	{
		if(levelRoot == null)
			return null;
		
		GameObject kMainChar = new GameObject();
		kMainChar.transform.parent = levelRoot.transform;
		kMainChar.name = "@MainCharacter";
		kMainChar.transform.localPosition = birthPos;
		kMainChar.transform.localRotation = direction;
		kMainChar.transform.localScale = Vector3.one;
		kMainChar.layer = LayerMask.NameToLayer("Player");

		mainChar = kMainChar.AddComponent<sdMainChar>();
		//mainChar.SetVisiable_WhenLoading(false);

		CharacterController kCharacterController = kMainChar.AddComponent<CharacterController>();
		if (kCharacterController != null)
		{
			kCharacterController.center = new Vector3(0, 1.0f, 0);
			kCharacterController.radius = 0.5f;
			kCharacterController.height = 2.0f;

			mainChar.MotionController = kCharacterController;
		}

		NavMeshAgent kNavMeshAgent = kMainChar.AddComponent<NavMeshAgent>();
		if (kNavMeshAgent != null)
		{
			kNavMeshAgent.enabled = false;

			mainChar.NavAgent = kNavMeshAgent;
		}

		AudioListener audioL = kMainChar.AddComponent<AudioListener>();
		mainChar.SelfAudioSource = kMainChar.AddComponent<AudioSource>();
		mainChar.init(kInfo);
        mainChar.RequestEnd();
		return mainChar;		
	}

	// 创建其他玩家角色(PVP)aa
	public sdOtherPVPChar createOtherPVPChar(sdGameActorCreateInfo kInfo, Vector3 birthPos, Quaternion direction)
	{
		if (levelRoot == null)
			return null;

		if (otherCharacterRoot == null)
		{
			otherCharacterRoot = new GameObject();
			otherCharacterRoot.transform.parent = levelRoot.transform;
			otherCharacterRoot.name = "@OtherCharacters";
			otherCharacterRoot.transform.localPosition = Vector3.zero;
			otherCharacterRoot.transform.localRotation = Quaternion.identity;
			otherCharacterRoot.transform.localScale = Vector3.one;
			otherCharacterRoot.layer = LayerMask.NameToLayer("Player");
			if (levelType == LevelType.WorldMap)
			{
				otherCharacterRoot.SetActive(false);
			}
		}

		GameObject kOtherCharacter = new GameObject();
		kOtherCharacter.transform.parent = otherCharacterRoot.transform;
		kOtherCharacter.name = kInfo.mDBID.ToString() + "   " + kInfo.mRoleName;
		kOtherCharacter.transform.localPosition = birthPos;
		kOtherCharacter.transform.localRotation = direction;
		kOtherCharacter.transform.localScale = Vector3.one;
		kOtherCharacter.layer = LayerMask.NameToLayer("Player");

		sdOtherPVPChar kOtherPVPChar = kOtherCharacter.AddComponent<sdOtherPVPChar>();
		kOtherPVPChar.init(kInfo);

		CharacterController kCharacterController = kOtherCharacter.AddComponent<CharacterController>();
		if (kCharacterController != null)
		{
			kCharacterController.center = new Vector3(0, 1.0f, 0);
			kCharacterController.radius = 0.5f;
			kCharacterController.height = 2.0f;

			kOtherPVPChar.MotionController = kCharacterController;
		}

		NavMeshAgent kNavMeshAgent = kOtherCharacter.AddComponent<NavMeshAgent>();
		if (kNavMeshAgent != null)
		{
			kNavMeshAgent.enabled = false;

			kOtherPVPChar.NavAgent = kNavMeshAgent;
		}

		return kOtherPVPChar;	
	}
	
	// 创建其他玩家角色(主城)aa
	public sdOtherChar createOtherChar(sdGameActorCreateInfo kInfo, Vector3 birthPos, Quaternion direction)
	{
		if (levelRoot == null)
			return null;
		
		if (otherCharacterRoot == null)
		{
			otherCharacterRoot = new GameObject();
			otherCharacterRoot.transform.parent = levelRoot.transform;
			otherCharacterRoot.name = "@OtherCharacters";
			otherCharacterRoot.transform.localPosition = Vector3.zero;
			otherCharacterRoot.transform.localRotation = Quaternion.identity;
			otherCharacterRoot.transform.localScale = Vector3.one;
			otherCharacterRoot.layer = LayerMask.NameToLayer("Player");
			if(levelType==LevelType.WorldMap)
			{
				otherCharacterRoot.SetActive(false);
			}
		}
		
		sdOtherChar kOtherChar = findOtherCharByDBID(kInfo.mDBID);
		if (kOtherChar != null)
			return kOtherChar;
		
		GameObject kOtherCharacter = new GameObject();
		kOtherCharacter.transform.parent = otherCharacterRoot.transform;
		kOtherCharacter.name = kInfo.mDBID.ToString() + "   " + kInfo.mRoleName;
		kOtherCharacter.transform.localPosition = birthPos;
		kOtherCharacter.transform.localRotation = direction;
		kOtherCharacter.transform.localScale = Vector3.one;
		kOtherCharacter.layer = LayerMask.NameToLayer("Player");

		kOtherChar = kOtherCharacter.AddComponent<sdOtherChar>();
		kOtherChar.init(kInfo);
        //sdRoleHud hud = kOtherCharacter.AddComponent<sdRoleHud>();
        //if (hud)
        //{
        //    kOtherChar.notifyDisappear += hud.Disappear;
        //    hud.SetInfo(kInfo.mRoleName, "中华总工会", 1);
        //}

		NavMeshAgent kNavMeshAgent = kOtherCharacter.AddComponent<NavMeshAgent>();
		if (kNavMeshAgent != null)
		{
			kNavMeshAgent.speed = 0.0f;
			kNavMeshAgent.acceleration = 100.0f;	//< 尽量大点以改善效果aa
			kNavMeshAgent.angularSpeed = 2000.0f;	//< 尽量大点以改善效果aa
			kNavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

			kOtherChar.NavAgent = kNavMeshAgent;
		}

		return kOtherChar;		
	}
	
	public sdOtherChar findOtherCharByDBID(ulong ulDBID)
	{		
		if (otherCharacterRoot != null)
		{
			sdOtherChar [] otherCharsTable = otherCharacterRoot.GetComponentsInChildren<sdOtherChar>();
			foreach (sdOtherChar otherChar in otherCharsTable)
			{
				if (otherChar.DBID == ulDBID)
					return otherChar;
			}
		}
		
		return null;
	}
	
	public sdOtherChar findOtherCharByObjID(ulong ulObjID)
	{			
		if (otherCharacterRoot != null)
		{
			sdOtherChar [] otherCharsTable = otherCharacterRoot.GetComponentsInChildren<sdOtherChar>();
			foreach (sdOtherChar otherChar in otherCharsTable)
			{
				if (otherChar.ObjID == ulObjID)
					return otherChar;
			}
		}
		
		return null;
	}
	
	public void removeOtherCharByObjID(ulong ulObjID)
	{		
		if (otherCharacterRoot != null)
		{
			Transform rootTransform = otherCharacterRoot.transform;
			for (int i = 0; i < rootTransform.childCount; ++i)
			{
				Transform childTransform = rootTransform.GetChild(i);
				GameObject childObject = childTransform.gameObject;
				sdOtherChar otherChar = childObject.GetComponent<sdOtherChar>();
				if (otherChar != null && otherChar.ObjID == ulObjID)
				{
                    otherChar.NotifyDisappearEvent();
					GameObject.Destroy(childObject);
					return;
				}
			}
		}
	}
	
	public sdSummonObject createSummonObject()
	{
		return null;
	}
	
	public void mainCharMoveControl(out float axisX, out float axisY)
	{
		//< 角色移动的UI逻辑控制, by ruantianlong
		axisX = 0;
		axisY = 0;
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{	
			if(joyStickController == null)
				return;
			
			if(Input.touchCount > 0 && joyStickObject != null)
			{
				for(int i = 0; i < Input.touchCount; i++)
				{
					Touch touchInfo = Input.GetTouch(i);
					if(touchInfo.phase == TouchPhase.Began && !joyStickController.IsFingerDown())
					{
						if(!CheckHitUI(touchInfo.position))
						{
							if((touchInfo.position.x < Screen.width*0.5f)&&
									(touchInfo.position.y < Screen.height*0.5f))
							{
								float rectLeft = touchInfo.position.x - joyStickTexture.pixelInset.width*0.5f;
								float rectTop = touchInfo.position.y - joyStickTexture.pixelInset.height*0.5f;
								float rectWidth = joyStickTexture.pixelInset.width;
								float rectHeight = joyStickTexture.pixelInset.height;
								Rect pixelInset = new Rect(rectLeft, rectTop, rectWidth, rectHeight);
								
								joyStickTexture.pixelInset = pixelInset;
								joyStickController.ReSetDefaultRect();
								joyStickController.ResetJoystick();
								joyStickObject.SetActive(true);
							
								if(joyBackTexture != null)
								{
									pixelInset = new Rect(touchInfo.position.x-joyBackTexture.pixelInset.width*0.5f,
									touchInfo.position.y-joyBackTexture.pixelInset.height*0.5f,
									joyBackTexture.pixelInset.width,joyBackTexture.pixelInset.height);
									joyBackTexture.pixelInset = pixelInset;
								}
							}
						}
					}
				}
			}
			
			if(joyStickController.IsFingerDown())
			{
				axisX = joyStickController.position.x;
				axisY = joyStickController.position.y;
				if(joyBack != null)
				{
					joyBack.SetActive(true);
				}
			}
			else
			{
				if(joyBack != null)
				{
					joyBack.SetActive(false);
				}
			}
		}
		else
		{
			axisX = Input.GetAxis("Horizontal");
		 	axisY = Input.GetAxis("Vertical");			
		}
		
	}
	
	public sdGameCamera createMainCamera()
	{
		GameObject c = GameObject.Find("@MainCamera");
		if(c == null)
		{
			Debug.LogError("Must have a @MainCamera gameobject under @GameLevel!");
			return null;
		}		
		
		Camera cCom = c.GetComponent<Camera>();
		if(cCom == null)
		{
			Debug.LogError("@MainCamera Must have Camera component!");
			return null;
		}
		
		AudioListener al = c.GetComponent<AudioListener>();
		Destroy(al);
		
		int avatarLayer = LayerMask.NameToLayer("AvatarNode");
        int petLayer = LayerMask.NameToLayer("PetNode");
        cCom.cullingMask = cCom.cullingMask & (~(1 << avatarLayer)) & (~(1 << petLayer));		
		mainCamera = c.AddComponent<sdGameCamera>();		
		if(birthObject != null)
			cameraRelativeDistance = mainCamera.gameObject.transform.position - birthObject.transform.position;
		return mainCamera;
	}
	
	protected bool CheckHitUI(Vector2 touchPostion)
	{		
		if(UICamera == null)
			return false;
		
		Ray ray = UICamera.ScreenPointToRay(touchPostion);
		RaycastHit hit;
		LayerMask layerMask = 1 << uiLayer;
		return Physics.Raycast(ray, out hit, 100.0f, layerMask);
	}
	
	public bool storeLevelInfo()
	{
		if(mainChar != null)
		{
			sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] = mainChar.GetBaseProperty();
			sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] = mainChar.itemInfo;
		}
		return true;
	}
	
	//< for test
	void OnGUI()
	{
		if(hasMainChar && testMode)
		{
			int leftMargin = Screen.width - 100;
			if(GUI.Button(new Rect(leftMargin,0,80,80),"Dash"))
			{
				if(mainChar != null)
				{
					mainChar.CastSkill(1002);
					//Debug.Log ("_skillHit0");
					
				}
			}
			
			if(GUI.Button(new Rect(leftMargin,100,80,80),"Fatal Strike"))
			{
				mainChar.CastSkill(1003);
			}
			
			if(GUI.Button(new Rect(leftMargin,200,80,80),"Hurricane Strike"))
			{
				mainChar.CastSkill(1004);
			}
			
			if(GUI.Button(new Rect(leftMargin,300,80,80),"Circular Slay"))
			{
				mainChar.CastSkill(1005);
			}
			
			if(GUI.Button(new Rect(leftMargin,400,80,80),"Combo Hit"))
			{
				mainChar.CastSkill(1001);
			}
		}
	}
	
	//public void appendGameEffect(sdGameEffect effect, float lifeTime)
	//{
	//	if(gameEffectTable != null)
	//		gameEffectTable[effect] = lifeTime;
	//}
	
	//<
	public void quitLevel()
	{
		storeLevelInfo();
	}
	
	struct TimeEvent
	{
		public OnTime timeEvent;
		public float time;
		public ResLoadParams rlp;
	}
	
	List<TimeEvent> timeList = new List<TimeEvent>();
	
	public delegate void OnTime(ResLoadParams para);
	
	public void AddTimeEvent(float time, ResLoadParams para, OnTime timeEvent)
	{
		float eventTime = Time.time + time;
		TimeEvent te = new TimeEvent();
		te.time = eventTime;
		te.timeEvent = timeEvent;
		te.rlp = para;
		
		int num = timeList.Count;
		if (num == 0)
		{
			timeList.Add(te);		
		}
		else
		{
			for(int i = 0; i < num; ++i)
			{
				TimeEvent temp = timeList[i];
				if (temp.time <= te.time)
				{
					continue;	
				}
				else
				{
					timeList.Insert(i, te);	
				}
			}
		}

	}
	string 	ResNameToBundleName(string path)
	{
		int flagId  = path.LastIndexOf("$");
		if(flagId >= 0)
		{	
			int folderFlagId = path.IndexOf("/",flagId);
			string bundleName = path;
			if(folderFlagId >= 0)
			{
				bundleName = path.Substring(0,folderFlagId);
			}
			bundleName += ".unity3d";
			return bundleName;
		}
		return "";
	}
	void	MarkUnloadBundle()
	{
        sdResourceMgr.Instance.Clear();
        Resources.UnloadUnusedAssets();

		if(levelType == LevelType.WorldMap)
		{
            BundleGlobal.SetBundleDontUnload("$Font.unity3d");
            BundleGlobal.SetBundleDontUnload("UI/UIPrefab/$Fight.prefab.unity3d");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("UI/$Movie/moviedialogue.prefab");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/FX_UI/$Fx_Shadow/Fx_Shadow_prefab.prefab");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/FX_UI/$Fx_Zhixiang/Fx_Zhixiang_002_prefab.prefab");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("UI/$FightUI/HPBar_Jun.prefab");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("Model/drop/$qianbiDrop01/GoldCoin.prefab");
            sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/FX_UI/$Fx_Go/Fx_Go_001_prefab.prefab");

            //跑动音效...
            sdResourceMgr.Instance.PreLoadResourceDontUnload("Music/$warrior_sound/run_dirt_road_1.wav");

			Hashtable basePro = sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable;

			//设置裸模不卸载...
			byte sex	=	(byte)basePro["Sex"];
			if(sex==0)
			{
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Model/Mdl_mainChar_0/$base_hero/base_hero.fbx");
			}
			else
			{
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Model/Mdl_mainChar_1/$base_heroine/base_heroine.fbx");
			}
			//设置动画不卸载...
			byte job	=	(byte)basePro["Job"];
			Hashtable anims = sdConfDataMgr.Instance().GetTable("animation");
			
			{
				if(anims.Contains(job.ToString()))
				{
					Hashtable	animations	=	anims[job.ToString()] as Hashtable;
					foreach(DictionaryEntry de in animations)
					{
						string	name	=	de.Key	as string;
						string	str		=	de.Value as string;
						if(str.Length==0)
						{
							continue;
						}
						if(name=="JobId")
						{
							continue;
						}

                        sdResourceMgr.Instance.PreLoadResourceDontUnload(str);
						
					}
				}
			}

            Hashtable hitAudioTable = sdConfDataMgr.Instance().GetTable("hitsound");
            //预加载 技能特效 命中特效...
            Hashtable skillaction = sdConfDataMgr.Instance().m_vecJobSkillAction[job];
            foreach (DictionaryEntry de in skillaction)
            {
                Hashtable action = de.Value as Hashtable;
                string[] effectfile = action["EffectFile"] as string[];
                if (effectfile!=null )
                {
                    for (int i = 0; i < effectfile.Length; i++)
                    {
                        sdResourceMgr.Instance.PreLoadResourceDontUnload(effectfile[i]);
                    }
                }
                string HitEffect = action["HitEffect"] as string;
                if (HitEffect!=null && HitEffect.Length > 0)
                {
                    sdResourceMgr.Instance.PreLoadResourceDontUnload(HitEffect);
                }
                string[] Audio = action["AudioConf"] as string[];
                if (Audio!=null)
                {
                    for (int i = 0; i < Audio.Length; i++)
                    {
                        sdResourceMgr.Instance.PreLoadResourceDontUnload("Music/" + Audio[i]);
                    }
                }
                string weaponType = action["WeaponHitType"] as string;
                Hashtable table = hitAudioTable[weaponType] as Hashtable;
                if (table != null)
                {
                    string typePath;
                    if (weaponType == "poison" || weaponType == "ice" || weaponType == "fire" || weaponType == "lightning")
                        typePath = "hit_sound/$element_sound/";
                    else
                        typePath = "hit_sound/$" + weaponType + "_sound/";

                    foreach (DictionaryEntry hit in table)
                    {
                        string key = hit.Key as string;
                        if (key == "Weapon_Type")
                        {
                            continue;
                        }
                        string hitaudio = hit.Value as string;
                        if (hitaudio != null && hitaudio.Length > 0)
                        {
                            string[] hitArray = hitaudio.Split(';');
                            for (int i = 0; i < hitArray.Length; i++)
                            {
                                sdResourceMgr.Instance.PreLoadResourceDontUnload("Music/" + typePath + hitArray[i]);
                            }
                        }
                    }
                }
            }
            //各职业召唤物特效..
            if (job == 1)
            {
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Warrior/Fx_XuanFeng.prefab");
            }
            else if (job == 4)
            {
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Mage/Fx_Huoqiu_Fly_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Mage/Fx_HuoQiu_Buff_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Buff/Fx_Molixuanjing_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Mage/Fx_Shandianxianjng_Hit_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Hit_Effect/Fx_Fire_Hit_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Hit_Effect/Fx_lighting_Hit_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Mage/Fx_Huoqiu_Hit_001.prefab");
            }
            else if (job == 7)
            {
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Ranger/Fx_WhiteArrow.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Ranger/Fx_Yidonggongji_Fly_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Hit_Effect/Fx_Poison_Hit_001.prefab");
                sdResourceMgr.Instance.PreLoadResourceDontUnload("Effect/MainChar/$Hit_Effect/Fx_Hit_002.prefab");


            }
            else if (job == 10)
            {
                //sdResourceMgr.Instance.PreLoadResource("Effect/MainChar/$Priest.unity3d");
            }

			//设置装备不卸载..
			Hashtable items	=	sdGlobalDatabase.Instance.globalData["MainCharItemInfo"] as Hashtable;
			if(items!=null)
			{
				foreach(DictionaryEntry entry in items)
				{
					UInt64 itemID = UInt64.Parse(entry.Key.ToString());
					sdGameItem item = entry.Value as sdGameItem;
					if(item.bagIndex==2)
					{
						if(item.mdlPath.Length>0 && item.mdlPartName.Length>0)
						{
                            sdResourceMgr.Instance.PreLoadResourceDontUnload(item.mdlPath);
						}
					}
				}
			}

			//头发..
			sdPlayerInfo info = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
			if(info!=null)
			{
				int iTemplateID	=	info.mHairStyle+100+10*info.mGender+1;
				Hashtable hairTable	=	(Hashtable)sdConfDataMgr.Instance().GetHairTable();
				if(hairTable!=null)
				{
					Hashtable iteminfo	=	hairTable[iTemplateID.ToString()] as Hashtable;
					string hair = iteminfo["Filename"] as string;
                    sdResourceMgr.Instance.PreLoadResourceDontUnload(hair);
				}
			}

			
		}
		else if (levelType == LevelType.SelectRole)
		{
			BundleGlobal.ClearBundleDontUnloadFlag();
            sdResourceMgr.Instance.ClearTaskRef();
		}
	}
	
	public void PlayEffect(string strFileName, Vector3 pos, Vector3 fscale, Quaternion rot, float flifeTime)
	{
		ResLoadParams param = new ResLoadParams();
		param.pos = pos;
		param.scale = fscale;
		param.rot = rot;
		param.userdata0 = flifeTime;
		sdResourceMgr.Instance.LoadResource(strFileName, OnLoadEffect, param);
	}

	void OnLoadEffect(ResLoadParams param, UnityEngine.Object obj)
	{
		if(obj == null)
		{
			Debug.Log("gamelevel load effect fail");
			return;
		}
		GameObject effect = GameObject.Instantiate(obj) as GameObject;
		if(effect != null)
		{
			effect.transform.localPosition = param.pos;
			effect.transform.localScale = param.scale;
			effect.transform.localRotation = param.rot;

			sdAutoDestory autodestroy = effect.AddComponent<sdAutoDestory>();
			if(autodestroy != null)
			{
				autodestroy.Life = (float)param.userdata0;
			}
			else
			{
				GameObject.Destroy(effect);
			}
		}
	}
    float GC_Time       = 3.0f;
    float GC_CurrentTime = 0.0f;
    void CheckGC()
    {
        GC_CurrentTime += Time.unscaledDeltaTime;
        if (GC_CurrentTime > GC_Time)
        {
            GC_CurrentTime = 0.0f;
            GC.Collect();
            if (Application.isEditor)
            {
                Debug.Log(" GC.Collect();");
            }
        }
    }
	//<
}
