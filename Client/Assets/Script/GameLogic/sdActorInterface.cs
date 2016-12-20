using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActorType
{
	AT_None,
	AT_Player,
	AT_Monster,
	AT_Pet,
	AT_NPC,
    AT_Static,

}
public	class TeleportInfo
{
	public	int 				Type;
	public  int                 PlayActionProbality = 10000; 
	public  bool                TeleportState = false;
	public	float				MoveSpeed	=	0.0f;
	public	float				MoveTime	=	-1.0f;
	public	sdActorInterface 	castActor	=	null;
	public	Vector3        		castCenter	=	Vector3.zero;
}

public class SummonInfo
{
	public Vector3  		pos = Vector3.zero;
	public Vector3          targetPos = Vector3.zero;
	public Vector3  		scale = Vector3.one;
	public Quaternion 		rotate = Quaternion.identity;
	public int 				summonID;
	public int              skillID;
	public bool             bParabola = false;
	public bool             bFly  = true;
	public object           userdata = null;
	public object           userdata1 = null;

	//public object           userdata2 = null;
	//public object           userdata3 = null;
}
/// <summary>
/// 游戏角色对象(提供游戏角色对象的逻辑相关机制)
/// </summary>
public	class sdActorInterface	:	sdBufferReceiver
{
	
	private List<sdBuff> buffs = new List<sdBuff>();
	private List<sdBuff> HideBuffs = new List<sdBuff>();
	
	int[]		OldBaseProperty	=	new int[5]{0,0,0,0,0};
	Hashtable	TempProperty	=	new Hashtable();
	static	string[]	PropertyName	=	new string[43]{
		//0--9
		"MaxHP",			
		"MaxSP",			
		"Hptick",			
		"Sptick", 			
		"AtkDmgMin", 		
		"AtkDmgMax", 		
		"Def", 				
		"IceAtt", 			
		"FireAtt", 			
		"PoisonAtt", 	
		//10--19
		"ThunderAtt",		
		"IceDef", 			
		"FireDef", 			
		"PoisonDef", 		
		"ThunderDef",		
		"Pierce", 			
		"Hit", 				
		"Dodge", 			
		"Cri", 				
		"Flex", 
		//20-29
		"CriDmg", 			
		"CriDmgDef", 		
		"BodySize",	 		
		"AttSize", 			
		"AttSpeedModPer", 	
		"MoveSpeedModPer", 	
		"PiercePer", 		
		"HitPer", 			
		"DodgePer", 			
		"CriPer", 
		//30-39
		"FlexPer", 			
		"AttSpeed", 			
		"MoveSpeed",		
		"HurtOtherModify",
		"PhysicsModify",
		"IceModify",
		"FireModify",
		"PoisonModify",
		"ThunderModify",
		"StayModify",
		//40-42	
		"HoldModify",
		"StunModify",
		"BeHurtModify",
	};
	
	public	static string[]	BasePropertyName=	new string[79]{
	//0~9
	"Str",
	"Int",
	"Dex",
	"Sta",
	"Fai",
	"MaxHP",
	"MaxSP",
	"HPTick",
	"SPTick",
	"AtkDmgMin",
	//10~19
	"AtkDmgMax",
	"Def",
	"IceAtt",
	"FireAtt",
	"PoisonAtt",
	"ThunderAtt",
	"IceDef",
	"FireDef",
	"PoisonDef",
	"ThunderDef",
	//20~29
	"Pierce",
	"Hit",
	"Dodge",
	"Cri",
	"Flex",
	"CriDmg",
	"CriDmgDef",
	"BodySize",
	"AttSize",
	"AttSpeedModPer",
	//30~39
	"MoveSpeedModPer",
	"PiercePer",
	"HitPer",
	"DodgePer",
	"CriPer",
	"FlexPer",
	"AttSpeed",
	"MoveSpeed",
    "MaxEP",    //最大活力值aa
    "MaxAP",    //协助点数最大值aa
    //40~49
    "ExpPer",   //经验值 正负 万分比aaa
    "MoneyPer", //金钱 正负 万分比aa
    "Property_CanModMax", //可以被其他模块修正的属性枚举最大值,以下是预留的aa 
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    //50~59
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    //60~63
    "",
    "",
    "",
    "",
	//64~69
    "HP",       //
	"SP",
	"NonMoney",  //游戏币aa
	"NonCash",   //绑定徽章aaa
	"Cash",      //徽章aa
	"HurtOtherModify",
    //70~78
	"PhysicsModify",
	"IceModify",
	"FireModify",
	"PoisonModify",
	"ThunderModify",
	"StayModify",
	"HoldModify",
	"StunModify",
	"BeHurtModify",
	};
	public	static	string[]	SkillActionName=new string[40]
	{
	//0~9
	"dwCostSP",
	"dwIgnoreAction",
	"byTargetType",
	"dwHitPer",
	"dwCriticalPer",
	"dwCriticalDmgPer",
	"wAoeAimNum",
	"nAoeArea",
	"byAoeCenterType",
	"nAoeCenterData",
	//10~19
	"nAoeAreaData",
	"byIgnoreDef",
	"dwIgnoreDefendAmount",
	"dwDamagePer",
	"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
	"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
	"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
	"naMoreDamagePer[MONSTER_BODY_TYPE_max]",
	"bySkillEffect",
	"dwDirectDmgMin",
	//20~29
	"dwDirectDmgMax",
	"dwDmg[10]",
	"dwAtkPowerPer[10]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	"sSkillEffectSelf[8]",
	//30~38
	"skillEffectSelf7",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"sSkillEffect[8]",
	"HealRatio"
	};
	public	static	string[]	SkillPropertyName	=	new string[1]
	{
		"dwCooldown"
	};
	public	static	string[]	SkillEffectName		=	new string[11]
	{
		"byHitCondition",
		"byMonsterBodyTypeFlag",
		"bySelfCheckType",
		"dwSelfBuffID",
		"byTgtCheckType",
		"dwTgtBuffID",
		"dwProbability",
		"byFlag",
		"dwOperationID",
		"dwOperationData",
		"dwOperationData1"
	};
		

	public	Hashtable		Property		=	null;
	public	Hashtable		SkillProperty	=	null;
	public	Hashtable		SkillAction		=	null;
	public	Hashtable		m_SkillEffect	=	null;
	public  Hashtable 		m_summonInfo = null;

	int[]			DebuffState				=	new int[8];
	uint			DebuffStateFlag			=	0;//HeaderProto.EImmunityType
	uint			ImmunityAbilityFlag		=	0;//免疫能力标志位.
	protected 		List<TeleportInfo> tpInfo	=	new List<TeleportInfo>();
	//protected		float	fTeleportTime	=	0.0f;
	public			GroupIDType	groupid		=	GroupIDType.GIT_PlayerA;
	public			ActorType	actorType	=	ActorType.AT_None;
    public int      lastSummonID =  0;

    //是否处于隐藏状态aaa
	protected  bool  m_bHide   =   false;
	public bool    Hide
	{
		set{m_bHide = value;}
		get{return m_bHide;}
	}
    //是否处于播放剧情动画状态aaa
    protected bool m_bMovie = false;
    public bool Movie
    {
        set { m_bMovie = value; }
        get { return m_bMovie; }
    }

	public virtual	int this[string key]
	{
		get
		{
			if(Property != null && Property.ContainsKey(key))
			{
				return (int)Property[key];
			}
			else
			{
				return -1;
			}
		}
		
		set
		{
			Property[key] = (int)value;
		}
	}

	// 唤醒(继承自MonoBehaviour)aa
	protected override void	Awake()
	{
		base.Awake();
		InitHashTable(TempProperty);
	}

	// 启动(继承自MonoBehaviour)aa
	protected override void	Start()
	{
		base.Start();

		// 添加到对应分组aa
		if (sdGameLevel.instance)
			sdGameLevel.instance.actorMgr.AddActor(this);
	}

	// 销毁(继承自MonoBehaviour)aa
	protected virtual void OnDestroy()
	{
		// 从对应分组移除aa
		if (sdGameLevel.instance)
			sdGameLevel.instance.actorMgr.RemoveActor(this);
	}

	public	virtual	bool	IsActive()
	{
		return true;
	}
	public	virtual	ActorType	GetActorType()
	{
		return actorType;
	}
	public	bool	HasImmunityAbility(HeaderProto.EImmunityType type)
	{
		return (ImmunityAbilityFlag&(1<<(int)type))!=0;
	}
	public	sdActorInterface()
	{
		
	}
	public	virtual	void	AddDebuffState(HeaderProto.ECreatureActionState state)
	{

		DebuffState[(int)state]++;
		DebuffStateFlag	|=	(uint)(1<<(int)state);
	}
	public	virtual	void	RemoveDebuffState(HeaderProto.ECreatureActionState state)
	{	
		if(!CheckDebuffState(state))
			return;		
		DebuffState[(int)state]--;
		if(DebuffState[(int)state] == 0)
		{
			DebuffStateFlag		&=	~(uint)(1<<(int)state);
		}
	}
	public	bool	CheckDebuffState(HeaderProto.ECreatureActionState state)
	{
		return DebuffState[(int)state]!=0;
	}

	// 获取角色职业
	public virtual HeaderProto.ERoleJob GetJob()
	{
		return (HeaderProto.ERoleJob)Property["byJob"];
	}

	// 获取角色标准属性表(在基础属性表模版的基础上修订之后,进入战斗之前,的每个角色的属性表)aa
	public virtual Hashtable GetBaseProperty()
	{
		return null;
	}

	// 获取角色属性表(进入战斗之前的每个角色的属性表)aa
	public virtual Hashtable GetProperty()
	{
		return Property;
	}

	// 获取基础技能属性表aa
	public virtual Hashtable GetBaseSkillProperty()
	{
		return null;
	}

	// 获取技能属性表aa
	public virtual Hashtable GetSkillProperty()
	{
		return SkillProperty;
	}

	// 获取基础技能动作表aa
	public virtual Hashtable GetBaseSkillAction()
	{
		return null;
	}

	// 获取技能动作表aa
	public virtual Hashtable GetSkillAction()
	{
		return SkillAction;
	}

	// 设置技能动作表aa
	public virtual void SetSkillAction(Hashtable prop)
	{

	}
	
	public Hashtable GetSummonInfo()
	{
		return m_summonInfo;
	}
	
	public Hashtable GetSkillEffect()
	{
		return m_SkillEffect;
	}

	public	virtual	int	GetCurrentHP()
	{
		if(Property == null)
			return 0;
		return (int)Property["HP"];
	}
	public	virtual	int	GetMaxHP()
	{
		return (int)Property["MaxHP"];
	}
	
	public virtual int GetCurrentSP()
	{
		return (int)Property["SP"];
	}
	
	public virtual int GetMaxSP()
	{
		return (int)Property["MaxSp"];
	}
	public	virtual	void	OnHurt(DamageResult dr)
	{
		AddHP(-dr.damage);
	}
	public	virtual	void	AddHP(int hp)
	{
        if (hp < 0 && CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_UNBEAT))
            return;
		int iCurrentHP			=	(int)Property["HP"];
		iCurrentHP+=hp;
		int maxHP = (int)Property["MaxHP"];
		Property["HP"]	=	iCurrentHP>maxHP?maxHP:iCurrentHP;
	}
	public	virtual	void	AddSP(int sp)
	{
		int iCurrentSP			=	(int)Property["SP"];
		iCurrentSP+=sp;
		int maxSP = (int)Property["MaxSP"];
		Property["SP"]	=	iCurrentSP>maxSP?maxSP:iCurrentSP;
	}
	public virtual	void BuffChange(List<sdBuff> buffList)
	{
	
	}
	public	virtual	Vector3		GetDirection()
	{
		return new Vector3(0,0,1);
	}
	public	virtual	float		getRadius()
	{
		return 0.5f;
	}
    public virtual  float       getHeight()
    {
        return 1.5f;
    }
	public	virtual	GroupIDType			GetGroupID()
	{
		return groupid;
	}
	public	virtual	int			GetTeamID()
	{
		return 0;
	}
	public	virtual	float		GetMoveSpeed()
	{
		int ms	=	(int)Property["MoveSpeed"];
		return (float)ms*0.001f;
	}
	public	bool	AddSummon(SummonInfo info,int iUniqueID)
	{		
		Hashtable	table	=	m_summonInfo[info.summonID] as Hashtable;
		string	prefab		=	table["SummonEffect"] as string;
		ResLoadParams param = new ResLoadParams();

		param.pos	=	info.pos;
		param.scale	=	info.scale;
		param.rot	=	info.rotate;
		param.userdata0	=	info.summonID;
		param.userdata1	=	info.userdata;
		param.userdata2 =   info.skillID;
		param.userdata3 =   info.bParabola;
		param.userdata4 =   info.bFly;
		param.userdata5 =   info.targetPos;
        param.petInt = iUniqueID;

        sdResourceMgr.Instance.LoadResourceImmediately(prefab, OnLoadSummon, param);
		return true;
	}
	protected void OnLoadSummon(ResLoadParams param,UnityEngine.Object obj)
	{
 		GameObject gObj	=	GameObject.Instantiate(obj) as GameObject;
		if(gObj!=null)
		{
			gObj.transform.position		=	param.pos;
			gObj.transform.rotation		=	param.rot;
			gObj.transform.localScale	=	param.scale;
			sdSkillSummon	summon	=	gObj.AddComponent<sdSkillSummon>();
			if(summon!=null)
			{
				summon.SetInfo(this,param);
			}
		}	
	}
	public	static	void	AddHitEffect(
		string prefab,
		Transform bindNode,
		float fLife,
		Vector3	vLocalPos,
		bool bRotate)
	{
		if(prefab.Length==0)
		{
			return;
		}
		ResLoadParams param = new ResLoadParams();

		param.pos	=	vLocalPos;
		param.scale	=	new Vector3(1.0f,1.0f,1.0f);
		param.rot	=	Quaternion.identity;
		param.userdata0	=	bindNode;
		param.userdata1	=	fLife;
		param.userdata2 = 	bRotate;
		sdResourceMgr.Instance.LoadResource(prefab,OnLoadHitEffect,param);
	}
	static void OnLoadHitEffect(ResLoadParams param,UnityEngine.Object obj)
	{
		if(obj==null)
		{
			return;
		}
		GameObject gObj		=	null;
		Transform parent	=	(Transform)param.userdata0;
		if(parent!=null)
		{
			gObj	=	GameObject.Instantiate(obj,parent.position,parent.rotation) as GameObject;
		}
		else
		{
			gObj	=	GameObject.Instantiate(obj,param.pos,param.rot) as GameObject;
		}
		
		if(gObj!=null)
		{
			if(parent!=null)
			{
				bool bRotate = (bool)param.userdata2;
				if(!bRotate)
				{
					gObj.transform.parent = null;
					gObj.transform.localPosition = Vector3.zero;
					gObj.transform.localRotation = Quaternion.identity;
					sdAdjustPosition  adjust = gObj.AddComponent<sdAdjustPosition>();
					adjust.parent = parent;
				}
				else
				{
					gObj.transform.parent				=	parent;
					gObj.transform.localPosition		=	param.pos;
					gObj.transform.localRotation		=	param.rot;
				}
			}
			gObj.transform.localScale			=	param.scale;
			sdAutoDestory	auto	=	gObj.AddComponent<sdAutoDestory>();
			if(auto!=null)
			{
				auto.Life	=	(float)param.userdata1;
			}
		}
	}
	
	static	void	InitHashTable(Hashtable oTable)
	{
		foreach(string str in PropertyName)
		{
			oTable[str] 			= (int)0;
		}
	}
	static	void	AddHashTable(Hashtable dest,Hashtable source)
	{
		foreach(string str in PropertyName)
		{
			int d	=	(int)dest[str];
			int s	=	(int)source[str];
			dest[str]	=	d+s;
		}
	}
	static	void	SubHashTable(Hashtable dest,Hashtable source)
	{
		foreach(string str in PropertyName)
		{
			int d	=	(int)dest[str];
			int s	=	(int)source[str];
			dest[str]	=	d-s;
		}
	}
	
	bool 	UpdateBuff(List<sdBuff> lst)
	{
		bool Changed	=	false;
		for(int i = 0; i < lst.Count; i++)
		{
			if(lst[i] != null)
			{
				lst[i].Tick();
			}
		}
		for(int i=0;i<lst.Count;)
		{
			if(lst[i].IsDead())
			{
				lst.RemoveAt(i);
				Changed	=	true;
			}
			else
			{
				i++;
			}
		}
		return Changed;
	}
	// Update is called once per frame
	protected override	void Update () 
	{
		base.Update();
		
		bool bRefresh	=	UpdateBuff(buffs);
		if(bRefresh)
		{
			BuffChange(buffs);
		}
		UpdateBuff(HideBuffs);
		
		
		CalcPropertyTransform();
		
	}
	

	//HeaderProto.EBuffClass
	
	public	bool	CheckImmunity(Hashtable table)
	{
		if((int)table["byClass"] == (int)HeaderProto.EBuffClass.BUFF_CLASS_CONTROL)
		{
			switch((int)table["bySubClass"])
			{
				//束缚 不能移动，位移技能的位移无效...
				case (int)HeaderProto.EControlSubClass.CONTROL_SUBCLASS_STAY:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_STAY))
					{
						return true;
					}
				}break;
				case (int)HeaderProto.EControlSubClass.CONTROL_SUBCLASS_HOLD:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_HOLD))
					{
						return true;
					}
				}break;
				case (int)HeaderProto.EControlSubClass.CONTROL_SUBCLASS_STUN:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_STUN))
					{
						return true;
					}
				}break;
				case (int)HeaderProto.EControlSubClass.CONTROL_SUBCLASS_LIMIT_SKILL:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_LIMIT_SKILL))
					{
						return true;
					}
				}break;
				default:{
				
				}break;
			}
			
		}
		else if((int)table["byClass"] == (int)HeaderProto.EBuffClass.BUFF_CLASS_DECREASE)
		{
			switch((int)table["bySubClass"])
			{
				//束缚 不能移动，位移技能的位移无效...
				case (int)HeaderProto.EDecreaseSubClass.DECREASE_SUBCLASS_SPEED_UP_ATT:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_SLOW_ATT))
					{
						return true;
					}
				}break;
				case (int)HeaderProto.EDecreaseSubClass.DECREASE_SUBCLASS_SPEED_UP_MOVE:{
					if(HasImmunityAbility(HeaderProto.EImmunityType.IMMUNITY_TYPE_SLOW_MOVE))
					{
						return true;
					}
				}break;
			}
		}
		return false;
	}
	sdBuff	FindBuffByClass(int classID)
	{
		sdBuff buff=null;
		for(int i=0;i<buffs.Count;i++)
		{
			buff	=	buffs[i];
			if(buff.GetClassID() == classID)
			{
				return	buff;
			}
		}
		for(int i=0;i<HideBuffs.Count;i++)
		{
			buff	=	HideBuffs[i];
			if(buff.GetClassID() == classID)
			{
				return	buff;
			}
		}
		return null;
	}
	sdBuff	FindBuffBySwapFlag(int dwSwapFlag)
	{
		sdBuff buff=null;
		for(int i=0;i<buffs.Count;i++)
		{
			buff	=	buffs[i];
			if(buff.GetSwapFlag() == dwSwapFlag)
			{
				return	buff;
			}
		}
		for(int i=0;i<HideBuffs.Count;i++)
		{
			buff	=	HideBuffs[i];
			if(buff.GetSwapFlag() == dwSwapFlag)
			{
				return	buff;
			}
		}

		return null;
	}
    public  List<sdBuff> GetBuffs()
    {
        return buffs;
    }
    sdBuff FindBuffByCastActor(int id,sdActorInterface castActor)
    {
        sdBuff buff = null;
        for (int i = 0; i < buffs.Count; i++)
        {
            buff = buffs[i];
            if (buff.GetTemplateID() == id)
            {
                if (castActor == buff.m_CasterActor || castActor==null)
                {
                    return buff;
                }
            }
        }
        for (int i = 0; i < HideBuffs.Count; i++)
        {
            buff = HideBuffs[i];
            if (buff.GetTemplateID() == id)
            {
                if (castActor == buff.m_CasterActor || castActor == null)
                {
                    return buff;
                }
            }
        }
        return null;
    }

	sdBuff	FindBuff(int id)
	{
		sdBuff buff=null;
		for(int i=0;i<buffs.Count;i++)
		{
			buff	=	buffs[i];
			if(buff.GetTemplateID() == id)
			{
				return	buff;
			}
		}
		for(int i=0;i<HideBuffs.Count;i++)
		{
			buff	=	HideBuffs[i];
			if(buff.GetTemplateID() == id)
			{
				return	buff;
			}
		}
		return null;
	}
	bool	CheckBuffReplace(int id,Hashtable buffinfo,sdActorInterface castActor)
	{
		int isShare	=	(int)buffinfo["IsShare"];
		if(isShare!=0)
		{
            sdBuff buff = FindBuffByCastActor(id, castActor);//FindBuff(id);
			if(buff==null)
			{
				return true;
			}
			else
			{
				int isRefresh	=	(int)buffinfo["IsRefresh"];
				if(isRefresh!=0)
				{
					buff.Refresh();
					return false;
				}
				else
				{
					buff.Stop (HeaderProto.EBuffEndType.BUFF_END_HAND);
					return true;
				}
			}
		}
		else
		{
			int classID	=	(int)buffinfo["dwClassID"];
			sdBuff	buff	=	FindBuffByClass(classID);
			if(buff!=null)
			{
				int newLevel	=	(int)buffinfo["byLevel"];
				int oldLevel	=	buff.GetLevel();
				if(newLevel	>	oldLevel)
				{
					buff.Stop (HeaderProto.EBuffEndType.BUFF_END_HAND);
					return true;
				}
				else if(newLevel	<	oldLevel)
				{
					return false;
				}
				else 
				{
					int isRefresh	=	(int)buffinfo["IsRefresh"];
					if(isRefresh!=0)
					{
						buff.Refresh();
						return false;
					}
					else
					{
						buff.Stop (HeaderProto.EBuffEndType.BUFF_END_HAND);
						return true;
					}
				}
			}
			else
			{
				int dwSwapFlag	=	(int)buffinfo["dwSwapFlag"];
				buff	=	FindBuffBySwapFlag(dwSwapFlag);
				if(buff!=null)
				{
					if(buff.IsShare()!=0)
					{
						return true;
					}
					else
					{
						int IsSwap	=	(int)buffinfo["IsSwap"];
						if(IsSwap!=0)
						{
							int newLevel	=	(int)buffinfo["byLevel"];
							int oldLevel	=	buff.GetLevel();
							if(newLevel	>	oldLevel)
							{
								buff.Stop (HeaderProto.EBuffEndType.BUFF_END_HAND);
								return true;
							}
							else 
							{
								return false;
							}
						}
						else
						{
							return false;
						}
					}
				}
				else
				{
					return true;
				}
				
			}
		}
		return false;
	}
    public bool AddBuffLayer(int id, Hashtable buffinfo, sdActorInterface casterActor)
    {
        int iMaxLayerCount = (int)buffinfo["byAugment"];
        if (iMaxLayerCount > 1)
        {
            int isShare = (int)buffinfo["IsShare"];
            sdBuff layeredBuff = null;
            if (isShare != 0)
            {
                layeredBuff = FindBuffByCastActor(id, casterActor);
            }
            else
            {
                layeredBuff = FindBuff(id);
            }
            if (layeredBuff != null)
            {
                layeredBuff.AddLayer();
                if (!layeredBuff.IsHide())
                {
                    BuffChange(buffs);
                }
                return true;
            }
        }
        return false;
    }
	
	public virtual  bool RemoveBuff(int classID, int subClassID)
	{
		bool bRet = false;
		for(int index = 0; index < HideBuffs.Count;)
		{
			sdBuff buff = HideBuffs[index];
			if(buff.GetClass() == classID && buff.GetSubClass() == subClassID)
			{
				buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
				HideBuffs.RemoveAt(index);
				bRet = true;
				continue;
			}
			else
				++index;
		}
		for(int index = 0; index < buffs.Count;)
		{
			sdBuff buff = buffs[index];
			if(buff.GetClass() == classID && buff.GetSubClass() == subClassID)
			{
				buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
				buffs.RemoveAt(index);
				bRet = true;
				continue;
			}
			else
				++index;
		}
		return bRet;
	}

    public virtual bool RemoveBuffbyID(int templataID)
    {
        bool bRet = false;
        for (int index = 0; index < HideBuffs.Count; )
        {
            sdBuff buff = HideBuffs[index];
            if (buff.GetTemplateID() == templataID )
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                HideBuffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        for (int index = 0; index < buffs.Count; )
        {
            sdBuff buff = buffs[index];
            if (buff.GetTemplateID() == templataID)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                buffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        return bRet;
    }


    public virtual bool RemoveBuffbyClassType(int classtype)
    {
        bool bRet = false;
        for (int index = 0; index < HideBuffs.Count; )
        {
            sdBuff buff = HideBuffs[index];
            if (buff.GetClass() == classtype)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                HideBuffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        for (int index = 0; index < buffs.Count; )
        {
            sdBuff buff = buffs[index];
            if (buff.GetClass() == classtype)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                buffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        return bRet;
    }


    public virtual bool RemoveBuffbySubType(int subclasstype)
    {
        bool bRet = false;
        for (int index = 0; index < HideBuffs.Count; )
        {
            sdBuff buff = HideBuffs[index];
            if (buff.GetSubClass() == subclasstype)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                HideBuffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        for (int index = 0; index < buffs.Count; )
        {
            sdBuff buff = buffs[index];
            if (buff.GetSubClass() == subclasstype)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                buffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        return bRet;
    }

    public virtual bool RemoveBuffbyProperty(int property)
    {
        bool bRet = false;
        for (int index = 0; index < HideBuffs.Count; )
        {
            sdBuff buff = HideBuffs[index];
            if (buff.GetIntProperty() == property)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                HideBuffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        for (int index = 0; index < buffs.Count; )
        {
            sdBuff buff = buffs[index];
            if (buff.GetIntProperty() == property)
            {
                buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                buffs.RemoveAt(index);
                bRet = true;
                continue;
            }
            else
                ++index;
        }
        return bRet;
    }

    public virtual bool RemoveAllBuff()
    {
        bool bRet = false;
        for (int index = 0; index < HideBuffs.Count; )
        {
            sdBuff buff = HideBuffs[index];
            buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
            HideBuffs.RemoveAt(index);
            bRet = true;
        }
        for (int index = 0; index < buffs.Count; )
        {
            sdBuff buff = buffs[index];
            buff.Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
            buffs.RemoveAt(index);
            bRet = true;
        }
        return bRet;
    }
	
	public virtual	bool AddBuff(int templateId,int life,sdActorInterface casterActor)
	{
		if(GetCurrentHP()<=0)return false;
		Hashtable	buffInfoTable	=	sdConfDataMgr.Instance().GetTable("buff");		
		Hashtable	buffinfo		=	buffInfoTable[templateId.ToString()] as Hashtable;
		if(buffinfo==null)return false;	

        //闪避状态 抵挡任何减益 buff
        if ((int)buffinfo["IsDebuff"] == 1)
        {
            if (CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_UNBEAT))
                return false;
        }
		//Buff 替换规则..
        if (!CheckBuffReplace(templateId, buffinfo, casterActor))
		{
            int iHide = (int)buffinfo["IsHide"];
            if (iHide != 1)
            {
                BuffChange(buffs);
            }
			return false;
		}
		
		//免疫..
		if(CheckImmunity(buffinfo))
		{
			return false;
		}
		//无敌判断 ..
		//todo

        //尝试添加buff层数 如果成功则已经存在同样的buff 直接更新层数即可 ...
        if (AddBuffLayer(templateId, buffinfo, casterActor))
        {
            return true;
        }
		
        //添加一个新的buff..
		sdBuff buff = new sdBuff();
		buff.SetCaster(casterActor);
		buff.SetTemplateId(templateId);
		buff.SetActor(this);
		if(buff.IsHide())
		{
			HideBuffs.Add(buff);
		}
		else
		{
			buffs.Add(buff);
			BuffChange(buffs);
		}
		
		
		buff.Start();
		
		
		return true;
	}
	
	public bool RemoveBuff(int id)
	{
		bool bRet	=	false;

		for(int i=0;i<HideBuffs.Count;i++)
		{
			sdBuff buff = HideBuffs[i];
			if(buff.GetTemplateID()==id)
			{
				buff.Stop(HeaderProto.EBuffEndType.BUFF_END_HAND);
				HideBuffs.RemoveAt(i);
				bRet	=	true;
			}
		}
		bool buffchanged = false;
		for(int i=0;i<buffs.Count;i++)
		{
			sdBuff buff = buffs[i];
			if(buff.GetTemplateID()==id)
			{
				buff.Stop(HeaderProto.EBuffEndType.BUFF_END_HAND);
				buffs.RemoveAt(i);
				buffchanged	=	true;
				bRet		=	true;
			}
		}
		if(buffchanged)
		{
			BuffChange(buffs);
		}
		return bRet;
	}
	
	
	private void operatePropertyTransfer(string key,int val,Hashtable oTable)
	{
		Hashtable	table	=	sdConfDataMgr.Instance().GetTable("PropTransfer");
		Hashtable subTable 	= 	table[key] as Hashtable;
        if (subTable == null) return;
		
		oTable["MaxHP"] = (int)oTable["MaxHP"] + (val * (int)subTable["MaxHP"])/10000;
		oTable["MaxSP"] = (int)oTable["MaxSP"] + (val * (int)subTable["MaxSP"])/10000;
		oTable["Hptick"] = (int)oTable["Hptick"] + (val * (int)subTable["Hptick"])/10000;
		oTable["Sptick"] = (int)oTable["Sptick"] + (val * (int)subTable["Sptick"])/10000;
		oTable["AtkDmgMin"] = (int)oTable["AtkDmgMin"] + (val * (int)subTable["AtkDmgMin"])/10000;
		oTable["AtkDmgMax"] = (int)oTable["AtkDmgMax"] + (val * (int)subTable["AtkDmgMax"])/10000;
		oTable["Def"] = (int)oTable["Def"] + (val * (int)subTable["Def"])/10000;
		oTable["IceAtt"] = (int)oTable["IceAtt"] + (val * (int)subTable["IceAtt"])/10000;
		oTable["FireAtt"] = (int)oTable["FireAtt"] + (val * (int)subTable["FireAtt"])/10000;
		oTable["PoisonAtt"] = (int)oTable["PoisonAtt"] + (val * (int)subTable["PoisonAtt"])/10000;
		oTable["ThunderAtt"] = (int)oTable["ThunderAtt"] + (val * (int)subTable["ThunderAtt"])/10000;
		oTable["IceDef"] = (int)oTable["IceDef"] + (val * (int)subTable["IceDef"])/10000;
		oTable["FireDef"] = (int)oTable["FireDef"] + (val * (int)subTable["FireDef"])/10000;
		oTable["PoisonDef"] = (int)oTable["PoisonDef"] + (val * (int)subTable["PoisonDef"])/10000;
		oTable["ThunderDef"] = (int)oTable["ThunderDef"] + (val * (int)subTable["ThunderDef"])/10000;
		oTable["Pierce"] = (int)oTable["Pierce"] + (val * (int)subTable["Pierce"])/10000;
		oTable["Hit"] = (int)oTable["Hit"] + (val * (int)subTable["Hit"])/10000;
		oTable["Dodge"] = (int)oTable["Dodge"] + (val * (int)subTable["Dodge"])/10000;
		oTable["Cri"] = (int)oTable["Cri"] + (val * (int)subTable["Cri"])/10000;
		oTable["Flex"] = (int)oTable["Flex"] + (val * (int)subTable["Flex"])/10000;
		oTable["CriDmg"] = (int)oTable["CriDmg"] + (val * (int)subTable["CriDmg"])/10000;
		oTable["CriDmgDef"] = (int)oTable["CriDmgDef"] + (val * (int)subTable["CriDmgDef"])/10000;
		oTable["BodySize"] = (int)oTable["BodySize"] + (val * (int)subTable["BodySize"])/10000;
		oTable["AttSize"] = (int)oTable["AttSize"] + (val * (int)subTable["AttSize"])/10000;
		oTable["AttSpeedModPer"] = (int)oTable["AttSpeedModPer"] + (val * (int)subTable["AttSpeedModPer"])/10000;
		oTable["MoveSpeedModPer"] = (int)oTable["MoveSpeedModPer"] + (val * (int)subTable["MoveSpeedModPer"])/10000;
		oTable["PiercePer"] = (int)oTable["PiercePer"] + (val * (int)subTable["PiercePer"])/10000;
		oTable["HitPer"] = (int)oTable["HitPer"] + (val * (int)subTable["HitPer"])/10000;
		oTable["DodgePer"] = (int)oTable["DodgePer"] + (val * (int)subTable["DodgePer"])/10000;
		oTable["CriPer"] = (int)oTable["CriPer"] + (val * (int)subTable["CriPer"])/10000;
		oTable["FlexPer"] = (int)oTable["FlexPer"] + (val * (int)subTable["FlexPer"])/10000;
		oTable["AttSpeed"] = (int)oTable["AttSpeed"] + (val * (int)subTable["AttSpeed"])/10000;
		oTable["MoveSpeed"] = (int)oTable["MoveSpeed"] + (val * (int)subTable["MoveSpeed"])/10000;
	}
	
	public	bool 	CheckBuff(int type,int data)
	{
		
		//sdBuff[] allbuff	=	null;//m_BuffSys.
		if(type== (int)HeaderProto.ECheckBuffType.CHECK_BUFF_NONE)
		{
			return true;
		}
		switch(type)
		{
			case (int)HeaderProto.ECheckBuffType.CHECK_BUFF_CLASS:{
				for(int i=0;i<buffs.Count;i++)
				{
					sdBuff buff = buffs[i];
					if((int)buff.GetProperty()["byClass"]==data)
					{
						return true;	
					}
				}
			}break;
			case (int)HeaderProto.ECheckBuffType.CHECK_BUFF_SUB_CLASS:{
				for(int i=0;i<buffs.Count;i++)
				{
					sdBuff buff = buffs[i];
					int iClass		=	data/10000;
					int iSubClass	=	data%10000;
					if( (int)buff.GetProperty()["bySubClass"]==iSubClass &&
						(int)buff.GetProperty()["byClass"]==iClass	
					)
					{
						return true;	
					}
				}
			}break;
			case (int)HeaderProto.ECheckBuffType.CHECK_BUFF_CLASS_ID:{
				for(int i=0;i<buffs.Count;i++)
				{
					sdBuff buff = buffs[i];
					if((int)buff.GetProperty()["dwClassID"]==data)
					{
						return true;	
					}
				}
			}break;
			case (int)HeaderProto.ECheckBuffType.CHECK_BUFF_TEMPLATE_ID:{
				for(int i=0;i<buffs.Count;i++)
				{
					sdBuff buff = buffs[i];
					if(buff.GetTemplateID()==data)
					{
						return true;	
					}
				}
			}break;
			case (int)HeaderProto.ECheckBuffType.CHECK_BUFF_DAMAGE_PRO:{
				for(int i=0;i<buffs.Count;i++)
				{
					sdBuff buff = buffs[i];
					if((int)buff.GetProperty()["byProperty"]==data)
					{
						return true;	
					}
				}
			
			}break;
		}
		return false;
	}
	//用于做触发效果.
	public	void	OnBuffBorn(sdBuff buff)
	{
	}
	
	public	bool	CalcPropertyTransform()
	{
		if(GetActorType()!=ActorType.AT_Player)
		{
			return false;
		}
		int[] currentBaseProperty	=	new int[5];
		Hashtable	base_pro	=	GetBaseProperty();
		if(base_pro==null || GetProperty()==null)
		{
			return false;
		}
		for(int i=0;i<5;i++)
		{
			currentBaseProperty[i]	=	(int)GetProperty()[BasePropertyName[i]]	-	(int)base_pro[BasePropertyName[i]];
		}

		bool bNeedCalc	=	false;
		for(int i=0;i<5;i++)
		{
			if(currentBaseProperty[i]	!=	OldBaseProperty[i])
			{
				bNeedCalc	=	true;
				break;
			}
		}
		
		if(bNeedCalc)
		{
			OldBaseProperty	=	currentBaseProperty;
			
			Hashtable	table	=	new Hashtable();
			InitHashTable(table);
			for(int i=0;i<5;i++)
			{
				operatePropertyTransfer(BasePropertyName[i],currentBaseProperty[i],table);
			}
			
			SubHashTable(table,TempProperty);
			TempProperty	=	table;
			AddHashTable(GetProperty(),TempProperty);
		}
		return true;
	}
	public	void	ClearBuff()
	{
		for(int i=0;i<buffs.Count;i++)
		{
			buffs[i].Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
		}
		buffs.Clear();
		for(int i=0;i<HideBuffs.Count;i++)
		{
			HideBuffs[i].Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
		}
		HideBuffs.Clear();
	}
	public	virtual	void	SetTeleportInfo(TeleportInfo info)
	{
		tpInfo.Add(info);
	}
	public	virtual	void	UpdateTeleport()
	{
		if(GetCurrentHP() <= 0)
		{
			tpInfo.ForEach(delegate(TeleportInfo info)
			{
				if(info.TeleportState)
					RemoveDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_TELEPORT);
			});
			tpInfo.Clear();
		}
		else
		{
			for(int index = 0; index < tpInfo.Count;)
			{
				TeleportInfo info = tpInfo[index];
				if(info.MoveTime > 0.0f)
				{
					info.MoveTime -= Time.deltaTime;
					++index;
				}
				else
				{
					if(info.TeleportState)
						RemoveDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_TELEPORT);
					tpInfo.RemoveAt(index);
				}
			}
		}
	}
	
	public virtual void  OnAttackRestore(int actionID, int hp, int monsterType)
	{
	}
	
	public virtual void  DoHittedAudio(string strWeaponType)
	{
	}

    public void SetSummonAttackInfo(int summonID)
    {
        lastSummonID = summonID;
    }
    public bool IsCanSummonAttack(int summonID)
    {
        return (summonID != lastSummonID);
    }

	// 改变GroupIDaa
	public void ChangeGroupID(GroupIDType eGroupID)
	{
		if (eGroupID != GetGroupID())
		{
			sdGameLevel.instance.actorMgr.RemoveActor(this);
			groupid = eGroupID;
			sdGameLevel.instance.actorMgr.AddActor(this);
		}
	}
    public void ClearNotDeathHoldBuffer()
    {
        for (int i = 0; i < buffs.Count; )
        {
            int deathhold   =   (int)buffs[i].GetProperty()["IsDeadHold"];
            if (deathhold == 0)
            {
                buffs[i].Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                buffs.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        for (int i = 0; i < HideBuffs.Count;)
        {
            int deathhold = (int)HideBuffs[i].GetProperty()["IsDeadHold"];
            if (deathhold == 0)
            {
                HideBuffs[i].Stop(HeaderProto.EBuffEndType.BUFF_END_DEAD);
                HideBuffs.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

    }
}