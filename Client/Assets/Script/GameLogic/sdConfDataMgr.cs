using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;

enum RoleJob
{
	AllJob    = 0,   /// 全职业					   
					   
	Warrior   = 1,   /// 战士基础					   
	Berserker = 2,   /// 狂战士					   
	Knight    = 3,   /// 无畏骑士					   
					   
	Magic     = 4,   /// 法师基础					   
	Wizard    = 5,   /// 巫术师					   
	Warlock   = 6,   /// 魔导师					   
					   
	Rogue     = 7,   /// 游侠基础					   
	Assassin  = 8,   /// 刺客					   
	Gunner    = 9,   /// 重炮					   
					   
	Minister  = 10,  /// 牧师基础					   
	Priest    = 11,  /// 主教					   
	Pastor    = 12,  /// 战斗牧师
	RoleJobCount = 13,
}

enum EquipJob
{
	AllJob    = 0,   /// 全职业					   
					   
	Warrior   = 1<<1,   /// 战士基础					   
	Berserker = 1<<2,   /// 狂战士					   
	Knight    = 1<<3,   /// 无畏骑士					   
					   
	Magic     = 1<<4,   /// 法师基础					   
	Wizard    = 1<<5,   /// 巫术师					   
	Warlock   = 1<<6,   /// 魔导师					   
					   
	Rogue     = 1<<7,   /// 游侠基础					   
	Assassin  = 1<<8,   /// 刺客					   
	Gunner    = 1<<9,   /// 重炮					   
					   
	Minister  = 1<<10,  /// 牧师基础					   
	Priest    = 1<<11,  /// 主教					   
	Pastor    = 1<<12,  /// 战斗牧师	
}

enum PropertyIndex
{
	Str	= 1,			//力量
	Int,				//智力
	Dex,				//敏捷
	Sta,				//体力
	Fai,				//信念
	MaxHP,				//生命上限
	MaxSP,				//技力上限
	HPtick,				//生命回复
	SPtick,				//技力回复
	AtkDmgMin,			//最小伤害
	AtkDmgMax,			//最大伤害
	Def,				//防御
	IceAtt,				//冰属性攻击
	FireAtt,			//火属性攻击
	PoisonAtt,			//毒属性攻击
	ThunderAtt,			//雷属性攻击
	IceDef,				//冰属性抵抗
	FireDef,			//火属性抵抗
	PoisonDef,			//毒属性抵抗
	ThunderDef,			//雷属性抵抗
	Pierce,				//穿透
	Hit,				//命中
	Dodge,				//闪避
	Cri,				//致命一击
	Flex,				//韧性
	CriDmg,				//致命伤害
	CriDmgDef,			//致命防御
	AttSpeed,			//攻击速度
	MoveSpeed,			//移动速度
}

public enum StringTable
{
	Job,	
	UseLevel,
	Price,
}

public enum SkillForm
{
	Form_Cutting = 0,
	Form_Defend,
	Form_Recover,
	Form_Destroy,
	Form_Magic,
	Form_Secret,
	Form_Technique,
	Form_Agile,
	Form_Holy,
}

public enum Skill_ElementType
{
	Element_None = 0,
    Element_Ice,
	Element_Fire,
    Element_Poison,
	Element_Thunder,
}

public enum Item_Quility
{
	Grey = 0,
	White,
	Green,
	Blue,
	Purple,
	Gold,
}

public enum EquipType
{
	Hat,
	Clothes,
	Gloves,
	Pants,
	Cloak,
	Ring,
	Earring,
	Necklace,
	Weapon,
	Offhand,
	None,
}


public class skillIDRange
{
	public int min;
	public int max;
};

public class TitleLevel
{
	public int nTitle;
	public int nSta;
	public int Experience;
	public int HP;
	public int Money;
	public int AttackAdd;
	public int Reputation;
}

public	class SkillEffect
{
	//枚举..		
	//public	int		effectID;
	public  Int32     dwID;
	public	int		byTimingCondition;		//			action开始 结束 每hitpoint触发
	public	int   	byHitCondition;			//			效果生效条件 ESkillEffectHitCondition...
	public	int		byCountCondition;		//			每个目标触发 还是每命中触发
	public	int   	byMonsterBodyTypeFlag;	//			受到此效果的怪物体型需求 多种怪物 EMonsterBodyType的flag部分 只针对怪物...
	public	int   	bySelfCheckType;		//			检查自身的buff的类型 ECheckBuffType...
	public	int  	dwSelfBuffID;			//			检查自身buff的id...
	public	int   	byTgtCheckType;			//			检查目标的buff的类型 ECheckBuffType...
	public	int  	dwTgtBuffID;			//			检查目标buffid...
	public	int  	dwProbability;			//			技能效果发生概率 万分比...
	public	int   	byFlag;					//			技能效果作用目标标志 0不需要目标可能是位置也可能什么都不需要 1作用于释放者 2作用于目标...
	public	int  	dwOperationID;				//			效果模板id 调用operation.txt的效果...
	public	int  	dwOperationData;			//			带入参数1...
	public	int  	dwOperationData1;			//			带入参数2...

}

public struct stMovie
{
	public int stageID;     	//关卡idaaa
	public string npcName;		//npc名字aa
	public string npcModel;		//npc模型aa
	public int dialogueIndex;	//对话顺序号aa
	public int EndFlag;			//对话结束aaa
	public string content;		//对话内容aa
	public string portrait;			//表示npc是否出现
	public int npcShow;
	public int npcNoShow;		//表示npc消失
}

public struct stStageMovie
{
	public int nCharacter;
	public List<stMovie> movieList;
}


public enum DayQuestType
{
	Overmatch = 101,
	Challenge = 102,
	Partner = 103,
	Arena = 104,
	Running = 105,
	Dungeon = 106,
	Fighting = 107,
	Refine = 108,
}

public class DailyQuest
{
	public int DayQuestType;
	public int DayQuestCount;
	public int DayQuestScore;
	public int ResetEveryday;
	public int DayQuestId;
	public string QuestTitle;
	public string QuestDescription;
}

public class AwardBox
{
	public uint BoxID;
	public int NeedScore;
	public int ItemId1;
	public int ItemNum1;
	public int ItemId2;
	public int ItemNum2;
	public int ItemId3;
	public int ItemNum3;
	public int ItemId4;
	public int ItemNum4;
}

public class DailyAward
{
	public uint Index;
	public int Month;
	public int LoginDays;
	public int ItemID;
	public int ItemNum;
	public int VIPlevel;
	public int IfTips;
}

public class UpgradeAward
{
	public uint Level;
	public int ItemId1;
	public int ItemNum1;
	public int Item1IfTips;
	public int ItemId2;
	public int ItemNum2;
	public int Item2IfTips;
	public int ItemId3;
	public int ItemNum3;
	public int Item3IfTips;
	public int ItemId4;
	public int ItemNum4;
	public int Item4IfTips;
}

public class RandomName
{
	public int Id;
	public string FirstName;
	public string LastName;
}

public class VipUpgrade
{
	public int Viplevel;
	public int VIPItem1;
  	public int VipItemNumber1;
	public int VIPItem2;
	public int VipItemNumber2;
	public int VIPItem3;
	public int VipItemNumber3;
	public int VIPItem4;
	public int VipItemNumber4;
	public int CastCash;
	public int SaleCash;
	public string VipDescription;
	public int RMB;
	public int GoodsId;
}

public class RmbCharge
{
	public string UId;
	public int Rmb;
	public int Cash;
	public int Double;
	public int MonthCardId;
	public int IsOnSale;
	public int Position;
}

public class MonthCard
{


}

public class ActionPoint
{
	public int SpiritNum;
	public int CostNonCash;
	public int NeedVIPLevel;
}

public class sdConfDataMgr : object 
{	
	public  List<string> TitleName = new List<string>();  //军阶级别aaa
	private static string lan = "cn";
	public  static int GROUPCOUNT = 8;
	public	Hashtable[]	m_vecJobSkillInfo		=	new Hashtable[12];
	public	Hashtable[]	m_vecJobSkillAction		=	new Hashtable[12];
	public  skillIDRange[]   m_JobSkillRange    =   new skillIDRange[(int)RoleJob.RoleJobCount];
	public	Hashtable	m_MonsterSkillInfo 		= 	new Hashtable();
	public	Hashtable	m_MonsterSkillAction	= 	new Hashtable();
	public	Hashtable	m_BaseSkillEffect		=	new Hashtable();
	public	Hashtable	m_BuffAction			=	new Hashtable();
	public	GroupType[,] m_Group				=	new GroupType[GROUPCOUNT,GROUPCOUNT];
	public  Hashtable   m_BaseSummon            =   new Hashtable();
	public 	Hashtable	m_RoleLv				=	new Hashtable();
	public  Hashtable   m_Movie               	=   new Hashtable();
	private static sdConfDataMgr m_instance;
	Hashtable settingTable = new Hashtable();
    Hashtable roleSetting = new Hashtable();
    string roleSettingPath = "";

	public Hashtable m_dailyQuestDB = new Hashtable();
	public Hashtable m_dailyAwardDB = new Hashtable();
	public Hashtable m_upgradeAwardDB = new Hashtable();
	public Hashtable m_awardBoxDB = new Hashtable();
	public Hashtable m_randomNameDB = new Hashtable();
	public Hashtable m_vipUpgradeDB = new Hashtable();
	public Hashtable m_rmbChargeDB = new Hashtable();
	public Hashtable m_monthCardDB = new Hashtable();
	public Hashtable m_actionPointDB = new Hashtable();

	Dictionary<int, List<Hashtable>> itemUpTable = new Dictionary<int, List<Hashtable>>(); 
	
	public static sdConfDataMgr Instance()
	{
		if (m_instance == null)
		{
			m_instance = new sdConfDataMgr();
		}
		return m_instance;
	}

    public string GetProfessionText(int basejob)
    {
        string ret = "其他";
        switch (basejob)
        {
            case 1:
            case 2:
            case 3:
                ret = "战士";
                break;
            case 4:
            case 5:
            case 6:
                ret = "法师";
                break;
            case 7:
            case 8:
            case 9:
                ret = "游侠";
                break;
            case 10:
            case 11:
            case 12:
                ret = "牧师";
                break;
        }
        return ret;
    }
	
	public void Clear()
	{
		m_instance = null;
	}
	
	public void FinishLoadList(string path, SDCSV csv)
	{
		initCount--;
		string name = "";
		int index = path.LastIndexOf('.');
		if(index != -1)
			name = path.Substring(0, index);
		index = name.LastIndexOf('/');
		if(index != -1)
			name = name.Substring(index + 1);	
		if(!dataTable.ContainsKey(name))
			dataTable.Add(name, csv.listTable);
		if(name == "moviedialogue")
		{
			CreateMovie();
		}
	}
	
	public void FinishLoad(string path, SDCSV csv)
	{
		initCount--;
		
		string name = "";
		int index = path.LastIndexOf('.');
		if (index != -1)
		{
			name = path.Substring(0, index);
		}
		index = name.LastIndexOf('/');
		if (index != -1)
		{
			name = name.Substring(index + 1);	
		}
		
		if (!dataTable.ContainsKey(name))
		{
			dataTable.Add(name, csv.csvTable);
		}

//		if (fileTable.Count > 0) 
//		{
//			string file = fileTable[0];
//			fileTable.RemoveAt(0);
//			csv.LoadCSV(file, FinishLoad);
//		}
		
		if (name == "startequip")
		{
//			CreateStartEquip();		//< 初始装备aa
		}
		else if (name == "hair")
		{
//			CreateHairInfo();		//< 头发样式信息aa
		}
		else if(name ==	"skillaction")
		{
			CreateSkillAction();	//< 主角技能表aa
		}
		else if(name == "skillinfo")
		{
			CreateSkillInfo();		//< 主角技能表aa
		}
		else if(name == "skilleffect")
		{
			CreateSkillEffect();
		}
		else if(name ==	"buff")
		{
			CreateBuff();
		}
		else if(name ==	"buffeffect")
		{
			CreateBuffeffect(dataTable["buffeffect"] as  Hashtable);
		}
		else if(name == "buffaction")
		{
			CreateBuffAction();
		}
		else if(name == "group")
		{
			CreateGroup();
		}
		else if(name == "summonobject")
		{
			CreateSummon();
		}
		else if (name == "rolelevels")
		{
			CreateRoleLevel();
		}
		else if(name == "dmdscreatureconfig.monstertemplates")
		{
			CreateMonsterTemplate();		//< 怪物属性表aa
		}
		else if(name ==	"monster_skillinfo")
		{
			CreateMonsterSkillInfo();		//< 怪物技能表aa
		}
		else if(name ==	"monster_skillaction")
		{
			CreateMonsterSkillAction();		//< 怪物动作表aa
		}
		else if(name == "dmdscreatureconfig.pettemplates")
		{
			CreatePetTemplate();			//< 宠物属性表aa
		}
		else if (name == "dmdspetexconfig.petgroups")
		{
			CreatePetGroupsTable();			//< 宠物组合表aa
		}
		else if (name == "dmdssapconfig.saps")
		{
			CreatePetSapsTable();			//< 宠物组合效果表aa
		}
		else if(name == "PropTransfer")
		{
			CreatePropTrans();
		}
		else if (name == "dmdscreatureconfig.itemtemplates")
		{
			CreateItemInfo();	
		}
		else if(name == "dropmodel")
		{
			CreateDropModel();
		}
//		else if (name == "guide")
//		{
//			sdGuideMgr.Instance.Init();	
//		}
		else if (name == "dmdscreatureconfig.itemups")
		{
			foreach(DictionaryEntry item in csv.csvTable)
			{
				Hashtable info = item.Value as Hashtable;
				int key = int.Parse(info["UPQuility"].ToString());
				if (itemUpTable.ContainsKey(key))
				{
					List<Hashtable> list = itemUpTable[key];
					list.Add(info);
				}
				else
				{
					List<Hashtable> list = new List<Hashtable>();
					list.Add(info);
					itemUpTable[key] = list;
				}
			}
		}
		else if(name == "dmdsgiftconfig.gift_levels")
		{
			LoadUpgradeAwardDB();
		}
		else if(name == "dmdsgiftconfig.gift_signs")
		{
			LoadDailyAwardDB();
		}
		else if(name == "dmdsgiftconfig.gift_dayquests")
		{
			LoadDailyQuestDB();
		}
		else if(name == "dmdsgiftconfig.gift_dayquestboxs")
		{
			LoadDailyQuestBoxDB();
		}
		else if (name == "dmdssuitconfig.suits")
		{
			if (dataTable.ContainsKey("dmdscreatureconfig.itemtemplates"))
			{
				Hashtable suitTable = dataTable["dmdssuitconfig.suits"] as Hashtable;
				foreach(DictionaryEntry info in suitTable)
				{
					Hashtable item = info.Value as Hashtable;
					for (int i = 1; i <= 5; ++i)
					{
						string str = string.Format("Item{0}.ItemID", i);
						string id = item[str].ToString();
						if (id == "0") continue;
						Hashtable t = (Hashtable)dataTable["dmdscreatureconfig.itemtemplates"];
						Hashtable item1 = (Hashtable)(t[id]);
						if(item1!=null)
						{
							item1["SuitID"] = info.Key.ToString();
						}
						else
						{
							if(Application.isEditor)
							{
								Debug.LogError("Suit Item doesn't exist!"+id);
							}
						}
					}
				}
			}
		}
		else if(name == "RandomName")
		{
			LoadRandomName();
		}
		else if(name == "vip_client")
		{
			LoadVipUpgradeDB();
		}
        else if (name == "operation")
        {
            CreateEffectOperation();
        }
		else if(name == "dmdsrmbconfig.pay")
		{
			LoadRmbChargeDB();
		}
		else if(name == "dmdsshopconfig.shop_actionpoints")
		{
			LoadActionPointDB();
		}
	}

	void LoadActionPointDB()
	{
		if(dataTable["dmdsshopconfig.shop_actionpoints"] != null)
		{	
			Hashtable table = (Hashtable)dataTable["dmdsshopconfig.shop_actionpoints"];
			foreach(DictionaryEntry item in table)
			{
				ActionPoint actionPoint = new ActionPoint();
				ParseAllMember(actionPoint, item.Value as Hashtable);
				m_actionPointDB[actionPoint.SpiritNum] = actionPoint;
			}
		}
	}

	void LoadRmbChargeDB()
	{
		if(dataTable["dmdsrmbconfig.pay"] != null)
		{
			Hashtable table = (Hashtable)dataTable["dmdsrmbconfig.pay"]; 
			foreach(DictionaryEntry item in table)
			{
				RmbCharge rmbCharge = new RmbCharge();
				ParseAllMember(rmbCharge, item.Value as Hashtable);
				m_rmbChargeDB[rmbCharge.UId] = rmbCharge;
			}
		}

	}

	void LoadMonthCardDB()
	{

	}

	void LoadVipUpgradeDB()
	{
		if(dataTable["vip_client"] != null)
		{
			Hashtable table = (Hashtable)dataTable["vip_client"];
			foreach(DictionaryEntry item in table)
			{
				VipUpgrade vipUpgrade = new VipUpgrade();
				ParseAllMember(vipUpgrade, item.Value as Hashtable);
				m_vipUpgradeDB[vipUpgrade.Viplevel] = vipUpgrade;
			}
		}
	}

	void LoadRandomName()
	{
		if(dataTable["RandomName"] != null)
		{
			Hashtable table = (Hashtable)dataTable["RandomName"];
			foreach(DictionaryEntry item in table)
			{
				RandomName randomName = new RandomName();
				ParseAllMember(randomName, item.Value as Hashtable);
				m_randomNameDB[randomName.Id] = randomName;
			}
		}
	}

	void LoadDailyQuestBoxDB()
	{
		if (dataTable["dmdsgiftconfig.gift_dayquestboxs"] != null)
		{
			Hashtable table = (Hashtable)dataTable["dmdsgiftconfig.gift_dayquestboxs"];	
			foreach (DictionaryEntry item in table)
			{
				AwardBox awardBox = new AwardBox();
				ParseAllMember(awardBox, item.Value as Hashtable);
				m_awardBoxDB[awardBox.BoxID] = awardBox;
			}
		}
	}

	void LoadDailyQuestDB()
	{
		if (dataTable["dmdsgiftconfig.gift_dayquests"] != null)
		{
			Hashtable table = (Hashtable)dataTable["dmdsgiftconfig.gift_dayquests"];	
			foreach (DictionaryEntry item in table)
			{
				DailyQuest dailyQuest = new DailyQuest();
				ParseAllMember(dailyQuest, item.Value as Hashtable);
				m_dailyQuestDB[dailyQuest.DayQuestType] = dailyQuest;
			}
		}

	}

	void LoadDailyAwardDB()
	{
		if(dataTable["dmdsgiftconfig.gift_signs"] != null)
		{
			Hashtable table = (Hashtable)dataTable["dmdsgiftconfig.gift_signs"];
			foreach(DictionaryEntry item in table)
			{
				DailyAward dailyAward = new DailyAward();
				ParseAllMember(dailyAward, item.Value as Hashtable);
				m_dailyAwardDB[dailyAward.Index] = dailyAward;
			}
		}
	}

	void LoadUpgradeAwardDB()
	{
		if(dataTable["dmdsgiftconfig.gift_levels"] != null)
		{
			Hashtable table = (Hashtable)dataTable["dmdsgiftconfig.gift_levels"];
			foreach(DictionaryEntry item in table)
			{
				UpgradeAward upgradeAward = new UpgradeAward();
				ParseAllMember(upgradeAward, item.Value as Hashtable);
				m_upgradeAwardDB[upgradeAward.Level] = upgradeAward;
			}
		}
	}

	void CreateMovie()
	{
		List<Hashtable> list = (List<Hashtable>)dataTable["moviedialogue"];
		if(list != null && list.Count > 0)
		{
			Hashtable first = list[0];
			int nStage = int.Parse(first["StageId"] as string);
			List<stMovie> movieList = new List<stMovie>();
			List<string> characterName = new List<string>();
			for(int index = 0; index < list.Count; ++index)
			{
				Hashtable table = list[index];
				int stageid = int.Parse(table["StageId"] as string);
				if(stageid  != nStage)
				{
					stStageMovie  stagemovie = new stStageMovie();
					stagemovie.movieList = movieList;
					movieList = new List<stMovie>();
					stagemovie.nCharacter = characterName.Count;
					m_Movie.Add(nStage, stagemovie);
					movieList.Clear();
					characterName.Clear();
					nStage = stageid;
				}

				stMovie movieData= new stMovie();				
				movieData.stageID = int.Parse(table["StageId"] as string);
				movieData.npcName = table["NpcName"] as string;
				movieData.npcModel = table["NpcModel"] as string;
				movieData.dialogueIndex = int.Parse(table["DialogueSequence"] as string);
				movieData.EndFlag = int.Parse(table["DialogueEnd"] as string);
				movieData.content = table["DialogueContent"] as string;
				movieData.portrait = table["portrait"] as string;
				movieData.npcNoShow = int.Parse(table["NpcNoShow"] as string);
				movieData.npcShow = int.Parse(table["NpcShow"] as string);
				bool bFound = false;
				for(int j = 0; j < characterName.Count; ++j)
				{
					string strName = characterName[j];
					if(strName == movieData.npcName)
					{
						bFound = true;
						break;
					}
				}
				if(!bFound)
					characterName.Add(movieData.npcName);
				movieList.Add(movieData);
			}
			stStageMovie  stagemovie2 = new stStageMovie();
			stagemovie2.movieList = movieList;
			stagemovie2.nCharacter = characterName.Count;
			m_Movie.Add(nStage, stagemovie2);
		}
	}
		
	void CreateItemInfo()
	{
		Hashtable kTable = (Hashtable)dataTable["dmdscreatureconfig.itemtemplates"];
		if (kTable != null)
		{
			Hashtable kProperty = new Hashtable();
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				Hashtable subTable = new Hashtable();
				
				foreach(DictionaryEntry de2 in valTable)
				{
					string key2 = (string)de2.Key;
					string val = (string)de2.Value;
					key2 = key2.Replace("Property.","");
					subTable.Add(key2,val);
				}
				
				key1 = key1.Replace("Property.","");
				kProperty.Add(key1, subTable);
			}
			
			dataTable["dmdscreatureconfig.itemtemplates"] = kProperty;
		}

		if (dataTable.ContainsKey("dmdssuitconfig.suits"))
		{
			Hashtable suitTable = dataTable["dmdssuitconfig.suits"] as Hashtable;
			foreach(DictionaryEntry info in suitTable)
			{
				Hashtable item = info.Value as Hashtable;
				for (int i = 1; i <= 5; ++i)
				{
					string name = string.Format("Item{0}.ItemID", i);
					string id = item[name].ToString();
					((Hashtable)(((Hashtable)dataTable["dmdscreatureconfig.itemtemplates"])[id]))["SuitID"] = info.Key.ToString();
				}
			}
		}
	}
	void CreateDropModel()
	{
		Hashtable kTable = (Hashtable)dataTable["dropmodel"];
		if (kTable != null)
		{
			Hashtable newTable = new Hashtable();
			foreach (DictionaryEntry de in kTable)
			{
				Hashtable valTable = (Hashtable)de.Value;
				int iclass			=	int.Parse(valTable["class"] as string);
				int isubclass		=	int.Parse(valTable["subclass"] as string);
				string dropitem		=	valTable["dropitem"] as string;
				int id = iclass*100+isubclass;
				newTable.Add(id, dropitem);
			}
			
			dataTable["dropmodel"] = newTable;
		}
	}
	
	public string GetItemQuilityName(int quility)
	{
		string str = "";
		if (quility == (int)Item_Quility.Grey)
		{
			str = GetShowStr("Quility_Grey");
		}
		else if (quility == (int)Item_Quility.White)
		{
			str = GetShowStr("Quility_White");
		}
		else if (quility == (int)Item_Quility.Green)
		{
			str = GetShowStr("Quility_Green");
		}
		else if (quility == (int)Item_Quility.Blue)
		{
			str = GetShowStr("Quility_Blue");
		}
		else if (quility == (int)Item_Quility.Purple)
		{
			str = GetShowStr("Quility_Purple");
		}
		else if (quility == (int)Item_Quility.Gold)
		{
			str = GetShowStr("Quility_Gold");
		}
		else
		{
			str = GetShowStr("Quility_White");
		}
		
		return str;
	}

    public string GetGemEquipPosName(string pos)
    {
        int nPos = int.Parse(pos);

        string ret = "";
        int[] equipPos = new int[] { nPos };
        BitArray ary = new BitArray(equipPos);
        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Weapon])
        {
            ret += GetShowStr("Weapon");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Helmet])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Hat");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Clothing])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Coat");
        }
        
        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Gloves])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Gloves");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Trousers])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Pants");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Mantle])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Cloak");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Ring])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Ring");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Adron])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Ornaments");
        }

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_Necklace])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("Equip_Necklace");
        }          

        if (ary[(int)HeaderProto.ERoleItemEquipSlot.RoleItemEquipSlot_SecondWeapon])
        {
            if (ret != "") ret += ",";
            ret += GetShowStr("OffHand");
        }       

        return ret;
    }
	
	public string GetItemClassName(string strClass, string strSubClass)
	{
		int classIndex = int.Parse(strClass);
		int subClass = int.Parse(strSubClass);
		
		if (classIndex == 0)
		{
			if(subClass == 0)
			{
				return GetShowStr("Equip_Sword");	
			}
			else if (subClass == 1)
			{
				return GetShowStr("Equip_Staff");
			}
			else if (subClass == 2)
			{
				return GetShowStr("Equip_Bow");
			}
			else if (subClass == 3)
			{
				return GetShowStr("Equip_Hammer");
			}
			else if (subClass == 4)
			{
				return GetShowStr("Equip_FightShield");
			}
			else if (subClass == 5)
			{
				return GetShowStr("Equip_ProtectShield");
			}
			else if (subClass == 6)
			{
				return GetShowStr("Equip_ProtectAdron");
			}
			else if (subClass == 7)
			{
				return GetShowStr("Equip_BowBag");
			}
		}
		else if (classIndex == 1)
		{
			if(subClass == 0)
			{
				return GetShowStr("Equip_Hat");	
			}
			else if (subClass == 1)
			{
				return GetShowStr("Equip_Coat");
			}
			else if (subClass == 2)
			{
				return GetShowStr("Equip_Gloves");
			}
			else if (subClass == 3)
			{
				return GetShowStr("Equip_Pants");
			}
			else if (subClass == 4)
			{
				return GetShowStr("Equip_Cloak");
			}
		}
		else if (classIndex == 2)
		{
			if(subClass == 0)
			{
				return GetShowStr("Equip_Ring");	
			}
			else if (subClass == 1)
			{
				return GetShowStr("Equip_Ornaments");
			}
			else if (subClass == 2)
			{
				return GetShowStr("Equip_Necklace");
			}	
		}
		
		return "";
	}
	public Hashtable GetItemById(string id)
	{
		Hashtable table = GetItemTable();
		if (table != null) return (Hashtable)table[id];
		return null;
	}
	public Hashtable GetItemTable()
	{
		if (dataTable["dmdscreatureconfig.itemtemplates"] != null) 
		{
			return (Hashtable)dataTable["dmdscreatureconfig.itemtemplates"];
		}
		return null;
	}

	static int sortItemUp(Hashtable info, Hashtable compare)
	{
		int index = int.Parse(info["Index"].ToString());
		int index2 = int.Parse(compare["Index"].ToString());
		if (index > index2)
		{
			return 1;
		}
		else if (index < index2)
		{
			return -1;
		}
		else
		{
			return 0;
		}
	}

	public Hashtable GetItemUp(string id, int level)
	{
		Hashtable item = GetItemById(id);
		if (itemUpTable.ContainsKey(int.Parse(item["Quility"].ToString())))
		{
			List<Hashtable> info = itemUpTable[int.Parse(item["Quility"].ToString())] as List<Hashtable>;
			info.Sort(sortItemUp);
			if (level >= info.Count) return null;
			return info[level];
		}
		return null;
	}

	public int GetMaxItemUp(string id)
	{
		Hashtable item = GetItemById(id);
		if (itemUpTable.ContainsKey(int.Parse(item["Quility"].ToString())))
		{
			List<Hashtable> info = itemUpTable[int.Parse(item["Quility"].ToString())] as List<Hashtable>;
			return info.Count-1;
		}
		return 0;
	}

	public	Hashtable	GetTable(string str)
	{
		return dataTable[str] as Hashtable;
	}

	public List<Hashtable> GetList(string str)
	{
		return dataTable[str] as List<Hashtable>;
	}
	
	public Hashtable GetStartEquipTable()
	{
		if (dataTable["startequip"] != null) 
		{
			return (Hashtable)dataTable["startequip"];
		}
		return null;	
	}
	
	public Hashtable GetHairTable()
	{
		if (dataTable["hair"] != null) 
		{
			return (Hashtable)dataTable["hair"];
		}
		return null;
	}
	
	// 宠物基础属性表aa
	public Hashtable GetPetTemplateTable()
	{
		if (dataTable["dmdscreatureconfig.pettemplates"] != null) 
		{
			return (Hashtable)dataTable["dmdscreatureconfig.pettemplates"];
		}
		return null;
	}
	
	public Hashtable GetPetTemplateTableZS()
	{
		if (dataTable["PetTableZS"] != null) 
		{
			return (Hashtable)dataTable["PetTableZS"];
		}
		return null;
	}
	
	public Hashtable GetPetTemplateTableFS()
	{
		if (dataTable["PetTableFS"] != null) 
		{
			return (Hashtable)dataTable["PetTableFS"];
		}
		return null;
	}
	
	public Hashtable GetPetTemplateTableYX()
	{
		if (dataTable["PetTableYX"] != null) 
		{
			return (Hashtable)dataTable["PetTableYX"];
		}
		return null;
	}
	
	public Hashtable GetPetTemplateTableMS()
	{
		if (dataTable["PetTableMS"] != null) 
		{
			return (Hashtable)dataTable["PetTableMS"];
		}
		return null;
	}
	
	public string GetPetTemplateValueByStringKey(int iTemplateID, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetPetTemplateTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable petInfo = (Hashtable)item.Value;
				int id = int.Parse(petInfo["TemplateID"] as string);
				if(id == iTemplateID)
				{
					strResult = petInfo[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}
	
	public Hashtable GetPetTemplate(string templateID)
	{
		Hashtable table = GetPetTemplateTable();
		if (table != null) return (Hashtable)table[templateID];
		return null;
	}
	
	//宠物强化系数表..
	public Hashtable GetPetUpsTable()
	{
		if (dataTable["dmdspetexconfig.petups"] != null) 
		{
			return (Hashtable)dataTable["dmdspetexconfig.petups"];
		}
		return null;
	}
	
	public string GetPetUpsValueByStringKey(int iUPID, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetPetUpsTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable petUp = (Hashtable)item.Value;
				int id = int.Parse(petUp["UPID"] as string);
				if(id == iUPID)
				{
					strResult = petUp[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}
	
	public Hashtable GetPetUp(string strUPID)
	{
		Hashtable table = GetPetUpsTable();
		if (table != null) return (Hashtable)table[strUPID];
		return null;
	}
	
	//宠物升级经验表..
	public Hashtable GetPetLevelsTable()
	{
		if (dataTable["dmdspetexconfig.petlevels"] != null) 
		{
			return (Hashtable)dataTable["dmdspetexconfig.petlevels"];
		}
		return null;
	}
	
	public string GetPetLevelsValueByStringKey(int iIndex, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetPetLevelsTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable petLevel = (Hashtable)item.Value;
				int id = int.Parse(petLevel["Index"] as string);
				if(id == iIndex)
				{
					strResult = petLevel[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}
	
	public Hashtable GetPetLevel(string strIndex)
	{
		Hashtable table = GetPetLevelsTable();
		if (table != null) return (Hashtable)table[strIndex];
		return null;
	}
	
	// 宠物组合表aa
	public Hashtable GetPetGroupsTable()
	{
		if (dataTable["dmdspetexconfig.petgroups"] != null) 
		{
			return (Hashtable)dataTable["dmdspetexconfig.petgroups"];
		}
		return null;
	}

	// 预解析的宠物组合表aa
	protected Hashtable mParsedPetGroupsTable = null;
	public Hashtable ParsedPetGroupsTable
	{
		get { return mParsedPetGroupsTable; }
	}

	// 预解析宠物组合表aa
	protected void CreatePetGroupsTable()
	{
		Hashtable kTable = (Hashtable)dataTable["dmdspetexconfig.petgroups"];
		if (kTable != null)
		{
			Hashtable kParsedTable = new Hashtable();
			foreach (DictionaryEntry kEntry in kTable)
			{
				string kKey = (string)kEntry.Key;
				Hashtable kSubTable = (Hashtable)kEntry.Value;
				
				Hashtable kParsedSubTable = new Hashtable();
				kParsedSubTable["GROUPID"] = int.Parse(kSubTable["GROUPID"] as string);
				kParsedSubTable["SapID"] = int.Parse(kSubTable["SapID"] as string);
				kParsedSubTable["HasRole"] = !(bool)(int.Parse(kSubTable["HasRole"] as string) == 0);
				kParsedSubTable["HasPet"] = !(bool)(int.Parse(kSubTable["HasPet"] as string) == 0);
				kParsedSubTable["Data1.PetID"] = int.Parse(kSubTable["Data1.PetID"] as string);
				kParsedSubTable["Data2.PetID"] = int.Parse(kSubTable["Data2.PetID"] as string);
				kParsedSubTable["Data3.PetID"] = int.Parse(kSubTable["Data3.PetID"] as string);
				kParsedSubTable["Data4.PetID"] = int.Parse(kSubTable["Data4.PetID"] as string);

				kParsedTable.Add(int.Parse(kKey), kParsedSubTable);
			}

			mParsedPetGroupsTable = kParsedTable;	//< 保存解析后的表aa
		}
	}
	
	//
	public string GetPetGroupsValueByStringKey(int iKey, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetPetGroupsTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable petGroup = (Hashtable)item.Value;
				int id = int.Parse(petGroup["GROUPID"] as string);
				if(id == iKey)
				{
					strResult = petGroup[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}
	
	// 宠物组合效果表aa
	public Hashtable GetPetSapsTable()
	{
		if (dataTable["dmdssapconfig.saps"] != null) 
		{
			return (Hashtable)dataTable["dmdssapconfig.saps"];
		}
		return null;
	}

	// 预解析的宠物组合效果表aa
	protected Hashtable mParsedPetSapsTable = null;
	public Hashtable ParsedPetSapsTable
	{
		get { return mParsedPetSapsTable; }
	}

	// 预解析宠物组合表aa
	protected void CreatePetSapsTable()
	{
		Hashtable kTable = (Hashtable)dataTable["dmdssapconfig.saps"];
		if (kTable != null)
		{
			Hashtable kParsedTable = new Hashtable();
			foreach (DictionaryEntry kEntry in kTable)
			{
				string kKey = (string)kEntry.Key;
				Hashtable kSubTable = (Hashtable)kEntry.Value;

				Hashtable kParsedSubTable = new Hashtable();
				kParsedSubTable["ID"] = int.Parse(kSubTable["ID"] as string);
				kParsedSubTable["Desc"] = kSubTable["Desc"] as string;
				kParsedSubTable["Skill1.SkillID"] = int.Parse(kSubTable["Skill1.SkillID"] as string);
				kParsedSubTable["Skill1.Desc"] = kSubTable["Skill1.Desc"] as string;
				kParsedSubTable["Skill2.SkillID"] = int.Parse(kSubTable["Skill2.SkillID"] as string);
				kParsedSubTable["Skill2.Desc"] = kSubTable["Skill2.Desc"] as string;
				kParsedSubTable["Skill3.SkillID"] = int.Parse(kSubTable["Skill3.SkillID"] as string);
				kParsedSubTable["Skill3.Desc"] = kSubTable["Skill3.Desc"] as string;
				kParsedSubTable["Skill4.SkillID"] = int.Parse(kSubTable["Skill4.SkillID"] as string);
				kParsedSubTable["Skill4.Desc"] = kSubTable["Skill4.Desc"] as string;
				kParsedSubTable["Skill5.SkillID"] = int.Parse(kSubTable["Skill5.SkillID"] as string);
				kParsedSubTable["Skill5.Desc"] = kSubTable["Skill5.Desc"] as string;
				kParsedSubTable["Skill6.SkillID"] = int.Parse(kSubTable["Skill6.SkillID"] as string);
				kParsedSubTable["Skill6.Desc"] = kSubTable["Skill6.Desc"] as string;

				kParsedTable.Add(int.Parse(kKey), kParsedSubTable);
			}

			mParsedPetSapsTable = kParsedTable;	//< 保存解析后的表aa
		}
	}
	
	//
	public string GetPetSapsValueByStringKey(int iKey, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetPetSapsTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable petSap = (Hashtable)item.Value;
				int id = int.Parse(petSap["ID"] as string);
				if(id == iKey)
				{
					strResult = petSap[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}

	//商城配置
	public Hashtable GetShopsTable()
	{
		if (dataTable ["dmdsshopconfig.shops"] != null) 
		{
			return (Hashtable)dataTable["dmdsshopconfig.shops"];
		}
		return null;
	}

	public string GetShopsValueByStringKey(int iGoodsID, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetShopsTable ();
		if (table != null) 
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable shop = (Hashtable)item.Value;
				int id = int.Parse(shop["GoodsID"] as string);
				if(id == iGoodsID)
				{
					strResult = shop[stringKey] as string;
					break;
				}
			}
		}
		return strResult;
	}

	public Hashtable GetShopProduct(string sGoodID)
	{
		Hashtable table = GetShopsTable ();
		if (table != null)
			return (Hashtable)table [sGoodID];
		return null;
	}

	public Hashtable GetShopPetsTable()
	{
		if (dataTable ["dmdsshopconfig.shop_pets"] != null) 
		{
			return (Hashtable)dataTable["dmdsshopconfig.shop_pets"];
		}
		return null;
	}

	public string GetShopPetsValueByStringKey (int iClassID, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetShopsTable ();
		if (table != null) 
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable shop = (Hashtable)item.Value;
				int id = int.Parse(shop["ClassID"] as string);
				if(id == iClassID)
				{
					strResult = shop[stringKey] as string;
					break;
				}
			}
		}
		return strResult;
	}

	public Hashtable GetShopPets(string sClassID)
	{
		Hashtable table = GetShopPetsTable();
		if (table != null)
			return (Hashtable)table [sClassID];
		return null;
	}

	public Hashtable GetShopActionPointsTable()
	{
		if (dataTable ["dmdsshopconfig.shop_actionpoints"] != null) 
		{
			return (Hashtable)dataTable["dmdsshopconfig.shop_actionpoints"];
		}
		return null;
	}

	public string GetShopActionPointsValueByStringKey(int iSpiritNum, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetShopsTable ();
		if (table != null) 
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable shop = (Hashtable)item.Value;
				int id = int.Parse(shop["SpiritNum"] as string);
				if(id == iSpiritNum)
				{
					strResult = shop[stringKey] as string;
					break;
				}
			}
		}
		return strResult;
	}

	public Hashtable GetShopActionPoints (string sSpiritNum)
	{
		Hashtable table = GetShopActionPointsTable();
		if (table != null)
			return (Hashtable)table [sSpiritNum];
		return null;
	}
	//商城配置结束

	//舔怪配置..
	public Hashtable GetLapBossTemplateTable()
	{
		if (dataTable["dmdsxactivitytemplateconfig.abysstemplates"] != null) 
		{
			return (Hashtable)dataTable["dmdsxactivitytemplateconfig.abysstemplates"];
		}
		return null;
	}

	public Hashtable GetLapBossTemplate(string templateID)
	{
		Hashtable table = GetLapBossTemplateTable();
		if (table != null) return (Hashtable)table[templateID];
		return null;
	}
	
	public string GetLapBossTemplateValueByStringKey(int iKey, string stringKey)
	{
		string strResult = "";
		Hashtable table = GetLapBossTemplateTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable bossValue = (Hashtable)item.Value;
				int id = int.Parse(bossValue["TemplateID"] as string);
				if(id == iKey)
				{
					strResult = bossValue[stringKey] as string;
					break;
				}
			}
		}
		
		return strResult;
	}
	//舔怪配置结束..

	//邮件配置..
	public Hashtable GetMailTable()
	{
		if (dataTable["mailhead"] != null) 
		{
			return (Hashtable)dataTable["mailhead"];
		}
		return null;
	}

	public string GetMailHeadTex(string headName)
	{
		string strResult = "";
		Hashtable table = GetMailTable();
		if (table != null)
		{
			foreach(DictionaryEntry item in table)
			{
				Hashtable headValue = (Hashtable)item.Value;
				string strName = headValue["headname"].ToString();
				if(strName == headName)
				{
					strResult = headValue["headtex"].ToString();
					break;
				}
			}
		}

		return strResult;
	}
	//邮件配置结束..

	//世界BOSS配置..
	public Hashtable GetWorldBossParamTable()
	{
		if (dataTable["dmdsworldbossconfig.worldbossparam"] != null) 
		{
			return (Hashtable)dataTable["dmdsworldbossconfig.worldbossparam"];
		}
		return null;
	}

	public Hashtable GetWorldBossTemplatesTable()
	{
		if (dataTable["dmdsworldbossconfig.worldbosstemplates"] != null) 
		{
			return (Hashtable)dataTable["dmdsworldbossconfig.worldbosstemplates"];
		}
		return null;
	}
	//世界BOSS配置结束..
	
	public string GetJobNameByIndex(string index)
	{
		int id = int.Parse(index);

		if (id == (int)EquipJob.AllJob)
		{
			return GetShowStr(EquipJob.AllJob.ToString());
		}
		
		string name = "";
		
		foreach(string item in Enum.GetNames(typeof(EquipJob)))
		{
			EquipJob temp = (EquipJob)Enum.Parse(typeof(EquipJob), item);
			if ((id & (int)temp) > 0)
			{
				name += GetShowStr(item);	
				name += " ";
			}
		}
		
		return name;
	}
	
	public string GetJobName(string index)
	{
		int id = int.Parse(index);

		if (id == (int)RoleJob.AllJob)
		{
			return GetShowStr(RoleJob.AllJob.ToString());
		}
		
		string name = "";
		
		foreach(string item in Enum.GetNames(typeof(RoleJob)))
		{
			RoleJob temp = (RoleJob)Enum.Parse(typeof(RoleJob), item);
			if (id == (int)temp)
			{
				name = GetShowStr(item);
				break;
			}
		}
		
		return name;
	}

    public string GetItemQuilityBorder(int quility)
    {
        string border = "IconFrame";
        return (border + quility.ToString());
    }

	public string GetPetQuilityBorder(int quility)
	{
		if( quility == 1 )		return "IconL2w";
		else if( quility == 2 )	return "IconL2g";
		else if( quility == 3 )	return "IconL2b";
		else if( quility == 4 )	return "IconL2p";
		else if( quility == 5 )	return "IconL2y";
		else return "";
	}
	
	public Color GetItemQuilityColor(int quility)
	{
		Color color;
		if (quility == (int)Item_Quility.Grey)
		{
			color = Color.grey;	
		}
		else if (quility == (int)Item_Quility.White)
		{
			color = new Color(255f/255f, 255f/255f, 255f/255f, 1f);
		}
		else if (quility == (int)Item_Quility.Green)
		{
			color = new Color(45f/255f, 210f/255f, 18f/255f, 1f);
		}
		else if (quility == (int)Item_Quility.Blue)
		{
			color = new Color(0f, 144f/255f, 255f/255f, 1f);
		}
		else if (quility == (int)Item_Quility.Purple)
		{
			color = new Color(164f/255f, 84f/255f, 254f/255f, 1f);
		}
		else if (quility == (int)Item_Quility.Gold)
		{
			color = new Color(255f/255f, 179f/255f, 15f/255f, 1f);
		}
		else
		{
			color = Color.white;
		}
		
		return color;
	}
	
	public Hashtable GetProperty(string id)
	{
		Hashtable item = GetItemById(id);
		Hashtable info = new Hashtable();
		if (item == null) return info;
		foreach(string property in Enum.GetNames(typeof(PropertyIndex)))
		{
			if (item[property] != null && item[property].ToString() != "" && item[property].ToString() != "0")
			{
				info.Add(GetShowStr(property), item[property]);
			}
		}
		
		return info;
	}
	
    public int GetItemScore(ulong instanceId)
    {
        sdGameItem item = sdGameItemMgr.Instance.getItem(instanceId);
        if (item == null) return 0;
        
        return GetItemScore(item.templateID.ToString(), item.upLevel);
    }

	public int GetItemScore(string id, int upLv)
	{
		Hashtable scoreTable = dataTable["score"] as Hashtable;
		if(scoreTable == null) return 0;
		Hashtable item = GetItemById(id);
		Int64 point = 0;
        string mainAtt = item["StringExtra3"].ToString();
		foreach(string property in Enum.GetNames(typeof(PropertyIndex)))
		{
			if (item[property] != null && item[property].ToString() != "")
			{
				if (scoreTable["1"] != null)
				{
					Hashtable score = scoreTable["1"] as Hashtable;
					if (score[property] != null && score[property].ToString() != "")
					{
                        if (property == mainAtt && upLv > 0)
                        {
                            Hashtable curInfo = sdConfDataMgr.Instance().GetItemUp(id, upLv);
                            if (curInfo != null)
                            {
                                Int64 rate = int.Parse(curInfo["UpMainTypeCoe"].ToString());
                                Int64 value = int.Parse(item[property].ToString());
                                Int64 curValue = value + (value * rate) / 10000;
                                point += (Int64.Parse(score[property].ToString()) * curValue)/10000;
                            }
                        }
                        else
                        {
                            point += (Int64.Parse(score[property].ToString()) * Int64.Parse(item[property].ToString()))/10000;
                        }
					}					
				}
			}
		}

		return (int)point;
	}
	
	public int PlayerScore()
	{
		sdMainChar mainchar = sdGameLevel.instance.mainChar;
        Int64 point = 0;
		if (mainchar != null)
		{
			Hashtable scoreTable = dataTable["score"] as Hashtable;
			if(scoreTable == null) return 0;
			foreach(string property in Enum.GetNames(typeof(PropertyIndex)))
			{
				if (mainchar[property] != null && mainchar[property].ToString() != "")
				{
					if (scoreTable["1"] != null)
					{
						Hashtable score = scoreTable["1"] as Hashtable;
						if (score[property] != null && score[property].ToString() != "")
						{
                            point += (Int64.Parse(score[property].ToString()) * Int64.Parse(mainchar[property].ToString()))/10000;
						}					
					}
				}
			}
		}

		return (int)point;
	}

	public int GetPetScore(UInt64 uuPetDBID)
	{
		if (uuPetDBID==UInt64.MaxValue)
			return 0;

		int point = 0;
		Hashtable petProp = sdNewPetMgr.Instance.GetPetPropertyFromDBID(uuPetDBID);
		if (petProp!=null)
		{
			Hashtable scoreTable = dataTable["score"] as Hashtable;
			if(scoreTable == null) return 0;
			foreach(string property in Enum.GetNames(typeof(PropertyIndex)))
			{
				if (petProp[property] != null && petProp[property].ToString() != "")
				{
					if (scoreTable["1"] != null)
					{
						Hashtable score = scoreTable["1"] as Hashtable;
						if (score[property] != null && score[property].ToString() != "")
						{
							point += int.Parse(score[property].ToString())*int.Parse(petProp[property].ToString());
						}					
					}
				}
			}
		}

		point = point/10000;
		return point;
	}

	public int GetPetScoreByTemplateID(int iID, int iUp, int iLevel)
	{
		if (iID<=0)
			return 0;
		
		int point = 0;
		Hashtable petProp = sdNewPetMgr.Instance.GetPetPropInfoByTemplateID(iID, iUp, iLevel);
		if (petProp!=null)
		{
			Hashtable scoreTable = dataTable["score"] as Hashtable;
			if(scoreTable == null) return 0;
			foreach(string property in Enum.GetNames(typeof(PropertyIndex)))
			{
				if (petProp[property] != null && petProp[property].ToString() != "")
				{
					if (scoreTable["1"] != null)
					{
						Hashtable score = scoreTable["1"] as Hashtable;
						if (score[property] != null && score[property].ToString() != "")
						{
							point += int.Parse(score[property].ToString())*int.Parse(petProp[property].ToString());
						}					
					}
				}
			}
		}

		point = point/10000;
		return point;
	}

	public Hashtable GetSkillList()
	{
		if (dataTable["skillinfo"] != null) 
		{
			return (Hashtable)dataTable["skillinfo"];
		}
		return null;
	}
	
	public Hashtable GetSkill(string id)
	{
		Hashtable table = GetSkillList();
		if (table != null) return (Hashtable)table[id];
		return null;
	}
	
	public Hashtable GetPassiveSkill(string id)
	{
		Hashtable list = new Hashtable();
        if (dataTable["skillinfo"] != null) 
		{
			Hashtable table = (Hashtable)dataTable["skillinfo"];
            if (table.ContainsKey(id))
            {
                Hashtable parent = table[id] as Hashtable;
                string tid = parent["dwTemplateID"].ToString();
                foreach (DictionaryEntry item in table)
                {
                    Hashtable skill = item.Value as Hashtable;
                    if (skill["byIsPassive"].ToString() == "1" &&
                        skill["parentid"].ToString() == id && skill["dwClassID"].ToString() == skill["dwTemplateID"].ToString())
                    {
                        list.Add(skill["dwClassID"], skill);
                    }
                }
            }
		}	
		
		return list;
	}
	
	public string GetShowStr(string str)
	{
		if (dataTable["stringtable"] != null)
		{
			Hashtable table = (Hashtable)dataTable["stringtable"];
			if (table[str] != null)
			{
				Hashtable info = (Hashtable)table[str];
				if (info[lan] != null)
				{
					return info[lan].ToString();
				}
			}
		}
		else
		{
			if( str == "QuitGameConfirm" )	return "确认要退出游戏吗？";
			else if( str == "Confirm" )		return "确定";
			else if( str == "Cancel" )		return "取消";
		}
		return "";
	}
	
	public bool IsItemRightType(HeaderProto.ERoleItemEquipSlot type, int index)
	{
		if ((int)type == index)
		{
			return true;	
		}
		return false;
	}
	
	private Hashtable dataTable = new Hashtable();	
	private List<string> fileTable = new List<string>();
	
	public UIAtlas skilliconAtlas = null;
	
	public UIAtlas jobAtlas = null;

    public UIAtlas commonAtlas = null;
	
	public Hashtable jobAtlasList = new Hashtable();
	
	public Hashtable itemAtlastList = new Hashtable();

	//< 宠物图集aa
	protected UIAtlas mPetAtlas = null;
	public UIAtlas PetAtlas
	{
		get { return mPetAtlas; }
		set { mPetAtlas = value; }
	}

	protected bool onLoadPetAtlas = false;
	protected List<EventDelegate> onLoadPetAtlasEvent = new List<EventDelegate>();
	
	public void LoadPetAtlas(EventDelegate e)
	{
		if( mPetAtlas != null )
		{
			if (e!=null) e.Execute();
			return;
		}
		if( e!=null ) onLoadPetAtlasEvent.Add(e);
		if( onLoadPetAtlas ) return;

		string name = "UI/Icon/$icon_pet_0/icon_pet_0.prefab";
		ResLoadParams para = new ResLoadParams();
		sdResourceMgr.Instance.LoadResource(name,PetAtlasFinish,para,typeof(UIAtlas));
		onLoadPetAtlas = true;
	}
	private void PetAtlasFinish(ResLoadParams res, UnityEngine.Object obj)
	{
		if(obj == null) return;
		
		mPetAtlas = obj as UIAtlas;
		EventDelegate.Execute(onLoadPetAtlasEvent);
		onLoadPetAtlasEvent.Clear();
		onLoadPetAtlas = false;
	}

	// 宠物技能图集aa
	protected UIAtlas mPetSkillAtlas = null;
	public UIAtlas PetSkillAtlas
	{
		get { return mPetSkillAtlas; }
		set { mPetSkillAtlas = value; }
	}	

	// Buff图集aa
	protected UIAtlas mBuffAtlas = null;		
	public UIAtlas BuffAtlas
	{
		get { return mBuffAtlas; }
		set { mBuffAtlas = value; }
	}

	private void SetAtlas(ResLoadParams res, UnityEngine.Object obj)
	{
		if (res.info == "skill")
		{
			skilliconAtlas = obj as UIAtlas;
		}
		else if (res.info == "back")
		{
			if ((bool)res.userdata1)
			{
				jobAtlas = obj as UIAtlas;
			}

			jobAtlasList[res.userdata0.ToString()]= obj as UIAtlas;	
		}
        else if (res.info == "common")
        {
            commonAtlas = obj as UIAtlas;
        }
	}
	
	public UIAtlas GetAtlas(string key)
	{
		if (jobAtlasList.Contains(key))
		{
			return jobAtlasList[key] as UIAtlas;	
		}
		return null;
	}
	
	public string GetJobIcon(string job)
	{
		if (dataTable["JobIcon"] != null)
		{
			Hashtable table = (Hashtable)dataTable["JobIcon"];	
			if (table[job] != null)
			{
				return ((Hashtable)table[job])["JobIcon"].ToString();
			}
		}
		
		return "";
	}
	
	public string GetJobWordPic(string job)
	{
		if (dataTable["JobIcon"] != null)
		{
			Hashtable table = (Hashtable)dataTable["JobIcon"];	
			if (table[job] != null)
			{
				return ((Hashtable)table[job])["word"].ToString();
			}
		}
		
		return "";
	}
	
	public List<string> GetLinkJob(string job)
	{
		List<string> list = new List<string>();
		string iconName = "";
		if (dataTable["JobIcon"] != null)
		{
			Hashtable table = (Hashtable)dataTable["JobIcon"];	
			if (table[job] != null)
			{	
				iconName = ((Hashtable)table[job])["beijing"].ToString();
			}
			
			if (iconName != null)
			{
				foreach(DictionaryEntry item in table)	
				{
					Hashtable info = item.Value as Hashtable;
					if (info["beijing"].ToString() == iconName && job != item.Key.ToString())
					{
						list.Add(item.Key.ToString());
					}
				}
			}
		}
		
		return list;
	}
	
	public string GetJobBack(string job)
	{
		if (dataTable["JobIcon"] != null)
		{
			Hashtable table = (Hashtable)dataTable["JobIcon"];	
			if (table[job] != null)
			{
				return ((Hashtable)table[job])["beijing"].ToString();
			}
		}
		
		return "";
	}
	
	public void CreateStartEquip()
	{
		if (dataTable["startequip"] != null)
		{
			Hashtable table = (Hashtable)dataTable["startequip"];
			foreach(DictionaryEntry item in table)
			{
				//sdGameItemMgr.Instance.createDummyItem((Hashtable)item.Value);
			}
		}
	}
	
	public int GetLevelExp(string job, string level)
	{
		foreach(DictionaryEntry item in m_RoleLv)
		{
			Hashtable table = item.Value as Hashtable;

			if (table["Job"].ToString() == job && table["Level"].ToString() == level)
			{
				return (int)table["Exp"];
			}
		}
		
		return 0;
	}
	
	public	static	void	ToIntArray(string str,Hashtable table)
	{
		if(!table.ContainsKey(str))
		{
			return;
		}
		string s	=	(string)table[str];
		if(s.Length!=0)
		{
			string[] arrayValue	=	s.Split(';');
			int[]	fArray	=	new int[arrayValue.Length];
			for(int i = 0;i<arrayValue.Length;i++)
			{
				fArray[i]	=	int.Parse(arrayValue[i]);
			}
			table[str]	=	fArray;	
		}
		else
		{
			table.Remove(str);
			//table[str]	=	0.0f;	
		}
	}
	public	static	void	ToFloatArray(string str,Hashtable table)
	{
		if(!table.ContainsKey(str))
		{
			return;
		}
		string s = (string)table[str];
		if(s.Length!=0)
		{
			string[] arrayValue	=	s.Split(';');
			float[]	fArray	=	new float[arrayValue.Length];
			for(int i = 0;i<arrayValue.Length;i++)
			{
				fArray[i]	=	float.Parse(arrayValue[i]);
			}
			table[str]	=	fArray;	
		}
		else
		{
			table.Remove(str);
			//table[str]	=	0.0f;	
		}
	}
	public	static	void	ToFloat(string str,Hashtable table)
	{
		if(!table.ContainsKey(str))
		{
			return;
		}
		string s	=	(string)table[str];
		if(s.Length!=0)
		{
			float	f	=	float.Parse(s);
			table[str]	=	f;	
		}
		else
		{
			table.Remove(str);
			//table[str]	=	0.0f;	
		}
	}
	
	public  static  void    ToUInt(string str, Hashtable table)
	{
		if(!table.ContainsKey(str))
			return;
		string s = (string)table[str];
		if(s.Length != 0)
		{
			UInt32 f = UInt32.Parse(s);
			table[str] = f;
		}
		else
			table.Remove(str);
	}
	
	public	static	void	ToInt(string str,Hashtable table)
	{
		if(!table.ContainsKey(str))
		{
			return;
		}
		string s	=	(string)table[str];
		if(s.Length!=0)
		{
			int	f	=	int.Parse(s);
			table[str]	=	f;	
		}
		else
		{
			table.Remove(str);
		}
	}
	public	static	void	ToStringArray(string str,Hashtable table)
	{
		if(!table.ContainsKey(str))
		{
			return;
		}
		string s	=	(string)table[str];
		if(s.Length!=0)
		{
			string[]	strArray	=	s.Split(';');
			table[str]	=	strArray;	
		}
		else
		{
			table.Remove(str);
		}
	}
	public	static	object CloneObject(object obj)
	{
		Type t = obj.GetType();
		System.Reflection.FieldInfo[] info = t.GetFields();
		object	ret	=	Activator.CreateInstance(t);
		for(int i=0;i<info.Length;i++)
		{
			string	name	=	info[i].Name;
			SetMemberValue(ret,name,GetMemberValue(obj,name));
		}
		return ret;
	}
	public	static	Hashtable CloneHashTable(Hashtable table)
	{
		if(table==null)
		{
			return null;
		}
		Hashtable ret	=	new Hashtable();
		foreach(DictionaryEntry item in table)
		{
			object _key	=	item.Key;
			object _value	=	item.Value;
			System.Type type	=	_value.GetType();
			{
				if(type== typeof(int)){
					ret[_key] = (int)_value;
				}
				else if(type== typeof(int[])){
					int[] oldArray	=	(int[])_value;
					int[] newArray	=	new int[oldArray.Length];
					for(int i=0;i<oldArray.Length;i++)
					{
						newArray[i]	=	oldArray[i];
					}
					ret[_key] = newArray;
				}
				else if(type== typeof(float)){
					ret[_key] = (float)_value;
				}
				else if(type == typeof(float[])){
					float[] oldArray	=	(float[])_value;
					float[] newArray	=	new float[oldArray.Length];
					for(int i=0;i<oldArray.Length;i++)
					{
						newArray[i]	=	oldArray[i];
					}
					ret[_key] = newArray;
				}
				else if(type == typeof(string[]))
				{
					string[] oldArray = (string[])_value;
					string[] newArray = new string[oldArray.Length];
					for(int i = 0; i < oldArray.Length; i++)
					{
						newArray[i] = oldArray[i];
					}
					ret[_key] = newArray;
				}
				else if(type== typeof(string)){
					ret[_key] = (string)_value;
				}
				else if(type== typeof(byte))
				{
					ret[_key] = (byte)_value;
				}
				else if(type== typeof(Int64))
				{
					ret[_key] = (Int64)_value;
				}
				else if(type== typeof(UInt64))
				{
					ret[_key] = (UInt64)_value;
				}
				else if(type== typeof(uint))
				{
					ret[_key] = (uint)_value;
				}
				else if(type == typeof(ushort))
				{
					ret[_key] = (ushort)_value;
				}
				else if(type == typeof(Hashtable))
				{
					ret[_key] = CloneHashTable(_value as Hashtable);
				}
				else if(type == typeof(byte[]))
				{
					byte[] oldArray	=	(byte[])_value;
					
					string	str		=	System.Text.Encoding.UTF8.GetString(oldArray).Trim('\0');
					ret[_key] = str;
				}
				else
				{
					ret[_key] = 	CloneObject(_value);
				}
			}
		}
		return ret;
	}
	void	CreateBuffAction()
	{
		if (dataTable["buffaction"] != null)
		{
			Hashtable table = (Hashtable)dataTable["buffaction"];
			foreach(DictionaryEntry item in table)
			{
				int id		=	int.Parse(item.Key as string);
				Hashtable	skillaction	=	(Hashtable)item.Value;
                ToInt("dwTemplateID", skillaction);

				ToInt("dwHitPer",skillaction);
				ToInt("dwCriticalPer",skillaction);
				ToInt("dwCriticalDmgPer",skillaction);
				ToIntArray("dwAtkPowerPer[10]",skillaction);
				ToIntArray("dwDmg[10]",skillaction);
				ToInt("dwDamagePer",skillaction);
				ToInt("nAoeAreaData",skillaction);
				ToInt("byAoeAreaType",skillaction);
				ToInt("byAoeCenterType",skillaction);
				ToInt("nAoeCenterData",skillaction);
				ToFloat("nAoeArea",skillaction);
				ToInt("wAoeAimNum",skillaction);
				ToInt("byTargetType",skillaction);
				ToInt("dwCostSP",skillaction);
				//ToInt("nLearnLevel",skillaction);
				ToInt("byJob",skillaction);
				ToInt("byDamegePro",skillaction);			
				ToIntArray("BreakState",skillaction);
				ToInt("bySkillEffect",skillaction);
				m_BuffAction[id]	=	skillaction;				
			}
		}
	}
	void	CreateSkillAction()
	{
		for(int index = 0; index < m_JobSkillRange.Length; ++index)
		{
			skillIDRange range = new skillIDRange();  
			range.max = -1;
			range.min = 99999;
			m_JobSkillRange[index] = range;		
		}
		if (dataTable["skillaction"] != null)
		{
			Hashtable table = (Hashtable)dataTable["skillaction"];
			foreach(DictionaryEntry item in table)
			{
				int id = int.Parse(item.Key as string);

				Hashtable kSkillaction = (Hashtable)item.Value;
				ParseSkillAction(kSkillaction);

				int iJob = (int)kSkillaction["byJob"];
				if(m_vecJobSkillAction[iJob]==null)
				{
					m_vecJobSkillAction[iJob]	=	new Hashtable();					
				}
				m_vecJobSkillAction[iJob][id] = kSkillaction;
				if(m_JobSkillRange[iJob].min > id)
					m_JobSkillRange[iJob].min = id;
				if(m_JobSkillRange[iJob].max < id)
					m_JobSkillRange[iJob].max = id;				
			}
		}
	}
	
	// 初始化主角技能信息表aa
	void CreateSkillInfo()
	{
		if (dataTable["skillinfo"] != null)
		{
			Hashtable kSkillInfoTable = (Hashtable)dataTable["skillinfo"];
			foreach (DictionaryEntry kEntry in kSkillInfoTable)
			{
				int iUUID = int.Parse(kEntry.Key as string);

				Hashtable kSkillInfo = (Hashtable)kEntry.Value;
				ParseSkillInfo(kSkillInfo);

				int iId = (int)kSkillInfo["dwTemplateID"];
				int iJob = (int)kSkillInfo["byJob"];
				if(m_vecJobSkillInfo[iJob]==null)
				{
					m_vecJobSkillInfo[iJob]	=	new Hashtable();
				}
				m_vecJobSkillInfo[iJob][iId] = kSkillInfo;
			}
		}
	}
	
	// 初始化怪物(宠物)技能信息表aa
	void CreateMonsterSkillInfo()
	{
		if (dataTable["monster_skillinfo"] != null)
		{
			Hashtable kSkillInfoTable = (Hashtable)dataTable["monster_skillinfo"];	
			foreach (DictionaryEntry kEntry in kSkillInfoTable)
			{
				int iUUID = int.Parse(kEntry.Key as string);
				
				Hashtable kSkillInfo = (Hashtable)kEntry.Value;
				ParseSkillInfo(kSkillInfo);
				
				m_MonsterSkillInfo[iUUID] = kSkillInfo;
			}
		}
	}
	
	// 初始化怪物(宠物)动作信息表aa
	void CreateMonsterSkillAction()
	{
		if (dataTable["monster_skillaction"] != null)
		{
			Hashtable table = (Hashtable)dataTable["monster_skillaction"];
			foreach(DictionaryEntry item in table)
			{
				Hashtable kSkillaction = (Hashtable)item.Value;
				ParseSkillAction(kSkillaction);

				int id = int.Parse(item.Key as string);			//< 技能模板IDaa
				m_MonsterSkillAction[id] = kSkillaction;	
			}
		}
	}

	// 解析SkillInfo表的字段(包括主角\怪物\宠物三种)aa
	protected void ParseSkillInfo(Hashtable kSkillInfoTable)
	{
		if (kSkillInfoTable == null)
			return;

		ToInt("dwTemplateID", kSkillInfoTable);
		ToInt("dwCooldown", kSkillInfoTable);
		ToInt("byIsPassive", kSkillInfoTable);
		ToInt("byJob", kSkillInfoTable);
        ToInt("UnLimited", kSkillInfoTable);
	}

	// 解析SkillAction表的字段(包括主角\怪物\宠物三种)aa
	protected void ParseSkillAction(Hashtable kSkillAction)
	{
		if (kSkillAction == null)
			return;

		ToInt("dwTemplateID", kSkillAction);				//< 模板IDaa
//		ToString("strName", kSkillAction);					//< 名称aa
		ToInt("byShape", kSkillAction);
		ToInt("byDamegePro", kSkillAction);					//< 伤害类型(物理\冰\火\雷)aa
		ToInt("byJob", kSkillAction);						//< 职业aa
		ToInt("dwCostSP", kSkillAction);					//< 魔法消耗aa
//		ToString("dwIgnoreAction", kSkillAction);			//< 忽略某些动作aa
		ToInt("byTargetType", kSkillAction);				//< 目标类型aa
		ToInt("dwHitPer", kSkillAction);					//< 命中率aa
		ToInt("dwCriticalPer", kSkillAction);				//< 暴击率aa
		ToInt("dwCriticalDmgPer", kSkillAction);			//< 暴击伤害百分比aa
		ToInt("wAoeAimNum", kSkillAction);					//< 最大伤害目标个数aa
		ToFloat("nAoeArea", kSkillAction);					//< 攻击范围(单位mm)aa
		ToInt("byAoeCenterType", kSkillAction);				//< 攻击中心点类型aa
		ToInt("nAoeCenterData", kSkillAction);				//< 攻击中心点偏移值aa
		ToInt("byAoeAreaType", kSkillAction);				//< 攻击范围类型(圆形\扇形\矩形)aa	
		ToInt("nAoeAreaData", kSkillAction);				//< 攻击范围参数(例如扇形区域的扇形角度)aa	
		ToInt("byIgnoreDef", kSkillAction);					//< 是否无视防御aa
		ToInt("dwIgnoreDefendAmount", kSkillAction);		//< 无视防御类型aa
		ToInt("dwDamagePer", kSkillAction);					//< 伤害百分比aa
		ToIntArray("naMoreDamagePer[MONSTER_BODY_TYPE_max]", kSkillAction);	//< 不同目标类型的伤害比例aa
		ToInt("bySkillEffect", kSkillAction);				//< 调用skilleffectidaa
//		ToInt("dwDirectDmgMin", kSkillAction);				//< 直接伤害最小值aa
//		ToInt("dwDirectDmgMax", kSkillAction);				//< 直接伤害最大值aa
		ToIntArray("dwDmg[10]", kSkillAction);				//< 伤害aa
		ToIntArray("dwAtkPowerPer[10]", kSkillAction);		//< 伤害百分比(每个打击点都要有)aa
		ToIntArray("sSkillEffectSelf[8]", kSkillAction);	//< 调用自身skilleffectidaa
		ToIntArray("sSkillEffect[8]", kSkillAction);		//< 调用目标skilleffectidaa
        ToIntArray("sSkillEffectHitPoint[8]", kSkillAction);//< 调用目标skilleffectHitPoint 每个Effect可以使用不同的打击点 aa
//		ToString("Description", kSkillAction);				//< 描述aa
//		ToString("icon", kSkillAction);						//< 图标aa
		ToInt("ParentID", kSkillAction);					//< 技能idaa
		ToFloatArray("hitPoint", kSkillAction);				//< 打击点时间aa	
		ToStringArray("EffectAnchor", kSkillAction);		//< 特效绑定点(可能有多个)aa
		ToIntArray("EffectStartTime", kSkillAction);		//< 特效开始时间(可能有多个)aa
		ToStringArray("EffectFile", kSkillAction);			//< 特效文件路径(可能有多个)aa
		ToIntArray("EffectLifeTime", kSkillAction);			//< 特效生命时间(可能有多个)aa	
		ToInt("SkillTimerTrigger", kSkillAction);
		ToInt("SkillTimerInterval", kSkillAction);
		ToInt("MoveSpeed", kSkillAction);					//< 释放技能过程中移动速度aa
		ToInt("MoveBeginTime", kSkillAction);				//< 技能移动开始时间aa
		ToInt("MoveEndTime", kSkillAction);					//< 技能移动结束时间aa
		ToInt("RotateBeginTime", kSkillAction);				//< 旋转开始时间aa
		ToInt("RotateEndTime", kSkillAction);				//< 旋转结束时间aa
		ToInt("shakeCamera", kSkillAction);					//< 是否震屏aa
		ToInt("ShakeLevel", kSkillAction);
		ToStringArray("AudioConf", kSkillAction);			//< 声音路径aa
		ToIntArray("AudioStartTime", kSkillAction);			//< 声音开始时间aa
//		ToString("ClassName", kSkillAction);				//< 类名aa
		ToInt("CastDistance", kSkillAction);				//< 释放距离aa
//		ToString("SelfEffect", kSkillAction);				//< 自身特效aa
		ToInt("SelfEffectLife", kSkillAction);				//< 自身特效时间aa
//		ToString("SummonEffect", kSkillAction);				//< 召唤特效(已废弃)aa
//		ToString("ExplosionEffect", kSkillAction);			//< 爆炸特效aa
		ToInt("ExplosionEffectLife", kSkillAction);			//< 爆炸特效时间aa
//		ToString("HitEffect", skillaction);					//< 命中特效aa
		ToInt("HitEffectRotate", kSkillAction);
		ToInt("HitEffectLife", kSkillAction);				//< 命中特效播放时间aa
//		ToInt("dwCooldown", kSkillAction);					//< (已废弃)冷却时间
//		ToFloat("SkillTime", kSkillAction);					//< (已废弃)技能持续时间(单位ms)aa	
//		ToInt("AnimId", kSkillAction);						//< (已废弃)
//		ToString("animation", kSkillAction);				//< (已废弃)技能动画名aa
//		ToString("AnimFadeTime", kSkillAction);				//< (已废弃)aa
//		ToInt("nLearnLevel",kSkillAction);
//		ToInt("byPeriodicDamage",kSkillAction);
//		ToFloat("dwPeriodicTraumaHP",kSkillAction);
		ToInt("WeaponTrailLife", kSkillAction);
		ToInt("WeaponTrailDisappear", kSkillAction);
		ToInt("WeaponTrailLength", kSkillAction);
		ToInt("WeaponTrailDelay", kSkillAction);
		ToInt("Enable", kSkillAction);
		ToInt("CalcDamage", kSkillAction);
		ToInt("ZoomInStartTime", kSkillAction);
		ToInt("ZoomInEndTime", kSkillAction);
		ToInt("ZoomInLevel", kSkillAction);
		ToInt("HealRatio", kSkillAction);
		ToIntArray("BreakState", kSkillAction);
		ToInt("HitDummy", kSkillAction);
	}
	
	void CreateGroup()
	{
		if (dataTable["group"] != null)
		{
			Hashtable table = (Hashtable)dataTable["group"];
			foreach(DictionaryEntry item in table)
			{
				int uid		=	int.Parse(item.Key as string);
				Hashtable	group1	=	(Hashtable)item.Value;
				//Hashtable	group2	=	new Hashtable();
				foreach(DictionaryEntry item1 in group1)
				{
					int iKey	=	0;
					if(int.TryParse(item1.Key as string,out iKey))
					{
						int iVal	=	int.Parse(item1.Value as string);
						//group2[iKey]	=	iVal;
						m_Group[uid,iKey]	=	(GroupType)iVal;
					}
					
				}
				//m_Group[uid]	=	group2;
			}
		}
	}
	public	static	void	ParseAllMember(object obj,Hashtable table)
	{
		Type t = obj.GetType();

		foreach(DictionaryEntry item in table)
		{
			string str	=	item.Value as string;
			if(str.Length==0)
			{
				continue;	
			}
			FieldInfo finfo = t.GetField(item.Key as string);
			if(finfo==null)
			{
				continue;
			}

			Type valType	=	finfo.FieldType;
			if(valType	==	typeof(int[]))
			{
				string[] strArray	=	(item.Value as string).Split(';');
				int[]    intArray	=	new int[strArray.Length];
				for(int i=0;i<intArray.Length;i++)
				{
					intArray[i]	=	int.Parse(strArray[i]);
				}
				finfo.SetValue(obj,intArray);
			}
			else if(valType	==	typeof(float[]))
			{
				string[] strArray	=	(item.Value as string).Split(';');
				float[]  floatArray	=	new float[strArray.Length];
				for(int i=0;i<floatArray.Length;i++)
				{
					floatArray[i]	=	float.Parse(strArray[i]);
				}
				finfo.SetValue(obj,floatArray);
			}
			else if(valType	==	typeof(string))
			{
				finfo.SetValue(obj,str);
			}
			else
			{
				MethodInfo minfo =valType.GetMethod("Parse",new Type[]{typeof(string)});
				if(minfo != null)
				{
					object val = minfo.Invoke(null,new object[]{str});
					finfo.SetValue(obj,val);	
				}
			}
		}
		//obj.
		//typeof(obj);
	}
	public	static	object	GetMemberValue(object obj,string str)
	{
		Type t = obj.GetType();
		FieldInfo finfo = t.GetField(str);
		if(finfo==null)
		{
			return null;
		}
		return finfo.GetValue(obj);
	}
	public	static	void	SetMemberValue(object obj,string str,object val)
	{
		Type t = obj.GetType();
		FieldInfo finfo = t.GetField(str);
		if(finfo==null)
		{
			return;
		}
		finfo.SetValue(obj,val);
	}
	void	CreateSkillEffect()
	{
		if (dataTable["skilleffect"] != null)
		{
			Hashtable table = (Hashtable)dataTable["skilleffect"];
			foreach(DictionaryEntry item in table)
			{
				SkillEffect effect	=	new SkillEffect();				
				ParseAllMember(effect,item.Value as Hashtable);
				effect.dwID = Int32.Parse(item.Key as string);
				m_BaseSkillEffect[effect.dwID]	=	effect;
			}
		}
	}
	void	CreateBuff()
	{
		if (dataTable["buff"] != null)
		{
			Hashtable table = (Hashtable)dataTable["buff"];
			foreach(DictionaryEntry item in table)
			{
				Hashtable buff	=	item.Value	as Hashtable;
				
				ToInt("dwClassID",buff);
				ToInt("byLevel",buff);
				ToInt("byClass",buff);
				ToInt("bySubClass",buff);
				ToInt("byProperty",buff);
				ToInt("nTotalTime",buff);
				ToInt("nPeriodicTime",buff);
				ToInt("IsHide",buff);
				ToInt("IsSave",buff);
				ToInt("IsTiming",buff);
				ToInt("IsShare",buff);
				ToInt("IsSwap",buff);
				ToInt("IsDeadHold",buff);
				ToInt("IsRefresh",buff);
				ToInt("IsAuraExclude",buff);
				ToInt("IsSceneSave",buff);
				ToInt("IsDebuff",buff);
				ToInt("byDisperseWay",buff);
				ToInt("dwSwapFlag",buff);
				ToInt("byEffectLevel",buff);
				ToInt("byAugment",buff);
				ToInt("dwUseUpValue",buff);
				ToInt("byAuraAimType",buff);
				ToInt("nArea",buff);
				ToInt("byAuraCenterType",buff);
				ToInt("nAuraCenterData",buff);
				ToInt("byAuraAreaType",buff);
				ToInt("nAuraAreaData",buff);
				ToInt("byBuffEffect",buff);
				ToInt("dwCritical",buff);
				ToInt("byIgnoreDefend",buff);
				ToInt("dwIgnoreDefendAmount",buff);
				ToInt("dwPhyDmg",buff);
				ToInt("dwAtkPowerPer",buff);
				ToInt("dwDamagePer",buff);
				ToInt("qwSpringFlag",buff);
				ToInt("Dummy",buff);
			}
		}
	}
	void	CreateBuffeffect(Hashtable table)
	{
		if (table != null)
		{
			//Hashtable table = (Hashtable)dataTable["buff"];
			foreach(DictionaryEntry item in table)
			{
				Hashtable subTable	=	item.Value	as Hashtable;

					
				ToInt("effectID",subTable);
				ToInt("byEffectType",subTable);
				ToInt("byEffectExplain",subTable);
				ToInt("dwOperationID",subTable);
				ToInt("dwOperationData",subTable);
				ToInt("dwOperationData1",subTable);
				ToInt("dwPeriodicData",subTable);
				ToInt("dwEXData",subTable);
				
				
			}
		}
	}
	void	CreateSummon()
	{
		if (dataTable["summonobject"] != null)
		{
			Hashtable table = (Hashtable)dataTable["summonobject"];
			foreach(DictionaryEntry item in table)
			{
				int id			=	int.Parse(item.Key as string);
				Hashtable summon	=	item.Value	as Hashtable;
                ToInt("dwTemplateID", summon);
				ToInt("byShape",summon);
				ToInt("byDamegePro",summon);
				ToInt("byTargetType",summon);
				ToInt("dwHitPer",summon);
				ToInt("dwCriticalPer",summon);
				ToInt("dwCriticalDmgPer",summon);
				ToInt("wAoeAimNum",summon);
				ToInt("Mode",summon);
				ToInt("PeriodTime",summon);
				ToInt("Count",summon);
				ToFloat("nAoeArea",summon);
				ToInt("byAoeAreaType",summon);
				ToInt("nAoeAreaData",summon);
				ToInt("byAoeCenterType",summon);
				ToInt("nAoeCenterData",summon);
				ToInt("byIgnoreDef",summon);
				ToInt("dwIgnoreDefendAmount",summon);
				ToInt("dwDamagePer",summon);
				ToIntArray("naMoreDamagePer[MONSTER_BODY_TYPE_max]",summon);
				ToInt("bySkillEffect",summon);
				ToIntArray("dwDmg[10]",summon);
				ToIntArray("dwAtkPowerPer[10]",summon);
				ToIntArray("sSkillEffect[8]",summon);
				//ToInt("Description",summon);
				ToInt("MoveSpeed",summon);
				ToInt("LifeTime",summon);
				//ToInt("AudioConf",summon);
				//ToInt("SummonEffect",summon);
				//ToInt("ExplosionEffect",summon);
				ToInt("ExplosionEffectLife",summon);
				//ToInt("ExplosionAudioConf",summon);
				ToInt("ExplosionAudioStartTime",summon);
				//ToInt("HitEffect",summon);
				ToInt("HitEffectLife",summon);
				ToInt("shakeCamera",summon);
				ToInt("ShakeLevel",summon);
				ToIntArray("BreakState",summon);
				ToInt("DelayDamage", summon);
				ToInt("HitDummy", summon);
                ToInt("CalcDamage", summon);
                ToInt("High", summon);
				int delay =0;
				if(id >= 1001)
					delay = (int)summon["DelayDamage"];
				m_BaseSummon[id] = summon;
			}
		}
	}
	
	void CreateRoleLevel()
	{
		if (dataTable["rolelevels"] != null)
		{
			Hashtable table = (Hashtable)dataTable["rolelevels"];
			foreach(DictionaryEntry item in table)
			{
				int id	= int.Parse(item.Key as string);
				Hashtable lvinfo	=	item.Value	as Hashtable;
				ToInt("Job",lvinfo);
				ToInt("Level",lvinfo);
				ToInt("Exp",lvinfo);
				m_RoleLv[id]	=	lvinfo;
			}
		}
	}
	
	// 预解析怪物属性表aa
	void CreateMonsterTemplate()
	{
		Hashtable kTable = (Hashtable)dataTable["dmdscreatureconfig.monstertemplates"];
		if (kTable != null)
		{
			Hashtable kProperty = new Hashtable();
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				Hashtable subTable = new Hashtable();
				foreach(DictionaryEntry de2 in valTable)
				{
					string key2 = (string)de2.Key;
					string val = (string)de2.Value;
					int intVal = 0;
					key2 = key2.Replace("Property.","");
					
					if(int.TryParse(val,out intVal))
					{
						subTable.Add(key2,intVal);
					}
					else
					{
                        subTable.Add(key2,val);
                        if("DeathHitPoint"==key2)
                        {
                            ToFloatArray("DeathHitPoint", subTable);
                        }
                        else if("DeathEffect"==key2)
                        {
                            ToIntArray("DeathEffect", subTable);
                        }
                        else if("sSkillEffectHitPoint"==key2)
                        {
                            ToIntArray("sSkillEffectHitPoint", subTable);
                        }
						
					}
				}


                subTable["HurtOtherModify"] = 0;
				subTable["PhysicsModify"] = 0;
				subTable["IceModify"] = 0;
				subTable["FireModify"] = 0;
				subTable["PoisonModify"] = 0;
				subTable["ThunderModify"] = 0;
				subTable["StayModify"] = 0;
				subTable["HoldModify"] = 0;
				subTable["StunModify"] = 0;	
				subTable["BeHurtModify"] = 0;	

				key1 = key1.Replace("Property.","");
				kProperty.Add(int.Parse(key1), subTable);
			}
			
			dataTable["MonsterProperty"] = kProperty;
		}
	}
	
	// 预解析宠物属性表aa
	void CreatePetTemplate()
	{
		Hashtable kTable = (Hashtable)dataTable["dmdscreatureconfig.pettemplates"];
		if (kTable != null)
		{
			Hashtable kProperty = new Hashtable();
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				Hashtable subTable = new Hashtable();
				
				foreach(DictionaryEntry de2 in valTable)
				{
					string key2 = (string)de2.Key;
					string val = (string)de2.Value;
					int intVal = 0;
					key2 = key2.Replace("Property.","");
					
					if(int.TryParse(val,out intVal))
					{
						subTable.Add(key2,intVal);
					}
					else
					{
						subTable.Add(key2,val);
					}
				}

				subTable["HurtOtherModify"] = 0;
				subTable["PhysicsModify"] = 0;
				subTable["IceModify"] = 0;
				subTable["FireModify"] = 0;
				subTable["PoisonModify"] = 0;
				subTable["ThunderModify"] = 0;
				subTable["StayModify"] = 0;
				subTable["HoldModify"] = 0;
				subTable["StunModify"] = 0;	
				subTable["BeHurtModify"] = 0;	
				key1 = key1.Replace("Property.","");
				kProperty.Add(int.Parse(key1), subTable);
			}
			
			dataTable["PetProperty"] = kProperty;
		}
		
		if (kTable != null)
		{
			Hashtable kTable_zs = new Hashtable();
			Hashtable kTable_fs = new Hashtable();
			Hashtable kTable_ms = new Hashtable();
			Hashtable kTable_yx = new Hashtable();
			
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				
				string key2 = "";
				string val = "";
				foreach(DictionaryEntry de2 in valTable)
				{
					key2 = (string)de2.Key;
					val = (string)de2.Value;
					
					if (key2=="BaseJob")
					{
						break;
					}
				}
				
				if (key2=="BaseJob")
				{
					if (val=="1")
					{
						kTable_zs.Add(int.Parse(key1), valTable);
					}
					else if (val=="2")
					{
						kTable_fs.Add(int.Parse(key1), valTable);
					}
					else if (val=="3")
					{
						kTable_yx.Add(int.Parse(key1), valTable);
					}
					else if (val=="4")
					{
						kTable_ms.Add(int.Parse(key1), valTable);
					}
				}
				
				key2 = "";
				val = "";
			}
			
			dataTable["PetTableZS"] = kTable_zs;
			dataTable["PetTableFS"] = kTable_fs;
			dataTable["PetTableYX"] = kTable_yx;
			dataTable["PetTableMS"] = kTable_ms;
		}
	}
	
	void CreatePropTrans()
	{
		
		Hashtable propTransferTable	=	new Hashtable();
		if (dataTable["PropTransfer"] != null)
		{
			Hashtable table = (Hashtable)dataTable["PropTransfer"];
			
			foreach(DictionaryEntry de in table)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				Hashtable subTable = new Hashtable();
				
				foreach(DictionaryEntry de2 in valTable)
				{
					string key2 = (string)de2.Key;
					string val = (string)de2.Value;
					int intVal = 0;
					if(int.TryParse(val,out intVal))
					{
						subTable.Add(key2,intVal);
					}
					else
					{
						subTable.Add(key2,val);
					}
				}
				
				propTransferTable.Add(key1,subTable);
			}
			table.Clear();
			dataTable["PropTransfer"]	=	propTransferTable;
		}
	}
	private int initCount = 0;
	public bool isInitFinish() { return (initCount<=0); }
	
	// 当前是否属于测试模式aa
	protected bool mTestMode = false;
	public bool TestMode
	{
		get { return mTestMode; }
	}

	// 当前是否已经初始化aa
	protected bool mIsInitialized = false;
	public bool IsInitialized
	{
		get { return mIsInitialized; }
	}

    void _LoadCSVResource(string str)
    {
        SDCSV csv = new SDCSV();
        csv.LoadCSVResource(str);
        dataTable[str] = csv.csvTable;
        //initCount++;
        
    }
	void _LoadCSV(string str)
	{
		SDCSV csv = new SDCSV();
		if (mTestMode)
		{
			csv.LoadCSVInTestMode(str, FinishLoad);
		}
		else
		{
			csv.LoadCSV(str, FinishLoad);
			initCount++;
		}
	}
	void _LoadCSVList(string str)
	{
		SDCSV csv = new SDCSV();
		if (mTestMode)
		{
			csv.LoadCSVListTestMode(str, FinishLoadList);
		}
		else
		{
			csv.LoadCSVList(str, FinishLoadList);
			initCount++;
		}
	}
	public void Init(bool testMode)
	{
		mIsInitialized = true;
		mTestMode = testMode;

		_LoadCSV("$Conf/dmdscreatureconfig.monstertemplates.txt");
		_LoadCSV("$Conf/startequip.txt");
		_LoadCSV("$Conf/hair.txt");
		_LoadCSV("$Conf/dmdscreatureconfig.itemtemplates.txt");
		_LoadCSV("$Conf/skill.txt");
		_LoadCSV("$Conf/stringtable.txt");
		_LoadCSV("$Conf/skillaction.txt"); 
		_LoadCSV("$Conf/skillinfo.txt");
		_LoadCSV("$Conf/score.txt");
		_LoadCSV("$Conf/skilleffect.txt");
        _LoadCSVResource("errorcode");
		_LoadCSV("$Conf/buff.txt");
		_LoadCSV("$Conf/buffeffect.txt"); 
		_LoadCSV("$Conf/operation.txt"); 
		_LoadCSV("$Conf/dmdscreatureconfig.pettemplates.txt"); 
		_LoadCSV("$Conf/dmdspetexconfig.petups.txt"); 
		_LoadCSV("$Conf/dmdspetexconfig.petlevels.txt"); 
		_LoadCSV("$Conf/dmdspetexconfig.petgroups.txt");
		_LoadCSV("$Conf/dmdssapconfig.saps.txt");
		_LoadCSV("$Conf/buffaction.txt"); 
		_LoadCSV("$Conf/group.txt"); 
		_LoadCSV("$Conf/summonobject.txt"); 
		_LoadCSV("$Conf/rolelevels.txt"); 
		_LoadCSV("$Conf/JobIcon.txt"); 
		_LoadCSV("$Conf/passiveaction.txt");
		_LoadCSV("$Conf/animation.txt");
		_LoadCSV("$Conf/monster_animation.txt");
		_LoadCSV("$Conf/monster_passiveaction.txt"); 
		_LoadCSV("$Conf/monster_skillaction.txt"); 
		_LoadCSV("$Conf/monster_skillinfo.txt"); 
		_LoadCSV("$Conf/PropTransfer.txt");
		_LoadCSV("$Conf/hitsound.txt");
		_LoadCSV("$Conf/levelscore.txt");
		_LoadCSV("$Conf/worldmappath.txt");
		_LoadCSV("$Conf/systemlock.txt");
		_LoadCSV("$Conf/dropmodel.txt");
		_LoadCSVList("$Conf/moviedialogue.txt");
		_LoadCSV("$Conf/moviebattle.txt");
		_LoadCSV("$Conf/dmdsxactivitytemplateconfig.abysstemplates.txt");
		
		_LoadCSV("$Conf/dmdsshopconfig.shops.txt");
		_LoadCSV("$Conf/dmdsshopconfig.shop_pets.txt");
		_LoadCSV("$Conf/dmdsshopconfig.shop_actionpoints.txt");			
		
		_LoadCSV("$Conf/dmdscreatureconfig.itemups.txt");
		_LoadCSV("$Conf/titleinfo.txt");
		_LoadCSV("$Conf/dmdssuitconfig.suits.txt");
		_LoadCSV("$Conf/dmdssapconfig.saps.txt");
		_LoadCSVList("$Conf/pvprewards.txt");
    	_LoadCSVList("$Conf/reputationRewards.txt");
		_LoadCSV("$Conf/mailhead.txt");
    	_LoadCSV("$Conf/GemLevel.txt");


		// Award center db
		_LoadCSV("$Conf/dmdsgiftconfig.gift_dayquestboxs.txt");
		_LoadCSV("$Conf/dmdsgiftconfig.gift_dayquests.txt");
		_LoadCSV("$Conf/dmdsgiftconfig.gift_signs.txt");
		_LoadCSV("$Conf/dmdsgiftconfig.gift_levels.txt");

    	_LoadCSV("$Conf/militarylevel.txt");
		
		//World Boss..
		_LoadCSV("$Conf/dmdsworldbossconfig.worldbossparam.txt");
		_LoadCSV("$Conf/dmdsworldbossconfig.worldbosstemplates.txt");

		_LoadCSV("$Conf/RandomName.txt");
        _LoadCSV("$Conf/config.txt");
        _LoadCSV("$Conf/dmdsshopconfig.shop_secrets.txt");

        _LoadCSV("$Conf/dmdscreatureconfig.itemprods.txt");

		_LoadCSV("$Conf/vip_client.txt");
        _LoadCSV("$Conf/dmdsxactivitytemplateconfig.pttemplates.txt");
		if (GetRoleSetting("IgnoreGuide") != "1")
		{
			
			//_LoadCSV("$Conf/guide.txt");
			
		}

		_LoadCSV("$Conf/dmdsrmbconfig.pay.txt");

	}
	
	Hashtable headPic = new Hashtable();
	Hashtable LoadedHead = new Hashtable();
	
	public delegate void HeadAtlas(int iGender, int iHairStyle, UIAtlas atlas);
	//public event HeadAtlas SetHeadAtlas;

    public void LoadRoleSetting(string config)
    {
        roleSetting.Clear();
        string[] lineList = config.Split('\n');
        foreach (string line in lineList)
        {
            if (line.Length > 0)
            {
                string[] item = line.Split(',');
                if (item.Length != 2) continue;
                roleSetting[item[0]] = item[1];
            }
        }
    }

    public string GetGemBg(int index)
    {
        switch (index)
        {
            case 1: return "bg-r";
            case 2: return "bg-y";
            case 3: return "bg-b";
            case 4: return "bg-g";
            case 5: return "bg-p";
        }

        return "";
    }

	public void LoadSetting()
	{
		string path = Application.persistentDataPath+"//setting.txt";
		if (File.Exists(path))
		{
			StreamReader	r	=	File.OpenText(Application.persistentDataPath+"//setting.txt");
			bool isHead = true;
			while(!r.EndOfStream)
			{	
				string line	=	r.ReadLine();
				if (isHead) 
				{
					isHead = false;
					continue;
				}
				if(line.Length>0)
				{
					string[] item =	line.Split(',');
					settingTable[item[0]] = item[1];
				}
			}
			r.Close();
		}
	}
	
	// 图形设置.
	public int[,] mGConfig = 
	{
		// id,width,height,fps,texture,shader,shadow,aa
		{ 0,0,0,0,0,0,0,0},
		{ 1, 848, 600,24,1,0,0,0},
		{ 2, 848, 600,30,1,0,1,0},
		{ 3, 848, 600,30,0,0,1,0},
		{ 4, 848, 600,30,0,1,1,0},
		{ 5, 848, 600,30,0,1,1,1},
		{ 6, 848, 600,60,0,1,1,0},
		{ 7, 848, 600,60,0,1,1,1},
		{ 8,1280, 800,24,0,0,1,0},
		{ 9,1280, 800,24,0,0,1,0},
		{10,1280, 800,24,0,1,1,0},
		{11,1280, 800,30,0,1,1,0},
		{12,1280, 800,30,0,1,2,1},
		{13,1280, 800,60,0,1,1,0},
		{14,1280, 800,60,0,1,2,1},
		{15,1920,2000,24,0,1,1,0},
		{16,1920,2000,30,0,1,1,0},
		{17,1920,2000,30,0,1,2,1},
		{18,1920,2000,60,0,1,1,0},
		{19,1920,2000,60,0,1,2,1},
	};
	// 图形选项表.
	public int[] mGConfigCurrent = {11,11,11};

	public void LoadGraphicConfig()
	{
		if( Application.platform!=RuntimePlatform.Android && Application.platform!=RuntimePlatform.IPhonePlayer ) return;

		// 判断手机型号.
		Hashtable GDevice = new Hashtable();
		// device,low,mid,high
		GDevice["low device"]	= new int[] { 1, 2,11};
		GDevice["mid device"]	= new int[] {10,11,16};
		GDevice["high device"]	= new int[] {15,16,19};
		GDevice["htc one"]		= new int[] {10,11,16};
		GDevice["t310"]			= new int[] {10,11,14};
		GDevice["t311"]			= new int[] {10,11,14};
		GDevice["n7100"]		= new int[] {10,11,14};
		GDevice["geak eye"]		= new int[] { 4,11,13};

		int[] GLevel = null;
		string str = sdConfDataMgr.Instance().GetSetting("DeviceMode");
		if( str != "" ) 
		{
			GLevel = (int[])GDevice[str];
		}
		else
		{
			string strMyDevice = SystemInfo.deviceModel.ToLower();
			foreach(DictionaryEntry de in GDevice )
			{
				if( strMyDevice.IndexOf(de.Key as string) < 0 ) continue;
				GLevel = (int[])de.Value;
				sdConfDataMgr.Instance().SetSetting("DeviceMode",de.Key as string);
				break;
			}

			if( GLevel == null )
			{
				if( Application.platform == RuntimePlatform.Android )
				{
					if( Screen.height<650 || SystemInfo.systemMemorySize<700 || SystemInfo.processorCount<2 )
						strMyDevice = "low device";
					else if( Screen.height<850 || SystemInfo.systemMemorySize<2100 || SystemInfo.processorCount<4 )
						strMyDevice = "mid device";
					else
						strMyDevice = "high device";
				}
				else
				{
					if( Screen.height<600 || SystemInfo.processorCount<2 )
						strMyDevice = "low device";
					else if( Screen.height<800 || SystemInfo.processorCount<4 )
						strMyDevice = "mid device";
					else
						strMyDevice = "high device";
				}

				GLevel = (int[])GDevice[strMyDevice];
				sdConfDataMgr.Instance().SetSetting("DeviceMode",strMyDevice);
			}
		}

		// 设置当前机型使用显示配置.
		if( GLevel != null )
		{
			mGConfigCurrent[0] = GLevel[0];
			mGConfigCurrent[1] = GLevel[1];
			mGConfigCurrent[2] = GLevel[2];
		}
	}

	// 应用图像设置..
	public void ApplyGraphicConfig(bool bInit)
	{
		int GLevel = 1;	// 缺省为标准画质..
		string str = GetSetting("CFG_Graphic");
		if( str!="" ) GLevel = int.Parse(str);
		int gIdx = mGConfigCurrent[GLevel];
		int HeightLimit = mGConfig[gIdx,2];

		// 限制分辨率上限，避免分辨率过高...
		if( Screen.height > HeightLimit )
		{
			if( HeightLimit < 800 )
			{
				if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r16_9 )
					Screen.SetResolution(1024,576,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r16_10 )
					Screen.SetResolution(1024,640,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r3_2 )
					Screen.SetResolution(960,640,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r4_3 )
					Screen.SetResolution(1024,768,true);
			}
			else
			{
				if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r16_9 )
					Screen.SetResolution(1280,720,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r16_10 )
					Screen.SetResolution(1280,800,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r3_2 )
					Screen.SetResolution(960,640,true);
				else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r4_3 )
					Screen.SetResolution(1024,768,true);
			}
		}

		// 设置贴图精度..
		QualitySettings.masterTextureLimit = mGConfig[gIdx,4];

		// 帧速限制..
		Application.targetFrameRate = mGConfig[gIdx,3];;

		// 除非是第一次设置，否则要重置显示设备..
		if( bInit == false )
		{
			int i = QualitySettings.GetQualityLevel();
			QualitySettings.SetQualityLevel(i,true);
		}
#if UNITY_EDITOR
		else
		{
			QualitySettings.vSyncCount = 1;
		}
#endif
	}

	public void SetHeadPic(int iGender, int iHairStyle, int color, UISprite owner)
	{
        if(owner == null)
            return;
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
		
		int iHairColorIndex = color+1;	//< 图标索引:
		string name = string.Format("{0}-{1}", iHairStyleIndex, iHairColorIndex);
		if (headPic[iGender] != null)
		{
			Hashtable info = headPic[iGender] as Hashtable;
			if (info[iHairStyle] != null)
			{
				UIAtlas atlas = info[iHairStyle] as UIAtlas;
                owner.atlas = atlas;
                owner.spriteName = name;
				return;
			}
		}
		LoadHeadPic(iGender, iHairStyle, owner, name);	
		
	}
	
	public void LoadHeadPic(int iGender, int iHairStyle, UISprite owner, string spriteName)
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
		
        //if (LoadedHead[iGender] != null)
        //{
        //    Hashtable info = LoadedHead[iGender] as Hashtable;
        //    if (info[iHairStyle] != null)
        //    {
        //        return;	
        //    }
        //}
		
        //if (LoadedHead[iGender] != null)
        //{
        //    Hashtable info = LoadedHead[iGender] as Hashtable;
        //    info.Add(iHairStyle, 1);
        //}
        //else
        //{
        //    Hashtable info = new Hashtable();
        //    info.Add(iHairStyle, 1);
        //    LoadedHead.Add(iGender, info);
        //}
		
		string kAssertPath = string.Format("UI/CharHair/$mhair{0}/mhair{0}_prefab.prefab", iHairStyleIndex); 
		if (iGender == 1)
			kAssertPath = string.Format("UI/CharHair/$whair{0}/whair{0}_prefab.prefab", iHairStyleIndex); 


		ResLoadParams kParam = new ResLoadParams();
		kParam.userdata0 = iGender;
		kParam.userdata1 = iHairStyle;
        kParam.userdata2 = owner;
        kParam.userdata3 = spriteName;
		sdResourceMgr.Instance.LoadResource(
			kAssertPath, 
			FinishLoadHeadPic, 
			kParam,
			typeof(UIAtlas));	
	}
	
	void FinishLoadHeadPic(ResLoadParams kParam, UnityEngine.Object kObj)
	{
		if (kObj == null)
			return;
		
		UIAtlas kAtlas = kObj as UIAtlas;
		
		int iGender = (int)kParam.userdata0;
		int iHairStyle = (int)kParam.userdata1;
		
		Hashtable info = null;
		if (headPic[iGender] == null)
		{
			info = new Hashtable();
			info.Add(iHairStyle, kAtlas);
			headPic.Add(iGender, info);
		}
		else
		{
			info = headPic[iGender] as Hashtable;
            if (info.ContainsKey(iHairStyle) == false)
			    info.Add(iHairStyle, kAtlas);
		}

        UISprite owner = (UISprite)kParam.userdata2;
        if (owner == null)
            return;
        owner.atlas = kAtlas;
        owner.spriteName = kParam.userdata3.ToString();
		//SetHeadAtlas(iGender, iHairStyle, kAtlas);
	}
	
	public void LoadSkillIcon(int job)
	{
		string name = "";
		switch(job)
		{
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:
		{
			name = "warrior";
			break;
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:
		{
			name = "magic";
			break;	
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:
		{
			name = "rogue";
			break;		
		}	
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:
		{
			name = "minister";
			break;		
		}
		}
		int num = 0;
		ResLoadParams para = new ResLoadParams();
		para.info = "skill";
		
		string namePreb = string.Format("UI/Icon/$icon_skill_{0}/{0}.prefab", name); 
		sdResourceMgr.Instance.LoadResource(namePreb,SetAtlas,para,typeof(UIAtlas));
		
		num++;
		ResLoadParams para1 = new ResLoadParams();
		para1.info = "back";
		para1.userdata0 = job;
		para1.userdata1 = true;
		namePreb = string.Format("UI/Icon/$icon_{0}_back/icon_{0}_back.prefab", name); 
		sdResourceMgr.Instance.LoadResource(namePreb,SetAtlas,para1,typeof(UIAtlas));

        ResLoadParams para2 = new ResLoadParams();
        para2.info = "common";
        namePreb = string.Format("UI/$common/common.prefab", name);
        sdResourceMgr.Instance.LoadResource(namePreb, SetAtlas, para2, typeof(UIAtlas));
	}
	
	public void LoadJobAtlas(int job)
	{
		if (jobAtlasList.ContainsKey(job.ToString())) return;
		
		string name = "";
		switch(job)
		{
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:
		{
			name = "warrior";
			break;
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:
		{
			name = "magic";
			break;	
		}
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:
		{
			name = "rogue";
			break;		
		}	
		case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:
		{
			name = "minister";
			break;		
		}
		}
		ResLoadParams para = new ResLoadParams();
		para.info = "back";
		para.userdata0 = job;
		para.userdata1 = false;
		string namePreb = string.Format("UI/Icon/$icon_{0}_back/icon_{0}_back.prefab", name); 
		sdResourceMgr.Instance.LoadResource(namePreb,SetAtlas,para,typeof(UIAtlas));
	}

	Hashtable onGetItemAtlas = new Hashtable();


	public void LoadItemAtlas(string index, ResLoadDelegate e)
	{
		LoadItemAtlas(index,e,null);
	}


	//public void 
	public void LoadItemAtlas(string index, ResLoadDelegate e, object userdata)
	{
        if (!itemAtlastList.ContainsKey(index) && !loadedItem.Contains(index)) 
		{
            string name = string.Format("UI/Icon/$icon_item_{0}/item_{0}.prefab", index);
            ResLoadParams para = new ResLoadParams();
            para.info = index.ToString();
            sdResourceMgr.Instance.LoadResource(name, ItemAtlasFinish, para, typeof(UIAtlas));
            loadedItem.Add(index);
		}

        if (itemAtlastList.ContainsKey(index))
        {
			ResLoadParams para = new ResLoadParams();
			para.info = index.ToString();
			para.userdata0 = userdata;
			if (e!=null)
				e(para, (UnityEngine.Object)itemAtlastList[index]);
        }
        else
        {
            string name = string.Format("UI/Icon/$icon_item_{0}/item_{0}.prefab", index);
            ResLoadParams para = new ResLoadParams();
            para.info = index.ToString();
			para.userdata0 = userdata;
            sdResourceMgr.Instance.LoadResource(name, e, para, typeof(UIAtlas));
        }
	}
	
	List<string> loadedItem = new List<string>(); 
	
	private void ItemAtlasFinish(ResLoadParams res, UnityEngine.Object obj)
	{
		if (obj == null)
			return;
		
		UIAtlas kAtlas = obj as UIAtlas;
		itemAtlastList.Add(res.info, kAtlas);
		loadedItem.Remove(res.info);

// 		if (onGetItemAtlas.ContainsKey(res.info))
// 		{
// 			List<EventDelegate> list = onGetItemAtlas[res.info] as List<EventDelegate>;
// 			EventDelegate.Execute(list);
// 			list.Clear();
// 		}
	}
	
	public UIAtlas GetItemAtlas(string index)
	{
		return itemAtlastList[index] as UIAtlas;	
	}
	
	public HeaderProto.ELevelCompleteResult GetResult(int levelId, float time)
	{
		Hashtable table = dataTable["levelscore"] as Hashtable;
		if (table == null) return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
		Hashtable result = table[levelId.ToString()] as Hashtable;
		if (result == null) return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
		float timeS = float.Parse(result["Time_S"].ToString());
		float timeA = float.Parse(result["Time_A"].ToString());
		float timeB = float.Parse(result["Time_B"].ToString());
		if (time <= timeS)
		{
			return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_S;	
		}
		else if (time <= timeA)
		{
			return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_A;	
		}
		
		return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
	}

	public HeaderProto.ELevelCompleteResult GetLapBossResult(int levelId, int iBossHp, int iKillHp)
	{
		if (iBossHp<=0)
			iBossHp = 1;
		if (iKillHp<0)
			iKillHp = 0;

		int iNum = levelId%2;
		float fVal = (float)iKillHp/(float)iBossHp;
		//普通难度..
		if (iNum==1)
		{
			if (fVal<0.1f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
			else if (fVal>=0.1f&&fVal<0.2f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_A;
			else if (fVal>=0.2f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_S;
		}
		//噩梦难度..
		else if (iNum==0)
		{
			if (fVal<0.05f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
			else if (fVal>=0.05f&&fVal<0.1f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_A;
			else if (fVal>=0.1f)
				return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_S;
		}

		return HeaderProto.ELevelCompleteResult.LEVEL_COMPLETE_RESULT_B;
	}
	
	public bool ShowMovie(int levelID)
	{
		Hashtable movietable = sdConfDataMgr.Instance().m_Movie;
		if(movietable.ContainsKey(levelID))
		{
			sdPlayerInfo kPlayerInfo = SDNetGlobal.playerList[SDNetGlobal.lastSelectRole];
            string movieDialogue = sdConfDataMgr.Instance().GetRoleSetting("MovieDialogue_" + kPlayerInfo.mRoleName);
            return movieDialogue.Length == 0 || levelID > int.Parse(movieDialogue);
		}
		return false;
	}

    public void SetRoleSettingNoWrite(string key, string val)
    {
        roleSetting[key] = val;
    }
    public void SetRoleSetting(string key, string val)
    {
        if (key != "") roleSetting[key] = val;
        string info = "";
        foreach (DictionaryEntry item in roleSetting)
        {
            info += string.Format("{0},{1}\n", item.Key.ToString(), item.Value.ToString());
        }

        sdChatMsg.notifyUpdateConfig(info);
    }
    public string GetRoleSetting(string key)
    {
        if (roleSetting[key] == null) return "";
        return roleSetting[key].ToString();
    }

	public void SetSettingNoWrite(string key, string val)
	{
		settingTable[key] = val;
	}
	public void SetSetting(string key, string val)
	{
		if( key!="" ) settingTable[key] = val;
		string info= "key,value\r\n";
		foreach(DictionaryEntry item in settingTable)
		{
			info += string.Format("{0},{1}\r\n", item.Key.ToString(),item.Value.ToString());
		}
		
		FileStream write = new FileStream(Application.persistentDataPath + "//setting.txt",FileMode.Create);
		byte[] data	=	Encoding.UTF8.GetBytes(info);
		write.Write(data,0,data.Length);
		write.Close();
	}
	public string GetSetting(string key)
	{
		if (settingTable[key] == null) return "";
		return 	settingTable[key].ToString();
	}

	public Hashtable GetGuideList(string roleName)
	{
		Hashtable guideList = new Hashtable();
		Hashtable table = dataTable["guide"] as Hashtable;
		foreach(DictionaryEntry item in table)
		{
			Hashtable info = item.Value as Hashtable;
			string classId = info["classID"].ToString();
			string maxId = sdConfDataMgr.Instance().GetRoleSetting(("guide"+ classId));
			if (maxId == "") maxId = "0";
			if (int.Parse(item.Key.ToString()) <= int.Parse(maxId)) continue;
			
			sdGuide guide = new sdGuide();
			guide.id = int.Parse(item.Key.ToString());
			guide.classId = int.Parse(classId);
			guide.conType = (GuideConditionType)Enum.Parse(typeof(GuideConditionType), info["condition"].ToString());
			guide.opType = int.Parse(info["operation"].ToString());
			guide.eventType = (GuidePlayerEvetnType)Enum.Parse(typeof(GuidePlayerEvetnType), info["event"].ToString());
			guide.conParam = info["conditionParam"];
			guide.opParam = info["operationParam"];
			guide.eventParam = info["eventParam"];
			guide.isSave = (int.Parse(info["isSave"].ToString()) == 1) ? true : false;
            guide.isSkip = (int.Parse(info["isSkip"].ToString()) == 1) ? false : true;
            guide.isLock = (int.Parse(info["isLock"].ToString()) == 1) ? true : false;
			
			List<sdGuide> list = null;
			if (!guideList.ContainsKey(info["classID"].ToString()))
			{
				list = new List<sdGuide>();
				guideList[info["classID"].ToString()] = list;
			}
			else
			{
				list = guideList[info["classID"].ToString()] as List<sdGuide>;
			}
			list.Add(guide);
		}
		
		return guideList;
	}

	public DateTime ConvertServerTimeToClientTime(double dbMilliseconds)
	{
		DateTime openTime = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
		openTime = openTime.AddMilliseconds(dbMilliseconds);
		openTime = openTime.ToLocalTime();

		return openTime;
	}

	public string ConvertServerTimeToStrClientTime(double dbMilliseconds)
	{
		DateTime openTime = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
		openTime = openTime.AddMilliseconds(dbMilliseconds);
		openTime = openTime.ToLocalTime();
		string strTime = openTime.ToString("yyyy-MM-dd HH:mm:ss");

		return strTime;
	}

	public Hashtable GetSuit(string key)
	{
		Hashtable table = (Hashtable)dataTable["dmdssuitconfig.suits"];
		return table[key] as Hashtable;
	}

	public Hashtable GetSap(string key)
	{
		Hashtable table = (Hashtable)dataTable["dmdssapconfig.saps"];
		return table[key] as Hashtable;
	}

    public Hashtable GetGemLevel(string level)
    {
        Hashtable table = (Hashtable)dataTable["GemLevel"];
        return table[level] as Hashtable;
    }

	public bool OnModelPlayIdleAnm(GameObject obj)
	{
		if (obj!=null)
		{
			Animation anm = obj.GetComponent<Animation>();
			if (anm!=null)
			{
				AnimationState state = anm["idle01"];
				if (state!=null)
				{
					state.layer = 0;
					anm.Play("idle01");

					return true;
				}
			}
		}

		return false;
	}
	
	public bool OnModelClickRandomPlayAnm(GameObject obj)
	{
		if (obj!=null && obj.activeSelf==true)
		{
			Animation anm = obj.GetComponent<Animation>();
			if (anm!=null && anm.clip!=null && anm.clip.name=="idle01")
			{
				int iClipCount = anm.GetClipCount();
				if (iClipCount>1)
				{
					List<string> listAnmName = new List<string>();
					string strAnm = "";
                    foreach (AnimationState s in anm)
					{
                        if (s.name != "idle01")
							listAnmName.Add(s.name);
					}

					int iRandom = UnityEngine.Random.Range(0, listAnmName.Count);
					strAnm = listAnmName[iRandom];
                   
					if (strAnm!="" && strAnm!="idle01")
					{
						AnimationState state = anm[strAnm];
						if (state!=null)
						{
							state.layer = 11;
							state.wrapMode = WrapMode.Once;
							anm.Play(strAnm);

							return true;
						}
						
					}
				}
			}
		}

		return false;
	}

    public string GetColorHex(Color color)
    {
        int colorNum = 0;
        colorNum |= Mathf.RoundToInt(color.r * 255f) << 16;
        colorNum |= Mathf.RoundToInt(color.g * 255f) << 8;
        colorNum |= Mathf.RoundToInt(color.b * 255f) << 0;
        return NGUIMath.DecimalToHex(colorNum);
    }
    void CreateEffectOperation()
    {
        if (dataTable["operation"] != null)
        {
            Hashtable newTable = new Hashtable();
            Hashtable table = (Hashtable)dataTable["operation"];
            foreach (DictionaryEntry item in table)
            {
                int uid = int.Parse(item.Key as string);
                Hashtable group1 = (Hashtable)item.Value;
                sdEffectOperation.Operation op = new sdEffectOperation.Operation();
                sdConfDataMgr.ParseAllMember(op, group1);
                newTable[uid] = op;
            }
            dataTable["operation"] = newTable;
        }
    }

    public bool CanItemLevelUp(string id)
    {
        return ((Hashtable)dataTable["dmdscreatureconfig.itemprods"]).ContainsKey(id);
    }

    public Hashtable GetFormula(string tid)
    {
        if (CanItemLevelUp(tid))
        {
            return ((Hashtable)dataTable["dmdscreatureconfig.itemprods"])[tid] as Hashtable;
        }
        else
        {
            return null;
        }
    }
}
