using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 64位储存...

/*enum EBuffSpringFlag:ulong    ///BUFF触发标志...
{
	SPRING_FLAG_HIT_PHY_NORMAL        = ((unsigned __int64)1) << CONDITION_HIT_PHY_NORMAL,     /// 普通攻击命中后判定      //0x00000001
	SPRING_FLAG_HIT_PHY_SKILL         = ((unsigned __int64)1) << CONDITION_HIT_PHY_SKILL,      /// 物理技能攻击命中后判定  //0x00000002
	SPRING_FLAG_HIT_YIN               = ((unsigned __int64)1) << CONDITION_HIT_YIN,            /// 冰攻击命中后判定      //0x00000004
	SPRING_FLAG_HIT_YANG              = ((unsigned __int64)1) << CONDITION_HIT_YANG,           /// 火攻击命中后判定      //0x00000008
	SPRING_FLAG_HIT_YUAN              = ((unsigned __int64)1) << CONDITION_HIT_YUAN,           /// 毒攻击命中后判定      //0x00000010
	SPRING_FLAG_HIT_POISON            = ((unsigned __int64)1) << CONDITION_HIT_POISON,         /// 雷攻击命中后判定        //0x00000020

	SPRING_FLAG_PHY_CRI_NORMAL        = ((unsigned __int64)1) << CONDITION_PHY_CRI_NORMAL,     /// 普通攻击暴击后判定      //0x00000040
	SPRING_FLAG_PHY_CRI_SKILL         = ((unsigned __int64)1) << CONDITION_PHY_CRI_SKILL,      /// 物理技能攻击暴击后判定  //0x00000080
	SPRING_FLAG_YIN_CRI               = ((unsigned __int64)1) << CONDITION_YIN_CRI,            /// 冰攻击暴击后判定      //0x00000100
	SPRING_FLAG_YANG_CRI              = ((unsigned __int64)1) << CONDITION_YANG_CRI,           /// 火攻击暴击后判定      //0x00000200
	SPRING_FLAG_YUAN_CRI              = ((unsigned __int64)1) << CONDITION_YUAN_CRI,           /// 毒攻击暴击后判定      //0x00000400
	SPRING_FLAG_POISON_CRI            = ((unsigned __int64)1) << CONDITION_POISON_CRI,         /// 雷攻击暴击后判定        //0x00000800
	
	//被动
	SPRING_FLAG_BE_HIT_PHY_NORMAL     = ((unsigned __int64)1) << CONDITION_BE_HIT_PHY_NORMAL,  /// 被普通攻击命中后判定
	SPRING_FLAG_BE_HIT_PHY_SKILL      = ((unsigned __int64)1) << CONDITION_BE_HIT_PHY_SKILL,   /// 被物理技能攻击命中后判定
	SPRING_FLAG_BE_HIT_YIN            = ((unsigned __int64)1) << CONDITION_BE_HIT_YIN,         /// 被冰攻击命中后判定
	SPRING_FLAG_BE_HIT_YANG           = ((unsigned __int64)1) << CONDITION_BE_HIT_YANG,        /// 被火攻击命中后判定
	SPRING_FLAG_BE_HIT_YUAN           = ((unsigned __int64)1) << CONDITION_BE_HIT_YUAN,        /// 被毒攻击命中后判定
	SPRING_FLAG_BE_HIT_POISON         = ((unsigned __int64)1) << CONDITION_BE_HIT_POISON,      /// 被雷攻击命中

	SPRING_FLAG_BE_PHY_CRI_NORMAL     = ((unsigned __int64)1) << CONDITION_BE_PHY_CRI_NORMAL,  /// 被普通攻击暴击后判定
	SPRING_FLAG_BE_PHY_CRI_SKILL      = ((unsigned __int64)1) << CONDITION_BE_PHY_CRI_SKILL,   /// 被物理技能攻击暴击后判定
	SPRING_FLAG_BE_YIN_CRI            = ((unsigned __int64)1) << CONDITION_BE_YIN_CRI,         /// 被冰攻击暴击后判定
	SPRING_FLAG_BE_YANG_CRI           = ((unsigned __int64)1) << CONDITION_BE_YANG_CRI,        /// 被火攻击暴击后判定
	SPRING_FLAG_BE_YUAN_CRI           = ((unsigned __int64)1) << CONDITION_BE_YUAN_CRI,        /// 被毒攻击暴击后判定
	SPRING_FLAG_BE_POISON_CRI         = ((unsigned __int64)1) << CONDITION_BE_POISON_CRI,      /// 被雷攻击暴击后判定

	SPRING_FLAG_PHY_DODGE             = ((unsigned __int64)1) << CONDITION_PHY_DODGE,          /// 躲闪攻击后判定
	//其他
	SPRING_FLAG_IS_DAMAGE             = ((unsigned __int64)1) << CONDITION_IS_DAMAGE,          /// 受到伤害后判定
	SPRING_FLAG_IS_CURE               = ((unsigned __int64)1) << CONDITION_IS_CURE,            /// 受到治疗后判定
	SPRING_FLAG_BUFF_ABSORB           = ((unsigned __int64)1) << CONDITION_BUFF_ABSORB,        /// 伤害吸收特效触发判定 吸收伤害以后判定

	SPRING_FLAG_IMMUNITY_HOLD1        = ((unsigned __int64)1) << CONDITION_IMMUNITY_HOLD,      /// 免疫昏睡后判定
	SPRING_FLAG_IMMUNITY_STAY         = ((unsigned __int64)1) << CONDITION_IMMUNITY_STAY,      /// 免疫束缚后判定
	SPRING_FLAG_IMMUNITY_STUN1        = ((unsigned __int64)1) << CONDITION_IMMUNITY_STUN,      /// 免疫昏迷后判定
	SPRING_FLAG_IMMUNITY_SILENCE      = ((unsigned __int64)1) << CONDITION_IMMUNITY_SILENCE,   /// 免疫沉默后判定

	SPRING_FLAG_BE_HOLD1              = ((unsigned __int64)1) << CONDITION_BE_HOLD1,           /// 被昏睡后判定
	SPRING_FLAG_BE_STAY               = ((unsigned __int64)1) << CONDITION_BE_STAY,            /// 被束缚后判定
	SPRING_FLAG_BE_STUN1              = ((unsigned __int64)1) << CONDITION_BE_STUN1,           /// 被昏迷后判定
	SPRING_FLAG_BE_SILENCE            = ((unsigned __int64)1) << CONDITION_BE_SILENCE,         /// 被沉默后判定

};*/

public	class SBuffEffect ///byEffectType,byEffectExplain,dwEffectID,dwEffectData,dwPeriodicData
{
	public	int   byEffectType;        ///[0]效果生效类型,见EBuffEffectType
	public	int   byEffectExplain;     ///[1]
	///效果附加数据
	///周期性效果,周期性效果类型 EPeriodicDamage
	///结束性效果,表示结束类型,见EBuffEndType 
	///触发性效果,表示作用与自己(0) 还是触发者(1) 还是作用buff释放者(2)  还是buff 的aoe对象(3)
	///层数叠满时效果,无意义
	public	int  dwOperationID;         ///[2]BUFF效果ID
	public	int  dwOperationData;       ///[3]BUFF效果参数0
	public	int  dwOperationData1;      ///[4]BUFF效果参数1
	public	int  dwPeriodicData;     ///[5]周期生效参数（固定值或者万分比）,触发性效果表示该效果触发的概率100为100%  
	///结束生效：被驱散生效：0代表对自己的效果，1代表对驱散者的效果 2代表对释放者的效果，释放者可能找不到
	///          其他生效：0代表对自己的效果， 1代表对释放者的效果，释放者可能找不到 2 对AOE目标的效果
	public	int  dwEXData; //额外数据，在修正技能时使用
}

/*public class SBuffProperty
{
	public uint dwTemplateId;
	public string szName;
	public uint dwClassID;
	public byte byLevel;
	
	public byte byClass;
	public byte bySubClass;
	public byte byProperty;
	
	public int nTotalTime;
	public int nPeriodicTime;
	
	public uint dwFlag;
	public byte   byDisperseWay;
	public byte   bySwapType;
	public uint  dwSwapFlag;
	public byte   byEffectLevel;
	
	public byte   byAugment;
	public uint  dwUseUpValue;
	public byte   byAfterUseUp;

	public byte   byAuraAimType;
	public int   nArea;
	public byte   byAuraCenterType;
	public int   nAuraCenterData;
	public byte   byAuraAreaType;
	public int   nAuraAreaData;

	public ulong  qwSpringFlag;
	public byte[]   bySpringCondition = new byte[4];
	public uint[]  dwSrpingConditionParam = new uint[4];

	public BUFF_EFFECT[] stEffect = new BUFF_EFFECT[4];
	//damage属性...
	public byte   byBuffEffect;
	public uint  dwCritical;
	public byte   byIgnoreDefend;
	public uint  dwIgnoreDefendAmount;

	//物理或者内功直接伤害....
	public uint  dwPhyDmg;
	public uint  dwAtkPowerPer;
	public uint  dwDamagePer;
}*/

	

public class sdBuff{
	
	public	sdActorInterface	m_CasterActor;
	private int 				templateId;
	int							m_Layer	=	0;
	GameObject					m_Effect	=	null;
	HeaderProto.EBuffEndType	EndType	=	HeaderProto.EBuffEndType.BUFF_END_NONE;
	private Hashtable prop;
	private Hashtable[] effects = new Hashtable[8];
	
	
	private sdActorInterface 	holder;
	int			PeriodicCount	=	0;

	private float lifeTime = 0.0f;
	
	// Update is called once per frame
	void Update () {
		
	}
	public	bool	IsHide()
	{
		return (int)prop["IsHide"]==1;
	}
	public	bool	IsDead()
	{
		return EndType	!=	HeaderProto.EBuffEndType.BUFF_END_NONE;
	}
	public	sdActorInterface	GetCaster()
	{
		return m_CasterActor;
	}
	public	void				SetCaster(sdActorInterface casterActor)
	{
		m_CasterActor	=	casterActor;
	}
	public	sdActorInterface	GetActor()
	{
		return holder;
	}
	public void SetActor(sdActorInterface actor)
	{
		holder = actor;
	}
	public	int	GetTemplateID()
	{
		return templateId;
	}
    public int GetLayerCount()
    {
        return m_Layer;
    }
    public int GetMaxLayerCount()
    {
        return (int)prop["byAugment"];
    }
	public	int	GetSwapFlag()
	{
		return (int)prop["dwSwapFlag"];
	}
	public	int	GetClassID()
	{
		return (int)prop["dwClassID"];
	}
	
	public int GetSubClass()
	{
		return (int)prop["bySubClass"];
	}
	
	public int GetClass()
	{
		return (int)prop["byClass"];
	}
	
	public	int	GetLevel()
	{
		return (int)prop["byLevel"];
	}
	public	int	IsShare()
	{
		return (int)prop["IsShare"];
	}
	public	void	Refresh()
	{
		lifeTime	=	0.0f;
	}
	public Hashtable GetEffect(int id)
	{
		return effects[id];
	}

    public int GetIntProperty()
    {
        return (int)prop["byProperty"];
    }
	
	public void SetTemplateId(int id)
	{
		templateId = id;
		Hashtable table	= (Hashtable)sdConfDataMgr.Instance().GetTable("buff");
		Hashtable buffeffectTable	=	(Hashtable)sdConfDataMgr.Instance().GetTable("buffeffect");
		
		prop =	table[id.ToString()] as Hashtable;
		string effStr = (string)prop["adwBuffEffect[8]"];
		effStr = effStr.Replace("{","");
		effStr = effStr.Replace("}","");
		string[] subStr =	effStr.Split(new char[]{';'});
		
		for(int i = 0; i < subStr.Length; i++)
		{
			int effectId = int.Parse(subStr[i]);
			
			if(effectId > 0)
			{
				effects[i] = (Hashtable)buffeffectTable[subStr[i]];
			}
		}
	}
	

	
	public bool Tick()
	{
		lifeTime += Time.deltaTime;		
		int mTime = (int)(lifeTime*1000);
		int totalTime = (int)prop["nTotalTime"];		
		if(totalTime != 0 && mTime > totalTime)
		{
			Stop(HeaderProto.EBuffEndType.BUFF_END_TIMEUP);
		}
		int iPeriodicTime	=	(int)prop["nPeriodicTime"];
		if(iPeriodicTime>0)
		{
			int iCount 		= 	mTime / iPeriodicTime;
			int iPeriodic	=	(iCount - PeriodicCount);
			for(int i=0;i<iPeriodic;i++)
			{
				DoPeriodicEffect();
			}
			PeriodicCount	=	iCount;
		}
		return true;
	}
		
	void 	OnBorn()
	{
		m_Layer++;
		DoBeginEffect(HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START,m_Layer);
		
		PlayEffect();
	}
	void	OnDead()
	{
		DoBeginEffect(HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE,m_Layer);
		if(m_Effect!=null)	
		{
			GameObject.Destroy(m_Effect);
			m_Effect=null;
		}
	}
	public	void	Start()
	{
		OnBorn();
	}
	public	void	Stop(HeaderProto.EBuffEndType endType)
	{
		EndType	=	endType;
		OnDead();
	}
	
	public float GetLifeTime()
	{
		return 	lifeTime;
	}
	public	Hashtable GetProperty()
	{
		return prop;
	}
	void	DoBeginEffect(HeaderProto.EDoOperationType doType,int layer)
	{
		bool bBegin	=	(doType	==	HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START);
		for(int i = 0; i < 8; i++)
		{
			if(effects[i] == null)
				continue;
            int templateid          =    (int)effects[i]["effectID"];
			int iType				=	(int)effects[i]["byEffectType"];
			int byEffectExplain		=	(int)effects[i]["byEffectExplain"];  //属性类型HeaderProto.EProperty
			int id					=	(int)effects[i]["dwOperationID"];     //HeaderProto.EOpreationFlag
			int dwOperationData		=	(int)effects[i]["dwOperationData"];	
			int dwOperationData1	=	(int)effects[i]["dwOperationData1"];	
			int dwPeriodicData		=	(int)effects[i]["dwPeriodicData"];
			int dwEXData			=	(int)effects[i]["dwEXData"];
	
			if( iType	== (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_START)
			{
				if(!bBegin)
				{
					if(dwEXData==0)
					{
						return;
					}
				}
				switch(dwPeriodicData)
				{
					case 0:{  //对buff的所有者
						sdBuffEffect.Add(m_CasterActor,GetActor(),effects[i],doType,layer);
						break;}
					case 1:{//对buff的释放者
						sdBuffEffect.Add(m_CasterActor,m_CasterActor,effects[i],doType,layer);
						break;}
					case 2:{
						if(bBegin)
						{
						//获取AOE范围内的所有目标.
							List<sdActorInterface> lstAoe	=	GetEffectedActors();
							foreach(sdActorInterface a in lstAoe)
							{
								sdBuffEffect.Add(m_CasterActor,a,effects[i],doType,layer);
							}
						}
						break;}
				}
				
			}
			else if(iType== (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_MOD_PRO)
			{				
				ModifyActorProperty(
					(HeaderProto.EProperty)byEffectExplain,
					(HeaderProto.EOpreationFlag)id,
					(HeaderProto.EOpreationFlag)dwOperationData,
					dwOperationData1,
					bBegin);
			}
			else if(iType == (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_MOD_SKILL_INFO_PRO)
			{				
				ModifySkillProperty(
					(HeaderProto.ESkillInfoPro)byEffectExplain,
					(HeaderProto.EOpreationFlag)id,
					(HeaderProto.EOpreationFlag)dwOperationData,
					dwOperationData1,
					(HeaderProto.EEffectSkillBuffOptType)dwPeriodicData,
					dwEXData,
					bBegin);
			}
			else if(iType== (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_MOD_SKILL_ACTION_PRO)
			{

				ModifySkillActionProperty(
					(HeaderProto.ESkillActionPro)byEffectExplain,
					(HeaderProto.EOpreationFlag)id,
					(HeaderProto.EOpreationFlag)dwOperationData,
					dwOperationData1,
					(HeaderProto.EEffectSkillBuffOptType)dwPeriodicData,
					dwEXData,
					bBegin);
			}
			else if(iType== (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_MOD_SKILL_EFFECT_PRO)
			{
				
				ModifySkillEffectProperty(
					(HeaderProto.ESkillEffectPro)byEffectExplain,
					(HeaderProto.EOpreationFlag)id,
					(HeaderProto.EOpreationFlag)dwOperationData,
					dwOperationData1,
					(HeaderProto.EEffectSkillBuffOptType)dwPeriodicData,
					dwEXData,
					bBegin);
			}
			else if(iType == (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_MOD_SUMMONOBJECT_PRO)
			{
				ModifySummonObjectProperty(
					(HeaderProto.ESummonObjectPro)byEffectExplain,
					(HeaderProto.EOpreationFlag)id,
					(HeaderProto.EOpreationFlag)dwOperationData,
					dwOperationData1,
					dwPeriodicData,
					dwEXData,
					bBegin);
			}
		}		
	}
	int	ConvertValue(HeaderProto.EOpreationFlag dataType,int iValue,int Orgin)
	{
		switch(dataType)
		{
			case HeaderProto.EOpreationFlag.OPREATION_FLAG_INTEGER:{
				
			}break;
			case HeaderProto.EOpreationFlag.OPREATION_FLAG_PERCENT:{
				iValue	=	(Orgin*iValue)/10000;
			}break;	
		}
		return iValue;
	}
	int	GetModifyValue(bool bBegin,HeaderProto.EOpreationFlag opType,int Orgin,int Current,int iValue)
	{
        int iLayerValue = iValue * m_Layer;
		if(bBegin)
		{
			switch(opType)
			{
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_ADD:{
                    Current += iLayerValue;
				}break;
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_REDUCE:{
                    Current -= iLayerValue;
				}break;
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_SET:{
					Current	=	iValue;
				}break;
				default:{
					Current	=	iValue;
				}break;
			}
		}
		else
		{
			switch(opType)
			{
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_ADD:{
                    Current -= iLayerValue;
				}break;
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_REDUCE:{
                    Current += iLayerValue;
				}break;
				case HeaderProto.EOpreationFlag.OPREATION_FLAG_SET:{
					Current	=	Orgin;
				}break;
				default:{
					Current	=	Orgin;
				}break;
			}
		}
		return Current;
	}
	//PropertyType < PROPERTY_CanModMax
	//opType  取值范围
	//	OPREATION_FLAG_ADD     = 0,
	//	OPREATION_FLAG_REDUCE  = 1,
	//	OPREATION_FLAG_SET     = 3,
	//dataType 取值范围
	//	OPREATION_FLAG_INTEGER = 0,
	//	OPREATION_FLAG_PERCENT = 1,
	void	ModifyActorProperty(
		HeaderProto.EProperty PropertyType,
		HeaderProto.EOpreationFlag opType,
		HeaderProto.EOpreationFlag dataType,
		int data,
		bool bBegin)
	{
		string	PropertyName	=	sdActorInterface.BasePropertyName[(int)PropertyType];
		if(PropertyName.Length==0)
		{
			return;
		}
		Hashtable	baseProperty	=	GetActor().GetBaseProperty();
		Hashtable	Property		=	GetActor().GetProperty();
		
		int Orgin	=	(int)baseProperty[PropertyName];
		int	Current	=	(int)Property[PropertyName];
		int	iValue	=	ConvertValue(dataType,data,Orgin);
		
		
		Property[PropertyName]	=	GetModifyValue(bBegin,opType,Orgin,Current,iValue);
		if(bBegin&&PropertyType==HeaderProto.EProperty.PROPERTY_MaxHP)
		{
			GetActor().AddHP(iValue);
		}
		else if(bBegin&&PropertyType==HeaderProto.EProperty.PROPERTY_MaxSP)
		{
			GetActor().AddSP(iValue);
		}
	}
	
	void	ModifySkillProperty(
		HeaderProto.ESkillInfoPro PropertyType,
		HeaderProto.EOpreationFlag opType,
		HeaderProto.EOpreationFlag dataType,
		int data,
		HeaderProto.EEffectSkillBuffOptType optType,
		int skill_UID,
		bool bBegin
		)
	{
		string	PropertyName	=	sdActorInterface.SkillPropertyName[(int)PropertyType];
		if(PropertyName.Length==0)
		{
			return;
		}
		Hashtable	baseProperty	=	GetActor().GetBaseSkillProperty();
		Hashtable	Property		=	GetActor().GetSkillProperty();
		
		int iSkillID	=	skill_UID/100;
		
		Hashtable	baseSkill		=	baseProperty[iSkillID] as Hashtable;
		Hashtable	Skill			=	Property[iSkillID]as Hashtable;
		
		int Orgin	=	(int)baseSkill[PropertyName];
		int	Current	=	(int)Skill[PropertyName];
		int	iValue	=	ConvertValue(dataType,data,Orgin);
		
		Skill[PropertyName]	=	GetModifyValue(bBegin,opType,Orgin,Current,iValue);;
		//t[]
	}
	void	ModifySkillActionProperty(
		HeaderProto.ESkillActionPro PropertyType,
		HeaderProto.EOpreationFlag opType,
		HeaderProto.EOpreationFlag dataType,
		int data,
		HeaderProto.EEffectSkillBuffOptType optType,
		int action_UID,
		bool bBegin
		)
	{
		string	PropertyName	=	sdActorInterface.SkillActionName[(int)PropertyType];
		if(PropertyName.Length==0)
		{
			return;
		}
		Hashtable	baseProperty	=	GetActor().GetBaseSkillAction();
		Hashtable	Property		=	GetActor().GetSkillAction();
		
		Hashtable	baseAction		=	baseProperty[action_UID] as Hashtable;
		Hashtable	Action			=	Property[action_UID]as Hashtable;
		object 	orginValue		=	baseAction[PropertyName];
		object	currentValue	=	Action[PropertyName];
		if(orginValue.GetType()==typeof(int[]))
		{
			int[] Orgin			=	(int[])orginValue;
			int[] Current		=	(int[])currentValue;
			int index = -1;
			if(PropertyName == "naMoreDamagePer[MONSTER_BODY_TYPE_max]")
			{
				int nType = (int)PropertyType;
				int nSmall = (int)HeaderProto.ESkillActionPro.SKILL_ACTION_PRO_MoreDamagePer_BODY_TYPE_small;
				index = nType>=nSmall ? nType - nSmall:-1;
			}
				
			for(int i=0;i<Orgin.Length;i++)
			{
				if(index != -1 && i != index)
					continue;
				int iValue	=	ConvertValue(dataType,data,Orgin[i]);
				Current[i]	=	GetModifyValue(bBegin,opType,Orgin[i],Current[i],iValue);
			}
			Action[PropertyName]	=	Current;
		}
		else if(orginValue.GetType()==typeof(int))
		{
			int Orgin	=	(int)orginValue;
			int Current	=	(int)currentValue;
			int	iValue	=	ConvertValue(dataType,data,Orgin);
			Action[PropertyName]	=	GetModifyValue(bBegin,opType,Orgin,Current,iValue);
		}		
	}
	
	
	void ModifySummonObjectProperty(
		HeaderProto.ESummonObjectPro PropertyType,
		HeaderProto.EOpreationFlag opType,
		HeaderProto.EOpreationFlag dataType,
		int dwOperationData1,
		int data,
		int summonID,
		bool bBegin)
	{
		string strProName = sdSkillSummon.SummonProperty[(int)PropertyType];
		if(strProName.Length == 0)
			return;
		Hashtable summonTabel = GetActor().GetSummonInfo();
		Hashtable summonInfo = summonTabel[summonID] as Hashtable;
		Hashtable  baseSummonInfo = sdConfDataMgr.Instance().m_BaseSummon[summonID] as Hashtable;
		object OrginValue = baseSummonInfo[strProName];
		object CurrentValue = summonInfo[strProName];
		if(OrginValue.GetType() == typeof(int[]))
		{
			int[] Orgin = (int[])OrginValue;
			int[] Current = (int[])CurrentValue;
            int index = -1;
            if (strProName == "naMoreDamagePer[MONSTER_BODY_TYPE_max]")
            {
                int nType = (int)PropertyType;
                int nSmall = (int)HeaderProto.ESummonObjectPro.SUMMONOBJECT_PRO_BodyType_Small;
                index = (nType >= nSmall ? nType - nSmall : -1);
            }
			for(int i = 0; i < Orgin.Length;i++)
			{
                if(index != -1 && i != index)
                    continue;
				int iValue = ConvertValue(dataType, dwOperationData1, Orgin[i]);
				Current[i] = GetModifyValue(bBegin, opType, Orgin[i], Current[i], iValue);
			}
			summonInfo[strProName] = Current;
		}
        else if (OrginValue.GetType() == typeof(int))
        {
            int Orgin = (int)OrginValue;
            int Current = (int)CurrentValue;
            int iValue = ConvertValue(dataType, dwOperationData1, Orgin);
            summonInfo[strProName] = GetModifyValue(bBegin, opType, Orgin, Current, iValue);
        }
	}
	void	ModifySkillEffectProperty(
		HeaderProto.ESkillEffectPro PropertyType,
		HeaderProto.EOpreationFlag opType,
		HeaderProto.EOpreationFlag dataType,
		int data,
		HeaderProto.EEffectSkillBuffOptType optType,
		int SkillEffect_UID,
		bool bBegin
		)
	{
		string	PropertyName	=	sdActorInterface.SkillEffectName[(int)PropertyType];
		if(PropertyName.Length==0)
		{
			return;
		}
		SkillEffect	baseSkillEffect	=	sdConfDataMgr.Instance().m_BaseSkillEffect[SkillEffect_UID] as SkillEffect;
		Hashtable skillEffectArray = GetActor().GetSkillEffect();
		SkillEffect	skillEffect	= skillEffectArray[SkillEffect_UID] as SkillEffect;

		int Orgin	=	(int)sdConfDataMgr.GetMemberValue(baseSkillEffect,PropertyName);
		int	Current	=	(int)sdConfDataMgr.GetMemberValue(skillEffect,PropertyName);
		int	iValue	=	ConvertValue(dataType,data,Orgin);
		
		Current	=	GetModifyValue(bBegin,opType,Orgin,Current,iValue);
		
		sdConfDataMgr.SetMemberValue(skillEffect,PropertyName,Current);
		
	}
	
	void	DoPeriodicEffect()
	{
		for(int i = 0; i < 8; i++)
		{
			if(effects[i] == null)
				continue;
			int iType	=	(int)effects[i]["byEffectType"];
			//int id		=	(int)effects[i]["dwOperationID"];
			//AOE半径.
			int nAoeArea	=	(int)prop["nArea"];
		
			if( iType	== (int)HeaderProto.EBuffEffectType.BUFF_EFFECT_PERIODIC)
			{
				int dwPeriodicData	=	(int)effects[i]["dwPeriodicData"];
				if(nAoeArea==0)
				{
					sdBuffEffect.Add(m_CasterActor,GetActor(),effects[i],HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START,m_Layer);
				}
				else
				{
					//获取AOE范围内的所有目标.
					List<sdActorInterface> lstAoe	=	GetEffectedActors();
                    if (lstAoe != null)
                    {
                        foreach (sdActorInterface a in lstAoe)
                        {
                            sdBuffEffect.Add(m_CasterActor, a, effects[i], HeaderProto.EDoOperationType.DO_OPERATION_TYPE_START, m_Layer);
                        }
                    }
				}
				
			}
			
		}
	}
	void	DoEndEffect(HeaderProto.EBuffEndType endTpye)
	{
		for(int i = 0; i < 8; i++)
		{
			if(effects[i] == null)
				continue;
			int iType	=	(int)effects[i]["byEffectType"];
			//int id		=	(int)effects[i]["dwOperationID"];
			HeaderProto.EBuffEndType et	=	(HeaderProto.EBuffEndType)effects[i]["byEffectExplain"];
			if(et!=HeaderProto.EBuffEndType.BUFF_END_NONE)
			{
				if(et!=endTpye)
				{
					continue;
				}
				int dwPeriodicData	=	(int)effects[i]["dwPeriodicData"];
				switch(dwPeriodicData)
				{
					case 0:{
						sdBuffEffect.Add(m_CasterActor,GetActor(),effects[i],HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE,m_Layer);
						break;}
					case 1:{
						sdBuffEffect.Add(m_CasterActor,m_CasterActor,effects[i],HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE,m_Layer);
						break;}
					case 2:{
						//获取AOE范围内的所有目标.
						List<sdActorInterface> lstAoe	=	GetEffectedActors();
						foreach(sdActorInterface a in lstAoe)
						{
							sdBuffEffect.Add(m_CasterActor,a,effects[i],HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE,m_Layer);
						}
						break;}
				}
			}
		}
	}
	void	PlayEffect()
	{
		if(m_Effect!=null)
		{
			return;
		}
		Hashtable	buffs	=	sdConfDataMgr.Instance().GetTable("buff");
		object table	=	buffs[templateId.ToString()];
		if(table!=null)
		{
			Hashtable	buffinfo	=	table as Hashtable;
			if(buffinfo!=null)
			{
				object texiao	=	buffinfo["Effect"];
				if(texiao!=null)
				{
					string	str	=	texiao as string;
					if(str!=null && str.Length > 0)
					{
						object bind	=	buffinfo["Dummy"];
						sdActorInterface actor	=	GetActor();
						GameObject bindNode	=	GetActor().gameObject;
                        bool bHead = false;
						if(actor.actorType == ActorType.AT_Player)
						{
							sdGameActor gactor 	=	(sdGameActor)actor;
							if(bind!=null)
							{
								int	Dummy	=	(int)bind;
								if(Dummy==1)
								{
									bindNode	=	gactor.GetNode("Bip01_attack_target");
								}
								else if(Dummy==2)
								{
									bindNode	=	gactor.GetNode("Bip01 HeadNub");
                                    bHead = true;
								}
							}
						}
						else
						{
							sdGameMonster monster 	=	(sdGameMonster)actor;
							if(bind!=null)
							{
								int	Dummy	=	(int)bind;
								if(Dummy==1)
								{
									bindNode	=	monster.GetNode(monster.ChestPoint.Replace("\r\n",""));
								}
								else if(Dummy==2)
								{
									bindNode	=	monster.GetNode(monster.HeadPoint.Replace("\r\n",""));
                                    bHead = true;
								}
							}
						}
						if(bindNode!=null)
						{
							bool bRotate = (int.Parse((string)buffinfo["EffectRotate"])) != 0;
                            if (bHead)
                            {
                                CharacterController cc = actor.GetComponent<CharacterController>();
                                AddEffect(str, actor.transform, 0.0f, new Vector3(0, cc.height, 0), false);
                            }
                            else
                            {
                                AddEffect(str, bindNode.transform, 0.0f, new Vector3(0, 0.0f, 0), bRotate);
                            }
						}
						else
						{
							AddEffect(str,GetActor().transform,0.0f,new Vector3(0,0,0), true);
						}
					}
				}
			}
		}
	}
	public	void	AddEffect(
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
		param.userdata2 =   bRotate;
		sdResourceMgr.Instance.LoadResource(prefab,OnLoadEffect,param);
	}
	protected void OnLoadEffect(ResLoadParams param,UnityEngine.Object obj)
	{
		if(obj==null)
		{
			return;
		}
		if(holder.GetCurrentHP()<=0)
		{
			return;
		}
		GameObject gObj	=	GameObject.Instantiate(obj) as GameObject;
		if(gObj!=null)
		{
			m_Effect	=	gObj;
			bool bRotate = ((bool)param.userdata2);
			if(!bRotate)
			{
				m_Effect.transform.parent				= null;
				m_Effect.transform.localPosition        = Vector3.zero;
				m_Effect.transform.localRotation		= Quaternion.identity;
				sdAdjustPosition  adjust   = m_Effect.AddComponent<sdAdjustPosition>();
				adjust.parent = (Transform)param.userdata0;
                adjust.posOffset = param.pos;
			}
			else
			{
				m_Effect.transform.parent				=	(Transform)param.userdata0;
				m_Effect.transform.localPosition		=	param.pos;
				m_Effect.transform.localRotation		=	param.rot;
				m_Effect.transform.localScale			=	param.scale;
			}
		}
	}
	protected List<sdActorInterface>	GetEffectedActors()
	{
		List<sdActorInterface>	lstActor	=	null;
		
		int byAuraAimType		=	(int)prop["byAuraAimType"];
		if(byAuraAimType==(int)HeaderProto.ESkillObjType.SKILL_OBJ_SELF)
		{
			lstActor 	=	new List<sdActorInterface>();
			lstActor.Add(GetActor());
			return lstActor;
		}
		
		
		int byAuraAreaType		=	(int)prop["byAuraAreaType"];
		//if(byAuraAreaType==(int)HeaderProto.ESkillAreaType.SKILL_AREA_TYPE_ROUND)
		{
			int nArea				=	(int)prop["nArea"];
			int byAuraCenterType	=	(int)prop["byAuraCenterType"];
			int nAuraCenterData		=	(int)prop["nAuraCenterData"];
			int nAuraAreaData		=	(int)prop["nAuraAreaData"];
			
			Vector3 vCenter			=	GetActor().transform.position;
			Vector3	vDir			=	GetActor().GetDirection();
			if(byAuraCenterType==(int)HeaderProto.EAoeCenter.AOE_CETER_FRONT)
			{
				vCenter+=	vDir*(nAuraCenterData)*0.001f;
			}
			else if(byAuraCenterType==(int)HeaderProto.EAoeCenter.AOE_CETER_BACK)
			{
				vCenter-=	vDir*(nAuraCenterData)*0.001f;
			}
			
			float fRadius	=	nArea*0.001f;		
			lstActor = sdGameLevel.instance.actorMgr.FindActor(
				m_CasterActor,
				(HeaderProto.ESkillObjType)byAuraAimType,
				vCenter,
				Vector3.zero,
				1,
				0,
				fRadius,
				true);	
		}
		
		return lstActor;
	}

    public void AddLayer()
    {
        int maxLayerCount = GetMaxLayerCount();
        if (maxLayerCount > m_Layer)
        {
            DoBeginEffect(HeaderProto.EDoOperationType.DO_OPERATION_TYPE_REMOVE, m_Layer);
            m_Layer++;
            DoBeginEffect(HeaderProto.EDoOperationType.DO_OPERATION_TYPE_ADD, m_Layer);
        }
        Refresh();
    }
}
