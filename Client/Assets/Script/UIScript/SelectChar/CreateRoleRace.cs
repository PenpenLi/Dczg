using UnityEngine;
using System.Collections;

/// <summary>
/// 创建指定种族角色(执行脚本)
/// </summary>
public class CreateRoleRace : MonoBehaviour 
{
	public GameObject m_CreateRoleWndObject;	//< 创建角色窗口UI对象aa
	protected CreateChar mCreateChar;			//< 创建角色窗口UI脚本组件aa

	public GameObject	m_IntroWarrior;		//< 种族介绍UIaa
	public GameObject	m_IntroMage;		//< 
	public GameObject	m_IntroRanger;		//< 
	public GameObject	m_IntroCleric;		//< 
	
	public UIButton		m_btWarrior;		//< 种族介绍UIaa
	public UIButton		m_btMage;			//<
	public UIButton		m_btRanger;			//<
	public UIButton		m_btCleric;			//<
	
	static Vector3 m_UnselectScale = new Vector3(0.7f, 0.7f, 0.7f);
	static Color m_UnselectColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
	
	static bool m_isLeaveCreateChar1	= false;
	
	
	void Awake()
	{
		if (m_CreateRoleWndObject)	
			mCreateChar = m_CreateRoleWndObject.GetComponent<CreateChar>();
	}
	
	void Start() 
	{

	}

	void Update() 
	{
		if( m_isLeaveCreateChar1==true && mCreateChar && mCreateChar.IsLeaveCreateUIFinish() )
		{
			mCreateChar.EnterCreateUI2(mCreateChar.SelectedRace);
			m_isLeaveCreateChar1 = false;
		}
	}
	
	void OnClick()
	{
		if (mCreateChar != null)
		{
			mCreateChar.StartLeaveCreateUI();
			m_isLeaveCreateChar1 = true;
		}
	}
	
	// 更新角色种族按钮效果aa
	public void SelectRace(int iRace)
	{	
		// 隐藏种族介绍页签aa
		m_IntroWarrior.SetActive(false);
		m_IntroMage.SetActive(false);
		m_IntroRanger.SetActive(false);
		m_IntroCleric.SetActive(false);
		
		// 恢复种族按钮缩放比例aa
		m_btWarrior.transform.localScale	= m_UnselectScale;
		m_btMage.transform.localScale		= m_UnselectScale;
		m_btRanger.transform.localScale		= m_UnselectScale;
		m_btCleric.transform.localScale		= m_UnselectScale;
		
		// 恢复种族按钮颜色aa
		//m_btWarrior.GetComponentInChildren<UISprite>().color	= m_UnselectColor;
		//m_btMage.GetComponentInChildren<UISprite>().color		= m_UnselectColor;
		//m_btRanger.GetComponentInChildren<UISprite>().color	= m_UnselectColor;
		//m_btCleric.GetComponentInChildren<UISprite>().color	= m_UnselectColor;
				
		// 处理选中种族aa
		if( iRace == 0 ) 
		{
			m_IntroWarrior.SetActive(true);
			m_btWarrior.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			//m_btWarrior.GetComponentInChildren<UISprite>().color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
		else if( iRace == 1 ) 
		{
			m_IntroMage.SetActive(true);
			m_btMage.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			//m_btMage.GetComponentInChildren<UISprite>().color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
		else if( iRace == 2 ) 
		{
			m_IntroRanger.SetActive(true);
			m_btRanger.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			//m_btRanger.GetComponentInChildren<UISprite>().color = new Color(1.0f,1.0f,1.0f,1.0f);			
		}
		else if( iRace == 3 ) 
		{
			m_IntroCleric.SetActive(true);
			m_btCleric.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			//m_btCleric.GetComponentInChildren<UISprite>().color = new Color(1.0f,1.0f,1.0f,1.0f);			
		}
	}
}
