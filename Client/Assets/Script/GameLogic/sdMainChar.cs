using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

/// <summary>
/// 主角角色对象
/// </summary>
public class sdMainChar : sdGameActor_Impl
{
	public int			actionIndex = 0;
	public int			actionCount = 20;	
	
	// 位移同步aa
	protected bool mLastSynIsBeginPacket = false;	//< 上个同步包类型(用于确定当前是Begin还是End)
	protected float mLastSynTime = 0.0f;			//< 上个同步包发送时间(本地时间,单位为秒)aa
	protected float mLastSynOri = 0.0f;				//< 上个同步包角色朝向(单位为度)aa
	protected Vector3 mLastSynPos = Vector3.zero;	//< 上个同步包角色位置(单位为米)aa

    protected float m_fSkillEndTime = 0.0f;
    public float SkillEndTime
    {
        set { m_fSkillEndTime = value; }
        get { return m_fSkillEndTime; }
    }
	
	public override	float getRadius()
	{
		return MotionController.radius;
	}
    // 角色碰撞体高度(继承自sdActorInterface)aa
    public override float getHeight()
    {
        return MotionController.height;
    }
	// 获取角色标准属性表(继承自sdActorInterface)aa
	public override Hashtable GetBaseProperty()
	{
		return sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable;
	}

	// 初始化(继承自sdActorInterface)
	protected override void	Awake()
	{                                                                                                                                                                        
		base.Awake();
	
		// 阻挡检测aa
		mBlockDetection = true;
		mBlockDetectionTime = 3.0f;
		NotifyBlocked += NotifyFindBlocked;
	}

	// 更新(继承自sdBaseTrigger)
	protected override void Update () 
	{
		base.Update();

		tickFrame();
	}

	// 每帧更新aa
	private int test_tick = 0;
	public void tickFrame()
	{
		// 更新技能树aa
		if (skillTree != null)
			skillTree.tick();
		
		// 更新状态机aa
		if (logicTSM != null && renderNode != null )
		{
			// 必须多跑一个循环,准备好动画才行aaa
			if(test_tick > 1)
				logicTSM.UpdateSelf();
			++test_tick;
		}

		// 加载刀光
		if (mWeaponTrail == null)
			mWeaponTrail = createWeaponTrail("dummy_right_weapon_at");

		// 如果在主城则向服务器同步位移aa
		if (sdGameLevel.instance != null)
		{
			if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.MainCity)
				tickMove();
		}

		// 颜色闪白aa
		UpdateHittedLight();	
		
		// 推开其他角色aa
		PushOtherActor();
	}
	
	// 检查主角移动并向服务器发送移动同步数据aa
	//	1.在检测到移动时,每隔一定的时间发送Begin数据包aa
	//	2.在第一次检测到移动停止时,立即发送End数据包aa
	protected void tickMove()
	{
		float fMinSynMoveDeltaTime = 1.0f;
		float fMinSynStopDeltaTime = 0.1f;
		float fMinSynSqlDis = 0.1f * 0.1f;
		float fMinSynCosOri = Mathf.Cos(1.0f);
		
		float fDeltaTime = Time.time - mLastSynTime;
		float fDeltaSqlDis = (this.gameObject.transform.position - mLastSynPos).sqrMagnitude;	
		float fClimbOri = Mathf.Max(0.0f, Mathf.Min(360.0f, this.gameObject.transform.eulerAngles.y));
		float fDeltaOri = fClimbOri - mLastSynOri;
		if (fDeltaSqlDis > fMinSynSqlDis || Mathf.Cos(fDeltaOri) < fMinSynCosOri)
		{
			if (fDeltaTime > fMinSynMoveDeltaTime)
			{
				mLastSynIsBeginPacket = true;
				mLastSynTime = Time.time;
				mLastSynOri = this.gameObject.transform.eulerAngles.y;
				mLastSynPos = this.gameObject.transform.position;	
				sdActorMsg.send_CSID_SCID_MOVE_BEGIN(mLastSynPos, mLastSynOri, (ulong)(System.DateTime.Now.Ticks / 10000L));
			}
		}
		else
		{
			if (fDeltaTime > fMinSynStopDeltaTime)
			{
				if (mLastSynIsBeginPacket)
				{
					mLastSynIsBeginPacket = false;
					mLastSynTime = Time.time;
					mLastSynOri = this.gameObject.transform.eulerAngles.y;
					mLastSynPos = this.gameObject.transform.position;		
					sdActorMsg.send_CSID_SCID_MOVE_STOP(mLastSynPos, mLastSynOri, (ulong)(System.DateTime.Now.Ticks / 10000L));	
				}
			}
		}
	}

	public void ClearSuitBuff()
	{
		if (sdGlobalDatabase.Instance.globalData.ContainsKey("suitBuff"))
		{
			List<int> list = sdGlobalDatabase.Instance.globalData["suitBuff"] as List<int>;
			foreach(int id in list)
			{
				RemoveBuff(id);
			}
		}
	}

	public void AddSuitBuff()
	{
		if (sdGlobalDatabase.Instance.globalData.ContainsKey("suitBuff"))
		{
			List<int> list = sdGlobalDatabase.Instance.globalData["suitBuff"] as List<int>;
			foreach(int id in list)
			{
				AddBuff(id, 0, this);
			}
		}
	}
	
	public void RefreshProp()
	{
		//SetProperty(props);
		if (sdGlobalDatabase.Instance.globalData.ContainsKey("suitBuff"))
		{

		}

		Hashtable valueDesc = new Hashtable();
		int maxExp = sdConfDataMgr.Instance().GetLevelExp(Property["Job"].ToString(), Property["Level"].ToString());
		valueDesc["value"] = maxExp.ToString();
		sdUICharacter.Instance.SetProperty("MaxExperience", valueDesc);
		
		foreach(DictionaryEntry entry in Property)
		{
			string name = entry.Key as string;
			object val = entry.Value;				
			
			Hashtable uiValueDesc = new Hashtable();
			uiValueDesc["value"] = val;
			uiValueDesc["des"] = "";
			sdUICharacter.Instance.SetProperty(name, uiValueDesc);
		}
		
		
		string score = string.Format("{0} {1}", sdConfDataMgr.Instance().GetShowStr("Score"), sdConfDataMgr.Instance().PlayerScore());
		valueDesc["value"] = score;
		valueDesc["des"] = "";
		sdUICharacter.Instance.SetProperty("Score", valueDesc);
		
		int attdmg = int.Parse(Property["AtkDmgMin"].ToString()) + int.Parse(Property["AtkDmgMax"].ToString());
		valueDesc["value"] = (attdmg/2).ToString();
		sdUICharacter.Instance.SetProperty("AttDmg", valueDesc);
		

	}
	
	// 初始化aa
	public new bool init(sdGameActorCreateInfo kInfo)
	{
		base.init(kInfo);

		// 设置角色初始朝向aa
		mFaceDirection = transform.rotation * Vector3.forward;
		
		// 初始化技能树aa
		CreateSkill_Action(kInfo.mJob);
	
		// 初始化角色属性aa
		if(sdGlobalDatabase.Instance.globalData != null)
		{
			// 属性表
			if (Property == null)
				Property = sdConfDataMgr.CloneHashTable(sdGlobalDatabase.Instance.globalData["MainCharBaseProp"] as Hashtable);
				
			// 每次进图就满血满蓝aa
			if (Property.ContainsKey("HP") && Property.ContainsKey("MaxHP"))
			{
				Property["HP"] = Property["MaxHP"];
			}

			if (Property.ContainsKey("SP") && Property.ContainsKey("MaxSP"))
			{
				Property["SP"] = Property["MaxSP"];
			}
			//
            AddSuitBuff();

			// 设置到UIaa
			RefreshProp();

			//
			if (sdConfDataMgr.Instance().skilliconAtlas == null)
			{
				int job = int.Parse(Property["BaseJob"].ToString());
				sdConfDataMgr.Instance().LoadSkillIcon(job);	
			}
			
			// 换装aa
			if(sdGlobalDatabase.Instance.globalData.Contains("MainCharItemInfo"))
			{
				SetItemInfo(sdGlobalDatabase.Instance.globalData["MainCharItemInfo"]as Hashtable);
			}

			// 技能aa
			if(sdGlobalDatabase.Instance.globalData.Contains("MainCharSkillInfo"))
			{
				SetSkillInfo(sdGlobalDatabase.Instance.globalData["MainCharSkillInfo"]as Hashtable);
			}
	
			// 初始Buffaa
			if (sdGlobalDatabase.Instance.globalData.Contains("InitBuff"))
			{
				int[] buffArray = (int[])sdGlobalDatabase.Instance.globalData["InitBuff"];
				if(buffArray!=null)
				{
					foreach(int id in buffArray)
					{
						AddBuff(id,0,this);
					}
				}
				sdGlobalDatabase.Instance.globalData["InitBuff"] = null;
			}

			// 宠物战队Buffaa
			List<int> kBuffList = sdNewPetMgr.Instance.GetPetGroupBuff(this);
			if (kBuffList != null)
			{
				foreach (int iBuffId in kBuffList)
				{
					AddBuff(iBuffId, 0, this);
				}
			}
		}	
	
		// 加载角色动画aa
		LoadAnimationFile(kInfo.mJob);

		// 初始化个体AI参数(自动战斗系统)aa
		FingerControl kFingerControl = sdGameLevel.instance.GetFingerControl();
		if (kFingerControl != null)
		{
			mAutoFight = kFingerControl;
			mAutoFight.NotifyChangeTarget += NotifyBattleSystemChangeTarget;

			sdSkill kSkill = skillTree.getSkill(1001);
			if (kSkill != null)
			{
				sdBaseState kState = kSkill.actionStateList[0];
				int iCastDistance = (int)kState.stateData["CastDistance"];
				mAutoFight.BattleDistance = (iCastDistance) * 0.001f;
			}
		}
        sdGuideMgr.Instance.Init(Property["Name"].ToString().Trim('\0'));

		// 初始化群体AI参数aa
		sdBehaviourNode kBehaviourNode = sdBehaviourManager.GetSingleton().GetBehaviourNode(1);
		if (kBehaviourNode != null)
		{
			kBehaviourNode.AddActor(this);
		}

		return true;
	}
	
	public void finl()
	{
	}
	
	public bool hasWeapon()
	{
		//< todo: temp code
		bool gear = false;
		Transform weaponLeft = bindNodeTable["dummy_left_weapon_at"] as Transform;
		Transform weaponRight = bindNodeTable["dummy_right_weapon_at"] as Transform;
		if(weaponLeft.childCount > 0 || weaponRight.childCount > 1)
		{
			gear = true;
		}
		
		return gear;
	}
	
//	// 角色移动(继承自sdGameActor)aa
//	public override	void moveInternal(Vector3 delta)
//	{
//		if (mMotionController == null)
//			return;
//		
//		CollisionFlags flags = mMotionController.Move(delta*Time.deltaTime);
//		if (sdGameLevel.instance != null && sdGameLevel.instance.AutoMode)
//		{
//			if((int)(flags&CollisionFlags.Sides) != 0)
//			{
//				if (mBlocked)
//				{
//					mBlockedTime += Time.deltaTime;
//					if (mBlockedTime > 3.0f)
//					{
//						mBlocked = false;
//						mBlockedTime = 0.0f;
//						sdGameLevel.instance.ClearMainCharacterMove();	//< 如果被阻挡超过3s则清空移动数据aa
//					}
//				}
//				else
//				{
//					mBlocked = true;
//					mBlockedTime = 0.0f;
//				}
//			}
//			else
//			{
//				mBlocked = false;
//				mBlockedTime = 0.0f;	
//			}
//		}
//	}
	
	// 阻挡回调aa
	protected void NotifyFindBlocked(sdActorInterface kActor)
	{
		if (sdGameLevel.instance != null && sdGameLevel.instance.AutoMode)
		{
			sdGameLevel.instance.ClearMainCharacterMove();	//< 如果被阻挡超过3s则清空移动数据aa	
		}
	}

	// 战斗系统切换攻击目标事件(注册到战斗系统的事件回调)aa
	protected void NotifyBattleSystemChangeTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		if (NotifyChangeTarget != null)
			NotifyChangeTarget(kActor, kPreviousTarget, kTarget);
		
		if (GetActorType() == ActorType.AT_Player)
		{
			if (!mLoadTargetEffect)
				LoadTargetEffect();
			
			mTargetEffectTarget = kTarget;

			if (kTarget == null)
			{
				if (mTargetEffect != null)
					mTargetEffect.SetActive(false);
			}
		}
	}
	
	// 更新主角展示模型aa
	public	override void RefreshAvatar()
	{
		sdGameLevel gameLevel = sdGameLevel.instance;
		if (gameLevel == null|| gameLevel.avatarCamera == null)
			return;
		
		if (gameLevel.avatarCamera.transform.childCount > 0)
		{
			Transform tt = gameLevel.avatarCamera.transform.GetChild(0);
			if(tt != null)
			{
				tt.parent = null;
				GameObject.Destroy(tt.gameObject);
			}
		}

		GameObject avatarNode = GameObject.Instantiate(this.renderNode) as GameObject;
		avatarNode.transform.parent = gameLevel.avatarCamera.transform;
		avatarNode.transform.localPosition = new Vector3(0.15f, -0.75f, 2.0f);
		avatarNode.transform.localRotation = Quaternion.AngleAxis(180.0f, Vector3.up);
		avatarNode.transform.localScale = Vector3.one;
					
		int avatarLayer = LayerMask.NameToLayer("AvatarNode");
		Transform[] renderData = avatarNode.GetComponentsInChildren<Transform>();
		foreach(Transform t in renderData)
		{
			t.gameObject.layer = avatarLayer;
		}
		
		Animation aniCtrl = avatarNode.GetComponent<Animation>();
		if(aniCtrl != null)
		{
			if(logicTSM.idle!=null)
			{
				AnimationState aniState = aniCtrl[logicTSM.idle.info];
				if(aniState != null)
				{
					aniCtrl.Play(logicTSM.idle.info);
				}
			}
		}
		
	}
	
	public	override	void	ShowDamageBubble(DamageResult dr)
	{
		if(dr.bubbleType ==	Bubble.BubbleType.eBT_Miss)  //屏蔽怪物的未命中aa
			return;


        if (dr.bubbleType == Bubble.BubbleType.eBT_Miss)
        {
            Bubble.MissBubble(Vector3.zero, true);
        }
        else if (dr.bubbleType == Bubble.BubbleType.eBT_Dodge)
            Bubble.Dodge(transform.position + new Vector3(0.0f, 2.0f, 0.0f), true);
        else if (dr.bubbleType == Bubble.BubbleType.eBT_SelfHurt)
        {
            Bubble.SelfHurt(-dr.damage, transform.position + new Vector3(0.0f, 2.0f, 0.0f), dr.bubbleType);
        }
        else
        {
            Bubble.OtherHurt(-dr.damage, transform.position, dr.bubbleType);
        }
	}
	public	override	void	AddHP(int hp)
	{
		base.AddHP(hp);
	
		if(hp > 0)
		{
            Vector3 kBubblePos = this.transform.position;
            kBubblePos.y += 2.0f;

			Bubble.AddHp(hp, kBubblePos, false);
		}
				
		int iCurrentHP			=	(int)Property["HP"];
		sdUICharacter uiChar = sdUICharacter.Instance;
		if(uiChar != null)
		{
			Hashtable hpDesc = new Hashtable();
			hpDesc["value"] = iCurrentHP > 0 ? iCurrentHP : 0;
			hpDesc["des"] = "";
			uiChar.SetProperty("HP", hpDesc);
			
			Hashtable maxHpDesc = new Hashtable();
			maxHpDesc["value"] = Property["MaxHP"];
			maxHpDesc["des"] = "";
			uiChar.SetProperty("MaxHP", maxHpDesc);
		}
		if(iCurrentHP<=0)
		{
            ClearNotDeathHoldBuffer();

            //pvp战场 不复活aaa
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PVP)
                return;
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PET_TRAIN)
            {
                sdPTManager.Instance.Fail();
                return;
            }
			//深渊中死亡，不请求复活，直接发送结算申请..
			sdTuiTuLogic ttLogic = sdGameLevel.instance.tuiTuLogic;
			if (ttLogic!=null && sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
			{
				sdActGameMsg.Send_CS_LEVEL_RESULT_NTF();
			}
			//世界BOSS中死亡，弹出自己的请求复活界面..
			else if (ttLogic!=null && sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_WORLD_BOSS)
			{
				if (sdActGameMgr.Instance.m_WorldBossInfo.m_Status==2)
				{
					//自杀宠物..
					if (this.Retainer!=null)
					{
						int iPetHp = this.Retainer.GetCurrentHP();
						if (iPetHp>0)
							iPetHp = iPetHp*(-1);
						this.Retainer.AddHP(iPetHp);
					}
					//弹出世界BOSS界面..
					sdActGameControl.Instance.ActiveWorldBossWnd(null);
					//请求结算..
					sdActGameMsg.Send_CS_WB_RESULT_REQ(0);
				}
			}
			else
			{
				sdUICharacter.Instance.ShowRelive();
			}
		}
	}
	public	override	void	AddSP(int sp)
	{
		base.AddSP(sp);
		if(sp > 0)
		{
			Bubble.AddSp(sp, Vector3.zero, true);
		}
			
		int iCurrentSP			=	(int)Property["SP"];
		sdUICharacter uiChar = sdUICharacter.Instance;
		if(uiChar != null)
		{
			Hashtable hpDesc = new Hashtable();
			hpDesc["value"] = iCurrentSP > 0 ? iCurrentSP : 0;
			hpDesc["des"] = "";
			uiChar.SetProperty("SP", hpDesc);
			
			Hashtable maxHpDesc = new Hashtable();
			maxHpDesc["value"] = Property["MaxSP"];
			maxHpDesc["des"] = "";
			uiChar.SetProperty("MaxSP", maxHpDesc);
		}
	}
	public override	void BuffChange(List<sdBuff> buffList)
	{
		sdUICharacter.Instance.BuffChange(buffList);
	}
	
	public override void SetTeleportInfo(TeleportInfo info)
	{
		tpInfo.Add(info);
		float distance = info.MoveSpeed*info.MoveTime;
		string knockback = "";
		if(distance < 3.0f)
			knockback = "knockslide01_004";
		else if(distance < 6.0f)
			knockback = "knockslide02_004";
		else
			knockback = "knockslide03_003";
		int random = UnityEngine.Random.Range(0, 10000);
		if(random < info.PlayActionProbality)
		{
			if(mAnimController != null && AnimController[knockback] != null)
			{
				AnimationState animState = AnimController[knockback];
				animState.wrapMode = WrapMode.Once;
				animState.layer = 13;
				mAnimController.Play(knockback);
			}
		}
		if(info.TeleportState)
			AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_TELEPORT);
	}
	
	public override	int Level
	{
		get 
		{ 
			return (int)Property["Level"];
		}	
	}
    protected override void OnAllLoadFinished()
    {
        StartCoroutine(DelayShow());
    }
    IEnumerator DelayShow()
    {
        yield return 0;
        yield return 0;
        SetVisiable_WhenLoading(true);
    }
}
