using UnityEngine;
using System.Collections;

/// <summary>
/// 主角技能管理器aa
/// </summary>
public class sdGameSkillMgr : Singleton<sdGameSkillMgr>
{
	private Hashtable skillInfo = new Hashtable();
	private int skillPoint = 0;
	public int learnedPoint = 0;
    public int newSkill = 0;
	
	// 创建技能aa
	public void CreateSkill(int tid, int coolDown)
	{
		sdGameSkill kSkill = GetSkill(tid);
		if (kSkill == null)
		{
			kSkill = CreateSkill(tid);
			skillInfo[tid] = kSkill;
		}
	}
	
	// 移除技能aa
	public void RemoveSkill(int tid)
	{
		skillInfo.Remove(tid);
	}
	
	public void SetSkillPoint(int point)
	{
		skillPoint = point;
	}
	
	public int GetSkillPoint()
	{
		return skillPoint;
	}
	
	public int GetTotalPoint()
	{
		return learnedPoint;	
	}
	
	public sdGameSkill GetSkill(int tid)
	{
		if (skillInfo[tid] != null)
		{
			return (sdGameSkill)skillInfo[tid];	
		}
		else
		{
			return null;	
		}
	}
	
	public Hashtable GetSkillList()
	{
		return skillInfo;
	}
	
	public sdGameSkill GetSkillByClassId(string classId)
	{
		foreach(DictionaryEntry item in skillInfo)
		{
			sdGameSkill skill = item.Value as sdGameSkill;
			if (skill != null && skill.classId.ToString() == classId)
			{
				return skill;
			}
		}
		
		return null;
	}

    public void Clear()
    {
        skillInfo.Clear();
    }

	// 创建技能aa
	public static sdGameSkill CreateSkill(int iTemplateId)
	{
		sdGameSkill kSkill = new sdGameSkill();
		kSkill.templateID = iTemplateId;

		Hashtable kSkillInfo = sdConfDataMgr.Instance().GetSkill(iTemplateId.ToString());
		if (kSkillInfo != null)
		{
			if (kSkillInfo["Index"].ToString() != "")
				kSkill.index = int.Parse(kSkillInfo["Index"].ToString());

			if (kSkillInfo["dwClassID"].ToString() != "")
				kSkill.classId = int.Parse(kSkillInfo["dwClassID"].ToString());

			if (kSkillInfo["dwCooldown"].ToString() != "")
				kSkill.coolDown = int.Parse(kSkillInfo["dwCooldown"].ToString());

			if (kSkillInfo["byLevel"].ToString() != "")
				kSkill.level = int.Parse(kSkillInfo["byLevel"].ToString());

			if (kSkillInfo["NextLevel"].ToString() != "")
				kSkill.nextlv = int.Parse(kSkillInfo["NextLevel"].ToString());

			if (kSkillInfo["byIsPassive"].ToString() != "")
				kSkill.isPassive = int.Parse(kSkillInfo["byIsPassive"].ToString()) == 1 ? true : false;
		}

		return kSkill;
	}
}
