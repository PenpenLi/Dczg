using UnityEngine;
using System.Collections;

/// <summary>
/// 角色删除按钮执行脚本
/// </summary>
public class DeleteRole : MonoBehaviour 
{	
	public SelectRole baseUI;
	
	void OnClick()
	{
		if (baseUI.m_CurrentSelect < 0) 
			return;
		
		sdUICharacter.Instance.ShowOkCanelMsg("确认删除?", MsgBoxOK, null);
	}
	
	protected void MsgBoxOK()
	{
		if (baseUI.m_CurrentSelect < 0) 
			return;
		
		// 向服务器请求删除角色
		CliProto.CG_DELROLE refMSG = new CliProto.CG_DELROLE();
		refMSG.m_RoleDBID = SDNetGlobal.playerList[baseUI.m_CurrentSelect].mDBID;
		SDNetGlobal.SendMessage(refMSG);
		
		SDGlobal.Log(SDNetGlobal.playerList[baseUI.m_CurrentSelect].mRoleName);
	}
}
