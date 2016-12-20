using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public	class sdEffectOperation
{
	public	class Operation
	{
		public	int		dwID;
		public	string	szName;
		public	int	byOperationType;
		public	int	dwOperationPara0;
		public	int	dwOperationPara1;
		public	int	dwOperationPara2;
		public	int	dwOperationPara3;
		public  int dwOperationPara4;
	}
	public	class OpParameter
	{
		public	int id;
		public	int data;
		public	int	data1;
		public  int data2;
		public 	int data3;
		public	int	layer;
		public	int	doType;
		public	sdActorInterface	attackActor;
		public	sdActorInterface 	targetActor;
		public	Transform	trans;
		
	}
	
	public	static 	int		ModifyProperty(ref int dst,int iAdd,int imin,int imax)
	{
		int ret	=	iAdd;
		int val	=	(dst);
		ret=val+iAdd;
		if(ret > imax)
		{
			ret	=	imax;
		}
		if(ret < imin)
		{
			ret	=	imin;
		}
		dst = ret;
		return ret - val;
	}
	public	static	int		GetModifyData(Operation op ,OpParameter param,int maxValue)
	{
		switch(op.dwOperationPara3)
		{
			case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_INTEGER:{
				return param.data;
			}//break;
			case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_PERCENT:{

                float value = (maxValue * param.data) / 10000.0f;
                return (int)Mathf.Ceil(value);
			}//break;
		}
		return 0;
	}
	public	static	int 	ModifyProperty_Template(string val,string max,Operation op ,OpParameter param,Hashtable dst)
	{
		int imax	=	(int)dst[max];
		int imin	=	0;
		int iValue	=	(int)dst[val];
		
		int iModifyData	=	GetModifyData(op,param,imax);
		if(param.data1>0)
		{
			int iLimit	=	(param.data1*imax)/10000;
			if(param.data	<	0)
			{
				imin	=	iLimit;
			}
			else
			{
				imax	=	iLimit;
			}
		}
		int ret =	ModifyProperty(ref iValue,iModifyData,imin,imax);
		//dst[val]	=	iValue;
		return ret;
	}
	public	static	void	AddProperty(sdActorInterface actor,Hashtable dst,Operation op ,OpParameter param)
	{
		switch(op.dwOperationPara0)
		{
			case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_MOD_HP:{
				int hp	=	ModifyProperty_Template("HP","MaxHP",op,param,dst);
				actor.AddHP(hp);
			}break;
			case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_MOD_SP:{
				int sp	=	ModifyProperty_Template("SP","MaxSP",op,param,dst);
				actor.AddSP(sp);
			}break;
		}
	}
	/*
	  CREATURE_ACTION_STATE_STAY" value="0" desc="束缚 不能移动，位移技能的位移无效 " />
	  CREATURE_ACTION_STATE_HOLD"  desc="昏睡 全禁止，被攻击就醒 " />
	  CREATURE_ACTION_STATE_STUN"  desc="昏迷 全禁止，被攻击也不醒 " />
	  CREATURE_ACTION_STATE_LIMIT_SKILL"  desc="禁魔 不能使用技能，包括普攻 " />
	  CREATURE_ACTION_STATE_UNBEAT"  desc="无敌状态 不受debuff和伤害和控制 " 
	 */
    public static int iSummonUniqueID = 0;
	public	static	bool	Do(sdActorInterface actor,OpParameter param)
	{		
		Hashtable	table	=	sdConfDataMgr.Instance().GetTable("operation");

		Operation op	=	(Operation)table[param.id];

		switch(op.byOperationType)
		{
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_NONE:{
				return true;
			}
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_FOREVER:{
				if(	param.doType == (int)HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START	||
					param.doType == (int)HeaderProto.EDoOperationType.DO_OPERATION_TYPE_ADD		
				)
				{
					if(actor==null)
					{
						return false;
					}
					Hashtable dst		=	actor.GetProperty();
					switch(op.dwOperationPara1)
					{
						case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_ADD:{
					
							AddProperty(actor,dst,op,param);
						}break;
						case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_REDUCE:{
						
							param.data=-param.data;
							AddProperty(actor,dst,op,param);
						}break;
						case (int)HeaderProto.EOpreationFlag.OPREATION_FLAG_SET:{}break;
					}
				}
				
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_STATE:{
				if(actor==null)
				{
					return false;
				}
				if(	param.doType == (int)HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START)
				{
					HeaderProto.ECreatureActionState state	=	(HeaderProto.ECreatureActionState)param.data;
					actor.AddDebuffState(state);	
				}
				else if(param.doType == (int)HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE)
				{					
					HeaderProto.ECreatureActionState state	=	(HeaderProto.ECreatureActionState)param.data;
					actor.RemoveDebuffState(state);
				}
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_ADD_BUFF:{
				if(actor==null)
				{
					return false;
				}
				actor.AddBuff(param.data,param.data1,param.attackActor);
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_DISPEL:{
				if(actor == null)
					return false;
                if (op.dwOperationPara0 == 0)
                {
                    actor.RemoveBuffbyClassType(op.dwOperationPara1);
                }
                else if (op.dwOperationPara0 == 1)
                {
                    actor.RemoveBuff(op.dwOperationPara1, op.dwOperationPara2);
                }
                else if(op.dwOperationPara0 == 2)
                {
                    actor.RemoveBuffbyID(param.data);
                }
                else if (op.dwOperationPara0 == 3)
                {
                    actor.RemoveBuffbyProperty(op.dwOperationPara1);
                }
                else if (op.dwOperationPara0 == 4)
                {
                    
                }
                else if (op.dwOperationPara0 == 5)
                {
                    actor.RemoveAllBuff();
                }
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_DO_BUFF_DAMAGE:
			{
				if(param.targetActor==null)
				{
					return false;
				}
				Hashtable action	=	sdConfDataMgr.Instance().m_BuffAction[param.data]	as Hashtable;
			
				int strikeType = (int)action["byAoeAreaType"];
                int nCombo = 0;
				if(strikeType	==	0)
				{
					HeaderProto.ESkillEffect skilleffect = HeaderProto.ESkillEffect.SKILL_EFFECT_DAMAGE_HP;
					if(action.ContainsKey("bySkillEffect"))
					{
						skilleffect = (HeaderProto.ESkillEffect)(action["bySkillEffect"]);
					}
					action["ParentID"] = 0;
					DamageResult dr	=	sdGameLevel.instance.battleSystem.testHurt(param.attackActor,
											action,
											param.targetActor,
											0,
											skilleffect);
                    if (Bubble.IsHurtOther(dr.bubbleType))
                        nCombo++;
				}
				else
				{
                    nCombo = sdGameLevel.instance.battleSystem.DoSDAttack(
						param.attackActor,
						action,
						param.targetActor.transform.position,
						0, 
						null);
				}
                if (param.attackActor == sdGameLevel.instance.mainChar && nCombo > 0)
                    sdUICharacter.Instance.ShowComboWnd(true, nCombo);
			}break;
		case (int)HeaderProto.EOpreationType.OPREATION_TYPE_CLEAR_SKILL_COOLDOWN:
			{
				sdGameActor gameActor = (sdGameActor)actor;
				if(op.dwOperationPara0 == 0) //某个技能aa
				{
					int skillID = param.data/100;
					sdSkill skill = gameActor.skillTree.getSkill(skillID);
					if(skill != null)
					{
						if(op.dwOperationPara1 == 0)
						   skill.skillState = (int)sdSkill.State.eSS_OK;
						else if(op.dwOperationPara1 == 1)
						{
							int time = CalculateSkillCoolDown(op.dwOperationPara2, op.dwOperationPara3, skill.GetCD(), param.data1);
							sdUICharacter.Instance.SetShortCutCd(param.data, time, true);
							skill.cooldown = time;
							skill.Setct(0.0f);
						}
					}
				}
				else if(op.dwOperationPara0 == 1)//某种形态的技能aa
				{
					foreach(DictionaryEntry de in gameActor.skillTree.AllSkill)
					{
						sdSkill skill = de.Value as sdSkill;
						if(skill.skillProperty.ContainsKey("byShape"))
						{
							int byShape = (int)skill.skillProperty["byShape"];
							if(byShape == param.data)
							{
								if(op.dwOperationPara1 == 0)
									skill.skillState = (int)sdSkill.State.eSS_OK;
								else if(op.dwOperationPara1 == 1)
								{
									int time = CalculateSkillCoolDown(op.dwOperationPara2, op.dwOperationPara3, skill.cooldown, param.data1);
									sdUICharacter.Instance.SetShortCutCd(skill.id, time, true);
									skill.cooldown = time;
									skill.Setct(0.0f);	
								}
							}
						}
					}
				}
				else if(op.dwOperationPara0 == 2)//所有技能aa
				{
					foreach(DictionaryEntry de in gameActor.skillTree.AllSkill)
					{
						sdSkill skill = de.Value as sdSkill;
						if(op.dwOperationPara1 == 0)
							skill.skillState = (int)sdSkill.State.eSS_OK;
						else if(op.dwOperationPara1 == 1)
						{
							int time = CalculateSkillCoolDown(op.dwOperationPara2, op.dwOperationPara3, skill.cooldown, param.data1);
							sdUICharacter.Instance.SetShortCutCd(skill.id, time, true);
							skill.cooldown = time;
							skill.Setct(0.0f);
						}
					}
				}
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_SUMMON_MONSTER:  //operator.txt byOperationType
			{
                iSummonUniqueID++;
				if(op.dwOperationPara0==1)
				{
					int	iAngle			=	op.dwOperationPara2;
					int iSummonID		=	param.data;
					int	iSommonCount	=	param.data1;
					int skillID         =   param.data2;
					Vector3 v	=	param.attackActor.GetDirection();
					v.y=0.0f;
					v.Normalize();
					Quaternion tempQ = Quaternion.FromToRotation(new Vector3(0,0,1),v);
					if(v.z<-0.9999f)
					{
						tempQ	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));
					}
				
					List<sdActorInterface>	lstActor	=	null;
					if(op.dwOperationPara3 == 1 || op.dwOperationPara3 == 2) //1 方向并跟踪目标  2.方向不跟踪aaa
					{
						List<sdActorInterface> actorList = null;
						if(iAngle == 360)
						{
							actorList = sdGameLevel.instance.actorMgr.FindActor(
							param.attackActor,
							HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
							param.attackActor.transform.position,
							Vector3.zero,
							1,
							0,
							15.0f,
							true);
						}
						else
						{
							actorList = sdGameLevel.instance.actorMgr.FindActor(
							param.attackActor,
							HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
							param.attackActor.transform.position,
							param.attackActor.GetDirection(),
							5,
							iAngle,
							15.0f,
							true);
						}
						lstActor = sdGameLevel.instance.actorMgr.SortActor(param.attackActor.transform.position, actorList);						
					}			

					int i = 0;
					if(op.dwOperationPara3 == 1)
					{
						for(; i < iSommonCount && i < lstActor.Count; i++)
						{
							sdActorInterface target = lstActor[i];
							SummonInfo sumInfo = new SummonInfo();
							Vector3 vDir = target.transform.position - param.attackActor.transform.position;
							vDir.y = 0.0f;
                            vDir.Normalize();
							if(v.z < -0.99999f)
								sumInfo.rotate = Quaternion.AngleAxis(180.0f, new Vector3(0,1,0));
							else
								sumInfo.rotate = Quaternion.FromToRotation(new Vector3(0,0,1), vDir);
							sumInfo.pos	=	param.trans.position;							
							sumInfo.userdata = target;
							sumInfo.summonID = iSummonID;
							sumInfo.skillID = skillID;
                            param.attackActor.AddSummon(sumInfo, iSummonUniqueID);
						}
					}
					else if(op.dwOperationPara3 == 2)
					{
                        
						for(; i < iSommonCount && i < lstActor.Count; i++)
						{
							sdActorInterface target = lstActor[i];
							Vector3 vDir = target.transform.position - param.attackActor.transform.position;
                            vDir.y = 0.0f;
                            vDir.Normalize();
							SummonInfo sumInfo = new SummonInfo();
							if(v.z < -0.99999f)
								sumInfo.rotate = Quaternion.AngleAxis(180.0f, new Vector3(0,1,0));
							else
								sumInfo.rotate = Quaternion.FromToRotation(new Vector3(0,0,1), vDir);
							sumInfo.pos	=	param.trans.position;
							sumInfo.summonID = iSummonID;
							sumInfo.skillID = skillID;
                            param.attackActor.AddSummon(sumInfo, iSummonUniqueID);
						}
					}
					if(i < iSommonCount)
					{
						if(iAngle==360)
						{
							float iPerAngle	=	(float)iAngle/(float)(iSommonCount - i);
							for(int j = 0;j<iSommonCount - i;j++)
							{
								SummonInfo sumInfo = new SummonInfo();
								sumInfo.rotate=Quaternion.AngleAxis(iPerAngle*j,new Vector3(0,1,0))*tempQ;									
								sumInfo.pos	=	param.trans.position;
								sumInfo.summonID = iSummonID;
								sumInfo.skillID = skillID;
                                param.attackActor.AddSummon(sumInfo, iSummonUniqueID);
							}
						}
						else
						{
							float iPerAngle	=	(float)iAngle/(float)(iSommonCount - i + 1);
							float iAngleStart	=	-iAngle*0.5f+iPerAngle;
							for(int j=0;j<iSommonCount - i;j++)
							{
								SummonInfo sumInfo = new SummonInfo();
								sumInfo.rotate	=	Quaternion.AngleAxis(iAngleStart+iPerAngle*j,new Vector3(0,1,0))*tempQ;
								sumInfo.pos	=	param.trans.position;
								sumInfo.summonID = iSummonID;
								sumInfo.skillID = skillID;
                                param.attackActor.AddSummon(sumInfo, iSummonUniqueID);
							}
						}
					}
				}
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_TELEPORT:
			{
				TeleportInfo tpInfo			=	new TeleportInfo();
				tpInfo.Type					=	op.dwOperationPara0;
				tpInfo.PlayActionProbality 	=   op.dwOperationPara1;
				tpInfo.TeleportState        =   (op.dwOperationPara2 == 1);
				tpInfo.MoveSpeed	=	(float)param.data*0.001f;
				tpInfo.MoveTime		=	(float)param.data1*0.001f;
				tpInfo.castActor	=	param.attackActor;
				tpInfo.castCenter	=	param.attackActor.transform.position;
				if(actor!=null)
				{
					actor.SetTeleportInfo(tpInfo);
				}
				else
				{
					float dis	=	tpInfo.MoveSpeed*tpInfo.MoveTime;
					List<sdActorInterface> lstActor	=	sdGameLevel.instance.actorMgr.FindActor(param.attackActor,HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,tpInfo.castCenter,Vector3.zero,1,0,dis,true);
					if(lstActor!=null)
					{
						foreach(sdActorInterface a in lstActor)
						{
							a.SetTeleportInfo(tpInfo);
						}
					}
				}
			}break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_DOACTION:
			{
				sdGameActor gameActor = (sdGameActor)(param.attackActor);			
				sdBaseState	baseState	=	(sdBaseState)gameActor.logicTSM.states[param.data];
				if(baseState != null)
				{
					int nCombo = sdGameLevel.instance.battleSystem.DoSDAttack(gameActor, baseState.stateData, gameActor.transform.position, 0, baseState);
					baseState.playEffectNow(gameActor);
					baseState.PlayAudioNow(gameActor);
                    if (gameActor == sdGameLevel.instance.mainChar && nCombo > 0)
                        sdUICharacter.Instance.ShowComboWnd(true, nCombo);
				}
			}
			break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_COMBO:
			{
				if(param.attackActor.GetActorType() == ActorType.AT_Player)
				{
					sdGameActor gameActor = (sdGameActor)(param.attackActor);
					sdBaseState baseState = (sdBaseState)gameActor.logicTSM.states[param.data];
					if(baseState != null)
					{
						baseState.bEnable = (param.data1 == 1);
					}
				}
			}
			break;
			case (int)HeaderProto.EOpreationType.OPREATION_TYPE_ATTACKGETHP:
			{
				AttackRestore  data = new AttackRestore();
				data.upperLimit 	= op.dwOperationPara1*0.0001f;
				data.restoreHp 	= (op.dwOperationPara0 == 0);
				data.monsterType   = op.dwOperationPara2;
				data.ratio     	= param.data*0.0001f;
				data.actionID      = param.data1;
				sdGameActor gameActor = (sdGameActor)(param.attackActor);
				gameActor.AddAttackRestore(data);
			}
			break;
			case (int)HeaderProto.EOpreationType.OPERATION_TYPE_SUMMON:
			{
                iSummonUniqueID++;
				switch (op.dwOperationPara0)
				{
					case 0:
					case 1:
						PointSummon(actor, param, op);
						break;
					case 2:
					case 3:
						RandomSummon(actor, param, op);
						break;
					case 4:
					case 5:
						RoundSummon(actor,param, op);
						break;
				}
			}
			break;
			case (int)HeaderProto.EOpreationType.OPERATION_TYPE_HIDESHOW:
			{
				HideShowInfo info = new HideShowInfo();
				info.actor = param.attackActor;
				info.fHideTime = param.data*0.001f;
				info.fDistance = param.data1*0.001f;
				sdHideShowMgr.Instance.AddActor(info);
			}
            break;
            case (int)HeaderProto.EOpreationType.OPERATION_TYPE_FLASH:
            {
                float fFront = 1.0f;
                if ((op.dwOperationPara0 & 1) == 1)
                {
                    fFront = -1.0f;
                }
                float fDistance = (float)op.dwOperationPara3 * 0.001f;

                sdActorInterface castActor = param.attackActor;

                int playerLayer     = 1<<LayerMask.NameToLayer("Player");
                int petLayer    = 1<<LayerMask.NameToLayer("Pet");
                int monsterLayer    = 1<<LayerMask.NameToLayer("Monster");
                int mask = ~(playerLayer | petLayer | monsterLayer);

                switch (op.dwOperationPara0)
                {
                    case 0:
                    case 1:
                        {
                            fDistance = (float)param.data1 * 0.001f;
                            Vector3 dir = castActor.GetDirection() * fFront;
                            Vector3 pos = castActor.transform.position;

                            

                            int oldLayer    =   castActor.gameObject.layer;
                            int[] layer = new int[]{
                                                        LayerMask.NameToLayer("Monster"),
                                                        LayerMask.NameToLayer("Pet"),
                                                        LayerMask.NameToLayer("Player")
                                                     };
                            bool[] oldcollision = new bool[3];
                            for (int i = 0; i < 3;i++ )
                            {
                                oldcollision[i] = Physics.GetIgnoreLayerCollision(oldLayer, layer[i]);
                                Physics.IgnoreLayerCollision(oldLayer, layer[i]);
                            }

                            ((sdGameActor)castActor).moveInternal(dir * fDistance/Time.deltaTime);

                            for (int i = 0; i < 3; i++)
                            {
                                Physics.IgnoreLayerCollision(oldLayer, layer[i], oldcollision[i]);
                            }
                            castActor.gameObject.layer = oldLayer;

                            Vector3 newPos = castActor.transform.position;

                            sdGameLevel.instance.actorMgr.ManualCheckTrigger((sdGameActor)castActor, pos, newPos-pos);

                            if (param.data > 0)
                            {
                                HideShowInfo info = new HideShowInfo();
                                info.actor = castActor;
                                info.fHideTime = param.data * 0.001f;
                                info.fDistance = 0.0f;
                                sdHideShowMgr.Instance.AddActorNoRandomPosition(info);
                            }
                           
                        }
                        break;
                    case 2:
                    case 3:
                        {
                            float fResearchDistance = param.data1 * 0.001f;
                            int iAngle = op.dwOperationPara1;
                            List<sdActorInterface> actorList = null;
                            if (iAngle == 360)
                            {
                                actorList = sdGameLevel.instance.actorMgr.FindActor(
                                    param.attackActor,
                                    HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
                                    param.attackActor.transform.position,
                                    Vector3.zero,
                                    1,
                                    0,
                                    fResearchDistance,
                                    true);
                            }
                            else
                            {
                                actorList = sdGameLevel.instance.actorMgr.FindActor(
                                    param.attackActor,
                                    HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
                                    param.attackActor.transform.position,
                                    param.attackActor.GetDirection(),
                                    5,
                                    iAngle,
                                    fResearchDistance,
                                    true);
                            }

                            sdActorInterface    targetActor =   null;
                            if (actorList != null)
                            {
                                if (actorList.Count > 0)
                                {
                                    if (op.dwOperationPara2 == 0)
                                    {
                                        targetActor = actorList[0];
                                        float min = (castActor.transform.position - targetActor.transform.position).sqrMagnitude;
                                        for (int i = 1; i < actorList.Count; i++)
                                        {
                                            Vector3 v = castActor.transform.position - actorList[i].transform.position;
                                            if (v.sqrMagnitude < min)
                                            {
                                                min = v.sqrMagnitude;
                                                targetActor = actorList[i];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int index   =   Random.Range(0,actorList.Count);
                                        if(index>=actorList.Count)
                                        {
                                            index   =   0;   
                                        }
                                        targetActor = actorList[index];
                                    }
                                }
                            }

                            if (targetActor != null)
                            {
                                if (fDistance < targetActor.getRadius() + castActor.getRadius())
                                {
                                    fDistance = targetActor.getRadius() + castActor.getRadius();
                                }
                                Vector3 dir = targetActor.GetDirection() * fFront;
                                Vector3 pos = targetActor.transform.position + new Vector3(0, 0.1f, 0);
                                RaycastHit hit;
                                if (Physics.Raycast(pos, dir, out hit, fDistance * 2, mask))
                                {
                                    if (hit.distance < fDistance)
                                    {
                                        fDistance = hit.distance;
                                    }
                                }

                                castActor.transform.position = targetActor.transform.position + dir * fDistance;

                                dir.y=0.0f;
                                dir.Normalize();

                                ((sdGameActor)castActor).spinToTargetDirection(-dir, true);
                                if (param.data > 0)
                                {
                                    HideShowInfo info = new HideShowInfo();
                                    info.actor = castActor;
                                    info.fHideTime = param.data * 0.001f;
                                    info.fDistance = 0.0f;
                                    sdHideShowMgr.Instance.AddActorNoRandomPosition(info);
                                }
                            }
                        
                        }break;
                }
            }
            break;
		}				
		return true;
	}	
	
	public static int CalculateSkillCoolDown(int type, int minimum, int current, int nValue)
	{
		int nResult = current;
		if(type == 0) //按照数值减少aa
		{
			nResult = current - nValue;			
		}
		else if(type == 1)//按照百分比减少aa
		{
			nResult = (int)(nResult*(1.0f - nValue/100.0f));
		}
		return nResult<minimum?minimum:nResult;
	}
	
	public static void OnLoadAlarmEffect(ResLoadParams param,Object obj)
	{
		if(obj == null)
		{
			Debug.Log("arlarm effect is null");
			return;
		}
		GameObject alarmEffect = GameObject.Instantiate(obj) as GameObject;
		if(alarmEffect != null)
		{
			float fScale = (float)param.userdata1;
			alarmEffect.transform.position = param.pos;
			alarmEffect.transform.localScale = new Vector3(fScale, fScale, fScale);
			sdAutoDestory autoDestory = alarmEffect.AddComponent<sdAutoDestory>();
			if(autoDestory != null)
			{
				autoDestory.Life = (float)param.userdata0;
			}
		}
	}

	//朝目标点召唤aaa
	public static void PointSummon(sdActorInterface actor,OpParameter param, Operation op)
	{
		//int summonID = param.data;  //召唤物id
		//int skillId = param.data2;
		int iSommonCount = param.data1;  //召唤数量aaa				
		float alarmTime = op.dwOperationPara4*0.001f;   //预警特效时间aaa
		float alarmRadius = op.dwOperationPara2*0.001f; //预警半径aaa
		int iAngle = op.dwOperationPara1; //召唤物在多少角度范围内召唤aaa
		float radius = op.dwOperationPara3*0.001f;
		
		Vector3 v	=	param.attackActor.GetDirection();
		v.y=0.0f;
		v.Normalize();
		Quaternion tempQ = Quaternion.FromToRotation(new Vector3(0,0,1),v);
		if(v.z<-0.9999f)
			tempQ	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));

		List<sdActorInterface> actorList = null;
		List<sdActorInterface> lstActor = null;
		if(iAngle == 360)
		{
			actorList = sdGameLevel.instance.actorMgr.FindActor(
				param.attackActor,
				HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
				param.attackActor.transform.position,
				Vector3.zero,
				1,
				0,
				radius,
				true);
		}
		else
		{
			actorList = sdGameLevel.instance.actorMgr.FindActor(
				param.attackActor,
				HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY,
				param.attackActor.transform.position,
				param.attackActor.GetDirection(),
				5,
				iAngle,
				radius,
				true);
		}
		lstActor = sdGameLevel.instance.actorMgr.SortActor(param.attackActor.transform.position, actorList);									
		int i = 0;
		for(; i < iSommonCount && i < lstActor.Count; i++)
		{
			sdActorInterface target = lstActor[i];
			SummonInfo sumInfo = new SummonInfo();
			ResLoadParams alarmParam = new ResLoadParams();
			sumInfo.summonID = param.data;
			sumInfo.skillID = param.data2;
			if(op.dwOperationPara0 == 0)//朝目标所在直接召唤aaa
			{
				sumInfo.pos = target.gameObject.transform.position;
				alarmParam.pos = sumInfo.pos;
				sumInfo.bFly = false;
			}
			else//朝目标所在点丢东西aaa
			{
				sumInfo.pos = param.trans.position;
				sumInfo.targetPos = target.gameObject.transform.position;
				alarmParam.pos = sumInfo.targetPos;
				sumInfo.bParabola = true;
				sumInfo.bFly = true;
			}
            param.attackActor.AddSummon(sumInfo, iSummonUniqueID);
			alarmParam.userdata0 = alarmTime;
			alarmParam.userdata1 = alarmRadius;
            sdResourceMgr.Instance.LoadResourceImmediately("Effect/MainChar/$Tongyong/yujing/FX_yujing.prefab", OnLoadAlarmEffect, alarmParam);
		}

		if(i < iSommonCount)
		{
			float iPerAngle = 0.0f;
			float iAngleStart = 0.0f;
			if(iAngle==360)
				iPerAngle = (float)iAngle/(float)(iSommonCount - i);
			else
			{
				iPerAngle	=	(float)iAngle/(float)(iSommonCount - i + 1);
				iAngleStart	=	-iAngle*0.5f+iPerAngle;
			}
			for(int j = 0;j< iSommonCount - i;j++)
			{
				SummonInfo sumInfo = new SummonInfo();
				sumInfo.summonID = param.data;
				sumInfo.skillID = param.data2;
				ResLoadParams alarmParam2 = new ResLoadParams();
				sumInfo.pos = param.trans.position;
				Quaternion rot =Quaternion.AngleAxis(iAngleStart + iPerAngle*j,new Vector3(0,1,0))*tempQ;	
				if(op.dwOperationPara0 == 0)//朝目标所在直接召唤aaa
				{
					sumInfo.pos = param.trans.position + rot*Vector3.forward*radius;
					Vector3 hitpos = Vector3.zero;
					if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.pos, ref hitpos))
						sumInfo.pos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
					alarmParam2.pos = sumInfo.pos;
					sumInfo.bFly = false;
				}
				else//朝目标所在点丢东西aaa
				{
					sumInfo.pos = param.trans.position;
					sumInfo.targetPos = param.trans.position + rot*Vector3.forward*radius;
					Vector3 hitpos = Vector3.zero;
					if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.targetPos, ref hitpos))
						sumInfo.targetPos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
					sumInfo.bFly = true;
					sumInfo.bParabola = true;
					alarmParam2.pos = sumInfo.targetPos;
				}
                param.attackActor.AddSummon(sumInfo, iSummonUniqueID);

				alarmParam2.userdata0 = alarmTime;
				alarmParam2.userdata1 = alarmRadius;
                sdResourceMgr.Instance.LoadResourceImmediately("Effect/MainChar/$Tongyong/yujing/FX_yujing.prefab", OnLoadAlarmEffect, alarmParam2);	
			}
		}		
	}
	//目标点周围随机召唤aaa
	public static void RandomSummon(sdActorInterface actor,OpParameter param, Operation op)
	{
		//int summonID = param.data;  //召唤物id
		//int skillId = param.data2;
		int summonCount = param.data1;  //召唤数量aaa				
		float alarmTime = op.dwOperationPara4*0.001f;   //预警特效时间aaa
		alarmTime = 0.5f;
		float alarmRadius = op.dwOperationPara2*0.001f; //预警半径aaa
		int iAngle = op.dwOperationPara1; //召唤物在多少角度范围内召唤aaa
		float radius = op.dwOperationPara3*0.001f;
		
		Vector3 v	=	param.attackActor.GetDirection();
		v.y=0.0f;
		v.Normalize();
		Quaternion tempQ = Quaternion.FromToRotation(new Vector3(0,0,1),v);
		if(v.z<-0.9999f)
			tempQ	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));

		for(int j = 0;j<summonCount;j++)
		{
			SummonInfo sumInfo = new SummonInfo();
			sumInfo.summonID = param.data;
			sumInfo.skillID = param.data2;
			ResLoadParams alarmParam = new ResLoadParams();
			int angle = Random.Range((int)(-iAngle*0.5f), (int)(iAngle*0.5f));
			float distance = Random.Range(0, radius);
			Quaternion rot =Quaternion.AngleAxis(angle,new Vector3(0,1,0))*tempQ;								
			if(op.dwOperationPara0 == 2)//指定点召唤东西aaa
			{
				sumInfo.pos = param.trans.position + rot*Vector3.forward*distance;	
				Vector3 hitpos = Vector3.zero;
				if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.pos, ref hitpos))
					sumInfo.pos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
				sumInfo.bFly = false;
				alarmParam.pos = sumInfo.pos;
			}
			else//指定点丢东西aaa
			{
				sumInfo.pos = param.trans.position;
				sumInfo.targetPos = param.trans.position + rot*Vector3.forward*distance;
				Vector3 hitpos = Vector3.zero;
				if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.targetPos, ref hitpos))
					sumInfo.targetPos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
				sumInfo.bParabola = true;
				sumInfo.bFly = true;
				alarmParam.pos = sumInfo.targetPos;
			}
            param.attackActor.AddSummon(sumInfo, iSummonUniqueID);	
			alarmParam.userdata0 = alarmTime;
			alarmParam.userdata1 = alarmRadius;
            sdResourceMgr.Instance.LoadResourceImmediately("Effect/MainChar/$Tongyong/yujing/FX_yujing.prefab", OnLoadAlarmEffect, alarmParam);	
		}
	}
	//目标点等距离圆形或者扇形aaa
	public static void RoundSummon(sdActorInterface actor,OpParameter param, Operation op)
	{
		//int summonID = param.data;  //召唤物id
		//int skillId = param.data2;
		int summonCount = param.data1;  //召唤数量aaa				
		float alarmTime = op.dwOperationPara4*0.001f;   //预警特效时间aaa
		alarmTime = 0.5f;
		float alarmRadius = op.dwOperationPara2*0.001f; //预警半径aaa
		int iAngle = op.dwOperationPara1; //召唤物在多少角度范围内召唤aaa
		float radius = op.dwOperationPara3*0.001f;
		
		Vector3 v	=	param.attackActor.GetDirection();
		v.y=0.0f;
		v.Normalize();
		Quaternion tempQ = Quaternion.FromToRotation(new Vector3(0,0,1),v);
		if(v.z<-0.9999f)
			tempQ	=	Quaternion.AngleAxis(180.0f,new Vector3(0,1,0));

		float iPerAngle = 0.0f;
		float iAngleStart = 0.0f;
		if(iAngle == 360)
			iPerAngle =	(float)iAngle/(float)(summonCount);
		else
		{
			iPerAngle	=	(float)iAngle/(float)(summonCount + 1);
			iAngleStart	=	-iAngle*0.5f+iPerAngle;
		}
 
		for(int j = 0;j<summonCount;j++)
		{
			SummonInfo sumInfo = new SummonInfo();
			sumInfo.summonID = param.data;
			sumInfo.skillID = param.data2;
			ResLoadParams alarmParam = new ResLoadParams();
			Quaternion rot =Quaternion.AngleAxis(iAngleStart + iPerAngle*j,new Vector3(0,1,0))*tempQ;								
			if(op.dwOperationPara0 == 4)//指定点召唤东西aaa
			{
				sumInfo.pos = param.trans.position + rot*v*radius;	
				Vector3 hitpos = Vector3.zero;
				if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.pos, ref hitpos))
					sumInfo.pos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
				sumInfo.bFly = false;
				alarmParam.pos = sumInfo.pos;
			}
			else//指定点丢东西aaa
			{
				sumInfo.pos = param.trans.position;
				sumInfo.targetPos = param.trans.position + rot*Vector3.forward*radius;
				Vector3 hitpos = Vector3.zero;
				if(sdTuiTuLogic.NavMesh_RayCast(sumInfo.targetPos, ref hitpos))
					sumInfo.targetPos = hitpos + new Vector3(0.0f, 0.1f, 0.0f);
				sumInfo.bParabola = true;
				sumInfo.bFly = true;
				alarmParam.pos = sumInfo.targetPos;
			}

            param.attackActor.AddSummon(sumInfo, iSummonUniqueID);	
			alarmParam.userdata0 = alarmTime;
			alarmParam.userdata1 = alarmRadius;
			sdResourceMgr.Instance.LoadResourceImmediately("Effect/MainChar/$Tongyong/yujing/FX_yujing.prefab", OnLoadAlarmEffect, alarmParam);	
		}
	}

}