using UnityEngine;
using System.Collections;

/// <summary>
/// 创建角色(执行脚本)
/// </summary>
public class CreateRole : MonoBehaviour 
{
	public GameObject m_CreateRoleWndObject;	//< 创建角色窗口UI对象aa
	protected CreateChar mCreateChar;			//< 创建角色窗口UI脚本组件aa

	public UIInput m_Username;					//< 角色名称文本框aa
	
	void Awake()
	{
		if (m_CreateRoleWndObject)	
			mCreateChar = m_CreateRoleWndObject.GetComponent<CreateChar>();
	}
	
	void Start () 
	{
	
	}

	void Update () 
	{

	}
	
	void OnClick()
	{
		if (m_Username.value.Length <= 0)
		{
			sdUICharacter.Instance.ShowOkMsg("请输入角色名!", null);
			return;
		}
		
		CliProto.CG_CREATEROLE refMSG = new CliProto.CG_CREATEROLE();
		
		int iHairStyle = 0;
		int iHairColor = 0;
		if (mCreateChar)
		{
			iHairStyle = mCreateChar.HairStyle;
			iHairColor = mCreateChar.HairColor;
		}
				
		mCreateChar.m_strNewCharName	= m_Username.value;
		refMSG.m_RoleInfo.m_RoleName	= System.Text.Encoding.UTF8.GetBytes( m_Username.value );
		refMSG.m_RoleInfo.m_Level		= 1;
		refMSG.m_RoleInfo.m_SkinColor	= (byte)iHairColor;
		refMSG.m_RoleInfo.m_HairStyle	= (byte)iHairStyle;
		
		int iCurrentRace = 0;
		if (mCreateChar)
			iCurrentRace = mCreateChar.SelectedRace;
			
		if (iCurrentRace == 0 )
		{
			refMSG.m_RoleInfo.m_Job		= (byte)HeaderProto.ERoleJob.ROLE_JOB_Warrior;
			refMSG.m_RoleInfo.m_Gender	= 0;
		}
		else if (iCurrentRace == 1)
		{
			refMSG.m_RoleInfo.m_Job		= (byte)HeaderProto.ERoleJob.ROLE_JOB_Magic;
			refMSG.m_RoleInfo.m_Gender	= 1;
		}
		else if (iCurrentRace == 2)
		{
			refMSG.m_RoleInfo.m_Job		= (byte)HeaderProto.ERoleJob.ROLE_JOB_Rogue;
			refMSG.m_RoleInfo.m_Gender	= 0;
		}
		else if (iCurrentRace == 3)
		{
			refMSG.m_RoleInfo.m_Job		= (byte)HeaderProto.ERoleJob.ROLE_JOB_Minister;
			refMSG.m_RoleInfo.m_Gender	= 1;
		}
		
		SDNetGlobal.SendMessage(refMSG);
	}
}
