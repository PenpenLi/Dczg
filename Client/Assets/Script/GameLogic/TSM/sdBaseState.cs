using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public enum eStateEventType
{
	eSET_enter,
	eSET_leave,
	eSET_hit,
}

public class sdBaseState : sdAttackCB 
{
	public sdBaseState()
	{
		transitPath = new ArrayList();
	}
	public string	name;
	public const int warriorBaseID = 1000;
	public const int wizardBaseID = 100;
	
	public int strikeID = 0;
	
	public Hashtable stateData = null;
	public ArrayList transitPath = null;
	
	//public Hashtable audioConf = null;
	
	public string info = "";
    public string currentPlayedAnim = "";
	public ArrayList extraInfo = new ArrayList();
	public int id = -1;
	public int actionID	=	0;
	public float fadeTime = 0.0f;
	
	public 	sdSkill 			belongToSkill 	= 	null;
	public 	AnimationState 		aniState 		= null;

    public delegate void OnStateEvent(eStateEventType param);
	public  event  	OnStateEvent  StateEvent;

	public	bool				bPassive		=	false;
	public	bool				bEnable			=	true;
	public	SkillEffect[]		skillEffect		=	null;
    public int[]                skillEffectHitPoint = null;
	int							WeaponTrailLife	=	0;
	public	bool				bMoveState		=	false;
	public	bool				bCalcDamage		=	true;
	public	float				OldSpeed		=	1.0f;
	public	bool 				bAllHitPointDone	=	false;
    bool bHasZoomIn = false;
	
	public	bool	IsAnimValid(sdGameActor _mainChar)
	{
		return null != _mainChar.AnimController.GetClip(info);
	}
	public	virtual	void	SetUserData(object userdata)
	{
		
	}

    public void CallStateEvent(eStateEventType param)
	{
		if(StateEvent != null)
			StateEvent(param);
	}
	public	void	SetInfo(Hashtable acttioninfo, sdGameActor actor)
	{
		stateData	=	acttioninfo;
		object iEffectid	=	stateData["sSkillEffect[8]"];
		if(iEffectid!=null)
		{
			int[] idArray	=	(int[])iEffectid;
			skillEffect		=	new SkillEffect[idArray.Length];
			for(int i=0;i<idArray.Length;i++)
			{
				int id	=	idArray[i];
				Hashtable skillEffectArray = actor.GetSkillEffect();
				SkillEffect effect = skillEffectArray[id] as SkillEffect;
				skillEffect[i]	=	effect;	
			}
		}
        object effectHitPoint = stateData["sSkillEffectHitPoint[8]"];
        if (effectHitPoint!=null)
        {
            skillEffectHitPoint = (int[])effectHitPoint;
        }
		
		// 有特殊攻击动作则使用,否则使用默认攻击动作aa
		string kSkillAnimation = stateData["animation"] as string;
		if (kSkillAnimation != null && kSkillAnimation != "")
			info =	kSkillAnimation;
		else
			info = "skill01";
		
		
		WeaponTrailLife	=	(int)stateData["WeaponTrailLife"];
		bCalcDamage		=	((int)stateData["CalcDamage"]==1);
	}
	public void	AddHitPoint(float fTime)
	{
		object	obj	=	stateData["hitPoint"];
		if(obj	==	null)
		{
			stateData["hitPoint"]	=	fTime;
		}
		else if(obj.GetType().IsArray)
		{
			float[] hitArray	=	(float[])obj;
			float[] newArray	=	new float[hitArray.Length+1];
			for(int i=0;i<hitArray.Length;i++)
			{
				newArray[i]	=	hitArray[i];
			}
			newArray[hitArray.Length]	=	fTime;
			stateData["hitPoint"]		=	newArray;
		}
		else
		{
			float[] newArray	=	new float[2];
			newArray[0]	=	(float)obj;
			newArray[1]	=	fTime;
			stateData["hitPoint"]		=	newArray;
		}
	}
	public void playRunAudio(sdGameActor _gameActor)
	{
		if(aniState != null)
		{
			if(_gameActor.RunLFootAudioEnable && aniState.normalizedTime > 0.42f)
			{
				_gameActor.PlayAudio("$warrior_sound/run_dirt_road_1.wav");
				_gameActor.RunRFootAudioEnable = true;
				_gameActor.RunLFootAudioEnable = false;							
			}
			else if(_gameActor.RunRFootAudioEnable && aniState.normalizedTime > 0.92f)
			{
				_gameActor.PlayAudio("$warrior_sound/run_dirt_road_1.wav");
				_gameActor.RunLFootAudioEnable = true;
				_gameActor.RunRFootAudioEnable = false;
			}
			if(aniState.normalizedTime > 1.0f)
			{
				aniState.normalizedTime -= 1.0f;
				_gameActor.RunLFootAudioEnable = true;
			}
		}
	}
	
	public void playAudio(sdGameActor _gameActor)
	{
		if( _gameActor == null )
			return;
		if(aniState == null)
			return;
		if(stateData==null)
		{
			if(_gameActor.GetInstanceID() == sdGameLevel.instance.mainChar.GetInstanceID() && this == _gameActor.logicTSM.run)
				playRunAudio(_gameActor);
			return;
		}
		
		AudioSource audio = _gameActor.SelfAudioSource;
		string[] strAudio	=	(string[])stateData["AudioConf"];
		if(strAudio != null && strAudio.Length != 0)
		{		
			int[]	startTime	=	(int[])stateData["AudioStartTime"];
	
			for(int index = 0; index < startTime.Length && index < strAudio.Length; ++index)
			{				
				float testPoint = (float)startTime[index]*0.0001f;			
				if(aniState.wrapMode == WrapMode.Loop)
				{
					testPoint = testPoint+ ((int)aniState.normalizedTime);
				}

				if(audio != null && testPoint >= _gameActor.PreviousAudioTime && testPoint <= aniState.normalizedTime)
				{					
					_gameActor.PlayAudio(strAudio[index]);
				}
			}
			_gameActor.PreviousAudioTime = aniState.normalizedTime;
		}
	}
	
	public void playEffect(sdGameActor _gameActor)
	{
		if(stateData == null)
			return;
		if(aniState == null)
			return;
		Hashtable data = stateData;	
		if(data.Contains("EffectFile") && data.Contains("EffectStartTime") && data.Contains("EffectAnchor") && data.Contains("EffectLifeTime"))
		{
			string[] fileName = (string[])data["EffectFile"];
			if(fileName.Length == 0)
				return;
			int[] effectStartTime = (int[])data["EffectStartTime"];
			if(effectStartTime.Length == 0)
				return;
			string[] anchorNameArray = (string[])data["EffectAnchor"];
			int[] effectLifeTime = (int[])data["EffectLifeTime"];
			for(int index = 0; index < effectStartTime.Length && index < fileName.Length && index < anchorNameArray.Length && index < effectLifeTime.Length; ++index)
			{
				float testpoint = (float)effectStartTime[index]*0.0001f;
				if(testpoint > _gameActor.PreviousEffectTime && testpoint <= aniState.normalizedTime)
				{
					Vector3 effectPosition = Vector3.zero;
					Quaternion rot = Quaternion.identity;
					if(anchorNameArray[index] == "gamelevel")
					{
						effectPosition 	= 	_gameActor.transform.position;
						rot				=	_gameActor.transform.rotation;
					}
					_gameActor.attachEffect(fileName[index], anchorNameArray[index], effectPosition, rot,1.0f, effectLifeTime[index]*0.001f);
				}
			}
			_gameActor.PreviousEffectTime = aniState.normalizedTime;
		}
	}
	
	public void playEffectNow(sdGameActor _gameActor)
	{
		if(stateData==null)
			return;
		Hashtable data = stateData;
		if(data.Contains("EffectFile") && data.Contains("EffectAnchor") && data.Contains("EffectLifeTime"))
		{
			string[] fileName = (string[])data["EffectFile"];
			if(fileName.Length ==0)
				return;
			string[] anchorNameArray = (string[])data["EffectAnchor"];
			if(anchorNameArray.Length == 0)	
				return;	
			int[] effectLifeTime = (int[])data["EffectLifeTime"];
			if(effectLifeTime.Length == 0)
				return;			
			Vector3 	effectPosition 	= 	Vector3.zero;
			Quaternion 	rot				=	Quaternion.identity;
			if(anchorNameArray[0] == "gamelevel")
			{
				effectPosition 	= 	_gameActor.transform.position;
				rot				=	_gameActor.transform.rotation;
			}
			_gameActor.attachEffect(fileName[0], anchorNameArray[0], effectPosition, rot,1.0f, effectLifeTime[0]*0.001f);
		}
	}
	
	public	void	PlayAudioNow(sdGameActor _gameActor)
	{
		if(stateData==null)
		{
			return;
		}
		
		AudioSource audio = _gameActor.SelfAudioSource;
		string[] strAudio	=	(string[])stateData["AudioConf"];
		if(strAudio==null)
		{
			return;
		}
		if(strAudio.Length != 0)
		{		
			_gameActor.PlayAudio(strAudio[0]);
		}
	}

	public void checkHitPoint(sdGameActor _gameActor)
	{
 		if(_gameActor == null || _gameActor.AnimController == null || stateData == null)
		{
			return;
		}		
		AnimationState aniStateTemp = aniState;

		if(aniStateTemp == null)
			return;		
		if(stateData.ContainsKey("hitPoint"))
		{			
			bool shakeCamera	=	((int)stateData["shakeCamera"] == 1);
			float	shakelevel	=	((int)stateData["ShakeLevel"])*0.0001f;
			
			object hitPointData = stateData["hitPoint"];			
			float[] hitPoints = null;
			if(hitPointData.GetType().IsArray)
			{				
				hitPoints = (float[])hitPointData;
			}
			else
			{
				hitPoints = new float[1]{(float)hitPointData};
			}
			float	fCompareTime	=	aniStateTemp.normalizedTime;
			fCompareTime	-=	Mathf.Floor(fCompareTime);
			for(int k = 0; k < hitPoints.Length; ++k)
			{
				float testPoint = hitPoints[k]*0.0001f;
				
				if(testPoint > _gameActor.PreviousTime && testPoint <= fCompareTime)
				{	
					if(sdGameLevel.instance != null && sdGameLevel.instance.battleSystem != null)
					{		
						OnAttack(_gameActor,k);
						
						AddSkillEffect(
                            _gameActor,
                            HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_HIT,
                            k+1,
                            skillEffect,
                            skillEffectHitPoint,
                            null,
                            ((int)stateData["ParentID"])/100);

						
                        CallStateEvent(eStateEventType.eSET_hit);	

						sdGameLevel level = sdGameLevel.instance;
						if(level != null && shakeCamera)
							level.mainCamera.addRandomCameraShake(0.3f, 0.3f*shakelevel, 60.0f, 3.0f);
					}
					if(k==(hitPoints.Length-1))
					{
						bAllHitPointDone	=	true;
					}
				}
			}
			_gameActor.PreviousTime =	fCompareTime;
		}
		else
		{
			bAllHitPointDone	=	true;
		}
		
	}

    public void checkDeathHitPoint(sdGameActor _gameActor)
    {
        if (_gameActor == null || _gameActor.AnimController == null)
        {
            return;
        }
        if (this != _gameActor.logicTSM.die)
        {
            return;
        }
        if (skillEffect == null)
        {
            object deathEffect = _gameActor.GetProperty()["DeathEffect"];
            if (deathEffect == null)
            {
                return;
            }
            int[] effectArray = null;
            if (deathEffect.GetType() == typeof(int))
            {
                effectArray = new int[1] { (int)deathEffect };
            }
            else
            {
                effectArray =   (int[])deathEffect;
            }

            skillEffect = new SkillEffect[effectArray.Length];
            for (int i = 0; i < effectArray.Length; i++)
            {
                int id = effectArray[i];
                Hashtable skillEffectArray = _gameActor.GetSkillEffect();
                SkillEffect effect = skillEffectArray[id] as SkillEffect;
                skillEffect[i] = effect;
            }
        }

        object hitpoint = _gameActor.GetProperty()["DeathHitPoint"];
        if (hitpoint == null)
        {
            return;
        }
        

        if (skillEffectHitPoint == null)
        {
            object deathEffectHitPoint = _gameActor.GetProperty()["sSkillEffectHitPoint"];
            if (deathEffectHitPoint != null)
            {
                skillEffectHitPoint = null;
                if (deathEffectHitPoint.GetType() == typeof(int))
                {
                    skillEffectHitPoint = new int[1] { (int)deathEffectHitPoint };
                }
                else
                {
                    skillEffectHitPoint = (int[])deathEffectHitPoint;
                }
            }
        }
        

        AnimationState aniStateTemp = aniState;
 
        if (aniStateTemp == null)
            return;
        
        {
            object hitPointData = hitpoint;
            float[] hitPoints = null;
            if (hitPointData.GetType().IsArray)
            {
				hitPoints	=	(float[])hitPointData;
            }
            else
            {
                hitPoints = new float[1] { (float)(int)hitPointData };
            }
            float fCompareTime = aniStateTemp.normalizedTime;
            fCompareTime -= Mathf.Floor(fCompareTime);
            for (int k = 0; k < hitPoints.Length; ++k)
            {
                float testPoint = hitPoints[k] * 0.0001f;

                if (testPoint > _gameActor.PreviousTime && testPoint <= fCompareTime)
                {
                    if (sdGameLevel.instance != null && sdGameLevel.instance.battleSystem != null)
                    {

                        AddSkillEffect(
                            _gameActor,
                            HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_HIT,
                            k + 1,
                            skillEffect,
                            skillEffectHitPoint,
                            null,
                            0);


                        CallStateEvent(eStateEventType.eSET_hit);


                    }
                    
                }
            }
            _gameActor.PreviousTime = fCompareTime;
        }
       

    }
			
	public	override	void	OnAttack(sdActorInterface _gameActor,int index)
	{
		//if(bCalcDamage)
		{
			sdBattleSystem battleField = sdGameLevel.instance.battleSystem;
			int nCombo =battleField.DoSDAttack(
				_gameActor, 
				stateData,
				_gameActor.transform.position,
				index,
				this);
            if (_gameActor == sdGameLevel.instance.mainChar && nCombo > 0)
                sdUICharacter.Instance.ShowComboWnd(true, nCombo);
		}
	}
	
	public	virtual	void	PlayHitEffect(sdActorInterface monster)
	{
		if(stateData!=null)
		{
			string	strHitEffect	=	stateData["HitEffect"] as string;
			
			if(strHitEffect.Length>0)
			{
				int		hitEffectLife	=	(int)stateData["HitEffectLife"];
				float fLife	=	hitEffectLife*0.001f;
				Transform	bindnode	=	monster.transform;
				if(monster.actorType == ActorType.AT_Monster)
				{
					sdGameMonster actor	=	(sdGameMonster)monster;
					if(actor.ChestPoint.Length>0)
					{
						GameObject chest	=	actor.GetNode(actor.ChestPoint.Replace("\r\n",""));
						if(chest!=null)
						{
							bindnode	=	chest.transform;
						}
					}
				}
				else if(monster.actorType == ActorType.AT_Player)
				{
					if(stateData.ContainsKey("HitDummy"))
					{
						int pos = (int)stateData["HitDummy"];
						if(pos == 1)
						{
							sdGameActor gameactor = (sdGameActor)monster;
							GameObject chest = gameactor.GetNode("Bip01 Spine");
							if(chest != null)
								bindnode = chest.transform;
						}
					}
				}
				bool bRotate = true;
				if(stateData.ContainsKey("HitEffectRotate"))
					bRotate = ((int)stateData["HitEffectRotate"] == 1);
				sdActorInterface.AddHitEffect(strHitEffect,bindnode,fLife,Vector3.zero,bRotate);
			}
		}
	}
	
	public	override	int	OnHit(sdActorInterface _gameActor,List<sdActorInterface>	lstMonster,int iHitPointIndex,object userdata, HeaderProto.ESkillEffect skilleffect)
	{
        int nRet = 0;
		if(lstMonster.Count==0)
			return nRet;
        List<Bubble.BubbleType> lstBubbleType = new List<Bubble.BubbleType>();
        if (bCalcDamage)
        {
            sdTuiTuLogic tuiTuLogic = sdGameLevel.instance.battleSystem.tuiTuLogic;
            bool bMaincChar = (_gameActor == sdGameLevel.instance.mainChar);
            lstMonster.ForEach(delegate(sdActorInterface monster)
            {
                CallStateEvent(eStateEventType.eSET_hit);

                sdBattleSystem bs = sdGameLevel.instance.battleSystem;
                DamageResult dr = bs.testHurt(
                    _gameActor,
                    stateData,
                    monster,
                    iHitPointIndex,
                    skilleffect
                    );

                //播放命中特效.闪避buff的不播放命中特效aaa
                if (dr.damage > 0)
                    PlayHitEffect(monster);
                lstBubbleType.Add(dr.bubbleType);
                if(Bubble.IsHurtOther(dr.bubbleType))
                    nRet++;
            });
        }
        AddSkillEffect(_gameActor, lstMonster, lstBubbleType, skillEffect, null, ((int)stateData["ParentID"]) / 100);
        return nRet;
    }
	void comboMove(sdGameActor _gameActor, float tt)
	{
		if(_gameActor.AnimController != null)
		{
			if(aniState != null)
			{
                float f = aniState.normalizedTime - (float)System.Math.Floor(aniState.normalizedTime);
				if(f > tt)
				{
					_gameActor.motionFunction_todo();
				}
			}
		}
	}
		
	public void StartTimer(sdGameActor _gameActor)
	{
        sdActionStateTimer timer = _gameActor.actionStateTimer;
		if(stateData!=null)
		{
			if(stateData.ContainsKey("SkillTimerInterval") &&
				stateData.ContainsKey("SkillTimerTrigger"))
			{
				int _interval = (int)stateData["SkillTimerInterval"];
				int _trigger = 0;//(int)stateData["SkillTimerTrigger"];

				



				if(_interval==0)
				{
					if(timer.targetSkill == belongToSkill)
					{
						timer.targetSkill = null;
						timer.end(_gameActor);
					}
					return;
				}
				if(belongToSkill!=null)
				{
					sdBaseState next = GetNextSkillAction();
					if(next==null)
					{
						timer.end(_gameActor);
						return;
					}
				}

				float length = 0.0f;
				if(aniState!=null)
				{
					length	=	aniState.length;
				}


				timer.begin(_gameActor,this,length+_interval*0.001f, _trigger*0.001f);
                return;
			}
		}

        timer.end(_gameActor);
	}
	void fatalStrikeMove(sdGameActor _gameActor,int s, int e, float speed)
	{		
		if(_gameActor.AnimController != null)
		{
			
			AnimationState aniStateTemp = aniState;
			
			if(aniStateTemp != null)
			{
				//float f = aniState.normalizedTime - (float)Math.Floor(aniState.normalizedTime);
				int f = (int)(aniStateTemp.normalizedTime*10000.0f);
				if(f > s && f <= e)
				{
					if(belongToSkill!=null && belongToSkill.id == 1002)
					{
						_gameActor.moveGravity(_gameActor.GetDirection()*speed);
					}
					else
					{
						_gameActor.moveInternal(_gameActor.GetDirection()*speed);
					}
				}
				
			}
			//<
		}
	}
	void	_InternalRotate(sdGameActor _gameActor,int begin, int end)
	{
		AnimationState aniStateTemp = aniState;

		if(aniStateTemp != null)
		{
			int f = (int)(aniStateTemp.normalizedTime*10000.0f);

			if(f > begin && f <= end)
			{
				_gameActor.spinToTargetDirection(_gameActor.TargetFaceDirection, true);
			}
		}
	}
	
	public	virtual	void	Enter(sdGameActor _gameActor)
	{
        bHasZoomIn = false;
        if (!bPassive)
        {
			_gameActor.spinToTargetDirection(_gameActor.TargetFaceDirection, true);
        }
		bAllHitPointDone	=	false;

		int id = 0;
		if(stateData!=null)
		{
			int sp			=	(int)stateData["dwCostSP"];
			_gameActor.AddSP(-sp);
			
			id = (int)stateData["ParentID"];
			id = id/100;
			if(id == 1002)
				_gameActor.gameObject.layer = LayerMask.NameToLayer("NoMonster");
		}

		AddSkillEffect(_gameActor,HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_START,0,skillEffect,null,null, id);
			

		CallStateEvent(eStateEventType.eSET_enter);	

		_gameActor.PreviousTime		=	0.0f;
		_gameActor.PreviousAudioTime = 0.0f;
		_gameActor.PreviousEffectTime = 0.0f;

		PlayAnim(_gameActor);

		StartTimer(_gameActor);
		
		checkHitPoint(_gameActor);
		
		BeginWeaponTrail(_gameActor);
		
		if(stateData!=null)
		{
			//播放自身特效..
			object	strSelfEffect	=	stateData["SelfEffect"];
			int		selfEffectLife	=	(int)stateData["SelfEffectLife"];
			if(strSelfEffect!=null && ((string)strSelfEffect).Length != 0)
			{
				Transform	bone	=	_gameActor.FindBone("Bip01");
				if(bone==null)
				{
					bone	=	_gameActor.transform;
				}
				float life	=	(float)selfEffectLife*0.001f;
				sdActorInterface.AddHitEffect((string)strSelfEffect,bone,life,Vector3.zero,true);
			
			}
		}
		if(_gameActor == sdGameLevel.instance.mainChar)
		{
			if(	belongToSkill != null && 
				sdUICharacter.Instance != null)
			{
				int skillID = belongToSkill.id*100 + _gameActor.Job;
				if(belongToSkill.id!=1001)
				{
					sdUICharacter.Instance.SetSkillCurStage(actionID,belongToSkill.GetValidActionCount());
				}
				//sdUICharacter.Instance.ShowShortCutCd(skillID);
				//belongToSkill.startColding();
				int cdInterval = belongToSkill.cooldown;
				sdUICharacter.Instance.SetShortCutCd(skillID, cdInterval, false);
                sdGameLevel.instance.mainChar.SkillEndTime = float.MaxValue;
			}         
		}	
		PlayDieAudio(_gameActor);
		
		_gameActor.RunLFootAudioEnable = true;
		_gameActor.RunRFootAudioEnable = true;
	}
	
	protected   void    PlayDieAudio(sdGameActor actor)
	{
		string[]  strAudioArray = {"$warrior_sound/m_2_die_01.wav", "$mage_sound/w_2_die_02.wav", "$ranger_sound/m_4_die_02.wav", "$cleric_sound/woman_die_01.wav"};
		sdGameActor mainchar = sdGameLevel.instance.mainChar;
		if(actor.GetInstanceID() == mainchar.GetInstanceID() && this == mainchar.logicTSM.die)
		{
			int job = (int)mainchar.GetJob();
			job = job/3;
			if(job >= 0 && job < strAudioArray.Length)
			{
				actor.PlayAudio(strAudioArray[job]);
			}
		}
	}
	
	protected	virtual	void	PlayAnim(sdGameActor actor)
	{
        if (id != 100)
        {
            aniState = actor.AnimController[info];
            currentPlayedAnim = info;
        }
        else
        {
            aniState = actor.AnimController["knockback_death01"];
            if (aniState == null || (Time.frameCount & 1) == 0)
            {
                aniState = actor.AnimController[info];
            }
            else
            {
                if (Application.isEditor)
                {
                    //Debug.Log("knockback_death01");
                }
            }
			if(aniState!=null)
			{
            	currentPlayedAnim   =   aniState.name;
			}
			else
			{
				currentPlayedAnim	=	info;
			}
        }
		if(aniState != null)
		{
			if(bPassive)
			{
				if(id != 100)
				{
					aniState.wrapMode	=	WrapMode.Loop;
				}
				else
				{
					aniState.wrapMode	=	WrapMode.ClampForever;
				}
			}
			else
			{
				aniState.wrapMode	=	WrapMode.Once;
				aniState.speed	=	1.0f+actor["AttSpeedModPer"]*0.0001f;
			}
			if(belongToSkill!=null)
			{
				if(belongToSkill.id == 1002)
				{
                    actor.AnimController.Play(info, PlayMode.StopSameLayer);
					return;
				}
			}
			actor.AnimController.Play(info,PlayMode.StopSameLayer);
		}	
	}
	
	public	virtual	void	Leave(sdGameActor _gameActor)
	{
		bAllHitPointDone	=	true;

		int id = 0;
		if(stateData != null)
		{
			id = (int)stateData["ParentID"];
			id = id/100;
			if (id == 1002)
			{
				// 主角回到默认图层aa
				_gameActor.gameObject.layer = LayerMask.NameToLayer("Player");	

				// 主角强制向下移动(强制落到地面)aa
				if (_gameActor.MotionController)
				{
					Vector3 kDelta = new Vector3(0.0f, -10.0f, 0.0f);
					_gameActor.MotionController.Move(kDelta);
				}
			}
		}

		AddSkillEffect(_gameActor,HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_END,-1,skillEffect,null,null, id);
		
		EndWeaponTrail(_gameActor);

		CallStateEvent(eStateEventType.eSET_leave);
        if (_gameActor.AnimController)
        {
            _gameActor.AnimController.Stop(currentPlayedAnim);
        }
        if(_gameActor == sdGameLevel.instance.mainChar)
        {
            sdGameLevel.instance.mainChar.SkillEndTime = (belongToSkill != null ? Time.time + 0.3f: 0.0f); 
        }

	}
	public	virtual	void	Update(sdGameActor _gameActor)
	{
		playAudio(_gameActor);		
		playEffect(_gameActor);		

		if(stateData!=null)
		{
			int	ms		=	(int)stateData["MoveSpeed"];
			if(ms!=0)
			{
				int	begin	=	(int)stateData["MoveBeginTime"];
				int	end		=	(int)stateData["MoveEndTime"];
				
				float	speed	=	_gameActor.GetMoveSpeed()*(float)ms*0.0001f;
				if(!bPassive)
				{
					speed*=1.0f+_gameActor["AttSpeedModPer"]*0.0001f;
				}
				fatalStrikeMove(_gameActor,begin,end,speed);
			}
			int	rotbegin	=	(int)stateData["RotateBeginTime"];
			int	rotend		=	(int)stateData["RotateEndTime"];
			if(rotbegin!=rotend)
			{
				_InternalRotate(_gameActor,rotbegin,rotend);
			}
		}
		if(bMoveState)
		{
			_gameActor.motionFunction_todo();
		}
        if (_gameActor.actorType == ActorType.AT_Monster)
        {
            checkDeathHitPoint(_gameActor);
        }
		checkHitPoint(_gameActor);
		CheckZoomIn(_gameActor);
	}

	public	virtual	bool OnCastSkill(sdGameActor _gameActor,sdSkill s)
	{
		if(belongToSkill==s)
		{
			return OnContinueSkill(_gameActor);
		}
		else
		{
			_gameActor.logicTSM.nextState	=	s.GetFirstValidAction();
			return (_gameActor.logicTSM.nextState != null);
		}
		
		return false;
	}
	public	bool	OnContinueSkill(sdGameActor _gameActor)
	{
		sdBaseState next = GetNextSkillAction();
		if(next!=null)
		{
			_gameActor.logicTSM.nextState	=	next;
			return true;
		}

		return false;
	}
	public sdBaseState GetNextSkillAction()
	{
		if(belongToSkill==null)
		{
			return null;
		}
		int index = -1;
		for(int i=0;i<belongToSkill.actionStateList.Count;i++)
		{	
			if(belongToSkill.actionStateList[i]	==	this)
			{
				index	=	i+1;
				break;
			}
		}
		if(index==-1)
		{
			return null;
		}
		
		for(int i=index;i<belongToSkill.actionStateList.Count;i++)
		{	
			sdBaseState	nextContinueState	=	belongToSkill.actionStateList[i];
			//当前这个连招是否有效---
			if(nextContinueState.bEnable)
			{
				return	nextContinueState;
			}
		}
        if (belongToSkill.bUnLimitedAction)
        {
            for (int i = 0; i < belongToSkill.actionStateList.Count; i++)
            {
                sdBaseState nextContinueState = belongToSkill.actionStateList[i];
                //当前这个连招是否有效---
                if (nextContinueState.bEnable)
                {
                    return nextContinueState;
                }
            }
        }
		return null;
	}
	
	
	//只针对开始、结束、hit点aaa
	public	static void	AddSkillEffect(
		sdActorInterface castActor,
		HeaderProto.ESkillTimingConditionType timing,
        int             hitPointIndex,
		SkillEffect[]	skillEffect,
        int[]           skillEffectHitPoint,
		Transform		customTrans,
		int             skillID
		)
	{
		if(skillEffect==null)
			return;
		
		for(int i=0;i<skillEffect.Length;i++)
		{
			SkillEffect	eff	=	skillEffect[i];	
			if(eff==null)
			{
				continue;
			}
			if(	eff.byTimingCondition == (int)HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_HIT && 
				eff.byHitCondition    != (int)HeaderProto.ESkillEffectHitCondition.SKILL_EFT_CONDITOION_ALWAYS)
			{
				continue;
			}
				
			if(eff.byTimingCondition == (int)timing)
			{
                //每个skilleffect 必须判定hit点
                if (skillEffectHitPoint!=null && eff.byTimingCondition == (int)HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_HIT)
                {
                    if (    hitPointIndex != skillEffectHitPoint[i] && skillEffectHitPoint[i]!=0    )
                    {
                        continue;
                    }
                }
				sdSkillEffect.Add(castActor,null,eff,customTrans,skillID);
			}
		}
		
		
	}
   
	//只针对击中目标aaa
	public	static void	AddSkillEffect(
		sdActorInterface castActor,
		List<sdActorInterface>	lstMonster,
        List<Bubble.BubbleType> lstBubble,
		SkillEffect[]	skillEffect,
		Transform customTrans,
		int  skillID)
	{
		if(skillEffect==null)
			return;
		for(int i=0;i<skillEffect.Length;i++)
		{
			SkillEffect	eff	=	skillEffect[i];	
			if(eff==null)
			{
				continue;
			}
			//每命中一个目标都生效aaa
			if(eff.byCountCondition == 1)
			{
				switch(eff.byHitCondition)
				{
					case (int)HeaderProto.ESkillEffectHitCondition.SKILL_EFT_CONDITOION_HIT:{
						lstMonster.ForEach(delegate(sdActorInterface obj) {
							sdSkillEffect.Add(castActor,obj,eff,customTrans, skillID);
						});
					}break;
                    case (int)HeaderProto.ESkillEffectHitCondition.SKILL_EFT_CONDITOION_DEADLINESS://暴击aa
                        {
                            for (int index = 0; index < lstMonster.Count && index < lstBubble.Count; ++index)
                            {
                                if (lstBubble[index] == Bubble.BubbleType.eBT_CriticalBaseHurt || lstBubble[index] == Bubble.BubbleType.eBT_CriticalSkillHurt)
                                {
                                    sdSkillEffect.Add(castActor, lstMonster[index], eff, customTrans, skillID);
                                }
                            }
                        }
                        break;
				}
			}
			///只生效一次aaa
			else
			{
				switch(eff.byHitCondition)
				{
					case (int)HeaderProto.ESkillEffectHitCondition.SKILL_EFT_CONDITOION_HIT:{
					for(int index = 0; index < lstMonster.Count; ++index )
					{
						bool bValid = sdSkillEffect.Add(castActor,lstMonster[index],eff,customTrans, skillID);
						if(bValid)
							break;
					}
						
					}break;
				}
			}
		}
		
	}
	void	BeginWeaponTrail(sdGameActor actor)
	{
		if(stateData==null)
			return;
		
		if(actor.WeaponTrail != null && WeaponTrailLife!=0)
		{
			float	life		=	(float)WeaponTrailLife;
			float	disappear	=	(float)(int)stateData["WeaponTrailDisappear"];
			float	length		=	(float)(int)stateData["WeaponTrailLength"];
			float	delay		=	(float)(int)stateData["WeaponTrailDelay"];
			
			actor.WeaponTrail.StartTrail(life*0.001f,disappear*0.001f,length*0.001f,delay*0.001f);
			
		}
	}
	void	EndWeaponTrail(sdGameActor actor)
	{
		if(stateData==null)
			return;
		
		if(actor.WeaponTrail != null&&WeaponTrailLife!=0)
		{
			actor.WeaponTrail.ClearTrail();
		}
	}
	public	int	GetCostSP()
	{
		if(stateData==null)
			return 0;
		int sp	=	(int)stateData["dwCostSP"];
		return sp;
	}
	void	CheckZoomIn(sdGameActor _gameActor)
	{
        if (bHasZoomIn)
        {
            return;
        }
		if( _gameActor == null )
			return;
		if(aniState == null)
			return;
		if(stateData==null)
		{
			return;
		}
		
		int	ZoomInLevel	=	((int)stateData["ZoomInLevel"]);
		if(ZoomInLevel!=0)
		{

			
			int	ZoomInBegin	=	((int)stateData["ZoomInStartTime"]);
			int	ZoomInEnd	=	((int)stateData["ZoomInEndTime"]);
            if (ZoomInBegin == 0)
            {
                ZoomInBegin = 1;
            }
			if(ZoomInEnd<ZoomInBegin)
			{
				return;
			}
            if (aniState != null)
            {
                int nTime   =   (int)(aniState .normalizedTime* 10000);

                float fTime = (ZoomInEnd - ZoomInBegin) * 0.0001f * aniState.length;
                if (ZoomInBegin < nTime)
                {
                    sdGameLevel.instance.mainCamera.BeginZoomIn(ZoomInLevel * 0.0001f, fTime);
                    bHasZoomIn = true;
                }
            }
			
		}
	}
}
