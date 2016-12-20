using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SkillError
{
    NoSp = 1,
}

public class sdEnemyStrikeInfo : object
{
	public sdEnemyStrikeInfo(float t, sdGameMonster e, int d)
	{
		strikeTime = t; enemy = e; damage = d; 
	}
	public float strikeTime = 0.0f;
	public sdGameMonster enemy = null;
	public int damage = 0;
}

/// <summary>
/// 资源异步加载
/// </summary>
public class sdResourceLoadREQ : object
{
	public sdResourceLoadREQ(string path_, string partName_, string anchorName_,
		Vector3 _loc,Vector3 _scale, float _lifeTime)
	{
		path = path_; partName = partName_; anchorName = anchorName_;
		location = _loc; scale = _scale; liftTime = _lifeTime;
	}
	public string path = "";
	public string partName = "";
	public string anchorName = "";
	public bool requsted = false;
	//< for effect load only
	public Vector3 location = Vector3.zero;
	public Quaternion rot	=	Quaternion.identity;
	public Vector3 scale = Vector3.one;
	public float liftTime = 0.0f;
}

//攻击恢复sp、hp
public class AttackRestore
{
	public float upperLimit = 0.0f ;  	//恢复的上限，超过该上限，不会恢复		
	public bool  restoreHp = true;   	//true 恢复hp false 恢复sp	
	public float ratio = 0.0f;       	//恢复的万分比	
	public int   actionID = 0;    		//只有是这些actionid的时候才会有效
	public int   monsterType = 0;  		//怪物体型； 按位于
}

// 被召唤事件aa
public delegate void NotifySummonedDelegate(sdActorInterface kActor);

// 死亡事件aa
public delegate void NotifyKilledDelegate(sdActorInterface kActor);

// 掉血事件aa
public delegate void NotifyHurtDelegate(sdActorInterface kActor, sdActorInterface kAttacker, int iHP);

// 掉血事件aa
public delegate void NotifyAddHPDelegate(sdActorInterface kActor, int iHP);

// 阻挡事件aa
public delegate void NotifyBlockedDelegate(sdActorInterface kActor);

// 状态事件aa
public delegate void NotifyAddDebuffStateDelegate(sdActorInterface kActor, HeaderProto.ECreatureActionState eState);
public delegate void NotifyRemoveDebuffStateDelegate(sdActorInterface kActor, HeaderProto.ECreatureActionState eState);



//玩家下线aaa
public delegate void NotifyDisappear();

/// <summary>
/// 游戏角色对象(提供游戏角色对象的渲染相关机制但不提供具体策略)
/// </summary>
public class sdGameActor : sdActorInterface 
{
	// 全局ID(全局唯一)aa
	protected ulong mDBID = ulong.MaxValue;	
	public ulong DBID
	{
		get { return mDBID;}
		set { mDBID = value;}
	}
	
	// 对象ID(只在当前场景有效)aa
	protected ulong mObjID = ulong.MaxValue;
	public ulong ObjID
	{
		get { return mObjID;}	
	}
	
	// 性别aa
	protected byte mGender = 0; 
	public byte Gender
	{
		get { return mGender;}	
	}
	
	// 肤色aa
	protected byte mSkinColor = 0; 
	public byte SkinColor
	{
		get { return mSkinColor;}	
	}
	
	// 发型aa
	protected byte mHairStyle = 0; 
	public byte HairStyle
	{
		get { return mHairStyle;}	
	}
	
	// 基础职业aa
	protected byte mBaseJob = 0; 
	public byte BaseJob
	{
		get { return mBaseJob;}	
	}
	
	// 职业aa
	protected byte mJob = 0; 
	public byte Job
	{
		get { return mJob;}	
	}
	
	// 等级aa
	protected int mLevel = 0; 
	public virtual	int Level
	{
		get { return mLevel;}	
	}
    //名字aaa
    protected string mName = null;
    public string Name
    {
        get { return mName; }
        set { mName = value; }
    }
	
	// 移动方向aa
	protected Vector3 moveVector = Vector3.zero;		
	public Vector3 _moveVector 
	{
		get
		{
			return moveVector;
		}
		set
		{
			moveVector = value;
			moveVector.y = 0.0f;
			moveVector.Normalize();
		}
	}
	
	// 旋转权重aa
	protected float spinWeight = 0.0f;	

	// 攻击回血回蓝aaa
	public List<AttackRestore>  attackRestoreList = new List<AttackRestore>();
	
	// 动作技能状态机aa
	public sdTSM logicTSM = new sdTSM();

	// 技能衔接时钟(用于连招系统)aa
	public sdActionStateTimer actionStateTimer = new sdActionStateTimer();
	
	public const int MAX_STRIKED_INFO_COUNT = 5;

	// 装备物品信息aaa
	public Hashtable itemInfo = null;
	
	// 挂载点数据aaa
	public Hashtable bindNodeTable	= new Hashtable();
	
	// mesh数据aaa
	public Hashtable meshTable = new Hashtable();
	
	// 技能信息aaaa
	public sdSkillTree skillTree = null;
	
	// 被攻击信息aa
	public ArrayList strikedInfo = null;
	
	// 自身音源aa
	protected AudioSource mSelfAudioSource = null;
	public AudioSource SelfAudioSource
	{
		set { mSelfAudioSource = value;}
		get { return mSelfAudioSource;}	
	}
	
	// 渲染根节点aa 可以通过renderNode 判断 裸模是否加载完成..
	protected GameObject renderNode = null;					
	public GameObject RenderNode
	{
		get { return renderNode;}	
	}
	
	// 骨骼根节点aa
	protected GameObject boneRoot = null;						
	public GameObject BoneNode
	{
		get { return boneRoot;}	
	}
	
	// 渲染节点组aa
	protected Hashtable renderNodeObjects = new Hashtable();

	// 当前朝向aa
	protected Vector3 mFaceDirection = Vector3.forward;

	// 目标面朝方向aa
	protected Vector3 mTargetFaceDirection = Vector3.forward;
	public Vector3 TargetFaceDirection
	{
		get { return mTargetFaceDirection; }
		set { mTargetFaceDirection = value; }
	}

	// 动画控制器aa
	protected Animation mAnimController = null;
	public Animation AnimController
	{
		set { mAnimController = value;}
		get { return mAnimController;}	
	}
	
	// 移动控制器aa
	protected CharacterController mMotionController = null;
	public CharacterController MotionController
	{
		set { mMotionController = value;}
		get { return mMotionController;}	
	}

	// 导航客户端aa
	protected NavMeshAgent mNavAgent = null;
	public NavMeshAgent NavAgent
	{
		set { mNavAgent = value; }
		get { return mNavAgent; }
	}

	// 自动战斗模块aa
	protected AutoFight mAutoFight = null;
	public AutoFight AutoFightSystem
	{
		get { return mAutoFight; }
	}

	//
	protected float mPreviousTime = 0.0f;
	public float PreviousTime
	{
		set { mPreviousTime = value;}
		get { return mPreviousTime;}	
	}
	
	//
	protected float mPreviousAudioTime = 0.0f;
	public float PreviousAudioTime
	{
		set { mPreviousAudioTime = value;}
		get { return mPreviousAudioTime;}	
	}
	
	protected bool mRunLFootAudioEnable = true;
	public bool RunLFootAudioEnable
	{
		set {mRunLFootAudioEnable = value;}
		get {return mRunLFootAudioEnable;}
	}
	
	protected bool bRunRFootAudioEnable = true;
	public bool RunRFootAudioEnable
	{
		set {bRunRFootAudioEnable = value;}
		get {return bRunRFootAudioEnable;}
	}
	
	//
	protected float mPreviousEffectTime = 0.0f;
	public float PreviousEffectTime
	{
		set { mPreviousEffectTime = value;}
		get { return mPreviousEffectTime;}	
	}
	
	// 刀光aa
	protected sdWeaponTrail mWeaponTrail = null;
	public sdWeaponTrail WeaponTrail
	{
		get { return mWeaponTrail;}	
	}	
	
	// 自动跟随目标aa
	protected sdActorInterface mMaster = null;
	public sdActorInterface Master
	{
		set {mMaster = value;}
		get	{return mMaster;}
	}
	
	// 自动跟随的随从aa
	protected sdActorInterface mRetainer = null;
	public sdActorInterface Retainer
	{
		set {mRetainer = value;}
		get	{return mRetainer;}
	}
	
	// 受击反白aa
	protected bool mHittedLight = false;
	protected Color mHittedLightColor = new Color(0.0f, 0.0f, 0.0f,1.0f);
	protected float mHittedLightTimeTmp = 0.0f;	

	// 选中目标效果(用于主角和宠物)aa
	protected bool mLoadTargetEffect = false;				//< 特效是否已经被请求加载aa
	protected float mTargetEffectScale = 2.5f;				//< 目标选中特效动态缩放aaa
    public float TargetEffectScale
    {
        set { mTargetEffectScale = value; }
    }
	protected GameObject mTargetEffect = null;				//< 特效对象aa
	protected sdActorInterface mTargetEffectTarget = null;	//< 特效对象目标aa	
    public sdActorInterface TargetEffectTarget
    {
        get { return mTargetEffectTarget; }
    }

	protected List<GameObject>	lstAnimation	=	new List<GameObject>();
	
	// 阻挡检测aa
	protected bool mBlockDetection = false;
	public bool BlockDetection
	{
		set {mBlockDetection = value; mBlocked = false; mBlockedTime = 0.0f;}	
		get	{return mBlockDetection;}
	}
	
	// 阻挡检测时间(超过此时间则发出一次阻挡事件)aa
	protected float mBlockDetectionTime = 3.0f;
	public float BlockDetectionTime
	{
		set {mBlockDetectionTime = value;}
		get	{return mBlockDetectionTime;}	
	}
	
	// 阻挡检测aa
	protected bool mBlocked = false;
	protected float mBlockedTime = 0.0f;

	// 导航网格相关信息aa
	protected float mLastHexagonInjectTime = 0.0f;
	protected Vector3 mLastHexagonInjectPosition = new Vector3();
	protected List<Hexagon.Coord> mDynamicHexagonWeight = new List<Hexagon.Coord>();
	
	// 被召唤事件aa
	public NotifySummonedDelegate NotifySummoned;
	
	// 被杀死事件aa
	public NotifyKilledDelegate NotifyKilled;
	
	// 掉血事件aa
	public NotifyHurtDelegate NotifyHurt;
	
	// 掉血事件(血量改变事件)aa
	public NotifyAddHPDelegate NotifyAddHP;

	// 阻挡检测事件aa
	public NotifyBlockedDelegate NotifyBlocked;
	
	// 切换攻击目标事件aa
	public NotifyChangeTargetDelegate NotifyChangeTarget;

	// 获得Buff事件aa
	public NotifyAddDebuffStateDelegate NotifyAddDebuffState;

	//
	public	GameObject GetNode(string	str)
	{
		return renderNodeObjects[str] as GameObject;
	}
	
	protected override void	Awake()
	{
		base.Awake();
	
		logicTSM._gameActor	=	this;
	}
	
	protected override void Start () 
	{
		base.Start();
	}

    protected override void Update()
    {
        base.Update();

        // 技能衔接时钟aa
        if (actionStateTimer != null)
            actionStateTimer.tick(this);

        UpdateTeleport();		//< 更新击退信息aa
        UpdateTargetEffect();	//< 更新选中目标特效aa

        // 向导航网格注入位置信息aa
		if (Time.time - mLastHexagonInjectTime > 0.1f)
        {
			bool bNeedReinject = true;

			Vector3 kOffset = this.transform.position - mLastHexagonInjectPosition;
			kOffset.y = 0.0f;
			if (kOffset.magnitude < Hexagon.Manager.GetSingleton().GetHexagonSize() * 0.2f)
			{
				bNeedReinject = false;
			}

			if (bNeedReinject)
			{
				mLastHexagonInjectTime = Time.time;
				mLastHexagonInjectPosition = this.transform.position;

				UnInject(true);
				if (IsActive())
				{
					Inject(true);
				}
			}
        }
        
    }

    // 向导航网格反注入信息
    public void UnInject(bool bClear)
    {
        Hexagon.Manager.GetSingleton().UninjectActor(ref mDynamicHexagonWeight);
        if (bClear)
        {
            mDynamicHexagonWeight.Clear();
        }
    }

	// 向导航网格注入信息aa
    public void Inject(bool bFindInjectCell)
    {
        if (bFindInjectCell)
        {
            if (Hexagon.Manager.GetSingleton().FindInjectCell(transform.position, getRadius(), ref mDynamicHexagonWeight))
            {
                Hexagon.Manager.GetSingleton().InjectActor(mDynamicHexagonWeight);
            }
        }
        else
        {
			Hexagon.Manager.GetSingleton().InjectActor(mDynamicHexagonWeight);
        }
    }

	// 销毁回调(继承自MonoBehaviour)aa
	protected override void OnDestroy()
	{
		base.OnDestroy();
		
		// 取消的Master挂接aa
		if (mMaster != null)
		{
			sdGameActor kActor = mMaster as sdGameActor;
			kActor.Retainer = null;
			
			mMaster = null;
		}
		
		// 取消Retainer的挂接aa
		if (mRetainer != null)
		{
			sdGameActor kActor = mRetainer as sdGameActor;
			kActor.Master = null;
			
			mRetainer = null;	
		}

		// 销毁选中目标特效aa
		DestoryTargetEffect();
	}
	
	public void AddAttackRestore(AttackRestore  info)
	{
		bool bFind = false;
		for(int index = 0; index < attackRestoreList.Count; ++index)
		{
			AttackRestore data = attackRestoreList[index];
			if(info.actionID == data.actionID && info.monsterType == data.monsterType && info.restoreHp == data.restoreHp)
			{
				bFind = true;
				break;
			}
		}
		if(!bFind)
			attackRestoreList.Add(info);
	}
	
	//< --------------------- 自定义功能函数 ------------------------	aaaa
	static	Transform FindChildByName(Transform node,string str)
	{
		Transform ret	=	 node.FindChild(str);
		if(ret==null)
		{
			for(int i=0;i<node.childCount;i++)
			{
				ret	=	FindChildByName(node.GetChild(i),str);
				if(ret!=null)
				{
					return ret;	
				}
			}
		}
		return ret;
	}
	public	Transform	FindBone(string	str)
	{
		
		return FindChildByName(transform,str);
	}
	public int refreshAnchorPointInfo()
	{
		return 0;
	}
	
	public int appendAnchorPointInfo(GameObject part)
	{
		return 0;
	}
	
	public int removeAnchorPointInfo(GameObject part)
	{
		return 0;
	}
	
	public void appendStrikedInfo(sdEnemyStrikeInfo info)
	{
		if(strikedInfo != null)
		{
			strikedInfo.Insert(0, info);
			//Debug.Log(strikedInfo.Count);
			//Debug.Log(info.enemy.name + "  " + info.damage.ToString());
			if(strikedInfo.Count > MAX_STRIKED_INFO_COUNT)
			{
				//strikedInfo.RemoveRange(MAX_STRIKED_INFO_COUNT, strikedInfo.Count - MAX_STRIKED_INFO_COUNT);
				strikedInfo.RemoveAt(strikedInfo.Count - 1);
			}
		}
	}
	public	static void	FindAllTransform(Transform t,List<Transform> lst)
	{
		for(int i=0;i<t.childCount;i++)
		{
			Transform child	=	t.GetChild(i);
			lst.Add (child);
			FindAllTransform(child,lst);
		}
	}
	// 收集挂载点信息aa
	protected int gatherRenderNodesInfo(GameObject rootRenderNode)
	{	
		if (rootRenderNode == null)
			return 0;
		
		List<Transform> lst = new List<Transform>();
		FindAllTransform(rootRenderNode.transform,lst);
		int count = 0;
		
		foreach(Transform t in lst)
		{
			renderNodeObjects[t.name] = t.gameObject;
			//for test
			if(t.name == "dummy_left_weapon_at" || t.name == "dummy_right_weapon_at" ||
				t.name == "dummy_left_shield_at")
			{
				bindNodeTable[t.name] = t;
				//Debug.Log(t.name);
			}
			
			++count;
		}
		
		Transform gameLevelRoot = null;
		sdGameLevel gameLevel = sdGameLevel.instance;
		if(gameLevel != null && gameLevel.effectNode != null)
			gameLevelRoot = gameLevel.effectNode.transform;
		
		bindNodeTable["gamelevel"] = gameLevelRoot;
		bindNodeTable["self"] = this.transform;
		bindNodeTable["rendernode"] = rootRenderNode.transform;
		//Debug.Log("gatherRenderNodesInfo");
		return count;
	}
	
	// 创建刀光aa
	public sdWeaponTrail createWeaponTrail(string anchorNodeName)
	{
		if(!bindNodeTable.ContainsKey(anchorNodeName))
			return null;
		
		Transform anchorNode = bindNodeTable[anchorNodeName] as Transform;
		GameObject trailDummy = new GameObject();
		trailDummy.name = "$trailDummy";
		trailDummy.transform.parent = anchorNode;
		trailDummy.transform.localPosition = Vector3.zero;
		trailDummy.transform.localRotation = Quaternion.AngleAxis(90.0f, new Vector3(1, 0, 0));
		trailDummy.transform.localScale = Vector3.one;
		
		MeshFilter mf = trailDummy.AddComponent<MeshFilter>();
		mf.mesh = null;
		
		MeshRenderer meshRenderer = trailDummy.AddComponent<MeshRenderer>();
		Material mat = Resources.Load("weaponTrailMat") as Material;
		meshRenderer.material = mat;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		
		sdWeaponTrail trail = trailDummy.AddComponent<sdWeaponTrail>();
		
		return trail;
	}

	// 加载选中目标效果(用于主角和宠物)aa
	protected void LoadTargetEffect()
	{
		if (mLoadTargetEffect)
			return;

		mLoadTargetEffect = true;

		if (mTargetEffect == null)
		{
			if (GetActorType() == ActorType.AT_Player)
			{
				string kPath = "Effect/MainChar/FX_UI/$Fx_Go/Fx_Go_001_prefab.prefab";
				ResLoadParams kParam = new ResLoadParams();
				kParam.info = "targetEffect";

				sdResourceMgr.Instance.LoadResource(kPath, OnLoadTargetEffect, kParam);
			}
			else if (GetActorType() == ActorType.AT_Pet)
			{
				string kPath = "Effect/MainChar/FX_UI/$Fx_Go/Fx_Go_Pet_prefab.prefab";
				ResLoadParams kParam = new ResLoadParams();
				kParam.info = "targetEffect";

				sdResourceMgr.Instance.LoadResource(kPath, OnLoadTargetEffect, kParam);
			}
		}
	}

	// 加载选中目标效果回调(用于主角和宠物)aa
	protected void OnLoadTargetEffect(ResLoadParams kParam, Object kObj)
	{
		if (kObj == null)
			return;

		if (kParam.info == "targetEffect")
		{
			if (mTargetEffect == null)
			{
				mTargetEffect = GameObject.Instantiate(kObj) as GameObject;
				mTargetEffect.SetActive(false);
			}
		}
	}
	
	// 销毁选中目标效果(用于主角和宠物)aa
	protected void DestoryTargetEffect()
	{
		if (mTargetEffect != null)
			GameObject.Destroy(mTargetEffect);
	}

	// 取消选中目标效果aa
	protected void DeactiveTargetEffect()
	{
		if (mTargetEffect != null)
		{
			mTargetEffect.SetActive(false);
			mTargetEffectTarget = null;
		}
	}

	// 更新选中目标效果(用于主角和宠物)aa
	protected void UpdateTargetEffect()
	{
		if (mTargetEffect != null && mTargetEffectTarget != null)
		{
			if (mTargetEffectTarget.GetCurrentHP() <= 0 || mTargetEffectTarget.Hide || mTargetEffectTarget.Movie || !mTargetEffectTarget.IsActive())
			{
				mTargetEffect.SetActive(false);
			}
			else
			{
				if (!mTargetEffect.activeSelf)
				{
					mTargetEffectScale = 2.5f;
					mTargetEffect.SetActive(true);
				}

				mTargetEffect.transform.position = mTargetEffectTarget.transform.position;

				if (mTargetEffectScale > 1.5f)
					mTargetEffectScale -= 2.5f * Time.deltaTime;	//< 0.4s内从2.5f->1.5faa
				float fScale = mTargetEffectTarget.getRadius() * mTargetEffectScale;
				mTargetEffect.transform.localScale = Vector3.one * fScale;
			}
		}
	} 
	
//	protected void _flashColorInternal()
//	{
//		if(colorTimeLerp >= 0.0f && swithColorFlash)
//		{
//			foreach(DictionaryEntry entry in meshTable)
//			{
//				Color clr = new Color(1.0f, 1.0f, 1.0f, 0.0f);
//				Renderer meshInfo = (entry.Value as GameObject).GetComponent<Renderer>();
//				foreach(Material mat in meshInfo.materials)
//				{
//					mat.SetColor("_AddColor", clr*colorTimeLerp);
//				}
//			}
//			
//			colorTimeLerp -= (Time.deltaTime)*6.0f;
//			if(colorTimeLerp < 0.0f)
//			{
//				swithColorFlash = false;
//				colorTimeLerp = 0.0f;
//			}
//		}
//	}
//	
//	// 受击反白aa
//	public void FlashWhite()
//	{
//		swithColorFlash = true;
//		colorTimeLerp = 1.0f;
//	}
	
	// 受击反白aa
	public void DoHittedLight()
	{
		mHittedLightTimeTmp = 0.0f;
		mHittedLight = true;
		mHittedLightColor = Color.white;
	}
	
	public void DoHittedLight(Color kColor)
	{
		mHittedLightTimeTmp = 0.0f;
		mHittedLight = true;
		mHittedLightColor = kColor;
	}
	
	// 更新受击反白aa
	protected void UpdateHittedLight()
	{
		if (mHittedLight)
		{
			mHittedLightTimeTmp += Time.deltaTime;
			
			float fLightTime = 0.15f;	
			float fLightParam = (mHittedLightTimeTmp / fLightTime) * Mathf.PI * 0.5f;
			
			if (mHittedLightTimeTmp > fLightTime)
			{
				mHittedLight = false;
				mHittedLightTimeTmp = 0.0f;
				fLightParam = Mathf.PI * 0.5f;
				
				//return;
			}
			
			fLightParam = Mathf.Cos(fLightParam);
			foreach(DictionaryEntry kEntry in meshTable)
			{
				GameObject kObject = kEntry.Value as GameObject;
				Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
				for (int k = 0; k < kObjectRenderer.materials.Length; k++)
				{
					if (kObjectRenderer.materials[k] != null)
						kObjectRenderer.materials[k].SetColor("_AddColor", mHittedLightColor * fLightParam);
				}
			}
		}
	}
	
	// 收集渲染对象信息aa
	protected int gatherMeshes(GameObject rootRenerNode)
	{
		if (rootRenerNode == null)
			return 0;
		
		SkinnedMeshRenderer[] skinmesh = rootRenerNode.GetComponentsInChildren<SkinnedMeshRenderer>();
		int i = skinmesh.Length;
		foreach (SkinnedMeshRenderer child in skinmesh)
		{
			meshTable[child.name] = child.gameObject;
		}
		MeshRenderer[] staticmesh = rootRenerNode.GetComponentsInChildren<MeshRenderer>();
		i += staticmesh.Length;
		foreach (MeshRenderer child in staticmesh)
		{
			meshTable[child.name] = child.gameObject;
		}

		return i;
	}
	
	// 加载角色动画aa
	protected void loadAnimation(string path)
	{
		ResLoadParams param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResource(path,OnLoadAnimation,param);
	}
	
	virtual protected void NotifyFinishLoadAni(string name)
	{
		return;
	}
	
	protected void	OnLoadAnimation(ResLoadParams param,Object obj)
	{
		if(obj==null)
		{
			return;
		}
		if(mAnimController==null)
		{
			lstAnimation.Add(obj as GameObject);
			return;
		}
		foreach(AnimationState aniState in (obj as GameObject).GetComponent<Animation>())
		{
			if(mAnimController!=null)	
			{
				mAnimController.AddClip(aniState.clip, aniState.name);
					NotifyFinishLoadAni(aniState.name);
			}
		}
	}
	
	//< 在指定特效挂载点上挂载特效aaa
	public void attachEffectInternal(GameObject newEffect, string anchorPoint, Vector3 location, Quaternion rot,Vector3 scale,float life)
	{
		if(newEffect != null)
		{
			bool isExist = bindNodeTable.ContainsKey(anchorPoint);
			if(isExist)
			{		
				Transform bindParent = bindNodeTable[anchorPoint] as Transform;
				if(bindParent != null)
				{
					Vector3 	initPos	=	bindParent.position;
					Quaternion 	initRot	=	bindParent.rotation;
					GameObject newInstance = GameObject.Instantiate(newEffect,initPos,initRot) as GameObject;		
					if(newInstance != null)
					{				
						sdAutoDestory autoDestory = newInstance.AddComponent<sdAutoDestory>();
						if(autoDestory != null)
						{					
							newInstance.transform.parent = bindParent;
							newInstance.transform.localPosition = location;
							newInstance.transform.localRotation = rot;
							newInstance.transform.localScale = scale;
							autoDestory.Life	=	life;
                            if (actorType == ActorType.AT_Monster)
                            {
                                autoDestory.actor = this;
                            }
						}
						else
						{
						
							Debug.Log("[attachEffectInternal] AddComponent<sdAutoDestory>() == null");
							
						}
					}
					else
					{
						Debug.Log("[attachEffectInternal] GameObject.Instantiate == null");
					}
				}
				else
				{
					Debug.Log("[attachEffectInternal] bindParent == null");
				}
			}
			else
			{
				Debug.Log("[attachEffectInternal] anchorPoint don't exist");
			}
		}
		else
		{
			Debug.Log("[attachEffectInternal] newEffect == null");
		}
		
	
	}
	
	// 加载特效aa
	public void attachEffect(string path, string anchorPoint, Vector3 location,Quaternion rot,float scale, float lifeTime)
	{
		ResLoadParams param	=	new ResLoadParams();
		param.pos	=	location;
		param.scale	=	new Vector3(scale,scale,scale);
		param.rot	=	rot;
		param.info	=	anchorPoint;
		param.userdata0	=	lifeTime;
        sdResourceMgr.Instance.LoadResourceImmediately(path, OnLoadEffect, param);
	}
	void	OnLoadEffect(ResLoadParams param,Object obj)
	{
		if(obj==null)
			return;
		attachEffectInternal(obj as GameObject, param.info, param.pos,param.rot,param.scale,(float)param.userdata0);

	}
	
	public void PlayAudio(string path)
	{
		string audioFilePath = "Music/" + path;	
		ResLoadParams param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResource(audioFilePath, OnLoadAudio, param);
	}
	
	void OnLoadAudio(ResLoadParams param, Object obj)
	{
		AudioClip clip = obj as AudioClip;
		if(clip != null && SelfAudioSource != null)
		{
			SelfAudioSource.PlayOneShot(clip);
		}
	}

	protected static	void	AddNewBone(Transform newBone,Transform oldBone)
	{
		for(int i=0;i<newBone.childCount;i++)
		{
			Transform	newChild	=	newBone.GetChild(i);
			bool bFind = false;
			for(int j=0;j<oldBone.childCount;j++)
			{
				Transform oldChild	=	oldBone.GetChild(j);
				if(oldChild.name	==	newChild.name){
					AddNewBone(newChild,oldChild);
					bFind	=	true;
					break;
				}
			}
			if(!bFind){
				GameObject	newObj	=	new GameObject();
				newObj.transform.parent	=	oldBone;
				newObj.name				=	newChild.name;
				newObj.transform.localEulerAngles	=	newChild.localEulerAngles;
				newObj.transform.localPosition		=	newChild.localPosition;
				newObj.transform.localRotation		=	newChild.localRotation;
				newObj.transform.localScale			=	newChild.localScale;
				
				AddNewBone(newChild,newObj.transform);
			}
			
		}
	}
	protected static Transform	SearchRoot(Transform newBone,string name){

		for(int k = 0; k < newBone.childCount; ++k){
			Transform kChild = newBone.GetChild(k);
			if(kChild.name	==	name){
				return kChild;
			}else{
				Transform	bone = SearchRoot(kChild,name);
				if(bone!=null)
				{
					return bone;
				}
			}
		}
		return null;
	}
	void	ChangeModel(Transform childTrans,string anchorName)
	{
		
		if(childTrans != null)
		{			
			SkinnedMeshRenderer skinnedMesh = childTrans.gameObject.GetComponent<SkinnedMeshRenderer>();
			MeshRenderer staticMesh = childTrans.gameObject.GetComponent<MeshRenderer>();
			bool avatarChanged = false;
			if(skinnedMesh != null)
			{
				skinnedMesh.receiveShadows	=	false;
				
				Transform rootbone	=	skinnedMesh.rootBone;
				
				Transform	oldRoot	=	SearchRoot(renderNode.transform,rootbone.name);
				if(oldRoot!=null){
					AddNewBone(rootbone,oldRoot);
				}else{
					Debug.LogError("Cant Find Root Bone From Old Skeleton!");
				}
				List<Transform> newTargetBones = new List<Transform>();
				foreach(Transform srcBone in skinnedMesh.bones)
				{
					if(renderNodeObjects.Contains(srcBone.name))
						newTargetBones.Add((renderNodeObjects[srcBone.name] as GameObject).transform);
				}					
	
				skinnedMesh.bones = newTargetBones.ToArray();
				skinnedMesh.rootBone = (renderNodeObjects[skinnedMesh.rootBone.name] as GameObject).transform;
				childTrans.parent = renderNode.transform;
				
				avatarChanged = true;

				NotifySkinnedMeshChanged(skinnedMesh);		//< 回调函数
			}
			else if(staticMesh != null)
			{
				staticMesh.receiveShadows	=	false;
				bool isExist = bindNodeTable.ContainsKey(anchorName);
				if(isExist)
				{
					Transform bindParent = bindNodeTable[anchorName] as Transform;
					childTrans.parent = bindParent;
					childTrans.localPosition = new Vector3(0, 0, 0);
					childTrans.localRotation = Quaternion.identity;
					childTrans.localScale =  Vector3.one;
					avatarChanged = true;
					
					NotifyStaticMeshChanged(staticMesh);	//< 回调函数aa
				}
			}
			
			if(avatarChanged)
			{
				if(renderNodeObjects.Contains(childTrans.name))
				{
					GameObject oldAvatarPart = renderNodeObjects[childTrans.name] as GameObject;
					oldAvatarPart.transform.parent = null;
					//Debug.Log ("Destroy " + oldAvatarPart.name);
					GameObject.Destroy(oldAvatarPart);
				}
				renderNodeObjects[childTrans.name] = childTrans.gameObject;			
				
				meshTable[childTrans.name] = childTrans.gameObject;
				RefreshAvatar();	
									
			}
		}
		
	}
	// 换装aa
	protected GameObject changeAvatarInteral(GameObject avatarSuite, string path, string partName, string anchorName)
	{
		if(renderNode == null || renderNodeObjects.Count==0)
		{
			return null;
		}
		
		GameObject retObj	=	null;
		
		
		sdGameLevel gameLevel = sdGameLevel.instance;
		
		if(path.Length == 0)
		{
			if(renderNodeObjects.Contains(partName))
			{                    
				GameObject oldAvatarPart = renderNodeObjects[partName] as GameObject;
				oldAvatarPart.transform.parent = null;
				GameObject.Destroy(oldAvatarPart);
				renderNodeObjects.Remove(partName);

				RefreshAvatar();
			}
			return null;
		}
		
			
		if(avatarSuite == null)
		{
			Debug.LogError("can not find avatar suite " + path);
			return null;
		}
		
		GameObject newAvatarWidget = GameObject.Instantiate(avatarSuite) as GameObject;	
		newAvatarWidget.name	=	avatarSuite.name;
		bool bNeedDestory	=	true;
		if(newAvatarWidget != null)
		{
			int childCount = newAvatarWidget.transform.childCount;
			if(newAvatarWidget.name	==	partName && childCount == 0)
			{
				retObj	=	newAvatarWidget;
				ChangeModel(newAvatarWidget.transform,anchorName);
				bNeedDestory	=	false;
			}
			else
			{
				
				for(int i = 0; i < childCount; ++i)
				{
					Transform childTrans = newAvatarWidget.transform.GetChild(i);
					if(childTrans.name	==	partName)
					{
						retObj	=	childTrans.gameObject;
						ChangeModel(childTrans.transform,anchorName);

						i=childCount;
					}
				}
			}
			if(retObj==null)
			{
				
				Debug.Log("retObj==null");
			}
		}
		else
		{
			Debug.Log("newAvatarWidget == null");
		}
		if(bNeedDestory)
		{
			GameObject.Destroy(newAvatarWidget);
		}
		return retObj;
	}
	
	// 通知角色装备被更新aa
	public virtual void RefreshAvatar()
	{
		//refreshAvatarAnimation(character);
	}
	
	// 通知aa
	protected virtual void NotifyStaticMeshChanged(MeshRenderer kSkinnedMeshRenderer)
	{
		
	}
	
	protected virtual void NotifySkinnedMeshChanged(SkinnedMeshRenderer kSkinnedMeshRenderer)
	{
		
	}

	// 获取职业(继承自sdActorInterface)aa
	public override HeaderProto.ERoleJob GetJob()
	{
		return (HeaderProto.ERoleJob)Job;
	}

	// 获取基础技能属性表(继承自sdActorInterface)aa
	public override Hashtable GetBaseSkillProperty()
	{
		return sdConfDataMgr.Instance().m_vecJobSkillInfo[Job];
	}

	// 获取基础技能动作表(继承自sdActorInterface)aa
	public override Hashtable GetBaseSkillAction()
	{
		return sdConfDataMgr.Instance().m_vecJobSkillAction[Job];
	}

	// 设置技能动作表(继承自sdActorInterface)aa
	public override void SetSkillAction(Hashtable prop)
	{
		SkillAction		=	prop;
		Hashtable table	=	logicTSM.states;
		foreach(DictionaryEntry item in prop)
		{
			int id	=	(int)item.Key;
			sdBaseState	st	=	table[id] as sdBaseState;
			if(st!=null)
			{
				st.SetInfo(item.Value as Hashtable, this);
			}
		}
	}

	// 角色当前朝向aa
	public override Vector3 GetDirection()
	{
		return mFaceDirection;
	}
	
	// 临时移动函数aa
	public void motionFunction_todo()
	{
        mTargetFaceDirection = _moveVector;

		spinToTargetDirection(mTargetFaceDirection, false);
		moveGravity(GetDirection()*GetMoveSpeed());
	}
	public	void	moveGravity(Vector3 v)
	{
		v.y-=9.8f;
		moveInternal(v);
	}
	
	// 角色移动aa
	public	virtual void moveInternal(Vector3 kDelta)
	{
		if (mMotionController == null)
			return;
		if (!mMotionController.enabled)
			return;
		
		// 状态检测aa
		if (CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY)
			|| CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD)
			|| CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN)
			|| CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_KNOCKBACK))
			return;
		
		// 阻挡检测aa
		CollisionFlags flags = mMotionController.Move(kDelta * Time.deltaTime);
		if (mBlockDetection)
		{
			if((int)(flags&CollisionFlags.Sides) != 0)
			{
				if (mBlocked)
				{
					mBlockedTime += Time.deltaTime;
					if (mBlockedTime >= mBlockDetectionTime)
					{
						mBlocked = false;
						mBlockedTime = 0.0f;
						
						if (NotifyBlocked != null)
							NotifyBlocked(this);
					}
				}
				else
				{
					mBlocked = true;
					mBlockedTime = 0.0f;
				}
			}
			else
			{
				mBlocked = false;
				mBlockedTime = 0.0f;	
			}
		}
	}
	
	// 角色旋转aa
	public virtual	void spinToTargetDirection(Vector3 targetDirection, bool Immediate)
	{
		if(renderNode == null)
		{
			Debug.LogError("Main char has no render node!");
			return;
		}
		
		if(CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD)
			|| CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN))
			return;
		
		if(Mathf.Abs(targetDirection.x) > 0.1f || Mathf.Abs(targetDirection.y) > 0.1f || Mathf.Abs(targetDirection.z) > 0.1f)
		{
			float fDot = Vector3.Dot(mFaceDirection, targetDirection);
			if(fDot > 0.9999f)
			{
				spinWeight = 0.0f;
				return;
			}
			
			Quaternion targetQuat;
			if(Vector3.Dot(Vector3.forward,targetDirection) < -0.9999f)
				targetQuat = Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f));	
			else{
				targetQuat = Quaternion.FromToRotation(Vector3.forward, targetDirection);	
			}

			Quaternion result = Quaternion.identity;
			if(Immediate)
			{
				result = targetQuat;
			}
			else
			{
				Quaternion currentQuat;
				if (Vector3.Dot(Vector3.forward, mFaceDirection) < -0.9999f)
					currentQuat = Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f));	
				else
					currentQuat = Quaternion.FromToRotation(Vector3.forward, mFaceDirection);
				
				spinWeight += (Time.deltaTime*5.0f);
				if(spinWeight > 1.0f)
				{
					spinWeight = 1.0f;
				}
				
				result = Quaternion.Slerp(currentQuat, targetQuat, spinWeight);
			}

			mFaceDirection = result * Vector3.forward;
			this.gameObject.transform.rotation = result;
		}
	}
	// 释放技能aa
    public bool CastSkill(int id)
    {
        int error = -1;
        return CastSkill(id, ref error);
    }

	// 释放技能aa
    public bool CastSkill(int id, ref int error)
	{
		// 技能信息aa
		if (skillTree == null)
			return false;
		
		// 技能信息aa
		sdSkill	kSkill = skillTree.getSkill(id);
		if (kSkill == null)
			return false;

		// 
		if (!kSkill.skillEnabled)
            return false;

		// 技能冷却aa
		if (kSkill.skillState == (int)sdSkill.State.eSS_CoolDown)
			return false;	
		
		// 技能耗蓝aa
        if (!kSkill.CheckSP(this))
        {
            error = (int)SkillError.NoSp;
            return false;
        }

		// 角色处于昏睡aa
		if(CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD))
			return false;	
		
		// 角色处于昏迷aa
		if(CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN))
			return false;
		
		// 
		if(CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_TELEPORT))
			return false;
		
		// 角色处于封技aa
		if(CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL))
			return false;		
		//
		if (logicTSM==null)
			return false;
		//
		if (actionStateTimer.OnCastSkill(this, kSkill))
		{
			return true;
		}
		else
		{
			return logicTSM.OnCastSkill(kSkill);	
		}
	}
    public bool CastSkill(int id, Vector3 targetDir)
    {
		mTargetFaceDirection = targetDir;
        return CastSkill(id);
    }
	
	// 虚函数继承(继承自sdActorInterface)
	public override	void AddDebuffState(HeaderProto.ECreatureActionState state)
	{
		if (NotifyAddDebuffState != null)
			NotifyAddDebuffState(this, state);
		
		base.AddDebuffState(state);
	}
	
	// 虚函数继承(继承自sdActorInterface)aa
	public override void OnHurt(DamageResult dr)
	{
		if (NotifyHurt != null)
			NotifyHurt(this, dr.attracker, dr.damage);
		
		base.OnHurt(dr);
		if(dr.damage > 0) //闪避不需要泛白aaa
		    DoHittedLight();		//< 受击泛白aa
		ShowDamageBubble(dr);	//< 受击伤害冒泡aa
	}
	
	// 虚函数继承(继承自sdActorInterface)aa
	public override	void AddHP(int hp)
	{
		if (NotifyAddHP != null)
			NotifyAddHP(this, hp);
		base.AddHP(hp);
        // 通知死亡消息aa
        if (GetCurrentHP() <= 0 && NotifyKilled != null)
        {
           NotifyKilled(this);
        }
	}
	
	public	virtual	void	ShowDamageBubble(DamageResult dr)
	{
		Vector3 bubblePos	=	transform.position;
		bubblePos.y+=1.5f;
		

		if(dr.bubbleType ==	Bubble.BubbleType.eBT_Miss)
		{
			Bubble.MissBubble(bubblePos, false);
		}
		else if(dr.bubbleType == Bubble.BubbleType.eBT_BaseHurt 
			|| dr.bubbleType == Bubble.BubbleType.eBT_SkillHurt 
			|| dr.bubbleType == Bubble.BubbleType.eBT_CriticalBaseHurt 
			|| dr.bubbleType == Bubble.BubbleType.eBT_CriticalSkillHurt
			|| dr.bubbleType == Bubble.BubbleType.eBT_PetHurt)
		{
			Bubble.OtherHurt(dr.damage, bubblePos, dr.bubbleType);
		}
        else if (dr.bubbleType == Bubble.BubbleType.eBT_Dodge)
        {
            Bubble.Dodge(bubblePos, false);
        }
        else
        {
            Bubble.OtherHurt(dr.damage, bubblePos, dr.bubbleType);
        }


	}
	public	override	void	SetTeleportInfo(TeleportInfo info)
	{
		base.SetTeleportInfo(info);
		//播放被击动画 动画播放模式必须是Once
		if(mAnimController!=null)
		{
			if(	logicTSM.statePointer 	== 	logicTSM.idle ||
				logicTSM.statePointer	==	logicTSM.run)
			{
				AnimationState state	=	mAnimController["knockback01"];
				if(state!=null)
				{
					state.wrapMode	=	WrapMode.Once;
					state.layer	=	10;
					mAnimController.Play("knockback01",PlayMode.StopSameLayer);
				}
			}
		}
		if(info.TeleportState)
			AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_TELEPORT);

	}
	public	override	void	UpdateTeleport()
	{
		base.UpdateTeleport();
		Vector3 dir = Vector3.zero;
		for(int index = 0; index < tpInfo.Count; ++index)
		{
			TeleportInfo info = tpInfo[index];
			if(info.MoveTime > 0.0f)
			{
				Vector3 vec = info.castCenter - transform.position;
				if(info.Type == 0)
					vec = -vec;
				vec.Normalize();
				vec*=info.MoveSpeed;
				dir += vec;
			}
		}
		dir.y -= 9.8f;
		if(mMotionController != null&&tpInfo.Count>0 && dir.magnitude > 0.01f)
		{
			mMotionController.Move(dir* Time.deltaTime);	
		}
	}
	
	public	static	string	WeaponDummy(string strID)
	{
		int id	=	int.Parse(strID);
		if (id == 0)
		{
			return	"dummy_right_weapon_at";
		}
		else if(id == 9)
		{
			return	"dummy_left_shield_at";
		}
		else
		{
			return	"dummy_right_weapon_at";
		}
	}
	
	public override void OnAttackRestore(int actionID, int hp, int monsterType)
	{
		for(int index = 0; index < attackRestoreList.Count; ++index)
		{
			AttackRestore info = attackRestoreList[index];
			if((actionID == info.actionID || info.actionID == 0) && (((1<<monsterType)&info.monsterType) != 0))
			{
				if(info.restoreHp)
				{
					int currentHp = GetCurrentHP();
					int maxHp = GetMaxHP();
					float fRatio = (float)((float)currentHp/maxHp);
					bool bUnlimit = float.Equals(info.upperLimit, 0.0f);
					if(fRatio < info.upperLimit || bUnlimit)
					{
						int Value = (int)(hp*info.ratio);
                        if (Value == 0)Value = 1;
						if(!bUnlimit)
						{
							if(Value + GetCurrentHP() > GetMaxHP()*info.upperLimit)
								Value = (int)(GetMaxHP()*info.upperLimit) - GetCurrentHP();
						}
						AddHP(Value);
					}						
				}
				else
				{
					int currentSp = GetCurrentSP();
					int maxSp = GetMaxSP();
					float fRatio = (float)((float)currentSp/maxSp);
					bool bUnlimit = float.Equals(info.upperLimit, 0.0f);
					if(fRatio < info.upperLimit || bUnlimit)
					{
						int Value = (int)(hp*info.ratio);
                        if (Value == 0)Value = 1;
						if(!bUnlimit)
						{
							if(Value + GetCurrentSP() > GetMaxSP()*info.upperLimit)
								Value = (int)(GetMaxSP()*info.upperLimit) - GetCurrentSP();
						}
						AddSP((int)(hp*info.ratio));
					}
				}
			}
		}
	}

	// 推开其他角色aa
	protected void	PushOtherActor()
	{
		float 	fLength		=	moveVector.magnitude*0.5f;
		Vector3	vMoveDir	=	moveVector;
		if( fLength	< 0.01f)
		{
			fLength=0.25f;
			vMoveDir = mFaceDirection;
		}
		
		
		sdCapsuleShape	myShape	=	new sdCapsuleShape();
		myShape.SetInfo(mMotionController);
		myShape.point += mFaceDirection * fLength;
		
		float radius		=	myShape.length + myShape.radius;
		float	myVolume	=	myShape.radius;
		List<sdActorInterface> lstActor	=	sdGameLevel.instance.actorMgr.FindActor(this,HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,myShape.point,new Vector3(0,0,1),1,0,radius,true);
		
		// 追随者加入推开列表aa
		if (mRetainer != null && mRetainer.IsActive())
		{
			if (lstActor == null)
				lstActor = new List<sdActorInterface>();
			
			lstActor.Add(mRetainer);	
		}
	
		if (lstActor == null || lstActor.Count == 0)
			return;
	
		sdCapsuleShape	otherShape	=	new sdCapsuleShape();
		for(int i=0;i<lstActor.Count;i++)
		{
			sdGameActor	actor	=	(sdGameActor)lstActor[i];
			if(actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN))
			{
				continue;
			}
			if(actor.actorType == ActorType.AT_Monster)
			{
				int Ability	=	actor["Ability"];
				if(Ability>=((int)HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss))
				{
					continue;
				}
			}
			otherShape.SetInfo(actor.mMotionController);
		
			float Volume	=	otherShape.radius;
			float fParam	=	Volume/myVolume;
			if(fParam	>	2.0f)
			{
				continue;
			}
			if(fParam<0.5f)
			{
				fParam	=	0.5f;
			}
		
			// 如果发生碰撞则推开目标(其余怪物往对应方向推,宠物往两侧推)aa
			if (myShape.IsIntersect(otherShape))
			{
				if (actor == mRetainer)
				{
					Vector3 vDir = actor.transform.position - transform.position;
					vDir.y = 0.0f;
					
					Vector3 kDirection = _moveVector;
					kDirection.y = 0.0f;
					
					Vector3 kRight = Vector3.Cross(kDirection, Vector3.up);
					Vector3 kPushDir = kRight;
					if (Vector3.Dot(vDir, kRight) < 0.0f)
						kPushDir = -kRight;
					
					kPushDir.Normalize();
					actor.moveInternal(kPushDir * 10.0f);
				}
				else
				{
					Vector3 vDir = actor.transform.position - transform.position;
					vDir.y = 0.0f;
					vDir.Normalize();
					
					actor.moveInternal(vDir/(fParam));
				}	
			}
		}
		
	}
	protected void	LoadShadow()
	{
		//if(QualitySettings.GetQualityLevel() < 2)
		{
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("Effect/MainChar/FX_UI/$Fx_Shadow/Fx_Shadow_prefab.prefab",OnLoadShadow,param);
		}
	}
	void	OnLoadShadow(ResLoadParams param,Object obj)
	{
		if(obj==null)
		{
			return;
		}
		
		GameObject	shadow	=	GameObject.Instantiate(obj,renderNode.transform.position,Quaternion.identity) as GameObject;
		if(shadow!=null)
		{
			shadow.transform.parent	=	renderNode.transform;
			shadow.transform.localPosition	=	Vector3.zero;
			shadow.transform.localRotation	=	Quaternion.identity;
			shadow.transform.localScale	=	Vector3.one*getRadius();
		}
	}

	// 挂接到群体AI节点回调aa
	public virtual void OnAddToBehaviourNode(sdBehaviourNode kBehaviourNode)
	{

	}

	// 从群体AI节点取消挂接回调aa
	public virtual void OnRemoveFromBehaviourNode(sdBehaviourNode kBehaviourNode)
	{

	}
}
