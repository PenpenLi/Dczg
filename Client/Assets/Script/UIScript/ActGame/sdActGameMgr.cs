using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAbyssLockInfo
{
	public	UInt64  m_ActDBID;		/// 活动dbid..
	public	uint	m_ActTmpId;		/// 活动配置模板的index..
	public	int		m_Blood;		/// 怪物血量..
	public	UInt64  m_Roleid;		/// 拥有者的角色id ..
	public	string	m_RoleName;		/// 拥有者的角色名..
	public	int  	m_Atkcount;		/// 攻击者数量..
	public	UInt64  [] m_Atklist;	/// 攻击深渊的角色id列表..
	public	int     m_Status;		/// 状态 0-初始 1-触发 2-打开 3-被杀死 4-逃跑..
	public	UInt64  m_Tritime;		/// 触发时间..
	public	UInt64  m_Opentime;		/// 打开时间..
	public	UInt64  m_Killtime;		/// 杀死时间..
	public	UInt64  m_EntranceExistTime;/// 触发的入口存在的时间..
	public	UInt64  m_AbyssExistTime;	/// 打开的入口存在时间..
	
	public SAbyssLockInfo()
	{
		m_ActDBID = UInt64.MaxValue;
		m_ActTmpId = 0;
		m_Blood = 0;
		m_Roleid = UInt64.MaxValue;
		m_RoleName = "";
		m_Atkcount = 0;
		m_Atklist=new UInt64[(int)HeaderProto.MAX_FRIEND_COUNT];
		m_Status = 0;
		m_Tritime = UInt64.MaxValue;
		m_Opentime = UInt64.MaxValue;
		m_Killtime = UInt64.MaxValue;
		m_EntranceExistTime = UInt64.MaxValue;
		m_AbyssExistTime = UInt64.MaxValue;
	}
}

public class SAbyssOpenRecord
{
	public	UInt64           m_Abydbid;	/// 深渊dbid         .
	public	UInt64           m_AbyTmpid;
	public	UInt64           m_Opentime;
	public	UInt64           m_Roleid;
	public	string           m_Rolename;

	public SAbyssOpenRecord()
	{
		m_Abydbid = UInt64.MaxValue;
		m_AbyTmpid = UInt64.MaxValue;
		m_Opentime = UInt64.MaxValue;
		m_Roleid = UInt64.MaxValue;
		m_Rolename = "";
	}
};

// 活动管理..
public class sdActGameMgr : Singleton<sdActGameMgr>
{
	// 深渊可开启列表..
	public Hashtable m_LapBossLockInfo = new Hashtable();
	// 深渊可进入列表..
	public Hashtable m_LapBossEnterInfo = new Hashtable();
	// 深渊开启记录列表..
	public Hashtable m_LapBossRecordInfo = new Hashtable();

	// 进入深渊时BOSS的血量..
	public int m_uuLapBossLastBlood = 0;
	public int m_uuLapBossNowBlood = 0;

	// 是否触发深渊..
	public bool m_bTiggerLapBossWnd = false;

	//世界BOSS相关....
	public HeaderProto.SWorldBossInfo m_WorldBossInfo = new HeaderProto.SWorldBossInfo();
	public int m_iWBMyBuff = 0;

	public int m_uuWorldBossLastBlood = 0;
	public int m_uuWorldBossNowBlood = 0;

	//初始化数据..
	public void ClearData()
	{
		m_LapBossLockInfo.Clear();
		m_LapBossEnterInfo.Clear();
		m_LapBossRecordInfo.Clear();
		m_uuLapBossLastBlood = 0;
		m_uuLapBossNowBlood = 0;
		m_bTiggerLapBossWnd = false;

		//世界BOSS数据..
		ResetWorldBossInfo();
	}

	public SAbyssLockInfo GetLockInfo(UInt64 uuDBID)
	{
		if (m_LapBossLockInfo[uuDBID] != null)
		{
			return (SAbyssLockInfo)m_LapBossLockInfo[uuDBID];	
		}
		else
		{
			return null;	
		}
	}

	public SAbyssLockInfo GetEnterInfo(UInt64 uuDBID)
	{
		if (m_LapBossEnterInfo[uuDBID] != null)
		{
			return (SAbyssLockInfo)m_LapBossEnterInfo[uuDBID];	
		}
		else
		{
			return null;	
		}
	}

	public bool SetLockExistTime(UInt64 uuDBID, UInt64 uuTime)
	{
		if (m_LapBossLockInfo[uuDBID] != null)
		{
			((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_EntranceExistTime = uuTime;
			return true;
		}
		else
		{
			return false;	
		}
	}
	
	public bool SetEnterExistTime(UInt64 uuDBID, UInt64 uuTime)
	{
		if (m_LapBossEnterInfo[uuDBID] != null)
		{
			((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_AbyssExistTime = uuTime;	
			return true;
		}
		else
		{
			return false;
		}
	}

	public SAbyssOpenRecord GetRecordInfo(UInt64 uuDBID)
	{
		if (m_LapBossRecordInfo[uuDBID] != null)
		{
			return (SAbyssOpenRecord)m_LapBossRecordInfo[uuDBID];	
		}
		else
		{
			return null;	
		}
	}

	public void ResetLapBossLockInfo(CliProto.SC_GET_ABYSS_TRIGGER_LIST_ACK msg)
	{
		m_LapBossLockInfo.Clear();

		CliProto.SC_GET_ABYSS_TRIGGER_LIST_ACK refMSG = msg;
		int iCount = (int)refMSG.m_Info.m_AbyssCount;
		for (int i=0; i<iCount; i++)
		{
			UInt64 uuDBID = refMSG.m_Info.m_AbyssInfo[i].m_ActDBID;
			if (uuDBID!=UInt64.MaxValue)
			{
				SAbyssLockInfo info = new SAbyssLockInfo();

				info.m_ActDBID = refMSG.m_Info.m_AbyssInfo[i].m_ActDBID;
				info.m_ActTmpId = refMSG.m_Info.m_AbyssInfo[i].m_ActTmpId;
				info.m_Blood = refMSG.m_Info.m_AbyssInfo[i].m_Blood;
				info.m_Roleid = refMSG.m_Info.m_AbyssInfo[i].m_Roleid;
				info.m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo[i].m_Rolename);
				int iAtkcount = (int)refMSG.m_Info.m_AbyssInfo[i].m_Atkcount;
				info.m_Atkcount = iAtkcount;
				for (int j=0; j<iAtkcount; j++)
				{
					info.m_Atklist[j] = refMSG.m_Info.m_AbyssInfo[i].m_Atklist[j];
				}
				info.m_Status = refMSG.m_Info.m_AbyssInfo[i].m_Status;
				info.m_Tritime = refMSG.m_Info.m_AbyssInfo[i].m_Tritime;
				info.m_Opentime = refMSG.m_Info.m_AbyssInfo[i].m_Opentime;
				info.m_Killtime = refMSG.m_Info.m_AbyssInfo[i].m_Killtime;
				info.m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo[i].m_EntranceExistTime;
				info.m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo[i].m_AbyssExistTime;
				
				m_LapBossLockInfo[uuDBID] = info;
			}
		}
	}
	
	public void SetLapBossLockInfo(CliProto.SC_ABYSS_TRIGGER_ACK msg)
	{
		CliProto.SC_ABYSS_TRIGGER_ACK refMSG = msg;
		UInt64 uuDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_LapBossLockInfo[uuDBID] != null)
			{
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				((SAbyssLockInfo)m_LapBossLockInfo[uuDBID]).m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
			}
			else
			{
				SAbyssLockInfo info = new SAbyssLockInfo();
				
				info.m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				info.m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				info.m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				info.m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				info.m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				info.m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					info.m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				info.m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				info.m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				info.m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				info.m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				info.m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				info.m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
				
				m_LapBossLockInfo[uuDBID] = info;
			}
		}
	}

	public void ResetLapBossEnterInfo(CliProto.SC_GET_ABYSS_OPEN_LIST_ACK msg)
	{
		m_LapBossEnterInfo.Clear();
		
		CliProto.SC_GET_ABYSS_OPEN_LIST_ACK refMSG = msg;
		int iCount = (int)refMSG.m_Info.m_AbyssCount;
		for (int i=0; i<iCount; i++)
		{
			UInt64 uuDBID = refMSG.m_Info.m_AbyssInfo[i].m_ActDBID;
			if (uuDBID!=UInt64.MaxValue)
			{
				SAbyssLockInfo info = new SAbyssLockInfo();
				
				info.m_ActDBID = refMSG.m_Info.m_AbyssInfo[i].m_ActDBID;
				info.m_ActTmpId = refMSG.m_Info.m_AbyssInfo[i].m_ActTmpId;
				info.m_Blood = refMSG.m_Info.m_AbyssInfo[i].m_Blood;
				info.m_Roleid = refMSG.m_Info.m_AbyssInfo[i].m_Roleid;
				info.m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo[i].m_Rolename);
				int iAtkcount = (int)refMSG.m_Info.m_AbyssInfo[i].m_Atkcount;
				info.m_Atkcount = iAtkcount;
				for (int j=0; j<iAtkcount; j++)
				{
					info.m_Atklist[j] = refMSG.m_Info.m_AbyssInfo[i].m_Atklist[j];
				}
				info.m_Status = refMSG.m_Info.m_AbyssInfo[i].m_Status;
				info.m_Tritime = refMSG.m_Info.m_AbyssInfo[i].m_Tritime;
				info.m_Opentime = refMSG.m_Info.m_AbyssInfo[i].m_Opentime;
				info.m_Killtime = refMSG.m_Info.m_AbyssInfo[i].m_Killtime;
				info.m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo[i].m_EntranceExistTime;
				info.m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo[i].m_AbyssExistTime;
				
				m_LapBossEnterInfo[uuDBID] = info;
			}
		}
	}

	public void SetLapBossEnterAckInfo(CliProto.SC_ABYSS_OPEN_ACK msg)
	{
		CliProto.SC_ABYSS_OPEN_ACK refMSG = msg;
		UInt64 uuDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_LapBossEnterInfo[uuDBID] != null)
			{
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
			}
			else
			{
				SAbyssLockInfo info = new SAbyssLockInfo();
				
				info.m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				info.m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				info.m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				info.m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				info.m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				info.m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					info.m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				info.m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				info.m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				info.m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				info.m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				info.m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				info.m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
				
				m_LapBossEnterInfo[uuDBID] = info;
			}

			//深渊入口列表中的要删除..
			if(m_LapBossLockInfo.ContainsKey(uuDBID))
				m_LapBossLockInfo.Remove(uuDBID);
		}
	}

	public void SetLapBossEnterNtfInfo(CliProto.SC_ABYSS_OPEN_NTF msg)
	{
		CliProto.SC_ABYSS_OPEN_NTF refMSG = msg;
		UInt64 uuDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
		if(uuDBID != UInt64.MaxValue)
		{
			if (m_LapBossEnterInfo[uuDBID] != null)
			{
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				((SAbyssLockInfo)m_LapBossEnterInfo[uuDBID]).m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
			}
			else
			{
				SAbyssLockInfo info = new SAbyssLockInfo();
				
				info.m_ActDBID = refMSG.m_Info.m_AbyssInfo.m_ActDBID;
				info.m_ActTmpId = refMSG.m_Info.m_AbyssInfo.m_ActTmpId;
				info.m_Blood = refMSG.m_Info.m_AbyssInfo.m_Blood;
				info.m_Roleid = refMSG.m_Info.m_AbyssInfo.m_Roleid;
				info.m_RoleName = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_AbyssInfo.m_Rolename);
				int iCount = (int)refMSG.m_Info.m_AbyssInfo.m_Atkcount;
				info.m_Atkcount = iCount;
				for (int i=0; i<iCount; i++)
				{
					info.m_Atklist[i] = refMSG.m_Info.m_AbyssInfo.m_Atklist[i];
				}
				info.m_Status = refMSG.m_Info.m_AbyssInfo.m_Status;
				info.m_Tritime = refMSG.m_Info.m_AbyssInfo.m_Tritime;
				info.m_Opentime = refMSG.m_Info.m_AbyssInfo.m_Opentime;
				info.m_Killtime = refMSG.m_Info.m_AbyssInfo.m_Killtime;
				info.m_EntranceExistTime = refMSG.m_Info.m_AbyssInfo.m_EntranceExistTime;
				info.m_AbyssExistTime = refMSG.m_Info.m_AbyssInfo.m_AbyssExistTime;
				
				m_LapBossEnterInfo[uuDBID] = info;
			}
		}
	}

	public void ResetLapBossOpenRecordInfo(CliProto.SC_GET_ABYSS_OPEN_REC_ACK msg)
	{
		m_LapBossRecordInfo.Clear();
		
		CliProto.SC_GET_ABYSS_OPEN_REC_ACK refMSG = msg;
		int iCount = (int)refMSG.m_Info.m_Count;
		for (int i=0; i<iCount; i++)
		{
			UInt64 uuDBID = refMSG.m_Info.m_Records[i].m_Abydbid;
			if (uuDBID!=UInt64.MaxValue)
			{
				SAbyssOpenRecord info = new SAbyssOpenRecord();
				info.m_Abydbid = refMSG.m_Info.m_Records[i].m_Abydbid;
				info.m_AbyTmpid = refMSG.m_Info.m_Records[i].m_AbyTmpid;
				info.m_Opentime = refMSG.m_Info.m_Records[i].m_Opentime;
				info.m_Roleid = refMSG.m_Info.m_Records[i].m_Roleid;
				info.m_Rolename = System.Text.Encoding.UTF8.GetString(refMSG.m_Info.m_Records[i].m_Rolename);
				
				m_LapBossRecordInfo[uuDBID] = info;
			}
		}
	}

	//世界BOSS..
	//数据初始化..
	public void ResetWorldBossInfo()
	{
		m_WorldBossInfo.m_WBDBID = UInt64.MaxValue;
		m_WorldBossInfo.m_WBTmpId = 0;
		m_WorldBossInfo.m_TotalNum = 0;
		m_WorldBossInfo.m_Blood = 0;
		m_WorldBossInfo.m_BossBuff.m_Percent = 0;
		m_WorldBossInfo.m_BossBuff.m_BuffID = 0;
		m_WorldBossInfo.m_Atkcount = 0;
		m_WorldBossInfo.m_AtkInfo.m_Buf = 0;
		m_WorldBossInfo.m_AtkInfo.m_BufId = 0;
		m_WorldBossInfo.m_AtkInfo.m_Dmg = 0;
		m_WorldBossInfo.m_AtkInfo.m_Id = 0;
		m_WorldBossInfo.m_AtkInfo.m_Nm = System.Text.Encoding.UTF8.GetBytes("");
		m_WorldBossInfo.m_AtkInfo.m_Rank = 0;
		m_WorldBossInfo.m_Status = 0;
		m_WorldBossInfo.m_StartTime = 0;
		m_WorldBossInfo.m_EndTime = 0;
		m_WorldBossInfo.m_Killtime = 0;
		m_WorldBossInfo.m_ReliveTime = 0;
		m_WorldBossInfo.m_NextTime = 0;
		m_WorldBossInfo.m_Time = 0;

		m_iWBMyBuff = 0;
		m_uuWorldBossLastBlood = 0;
		m_uuWorldBossNowBlood = 0;
	}

	//数据填充..
	public void BuildWorldBossInfo(HeaderProto.SWorldBossInfo bossInfo)
	{
		m_WorldBossInfo.m_WBDBID = bossInfo.m_WBDBID;
		m_WorldBossInfo.m_WBTmpId = bossInfo.m_WBTmpId;
		m_WorldBossInfo.m_TotalNum = bossInfo.m_TotalNum;
		m_WorldBossInfo.m_Blood = bossInfo.m_Blood;
		m_WorldBossInfo.m_BossBuff.m_Percent = bossInfo.m_BossBuff.m_Percent;
		m_WorldBossInfo.m_BossBuff.m_BuffID = bossInfo.m_BossBuff.m_BuffID;
		m_WorldBossInfo.m_Atkcount = bossInfo.m_Atkcount;

		for (int i = 0; i<bossInfo.m_Atkcount; i++)
		{
			if (m_WorldBossInfo.m_Atklist[i]==null)
			{
				m_WorldBossInfo.m_Atklist[i] = new HeaderProto.WBAtkInfo();
			}
			m_WorldBossInfo.m_Atklist[i].m_Buf = bossInfo.m_Atklist[i].m_Buf;
			m_WorldBossInfo.m_Atklist[i].m_BufId = bossInfo.m_Atklist[i].m_BufId;
			m_WorldBossInfo.m_Atklist[i].m_Dmg = bossInfo.m_Atklist[i].m_Dmg;
			m_WorldBossInfo.m_Atklist[i].m_Id = bossInfo.m_Atklist[i].m_Id;
			m_WorldBossInfo.m_Atklist[i].m_Nm = bossInfo.m_Atklist[i].m_Nm;
			m_WorldBossInfo.m_Atklist[i].m_Rank = bossInfo.m_Atklist[i].m_Rank;
		}
		m_WorldBossInfo.m_AtkInfo.m_Buf = bossInfo.m_AtkInfo.m_Buf;
		m_iWBMyBuff = bossInfo.m_AtkInfo.m_Buf;
		m_WorldBossInfo.m_AtkInfo.m_BufId = bossInfo.m_AtkInfo.m_BufId;
		m_WorldBossInfo.m_AtkInfo.m_Dmg = bossInfo.m_AtkInfo.m_Dmg;
		m_WorldBossInfo.m_AtkInfo.m_Id = bossInfo.m_AtkInfo.m_Id;
		m_WorldBossInfo.m_AtkInfo.m_Nm = bossInfo.m_AtkInfo.m_Nm;
		m_WorldBossInfo.m_AtkInfo.m_Rank = bossInfo.m_AtkInfo.m_Rank;
		m_WorldBossInfo.m_Status = bossInfo.m_Status;
		m_WorldBossInfo.m_StartTime = bossInfo.m_StartTime;
		m_WorldBossInfo.m_EndTime = bossInfo.m_EndTime;
		m_WorldBossInfo.m_Killtime = bossInfo.m_Killtime;
		m_WorldBossInfo.m_ReliveTime = bossInfo.m_ReliveTime;
		m_WorldBossInfo.m_NextTime = bossInfo.m_NextTime;
		m_WorldBossInfo.m_Time = bossInfo.m_Time;
	}

	//取得世界BOSS场景ID..
	public int GetWorldBossLevelID()
	{
		int iLevelID = 0;
		if (m_WorldBossInfo!=null)
		{
			int iTmpId = (int)m_WorldBossInfo.m_WBTmpId;
			Hashtable wbTable = sdConfDataMgr.Instance().GetWorldBossTemplatesTable();
			if (wbTable!=null && iTmpId>0)
			{
				foreach(DictionaryEntry item in wbTable)
				{
					Hashtable boss = (Hashtable)item.Value;
					int id = int.Parse(item.Key as string);
					if(id == iTmpId)
					{
						iLevelID = int.Parse(boss["LevelID"].ToString());
						break;
					}
				}
			}
		}

		return iLevelID;
	}

	//取得世界属性..
	public string GetWorldBossPramStr(string pramStr)
	{
		string str = "";
		if (m_WorldBossInfo!=null)
		{
			int iTmpId = (int)m_WorldBossInfo.m_WBTmpId;
			Hashtable wbTable = sdConfDataMgr.Instance().GetWorldBossTemplatesTable();
			if (wbTable!=null && iTmpId>0)
			{
				foreach(DictionaryEntry item in wbTable)
				{
					Hashtable boss = (Hashtable)item.Value;
					int id = int.Parse(item.Key as string);
					if(id == iTmpId)
					{
						str = boss[pramStr].ToString();
						break;
					}
				}
			}
		}
		
		return str;
	}
}
