using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct stPVPRival
{
	//eTitleLevel
	public string  strName;
    public uint nLevel;
    public int nMilitaryLevel;
    public uint nScore;
    public int nSex;
    public int nProfession;
    public int hairstyle;
    public int haircolor;
    public ulong roleID;
}
public enum eCountDownType
{
    eCDT_None = 0,
    eCDT_pvpReady,
    eCDT_pvp,
}

public class sdPVPManager : Singleton<sdPVPManager>
{
    public static float ms_ReadyTime = 10.0f;
    public static float ms_pkTime = 120.0f;
    public uint nWin;   		//赢的次数aaa
    public uint nLose;  		//输的次数aaa 
    public uint nRank;			//排名aaa
    public uint nChallenge;     //次数aaa
    public uint nReputation;	//声望值aaaa
    public uint nDayReputation;  //
    public uint nScore;         //积分aaa
    public int nMilitaryLevel = 0; //军阶等级aaa  
    public int m_ChallengeBuyLeft; //可购买的剩余挑战次数aaa

    public bool mMilitaryRewards;  //军阶奖励是否领取  true 领取aaa
    public uint  mRewardFlag = 0;  //声望奖励是否领取  aaa
    public bool m_bRequestMatch = true; //
    public bool m_bMilitaryLevelUp = false; // 是否升级
    public bool m_bPVPSuspend = false;  //周日22：00-周一 8：00 停止pvp战场aaa
    public long m_nTimeTick = 0; //单位秒aaa
    sdOtherPVPChar m_pvpRival = null;
    sdFightUi m_Fightui = null;
    public byte m_buyWndType = 0;//购买窗口类型 0 pvp ，1 战魂试炼aaa
	// PVP角色标准基础属性表aa
	protected Hashtable mPVPBaseProperty = null;
	public Hashtable PVPBaseProperty
	{
		get { return mPVPBaseProperty; }
	}

	// PVP角色标准装备表aa
	protected Hashtable mPVPItemProperty = null;
	public Hashtable PVPItemProperty
	{
		get { return mPVPItemProperty; }
	}

	// PVP角色标准技能表aa
	protected Hashtable mPVPSkillProperty = null;
	public Hashtable PVPSkillProperty
	{
		get { return mPVPSkillProperty; }
	}

	// PVP角色宠物列表aa
	protected Hashtable mPetList = new Hashtable();
	protected Hashtable mActivePet = null;

    public float m_fCountDown = 0.0f;
    public eCountDownType m_eCountDownType = eCountDownType.eCDT_None;

    private Vector3 mOtherBirthPoint = Vector3.zero;

    private Hashtable m_reputationRewards = new Hashtable();

    public bool mbWin = false;

    public int nJiesuan_score = 0;

    public uint nJiesuan_reputation = 0;

	// 保存PVP角色信息aa
	public void SetPVPRoleInfo(CliProto.SC_ENTER_PVP_ACK kRoleInfo)
	{
		mPVPBaseProperty = null;
		mPVPItemProperty = null;
		mPVPSkillProperty = null;
		mPVPBaseProperty = CreateBasePropertyTable(kRoleInfo.m_BasePro);
		mPVPItemProperty = CreateItemPropertyTable(kRoleInfo.m_Equip);
		mPVPSkillProperty = CreateSkillPropertyTable(kRoleInfo.m_Skill);

		mActivePet = null;
		mPetList.Clear();
		for (int i = 0; i < kRoleInfo.m_Pet.m_PetCount; ++i)
		{
			CliProto.SPetInfo kPetInfo = kRoleInfo.m_Pet.m_PetsInfo[i];
			mPetList[kPetInfo.m_DBID] = CreatePetPropertyTable(kPetInfo);
		}
	}

	// 创建角色属性表aa
	protected Hashtable CreateBasePropertyTable(CliProto.SC_SELF_BASE_PRO kBaseProperty)
	{
		Hashtable kTable = new Hashtable();

		kTable["HurtOtherModify"] = 0;
		kTable["PhysicsModify"] = 0;
		kTable["IceModify"] = 0;
		kTable["FireModify"] = 0;
		kTable["PoisonModify"] = 0;
		kTable["ThunderModify"] = 0;
		kTable["StayModify"] = 0;
		kTable["HoldModify"] = 0;
		kTable["StunModify"] = 0;
		kTable["BeHurtModify"] = 0;

		MemberInfo[] kBasePropertyInfo = kBaseProperty.GetDesc();
		for (int i = 0; i < kBasePropertyInfo.Length; ++i)
		{
			object val = kBaseProperty.GetMemberValue(i);
			kTable[kBasePropertyInfo[i].name] = val;
		}

		// 角色名转换编码aa
		kTable["Name"] = System.Text.Encoding.UTF8.GetString((byte[])kTable["Name"]).Trim('\0');

		return kTable;
	}

	// 创建角色装备属性表aa
	protected Hashtable CreateItemPropertyTable(CliProto.SC_ROLE_EQUIP_NTF kItemInfoNtf)
	{
		Hashtable kTable = new Hashtable();

		for (int i = 0; i < kItemInfoNtf.m_Items.m_ItemCount; ++i)
		{
			HeaderProto.SXITEM kItemInfo = kItemInfoNtf.m_Items.m_Items[i];

			sdGameItem kItem = new sdGameItem();
			kItem.templateID = kItemInfo.m_TID;
			kItem.instanceID = kItemInfo.m_UID;
			kItem.bagIndex = 0;
			kItem.count = kItemInfo.m_CT;
			kItem.upExp = kItemInfo.m_EXP;
			kItem.upLevel = kItemInfo.m_UP;
			kItem.gemNum = kItemInfo.m_GEMCount;
			kItem.gemList = new int[kItemInfo.m_GEMCount];
			for (int j = 0; j < kItemInfo.m_GEMCount; ++j)
			{
				kItem.gemList[j] = kItemInfo.m_GEM[j].m_TID;
			}

			Hashtable kItemBaseInfo = sdConfDataMgr.Instance().GetItemById(kItemInfo.m_TID.ToString());
			if (kItemBaseInfo != null)
			{
				kItem.mdlPath = kItemBaseInfo["Filename"].ToString();
				kItem.mdlPartName = kItemBaseInfo["FilePart"].ToString();
				kItem.anchorNodeName = sdGameActor.WeaponDummy(kItemBaseInfo["Character"].ToString());
				kItem.itemClass = int.Parse(kItemBaseInfo["Class"].ToString());
				kItem.subClass = int.Parse(kItemBaseInfo["SubClass"].ToString());
				kItem.level = int.Parse(kItemBaseInfo["NeedLevel"].ToString());
				kItem.quility = int.Parse(kItemBaseInfo["Quility"].ToString());
				kItem.equipPos = int.Parse(kItemBaseInfo["Character"].ToString());
			}

			kTable[kItemInfo.m_UID] = kItem;
		}

		return kTable;
	}

	// 创建角色技能属性表aa
	protected Hashtable CreateSkillPropertyTable(CliProto.SC_USER_SKILLS_NTF kSkillInfoNtf)
	{
		Hashtable kTable = new Hashtable();

		for (int i = 0; i < kSkillInfoNtf.m_Count; ++i)
		{
			CliProto.SSkillInfo kSkillInfo = kSkillInfoNtf.m_SkillsInfo[i];
			kTable[(int)kSkillInfo.m_SkillID] = sdGameSkillMgr.CreateSkill((int)kSkillInfo.m_SkillID);
		}

		return kTable;
	}

	// 创建宠物属性表aa
	protected Hashtable CreatePetPropertyTable(CliProto.SPetInfo kPetInfo)
	{
		Hashtable kPetPropertyTable = sdConfDataMgr.Instance().GetTable("PetProperty");
		Hashtable kPetTable = kPetPropertyTable[(int)kPetInfo.m_TemplateID] as Hashtable;
		Hashtable kLocalPetTable = sdConfDataMgr.CloneHashTable(kPetTable);
		if (kLocalPetTable != null)
		{
			MemberInfo[] akPetInfoMember = kPetInfo.GetDesc();
			for (int i = 0; i < akPetInfoMember.Length; ++i)
			{
				object kValue = kPetInfo.GetMemberValue(i);
				string kName = akPetInfoMember[i].name;
				if (kName != "Equip")
					kLocalPetTable[kName] = kValue;	//< 跳过宠物装备aa
			}

			kLocalPetTable["Enable"] = true;	//< 宠物是否可以被召唤aa
			kLocalPetTable["Active"] = false;	//< 宠物是否被激活aa
		}

		// 纠正TemplateId数据类型aa
		kLocalPetTable["TemplateID"] = (int)(uint)kLocalPetTable["TemplateID"];

		// 纠正HP和SPaa
		kLocalPetTable["HP"] = kLocalPetTable["MaxHP"];
		kLocalPetTable["SP"] = kLocalPetTable["MaxSP"];

		return kLocalPetTable;
	}

	// 激活一个可用的宠物aa
	protected void ActiveOnePet()
	{
		if (mActivePet != null)
		{
			mActivePet["Enable"] = false;
			mActivePet["Active"] = false;

			mActivePet = null;
		}

		foreach (DictionaryEntry kEntry in mPetList)
		{
			Hashtable kProperty = kEntry.Value as Hashtable;
			if (kProperty == null)
				continue;

			if (!(bool)kProperty["Enable"])
				continue;

			mActivePet = kProperty;
			break;
		}

		if (mActivePet != null)
		{
			string kPath = mActivePet["Res"] as string;
			ResLoadParams kParam = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource(kPath, OnLoadPet, kParam);

			mActivePet["Enable"] = false;
			mActivePet["Active"] = true;
		}
	}

	// 宠物加载回调aa
	protected void OnLoadPet(ResLoadParams kParam, UnityEngine.Object kObj)
	{
		if (kObj == null)
			return;

		if (m_pvpRival == null)
			return;

		Hashtable kPetProperty = mActivePet;
		if (kPetProperty == null)
			return;

		Vector3 kPlayerPos = m_pvpRival.transform.position;
		Vector3 kBornPosition = kPlayerPos;
		float fFollowDistance = ((int)(kPetProperty["FollowDistance"])) / 1000.0f;
		sdTuiTuLogic.BornPosition(kPlayerPos, fFollowDistance, ref kBornPosition);

		GameObject kPetObj = GameObject.Instantiate(kObj, kBornPosition, Quaternion.identity) as GameObject;
		sdGameMonster kActivePet = kPetObj.GetComponent<sdGameMonster>();
		if (kActivePet != null)
		{
			kActivePet.templateId = (int)kPetProperty["TemplateID"];
			kActivePet.bornLive = true;
			kActivePet.DBID = (ulong)kPetProperty["DBID"];
			kActivePet.Master = m_pvpRival;
			m_pvpRival.Retainer = kActivePet;

			kActivePet.NotifyKilled += OnPetKilled;

			kActivePet.ChangeGroupID(GroupIDType.GIT_MonsterA);

			// 宠物战队Buffaa
			List<int> kBuffList = GetPetGroupBuff(kActivePet);
			if (kBuffList != null)
			{
				foreach (int iBuffId in kBuffList)
				{
					kActivePet.AddBuff(iBuffId, 0, kActivePet);
				}
			}
		}
	}

	// 宠物被杀死回调aa
	protected void OnPetKilled(sdActorInterface kActor)
	{
		sdGameMonster kMonster = kActor as sdGameMonster;
		if (kMonster == null)
			return;

		//
		kMonster.NotifyKilled -= OnPetKilled;

		// 
		if (kMonster.Master != null)
		{
			sdGameActor kMasterActor = kMonster.Master as sdGameActor;
			kMasterActor.Retainer = null;

			kMonster.Master = null;
		}

		//
		ActiveOnePet();
	}

	// 获取指定DBID宠物的属性表aa
	public Hashtable GetPetPropertyFromDBID(UInt64 ulDBID)
	{
		return mPetList[ulDBID] as Hashtable;
	}

	// 检查指定模版的宠物在战队是否存在aa
	protected bool IsPetInPetTeam(int iPetTid)
	{
		foreach (DictionaryEntry kEntry in mPetList)
		{
			Hashtable kProperty = kEntry.Value as Hashtable;
			if (kProperty == null)
				continue;

			int iTid = (int)kProperty["TemplateID"];
			if (iTid == iPetTid)
				return true;
		}

		return false;
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

	//
    public void SetOtherBirthPoint(Vector3 value)
    {
        mOtherBirthPoint = value;
    }

    public Vector3 GetOtherBirthPoint()
    {
        return mOtherBirthPoint;
    }

    public void AddRival(stPVPRival rival)
    {
        m_Rivallist.Add(rival);
        //服务器发了5个，只显示4个，最后一个是机器人，必须要显示，所以就把最后一个非机器人干掉了aaa
        if (m_Rivallist.Count >= 5)
            m_Rivallist.RemoveAt(3);
    }

    public void AddRank(stPVPRival rank)
    {
        m_RankList.Add(rank);
    }

    protected List<stPVPRival> m_Rivallist = new List<stPVPRival>();

    protected List<stPVPRival> m_RankList = new List<stPVPRival>();

	public delegate void RefreshPKEvent();
    public event RefreshPKEvent RefreshPKData;

    public delegate void RefreshRankListEvent();
    public event RefreshRankListEvent RefreshRankListData;

    public delegate void RefreshRewardEvent();
    public event RefreshRewardEvent RefreshRewardData;

    public void RefreshRankList()
    {
        if (RefreshRankListData != null)
            RefreshRankListData();
    }

	public void RefreshPK()
	{
        if (RefreshPKData != null)
            RefreshPKData();
	}

    public void RefreshReward()
    {
        if (RefreshRewardData != null)
            RefreshRewardData();
    }
    public List<stPVPRival> GetRivallist()
    {
        return m_Rivallist;
    }
    public void ClearRival()
    {
        m_Rivallist.Clear();
    }

    public List<stPVPRival>GetRanklist()
    {
        return m_RankList;
    }
    public void ClearRank()
    {
        m_RankList.Clear();
    }

    void Update()
    {
        if (m_eCountDownType == eCountDownType.eCDT_None)
            return;
        m_fCountDown -= Time.deltaTime;
        if (m_fCountDown < 0.0f && m_eCountDownType == eCountDownType.eCDT_pvpReady)
        {
            m_fCountDown = ms_pkTime;
            m_eCountDownType = eCountDownType.eCDT_pvp;
            sdUICharacter.Instance.ShowCountDownTime2(false);
            sdUICharacter.Instance.ShowCountDownTime(true, eCountDownType.eCDT_pvp);
        }
        if (m_eCountDownType == eCountDownType.eCDT_pvp && m_fCountDown < 0.0f)
        {
            m_eCountDownType = eCountDownType.eCDT_None;
            PKStop();
            sdPVPMsg.Send_CSID_PVP_RETULT_REQ(1, 1);
        }
        if (m_Fightui == null)
        {
            //初始化血条aaa
            GameObject ui = GameObject.Find("FightUi");
            if (ui != null && m_pvpRival != null)
            {
                int iMonsterHPType = 0;
                int iHpBarNum = 1;
                int iMaxHp = m_pvpRival.GetMaxHP();
                sdUICharacter.Instance.SetMonsterMaxHp(iMonsterHPType, iMaxHp, iHpBarNum);
                m_Fightui = ui.GetComponent<sdFightUi>();
                m_Fightui.ShowMonsterHp();
                m_Fightui.SetBossName(m_pvpRival.Name);
            }
        }
        else
        {
            //血条更新
            if (m_pvpRival != null)
            {
                Hashtable uiValueDesc = new Hashtable();
                uiValueDesc["value"] = m_pvpRival.GetCurrentHP();
                uiValueDesc["des"] = "";
                sdUICharacter.Instance.SetProperty("MonsterHp", uiValueDesc);
                if (m_pvpRival.GetCurrentHP() <= 0)
                    m_Fightui.HideMonsterHp();
            }
        }
    }

    public void InitPVPRival(sdOtherPVPChar pvpChar)
    {
        if (pvpChar == null)
            return;

        m_pvpRival = pvpChar;
        m_pvpRival.NotifyKilled += KillPVPRival;

        sdGameLevel.instance.mainChar.NotifyKilled += KillMe;
        sdGameLevel.instance.FullAutoMode = false;

		// 激活宠物aa
		ActiveOnePet();
    }

	// PVP角色被杀死回调aa
    public void KillPVPRival(sdActorInterface actor)
    {
		// 禁用所有PVP宠物aa
		mActivePet = null;
		foreach (DictionaryEntry kEntry in mPetList)
		{
			Hashtable kProperty = kEntry.Value as Hashtable;
			if (kProperty == null)
				continue;

			kProperty["Enable"] = false;
		}

        sdActorInterface activePet = m_pvpRival.Retainer;
        if (activePet != null)
            activePet.AddHP(int.MinValue);

		// 清除计时aa
        m_eCountDownType = eCountDownType.eCDT_None;

        //
        PKStop();

		//
        if (m_Fightui != null)
            m_Fightui.HideMonsterHp();

		// 通知服务器aa
        sdPVPMsg.Send_CSID_PVP_RETULT_REQ(0, 0);
    }

	// 主角被杀死回调aa
    public void KillMe(sdActorInterface actor)
    {
		// 清除主角宠物aa
        sdActorInterface kActivePet = sdGameLevel.instance.mainChar.Retainer;
		if (kActivePet != null)
			kActivePet.AddHP(int.MinValue);

		// 清除计时aa
        m_eCountDownType = eCountDownType.eCDT_None;

		//
        PKStop();

		// 通知服务器aa
        sdPVPMsg.Send_CSID_PVP_RETULT_REQ(1, 0);
    }

	// 禁止双方移动和使用技能aa
    protected void PKStop()
    {
		if (m_pvpRival != null)
		{
			m_pvpRival.NotifyKilled -= sdPVPManager.Instance.KillPVPRival;

			sdActorInterface kActivePet = m_pvpRival.Retainer;
			HeaderProto.ECreatureActionState[] state = new HeaderProto.ECreatureActionState[2];
            state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY;
			state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;
			for (int i = 0; i < state.Length; i++)
			{
				if (kActivePet != null)
					kActivePet.AddDebuffState(state[i]);

				m_pvpRival.AddDebuffState(state[i]);
			}

			m_pvpRival = null;
			mActivePet = null;

			foreach (DictionaryEntry kEntry in mPetList)
			{
				Hashtable kProperty = kEntry.Value as Hashtable;
				if (kProperty == null)
					continue;

				kProperty["Enable"] = false;
			}
		}

		if (sdGameLevel.instance.mainChar != null)
		{
			sdGameLevel.instance.mainChar.NotifyKilled -= KillMe;

			sdActorInterface kActivePet = sdGameLevel.instance.mainChar.Retainer;
			HeaderProto.ECreatureActionState[] state = new HeaderProto.ECreatureActionState[2];
            state[0] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY;
			state[1] = HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL;
			for (int i = 0; i < state.Length; i++)
			{
				if (kActivePet != null)
					kActivePet.AddDebuffState(state[i]);

				sdGameLevel.instance.mainChar.AddDebuffState(state[i]);
			}
		}
    }

    public void Clear()
    {
        m_bRequestMatch = true;
        m_bMilitaryLevelUp = false;
    }
} 
