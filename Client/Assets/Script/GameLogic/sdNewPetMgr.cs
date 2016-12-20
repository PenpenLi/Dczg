using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PetEquipType
{
	Pet_EquipType_all = -1,
	Pet_EquipType_wq = 0,
	Pet_EquipType_sp = 1,
	Pet_EquipType_fj = 2,
}

public enum PetSortType
{
	Pet_SortBy_Level = 0,
	Pet_SortBy_Color = 1,
	Pet_SortBy_Up = 2,
}

//宠物属性信息aa
public class SClientPetProperty
{
	public	int		m_iStr = 0;				// 当前力量aa
	public	int		m_iInt = 0;				// 当前智力aa
	public	int		m_iDex = 0;				// 当前敏捷aa
	public	int		m_iSta = 0;				// 当前体力aa
	public	int		m_iFai = 0;				// 当前信念aa
	
	public	int		m_iMaxHP = 0;
	public	int		m_iMaxSP = 0;
	
	public	int		m_iHPTick = 0;			// 每轮回复血量aa
    public	int		m_iSPTick = 0;			// 每轮回复技力量aa
    public	int		m_iAtkDmgMin = 0;		// 当前伤害下限aa
    public	int		m_iAtkDmgMax = 0;		// 当前伤害上限aa

    public	int		m_iDef = 0;				// 当前防御aa

    public	int		m_iIceAtt = 0;			// 冰属性攻击aa
    public	int		m_iFireAtt = 0;			// 火属性攻击aa
    public	int		m_iPoisonAtt = 0;		// 毒属性攻击aa
    public	int		m_iThunderAtt = 0;		// 雷属性攻击aa

    public	int		m_iIceDef = 0;			// 冰属性抵抗aa
    public	int		m_iFireDef = 0;			// 火属性抵抗aa
    public	int		m_iPoisonDef = 0;		// 毒属性抵抗aa
    public	int		m_iThunderDef = 0;		// 雷属性抵抗aa

    public	int		m_iPierce = 0;			// 穿透点数aa
    public	int		m_iHit = 0;				// 命中点数aa
    public	int		m_iDodge = 0;			// 闪避点数aa
    public	int		m_iCri = 0;				// 致命一击点数aa
    public	int		m_iFlex = 0;			// 韧性点数aa

    public	int		m_iCriDmg = 0;			// 致命一击伤害系数 15000 = 1.5
    public	int		m_iCriDmgDef = 0;		// 致命一击防御系数修正 正负 万分比aa

    public	int		m_iBodySize = 0;		// 体型半径 mm
    public	int		m_iAttSize = 0;			// 攻击半径 mm

    public	int		m_iAttSpeedModPer = 0;	// 攻击加速百分比aa
    public	int		m_iMoveSpeedModPer = 0;	// 移动加速百分比aa

    public	int		m_iPiercePer = 0;		// 穿透百分比修正 正负 万分比aa
    public	int		m_iHitPer = 0;			// 命中率修正  正负  万分比aa
    public	int		m_iDodgePer = 0;		// 闪避率修正 正负  万分比aa
    public	int		m_iCriPer = 0;			// 致命一击率修正 正负 万分比aa
    public	int		m_iFlexPer = 0;			// 韧性率修正  正负 万分比aa
    public	int		m_iAttSpeed = 0;		// 攻击速度aa
    public	int		m_iMoveSpeed = 0;		// 基础移动速度 1秒移动多少mm

	public	int		m_iMaxEP = 0;			/// 最大协助点数 aa
	public	int		m_iMaxAP = 0;			/// 最大活力值aa
	public	int		m_iExpPer = 0;			/// 经验值 正负 万分比aa
	public	int		m_iMoneyPer = 0;		/// 金钱 正负 万分比mm
}

// 宠物属性计算相关aa
public class SClientPetCoe
{
	public int m_iAttA = 0;		// 攻击次方aa
	public int m_iAttB = 0;		// 攻击倍数aa
	public int m_iAttC = 0;		// 攻击系数aa
	
	public int m_iDefA = 0;		// 防御次方aa
	public int m_iDefB = 0;		// 防御倍数aa
	public int m_iDefC = 0;		// 防御系数aa
	
	public int m_iHPA = 0;		// 生命次方aa
	public int m_iHPB = 0;		// 生命倍数aa
	public int m_iHPC = 0;		// 生命系数aa
}

// 宠物动态信息aa
public class SClientPetInfo
{
	//宠物基础信息aa
	public	UInt64	m_uuDBID = UInt64.MaxValue;
	public	int		m_iBattlePos = 0;
	public	uint	m_uiTemplateID = 0;	
	public	int		m_iLevel = 0;	
	public	UInt64	m_uuExperience = 0;
	public	int		m_iUp = 0;
	public	int		m_Lock = 0;
	//宠物当前HP,SP
	public	int		m_iHP = 0;
	public	int		m_iSP = 0;
	//宠物当前属性aa
	public	SClientPetProperty m_CurProperty = new SClientPetProperty();
	//宠物模板数据信息aa
	public	string	m_strName = "";
	public	int		m_iAbility = 0;
	public	int		m_iBodyType = 0;
	public	int		m_iKnockDownDef = 0;
    public	int		m_iKnockFlyDef = 0;
    public	int		m_iKnockBackDef = 0;
    public	int		m_iEyeSize = 0;
    public	int		m_iChaseSize = 0;
	
	public	int		m_iDfSkill = 0;
    public	int		m_iSpSkill = 0;
    public	int		m_iSapSkill = 0;
	public	SClientPetProperty	m_TemplateProperty = new SClientPetProperty();
	public	SClientPetCoe		m_TemplateCoe = new SClientPetCoe();
    public	int		m_iBaseJob = 0;		// 参见EPetJob
    public	string	m_strDesc = "";		// 介绍aa
    public	string	m_strSPD1 = "";		// 特点1
    public	string	m_strSPD2 = "";		// 特点2
    public	string	m_strRes = "";		// 资源地址aa

    public	int		m_iSkill1 = 0;
    public	int		m_iSkill2 = 0;
    public	int		m_iSkill3 = 0;
    public	int		m_iSkill4 = 0;
    public	int		m_iBuff1 = 0;
    public	int		m_iBuff2 = 0;
    public	int		m_iBuff3 = 0;
    public	int		m_iBuff4 = 0;
    public	int		m_iAIID = 0;
    public	string	m_strIcon = "";		// Icon

	public	int		m_FollowDistance = 0;
	public	int		m_BattleFollowDistance = 0;
	public	int		m_Speed = 0;
	public	int		m_BeatPriority = 0;
	public	int		m_DisappearTime = 0;
	public	int		m_HPBarNum = 0;
	public	int		m_HPBarHeight = 0;
	public	int		m_FriAIID = 0;
	public	int		m_SellMoney = 0;

	public	int		m_GID = 0;
	public	int		m_CanLevelUp = 0;
	public	int		m_CanMerge = 0;
	public	int		m_ExpBase = 0;
	public	int		m_ExpCoe = 0;
	public	int		m_Exp2Coe = 0;
	public	int		m_MoneyCoe = 0;
	public	int		m_Crystal = 0;
	public	int		m_UpMoneyCoe = 0;

	public	Hashtable m_EquipedDB = new Hashtable();

	static public int PetSortByLevel(SClientPetInfo item, SClientPetInfo compare)
	{
		if (compare.m_iLevel > item.m_iLevel)
		{
			return 1;	
		}
		else if (compare.m_iLevel < item.m_iLevel)
		{
			return -1;	
		}	
		else
		{
			if (compare.m_iAbility > item.m_iAbility) 
			{
				return 1;
			}
			else if (compare.m_iAbility < item.m_iAbility)
			{
				return -1;
			}
			else
			{
				if (compare.m_iUp > item.m_iUp) 
				{
					return 1;
				}
				else if (compare.m_iUp < item.m_iUp)
				{
					return -1;
				}
				else
				{
					if (compare.m_uiTemplateID > item.m_uiTemplateID)
					{
						return -1;
					}
					else if (compare.m_uiTemplateID < item.m_uiTemplateID)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	}

	static public int PetSortByLevelBeginBig(SClientPetInfo item, SClientPetInfo compare)
	{
		if (compare.m_iLevel > item.m_iLevel)
		{
			return -1;	
		}
		else if (compare.m_iLevel < item.m_iLevel)
		{
			return 1;	
		}	
		else
		{
			if (compare.m_iAbility > item.m_iAbility) 
			{
				return -1;
			}
			else if (compare.m_iAbility < item.m_iAbility)
			{
				return 1;
			}
			else
			{
				if (compare.m_iUp > item.m_iUp) 
				{
					return -1;
				}
				else if (compare.m_iUp < item.m_iUp)
				{
					return 1;
				}
				else
				{
					if (compare.m_uiTemplateID < item.m_uiTemplateID)
					{
						return -1;
					}
					else if (compare.m_uiTemplateID > item.m_uiTemplateID)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	}

	static public int PetSortByAbility(SClientPetInfo item, SClientPetInfo compare)
	{
		if (compare.m_iAbility > item.m_iAbility)
		{
			return 1;	
		}
		else if (compare.m_iAbility < item.m_iAbility)
		{
			return -1;	
		}	
		else
		{
			if (compare.m_iUp > item.m_iUp) 
			{
				return 1;
			}
			else if (compare.m_iUp < item.m_iUp)
			{
				return -1;
			}
			else
			{
				if (compare.m_iLevel > item.m_iLevel) 
				{
					return 1;
				}
				else if (compare.m_iLevel < item.m_iLevel)
				{
					return -1;
				}
				else
				{
					if (compare.m_uiTemplateID > item.m_uiTemplateID)
					{
						return -1;
					}
					else if (compare.m_uiTemplateID < item.m_uiTemplateID)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	}

	static public int PetSortByAbilityBeginBig(SClientPetInfo item, SClientPetInfo compare)
	{
		if (compare.m_iAbility > item.m_iAbility)
		{
			return -1;	
		}
		else if (compare.m_iAbility < item.m_iAbility)
		{
			return 1;	
		}	
		else
		{
			if (compare.m_iUp > item.m_iUp) 
			{
				return -1;
			}
			else if (compare.m_iUp < item.m_iUp)
			{
				return 1;
			}
			else
			{
				if (compare.m_iLevel > item.m_iLevel) 
				{
					return -1;
				}
				else if (compare.m_iLevel < item.m_iLevel)
				{
					return 1;
				}
				else
				{
					if (compare.m_uiTemplateID < item.m_uiTemplateID)
					{
						return -1;
					}
					else if (compare.m_uiTemplateID > item.m_uiTemplateID)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	}
}

//用在图鉴排序中的宠物模板类..
public class SPetSmallClass
{
	public int iTemplateID = 0;
	public int iAbility = 0;
	public int iCurNum = 0;
	public int iMaxNum = 0;

	static public int PetTujianSortByAbilityBeginBig(SPetSmallClass item, SPetSmallClass compare)
	{
		if (compare.iAbility > item.iAbility)
		{
			return -1;	
		}
		else if (compare.iAbility < item.iAbility)
		{
			return 1;	
		}	
		else
		{
			return 0;
		}
	}

	static public int PetTujianSortByAbilityBeginSmall(SPetSmallClass item, SPetSmallClass compare)
	{
		if (compare.iAbility > item.iAbility)
		{
			return 1;
		}
		else if (compare.iAbility < item.iAbility)
		{
			return -1;
		}	
		else
		{
			return 0;
		}
	}
}

// 宠物管理aa
public class sdNewPetMgr : Singleton<sdNewPetMgr>
{
	public const int MAX_PET_STRONG_LEVEL = 5;
	public const int MAX_PET_CAN_STRONG_LEVEL = 20;
	public const int MAX_PET_LEVEL = 120;

	// 拥有过的宠物列表..
	public List<int> m_petGettedList = new List<int>();
	// 宠物列表aa
	private Hashtable m_petListInfo = new Hashtable();
	
	// 宠物列表(哈希表)aa
	protected Hashtable mPetList = new Hashtable();

	// 宠物当前战队,总共7个位置(保存宠物的DBID)
	protected UInt64[] mPetTeam = 
	{
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
	};

	// 宠物战队个数aa
	protected const int mPetTeamSize = 7;	
	public int PetTeamSize
	{
		get { return mPetTeamSize;}
	}
	
	// 参展宠物个数aa
	protected const int mBattlePetNum = 3;
	public int BattlePetNum
	{
		get { return mBattlePetNum;}
	}

	// 当前激活宠物的索引,回值为宠物队伍位置(当前为0/1/2,-1代表当前没有任何宠物被激活)aa
	protected int mActivePetIndex = -1;
	public int ActivePetIndex
	{
		get { return mActivePetIndex; }
	}

	// 好友宠物属性aa
	protected Hashtable mFriendPetProperty = null;
	public Hashtable FriendPetProperty
	{
		get { return mFriendPetProperty; }
	}

	// 是否激活好友宠物aa
	protected bool mIsFriendPetActived = false;
	public bool IsFriendPetActived
	{
		get { return mIsFriendPetActived;}
	}

	
	// 宠物所有战队,总共21个位置(保存宠物的DBID)
	public UInt64[] mPetAllTeam = 
	{
		//team0..
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		//team1..
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		//team2..
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 参战宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
		UInt64.MaxValue,		//< 助阵宠物aa
	};
	
	public int mPetCurTeam = 0;
	
	public UInt64[] m_uuPetLevelSelectID = 
	{
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue, 
		UInt64.MaxValue
	};
	
	public UInt64 m_uuPetStrongSelectID = UInt64.MaxValue;
	// 主角的宠物装备背包..
	public Hashtable petItemDB = new Hashtable();
	// 主角的宠物碎片信息(装备碎片等等其实也在里面)..
	public Hashtable petGatherItemDB = new Hashtable();

	//初始化数据..
	public void ClearData()
	{
		m_petListInfo.Clear();
		mPetList.Clear();

		for (int i=0;i<mPetTeamSize;i++)
		{
			mPetTeam[i] = UInt64.MaxValue;
		}

		for (int i=0;i<21;i++)
		{
			mPetAllTeam[i] = UInt64.MaxValue;
		}

		mPetCurTeam = 0;

		for (int i=0;i<8;i++)
		{
			m_uuPetLevelSelectID[i] = UInt64.MaxValue;
		}

		m_uuPetStrongSelectID = UInt64.MaxValue;
		petItemDB.Clear();
		petGatherItemDB.Clear();
	}

	//宠物背包当前数量..
	public int GetMyPetCount()
	{
		return m_petListInfo.Count;
	}

	//宠物背包最大数量..
	public int GetMyPetMaxCount()
	{
		int iMax = (int)HeaderProto.MAX_PET_COUNT;
		iMax = iMax - 10;
		return iMax;
	}

	//服务器下发拥有过的宠物模板ID列表..
	public void CreatePetRecord(CliProto.SC_PETS_RECORD_NTF msg)
	{
		m_petGettedList.Clear();
		CliProto.SC_PETS_RECORD_NTF refMSG = msg;
		ushort num = refMSG.m_PetRecordCount;
		for (ushort i = 0; i < num; i++)
			m_petGettedList.Add(refMSG.m_Record[i].m_TID);
	}

	// 是否拥有过该宠物..
	public bool IsPetHasGetted(int iID)
	{
		bool bResult = false;
		foreach (int iListID in m_petGettedList)
		{
			if (iListID==iID)
			{
				bResult = true;
				break;
			}
		}

		return bResult;
	}
	
	// 服务器下发宠物信息aa
	public void CreatePetInfo(CliProto.SC_USER_PETS_NTF msg)
	{
		m_petListInfo.Clear();
		mPetList.Clear();
		
		CliProto.SC_USER_PETS_NTF refMSG = msg;
		int num = refMSG.m_PetCount;
		for (int i = 0; i < num; i++)
		{
			UInt64 uuDBID = refMSG.m_PetsInfo[i].m_DBID;
			if (uuDBID != UInt64.MaxValue)
			{
				SClientPetInfo pet = new SClientPetInfo();
				
				pet.m_uuDBID = refMSG.m_PetsInfo[i].m_DBID;
				pet.m_iBattlePos = refMSG.m_PetsInfo[i].m_BattlePos;
				pet.m_uiTemplateID = refMSG.m_PetsInfo[i].m_TemplateID;	
				pet.m_iLevel = refMSG.m_PetsInfo[i].m_Level;
				pet.m_uuExperience = (UInt64)refMSG.m_PetsInfo[i].m_Experience;
				pet.m_iUp = refMSG.m_PetsInfo[i].m_Up;
				pet.m_Lock = refMSG.m_PetsInfo[i].m_Lock;
				pet.m_iHP = refMSG.m_PetsInfo[i].m_HP;
				pet.m_iSP = refMSG.m_PetsInfo[i].m_SP;
				
				pet.m_CurProperty.m_iStr = refMSG.m_PetsInfo[i].m_Str;
				pet.m_CurProperty.m_iInt = refMSG.m_PetsInfo[i].m_Int;
				pet.m_CurProperty.m_iDex = refMSG.m_PetsInfo[i].m_Dex;
				pet.m_CurProperty.m_iSta = refMSG.m_PetsInfo[i].m_Sta;
				pet.m_CurProperty.m_iFai = refMSG.m_PetsInfo[i].m_Fai;
				pet.m_CurProperty.m_iMaxHP = refMSG.m_PetsInfo[i].m_MaxHP;
				pet.m_CurProperty.m_iMaxSP = refMSG.m_PetsInfo[i].m_MaxSP;
				pet.m_CurProperty.m_iHPTick = refMSG.m_PetsInfo[i].m_HPTick;
			    pet.m_CurProperty.m_iSPTick = refMSG.m_PetsInfo[i].m_SPTick;
			    pet.m_CurProperty.m_iAtkDmgMin = refMSG.m_PetsInfo[i].m_AtkDmgMin;
			    pet.m_CurProperty.m_iAtkDmgMax = refMSG.m_PetsInfo[i].m_AtkDmgMax;
			    pet.m_CurProperty.m_iDef = refMSG.m_PetsInfo[i].m_Def;
			    pet.m_CurProperty.m_iIceAtt = refMSG.m_PetsInfo[i].m_IceAtt;
			    pet.m_CurProperty.m_iFireAtt = refMSG.m_PetsInfo[i].m_FireAtt;
			    pet.m_CurProperty.m_iPoisonAtt = refMSG.m_PetsInfo[i].m_PoisonAtt;
			    pet.m_CurProperty.m_iThunderAtt = refMSG.m_PetsInfo[i].m_ThunderAtt;
			    pet.m_CurProperty.m_iIceDef = refMSG.m_PetsInfo[i].m_IceDef;
			    pet.m_CurProperty.m_iFireDef = refMSG.m_PetsInfo[i].m_FireDef;
			    pet.m_CurProperty.m_iPoisonDef = refMSG.m_PetsInfo[i].m_PoisonDef;
			    pet.m_CurProperty.m_iThunderDef = refMSG.m_PetsInfo[i].m_ThunderDef;
			    pet.m_CurProperty.m_iPierce = refMSG.m_PetsInfo[i].m_Pierce;
			    pet.m_CurProperty.m_iHit = refMSG.m_PetsInfo[i].m_Hit;
			    pet.m_CurProperty.m_iDodge = refMSG.m_PetsInfo[i].m_Dodge;
			    pet.m_CurProperty.m_iCri = refMSG.m_PetsInfo[i].m_Cri;
			    pet.m_CurProperty.m_iFlex = refMSG.m_PetsInfo[i].m_Flex;
			    pet.m_CurProperty.m_iCriDmg = refMSG.m_PetsInfo[i].m_CriDmg;
			    pet.m_CurProperty.m_iCriDmgDef = refMSG.m_PetsInfo[i].m_CriDmgDef;
			    pet.m_CurProperty.m_iBodySize = refMSG.m_PetsInfo[i].m_BodySize;
			    pet.m_CurProperty.m_iAttSize = refMSG.m_PetsInfo[i].m_AttSize;
			    pet.m_CurProperty.m_iAttSpeedModPer = refMSG.m_PetsInfo[i].m_AttSpeedModPer;
			    pet.m_CurProperty.m_iMoveSpeedModPer = refMSG.m_PetsInfo[i].m_MoveSpeedModPer;
			    pet.m_CurProperty.m_iPiercePer = refMSG.m_PetsInfo[i].m_PiercePer;
			    pet.m_CurProperty.m_iHitPer = refMSG.m_PetsInfo[i].m_HitPer;
			    pet.m_CurProperty.m_iDodgePer = refMSG.m_PetsInfo[i].m_DodgePer;
			    pet.m_CurProperty.m_iCriPer = refMSG.m_PetsInfo[i].m_CriPer;
			    pet.m_CurProperty.m_iFlexPer = refMSG.m_PetsInfo[i].m_FlexPer;
			    pet.m_CurProperty.m_iAttSpeed = refMSG.m_PetsInfo[i].m_AttSpeed;
			    pet.m_CurProperty.m_iMoveSpeed = refMSG.m_PetsInfo[i].m_MoveSpeed;
				
				//宠物模板数据信息aa
				uint uiTemplateID = refMSG.m_PetsInfo[i].m_TemplateID;
				Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(uiTemplateID.ToString());
				if (info != null)
				{
					if (info["Name"].ToString() != "")
					{
						pet.m_strName = info["Name"].ToString();
					}
					
					if (info["Ability"].ToString() != "")
					{
						pet.m_iAbility = int.Parse(info["Ability"].ToString());
					}
					
					if (info["BodyType"].ToString() != "")
					{
						pet.m_iBodyType = int.Parse(info["BodyType"].ToString());
					}
					
					if (info["KnockDownDef"].ToString() != "")
					{
						pet.m_iKnockDownDef = int.Parse(info["KnockDownDef"].ToString());
					}
					
					if (info["KnockFlyDef"].ToString() != "")
					{
						pet.m_iKnockFlyDef = int.Parse(info["KnockFlyDef"].ToString());
					}
					
					if (info["KnockBackDef"].ToString() != "")
					{
						pet.m_iKnockBackDef = int.Parse(info["KnockBackDef"].ToString());
					}
					
					if (info["EyeSize"].ToString() != "")
					{
						pet.m_iEyeSize = int.Parse(info["EyeSize"].ToString());
					}
					
					if (info["ChaseSize"].ToString() != "")
					{
						pet.m_iChaseSize = int.Parse(info["ChaseSize"].ToString());
					}

					if (info["DfSkill"].ToString() != "")
					{
						pet.m_iDfSkill = int.Parse(info["DfSkill"].ToString());
					}
					
					if (info["SpSkill"].ToString() != "")
					{
						pet.m_iSpSkill = int.Parse(info["SpSkill"].ToString());
					}
					
					if (info["SapSkill"].ToString() != "")
					{
						pet.m_iSapSkill = int.Parse(info["SapSkill"].ToString());
					}
					
					//模板属性aa
					if (info["Property.Str"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iStr = int.Parse(info["Property.Str"].ToString());
					}
					if (info["Property.Int"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iInt = int.Parse(info["Property.Int"].ToString());
					}
					if (info["Property.Dex"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDex = int.Parse(info["Property.Dex"].ToString());
					}
					if (info["Property.Sta"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iSta = int.Parse(info["Property.Sta"].ToString());
					}
					if (info["Property.Fai"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFai = int.Parse(info["Property.Fai"].ToString());
					}
					if (info["Property.MaxHP"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMaxHP = int.Parse(info["Property.MaxHP"].ToString());
					}
					if (info["Property.MaxSP"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMaxSP = int.Parse(info["Property.MaxSP"].ToString());
					}
					if (info["Property.HPTick"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHPTick = int.Parse(info["Property.HPTick"].ToString());
					}
					if (info["Property.SPTick"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iSPTick = int.Parse(info["Property.SPTick"].ToString());
					}
					if (info["Property.AtkDmgMin"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAtkDmgMin = int.Parse(info["Property.AtkDmgMin"].ToString());
					}
					if (info["Property.AtkDmgMax"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAtkDmgMax = int.Parse(info["Property.AtkDmgMax"].ToString());
					}
					if (info["Property.Def"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDef = int.Parse(info["Property.Def"].ToString());
					}
					if (info["Property.IceAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iIceAtt = int.Parse(info["Property.IceAtt"].ToString());
					}
					if (info["Property.FireAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFireAtt = int.Parse(info["Property.FireAtt"].ToString());
					}
					if (info["Property.PoisonAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPoisonAtt = int.Parse(info["Property.PoisonAtt"].ToString());
					}
					if (info["Property.ThunderAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iThunderAtt = int.Parse(info["Property.ThunderAtt"].ToString());
					}
					if (info["Property.IceDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iIceDef = int.Parse(info["Property.IceDef"].ToString());
					}
					if (info["Property.FireDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFireDef = int.Parse(info["Property.FireDef"].ToString());
					}
					if (info["Property.PoisonDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPoisonDef = int.Parse(info["Property.PoisonDef"].ToString());
					}
					if (info["Property.ThunderDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iThunderDef = int.Parse(info["Property.ThunderDef"].ToString());
					}
					if (info["Property.Pierce"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPierce = int.Parse(info["Property.Pierce"].ToString());
					}
				    if (info["Property.Hit"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHit = int.Parse(info["Property.Hit"].ToString());
					}
					if (info["Property.Dodge"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDodge = int.Parse(info["Property.Dodge"].ToString());
					}
				    if (info["Property.Cri"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCri = int.Parse(info["Property.Cri"].ToString());
					}
					if (info["Property.Flex"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFlex = int.Parse(info["Property.Flex"].ToString());
					}
					if (info["Property.CriDmg"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriDmg = int.Parse(info["Property.CriDmg"].ToString());
					}
				    if (info["Property.CriDmgDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriDmgDef = int.Parse(info["Property.CriDmgDef"].ToString());
					}
					if (info["Property.BodySize"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iBodySize = int.Parse(info["Property.BodySize"].ToString());
					}
				    if (info["Property.AttSize"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSize = int.Parse(info["Property.AttSize"].ToString());
					}
					if (info["Property.AttSpeedModPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSpeedModPer = int.Parse(info["Property.AttSpeedModPer"].ToString());
					}
					if (info["Property.MoveSpeedModPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMoveSpeedModPer = int.Parse(info["Property.MoveSpeedModPer"].ToString());
					}
					if (info["Property.PiercePer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPiercePer = int.Parse(info["Property.PiercePer"].ToString());
					}
					if (info["Property.HitPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHitPer = int.Parse(info["Property.HitPer"].ToString());
					}
					if (info["Property.DodgePer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDodgePer = int.Parse(info["Property.DodgePer"].ToString());
					}
					if (info["Property.CriPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriPer = int.Parse(info["Property.CriPer"].ToString());
					}
					if (info["Property.FlexPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFlexPer = int.Parse(info["Property.FlexPer"].ToString());
					}
				    if (info["Property.AttSpeed"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSpeed = int.Parse(info["Property.AttSpeed"].ToString());
					}
					if (info["Property.MoveSpeed"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMoveSpeed = int.Parse(info["Property.MoveSpeed"].ToString());
					}
				    //属性计算参数aa
				    if (info["Coe.AttA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttA = int.Parse(info["Coe.AttA"].ToString());
					}
					if (info["Coe.AttB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttB = int.Parse(info["Coe.AttB"].ToString());
					}
					if (info["Coe.AttC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttC = int.Parse(info["Coe.AttC"].ToString());
					}
					if (info["Coe.DefA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefA = int.Parse(info["Coe.DefA"].ToString());
					}
					if (info["Coe.DefB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefB = int.Parse(info["Coe.DefB"].ToString());
					}
					if (info["Coe.DefC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefC = int.Parse(info["Coe.DefC"].ToString());
					}
					if (info["Coe.HPA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPA = int.Parse(info["Coe.HPA"].ToString());
					}
					if (info["Coe.HPB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPB = int.Parse(info["Coe.HPB"].ToString());
					}
					if (info["Coe.HPC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPC = int.Parse(info["Coe.HPC"].ToString());
					}
					//其他信息aa
					if (info["BaseJob"].ToString() != "")
					{
						pet.m_iBaseJob = int.Parse(info["BaseJob"].ToString());
					}
					if (info["Desc"].ToString() != "")
					{
						pet.m_strDesc = info["Desc"].ToString();
					}
					if (info["SPD1"].ToString() != "")
					{
						pet.m_strSPD1 = info["SPD1"].ToString();
					}
					if (info["SPD2"].ToString() != "")
					{
						pet.m_strSPD2 = info["SPD2"].ToString();
					}
					if (info["Res"].ToString() != "")
					{
						pet.m_strRes = info["Res"].ToString();
					}
					if (info["Skill1"].ToString() != "")
					{
						pet.m_iSkill1 = int.Parse(info["Skill1"].ToString());
					}
				    if (info["Skill2"].ToString() != "")
					{
						pet.m_iSkill2 = int.Parse(info["Skill2"].ToString());
					}
					if (info["Skill3"].ToString() != "")
					{
						pet.m_iSkill3 = int.Parse(info["Skill3"].ToString());
					}
					if (info["Skill4"].ToString() != "")
					{
						pet.m_iSkill4 = int.Parse(info["Skill4"].ToString());
					}
					if (info["Buff1"].ToString() != "")
					{
						pet.m_iBuff1 = int.Parse(info["Buff1"].ToString());
					}
				    if (info["Buff2"].ToString() != "")
					{
						pet.m_iBuff2 = int.Parse(info["Buff2"].ToString());
					}
					if (info["Buff3"].ToString() != "")
					{
						pet.m_iBuff3 = int.Parse(info["Buff3"].ToString());
					}
					if (info["Buff4"].ToString() != "")
					{
						pet.m_iBuff4 = int.Parse(info["Buff4"].ToString());
					}
					if (info["AIID"].ToString() != "")
					{
						pet.m_iAIID = int.Parse(info["AIID"].ToString());
					}
					if (info["Icon"].ToString() != "")
					{
						pet.m_strIcon = info["Icon"].ToString();
					}
					if (info["FollowDistance"].ToString() != "")
					{
						pet.m_FollowDistance = int.Parse(info["FollowDistance"].ToString());
					}
					if (info["BattleFollowDistance"].ToString() != "")
					{
						pet.m_BattleFollowDistance = int.Parse(info["BattleFollowDistance"].ToString());
					}
					if (info["Speed"].ToString() != "")
					{
						pet.m_Speed = int.Parse(info["Speed"].ToString());
					}
					if (info["BeatPriority"].ToString() != "")
					{
						pet.m_BeatPriority = int.Parse(info["BeatPriority"].ToString());
					}
					if (info["DisappearTime"].ToString() != "")
					{
						pet.m_DisappearTime = int.Parse(info["DisappearTime"].ToString());
					}
					if (info["HPBarNum"].ToString() != "")
					{
						pet.m_HPBarNum = int.Parse(info["HPBarNum"].ToString());
					}
					if (info["HPBarHeight"].ToString() != "")
					{
						pet.m_HPBarHeight = int.Parse(info["HPBarHeight"].ToString());
					}
					if (info["FriAIID"].ToString() != "")
					{
						pet.m_FriAIID = int.Parse(info["FriAIID"].ToString());
					}
					if (info["SellMoney"].ToString() != "")
					{
						pet.m_SellMoney = int.Parse(info["SellMoney"].ToString());
					}
					if (info["CanLevelUp"].ToString() != "")
					{
						pet.m_CanLevelUp = int.Parse(info["CanLevelUp"].ToString());
					}
					if (info["CanMerge"].ToString() != "")
					{
						pet.m_CanMerge = int.Parse(info["CanMerge"].ToString());
					}
				}
				//宠物装备..
				pet.m_EquipedDB.Clear();
				int count = refMSG.m_PetsInfo[i].m_Equip.m_ItemCount;
				for(int j = 0; j < count; ++j)
				{
					sdGamePetItem item = new sdGamePetItem();
					item.templateID = refMSG.m_PetsInfo[i].m_Equip.m_Items[j].m_TID;
					item.instanceID = refMSG.m_PetsInfo[i].m_Equip.m_Items[j].m_UID;
					item.count = refMSG.m_PetsInfo[i].m_Equip.m_Items[j].m_CT;
					Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
					if (itemInfo != null)
					{
						item.mdlPath = itemInfo["Filename"].ToString();
						item.mdlPartName = itemInfo["FilePart"].ToString();
						item.anchorNodeName	= sdGameActor.WeaponDummy(itemInfo["Character"].ToString());
						item.itemClass = int.Parse(itemInfo["Class"].ToString());
						item.subClass = int.Parse(itemInfo["SubClass"].ToString());
						item.iCharacter = int.Parse(itemInfo["Character"].ToString());
						item.level = int.Parse(itemInfo["Level"].ToString());
						item.quility = int.Parse(itemInfo["Quility"].ToString());
					}
					
					pet.m_EquipedDB[item.instanceID] = item;
				}
				
				m_petListInfo[uuDBID] = pet;
				
				// 哈希表形式的属性aa
				CliProto.SPetInfo kPetInfo = refMSG.m_PetsInfo[i];
				
				Hashtable kPetPropertyTable = sdConfDataMgr.Instance().GetTable("PetProperty");
				Hashtable kPetTable = kPetPropertyTable[(int)kPetInfo.m_TemplateID] as Hashtable;
				Hashtable kLocalPetTable = sdConfDataMgr.CloneHashTable(kPetTable);
				if (kLocalPetTable != null)
				{
					UpdatePetPropertyFromPetInfo(kLocalPetTable, m_petListInfo[uuDBID] as SClientPetInfo);

					mPetList[uuDBID] = kLocalPetTable;
				}
			}
		}
	}

    public void UpdatePetInfo(CliProto.SC_PET_SINGLE_ENTER_NTF msg)
	{
        CliProto.SC_PET_SINGLE_ENTER_NTF refMSG = msg;
		UInt64 uuDBID = refMSG.m_PetInfo.m_DBID;
		
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_petListInfo[uuDBID] != null)
			{
				((SClientPetInfo)m_petListInfo[uuDBID]).m_uuDBID = refMSG.m_PetInfo.m_DBID;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iBattlePos = refMSG.m_PetInfo.m_BattlePos;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_uiTemplateID = refMSG.m_PetInfo.m_TemplateID;	
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iLevel = refMSG.m_PetInfo.m_Level;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_uuExperience = (UInt64)refMSG.m_PetInfo.m_Experience;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iUp = refMSG.m_PetInfo.m_Up;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_Lock = refMSG.m_PetInfo.m_Lock;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iHP = refMSG.m_PetInfo.m_HP;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iSP = refMSG.m_PetInfo.m_SP;
				
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iStr = refMSG.m_PetInfo.m_Str;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iInt = refMSG.m_PetInfo.m_Int;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iDex = refMSG.m_PetInfo.m_Dex;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iSta = refMSG.m_PetInfo.m_Sta;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iFai = refMSG.m_PetInfo.m_Fai;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iMaxHP = refMSG.m_PetInfo.m_MaxHP;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iMaxSP = refMSG.m_PetInfo.m_MaxSP;
				((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iHPTick = refMSG.m_PetInfo.m_HPTick;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iSPTick = refMSG.m_PetInfo.m_SPTick;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iAtkDmgMin = refMSG.m_PetInfo.m_AtkDmgMin;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iAtkDmgMax = refMSG.m_PetInfo.m_AtkDmgMax;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iDef = refMSG.m_PetInfo.m_Def;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iIceAtt = refMSG.m_PetInfo.m_IceAtt;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iFireAtt = refMSG.m_PetInfo.m_FireAtt;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iPoisonAtt = refMSG.m_PetInfo.m_PoisonAtt;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iThunderAtt = refMSG.m_PetInfo.m_ThunderAtt;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iIceDef = refMSG.m_PetInfo.m_IceDef;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iFireDef = refMSG.m_PetInfo.m_FireDef;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iPoisonDef = refMSG.m_PetInfo.m_PoisonDef;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iThunderDef = refMSG.m_PetInfo.m_ThunderDef;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iPierce = refMSG.m_PetInfo.m_Pierce;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iHit = refMSG.m_PetInfo.m_Hit;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iDodge = refMSG.m_PetInfo.m_Dodge;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iCri = refMSG.m_PetInfo.m_Cri;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iFlex = refMSG.m_PetInfo.m_Flex;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iCriDmg = refMSG.m_PetInfo.m_CriDmg;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iCriDmgDef = refMSG.m_PetInfo.m_CriDmgDef;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iBodySize = refMSG.m_PetInfo.m_BodySize;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iAttSize = refMSG.m_PetInfo.m_AttSize;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iAttSpeedModPer = refMSG.m_PetInfo.m_AttSpeedModPer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iMoveSpeedModPer = refMSG.m_PetInfo.m_MoveSpeedModPer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iPiercePer = refMSG.m_PetInfo.m_PiercePer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iHitPer = refMSG.m_PetInfo.m_HitPer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iDodgePer = refMSG.m_PetInfo.m_DodgePer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iCriPer = refMSG.m_PetInfo.m_CriPer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iFlexPer = refMSG.m_PetInfo.m_FlexPer;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iAttSpeed = refMSG.m_PetInfo.m_AttSpeed;
			    ((SClientPetInfo)m_petListInfo[uuDBID]).m_CurProperty.m_iMoveSpeed = refMSG.m_PetInfo.m_MoveSpeed;

				//宠物装备
				((SClientPetInfo)m_petListInfo[uuDBID]).m_EquipedDB.Clear();
				int count = refMSG.m_PetInfo.m_Equip.m_ItemCount;
				for(int j = 0; j < count; ++j)
				{
					sdGamePetItem item = new sdGamePetItem();
					item.templateID = refMSG.m_PetInfo.m_Equip.m_Items[j].m_TID;
					item.instanceID = refMSG.m_PetInfo.m_Equip.m_Items[j].m_UID;
					item.count = refMSG.m_PetInfo.m_Equip.m_Items[j].m_CT;
					Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
					if (itemInfo != null)
					{
						item.mdlPath = itemInfo["Filename"].ToString();
						item.mdlPartName = itemInfo["FilePart"].ToString();
						item.anchorNodeName	= sdGameActor.WeaponDummy(itemInfo["Character"].ToString());
						item.itemClass = int.Parse(itemInfo["Class"].ToString());
						item.subClass = int.Parse(itemInfo["SubClass"].ToString());
						item.iCharacter = int.Parse(itemInfo["Character"].ToString());
						item.level = int.Parse(itemInfo["Level"].ToString());
						item.quility = int.Parse(itemInfo["Quility"].ToString());
					}
					
					((SClientPetInfo)m_petListInfo[uuDBID]).m_EquipedDB[item.instanceID] = item;
				}
			}
			else
			{
				SClientPetInfo pet = new SClientPetInfo();
				
				pet.m_uuDBID = refMSG.m_PetInfo.m_DBID;
				pet.m_iBattlePos = refMSG.m_PetInfo.m_BattlePos;
				pet.m_uiTemplateID = refMSG.m_PetInfo.m_TemplateID;	
				pet.m_iLevel = refMSG.m_PetInfo.m_Level;
				pet.m_uuExperience = (UInt64)refMSG.m_PetInfo.m_Experience;
				pet.m_iUp = refMSG.m_PetInfo.m_Up;
				pet.m_Lock = refMSG.m_PetInfo.m_Lock;
				pet.m_iHP = refMSG.m_PetInfo.m_HP;
				pet.m_iSP = refMSG.m_PetInfo.m_SP;
				
				pet.m_CurProperty.m_iStr = refMSG.m_PetInfo.m_Str;
				pet.m_CurProperty.m_iInt = refMSG.m_PetInfo.m_Int;
				pet.m_CurProperty.m_iDex = refMSG.m_PetInfo.m_Dex;
				pet.m_CurProperty.m_iSta = refMSG.m_PetInfo.m_Sta;
				pet.m_CurProperty.m_iFai = refMSG.m_PetInfo.m_Fai;
				pet.m_CurProperty.m_iMaxHP = refMSG.m_PetInfo.m_MaxHP;
				pet.m_CurProperty.m_iMaxSP = refMSG.m_PetInfo.m_MaxSP;
				pet.m_CurProperty.m_iHPTick = refMSG.m_PetInfo.m_HPTick;
			    pet.m_CurProperty.m_iSPTick = refMSG.m_PetInfo.m_SPTick;
			    pet.m_CurProperty.m_iAtkDmgMin = refMSG.m_PetInfo.m_AtkDmgMin;
			    pet.m_CurProperty.m_iAtkDmgMax = refMSG.m_PetInfo.m_AtkDmgMax;
			    pet.m_CurProperty.m_iDef = refMSG.m_PetInfo.m_Def;
			    pet.m_CurProperty.m_iIceAtt = refMSG.m_PetInfo.m_IceAtt;
			    pet.m_CurProperty.m_iFireAtt = refMSG.m_PetInfo.m_FireAtt;
			    pet.m_CurProperty.m_iPoisonAtt = refMSG.m_PetInfo.m_PoisonAtt;
			    pet.m_CurProperty.m_iThunderAtt = refMSG.m_PetInfo.m_ThunderAtt;
			    pet.m_CurProperty.m_iIceDef = refMSG.m_PetInfo.m_IceDef;
			    pet.m_CurProperty.m_iFireDef = refMSG.m_PetInfo.m_FireDef;
			    pet.m_CurProperty.m_iPoisonDef = refMSG.m_PetInfo.m_PoisonDef;
			    pet.m_CurProperty.m_iThunderDef = refMSG.m_PetInfo.m_ThunderDef;
			    pet.m_CurProperty.m_iPierce = refMSG.m_PetInfo.m_Pierce;
			    pet.m_CurProperty.m_iHit = refMSG.m_PetInfo.m_Hit;
			    pet.m_CurProperty.m_iDodge = refMSG.m_PetInfo.m_Dodge;
			    pet.m_CurProperty.m_iCri = refMSG.m_PetInfo.m_Cri;
			    pet.m_CurProperty.m_iFlex = refMSG.m_PetInfo.m_Flex;
			    pet.m_CurProperty.m_iCriDmg = refMSG.m_PetInfo.m_CriDmg;
			    pet.m_CurProperty.m_iCriDmgDef = refMSG.m_PetInfo.m_CriDmgDef;
			    pet.m_CurProperty.m_iBodySize = refMSG.m_PetInfo.m_BodySize;
			    pet.m_CurProperty.m_iAttSize = refMSG.m_PetInfo.m_AttSize;
			    pet.m_CurProperty.m_iAttSpeedModPer = refMSG.m_PetInfo.m_AttSpeedModPer;
			    pet.m_CurProperty.m_iMoveSpeedModPer = refMSG.m_PetInfo.m_MoveSpeedModPer;
			    pet.m_CurProperty.m_iPiercePer = refMSG.m_PetInfo.m_PiercePer;
			    pet.m_CurProperty.m_iHitPer = refMSG.m_PetInfo.m_HitPer;
			    pet.m_CurProperty.m_iDodgePer = refMSG.m_PetInfo.m_DodgePer;
			    pet.m_CurProperty.m_iCriPer = refMSG.m_PetInfo.m_CriPer;
			    pet.m_CurProperty.m_iFlexPer = refMSG.m_PetInfo.m_FlexPer;
			    pet.m_CurProperty.m_iAttSpeed = refMSG.m_PetInfo.m_AttSpeed;
			    pet.m_CurProperty.m_iMoveSpeed = refMSG.m_PetInfo.m_MoveSpeed;
				
				//宠物模板数据信息aa
				uint uiTemplateID = refMSG.m_PetInfo.m_TemplateID;
				Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(uiTemplateID.ToString());
				if (info != null)
				{
					if (info["Name"].ToString() != "")
					{
						pet.m_strName = info["Name"].ToString();
					}
					
					if (info["Ability"].ToString() != "")
					{
						pet.m_iAbility = int.Parse(info["Ability"].ToString());
					}
					
					if (info["BodyType"].ToString() != "")
					{
						pet.m_iBodyType = int.Parse(info["BodyType"].ToString());
					}
					
					if (info["KnockDownDef"].ToString() != "")
					{
						pet.m_iKnockDownDef = int.Parse(info["KnockDownDef"].ToString());
					}
					
					if (info["KnockFlyDef"].ToString() != "")
					{
						pet.m_iKnockFlyDef = int.Parse(info["KnockFlyDef"].ToString());
					}
					
					if (info["KnockBackDef"].ToString() != "")
					{
						pet.m_iKnockBackDef = int.Parse(info["KnockBackDef"].ToString());
					}
					
					if (info["EyeSize"].ToString() != "")
					{
						pet.m_iEyeSize = int.Parse(info["EyeSize"].ToString());
					}
					
					if (info["ChaseSize"].ToString() != "")
					{
						pet.m_iChaseSize = int.Parse(info["ChaseSize"].ToString());
					}

					if (info["DfSkill"].ToString() != "")
					{
						pet.m_iDfSkill = int.Parse(info["DfSkill"].ToString());
					}
					
					if (info["SpSkill"].ToString() != "")
					{
						pet.m_iSpSkill = int.Parse(info["SpSkill"].ToString());
					}
					
					if (info["SapSkill"].ToString() != "")
					{
						pet.m_iSapSkill = int.Parse(info["SapSkill"].ToString());
					}
					
					//模板属性aa
					if (info["Property.Str"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iStr = int.Parse(info["Property.Str"].ToString());
					}
					if (info["Property.Int"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iInt = int.Parse(info["Property.Int"].ToString());
					}
					if (info["Property.Dex"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDex = int.Parse(info["Property.Dex"].ToString());
					}
					if (info["Property.Sta"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iSta = int.Parse(info["Property.Sta"].ToString());
					}
					if (info["Property.Fai"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFai = int.Parse(info["Property.Fai"].ToString());
					}
					if (info["Property.MaxHP"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMaxHP = int.Parse(info["Property.MaxHP"].ToString());
					}
					if (info["Property.MaxSP"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMaxSP = int.Parse(info["Property.MaxSP"].ToString());
					}
					if (info["Property.HPTick"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHPTick = int.Parse(info["Property.HPTick"].ToString());
					}
					if (info["Property.SPTick"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iSPTick = int.Parse(info["Property.SPTick"].ToString());
					}
					if (info["Property.AtkDmgMin"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAtkDmgMin = int.Parse(info["Property.AtkDmgMin"].ToString());
					}
					if (info["Property.AtkDmgMax"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAtkDmgMax = int.Parse(info["Property.AtkDmgMax"].ToString());
					}
					if (info["Property.Def"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDef = int.Parse(info["Property.Def"].ToString());
					}
					if (info["Property.IceAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iIceAtt = int.Parse(info["Property.IceAtt"].ToString());
					}
					if (info["Property.FireAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFireAtt = int.Parse(info["Property.FireAtt"].ToString());
					}
					if (info["Property.PoisonAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPoisonAtt = int.Parse(info["Property.PoisonAtt"].ToString());
					}
					if (info["Property.ThunderAtt"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iThunderAtt = int.Parse(info["Property.ThunderAtt"].ToString());
					}
					if (info["Property.IceDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iIceDef = int.Parse(info["Property.IceDef"].ToString());
					}
					if (info["Property.FireDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFireDef = int.Parse(info["Property.FireDef"].ToString());
					}
					if (info["Property.PoisonDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPoisonDef = int.Parse(info["Property.PoisonDef"].ToString());
					}
					if (info["Property.ThunderDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iThunderDef = int.Parse(info["Property.ThunderDef"].ToString());
					}
					if (info["Property.Pierce"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPierce = int.Parse(info["Property.Pierce"].ToString());
					}
				    if (info["Property.Hit"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHit = int.Parse(info["Property.Hit"].ToString());
					}
					if (info["Property.Dodge"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDodge = int.Parse(info["Property.Dodge"].ToString());
					}
				    if (info["Property.Cri"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCri = int.Parse(info["Property.Cri"].ToString());
					}
					if (info["Property.Flex"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFlex = int.Parse(info["Property.Flex"].ToString());
					}
					if (info["Property.CriDmg"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriDmg = int.Parse(info["Property.CriDmg"].ToString());
					}
				    if (info["Property.CriDmgDef"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriDmgDef = int.Parse(info["Property.CriDmgDef"].ToString());
					}
					if (info["Property.BodySize"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iBodySize = int.Parse(info["Property.BodySize"].ToString());
					}
				    if (info["Property.AttSize"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSize = int.Parse(info["Property.AttSize"].ToString());
					}
					if (info["Property.AttSpeedModPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSpeedModPer = int.Parse(info["Property.AttSpeedModPer"].ToString());
					}
					if (info["Property.MoveSpeedModPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMoveSpeedModPer = int.Parse(info["Property.MoveSpeedModPer"].ToString());
					}
					if (info["Property.PiercePer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iPiercePer = int.Parse(info["Property.PiercePer"].ToString());
					}
					if (info["Property.HitPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iHitPer = int.Parse(info["Property.HitPer"].ToString());
					}
					if (info["Property.DodgePer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iDodgePer = int.Parse(info["Property.DodgePer"].ToString());
					}
					if (info["Property.CriPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iCriPer = int.Parse(info["Property.CriPer"].ToString());
					}
					if (info["Property.FlexPer"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iFlexPer = int.Parse(info["Property.FlexPer"].ToString());
					}
				    if (info["Property.AttSpeed"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iAttSpeed = int.Parse(info["Property.AttSpeed"].ToString());
					}
					if (info["Property.MoveSpeed"].ToString() != "")
					{
						pet.m_TemplateProperty.m_iMoveSpeed = int.Parse(info["Property.MoveSpeed"].ToString());
					}
				    //属性计算参数aa
				    if (info["Coe.AttA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttA = int.Parse(info["Coe.AttA"].ToString());
					}
					if (info["Coe.AttB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttB = int.Parse(info["Coe.AttB"].ToString());
					}
					if (info["Coe.AttC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iAttC = int.Parse(info["Coe.AttC"].ToString());
					}
					if (info["Coe.DefA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefA = int.Parse(info["Coe.DefA"].ToString());
					}
					if (info["Coe.DefB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefB = int.Parse(info["Coe.DefB"].ToString());
					}
					if (info["Coe.DefC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iDefC = int.Parse(info["Coe.DefC"].ToString());
					}
					if (info["Coe.HPA"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPA = int.Parse(info["Coe.HPA"].ToString());
					}
					if (info["Coe.HPB"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPB = int.Parse(info["Coe.HPB"].ToString());
					}
					if (info["Coe.HPC"].ToString() != "")
					{
						pet.m_TemplateCoe.m_iHPC = int.Parse(info["Coe.HPC"].ToString());
					}
					//其他信息aa
					if (info["BaseJob"].ToString() != "")
					{
						pet.m_iBaseJob = int.Parse(info["BaseJob"].ToString());
					}
					if (info["Desc"].ToString() != "")
					{
						pet.m_strDesc = info["Desc"].ToString();
					}
					if (info["SPD1"].ToString() != "")
					{
						pet.m_strSPD1 = info["SPD1"].ToString();
					}
					if (info["SPD2"].ToString() != "")
					{
						pet.m_strSPD2 = info["SPD2"].ToString();
					}
					if (info["Res"].ToString() != "")
					{
						pet.m_strRes = info["Res"].ToString();
					}
					if (info["Skill1"].ToString() != "")
					{
						pet.m_iSkill1 = int.Parse(info["Skill1"].ToString());
					}
				    if (info["Skill2"].ToString() != "")
					{
						pet.m_iSkill2 = int.Parse(info["Skill2"].ToString());
					}
					if (info["Skill3"].ToString() != "")
					{
						pet.m_iSkill3 = int.Parse(info["Skill3"].ToString());
					}
					if (info["Skill4"].ToString() != "")
					{
						pet.m_iSkill4 = int.Parse(info["Skill4"].ToString());
					}
					if (info["Buff1"].ToString() != "")
					{
						pet.m_iBuff1 = int.Parse(info["Buff1"].ToString());
					}
				    if (info["Buff2"].ToString() != "")
					{
						pet.m_iBuff2 = int.Parse(info["Buff2"].ToString());
					}
					if (info["Buff3"].ToString() != "")
					{
						pet.m_iBuff3 = int.Parse(info["Buff3"].ToString());
					}
					if (info["Buff4"].ToString() != "")
					{
						pet.m_iBuff4 = int.Parse(info["Buff4"].ToString());
					}
					if (info["AIID"].ToString() != "")
					{
						pet.m_iAIID = int.Parse(info["AIID"].ToString());
					}
					if (info["Icon"].ToString() != "")
					{
						pet.m_strIcon = info["Icon"].ToString();
					}
					if (info["FollowDistance"].ToString() != "")
					{
						pet.m_FollowDistance = int.Parse(info["FollowDistance"].ToString());
					}
					if (info["BattleFollowDistance"].ToString() != "")
					{
						pet.m_BattleFollowDistance = int.Parse(info["BattleFollowDistance"].ToString());
					}
					if (info["Speed"].ToString() != "")
					{
						pet.m_Speed = int.Parse(info["Speed"].ToString());
					}
					if (info["BeatPriority"].ToString() != "")
					{
						pet.m_BeatPriority = int.Parse(info["BeatPriority"].ToString());
					}
					if (info["DisappearTime"].ToString() != "")
					{
						pet.m_DisappearTime = int.Parse(info["DisappearTime"].ToString());
					}
					if (info["HPBarNum"].ToString() != "")
					{
						pet.m_HPBarNum = int.Parse(info["HPBarNum"].ToString());
					}
					if (info["HPBarHeight"].ToString() != "")
					{
						pet.m_HPBarHeight = int.Parse(info["HPBarHeight"].ToString());
					}
					if (info["FriAIID"].ToString() != "")
					{
						pet.m_FriAIID = int.Parse(info["FriAIID"].ToString());
					}
					if (info["SellMoney"].ToString() != "")
					{
						pet.m_SellMoney = int.Parse(info["SellMoney"].ToString());
					}
					if (info["CanLevelUp"].ToString() != "")
					{
						pet.m_CanLevelUp = int.Parse(info["CanLevelUp"].ToString());
					}
					if (info["CanMerge"].ToString() != "")
					{
						pet.m_CanMerge = int.Parse(info["CanMerge"].ToString());
					}
				}
				//宠物装备..
				pet.m_EquipedDB.Clear();
				int count = refMSG.m_PetInfo.m_Equip.m_ItemCount;
				for(int j = 0; j < count; ++j)
				{
					sdGamePetItem item = new sdGamePetItem();
					item.templateID = refMSG.m_PetInfo.m_Equip.m_Items[j].m_TID;
					item.instanceID = refMSG.m_PetInfo.m_Equip.m_Items[j].m_UID;
					item.count = refMSG.m_PetInfo.m_Equip.m_Items[j].m_CT;
					Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
					if (itemInfo != null)
					{
						item.mdlPath = itemInfo["Filename"].ToString();
						item.mdlPartName = itemInfo["FilePart"].ToString();
						item.anchorNodeName	= sdGameActor.WeaponDummy(itemInfo["Character"].ToString());
						item.itemClass = int.Parse(itemInfo["Class"].ToString());
						item.subClass = int.Parse(itemInfo["SubClass"].ToString());
						item.iCharacter = int.Parse(itemInfo["Character"].ToString());
						item.level = int.Parse(itemInfo["Level"].ToString());
						item.quility = int.Parse(itemInfo["Quility"].ToString());
					}
					
					pet.m_EquipedDB[item.instanceID] = item;
				}
				
				m_petListInfo[uuDBID] = pet;
			}
			
			// 哈希表形式的属性aa
			{
				CliProto.SPetInfo kPetInfo = refMSG.m_PetInfo;
				
				Hashtable kLocalPetTable = mPetList[uuDBID] as Hashtable;
				if (kLocalPetTable == null)
				{
					Hashtable kPetPropertyTable = sdConfDataMgr.Instance().GetTable("PetProperty");
					Hashtable kPetTable = kPetPropertyTable[(int)kPetInfo.m_TemplateID] as Hashtable;
					kLocalPetTable = sdConfDataMgr.CloneHashTable(kPetTable);
					
					mPetList[uuDBID] = kLocalPetTable;
				}
				
				if (kLocalPetTable != null)
					UpdatePetPropertyFromPetInfo(kLocalPetTable, m_petListInfo[uuDBID] as SClientPetInfo);
			}
		}
	}

    public void DeletePetInfo(CliProto.SC_PET_SINGLE_LEAVE_NTF refMSG)
    {
        UInt64 uuDBID = refMSG.m_DBID;
        m_petListInfo.Remove(uuDBID);
    }

	public void UpdatePetLevel(UInt64 uuDBID, int iLevel)
	{
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_petListInfo[uuDBID] != null)
			{
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iLevel = iLevel;
			}
			
			Hashtable kLocalPetTable = mPetList[uuDBID] as Hashtable;
			if (kLocalPetTable != null)
			{
				kLocalPetTable["Level"]	= (int)iLevel;
			}
		}
	}
	
	public void UpdatePetUp(UInt64 uuDBID, int iUp)
	{
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_petListInfo[uuDBID] != null)
			{
				((SClientPetInfo)m_petListInfo[uuDBID]).m_iUp = iUp;
			}
			
			Hashtable kLocalPetTable = mPetList[uuDBID] as Hashtable;
			if (kLocalPetTable != null)
			{
				kLocalPetTable["Up"] = (int)iUp;
			}
		}
	}
	
	// 更新宠物属性表aa
	public static void UpdatePetPropertyFromPetInfo(Hashtable kPetProperty, SClientPetInfo kPetInfo)
	{
		if (kPetProperty == null || kPetInfo == null)
			return;

		kPetProperty["DBID"] = kPetInfo.m_uuDBID;
		kPetProperty["BattlePos"] = kPetInfo.m_iBattlePos.ToString();
		kPetProperty["TemplateID"] = (int)kPetInfo.m_uiTemplateID;
		kPetProperty["Level"] = (int)kPetInfo.m_iLevel;
		kPetProperty["Experience"] = kPetInfo.m_uuExperience.ToString();
		kPetProperty["HP"] = (int)kPetInfo.m_iHP;
		kPetProperty["SP"] = (int)kPetInfo.m_iSP;

		kPetProperty["Str"] = (int)kPetInfo.m_CurProperty.m_iStr;
		kPetProperty["Int"]	= (int)kPetInfo.m_CurProperty.m_iInt;
		kPetProperty["Dex"]	= (int)kPetInfo.m_CurProperty.m_iDex;
		kPetProperty["Sta"]	= (int)kPetInfo.m_CurProperty.m_iSta;
		kPetProperty["Fai"] = (int)kPetInfo.m_CurProperty.m_iFai;
		kPetProperty["MaxHP"] = (int)kPetInfo.m_CurProperty.m_iMaxHP;
		kPetProperty["MaxSP"] = (int)kPetInfo.m_CurProperty.m_iMaxSP;
		kPetProperty["HPTick"] = (int)kPetInfo.m_CurProperty.m_iHPTick;
		kPetProperty["SPTick"] = (int)kPetInfo.m_CurProperty.m_iSPTick;
		kPetProperty["AtkDmgMin"] = (int)kPetInfo.m_CurProperty.m_iAtkDmgMin;
		kPetProperty["AtkDmgMax"] = (int)kPetInfo.m_CurProperty.m_iAtkDmgMax;
		kPetProperty["Def"] = (int)kPetInfo.m_CurProperty.m_iDef;
		kPetProperty["IceAtt"] = (int)kPetInfo.m_CurProperty.m_iIceAtt;
		kPetProperty["FireAtt"] = (int)kPetInfo.m_CurProperty.m_iFireAtt;
		kPetProperty["PoisonAtt"] = (int)kPetInfo.m_CurProperty.m_iPoisonAtt;
		kPetProperty["ThunderAtt"] = (int)kPetInfo.m_CurProperty.m_iThunderAtt;
		kPetProperty["IceDef"] = (int)kPetInfo.m_CurProperty.m_iIceDef;
		kPetProperty["FireDef"] = (int)kPetInfo.m_CurProperty.m_iFireDef;
		kPetProperty["PoisonDef"] = (int)kPetInfo.m_CurProperty.m_iPoisonDef;
		kPetProperty["ThunderDef"] = (int)kPetInfo.m_CurProperty.m_iThunderDef;
		kPetProperty["Pierce"] = (int)kPetInfo.m_CurProperty.m_iPierce;
		kPetProperty["Hit"] = (int)kPetInfo.m_CurProperty.m_iHit;
		kPetProperty["Dodge"] = (int)kPetInfo.m_CurProperty.m_iDodge;
		kPetProperty["Cri"] = (int)kPetInfo.m_CurProperty.m_iCri;
		kPetProperty["Flex"] = (int)kPetInfo.m_CurProperty.m_iFlex;
		kPetProperty["CriDmg"] = (int)kPetInfo.m_CurProperty.m_iCriDmg;
		kPetProperty["CriDmgDef"] = (int)kPetInfo.m_CurProperty.m_iCriDmgDef;
		kPetProperty["BodySize"] = (int)kPetInfo.m_CurProperty.m_iBodySize;
		kPetProperty["AttSize"] = (int)kPetInfo.m_CurProperty.m_iAttSize;
		kPetProperty["AttSpeedModPer"] = (int)kPetInfo.m_CurProperty.m_iAttSpeedModPer;
		kPetProperty["MoveSpeedModPer"] = (int)kPetInfo.m_CurProperty.m_iMoveSpeedModPer;
		kPetProperty["PiercePer"] = (int)kPetInfo.m_CurProperty.m_iPiercePer;
		kPetProperty["HitPer"] = (int)kPetInfo.m_CurProperty.m_iHitPer;
		kPetProperty["DodgePer"] = (int)kPetInfo.m_CurProperty.m_iDodgePer;
		kPetProperty["CriPer"] = (int)kPetInfo.m_CurProperty.m_iCriPer;
		kPetProperty["FlexPer"] = (int)kPetInfo.m_CurProperty.m_iFlexPer;
		kPetProperty["AttSpeed"] = (int)kPetInfo.m_CurProperty.m_iAttSpeed;
		kPetProperty["MoveSpeed"] = (int)kPetInfo.m_CurProperty.m_iMoveSpeed;
	}

	// 进入场景回调(重置宠物CD和HP)aa
	public void OnEnterLevel()
	{
		// 出战宠物aa
		for (int i = 0; i < mBattlePetNum; i++)
		{
			UInt64 uuDBID = mPetTeam[i];
			if (uuDBID == UInt64.MaxValue) 
				continue;
			
			Hashtable kProperty = GetPetPropertyFromDBID(uuDBID);
			if (kProperty == null)
				continue;
			
			kProperty["Active"] = false;
			kProperty["CooldownStart"] = (long)0;
			kProperty["CooldownCount"] = (long)0;
			
			kProperty["HP"] = kProperty["MaxHP"];
		}
		
		//
		mActivePetIndex = -1;

		// 好友出战宠物aa
		mFriendPetProperty = null;

		sdFriend kFightFriend = sdFriendMgr.Instance.fightFriend;
		if (kFightFriend != null)
		{
			if (kFightFriend.petInfo != null)
			{
				Hashtable kPetPropertyTable = sdConfDataMgr.Instance().GetTable("PetProperty");
				Hashtable kPetTable = kPetPropertyTable[(int)kFightFriend.petInfo.m_uiTemplateID] as Hashtable;
				Hashtable kLocalPetTable = sdConfDataMgr.CloneHashTable(kPetTable);

				if (kLocalPetTable != null)
				{
					UpdatePetPropertyFromPetInfo(kLocalPetTable, kFightFriend.petInfo);
					mFriendPetProperty = kLocalPetTable;
					
					// 纠正HP和SPaa
					kLocalPetTable["HP"] = kLocalPetTable["MaxHP"];
					kLocalPetTable["SP"] = kLocalPetTable["MaxSP"];
					
					// 注意这里把AIID替换掉了aa
					mFriendPetProperty["AIID"] = mFriendPetProperty["FriAIID"];	
				}
			}
		}
	}
	
	// 获取指定DBID宠物的属性表aa
	public Hashtable GetPetPropertyFromDBID(UInt64 ulDBID)
	{
		Hashtable kPetPropTable = mPetList[ulDBID] as Hashtable;	//< 尝试从宠物列表获取数据aa
		if (kPetPropTable != null)
			return kPetPropTable;

		if (mFriendPetProperty != null && (ulong)mFriendPetProperty["DBID"] == ulDBID)
		{
			kPetPropTable = mFriendPetProperty;						//< 尝试从好友宠物属性表获取数据aa
			return kPetPropTable;
		}


		kPetPropTable = sdPVPManager.Instance.GetPetPropertyFromDBID(ulDBID);
		if (kPetPropTable != null)
			return kPetPropTable;									//< 尝试从PVP获取好友宠物属性aa

		return null;
	}
	
	// 获取宠物战队指定位置宠物的DBIDaa
	public UInt64 GetPetFromTeamByIndex(int iIndex)
	{
		if (iIndex < 0 || iIndex > mPetTeamSize) 
			return UInt64.MaxValue;
		
		return mPetTeam[iIndex];
	}
	
	// 设置宠物战队指定位置宠物的DBIDaa
	public void SetPetBattlePos(UInt64 uuDBID, int iPos)
	{
		//取消出战aa
		if (uuDBID==UInt64.MaxValue)
		{
			if (iPos<=6&&iPos>=0)
				mPetTeam[iPos] = UInt64.MaxValue;
		}
		//设置出战aa
		else
		{
			if (iPos<=6&&iPos>=0)
				mPetTeam[iPos] = uuDBID;
		}
	}
	
	// @description 反激活当前宠物aa
	public void DeactivePet()
	{
		if (mActivePetIndex < 0)
			return;
		
		//
		UInt64 uuOldPetDBID = mPetTeam[mActivePetIndex];
		Hashtable kOldPetProperty = GetPetPropertyFromDBID(uuOldPetDBID);
		if (kOldPetProperty != null)
		{
			kOldPetProperty["Active"] = false;
//			kOldPetProperty["CooldownStart"] = (long)0;
//			kOldPetProperty["CooldownCount"] = (long)0;
		}
		
		//
		mActivePetIndex = -1;
		
		// 通知推图逻辑aa
		if (sdGameLevel.instance.tuiTuLogic != null)
		{
			sdGameLevel.instance.tuiTuLogic.DeactivePet();
		}	
	}

	// @description 激活一个宠物，CD的单位为秒，在此CD内此宠物无法再次被激活aa
	// @param[in]	nPetPos		宠物队伍中的位置aa
	// @param[in]	nCooldown	冷却时间aa
	// return					是否激活成功aa
	public bool ActivePetByIndex(int iPetIndex, int nCooldown)
	{
		if (iPetIndex < 0 || iPetIndex > mBattlePetNum) 
			return false;
		
		UInt64 uuPetDBID = mPetTeam[iPetIndex];
		if (uuPetDBID == UInt64.MaxValue) 
			return false;
		
		Hashtable kPetProperty = GetPetPropertyFromDBID(uuPetDBID);
		if (kPetProperty == null)
			return false;

		if ((bool)kPetProperty["Active"] == true) 
			return false;
		
		// 判断要激活的宠物血量是否达到30%aa
		int iMaxHP = (int)kPetProperty["MaxHP"];
		int iCurHP = (int)kPetProperty["HP"];
		if (iCurHP / (float)iMaxHP < 0.3f)
			return false;

		// 判断要激活的宠物是否CD结束aa
		if (GetPetActiveCD(iPetIndex) > 0) 
			return false;	
	
		// 之前激活的宠物改为非激活装.
		if (mActivePetIndex >= 0)
			DeactivePet();
	
		// 激活要激活的宠物.
		kPetProperty["Active"] = true;
		kPetProperty["CooldownStart"] = (long)(System.DateTime.Now.Ticks / 100000L);
		kPetProperty["CooldownCount"] = (long)(nCooldown * 100);
		
		mActivePetIndex = iPetIndex;
	
		// 通知推图逻辑aa
		if (sdGameLevel.instance.tuiTuLogic != null)
		{
			sdGameLevel.instance.tuiTuLogic.CreatePet();
		}
		
		return true;
	}

	// 激活好友宠物aa
	public bool ActiveFriendPet()
	{
		if (mIsFriendPetActived)
			return false;

		//
		mIsFriendPetActived = true;

		// 通知推图逻辑aa
		if (sdGameLevel.instance.tuiTuLogic != null)
			sdGameLevel.instance.tuiTuLogic.CreateFriendPet();

		return true;
	}

	// 反激活好友宠物aa
	public void DeactiveFriendPet()
	{
		if (!mIsFriendPetActived)
			return;
		
		//
		mIsFriendPetActived = false;
		
		// 通知推图逻辑aa
		if (sdGameLevel.instance.tuiTuLogic != null)
			sdGameLevel.instance.tuiTuLogic.DeactiveFriendPet();
	}
	
	// 返回宠物战队的一个宠物的CDaa
	public float GetPetActiveCD(int iPetIndex)
	{
		if (iPetIndex < 0 || iPetIndex > mBattlePetNum) 
			return 0.0f;
		
		UInt64 uuPetDBID = mPetTeam[iPetIndex];
		if (uuPetDBID == UInt64.MaxValue) 
			return 0.0f;
		
		Hashtable kPetProperty = GetPetPropertyFromDBID(uuPetDBID);
		if (kPetProperty == null)
			return 0.0f;
		
		object kCooldownCount = kPetProperty["CooldownCount"];
		if (kCooldownCount == null)
			return 0.0f;
		
		long lCooldownCount = (long)kCooldownCount;
		if (lCooldownCount <= 0 ) 
			return 0.0f;
	
		object kCooldownStart = kPetProperty["CooldownStart"];
		if (kCooldownStart == null)
			return 0.0f;
		
		long lCooldownStart = (long)kCooldownStart;
		long lTickNow = System.DateTime.Now.Ticks / 100000L;
		float fCD = 1.0f - (float)(lTickNow - lCooldownStart) / lCooldownCount;
		if (fCD <= 0.0f) 
		{
			fCD = 0.0f;
			kPetProperty["CooldownCount"] = 0L;
		}
		
		return fCD;
	}

	public SClientPetInfo GetPetInfo(UInt64 uuDBID)
	{
		if (m_petListInfo[uuDBID] != null)
		{
			return (SClientPetInfo)m_petListInfo[uuDBID];	
		}
		else
		{
			return null;	
		}
	}

	public SClientPetInfo GetPetInfoByTemplateID(int iID, int iUp, int iLevel)
	{
		if (iID<=0)
			return null;

		SClientPetInfo pet = new SClientPetInfo();
		pet.m_uiTemplateID = (uint)iID;
		pet.m_iLevel = iLevel;
		pet.m_iUp = iUp;
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iID.ToString());
		if (info != null)
		{
			float fTemp = sdNewPetMgr.Instance.GetPetAttCoe((uint)iID, iUp, iLevel);
			int iTemp = (int)fTemp + int.Parse(info["Property.AtkDmgMax"].ToString());
			pet.m_CurProperty.m_iAtkDmgMax = iTemp;

			fTemp = sdNewPetMgr.Instance.GetPetDefCoe((uint)iID, iUp, iLevel);
			iTemp = (int)fTemp + int.Parse(info["Property.Def"].ToString());
			pet.m_CurProperty.m_iDef = iTemp;

			fTemp = sdNewPetMgr.Instance.GetPetHPCoe((uint)iID, iUp, iLevel);
			iTemp = (int)fTemp + int.Parse(info["Property.MaxHP"].ToString());
			pet.m_CurProperty.m_iMaxHP = iTemp;

			if (info["Name"].ToString() != "")
			{
				pet.m_strName = info["Name"].ToString();
			}
			
			if (info["Ability"].ToString() != "")
			{
				pet.m_iAbility = int.Parse(info["Ability"].ToString());
			}
			
			if (info["BodyType"].ToString() != "")
			{
				pet.m_iBodyType = int.Parse(info["BodyType"].ToString());
			}
			
			if (info["KnockDownDef"].ToString() != "")
			{
				pet.m_iKnockDownDef = int.Parse(info["KnockDownDef"].ToString());
			}
			
			if (info["KnockFlyDef"].ToString() != "")
			{
				pet.m_iKnockFlyDef = int.Parse(info["KnockFlyDef"].ToString());
			}
			
			if (info["KnockBackDef"].ToString() != "")
			{
				pet.m_iKnockBackDef = int.Parse(info["KnockBackDef"].ToString());
			}
			
			if (info["EyeSize"].ToString() != "")
			{
				pet.m_iEyeSize = int.Parse(info["EyeSize"].ToString());
			}
			
			if (info["ChaseSize"].ToString() != "")
			{
				pet.m_iChaseSize = int.Parse(info["ChaseSize"].ToString());
			}
			
			if (info["DfSkill"].ToString() != "")
			{
				pet.m_iDfSkill = int.Parse(info["DfSkill"].ToString());
			}
			
			if (info["SpSkill"].ToString() != "")
			{
				pet.m_iSpSkill = int.Parse(info["SpSkill"].ToString());
			}
			
			if (info["SapSkill"].ToString() != "")
			{
				pet.m_iSapSkill = int.Parse(info["SapSkill"].ToString());
			}
			
			//模板属性aa
			if (info["Property.Str"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iStr = int.Parse(info["Property.Str"].ToString());
			}
			if (info["Property.Int"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iInt = int.Parse(info["Property.Int"].ToString());
			}
			if (info["Property.Dex"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iDex = int.Parse(info["Property.Dex"].ToString());
			}
			if (info["Property.Sta"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iSta = int.Parse(info["Property.Sta"].ToString());
			}
			if (info["Property.Fai"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iFai = int.Parse(info["Property.Fai"].ToString());
			}
			if (info["Property.MaxHP"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iMaxHP = int.Parse(info["Property.MaxHP"].ToString());
			}
			if (info["Property.MaxSP"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iMaxSP = int.Parse(info["Property.MaxSP"].ToString());
			}
			if (info["Property.HPTick"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iHPTick = int.Parse(info["Property.HPTick"].ToString());
			}
			if (info["Property.SPTick"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iSPTick = int.Parse(info["Property.SPTick"].ToString());
			}
			if (info["Property.AtkDmgMin"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iAtkDmgMin = int.Parse(info["Property.AtkDmgMin"].ToString());
			}
			if (info["Property.AtkDmgMax"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iAtkDmgMax = int.Parse(info["Property.AtkDmgMax"].ToString());
			}
			if (info["Property.Def"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iDef = int.Parse(info["Property.Def"].ToString());
			}
			if (info["Property.IceAtt"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iIceAtt = int.Parse(info["Property.IceAtt"].ToString());
			}
			if (info["Property.FireAtt"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iFireAtt = int.Parse(info["Property.FireAtt"].ToString());
			}
			if (info["Property.PoisonAtt"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iPoisonAtt = int.Parse(info["Property.PoisonAtt"].ToString());
			}
			if (info["Property.ThunderAtt"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iThunderAtt = int.Parse(info["Property.ThunderAtt"].ToString());
			}
			if (info["Property.IceDef"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iIceDef = int.Parse(info["Property.IceDef"].ToString());
			}
			if (info["Property.FireDef"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iFireDef = int.Parse(info["Property.FireDef"].ToString());
			}
			if (info["Property.PoisonDef"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iPoisonDef = int.Parse(info["Property.PoisonDef"].ToString());
			}
			if (info["Property.ThunderDef"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iThunderDef = int.Parse(info["Property.ThunderDef"].ToString());
			}
			if (info["Property.Pierce"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iPierce = int.Parse(info["Property.Pierce"].ToString());
			}
			if (info["Property.Hit"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iHit = int.Parse(info["Property.Hit"].ToString());
			}
			if (info["Property.Dodge"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iDodge = int.Parse(info["Property.Dodge"].ToString());
			}
			if (info["Property.Cri"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iCri = int.Parse(info["Property.Cri"].ToString());
			}
			if (info["Property.Flex"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iFlex = int.Parse(info["Property.Flex"].ToString());
			}
			if (info["Property.CriDmg"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iCriDmg = int.Parse(info["Property.CriDmg"].ToString());
			}
			if (info["Property.CriDmgDef"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iCriDmgDef = int.Parse(info["Property.CriDmgDef"].ToString());
			}
			if (info["Property.BodySize"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iBodySize = int.Parse(info["Property.BodySize"].ToString());
			}
			if (info["Property.AttSize"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iAttSize = int.Parse(info["Property.AttSize"].ToString());
			}
			if (info["Property.AttSpeedModPer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iAttSpeedModPer = int.Parse(info["Property.AttSpeedModPer"].ToString());
			}
			if (info["Property.MoveSpeedModPer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iMoveSpeedModPer = int.Parse(info["Property.MoveSpeedModPer"].ToString());
			}
			if (info["Property.PiercePer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iPiercePer = int.Parse(info["Property.PiercePer"].ToString());
			}
			if (info["Property.HitPer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iHitPer = int.Parse(info["Property.HitPer"].ToString());
			}
			if (info["Property.DodgePer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iDodgePer = int.Parse(info["Property.DodgePer"].ToString());
			}
			if (info["Property.CriPer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iCriPer = int.Parse(info["Property.CriPer"].ToString());
			}
			if (info["Property.FlexPer"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iFlexPer = int.Parse(info["Property.FlexPer"].ToString());
			}
			if (info["Property.AttSpeed"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iAttSpeed = int.Parse(info["Property.AttSpeed"].ToString());
			}
			if (info["Property.MoveSpeed"].ToString() != "")
			{
				pet.m_TemplateProperty.m_iMoveSpeed = int.Parse(info["Property.MoveSpeed"].ToString());
			}
			//属性计算参数aa
			if (info["Coe.AttA"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iAttA = int.Parse(info["Coe.AttA"].ToString());
			}
			if (info["Coe.AttB"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iAttB = int.Parse(info["Coe.AttB"].ToString());
			}
			if (info["Coe.AttC"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iAttC = int.Parse(info["Coe.AttC"].ToString());
			}
			if (info["Coe.DefA"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iDefA = int.Parse(info["Coe.DefA"].ToString());
			}
			if (info["Coe.DefB"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iDefB = int.Parse(info["Coe.DefB"].ToString());
			}
			if (info["Coe.DefC"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iDefC = int.Parse(info["Coe.DefC"].ToString());
			}
			if (info["Coe.HPA"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iHPA = int.Parse(info["Coe.HPA"].ToString());
			}
			if (info["Coe.HPB"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iHPB = int.Parse(info["Coe.HPB"].ToString());
			}
			if (info["Coe.HPC"].ToString() != "")
			{
				pet.m_TemplateCoe.m_iHPC = int.Parse(info["Coe.HPC"].ToString());
			}
			//其他信息aa
			if (info["BaseJob"].ToString() != "")
			{
				pet.m_iBaseJob = int.Parse(info["BaseJob"].ToString());
			}
			if (info["Desc"].ToString() != "")
			{
				pet.m_strDesc = info["Desc"].ToString();
			}
			if (info["SPD1"].ToString() != "")
			{
				pet.m_strSPD1 = info["SPD1"].ToString();
			}
			if (info["SPD2"].ToString() != "")
			{
				pet.m_strSPD2 = info["SPD2"].ToString();
			}
			if (info["Res"].ToString() != "")
			{
				pet.m_strRes = info["Res"].ToString();
			}
			if (info["Skill1"].ToString() != "")
			{
				pet.m_iSkill1 = int.Parse(info["Skill1"].ToString());
			}
			if (info["Skill2"].ToString() != "")
			{
				pet.m_iSkill2 = int.Parse(info["Skill2"].ToString());
			}
			if (info["Skill3"].ToString() != "")
			{
				pet.m_iSkill3 = int.Parse(info["Skill3"].ToString());
			}
			if (info["Skill4"].ToString() != "")
			{
				pet.m_iSkill4 = int.Parse(info["Skill4"].ToString());
			}
			if (info["Buff1"].ToString() != "")
			{
				pet.m_iBuff1 = int.Parse(info["Buff1"].ToString());
			}
			if (info["Buff2"].ToString() != "")
			{
				pet.m_iBuff2 = int.Parse(info["Buff2"].ToString());
			}
			if (info["Buff3"].ToString() != "")
			{
				pet.m_iBuff3 = int.Parse(info["Buff3"].ToString());
			}
			if (info["Buff4"].ToString() != "")
			{
				pet.m_iBuff4 = int.Parse(info["Buff4"].ToString());
			}
			if (info["AIID"].ToString() != "")
			{
				pet.m_iAIID = int.Parse(info["AIID"].ToString());
			}
			if (info["Icon"].ToString() != "")
			{
				pet.m_strIcon = info["Icon"].ToString();
			}
			if (info["FollowDistance"].ToString() != "")
			{
				pet.m_FollowDistance = int.Parse(info["FollowDistance"].ToString());
			}
			if (info["BattleFollowDistance"].ToString() != "")
			{
				pet.m_BattleFollowDistance = int.Parse(info["BattleFollowDistance"].ToString());
			}
			if (info["Speed"].ToString() != "")
			{
				pet.m_Speed = int.Parse(info["Speed"].ToString());
			}
			if (info["BeatPriority"].ToString() != "")
			{
				pet.m_BeatPriority = int.Parse(info["BeatPriority"].ToString());
			}
			if (info["DisappearTime"].ToString() != "")
			{
				pet.m_DisappearTime = int.Parse(info["DisappearTime"].ToString());
			}
			if (info["HPBarNum"].ToString() != "")
			{
				pet.m_HPBarNum = int.Parse(info["HPBarNum"].ToString());
			}
			if (info["HPBarHeight"].ToString() != "")
			{
				pet.m_HPBarHeight = int.Parse(info["HPBarHeight"].ToString());
			}
			if (info["FriAIID"].ToString() != "")
			{
				pet.m_FriAIID = int.Parse(info["FriAIID"].ToString());
			}
			if (info["SellMoney"].ToString() != "")
			{
				pet.m_SellMoney = int.Parse(info["SellMoney"].ToString());
			}
			if (info["CanLevelUp"].ToString() != "")
			{
				pet.m_CanLevelUp = int.Parse(info["CanLevelUp"].ToString());
			}
			if (info["CanMerge"].ToString() != "")
			{
				pet.m_CanMerge = int.Parse(info["CanMerge"].ToString());
			}
		}

		return pet;
	}

	public Hashtable GetPetPropInfoByTemplateID(int iID, int iUp, int iLevel)
	{
		Hashtable kPetProperty = new Hashtable();
		if (kPetProperty == null)
			return null;

		SClientPetInfo pet = GetPetInfoByTemplateID(iID, iUp, iLevel);
		if (pet != null)
		{
			kPetProperty["TemplateID"] = iID;
			kPetProperty["Level"] = iLevel;
			kPetProperty["Up"] = iUp;

			kPetProperty["MaxHP"] = (int)pet.m_CurProperty.m_iMaxHP;
			kPetProperty["AtkDmgMax"] = (int)pet.m_CurProperty.m_iAtkDmgMax;
			kPetProperty["Def"] = (int)pet.m_CurProperty.m_iDef;

			kPetProperty["Str"] = (int)pet.m_TemplateProperty.m_iStr;
			kPetProperty["Int"]	= (int)pet.m_TemplateProperty.m_iInt;
			kPetProperty["Dex"]	= (int)pet.m_TemplateProperty.m_iDex;
			kPetProperty["Sta"]	= (int)pet.m_TemplateProperty.m_iSta;
			kPetProperty["Fai"] = (int)pet.m_TemplateProperty.m_iFai;
			kPetProperty["MaxSP"] = (int)pet.m_TemplateProperty.m_iMaxSP;
			kPetProperty["HPTick"] = (int)pet.m_TemplateProperty.m_iHPTick;
			kPetProperty["SPTick"] = (int)pet.m_TemplateProperty.m_iSPTick;
			kPetProperty["AtkDmgMin"] = (int)pet.m_TemplateProperty.m_iAtkDmgMin;
			kPetProperty["IceAtt"] = (int)pet.m_TemplateProperty.m_iIceAtt;
			kPetProperty["FireAtt"] = (int)pet.m_TemplateProperty.m_iFireAtt;
			kPetProperty["PoisonAtt"] = (int)pet.m_TemplateProperty.m_iPoisonAtt;
			kPetProperty["ThunderAtt"] = (int)pet.m_TemplateProperty.m_iThunderAtt;
			kPetProperty["IceDef"] = (int)pet.m_TemplateProperty.m_iIceDef;
			kPetProperty["FireDef"] = (int)pet.m_TemplateProperty.m_iFireDef;
			kPetProperty["PoisonDef"] = (int)pet.m_TemplateProperty.m_iPoisonDef;
			kPetProperty["ThunderDef"] = (int)pet.m_TemplateProperty.m_iThunderDef;
			kPetProperty["Pierce"] = (int)pet.m_TemplateProperty.m_iPierce;
			kPetProperty["Hit"] = (int)pet.m_TemplateProperty.m_iHit;
			kPetProperty["Dodge"] = (int)pet.m_TemplateProperty.m_iDodge;
			kPetProperty["Cri"] = (int)pet.m_TemplateProperty.m_iCri;
			kPetProperty["Flex"] = (int)pet.m_TemplateProperty.m_iFlex;
			kPetProperty["CriDmg"] = (int)pet.m_TemplateProperty.m_iCriDmg;
			kPetProperty["CriDmgDef"] = (int)pet.m_TemplateProperty.m_iCriDmgDef;
			kPetProperty["BodySize"] = (int)pet.m_TemplateProperty.m_iBodySize;
			kPetProperty["AttSize"] = (int)pet.m_TemplateProperty.m_iAttSize;
			kPetProperty["AttSpeedModPer"] = (int)pet.m_TemplateProperty.m_iAttSpeedModPer;
			kPetProperty["MoveSpeedModPer"] = (int)pet.m_TemplateProperty.m_iMoveSpeedModPer;
			kPetProperty["PiercePer"] = (int)pet.m_TemplateProperty.m_iPiercePer;
			kPetProperty["HitPer"] = (int)pet.m_TemplateProperty.m_iHitPer;
			kPetProperty["DodgePer"] = (int)pet.m_TemplateProperty.m_iDodgePer;
			kPetProperty["CriPer"] = (int)pet.m_TemplateProperty.m_iCriPer;
			kPetProperty["FlexPer"] = (int)pet.m_TemplateProperty.m_iFlexPer;
			kPetProperty["AttSpeed"] = (int)pet.m_TemplateProperty.m_iAttSpeed;
			kPetProperty["MoveSpeed"] = (int)pet.m_TemplateProperty.m_iMoveSpeed;
		}

		return kPetProperty;
	}
	
	public bool hasPet(string tid)
	{
		foreach(DictionaryEntry info in m_petListInfo)
		{	
			SClientPetInfo pet = info.Value as SClientPetInfo;
			if (pet != null && pet.m_uiTemplateID.ToString() == tid)
			{
				return true;	
			}
		}
		return false;
	}
	
	public Hashtable GetPetList()
	{
		return m_petListInfo;
	}
	
	public float GetPetAttCoe(uint iTemplateID, int iUp, int iLevel)
	{
		float fResult = 0.0f;
		
		if (iTemplateID<=0)
			return fResult;
		
		Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(iTemplateID.ToString());
		if (petInfo==null)
			return fResult;
		
		Hashtable upItem = null;
		upItem = sdConfDataMgr.Instance().GetPetUp(iUp.ToString());
		if (upItem==null)
			return fResult;
		
		if (upItem["UpCoe"].ToString() != "")
		{
			int iPetUpCoe = int.Parse(upItem["UpCoe"].ToString());
			float fTemp1 = (float)iLevel/200.0f;
			float fTemp2 = (float)(int.Parse(petInfo["Coe.AttA"].ToString()))/10000.0f;
			fResult = Mathf.Pow(fTemp1, fTemp2);
			fResult = fResult*(float)(int.Parse(petInfo["Coe.AttB"].ToString()));
			fResult = fResult*(float)iPetUpCoe/10000.0f;
			fResult = fResult + (float)(int.Parse(petInfo["Coe.AttC"].ToString()));
		}
		
		return fResult;
	}
	
	public float GetPetDefCoe(uint iTemplateID, int iUp, int iLevel)
	{
		float fResult = 0.0f;
		
		if (iTemplateID<=0)
			return fResult;
		
		Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(iTemplateID.ToString());
		if (petInfo==null)
			return fResult;
		
		Hashtable upItem = null;
		upItem = sdConfDataMgr.Instance().GetPetUp(iUp.ToString());
		if (upItem==null)
			return fResult;
		
		if (upItem["UpCoe"].ToString() != "")
		{
			int iPetUpCoe = int.Parse(upItem["UpCoe"].ToString());
			float fTemp1 = (float)iLevel/200.0f;
			float fTemp2 = (float)(int.Parse(petInfo["Coe.DefA"].ToString()))/10000.0f;
			fResult = Mathf.Pow(fTemp1, fTemp2);
			fResult = fResult*(float)(int.Parse(petInfo["Coe.DefB"].ToString()));
			fResult = fResult*(float)iPetUpCoe/10000.0f;
			fResult = fResult + (float)(int.Parse(petInfo["Coe.DefC"].ToString()));
		}
		
		return fResult;
	}
	
	public float GetPetHPCoe(uint iTemplateID, int iUp, int iLevel)
	{
		float fResult = 0.0f;
		
		if (iTemplateID<=0)
			return fResult;
		
		Hashtable petInfo = sdConfDataMgr.Instance().GetPetTemplate(iTemplateID.ToString());
		if (petInfo==null)
			return fResult;
		
		Hashtable upItem = null;
		upItem = sdConfDataMgr.Instance().GetPetUp(iUp.ToString());
		if (upItem==null)
			return fResult;
		
		if (upItem["UpCoe"].ToString() != "")
		{
			int iPetUpCoe = int.Parse(upItem["UpCoe"].ToString());
			float fTemp1 = (float)iLevel/200.0f;
			float fTemp2 = (float)(int.Parse(petInfo["Coe.HPA"].ToString()))/10000.0f;
			fResult = Mathf.Pow(fTemp1, fTemp2);
			fResult = fResult*(float)(int.Parse(petInfo["Coe.HPB"].ToString()));
			fResult = fResult*(float)iPetUpCoe/10000.0f;
			fResult = fResult + (float)(int.Parse(petInfo["Coe.HPC"].ToString()));
		}
		
		return fResult;
	}
	
	public float GetPetAttackValueCoe(SClientPetInfo petInfo)
	{
		float fResult = 0.0f;
		
		if (petInfo==null)
			return fResult;
		
		float fTemp1 = (float)petInfo.m_CurProperty.m_iDef+270.0f*(float)petInfo.m_iLevel+2700.0f;
		fTemp1 = (float)petInfo.m_CurProperty.m_iDef/fTemp1;
		fTemp1 = 1.0f - fTemp1;
		fTemp1 = (float)petInfo.m_CurProperty.m_iMaxHP/fTemp1;
		fTemp1 = fTemp1*0.2f;
		float fTemp2 = (float)petInfo.m_CurProperty.m_iAtkDmgMax*0.416f;
		fResult = fTemp1 + fTemp2;
		
		return fResult;
	}

	//取得一个宠物当前等级下升级需要经验..
	public UInt64 GetPetCurLevelNeedExp(uint iTemplateID, int iAbility, int iLevel)
	{
		UInt64 needExp = 0;
		int iIndex = iAbility*10000+iLevel;
		float fNeedExp = float.Parse(sdConfDataMgr.Instance().GetPetLevelsValueByStringKey(iIndex, "Exp"));
		float fExpCoe = float.Parse( sdConfDataMgr.Instance().GetPetTemplateValueByStringKey((int)iTemplateID, "ExpCoe") );
		fExpCoe = fExpCoe/10000.0f;
		fNeedExp = fNeedExp*fExpCoe;
		needExp = (UInt64)fNeedExp;
		return needExp;
	}

	//取得一个宠物能提供的经验..
	public UInt64 GetPetCurLevelGiveExp(uint iTemplateID, int iAbility, int iLevel)
	{
		UInt64 needExp = 0;
		int iIndex = iAbility*10000+iLevel;
		float fNeedExp = float.Parse(sdConfDataMgr.Instance().GetPetLevelsValueByStringKey(iIndex, "Exp2"));
		float fExpCoe = float.Parse( sdConfDataMgr.Instance().GetPetTemplateValueByStringKey((int)iTemplateID, "Exp2Coe") );
		fExpCoe = fExpCoe/10000.0f;
		fNeedExp = fNeedExp*fExpCoe;
		needExp = (UInt64)fNeedExp;
		string strExp = sdConfDataMgr.Instance().GetPetTemplateValueByStringKey((int)iTemplateID, "ExpBase");
		needExp += UInt64.Parse(strExp);
		return needExp;
	}

	//取得一个宠物当前能升级到最高等级所需总经验..
	public UInt64 GetPetLevelMaxAllNeedExp(uint iTemplateID, int iAbility)
	{
		int iMaxLevel = MAX_PET_LEVEL;
		int iMyLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
		iMyLevel = iMyLevel+20;
		if (iMaxLevel>iMyLevel)
			iMaxLevel = iMyLevel;

		UInt64 needExp = 0;
		if (iMaxLevel>1)
		{
			iMaxLevel = iMaxLevel-1;
			for (int i = 1; i<=iMaxLevel; i++)
			{
				UInt64 uuExp = GetPetCurLevelNeedExp(iTemplateID, iAbility, i);
				needExp += uuExp;
			}
		}

		return needExp;
	}

	//取得A宠物吃B宠物需要消耗的金钱..
	public UInt64 GetPetAEatPetBNeedMoney(uint iTemplateIDA, int iAbilityB, int iLevelB)
	{
		UInt64 needMoney = 0;
		int iIndex = iAbilityB*10000+iLevelB;
		float fNeedMoney = float.Parse(sdConfDataMgr.Instance().GetPetLevelsValueByStringKey(iIndex, "UseMoney"));
		float fMoneyCoe = float.Parse( sdConfDataMgr.Instance().GetPetTemplateValueByStringKey((int)iTemplateIDA, "MoneyCoe") );
		fMoneyCoe = fMoneyCoe/10000.0f;
		fNeedMoney = fNeedMoney*fMoneyCoe;
		needMoney = (UInt64)fNeedMoney;
		return needMoney;
	}

	public void SetCurBattleTeamPetDBID()
	{
		if (mPetCurTeam<0||mPetCurTeam>2)
			return;
		
		for (int i=0;i<7;i++)
		{
			mPetTeam[i] = mPetAllTeam[i+7*mPetCurTeam];
		}
	}
	
	//宠物是否在当前出战队列..
	public int GetIsInBattleTeam(UInt64 uuDBID)
	{
		if (uuDBID==UInt64.MaxValue)
			return 0;
		
		if (uuDBID==mPetTeam[0]||uuDBID==mPetTeam[1]||uuDBID==mPetTeam[2])
			return 1;
		
		if (uuDBID==mPetTeam[3]||uuDBID==mPetTeam[4]||uuDBID==mPetTeam[5]||uuDBID==mPetTeam[6])
			return 2;
		
		return 0;
	}
	
	//宠物是否在完整出战队列中..
	public int GetIsInBattleAllTeam(UInt64 uuDBID)
	{
		if (uuDBID==UInt64.MaxValue)
			return 0;
		
		if (uuDBID==mPetAllTeam[0]||uuDBID==mPetAllTeam[1]||uuDBID==mPetAllTeam[2])
			return 1;

		if (uuDBID==mPetAllTeam[7]||uuDBID==mPetAllTeam[8]||uuDBID==mPetAllTeam[9])
			return 2;

		if (uuDBID==mPetAllTeam[14]||uuDBID==mPetAllTeam[15]||uuDBID==mPetAllTeam[16])
			return 3;
		
		if (uuDBID==mPetAllTeam[3]||uuDBID==mPetAllTeam[4]||uuDBID==mPetAllTeam[5]||uuDBID==mPetAllTeam[6])
			return 11;

		if (uuDBID==mPetAllTeam[10]||uuDBID==mPetAllTeam[11]||uuDBID==mPetAllTeam[12]||uuDBID==mPetAllTeam[13])
			return 22;

		if (uuDBID==mPetAllTeam[17]||uuDBID==mPetAllTeam[18]||uuDBID==mPetAllTeam[19]||uuDBID==mPetAllTeam[20])
			return 33;
		
		return 0;
	}
	//宠物是否在当前页展示的出战队列中..
	public int GetPetTemplateIsInCurPageBattleTeam(int iTemplateID, int iTeamIndex)
	{
		if (iTemplateID<=0)
			return 0;
		
		int[] iPetTeam = {0,0,0,0,0,0,0};
		
		for(int i=0;i<7;i++)
		{
			UInt64 uuDBID = mPetAllTeam[iTeamIndex*7+i];
			if (uuDBID==UInt64.MaxValue)
			{
				iPetTeam[i] = 0;
			}
			else
			{
				SClientPetInfo petInfo = GetPetInfo(uuDBID);
				if (petInfo!=null)
				{
					iPetTeam[i] = (int)petInfo.m_uiTemplateID;
				}
				else
				{
					iPetTeam[i] = 0;
				}
			}
		}
		
		if (iTemplateID==iPetTeam[0]||iTemplateID==iPetTeam[1]||iTemplateID==iPetTeam[2])
			return 1;
		
		if (iTemplateID==iPetTeam[3]||iTemplateID==iPetTeam[4]||iTemplateID==iPetTeam[5]||iTemplateID==iPetTeam[6])
			return 2;
		
		return 0;
	}
	//宠物在哪个战队中..
	
	public void ResetPetLevelUpDBID()
	{
		m_uuPetLevelSelectID[0] = UInt64.MaxValue;
		m_uuPetLevelSelectID[1] = UInt64.MaxValue;
		m_uuPetLevelSelectID[2] = UInt64.MaxValue;
		m_uuPetLevelSelectID[3] = UInt64.MaxValue;
		m_uuPetLevelSelectID[4] = UInt64.MaxValue;
		m_uuPetLevelSelectID[5] = UInt64.MaxValue;
		m_uuPetLevelSelectID[6] = UInt64.MaxValue;
		m_uuPetLevelSelectID[7] = UInt64.MaxValue;
	}
	
	public bool AllPetLevelUpDBIDIsNull()
	{
		bool bResult = true;
		for (int i=0;i<8;i++)
		{
			if (m_uuPetLevelSelectID[i]!=UInt64.MaxValue)
			{
				bResult=false;
				break;
			}
		}
		
		return bResult;
	}
	
	public int GetPetLevelupDBIDNum()
	{
		int iResult = 0;
		for (int i=0;i<8;i++)
		{
			if (m_uuPetLevelSelectID[i]!=UInt64.MaxValue)
			{
				iResult ++;
			}
		}
		
		return iResult;
	}
	
	//宠物装备相关..
	public sdGamePetItem createItem(int templateID, UInt64 instanceID, int count)
	{
		sdGamePetItem item = new sdGamePetItem();
		petItemDB[instanceID] = item;
		
		item.templateID = templateID;
		item.instanceID = instanceID;
		item.count = count;
		
		Hashtable info = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
		if (info != null)
		{
			item.mdlPath = info["Filename"].ToString();
			item.mdlPartName = info["FilePart"].ToString();
			item.anchorNodeName	= sdGameActor.WeaponDummy(info["Character"].ToString());
			item.itemClass = int.Parse(info["Class"].ToString());
			item.subClass = int.Parse(info["SubClass"].ToString());
			item.iCharacter = int.Parse(info["Character"].ToString());
			item.level = int.Parse(info["Level"].ToString());
			item.quility = int.Parse(info["Quility"].ToString());
		}
			
		return item;
	}
	
	public sdGamePetItem getPetItem(UInt64 instanceID)
	{
		if(petItemDB.ContainsKey(instanceID))
			return petItemDB[instanceID] as sdGamePetItem;
		else
			return null;
	}
	
	public bool removePetItem(UInt64 instanceID)
	{
		if(petItemDB.ContainsKey(instanceID))
		{
			petItemDB.Remove(instanceID);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool addOrUpdatePetItem(int templateID, UInt64 instanceID, int count)
	{
		if(petItemDB.ContainsKey(instanceID))
		{
			sdGamePetItem item = getPetItem(instanceID);
			if (item!=null)
			{
				item.templateID = templateID;
				item.instanceID = instanceID;
				item.count = count;
			}
			return false;
		}
		else
		{
			createItem(templateID, instanceID, count);
			return true;
		}
	}
	
	public sdGamePetItem getPetItemByClass(int strClass, int iCharacter)
	{
		foreach(DictionaryEntry item in petItemDB)
		{
			sdGamePetItem info = (sdGamePetItem)item.Value;
			if (info.itemClass == strClass && info.iCharacter == iCharacter)
			{
				return info;	
			}
		}
		return null;	
	}
	
	public Hashtable getAllPetItemByCharacter(int iCharacter)
	{
		Hashtable table = new Hashtable();
		foreach(DictionaryEntry item in petItemDB)
		{
			sdGamePetItem info = (sdGamePetItem)item.Value;
			if (info.iCharacter == iCharacter || iCharacter == -1)
			{
				table[item.Key] = info;
			}
		}
		
		return table;
	}

	public sdGamePetItem getBestPetItemByCharacter(int iCharacter)
	{
		sdGamePetItem bestItem = null;
		foreach(DictionaryEntry item in petItemDB)
		{
			sdGamePetItem info = (sdGamePetItem)item.Value;
			if (info.iCharacter == iCharacter || iCharacter == -1)
			{
				if (bestItem==null)
				{
					bestItem = info;
				}
				else
				{
					int score1 = sdConfDataMgr.Instance().GetItemScore(bestItem.templateID.ToString(), 0);
					int score2 = sdConfDataMgr.Instance().GetItemScore(info.templateID.ToString(), 0);
					if (score2>score1)
						bestItem = info;
				}
			}
		}

		return bestItem;
	}
	
	public static void SetLabelColorByAbility(int iAbility, GameObject lb_name)
	{
		if (lb_name!=null)
		{
			Color PetEquipColor0 = new Color(255f/255f, 255f/255f, 255f/255f, 1f);
			Color PetEquipColor1 = new Color(45f/255f, 210f/255f, 18f/255f, 1f);
			Color PetEquipColor2 = new Color(0f, 144f/255f, 1f, 1f);
			Color PetEquipColor3 = new Color(164f/255f, 84f/255f, 254f/255f, 1f);
			Color PetEquipColor4 = new Color(1f, 179f/255f, 15f/255f, 1f);
			
			if (iAbility==1)
				lb_name.GetComponent<UILabel>().color = PetEquipColor0;
			else if (iAbility==2)
				lb_name.GetComponent<UILabel>().color = PetEquipColor1;
			else if (iAbility==3)
				lb_name.GetComponent<UILabel>().color = PetEquipColor2;
			else if (iAbility==4)
				lb_name.GetComponent<UILabel>().color = PetEquipColor3;
			else if (iAbility==5)
				lb_name.GetComponent<UILabel>().color = PetEquipColor4;
			else 
				lb_name.GetComponent<UILabel>().color = PetEquipColor0;
		}
	}

	public void AutoSelectPetLevelMaterial()
	{
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdUIPetLevelPnl petLevelPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
			if (petLevelPnl)
			{
				UInt64 uuSelfDBID = petLevelPnl.m_uuDBID;
				if (uuSelfDBID!=UInt64.MaxValue)
				{
					//清空原先的选择..
					ResetPetLevelUpDBID();
					//取得满足条件的宠物材料..
					Hashtable listPet = sdNewPetMgr.Instance.GetPetList();
					Hashtable list = new Hashtable();
					foreach(DictionaryEntry info in listPet)
					{
						string key1 = info.Key.ToString();
						SClientPetInfo petvalue = info.Value as SClientPetInfo;
						if ( sdNewPetMgr.Instance.GetIsInBattleAllTeam(UInt64.Parse(key1))==0 && key1!=uuSelfDBID.ToString() && petvalue.m_Lock!=1 )
							list.Add(key1,petvalue);
					}
					//将取得的宠物数据填充到List中，用来按星级排序..
					List<SClientPetInfo> listOther = new List<SClientPetInfo>();
					List<SClientPetInfo> listExp = new List<SClientPetInfo>();
					foreach(DictionaryEntry info in list)
					{
						SClientPetInfo info1 = info.Value as SClientPetInfo;
						if (sdNewPetMgr.Instance.IsExpPet(info1.m_uiTemplateID))
							listExp.Add (info1);
						else
							listOther.Add(info1);
					}
					listOther.Sort(SClientPetInfo.PetSortByAbilityBeginBig);
					listExp.Sort(SClientPetInfo.PetSortByAbilityBeginBig);
					//取得满足条件的1星，2星宠物..
					int iIndex = 0;
					//不满8个，白色经验童子..
					if (iIndex<8)
					{
						foreach (SClientPetInfo infoEntry in listExp)
						{
							if (iIndex<8)
							{
								if (infoEntry.m_iAbility==1)
								{
									m_uuPetLevelSelectID[iIndex] = infoEntry.m_uuDBID;
									iIndex++;
								}
							}
							else
							{
								break;
							}
						}
					}
					//不满8个，白色普通卡..
					if (iIndex<8)
					{
						foreach (SClientPetInfo infoEntry in listOther)
						{
							if (iIndex<8)
							{
								if (infoEntry.m_iAbility==1)
								{
									m_uuPetLevelSelectID[iIndex] = infoEntry.m_uuDBID;
									iIndex++;
								}
							}
							else
							{
								break;
							}
						}
					}
					//不满8个，绿色经验童子..
					if (iIndex<8)
					{
						foreach (SClientPetInfo infoEntry in listExp)
						{
							if (iIndex<8)
							{
								if (infoEntry.m_iAbility==2)
								{
									m_uuPetLevelSelectID[iIndex] = infoEntry.m_uuDBID;
									iIndex++;
								}
							}
							else
							{
								break;
							}
						}
					}
					//不满8个，绿色普通卡..
					if (iIndex<8)
					{
						foreach (SClientPetInfo infoEntry in listOther)
						{
							if (iIndex<8)
							{
								if (infoEntry.m_iAbility==2)
								{
									m_uuPetLevelSelectID[iIndex] = infoEntry.m_uuDBID;
									iIndex++;
								}
							}
							else
							{
								break;
							}
						}
					}
					//不满8个，其他经验童子..
					foreach (SClientPetInfo infoEntry in listExp)
					{
						if (iIndex<8)
						{
							if (infoEntry.m_iAbility==3||infoEntry.m_iAbility==4)
							{
								m_uuPetLevelSelectID[iIndex] = infoEntry.m_uuDBID;
								iIndex++;
							}
						}
						else
						{
							break;
						}
					}
					//刷新UI界面..
					petLevelPnl.ReflashPetLevelIcon(true);
					petLevelPnl.ResetPetLevelCostAndExp();
				}
			}
		}
	}

	// 计算宠物战队Buff效果aa
	public List<int> GetPetGroupBuff(sdGameActor kActor)
	{
		Hashtable kGroupTable = sdConfDataMgr.Instance().ParsedPetGroupsTable;
		if (kGroupTable == null)
			return null;

		Hashtable kSapTable = sdConfDataMgr.Instance().ParsedPetSapsTable;
		if (kSapTable == null)
			return null;

		// 
		bool bIsPet = (kActor.GetActorType() == ActorType.AT_Pet);

		// 查找匹配的组合效果aa
		List<int> kValidSaps = new List<int>();
		foreach (DictionaryEntry kEntry in kGroupTable)
		{
			Hashtable kGroupItem = kEntry.Value as Hashtable;
			if (kGroupItem == null)
				continue;

			if (bIsPet)
			{
				bool bHasPet = (bool)(kGroupItem["HasPet"]);
				if (!bHasPet)
					continue;	//< 组合对宠物无效aa
			}
			else
			{
				bool bHasRole = (bool)(kGroupItem["HasRole"]);
				if (!bHasRole)
					continue;	//< 组合对角色无效aa
			}

			bool bValid = true;
			for (int iIndex = 1; iIndex <= 4; ++iIndex)
			{
				string kSkillKey = "Data" + iIndex.ToString() + ".PetID";
				int iPetTid = (int)kGroupItem[kSkillKey];
				if (iPetTid > 0 && !IsPetInPetTeam(iPetTid))
				{
					bValid = false;
					break;
				}
			}

			if (bValid)
			{
				int iSapId = (int)kGroupItem["SapID"];
				kValidSaps.Add(iSapId);
			}
		}

		// 查找组合的效果
		List<int> kBuffList = new List<int>();
		foreach (int iSap in kValidSaps)
		{
			Hashtable kSapItem = kSapTable[iSap] as Hashtable;
			if (kSapItem == null)
				continue;

			for (int iIndex = 1; iIndex <= 6; ++iIndex)
			{
				string kSkillKey = "Skill" + iIndex.ToString() + ".SkillID";
				int iSkillId = (int)kSapItem[kSkillKey];
				if (iSkillId > 0)
				{
					kBuffList.Add(iSkillId);
				}
			}
		}

		return kBuffList;
	}

	// 检查指定模版的宠物在战队是否存在aa
	protected bool IsPetInPetTeam(int iPetTid)
	{
		for (int i = 0; i < mPetTeamSize; ++i)
		{
			UInt64 uuDBID = mPetTeam[i];
			if (uuDBID == UInt64.MaxValue) 
				continue;
			
			Hashtable kProperty = GetPetPropertyFromDBID(uuDBID);
			if (kProperty == null)
				continue;

			int iTid = (int)kProperty["TemplateID"];
			if (iTid == iPetTid)
				return true;
		}

		return false;
	}

	// 新建宠物碎片列表..
	public void CreatePetGatherList(CliProto.SC_GATHER_INFO_NTF msg)
	{
		petGatherItemDB.Clear();

		CliProto.SC_GATHER_INFO_NTF refMSG = msg;
		int num = refMSG.m_Count;
		for (int i = 0; i < num; i++)
		{
			int iGId = refMSG.m_Gathers[i].m_ID;
			if (iGId>0)
			{
				CliProto.SC_GATHER_ITEM item = new CliProto.SC_GATHER_ITEM();
				item.m_ID = refMSG.m_Gathers[i].m_ID;
				item.m_CT = refMSG.m_Gathers[i].m_CT;

				petGatherItemDB[item.m_ID] = item;
			}
		}
	}

	// 取一个宠物碎片..
	public CliProto.SC_GATHER_ITEM getPetGatherItem(int iID)
	{
		if(petGatherItemDB.ContainsKey(iID))
			return petGatherItemDB[iID] as CliProto.SC_GATHER_ITEM;
		else
			return null;
	}

	// 根据宠物的模板ID取对应碎片最大数量..
	public int getPetGatherMaxNumByPetId(int iPetID)
	{
		int iCount = 0;
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iPetID.ToString());
		if (info != null)
		{
			int iGatherId = int.Parse(info["GID"].ToString());
			Hashtable infoGahter = sdConfDataMgr.Instance().GetItemById(iGatherId.ToString());
			if (infoGahter != null)
			{
				iCount = int.Parse(infoGahter["GatherCostCount"].ToString());
			}
		}

		return iCount;
	}

	// 根据宠物的模板ID取对应碎片当前数量..
	public int getPetGatherCurNumByPetId(int iPetID)
	{
		int iCount = 0;
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iPetID.ToString());
		if (info != null)
		{
			int iGatherId = int.Parse(info["GID"].ToString());
			if(petGatherItemDB.ContainsKey(iGatherId))
			{
				CliProto.SC_GATHER_ITEM item = getPetGatherItem(iGatherId);
				iCount = item.m_CT;
			}
		}
		
		return iCount;
	}

	// 删除一个宠物碎片..
	public bool removePetGatherItem(int iID)
	{
		if(petGatherItemDB.ContainsKey(iID))
		{
			petGatherItemDB.Remove(iID);
			return true;
		}
		else
		{
			return false;
		}
	}

	// 添加或者更新一个宠物碎片..
	public bool addOrUpdatePetGatherItem(int iID, int count)
	{
		if(petGatherItemDB.ContainsKey(iID))
		{
			CliProto.SC_GATHER_ITEM item = getPetGatherItem(iID);
			if (item!=null)
			{
				item.m_ID = iID;
				item.m_CT = count;
			}
			return false;
		}
		else
		{
			CliProto.SC_GATHER_ITEM item = new CliProto.SC_GATHER_ITEM();
			item.m_ID = iID;
			item.m_CT = count;
			petGatherItemDB[iID] = item;
			return true;
		}
	}

	public bool IsExpPet(uint uiID)
	{
		bool bResult = false;
		if (uiID==931 || uiID==932 || uiID==933 || uiID==934)
			bResult = true;

		return bResult;
	}
}
