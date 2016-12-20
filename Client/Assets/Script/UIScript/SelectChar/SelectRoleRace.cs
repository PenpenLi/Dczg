using UnityEngine;
using System.Collections;

/// <summary>
/// 选择角色种族(执行脚本)
/// </summary>
public class SelectRoleRace : MonoBehaviour 
{
	public GameObject m_CreateRoleWndObject;	//< 创建角色窗口UI对象aa
	protected CreateChar mCreateChar;			//< 创建角色窗口UI脚本组件aa
	
	public int m_Race;							//< 按钮所代表的种族aa
	
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
		if( mCreateChar.mLoginChar!=null && mCreateChar.mLoginChar.IsShowAnimPlaying() )
		{
			GameObject EffectNode = sdGameLevel.instance.effectNode;//("@GameEffect");
			if( EffectNode != null )
			{
				sdAutoDestory[] effects = EffectNode.GetComponentsInChildren<sdAutoDestory>();
				foreach(sdAutoDestory e in effects)
				{
					e.Life = 0;
				}
			}
		}
		
		mCreateChar.SelectRace(m_Race);
	}
}
