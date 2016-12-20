using UnityEngine;
using System.Collections;

// 关卡数据.
public class LevelItem
{
	public int			levelID;
	public int			crystal		= 0;
	public ushort		usedTimes	= 0;
	public bool			valid		= false;
	public Hashtable	levelProp	= new Hashtable();
}

// 奖励宝箱数据.
public class BattleBoxItem
{
	public uint			BoxID		= 0;		// 奖励宝箱ID.
	public uint			Job			= 0;		// 此宝箱记录对应的职业.	
	public uint			NeedStar	= 0;		// 领奖需求的星数.	
	public uint			NonMoney	= 0;
	public uint			NonCash		= 0;
	public uint			Item1ID		= 0;
	public uint			Item1Count	= 0;
	public uint			Item2ID		= 0;
	public uint			Item2Count	= 0;
	public uint			Item3ID		= 0;
	public uint			Item3Count	= 0;
	public bool			IsTake		= false;	// 此宝箱是否已经被拿过.
}

// 战役数据.
public class BattleItem
{
	public int				starCount	= 0;		// 此战役已经获取的星数.
	public BattleBoxItem[,]	rewardBox	= new BattleBoxItem[3,5];	// 奖励宝箱数据，每个战役有3个奖励宝箱，每个职业有一个宝箱配置（和一个通用配置）.
	public int				difficulty	= 1;		// 此关卡当前选择的难度.
}

// 关卡信息结构.
public class sdLevelInfo
{	
	public static LevelItem[]	levelInfos	= null;					// 关卡数据.
	public static BattleItem[]	battleInfos	= new BattleItem[17];	// 战役数据，目前总共16个战役，从1开始.
	public static bool			NeedReflash	= false;				// 是否需要更新关卡显示数据.
	private static int 			curId		= 0;

	public static int BattleInfoID(int bid)
	{
		return (bid/10-1)*4 + bid%10;	
	}

	public static void Clear()
	{
		if( levelInfos != null )
		{
            for (int i = 0; i < levelInfos.Length; i++)
            {
                if (levelInfos[i] != null)
                {
                    levelInfos[i].valid = false;
                }
            }
		}
        if (battleInfos != null)
        {
            for (int i = 0; i < battleInfos.Length; i++)
            {
                if (battleInfos[i] != null)
                {
                    battleInfos[i].starCount = 0;
                }
            }
        }
	}
	
	public static string GetLevelShowName(int levelId)
	{
		if(levelInfos == null)
			return "";
		for(int i = 0; i < levelInfos.Length; i++)
		{
			if(levelInfos[i].levelID == levelId)
			{
				return (string)levelInfos[i].levelProp["ShowName"];
			}
		}
		
		return "";
	}
	
	public static int ReliveLimit(int levelId)
	{
		if(levelInfos == null)
			return 0;
		for(int i = 0; i < levelInfos.Length; i++)
		{
			if(levelInfos[i].levelID == levelId)
			{
				Hashtable	t	=	levelInfos[i].levelProp;
				object obj	=	t["ReliveLimit"];
				if(obj!=null)
				{
					return (int)obj;
				}
				else
				{
					return 3;
				}
			}
		}
		
		return 3;	
	}
	
	public static void SetCurLevelId(int id)
	{
		curId = id;
	}
	
	public static int GetCurLevelId()
	{
		return curId;	
	}
	
	public static int ReliveCash(int levelId)
	{
		if(levelInfos == null)
			return 0;
		for(int i = 0; i < levelInfos.Length; i++)
		{
			if(levelInfos[i].levelID == levelId)
			{
				object obj = levelInfos[i].levelProp["ReliveCash"];
				if(obj!=null)
				{
					return (int)obj;
				}
				else
				{
					return -2;
				}
			}
		}
		
		return 0;	
	}
	
	public static bool GetLevelValid(int levelId)
	{
		if(levelInfos == null)
			return false;
		for(int i = 0; i < levelInfos.Length; i++)
		{
			if(levelInfos[i].levelID == levelId)
			{
				return levelInfos[i].valid;
			}
		}
		
		return false;
	}
	
	public static void SetLevelValid(int levelId)
	{
		if(levelInfos == null) return;
		for(int i = 0; i < levelInfos.Length; i++)
		{
			if( levelInfos[i].levelID == levelId )
			{
				/*
				if( levelInfos[i].crystal < 1 )
				{
					levelInfos[i].crystal = 1;
					int battleID = (sdUICharacter.Instance.iLastCampaignID/10-1)*4 + (sdUICharacter.Instance.iLastCampaignID%10);
					battleInfos[battleID].starCount++;
				}
				*/
			}
			else if( (int)levelInfos[i].levelProp["PrecedentID"] == levelId )
			{
				levelInfos[i].valid = true;	
			}
		}
	}
	
	public static void OnMessage_SC_LEVEL_INFO(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_LEVEL_INFO refMSG = (CliProto.SC_LEVEL_INFO)msg;
		
		if(refMSG.m_Count > 0)
		{
			for(int i = 0; i < refMSG.m_Count; i++)
			{
				int levelID = (int)refMSG.m_Info[i].m_LevelID;
				int crystal = (int)refMSG.m_Info[i].m_Crystal;
				ushort usedTimes = refMSG.m_Info[i].m_UsedTimes;
				
				for(int j = 0; j < levelInfos.Length; j++)
				{
					if(levelID == levelInfos[j].levelID)
					{
						levelInfos[j].crystal = crystal;
						levelInfos[j].usedTimes = usedTimes;
						levelInfos[j].valid = true;
						break;
					}
				}
			}
			
			// 更新已开启的战役ID.
			int level = 11011;
			for(int j = 0; j < levelInfos.Length; j++)
			{
				if( levelInfos[j].valid && levelInfos[j].levelID<100000 && levelInfos[j].levelID>level )
					level = levelInfos[j].levelID;
			}
			sdWorldMapPath.SetLevel(level/1000,false);
		}
		
		if(refMSG.m_BattleCount > 0)
		{
			for(int i = 0; i < refMSG.m_BattleCount; i++)
			{
				int bid = (int)refMSG.m_BtInfo[i].m_BattleID;
				if( bid>0 && bid<=16 )
				{
					battleInfos[bid].starCount = refMSG.m_BtInfo[i].m_GetStars;
				}
			}
		}
		
		if(refMSG.m_BoxCount > 0)
		{
			for(int i = 0; i < refMSG.m_BoxCount; i++)
			{
				uint bid = refMSG.m_GotBattleBox[i];
				for(int j=1;j<=16;j++)
				{
					for(int i1=0;i1<3;i1++)
					{
						for(int i2=0;i2<5;i2++)
						{
							BattleBoxItem box = battleInfos[j].rewardBox[i1,i2];
							if( box!=null && box.BoxID==bid )
							{
								box.IsTake = true;
								j = i1 = i2 = 100;
							}
						}
					}
				}
			}
		}
	}
	
	public static void OnMessage_SC_BATTLE_GOT_STAR_NTF(int iMsgID, ref CMessage msg)
	{
		/*
		CliProto.SC_BATTLE_GOT_STAR_NTF refMSG = (CliProto.SC_BATTLE_GOT_STAR_NTF)msg;
		if( refMSG.m_BattleID>0 && refMSG.m_BattleID<=16 )
		{
			battleInfos[refMSG.m_BattleID].starCount = (int)refMSG.m_GotStar;
		}
		*/
	}
	
	public static void AskRewardBox(uint bid)
	{
		CliProto.CS_GET_BATTLE_BOX_REQ refMSG = new CliProto.CS_GET_BATTLE_BOX_REQ();
		refMSG.m_BattleboxID = bid;
		SDNetGlobal.SendMessage(refMSG);
	}
	
	public static void OnMessage_SC_GET_BATTLE_BOX_ACK(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_GET_BATTLE_BOX_ACK refMSG = (CliProto.SC_GET_BATTLE_BOX_ACK)msg;
		if( refMSG.m_Result == (byte)HeaderProto.EReturnType.GS_RESULT_YES )
		{
			sdUICharacter.Instance.ShowSuccessPanel();
			
			uint bid = refMSG.m_BattleboxID;
			for(int j=1;j<=16;j++)
			{
				for(int i1=0;i1<3;i1++)
				{
					for(int i2=0;i2<5;i2++)
					{
						BattleBoxItem box = battleInfos[j].rewardBox[i1,i2];
						if( box!=null && box.BoxID==bid )
						{
							NeedReflash = true;
							box.IsTake = true;
							j = i1 = i2 = 100;
						}
					}
				}
			}
		}
	}
	
	public static void LoadLevelConfig(string level,string levelLang,string levelMeta,string levelBox)
	{
		SDCSV levelCSV		= new SDCSV();
		SDCSV levelLangCSV	= new SDCSV();
		SDCSV levelMetaCSV	= new SDCSV();
		SDCSV levelBoxCSV	= new SDCSV();
		
		levelCSV.CreateCSVFromStr(level,0);
		levelLangCSV.CreateCSVFromStr(levelLang,0);
		levelMetaCSV.CreateCSVFromStr(levelMeta,1);
		levelBoxCSV.CreateCSVFromStr(levelBox,0);
		
		//首先遍历level表的每一行，每一行又是一个HashTable...
		levelInfos = new LevelItem[levelCSV.csvTable.Count];
		int count = 0;
		foreach(DictionaryEntry de in levelCSV.csvTable)
		{
			levelInfos[count] = new LevelItem();
			string key = (string)de.Key;
			Hashtable valTable = (Hashtable)de.Value;
			
			levelInfos[count].levelID = int.Parse(key);
			//然后遍历行中的每一列，每一列是一个string
			foreach(DictionaryEntry de2 in valTable)
			{
				//获得名字..
				string name = (string)de2.Key;
				string val = (string)de2.Value;
				//通过名字查找metaTable，获得数据类型..
				Hashtable meta = (Hashtable)levelMetaCSV.csvTable[name];
				string type = (string)meta["Type"];
				
				if(type == "int")
				{
					levelInfos[count].levelProp.Add(name,int.Parse(val));
				}
				else
				{
					levelInfos[count].levelProp.Add(name,val);
				}	
			}
			
			//读取国际化表对应的行..
			Hashtable langHash = (Hashtable)levelLangCSV.csvTable[key];
            if (langHash != null)
            {
                foreach (DictionaryEntry de3 in langHash)
                {
                    //获得名字..
                    string name = (string)de3.Key;
                    string val = (string)de3.Value;
                    //通过名字查找metaTable，获得数据类型..
                    Hashtable meta = (Hashtable)levelMetaCSV.csvTable[name];
                    string type = (string)meta["Type"];

                    if (type == "int")
                    {
                        levelInfos[count].levelProp[name] = int.Parse(val);
                    }
                    else
                    {
                        levelInfos[count].levelProp[name] = val;
                    }
                }
            }			
			count++;
		}
		
		for(count=1;count<=16;count++)
			battleInfos[count] = new BattleItem();
		
		// 处理关卡领奖数据结构.
		foreach(DictionaryEntry de in levelBoxCSV.csvTable)
		{
			string key = (string)de.Key;
			Hashtable valTable = (Hashtable)de.Value;
			
			uint bid = 0;
			BattleBoxItem box = new BattleBoxItem();
			box.BoxID = uint.Parse(key);
			
			foreach(DictionaryEntry de2 in valTable)
			{
				string name = (string)de2.Key;
				string val = (string)de2.Value;
				if( name == "BattleID" )
					bid = uint.Parse(val);
				else if( name == "Job" )
					box.Job = uint.Parse(val);
				else if( name == "NeedStar" )
					box.NeedStar = uint.Parse(val);
				else if( name == "NonMoney" )
					box.NonMoney = uint.Parse(val);
				else if( name == "NonCash" )
					box.NonCash = uint.Parse(val);
				else if( name == "i1.TID" )
					box.Item1ID = uint.Parse(val);
				else if( name == "i1.Count" )
					box.Item1Count = uint.Parse(val);
				else if( name == "i2.TID" )
					box.Item2ID = uint.Parse(val);
				else if( name == "i2.Count" )
					box.Item2Count = uint.Parse(val);
				else if( name == "i3.TID" )
					box.Item3ID = uint.Parse(val);
				else if( name == "i3.Count" )
					box.Item3Count = uint.Parse(val);
			}
			
			// 总计16个战役，从1开始.
			if( bid>=1 && bid<=16 )
			{
				if( box.NeedStar <= 18 )		count = 0;
				else if( box.NeedStar <= 36 )	count = 1;
				else 							count = 2;
				
				int job = 0;
				if( box.Job == 1 )		job = 1;	// 战士.
				else if( box.Job == 4 )	job = 2;	// 法师.
				else if( box.Job == 7 )	job = 3;	// 游侠.
				else if( box.Job == 10)	job = 4;	// 牧师.
				
				//battleInfos[bid].rewardBox[count,job] = new BattleBoxItem();
				battleInfos[bid].rewardBox[count,job] = box;
			}
		}
	}
    public static void BeginLevel()
    {
//		for (int j = 0; j < levelInfos.Length; j++)
//		{
//			if (curId == levelInfos[j].levelID)
//			{
//				levelInfos[j].usedTimes++;
//				break;
//			}
//		}
    }

    public static void EndLevel(uint levelid,int stars)
    {
        for (int j = 0; j < levelInfos.Length; j++)
        {
			if (levelid == levelInfos[j].levelID)
            {
				levelInfos[j].valid = true;
				levelInfos[j].usedTimes++;	//< 关卡进入次数自增aa
				if( stars > levelInfos[j].crystal )
				{
					int battleID = (sdUICharacter.Instance.iLastCampaignID/10-1)*4 + (sdUICharacter.Instance.iLastCampaignID%10);
					battleInfos[battleID].starCount = battleInfos[battleID].starCount - levelInfos[j].crystal + stars;
					levelInfos[j].crystal = stars;
				}
				break;
            }
        }
    }
}
