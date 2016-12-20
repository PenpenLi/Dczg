using UnityEngine;
using System.Collections;
using System;

public class sdSkillMsg : UnityEngine.Object
{
	public static bool init()
	{
		SDGlobal.Log("sdSkillMsg.init");
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_USER_SKILLS_NTF, msg_SCID_USER_SKILLS_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SKILL_INFO_NTF, msg_SCID_SKILL_INFO_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_REMOVE_SKILL_NTF, msg_SCID_REMOVE_SKILL_NTF);
		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_SKILL_POINT_NTF, msg_SCID_SKILL_POINT_NTF);
        return true;
	}
	
	private static void msg_SCID_USER_SKILLS_NTF(int iMsgID, ref CMessage msg)
	{
        sdGameSkillMgr.Instance.Clear();
		CliProto.SC_USER_SKILLS_NTF refMSG = (CliProto.SC_USER_SKILLS_NTF)msg;
		sdGameSkillMgr.Instance.SetSkillPoint((int)refMSG.m_SkillPoint);
		sdGameSkillMgr.Instance.learnedPoint = (int)refMSG.m_TotalSkillTreePoint;
		int num = refMSG.m_Count;
		for (int i = 0; i < num; i++)
		{
			sdGameSkillMgr.Instance.CreateSkill((int)refMSG.m_SkillsInfo[i].m_SkillID, (int)refMSG.m_SkillsInfo[i].m_CooldownTime);
		}
		sdGlobalDatabase.Instance.globalData["MainCharSkillInfo"] = sdGameSkillMgr.Instance.GetSkillList();
		if (sdGameLevel.instance != null)
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdSkillWnd skill = wnd.GetComponentInChildren<sdSkillWnd>();
				if (skill != null) skill.Refresh();
			}
			if (sdGameLevel.instance.mainChar != null)
			{
				sdGameLevel.instance.mainChar.SetSkillInfo(sdGlobalDatabase.Instance.globalData["MainCharSkillInfo"] as Hashtable);
			}
		}
	}
	
	private static void msg_SCID_SKILL_INFO_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SKILL_INFO_NTF refMSG = (CliProto.SC_SKILL_INFO_NTF)msg;
		SDGlobal.Log("<- SCID_SKILL_INFO_NTF : ");
		
		sdGameSkillMgr.Instance.CreateSkill((int)refMSG.m_Info.m_SkillID, (int)refMSG.m_Info.m_CooldownTime);
        sdGameSkillMgr.Instance.newSkill = (int)refMSG.m_Info.m_SkillID;
		sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Skill);
		GameObject wnd = sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdSkillWnd skill = wnd.GetComponentInChildren<sdSkillWnd>();
			if (skill != null) skill.Refresh();
		}
		
		sdUICharacter.Instance.TipNextSkill();
	}
	
	private static void msg_SCID_REMOVE_SKILL_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_REMOVE_SKILL_NTF refMsg = (CliProto.SC_REMOVE_SKILL_NTF)msg;
		sdGameSkillMgr.Instance.RemoveSkill((int)refMsg.m_SkillID);
		GameObject wnd =sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdSkillWnd skill = wnd.GetComponentInChildren<sdSkillWnd>();
			if (skill != null) skill.Refresh();
		}
	}
	
	private static void msg_SCID_SKILL_POINT_NTF(int iMsgID, ref CMessage msg)
	{
		CliProto.SC_SKILL_POINT_NTF refMsg = (CliProto.SC_SKILL_POINT_NTF)msg;
		if (!sdUICharacter.Instance.lvupChange.Contains("Skill"))
		{
			sdUICharacter.Instance.lvupChange.Add("Skill", (int)refMsg.m_SkillPoint - sdGameSkillMgr.Instance.GetSkillPoint());
		}
		else
		{
			sdUICharacter.Instance.lvupChange["Skill"] = (int)refMsg.m_SkillPoint - sdGameSkillMgr.Instance.GetSkillPoint();
		}
		sdGameSkillMgr.Instance.SetSkillPoint((int)refMsg.m_SkillPoint);
		sdGameSkillMgr.Instance.learnedPoint = (int)refMsg.m_TotalSkillTreePoint;
		sdNewInfoMgr.Instance.CreateNewInfo(NewInfoType.Type_Skill);
		GameObject wnd =sdGameLevel.instance.NGUIRoot;
		if (wnd)
		{
			sdSkillWnd skill = wnd.GetComponentInChildren<sdSkillWnd>();
			if (skill != null) skill.Refresh();
		}
		
	}
	
	public static bool notifyLearnSkill(int id)
	{
		CliProto.CS_LEARN_SKILL refMSG = new CliProto.CS_LEARN_SKILL();
		refMSG.m_SkillID = (uint)id;

		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
	
    public static bool notifyResetSkill()
    {
        CliProto.CS_SKILL_RESET_ALL_REQ refMSG = new CliProto.CS_SKILL_RESET_ALL_REQ();
        SDNetGlobal.SendMessage(refMSG);
        return true;
    }
}

