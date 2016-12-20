using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class sdSummonAttackCB  : sdAttackCB
{
	public	sdSkillSummon summon	=	null;
	public	override	void	OnAttack(sdActorInterface _gameActor,int index)
	{
		sdBattleSystem battleField = sdGameLevel.instance.battleSystem;
		int nCombo = battleField.DoSDAttack(
			_gameActor, 
			summon.info, 
			summon.transform.position, 
			index, 
			this);
        if (summon.castActor == sdGameLevel.instance.mainChar && nCombo > 0)
            sdUICharacter.Instance.ShowComboWnd(true, nCombo);
	}
	public	override	int	OnHit(sdActorInterface _gameActor,List<sdActorInterface>	lstMonster,int iHitPointIndex,object userdata, HeaderProto.ESkillEffect eSkilleffect)
	{
        return summon.OnHit(lstMonster, eSkilleffect);			
	}
}



public class sdSkillSummon : MonoBehaviour
	{
		public static string[] SummonProperty = new string[32]
		{
		"byTargetType",
		"dwHitPer",
		"dwCriticalPer",
		"dwCriticalDmgPer",
		"wAoeAimNum",
		"Mode",
		"PeriodTime",
		"Count",
		"nAoeArea",
		"byAoeAreaType",
		"naoeAreaData",
		"byIgnoreDef",
		"dwIgnoreDefendAmount",
		"dwDamagePer",
		"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
		"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
		"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
		"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
		"bySkillEffect",
		"dwDmg[10]",
		"dwAtkPowerPer[10]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"sSkillEffect[8]",
		"MoveSpeed",
		"LifeTime",
		"DelayDamage",
		};
		public sdActorInterface	    castActor		=	null;
		sdActorInterface            targetActor     =   null; 
		Vector3                     targetPos       = 	Vector3.zero;
		sdSummonAttackCB			cb				=	new sdSummonAttackCB();
		public	Hashtable			info			=	null;
		int							id				=	0;
		int							life			=	1000;
		int							CurrentLife		=	0;
		int							PeriodTime		=	0;
		int							PeriodCount		=	0;
		int							TriggerCount	=	0;
		int							HitCount		=	0;
		int							iCurrentHit		=	0;
		int							layermask		=	0;
		Vector3						Direction		=	Vector3.zero;
		public	SkillEffect[]		skillEffect		=	null;
		public  int                 skillID         =   0;
		Vector3						lastPostion		=	Vector3.zero;
		int                         dwDelayDamage   = 	0;  //鍙?敜鐗╀激瀹崇敓鏁堝掕?鏃堕棿aaa
		bool                        bParabola       = 	false;  //鏄?惁鏄?姏鐗╃嚎椋炶?aaaa
		bool                        bFly            = 	true;   //鍙?敜鐗╂槸鍚﹂?琛宎aa
		bool                        bDelayExplosion =   false;  //鍙?敜鐗╀激瀹崇敓鏁堝掕?鏃堕棿==0 鎾?斁鐖嗙偢鐗规晥aaa
        bool                        bPlayExplosion  = true;  //鏄?惁鎾?斁鐖嗙偢鐗规晥
        int                         uniqueID = 0;

        float g = -9.8f;
        float initY = 0.0f;
		//CapsuleCollider				colli			=	null;
		sdCapsuleShape				colliShape		=	new sdCapsuleShape();
		List<sdActorInterface>		LastActorList	=	new List<sdActorInterface>();
		List<sdActorInterface>	FindActor()
		{
			Vector3	center	=	colliShape.point;
			float	radius	=	colliShape.radius;
		
			if(colliShape.type == ShapeType.eCapsule)
			{
				center	=	colliShape.point+colliShape.dir*colliShape.length*0.5f;
				radius	=	colliShape.length*0.5f+colliShape.radius;
			}
			HeaderProto.ESkillObjType objType = (HeaderProto.ESkillObjType)info["byTargetType"];
			List<sdActorInterface> lstActor	=	sdGameLevel.instance.actorMgr.FindActor(castActor, objType, center, new Vector3(0,0,1), 1, 0, radius, true);
			if(lstActor==null)
			{
				return null;
			}
			if(colliShape.type == ShapeType.eCapsule)
			{
				List<sdActorInterface> retActor	=	new List<sdActorInterface>();
				sdCapsuleShape	actorShape	=	new sdCapsuleShape();
				for(int i=0;i<lstActor.Count;i++)
				{
					sdActorInterface	actor	=	lstActor[i];
					if(actor.GetComponent<Collider>()!=null)
					{
						if(actor.GetComponent<Collider>().GetType() == typeof(CharacterController))
						{
							actorShape.SetInfo(actor.GetComponent<Collider>() as CharacterController);
							if(colliShape.IsIntersect(actorShape))
							{
								retActor.Add(actor);
							}
						}
					}
				}
				return retActor;
			}
			else if(colliShape.type == ShapeType.eSphere)
			{
				return	lstActor;
			}
			return null;
			//if(Physics.SphereCastAll(point,cc.radius,dir,halfheight*2.0f))
		}
		public	void	SetLife(float fLife)
		{
			life	=	(int)(fLife*1000.0f);
		}
		sdActorInterface	FindOneActor()
		{
			Vector3	center	=	colliShape.point;
			float	radius	=	colliShape.radius;
		
			if(colliShape.type == ShapeType.eCapsule)
			{
				center	=	colliShape.point+colliShape.dir*colliShape.length*0.5f;
				radius	=	colliShape.length*0.5f+colliShape.radius;
			}
			HeaderProto.ESkillObjType objType = (HeaderProto.ESkillObjType)((int)info["byTargetType"]);
			List<sdActorInterface> lstActor	=	sdGameLevel.instance.actorMgr.FindActor(castActor, objType, center, new Vector3(0,0,1), 1, 0, radius, true);
			if(lstActor==null)
			{
				return null;
			}
			if(colliShape.type == ShapeType.eCapsule)
			{
				sdCapsuleShape	actorShape	=	new sdCapsuleShape();
				for(int i=0;i<lstActor.Count;i++)
				{
					sdActorInterface	actor	=	lstActor[i];
                    if (!actor.IsCanSummonAttack(uniqueID))
                    {
                        continue;
                    }
					if(actor.GetComponent<Collider>()!=null)
					{
						if(actor.GetComponent<Collider>().GetType() == typeof(CharacterController))
						{
							actorShape.SetInfo(actor.GetComponent<Collider>() as CharacterController);
							
							if(actorShape.IsIntersect(colliShape))
							{
								return actor;
							}
							
						}
					}
				}
			}
			else if(colliShape.type == ShapeType.eSphere)
			{
				if(lstActor.Count>0)
				{
					return lstActor[0];
				}
			}
			return null;
		}

	//param.userdata0	=	info.summonID;
	//param.userdata1	=	info.userdata; target璺熻釜鐩?爣aaa
	//param.userdata2 =   info.skillID;
	//param.userdata3 =   info.bParabola;
		public	void	SetInfo(sdActorInterface	actor,ResLoadParams param)
		{
			bFly        =    (bool)param.userdata4;
			id			=	(int)param.userdata0;
			castActor	=	actor;
			info		=	castActor.m_summonInfo[id] as Hashtable;
			life		=	(int)info["LifeTime"];
			PeriodTime	=	(int)info["PeriodTime"];
			HitCount	=	(int)info["Count"];
			bParabola   =   (bool)param.userdata3;
			int Speed	=	(int)info["MoveSpeed"];
			dwDelayDamage = (int)info["DelayDamage"];
			bDelayExplosion = (dwDelayDamage == 0 ? false : true);
            bPlayExplosion = (dwDelayDamage == 0 ? true : false);
            uniqueID = param.petInt;
            targetActor = (sdActorInterface)param.userdata1;
            string BindNode = "";
            if (info.ContainsKey("BindNode"))
                BindNode    =   info["BindNode"] as string;
            if (BindNode.Length>0)
            {
                
                bFly = false;
                targetActor = null;
                Transform[] nodes = castActor.GetComponentsInChildren<Transform>();
                for (int i=0;i<nodes.Length;i++)
                {
                    if (nodes[i].name == BindNode)
                    {
                        transform.parent = nodes[i];
                        transform.localPosition = Vector3.zero;
                        transform.localRotation = Quaternion.identity;
                        transform.localScale = Vector3.one;
                        //AddComponent(
                        break;
                    }
                }
            }
            else
            {
                if (bParabola)
                {
                    float initHeight = (int)info["High"] * 0.001f;
                    float maxHeight = 4.0f;
                    float ftime = (float)life * 0.001f;

                    Vector3 targetpos = (Vector3)param.userdata5;
                    Vector3 currentpos = (Vector3)param.pos;
                    
                    Direction = targetpos - currentpos;
                    g = -9.8f;
                    Vector3 v = transform.position;
                    v.y += initHeight;
                    transform.position = v;

                    initHeight -= Direction.y;

                    Direction = new Vector3(Direction.x / ftime, (Direction.y - initHeight) / ftime, Direction.z / ftime);

                    float i = maxHeight - initHeight;
                    float ii = i * i;
                    float velocity = (2 * i + 2 * Mathf.Sqrt(ii + initHeight * i)) / ftime;
                    g = -velocity * velocity * 0.5f / i;

                    Direction.y = velocity;
                    //Direction.y += -0.5f*g*ftime;
                    initY = transform.position.y;


                    

                    //initSpeed = initHeight / ftime;
                }
                else
                {
                    Direction.z = (float)Speed * 0.001f;
                    Direction = param.rot * Direction;
                }
            }
			
			if(param.userdata2 != null && param.userdata2.GetType() == typeof(int))
				skillID     =    (int)param.userdata2;

			object iEffectid	=	info["sSkillEffect[8]"];
			if(iEffectid!=null)
			{
				int[] idArray	=	(int[])iEffectid;
				skillEffect		=	new SkillEffect[idArray.Length];
				for(int i=0;i<idArray.Length;i++)
				{
					int effid	=	idArray[i];				
					Hashtable skillEffectArray = actor.GetSkillEffect();
					SkillEffect	effect	= skillEffectArray[effid] as SkillEffect;
					skillEffect[i]	=	effect;	
				}
			}
			cb.summon	=	this;
		
			sdBaseState.AddSkillEffect(castActor,HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_START,0,skillEffect,null,transform, skillID);
		
			//colli	=	GetComponent<CapsuleCollider>();
		}
		void	Start()
		{
			
			layermask		=	LayerMask.NameToLayer("Monster");
			lastPostion		=	transform.position;
		}
		void	End()
		{
            sdBaseState.AddSkillEffect(castActor, HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_END, -1, skillEffect, null, transform, skillID);
			GameObject.Destroy(gameObject);
			life	=	0;
		}
		void	Update()
		{	
			if(life==0)
			{
				Debug.Log("Life 0");
				return;
			}
			CurrentLife	+=	(int)(Time.deltaTime*1000.0f);
			if(CurrentLife>life)
			{
				End();
			}
			if(GetComponent<Collider>().GetType() == typeof(SphereCollider))
			{
				SphereCollider sphere =	(SphereCollider)GetComponent<Collider>();
				lastPostion		=	transform.TransformPoint(sphere.center);
			}

			if(bFly)
			{
				transform.position	+=	Direction*Time.deltaTime;
                if (bParabola)
                {
                    float t = CurrentLife*0.001f;
                    Vector3 v = transform.position;
                    v.y =   initY + Direction.y * t + 0.5f * g * t * t;
                    transform.position = v;
                }
			}
			
			if(Direction.magnitude < 0.1f)
			{
				colliShape.SetInfo(GetComponent<Collider>());
			}
			else
			{
				colliShape.SetCCDInfo(GetComponent<Collider>(),lastPostion);
			}
			
			if(dwDelayDamage <= 0)
			{
				if(bDelayExplosion)
				{
					bDelayExplosion = false;
					object	ExplosionEffect	=	info["ExplosionEffect"];				
					if(ExplosionEffect!=null)
					{
						string	strExplosion	=	ExplosionEffect as string;
						if(strExplosion.Length>0)
						{
							int		ExplosionEffectLife	=	(int)info["ExplosionEffectLife"];
							float flife	=	(float)ExplosionEffectLife*0.001f;
							sdGameLevel.instance.PlayEffect(strExplosion, 
						                                	transform.position, 
						                                	Vector3.one, 
						                                	Quaternion.identity,
						                                	flife);
						}
					}					
				}
				int nCombo = CheckDamage();
                if (nCombo > 0 && castActor == sdGameLevel.instance.mainChar)
                    sdUICharacter.Instance.ShowComboWnd(true, nCombo);
			}
			else
				dwDelayDamage -= (int)(Time.deltaTime*1000.0f);

			UpdateDirection();
		}


	//鏇存柊椋炶?鏂瑰悜aaa
	void   UpdateDirection()
	{
		if(bParabola)
		{
            //Direction.y += g*Time.deltaTime;
		}
		else
		{
			if(targetActor != null)
			{
				if(targetActor.GetCurrentHP() > 0)
				{
					Vector3 v =  targetActor.transform.position - transform.position;
					Vector3 vTemp	=	v-Direction;
					float fSpeed	=	Direction.magnitude;
					Direction+=vTemp*Time.deltaTime*20.0f;
					Direction.y=0.0f;
					Direction.Normalize();
					Direction	=	Direction*fSpeed;	
				}
			}
		}
	}

	//浼ゅ?妫娴媋aa
	int   CheckDamage()
	{
        int nRet = 0;
		HeaderProto.ESkillEffect eSkilleffect = HeaderProto.ESkillEffect.SKILL_EFFECT_DAMAGE_HP;
		if(info.ContainsKey("bySkillEffect"))
            eSkilleffect = (HeaderProto.ESkillEffect)(info["bySkillEffect"]);
		//鍛ㄦ湡鎬у彫鍞ょ墿
		if(PeriodTime!=0)
		{
			TriggerCount	=	0;
			int iCurrentCount	=	CurrentLife/PeriodTime;
			TriggerCount		=	iCurrentCount	-	PeriodCount;
			PeriodCount			=	iCurrentCount;
			
			if(TriggerCount!=0)
			{
				sdBaseState.AddSkillEffect(castActor,HeaderProto.ESkillTimingConditionType.SKILL_TIMING_CONDITION_TYPE_HIT,0,skillEffect,null,transform, skillID);
				//璁＄畻浼ゅ?
				List<sdActorInterface> lstActor =	FindActor();
				if(lstActor!=null)
				{
					foreach(sdActorInterface a in lstActor)
					{
						if(a.IsActive()&&a.GetCurrentHP()>0)
						{	
							List<sdActorInterface> listActor = new List<sdActorInterface>();
							listActor.Add(a);
                            nRet += OnHit(listActor, eSkilleffect);
						}
					}
				}
				bool shakeCamera	=	((int)info["shakeCamera"] == 1);
				if(shakeCamera)
				{
					float	shakelevel	=	((int)info["ShakeLevel"])*0.0001f;
					sdGameLevel.instance.mainCamera.addRandomCameraShake(0.3f, 0.3f*shakelevel, 60.0f, 3.0f);
				}
			}
            return nRet;
		}
		//鍙?敓鏁堜竴娆＄殑鎶鑳既
		if(HitCount	==	1)
		{
			sdActorInterface	a	=	FindOneActor();
			if(a!=null){
				if(a.IsActive()&&a.GetCurrentHP()>0)
                {
                   nRet += CalcDamage(a);
                   End();
                    
				}
			}
            return nRet;
		}
		//浼氱敓鏁堝?娆＄殑鎶鑳忌		if(HitCount	>	1)
		{
			List<sdActorInterface> lstActor =	FindActor();
			if(lstActor!=null)
			{
				
				for(int i=0;i<lstActor.Count;i++)
				{
					sdActorInterface a = lstActor[i];
					bool bInLastList	=	false;
					for(int j=0;j<LastActorList.Count;j++)
					{
						if(a == lstActor[j])
						{
							bInLastList = true;
							break;
						}
					}
					if(!bInLastList)
					{
						nRet += CalcDamage(a);
						iCurrentHit++;
						if( iCurrentHit >= HitCount)	
						{
							End();
						}
					}
				}
				LastActorList	=	lstActor;
			}
			else
			{
				LastActorList.Clear();
			}
		}
        return nRet;
	}

	
	int	CalcDamage(sdActorInterface monster)
	{
		bool shakeCamera	=	((int)info["shakeCamera"] == 1);
		if(shakeCamera)
		{
			float	shakelevel	=	((int)info["ShakeLevel"])*0.0001f;
			sdGameLevel.instance.mainCamera.addRandomCameraShake(0.3f, 0.3f*shakelevel, 60.0f, 3.0f);
		}
		//鎾?斁鍛戒腑涔嬪悗鐨勭垎鐐哥壒鏁囘濡傛灉瀛樺湪鐖嗙偢鐗规晥 鍒欓渶瑕佺敤summon涓?寖鍥撮噸鏂拌?绠椾激瀹冲垽瀹欗
		object	ExplosionEffect	=	info["ExplosionEffect"];
			
		if(ExplosionEffect!=null)
		{
			string	strExplosion	=	ExplosionEffect as string;
			if(strExplosion.Length>0)
			{
                if (bPlayExplosion)
                {
                    int ExplosionEffectLife = (int)info["ExplosionEffectLife"];
                    float flife = (float)ExplosionEffectLife * 0.001f;
                    sdActorInterface.AddHitEffect(strExplosion, null, flife, transform.position, true);
                }

                return sdGameLevel.instance.battleSystem.DoSDAttack(
                    castActor,
                    info,
                    transform.position,
                    0,
                    cb);
            }
		}
        HeaderProto.ESkillEffect eSkilleffect = HeaderProto.ESkillEffect.SKILL_EFFECT_DAMAGE_HP;
        if (info.ContainsKey("bySkillEffect"))
            eSkilleffect = (HeaderProto.ESkillEffect)(info["bySkillEffect"]);
        List<sdActorInterface> lstMonster = new List<sdActorInterface>();
        lstMonster.Add(monster);
        return OnHit(lstMonster, eSkilleffect);			
	}
		public	int	OnHit(List<sdActorInterface> lstactor, HeaderProto.ESkillEffect eSkileffect)
		{
            int CalcDamage  =   (int)info["CalcDamage"];            
            info["ParentID"] = skillID * 100;
            int nRet = 0;
            List<Bubble.BubbleType> lstBubbleType = new List<Bubble.BubbleType>();
            for (int index = 0; index < lstactor.Count; ++index)
            {
                sdActorInterface actor = lstactor[index];

                if (PeriodTime == 0)
                {
                    if (!actor.IsCanSummonAttack(uniqueID))
                    {
                        lstBubbleType.Add(Bubble.BubbleType.eBT_Max);
                        continue;
                    }
                    actor.SetSummonAttackInfo(uniqueID);
                }
                DamageResult dr;
                dr.damage = 0;
                if (CalcDamage != 0)
                {
                    dr = sdGameLevel.instance.battleSystem.testHurt(castActor, info, actor, 0, eSkileffect);
                    lstBubbleType.Add(dr.bubbleType);
                    if (Bubble.IsHurtOther(dr.bubbleType))
                        ++nRet;
                }
                else
                    lstBubbleType.Add(Bubble.BubbleType.eBT_Max);

                string hitEffect = info["HitEffect"] as string;
                if (hitEffect.Length > 0 && dr.damage > 0)
                {
                    int life = (int)info["HitEffectLife"];
                    Transform bindnode = actor.transform;
                    if (actor.actorType == ActorType.AT_Player)
                    {
                        if (info.ContainsKey("HitDummy"))
                        {
                            int pos = (int)info["HitDummy"];
                            if (pos == 1)
                            {
                                sdGameActor gameactor = (sdGameActor)actor;
                                GameObject chest = gameactor.GetNode("Bip01 Spine");
                                if (chest != null)
                                    bindnode = chest.transform;
                            }
                        }
                    }
                    else
                    {
                        sdGameMonster monster = (sdGameMonster)actor;
                        if (monster.ChestPoint.Length > 0)
                        {
                            GameObject chest = monster.GetNode(monster.ChestPoint.Replace("\r\n", ""));
                            if (chest != null)
                            {
                                bindnode = chest.transform;
                            }
                        }
                    }
                    sdGameActor.AddHitEffect(hitEffect, bindnode, life * 0.001f, Vector3.zero, true);
                }
            }            
			sdBaseState.AddSkillEffect(castActor,lstactor, lstBubbleType, skillEffect,transform,skillID);
            return nRet;
		}
	}