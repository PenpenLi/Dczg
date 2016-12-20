using UnityEngine;
using System;
using System.Collections;
using System.Text;

public class sdFriendMgr : Singleton<sdFriendMgr>
{
	Hashtable friendList = new Hashtable();
	Hashtable tempList = new Hashtable();
	Hashtable fightFriList = new Hashtable();
	
	public int curPage = 0;
	public int maxPage = 0;
	public string lastSearchName = "";
	public bool isReceive = true;
	
	private string removeItem = "";
	
	public sdFriend searchFri = new sdFriend();
	public sdFriend fightFriend = null;

    public int sendEp = 0;
    public int sendEpMax = 0;
    public int getEp = 0;
    public int getEpMax = 0;
	
    public void Clear()
    {
        friendList.Clear();
        tempList.Clear();
        fightFriList.Clear();
    }

	public void CreateSearchFri(HeaderProto.SFriendInfo info)
	{
		searchFri.id = info.m_RoleDBID.ToString();
		searchFri.gender = info.m_Sex;
		searchFri.hairStyle = (byte)info.m_Head;
		searchFri.color = info.m_SkinColor;
		searchFri.name = Encoding.UTF8.GetString(info.m_Name).Trim('\0');
		searchFri.level = info.m_Level.ToString();
		searchFri.job = info.m_Job.ToString();
		searchFri.power = info.m_Attack.ToString();
		searchFri.isOnline = info.m_IsOnline == 1 ? true : false;
		searchFri.point = (int)info.m_ActionPoint;
		searchFri.canSend = info.m_CanDonateActionPoint == 1 ? true : false;
		HeaderProto.SRSRoleEquipData equip = info.m_Equip;
		int itemNum = equip.m_Count;
		for(int i = 0; i < itemNum; ++i)
		{
			searchFri.equipList.Add((uint)equip.m_EquipInfo[i].m_TID);
		}
		
		HeaderProto.SRSRolePetData pet = info.m_Pet;
		int petNum = pet.m_Count;
		for(int i = 0; i < petNum; ++i)
		{
            SClientPetInfo petInfo = new SClientPetInfo();
            petInfo.m_uiTemplateID = (uint)(pet.m_PetInfo[i].m_TemplateID);
			searchFri.petList.Add(petInfo);
		}
	}
	
	public void ClearFightFri()
	{
		fightFriList.Clear();	
	}
	
	public Hashtable GetFightList()
	{
		Hashtable table = new Hashtable();
		int num = 0;
		foreach(DictionaryEntry item in fightFriList)
		{
			sdFriend fri = item.Value as sdFriend;
			if (fri.isFri && num < 10)
			{
				table.Add(fri.id, fri);
				++num;
			}
		}
		
		foreach(DictionaryEntry item in fightFriList)
		{
			sdFriend fri = item.Value as sdFriend;
			if (!fri.isFri && num < 15)
			{
				table.Add(fri.id, fri);
				++num;
			}
		}
		
		return table;	
	}
	
	public void RemoveFightFri(string id)
	{
		fightFriList.Remove(id);	
	}
	
	public void CreateFightFri(CliProto.SAssistBattleInfo info)
	{
		sdFriend fightFri = new sdFriend();
		fightFri.id = info.m_Assistor.m_Info.m_RoleDBID.ToString();
		fightFri.gender = info.m_Assistor.m_Info.m_Sex;
		fightFri.hairStyle = (byte)info.m_Assistor.m_Info.m_Head;
		fightFri.color = info.m_Assistor.m_Info.m_SkinColor;
		fightFri.name = Encoding.UTF8.GetString(info.m_Assistor.m_Info.m_Name).Trim('\0');
		fightFri.level = info.m_Assistor.m_Info.m_Level.ToString();
		fightFri.job = info.m_Assistor.m_Info.m_Job.ToString();
		fightFri.power = info.m_Assistor.m_Info.m_Attack.ToString();
		fightFri.isOnline = info.m_Assistor.m_Info.m_IsOnline == 1 ? true : false;
		fightFri.point = (int)info.m_Assistor.m_Info.m_ActionPoint;
		fightFri.canSend = info.m_Assistor.m_Info.m_CanDonateActionPoint == 1 ? true : false;
		fightFri.isFri = info.m_Assistor.m_IsFriend == 1 ? true : false;
		HeaderProto.SRSRoleEquipData equip = info.m_Assistor.m_Info.m_Equip;
		int itemNum = equip.m_Count;
		for(int i = 0; i < itemNum; ++i)
		{
			fightFri.equipList.Add((uint)equip.m_EquipInfo[i].m_TID);
		}
		
		CliProto.SPetInfo petInfo = info.m_BattlePet;
		if (petInfo != null)
		{
			fightFri.petInfo.m_uuDBID = petInfo.m_DBID;
			fightFri.petInfo.m_iBattlePos = petInfo.m_BattlePos;
			fightFri.petInfo.m_uiTemplateID = petInfo.m_TemplateID;	
			fightFri.petInfo.m_iLevel = petInfo.m_Level;
			fightFri.petInfo.m_uuExperience = (UInt64)petInfo.m_Experience;
			fightFri.petInfo.m_iUp = petInfo.m_Up;
			fightFri.petInfo.m_iHP = petInfo.m_HP;
			fightFri.petInfo.m_iSP = petInfo.m_SP;
			
			fightFri.petInfo.m_CurProperty.m_iStr = petInfo.m_Str;
			fightFri.petInfo.m_CurProperty.m_iInt = petInfo.m_Int;
			fightFri.petInfo.m_CurProperty.m_iDex = petInfo.m_Dex;
			fightFri.petInfo.m_CurProperty.m_iSta = petInfo.m_Sta;
			fightFri.petInfo.m_CurProperty.m_iFai = petInfo.m_Fai;
			fightFri.petInfo.m_CurProperty.m_iMaxHP = petInfo.m_MaxHP;
			fightFri.petInfo.m_CurProperty.m_iMaxSP = petInfo.m_MaxSP;
			fightFri.petInfo.m_CurProperty.m_iHPTick = petInfo.m_HPTick;
		    fightFri.petInfo.m_CurProperty.m_iSPTick = petInfo.m_SPTick;
		    fightFri.petInfo.m_CurProperty.m_iAtkDmgMin = petInfo.m_AtkDmgMin;
		    fightFri.petInfo.m_CurProperty.m_iAtkDmgMax = petInfo.m_AtkDmgMax;
		    fightFri.petInfo.m_CurProperty.m_iDef = petInfo.m_Def;
		    fightFri.petInfo.m_CurProperty.m_iIceAtt = petInfo.m_IceAtt;
		    fightFri.petInfo.m_CurProperty.m_iFireAtt = petInfo.m_FireAtt;
		    fightFri.petInfo.m_CurProperty.m_iPoisonAtt = petInfo.m_PoisonAtt;
		    fightFri.petInfo.m_CurProperty.m_iThunderAtt = petInfo.m_ThunderAtt;
		    fightFri.petInfo.m_CurProperty.m_iIceDef = petInfo.m_IceDef;
		    fightFri.petInfo.m_CurProperty.m_iFireDef = petInfo.m_FireDef;
		    fightFri.petInfo.m_CurProperty.m_iPoisonDef = petInfo.m_PoisonDef;
		    fightFri.petInfo.m_CurProperty.m_iThunderDef = petInfo.m_ThunderDef;
		    fightFri.petInfo.m_CurProperty.m_iPierce = petInfo.m_Pierce;
		    fightFri.petInfo.m_CurProperty.m_iHit = petInfo.m_Hit;
		    fightFri.petInfo.m_CurProperty.m_iDodge = petInfo.m_Dodge;
		    fightFri.petInfo.m_CurProperty.m_iCri = petInfo.m_Cri;
		    fightFri.petInfo.m_CurProperty.m_iFlex = petInfo.m_Flex;
		    fightFri.petInfo.m_CurProperty.m_iCriDmg = petInfo.m_CriDmg;
		    fightFri.petInfo.m_CurProperty.m_iCriDmgDef = petInfo.m_CriDmgDef;
		    fightFri.petInfo.m_CurProperty.m_iBodySize = petInfo.m_BodySize;
		    fightFri.petInfo.m_CurProperty.m_iAttSize = petInfo.m_AttSize;
		    fightFri.petInfo.m_CurProperty.m_iAttSpeedModPer = petInfo.m_AttSpeedModPer;
		    fightFri.petInfo.m_CurProperty.m_iMoveSpeedModPer = petInfo.m_MoveSpeedModPer;
		    fightFri.petInfo.m_CurProperty.m_iPiercePer = petInfo.m_PiercePer;
		    fightFri.petInfo.m_CurProperty.m_iHitPer = petInfo.m_HitPer;
		    fightFri.petInfo.m_CurProperty.m_iDodgePer = petInfo.m_DodgePer;
		    fightFri.petInfo.m_CurProperty.m_iCriPer = petInfo.m_CriPer;
		    fightFri.petInfo.m_CurProperty.m_iFlexPer = petInfo.m_FlexPer;
		    fightFri.petInfo.m_CurProperty.m_iAttSpeed = petInfo.m_AttSpeed;
		    fightFri.petInfo.m_CurProperty.m_iMoveSpeed = petInfo.m_MoveSpeed;
			uint uiTemplateID = petInfo.m_TemplateID;
			Hashtable table = sdConfDataMgr.Instance().GetPetTemplate(uiTemplateID.ToString());
			if (table != null)
			{
				if (table["Name"].ToString() != "")
				{
					fightFri.petInfo.m_strName = table["Name"].ToString();
				}
				
				if (table["Ability"].ToString() != "")
				{
					fightFri.petInfo.m_iAbility = int.Parse(table["Ability"].ToString());
				}
				
				if (table["BodyType"].ToString() != "")
				{
					fightFri.petInfo.m_iBodyType = int.Parse(table["BodyType"].ToString());
				}
				
				if (table["KnockDownDef"].ToString() != "")
				{
					fightFri.petInfo.m_iKnockDownDef = int.Parse(table["KnockDownDef"].ToString());
				}
				
				if (table["KnockFlyDef"].ToString() != "")
				{
					fightFri.petInfo.m_iKnockFlyDef = int.Parse(table["KnockFlyDef"].ToString());
				}
				
				if (table["KnockBackDef"].ToString() != "")
				{
					fightFri.petInfo.m_iKnockBackDef = int.Parse(table["KnockBackDef"].ToString());
				}
				
				if (table["EyeSize"].ToString() != "")
				{
					fightFri.petInfo.m_iEyeSize = int.Parse(table["EyeSize"].ToString());
				}
				
				if (table["ChaseSize"].ToString() != "")
				{
					fightFri.petInfo.m_iChaseSize = int.Parse(table["ChaseSize"].ToString());
				}

				if (table["DfSkill"].ToString() != "")
				{
					fightFri.petInfo.m_iDfSkill = int.Parse(table["DfSkill"].ToString());
				}
				
				if (table["SpSkill"].ToString() != "")
				{
					fightFri.petInfo.m_iSpSkill = int.Parse(table["SpSkill"].ToString());
				}
				
				if (table["SapSkill"].ToString() != "")
				{
					fightFri.petInfo.m_iSapSkill = int.Parse(table["SapSkill"].ToString());
				}
				
				//模板属性aa
				if (table["Property.Str"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iStr = int.Parse(table["Property.Str"].ToString());
				}
				if (table["Property.Int"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iInt = int.Parse(table["Property.Int"].ToString());
				}
				if (table["Property.Dex"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iDex = int.Parse(table["Property.Dex"].ToString());
				}
				if (table["Property.Sta"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iSta = int.Parse(table["Property.Sta"].ToString());
				}
				if (table["Property.Fai"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iFai = int.Parse(table["Property.Fai"].ToString());
				}
				if (table["Property.MaxHP"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iMaxHP = int.Parse(table["Property.MaxHP"].ToString());
				}
				if (table["Property.MaxSP"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iMaxSP = int.Parse(table["Property.MaxSP"].ToString());
				}
				if (table["Property.HPTick"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iHPTick = int.Parse(table["Property.HPTick"].ToString());
				}
				if (table["Property.SPTick"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iSPTick = int.Parse(table["Property.SPTick"].ToString());
				}
				if (table["Property.AtkDmgMin"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iAtkDmgMin = int.Parse(table["Property.AtkDmgMin"].ToString());
				}
				if (table["Property.AtkDmgMax"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iAtkDmgMax = int.Parse(table["Property.AtkDmgMax"].ToString());
				}
				if (table["Property.Def"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iDef = int.Parse(table["Property.Def"].ToString());
				}
				if (table["Property.IceAtt"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iIceAtt = int.Parse(table["Property.IceAtt"].ToString());
				}
				if (table["Property.FireAtt"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iFireAtt = int.Parse(table["Property.FireAtt"].ToString());
				}
				if (table["Property.PoisonAtt"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iPoisonAtt = int.Parse(table["Property.PoisonAtt"].ToString());
				}
				if (table["Property.ThunderAtt"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iThunderAtt = int.Parse(table["Property.ThunderAtt"].ToString());
				}
				if (table["Property.IceDef"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iIceDef = int.Parse(table["Property.IceDef"].ToString());
				}
				if (table["Property.FireDef"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iFireDef = int.Parse(table["Property.FireDef"].ToString());
				}
				if (table["Property.PoisonDef"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iPoisonDef = int.Parse(table["Property.PoisonDef"].ToString());
				}
				if (table["Property.ThunderDef"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iThunderDef = int.Parse(table["Property.ThunderDef"].ToString());
				}
				if (table["Property.Pierce"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iPierce = int.Parse(table["Property.Pierce"].ToString());
				}
			    if (table["Property.Hit"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iHit = int.Parse(table["Property.Hit"].ToString());
				}
				if (table["Property.Dodge"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iDodge = int.Parse(table["Property.Dodge"].ToString());
				}
			    if (table["Property.Cri"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iCri = int.Parse(table["Property.Cri"].ToString());
				}
				if (table["Property.Flex"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iFlex = int.Parse(table["Property.Flex"].ToString());
				}
				if (table["Property.CriDmg"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iCriDmg = int.Parse(table["Property.CriDmg"].ToString());
				}
			    if (table["Property.CriDmgDef"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iCriDmgDef = int.Parse(table["Property.CriDmgDef"].ToString());
				}
				if (table["Property.BodySize"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iBodySize = int.Parse(table["Property.BodySize"].ToString());
				}
			    if (table["Property.AttSize"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iAttSize = int.Parse(table["Property.AttSize"].ToString());
				}
				if (table["Property.AttSpeedModPer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iAttSpeedModPer = int.Parse(table["Property.AttSpeedModPer"].ToString());
				}
				if (table["Property.MoveSpeedModPer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iMoveSpeedModPer = int.Parse(table["Property.MoveSpeedModPer"].ToString());
				}
				if (table["Property.PiercePer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iPiercePer = int.Parse(table["Property.PiercePer"].ToString());
				}
				if (table["Property.HitPer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iHitPer = int.Parse(table["Property.HitPer"].ToString());
				}
				if (table["Property.DodgePer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iDodgePer = int.Parse(table["Property.DodgePer"].ToString());
				}
				if (table["Property.CriPer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iCriPer = int.Parse(table["Property.CriPer"].ToString());
				}
				if (table["Property.FlexPer"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iFlexPer = int.Parse(table["Property.FlexPer"].ToString());
				}
			    if (table["Property.AttSpeed"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iAttSpeed = int.Parse(table["Property.AttSpeed"].ToString());
				}
				if (table["Property.MoveSpeed"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateProperty.m_iMoveSpeed = int.Parse(table["Property.MoveSpeed"].ToString());
				}
			    //属性计算参数aa
			    if (table["Coe.AttA"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iAttA = int.Parse(table["Coe.AttA"].ToString());
				}
				if (table["Coe.AttB"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iAttB = int.Parse(table["Coe.AttB"].ToString());
				}
				if (table["Coe.AttC"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iAttC = int.Parse(table["Coe.AttC"].ToString());
				}
				if (table["Coe.DefA"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iDefA = int.Parse(table["Coe.DefA"].ToString());
				}
				if (table["Coe.DefB"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iDefB = int.Parse(table["Coe.DefB"].ToString());
				}
				if (table["Coe.DefC"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iDefC = int.Parse(table["Coe.DefC"].ToString());
				}
				if (table["Coe.HPA"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iHPA = int.Parse(table["Coe.HPA"].ToString());
				}
				if (table["Coe.HPB"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iHPB = int.Parse(table["Coe.HPB"].ToString());
				}
				if (table["Coe.HPC"].ToString() != "")
				{
					fightFri.petInfo.m_TemplateCoe.m_iHPC = int.Parse(table["Coe.HPC"].ToString());
				}
				//其他信息aa
				if (table["BaseJob"].ToString() != "")
				{
					fightFri.petInfo.m_iBaseJob = int.Parse(table["BaseJob"].ToString());
				}
				if (table["Desc"].ToString() != "")
				{
					fightFri.petInfo.m_strDesc = table["Desc"].ToString();
				}
				if (table["SPD1"].ToString() != "")
				{
					fightFri.petInfo.m_strSPD1 = table["SPD1"].ToString();
				}
				if (table["SPD2"].ToString() != "")
				{
					fightFri.petInfo.m_strSPD2 = table["SPD2"].ToString();
				}
				if (table["Res"].ToString() != "")
				{
					fightFri.petInfo.m_strRes = table["Res"].ToString();
				}
				if (table["Skill1"].ToString() != "")
				{
					fightFri.petInfo.m_iSkill1 = int.Parse(table["Skill1"].ToString());
				}
			    if (table["Skill2"].ToString() != "")
				{
					fightFri.petInfo.m_iSkill2 = int.Parse(table["Skill2"].ToString());
				}
				if (table["Skill3"].ToString() != "")
				{
					fightFri.petInfo.m_iSkill3 = int.Parse(table["Skill3"].ToString());
				}
				if (table["Skill4"].ToString() != "")
				{
					fightFri.petInfo.m_iSkill4 = int.Parse(table["Skill4"].ToString());
				}
				if (table["Buff1"].ToString() != "")
				{
					fightFri.petInfo.m_iBuff1 = int.Parse(table["Buff1"].ToString());
				}
			    if (table["Buff2"].ToString() != "")
				{
					fightFri.petInfo.m_iBuff2 = int.Parse(table["Buff2"].ToString());
				}
				if (table["Buff3"].ToString() != "")
				{
					fightFri.petInfo.m_iBuff3 = int.Parse(table["Buff3"].ToString());
				}
				if (table["Buff4"].ToString() != "")
				{
					fightFri.petInfo.m_iBuff4 = int.Parse(table["Buff4"].ToString());
				}
				if (table["AIID"].ToString() != "")
				{
					fightFri.petInfo.m_iAIID = int.Parse(table["AIID"].ToString());
				}
				if (table["Icon"].ToString() != "")
				{
					fightFri.petInfo.m_strIcon = table["Icon"].ToString();
				}
			}
			//宠物装备..
			fightFri.petInfo.m_EquipedDB.Clear();
			int count = info.m_BattlePet.m_Equip.m_ItemCount;
			for(int j = 0; j < count; ++j)
			{
				sdGamePetItem item = new sdGamePetItem();
				item.templateID = info.m_BattlePet.m_Equip.m_Items[j].m_TID;
				item.instanceID = info.m_BattlePet.m_Equip.m_Items[j].m_UID;
				item.count = info.m_BattlePet.m_Equip.m_Items[j].m_CT;
				Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
				if (itemInfo != null)
				{
					item.mdlPath = itemInfo["Filename"].ToString();
					item.mdlPartName = itemInfo["FilePart"].ToString();
					item.anchorNodeName	= sdGameActor.WeaponDummy(itemInfo["Character"].ToString());
					item.itemClass = int.Parse(itemInfo["Class"].ToString());
					item.subClass = int.Parse(itemInfo["SubClass"].ToString());
					item.level = int.Parse(itemInfo["Level"].ToString());
					item.quility = int.Parse(itemInfo["Quility"].ToString());
				}
				
				fightFri.petInfo.m_EquipedDB[item.instanceID] = item;
			}
		}
	
		
		if (fightFriList.ContainsKey(fightFri.id))
		{
			fightFriList[fightFri.id] = fightFri;
		}
		else
		{
			fightFriList.Add(fightFri.id, fightFri);
		}
	}
	
	public void CreateTempFriend(HeaderProto.SFriendInfo info)
	{
		sdFriend friend = new sdFriend();
		friend.id = info.m_RoleDBID.ToString();
		friend.gender = info.m_Sex;
		friend.hairStyle = (byte)info.m_Head;
		friend.color = info.m_SkinColor;
		friend.name = Encoding.UTF8.GetString(info.m_Name).Trim('\0');
		friend.level = info.m_Level.ToString();
		friend.job = info.m_Job.ToString();
		friend.power = info.m_Attack.ToString();
		friend.isOnline = info.m_IsOnline == 1 ? true : false;
		friend.point = (int)info.m_ActionPoint;
		friend.canSend = info.m_CanDonateActionPoint == 1 ? true : false;
		HeaderProto.SRSRoleEquipData equip = info.m_Equip;
		int itemNum = equip.m_Count;
		for(int i = 0; i < itemNum; ++i)
		{
			friend.equipList.Add((uint)equip.m_EquipInfo[i].m_TID);
		}
		
		HeaderProto.SRSRolePetData pet = info.m_Pet;
		int petNum = pet.m_Count;
		for(int i = 0; i < petNum; ++i)
		{
            SClientPetInfo petInfo = new SClientPetInfo();
            petInfo.m_uiTemplateID = (uint)(pet.m_PetInfo[i].m_TemplateID);
			friend.petList.Add(petInfo);
		}
		
		friend.index = tempList.Count;
		
		if (tempList.ContainsKey(friend.id))
		{
			tempList[friend.id] = friend;
		}
		else
		{
			tempList.Add(friend.id, friend);
		}
		
		
	}
	
	public void CreateFriend(HeaderProto.SFriendInfo info)
	{
        sdFriend friend = GetFriend(info.m_RoleDBID.ToString());
        if (friend == null)friend = new sdFriend();
		friend.id = info.m_RoleDBID.ToString();
		friend.gender = info.m_Sex;
		friend.hairStyle = (byte)info.m_Head;
		friend.color = info.m_SkinColor;
		friend.name = Encoding.UTF8.GetString(info.m_Name).Trim('\0');
		friend.level = info.m_Level.ToString();
		friend.job = info.m_Job.ToString();
		friend.power = info.m_Attack.ToString();
		friend.isOnline = info.m_IsOnline == 1 ? true : false;
		friend.point = (int)info.m_ActionPoint;
		friend.canSend = info.m_CanDonateActionPoint == 1 ? true : false;
		HeaderProto.SRSRoleEquipData equip = info.m_Equip;
		int itemNum = equip.m_Count;
		for(int i = 0; i < itemNum; ++i)
		{
			friend.equipList.Add((uint)equip.m_EquipInfo[i].m_TID);
		}
		
		HeaderProto.SRSRolePetData pet = info.m_Pet;
		int petNum = pet.m_Count;
		for(int i = 0; i < petNum; ++i)
		{
            SClientPetInfo petInfo = new SClientPetInfo();
            petInfo.m_uiTemplateID = (uint)(pet.m_PetInfo[i].m_TemplateID);
			friend.petList.Add(petInfo);
		}
		if (friendList.ContainsKey(friend.id))
		{
			friendList[friend.id] = friend;
		}
		else
		{
			friendList.Add(friend.id, friend);
		}
	}


    public bool IsFriend(string strName)
    {
        foreach (DictionaryEntry item in friendList)
        {
            sdFriend friend = item.Value as sdFriend;
            if (friend.name == strName)
                return true;
        }
        return false;
    }

	public int GetFriendNum()
	{
		return friendList.Count;	
	}
	
	public int GetTempFriNum()
	{
		return tempList.Count;
	}
	
	public Hashtable GetTempFriends()
	{
		return tempList;	
	}
	
	public sdFriend GetTempFriend(string id)
	{
		return tempList[id] as sdFriend;
	}
		
	public Hashtable GetFriends()
	{
		return friendList;	
	}
	
	public Hashtable GetOnlineFriends()
	{
		Hashtable table = new Hashtable();
		foreach(DictionaryEntry item in friendList)
		{
			sdFriend friend = item.Value as sdFriend;
			if (friend.isOnline) table.Add(friend.id, friend);
		}
		
		return table;
	}
	
	public int GetOnlineFriendsNum()
	{
		int ret = 0;
		foreach(DictionaryEntry item in friendList)
		{
			sdFriend friend = item.Value as sdFriend;
			if (friend.isOnline) ++ret;
		}
		
		return ret;
	}
	
	public sdFriend GetFriend(string id)
	{
		return friendList[id] as sdFriend;
	}
	
	public void RemoveInviteFri(string id)
	{
		tempList.Remove(id);	
	}
	
	public void RemoveFriend(string id)
	{
		removeItem = id; 
	}
	
	public void ConfirmRemove()
	{
		friendList.Remove(removeItem);
	}
}
