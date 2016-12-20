using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 鍙嶅嚮浼樺厛绾?a
public enum EBeatPriority
{
	BP_Unknown 	= 0,
	BP_Hight 	= 1,
	BP_Middle 	= 2,
	BP_Low 		= 3,
}

// 鎶ょ敳绫诲瀷aa
public enum EArmType
{
	AT_Armor,
	AT_Bone,
	AT_Dry,
	AT_Flesh,
	AT_Liquid,
	AT_Stone,
	AT_Ghost,
	AT_Leather,
	AT_Wood,
}

/// <summary>
/// 鎬?墿瀵硅薄aa
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("SNDA/GameLogic/sdGameMonster")]
public class sdGameMonster : sdGameActor
{
	
	
	// 鐗规畩鎸傛帴鐐筧a
	public	string	ChestPoint;
	public	string	HeadPoint;

	// 鎬?墿妯℃澘idaa
	public int templateId = -1;
	
	// 鎬?墿鍞?竴IDaa
	public int uniqueId = -1; 
	
	// 鎬?墿鍒锋柊鏈哄埗,榛樿?鍑虹幇鍦ㄥ満鏅?腑鎴栬呰Е鍙戜箣鍚庢墠鍑虹幇鍦ㄥ満鏅痑a
	public bool bornLive = false;

	// 鎬?墿鏄?惁灞炰簬娣辨笂BOSS..
	public bool isLapBoss = false;
	
	// 鎶ょ敳绫诲瀷aa
	public EArmType armType = EArmType.AT_Armor;
	
	// 鍑虹敓闊虫晥aa
	public string bornAudio = null;
	
	// 鍑虹敓鐗规晥aa
	public string bornEffect = null;
	
	// 鍑虹敓鍔ㄤ綔aa
	public string bornAinm = null;

	//鍑虹敓鍔ㄨ繃鍚庢帴鐨勫姩浣渁a
	//public string bornAnimNext = null;
	
	// 姝讳骸闊虫晥aa
	public string deathAudio = null;
	
	//鍑虹敓闊虫晥 鐗规晥 鍔ㄤ綔鏄?惁鏈夋晥aa
	public bool   bornValid = true;
	
	// 鏄?惁浣跨敤AI
	public bool	  useAI		=	true;

	// 鍙?敜鏍囪?aa
	protected bool mIsSummoned = false;
	public bool IsSummoned
	{
		get { return mIsSummoned;}	
	}
		
	// 琛鏉?a
	protected GameObject mHpUI;				//< 琛鏉?Iaa
	protected GameObject mHpSprite;			//< 琛鏉?I鍐呴儴瀵硅薄aa
	protected GameObject mHpDullRed;		//< 琛鏉?I鍐呴儴瀵硅薄aa
	protected UIAnchor mHpAnchor;			//< 琛鏉?I鍐呴儴瀵硅薄aa
	protected bool mHpUIActive = false;		//< 琛鏉?I鏄?惁琚?縺娲绘樉绀篴a
	protected Vector2 mHpDimention;

	// 鎺夎惤aa
	protected SDMonsterDrop mDropInfo = null;
	public SDMonsterDrop DropInfo
	{
		set { mDropInfo = value;}
		get { return mDropInfo;}	
	}
	
	// 鍦烘櫙aa
	protected sdTuiTuLogic mTuituLogic;		//< 鎺ㄥ浘閫昏緫aa
	protected Camera mMainCamera;			//< 涓荤浉鏈圭鐢ㄤ簬璁＄畻琛鏉犺aa
	
	// 鍗婇廰a
	protected List<Shader> mOriginShaders = new List<Shader>();
	protected bool mBlendMode = false;
	protected float mCurrentAlpha = 0.0f;
	protected bool mDisappeared = false;	//< 瑙掕壊鏄?惁闇瑕佹秷澶盿a	
	protected float mDisapearTime = 3.0f;	//< 瑙掕壊娑堝け鏃堕棿aa

	// 鏍囪?鏄?惁鍛楀垵濮嬪寲aa
	protected bool mInitizlized = false;
	public bool Initialized
	{
		get { return mInitizlized;}	
	}

	// 鏍囪?鏄?惁鏄??娆″彫鍞?a
	protected bool mIsFirstSummoned = true; 

	// 棣栨?琚?彫鍞ゆ椂鐨勪綅缃產a
	protected Vector3 mFirstSummonedPosition;
	public Vector3 FirstSummonedPosition
	{
		get { return mFirstSummonedPosition;}
	}

	// 棣栨?琚?彫鍞ゆ椂鐨勫Э鎬乤a
	protected Quaternion mFirstSummonedRotation;
	public Quaternion FirstSummonedRotation
	{
		get { return mFirstSummonedRotation;}
	}
	
	//
	protected override void Awake()
	{
		base.Awake();
        if (deathAudio != null && deathAudio.Length != 0)
        {
            sdResourceMgr.Instance.PreLoadResource("Music/" + deathAudio);
        }
	}
	
	//
	protected override	void Start () 
	{
		base.Start();
		
		// 鑾峰彇妯″瀷鍙婃帶鍒跺櫒淇℃伅aa
		GameObject rootRenderNode = transform.FindChild("@RenderNode").gameObject;
		gatherRenderNodesInfo(rootRenderNode);
		gatherMeshes(rootRenderNode);
		GatherMeshShaders();
		renderNode	=	rootRenderNode;
		renderNode.SetActive(false);
		
		for (int k = 0; k < renderNode.transform.childCount; ++k)
		{
			Transform kChildNode = renderNode.transform.GetChild(k);
			if( kChildNode.name.Equals("Bip01"))
			{
				boneRoot = kChildNode.gameObject;
				break;
			}
		}

		mAnimController = renderNode.GetComponent<Animation>();
//		mAnimController.enabled = false;
		
		mMotionController = this.gameObject.GetComponent<CharacterController>();		
		mMotionController.enabled = false;
		
		mNavAgent = this.gameObject.GetComponent<NavMeshAgent>();
		mNavAgent.enabled = false;

		mSelfAudioSource = this.gameObject.GetComponent<AudioSource>();
//		mSelfAudioSource.enabled = false;
			
		// 鍦烘櫙淇℃伅aa
		mMainCamera = sdGameLevel.instance.mainCamera.GetComponent<Camera>();
		mTuituLogic = sdGameLevel.instance.tuiTuLogic;
			
		// 鍔犺浇鎬?墿灞炴ц〃(鍩虹?灞炴﹁aa
		if (ActorType.AT_Pet == GetActorType())
		{	
			Hashtable kTable = sdNewPetMgr.Instance.GetPetPropertyFromDBID(mDBID);
			Property = sdConfDataMgr.CloneHashTable(kTable);
		}
		else
		{
			Hashtable kMonsterProperty = sdConfDataMgr.Instance().GetTable("MonsterProperty");
			if(Application.platform == RuntimePlatform.WindowsEditor)
			{
				if(!kMonsterProperty.ContainsKey(templateId))
				{
					Debug.LogError("monster templateID error="+templateId);
				}
			}
			Hashtable kTable = kMonsterProperty[templateId] as Hashtable;
			Property = sdConfDataMgr.CloneHashTable(kTable);

			//娣辨笂BOSS闇瑕佺壒娈婂?鐞嗚?閲庬.
            if (isLapBoss && !sdGameLevel.instance.testMode)
            {
				if (sdActGameMgr.Instance.m_uuLapBossLastBlood>0)
                	Property["HP"] = (int)sdActGameMgr.Instance.m_uuLapBossLastBlood;
				else
					Property["HP"] = (int)sdActGameMgr.Instance.m_uuWorldBossLastBlood;
            }
            else
            {
                Property["HP"] = Property["MaxHP"];	//< 淇??琛閲庣鍙?湁鎬?墿闇瑕佷慨姝㈣aa
            }
		}

		Property["MaxSP"] = 100;			//< 淇??鏈楂樻硶鍔涘糰a
		Property["SP"] = Property["MaxSP"];	//< 淇??娉曞姏鍊糰a
		
		// 鍒濆?鍖栬?鑹叉秷澶辨椂闂碼a
		mDisappeared = false;
		mDisapearTime = (int)Property["DisappearTime"] / 1000.0f;
		
		//
		m_summonInfo = sdConfDataMgr.CloneHashTable(sdConfDataMgr.Instance().m_BaseSummon);
		m_SkillEffect = sdConfDataMgr.CloneHashTable(sdConfDataMgr.Instance().m_BaseSkillEffect);
		
		// 鍒濆?鍖栬?鏉?a
		LoadHPUI();

		// 鍒濆?鍖栭変腑鐩?爣鐗规晥aa
		//if (GetActorType() == ActorType.AT_Pet)
		//	LoadTargetEffect();
		
		// 鍒涘缓鎶鑳絘a
		CreateSkill_Action((ulong)templateId);
		
		// 鍒濆?鍖栦釜浣揂I鍙傛暟aa
		if (ActorType.AT_Pet == GetActorType())
		{
            if (mAnimController!=null)
                mAnimController.cullingType = AnimationCullingType.AlwaysAnimate;

			PetAutoFight kPetAutoFight = new PetAutoFight();
			kPetAutoFight.FollowDistance = ((int)Property["FollowDistance"]) / 1000.0f;
			kPetAutoFight.BattleFollowDistance =  ((int)Property["BattleFollowDistance"]) / 1000.0f;
			kPetAutoFight.EyeDistance = ((int)Property["EyeSize"]) / 1000.0f;
			kPetAutoFight.Enable = useAI;

			kPetAutoFight.AutoRetreat = (int)(Property["Retreat"]) != 0;
			kPetAutoFight.RetreatDetectInterval = ((int)Property["RetreatDetectInterval"]) / 1000.0f;
			kPetAutoFight.RetreatDetectMinDistance = ((int)Property["RetreatDetectMinDistance"]) / 1000.0f;
			kPetAutoFight.RetreatElapseTime = ((int)Property["RetreatElapseTime"]) / 1000.0f;

			kPetAutoFight.NotifyChangeTarget += NotifyBattleSystemChangeTarget;

			sdSkill kSkill = skillTree.getSkill(1001);
			if (kSkill != null)
			{
				sdBaseState kState = kSkill.actionStateList[0];
				int iCastDistance = (int)kState.stateData["CastDistance"];
				kPetAutoFight.BattleDistance = (iCastDistance) * 0.001f;
			}
			
			mAutoFight = kPetAutoFight;
			mAutoFight.SetActor(this);
		}
		else 
		{
			MonsterAutoFight kMonsterAutoFight = new MonsterAutoFight();
			kMonsterAutoFight.EyeDistance = ((int)Property["EyeSize"]) / 1000.0f;
			kMonsterAutoFight.ChaseDistance = ((int)Property["ChaseSize"]) / 1000.0f;
			kMonsterAutoFight.Enable = useAI;

			kMonsterAutoFight.NotifyChangeTarget += NotifyBattleSystemChangeTarget;

			sdSkill kSkill = skillTree.getSkill(1001);
			if (kSkill != null)
			{
				sdBaseState kState = kSkill.actionStateList[0];
				int iCastDistance = (int)kState.stateData["CastDistance"];
				kMonsterAutoFight.BattleDistance = (iCastDistance) * 0.001f;
			}

			mAutoFight = kMonsterAutoFight;
			mAutoFight.SetActor(this);
		}

		// 鍒濆?鍖栫兢浣揂I鍙傛暟aa
		if (ActorType.AT_Pet == GetActorType())
		{
			sdBehaviourNode kBehaviourNode = sdBehaviourManager.GetSingleton().GetBehaviourNode(1);
			if (kBehaviourNode != null)
				kBehaviourNode.AddActor(this);
		}
		
		//
		LoadShadow();
	}

	protected override void Update ()
	{
		base.Update ();
		
		// 澶勭悊鍑虹敓aa
		if (bornLive && (!mIsSummoned) && (GetCurrentHP() > 0))
		{
			bornLive = false;

			mIsSummoned = true;
			MonsterSummoned();
		}
		
		UpdateHittedLight();	//< 鍙楀嚮棰滆壊闂?櫧aaa
		UpdateHPUIPos();		//< 鏇存柊琛鏉?a
		
		// 鏇存柊鑷?姩鎴樻枟aa
        if (actorType != ActorType.AT_Static)
        {
            if (IsActive() && GetCurrentHP() > 0)
                mAutoFight.Update();
        }
		
		// 澶勭悊琚?潃姝绘秷澶盿a
		if (GetCurrentHP() <= 0)
		{
			mDisapearTime -= Time.deltaTime;
			if (mDisapearTime < 0.0f)
			{
				mDisapearTime = 0.0f;
				mDisappeared = true;
			}
		}
	
		// 澶勭悊鍗婇忔槑aa
		if (mIsSummoned)
		{		
			if (!mDisappeared)
			{
//				// 褰撳墠澶勪簬澶嶆椿鐘舵?瑙掕壊浠庡崐閫忓埌瀹屽叏涓嶉廰a
//				if (mCurrentAlpha < 1.0f)
//				{
//					mCurrentAlpha += Time.deltaTime;
//					if (mCurrentAlpha > 1.0f)
//						mCurrentAlpha = 1.0f;
//				
//					SetAlpha(mCurrentAlpha);
//				}
			}
			else
			{
				// 褰撳墠澶勪簬姝讳骸鐘舵?瑙掕壊浠庡崐閫忓埌閫忔槑aa
				if (mCurrentAlpha > 0.0f)
				{
					mCurrentAlpha -= Time.deltaTime;
					if (mCurrentAlpha < 0.0f)
						mCurrentAlpha = 0.0f;

					SetAlpha(mCurrentAlpha);

					if (mCurrentAlpha <= 0.0f)
					{
						mIsSummoned = false;
						renderNode.SetActive(false);
					}
				}
			}
		}
		
		//
		tickFrame();
		
		mInitizlized = true;
	}
	
	private int test_tick = 0;
	void tickFrame()
	{
		if (!mIsSummoned)
			return;
		
		if (skillTree!=null)
			skillTree.tick();
		
			if (logicTSM != null && renderNode != null )
			{
				// 蹇呴』澶氳窇涓涓?惊鐜?鍑嗗?濂藉姩鐢绘墠琛宎aa
				if(test_tick > 1)
					logicTSM.UpdateSelf();
				++test_tick;
			}
		
	}
	
	// 閿姣佸洖璋傜缁ф壙鑷狹onoBehaviour)aa
	protected override void OnDestroy()
	{
		base.OnDestroy();

		DestroyHPUI();
	}

	// 瑙掕壊鏄?惁婵娲虹缁ф壙鑷猻dActorInterface)aa
	public override	bool IsActive()
	{
		return mIsSummoned;
	}

	// 瑙掕壊鍗婂緞(缁ф壙鑷猻dActorInterface)aa
	public override	float getRadius()
	{
		return mMotionController.radius;
	}
    // 瑙掕壊纰版挒浣撻珮搴ョ缁ф壙鑷猻dActorInterface)aa
    public override float getHeight()
    {
        return mMotionController.height;
    }
	
	// 鑾峰彇鍩虹?灞炴ц〃(缁ф壙鑷猻dBufferReceiver)aa
	public override Hashtable GetBaseProperty ()
	{
		if (ActorType.AT_Pet == GetActorType())
		{	
			Hashtable kTable = sdNewPetMgr.Instance.GetPetPropertyFromDBID(mDBID);
			return kTable;
		}
		else
		{
			Hashtable kMonsterProperty = sdConfDataMgr.Instance().GetTable("MonsterProperty");
			Hashtable kTable = kMonsterProperty[templateId] as Hashtable;
			return kTable;
		}
	}
	
	// 鏀堕泦Mesh鐨凷haderaa
	protected void GatherMeshShaders()
	{
		if (meshTable == null)
			return;
		
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for (int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
					mOriginShaders.Add(kObjectRenderer.materials[k].shader);
			}
		}
	}
	
	// 鍔犺浇鎶鑳藉姩鐢昏〃aa
	//	1.琚?姩Action琛╝a
	//	2.鎶鑳戒俊鎭?〃aa
	//	3.鎶鑳紸ction琛╝a
	protected void CreateSkill_Action(ulong ulTemplateID)
	{
		{
				sdBaseState kPassiveDeathState = new sdBaseState();
				kPassiveDeathState.id		= 100;
				kPassiveDeathState.info		= "death01";
				kPassiveDeathState.name		= "death01";
				kPassiveDeathState.bPassive	= true;
		
				sdBaseState kPassiveIdleState = new sdBaseState();
				kPassiveIdleState.id		= 101;
				kPassiveIdleState.info		= "idle01";
				kPassiveIdleState.name		= "idle01";
				kPassiveIdleState.bPassive	= true;
		
				sdBaseState kPassiveKnockState = new sdBaseState();
				kPassiveKnockState.id		= 102;
				kPassiveKnockState.info		= "death01";
				kPassiveKnockState.name		= "death01";
				kPassiveKnockState.bPassive	= true;
		
				sdBaseState kPassiveWalkState = new sdBaseState();
				kPassiveWalkState.id		= 103;
				kPassiveWalkState.info		= "walk01";
				kPassiveWalkState.name		= "walk01";
				kPassiveWalkState.bPassive	= true;
			
				sdBaseState kPassiveStunState = new sdBaseState();
				kPassiveStunState.id		= 104;
				kPassiveStunState.info		= "stun01";
				kPassiveStunState.name		= "stun01";
				kPassiveStunState.bPassive	= true;
			
				logicTSM.AddActionState(kPassiveDeathState);	
				logicTSM.AddActionState(kPassiveIdleState);
				logicTSM.AddActionState(kPassiveKnockState);
				logicTSM.AddActionState(kPassiveWalkState);
				logicTSM.AddActionState(kPassiveStunState);
		}
		

		skillTree = new sdSkillTree();

		// 鏅?氭敾鍑绘妧鑳絘a
		{
			int iDfSkillId = (int)Property["DfSkill"];	
			
			Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[iDfSkillId] as Hashtable;
			Hashtable kSkillAction = sdConfDataMgr.Instance().m_MonsterSkillAction[iDfSkillId] as Hashtable;
			if (kSkillInfo != null)
			{
				int iTemplateId = (int)kSkillInfo["dwTemplateID"];
				int iPassive = (int)kSkillInfo["byIsPassive"];
				string name	= kSkillInfo["strName"] as string;
				
				sdSkill kSkill = new sdSkill(name, iPassive==1, iTemplateId);
				skillTree.Add(kSkill);
				kSkill.skillProperty	=	kSkillInfo;
				if (kSkillAction != null)
				{
					sdBaseState kState = kSkill.AddAction(iDfSkillId, kSkillAction);
					logicTSM.AddActionState(kState);
					kState.SetInfo(kSkillAction, this);
				}
			}
		}

		// 瀹犵墿澶ф嫑aa
		if (GetActorType() == ActorType.AT_Pet)
		{	
			int iSpSkillId = (int)Property["SpSkill"];	
			
			Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[iSpSkillId] as Hashtable;
			Hashtable kSkillAction = sdConfDataMgr.Instance().m_MonsterSkillAction[iSpSkillId] as Hashtable;
			if (kSkillInfo != null)
			{
				int iTemplateId = (int)kSkillInfo["dwTemplateID"];
				int iPassive = (int)kSkillInfo["byIsPassive"];
				string name	= kSkillInfo["strName"] as string;
				
				sdSkill kSkill = new sdSkill(name, iPassive==1, iSpSkillId);
				skillTree.Add(kSkill);
				kSkill.skillProperty = kSkillInfo;
				if (kSkillAction != null)
				{
					sdBaseState kState = kSkill.AddAction(iSpSkillId, kSkillAction);
					logicTSM.AddActionState(kState);
					kState.SetInfo(kSkillAction, this);
				}
			}
		}
		
		// 瀹犵墿鍜屾?墿瑙﹀彂鎶鑳絘a
		{
			int iSkillNum = 10;
			if (GetActorType() == ActorType.AT_Pet)
				iSkillNum = 4;

			for (int iIndex = 1; iIndex <= iSkillNum; ++iIndex)
			{
				string kSkillKey = "Skill" + iIndex.ToString();
				int iSkillId = (int)Property[kSkillKey];
				if (iSkillId > 0)
				{
					Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[iSkillId] as Hashtable;
					Hashtable kSkillAction = sdConfDataMgr.Instance().m_MonsterSkillAction[iSkillId] as Hashtable;
					if (kSkillInfo != null)
					{
						int iTemplateId = (int)kSkillInfo["dwTemplateID"];
						int iPassive = (int)kSkillInfo["byIsPassive"];
						string name	= kSkillInfo["strName"] as string;
						
						sdSkill kSkill = new sdSkill(name, iPassive==1, iSkillId);
						skillTree.Add(kSkill);
						kSkill.skillProperty = kSkillInfo;
						if (kSkillAction != null)
						{
							sdBaseState kState = kSkill.AddAction(iSkillId, kSkillAction);
							logicTSM.AddActionState(kState);
							kState.SetInfo(kSkillAction, this);
						}
					}	
				}
			}
		}
	}
	
	// 浼ゅ?鍥炶皟(缁ф壙鑷猻dActorInterface)aa
	public override	void OnHurt(DamageResult dr)
	{
		if (mAutoFight != null)
			mAutoFight.OnHurt(dr.attracker);

		base.OnHurt(dr);
	}
	
	// 鍔犺?鎺夎?(缁ф壙鑷猻dActorInterface)aa
	//	1.璋冩暣琛閲廰a
	//	2.鏄剧ず姘旀场aa
	//	3.婵娲昏?鏉℃樉绀篴a
	// 	4.鍑绘潃澶勭悊aa
	public override	void AddHP(int iHP)
	{
		base.AddHP(iHP);
		
		// 鍥炶?鍐掓场aa
		if (iHP > 0)	
		{
			Vector3 kBubblePos = this.transform.position;
			kBubblePos.y += 2.0f;
			
			Bubble.AddHp(iHP, kBubblePos, false);

		}
		
		// 婵娲昏?鏉犽閲嶆柊鍙?敜aa
		if (GetCurrentHP() > 0)
		{
			if (!mHpUIActive)
				ActiveHPUI();

			if (!mIsSummoned)
			{
				mIsSummoned = true;
				MonsterSummoned();
			}
		}
		
		// 澶勭悊鎬?墿琚?潃姝籥a
		if (GetCurrentHP() <= 0)
		{		
			MonsterKilled();
		}
	}
	
	// 瑙﹀彂(缁ф壙鑷猻dTriggerReceiver)aa
	public override void OnTriggerHitted (GameObject kObject, int[] kParam)
	{
		base.OnTriggerHitted(kObject, kParam);

		// 鏉姝昏嚜宸盿a
		if (kParam[3] == 1)
		{
			SetCurrentHP(-1);
			AddHP(0);

			return;
		}
		
		if (mIsSummoned || (GetCurrentHP() <= 0))
			return;
		
		mIsSummoned = true;
		MonsterSummoned();	

		sdMovieScene movieScene = gameObject.GetComponent<sdMovieScene>();
		if(movieScene != null)
		{
			sdLevelArea area = transform.parent.gameObject.GetComponent<sdLevelArea>();
			if(area != null)
				movieScene.SetTarget(gameObject, area);
		}
	}
	
	// 瑙﹀彂(缁ф壙鑷猻dBaseTrigger)aa
	protected override void WhenEnterTrigger(GameObject obj,int[] param)
	{
		base.WhenEnterTrigger (obj,param);
		
		for (int i = 0; i < enterReceivers.Length; i++)
		{
			if (enterReceivers[i] != null)
			{
				enterReceivers[i].OnTriggerHitted(
					gameObject,
					iEnterParams[i].v);
			}
		}
	}
	
	// 瑙﹀彂(缁ф壙鑷猻dBaseTrigger)aa
	protected override void WhenLeaveTrigger(GameObject obj,int[] param)
	{
		base.WhenLeaveTrigger (obj,param);
		
		for (int i = 0; i < leaveReceivers.Length; i++)
		{
			if (leaveReceivers[i] != null)
			{
				leaveReceivers[i].OnTriggerHitted(
					gameObject,
					iLeaveParams[i].v);
			}
		}
	}
	
	// 鑾峰彇鎬?墿鍚嶇Оaa
	public string GetName()
	{
		return (string)Property["Name"];
	}
	
	// 鑾峰彇鎬?墿鑳藉姏aa
	public HeaderProto.EMonsterAbility GetAbility()
	{
		return (HeaderProto.EMonsterAbility)(int)Property["Ability"];
	}
	
	// 鑾峰彇鎬?墿鍙嶅嚮浼樺厛绾?a
	public EBeatPriority GetBeatPriority()
	{
		return (EBeatPriority)Property["BeatPriority"];
	}

	// 瑙嗛噹鑼冨洿(鍗曚綅m)aa
	public float GetViewDistance()
	{
		return (int)Property["EyeSize"] / 1000.0f;	//< 鍝堝笇琛ㄩ噷闈㈠崟浣嶆槸mmaa
	}
	
	// 璁剧疆鏈澶ц?閲廰a
	public void SetMaxHP(int iMaxHP)
	{
	 	Property["MaxHP"] = iMaxHP;
	}
	
	// 璁剧疆鍒濆?琛閲廰a
	public void SetCurrentHP(int iHP)
	{
	 	Property["HP"] = iHP;
	}
	
	// 琛鏉℃暟閲廰a
	public int GetHPBarNum()
	{
		return (int)Property["HPBarNum"];
	}
	
	// 琛鏉＄浉瀵硅?鑹叉寕鎺ョ偣楂樺害(鍗曚綅m)aa
	public float GetHPBarHeight()
	{
		return (int)Property["HPBarHeight"] / 1000.0f;	//< 鍝堝笇琛ㄩ噷闈㈠崟浣嶆槸mmaa
	}
	
	// 鍒濆?鍖栬?鏉＄晫闈?a
	protected void LoadHPUI()
	{
		ActorType eActorType = GetActorType();
		if (eActorType == ActorType.AT_Pet)
		{
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "hpUI";
			sdResourceMgr.Instance.LoadResource("UI/$FightUI/HPBar_Pet.prefab", LoadUIObj, kParam);	
		}
		else
		{
			int eMonsterAbility = (int)GetAbility();
			if (eMonsterAbility < (int)HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss)
			{
				ResLoadParams kParam = new ResLoadParams();
				kParam.info = "hpUI";
				sdResourceMgr.Instance.LoadResource("UI/$FightUI/HPBar_Jun.prefab", LoadUIObj, kParam);
			}	
		}
	}
	
	// 婵娲昏?鏉?a
	public void ActiveHPUI()
	{
		if (mHpUIActive)
			return;
		
		ActorType eActorType = GetActorType();
		int eMonsterAbility = (int)GetAbility();	
		if ((eMonsterAbility < (int)HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss) || (eActorType == ActorType.AT_Pet))
			mHpDimention = new Vector2(120.0f,11.0f);
		else
			mHpDimention = new Vector2(487.0f,29.0f);
		
		if (mHpUI != null)
		{
			mHpUI.SetActive(true);
			mHpUIActive = true;
		}
	}
	
	// 闅愯棌琛鏉?a
	public void DeactiveHPUI()
	{
		if (!mHpUIActive)
			return;	
		
		if (mHpUI != null)
		{
			mHpUI.SetActive(false);
			mHpUIActive = false;
		}
	}
	
	// 閿姣佽?鏉?a
	protected void DestroyHPUI()
	{
		if (mHpUI != null)
		{
			GameObject.Destroy(mHpUI);
			
			mHpUI = null;
			mHpSprite = null;
			mHpDullRed = null;
			mHpAnchor = null;
			mHpUIActive = false;
		}
	}
	
	// 鏇存柊琛鏉?a
	protected void UpdateHPUIPos()
	{
		if (!mHpUIActive)
			return;
		
		Vector3 kPos = this.transform.position;
		kPos.y += GetHPBarHeight();

		Vector3 kViewPos = mMainCamera.WorldToViewportPoint(kPos);
		float fHpScale = 1.0f;	
		ActorType eActorType = GetActorType();
		int eMonsterAbility = (int)GetAbility();	
		if ((eMonsterAbility < (int)HeaderProto.EMonsterAbility.MONSTER_ABILITY_boss) || (eActorType == ActorType.AT_Pet))
		{
			if (GetMaxHP() > 0)
			{
				fHpScale = (float)GetCurrentHP() / (float)(int)Property["MaxHP"];
				mHpSprite.transform.localScale = new Vector3(fHpScale, mHpUI.transform.localScale.y, mHpUI.transform.localScale.z);
			}
		
			float fRelX = - mHpDimention.x * (1.0f- fHpScale) * 0.5f;
			mHpAnchor.relativeOffset = new Vector2(kViewPos.x, kViewPos.y);
			mHpSprite.transform.localPosition = new Vector3(fRelX, 0.0f, 0.0f);
		}
//		else
//		{
//			if (GetMaxHP() > 0)
//			{
//				int iSubHP = GetMaxHP() / GetHPBarNum();
//				int iBarNum = GetCurrentHP() / iSubHP;
//
//				if (GetCurrentHP() == GetMaxHP())
//					iBarNum -= 1;
//	
//				if (iBarNum == 0)
//					mHpDullRed.SetActive(false);
//				
//				int iSubCurrentHP = GetCurrentHP() - iSubHP * iBarNum;
//				fHpScale = (float)iSubCurrentHP/(float)iSubHP;
//				mHpSprite.transform.localScale = new Vector3(fHpScale, mHpUI.transform.localScale.y, mHpUI.transform.localScale.z);
//			}
//
//			float fRelX = - mHpDimention.x * (1.0f- fHpScale) * 0.5f;
//			mHpAnchor.relativeOffset = new Vector2(kViewPos.x, kViewPos.y);
//			mHpSprite.transform.localPosition = new Vector3(fRelX, 0.0f, 0.0f);
//     }
	}
	
	// UI鍔犺浇鍥炶皟aa
	protected void LoadUIObj(ResLoadParams kParam, Object kObj)
	{
		if (kParam.info == "hpUI")
		{
			if (mHpUI == null)
			{
				mHpUI = GameObject.Instantiate(kObj) as GameObject;
		
				if(mTuituLogic.uiPanel != null)
				{
					mHpUI.transform.parent = mTuituLogic.uiPanel.transform;
					mHpUI.transform.localPosition = Vector3.zero;
					mHpUI.transform.localRotation = Quaternion.identity;
					mHpUI.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
				}
				
				mHpAnchor = mHpUI.GetComponent<UIAnchor>();
				mHpSprite = mHpUI.transform.FindChild("Sprite").gameObject;
				mHpUI.SetActive(false);
				
				ActorType eActorType = GetActorType();
				if (eActorType == ActorType.AT_Pet)
					ActiveHPUI();
			}
		}
	}

	// 璁剧疆閫忔槑搴?a
	protected void SetAlpha(float fAlpha)
	{
		if (!mIsSummoned)
			return;
		
		if (fAlpha < 0.999f)
		{
			TurnToBlendShader();
			SetShadowEnable(false);
		}
		else
		{
			ReturnToOriginShader();
			SetShadowEnable(true);

			return;
		}
		
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for(int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				Color kSourceColor = kObjectRenderer.materials[k].GetColor("_Color");
				kSourceColor.a = fAlpha;
				
				kObjectRenderer.materials[k].SetColor("_Color", kSourceColor);
			}
		}
	}
	
	//
	public void TurnToBlendShader()
	{
		if (mBlendMode)
			return;
		
		mBlendMode = true;

		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for(int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
					kObjectRenderer.materials[k].shader = mTuituLogic.GetBlendShader();
			}
		}
	}
	
	// 鏇挎崲鍥炲師鏈夌潃鑹插櫒,骞惰?缃?负涓嶉忔槑aa
	protected void ReturnToOriginShader()
	{
		if (!mBlendMode)
			return;
		
		mBlendMode = false;
		
		int iCount = 0;
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			for(int k = 0; k < kObjectRenderer.materials.Length; k++)
			{
				if (kObjectRenderer.materials[k] != null)
				{
					kObjectRenderer.materials[k].shader = mOriginShaders[iCount++];

					Color kSourceColor = kObjectRenderer.materials[k].GetColor("_Color");
					kSourceColor.a = 1.0f;	
					kObjectRenderer.materials[k].SetColor("_Color", kSourceColor);
				}
			}
		}
	}
	
	// 
	protected void SetShadowEnable(bool bEnable)
	{
		foreach(DictionaryEntry kEntry in meshTable)
		{
			GameObject kObject = kEntry.Value as GameObject;
			Renderer kObjectRenderer = kObject.GetComponent<Renderer>();
			kObjectRenderer.castShadows = bEnable;
			kObjectRenderer.receiveShadows = false;
			//kObjectRenderer.receiveShadows = bEnable;
		}
	}
	
	//
	protected Transform FindBone(Transform kParent, string kName)
	{
		Transform kTransform = kParent.Find(kName);
		if (kTransform != null)
			return kTransform;
		
		for (int i = 0; i < kParent.childCount; i++)
		{
			kTransform = FindBone(kParent.GetChild(i), kName);
			if (kTransform != null)
				return kTransform;
		}
		
		return null;
	}
	
	// 琚?彫鍞ゅ洖璋僡a
	protected void MonsterSummoned()
	{
		WhenEnterTrigger(gameObject,new int[4]{0,0,0,0});
		
		//
		if (NotifySummoned != null)
			NotifySummoned(this);
		
		// 
		renderNode.SetActive(true);			//< 鎵撳紑娓叉煋鑺傜偣aa

		//
		mMotionController.enabled = true;	//< 鎵撳紑鍔ㄤ綔鎺у埗鍣╝a
		
		// 閫忔槑搴?a
		mDisappeared = false;
		mCurrentAlpha = 1.0f;
		SetAlpha(mCurrentAlpha);

		// 璁板綍棣栨?琚?彫鍞ょ殑浣嶇疆鍜屽Э鎬乤a
		if (mIsFirstSummoned)
		{
			mIsFirstSummoned = false;
			mFirstSummonedPosition = this.gameObject.transform.position;
			mFirstSummonedRotation = this.gameObject.transform.rotation;
		}
		
		// 鍑虹敓闊虫晥\鐗规晥\鍔ㄧ敾aa
		if (bornValid)
		{
			if(bornAinm != null && bornAinm.Length != 0)
			{
				AnimationState state = AnimController[bornAinm];
				if(state != null)
				{
					//Debug.Log(gameObject.name + "\n");
					state.layer = 20;
					//state.speed = 0.5f;
					state.wrapMode = WrapMode.Once;
					AnimController.Play(bornAinm,PlayMode.StopAll);
				}
			}
			if(bornEffect != null && bornEffect.Length != 0)
				attachEffect(bornEffect, "gamelevel", transform.localPosition, transform.localRotation, 1.0f, 5.0f);
			if(bornAudio != null && bornAudio.Length != 0)
				PlayAudio(bornAudio);
		}
	}
	
	// 琚?潃姝诲洖璋僡a
	protected void MonsterKilled()
	{
		// 娓呴櫎Buffaa
		ClearBuff();

		// 绂诲紑瑙﹀彂鍣╝a
		WhenLeaveTrigger(gameObject,new int[4]{0,0,0,0});
		
        //// 閫氱煡姝讳骸娑堟伅aa
        //if (NotifyKilled != null)
        //    NotifyKilled(this);

		//
		mMotionController.enabled = false;
			
		// 澶勭悊琛鏉?a
		DeactiveHPUI();

		// 澶勭悊閫変腑鍏夊湀aa
		if (GetActorType() == ActorType.AT_Pet)
			DeactiveTargetEffect();

		// 澶勭悊寤惰繜娑堝けaa
		mDisapearTime = (int)Property["DisappearTime"] / 1000.0f;	
		mDisappeared = false;
		
		// 鎺夎惤aa
		if (mDropInfo != null)
		{
            sdResourceMgr.Instance.AddDropInfo(this);
		}
		
		// 姝讳骸闊虫晥aaa
		if (deathAudio != null && deathAudio.Length != 0)
			PlayAudio(deathAudio);

        AnimationState dieAnim  =   AnimController[logicTSM.die.info];
        if (dieAnim != null)
        {
            mDisapearTime = dieAnim.length;
        }

        UnInject(true);
	}
	
	
	
	void	CheckUpdateAnimation()
	{
		sdMainChar c	=	sdGameLevel.instance.mainChar;
		if(c!=null)
		{
			if(!renderNode.GetComponent<Animation>().enabled)
			{
				float	fDis	=	Vector3.Distance(c.transform.position,transform.position);
				if(fDis<15.0f)
				{
					renderNode.GetComponent<Animation>().enabled	=	true;
				}
			}
			else
			{
				float	fDis	=	Vector3.Distance(c.transform.position,transform.position);
				if(fDis>15.0f)
				{
					renderNode.GetComponent<Animation>().enabled	=	false;
				}
			}
		}
	}
	public override void DoHittedAudio(string strWeaponType)
	{
		if(strWeaponType == null || strWeaponType.Length == 0)
			return;
		Hashtable  hitAudioTable = sdConfDataMgr.Instance().GetTable("hitsound");
		if(hitAudioTable == null)
			return;
		Hashtable  table = hitAudioTable[strWeaponType] as Hashtable;
		if(table == null)
			return;

		string type = armType.ToString();		
		string strPath = table[type] as string;	
		string[] fileName = strPath.Split(new char[]{';'});
		if(fileName.Length == 0)
			return;
		int index = Random.Range(0, fileName.Length);

		string typePath;
		if( strWeaponType=="poison" || strWeaponType=="ice" || strWeaponType=="fire" || strWeaponType=="lightning" )
			typePath = "hit_sound/$element_sound/";
		else
			typePath = "hit_sound/$" + strWeaponType + "_sound/";

		PlayAudio(typePath + fileName[index]);
	}

	// 鎬?墿缇ゆ垚鍛樺彈浼ゅ?鎴栬呭彂鐜扮?涓涓?洰鏍囧洖璋僡a
	public void OnAreaGroupAlert(sdActorInterface kAttacker)
	{
		if (GetActorType() == ActorType.AT_Monster)
		{
			MonsterAutoFight kMonsterAutoFight = mAutoFight as MonsterAutoFight;
			kMonsterAutoFight.OnFriendHurt(kAttacker);	//< 鏀诲嚮缇ゆ垚鍛樿?浣滄敾鍑昏嚜宸半缇ゆ垚鍛樺彂鐜扮洰鏍囪?浣滆嚜宸卞彂鐜癮a
		}
	}

	// 鎸傛帴鍒扮兢浣揂I鑺傜偣鍥炶皟(缁ф壙鑷猻dGameActor)aa
	public override void OnAddToBehaviourNode(sdBehaviourNode kBehaviourNode)
	{
//		kBehaviourNode.NotifyMemberChangeTarget += this.NotifyBehaviourChangeTarget;
		kBehaviourNode.NotifyMemberHurt += this.NotifyBehaviourNodeHurt;
	}

	// 浠庣兢浣揂I鑺傜偣鍙栨秷鎸傛帴鍥炶皟(缁ф壙鑷猻dGameActor)aa
	public override void OnRemoveFromBehaviourNode(sdBehaviourNode kBehaviourNode)
	{
//		kBehaviourNode.NotifyMemberChangeTarget -= this.NotifyBehaviourChangeTarget;
		kBehaviourNode.NotifyMemberHurt -= this.NotifyBehaviourNodeHurt;
	}

//	// 鐩熷弸鍒囨崲鏀诲嚮鐩?爣浜嬩欢(娉ㄥ唽鍒伴昏緫鑺傜偣鐨勪簨浠跺洖璋傝aa
// 	protected void NotifyBehaviourChangeTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
// 	{
// 		if (kActor != this)
// 		{
// 		    if (kPreviousTarget == null && kTarget != null)
// 		    {
// 				if (mAutoFight != null)
// 				{
// 					mAutoFight.OnFriendHurt(kTarget);	//< 鐩熷弸鍙戠幇鐩?爣瑙嗕綔鑷?繁鍙戠幇aa
// 				}
// 			}
// 		}
// 	}

	// 鐩熷弸鍙椾激瀹充簨浠电娉ㄥ唽鍒伴昏緫鑺傜偣鐨勪簨浠跺洖璋傝aa
	protected void NotifyBehaviourNodeHurt(sdActorInterface kActor, sdActorInterface kAttacker, int iHP)
	{
		if (kActor != this)
		{
			if (mAutoFight != null)
			{
				mAutoFight.OnFriendHurt(kAttacker);		//< 鏀诲嚮鐩熷弸瑙嗕綔鏀诲嚮鑷?繁aa
			}
		}
	}
	
	// 鎴樻枟绯荤粺鍒囨崲鏀诲嚮鐩?爣浜嬩欢(娉ㄥ唽鍒版垬鏂楃郴缁熺殑浜嬩欢鍥炶皟)aa
	protected void NotifyBattleSystemChangeTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		if (NotifyChangeTarget != null)
			NotifyChangeTarget(kActor, kPreviousTarget, kTarget);

		if (GetActorType() == ActorType.AT_Pet)
		{
			mTargetEffectTarget = kTarget;

			if (kTarget == null)
			{
				if (mTargetEffect != null)
					mTargetEffect.SetActive(false);
			}
		}
	}
	
	// 瀹犵墿鎵嬪姩鎶鑳肩鐢ㄤ簬瀹犵墿)aa
	public bool DoXPSkill(ref int iErrorCode)
	{
		if (GetActorType() == ActorType.AT_Pet)
		{
			PetAutoFight kPetAutoFight = mAutoFight as PetAutoFight;
			int iSkillId = (int)Property["SpSkill"];
			if (iSkillId != 0)
			{
				return kPetAutoFight.DoXPSkill(iSkillId, ref iErrorCode);
			}
		}

		return false;
	}
	
	// 瀹犵墿涓庢?墿鐨勭?鎾濈鐢ㄤ簬瀹犵墿)aa
	public void AvtiveCollisionWithMonster(bool bEnable)
	{
		if (GetActorType() == ActorType.AT_Pet)
		{
			if (bEnable)
				this.gameObject.layer = LayerMask.NameToLayer("Pet");
			else
				this.gameObject.layer = LayerMask.NameToLayer("PetNoMonster");
		}
	}
    
}

