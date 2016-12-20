using UnityEngine;
using System.Collections;

/// <summary>
/// 返回按钮执行脚本aa
/// </summary>
public class BackSelectRole : MonoBehaviour 
{
	public GameObject m_CreateRoleWndObject;	//< 创建角色窗口UI对象aa
	protected CreateChar mCreateChar;			//< 创建角色窗口UI脚本组件aa
	
	static bool m_isLeaveCreateChar1	= false;
	static bool m_isLeaveCreateChar2	= false;
	
	
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
		if( m_isLeaveCreateChar1==true && mCreateChar && mCreateChar.IsLeaveCreateUIFinish() )
		{
			mCreateChar.LeaveCreateUI();							//< 离开创建角色界面aa
				
			GameObject kSelectWndObject = mCreateChar.m_SelectRoleWndObject;
			if (kSelectWndObject)
			{
				SelectChar kSelectChar = kSelectWndObject.GetComponent<SelectChar>();
				if (kSelectChar)
					kSelectChar.EnterSelectUI();					//< 回到选择角色界面aa
			}
			
			m_isLeaveCreateChar1 = false;
		}
		
		if( m_isLeaveCreateChar2==true && mCreateChar && mCreateChar.IsLeaveCreateUIFinish2() )
		{
			mCreateChar.m_ReturnModel = true;
			mCreateChar.EnterCreateUI1(mCreateChar.SelectedRace);	//< 回到创建角色第一步界面aa
			m_isLeaveCreateChar2 = false;
		}
	}
	
	void OnClick ()
	{
		if( gameObject.name=="bt_back_rolecreate" )
		{
			if (mCreateChar)
			{
				mCreateChar.StartLeaveCreateUI2();
				m_isLeaveCreateChar2 = true;
			}
		}
		else if( gameObject.name=="bt_back_roleselect" )
		{
			if (mCreateChar)
			{
				mCreateChar.StartLeaveCreateUI();
				m_isLeaveCreateChar1 = true;
			}
		}
	}
}
